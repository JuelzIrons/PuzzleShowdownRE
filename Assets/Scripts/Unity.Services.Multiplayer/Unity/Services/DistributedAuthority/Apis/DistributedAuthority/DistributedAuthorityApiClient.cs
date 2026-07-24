namespace Unity.Services.DistributedAuthority.Apis.DistributedAuthority
{
	internal class DistributedAuthorityApiClient : global::Unity.Services.DistributedAuthority.Http.BaseApiClient, global::Unity.Services.DistributedAuthority.Apis.DistributedAuthority.IDistributedAuthorityApiClient
	{
		private global::Unity.Services.Authentication.Internal.IAccessToken _accessToken;

		private const int _baseTimeout = 10;

		private global::Unity.Services.DistributedAuthority.Configuration _configuration;

		public global::Unity.Services.DistributedAuthority.Configuration Configuration
		{
			get
			{
				global::Unity.Services.DistributedAuthority.Configuration b = new global::Unity.Services.DistributedAuthority.Configuration("http://localhost:3000", 10, 4, null);
				return global::Unity.Services.DistributedAuthority.Configuration.MergeConfigurations(_configuration, b);
			}
			set
			{
				_configuration = value;
			}
		}

		public DistributedAuthorityApiClient(global::Unity.Services.DistributedAuthority.Http.IHttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.DistributedAuthority.Configuration configuration = null)
			: base(httpClient)
		{
			_configuration = configuration;
			_accessToken = accessToken;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.DistributedAuthority.Response<global::Unity.Services.DistributedAuthority.Models.Session>> CreateSessionAsync(global::Unity.Services.DistributedAuthority.DistributedAuthority.CreateSessionRequest request, global::Unity.Services.DistributedAuthority.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.DistributedAuthority.Models.Session)
				},
				{
					"400",
					typeof(global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody)
				},
				{
					"401",
					typeof(global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody)
				},
				{
					"404",
					typeof(global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody)
				},
				{
					"409",
					typeof(global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody)
				},
				{
					"500",
					typeof(global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody)
				}
			};
			global::Unity.Services.DistributedAuthority.Configuration configuration = global::Unity.Services.DistributedAuthority.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.DistributedAuthority.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.DistributedAuthority.Models.Session result = global::Unity.Services.DistributedAuthority.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.DistributedAuthority.Models.Session>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.DistributedAuthority.Response<global::Unity.Services.DistributedAuthority.Models.Session>(obj, result);
		}
	}
}
