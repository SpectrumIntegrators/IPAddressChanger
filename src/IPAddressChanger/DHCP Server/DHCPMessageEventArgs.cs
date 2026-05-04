using System.Net;

namespace IPAddressChanger.DHCP_Server;

public enum DHCPMessageDirection {
	Received,
	Sent,
}

public class DHCPMessageEventArgs(string macAddress, IPAddress ipAddress, DHCPMessageTypes message, DateTime timestamp, DHCPMessageDirection direction = DHCPMessageDirection.Received) : EventArgs {
	public string MACAddress { get; private set; } = macAddress;
	public IPAddress? IPAddress { get; private set; } = ipAddress;
	public DHCPMessageTypes Message { get; private set; } = message;
	public DateTime Timestamp { get; private set; } = timestamp;
	public DHCPMessageDirection Direction { get; private set; } = direction;

	/// <summary>"TX" for sent, "RX" for received — convenient for log/UI prefixes.</summary>
	public string DirectionLabel => Direction == DHCPMessageDirection.Sent ? "TX" : "RX";
}
