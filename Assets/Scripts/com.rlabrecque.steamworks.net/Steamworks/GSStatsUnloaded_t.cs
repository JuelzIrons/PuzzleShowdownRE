namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1108)]
	public struct GSStatsUnloaded_t
	{
		public const int k_iCallback = 1108;

		public global::Steamworks.CSteamID m_steamIDUser;
	}
}
