using Microsoft.Management.Infrastructure;

namespace IPAddressChanger {

	internal class IPAddressInfo {
		public UInt16 AddressFamily { get; set; }
		public string IPAddress { get; set; } = "";
		public string IPv4Address { get; set; } = "";
		public string IPv6Address { get; set; } = "";
		public byte PrefixLength { get; set; }
		public UInt16 PrefixOrigin { get; set; }
		public UInt16 SuffixOrigin { get; set; }
	}

	internal static class NetworkManager {
		private const string Namespace = @"root\StandardCimv2";

		public static Task<List<AdapterInfo>> GetAdaptersAsync() => Task.Run(GetAdapters);

		private static List<AdapterInfo> GetAdapters() {
			var list = new List<AdapterInfo>();
			using CimSession session = CimSession.Create(null);
			foreach (CimInstance ci in session.QueryInstances(Namespace, "WQL", "SELECT * FROM MSFT_NetAdapter")) {
				using (ci) {
					var p = ci.CimInstanceProperties;
					UInt32 operational = (p["InterfaceOperationalStatus"]?.Value as UInt32?) ?? 0;
					UInt32 admin = (p["InterfaceAdminStatus"]?.Value as UInt32?) ?? 0;
					list.Add(new AdapterInfo(
						(p["InterfaceIndex"]?.Value as UInt32?) ?? 0,
						p["Name"]?.Value?.ToString() ?? "<unknown>",
						p["DriverDescription"]?.Value?.ToString() ?? "<unknown>",
						operational == 1,
						admin == 1,
						(p["Speed"]?.Value as UInt64?) ?? 0,
						p["PermanentAddress"]?.Value?.ToString() ?? "<unknown>",
						p["DeviceID"]?.Value?.ToString() ?? "<unknown>"
					));
				}
			}
			return list;
		}

		public static Task<List<IPAddressInfo>> GetIPAddressesAsync(uint interfaceIndex) =>
			Task.Run(() => GetIPAddresses(interfaceIndex));

		private static List<IPAddressInfo> GetIPAddresses(uint interfaceIndex) {
			var list = new List<IPAddressInfo>();
			using CimSession session = CimSession.Create(null);
			string query = $"SELECT * FROM MSFT_NetIPAddress WHERE InterfaceIndex={interfaceIndex}";
			foreach (CimInstance ci in session.QueryInstances(Namespace, "WQL", query)) {
				using (ci) {
					var p = ci.CimInstanceProperties;
					list.Add(new IPAddressInfo {
						AddressFamily = (p["AddressFamily"]?.Value as UInt16?) ?? 0,
						IPAddress = p["IPAddress"]?.Value?.ToString() ?? "",
						IPv4Address = p["IPv4Address"]?.Value?.ToString() ?? "",
						IPv6Address = p["IPv6Address"]?.Value?.ToString() ?? "",
						PrefixLength = (p["PrefixLength"]?.Value as byte?) ?? 0,
						PrefixOrigin = (p["PrefixOrigin"]?.Value as UInt16?) ?? 0,
						SuffixOrigin = (p["SuffixOrigin"]?.Value as UInt16?) ?? 0,
					});
				}
			}
			return list;
		}

		public static Task SetDhcpAsync(uint interfaceIndex, bool enabled) =>
			Task.Run(() => SetDhcp(interfaceIndex, enabled));

		private static void SetDhcp(uint interfaceIndex, bool enabled) {
			using CimSession session = CimSession.Create(null);
			// Set-NetIPInterface without -AddressFamily applies to all families on the interface;
			// preserve that behavior. Some interfaces (e.g. IPv6 link-local only) reject the change,
			// so swallow per-instance failures rather than aborting the whole operation.
			string query = $"SELECT * FROM MSFT_NetIPInterface WHERE InterfaceIndex={interfaceIndex}";
			foreach (CimInstance iface in session.QueryInstances(Namespace, "WQL", query)) {
				using (iface) {
					CimProperty? dhcpProp = iface.CimInstanceProperties["Dhcp"];
					if (dhcpProp is null) continue;
					dhcpProp.Value = (byte)(enabled ? 1 : 0);
					try {
						session.ModifyInstance(Namespace, iface);
					} catch (CimException) {
					}
				}
			}
		}

		public static Task RemoveAllIPAddressesAsync(uint interfaceIndex) =>
			Task.Run(() => RemoveAllIPAddresses(interfaceIndex));

		private static void RemoveAllIPAddresses(uint interfaceIndex) {
			using CimSession session = CimSession.Create(null);
			string query = $"SELECT * FROM MSFT_NetIPAddress WHERE InterfaceIndex={interfaceIndex}";
			List<CimInstance> addresses = session.QueryInstances(Namespace, "WQL", query).ToList();
			foreach (CimInstance addr in addresses) {
				using (addr) {
					try {
						session.DeleteInstance(Namespace, addr);
					} catch (CimException) {
					}
				}
			}
		}

		public static Task NewIPAddressAsync(uint interfaceIndex, string ipAddress, byte prefixLength) =>
			Task.Run(() => NewIPAddress(interfaceIndex, ipAddress, prefixLength));

		private static void NewIPAddress(uint interfaceIndex, string ipAddress, byte prefixLength) {
			using CimSession session = CimSession.Create(null);
			using var inputs = new CimMethodParametersCollection {
				CimMethodParameter.Create("InterfaceIndex", interfaceIndex, CimType.UInt32, CimFlags.In),
				CimMethodParameter.Create("IPAddress", ipAddress, CimType.String, CimFlags.In),
				CimMethodParameter.Create("PrefixLength", prefixLength, CimType.UInt8, CimFlags.In),
			};
			using CimMethodResult result = session.InvokeMethod(Namespace, "MSFT_NetIPAddress", "Create", inputs);
			UInt32 returnCode = (result.ReturnValue?.Value as UInt32?) ?? 0;
			if (returnCode != 0) {
				throw new CimException($"MSFT_NetIPAddress.Create returned error code {returnCode}");
			}
		}

		public static string DescribeProperties(IEnumerable<CimProperty> properties) {
			List<string> values = new();
			foreach (CimProperty p in properties) {
				values.Add($"[{p.CimType}] {p.Name} = {p.Value ?? "null"}");
			}
			return string.Join("; ", values);
		}
	}
}
