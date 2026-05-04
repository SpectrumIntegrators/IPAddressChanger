namespace IPAddressChanger.DHCP_Server;
public class LeaseEventArgs(DHCPLease lease) : EventArgs {
	public DHCPLease Lease { get; private set; } = lease;
}
