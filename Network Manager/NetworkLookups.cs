using System.Collections.Frozen;

namespace IPAddressChanger.Network_Manager;
internal static class NetworkLookups {
	internal static readonly FrozenDictionary<int, string> AddressFamilies;
	internal static readonly FrozenDictionary<int, string> PrefixOrigins;
	internal static readonly FrozenDictionary<int, string> SuffixOrigins;
	static NetworkLookups() {
		PrefixOrigins = (new Dictionary<int, string> {
			[0] = "Other",
			[1] = "Manual",
			[2] = "Well Known",
			[3] = "DHCP",
			[4] = "Router Advertisement"
		}).ToFrozenDictionary();

		SuffixOrigins = (new Dictionary<int, string> {
			[0] = "Other",
			[1] = "Manual",
			[2] = "Well Known",
			[3] = "Origin DHCP",
			[4] = "Link Layer Address",
			[5] = "Random"
		}).ToFrozenDictionary();

		AddressFamilies = (new Dictionary<int, string> {
			[-1] = "Unknown",
			[0] = "Unspecified",
			[1] = "Unix",
			[2] = "IPv4",
			[3] = "ImpLink",
			[4] = "Pup",
			[5] = "Chaos",
			[6] = "Ipx or NS",
			[7] = "Iso or Osi",
			[8] = "Ecma",
			[9] = "DataKit",
			[10] = "Ccitt",
			[11] = "Sna",
			[12] = "DecNet",
			[13] = "DataLink",
			[14] = "Lat",
			[15] = "HyperChannel",
			[16] = "AppleTalk",
			[17] = "NetBios",
			[18] = "VoiceView",
			[19] = "FireFox",
			[21] = "Banyan",
			[22] = "Atm",
			[23] = "IPv6",
			[24] = "Cluster",
			[25] = "Ieee12844",
			[26] = "Irda",
			[28] = "NetworkDesigners",
			[29] = "Max",
			[65536] = "Packet",
			[65537] = "ControllerAreaNetwork"
		}).ToFrozenDictionary();
	}
}
