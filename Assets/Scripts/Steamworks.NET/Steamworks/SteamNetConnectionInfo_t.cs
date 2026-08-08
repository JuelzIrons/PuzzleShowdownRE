namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionInfo_t
	{
		public global::Steamworks.SteamNetworkingIdentity m_identityRemote;

		public long m_nUserData;

		public global::Steamworks.HSteamListenSocket m_hListenSocket;

		public global::Steamworks.SteamNetworkingIPAddr m_addrRemote;

		public ushort m__pad1;

		public global::Steamworks.SteamNetworkingPOPID m_idPOPRemote;

		public global::Steamworks.SteamNetworkingPOPID m_idPOPRelay;

		public global::Steamworks.ESteamNetworkingConnectionState m_eState;

		public int m_eEndReason;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szEndDebug_;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szConnectionDescription_;

		public int m_nFlags;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 63)]
		public uint[] reserved;

		public string m_szEndDebug
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_szEndDebug_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_szEndDebug_, 128);
			}
		}

		public string m_szConnectionDescription
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_szConnectionDescription_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_szConnectionDescription_, 128);
			}
		}
	}
}
