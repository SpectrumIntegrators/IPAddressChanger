using IPAddressChanger.Properties;
using Microsoft.Management.Infrastructure;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;
using System.Net.NetworkInformation;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.Xml.Linq;
using System;
using System.Configuration;

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
		private readonly PowerShell powerShell = PowerShell.Create(); // Use this PowerShell instance for all commands
		internal frmSettings? settingsForm = null; // Don't load the settings form now, but keep a reference when we do load it
		internal frmDebug debugForm = new(); // Create and reference a debug form to send it debug log messages
		internal KeyboardHook? hook = null;// Global hotkey object
		private bool isClosing = false; // true if the application is closing and no more PowerShell commands should be executed
		private FormWindowState lastWindowState = FormWindowState.Normal; // Remember the last window state before minimizing or maximizing

		public frmMain() {
			InitializeComponent();
			if (Control.ModifierKeys == Keys.Shift) {
				Settings.Default.Reset();
				Settings.Default.Save();
			}
			LoadSettings();
		}

		public void AddressChangedCallback(object? sender, EventArgs e) {

			NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface n in adapters) {
				debugForm.AddMessage("NetworkAdapterChanged: {0} is {1}", n.Name, n.OperationalStatus);
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

		private string PSObjectToString(PSObject property) {
			List<string> propvalues = [];
			foreach (var p in property.Properties) {
				propvalues.Add($"[{p.TypeNameOfValue}] {p.Name} = {p.Value ?? "null"}");
			}
			return string.Join("; ", propvalues);

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
			SetStatus("Getting adapters...", true);
			debugForm.AddMessage("Getting adapters");
			if (powerShell.InvocationStateInfo.State == PSInvocationState.Running) {
				debugForm.AddMessage("PowerShell is still busy");
				return;
			}

			ClearEverything();
			bool thereWereErrors = false;
			powerShell.Commands.Clear();
			powerShell.AddCommand("Get-NetAdapter");
			PSDataCollection<PSObject> results = await powerShell.InvokeAsync();
			if (isClosing) return;
			if (!powerShell.HadErrors) {
				foreach (PSObject result in results) {
					debugForm.AddMessage("Adapter details: " + PSObjectToString(result));
					if (result.BaseObject is CimInstance ci) {
						try {
							if (ci is null || ci.CimInstanceProperties is null) {
								continue;
							}
							if ((UInt32)ci.CimInstanceProperties["InterfaceOperationalStatus"].Value == 1 || !tsbOnlineOnly.Checked) {
								AdapterInfo adapterInfo = new AdapterInfo(
										(UInt32)ci.CimInstanceProperties["InterfaceIndex"].Value,
										ci.CimInstanceProperties["Name"].Value.ToString() ?? "<unknown>",
										ci.CimInstanceProperties["DriverDescription"].Value.ToString() ?? "<unknown>",
										(UInt32)ci.CimInstanceProperties["InterfaceOperationalStatus"].Value == 1,
										(UInt32)ci.CimInstanceProperties["InterfaceAdminStatus"].Value == 1,
										ci.CimInstanceProperties["Speed"].Value is not null ? (UInt64)ci.CimInstanceProperties["Speed"].Value : 0,
										ci.CimInstanceProperties["PermanentAddress"].Value.ToString() ?? "<unknown>",
										ci.CimInstanceProperties["DeviceID"].Value.ToString() ?? "<unknown>"
								);
								ListViewItem li = new() {
									ImageKey = adapterInfo.Status.ToString().ToLower(),
									Tag = adapterInfo,
								};
								li.SubItems.Add(adapterInfo.ToString());
								lsvAdapters.Items.Add(li);
							}
						} catch (Exception ex) {
							ShowAndLogError($"Error getting adapter data:\r\n{ex.Message}", "Error Getting Adapter Data");
							thereWereErrors = true;
							continue;
						}
					}
				}
				tsbRefresh.Image = Resources.Refresh_16x;
			} else {
				ShowAndLogError($"Error getting adapters:\r\n{GetPowerShellErrors()}", "Error Getting Adapters");
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
		}

		private async void ShowAdapterInfo(AdapterInfo adapter) {
			UInt32 adapterIndex = adapter.Index;
			if (isClosing) return;
			debugForm.AddMessage("Getting adapter details");
			if (powerShell.InvocationStateInfo.State == PSInvocationState.Running) {
				debugForm.AddMessage("PowerShell is still busy");
				return;
			}
			SetStatus("Getting adapter details...", true);
			lsvAddresses.Items.Clear();
			txtHardwareAddress.Text = adapter.HardwareAddress;
			txtSpeed.Text = GetHRBitrate(adapter.Speed);
			txtDriver.Text = adapter.Driver;
			txtDeviceID.Text = adapter.DeviceID;
			if (adapter.IsEnabled) {
				powerShell.Commands.Clear();
				powerShell.AddCommand("Get-NetIPAddress");
				powerShell.AddParameter("-InterfaceIndex", adapterIndex);
				if (isClosing) return;
				PSDataCollection<PSObject> results = await powerShell.InvokeAsync();
				if (!powerShell.HadErrors) {
					foreach (PSObject result in results) {
						debugForm.AddMessage("Adapter Details: " + PSObjectToString(result));
						if (result.BaseObject is CimInstance ci) {
							ListViewItem item = new();
							if ((UInt16)ci.CimInstanceProperties["AddressFamily"].Value == (UInt16)AddressFamily.InterNetwork) {
								item.Text = ci.CimInstanceProperties["IPv4Address"].Value.ToString();
							} else if ((UInt16)ci.CimInstanceProperties["AddressFamily"].Value == (UInt16)AddressFamily.InterNetworkV6) {
								item.Text = ci.CimInstanceProperties["IPv6Address"].Value.ToString();
							} else {
								item.Text = ci.CimInstanceProperties["Address"].Value.ToString();
							}
							item.SubItems.Add(ci.CimInstanceProperties["PrefixLength"].Value.ToString());
							item.SubItems.Add(ADDRESS_FAMILIES[(UInt16)ci.CimInstanceProperties["AddressFamily"].Value]);
							item.SubItems.Add(PREFIX_ORIGINS[(UInt16)ci.CimInstanceProperties["PrefixOrigin"].Value]);
							item.SubItems.Add(SUFFIX_ORIGINS[(UInt16)ci.CimInstanceProperties["SuffixOrigin"].Value]);
							lsvAddresses.Items.Add(item);
						}
					}
				} else {
					ShowAndLogError($"Error getting address info for {adapter.Name}: {GetPowerShellErrors()}", "Error Getting Addresses");
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
				Settings.Default.Shortcuts = JsonConvert.SerializeObject(ipAddressShortcuts);
				Settings.Default.Save();
			} catch (Exception ex) {
				debugForm.AddMessage($"Error saving shortcuts: {ex.Message}");
			}
		}

		private void LoadShortcuts() {
			debugForm.AddMessage("Loading shortcuts");
			SetStatus("Loading shortcuts");
			try {
				List<IPAddressShortcut> newShortcuts = JsonConvert.DeserializeObject<List<IPAddressShortcut>>(Settings.Default.Shortcuts) ?? [];
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

		private string GetPowerShellErrors() {
			StringBuilder ret = new();
			Collection<ErrorRecord> errors = powerShell.Streams.Error.ReadAll();
			foreach (ErrorRecord error in errors) {
				ret.AppendLine(error.ToString());
			}
			return ret.ToString();
		}

		private async void RunShortcut(IPAddressShortcut shortcut) {
			debugForm.AddMessage($"Running shortcut {shortcut.Name}");
			AdapterInfo? ai = GetAdapterInfoFromDeviceID(shortcut.DeviceID);

			if (powerShell.InvocationStateInfo.State == PSInvocationState.Running) {
				debugForm.AddMessage("PowerShell is still busy");
				return;
			}

			if (ai == null) {
				ShowAndLogError($"Adapter with device ID {shortcut.DeviceID} not found!", "Adapter Not Found");
				return;
			}

			if (!shortcut.UseDHCP) {
				SetStatus($"Disabling DHCP on {ai.Name}");
				debugForm.AddMessage($"Disabling DHCP on {ai.Name}");
				powerShell.Commands.Clear();
				powerShell.AddCommand("Set-NetIPInterface");
				powerShell.AddParameter("-InterfaceIndex", ai.Index);
				powerShell.AddParameter("-Dhcp", "Disabled");
				powerShell.AddParameter("-Confirm", false);
				PSDataCollection<PSObject> dhcpResults = await powerShell.InvokeAsync();
				if (powerShell.HadErrors) {
					ShowAndLogError($"Error disabling DHCP on {ai.Name}:\r\n{GetPowerShellErrors()}", "Error Disabling DHCP");
					return;
				}

				SetStatus($"Removing addresses from {ai.Name}");
				debugForm.AddMessage($"Removing addresses from {ai.Name}");
				powerShell.Commands.Clear();
				powerShell.AddCommand("Remove-NetIPAddress");
				powerShell.AddParameter("-InterfaceIndex", ai.Index);
				powerShell.AddParameter("-Confirm", false);
				PSDataCollection<PSObject> removeResults = await powerShell.InvokeAsync();
				if (powerShell.HadErrors) {
					ShowAndLogError($"Error removing address from {ai.Name}:\r\n {GetPowerShellErrors()}", "Error Removing Address");
					return;
				}

				debugForm.AddMessage($"Setting new IP address for {ai.Name} to {shortcut.IPAddress}/{shortcut.PrefixLength}");
				SetStatus($"Setting new IP address for {ai.Name}");
				powerShell.Commands.Clear();
				powerShell.AddCommand("New-NetIPAddress");
				powerShell.AddParameter("-InterfaceIndex", ai.Index);
				powerShell.AddParameter("-IPAddress", shortcut.IPAddress);
				powerShell.AddParameter("-PrefixLength", shortcut.PrefixLength);
				powerShell.AddParameter("-Confirm", false);
				PSDataCollection<PSObject> newResults = await powerShell.InvokeAsync();
				if (powerShell.HadErrors) {
					ShowAndLogError($"Error adding IP address on {ai.Name}:\r\n{GetPowerShellErrors()}", "Error Adding Address");
					return;
				}
			} else {
				debugForm.AddMessage($"Enabling DHCP on {ai.Name}");
				SetStatus($"Enabling DHCP on {ai.Name}");
				powerShell.Commands.Clear();
				powerShell.AddCommand("Set-NetIPInterface");
				powerShell.AddParameter("-InterfaceIndex", ai.Index);
				powerShell.AddParameter("-Dhcp", "Enabled");
				powerShell.AddParameter("-Confirm", false);
				PSDataCollection<PSObject> dhcpResults = await powerShell.InvokeAsync();
				if (powerShell.HadErrors) {
					ShowAndLogError($"Error enabling DHCP on {ai.Name}:\r\n{GetPowerShellErrors()}", "Error Enabling DHCP");
					return;
				}

			}
			GetAdapters();
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

		private async void frmMain_Load(object sender, EventArgs e) {

			debugForm.AddMessage("Main form loading");
			tsslVersion.Text = "Version " + Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "UNKNOWN";

			if (isClosing) return;
			debugForm.AddMessage("Enabling PowerShell scripts");
			powerShell.Commands.Clear();
			powerShell.AddCommand("Set-ExecutionPolicy");
			powerShell.AddParameter("-Scope", "Process");
			powerShell.AddParameter("-ExecutionPolicy", "Bypass");
			try {
				PSDataCollection<PSObject> results = await powerShell.InvokeAsync();
			} catch (Exception ex) {
				ShowAndLogError($"Error setting execution policy:\r\n{ex.Message}\r\nThe application can not continue.", "Error Setting Policy", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
				return;
			}
			if (powerShell.HadErrors) {
				ShowAndLogError($"Error setting execution policy:\r\n{GetPowerShellErrors()}\r\nThe application can not continue.", "Error Setting Policy", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
				return;
			}


			if (isClosing) return;
			debugForm.AddMessage("Importing NetTCPIP PowerShell module");
			powerShell.Commands.Clear();
			powerShell.AddCommand("Import-Module");
			powerShell.AddArgument("NetTCPIP");
			try {
				PSDataCollection<PSObject> results = await powerShell.InvokeAsync();
			} catch (Exception ex) {
				ShowAndLogError($"Error importing NetTCPIP:\r\n{ex.Message}\r\nThe application can not continue.", "Error Importing Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
				return;
			}
			if (powerShell.HadErrors) {
				ShowAndLogError($"Error importing NetTCPIP:\r\n{GetPowerShellErrors()}\r\nThe application can not continue.", "Error Importing Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
				return;
			}

			if (isClosing) return;
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
			powerShell.Stop();
			powerShell.Dispose();
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

		private void tsbEditShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			string indexpart = ((lsbShortcuts.Items[lsbShortcuts.SelectedIndex].ToString()) ?? "").Split(":")[0];
			if (int.TryParse(indexpart, out int idx)) {
				EditShortcut(idx - 1, null);
			}
		}

		private void tsbDeleteShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			if (MessageBox.Show("Are you sure you want to delete this shortcut?", "Delete Shortcut?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
				string indexpart = ((lsbShortcuts.Items[lsbShortcuts.SelectedIndex].ToString()) ?? "").Split(":")[0];
				if (int.TryParse(indexpart, out int idx)) {
					ipAddressShortcuts.RemoveAt(idx - 1);
					BuildShortcutsList();
				}
			}
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
			string indexpart = ((lsbShortcuts.Items[lsbShortcuts.SelectedIndex].ToString()) ?? "").Split(":")[0];
			if (int.TryParse(indexpart, out int idx)) {
				RunShortcut(ipAddressShortcuts[idx - 1]);
			}

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
				ShowAdapterInfo((AdapterInfo)lsvAdapters.SelectedItems[0].Tag);
			} else {
				txtSpeed.Text = "";
				txtHardwareAddress.Text = "";
				txtDriver.Text = "";
				txtDeviceID.Text = "";
				lsvAddresses.Items.Clear();
				tsbNewShortcut.Enabled = false;
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

