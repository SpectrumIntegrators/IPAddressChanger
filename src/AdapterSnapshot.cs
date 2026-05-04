using System.Net.NetworkInformation;

namespace IPAddressChanger;

internal class AdapterSnapshot {
	public string Name { get; set; } = "";
	public OperationalStatus Status { get; set; }
	public long Speed { get; set; }
	public HashSet<string> IPs { get; set; } = new();
}