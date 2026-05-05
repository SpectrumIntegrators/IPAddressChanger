namespace IPAddressChanger.Network_Manager;
internal class IPAddressInfo {
	// MSFT_NetIPAddress.AddressState enum (NL_DAD_STATE).
	public const byte ADDRESS_STATE_INVALID = 0;
	public const byte ADDRESS_STATE_TENTATIVE = 1;
	public const byte ADDRESS_STATE_DUPLICATE = 2;
	public const byte ADDRESS_STATE_DEPRECATED = 3;
	public const byte ADDRESS_STATE_PREFERRED = 4;

	public ushort AddressFamily { get; set; }
	public string IPAddress { get; set; } = "";
	public string IPv4Address { get; set; } = "";
	public string IPv6Address { get; set; } = "";
	public byte PrefixLength { get; set; }
	public ushort PrefixOrigin { get; set; }
	public ushort SuffixOrigin { get; set; }
	public byte AddressState { get; set; }
}
