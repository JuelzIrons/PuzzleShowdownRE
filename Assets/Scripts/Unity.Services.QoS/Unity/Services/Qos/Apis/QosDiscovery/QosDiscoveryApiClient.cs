namespace Unity.Services.Qos.Apis.QosDiscovery
{
	internal class QosDiscoveryApiClient : global::Unity.Services.Qos.Http.BaseApiClient, global::Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient
	{
		private global::Unity.Services.Authentication.Internal.IAccessToken _accessToken;

		private const int _baseTimeout = 10;

		private global::Unity.Services.Qos.Configuration _configuration;

		public global::Unity.Services.Qos.Configuration Configuration
		{
			get
			{
				global::Unity.Services.Qos.Configuration b = new global::Unity.Services.Qos.Configuration("https://qos-discovery.services.api.unity.com", 10, 4, null);
				return global::Unity.Services.Qos.Configuration.MergeConfigurations(_configuration, b);
			}
			set
			{
				_configuration = value;
			}
		}

		public QosDiscoveryApiClient(global::Unity.Services.Qos.Http.IHttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Qos.Configuration configuration = null)
			: base(httpClient)
		{
			_configuration = configuration;
			_accessToken = accessToken;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Qos.Response<global::Unity.Services.Qos.Models.QosServersResponseBody>> GetServersAsync(global::Unity.Services.Qos.QosDiscovery.GetServersRequest request, global::Unity.Services.Qos.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Qos.Models.QosServersResponseBody)
				},
				{
					"400",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"401",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"403",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"429",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"500",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"503",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				}
			};
			global::Unity.Services.Qos.Configuration configuration = global::Unity.Services.Qos.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Qos.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Qos.Models.QosServersResponseBody result = global::Unity.Services.Qos.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Qos.Models.QosServersResponseBody>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Qos.Response<global::Unity.Services.Qos.Models.QosServersResponseBody>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Qos.Response<global::Unity.Services.Qos.Models.QosServiceServersResponseBody>> GetServiceServersAsync(global::Unity.Services.Qos.QosDiscovery.GetServiceServersRequest request, global::Unity.Services.Qos.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Qos.Models.QosServiceServersResponseBody)
				},
				{
					"400",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"401",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"403",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"429",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"500",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				},
				{
					"503",
					typeof(global::Unity.Services.Qos.Models.ErrorResponseBody)
				}
			};
			global::Unity.Services.Qos.Configuration configuration = global::Unity.Services.Qos.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Qos.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Qos.Models.QosServiceServersResponseBody result = global::Unity.Services.Qos.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Qos.Models.QosServiceServersResponseBody>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Qos.Response<global::Unity.Services.Qos.Models.QosServiceServersResponseBody>(obj, result);
		}
	}
}
