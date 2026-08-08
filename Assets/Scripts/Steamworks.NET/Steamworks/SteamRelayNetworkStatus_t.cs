namespace Steamworks
{
	[global::Steamworks.CallbackIdentity(1281)]
	public struct SteamRelayNetworkStatus_t
	{
		public const int k_iCallback = 1281;

		public global::Steamworks.ESteamNetworkingAvailability m_eAvail;

		public int m_bPingMeasurementInProgress;

		public global::Steamworks.ESteamNetworkingAvailability m_eAvailNetworkConfig;

		public global::Steamworks.ESteamNetworkingAvailability m_eAvailAnyRelay;

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
