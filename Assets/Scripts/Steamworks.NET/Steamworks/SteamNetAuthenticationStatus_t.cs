namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1222)]
	public struct SteamNetAuthenticationStatus_t
	{
		public const int k_iCallback = 1222;

		public global::Steamworks.ESteamNetworkingAvailability m_eAvail;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_debugMsg_;

		public string m_debugMsg
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_debugMsg_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_debugMsg_, 256);
			}
		}
	}
}
