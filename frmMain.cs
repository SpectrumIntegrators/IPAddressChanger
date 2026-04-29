using IPAddressChanger.Properties;
using Microsoft.Management.Infrastructure;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text.Json;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Text.RegularExpressions;

namespace IPAddressChanger {

	public partial class frmMain : Form {

		public enum SHORTCUT_DOUBLECLICK_ACTIONS {
			EditShortcut,
			RunShortcut
		}

		private readonly Dictionary<int, string> PREFIX_ORIGINS = new() {
			[0] = "Other",
			[1] = "Manual",
			[2] = "Well Known",
			[3] = "DHCP",
			[4] = "Router Advertisement"
		};

		private readonly Dictionary<int, string> SUFFIX_ORIGINS = new() {
			[0] = "Other",
			[1] = "Manual",
			[2] = "Well Known",
			[3] = "Origin DHCP",
			[4] = "Link Layer Address",
			[5] = "Random"
		};

		private readonly Dictionary<int, string> ADDRESS_FAMILIES = new() {
			[-1] = "Unknown",
			[0] = "Unspecified",
			[1] = "Unix",
			[2] = "IPv4",
			[3] = "ImpLink",
			[4] = "Pup",
			[5] = "Chaos",
			[6] = "Ipx or NS",
			[7] = "Iso or Osi",
			[8] = "Ecma",
			[9] = "DataKit",
			[10] = "Ccitt",
			[11] = "Sna",
			[12] = "DecNet",
			[13] = "DataLink",
			[14] = "Lat",
			[15] = "HyperChannel",
			[16] = "AppleTalk",
			[17] = "NetBios",
			[18] = "VoiceView",
			[19] = "FireFox",
			[21] = "Banyan",
			[22] = "Atm",
			[23] = "IPv6",
			[24] = "Cluster",
			[25] = "Ieee12844",
			[26] = "Irda",
			[28] = "NetworkDesigners",
			[29] = "Max",
			[65536] = "Packet",
			[65537] = "ControllerAreaNetwork"
		};

		private List<IPAddressShortcut> ipAddressShortcuts = []; // list of all of the stored network adapter settings shortcuts
		internal frmSettings? settingsForm = null; // Don't load the settings form now, but keep a reference when we do load it
		internal frmDebug debugForm = new(); // Create and reference a debug form to send it debug log messages
		internal KeyboardHook? hook = null;// Global hotkey object
		private bool isClosing = false; // true if the application is closing and no more network operations should run
		private bool isBusy = false; // serialize adapter queries and changes so concurrent calls don't fight over the UI
		private FormWindowState lastWindowState = FormWindowState.Normal; // Remember the last window state before minimizing or maximizing
		private Dictionary<string, AdapterSnapshot> adapterSnapshots = new(); // last-known adapter state, keyed by NetworkInterface.Id, used to suppress no-op change notifications
		private Dictionary<AdapterInfo, frmAdapterBusy?> busyAdapterDialogs = new(); // Tracks per-adapter in-flight operations. Key presence = adapter busy. Value = open dialog or null if user dismissed it.
		private frmAddressConflictWarning? addressConflictWarningDialog;
		private bool suppressFutureAddressConflictWarnings = false;

		internal static partial class IPValidator {
			[GeneratedRegex(@"(?:^(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)/(?:3[0-2]|[12]?\d)$)|(?:^DHCP$)")]
			public static partial Regex IpCidrOrDhcp();
		}

		private class AdapterSnapshot {
			public string Name { get; set; } = "";
			public OperationalStatus Status { get; set; }
			public long Speed { get; set; }
			public HashSet<string> IPs { get; set; } = new();
		}

		public frmMain() {
			InitializeComponent();
			// ImageList contents loaded programmatically rather than persisted by the designer,
			// because the designer serializes ImageList.ImageStream via BinaryFormatter, which is removed in .NET 9+.
			netAdapterIcons.Images.Add("disabled", Resources.disabled);
			netAdapterIcons.Images.Add("up", Resources.up);
			netAdapterIcons.Images.Add("down", Resources.down);
			debugForm.AddMessage($"Starting {Application.ProductName} version {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}");
			if (Control.ModifierKeys == Keys.Shift) {
				Settings.Default.Reset();
				Settings.Default.Save();
			}
			LoadSettings();
		}

		private static Dictionary<string, AdapterSnapshot> BuildAdapterSnapshot() {
			Dictionary<string, AdapterSnapshot> snapshot = new();
			foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces()) {
				HashSet<string> ips = new();
				try {
					foreach (UnicastIPAddressInformation ua in n.GetIPProperties().UnicastAddresses) {
						ips.Add(ua.Address.ToString());
					}
				} catch (NetworkInformationException) {
					// some adapters refuse GetIPProperties; treat as no addresses
				}
				long speed = 0;
				try { speed = n.Speed; } catch (NetworkInformationException) { }
				snapshot[n.Id] = new AdapterSnapshot {
					Name = n.Name,
					Status = n.OperationalStatus,
					Speed = speed,
					IPs = ips,
				};
			}
			return snapshot;
		}

		public void AddressConflictWarningDialogClosing(bool suppressFutureMessages = false) {
			addressConflictWarningDialog = null;
			suppressFutureAddressConflictWarnings = suppressFutureMessages;
		}

		private void ShowAddressConflictWarningDialog(IEnumerable<string> adapters) {
			if (suppressFutureAddressConflictWarnings) {
				return;
			}
			if (addressConflictWarningDialog == null) {
				addressConflictWarningDialog = new frmAddressConflictWarning(this);
			}
			addressConflictWarningDialog.SetAddressConflicts(adapters);
			addressConflictWarningDialog.Show(this);
		}

		private async Task DetectAddressConflictsAsync(List<AdapterInfo> adapters) {
			if (suppressFutureAddressConflictWarnings) return;

			Dictionary<uint, List<IPAddressInfo>> allAddresses;
			try {
				allAddresses = await NetworkManager.GetAllIPAddressesAsync();
			} catch (Exception ex) {
				debugForm.AddMessage($"Error checking for address conflicts: {ex.Message}");
				return;
			}
			if (isClosing) return;

			// Build a map of IP -> the adapter names that hold it. The IPAddress field from
			// MSFT_NetIPAddress is the canonical scoped form, so two interfaces with their own
			// link-local fe80::* addresses won't false-positive (their scope IDs differ).
			Dictionary<string, List<string>> ipToAdapters = new();
			foreach (var kv in allAddresses) {
				AdapterInfo? adapter = adapters.FirstOrDefault(a => a.Index == kv.Key);
				if (adapter == null) continue;
				foreach (IPAddressInfo addr in kv.Value) {
					string ip = addr.IPAddress;
					if (string.IsNullOrEmpty(ip)) continue;
					if (!ipToAdapters.ContainsKey(ip)) ipToAdapters[ip] = new List<string>();
					ipToAdapters[ip].Add(adapter.Name);
				}
			}

			List<string> conflicts = new();
			foreach (var kv in ipToAdapters) {
				if (kv.Value.Count > 1) {
					conflicts.Add($"{kv.Key} is assigned to: {string.Join(", ", kv.Value)}");
				}
			}

			if (conflicts.Count > 0) {
				debugForm.AddMessage($"Address conflicts detected: {conflicts.Count}");
				foreach (string c in conflicts) debugForm.AddMessage(c);
				ShowAddressConflictWarningDialog(conflicts);
			}
		}

