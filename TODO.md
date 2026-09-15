# IP Address Changer — Outstanding Work

The DHCP server feature is done and shipped: server, lease/reservation UI, busy
dialogs, pre-flight probe, first-use warning, custom pool range, README coverage,
screenshots, help anchors. The next phase is lease lifecycle — the server
currently never expires or reclaims anything, so a lease list that's been in use
for months accumulates addresses for devices that are long gone.

## Where we left off (2026-08-21)

**Uncommitted in the working tree — nothing was committed:**

- `TODO.md` — this rewrite. Stale/completed items cleared out, lease-lifecycle
  design below captured from the design discussion.
- `src/DHCP Server/DHCPServer.cs` — orphaned `SetLeaseRange` XML doc block moved
  back onto the method; `prefixLength` wording now explains protocol bounds vs.
  the form-layer policy bounds.
- `src/frmDHCPServer.Designer.cs` — `txtPrefixLength.PlaceholderText` `"32"` →
  `"24"`; ping checkbox removed from this form (your change).
- `src/frmSettings.Designer.cs`, `src/frmSettings.resx` — `chkPingBeforeDHCPAssignment`
  + `label6` added (your change). Not wired to anything yet; see the ping section.
- `src/frmMain.cs` — prefix-range debug message said "exclusive", now "inclusive".

Build verified clean: 0 warnings, 0 errors. Note that having the app running locks
`bin\Debug\net8.0-windows\IPAddressChanger.exe`, so a normal build compiles fine
and then fails on the final copy (MSB3021/MSB3027). To compile-check without
closing it: `dotnet build src/IPAddressChanger.csproj -p:BaseOutputPath=<temp dir>`.

**Do NOT re-open the prefix-length item.** It's closed. The `/8` lower bound was
never meant to be `MIN_PREFIX_LENGTH` — that constant is the protocol clamp and
must stay `0`. The policy bound lives in the `DHCPPrefixMinLength` /
`DHCPPrefixMaxLength` settings (defaults 8/30), clamped at the form layer
(`frmDHCPServer.cs:262-263`). Raising the constant would break the documented
`user.config` override (README, "Prefix Length Policy").

**Next session, in this order:**

1. `LEASE_SECONDS` 15 min → 1 hour, plus the cull timer (section 1 below). Start
   here — it's self-contained and everything else builds on the expiry semantics.
2. Lease persistence (section 2). Has to follow the cull, because the serialized
   format needs to carry `Assigned`/`Expires` for the startup expiry pass to have
   anything to evaluate — and until the `TryAddReservation`-for-everything bug is
   fixed, restored leases would be immortal and the cull would look broken.
3. DHCPRELEASE (section 3) — small, independent, can slot in anywhere after 1.
4. Ping before assign + ghost entries. The Settings checkbox is already placed
   but disabled (`Enabled = false`); drop that when it's wired up.

Then the ARP probe as phase two, reusing the ghost-entry machinery.

## Pending tasks

### DHCP lease lifecycle: expiry, culling, persistence, RELEASE

Settled design decisions (2026-08-21):

- **Culling is automatic**, on a timer, not a manual "clean up" button. A device
  that disappears and comes back later getting a *different* address is fine.
