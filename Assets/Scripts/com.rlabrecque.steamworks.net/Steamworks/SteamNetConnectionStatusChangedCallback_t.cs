namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1221)]
	public struct SteamNetConnectionStatusChangedCallback_t
	{
		public const int k_iCallback = 1221;

		public global::Steamworks.HSteamNetConnection m_hConn;

		public global::Steamworks.SteamNetConnectionInfo_t m_info;

		public global::Steamworks.ESteamNetworkingConnectionState m_eOldState;
	}
}
