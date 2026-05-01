namespace IPAddressChanger.DHCP_Server;
public class DHCPMessageTypes(int val, string name) {

	public static DHCPMessageTypes DHCPDISCOVER { get; } = new DHCPMessageTypes(1, "DHCPDISCOVER");
	public static DHCPMessageTypes DHCPOFFER { get; } = new DHCPMessageTypes(2, "DHCPOFFER");
	public static DHCPMessageTypes DHCPREQUEST { get; } = new DHCPMessageTypes(3, "DHCPREQUEST");
	public static DHCPMessageTypes DHCPDECLINE { get; } = new DHCPMessageTypes(4, "DHCPDECLINE");
	public static DHCPMessageTypes DHCPACK { get; } = new DHCPMessageTypes(5, "DHCPACK");
	public static DHCPMessageTypes DHCPNAK { get; } = new DHCPMessageTypes(6, "DHCPNAK");
	public static DHCPMessageTypes DHCPRELEASE { get; } = new DHCPMessageTypes(7, "DHCPRELEASE");
	public static DHCPMessageTypes DHCPINFORM { get; } = new DHCPMessageTypes(8, "DHCPINFORM");
	public static DHCPMessageTypes DHCPFORCERENEW { get; } = new DHCPMessageTypes(9, "DHCPFORCERENEW");
	public static DHCPMessageTypes DHCPLEASEQUERY { get; } = new DHCPMessageTypes(10, "DHCPLEASEQUERY");
	public static DHCPMessageTypes DHCPLEASEUNASSIGNED { get; } = new DHCPMessageTypes(11, "DHCPLEASEUNASSIGNED");
	public static DHCPMessageTypes DHCPLEASEUNKNOWN { get; } = new DHCPMessageTypes(12, "DHCPLEASEUNKNOWN");
	public static DHCPMessageTypes DHCPLEASEACTIVE { get; } = new DHCPMessageTypes(13, "DHCPLEASEACTIVE");
	public static DHCPMessageTypes DHCPBULKLEASEQUERY { get; } = new DHCPMessageTypes(14, "DHCPBULKLEASEQUERY");
	public static DHCPMessageTypes DHCPLEASEQUERYDONE { get; } = new DHCPMessageTypes(15, "DHCPLEASEQUERYDONE");
	public static DHCPMessageTypes DHCPACTIVELEASEQUERY { get; } = new DHCPMessageTypes(16, "DHCPACTIVELEASEQUERY");
	public static DHCPMessageTypes DHCPLEASEQUERYSTATUS { get; } = new DHCPMessageTypes(17, "DHCPLEASEQUERYSTATUS");
	public static DHCPMessageTypes DHCPTLS { get; } = new DHCPMessageTypes(18, "DHCPTLS");

	public int Value { get; private set; } = val;
	public string Name { get; private set; } = name;

	public static IEnumerable<DHCPMessageTypes> List() {
		return new[] { DHCPDISCOVER, DHCPOFFER, DHCPREQUEST, DHCPDECLINE, DHCPACK, DHCPNAK, DHCPRELEASE, DHCPINFORM, DHCPFORCERENEW, DHCPLEASEQUERY, DHCPLEASEUNASSIGNED, DHCPLEASEUNKNOWN, DHCPLEASEACTIVE, DHCPBULKLEASEQUERY, DHCPLEASEQUERYDONE, DHCPACTIVELEASEQUERY, DHCPLEASEQUERYSTATUS, DHCPTLS };
	}

	public static DHCPMessageTypes FromValue(int value) {
		return List().Single(r => r.Value == value);
	}

	public static DHCPMessageTypes FromName(string name) {
		return List().Single(r => String.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));
	}

	public override string ToString() {
		return Name;
	}


}