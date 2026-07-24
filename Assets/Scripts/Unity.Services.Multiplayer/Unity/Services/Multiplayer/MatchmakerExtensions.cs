namespace Unity.Services.Multiplayer
{
	public static class MatchmakerExtensions
	{
		public static global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults GetMatchmakingResults(this global::Unity.Services.Multiplayer.ISession session)
		{
			return ((global::Unity.Services.Multiplayer.SessionHandler)session).GetModule<global::Unity.Services.Multiplayer.MatchmakerModule>().MatchmakingResults;
		}
	}
}