		public void AddressChangedCallback(object? sender, EventArgs e) {
			Dictionary<string, AdapterSnapshot> current = BuildAdapterSnapshot();
			List<string> changes = new();

			foreach (var kv in current) {
				if (!adapterSnapshots.TryGetValue(kv.Key, out AdapterSnapshot? prev)) {
					changes.Add($"Adapter added: {kv.Value.Name} ({kv.Value.Status})");
					continue;
				}
				if (prev.Status != kv.Value.Status) {
					changes.Add($"{kv.Value.Name}: status {prev.Status} -> {kv.Value.Status}");
				}
				if (prev.Speed != kv.Value.Speed) {
					changes.Add($"{kv.Value.Name}: speed {prev.Speed} -> {kv.Value.Speed}");
				}
				foreach (string ip in kv.Value.IPs.Except(prev.IPs)) {
					changes.Add($"{kv.Value.Name}: +{ip}");
				}
				foreach (string ip in prev.IPs.Except(kv.Value.IPs)) {
					changes.Add($"{kv.Value.Name}: -{ip}");
				}
			}
			foreach (var kv in adapterSnapshots) {
				if (!current.ContainsKey(kv.Key)) {
					changes.Add($"Adapter removed: {kv.Value.Name}");
				}
			}

			adapterSnapshots = current;

			if (changes.Count == 0) {
				debugForm.AddMessage("Got change notification but no detectable change");
				return;
			}
			foreach (string c in changes) {
				debugForm.AddMessage(c);
			}
			tsbRefresh.Image = Resources.refreshwarning;
		}
		private string GetHRBitrate(UInt64 byteCount) {
			string[] suf = ["B/sec", "KBit", "MBit", "GBit", "TBit", "PBit", "EBit"]; //Longs run out around EB
			if (byteCount == 0)
				return "0" + suf[0];
			long bytes = (long)Math.Abs(new decimal(byteCount));
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1000)));
			double num = Math.Round(bytes / Math.Pow(1000, place), 1);
			return (Math.Sign(new decimal(byteCount)) * num).ToString() + suf[place];
		}

		private void SetStatus(string status, bool busy = false) {
			tsslStatus.Text = status;
			if (busy) {
				this.UseWaitCursor = true;
			} else {
				this.UseWaitCursor = false;
			}
		}

		private void ClearEverything() {
			lsvAdapters.Items.Clear();
			lsvAddresses.Items.Clear();
			txtDriver.Text = "";
			txtHardwareAddress.Text = "";
			txtSpeed.Text = "";
			txtDeviceID.Text = "";
		}

		private async void GetAdapters() {
			if (isBusy) {
				debugForm.AddMessage("Skipping GetAdapters: another network operation is in progress");
				return;
			}
			isBusy = true;
			SetStatus("Getting adapters...", true);
			debugForm.AddMessage("Getting adapters");

			ClearEverything();
			bool thereWereErrors = false;
			try {
				List<AdapterInfo> adapters = await NetworkManager.GetAdaptersAsync();
				if (isClosing) return;
				foreach (AdapterInfo adapterInfo in adapters) {
					debugForm.AddMessage($"Adapter: {adapterInfo}");
					if (adapterInfo.IsConnected || !tsbOnlineOnly.Checked) {
						ListViewItem li = new() {
							ImageKey = adapterInfo.Status.ToString().ToLower(),
							Tag = adapterInfo,
						};
						li.SubItems.Add(adapterInfo.ToString());
						lsvAdapters.Items.Add(li);
					}
				}
				tsbRefresh.Image = Resources.Refresh_16x;
				await DetectAddressConflictsAsync(adapters);
			} catch (Exception ex) {
				ShowAndLogError($"Error getting adapters:\r\n{ex.Message}", "Error Getting Adapters");
				thereWereErrors = true;
			}

			if (lsvAdapters.Items.Count > 0) {
				lsvAdapters.SelectedIndices.Add(0);
			}
			if (!thereWereErrors) {
				debugForm.AddMessage("Done getting adapters");
				SetStatus("Done", false);
			} else {
				debugForm.AddMessage("Done getting adapters, but there were errors");
				SetStatus("Done but there were errors", false);
			}
			isBusy = false;
		}

		private async void ShowAdapterInfo(AdapterInfo adapter) {
			if (isClosing) return;
			debugForm.AddMessage("Getting adapter details");
			SetStatus("Getting adapter details...", true);
			lsvAddresses.Items.Clear();
			txtHardwareAddress.Text = adapter.HardwareAddress;
			txtSpeed.Text = GetHRBitrate(adapter.Speed);
			txtDriver.Text = adapter.Driver;
			txtDeviceID.Text = adapter.DeviceID;
			cmdRenewDHCPLease.Enabled = false;
			tsmiRenewDHCPForAdapter.Enabled = false;
			if (adapter.IsEnabled) {
				try {
					List<IPAddressInfo> addresses = await NetworkManager.GetIPAddressesAsync(adapter.Index);
					if (isClosing) return;
					foreach (IPAddressInfo addr in addresses) {
						debugForm.AddMessage($"Address: {addr.IPAddress} ({ADDRESS_FAMILIES.GetValueOrDefault(addr.AddressFamily, "Unknown")})");
						ListViewItem item = new();
						if (addr.AddressFamily == (UInt16)AddressFamily.InterNetwork) {
							item.Text = addr.IPv4Address;
						} else if (addr.AddressFamily == (UInt16)AddressFamily.InterNetworkV6) {
							item.Text = addr.IPv6Address;
						} else {
							item.Text = addr.IPAddress;
						}
						item.SubItems.Add(addr.PrefixLength.ToString());
						item.SubItems.Add(ADDRESS_FAMILIES.GetValueOrDefault(addr.AddressFamily, "Unknown"));
						item.SubItems.Add(PREFIX_ORIGINS.GetValueOrDefault(addr.PrefixOrigin, "Unknown"));
						item.SubItems.Add(SUFFIX_ORIGINS.GetValueOrDefault(addr.SuffixOrigin, "Unknown"));
						lsvAddresses.Items.Add(item);
					}
				} catch (Exception ex) {
					ShowAndLogError($"Error getting address info for {adapter.Name}: {ex.Message}", "Error Getting Addresses");
				}
				try {
					bool dhcpEnabled = await NetworkManager.GetIPv4DhcpEnabledAsync(adapter.Index);
					if (isClosing) return;
					// only apply if the user hasn't navigated to a different adapter while we were awaiting
					if (lsvAdapters.SelectedItems.Count > 0 &&
						((AdapterInfo)lsvAdapters.SelectedItems[0].Tag).DeviceID == adapter.DeviceID) {
						cmdRenewDHCPLease.Enabled = dhcpEnabled;
						tsmiRenewDHCPForAdapter.Enabled = dhcpEnabled;
					}
				} catch (Exception ex) {
					debugForm.AddMessage($"Could not determine DHCP state for {adapter.Name}: {ex.Message}");
				}
			} else {
				lsvAddresses.Items.Add("Adapter disabled");
			}
			debugForm.AddMessage("Done getting adapter details");
			SetStatus("Done", false);
		}

		private DialogResult ShowAndLogError(string message, string title = "Error", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Warning) {
			debugForm.AddMessage($"{message}");
			return MessageBox.Show(message, title, buttons, icon);
		}

		internal void LoadSettings() {
			debugForm.AddMessage($"Current settings file location: {ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath}");
			try {
				this.Width = Settings.Default.WindowWidth;
				this.Height = Settings.Default.WindowHeight;
				this.WindowState = (FormWindowState)Settings.Default.WindowState;
				splitContainer1.SplitterDistance = Settings.Default.SplitterWidth;
				splitContainer2.SplitterDistance = Settings.Default.SplitterHeight;
				tsbOnlineOnly.Checked = Settings.Default.HideOfflineAdapters;
				string[] columnsOrder = Settings.Default.ColumnsOrder.Split(",");
				string[] columnsWidths = Settings.Default.ColumnsWidths.Split(",");
				for (int i = 0; i < lsvAddresses.Columns.Count; i++) {
					if (i < columnsOrder.Length) {
						if (int.TryParse(columnsOrder[i], out int newColOrder)) {
							lsvAddresses.Columns[i].DisplayIndex = newColOrder;
						}
					}

					if (i < columnsOrder.Length) {
						if (int.TryParse(columnsWidths[i], out int newColWidth)) {
							lsvAddresses.Columns[i].Width = newColWidth;
						}

					}
				}
				if (hook is not null) {
					// make sure we unregistger/get rid of the old keyboard shortcut
					debugForm.AddMessage("Disposing old hotkey");
					hook.KeyPressed -= hook_KeyPressed;
					hook.Dispose();
					hook = null;
				}
				debugForm.AddMessage($"Adding new hotkey (modifiers: {Settings.Default.HotkeyModifier}, key: {Settings.Default.Hotkey})");
				hook = new KeyboardHook();
				hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
				hook.RegisterHotKey((ModifierKeys)Settings.Default.HotkeyModifier, (Keys)Settings.Default.Hotkey);
				hook.KeyPressed += hook_KeyPressed;
			} catch (Exception ex) {
				ShowAndLogError($"Error loading settings: {ex.Message}", "Error Loading Settings");
			}
		}
		void hook_KeyPressed(object? sender, KeyPressedEventArgs e) {
			this.Show();
			if (this.WindowState == FormWindowState.Minimized) {
				this.WindowState = lastWindowState;
			}
			this.Activate();
			notifyIcon1.Visible = false;
			notifyIcon1.Visible = true;
		}

		private void SaveSettings() {
			debugForm.AddMessage("Saving settings");
			try {
				if (this.WindowState != FormWindowState.Minimized) {
					// Don't save the state if it's minimized
					Settings.Default.WindowState = (int)this.WindowState;
				}
				if (this.WindowState == FormWindowState.Normal) {
					// only save the size if it's not maximized or minimized
					Settings.Default.WindowWidth = this.Width;
					Settings.Default.WindowHeight = this.Height;
				}
				Settings.Default.SplitterWidth = splitContainer1.SplitterDistance;
				Settings.Default.SplitterHeight = splitContainer2.SplitterDistance;
				Settings.Default.HideOfflineAdapters = tsbOnlineOnly.Checked;
				List<int> columnDisplayIndexes = [];
				List<int> columnWidths = [];
				foreach (ColumnHeader col in lsvAddresses.Columns) {
					columnDisplayIndexes.Add(col.DisplayIndex);
					columnWidths.Add(col.Width);
				}
				Settings.Default.ColumnsOrder = string.Join(",", columnDisplayIndexes);
				Settings.Default.ColumnsWidths = string.Join(",", columnWidths);
			} catch (Exception ex) {
				ShowAndLogError($"Error saving settings: {ex.Message}", "Error Saving Settings");
			}
		}

		private void SaveShortcuts() {
			debugForm.AddMessage("Saving shortcuts");
			try {
				Settings.Default.Shortcuts = JsonSerializer.Serialize(ipAddressShortcuts);
				Settings.Default.Save();
			} catch (Exception ex) {
				debugForm.AddMessage($"Error saving shortcuts: {ex.Message}");
			}
		}

		private void LoadShortcuts() {
			debugForm.AddMessage("Loading shortcuts");
			SetStatus("Loading shortcuts");
			try {
				string raw = Settings.Default.Shortcuts;
				List<IPAddressShortcut> newShortcuts = string.IsNullOrWhiteSpace(raw)
					? []
					: JsonSerializer.Deserialize<List<IPAddressShortcut>>(raw) ?? [];
				ipAddressShortcuts = newShortcuts;
			} catch (Exception ex) {
				ShowAndLogError($"Error loading shortcuts: {ex.Message}", "Error Loading Shortcuts");
			}
			BuildShortcutsList();
		}

		private void BuildShortcutsList() {
			debugForm.AddMessage("Building shortcuts list and menu");
			SetStatus("Building shortcuts", true);
			tsmiShortcuts.DropDownItems.Clear();
			lsbShortcuts.Items.Clear();
			tsbDeleteShortcut.Enabled = false;
			tsbEditShortcut.Enabled = false;
			tsbRecallShortcut.Enabled = false;
			if (ipAddressShortcuts.Count == 0) {
				debugForm.AddMessage("There are no shortcuts saved");
				ToolStripMenuItem noShortcutsMenuItem = new("No Shortcuts") {
					Enabled = false
				};
				tsmiShortcuts.DropDownItems.Add(noShortcutsMenuItem);
			}
			for (int i = 0; i < ipAddressShortcuts.Count; i++) {
				IPAddressShortcut ips = ipAddressShortcuts[i];
				ToolStripMenuItem tsmi = new(ips.Name) {
					Tag = i
				};
				tsmi.Click += shortcut_Click;
				tsmiShortcuts.DropDownItems.Add(tsmi);
				lsbShortcuts.Items.Add($"{i + 1}: {ips.Name}");
				debugForm.AddMessage($"Adding shortcut named {ips.Name}");
			}
			SetStatus("", false);
		}

		private AdapterInfo? GetAdapterInfoFromDeviceID(string deviceID) {
			foreach (ListViewItem item in lsvAdapters.Items) {
				AdapterInfo ai = (AdapterInfo)item.Tag;
				if (ai.DeviceID == deviceID) {
					return ai;
				}
			}
			return null;
		}

		private async void RunShortcut(IPAddressShortcut shortcut) {
			debugForm.AddMessage($"Running shortcut {shortcut.Name}");
			AdapterInfo? ai = GetAdapterInfoFromDeviceID(shortcut.DeviceID);
			if (ai == null) {
				ShowAndLogError($"Adapter with device ID {shortcut.DeviceID} not found!", "Adapter Not Found");
				return;
			}
			await ApplyAddressToAdapter(ai, shortcut.UseDHCP, shortcut.IPAddress, shortcut.PrefixLength, $"shortcut '{shortcut.Name}'");
		}

		// Applies a static IP+prefix or DHCP to the given adapter, with the per-adapter busy
		// dialog and progress messages. operationDescription is folded into status/log lines
		// (e.g. "shortcut 'Office Static'", "address 10.0.0.69/16", "DHCP").
		private async Task ApplyAddressToAdapter(AdapterInfo ai, bool useDhcp, string ipAddress, int prefixLength, string operationDescription) {
			if (busyAdapterDialogs.ContainsKey(ai)) {
				debugForm.AddMessage($"Skipping {operationDescription}: {ai.Name} is busy with another operation");
				if (busyAdapterDialogs[ai] == null) {
					// the user had closed the dialog, so re-show it
					ShowAdapterBusyDialog($"{ai.Name} is busy with another operation", ai);
				}
				return;
			}

			// Pre-check: refuse before mutating state if this IP is already on a different adapter.
			// Without this check, the OS rejects MSFT_NetIPAddress.Create with "already exists" AFTER we've
			// already disabled DHCP and removed the existing addresses, leaving the adapter with no IP.
			if (!useDhcp) {
				try {
					Dictionary<uint, List<IPAddressInfo>> allAddresses = await NetworkManager.GetAllIPAddressesAsync();
					foreach (var kv in allAddresses) {
						if (kv.Key == ai.Index) continue;
						foreach (IPAddressInfo addr in kv.Value) {
							if (addr.IPAddress == ipAddress) {
								string conflictName = GetAdapterNameByIndex(kv.Key) ?? $"interface {kv.Key}";
								ShowAndLogError($"Cannot apply {ipAddress}/{prefixLength} to {ai.Name}: the address is already in use on {conflictName}.", "Address Already In Use");
								return;
							}
						}
					}
				} catch (Exception ex) {
					debugForm.AddMessage($"Could not pre-check for address conflicts: {ex.Message}");
					// fall through; the OS will reject the duplicate if the pre-check missed it
				}
			}

			SetStatus($"Applying {operationDescription} to {ai.Name}...", false);
			ShowAdapterBusyDialog($"Applying {operationDescription} to {ai.Name}", ai);
			try {
				if (!useDhcp) {
					ShowAdapterBusyDialog($"Disabling DHCP on {ai.Name}", ai);
					debugForm.AddMessage($"Disabling DHCP on {ai.Name}");
					try {
						await NetworkManager.SetDhcpAsync(ai.Index, false);
					} catch (Exception ex) {
						ShowAndLogError($"Error disabling DHCP on {ai.Name}:\r\n{ex.Message}", "Error Disabling DHCP");
						return;
					}

					ShowAdapterBusyDialog($"Removing addresses from {ai.Name}", ai);
					debugForm.AddMessage($"Removing addresses from {ai.Name}");
					try {
						await NetworkManager.RemoveAllIPAddressesAsync(ai.Index);
					} catch (Exception ex) {
						ShowAndLogError($"Error removing address from {ai.Name}:\r\n {ex.Message}", "Error Removing Address");
						return;
					}

					debugForm.AddMessage($"Setting new IP address for {ai.Name} to {ipAddress}/{prefixLength}");
					ShowAdapterBusyDialog($"Setting new IP address for {ai.Name}", ai);
					try {
						await NetworkManager.NewIPAddressAsync(ai.Index, ipAddress, (byte)prefixLength);
					} catch (Exception ex) {
						string detail = ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase)
							? "the address is already assigned to another adapter on this system."
							: ex.Message;
						ShowAndLogError($"Error adding IP address on {ai.Name}:\r\n{detail}", "Error Adding Address");
						return;
					}
				} else {
					debugForm.AddMessage($"Enabling DHCP on {ai.Name}");
					ShowAdapterBusyDialog($"Enabling DHCP on {ai.Name}", ai);
					try {
						await NetworkManager.SetDhcpAsync(ai.Index, true);
					} catch (Exception ex) {
						ShowAndLogError($"Error enabling DHCP on {ai.Name}:\r\n{ex.Message}", "Error Enabling DHCP");
						return;
					}
				}
				SetStatus($"Applied {operationDescription} to {ai.Name}", false);
			} finally {
				RemoveAdapterBusyDialog(ai);
				// Always refresh — even on error, since intermediate steps may have already mutated adapter state.
				GetAdapters();
			}
		}

		private string? GetAdapterNameByIndex(uint index) {
			foreach (ListViewItem item in lsvAdapters.Items) {
				if (item.Tag is AdapterInfo ai && ai.Index == index) return ai.Name;
			}
			return null;
		}

		private void EditShortcut(int? shortcutIndex = null, AdapterInfo? device = null, IPAddressShortcut? newShortcut = null) {
			frmEditShortcut frm = new() {
				ShortcutIndex = shortcutIndex
			};
			if (shortcutIndex == null && device is not null) {
				frm.Text = "New Shortcut";
				frm.DeviceID = device.DeviceID;
				frm.cmdDelete.Visible = false;
				frm.txtAdapterName.Text = $"{device.Name} {device.DeviceID}";
				frm.adapterName = device.Name;
				frm.txtName.Text = device.Name;
				if (newShortcut is not null) {
					if (newShortcut.UseDHCP) {
						frm.chkUseDHCP.Checked = true;
						frm.txtName.Text = $"{device.Name} - DHCP";
					} else {
						frm.txtIPAddress.Text = newShortcut.IPAddress;
						frm.nudPrefixLength.Value = newShortcut.PrefixLength;
						frm.txtName.Text = $"{device.Name} - {newShortcut.IPAddress}/{newShortcut.PrefixLength}";
					}
				}
				frm.nameHasChanged = false;
			}
			if (shortcutIndex is not null) {
				int idx = (int)shortcutIndex;
				frm.Text = "Edit Shortcut";
				frm.DeviceID = ipAddressShortcuts[idx].DeviceID ?? "";
				frm.txtAdapterName.Text = $"{ipAddressShortcuts[idx].AdapterName} {ipAddressShortcuts[idx].DeviceID}";
				frm.txtName.Text = ipAddressShortcuts[idx].Name;
				frm.txtIPAddress.Text = ipAddressShortcuts[idx].IPAddress;
				frm.nudPrefixLength.Value = ipAddressShortcuts[idx].PrefixLength;
				frm.chkUseDHCP.Checked = ipAddressShortcuts[idx].UseDHCP;
				frm.cmdDelete.Visible = true;
			}

			DialogResult result = frm.ShowDialog(this);
			if (result == DialogResult.OK) {
				// save/update the shortcut
				IPAddressShortcut ipas = new() {
					Name = frm.txtName.Text,
					DeviceID = frm.DeviceID,
					IPAddress = frm.txtIPAddress.Text,
					PrefixLength = (int)frm.nudPrefixLength.Value,
					UseDHCP = frm.chkUseDHCP.Checked,
					AdapterName = frm.txtAdapterName.Text
				};
				if (frm.ShortcutIndex is null) {
					ipAddressShortcuts.Add(ipas);
				} else {
					ipAddressShortcuts[(int)frm.ShortcutIndex] = ipas;
				}
				BuildShortcutsList();
			} else if (result == DialogResult.Yes) {
				// delete the shortcut
				if (frm.ShortcutIndex is not null) {
					ipAddressShortcuts.RemoveAt((int)frm.ShortcutIndex);
				}
				BuildShortcutsList();
			}
		}

		private void shortcut_Click(object? sender, EventArgs e) {
			if (sender is null) return;
			ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
			if (tsmi.Tag == null) return;
			RunShortcut(ipAddressShortcuts[(int)tsmi.Tag]);
		}

		private void frmMain_Load(object sender, EventArgs e) {

			debugForm.AddMessage("Main form loading");
			tsslVersion.Text = "Version " + Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "UNKNOWN";

			if (isClosing) return;
			adapterSnapshots = BuildAdapterSnapshot();
			NetworkChange.NetworkAddressChanged += new
			NetworkAddressChangedEventHandler(AddressChangedCallback);
			LoadShortcuts();
			GetAdapters();
			if (Settings.Default.StartMinimized) {
				this.WindowState = FormWindowState.Minimized;
				if (Settings.Default.HideWhenMinimized) {
					this.ShowInTaskbar = false;
					this.Hide();
				}
			}
			notifyIcon1.Visible = true;
			debugForm.AddMessage("Main form loaded, application ready");
		}

		private void tsbRefresh_Click(object sender, EventArgs e) {
			GetAdapters();
		}

		private void lsbAdapters_SelectedIndexChanged(object sender, EventArgs e) {
			if (lsvAdapters.SelectedItems.Count > 0) {
				tsbNewShortcut.Enabled = true;
				ShowAdapterInfo((AdapterInfo)lsvAdapters.SelectedItems[0].Tag);
			} else {
				txtSpeed.Text = "";
				txtHardwareAddress.Text = "";
				txtDriver.Text = "";
				txtDeviceID.Text = "";
				lsvAddresses.Items.Clear();
				tsbNewShortcut.Enabled = false;
				cmdRenewDHCPLease.Enabled = false;
				tsmiRenewDHCPForAdapter.Enabled = false;
			}
		}

		private void tsbOnlineOnly_Click(object sender, EventArgs e) {
			GetAdapters();
		}

		private void tsmiShow_Click(object sender, EventArgs e) {
			this.ShowInTaskbar = true;
			this.Show();
			if (this.WindowState == FormWindowState.Minimized) {
				this.WindowState = lastWindowState;
			}
		}

		private void tsmiExit_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
			tsmiShow.PerformClick();
		}

		private void tsmiHide_Click(object sender, EventArgs e) {
			this.WindowState = FormWindowState.Minimized;
			this.Hide();
			this.ShowInTaskbar = false;
		}

		private void frmMain_SizeChanged(object sender, EventArgs e) {
			if (this.WindowState != FormWindowState.Minimized) {
				lastWindowState = this.WindowState;
			} else {
				if (Settings.Default.HideWhenMinimized) {
					this.Hide();
				}
			}
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
			isClosing = true;
			SaveSettings();
			SaveShortcuts();
		}

		private void tsbNewShortcut_Click(object sender, EventArgs e) {
			if (lsvAdapters.SelectedItems.Count == 0) {
				return;
			}
			EditShortcut(null, (AdapterInfo)lsvAdapters.SelectedItems[0].Tag);
		}

		private void lsbShortcuts_DoubleClick(object sender, EventArgs e) {
			if (Settings.Default.ShortcutDoubleClick == (int)SHORTCUT_DOUBLECLICK_ACTIONS.EditShortcut) {
				tsbEditShortcut.PerformClick();
			} else if (Settings.Default.ShortcutDoubleClick == (int)SHORTCUT_DOUBLECLICK_ACTIONS.RunShortcut) {
				tsbRecallShortcut.PerformClick();
			}
		}

		private int? GetShortcutIndexFromListIndex(int listIndex) {
			string indexpart = ((lsbShortcuts.Items[lsbShortcuts.SelectedIndex].ToString()) ?? "").Split(":")[0];
			if (int.TryParse(indexpart, out int idx)) {
				return idx - 1;
			}
			return null;
		}

		private void tsbEditShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			EditShortcut(GetShortcutIndexFromListIndex(lsbShortcuts.SelectedIndex));
		}

		private void AskDeleteShortcut(int shortcutIndex) {
			if (MessageBox.Show("Are you sure you want to delete this shortcut?", "Delete Shortcut?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
				string indexpart = ((lsbShortcuts.Items[shortcutIndex].ToString()) ?? "").Split(":")[0];
				if (int.TryParse(indexpart, out int idx)) {
					ipAddressShortcuts.RemoveAt(idx - 1);
					BuildShortcutsList();
				}
			}
		}

		private void tsbDeleteShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			AskDeleteShortcut(lsbShortcuts.SelectedIndex);
		}

		private void lsbShortcuts_SelectedIndexChanged(object sender, EventArgs e) {
			bool buttonsenabled = (lsbShortcuts.SelectedIndex >= 0);
			tsbDeleteShortcut.Enabled = buttonsenabled;
			tsbEditShortcut.Enabled = buttonsenabled;
			tsbRecallShortcut.Enabled = buttonsenabled;
			tsbMoveShortcutDown.Enabled = buttonsenabled;
			tsbMoveShortcutUp.Enabled = buttonsenabled;
		}

		private void lsbAdapters_DoubleClick(object sender, EventArgs e) {
			tsbNewShortcut.PerformClick();
		}

		private void tsbRecallShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			int? shortcutIndex = GetShortcutIndexFromListIndex(lsbShortcuts.SelectedIndex);
			if (shortcutIndex is null) { return; }
			RunShortcut(ipAddressShortcuts[(int)shortcutIndex]);
		}

		private void tsbSettings_Click(object sender, EventArgs e) {
			if (settingsForm is null) {
				settingsForm = new frmSettings(this);
				settingsForm.Show(this);
			} else {
				settingsForm.Show(this);
			}
		}

		private void tsbDebug_Click(object sender, EventArgs e) {
			debugForm.Show(this);
		}

		private void tsbControlPanel_Click(object sender, EventArgs e) {
			try {
				ProcessStartInfo psi = new(Settings.Default.AdaptersControlPanelFile) {
					UseShellExecute = true
				};
				Process.Start(psi);
			} catch (Exception ex) {
				ShowAndLogError($"Error launching control panel: {ex.Message}", "Error Launching Control Panel");
			}
		}

		private void tsmiControlPanel_Click(object sender, EventArgs e) {
			tsbControlPanel.PerformClick();
		}

		private void lsvAddresses_DoubleClick(object sender, EventArgs e) {
			if (lsvAddresses.SelectedItems.Count == 0) {
				return;
			}
			AdapterInfo ai = (AdapterInfo)lsvAdapters.SelectedItems[0].Tag;
			ListViewItem lvi = lsvAddresses.SelectedItems[0];
			EditShortcut(null, ai, new IPAddressShortcut() { IPAddress = lvi.SubItems[0].Text, PrefixLength = int.Parse(lvi.SubItems[1].Text), UseDHCP = (lvi.SubItems[3].Text == "DHCP") });
		}

		private void tsbHelp_Click(object sender, EventArgs e) {
			Help.ShowHelp(this, helpProvider1.HelpNamespace);
		}

		private void tsbBugReport_Click(object sender, EventArgs e) {
			ProcessStartInfo psi = new() {
				UseShellExecute = true,
				Verb = "open",
				FileName = Resources.FeedbackURL
			};
			try {
				Process.Start(psi);
			} catch (Exception ex) {
				ShowAndLogError($"Could not launch bug report URL... how ironic.\n{ex.Message}", "Error Launching URL");
			}
		}

		private void lsvAdapters_SelectedIndexChanged(object sender, EventArgs e) {
			if (lsvAdapters.SelectedItems.Count > 0) {
				tsbNewShortcut.Enabled = true;
				tsmiNewShortcut.Enabled = true;
				ShowAdapterInfo((AdapterInfo)lsvAdapters.SelectedItems[0].Tag);
			} else {
				txtSpeed.Text = "";
				txtHardwareAddress.Text = "";
				txtDriver.Text = "";
				txtDeviceID.Text = "";
				lsvAddresses.Items.Clear();
				tsbNewShortcut.Enabled = false;
				tsmiNewShortcut.Enabled = false;
				cmdRenewDHCPLease.Enabled = false;
				tsmiRenewDHCPForAdapter.Enabled = false;
			}
		}

		private void lsvAdapters_DoubleClick(object sender, EventArgs e) {
			tsbNewShortcut.PerformClick();
		}

		private void tsbMoveShortcutUp_Click(object sender, EventArgs e) {
			int curSelectedIndex = lsbShortcuts.SelectedIndex;
			if (curSelectedIndex == 0) {
				// first item, can't move up
				return;
			}
			// swap this item with the one above it
			IPAddressShortcut tempshortcut = ipAddressShortcuts[curSelectedIndex - 1];
			ipAddressShortcuts[curSelectedIndex - 1] = ipAddressShortcuts[curSelectedIndex];
			ipAddressShortcuts[curSelectedIndex] = tempshortcut;
			BuildShortcutsList();
			// select this same item again
			lsbShortcuts.SelectedIndex = curSelectedIndex - 1;
		}

		private void tsbMoveShortcutDown_Click(object sender, EventArgs e) {
			int curSelectedIndex = lsbShortcuts.SelectedIndex;
			if (curSelectedIndex == lsbShortcuts.Items.Count - 1) {
				// last item, can't move down
				return;
			}
			// swap this item with the one above it
			IPAddressShortcut tempshortcut = ipAddressShortcuts[curSelectedIndex + 1];
			ipAddressShortcuts[curSelectedIndex + 1] = ipAddressShortcuts[curSelectedIndex];
			ipAddressShortcuts[curSelectedIndex] = tempshortcut;
			BuildShortcutsList();
			// select this same item again
			lsbShortcuts.SelectedIndex = curSelectedIndex + 1;
		}

		private void tsmiDeleteShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			AskDeleteShortcut(lsbShortcuts.SelectedIndex);
		}

		private void tsmiEditShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			EditShortcut(GetShortcutIndexFromListIndex(lsbShortcuts.SelectedIndex));
		}

		private void tsmiRecallShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			int? shortcutIndex = GetShortcutIndexFromListIndex(lsbShortcuts.SelectedIndex);
			if (shortcutIndex is null) { return; }
			RunShortcut(ipAddressShortcuts[(int)shortcutIndex]);
		}

		private void cmsShortcutsListMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			bool menuItemsEnabled = (lsbShortcuts.SelectedIndex >= 0);
			tsmiDeleteShortcut.Enabled = menuItemsEnabled;
			tsmiRecallShortcut.Enabled = menuItemsEnabled;
			tsmiEditShortcut.Enabled = menuItemsEnabled;
			tsmiCopyShortcut.Enabled = menuItemsEnabled;
			if (menuItemsEnabled) {
				tsmiCopyShortcut.Text = $"Copy {GetShortcutItemAddressText(GetShortcutIndexFromListIndex(lsbShortcuts.SelectedIndex))}";
			} else {
				tsmiCopyShortcut.Text = "Copy";
			}
		}

		private void tsmiNewShortcut_Click(object sender, EventArgs e) {
			if (lsvAdapters.SelectedItems.Count == 0) {
				return;
			}
			EditShortcut(null, (AdapterInfo)lsvAdapters.SelectedItems[0].Tag);
		}

		private void tsmiNewShortcutForAdapter_Click(object sender, EventArgs e) {
			if (lsvAdapters.SelectedItems.Count == 0) {
				return;
			}
			EditShortcut(null, (AdapterInfo)lsvAdapters.SelectedItems[0].Tag);
		}

		private void tsmiRenewDHCPForAdapter_Click(object sender, EventArgs e) {
			if (lsvAdapters.SelectedItems.Count == 0) {
				return;
			}
			RenewAdapterDHCPLease((AdapterInfo)lsvAdapters.SelectedItems[0].Tag);
		}

		private void cmsAdaptersListMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			if (lsvAdapters.SelectedItems.Count == 0) {
				e.Cancel = true;
				return;
			}
			string clipboardText = Clipboard.GetText();
			if (IPValidator.IpCidrOrDhcp().IsMatch(clipboardText)) {
				tsmiPasteAddressForAdapter.Enabled = true;
				tsmiPasteAddressForAdapter.Text = $"Paste {clipboardText}";
				tsmiPasteAddressForAdapter.Tag = clipboardText;
			} else {
				tsmiPasteAddressForAdapter.Enabled = false;
				tsmiPasteAddressForAdapter.Text = "Paste";
				tsmiPasteAddressForAdapter.Tag = null;
			}
			// "New Shortcut with X" — prefer DHCP if that's the configured method,
			// otherwise use the first IPv4 address currently shown for this adapter.
			// Both signals come from data already fetched by ShowAdapterInfo, so no extra CIM query.
			if (cmdRenewDHCPLease.Enabled) {
				tsmiNewShortcutForAdapterWithAddress.Enabled = true;
				tsmiNewShortcutForAdapterWithAddress.Text = "New Shortcut with DHCP";
				tsmiNewShortcutForAdapterWithAddress.Tag = "DHCP";
			} else {
				string? cidr = null;
				foreach (ListViewItem addrItem in lsvAddresses.Items) {
					if (addrItem.SubItems.Count >= 3 && addrItem.SubItems[2].Text == "IPv4") {
						cidr = $"{addrItem.Text}/{addrItem.SubItems[1].Text}";
						break;
					}
				}
				if (cidr != null) {
					tsmiNewShortcutForAdapterWithAddress.Enabled = true;
					tsmiNewShortcutForAdapterWithAddress.Text = $"New Shortcut with {cidr}";
					tsmiNewShortcutForAdapterWithAddress.Tag = cidr;
				} else {
					tsmiNewShortcutForAdapterWithAddress.Enabled = false;
					tsmiNewShortcutForAdapterWithAddress.Text = "New Shortcut with...";
					tsmiNewShortcutForAdapterWithAddress.Tag = null;
				}
			}
		}

		public void AdapterDialogClosing(AdapterInfo adapter) {
			if (!busyAdapterDialogs.ContainsKey(adapter)) {
				return;
			}
			if (busyAdapterDialogs[adapter] != null) {
				debugForm.AddMessage($"Busy dialog for {adapter.Name} is closing");
				busyAdapterDialogs[adapter] = null;
			}
		}

		private void ShowAdapterBusyDialog(string message, AdapterInfo adapter) {
			if (!busyAdapterDialogs.ContainsKey(adapter)) {
				// new adapter
				busyAdapterDialogs.Add(adapter, null);
				debugForm.AddMessage($"Adding busy dialog for {adapter.Name}");
			}

			// new or existing adapter
			if (busyAdapterDialogs[adapter] == null) {
				// form doesn't exist for this adapter, add one
				frmAdapterBusy newAdapterBusyDialog = new frmAdapterBusy(message, adapter, this);
				busyAdapterDialogs[adapter] = newAdapterBusyDialog;
				newAdapterBusyDialog.Show(this);
				debugForm.AddMessage($"Showing busy dialog for {adapter.Name}");
			} else {
				// form already exists, update its message
				busyAdapterDialogs[adapter].lblBusyReason.Text = message;
				debugForm.AddMessage($"Updating busy dialog for {adapter.Name}");
			}
		}

		private void CloseAdapterBusyDialog(AdapterInfo adapter) {
			if (!busyAdapterDialogs.ContainsKey(adapter)) {
				return;
			}
			if (busyAdapterDialogs[adapter] != null) {
				debugForm.AddMessage($"Closing busy dialog for {adapter.Name}");
				busyAdapterDialogs[adapter].Close();
				busyAdapterDialogs[adapter] = null;
			}
		}

		private void RemoveAdapterBusyDialog(AdapterInfo adapter) {
			if (!busyAdapterDialogs.ContainsKey(adapter)) {
				return;
			}
			CloseAdapterBusyDialog(adapter);
			debugForm.AddMessage($"Removing busy dialog for {adapter.Name}");
			busyAdapterDialogs.Remove(adapter);
		}

		private async void RenewAdapterDHCPLease(AdapterInfo adapter) {
			if (busyAdapterDialogs.ContainsKey(adapter)) {
				debugForm.AddMessage("Skipping DHCP renew: another network operation is in progress");
				if (busyAdapterDialogs[adapter] == null) {
					// the user had closed the dialog, so re-show it
					ShowAdapterBusyDialog($"Renewing DHCP lease for {adapter.Name}", adapter);
				}
				return;
			}
			SetStatus($"Renewing DHCP lease on {adapter.Name}...", false);
			debugForm.AddMessage($"Renewing DHCP lease on {adapter.Name}");
			ShowAdapterBusyDialog($"Renewing DHCP lease for {adapter.Name}", adapter);
			try {
				uint code = await NetworkManager.RenewDhcpAsync(adapter.Index);
				if (code == 0) {
					SetStatus($"DHCP lease renewed on {adapter.Name} — refresh to see new address", false);
					debugForm.AddMessage($"DHCP lease renewed on {adapter.Name}");
				} else {
					string reason = code switch {
						82 => "could not renew lease (no DHCP server responded or network unreachable)",
						84 => "IP is not enabled on this adapter",
						91 => "access denied",
						100 => "DHCP is not enabled on this adapter",
						_ => $"DHCP renew failed (code {code})"
					};
					ShowAndLogError($"Error renewing DHCP address for {adapter.Name}: {reason}", "Error Renewing DHCP");
					SetStatus($"DHCP renewal for {adapter.Name} done, but there were errors (see log)", false);
				}
			} catch (Exception ex) {
				ShowAndLogError($"Error renewing DHCP address for {adapter.Name}: {ex.Message}", "Error Renewing DHCP");
				SetStatus($"DHCP renewal for {adapter.Name} done, but there were errors (see log)", false);
			} finally {
				RemoveAdapterBusyDialog(adapter);
				// If they've navigated away, ShowAdapterInfo for the new selection has already set the correct state.
				if (lsvAdapters.SelectedItems.Count > 0 &&
					((AdapterInfo)lsvAdapters.SelectedItems[0].Tag).DeviceID == adapter.DeviceID) {
				}
			}
		}

		private void cmdRenewDHCPLease_Click(object sender, EventArgs e) {
			if (lsvAdapters.SelectedItems.Count == 0) {
				return;
			}
			RenewAdapterDHCPLease((AdapterInfo)lsvAdapters.SelectedItems[0].Tag);
		}

		private string GetShortcutItemAddressText(int? shortcutIndex) {
			if (shortcutIndex == null) {
				return "";
			}
			IPAddressShortcut selectedShortcut = ipAddressShortcuts[(int)shortcutIndex];
			if (!selectedShortcut.UseDHCP) {
				return $"{selectedShortcut.IPAddress}/{selectedShortcut.PrefixLength}";
			}
			return "DHCP";
		}

		private void tsmiCopyShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			int? shortcutIndex = GetShortcutIndexFromListIndex(lsbShortcuts.SelectedIndex);
			Clipboard.SetText(GetShortcutItemAddressText(shortcutIndex));
		}

		private async void tsmiPasteAddressForAdapter_Click(object sender, EventArgs e) {
			if (lsvAdapters.SelectedItems.Count == 0) return;
			if (tsmiPasteAddressForAdapter.Tag is not string clipboardText) return;
			if (!IPValidator.IpCidrOrDhcp().IsMatch(clipboardText)) return;

			AdapterInfo ai = (AdapterInfo)lsvAdapters.SelectedItems[0].Tag;

			if (clipboardText == "DHCP") {
				await ApplyAddressToAdapter(ai, true, "", 0, "DHCP");
			} else {
				string[] parts = clipboardText.Split('/');
				await ApplyAddressToAdapter(ai, false, parts[0], int.Parse(parts[1]), clipboardText);
			}
		}

		private void tsmiNewShortcutWithForAdapter_Click(object sender, EventArgs e) {
			if (lsvAdapters.SelectedItems.Count == 0) return;
			if (tsmiNewShortcutForAdapterWithAddress.Tag is not string addressData) return;

			AdapterInfo ai = (AdapterInfo)lsvAdapters.SelectedItems[0].Tag;

			IPAddressShortcut prototype = new();
			if (addressData == "DHCP") {
				prototype.UseDHCP = true;
			} else {
				string[] parts = addressData.Split('/');
				prototype.IPAddress = parts[0];
				prototype.PrefixLength = int.Parse(parts[1]);
			}
			EditShortcut(null, ai, prototype);
		}

		private void cmsAddressesListMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			if (lsvAddresses.SelectedItems.Count == 0) {
				e.Cancel = true;
				return;
			}
			ListViewItem lvi = lsvAddresses.SelectedItems[0];
			string text = (lvi.SubItems[3].Text == "DHCP")
				? "DHCP"
				: $"{lvi.SubItems[0].Text}/{lvi.SubItems[1].Text}";
			tsmiAddressesListNewShortcut.Text = $"New Shortcut with {text}";
			tsmiAddressesListCopy.Text = $"Copy {text}";
		}

		private void tsmiAddressesListNewShortcut_Click(object sender, EventArgs e) {
			// Same behavior as double-clicking an address row.
			lsvAddresses_DoubleClick(sender, e);
		}

		private void tsmiAddressesListCopy_Click(object sender, EventArgs e) {
			if (lsvAddresses.SelectedItems.Count == 0) return;
			ListViewItem lvi = lsvAddresses.SelectedItems[0];
			string textToCopy = (lvi.SubItems[3].Text == "DHCP")
				? "DHCP"
				: $"{lvi.SubItems[0].Text}/{lvi.SubItems[1].Text}";
			Clipboard.SetText(textToCopy);
		}
	}

	public class AdapterInfo {
		internal UInt32 Index { get; set; } = 0;
		internal string Name { get; set; } = "";
		internal string Driver { get; set; } = "";
		internal bool IsConnected { get; set; } = false;
		internal bool IsEnabled { get; set; } = false;
		internal UInt64 Speed { get; set; } = 0;
		internal string HardwareAddress { get; set; } = "";
		internal string DeviceID { get; set; } = "";

		public AdapterInfo() {
			Index = 0;
			Name = "";
			Driver = "";
			IsConnected = false;
			IsEnabled = false;
			Speed = 0;
			HardwareAddress = "";
			DeviceID = "";
		}
		public AdapterInfo(UInt32 index = 0, string name = "", string driver = "", bool isConnected = false, bool isEnabled = false, UInt64 speed = 0, string hardwareAddress = "", string deviceID = "") {
			Index = index;
			Name = name;
			Driver = driver;
			IsConnected = isConnected;
			IsEnabled = isEnabled;
			Speed = speed;
			HardwareAddress = hardwareAddress;
			DeviceID = deviceID;
		}

		public string Status { get {
				return (IsEnabled ? (IsConnected ? "UP" : "down") : "disabled");
			}
		}
		public override string ToString() {
			return $"{Index}: {Name} [{Status}]";
		}

		// Equality is by DeviceID so that Dictionary<AdapterInfo, ...> lookups stay valid
		// across a refresh that produces new AdapterInfo references for the same physical adapter.
		public override bool Equals(object? obj) {
			return obj is AdapterInfo other && other.DeviceID == DeviceID;
		}
		public override int GetHashCode() {
			return DeviceID?.GetHashCode() ?? 0;
		}
	}

	public class IPAddressShortcut {
		public string DeviceID { get; set; } = "";
		public string AdapterName { get; set; } = "";
		public string Name { get; set; } = "";
		public string IPAddress { get; set; } = "";
		public int PrefixLength { get; set; } = 0;
		public bool UseDHCP { get; set; } = false;

	}

}

