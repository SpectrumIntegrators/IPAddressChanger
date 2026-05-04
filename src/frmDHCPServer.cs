using IPAddressChanger.DHCP_Server;
using IPAddressChanger.Network_Manager;
using IPAddressChanger.Properties;
using System.Net;
using System.Net.Sockets;

namespace IPAddressChanger;

public partial class frmDHCPServer : Form {
	private readonly TextBox[] _octetTextBoxes;
	private readonly TextBox[] _allAddressTextBoxes;
	private readonly frmDebug _debugForm;
	private readonly DHCPServer _dhcpServer;
	private readonly Dictionary<string, ListViewItem> _leaseItems = [];
	public frmDHCPServer(DHCPServer dhcpServer, frmDebug debugForm) {
		InitializeComponent();
		_dhcpServer = dhcpServer;
		_debugForm = debugForm;
		_octetTextBoxes = [
			txtAddressOctet1,
			txtAddressOctet2,
			txtAddressOctet3,
			txtAddressOctet4
		];
		_allAddressTextBoxes = [
			txtAddressOctet1,
			txtAddressOctet2,
			txtAddressOctet3,
			txtAddressOctet4,
			txtPrefixLength
		];
		foreach (var tb in _allAddressTextBoxes) {
			tb.KeyPress += AddressTextBox_KeyPress;
			tb.KeyDown += AddressTextBox_KeyDown;
		}
	}

	// Lets the five address textboxes feel like a single field: '.' or '/' advances to the
	// next box, and Backspace on an empty box jumps back and trims one character off the
	// previous one.
	private void AddressTextBox_KeyPress(object? sender, KeyPressEventArgs e) {
		if (e.KeyChar != '.' && e.KeyChar != '/' && e.KeyChar != '\\') return;
		if (sender is not TextBox tb) return;
		int idx = Array.IndexOf(_allAddressTextBoxes, tb);
		if (idx < 0) return;
		e.Handled = true; // always swallow — these are separators, not data
		if (idx >= _allAddressTextBoxes.Length - 1) return;
		var next = _allAddressTextBoxes[idx + 1];
		next.Focus();
		next.SelectionStart = next.TextLength;
		next.SelectionLength = 0;
	}

	private void AddressTextBox_KeyDown(object? sender, KeyEventArgs e) {
		if (e.KeyCode != Keys.Back) return;
		if (sender is not TextBox tb || tb.TextLength != 0) return;
		int idx = Array.IndexOf(_allAddressTextBoxes, tb);
		if (idx <= 0) return;
		e.SuppressKeyPress = true;
		var prev = _allAddressTextBoxes[idx - 1];
		prev.Focus();
		if (prev.TextLength > 0) {
			prev.Select(prev.TextLength - 1, 1);
			prev.SelectedText = "";
		}
		prev.SelectionStart = prev.TextLength;
		prev.SelectionLength = 0;
	}

