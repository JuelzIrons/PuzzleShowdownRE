namespace Unity.Services.Relay.Apis.RelayAllocations
{
	internal class RelayAllocationsApiClient : global::Unity.Services.Relay.Http.BaseApiClient, global::Unity.Services.Relay.Apis.RelayAllocations.IRelayAllocationsApiClient
	{
		private global::Unity.Services.Authentication.Internal.IAccessToken _accessToken;

		private const int _baseTimeout = 10;

		private global::Unity.Services.Relay.Configuration _configuration;

		public global::Unity.Services.Relay.Configuration Configuration
		{
			get
			{
				global::Unity.Services.Relay.Configuration b = new global::Unity.Services.Relay.Configuration("https://relay-allocations.services.api.unity.com", 10, 4, null);
				return global::Unity.Services.Relay.Configuration.MergeConfigurations(_configuration, b);
			}
			set
			{
				_configuration = value;
			}
		}

		public RelayAllocationsApiClient(global::Unity.Services.Relay.Http.IHttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Relay.Configuration configuration = null)
			: base(httpClient)
		{
			_configuration = configuration;
			_accessToken = accessToken;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.AllocateResponseBody>> CreateAllocationAsync(global::Unity.Services.Relay.RelayAllocations.CreateAllocationRequest request, global::Unity.Services.Relay.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"201",
					typeof(global::Unity.Services.Relay.Models.AllocateResponseBody)
				},
				{
					"400",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"401",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"403",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"429",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"500",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"503",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				}
			};
			global::Unity.Services.Relay.Configuration configuration = global::Unity.Services.Relay.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Relay.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Relay.Models.AllocateResponseBody result = global::Unity.Services.Relay.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Relay.Models.AllocateResponseBody>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.AllocateResponseBody>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.JoinCodeResponseBody>> CreateJoincodeAsync(global::Unity.Services.Relay.RelayAllocations.CreateJoincodeRequest request, global::Unity.Services.Relay.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Relay.Models.JoinCodeResponseBody)
				},
				{
					"201",
					typeof(global::Unity.Services.Relay.Models.JoinCodeResponseBody)
				},
				{
					"400",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"401",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"403",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"404",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"429",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"500",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				}
			};
			global::Unity.Services.Relay.Configuration configuration = global::Unity.Services.Relay.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Relay.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Relay.Models.JoinCodeResponseBody result = global::Unity.Services.Relay.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Relay.Models.JoinCodeResponseBody>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.JoinCodeResponseBody>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.JoinResponseBody>> JoinRelayAsync(global::Unity.Services.Relay.RelayAllocations.JoinRelayRequest request, global::Unity.Services.Relay.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Relay.Models.JoinResponseBody)
				},
				{
					"400",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"401",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"403",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"404",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"429",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"500",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				}
			};
			global::Unity.Services.Relay.Configuration configuration = global::Unity.Services.Relay.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Relay.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Relay.Models.JoinResponseBody result = global::Unity.Services.Relay.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Relay.Models.JoinResponseBody>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.JoinResponseBody>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.RegionsResponseBody>> ListRegionsAsync(global::Unity.Services.Relay.RelayAllocations.ListRegionsRequest request, global::Unity.Services.Relay.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Relay.Models.RegionsResponseBody)
				},
				{
					"400",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"401",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"403",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"429",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				},
				{
					"500",
					typeof(global::Unity.Services.Relay.Models.ErrorResponseBody)
				}
			};
			global::Unity.Services.Relay.Configuration configuration = global::Unity.Services.Relay.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Relay.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Relay.Models.RegionsResponseBody result = global::Unity.Services.Relay.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Relay.Models.RegionsResponseBody>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.RegionsResponseBody>(obj, result);
		}
	}
}
