namespace IPAddressChanger;
public class AdapterInfo {
	internal UInt32 Index { get; set; } = 0;
	internal string Name { get; set; } = "";
	internal string Driver { get; set; } = "";
	internal bool IsConnected { get; set; } = false;
	internal bool IsEnabled { get; set; } = false;
	internal UInt64 Speed { get; set; } = 0;
	internal string HardwareAddress { get; set; } = "";
	internal string DeviceID { get; set; } = "";

	public AdapterInfo() {
		Index = 0;
		Name = "";
		Driver = "";
		IsConnected = false;
		IsEnabled = false;
		Speed = 0;
		HardwareAddress = "";
		DeviceID = "";
	}
	public AdapterInfo(UInt32 index = 0, string name = "", string driver = "", bool isConnected = false, bool isEnabled = false, UInt64 speed = 0, string hardwareAddress = "", string deviceID = "") {
		Index = index;
		Name = name;
		Driver = driver;
		IsConnected = isConnected;
		IsEnabled = isEnabled;
		Speed = speed;
		HardwareAddress = hardwareAddress;
		DeviceID = deviceID;
	}

	public string Status {
		get {
			return (IsEnabled ? (IsConnected ? "UP" : "down") : "disabled");
		}
	}
	public override string ToString() {
		return $"{Index}: {Name} [{Status}]";
	}

	// Equality is by DeviceID so that Dictionary<AdapterInfo, ...> lookups stay valid
	// across a refresh that produces new AdapterInfo references for the same physical adapter.
	public override bool Equals(object? obj) {
		return obj is AdapterInfo other && other.DeviceID == DeviceID;
	}
	public override int GetHashCode() {
		return DeviceID?.GetHashCode() ?? 0;
	}

	public static bool operator ==(AdapterInfo? left, AdapterInfo? right) {
		if (left is null) return right is null;
		return left.Equals(right);
	}

	public static bool operator !=(AdapterInfo? left, AdapterInfo? right) {
		return !(left == right);
	}
}