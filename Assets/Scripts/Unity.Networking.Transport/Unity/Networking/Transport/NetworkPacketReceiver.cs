namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::System.Obsolete("Use ReceiveJobArguments.ReceiveQueue instead", true)]
	public struct NetworkPacketReceiver
	{
		[global::System.Flags]
		public enum AppendPacketMode
		{
			None = 0,
			NoCopyNeeded = 1
		}

		public long LastUpdateTime
		{
			get
			{
				throw new global::System.NotImplementedException();
			}
		}

		public int ReceiveErrorCode
		{
			get
			{
				throw new global::System.NotImplementedException();
			}
		}

		public global::System.IntPtr AllocateMemory(ref int dataLen)
		{
			throw new global::System.NotImplementedException();
		}

		public bool AppendPacket(global::System.IntPtr data, ref global::Unity.Networking.Transport.NetworkEndpoint address, int dataLen, global::Unity.Networking.Transport.NetworkPacketReceiver.AppendPacketMode mode = global::Unity.Networking.Transport.NetworkPacketReceiver.AppendPacketMode.None)
		{
			throw new global::System.NotImplementedException();
		}

		public bool IsAddressUsed(global::Unity.Networking.Transport.NetworkEndpoint address)
		{
			throw new global::System.NotImplementedException();
		}
	}
}
