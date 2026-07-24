namespace Unity.Services.Matchmaker.Apis.Matches
{
	internal interface IMatchesApiClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults>> GetMatchmakingResultsAsync(global::Unity.Services.Matchmaker.Matches.GetMatchmakingResultsRequest request, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null);
	}
}
