namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionRealTimeStatus_t
	{
		public global::Steamworks.ESteamNetworkingConnectionState m_eState;

		public int m_nPing;

		public float m_flConnectionQualityLocal;

		public float m_flConnectionQualityRemote;

		public float m_flOutPacketsPerSec;

		public float m_flOutBytesPerSec;

		public float m_flInPacketsPerSec;

		public float m_flInBytesPerSec;

		public int m_nSendRateBytesPerSecond;

		public int m_cbPendingUnreliable;

		public int m_cbPendingReliable;

		public int m_cbSentUnackedReliable;

		public global::Steamworks.SteamNetworkingMicroseconds m_usecQueueTime;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16)]
		public uint[] reserved;
	}
}
