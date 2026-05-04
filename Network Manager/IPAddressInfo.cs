namespace IPAddressChanger.Network_Manager;
internal class IPAddressInfo {
	public ushort AddressFamily { get; set; }
	public string IPAddress { get; set; } = "";
	public string IPv4Address { get; set; } = "";
	public string IPv6Address { get; set; } = "";
	public byte PrefixLength { get; set; }
	public ushort PrefixOrigin { get; set; }
	public ushort SuffixOrigin { get; set; }
}
