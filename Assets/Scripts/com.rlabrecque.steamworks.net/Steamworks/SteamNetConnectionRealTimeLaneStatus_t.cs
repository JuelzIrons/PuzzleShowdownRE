namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionRealTimeLaneStatus_t
	{
		public int m_cbPendingUnreliable;

		public int m_cbPendingReliable;

		public int m_cbSentUnackedReliable;

		public int _reservePad1;

		public global::Steamworks.SteamNetworkingMicroseconds m_usecQueueTime;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 10)]
		public uint[] reserved;
	}
}
