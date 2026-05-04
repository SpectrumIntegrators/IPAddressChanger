using System.Buffers.Binary;
using System.Net;
using System.Text;

namespace IPAddressChanger.DHCP_Server;

/// <summary>
/// Parses and constructs DHCP/BOOTP packets per RFC 2131 (header) and RFC 2132 (options).
/// </summary>
/// <remarks>
/// Wire format: 240-byte BOOTP header + 4-byte magic cookie (0x63 0x82 0x53 0x63) + variable options
/// terminated by option 255. All multi-byte integer fields are big-endian (network byte order).
/// </remarks>
internal class DHCPPacket {
	// Op values
	public const byte OP_REQUEST = 1;
	public const byte OP_REPLY = 2;

	// HType values
	public const byte HTYPE_ETHERNET = 1;

	// Common options
	public const byte OPT_SUBNET_MASK = 1;
	public const byte OPT_ROUTER = 3;
	public const byte OPT_DNS_SERVERS = 6;
	public const byte OPT_HOSTNAME = 12;
	public const byte OPT_REQUESTED_IP = 50;
	public const byte OPT_LEASE_TIME = 51;
	public const byte OPT_MESSAGE_TYPE = 53;
	public const byte OPT_SERVER_IDENTIFIER = 54;
	public const byte OPT_PARAMETER_REQUEST_LIST = 55;
	public const byte OPT_END = 255;
	public const byte OPT_PAD = 0;

	// BOOTP header (240 bytes total)
	public byte Op;
	public byte HType;
	public byte HLen;
	public byte Hops;
	public uint Xid;
	public ushort Secs;
	public ushort Flags;
	public IPAddress CIAddr = IPAddress.Any;
	public IPAddress YIAddr = IPAddress.Any;
	public IPAddress SIAddr = IPAddress.Any;
	public IPAddress GIAddr = IPAddress.Any;
	public byte[] CHAddr = new byte[16];
	public string SName = "";
	public string File = "";

	public Dictionary<byte, byte[]> Options = [];

	/// <summary>True if the broadcast bit is set in the flags field.</summary>
	public bool BroadcastFlag => (Flags & 0x8000) != 0;

	/// <summary>Returns the client hardware address formatted as an upper-case colon-separated MAC string (using only HLen bytes).</summary>
	public string MacAddress {
		get {
			int len = Math.Min(HLen, (byte)16);
			if (len == 0) len = 6;
			var sb = new StringBuilder(len * 3);
			for (int i = 0; i < len; i++) {
				if (i > 0) sb.Append(':');
				sb.AppendFormat("{0:X2}", CHAddr[i]);
			}
			return sb.ToString();
		}
	}

	public DHCPMessageTypes? MessageType {
		get {
			if (Options.TryGetValue(OPT_MESSAGE_TYPE, out var data) && data.Length >= 1) {
				if (DHCPMessageTypes.TryFromValue(data[0], out var ret)) {
					return ret;
				}
			}
			return null;
		}
	}

	public IPAddress? RequestedAddress {
		get {
			if (Options.TryGetValue(OPT_REQUESTED_IP, out var data) && data.Length == 4) {
				return new IPAddress(data);
			}
			return null;
		}
	}

	public IPAddress? ServerIdentifier {
		get {
			if (Options.TryGetValue(OPT_SERVER_IDENTIFIER, out var data) && data.Length == 4) {
				return new IPAddress(data);
			}
			return null;
		}
	}

	public string Hostname {
		get {
			if (Options.TryGetValue(OPT_HOSTNAME, out var data)) {
				return Encoding.ASCII.GetString(data).TrimEnd('\0');
			}
			return "";
		}
	}

	/// <summary>
	/// Parses a received DHCP packet. Returns null if the buffer is too short, doesn't have
	/// the DHCP magic cookie, or is otherwise malformed.
	/// </summary>
	public static DHCPPacket? Parse(byte[] buffer, int length) {
		// 240 BOOTP header + 4 magic cookie minimum
		if (length < 240 + 4) return null;

		var packet = new DHCPPacket {
			Op = buffer[0],
			HType = buffer[1],
			HLen = buffer[2],
			Hops = buffer[3],
			Xid = BinaryPrimitives.ReadUInt32BigEndian(buffer.AsSpan(4, 4)),
			Secs = BinaryPrimitives.ReadUInt16BigEndian(buffer.AsSpan(8, 2)),
			Flags = BinaryPrimitives.ReadUInt16BigEndian(buffer.AsSpan(10, 2)),
			CIAddr = new IPAddress(buffer.AsSpan(12, 4).ToArray()),
			YIAddr = new IPAddress(buffer.AsSpan(16, 4).ToArray()),
			SIAddr = new IPAddress(buffer.AsSpan(20, 4).ToArray()),
			GIAddr = new IPAddress(buffer.AsSpan(24, 4).ToArray()),
		};
		Array.Copy(buffer, 28, packet.CHAddr, 0, 16);
		packet.SName = Encoding.ASCII.GetString(buffer, 44, 64).TrimEnd('\0');
		packet.File = Encoding.ASCII.GetString(buffer, 108, 128).TrimEnd('\0');

		// Magic cookie check
		if (buffer[236] != 0x63 || buffer[237] != 0x82 || buffer[238] != 0x53 || buffer[239] != 0x63) {
			return null;
		}

		// Options parsing
		int pos = 240;
		while (pos < length) {
			byte optType = buffer[pos++];
			if (optType == OPT_PAD) continue;
			if (optType == OPT_END) break;
			if (pos >= length) break;
			byte optLen = buffer[pos++];
			if (pos + optLen > length) break;
			byte[] optData = new byte[optLen];
			Array.Copy(buffer, pos, optData, 0, optLen);
			packet.Options[optType] = optData;
			pos += optLen;
		}

		return packet;
	}

