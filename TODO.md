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

### DHCP screenshots for the README

The README has one DHCP-server-related screenshot at most; the new windows need
fresh captures: `frmDHCPServer` (configured, server enabled, with leases and
reservations visible), `frmDHCPServerBusy` (mid-operation), `frmAddDHCPReservation`
(both add mode and edit mode), and the unhandled-exception dialogs are already
in `images/`. Save into `images/` with descriptive names (`dhcpserver.png`,
`dhcpserverbusy.png`, etc.) and reference from the README's DHCP section.

## Deferred (not started)

- **README** needs significant updates to cover the DHCP server feature (configuration, lease management, help-doc anchors, screenshot). Specific notes to remember when writing it: - In the "saving DHCP addresses" section: when "Save reserved and automatic   addresses" is selected, server-issued leases come back as reservations on   the next launch — round-trip flattens them because the load path uses   `TryAddReservation`, which sets `Assigned`/`Expires` to null. - In the address-entry section: `.`, `/`, and `\` all advance focus between   the four octet boxes and the prefix-length box, so users can type   `10.0.0.1/24`, `10/0/0/1/24`, etc. — whatever feels natural. Backspace   on an empty box jumps back and trims a character off the previous one. - In the address-entry section: entering the network address of the subnet   (e.g. `10.0.0.0/24`) is allowed; the server will automatically bump it to   the first host address (`10.0.0.1`) on Enable. Same goes for the broadcast   address — it's bumped down by one. Any other host address is kept as-is,   so the server doesn't have to live at `.1`. - In the adapter-selection section: the bound adapter is intentionally **not**   saved across launches, even though the server address and reservations are.   Two reasons: (1) USB-Ethernet adapters often come and go, so a saved choice   could easily be invalid on next launch, and (2) forcing the user to pick   the adapter every time is a deliberate guardrail against accidentally   binding the DHCP server to the wrong network. - In the reservation-dialog section: the MAC address field is forgiving about   formatting. Colon (`AA:BB:CC:DD:EE:FF`), dash (`AA-BB-CC-DD-EE-FF`), dot   (`AABB.CCDD.EEFF`), and unseparated (`AABBCCDDEEFF`) are all accepted; case   doesn't matter. Whatever the user types is normalized to upper-case   colon-separated form before being stored.
- **CHANGELOG** — single line "Added DHCP server" is sufficient.
- **Help doc** — add a section for the new help keyword `dhcp-server-busy-dialog` so F1 from the busy dialog doesn't 404.
- `frmDHCPServerBusy.Designer.cs` was hand-written; first time it's opened in the WinForms designer it may rewrite layout-percent values and re-add a duplicate `Dispose(bool)` method — if so, just delete the duplicate again.

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