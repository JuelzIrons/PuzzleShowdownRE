namespace Unity.Services.Matchmaker.Apis.Tickets
{
	internal class TicketsApiClient : global::Unity.Services.Matchmaker.Http.BaseApiClient, global::Unity.Services.Matchmaker.Apis.Tickets.ITicketsApiClient
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

		public TicketsApiClient(global::Unity.Services.Matchmaker.Http.IHttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Matchmaker.Configuration configuration = null)
			: base(httpClient)
		{
			_configuration = configuration;
			_accessToken = accessToken;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.CreateTicketResponse>> CreateTicketAsync(global::Unity.Services.Matchmaker.Tickets.CreateTicketRequest request, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"201",
					typeof(global::Unity.Services.Matchmaker.Models.CreateTicketResponse)
				},
				{
					"400",
					typeof(global::Unity.Services.Matchmaker.Models.ProblemDetails)
				},
				{
					"429",
					typeof(global::Unity.Services.Matchmaker.Models.ProblemDetails)
				}
			};
			global::Unity.Services.Matchmaker.Configuration configuration = global::Unity.Services.Matchmaker.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Matchmaker.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Matchmaker.Models.CreateTicketResponse result = global::Unity.Services.Matchmaker.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Matchmaker.Models.CreateTicketResponse>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.CreateTicketResponse>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response> DeleteTicketAsync(global::Unity.Services.Matchmaker.Tickets.DeleteTicketRequest request, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{ "200", null },
				{
					"400",
					typeof(global::Unity.Services.Matchmaker.Models.ProblemDetails)
				},
				{
					"429",
					typeof(global::Unity.Services.Matchmaker.Models.ProblemDetails)
				}
			};
			global::Unity.Services.Matchmaker.Configuration configuration = global::Unity.Services.Matchmaker.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Matchmaker.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("DELETE", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Matchmaker.Http.ResponseHandler.HandleAsyncResponse(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Matchmaker.Response(obj);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.TicketStatusResponse>> GetTicketStatusAsync(global::Unity.Services.Matchmaker.Tickets.GetTicketStatusRequest request, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Matchmaker.Models.TicketStatusResponse)
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
				}
			};
			global::Unity.Services.Matchmaker.Configuration configuration = global::Unity.Services.Matchmaker.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Matchmaker.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Matchmaker.Models.TicketStatusResponse result = global::Unity.Services.Matchmaker.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Matchmaker.Models.TicketStatusResponse>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.TicketStatusResponse>(obj, result);
		}
	}
}
