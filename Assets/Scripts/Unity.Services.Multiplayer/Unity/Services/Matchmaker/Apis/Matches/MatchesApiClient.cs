namespace Unity.Services.Matchmaker.Apis.Matches
{
	internal class MatchesApiClient : global::Unity.Services.Matchmaker.Http.BaseApiClient, global::Unity.Services.Matchmaker.Apis.Matches.IMatchesApiClient
	{
		private global::Unity.Services.Authentication.Internal.IAccessToken _accessToken;

		private const int _baseTimeout = 10;

		private global::Unity.Services.Matchmaker.Configuration _configuration;

		public global::Unity.Services.Matchmaker.Configuration Configuration
		{
			get
			{
				global::Unity.Services.Matchmaker.Configuration b = new global::Unity.Services.Matchmaker.Configuration("https://matchmaker.services.api.unity.com", 10, 4, null);
				return global::Unity.Services.Matchmaker.Configuration.MergeConfigurations(_configuration, b);
			}
			set
			{
				_configuration = value;
			}
		}

		public MatchesApiClient(global::Unity.Services.Matchmaker.Http.IHttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Matchmaker.Configuration configuration = null)
			: base(httpClient)
		{
			_configuration = configuration;
			_accessToken = accessToken;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults>> GetMatchmakingResultsAsync(global::Unity.Services.Matchmaker.Matches.GetMatchmakingResultsRequest request, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults)
				},
				{
					"400",
					typeof(global::Unity.Services.Matchmaker.Models.ProblemDetails)
				},
				{
					"404",
					typeof(global::Unity.Services.Matchmaker.Models.ProblemDetails)
				},
				{
					"429",
					typeof(global::Unity.Services.Matchmaker.Models.ProblemDetails)
				},
				{
					"500",
					typeof(global::Unity.Services.Matchmaker.Models.ProblemDetails)
				}
			};
			global::Unity.Services.Matchmaker.Configuration configuration = global::Unity.Services.Matchmaker.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Matchmaker.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults result = global::Unity.Services.Matchmaker.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults>(obj, result);
		}
	}
}
