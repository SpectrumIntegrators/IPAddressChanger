using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using IPAddressChanger.DHCP_Server;

namespace IPAddressChanger;
public partial class frmAddDHCPReservation : Form {
	private readonly DHCPServer _dhcpServer;
	private readonly frmDebug _debugForm;
	private readonly IPAddress _rangeStart;
	private readonly IPAddress _rangeEnd;
	public frmAddDHCPReservation(DHCPServer dhcpServer, frmDebug debugForm, IPAddress rangeStart, IPAddress rangeEnd) {
		InitializeComponent();
		_dhcpServer = dhcpServer;
		_debugForm = debugForm;
		_rangeStart = rangeStart;
		_rangeEnd = rangeEnd;
	}

	public static DialogResult ShowNewDialog(DHCPServer dhcpServer, frmDebug debugForm, IPAddress rangeStart, IPAddress rangeEnd, IWin32Window? owner) {
		using frmAddDHCPReservation me = new(dhcpServer, debugForm, rangeStart, rangeEnd);
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

		// Range check up front. The server's TryAddReservation does this too when its range is
		// set, but here we use the tentative range supplied by the parent form so reservations
		// can't sneak in outside the (possibly not-yet-applied) subnet.
		if (newIPAddress.AddressFamily != AddressFamily.InterNetwork) {
			MessageBox.Show("Reserved address must be IPv4", "Invalid Address", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			DialogResult = DialogResult.None;
			return;
		}
		uint ipInt = BinaryPrimitives.ReadUInt32BigEndian(newIPAddress.GetAddressBytes());
		uint startInt = BinaryPrimitives.ReadUInt32BigEndian(_rangeStart.GetAddressBytes());
		uint endInt = BinaryPrimitives.ReadUInt32BigEndian(_rangeEnd.GetAddressBytes());
		if (ipInt < startInt || ipInt > endInt) {
			MessageBox.Show($"Reserved address {newIPAddress} is outside the configured subnet ({_rangeStart}-{_rangeEnd})", "Address Out of Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
