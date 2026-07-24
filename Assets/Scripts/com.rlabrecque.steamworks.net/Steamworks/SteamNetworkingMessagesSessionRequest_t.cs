namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1251)]
	public struct SteamNetworkingMessagesSessionRequest_t
	{
		public const int k_iCallback = 1251;

		public global::Steamworks.SteamNetworkingIdentity m_identityRemote;
	}
}
