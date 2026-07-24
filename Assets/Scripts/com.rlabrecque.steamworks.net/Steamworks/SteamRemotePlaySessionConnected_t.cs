namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(5701)]
	public struct SteamRemotePlaySessionConnected_t
	{
		public const int k_iCallback = 5701;

		public global::Steamworks.RemotePlaySessionID_t m_unSessionID;
	}
}
