namespace Unity.Services.Matchmaker.Apis.Backfill
{
	internal class BackfillApiClient : global::Unity.Services.Matchmaker.Http.BaseApiClient, global::Unity.Services.Matchmaker.Apis.Backfill.IBackfillApiClient
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

		public BackfillApiClient(global::Unity.Services.Matchmaker.Http.IHttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Matchmaker.Configuration configuration = null)
			: base(httpClient)
		{
			_configuration = configuration;
			_accessToken = accessToken;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket>> ApproveBackfillTicketAsync(global::Unity.Services.Matchmaker.Backfill.ApproveBackfillTicketRequest request, string payloadProxyToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket)
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
			global::Unity.Services.Matchmaker.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(payloadProxyToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket result = global::Unity.Services.Matchmaker.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.CreateBackfillTicketResponse>> CreateBackfillTicketAsync(global::Unity.Services.Matchmaker.Backfill.CreateBackfillTicketRequest request, string payloadProxyToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"201",
					typeof(global::Unity.Services.Matchmaker.Models.CreateBackfillTicketResponse)
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
			global::Unity.Services.Matchmaker.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(payloadProxyToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Matchmaker.Models.CreateBackfillTicketResponse result = global::Unity.Services.Matchmaker.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Matchmaker.Models.CreateBackfillTicketResponse>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.CreateBackfillTicketResponse>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response> DeleteBackfillTicketAsync(global::Unity.Services.Matchmaker.Backfill.DeleteBackfillTicketRequest request, string payloadProxyToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{ "204", null },
				{
					"429",
					typeof(global::Unity.Services.Matchmaker.Models.ProblemDetails)
				}
			};
			global::Unity.Services.Matchmaker.Configuration configuration = global::Unity.Services.Matchmaker.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Matchmaker.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("DELETE", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(payloadProxyToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Matchmaker.Http.ResponseHandler.HandleAsyncResponse(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Matchmaker.Response(obj);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response> UpdateBackfillTicketAsync(global::Unity.Services.Matchmaker.Backfill.UpdateBackfillTicketRequest request, string payloadProxyToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{ "200", null },
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
			global::Unity.Services.Matchmaker.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("PUT", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(payloadProxyToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Matchmaker.Http.ResponseHandler.HandleAsyncResponse(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Matchmaker.Response(obj);
		}
	}
}