	private async void frmDHCPServer_Load(object sender, EventArgs e) {
		_dhcpServer.ServerStarted += this._dhcpServer_ServerStarted;
		_dhcpServer.ServerStopped += this._dhcpServer_ServerStopped;
		_dhcpServer.LeaseAssigned += this._dhcpServer_LeaseAssigned;
		_dhcpServer.DeviceCommunication += this._dhcpServer_DeviceCommunication;

		// Restore previously-configured address and prefix from the server (the textboxes are
		// not persisted directly; the server is the source of truth across form open/close).
		if (_dhcpServer.Address is not null) {
			byte[] octets = _dhcpServer.Address.GetAddressBytes();
			txtAddressOctet1.Text = octets[0].ToString();
			txtAddressOctet2.Text = octets[1].ToString();
			txtAddressOctet3.Text = octets[2].ToString();
			txtAddressOctet4.Text = octets[3].ToString();
		}
		if (_dhcpServer.PrefixLength is not null) {
			txtPrefixLength.Text = _dhcpServer.PrefixLength.Value.ToString();
		}

		if (_dhcpServer.Running) {
			// setting this to true will fire the check state changed event which will disable the server properties controls
			chkEnableDHCPServer.Checked = true;
		}

		if (_dhcpServer.Leases.Count > 0) {
			_debugForm.AddMessage($"Adding {_dhcpServer.Leases.Count} leases/reservations to the list");
			foreach (var lease in _dhcpServer.Leases) {
				AddLeaseListViewItem(lease);
			}
		} else {
			_debugForm.AddMessage("No leases/reservations in the DHCP server, the list will be empty");
		}


		List<AdapterInfo> adapters = await NetworkManager.GetAdaptersAsync();
		int cboItemIdx = 0;
		bool foundBoundAdapter = false;
		foreach (var adapter in adapters) {
			cboAdapters.Items.Add(adapter);
			if (adapter == _dhcpServer.Adapter) {
				cboAdapters.SelectedIndex = cboItemIdx;
				foundBoundAdapter = true;
			}
			cboItemIdx++;
		}
		if (!foundBoundAdapter && _dhcpServer.Adapter != null) {
			// the combo box at this point should have adapters but none selected, and it will be disabled so the user can't change it until they disable the server
			_debugForm.AddMessage($"DHCP server is bound to an adapter that doesn't exist anymore (currently bound to {_dhcpServer.Adapter})");
		}


	}

	private void _dhcpServer_DeviceCommunication(object? sender, DHCPMessageEventArgs e) {
		BeginInvoke(() => {
			_leaseItems.TryGetValue(e.MACAddress, out ListViewItem? item);
			if (item != null) {
				string text = $"{e.DirectionLabel}: {e.Message}";
				if (item.SubItems.Count >= 6) {
					item.SubItems[5].Text = text;
				} else {
					item.SubItems.Add(text);
				}
			}
		});
	}
	private void _dhcpServer_LeaseAssigned(object? sender, LeaseEventArgs e) {
		BeginInvoke(() => {
			AddLeaseListViewItem(e.Lease);
		});
	}
	private void _dhcpServer_ServerStopped(object? sender, EventArgs e) {
		BeginInvoke(() => chkEnableDHCPServer.Checked = false);
	}

	private void _dhcpServer_ServerStarted(object? sender, EventArgs e) {
		if (InvokeRequired) {
			BeginInvoke(() => _dhcpServer_ServerStarted(sender, e));
			return;
		}
		chkEnableDHCPServer.Checked = true;
	}

