# IP Address Changer — Outstanding Work

Snapshot of work-in-progress on the DHCP server feature and related polish. The
big scaffolding is done and end-to-end working; what's left below is edge-cases,
UX polish, and docs.

## Pending tasks

### Help anchor review (all forms)

Walk every form and confirm the F1 / `helpProvider.SetHelpKeyword` anchors point
at sections that actually exist in the help doc. The DHCP work added new windows
(`frmDHCPServer`, `frmDHCPServerBusy`, `frmAddDHCPReservation`) and the existing
forms may also have stale anchors after recent refactors. The `dhcp-server-busy-dialog`
keyword in particular is already known-broken (also tracked under the Help doc
deferred bullet below).

### ~~Manual "probe for DHCP server" toolbar button (frmDHCPServer)~~

~~Add a toolbar button on the DHCP Server window that runs the same DISCOVER probe
the start-server flow runs as its pre-flight check, but on demand and without
starting the server.~~ _Done 2026-05-05 — `cmdDHCPProbe` button on frmDHCPServer; uses `DHCPPreflightDuration` regardless of `DHCPPreflightCheck`; cancellable busy dialog; informational result MessageBox._

### DHCP screenshots for the README

The README has one DHCP-server-related screenshot at most; the new windows need
fresh captures: `frmDHCPServer` (configured, server enabled, with leases and
reservations visible), `frmDHCPServerBusy` (mid-operation), `frmAddDHCPReservation`
(both add mode and edit mode), and the unhandled-exception dialogs are already
in `images/`. Save into `images/` with descriptive names (`dhcpserver.png`,
`dhcpserverbusy.png`, etc.) and reference from the README's DHCP section.

## Deferred (not started)

- ~~**README** needs significant updates to cover the DHCP server feature (configuration, lease management, help-doc anchors, screenshot).~~ _Done 2026-05-05 — DHCP Server section now covers all the windows, behaviors, and design rationale. Pending screenshots tracked separately below._
- **CHANGELOG** — single line "Added DHCP server" is sufficient.
- **Help doc** — add a section for the new help keyword `dhcp-server-busy-dialog` so F1 from the busy dialog doesn't 404.
- `frmDHCPServerBusy.Designer.cs` was hand-written; first time it's opened in the WinForms designer it may rewrite layout-percent values and re-add a duplicate `Dispose(bool)` method — if so, just delete the duplicate again.
- **Enable/disable adapter from the application** (might-do-someday). `MSFT_NetAdapter` (the same CIM class in `root\StandardCimv2` that `NetworkManager.GetAdapters` already enumerates) exposes `Enable()` and `Disable()` methods — same shape as the existing `Create`/`SetDhcp`/`Remove` invocations in NetworkManager. Add `EnableAdapterAsync(uint interfaceIndex)` and `DisableAdapterAsync(uint interfaceIndex)` plus an "Enable adapter" / "Disable adapter" item on the per-adapter context menu in `frmMain`. Caveats to design through: (1) refuse if the adapter is the DHCP server's bound adapter and the server is running (use existing `DhcpServerBoundTo` guard); (2) consider a confirmation prompt if the user is disabling the adapter they're currently using to reach the network (detectable: the disabled-target adapter's connection state was UP and there's no other UP adapter); (3) `frmAdapterBusy` flow as with other mutating ops; (4) adapter index doesn't change across disable/enable, so the application's adapter-tracking should survive cleanly.

- **ARP probe before any set-address operation** (might-do-someday). Before binding a static IP to an adapter — DHCP server start, shortcut recall, paste-address — send a gratuitous ARP for the target IP and wait briefly for a reply. If anyone answers, abort with a clear "address already in use on the network by another device" message instead of relying on Windows DAD to detect it after-the-fact. More reliable than the DHCP DISCOVER probe (which only catches DHCP servers, not arbitrary devices that happen to have the IP) and gives a faster, clearer error than the bind-failure DAD catch in `EnableDHCPServer`. Cost: requires `SendARP` P/Invoke or raw socket, plus careful interface-binding so the ARP leaves through the right adapter; would need to be threaded into the existing `ApplyAddressToAdapter` flow as well as the DHCP server start.

### Pending screenshots (not yet captured)

These would slot into existing README sections; capture when convenient.