- **Expiry alone decides.** No liveness probing of existing leases — that isn't a
  thing DHCP servers do (RFC 2131 has no such mechanism; the client proves it's
  alive by renewing at T1/T2 or it doesn't). Lease expires without renewal → the
  address is free. No grace period.
- **Expired and RELEASEd leases are removed outright** from the list, not greyed
  out or kept in an expired state.
- **Manual reservations are never culled.** They have null `Assigned`/`Expires`
  by design and are exempt, full stop.
- **`LEASE_SECONDS` goes from 15 minutes to 1 hour** (`DHCPServer.cs:24`). Long
  enough that culling at expiry isn't a hair trigger; short enough that renewals
  at T1 = 30 min keep the "Last Communication" column a live signal.

#### 1. Cull timer

- Remove expired leases from **both** `_dhcpLeases` and `_usedAddresses`.
  `GetNextFreeAddress` consults only `_usedAddresses` (`DHCPServer.cs:210`), so
  dropping the lease row alone leaves the address permanently blocked.
- Fire the lease-changed event so `frmDHCPServer`'s list updates live.
- Run the timer only while the server is running, plus one pass at start.

#### 2. Lease persistence — existing code has a bug that breaks the cull

Persistence already exists (`frmMain.cs:505-508` save, `frmMain.cs:458` restore)
but is not usable as-is:

- **Everything reloads as a reservation.** The restore path calls
  `TryAddReservation` for every saved entry (`frmMain.cs:474`), so an automatic
  lease saved under "Save reservations and leases" comes back with null
  `Assigned`/`Expires` — indistinguishable from a hand-made reservation. Since
  reservations are exempt from culling, **every automatic lease that survives a
  restart would become immortal.** Split the load path: `TryAddReservation` for
  reservations, a new timestamp-preserving call for leases.
- **The serialized format is `MAC=IP`** — `Assigned`/`Expires` are dropped, so
  there's nothing for an expiry pass to evaluate. Add the timestamps; detect the
  old 2-field shape on load and treat those entries as reservations.
- **Run an unconditional one-shot expiry pass at startup**, after the load.
- **Reconsider the `SaveDHCPLeases` default** (`Settings.settings:63`, currently
  `0` = reservations only). The reason not to save automatic leases was that they
  accumulated forever; the cull removes that reason.

Why persistence is load-bearing here, not just nice-to-have: an unknown-MAC
REQUEST is silently ignored (`HandleRequestAsync`, per RFC 2131 §4.3.2 — a server
with no record MUST stay silent, since the client may be talking to a server we
don't know about). So after a restart with no memory, the device is *not* NAKed —
it keeps renewing into the void and keeps using its address until the lease fully
expires. At 15 minutes that was a short blind spot; at 1 hour the server can hand
that same address to another device while the original is still on it.

#### 3. Implement DHCPRELEASE

Currently a deliberate no-op (`DHCPServer.cs:847`) from the sticky-lease design.
With expiry-based reclamation it should be honored: remove from `_dhcpLeases`
**and** `_usedAddresses`, fire lease-changed, send no reply. Guard it the way
`HandleDeclineAsync` already does — check the server-identifier per RFC 2131
§4.4.4, and additionally check `ciaddr` matches our record for that MAC, so a
mistaken or spoofed RELEASE can't free another device's address.

### Ping before assign, with ghost entries

`chkPingBeforeDHCPAssignment` + `label6` ("Ping before DHCP assignment") are
already placed on the Settings window (`frmSettings.Designer.cs:490`) — it's a
general policy preference, not a per-session choice — but are UI-only: no help
keyword, no load/save in `frmSettings.cs`, no entry in `Settings.settings`, no
server-side property. The checkbox is therefore shipped with `Enabled = false`
so it can't be toggled into a state that does nothing; remove that line as part
of the wiring. Optional and off-by-default matches the field (Windows
DHCP Server's conflict detection defaults to 0 attempts; ISC dhcpd's `ping-check`
uses a 1-second timeout).

- Probe with `System.Net.NetworkInformation.Ping` — fully managed, no P/Invoke,
  no CIM needed. Short timeout (~1 s).
- **The probe cannot run where the allocation runs.** `HandleDiscoverAsync` picks
  its candidate inside `lock (_leasesLock)` (`DHCPServer.cs:863`) and an async
  ping can't happen under that lock. Shape: take lock → pick candidate → release
  → ping → re-take → re-verify still free → commit. Bound the attempts (~5) so a
  busy subnet can't stall an OFFER indefinitely. Only new MACs pay the cost;
  known MACs re-offer without probing.
- Document the honest caveat in the help text: Windows blocks ICMP echo by
  default, so silence is **not** proof the address is free. Best-effort net that
  catches printers, AV gear and appliances — not a guarantee.

**Ghost entries.** A ping reply means something is squatting on a pool address.
Add a visible row for it: IP, reason, and the responder's MAC. The MAC is free —
the ping just populated the neighbor cache, so read `MSFT_NetNeighbor`
(`root\StandardCimv2`, already NetworkManager's namespace) for that IP.

Ghost entries also fix an existing wart: `HandleDeclineAsync` (`DHCPServer.cs:1000`)
already blackholes DECLINEd addresses into `_usedAddresses` for the rest of the
session with **no UI trace at all** — the address silently vanishes from the pool
and nothing says why. Render DECLINEs as ghosts too. Ghosts are exempt from the
cull, session-only (not persisted), and manually deletable — that's the retry path.

## Deferred (not started)

- **ARP probe before assign** — the follow-on to ping-before-assign, reusing the
  same ghost-entry machinery. More reliable than ICMP: ARP is answered by the IP
  stack beneath any host firewall, so it catches the manually-configured Windows
  box that ping misses. The cost is why it's phase two: there's no managed
  Windows API for *sending* ARP (the client-side probe of RFC 2131 §4.4.1 happens
  inside the client's own stack), so it needs `SendARP` P/Invoke or Npcap for raw
  layer 2 — which cuts against the CIM-only convention. Possible P/Invoke-free
  route worth prototyping first: send a throwaway UDP datagram to the target to
  force the stack's own ARP, then read `MSFT_NetNeighbor` to see whether the entry
  resolved to a MAC or went Incomplete/Unreachable. Needs a poll window and
  stale-entry handling. Also worth extending to the set-address paths (shortcut
  recall, paste-address, DHCP server start) as a clearer, faster failure than the
  bind-time DAD catch in `EnableDHCPServer`.

- **Enable/disable adapter from the application** (might-do-someday).
  `MSFT_NetAdapter` (the same CIM class in `root\StandardCimv2` that
  `NetworkManager.GetAdapters` already enumerates) exposes `Enable()` and
  `Disable()` methods — same shape as the existing `Create`/`SetDhcp`/`Remove`
  invocations in NetworkManager. Add `EnableAdapterAsync(uint interfaceIndex)`
  and `DisableAdapterAsync(uint interfaceIndex)` plus an "Enable adapter" /
  "Disable adapter" item on the per-adapter context menu in `frmMain`. Caveats to
  design through: (1) refuse if the adapter is the DHCP server's bound adapter and
  the server is running (use existing `DhcpServerBoundTo` guard); (2) consider a
  confirmation prompt if the user is disabling the adapter they're currently using
  to reach the network (detectable: the disabled-target adapter's connection state
  was UP and there's no other UP adapter); (3) `frmAdapterBusy` flow as with other
  mutating ops; (4) adapter index doesn't change across disable/enable, so the
  application's adapter-tracking should survive cleanly.