	private void tsbAddCustomReservation_Click(object sender, EventArgs e) {
		// Resolve the range to validate against. When the server is running it has its own
		// range; otherwise compute a tentative one from the textboxes so reservations can't be
		// added outside a subnet the user has typed but not yet applied.
		IPAddress? rangeStart;
		IPAddress? rangeEnd;
		if (_dhcpServer.RangeStart is not null && _dhcpServer.RangeEnd is not null) {
			rangeStart = _dhcpServer.RangeStart;
			rangeEnd = _dhcpServer.RangeEnd;
		} else {
			if (!IPAddress.TryParse(GetAddressFromTextBoxes(), out IPAddress? tentativeAddress)
				|| !int.TryParse(txtPrefixLength.Text, out int tentativePrefix)
				|| DHCPServer.TryGetHostRange(tentativeAddress, tentativePrefix) is not (IPAddress s, IPAddress en)) {
				MessageBox.Show("Set a valid IP address and prefix length before adding a reservation.", "Address Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			rangeStart = s;
			rangeEnd = en;
		}
		var _ = frmAddDHCPReservation.ShowNewDialog(_dhcpServer, _debugForm, rangeStart, rangeEnd, this);
	}

	private void tsbDeleteLease_Click(object sender, EventArgs e) {
		
		if (lsvDHCPLeases.SelectedItems.Count == 0) {
			return;
		}

		// remove the selected mac addresses from the dhcp lease reservations
		foreach (ListViewItem toDelete in lsvDHCPLeases.SelectedItems) {
			RemoveLeaseEverywhere(toDelete.SubItems[0].Text);
		}
		
	}

	// Removes a lease from the server and from this form's view. MAC is the listview key.
	private void RemoveLeaseEverywhere(string macAddress) {
		_dhcpServer.RemoveLease(macAddress);
		if (_leaseItems.TryGetValue(macAddress, out ListViewItem? item)) {
			lsvDHCPLeases.Items.Remove(item);
			_leaseItems.Remove(macAddress);
		}
	}

	private void lsvDHCPLeases_SelectedIndexChanged(object sender, EventArgs e) {
		tsbDeleteLease.Enabled = lsvDHCPLeases.SelectedItems.Count > 0;
	}

	private string GetAddressFromTextBoxes() {
		return string.Join(".", [txtAddressOctet1.Text, txtAddressOctet2.Text, txtAddressOctet3.Text, txtAddressOctet4.Text]);
	}

	private async void chkEnableDHCPServer_Click(object sender, EventArgs e) {
		IPAddress? serverAddress = null;
		int prefixLength = 0;

		if (!_dhcpServer.Running) {
			// we only validate these fields if we're trying to START the server
			if (cboAdapters.SelectedItem is not AdapterInfo selectedAdapter) {
				MessageBox.Show("Select an adapter to bind to", "Select an Adapter", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				goto EnableDHCPServerBailout;
			}

			if (!IPAddress.TryParse(GetAddressFromTextBoxes(), out serverAddress)) {
				MessageBox.Show("Entered values do not form a valid IPv4 address", "Invalid Address", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				goto EnableDHCPServerBailout;
			}

			if (!int.TryParse(txtPrefixLength.Text, out prefixLength) || prefixLength < DHCPServer.MIN_PREFIX_LENGTH || prefixLength > DHCPServer.MAX_PREFIX_LENGTH) {
				MessageBox.Show($"Prefix length must be a number between {DHCPServer.MIN_PREFIX_LENGTH} and {DHCPServer.MAX_PREFIX_LENGTH}", "Invalid Prefix Length", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				goto EnableDHCPServerBailout;
			}

			// If the user typed the network address (host bits all zero), bump up to network+1 —
			// that's almost always what they meant. Update the textboxes so the correction is
			// visible and the user knows we adjusted it.
			byte[] addrBytes = serverAddress.GetAddressBytes();
			uint addrInt = (uint)((addrBytes[0] << 24) | (addrBytes[1] << 16) | (addrBytes[2] << 8) | addrBytes[3]);
			uint mask = prefixLength == 0 ? 0u : (uint.MaxValue << (32 - prefixLength));
			uint network = addrInt & mask;
			uint broadcast = network | ~mask;
			if (addrInt == network && prefixLength < 31) {
				uint corrected = network + 1;
				serverAddress = new([
					(byte)((corrected >> 24) & 0xFF),
					(byte)((corrected >> 16) & 0xFF),
					(byte)((corrected >> 8) & 0xFF),
					(byte)(corrected & 0xFF)
				]);
				addrBytes = serverAddress.GetAddressBytes();
				txtAddressOctet1.Text = addrBytes[0].ToString();
				txtAddressOctet2.Text = addrBytes[1].ToString();
				txtAddressOctet3.Text = addrBytes[2].ToString();
				txtAddressOctet4.Text = addrBytes[3].ToString();
				_debugForm.AddMessage($"Adjusted server address to {serverAddress} (network address is not a valid host)");
			} else if (addrInt == broadcast && prefixLength < 31) {
				uint corrected = broadcast - 1;
				serverAddress = new IPAddress([
					(byte)((corrected >> 24) & 0xFF),
					(byte)((corrected >> 16) & 0xFF),
					(byte)((corrected >> 8) & 0xFF),
					(byte)(corrected & 0xFF)
				]);
				addrBytes = serverAddress.GetAddressBytes();
				txtAddressOctet1.Text = addrBytes[0].ToString();
				txtAddressOctet2.Text = addrBytes[1].ToString();
				txtAddressOctet3.Text = addrBytes[2].ToString();
				txtAddressOctet4.Text = addrBytes[3].ToString();
				_debugForm.AddMessage($"Adjusted server address to {serverAddress} (broadcast address is not a valid host)");
			}

			try {
				_dhcpServer.SetBoundAdapter(selectedAdapter);
				_dhcpServer.SetLeaseRange(serverAddress, prefixLength);
			} catch (Exception ex) {
				MessageBox.Show($"Could not configure DHCP server: {ex.Message}", "DHCP Server Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_debugForm.AddMessage($"Error configuring DHCP server: {ex.Message}");
				goto EnableDHCPServerBailout;
			}

			// Existing reservations from a prior subnet may now be outside the new range. Prompt
			// the user before starting so they can clear them out (or knowingly keep them).
			List<DHCPLease> outOfRange = _dhcpServer.GetLeasesOutsideRange();
			if (outOfRange.Count > 0) {
				string subnet = $"{_dhcpServer.RangeStart}-{_dhcpServer.RangeEnd}";
				DialogResult choice = MessageBox.Show(
					$"{outOfRange.Count} reservation(s) fall outside the new subnet ({subnet}).\r\n\r\nDo you want to clear them before starting the server?",
					"Reservations Outside Subnet",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);
				if (choice == DialogResult.Cancel) {
					_debugForm.AddMessage("DHCP server start cancelled (reservations outside new subnet)");
					goto EnableDHCPServerBailout;
				}
				if (choice == DialogResult.Yes) {
					foreach (var lease in outOfRange) {
						RemoveLeaseEverywhere(lease.MACAddress);
					}
					_debugForm.AddMessage($"Removed {outOfRange.Count} out-of-range reservation(s)");
				} else {
					_debugForm.AddMessage($"Keeping {outOfRange.Count} out-of-range reservation(s) at user's request");
				}
			}
		}

		// enable or disable the dhcp server (checkbox is only enabled if all of the entered values are correct)
		if (chkEnableDHCPServer.Checked) {
			var _ = await EnableDHCPServer();
		} else {
			var _ = await DisableDHCPServer();
		}
		return;
	EnableDHCPServerBailout:
		chkEnableDHCPServer.Checked = false;
	}

	private void chkEnableDHCPServer_CheckedChanged(object sender, EventArgs e) {
		cboAdapters.Enabled = !chkEnableDHCPServer.Checked;
		foreach (TextBox t in _octetTextBoxes) {
			t.Enabled = !chkEnableDHCPServer.Checked;
		}
		txtPrefixLength.Enabled = !chkEnableDHCPServer.Checked;
	}

	private void frmDHCPServer_FormClosing(object sender, FormClosingEventArgs e) {
		_dhcpServer.ServerStarted -= this._dhcpServer_ServerStarted;
		_dhcpServer.ServerStopped -= this._dhcpServer_ServerStopped;
		_dhcpServer.LeaseAssigned -= this._dhcpServer_LeaseAssigned;
		_dhcpServer.DeviceCommunication -= this._dhcpServer_DeviceCommunication;
	}

	private void AddLeaseListViewItem(DHCPLease lease) {
		// Update in place when the MAC is already in the list. Can happen when the server
		// silently drops a lease (e.g. DHCPDECLINE removes from _dhcpLeases without firing
		// LeaseRemoved) and the same MAC then re-DISCOVERs, or when BeginInvoke ordering
		// reorders LeaseAssigned callbacks against a user-driven delete-then-reserve.
		if (_leaseItems.TryGetValue(lease.MACAddress, out ListViewItem? existing)) {
			_debugForm.AddMessage($"Updating lease for {lease.MACAddress} in list");
			existing.SubItems[1].Text = lease.IPAddress.ToString();
			existing.SubItems[2].Text = lease.Hostname;
			existing.SubItems[3].Text = lease.Assigned?.ToString() ?? "Reserved";
			existing.SubItems[4].Text = lease.Expires?.ToString() ?? "Reserved";
			return;
		}
		_debugForm.AddMessage($"Adding lease for {lease.MACAddress} to list");
		ListViewItem newLease = new() {
			Text = lease.MACAddress
		};
		newLease.SubItems.Add(lease.IPAddress.ToString());
		newLease.SubItems.Add(lease.Hostname);
		newLease.SubItems.Add(lease.Assigned?.ToString() ?? "Reserved");
		newLease.SubItems.Add(lease.Expires?.ToString() ?? "Reserved");
		lsvDHCPLeases.Items.Add(newLease);
		_leaseItems.Add(lease.MACAddress, newLease);
	}

	private async Task<bool> EnableDHCPServer() {
		if (_dhcpServer.Adapter == null) {
			MessageBox.Show("Select an adapter to bind to", "Select an Adapter", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return false;
		}

		_debugForm.AddMessage($"Starting DHCP server on adapter {_dhcpServer.Adapter.Name} with range {_dhcpServer.RangeStart?.ToString()}-{_dhcpServer.RangeEnd?.ToString()}");

		string desiredAddress = _dhcpServer.Address!.ToString();
		byte desiredPrefix = (byte)_dhcpServer.PrefixLength!.Value;
		uint adapterIndex = _dhcpServer.Adapter.Index;

		using var busy = new frmDHCPServerBusy("Starting DHCP server...");
		busy.Show(this);
		var ct = busy.CancellationToken;

		bool BailCancelled() {
			_debugForm.AddMessage("DHCP server start cancelled by user");
			if (!busy.IsDisposed) busy.Close();
			return false;
		}

		try {
			busy.SetStatus("Checking adapter DHCP state");
			bool boundAdapterDHCPEnabled = await NetworkManager.GetIPv4DhcpEnabledAsync(adapterIndex);
			if (ct.IsCancellationRequested) return BailCancelled();

			if (boundAdapterDHCPEnabled) {
				_debugForm.AddMessage("Selected DHCP server adapter has DHCP enabled, we will disable it");
				busy.SetStatus("Disabling DHCP on adapter");
				await NetworkManager.SetDhcpAsync(adapterIndex, false);
			}
			if (ct.IsCancellationRequested) return BailCancelled();

			busy.SetStatus($"Checking adapter for {desiredAddress}/{desiredPrefix}");
			List<IPAddressInfo> boundAdapterAddresses = await NetworkManager.GetIPAddressesAsync(adapterIndex);
			bool adapterHasTheRightAddress = false;
			bool addressBoundWithDifferentPrefix = false;

			foreach (IPAddressInfo addr in boundAdapterAddresses) {
				if (addr.AddressFamily != 2) continue; // IPv4 only
				if (addr.IPAddress != desiredAddress) continue;
				if (addr.PrefixLength == desiredPrefix) {
					adapterHasTheRightAddress = true;
				} else {
					addressBoundWithDifferentPrefix = true;
				}
			}
			if (ct.IsCancellationRequested) return BailCancelled();

			if (addressBoundWithDifferentPrefix) {
				// Same IP, wrong prefix — remove it now so the add-at-correct-prefix step below
				// can re-create it. Without this, NewIPAddressAsync would collide.
				_debugForm.AddMessage($"{desiredAddress} is on adapter at a different prefix — removing so we can re-add at /{desiredPrefix}");
				busy.SetStatus($"Updating prefix on {desiredAddress}");
				try {
					await NetworkManager.RemoveIPAddressAsync(adapterIndex, desiredAddress);
				} catch (Exception ex) {
					busy.Close();
					MessageBox.Show($"Could not remove existing {desiredAddress} from adapter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					_debugForm.AddMessage($"Failed to remove existing address: {ex.Message}");
					return false;
				}
				if (ct.IsCancellationRequested) return BailCancelled();
				// adapterHasTheRightAddress is already false; the add path below will run.
			}

			if (!adapterHasTheRightAddress) {
				_debugForm.AddMessage($"Adding {desiredAddress}/{desiredPrefix} to adapter {_dhcpServer.Adapter.Name}");
				busy.SetStatus($"Adding {desiredAddress}/{desiredPrefix} to adapter");
				try {
					await NetworkManager.NewIPAddressAsync(adapterIndex, desiredAddress, desiredPrefix);
				} catch (Exception ex) {
					busy.Close();
					MessageBox.Show($"Could not add {desiredAddress}/{desiredPrefix} to adapter: {ex.Message}", "Error Adding Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
					_debugForm.AddMessage($"Failed to add address: {ex.Message}");
					return false;
				}
				if (ct.IsCancellationRequested) return BailCancelled();
			}

			// Remove any OTHER IPv4 addresses left on the adapter so the DHCP server is the only
			// IPv4 identity on the wire. Done AFTER the desired address is up so a partial failure
			// leaves the user with at least our address rather than a stripped adapter.
			busy.SetStatus("Removing other IPv4 addresses from adapter");
			List<IPAddressInfo> currentAddresses = await NetworkManager.GetIPAddressesAsync(adapterIndex);
			foreach (IPAddressInfo addr in currentAddresses) {
				if (ct.IsCancellationRequested) return BailCancelled();
				if (addr.AddressFamily != 2) continue;
				if (addr.IPAddress == desiredAddress) continue;
				_debugForm.AddMessage($"Removing existing IPv4 address {addr.IPAddress}/{addr.PrefixLength} from adapter");
				busy.SetStatus($"Removing {addr.IPAddress} from adapter");
				try {
					await NetworkManager.RemoveIPAddressAsync(adapterIndex, addr.IPAddress);
				} catch (Exception ex) {
					_debugForm.AddMessage($"Failed to remove {addr.IPAddress}: {ex.Message} (continuing)");
				}
			}
			if (ct.IsCancellationRequested) return BailCancelled();

			// The CIM address-add returns before the TCP/IP stack will accept a bind to that
			// address — empirically there's a 2–3 s gap between "address visible in management"
			// and "Bind() works". Poll the bind directly instead of polling a proxy signal.
			busy.SetStatus("Starting DHCP listener");
			const int maxBindAttempts = 40; // ~10 s at 250 ms
			SocketException? lastBindEx = null;
			for (int attempt = 1; attempt <= maxBindAttempts; attempt++) {
				try {
					_dhcpServer.Start();
					lastBindEx = null;
					break;
				} catch (SocketException sex) when (sex.SocketErrorCode == SocketError.AddressNotAvailable) {
					lastBindEx = sex;
					if (attempt == 1) _debugForm.AddMessage("Stack not ready for bind, retrying...");
					busy.SetStatus($"Waiting for stack to accept bind — attempt {attempt}, {maxBindAttempts - attempt} remaining");
					try {
						await Task.Delay(250, ct);
					} catch (OperationCanceledException) {
						return BailCancelled();
					}
				} catch (Exception ex) {
					busy.Close();
					MessageBox.Show($"Error starting DHCP server: {ex.Message}", "Error Starting DHCP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
					_debugForm.AddMessage($"Error starting DHCP server: {ex.Message}");
					return false;
				}
			}
			if (lastBindEx is not null) {
				busy.Close();
				MessageBox.Show($"DHCP server could not bind to {desiredAddress}:67 — {lastBindEx.Message}", "Error Starting DHCP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_debugForm.AddMessage($"DHCP server bind failed after {maxBindAttempts} attempts: {lastBindEx.Message}");
				return false;
			}

			busy.Close();
			return true;
		} catch (OperationCanceledException) {
			return BailCancelled();
		}
	}

	private async Task<bool> DisableDHCPServer() {
		_debugForm.AddMessage("Stopping DHCP server");
		try {
			await _dhcpServer.StopAsync();
		} catch (Exception ex) {
			MessageBox.Show($"Error stopping DHCP server: {ex.Message}", "Error Stopping DHCP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
			_debugForm.AddMessage($"Error stopping DHCP server: {ex.Message}");
			return false;
		}
		return true;
	}


}
