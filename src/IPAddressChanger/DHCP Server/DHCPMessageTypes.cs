using Ardalis.SmartEnum;

namespace IPAddressChanger.DHCP_Server;

public sealed class DHCPMessageTypes : SmartEnum<DHCPMessageTypes> {

	public static readonly DHCPMessageTypes DHCPDISCOVER = new(nameof(DHCPDISCOVER), 1);
	public static readonly DHCPMessageTypes DHCPOFFER = new(nameof(DHCPOFFER), 2);
	public static readonly DHCPMessageTypes DHCPREQUEST = new(nameof(DHCPREQUEST), 3);
	public static readonly DHCPMessageTypes DHCPDECLINE = new(nameof(DHCPDECLINE), 4);
	public static readonly DHCPMessageTypes DHCPACK = new(nameof(DHCPACK), 5);
	public static readonly DHCPMessageTypes DHCPNAK = new(nameof(DHCPNAK), 6);
	public static readonly DHCPMessageTypes DHCPRELEASE = new(nameof(DHCPRELEASE), 7);
	public static readonly DHCPMessageTypes DHCPINFORM = new(nameof(DHCPINFORM), 8);
	public static readonly DHCPMessageTypes DHCPFORCERENEW = new(nameof(DHCPFORCERENEW), 9);
	public static readonly DHCPMessageTypes DHCPLEASEQUERY = new(nameof(DHCPLEASEQUERY), 10);
	public static readonly DHCPMessageTypes DHCPLEASEUNASSIGNED = new(nameof(DHCPLEASEUNASSIGNED), 11);
	public static readonly DHCPMessageTypes DHCPLEASEUNKNOWN = new(nameof(DHCPLEASEUNKNOWN), 12);
	public static readonly DHCPMessageTypes DHCPLEASEACTIVE = new(nameof(DHCPLEASEACTIVE), 13);
	public static readonly DHCPMessageTypes DHCPBULKLEASEQUERY = new(nameof(DHCPBULKLEASEQUERY), 14);
	public static readonly DHCPMessageTypes DHCPLEASEQUERYDONE = new(nameof(DHCPLEASEQUERYDONE), 15);
	public static readonly DHCPMessageTypes DHCPACTIVELEASEQUERY = new(nameof(DHCPACTIVELEASEQUERY), 16);
	public static readonly DHCPMessageTypes DHCPLEASEQUERYSTATUS = new(nameof(DHCPLEASEQUERYSTATUS), 17);
	public static readonly DHCPMessageTypes DHCPTLS = new(nameof(DHCPTLS), 18);

	private DHCPMessageTypes(string name, int value) : base(name, value) { }

}