- ~~`dhcpserverbusy.png` — `frmDHCPServerBusy` mid-operation.~~ _Captured; referenced in DHCP Server Busy Dialog section._
- ~~`dhcppreflightdetected.png` — the *"Detected N other DHCP server(s)"* MessageBox.~~ _Captured; referenced in DHCP DISCOVER Preflight Check subsection._
- ~~`dhcpserveraddressconflict.png` — the DAD-detected *"another device on the network appears to have this IP"* message.~~ _Captured; referenced in Address Conflict on Server Start subsection._
- ~~`dhcpoutofrange.png` — the *"Reservations Outside Subnet"* YesNoCancel prompt.~~ _Captured; referenced in Reservations Outside the Subnet subsection._
- ~~`dhcpserveradapterremovedwarning.png` and `dhcpserveraddressremovedwarning.png` — the two flavors of the *"DHCP Server Stopped"* MessageBox.~~ _Captured; both referenced in DHCP Server Stopped Warning subsection._

## Done (context for picking up)

Major scaffolding that's landed:

- ~~**#28 Debug window double-open exception.** `tsbDebug_Click` (`frmMain.cs:858`) calls `Show(this)` on an already-visible form, throwing `InvalidOperationException`. Fix: check `Visible` and `BringToFront`/`Activate` if already shown.~~ _Done 2025-05-04_
- ~~**#29 Reservation IP validation when server isn't running.** `TryAddReservation` only validates against `RangeStart`/`RangeEnd`, which are unset until `SetLeaseRange` runs. If the user has typed an address+prefix in `frmDHCPServer` but hasn't clicked Enable, they can still add reservations outside that subnet. Cleanest fix: have the reservation dialog accept a tentative range from the parent form and validate against it.~~
- ~~**#30 Existing leases outside new range at server start.** When the user starts the server (or stops, changes range, restarts), check whether any existing `_dhcpLeases` entries fall outside the newly-configured `[RangeStart, RangeEnd]`. If so, prompt three-way: "Clear and continue / Keep them and continue / Abort". Likely needs a `DHCPServer.GetLeasesOutsideRange()` helper plus a `ClearLeases()`. Open question: keep "Keep them and continue" or simplify to two-way?~~
- ~~**#33 Prevent shortcuts from clobbering bound DHCP adapter.** Running a shortcut on the adapter the DHCP server is bound to set the adapter to DHCP, stripped the static IP, and left the server in a zombie state. Block in `frmMain` (shortcuts, RenewDhcp, SetDhcp, ApplyAddress, per-adapter context menu) when target adapter matches `_dhcpServer.Adapter` and server is running. Either warn-and-refuse or prompt-to-stop-server-first.~~
- ~~**#34 Detect when bound DHCP adapter's IP changes and stop the server.** Defense-in-depth for #33 — even if external tooling (cmd-line netsh, USB unplug, sleep/wake, Windows resetting the adapter) strips the server's IP, the server should self-fault rather than zombie. Subscribe to `NetworkChange.NetworkAddressChanged` inside `DHCPServer`; on each event verify `Adapter` still has `Address`/`PrefixLength` bound. If not, fire a `ServerFaulted` event and `Stop()`. Form pipes `ServerFaulted` to a MessageBox.~~

- ~~**#22 Context menu (copy + delete) on lease list.** User is doing this.~~
- **#31 Edit reservation feature.** User is doing this. Likely an "Edit" toolbar button + reused `frmAddDHCPReservation` in edit mode + a `DHCPServer.UpdateReservation(oldMac, newMac, newIp)` helper.
- ~~**#32 Auto-scroll / jump-to-bottom for debug window.** `frmDebug` doesn't auto-scroll as messages arrive. Auto-scroll-with-pause is the standard pattern (engages auto when user scrolls up, disengages when they scroll back to bottom). Or just a "jump to bottom" button. Or both.~~