	/// <summary>
	/// Serializes the packet into a byte array suitable for transmission. The terminating
	/// option 255 is appended automatically; do not include it in Options.
	/// </summary>
	public byte[] Build() {
		using var ms = new MemoryStream(300);
		using var writer = new BinaryWriter(ms);

		writer.Write(Op);
		writer.Write(HType);
		writer.Write(HLen);
		writer.Write(Hops);

		Span<byte> tmp4 = stackalloc byte[4];
		BinaryPrimitives.WriteUInt32BigEndian(tmp4, Xid);
		writer.Write(tmp4);

		Span<byte> tmp2 = stackalloc byte[2];
		BinaryPrimitives.WriteUInt16BigEndian(tmp2, Secs);
		writer.Write(tmp2);
		BinaryPrimitives.WriteUInt16BigEndian(tmp2, Flags);
		writer.Write(tmp2);

		writer.Write(IPv4Bytes(CIAddr));
		writer.Write(IPv4Bytes(YIAddr));
		writer.Write(IPv4Bytes(SIAddr));
		writer.Write(IPv4Bytes(GIAddr));
		writer.Write(CHAddr);

		// SName (64 bytes, null-padded)
		var snameBytes = new byte[64];
		var snameSrc = Encoding.ASCII.GetBytes(SName);
		Array.Copy(snameSrc, snameBytes, Math.Min(snameSrc.Length, 64));
		writer.Write(snameBytes);

		// File (128 bytes, null-padded)
		var fileBytes = new byte[128];
		var fileSrc = Encoding.ASCII.GetBytes(File);
		Array.Copy(fileSrc, fileBytes, Math.Min(fileSrc.Length, 128));
		writer.Write(fileBytes);

		// Magic cookie
		writer.Write([ 0x63, 0x82, 0x53, 0x63 ]);

		// Options. Write message type first per convention (RFC 2132 §9.6).
		if (Options.TryGetValue(OPT_MESSAGE_TYPE, out var msgType)) {
			WriteOption(writer, OPT_MESSAGE_TYPE, msgType);
		}
		foreach (var kv in Options) {
			if (kv.Key == OPT_MESSAGE_TYPE) continue;
			WriteOption(writer, kv.Key, kv.Value);
		}
		writer.Write(OPT_END);

		return ms.ToArray();
	}

	private static void WriteOption(BinaryWriter writer, byte type, byte[] data) {
		writer.Write(type);
		writer.Write((byte)data.Length);
		writer.Write(data);
	}

	/// <summary>Returns 4 bytes for an IPv4 address; throws for IPv6.</summary>
	private static byte[] IPv4Bytes(IPAddress addr) {
		if (addr.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork) {
			throw new ArgumentException("Only IPv4 addresses supported in DHCP packets.");
		}
		return addr.GetAddressBytes();
	}

	// ----- Convenience setters for common options -----

	public void SetMessageType(DHCPMessageTypes type) {
		Options[OPT_MESSAGE_TYPE] = [ (byte)type.Value ];
	}

	public void SetServerIdentifier(IPAddress serverIp) {
		Options[OPT_SERVER_IDENTIFIER] = IPv4Bytes(serverIp);
	}

	public void SetSubnetMask(IPAddress mask) {
		Options[OPT_SUBNET_MASK] = IPv4Bytes(mask);
	}

	public void SetRouter(IPAddress router) {
		Options[OPT_ROUTER] = IPv4Bytes(router);
	}

	public void SetDnsServers(params IPAddress[] dns) {
		var bytes = new byte[dns.Length * 4];
		for (int i = 0; i < dns.Length; i++) {
			Array.Copy(IPv4Bytes(dns[i]), 0, bytes, i * 4, 4);
		}
		Options[OPT_DNS_SERVERS] = bytes;
	}

	public void SetLeaseTime(uint seconds) {
		var bytes = new byte[4];
		BinaryPrimitives.WriteUInt32BigEndian(bytes, seconds);
		Options[OPT_LEASE_TIME] = bytes;
	}
}
