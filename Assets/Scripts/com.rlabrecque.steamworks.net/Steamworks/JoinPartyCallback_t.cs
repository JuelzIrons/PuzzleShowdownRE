namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(5301)]
	public struct JoinPartyCallback_t
	{
		public const int k_iCallback = 5301;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.PartyBeaconID_t m_ulBeaconID;

		public global::Steamworks.CSteamID m_SteamIDBeaconOwner;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchConnectString_;

		public string m_rgchConnectString
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchConnectString_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchConnectString_, 256);
			}
		}
	}
}
