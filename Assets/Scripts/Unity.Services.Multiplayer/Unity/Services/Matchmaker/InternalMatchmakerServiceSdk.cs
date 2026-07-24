namespace Unity.Services.Matchmaker
{
	internal class InternalMatchmakerServiceSdk : global::Unity.Services.Matchmaker.IMatchmakerServiceSdk
	{
		public global::Unity.Services.Matchmaker.Apis.Backfill.IBackfillApiClient BackfillApi { get; set; }

		public global::Unity.Services.Matchmaker.Apis.Tickets.ITicketsApiClient TicketsApi { get; set; }

		public global::Unity.Services.Matchmaker.Apis.Matches.IMatchesApiClient MatchesApi { get; set; }

		public global::Unity.Services.Authentication.Internal.IAccessToken AccessToken { get; set; }

		public global::Unity.Services.Authentication.Server.Internal.IServerAccessToken ServerAccessToken { get; set; }

		public global::Unity.Services.Matchmaker.Configuration Configuration { get; set; }

		public InternalMatchmakerServiceSdk(global::Unity.Services.Matchmaker.Http.HttpClient httpClient, string cloudEnvironment = "production", global::Unity.Services.Authentication.Internal.IAccessToken accessToken = null, global::Unity.Services.Authentication.Server.Internal.IServerAccessToken serverAccessToken = null)
		{
			BackfillApi = new global::Unity.Services.Matchmaker.Apis.Backfill.BackfillApiClient(httpClient, accessToken);
			TicketsApi = new global::Unity.Services.Matchmaker.Apis.Tickets.TicketsApiClient(httpClient, accessToken);
			MatchesApi = new global::Unity.Services.Matchmaker.Apis.Matches.MatchesApiClient(httpClient, accessToken);
			AccessToken = accessToken;
			ServerAccessToken = serverAccessToken;
			Configuration = new global::Unity.Services.Matchmaker.Configuration(GetBasePath(cloudEnvironment), 10, 4, null);
		}

		private string GetBasePath(string cloudEnvironment)
		{
			if (cloudEnvironment == "staging")
			{
				return "https://matchmaker-stg.services.api.unity.com";
			}
			return "https://matchmaker.services.api.unity.com";
		}
	}
}
