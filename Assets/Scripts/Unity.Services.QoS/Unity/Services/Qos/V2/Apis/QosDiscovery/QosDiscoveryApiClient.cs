namespace Unity.Services.Qos.V2.Apis.QosDiscovery
{
	internal class QosDiscoveryApiClient : global::Unity.Services.Qos.V2.Http.BaseApiClient, global::Unity.Services.Qos.V2.Apis.QosDiscovery.IQosDiscoveryApiClient
	{
		private global::Unity.Services.Authentication.Internal.IAccessToken _accessToken;

		private const int _baseTimeout = 10;

		private global::Unity.Services.Qos.V2.Configuration _configuration;

		public global::Unity.Services.Qos.V2.Configuration Configuration
		{
			get
			{
				global::Unity.Services.Qos.V2.Configuration b = new global::Unity.Services.Qos.V2.Configuration("http://localhost", 10, 4, null);
				return global::Unity.Services.Qos.V2.Configuration.MergeConfigurations(_configuration, b);
			}
			set
			{
				_configuration = value;
			}
		}

		public QosDiscoveryApiClient(global::Unity.Services.Qos.V2.Http.IHttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Qos.V2.Configuration configuration = null)
			: base(httpClient)
		{
			_configuration = configuration;
			_accessToken = accessToken;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Qos.V2.Response<global::Unity.Services.Qos.V2.Models.QosServersResponseBody>> GetAllServersAsync(global::Unity.Services.Qos.V2.QosDiscovery.GetAllServersRequest request, global::Unity.Services.Qos.V2.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Qos.V2.Models.QosServersResponseBody)
				},
				{
					"400",
					typeof(global::Unity.Services.Qos.V2.Models.ErrorResponseBody)
				},
				{
					"401",
					typeof(global::Unity.Services.Qos.V2.Models.ErrorResponseBody)
				},
				{
					"403",
					typeof(global::Unity.Services.Qos.V2.Models.ErrorResponseBody)
				},
				{
					"429",
					typeof(global::Unity.Services.Qos.V2.Models.ErrorResponseBody)
				},
				{
					"500",
					typeof(global::Unity.Services.Qos.V2.Models.ErrorResponseBody)
				},
				{
					"503",
					typeof(global::Unity.Services.Qos.V2.Models.ErrorResponseBody)
				}
			};
			global::Unity.Services.Qos.V2.Configuration configuration = global::Unity.Services.Qos.V2.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Qos.V2.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Qos.V2.Models.QosServersResponseBody result = global::Unity.Services.Qos.V2.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Qos.V2.Models.QosServersResponseBody>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Qos.V2.Response<global::Unity.Services.Qos.V2.Models.QosServersResponseBody>(obj, result);
		}
	}
}
