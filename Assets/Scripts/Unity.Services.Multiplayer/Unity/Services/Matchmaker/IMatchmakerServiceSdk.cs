namespace Unity.Services.Matchmaker
{
	internal interface IMatchmakerServiceSdk
	{
		global::Unity.Services.Matchmaker.Apis.Backfill.IBackfillApiClient BackfillApi { get; set; }

		global::Unity.Services.Matchmaker.Apis.Tickets.ITicketsApiClient TicketsApi { get; set; }

		global::Unity.Services.Matchmaker.Apis.Matches.IMatchesApiClient MatchesApi { get; set; }

		global::Unity.Services.Authentication.Internal.IAccessToken AccessToken { get; set; }

		global::Unity.Services.Authentication.Server.Internal.IServerAccessToken ServerAccessToken { get; set; }

		global::Unity.Services.Matchmaker.Configuration Configuration { get; set; }
	}
}
