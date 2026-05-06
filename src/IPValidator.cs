using System.Text.RegularExpressions;

namespace IPAddressChanger;

internal static partial class IPValidator {
	[GeneratedRegex(@"(?:^(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)/(?:3[0-2]|[12]?\d)$)|(?:^DHCP$)")]
	public static partial Regex IpCidrOrDhcp();
}