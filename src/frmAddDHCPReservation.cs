using System.Net;
using IPAddressChanger.DHCP_Server;

namespace IPAddressChanger;
public partial class frmAddDHCPReservation : Form {
	private readonly DHCPServer _dhcpServer;
	private readonly frmDebug _debugForm;
	public frmAddDHCPReservation(DHCPServer dhcpServer, frmDebug debugForm) {
		InitializeComponent();
		_dhcpServer = dhcpServer;
		_debugForm = debugForm;
	}

	public static DialogResult ShowNewDialog(DHCPServer dhcpServer, frmDebug debugForm, IWin32Window? owner) {
		using frmAddDHCPReservation me = new(dhcpServer, debugForm);
		return me.ShowDialog(owner);
	}

	/// <summary>
	/// Accepts colon-, dash-, dot-separated, or unseparated 12-hex-digit MAC strings.
	/// Returns the normalized "AA:BB:CC:DD:EE:FF" form, or null if the input isn't a valid MAC.
	/// </summary>
	private static string? NormalizeMACAddress(string toValidate) {
		if (string.IsNullOrWhiteSpace(toValidate)) return null;
		string trimmed = toValidate.Trim();
		foreach (char c in trimmed) {
			if (!char.IsAsciiHexDigit(c) && c != ':' && c != '-' && c != '.') return null;
		}
		var hex = new System.Text.StringBuilder(12);
		foreach (char c in trimmed) {
			if (char.IsAsciiHexDigit(c)) hex.Append(c);
		}
		if (hex.Length != 12) return null;
		var sb = new System.Text.StringBuilder(17);
		for (int i = 0; i < 6; i++) {
			if (i > 0) sb.Append(':');
			sb.Append(char.ToUpperInvariant(hex[i * 2]));
			sb.Append(char.ToUpperInvariant(hex[i * 2 + 1]));
		}
		return sb.ToString();
	}

	private IPAddress? ValidateIPAddress(string toValidate) {
		IPAddress? ip;
		var _ = IPAddress.TryParse(toValidate, out ip);
		return ip;
	}
	private void cmdOK_Click(object sender, EventArgs e) {
		string? newMACAddress = NormalizeMACAddress(txtMACAddress.Text);
		if (newMACAddress == null) {
			MessageBox.Show("MAC address is not valid", "Invalid Address", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			DialogResult = DialogResult.None;
			return;
		}

		IPAddress? newIPAddress;
		newIPAddress = ValidateIPAddress(txtIPAddress.Text.Trim());
		if (newIPAddress == null) {
			MessageBox.Show("IP address is not valid", "Invalid Address", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			DialogResult = DialogResult.None;
			return;
		}


		if (!_dhcpServer.TryAddReservation(newMACAddress, newIPAddress, out string? errorMessage)) {
			MessageBox.Show($"{errorMessage}", "Address Already Reserved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			DialogResult = DialogResult.None;
			return;

		}

		// not assigned, reserve them
		_debugForm.AddMessage($"DHCP reservation added: {newMACAddress}: {newIPAddress.ToString()}");
		DialogResult = DialogResult.OK;
		// addresses were valid, unused, and have been added to the lease reservations
		
	}
}
