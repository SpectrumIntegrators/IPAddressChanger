using System.Buffers.Binary;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace IPAddressChanger.DHCP_Server;

/// <summary>
/// A minimal IPv4 DHCP server bound to a single Windows network adapter.
/// </summary>
/// <remarks>
/// Reservations are user-supplied and don't have an assigned/expiration or hostname (when that address is handed out, we don't go back-fill the table).
/// Leases are server-supplied and are based on the next available IP address considering existing leases and reservations. We record the expiration, but we never actually expire the reservation.
///
/// Safety: the server uses IP_PKTINFO to verify the receiving interface index of every packet
/// against <see cref="Adapter"/>. Packets that arrive on any other adapter are silently dropped,
/// guaranteeing the server cannot answer DHCP traffic on a network it isn't bound to.
/// </remarks>
public class DHCPServer : IDisposable {

	// Lease duration (seconds) we hand out to clients. Short enough that devices moved to a
	// different network don't hold a stale lease for too long; long enough to avoid renewal chatter.
	private const uint LEASE_SECONDS = 15 * 60;

	// Allowable prefix-length range for the DHCP scope. /31 has no usable hosts and /32 is a
	// single host, so we cap at /30 to guarantee room for a network address, the server, at
	// least one client, and a broadcast.
	public const int MIN_PREFIX_LENGTH = 0;
	public const int MAX_PREFIX_LENGTH = 30;

	private bool _disposed = false;

	private readonly object _disposedLock = new();
	private readonly Dictionary<string, DHCPLease> _dhcpLeases = new();
	private readonly HashSet<IPAddress> _usedAddresses = new();
	private readonly object _leasesLock = new();
	private AdapterInfo? _boundAdapter;

	private Socket? _socket;
	private CancellationTokenSource? _cts;
	private Task? _listenTask;

	private IPAddress _subnetMask = IPAddress.Any;
	private IPAddress _broadcastAddress = IPAddress.Broadcast;

	/// <summary>
	/// True if a valid network address and prefix length has been assigned and the server is ready to start
	/// </summary>
	public bool HasValidRange { get; private set; }

	public bool HasValidAddress { get; private set; }

	/// <summary>
	/// True if the DHCP server is currently running
	/// </summary>
	public bool Running { get; private set; } = false;

	public IPAddress? RangeStart { get; private set; }
	public IPAddress? RangeEnd { get; private set; }
	public IPAddress? Address { get; private set; }
	public int? PrefixLength { get; private set; }
	public AdapterInfo? Adapter {
		get {
			return _boundAdapter;
		}
	}

	public List<DHCPLease> Leases {
		get {
			lock (_leasesLock) {
				return [.. _dhcpLeases.Values];
			}
		}
	}

	public event EventHandler? ServerStarted;
	public event EventHandler? ServerStopped;
	/// <summary>Fired when a lease is created or its mutable fields (Hostname, IPAddress)
	/// change. Subscribers should treat this as "lease added or updated" — for an existing MAC,
	/// the form's listview is expected to update in place rather than add a duplicate row.</summary>
	public event EventHandler<LeaseEventArgs>? LeaseAssigned;
	/// <summary>Fired when a lease's entry is removed from the server. Currently fires when
	/// UpdateReservation changes a reservation's MAC (the old-MAC entry is replaced by a new
	/// one). The string payload is the removed MAC. Can fire on a non-UI thread.</summary>
	public event EventHandler<string>? LeaseRemoved;
	public event EventHandler<DHCPMessageEventArgs>? DeviceCommunication;
	/// <summary>Fired with a human-readable narration of decisions the server is making
	/// (e.g. "DISCOVER from MAC, new device, offering IP"). Subscribers can pipe to a debug log.</summary>
	public event EventHandler<string>? Log;
	/// <summary>Fired (after the server has been stopped) when the bound adapter's address
	/// configuration is changed out from under us — e.g. external netsh, USB unplug,
	/// sleep/wake. The string is a human-readable reason. Subscribers may show it to the
	/// user; this event can fire on a non-UI thread, so marshal as needed.</summary>
	public event EventHandler<string>? ServerFaulted;

	private void FireLog(string text) {
		try { Log?.Invoke(this, text); } catch { /* don't let a buggy subscriber crash us */ }
	}

	public DHCPServer() {

	}

	public DHCPServer(IPAddress networkAddress, int prefixLength, AdapterInfo boundAdapter) {
		SetLeaseRange(networkAddress, prefixLength);
		SetBoundAdapter(boundAdapter);
	}