- `DHCP Server/DHCPPacket.cs` — RFC 2131/2132 parser/builder with magic-cookie check and convenience setters.
- `DHCP Server/DHCPServer.cs` — full server: `SetLeaseRange`, `SetBoundAdapter`, `GetNextFreeAddress` with hard-skip on server's own IP, `Start`/`Stop`/ `StopAsync`, listen loop with `IP_PKTINFO` adapter-index drop filter, all message handlers (DISCOVER/REQUEST/INFORM/DECLINE; RELEASE intentionally no-op for sticky-lease design). DISCOVER and REQUEST handlers fire the new `Log` event with decision narration ("new device, offering X" / "doesn't match our record, sending NAK" / etc.).
- `frmDHCPServerBusy` — cancellable busy dialog with thread-safe `SetStatus`, Esc/Cancel/[X] all funnel to one cancel path.
- `frmDHCPServer.cs` — full enable/disable flow with phase-by-phase status updates and cancellation checkpoints. Auto-corrects `.0` (network address) to `.1` with visible textbox update. Handles same-IP/different-prefix by removing-and-re-adding rather than aborting. Removes other IPv4 addresses after our address is up. Bind-retry loop on `SocketError.AddressNotAvailable` (250 ms × 40 attempts, ~10 s).
- `frmAddDHCPReservation` — MAC normalizer accepts colon/dash/dot/no-separator, outputs canonical `AA:BB:CC:DD:EE:FF`. Validates IP is in subnet (when range is set on the server).
- `NetworkManager.RemoveIPAddressAsync` for single-IP delete (preserves IPv6).
- `AdapterInfo` got `==`/`!=` operators delegating to existing `Equals` (DeviceID-based) so dictionary/comparison sites stay valid across refresh.
- `DHCPMessageEventArgs` carries a `Direction` (TX/RX); `DeviceCommunication` fires after sends too. `frmMain` debug log shows `DHCP TX/RX [MAC]: TYPE`. Lease-list "Last Communication" cell shows `TX: DHCPACK` / `RX: DHCPREQUEST`.
- `frmMain` subscribes to the new `DHCPServer.Log` event and pipes narration lines to the debug form, prefixed `DHCP:`.
- `frmDHCPServer_Load` repopulates address octet textboxes and prefix length from `_dhcpServer.Address` / `_dhcpServer.PrefixLength` on form reopen.

### First-use DHCP warning dialog
~~The first time a user enables the DHCP server, show a warning dialog explaining
the risks (unauthorized DHCP servers can disrupt other devices on the network,
can cause IP conflicts, may run afoul of network policy, etc.) with a "Don't
remind me again" checkbox. When checked and dismissed, set a settings flag
(e.g. `DHCPWarningAcknowledged`) so subsequent uses skip the dialog. Distinct
from the pre-flight DISCOVER probe — that's a runtime detection of an actual
conflict; this one is a policy / "are you sure you should be doing this"
reminder that fires regardless of what's on the segment.~~

### Pre-flight DISCOVER probe (designed but not built)

~~Before binding the DHCP server, send a DISCOVER on the bound adapter and listen
briefly for OFFERs. If anything answers, warn the user (there's already a DHCP
server on this segment) and let them confirm or abort. Designed as part of the
early DHCP server work; never implemented. `frmDHCPServerBusy` was made cancel-
capable specifically with this in mind — same form, different sequence of
`SetStatus` calls and one cancellable `Task.Delay` waiting for OFFERs.~~

~~Add a settings checkbox to opt out of the probe — default on, but a tech who
knowingly wants to run a second server on a segment (lab work, intentional
override) shouldn't have to dismiss the prompt every time.~~

### Prefix length validation cleanup (proposed, not yet applied)

- ~~Tighten `SetLeaseRange` and form validation to **/8..30** (current is /0..30). Below /8 there's no realistic legitimate use and `GetNextFreeAddress` would iterate millions of addresses linearly. /30 cap is fine as-is.~~
- ~~Three string/doc fixes that still reference 32: - `frmDHCPServer.Designer.cs:280` placeholder text `"32"` → suggest `"24"` or `"30"`. - `DHCPServer.cs` XML doc on `SetLeaseRange` says `"0–32"`. - `DHCPServer.cs` comment claims `/0 and /31 and /32 don't make sense` but   `/0` is currently allowed by the code; comment is wrong.~~

~~User opinion needed: "/8 lower bound — go ahead, or wider?"~~

~~Verify before working this item: the `MIN_PREFIX_LENGTH`/`MAX_PREFIX_LENGTH`
constants refactor likely already cleaned up the inline `/0 and /31 and /32` comment
(that block was replaced). The Designer placeholder `"32"` and the `SetLeaseRange`
XML doc reference to `"0–32"` may also have been fixed in passing — re-check each
of the three sub-items and strike the ones already addressed before tightening
the bound to /8.~~