namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1252)]
	public struct SteamNetworkingMessagesSessionFailed_t
	{
		public const int k_iCallback = 1252;

		public global::Steamworks.SteamNetConnectionInfo_t m_info;
	}
}