	public void Dispose() {
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected void Dispose(bool disposing) {
		if (_disposed) { return; }
		lock (_disposedLock) {
			if (_disposed) { return; }
			if (disposing) {
				Stop();
				_dhcpLeases.Clear();
				_usedAddresses.Clear();
			}
			_disposed = true;
		}
	}

	/// <summary>
	/// Searches the reserved IP addresses and finds the next unused address within the subnet.
	/// </summary>
	/// <returns>IPAddress for the next unused address, or null if all addresses are leased/reserved</returns>
	/// <remarks>
	/// Caller must hold <see cref="_leasesLock"/>. The returned address is NOT yet recorded in
	/// <see cref="_usedAddresses"/>; the caller is responsible for adding it before releasing the lock,
	/// otherwise concurrent calls could return the same address.
	/// </remarks>
	private IPAddress? GetNextFreeAddress() {
		ObjectDisposedException.ThrowIf(_disposed, this);
		if (RangeStart is null || RangeEnd is null) return null;

		uint start = BinaryPrimitives.ReadUInt32BigEndian(RangeStart.GetAddressBytes());
		uint end = BinaryPrimitives.ReadUInt32BigEndian(RangeEnd.GetAddressBytes());

		for (uint i = start; i <= end; i++) {
			byte[] octets = new byte[4];
			BinaryPrimitives.WriteUInt32BigEndian(octets, i);
			IPAddress candidate = new(octets);
			// Defense-in-depth: never offer the server's own address even if _usedAddresses tracking
			// is out of sync for any reason.
			if (Address is not null && candidate.Equals(Address)) continue;
			if (!_usedAddresses.Contains(candidate)) {
				return candidate;
			}
		}
		return null;
	}

	/// <summary>
	/// Starts the DHCP server.
	/// </summary>
	public void Start() {
		ObjectDisposedException.ThrowIf(_disposed, this);
		if (!HasValidRange) {
			throw new InvalidOperationException("Network address and prefix length must be set");
		}
		if (_boundAdapter is null) {
			throw new InvalidOperationException("A bound adapter must be set before starting the server");
		}
		if (Address is null) {
			throw new InvalidOperationException("Server address has not been computed");
		}
		if (Running) { return; }

		_cts = new CancellationTokenSource();
		_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		try {
			_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
			_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			// IP_PKTINFO lets us read which interface received each datagram. Defense-in-depth
			// alongside the bind: even if a packet somehow reaches us on the wrong adapter,
			// the listen loop drops it.
			_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
			// Bind to the server's configured IP on port 67. Outgoing packets from this socket
			// use this adapter automatically because of the local IP binding.
			_socket.Bind(new IPEndPoint(Address, 67));
		} catch {
			_socket.Dispose();
			_socket = null;
			_cts.Dispose();
			_cts = null;
			throw;
		}

		Running = true;
		_listenTask = Task.Run(() => ListenLoopAsync(_cts.Token));
		// Watch for the bound adapter's address being yanked (external netsh, USB unplug,
		// sleep/wake, etc.) so we self-stop instead of zombieing on a missing IP.
		NetworkChange.NetworkAddressChanged += OnNetworkAddressChanged;
		ServerStarted?.Invoke(this, EventArgs.Empty);
	}

	/// <summary>
	/// Stops the DHCP server and waits for it to finish shutting down before returning.
	/// </summary>
	/// <remarks>Existing leases and reservations are maintained.</remarks>
	public void Stop() {
		if (_disposed) { return; }
		if (!Running) { return; }

		// Unsubscribe up front so a tear-down-induced address change doesn't re-enter the
		// fault path while we're already stopping.
		NetworkChange.NetworkAddressChanged -= OnNetworkAddressChanged;

		// Cancel the listen loop and close the socket so any in-flight ReceiveMessageFromAsync
		// throws and the loop exits.
		try { _cts?.Cancel(); } catch { /* already disposed */ }
		try { _socket?.Close(); } catch { /* already closed */ }

		try {
			_listenTask?.Wait(TimeSpan.FromSeconds(2));
		} catch { /* expected — task throws on cancel/close */ }

		_socket?.Dispose();
		_socket = null;
		_cts?.Dispose();
		_cts = null;
		_listenTask = null;
		Running = false;

		ServerStopped?.Invoke(this, EventArgs.Empty);
	}

	/// <summary>
	/// Stops the DHCP server asynchronously without blocking the caller. The returned Task completes
	/// when shutdown is finished.
	/// </summary>
	/// <remarks>Existing leases and reservations are maintained.</remarks>
	public Task StopAsync() {
		if (_disposed) { return Task.CompletedTask; }
		if (!Running) { return Task.CompletedTask; }
		return Task.Run(() => Stop());
	}

	// Defense-in-depth for the "shortcut clobbered the bound adapter" case: any time the OS
	// reports a network address change, verify our bound adapter still has our IP. If not,
	// stop and fault. Fires on a thread pool thread.
	private void OnNetworkAddressChanged(object? sender, EventArgs e) {
		if (!Running || _boundAdapter is null || Address is null) return;
		string? reason = null;
		try {
			NetworkInterface? nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(n => {
				try { return n.GetIPProperties().GetIPv4Properties()?.Index == _boundAdapter.Index; }
				catch { return false; }
			});
			if (nic is null) {
				reason = $"Bound adapter {_boundAdapter.Name} is no longer present.";
			} else if (!nic.GetIPProperties().UnicastAddresses.Any(ua => ua.Address.Equals(Address))) {
				reason = $"DHCP server's IP {Address} was removed from {_boundAdapter.Name}.";
			}
		} catch (Exception ex) {
			// If we can't check, don't fault — better to keep running and let a real bind
			// failure surface the issue. Log so it's visible if this becomes a pattern.
			Debug.WriteLine($"OnNetworkAddressChanged check failed: {ex.Message}");
			return;
		}
		if (reason is null) return;
		FireLog(reason);
		Stop();
		ServerFaulted?.Invoke(this, reason);
	}

	/// <summary>
	/// Sets the network address and prefix length to use for assigning IP addresses.
	/// </summary>
	/// <param name="serverAddress">The IPv4 address the server (and the bound adapter) will use within the subnet.</param>
	/// <param name="prefixLength">The subnet prefix length, 0–32.</param>
	/// <exception cref="InvalidOperationException">The server is currently running.</exception>
	/// <exception cref="ArgumentOutOfRangeException">The specified IPAddress does not represent an IPv4 address, or the prefix length is invalid.</exception>
	/// <summary>
	/// Computes the allocatable host-address range for a given server address and prefix length.
	/// Returns (start, end) covering every host address in the subnet excluding the network and
	/// broadcast addresses. Returns null if the inputs don't form a usable IPv4 range (non-IPv4
	/// address, or prefix outside 0–30).
	/// </summary>
	public static (IPAddress Start, IPAddress End)? TryGetHostRange(IPAddress serverAddress, int prefixLength) {
		if (serverAddress.AddressFamily != AddressFamily.InterNetwork) return null;
		if (prefixLength < MIN_PREFIX_LENGTH || prefixLength > MAX_PREFIX_LENGTH) return null;
		uint addrInt = BinaryPrimitives.ReadUInt32BigEndian(serverAddress.GetAddressBytes());
		uint mask = prefixLength == 0 ? 0u : (uint.MaxValue << (32 - prefixLength));
		uint network = addrInt & mask;
		uint broadcast = network | ~mask;
		byte[] startBytes = new byte[4];
		BinaryPrimitives.WriteUInt32BigEndian(startBytes, network + 1);
		byte[] endBytes = new byte[4];
		BinaryPrimitives.WriteUInt32BigEndian(endBytes, broadcast - 1);
		return (new IPAddress(startBytes), new IPAddress(endBytes));
	}

	public void SetLeaseRange(IPAddress serverAddress, int prefixLength) {
		ObjectDisposedException.ThrowIf(_disposed, this);
		if (Running) {
			throw new InvalidOperationException("Can not set lease range while server is running");
		}
		HasValidRange = false;
		HasValidAddress = false;
		if (serverAddress.AddressFamily != AddressFamily.InterNetwork) {
			throw new ArgumentOutOfRangeException(nameof(serverAddress), "AddressFamily must be InterNetwork (IPv4)");
		}
		ArgumentOutOfRangeException.ThrowIfLessThan(prefixLength, MIN_PREFIX_LENGTH, nameof(prefixLength));
		ArgumentOutOfRangeException.ThrowIfGreaterThan(prefixLength, MAX_PREFIX_LENGTH, nameof(prefixLength));

		uint addrInt = BinaryPrimitives.ReadUInt32BigEndian(serverAddress.GetAddressBytes());
		uint mask = prefixLength == 0 ? 0u : (uint.MaxValue << (32 - prefixLength));
		uint network = addrInt & mask;
		uint broadcast = network | ~mask;

		// The network address (host bits all 0) and broadcast address (host bits all 1) are not
		// valid host addresses — Windows refuses to bind UDP to them, and they're not legal to
		// hand out to clients either. Reject them up front with a clear message.
		if (addrInt == network && prefixLength < 31) {
			throw new ArgumentException($"Server address {serverAddress} is the network address of the /{prefixLength} subnet and cannot be used", nameof(serverAddress));
		}
		if (addrInt == broadcast && prefixLength < 31) {
			throw new ArgumentException($"Server address {serverAddress} is the broadcast address of the /{prefixLength} subnet and cannot be used", nameof(serverAddress));
		}

		byte[] maskBytes = new byte[4];
		BinaryPrimitives.WriteUInt32BigEndian(maskBytes, mask);
		_subnetMask = new IPAddress(maskBytes);

		byte[] bcastBytes = new byte[4];
		BinaryPrimitives.WriteUInt32BigEndian(bcastBytes, broadcast);
		_broadcastAddress = new IPAddress(bcastBytes);

		// Allocatable range: every host address in the subnet that isn't the network or broadcast.
		// The server's own address is excluded by being added to _usedAddresses below, so it can
		// fall anywhere in the subnet (no requirement that it be network+1).
		byte[] startBytes = new byte[4];
		BinaryPrimitives.WriteUInt32BigEndian(startBytes, network + 1);
		byte[] endBytes = new byte[4];
		BinaryPrimitives.WriteUInt32BigEndian(endBytes, broadcast - 1);

		lock (_leasesLock) {
			// Drop the previous server address from used-addresses if we had one, so re-running
			// SetLeaseRange with a different IP doesn't permanently reserve the old one.
			if (Address is not null) {
				_usedAddresses.Remove(Address);
			}

			RangeStart = new IPAddress(startBytes);
			RangeEnd = new IPAddress(endBytes);
			Address = serverAddress;
			PrefixLength = prefixLength;

			_usedAddresses.Add(Address);
		}

		HasValidRange = true;
		HasValidAddress = true;
	}

	public void SetBoundAdapter(AdapterInfo boundAdapter) {
		ObjectDisposedException.ThrowIf(_disposed, this);
		if (Running) {
			throw new InvalidOperationException("Can't change bound adapter while server is running");
		}
		if (!boundAdapter.IsConnected) {
			throw new InvalidOperationException("Can't bind to a disconnected adapter");
		}
		if (!boundAdapter.IsEnabled) {
			throw new InvalidOperationException("Can't bind to a disabled adapter");
		}
		_boundAdapter = boundAdapter;
	}

	/// <summary>
	/// Attempts to add an address reservation.
	/// </summary>
	/// <returns>True if the MAC and IP addresses did not already exist in the reservations/leases and were successfully added, false if either was already used.</returns>
	public bool TryAddReservation(string macAddress, IPAddress ipAddress, out string? errorMessage) {
		ObjectDisposedException.ThrowIf(_disposed, this);
		string normalizedMac = macAddress.ToUpperInvariant();
		lock (_leasesLock) {
			if (_dhcpLeases.ContainsKey(normalizedMac)) {
				errorMessage = "MAC address already has a reservation";
				return false;
			}

			// Reserved IP must be a host address inside the configured subnet — otherwise the
			// client would lease an address that doesn't match its gateway/mask and won't work.
			// If no range is set yet (no SetLeaseRange call), allow — caller is in setup.
			if (RangeStart is not null && RangeEnd is not null) {
				if (ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork) {
					errorMessage = "Reserved address must be IPv4";
					return false;
				}
				uint ipInt = BinaryPrimitives.ReadUInt32BigEndian(ipAddress.GetAddressBytes());
				uint startInt = BinaryPrimitives.ReadUInt32BigEndian(RangeStart.GetAddressBytes());
				uint endInt = BinaryPrimitives.ReadUInt32BigEndian(RangeEnd.GetAddressBytes());
				if (ipInt < startInt || ipInt > endInt) {
					errorMessage = $"Reserved address {ipAddress} is outside the configured subnet ({RangeStart}-{RangeEnd})";
					return false;
				}
			}

			if (_usedAddresses.Contains(ipAddress)) {
				errorMessage = "IP address already reserved";
				return false;
			}

			var lease = new DHCPLease(normalizedMac, ipAddress, "", null, null);
			_dhcpLeases.Add(normalizedMac, lease);
			_usedAddresses.Add(ipAddress);
			Debug.WriteLine($"Leases count: {_dhcpLeases.Count}");
			LeaseAssigned?.Invoke(this, new LeaseEventArgs(lease));
			errorMessage = null;
			return true;
		}
	}

	/// <summary>
	/// Changes the MAC and/or IP of an existing reservation/lease atomically. Validates the
	/// new pair against duplicates and the configured range, then either mutates the existing
	/// lease (IP-only change) or replaces it with a fresh entry that carries the original
	/// hostname/assigned/expires forward (MAC change).
	/// </summary>
	/// <returns>True if the reservation was updated, or already had the requested values.
	/// False if the old MAC isn't known, the new pair is invalid, or either the new MAC or new IP
	/// collides with another existing lease.</returns>
	public bool UpdateReservation(string oldMacAddress, string newMacAddress, IPAddress newIpAddress, out string? errorMessage) {
		ObjectDisposedException.ThrowIf(_disposed, this);
		string oldMac = oldMacAddress.ToUpperInvariant();
		string newMac = newMacAddress.ToUpperInvariant();
		lock (_leasesLock) {
			if (!_dhcpLeases.TryGetValue(oldMac, out DHCPLease? lease) || lease is null) {
				errorMessage = "No reservation or lease exists for that MAC address";
				return false;
			}
			bool macChanged = oldMac != newMac;
			bool ipChanged = !lease.IPAddress.Equals(newIpAddress);
			if (!macChanged && !ipChanged) {
				// No change — succeed quietly without firing events.
				errorMessage = null;
				return true;
			}
			if (newIpAddress.AddressFamily != AddressFamily.InterNetwork) {
				errorMessage = "Reserved address must be IPv4";
				return false;
			}
			if (RangeStart is not null && RangeEnd is not null) {
				uint ipInt = BinaryPrimitives.ReadUInt32BigEndian(newIpAddress.GetAddressBytes());
				uint startInt = BinaryPrimitives.ReadUInt32BigEndian(RangeStart.GetAddressBytes());
				uint endInt = BinaryPrimitives.ReadUInt32BigEndian(RangeEnd.GetAddressBytes());
				if (ipInt < startInt || ipInt > endInt) {
					errorMessage = $"Address {newIpAddress} is outside the configured subnet ({RangeStart}-{RangeEnd})";
					return false;
				}
			}
			// Duplicate checks ignore the entry being edited (it's about to be removed/replaced).
			if (macChanged && _dhcpLeases.ContainsKey(newMac)) {
				errorMessage = "MAC address already has a reservation";
				return false;
			}
			if (ipChanged && _usedAddresses.Contains(newIpAddress)) {
				errorMessage = "IP address already reserved";
				return false;
			}
			if (macChanged) {
				// MAC is the lease's identity (set at construction), so changing it means
				// replacing the entry with a new DHCPLease that carries hostname/assigned/expires
				// forward. Fire LeaseRemoved for the old MAC so the form drops its old row.
				_dhcpLeases.Remove(oldMac);
				_usedAddresses.Remove(lease.IPAddress);
				var newLease = new DHCPLease(newMac, newIpAddress, lease.Hostname, lease.Assigned, lease.Expires);
				_dhcpLeases.Add(newMac, newLease);
				_usedAddresses.Add(newIpAddress);
				LeaseRemoved?.Invoke(this, oldMac);
				LeaseAssigned?.Invoke(this, new LeaseEventArgs(newLease));
			} else {
				// MAC unchanged, IP changed: mutate in place; the form's idempotent
				// AddLeaseListViewItem will update the existing row.
				_usedAddresses.Remove(lease.IPAddress);
				lease.IPAddress = newIpAddress;
				_usedAddresses.Add(newIpAddress);
				LeaseAssigned?.Invoke(this, new LeaseEventArgs(lease));
			}
			errorMessage = null;
			return true;
		}
	}

	/// <summary>
	/// Adds a reservation for the specified MAC address.
	/// </summary>
	/// <exception cref="ArgumentException">The MAC or IP address is already part of a reservation or lease.</exception>
	public void AddReservation(string macAddress, IPAddress ipAddress) {
		ObjectDisposedException.ThrowIf(_disposed, this);
		// we don't lock here because TryAddReservation locks
		if (!TryAddReservation(macAddress, ipAddress, out string? errorMessage)) {
			throw new ArgumentException(errorMessage);
		}
	}

	/// <summary>
	/// Gets the IP address reserved for or leased to the specified MAC address, or null if the address has no reservation/lease.
	/// </summary>
	public IPAddress? GetLeasedIPAddress(string macAddress) {
		ObjectDisposedException.ThrowIf(_disposed, this);
		string normalizedMac = macAddress.ToUpperInvariant();
		lock (_leasesLock) {
			if (_dhcpLeases.TryGetValue(normalizedMac, out DHCPLease? lease)) {
				return lease?.IPAddress;
			}
			return null;
		}
	}

	/// <summary>
	/// Removes a reservation or lease for the specified MAC address. It is safe to attempt to remove a reservation that does not exist.
	/// </summary>
	/// <remarks>Warning: It is possible that the DHCP server will reassign this address at some point, so be sure that when you remove a reservation/lease that the device is no longer online or using the IP address it was assigned.</remarks>
	public void RemoveLease(string macAddress) {
		ObjectDisposedException.ThrowIf(_disposed, this);
		string normalizedMac = macAddress.ToUpperInvariant();
		lock (_leasesLock) {
			if (_dhcpLeases.TryGetValue(normalizedMac, out DHCPLease? lease)) {
				_usedAddresses.Remove(lease?.IPAddress ?? IPAddress.Any);
				_dhcpLeases.Remove(normalizedMac);
				Debug.WriteLine($"Leases count: {_dhcpLeases.Count}");
			}
		}
	}

	/// <summary>
	/// Returns a snapshot of leases/reservations whose IP falls outside the currently-configured
	/// [RangeStart, RangeEnd]. Returns an empty list if no range is set.
	/// </summary>
	public List<DHCPLease> GetLeasesOutsideRange() {
		ObjectDisposedException.ThrowIf(_disposed, this);
		if (RangeStart is null || RangeEnd is null) return [];
		uint startInt = BinaryPrimitives.ReadUInt32BigEndian(RangeStart.GetAddressBytes());
		uint endInt = BinaryPrimitives.ReadUInt32BigEndian(RangeEnd.GetAddressBytes());
		List<DHCPLease> result = [];
		lock (_leasesLock) {
			foreach (var lease in _dhcpLeases.Values) {
				if (lease.IPAddress.AddressFamily != AddressFamily.InterNetwork) {
					result.Add(lease);
					continue;
				}
				uint ipInt = BinaryPrimitives.ReadUInt32BigEndian(lease.IPAddress.GetAddressBytes());
				if (ipInt < startInt || ipInt > endInt) {
					result.Add(lease);
				}
			}
		}
		return result;
	}

	// ===================================================================================
	// Listen loop and packet handling
	// ===================================================================================

	private async Task ListenLoopAsync(CancellationToken ct) {
		var buffer = new byte[2048];
		var receiveFrom = (EndPoint)new IPEndPoint(IPAddress.Any, 0);

		while (!ct.IsCancellationRequested && _socket is not null) {
			SocketReceiveMessageFromResult result;
			try {
				result = await _socket.ReceiveMessageFromAsync(buffer, SocketFlags.None, receiveFrom, ct);
			} catch (OperationCanceledException) {
				break;
			} catch (ObjectDisposedException) {
				break;
			} catch (SocketException) {
				// Socket was closed during shutdown, or transient network error. Exit the loop;
				// the caller's Stop() flow handles cleanup.
				break;
			}

			// Drop packets that arrived on a different adapter than the one we're bound to.
			// This is the key safety guarantee — even if Windows hands us a packet from another
			// network (which shouldn't happen with our bind, but defense-in-depth), we ignore it.
			if (_boundAdapter is not null && result.PacketInformation.Interface != _boundAdapter.Index) {
				continue;
			}

			DHCPPacket? packet = DHCPPacket.Parse(buffer, result.ReceivedBytes);
			if (packet is null || packet.Op != DHCPPacket.OP_REQUEST || packet.MessageType is null) {
				continue;
			}

			try {
				await HandlePacketAsync(packet);
			} catch (Exception ex) {
				Debug.WriteLine($"DHCP packet handler error: {ex.Message}");
			}
		}
	}

	private Task HandlePacketAsync(DHCPPacket packet) {
		IPAddress clientAddress = packet.CIAddr.Equals(IPAddress.Any) ? packet.YIAddr : packet.CIAddr;
		DeviceCommunication?.Invoke(this, new DHCPMessageEventArgs(packet.MacAddress, clientAddress, packet.MessageType!, DateTime.Now, DHCPMessageDirection.Received));

		var msg = packet.MessageType!;
		if (msg == DHCPMessageTypes.DHCPDISCOVER) return HandleDiscoverAsync(packet);
		if (msg == DHCPMessageTypes.DHCPREQUEST) return HandleRequestAsync(packet);
		if (msg == DHCPMessageTypes.DHCPINFORM) return HandleInformAsync(packet);
		if (msg == DHCPMessageTypes.DHCPDECLINE) return HandleDeclineAsync(packet);
		if (msg == DHCPMessageTypes.DHCPRELEASE) return Task.CompletedTask; // by design we don't free leases

		// Other types (server-to-server query, etc.) — ignore.
		return Task.CompletedTask;
	}

	/// <summary>
	/// DISCOVER → allocate (or reuse for known MAC) and OFFER.
	/// </summary>
	private async Task HandleDiscoverAsync(DHCPPacket packet) {
		string mac = packet.MacAddress;
		IPAddress? offerAddress = null;
		DHCPLease? newLease = null;
		DHCPLease? hostnameUpdatedLease = null;

		lock (_leasesLock) {
			if (_dhcpLeases.TryGetValue(mac, out DHCPLease? existing) && existing is not null) {
				// MAC is already known — re-offer the same address (sticky leases per design).
				offerAddress = existing.IPAddress;
				if (TryUpdateHostname(existing, packet.Hostname)) {
					hostnameUpdatedLease = existing;
				}
			} else {
				IPAddress? candidate = GetNextFreeAddress();
				if (candidate is null) {
					FireLog($"DISCOVER from {mac}: pool exhausted, can't offer");
					return;
				}
				offerAddress = candidate;
				newLease = new DHCPLease(mac, candidate, packet.Hostname, DateTime.Now, DateTime.Now.AddSeconds(LEASE_SECONDS));
				_dhcpLeases.Add(mac, newLease);
				_usedAddresses.Add(candidate);
			}
		}

		if (newLease is not null) {
			FireLog($"DISCOVER from {mac}: new device, offering {offerAddress}");
			LeaseAssigned?.Invoke(this, new LeaseEventArgs(newLease));
		} else if (hostnameUpdatedLease is not null) {
			FireLog($"DISCOVER from {mac}: known device, re-offering {offerAddress} (hostname updated to '{hostnameUpdatedLease.Hostname}')");
			LeaseAssigned?.Invoke(this, new LeaseEventArgs(hostnameUpdatedLease));
		} else {
			FireLog($"DISCOVER from {mac}: known device, re-offering {offerAddress}");
		}

		var offer = BuildReply(packet, DHCPMessageTypes.DHCPOFFER, offerAddress!);
		await SendPacketAsync(offer, packet);
	}

	// Updates an existing lease's hostname from a packet's Option 12 if it carries a non-empty,
	// different hostname. Caller must hold _leasesLock. Returns true if the hostname changed.
	private static bool TryUpdateHostname(DHCPLease lease, string packetHostname) {
		if (string.IsNullOrEmpty(packetHostname)) return false;
		if (lease.Hostname == packetHostname) return false;
		lease.Hostname = packetHostname;
		return true;
	}

	/// <summary>
	/// REQUEST → ACK with options (and renew the lease's expires).
	/// If the requested IP doesn't match what we'd give this MAC, send NAK.
	/// </summary>
	private async Task HandleRequestAsync(DHCPPacket packet) {
		string mac = packet.MacAddress;
		IPAddress? ourAddress = null;
		DHCPLease? hostnameUpdatedLease = null;

		lock (_leasesLock) {
			if (_dhcpLeases.TryGetValue(mac, out DHCPLease? lease) && lease is not null) {
				ourAddress = lease.IPAddress;
				if (TryUpdateHostname(lease, packet.Hostname)) {
					hostnameUpdatedLease = lease;
				}
			}
		}

		if (hostnameUpdatedLease is not null) {
			FireLog($"REQUEST from {mac}: hostname updated to '{hostnameUpdatedLease.Hostname}'");
			LeaseAssigned?.Invoke(this, new LeaseEventArgs(hostnameUpdatedLease));
		}

		IPAddress? requested = packet.RequestedAddress ?? (packet.CIAddr.Equals(IPAddress.Any) ? null : packet.CIAddr);

		if (ourAddress is null) {
			// Client requesting from a server that has no record — NAK to make them re-DISCOVER.
			FireLog($"REQUEST from {mac} for {requested?.ToString() ?? "<no address>"}: no record for this MAC, sending NAK");
			var nak = BuildReply(packet, DHCPMessageTypes.DHCPNAK, IPAddress.Any);
			await SendPacketAsync(nak, packet);
			return;
		}

		if (requested is not null && !requested.Equals(ourAddress)) {
			// Client wants a different IP than we'd give them — refuse.
			FireLog($"REQUEST from {mac} for {requested}: doesn't match our record ({ourAddress}), sending NAK");
			var nak = BuildReply(packet, DHCPMessageTypes.DHCPNAK, IPAddress.Any);
			await SendPacketAsync(nak, packet);
			return;
		}

		FireLog($"REQUEST from {mac} for {ourAddress}: confirmed, sending ACK");
		var ack = BuildReply(packet, DHCPMessageTypes.DHCPACK, ourAddress);
		await SendPacketAsync(ack, packet);
	}

	/// <summary>
	/// INFORM → ACK with options but no IP allocation. Client already has its IP, just wants config.
	/// </summary>
	private async Task HandleInformAsync(DHCPPacket packet) {
		// For INFORM the client's address is already in CIAddr. Return options for that address.
		var ack = BuildReply(packet, DHCPMessageTypes.DHCPACK, packet.CIAddr);
		// INFORM responses must NOT include lease time per RFC 2131 §4.3.5.
		ack.Options.Remove(DHCPPacket.OPT_LEASE_TIME);
		await SendPacketAsync(ack, packet);
	}

	/// <summary>
	/// DECLINE → the client says the offered IP is already in use on the network. Mark it as
	/// permanently unavailable for the rest of the session and remove the lease record.
	/// </summary>
	private Task HandleDeclineAsync(DHCPPacket packet) {
		string mac = packet.MacAddress;
		IPAddress? declined = packet.RequestedAddress;

		lock (_leasesLock) {
			if (_dhcpLeases.TryGetValue(mac, out DHCPLease? lease) && lease is not null) {
				_dhcpLeases.Remove(mac);
				// Keep the IP in _usedAddresses so we never offer it again this session.
				// (The client said it's in use, so it's permanently a conflict source.)
			} else if (declined is not null) {
				_usedAddresses.Add(declined);
			}
		}
		return Task.CompletedTask;
	}

	/// <summary>
	/// Builds a reply packet (OFFER, ACK, NAK) populated with our standard option set.
	/// </summary>
	private DHCPPacket BuildReply(DHCPPacket request, DHCPMessageTypes type, IPAddress yiaddr) {
		var reply = new DHCPPacket {
			Op = DHCPPacket.OP_REPLY,
			HType = request.HType,
			HLen = request.HLen,
			Hops = 0,
			Xid = request.Xid,
			Secs = 0,
			Flags = request.Flags,
			CIAddr = request.CIAddr,
			YIAddr = yiaddr,
			SIAddr = Address ?? IPAddress.Any,
			GIAddr = request.GIAddr,
		};
		Array.Copy(request.CHAddr, reply.CHAddr, 16);

		reply.SetMessageType(type);
		reply.SetServerIdentifier(Address ?? IPAddress.Any);

		if (type != DHCPMessageTypes.DHCPNAK) {
			reply.SetSubnetMask(_subnetMask);
			// Use the server's own address as the gateway — devices that require a pingable gateway
			// will ARP us; devices that try to route through us drop into the OS's IP-forwarding
			// path which is disabled by default, so packets harmlessly black-hole.
			if (Address is not null) reply.SetRouter(Address);
			// DNS: server IP as primary, 1.1.1.1 as a fallback. Neither will resolve from this
			// adapter (we're not running DNS and there's no upstream), but devices that require a
			// DNS option to accept the lease are satisfied.
			if (Address is not null) {
				reply.SetDnsServers(Address, IPAddress.Parse("1.1.1.1"));
			}
			reply.SetLeaseTime(LEASE_SECONDS);
		}

		return reply;
	}

	/// <summary>
	/// Sends a built reply packet. Routes to broadcast or unicast based on the request's
	/// broadcast flag and whether the client already has an IP.
	/// </summary>
	private async Task SendPacketAsync(DHCPPacket reply, DHCPPacket originalRequest) {
		if (_socket is null) return;

		byte[] data = reply.Build();

		// Per RFC 2131 §4.1: if the broadcast flag is set OR the client has no IP, broadcast.
		// Otherwise, unicast to the client's existing IP.
		IPEndPoint destination;
		bool clientHasIp = !originalRequest.CIAddr.Equals(IPAddress.Any);
		if (originalRequest.BroadcastFlag || !clientHasIp || reply.MessageType == DHCPMessageTypes.DHCPNAK) {
			destination = new IPEndPoint(IPAddress.Broadcast, 68);
		} else {
			destination = new IPEndPoint(originalRequest.CIAddr, 68);
		}

		try {
			await _socket.SendToAsync(data, SocketFlags.None, destination);
			DeviceCommunication?.Invoke(this, new DHCPMessageEventArgs(
				originalRequest.MacAddress,
				reply.YIAddr,
				reply.MessageType!,
				DateTime.Now,
				DHCPMessageDirection.Sent));
		} catch (Exception ex) {
			FireLog($"Send error to {destination}: {ex.Message}");
		}
	}
}
