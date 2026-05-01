# IP Address Changer — Outstanding Work

Snapshot of work-in-progress on the DHCP server feature and related polish. The
big scaffolding is done and end-to-end working; what's left below is edge-cases,
UX polish, and docs.

## Pending tasks

### DHCP server bugs / behavior

- **#28 Debug window double-open exception.** `tsbDebug_Click` (`frmMain.cs:858`)
  calls `Show(this)` on an already-visible form, throwing
  `InvalidOperationException`. Fix: check `Visible` and `BringToFront`/`Activate`
  if already shown.
- **#29 Reservation IP validation when server isn't running.** `TryAddReservation`
  only validates against `RangeStart`/`RangeEnd`, which are unset until
  `SetLeaseRange` runs. If the user has typed an address+prefix in `frmDHCPServer`
  but hasn't clicked Enable, they can still add reservations outside that subnet.
  Cleanest fix: have the reservation dialog accept a tentative range from the
  parent form and validate against it.
- **#30 Existing leases outside new range at server start.** When the user starts
  the server (or stops, changes range, restarts), check whether any existing
  `_dhcpLeases` entries fall outside the newly-configured `[RangeStart, RangeEnd]`.
  If so, prompt three-way: "Clear and continue / Keep them and continue / Abort".
  Likely needs a `DHCPServer.GetLeasesOutsideRange()` helper plus a `ClearLeases()`.
  Open question: keep "Keep them and continue" or simplify to two-way?
- **#33 Prevent shortcuts from clobbering bound DHCP adapter.** Running a shortcut
  on the adapter the DHCP server is bound to set the adapter to DHCP, stripped the
  static IP, and left the server in a zombie state. Block in `frmMain` (shortcuts,
  RenewDhcp, SetDhcp, ApplyAddress, per-adapter context menu) when target adapter
  matches `_dhcpServer.Adapter` and server is running. Either warn-and-refuse or
  prompt-to-stop-server-first.
- **#34 Detect when bound DHCP adapter's IP changes and stop the server.**
  Defense-in-depth for #33 — even if external tooling (cmd-line netsh, USB
  unplug, sleep/wake, Windows resetting the adapter) strips the server's IP,
  the server should self-fault rather than zombie. Subscribe to
  `NetworkChange.NetworkAddressChanged` inside `DHCPServer`; on each event verify
  `Adapter` still has `Address`/`PrefixLength` bound. If not, fire a
  `ServerFaulted` event and `Stop()`. Form pipes `ServerFaulted` to a MessageBox.

### DHCP server UX

- **#22 Context menu (copy + delete) on lease list.** User is doing this.
- **#31 Edit reservation feature.** User is doing this. Likely an "Edit" toolbar
  button + reused `frmAddDHCPReservation` in edit mode + a
  `DHCPServer.UpdateReservation(oldMac, newMac, newIp)` helper.
- **#32 Auto-scroll / jump-to-bottom for debug window.** `frmDebug` doesn't
  auto-scroll as messages arrive. Auto-scroll-with-pause is the standard
  pattern (engages auto when user scrolls up, disengages when they scroll back
  to bottom). Or just a "jump to bottom" button. Or both.

### Pre-flight DISCOVER probe (designed but not built)

Before binding the DHCP server, send a DISCOVER on the bound adapter and listen
briefly for OFFERs. If anything answers, warn the user (there's already a DHCP
server on this segment) and let them confirm or abort. Designed as part of the
early DHCP server work; never implemented. `frmDHCPServerBusy` was made cancel-
capable specifically with this in mind — same form, different sequence of
`SetStatus` calls and one cancellable `Task.Delay` waiting for OFFERs.

### Prefix length validation cleanup (proposed, not yet applied)

- Tighten `SetLeaseRange` and form validation to **/8..30** (current is /0..30).
  Below /8 there's no realistic legitimate use and `GetNextFreeAddress` would
  iterate millions of addresses linearly. /30 cap is fine as-is.
- Three string/doc fixes that still reference 32:
  - `frmDHCPServer.Designer.cs:280` placeholder text `"32"` → suggest `"24"` or `"30"`.
  - `DHCPServer.cs` XML doc on `SetLeaseRange` says `"0–32"`.
  - `DHCPServer.cs` comment claims `/0 and /31 and /32 don't make sense` but
    `/0` is currently allowed by the code; comment is wrong.

User opinion needed: "/8 lower bound — go ahead, or wider?"

## Deferred (not started)

- **README** needs significant updates to cover the DHCP server feature
  (configuration, lease management, help-doc anchors, screenshot).
- **CHANGELOG** — single line "Added DHCP server" is sufficient.
- **Help doc** — add a section for the new help keyword
  `dhcp-server-busy-dialog` so F1 from the busy dialog doesn't 404.
- `frmDHCPServerBusy.Designer.cs` was hand-written; first time it's opened in
  the WinForms designer it may rewrite layout-percent values and re-add a
  duplicate `Dispose(bool)` method — if so, just delete the duplicate again.

## Done (context for picking up)

Major scaffolding that's landed:

- `DHCP Server/DHCPPacket.cs` — RFC 2131/2132 parser/builder with magic-cookie
  check and convenience setters.
- `DHCP Server/DHCPServer.cs` — full server: `SetLeaseRange`, `SetBoundAdapter`,
  `GetNextFreeAddress` with hard-skip on server's own IP, `Start`/`Stop`/
  `StopAsync`, listen loop with `IP_PKTINFO` adapter-index drop filter, all
  message handlers (DISCOVER/REQUEST/INFORM/DECLINE; RELEASE intentionally no-op
  for sticky-lease design). DISCOVER and REQUEST handlers fire the new `Log`
  event with decision narration ("new device, offering X" / "doesn't match our
  record, sending NAK" / etc.).
- `frmDHCPServerBusy` — cancellable busy dialog with thread-safe `SetStatus`,
  Esc/Cancel/[X] all funnel to one cancel path.
- `frmDHCPServer.cs` — full enable/disable flow with phase-by-phase status
  updates and cancellation checkpoints. Auto-corrects `.0` (network address) to
  `.1` with visible textbox update. Handles same-IP/different-prefix by
  removing-and-re-adding rather than aborting. Removes other IPv4 addresses
  after our address is up. Bind-retry loop on `SocketError.AddressNotAvailable`
  (250 ms × 40 attempts, ~10 s).
- `frmAddDHCPReservation` — MAC normalizer accepts colon/dash/dot/no-separator,
  outputs canonical `AA:BB:CC:DD:EE:FF`. Validates IP is in subnet (when range
  is set on the server).
- `NetworkManager.RemoveIPAddressAsync` for single-IP delete (preserves IPv6).
- `AdapterInfo` got `==`/`!=` operators delegating to existing `Equals`
  (DeviceID-based) so dictionary/comparison sites stay valid across refresh.
- `DHCPMessageEventArgs` carries a `Direction` (TX/RX); `DeviceCommunication`
  fires after sends too. `frmMain` debug log shows `DHCP TX/RX [MAC]: TYPE`.
  Lease-list "Last Communication" cell shows `TX: DHCPACK` / `RX: DHCPREQUEST`.
- `frmMain` subscribes to the new `DHCPServer.Log` event and pipes narration
  lines to the debug form, prefixed `DHCP:`.
- `frmDHCPServer_Load` repopulates address octet textboxes and prefix length
  from `_dhcpServer.Address` / `_dhcpServer.PrefixLength` on form reopen.
