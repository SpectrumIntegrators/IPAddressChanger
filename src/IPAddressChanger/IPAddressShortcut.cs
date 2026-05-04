namespace IPAddressChanger;
public class IPAddressShortcut {
	public string DeviceID { get; set; } = "";
	public string AdapterName { get; set; } = "";
	public string Name { get; set; } = "";
	public string IPAddress { get; set; } = "";
	public int PrefixLength { get; set; } = 0;
	public bool UseDHCP { get; set; } = false;

}
