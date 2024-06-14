using IPAddressChanger.Properties;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Management.Infrastructure;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Net.Sockets;
using System.Security.Policy;
using System.Windows.Forms.VisualStyles;
using System.Text.Json;
using Newtonsoft.Json;

namespace IPAddressChanger {

	public partial class frmMain : Form {

		public enum SHORTCUT_DOUBLECLICK_ACTIONS {
			EditShortcut,
			RunShortcut
		}

		private readonly Dictionary<int, string> PREFIX_ORIGINS = new Dictionary<int, string>() {
			[0] = "Other",
			[1] = "Manual",
			[2] = "Well Known",
			[3] = "DHCP",
			[4] = "Router Advertisement"
		};

		private readonly Dictionary<int, string> SUFFIX_ORIGINS = new Dictionary<int, string>() {
			[0] = "Other",
			[1] = "Manual",
			[2] = "Well Known",
			[3] = "Origin DHCP",
			[4] = "Link Layer Address",
			[5] = "Random"
		};

		private readonly Dictionary<int, string> ADDRESS_FAMILIES = new Dictionary<int, string>() {
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

		private List<IPAddressShortcut> ipAddressShortcuts = new List<IPAddressShortcut>();

		private PowerShell powerShell = PowerShell.Create();

		internal frmSettings? settingsForm = null;
		internal frmDebug debugForm = new frmDebug();

		private FormWindowState lastWindowState = FormWindowState.Normal;
		public frmMain() {
			InitializeComponent();
		}

		private string GetHRBitrate(UInt64 byteCount) {
			string[] suf = { "B/sec", "KBit", "MBit", "GBit", "TBit", "PBit", "EBit" }; //Longs run out around EB
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
			lsbAdapters.Items.Clear();
			lsvAddresses.Items.Clear();
			txtDriver.Text = "";
			txtHardwareAddress.Text = "";
			txtSpeed.Text = "";
			txtDeviceID.Text = "";
		}

		private async void GetAdapters() {
			SetStatus("Getting adapters...", true);
			debugForm.AddMessage("Getting adapters");
			ClearEverything();
			bool thereWereErrors = false;
			powerShell.Commands.Clear();
			powerShell.AddCommand("Get-NetAdapter");
			PSDataCollection<PSObject> results = await powerShell.InvokeAsync();
			foreach (PSObject result in results) {
				debugForm.AddMessage(result.ToString());
				if (result.BaseObject is CimInstance) {
					try {
						CimInstance ci = (CimInstance)result.BaseObject;
						if (ci is null || ci.CimInstanceProperties is null) {
							continue;
						}
						if ((UInt32)ci.CimInstanceProperties["InterfaceOperationalStatus"].Value == 1 || !tsbOnlineOnly.Checked) {
							lsbAdapters.Items.Add(

								new AdapterInfo(
									(UInt32)ci.CimInstanceProperties["InterfaceIndex"].Value,
									ci.CimInstanceProperties["Name"].Value.ToString() ?? "<unknown>",
									ci.CimInstanceProperties["DriverDescription"].Value.ToString() ?? "<unknown>",
									(UInt32)ci.CimInstanceProperties["InterfaceOperationalStatus"].Value == 1,
									ci.CimInstanceProperties["Speed"].Value is not null ? (UInt64)ci.CimInstanceProperties["Speed"].Value : 0,
									ci.CimInstanceProperties["PermanentAddress"].Value.ToString() ?? "<unknown>",
									ci.CimInstanceProperties["DeviceID"].Value.ToString() ?? "<unknown>"
								)
							); ;
						}
					} catch (Exception ex) {
						debugForm.AddMessage($"Error getting adapter data: {ex.Message}");
						thereWereErrors = true;
						continue;
					}
				}
			}
			if (lsbAdapters.Items.Count > 0) {
				lsbAdapters.SelectedIndex = 0;
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
			debugForm.AddMessage("Getting adapter details");
			SetStatus("Getting adapter details...", true);
			lsvAddresses.Items.Clear();
			txtHardwareAddress.Text = adapter.HardwareAddress;
			txtSpeed.Text = GetHRBitrate(adapter.Speed);
			txtDriver.Text = adapter.Driver;
			txtDeviceID.Text = adapter.DeviceID;
			PowerShell ps = PowerShell.Create();
			ps.AddCommand("Get-NetIPAddress");
			ps.AddParameter("-InterfaceIndex", adapterIndex);
			PSDataCollection<PSObject> results = await ps.InvokeAsync();
			foreach (PSObject result in results) {
				debugForm.AddMessage(result.ToString());
				if (result.BaseObject is CimInstance) {
					ListViewItem item = new ListViewItem();
					CimInstance ci = (CimInstance)result.BaseObject;
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

			debugForm.AddMessage("Done getting adapter details");
			SetStatus("Done", false);

		}

		private void LoadSettings() {
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
						int newColOrder = 0;
						if (int.TryParse(columnsOrder[i], out newColOrder)) {
							lsvAddresses.Columns[i].DisplayIndex = newColOrder;
						}
					}

					if (i < columnsOrder.Length) {
						int newColWidth = 0;
						if (int.TryParse(columnsWidths[i], out newColWidth)) {
							lsvAddresses.Columns[i].Width = newColWidth;
						}

					}
				}
			} catch (Exception ex) {
				debugForm.AddMessage($"Error loading settings: {ex.Message}");
				MessageBox.Show($"Error loading settings: {ex.Message}", "Error Loading Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
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
				List<int> columnDisplayIndexes = new List<int>();
				List<int> columnWidths = new List<int>();
				foreach (ColumnHeader col in lsvAddresses.Columns) {
					columnDisplayIndexes.Add(col.DisplayIndex);
					columnWidths.Add(col.Width);
				}
				Settings.Default.ColumnsOrder = string.Join(",", columnDisplayIndexes);
				Settings.Default.ColumnsWidths = string.Join(",", columnWidths);
			} catch (Exception ex) {
				debugForm.AddMessage($"Error saving settings: {ex.Message}");
				MessageBox.Show($"Error saving settings: {ex.Message}", "Error Saving Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
				List<IPAddressShortcut> newShortcuts = JsonConvert.DeserializeObject<List<IPAddressShortcut>>(Settings.Default.Shortcuts) ?? new List<IPAddressShortcut>();
				ipAddressShortcuts = newShortcuts;
			} catch (Exception ex) {
				debugForm.AddMessage($"Error loading shortcuts: {ex.Message}");
				MessageBox.Show($"Error loading shortcuts: {ex.Message}", "Error Loading Shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
			if (ipAddressShortcuts.Count() == 0) {
				debugForm.AddMessage("There are no shortcuts saved");
				ToolStripMenuItem noShortcutsMenuItem = new ToolStripMenuItem("No Shortcuts");
				noShortcutsMenuItem.Enabled = false;
				tsmiShortcuts.DropDownItems.Add(noShortcutsMenuItem);
			}
			for (int i = 0; i < ipAddressShortcuts.Count(); i++) {
				IPAddressShortcut ips = ipAddressShortcuts[i];
				ToolStripMenuItem tsmi = new ToolStripMenuItem(ips.Name);
				tsmi.Tag = i;
				tsmi.Click += shortcut_Click;
				tsmiShortcuts.DropDownItems.Add(tsmi);
				lsbShortcuts.Items.Add($"{i + 1}: {ips.Name}");
				debugForm.AddMessage($"Adding shortcut named {ips.Name}");
			}
			SetStatus("", false);
		}

		private AdapterInfo? GetAdapterInfoFromDeviceID(string deviceID) {
			for (int i = 0; i < lsbAdapters.Items.Count; i++) {
				AdapterInfo ai = (AdapterInfo)lsbAdapters.Items[i];
				if (ai.DeviceID == deviceID) {
					return ai;
				}
			}
			return null;
		}

		private async void RunShortcut(IPAddressShortcut shortcut) {
			debugForm.AddMessage($"Running shortcut {shortcut.Name}");
			AdapterInfo ? ai = GetAdapterInfoFromDeviceID(shortcut.DeviceID);
			if (ai == null) {
				MessageBox.Show($"Adapter with device ID {shortcut.DeviceID} not found!", "Adapter Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
				//debugForm.AddMessage($"DHCP disable result: {dhcpResults}");

				SetStatus($"Removing addresses from {ai.Name}");
				debugForm.AddMessage($"Removing addresses from {ai.Name}");
				powerShell.Commands.Clear();
				powerShell.AddCommand("Remove-NetIPAddress");
				powerShell.AddParameter("-InterfaceIndex", ai.Index);
				powerShell.AddParameter("-Confirm", false);
				PSDataCollection<PSObject> removeResults = await powerShell.InvokeAsync();
				//debugForm.AddMessage($"Remove address result: {removeResults}");

				debugForm.AddMessage($"Setting new IP address for {ai.Name} to {shortcut.IPAddress}/{shortcut.PrefixLength}");
				SetStatus($"Setting new IP address for {ai.Name}");
				powerShell.Commands.Clear();
				powerShell.AddCommand("New-NetIPAddress");
				powerShell.AddParameter("-InterfaceIndex", ai.Index);
				powerShell.AddParameter("-IPAddress", shortcut.IPAddress);
				powerShell.AddParameter("-PrefixLength", shortcut.PrefixLength);
				powerShell.AddParameter("-Confirm", false);
				PSDataCollection<PSObject> newResults = await powerShell.InvokeAsync();
				//debugForm.AddMessage($"New address result: {newResults}");
				debugForm.AddMessage($"{powerShell.HadErrors}");
			} else {
				debugForm.AddMessage($"Enabling DHCP on {ai.Name}");
				SetStatus($"Enabling DHCP on {ai.Name}");
				powerShell.AddCommand("Set-NetIPInterface");
				powerShell.AddParameter("-InterfaceIndex", ai.Index);
				powerShell.AddParameter("-Dhcp", "Enabled");
				powerShell.AddParameter("-Confirm", false);
				PSDataCollection<PSObject> dhcpResults = await powerShell.InvokeAsync();
				debugForm.AddMessage($"Enable DHCP result: {dhcpResults}");
			}
			GetAdapters();
		}

		private void EditShortcut(int? shortcutIndex, AdapterInfo? device) {
			frmEditShortcut frm = new frmEditShortcut();
			frm.ShortcutIndex = shortcutIndex;
			if (shortcutIndex == null && device is not null) {
				frm.Text = "New Shortcut";
				frm.DeviceID = device.DeviceID;
				frm.cmdDelete.Visible = false;
				frm.txtAdapterName.Text = $"{device.Name} {device.DeviceID}";
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
				IPAddressShortcut ipas = new IPAddressShortcut() {
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

		private void PowerShellDataAdded(string type, string message) {
			debugForm.AddMessage($"PowerShell {type}: {message}");
		}
		private void PowerShellErrorDataAdded(object? sender, EventArgs e) {
			Collection<ErrorRecord> errors = powerShell.Streams.Error.ReadAll();
			foreach (ErrorRecord error in errors) {
				PowerShellDataAdded("Error", error.ToString());
			}
		}

		private void PowerShellWarningDataAdded(object? sender, EventArgs e) {
			Collection<WarningRecord> warnings = powerShell.Streams.Warning.ReadAll();
			foreach (WarningRecord warning in warnings) {
				PowerShellDataAdded("Warning", warning.ToString());
			}
		}

		private void PowerShellInformationDataAdded(object? sender, EventArgs e) {
			Collection<InformationRecord> infos = powerShell.Streams.Information.ReadAll();
			foreach (InformationRecord info in infos) {
				PowerShellDataAdded("Info", info.ToString());
			}
		}

		private void frmMain_Load(object sender, EventArgs e) {
			debugForm.AddMessage("Main form loading");
			powerShell.Streams.Error.DataAdded += PowerShellErrorDataAdded;
			powerShell.Streams.Warning.DataAdded += PowerShellWarningDataAdded;
			powerShell.Streams.Information.DataAdded += PowerShellInformationDataAdded;
			LoadSettings();
			LoadShortcuts();
			GetAdapters();
			notifyIcon1.Visible = true;
			if (Settings.Default.StartMinimized) {
				this.WindowState = FormWindowState.Minimized;
				if (Settings.Default.HideWhenMinimized) {
					this.ShowInTaskbar = false;
					this.Hide();
				}
			}
			debugForm.AddMessage("Main form loaded, application ready");
		}

		private void tsbRefresh_Click(object sender, EventArgs e) {
			GetAdapters();
		}

		private void lsbAdapters_SelectedIndexChanged(object sender, EventArgs e) {
			if (lsbAdapters.SelectedIndex != -1) {
				tsbNewShortcut.Enabled = true;
				ShowAdapterInfo((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]);
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
			powerShell.Stop();
			powerShell.Dispose();
			SaveSettings();
			SaveShortcuts();
		}

		private void tsbNewShortcut_Click(object sender, EventArgs e) {
			if (lsbAdapters.SelectedIndex < 0) {
				return;
			}
			EditShortcut(null, ((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]));
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
			int idx = -1;
			string indexpart = ((lsbShortcuts.Items[lsbShortcuts.SelectedIndex].ToString()) ?? "").Split(":")[0];
			if (int.TryParse(indexpart, out idx)) {
				EditShortcut(idx - 1, null);
			}
		}

		private void tsbDeleteShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			if (MessageBox.Show("Are you sure you want to delete this shortcut?", "Delete Shortcut?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
				int idx = -1;
				string indexpart = ((lsbShortcuts.Items[lsbShortcuts.SelectedIndex].ToString()) ?? "").Split(":")[0];
				if (int.TryParse(indexpart, out idx)) {
					ipAddressShortcuts.RemoveAt(idx - 1);
					BuildShortcutsList();
				}
			}
		}

		private void lsbShortcuts_SelectedIndexChanged(object sender, EventArgs e) {
			tsbDeleteShortcut.Enabled = (lsbShortcuts.SelectedIndex >= 0);
			tsbEditShortcut.Enabled = (lsbShortcuts.SelectedIndex >= 0);
			tsbRecallShortcut.Enabled = (lsbShortcuts.SelectedIndex >= 0);
		}

		private void lsbAdapters_DoubleClick(object sender, EventArgs e) {
			tsbNewShortcut.PerformClick();
		}

		private void tsbRecallShortcut_Click(object sender, EventArgs e) {
			if (lsbShortcuts.SelectedIndex < 0) return;
			int idx = -1;
			string indexpart = ((lsbShortcuts.Items[lsbShortcuts.SelectedIndex].ToString()) ?? "").Split(":")[0];
			if (int.TryParse(indexpart, out idx)) {
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
	}

	public class AdapterInfo {
		internal UInt32 Index { get; set; } = 0;
		internal string Name { get; set; } = "";
		internal string Driver { get; set; } = "";
		internal bool IsConnected { get; set; } = false;
		internal UInt64 Speed { get; set; } = 0;
		internal string HardwareAddress { get; set; } = "";
		internal string DeviceID { get; set; } = "";

		public AdapterInfo(UInt32 index = 0, string name = "", string driver = "", bool isConnected = false, UInt64 speed = 0, string hardwareAddress = "", string deviceID = "") {
			Index = index;
			Name = name;
			Driver = driver;
			IsConnected = isConnected;
			Speed = speed;
			HardwareAddress = hardwareAddress;
			DeviceID = deviceID;
		}

		public override string ToString() {
			return $"{Index}: {Name} [{(IsConnected ? "UP" : "down")}]";
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

