using IPAddressChanger.Properties;
using Microsoft.Management.Infrastructure;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Net.Sockets;
using System.Security.Policy;

namespace IPAddressChanger {

	public partial class frmMain : Form {


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

		private async void GetAdapters(bool onlineOnly = false) {
			SetStatus("Getting adapters...", true);
			ClearEverything();
			PowerShell ps = PowerShell.Create();
			ps.AddCommand("Get-NetAdapter");
			PSDataCollection<PSObject> results = await ps.InvokeAsync();
			foreach (PSObject result in results) {
				if (result.BaseObject is CimInstance) {
					try {
						CimInstance ci = (CimInstance)result.BaseObject;
						if ((UInt32)ci.CimInstanceProperties["InterfaceOperationalStatus"].Value == 1 || !onlineOnly) {
							lsbAdapters.Items.Add(
								new AdapterInfo(
									(UInt32)ci.CimInstanceProperties["InterfaceIndex"].Value,
									ci.CimInstanceProperties["Name"].Value.ToString(),
									ci.CimInstanceProperties["DriverDescription"].Value.ToString(),
									(UInt32)ci.CimInstanceProperties["InterfaceOperationalStatus"].Value == 1,
									ci.CimInstanceProperties["Speed"].Value is not null ? (UInt64)ci.CimInstanceProperties["Speed"].Value : 0,
									ci.CimInstanceProperties["PermanentAddress"].Value.ToString(),
									ci.CimInstanceProperties["DeviceID"].Value.ToString()
								)
							); ;
						}
					} catch (Exception ex) {
						continue;
					}
				}
			}
			if (lsbAdapters.Items.Count > 0) {
				lsbAdapters.SelectedIndex = 0;
			}
			SetStatus("Done", false);
		}

		private async void ShowAdapterInfo(AdapterInfo adapter) {
			UInt32 adapterIndex = adapter.Index;
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
				//textBox1.Text += result.ToString() + "\r\n";
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


			SetStatus("Done", false);

		}

		private void frmMain_Load(object sender, EventArgs e) {
			this.Width = Settings.Default.WindowWidth;
			this.Height = Settings.Default.WindowHeight;
			this.WindowState = (FormWindowState)Settings.Default.WindowState;
			splitContainer1.SplitterDistance = Settings.Default.SplitterDistance;
			tsbOnlineOnly.Checked = Settings.Default.HideOfflineAdapters;
			GetAdapters(tsbOnlineOnly.Checked);
			notifyIcon1.Visible = true;
		}

		private void tsbRefresh_Click(object sender, EventArgs e) {
			GetAdapters(tsbOnlineOnly.Checked);
		}

		private void lsbAdapters_SelectedIndexChanged(object sender, EventArgs e) {
			if (lsbAdapters.SelectedIndex != -1) {
				tsbRemoveAddress.Enabled = true;
				tsbSetAddress.Enabled = true;
				tsbEnableDHCP.Enabled = true;
				ShowAdapterInfo((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]);
			} else {
				txtSpeed.Text = "";
				txtHardwareAddress.Text = "";
				txtDriver.Text = "";
				txtDeviceID.Text = "";
				lsvAddresses.Items.Clear();
				tsbRemoveAddress.Enabled = false;
				tsbSetAddress.Enabled = false;
				tsbEnableDHCP.Enabled = false;
			}
		}

		private void tsbOnlineOnly_Click(object sender, EventArgs e) {
			GetAdapters(tsbOnlineOnly.Checked);
		}

		private void tsmiShow_Click(object sender, EventArgs e) {
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
		}

		private void frmMain_SizeChanged(object sender, EventArgs e) {
			if (this.WindowState != FormWindowState.Minimized) {
				lastWindowState = this.WindowState;
			} else {
				this.Hide();
			}
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
			Settings.Default.WindowState = (int)this.WindowState;
			if (this.WindowState == FormWindowState.Normal) {
				Settings.Default.WindowWidth = this.Width;
				Settings.Default.WindowHeight = this.Height;
			}
			Settings.Default.SplitterDistance = splitContainer1.SplitterDistance;
			Settings.Default.HideOfflineAdapters = tsbOnlineOnly.Checked;
			Settings.Default.Save();
		}

		private async void tsbRemoveAddress_Click(object sender, EventArgs e) {
			SetStatus($"Removing addresses from {((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Name}");
			PowerShell ps = PowerShell.Create();
			ps.AddCommand("Remove-NetIPAddress");
			ps.AddParameter("-InterfaceIndex", ((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Index);
			ps.AddParameter("-Confirm", false);
			PSDataCollection<PSObject> results = await ps.InvokeAsync();

			GetAdapters();
		}

		private async void tsbSetAddress_Click(object sender, EventArgs e) {
			PowerShell ps = PowerShell.Create();

			SetStatus($"Disabling DHCP on {((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Name}");
			ps.Commands.Clear();
			ps.AddCommand("Set-NetIPInterface");
			ps.AddParameter("-InterfaceIndex", ((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Index);
			ps.AddParameter("-Dhcp", "Disabled");
			ps.AddParameter("-Confirm", false);
			PSDataCollection<PSObject> dhcpResults = await ps.InvokeAsync();

			SetStatus($"Removing addresses from {((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Name}");
			ps.Commands.Clear();
			ps.AddCommand("Remove-NetIPAddress");
			ps.AddParameter("-InterfaceIndex", ((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Index);
			ps.AddParameter("-Confirm", false);
			PSDataCollection<PSObject> removeResults = await ps.InvokeAsync();

			SetStatus($"Setting new IP address for {((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Name}");
			ps.Commands.Clear();
			ps.AddCommand("New-NetIPAddress");
			ps.AddParameter("-InterfaceIndex", ((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Index);
			ps.AddParameter("-IPAddress", "10.1.69.1");
			ps.AddParameter("-PrefixLength", 16);
			ps.AddParameter("-Confirm", false);
			PSDataCollection<PSObject> newResults = await ps.InvokeAsync();

			GetAdapters();
		}

		private async void tsbEnableDHCP_Click(object sender, EventArgs e) {
			PowerShell ps = PowerShell.Create();

			SetStatus($"Enabling DHCP on {((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Name}");

			ps.AddCommand("Set-NetIPInterface");
			ps.AddParameter("-InterfaceIndex", ((AdapterInfo)lsbAdapters.Items[lsbAdapters.SelectedIndex]).Index);
			ps.AddParameter("-Dhcp", "Enabled");
			ps.AddParameter("-Confirm", false);
			PSDataCollection<PSObject> dhcpResults = await ps.InvokeAsync();

			GetAdapters();
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
}
