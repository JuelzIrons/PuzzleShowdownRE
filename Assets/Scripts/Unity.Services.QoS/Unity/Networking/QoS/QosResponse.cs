namespace Unity.Networking.QoS
{
	internal class QosResponse
	{
		private const int MinPacketLen = 13;

		private const int MaxPacketLen = 1500;

		private const byte ResponseMagic = 149;

		private const byte ResponseVersion = 0;

		private byte m_Magic;

		private byte m_VerAndFlow;

		private byte m_Sequence;

		private ushort m_Identifier;

		private ulong m_Timestamp;

		private int m_LatencyMs;

		private ushort m_PacketLength;

		internal byte Magic => m_Magic;

		internal byte Version => (byte)((m_VerAndFlow >> 4) & 0xF);

		internal byte FlowControl => (byte)(m_VerAndFlow & 0xF);

		internal byte Sequence => m_Sequence;

		internal ushort Identifier => m_Identifier;

		internal ulong Timestamp => m_Timestamp;

		internal ushort Length => m_PacketLength;

		internal int LatencyMs => m_LatencyMs;

		internal unsafe (int received, int errorCode) Recv(global::System.IntPtr socketHandle, bool wait, global::System.DateTime expireTimeUtc, ref global::Unity.Networking.QoS.NetworkEndPoint endPoint)
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Message baselib_Socket_Message = default(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Message);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer unsafeAppendBuffer = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer(2048, 16, global::Unity.Collections.Allocator.Persistent);
			_ = global::System.DateTime.UtcNow;
			fixed (global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress* rawNetworkAddress = &endPoint.rawNetworkAddress)
			{
				baselib_Socket_Message.dataLen = (uint)unsafeAppendBuffer.Capacity;
				baselib_Socket_Message.address = rawNetworkAddress;
				baselib_Socket_Message.data = new global::System.IntPtr(unsafeAppendBuffer.Ptr);
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socket = new global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle
				{
					handle = socketHandle
				};
				uint num = 0u;
				int num2 = 0;
				while (!global::Unity.Networking.QoS.QosHelper.ExpiredUtc(expireTimeUtc))
				{
					baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
					num2++;
					num = global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_UDP_Recv(socket, &baselib_Socket_Message, 1u, &baselib_ErrorState);
					if (num != 0 || !global::Unity.Networking.QoS.QosHelper.WouldBlock(baselib_ErrorState.nativeErrorCode))
					{
						break;
					}
					if (!wait)
					{
						return (received: 0, errorCode: 0);
					}
				}
				if (num == 0)
				{
					unsafeAppendBuffer.Dispose();
					return (received: 0, errorCode: (int)baselib_ErrorState.code);
				}
				endPoint.rawNetworkAddress = *baselib_Socket_Message.address;
				m_PacketLength = (ushort)baselib_Socket_Message.dataLen;
				Deserialize(baselib_Socket_Message.data);
				m_LatencyMs = (int)((Length >= 13) ? (global::System.DateTime.UtcNow.Ticks / 10000 - (long)m_Timestamp) : (-1));
			}
			unsafeAppendBuffer.Dispose();
			return (received: Length, errorCode: 0);
		}

		internal void Deserialize(global::System.IntPtr msgData)
		{
			m_Magic = global::System.Runtime.InteropServices.Marshal.ReadByte(msgData);
			m_VerAndFlow = global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 1);
			m_Sequence = global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 2);
			ushort num = global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 3);
			ushort num2 = (ushort)(global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 4) << 8);
			m_Identifier = (ushort)(num + num2);
			ulong num3 = global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 5);
			ulong num4 = (ulong)global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 6) << 8;
			ulong num5 = (ulong)global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 7) << 16;
			ulong num6 = (ulong)global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 8) << 24;
			ulong num7 = (ulong)global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 9) << 32;
			ulong num8 = (ulong)global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 10) << 40;
			ulong num9 = (ulong)global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 11) << 48;
			ulong num10 = (ulong)global::System.Runtime.InteropServices.Marshal.ReadByte(msgData, 12) << 56;
			m_Timestamp = num3 + num4 + num5 + num6 + num7 + num8 + num9 + num10;
		}

		internal bool Verify(uint maxSequence, ref string error)
		{
			if (Length < 13)
			{
				error = $"response is too small got {Length} bytes min expected {13} bytes";
				return false;
			}
			if (Magic != 149)
			{
				error = $"response contains an invalid signature 0x{Magic:X} expected 0x{(byte)149:X}";
				return false;
			}
			if (Version != 0)
			{
				error = $"response contains an invalid version {Version} expected {(byte)0}";
				return false;
			}
			if (Sequence > maxSequence)
			{
				error = $"response contains an invalid sequence {Sequence} max expected {maxSequence}";
				return false;
			}
			return true;
		}

		internal (global::Unity.Networking.QoS.FcType type, byte units) ParseFlowControl()
		{
			if (FlowControl == 0)
			{
				return (type: global::Unity.Networking.QoS.FcType.None, units: 0);
			}
			int num = (((FlowControl & 8) == 0) ? 1 : 2);
			byte b = (byte)(FlowControl & 7);
			if (num == 2)
			{
				b++;
			}
			return (type: (global::Unity.Networking.QoS.FcType)num, units: b);
		}
	}
}
