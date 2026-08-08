namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1105)]
	public struct LeaderboardScoresDownloaded_t
	{
		public const int k_iCallback = 1105;

		public global::Steamworks.SteamLeaderboard_t m_hSteamLeaderboard;

		public global::Steamworks.SteamLeaderboardEntries_t m_hSteamLeaderboardEntries;

		public int m_cEntryCount;
	}
}
