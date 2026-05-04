using System.Net;

namespace IPAddressChanger.DHCP_Server;
public class DHCPLease(string macAddress, IPAddress ipAddress, string hostname, DateTime? assigned, DateTime? expires) {
	/// <summary>
	/// The MAC address for this lease/reservation
	/// </summary>
	public string MACAddress { get; private set; } = macAddress.ToUpperInvariant();
	/// <summary>
	/// The IP address assigned to this reservation (either manually via reservation or automatically from the server)
	/// </summary>
	public IPAddress IPAddress { get; private set; } = ipAddress;
	/// <summary>
	/// The hostname supplied by the device asking for an address; not set for manual reservations
	/// </summary>
	public string Hostname { get; private set; } = hostname;

	/// <summary>
	/// The time the lease was assigned; not set for manual reservations
	/// </summary>
	public DateTime? Assigned { get; private set; } = assigned;
	/// <summary>
	/// The time the lease will expire; not set for manual reservations (note that we don't expire the lease, but this tells the device to request a new address at or before that time)
	/// </summary>
	public DateTime? Expires { get; private set; } = expires;
}
