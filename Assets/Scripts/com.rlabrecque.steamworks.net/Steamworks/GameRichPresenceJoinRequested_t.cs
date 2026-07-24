namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(337)]
	public struct GameRichPresenceJoinRequested_t
	{
		public const int k_iCallback = 337;

		public global::Steamworks.CSteamID m_steamIDFriend;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchConnect_;

		public string m_rgchConnect
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchConnect_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchConnect_, 256);
			}
		}
	}
}
