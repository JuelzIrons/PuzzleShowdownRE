namespace Unity.Services.Lobbies.Apis.Lobby
{
	internal class LobbyApiClient : global::Unity.Services.Lobbies.Http.BaseApiClient, global::Unity.Services.Lobbies.Apis.Lobby.ILobbyApiClient
	{
		private global::Unity.Services.Authentication.Internal.IAccessToken _accessToken;

		private const int _baseTimeout = 10;

		private global::Unity.Services.Lobbies.Configuration _configuration;

		public global::Unity.Services.Lobbies.Configuration Configuration
		{
			get
			{
				global::Unity.Services.Lobbies.Configuration b = new global::Unity.Services.Lobbies.Configuration("https://lobby.services.api.unity.com/v1", 10, 4, null);
				return global::Unity.Services.Lobbies.Configuration.MergeConfigurations(_configuration, b);
			}
			set
			{
				_configuration = value;
			}
		}

		public LobbyApiClient(global::Unity.Services.Lobbies.Http.IHttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Lobbies.Configuration configuration = null)
			: base(httpClient)
		{
			_configuration = configuration;
			_accessToken = accessToken;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> BulkUpdateLobbyAsync(global::Unity.Services.Lobbies.Lobby.BulkUpdateLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"409",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"412",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> CreateLobbyAsync(global::Unity.Services.Lobbies.Lobby.CreateLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"201",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> CreateOrJoinLobbyAsync(global::Unity.Services.Lobbies.Lobby.CreateOrJoinLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"201",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"409",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response> DeleteLobbyAsync(global::Unity.Services.Lobbies.Lobby.DeleteLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{ "204", null },
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"412",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("DELETE", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response(obj);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.List<string>>> GetHostedLobbiesAsync(global::Unity.Services.Lobbies.Lobby.GetHostedLobbiesRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::System.Collections.Generic.List<string>)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::System.Collections.Generic.List<string> result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::System.Collections.Generic.List<string>>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.List<string>>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.List<string>>> GetJoinedLobbiesAsync(global::Unity.Services.Lobbies.Lobby.GetJoinedLobbiesRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::System.Collections.Generic.List<string>)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::System.Collections.Generic.List<string> result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::System.Collections.Generic.List<string>>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.List<string>>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> GetLobbyAsync(global::Unity.Services.Lobbies.Lobby.GetLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{ "304", null },
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.MigrationDataInfo>> GetMigrationDataInfoAsync(global::Unity.Services.Lobbies.Lobby.GetMigrationDataInfoRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.MigrationDataInfo)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("GET", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.MigrationDataInfo result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.MigrationDataInfo>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.MigrationDataInfo>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response> HeartbeatAsync(global::Unity.Services.Lobbies.Lobby.HeartbeatRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{ "204", null },
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response(obj);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> JoinLobbyByCodeAsync(global::Unity.Services.Lobbies.Lobby.JoinLobbyByCodeRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"409",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> JoinLobbyByIdAsync(global::Unity.Services.Lobbies.Lobby.JoinLobbyByIdRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"409",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"412",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.QueryResponse>> QueryLobbiesAsync(global::Unity.Services.Lobbies.Lobby.QueryLobbiesRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.QueryResponse)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.QueryResponse result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.QueryResponse>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.QueryResponse>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> QuickJoinLobbyAsync(global::Unity.Services.Lobbies.Lobby.QuickJoinLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"409",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> ReconnectAsync(global::Unity.Services.Lobbies.Lobby.ReconnectRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"409",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response> RemovePlayerAsync(global::Unity.Services.Lobbies.Lobby.RemovePlayerRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{ "204", null },
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"412",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("DELETE", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response(obj);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.TokenData>>> RequestTokensAsync(global::Unity.Services.Lobbies.Lobby.RequestTokensRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.TokenData>)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.TokenData> result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.TokenData>>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.TokenData>>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> UpdateLobbyAsync(global::Unity.Services.Lobbies.Lobby.UpdateLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"412",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> UpdatePlayerAsync(global::Unity.Services.Lobbies.Lobby.UpdatePlayerRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
			{
				{
					"200",
					typeof(global::Unity.Services.Lobbies.Models.Lobby)
				},
				{
					"400",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"403",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"404",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"412",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"429",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				},
				{
					"500",
					typeof(global::Unity.Services.Lobbies.Models.ErrorStatus)
				}
			};
			global::Unity.Services.Lobbies.Configuration configuration = global::Unity.Services.Lobbies.Configuration.MergeConfigurations(operationConfiguration, Configuration);
			global::Unity.Services.Lobbies.Http.HttpClientResponse obj = await HttpClient.MakeRequestAsync("POST", request.ConstructUrl(configuration.BasePath), request.ConstructBody(), request.ConstructHeaders(_accessToken, configuration), configuration.RequestTimeout ?? 10);
			global::Unity.Services.Lobbies.Models.Lobby result = global::Unity.Services.Lobbies.Http.ResponseHandler.HandleAsyncResponse<global::Unity.Services.Lobbies.Models.Lobby>(obj, statusCodeToTypeMap);
			return new global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>(obj, result);
		}
	}
}
