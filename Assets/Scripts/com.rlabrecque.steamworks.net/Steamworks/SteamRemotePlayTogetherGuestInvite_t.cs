namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(5703)]
	public struct SteamRemotePlayTogetherGuestInvite_t
	{
		public const int k_iCallback = 5703;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1024)]
		private byte[] m_szConnectURL_;

		public string m_szConnectURL
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_szConnectURL_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_szConnectURL_, 1024);
			}
		}
	}
}
