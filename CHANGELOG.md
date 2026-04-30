# Changelog

All notable changes to this project are documented in this file. The format is loosely based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to a four-segment `Major.Minor.Patch.Build` version scheme.

  ## [1.1.1.0] - 2026-04-30

  ### Changed
  - Restored compact section headings ("Adapters" / "Shortcuts") to the left of their respective toolbars.
  - Added Settings upgrade so new versions of the app will retain the previous versions' settings
  - Fixed null reference build warnings
  - Updated in-app help references so F1 works from anywhere

## [1.1.0.0] - 2026-04-29

A substantial overhaul of the underlying networking layer and the per-operation UX. The application no longer depends on PowerShell or any third-party JSON library, and operations on different adapters can now run concurrently without freezing the UI.

### Added
- **DHCP renew** for the selected adapter, available as a toolbar button and on the adapter context menu. The button is enabled only when the selected adapter has DHCP configured for IPv4. A non-modal busy dialog reports progress while the renew is in flight (typically 1–60 seconds depending on whether a DHCP server responds).
- **Address paste** to the selected adapter, from the adapter context menu. The menu item is enabled only when the clipboard contains a valid IPv4/CIDR string or `DHCP`, and it shows the value that will be pasted (e.g. *Paste 10.0.0.69/16*).
- **Address copy** from the shortcut list — right-click a shortcut to copy its IP/prefix (or `DHCP`) to the clipboard.
- **"New Shortcut with..."** on the adapter context menu, which pre-fills the new-shortcut dialog with the currently displayed IP/prefix or `DHCP`.
- **Address conflict warning dialog**, raised after a refresh when two or more adapters share an IP. The dialog lists every conflict and offers a "do not show again" checkbox.
- **Pre-apply duplicate-IP check**: before disabling DHCP and removing existing addresses, the app verifies the target IP is not already assigned to a different adapter. If it is, the operation is refused with a clear message and no state mutation occurs.
- **Per-adapter "busy" dialogs** that report progress for slow operations. Operations on different adapters can run in parallel; the dialog can be dismissed without aborting the operation, and re-shows itself if a second action is attempted while the adapter is still busy.
- **Adapter-state cache** so that change notifications which represent no user-visible difference are logged once as "no detectable change" instead of dumping the full adapter list, and the refresh icon only highlights when something actually changed.
- **Adapter address list context menu**: right-clicking a row in the per-adapter addresses list now offers *New Shortcut with {value}* (pre-fills the new-shortcut dialog with the selected address's IP/prefix or `DHCP`) and *Copy {value}* (copies the same string to the clipboard for pasting onto another adapter). Both menu labels reflect the actual selected row.

### Changed
- **Networking layer**: all `Get-NetAdapter` / `Get-NetIPAddress` / `Set-NetIPInterface` / `New-NetIPAddress` / `Remove-NetIPAddress` invocations replaced with direct CIM calls via `Microsoft.Management.Infrastructure` against `root\StandardCimv2`. DHCP renew uses `Win32_NetworkAdapterConfiguration.RenewDHCPLease` in `root\cimv2`.
- **JSON serialization** moved from `Newtonsoft.Json` to the built-in `System.Text.Json`. Existing user shortcut data round-trips unchanged.
- **`ImageList` icons** are now loaded programmatically at form construction from individually-stored resources, rather than via the designer's `ImageStream` blob. The designer relied on `BinaryFormatter`, which is removed in .NET 9. The application is now forward-compatible with .NET 9+.
- **Distribution size** dropped from ~100 MB to ~2 MB after removing the PowerShell SDK and its transitive dependencies.
- **Adapter selection no longer triggers a global busy state**; only the adapter currently being modified is locked. Selecting a different adapter mid-operation works correctly and the UI remains responsive.

### Fixed
- "*Object reference not set to an instance of an object.*" popup when iterating adapters with null CIM property values (loopback, virtual NICs, Bluetooth PAN adapters). Property access is now null-safe with documented fallbacks.
- Adapter list now refreshes after a failed shortcut apply so the displayed state reflects the actual (possibly partial) configuration rather than stale pre-failure data.
- The cryptic "*The object already exists*" CIM error from `MSFT_NetIPAddress.Create` (when an attempted IP assignment collides with another adapter) is now translated to a clear "the address is already assigned to another adapter on this system" message. Acts as defense-in-depth if the pre-apply duplicate check is bypassed by a race or external state change.
- Stray unused `using` directives removed.

### Removed
- `Microsoft.PowerShell.SDK` package reference.
- `Newtonsoft.Json` package reference.
- Startup `Set-ExecutionPolicy` / `Import-Module NetTCPIP` calls (no longer needed).

## [1.0.5.1] - 2024-07-21

### Added
- Sortable shortcut list (move up / move down buttons).

### Fixed
- GitHub workflow issue.

## [1.0.4.0] - 2024-06-24

### Added
- Adapter status indicators (up / down / disabled icons in the adapter list).
- *Start at log on* option, which creates a Task Scheduler entry to launch the app at user logon with elevated privileges (no UAC prompt at logon).

## [1.0.2.1] - 2024-06-22

### Added
- Global hotkey to show and focus the window (default `Ctrl + Alt + Shift + F11`, configurable in Settings).

## [1.0.1.0] - 2024-06-21

### Added
- Adapter change notifications — the refresh icon highlights when network state changes outside the application.

## [1.0.0.0] - 2024-06-20

Initial release.

### Features
- Saveable IP/prefix and DHCP shortcuts per adapter, recallable from the main window or the notification-area icon.
- Per-adapter detail pane with hardware address, link speed, driver description, and device ID.
- Per-adapter address list showing every assigned IPv4 / IPv6 address with prefix length, address family, and prefix/suffix origin.
- Notification-area icon with quick access to shortcuts, show/hide, and adapter control panel launcher.
- Settings dialog with start-minimized, hide-when-minimized, and double-click behavior options.
- Debug message log with copy / save / clear.
- Configurable network adapters control-panel launcher path.

[1.1.0.0]: https://github.com/SpectrumIntegrators/IPAddressChanger/releases/tag/v1.1.0.0
[1.0.5.1]: https://github.com/SpectrumIntegrators/IPAddressChanger/releases/tag/v1.0.5.1
[1.0.4.0]: https://github.com/SpectrumIntegrators/IPAddressChanger/releases/tag/v1.0.4.0
[1.0.2.1]: https://github.com/SpectrumIntegrators/IPAddressChanger/releases/tag/v1.0.2.1
[1.0.1.0]: https://github.com/SpectrumIntegrators/IPAddressChanger/releases/tag/v1.0.1.0
[1.0.0.0]: https://github.com/SpectrumIntegrators/IPAddressChanger/releases/tag/v1.0.0.0
