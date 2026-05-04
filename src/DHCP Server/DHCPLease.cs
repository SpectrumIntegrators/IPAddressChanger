using System.Net;

namespace IPAddressChanger.DHCP_Server;
public class DHCPLease(string macAddress, IPAddress ipAddress, string hostname, DateTime? assigned, DateTime? expires) {
	/// <summary>
	/// The MAC address for this lease/reservation
	/// </summary>
	public string MACAddress { get; private set; } = macAddress.ToUpperInvariant();
	/// <summary>
	/// The IP address assigned to this reservation (either manually via reservation or automatically from the server).
	/// Updated by the server when an existing reservation's IP is edited via UpdateReservation.
	/// </summary>
	public IPAddress IPAddress { get; internal set; } = ipAddress;
	/// <summary>
	/// The hostname supplied by the device asking for an address; not set for manual reservations,
	/// but the server updates it from the client's Option 12 on later DISCOVER/REQUEST packets so
	/// reservations end up with the device's hostname once it phones home.
	/// </summary>
	public string Hostname { get; internal set; } = hostname;

	/// <summary>
	/// The time the lease was assigned; not set for manual reservations
	/// </summary>
	public DateTime? Assigned { get; private set; } = assigned;
	/// <summary>
	/// The time the lease will expire; not set for manual reservations (note that we don't expire the lease, but this tells the device to request a new address at or before that time)
	/// </summary>
	public DateTime? Expires { get; private set; } = expires;
}
