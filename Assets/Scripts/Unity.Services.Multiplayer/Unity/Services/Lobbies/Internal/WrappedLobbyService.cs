namespace Unity.Services.Lobbies.Internal
{
	internal class WrappedLobbyService : global::Unity.Services.Lobbies.ILobbyService, global::Unity.Services.Lobbies.ILobbyServiceSDK, global::Unity.Services.Lobbies.ILobbyServiceSDKConfiguration, global::Unity.Services.Lobbies.Internal.ILobbyServiceInternal
	{
		private class DownloadMigrationDataRequest
		{
			public global::Unity.Services.Lobbies.Models.MigrationDataInfo MigrationDataInfo { get; set; }

			public global::Unity.Services.Lobbies.Models.LobbyDownloadMigrationDataOptions Options { get; set; }

			internal DownloadMigrationDataRequest(global::Unity.Services.Lobbies.Models.MigrationDataInfo migrationDataInfo, global::Unity.Services.Lobbies.Models.LobbyDownloadMigrationDataOptions options)
			{
				MigrationDataInfo = migrationDataInfo;
				Options = options;
			}
		}

		private class UploadMigrationDataRequest
		{
			public global::Unity.Services.Lobbies.Models.MigrationDataInfo MigrationDataInfo { get; set; }

			public global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataOptions Options { get; set; }

			public byte[] Data { get; set; }

			internal UploadMigrationDataRequest(global::Unity.Services.Lobbies.Models.MigrationDataInfo migrationDataInfo, byte[] data, global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataOptions options)
			{
				MigrationDataInfo = migrationDataInfo;
				Data = data;
				Options = options;
			}
		}

		private const int k_CommonErrorCodeRange = 100;

		private const string k_InvalidArgumentExceptionMessage = "Argument should be non-null, non-empty & not only whitespaces.";

		internal global::Unity.Services.Lobbies.ILobbyServiceSdk m_LobbyService;

		internal global::Unity.Services.Lobbies.Internal.LobbyChannel m_LobbyChannel;

		internal global::Lobbies.SDK.LobbyCacher.LobbyCacher m_LobbyCacher;

		private readonly global::Unity.Services.Lobbies.Http.ApiTelemetryScopeFactory m_TelemetryScopeFactory;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerId m_PlayerId;

		private readonly global::Unity.Services.Multiplayer.IServiceID m_ServiceId;

		internal const int LOBBY_ERROR_MIN_RANGE = 16000;

		internal global::Unity.Services.Lobbies.Http.IHttpClient m_HttpClient;

		public bool ConcurrencyControlEnabled { get; set; }

		internal WrappedLobbyService(global::Unity.Services.Lobbies.ILobbyServiceSdk lobbyService, global::Unity.Services.Authentication.Internal.IPlayerId playerId, global::Unity.Services.Multiplayer.IServiceID serviceId)
		{
			m_LobbyService = lobbyService;
			m_PlayerId = playerId;
			m_ServiceId = serviceId;
			m_TelemetryScopeFactory = new global::Unity.Services.Lobbies.Http.ApiTelemetryScopeFactory(lobbyService.Metrics);
			m_LobbyCacher = new global::Lobbies.SDK.LobbyCacher.LobbyCacher(m_PlayerId?.PlayerId);
			m_HttpClient = new global::Unity.Services.Lobbies.Http.HttpClient();
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> CreateLobbyAsync(string lobbyName, int maxPlayers, global::Unity.Services.Lobbies.CreateLobbyOptions options = null)
		{
			ValidateStringParam("lobbyName", lobbyName);
			if (maxPlayers < 1)
			{
				throw new global::System.InvalidOperationException("Parameters 'maxPlayers' cannot be less than 1.");
			}
			global::Unity.Services.Lobbies.Models.CreateRequest createRequest = ConvertCreateOptionsToRequest(lobbyName, maxPlayers, options);
			global::Unity.Services.Lobbies.Internal.WrappedLobbyService wrappedLobbyService = this;
			global::System.Func<global::Unity.Services.Lobbies.Lobby.CreateLobbyRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>> func = m_LobbyService.LobbyApi.CreateLobbyAsync;
			global::Unity.Services.Lobbies.Models.CreateRequest createRequest2 = createRequest;
			global::Unity.Services.Lobbies.Models.Lobby result = (await wrappedLobbyService.TryCatchRequest("CreateLobby", func, new global::Unity.Services.Lobbies.Lobby.CreateLobbyRequest(m_ServiceId?.ServiceID, null, createRequest2))).Result;
			AddOrUpdateLobbyCache(result);
			return result;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> CreateOrJoinLobbyAsync(string lobbyId, string lobbyName, int maxPlayers, global::Unity.Services.Lobbies.CreateLobbyOptions createOptions = null)
		{
			ValidateStringParam("lobbyId", lobbyId);
			ValidateStringParam("lobbyName", lobbyName);
			if (maxPlayers < 1)
			{
				throw new global::System.InvalidOperationException("Parameters 'maxPlayers' cannot be less than 1.");
			}
			global::Unity.Services.Lobbies.Models.CreateRequest createRequest = ConvertCreateOptionsToRequest(lobbyName, maxPlayers, createOptions);
			global::Unity.Services.Lobbies.Lobby.CreateOrJoinLobbyRequest request = new global::Unity.Services.Lobbies.Lobby.CreateOrJoinLobbyRequest(lobbyId, m_ServiceId?.ServiceID, null, createRequest);
			global::Unity.Services.Lobbies.Models.Lobby result = (await TryCatchRequest("CreateOrJoinLobby", (global::System.Func<global::Unity.Services.Lobbies.Lobby.CreateOrJoinLobbyRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>>)m_LobbyService.LobbyApi.CreateOrJoinLobbyAsync, request)).Result;
			AddOrUpdateLobbyCache(result);
			return result;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.ILobbyEvents> SubscribeToLobbyEventsAsync(string lobbyId, LobbyEventCallbacks lobbyEventCallbacks)
		{
			if (string.IsNullOrWhiteSpace(lobbyId))
			{
				throw new global::System.ArgumentNullException("lobbyId", "Cannot be null or empty.");
			}
			if (m_LobbyService.Wire != null)
			{
				global::Unity.Services.Wire.Internal.IChannel channel = m_LobbyService.Wire.CreateChannel(new global::Unity.Services.Lobbies.Internal.LobbyWireTokenProvider(lobbyId, this));
				m_LobbyChannel = new global::Unity.Services.Lobbies.Internal.LobbyChannel(m_PlayerId, channel, lobbyEventCallbacks, lobbyId, this);
				global::System.GC.SuppressFinalize(m_LobbyChannel);
				await m_LobbyChannel.SubscribeAsync();
				m_LobbyCacher?.WithEventSubscription(m_LobbyChannel.Callbacks);
				return m_LobbyChannel;
			}
			return null;
		}

		public global::Unity.Services.Lobbies.ILobbyEvents SetCacherLobbyCallbacks(string lobbyId, LobbyEventCallbacks lobbyEventCallbacks)
		{
			if (m_LobbyCacher == null)
			{
				return null;
			}
			m_LobbyCacher.WithEventSubscription(lobbyEventCallbacks);
			return m_LobbyCacher;
		}

		public global::System.Threading.Tasks.Task DeleteLobbyAsync(string lobbyId)
		{
			return DeleteLobbyAsync(lobbyId, ConcurrencyControlEnabled);
		}

		public async global::System.Threading.Tasks.Task DeleteLobbyAsync(string lobbyId, bool applyIfMatch)
		{
			ValidateStringParam("lobbyId", lobbyId);
			string ifMatchTag = m_LobbyCacher.GetIfMatchTag(lobbyId, applyIfMatch);
			await TryCatchRequest("DeleteLobby", m_LobbyService.LobbyApi.DeleteLobbyAsync, new global::Unity.Services.Lobbies.Lobby.DeleteLobbyRequest(lobbyId, m_ServiceId?.ServiceID, null, ifMatchTag));
			m_LobbyCacher.RemoveLobbyCache(lobbyId);
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<string>> GetJoinedLobbiesAsync()
		{
			global::Unity.Services.Lobbies.Lobby.GetJoinedLobbiesRequest request = new global::Unity.Services.Lobbies.Lobby.GetJoinedLobbiesRequest();
			return (await TryCatchRequest("GetJoinedLobbies", (global::System.Func<global::Unity.Services.Lobbies.Lobby.GetJoinedLobbiesRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.List<string>>>>)m_LobbyService.LobbyApi.GetJoinedLobbiesAsync, request)).Result;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> GetLobbyAsync(string lobbyId)
		{
			ValidateStringParam("lobbyId", lobbyId);
			global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby> response = await TryCatchRequest("GetLobby", (global::System.Func<global::Unity.Services.Lobbies.Lobby.GetLobbyRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>>)m_LobbyService.LobbyApi.GetLobbyAsync, new global::Unity.Services.Lobbies.Lobby.GetLobbyRequest(lobbyId, m_ServiceId?.ServiceID));
			if (response.Result != null)
			{
				AddOrUpdateLobbyCache(response.Result);
			}
			return response.Result;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> GetLobbyAsync(string lobbyId, string ifNoneMatchVersion)
		{
			ValidateStringParam("lobbyId", lobbyId);
			global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby> response = await TryCatchRequest("GetLobby", (global::System.Func<global::Unity.Services.Lobbies.Lobby.GetLobbyRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>>)m_LobbyService.LobbyApi.GetLobbyAsync, new global::Unity.Services.Lobbies.Lobby.GetLobbyRequest(lobbyId, m_ServiceId?.ServiceID, null, ifNoneMatchVersion));
			if (response.Result != null)
			{
				AddOrUpdateLobbyCache(response.Result);
			}
			return response.Result;
		}

		public async global::System.Threading.Tasks.Task SendHeartbeatPingAsync(string lobbyId)
		{
			ValidateStringParam("lobbyId", lobbyId);
			await TryCatchRequest("Heartbeat", m_LobbyService.LobbyApi.HeartbeatAsync, new global::Unity.Services.Lobbies.Lobby.HeartbeatRequest(lobbyId, m_ServiceId?.ServiceID));
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> JoinLobbyByCodeAsync(string lobbyCode, global::Unity.Services.Lobbies.JoinLobbyByCodeOptions options = null)
		{
			ValidateStringParam("lobbyCode", lobbyCode);
			try
			{
				global::Unity.Services.Lobbies.Lobby.JoinLobbyByCodeRequest request = new global::Unity.Services.Lobbies.Lobby.JoinLobbyByCodeRequest(null, null, new global::Unity.Services.Lobbies.Models.JoinByCodeRequest(lobbyCode, options?.Player, options?.Password));
				global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby> response = await TryCatchRequest("JoinLobbyByCode", (global::System.Func<global::Unity.Services.Lobbies.Lobby.JoinLobbyByCodeRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>>)m_LobbyService.LobbyApi.JoinLobbyByCodeAsync, request);
				AddOrUpdateLobbyCache(response.Result);
				return response.Result;
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException ex)
			{
				if (ex.Reason == global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyConflict)
				{
					global::Unity.Services.Lobbies.Models.Lobby lobby = await LobbyConflictResolver(options?.Player, null, ex);
					if (lobby != null)
					{
						AddOrUpdateLobbyCache(lobby);
						return lobby;
					}
				}
				throw;
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> JoinLobbyByIdAsync(string lobbyId, global::Unity.Services.Lobbies.JoinLobbyByIdOptions options = null)
		{
			ValidateStringParam("lobbyId", lobbyId);
			try
			{
				global::Unity.Services.Lobbies.Models.JoinByIdRequest joinByIdRequest = new global::Unity.Services.Lobbies.Models.JoinByIdRequest(options?.Password, options?.Player);
				global::Unity.Services.Lobbies.Lobby.JoinLobbyByIdRequest request = new global::Unity.Services.Lobbies.Lobby.JoinLobbyByIdRequest(lobbyId, null, null, null, joinByIdRequest);
				global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby> response = await TryCatchRequest("JoinLobbyById", (global::System.Func<global::Unity.Services.Lobbies.Lobby.JoinLobbyByIdRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>>)m_LobbyService.LobbyApi.JoinLobbyByIdAsync, request);
				AddOrUpdateLobbyCache(response.Result);
				return response.Result;
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException ex)
			{
				if (ex.Reason == global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyConflict)
				{
					global::Unity.Services.Lobbies.Models.Lobby lobby = await LobbyConflictResolver(options?.Player, lobbyId, ex);
					if (lobby != null)
					{
						AddOrUpdateLobbyCache(lobby);
						return lobby;
					}
				}
				throw;
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.QueryResponse> QueryLobbiesAsync(global::Unity.Services.Lobbies.QueryLobbiesOptions options = null)
		{
			global::Unity.Services.Lobbies.Models.QueryRequest queryRequest = ((options == null) ? null : new global::Unity.Services.Lobbies.Models.QueryRequest(options.Count, options.Skip, options.SampleResults, options.Filters, options.Order, options.ContinuationToken));
			global::Unity.Services.Lobbies.Lobby.QueryLobbiesRequest request = new global::Unity.Services.Lobbies.Lobby.QueryLobbiesRequest(m_ServiceId?.ServiceID, null, queryRequest);
			return (await TryCatchRequest("QueryLobbies", (global::System.Func<global::Unity.Services.Lobbies.Lobby.QueryLobbiesRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.QueryResponse>>>)m_LobbyService.LobbyApi.QueryLobbiesAsync, request)).Result;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> QuickJoinLobbyAsync(global::Unity.Services.Lobbies.QuickJoinLobbyOptions options = null)
		{
			try
			{
				global::Unity.Services.Lobbies.Models.QuickJoinRequest quickJoinRequest = ((options == null) ? null : new global::Unity.Services.Lobbies.Models.QuickJoinRequest(options.Filter, options.Player));
				global::Unity.Services.Lobbies.Lobby.QuickJoinLobbyRequest request = new global::Unity.Services.Lobbies.Lobby.QuickJoinLobbyRequest(null, null, quickJoinRequest);
				global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby> response = await TryCatchRequest("QuickJoinLobby", (global::System.Func<global::Unity.Services.Lobbies.Lobby.QuickJoinLobbyRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>>)m_LobbyService.LobbyApi.QuickJoinLobbyAsync, request);
				AddOrUpdateLobbyCache(response.Result);
				return response.Result;
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException ex)
			{
				if (ex.Reason == global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyConflict)
				{
					global::Unity.Services.Lobbies.Models.Lobby lobby = await LobbyConflictResolver(options?.Player, null, ex);
					if (lobby != null)
					{
						AddOrUpdateLobbyCache(lobby);
						return lobby;
					}
				}
				throw;
			}
		}

		public global::System.Threading.Tasks.Task RemovePlayerAsync(string lobbyId, string playerId)
		{
			return RemovePlayerAsync(lobbyId, playerId, ConcurrencyControlEnabled);
		}

		public global::System.Threading.Tasks.Task RemovePlayerAsync(string lobbyId, string playerId, bool applyIfMatch)
		{
			ValidateStringParam("lobbyId", lobbyId);
			ValidateStringParam("playerId", playerId);
			string impersonatedUserId = null;
			if (m_ServiceId?.ServiceID != null && m_PlayerId?.PlayerId != playerId)
			{
				impersonatedUserId = playerId;
			}
			global::Unity.Services.Lobbies.Lobby.RemovePlayerRequest removePlayerRequest = new global::Unity.Services.Lobbies.Lobby.RemovePlayerRequest(ifMatch: m_LobbyCacher.GetIfMatchTag(lobbyId, applyIfMatch), lobbyId: lobbyId, playerId: playerId, serviceId: m_ServiceId?.ServiceID, impersonatedUserId: impersonatedUserId);
			return RemovePlayerTask();
			async global::System.Threading.Tasks.Task RemovePlayerTask()
			{
				try
				{
					await TryCatchRequest("RemovePlayer", m_LobbyService.LobbyApi.RemovePlayerAsync, removePlayerRequest);
				}
				catch (global::Unity.Services.Lobbies.LobbyServiceException ex) when (ex.Reason == global::Unity.Services.Lobbies.LobbyExceptionReason.PlayerNotFound)
				{
				}
			}
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> UpdateLobbyAsync(string lobbyId, global::Unity.Services.Lobbies.UpdateLobbyOptions options)
		{
			return UpdateLobbyAsync(lobbyId, options, ConcurrencyControlEnabled);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> UpdateLobbyAsync(string lobbyId, global::Unity.Services.Lobbies.UpdateLobbyOptions options, bool applyIfMatch)
		{
			ValidateStringParam("lobbyId", lobbyId);
			if (options == null)
			{
				throw new global::System.ArgumentNullException("options", "Update Lobby Options object must not be null.");
			}
			string ifMatchTag = m_LobbyCacher.GetIfMatchTag(lobbyId, applyIfMatch);
			global::Unity.Services.Lobbies.Models.UpdateRequest updateRequest = new global::Unity.Services.Lobbies.Models.UpdateRequest(options.Name, options.MaxPlayers, options.IsPrivate, options.IsLocked, options.Data, options.HostId, options.Password);
			global::Unity.Services.Lobbies.Models.UpdateRequest updateRequest2 = updateRequest;
			global::Unity.Services.Lobbies.Lobby.UpdateLobbyRequest request = new global::Unity.Services.Lobbies.Lobby.UpdateLobbyRequest(lobbyId, m_ServiceId?.ServiceID, null, ifMatchTag, updateRequest2);
			global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby> response = await TryCatchRequest("UpdateLobby", (global::System.Func<global::Unity.Services.Lobbies.Lobby.UpdateLobbyRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>>)m_LobbyService.LobbyApi.UpdateLobbyAsync, request);
			AddOrUpdateLobbyCache(response.Result);
			return response.Result;
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> UpdatePlayerAsync(string lobbyId, string playerId, global::Unity.Services.Lobbies.UpdatePlayerOptions options)
		{
			return UpdatePlayerAsync(lobbyId, playerId, options, ConcurrencyControlEnabled);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> UpdatePlayerAsync(string lobbyId, string playerId, global::Unity.Services.Lobbies.UpdatePlayerOptions options, bool applyIfMatch)
		{
			ValidateStringParam("lobbyId", lobbyId);
			ValidateStringParam("playerId", playerId);
			if (options == null)
			{
				throw new global::System.ArgumentNullException("options", "Update player options object must not be null.");
			}
			string ifMatchTag = m_LobbyCacher.GetIfMatchTag(lobbyId, applyIfMatch);
			global::Unity.Services.Lobbies.Models.PlayerUpdateRequest playerUpdateRequest = new global::Unity.Services.Lobbies.Models.PlayerUpdateRequest(options.ConnectionInfo, options.Data, options.AllocationId);
			global::Unity.Services.Lobbies.Models.PlayerUpdateRequest playerUpdateRequest2 = playerUpdateRequest;
			global::Unity.Services.Lobbies.Lobby.UpdatePlayerRequest request = new global::Unity.Services.Lobbies.Lobby.UpdatePlayerRequest(lobbyId, playerId, m_ServiceId?.ServiceID, null, ifMatchTag, playerUpdateRequest2);
			global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby> response = await TryCatchRequest("UpdatePlayer", (global::System.Func<global::Unity.Services.Lobbies.Lobby.UpdatePlayerRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>>)m_LobbyService.LobbyApi.UpdatePlayerAsync, request);
			AddOrUpdateLobbyCache(response.Result);
			return response.Result;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> ReconnectToLobbyAsync(string lobbyId)
		{
			ValidateStringParam("lobbyId", lobbyId);
			global::Unity.Services.Lobbies.Lobby.ReconnectRequest request = new global::Unity.Services.Lobbies.Lobby.ReconnectRequest(lobbyId, m_ServiceId?.ServiceID);
			global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby> response = await TryCatchRequest("Reconnect", (global::System.Func<global::Unity.Services.Lobbies.Lobby.ReconnectRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>>>)m_LobbyService.LobbyApi.ReconnectAsync, request);
			AddOrUpdateLobbyCache(response.Result);
			return response.Result;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.MigrationDataInfo> GetMigrationDataInfoAsync(string lobbyId)
		{
			ValidateStringParam("lobbyId", lobbyId);
			global::Unity.Services.Lobbies.Lobby.GetMigrationDataInfoRequest request = new global::Unity.Services.Lobbies.Lobby.GetMigrationDataInfoRequest(lobbyId, m_ServiceId?.ServiceID);
			return (await TryCatchRequest("GetMigrationDataInfo", (global::System.Func<global::Unity.Services.Lobbies.Lobby.GetMigrationDataInfoRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.MigrationDataInfo>>>)m_LobbyService.LobbyApi.GetMigrationDataInfoAsync, request)).Result;
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.LobbyMigrationData> DownloadMigrationDataAsync(global::Unity.Services.Lobbies.Models.MigrationDataInfo migrationDataInfo, global::Unity.Services.Lobbies.Models.LobbyDownloadMigrationDataOptions options)
		{
			if (migrationDataInfo == null || string.IsNullOrWhiteSpace(migrationDataInfo.Read))
			{
				throw new global::System.ArgumentNullException("Read", "Argument should be non-null, non-empty & not only whitespaces.");
			}
			return DownloadTask();
			async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.LobbyMigrationData> DownloadTask()
			{
				global::Unity.Services.Lobbies.Internal.WrappedLobbyService.DownloadMigrationDataRequest request = new global::Unity.Services.Lobbies.Internal.WrappedLobbyService.DownloadMigrationDataRequest(migrationDataInfo, options);
				return new global::Unity.Services.Lobbies.Models.LobbyMigrationData((await TryCatchRequest("DownloadMigrationData", (global::System.Func<global::Unity.Services.Lobbies.Internal.WrappedLobbyService.DownloadMigrationDataRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<byte[]>>>)DownloadMigrationDataAsyncFunc, request)).Result);
			}
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataResults> UploadMigrationDataAsync(global::Unity.Services.Lobbies.Models.MigrationDataInfo migrationDataInfo, byte[] data, global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataOptions options)
		{
			if (migrationDataInfo == null || string.IsNullOrWhiteSpace(migrationDataInfo.Write))
			{
				throw new global::System.ArgumentNullException("Write", "Argument should be non-null, non-empty & not only whitespaces.");
			}
			if (data == null || data.Length == 0)
			{
				throw new global::System.ArgumentNullException("data", "Argument should be non-null & non-empty.");
			}
			return UploadTask();
			async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataResults> UploadTask()
			{
				global::Unity.Services.Lobbies.Internal.WrappedLobbyService.UploadMigrationDataRequest request = new global::Unity.Services.Lobbies.Internal.WrappedLobbyService.UploadMigrationDataRequest(migrationDataInfo, data, options);
				await TryCatchRequest("UploadMigrationData", UploadMigrationDataAsyncFunc, request);
				return new global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataResults();
			}
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.TokenData>> RequestTokensAsync(string lobbyId, params global::Unity.Services.Lobbies.Models.TokenRequest.TokenTypeOptions[] tokenOptions)
		{
			if (tokenOptions == null || tokenOptions.Length < 1)
			{
				throw new global::System.ArgumentNullException("Unable to request tokens when no token options were chosen to receive from the request!");
			}
			global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.TokenRequest> list = new global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.TokenRequest>(tokenOptions.Length);
			foreach (global::Unity.Services.Lobbies.Models.TokenRequest.TokenTypeOptions tokenType in tokenOptions)
			{
				list.Add(new global::Unity.Services.Lobbies.Models.TokenRequest(tokenType));
			}
			global::Unity.Services.Lobbies.Lobby.RequestTokensRequest request = new global::Unity.Services.Lobbies.Lobby.RequestTokensRequest(lobbyId, list);
			return (await TryCatchRequest("RequestTokens", (global::System.Func<global::Unity.Services.Lobbies.Lobby.RequestTokensRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.TokenData>>>>)m_LobbyService.LobbyApi.RequestTokensAsync, request)).Result;
		}

		public void SetBasePath(string basePath)
		{
			m_LobbyService.Configuration.BasePath = basePath;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response> TryCatchRequest<TRequest>(string api, global::System.Func<TRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response>> func, TRequest request)
		{
			global::Unity.Services.Lobbies.Response response = null;
			try
			{
				using (m_TelemetryScopeFactory.Instrument(api))
				{
					response = await func(request, m_LobbyService.Configuration);
				}
			}
			catch (global::Unity.Services.Lobbies.Http.HttpException<global::Unity.Services.Lobbies.Models.ErrorStatus> ex)
			{
				ResolveErrorWrapping((global::Unity.Services.Lobbies.LobbyExceptionReason)ex.ActualError.Code, ex);
			}
			catch (global::Unity.Services.Lobbies.Http.HttpException ex2)
			{
				int num = (int)ex2.Response.StatusCode;
				global::Unity.Services.Lobbies.LobbyExceptionReason reason = global::Unity.Services.Lobbies.LobbyExceptionReason.Unknown;
				if (ex2.Response.IsNetworkError)
				{
					reason = global::Unity.Services.Lobbies.LobbyExceptionReason.NetworkError;
				}
				else if (ex2.Response.IsHttpError && num < 1000)
				{
					num += 16000;
					if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Lobbies.LobbyExceptionReason), num))
					{
						reason = (global::Unity.Services.Lobbies.LobbyExceptionReason)num;
					}
				}
				ResolveErrorWrapping(reason, ex2);
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException)
			{
				throw;
			}
			catch (global::System.Exception exception)
			{
				ResolveErrorWrapping(global::Unity.Services.Lobbies.LobbyExceptionReason.Unknown, exception);
			}
			return response;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<TReturn>> TryCatchRequest<TRequest, TReturn>(string api, global::System.Func<TRequest, global::Unity.Services.Lobbies.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<TReturn>>> func, TRequest request)
		{
			global::Unity.Services.Lobbies.Response<TReturn> response = null;
			try
			{
				using (m_TelemetryScopeFactory.Instrument(api))
				{
					response = await func(request, m_LobbyService.Configuration);
				}
			}
			catch (global::Unity.Services.Lobbies.Http.HttpException<global::Unity.Services.Lobbies.Models.ErrorStatus> ex)
			{
				ResolveErrorWrapping((global::Unity.Services.Lobbies.LobbyExceptionReason)ex.ActualError.Code, ex);
			}
			catch (global::Unity.Services.Lobbies.Http.HttpException ex2)
			{
				int num = (int)ex2.Response.StatusCode;
				global::Unity.Services.Lobbies.LobbyExceptionReason reason = global::Unity.Services.Lobbies.LobbyExceptionReason.Unknown;
				if (ex2.Response.IsNetworkError)
				{
					reason = global::Unity.Services.Lobbies.LobbyExceptionReason.NetworkError;
				}
				else if (ex2.Response.IsHttpError && num < 1000)
				{
					num += 16000;
					if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Lobbies.LobbyExceptionReason), num))
					{
						reason = (global::Unity.Services.Lobbies.LobbyExceptionReason)num;
					}
				}
				ResolveErrorWrapping(reason, ex2);
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException)
			{
				throw;
			}
			catch (global::System.Exception exception)
			{
				ResolveErrorWrapping(global::Unity.Services.Lobbies.LobbyExceptionReason.Unknown, exception);
			}
			return response;
		}

		private void ResolveErrorWrapping(global::Unity.Services.Lobbies.LobbyExceptionReason reason, global::System.Exception exception = null)
		{
			if (reason == global::Unity.Services.Lobbies.LobbyExceptionReason.Unknown)
			{
				throw new global::Unity.Services.Lobbies.LobbyServiceException(reason, "Something went wrong.", exception);
			}
			if (TryMapCommonErrorCodeToLobbyExceptionReason((int)reason, out var reason2))
			{
				reason = reason2;
			}
			throw ConvertToLobbyServiceException(reason, exception);
		}

		private static global::Unity.Services.Lobbies.LobbyServiceException ConvertToLobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason reason, global::System.Exception exception)
		{
			if (!(exception is global::Unity.Services.Lobbies.Http.HttpException<global::Unity.Services.Lobbies.Models.ErrorStatus> ex))
			{
				return new global::Unity.Services.Lobbies.LobbyServiceException(reason, exception?.Message ?? string.Empty, exception);
			}
			string message = ex.ActualError.Detail;
			global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Detail> list = ex.ActualError?.Details;
			if (list != null && list.Count > 0)
			{
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(ex.ActualError.Detail, global::System.Linq.Enumerable.Sum(list, (global::Unity.Services.Lobbies.Models.Detail d) => d.Message.Length));
				for (int num = 0; num < list.Count; num++)
				{
					global::Unity.Services.Lobbies.Models.Detail detail = list[num];
					stringBuilder.AppendLine();
					stringBuilder.Append(detail.Message);
					if (num < list.Count - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				message = stringBuilder.ToString();
			}
			return new global::Unity.Services.Lobbies.LobbyServiceException(reason, message, ex);
		}

		private static bool TryMapCommonErrorCodeToLobbyExceptionReason(int code, out global::Unity.Services.Lobbies.LobbyExceptionReason reason)
		{
			if (code < 100)
			{
				switch (code)
				{
				case 0:
					reason = global::Unity.Services.Lobbies.LobbyExceptionReason.Unknown;
					break;
				case 3:
					reason = global::Unity.Services.Lobbies.LobbyExceptionReason.ServiceUnavailable;
					break;
				case 50:
					reason = global::Unity.Services.Lobbies.LobbyExceptionReason.RateLimited;
					break;
				case 53:
					reason = global::Unity.Services.Lobbies.LobbyExceptionReason.Forbidden;
					break;
				case 54:
					reason = global::Unity.Services.Lobbies.LobbyExceptionReason.EntityNotFound;
					break;
				case 55:
					reason = global::Unity.Services.Lobbies.LobbyExceptionReason.InvalidArgument;
					break;
				default:
					reason = global::Unity.Services.Lobbies.LobbyExceptionReason.UnknownErrorCode;
					break;
				}
				return true;
			}
			reason = global::Unity.Services.Lobbies.LobbyExceptionReason.Unknown;
			return false;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> LobbyConflictResolver(global::Unity.Services.Lobbies.Models.Player player, string lobbyId = null, global::Unity.Services.Lobbies.LobbyServiceException e = null)
		{
			if (!string.IsNullOrWhiteSpace(lobbyId))
			{
				global::System.Collections.Generic.List<string> list = await GetJoinedLobbiesAsync();
				if (list.Count != 1)
				{
					return null;
				}
				lobbyId = list[0];
			}
			if (lobbyId == null)
			{
				return null;
			}
			global::Unity.Services.Lobbies.Models.Lobby lobby = await GetLobbyAsync(lobbyId);
			if (lobby == null || player?.Id == null)
			{
				return lobby;
			}
			global::Unity.Services.Lobbies.Models.Player player2 = global::System.Linq.Enumerable.FirstOrDefault(lobby.Players, (global::Unity.Services.Lobbies.Models.Player x) => x.Id == player.Id);
			if (player2 == null)
			{
				throw new global::Unity.Services.Lobbies.LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason.PlayerNotFound, "Lobby join call failed and player was not added to Lobby due to an unexpected error.", e);
			}
			if (IsPlayerDataEqual(player, player2))
			{
				return lobby;
			}
			global::Unity.Services.Lobbies.UpdatePlayerOptions options = new global::Unity.Services.Lobbies.UpdatePlayerOptions
			{
				ConnectionInfo = player.ConnectionInfo,
				Data = player.Data,
				AllocationId = player.AllocationId
			};
			return await UpdatePlayerAsync(lobbyId, player.Id, options);
		}

		private bool IsPlayerDataEqual(global::Unity.Services.Lobbies.Models.Player a, global::Unity.Services.Lobbies.Models.Player b)
		{
			bool flag = a.Id == b.Id;
			flag &= a.ConnectionInfo == b.ConnectionInfo;
			flag &= a.AllocationId == b.AllocationId;
			flag &= a.Joined == b.Joined;
			flag &= a.LastUpdated == b.LastUpdated;
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject>.KeyCollection keys = a.Data.Keys;
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject>.KeyCollection keys2 = b.Data.Keys;
			bool flag2 = global::System.Linq.Enumerable.All(keys, ((global::System.Collections.Generic.IEnumerable<string>)keys2).Contains<string>) && keys.Count == keys2.Count;
			flag = flag && flag2;
			if (!flag)
			{
				return false;
			}
			foreach (string item in keys)
			{
				global::Unity.Services.Lobbies.Models.PlayerDataObject playerDataObject = a.Data[item];
				global::Unity.Services.Lobbies.Models.PlayerDataObject playerDataObject2 = b.Data[item];
				flag &= playerDataObject.Value == playerDataObject2.Value;
				flag &= playerDataObject.Visibility == playerDataObject2.Visibility;
			}
			return flag;
		}

		internal void AddOrUpdateLobbyCache(global::Unity.Services.Lobbies.Models.Lobby newLobby)
		{
			if (!m_LobbyCacher.TryGetLobbyCache(newLobby.Id, out var _))
			{
				global::Unity.Services.Lobbies.Models.Lobby lobby = CloneLobbyHelper(newLobby);
				m_LobbyCacher.AddLobbyCache(lobby.Id, lobby);
			}
			else
			{
				m_LobbyCacher.UpdateLobbyCache(newLobby.Id, newLobby);
			}
		}

		internal static global::Unity.Services.Lobbies.Models.Lobby CloneLobbyHelper(global::Unity.Services.Lobbies.Models.Lobby otherLobby)
		{
			global::Unity.Services.Lobbies.Models.Lobby lobby = new global::Unity.Services.Lobbies.Models.Lobby();
			lobby.Version = otherLobby.Version;
			lobby.Id = otherLobby.Id;
			lobby.Name = otherLobby.Name;
			lobby.AvailableSlots = otherLobby.AvailableSlots;
			lobby.HasPassword = otherLobby.HasPassword;
			if (otherLobby.Players != null)
			{
				lobby.Players = new global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player>();
				foreach (global::Unity.Services.Lobbies.Models.Player player2 in otherLobby.Players)
				{
					global::Unity.Services.Lobbies.Models.Player player = new global::Unity.Services.Lobbies.Models.Player
					{
						Id = player2.Id,
						AllocationId = player2.AllocationId,
						Joined = player2.Joined,
						ConnectionInfo = player2.ConnectionInfo,
						LastUpdated = player2.LastUpdated,
						Profile = player2.Profile
					};
					if (player2.Data != null)
					{
						player.Data = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject>();
						foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Lobbies.Models.PlayerDataObject> datum in player2.Data)
						{
							player.Data[datum.Key] = new global::Unity.Services.Lobbies.Models.PlayerDataObject(datum.Value.Visibility, datum.Value.Value);
						}
					}
					lobby.Players.Add(player);
				}
			}
			if (otherLobby.Data != null)
			{
				lobby.Data = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject>();
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Lobbies.Models.DataObject> datum2 in otherLobby.Data)
				{
					lobby.Data[datum2.Key] = new global::Unity.Services.Lobbies.Models.DataObject(datum2.Value.Visibility, datum2.Value.Value, datum2.Value.Index);
				}
			}
			lobby.Upid = otherLobby.Upid;
			lobby.EnvironmentId = otherLobby.EnvironmentId;
			lobby.HostId = otherLobby.HostId;
			lobby.IsLocked = otherLobby.IsLocked;
			lobby.IsPrivate = otherLobby.IsPrivate;
			lobby.LobbyCode = otherLobby.LobbyCode;
			lobby.MaxPlayers = otherLobby.MaxPlayers;
			lobby.Created = otherLobby.Created;
			lobby.LastUpdated = otherLobby.LastUpdated;
			return lobby;
		}

		public global::Lobbies.SDK.LobbyCacher.LobbyCacher GetLobbyCacher()
		{
			return m_LobbyCacher;
		}

		private global::Unity.Services.Lobbies.Models.CreateRequest ConvertCreateOptionsToRequest(string lobbyName, int maxPlayers, global::Unity.Services.Lobbies.CreateLobbyOptions options)
		{
			return new global::Unity.Services.Lobbies.Models.CreateRequest(lobbyName, maxPlayers, options?.IsPrivate, options?.IsLocked, options?.Player, options?.Data, options?.Password);
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<byte[]>> DownloadMigrationDataAsyncFunc(global::Unity.Services.Lobbies.Internal.WrappedLobbyService.DownloadMigrationDataRequest param, global::Unity.Services.Lobbies.Configuration _)
		{
			global::System.TimeSpan timeSpan = param.Options?.Timeout ?? global::Unity.Services.Lobbies.Models.LobbyDownloadMigrationDataOptions.DefaultTimeout;
			global::Unity.Services.Lobbies.Http.HttpClientResponse httpClientResponse = await m_HttpClient.MakeRequestAsync("GET", param.MigrationDataInfo.Read, (byte[])null, new global::System.Collections.Generic.Dictionary<string, string>(), SafeCast(timeSpan.TotalSeconds));
			switch (httpClientResponse.StatusCode)
			{
			case 404L:
				return new global::Unity.Services.Lobbies.Response<byte[]>(httpClientResponse, null);
			case 408L:
				throw new global::Unity.Services.Lobbies.LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason.MigrationDataRequestTimeout, "Download migration data failed due to request timeout");
			default:
				if (httpClientResponse.StatusCode != 200)
				{
					throw new global::Unity.Services.Lobbies.LobbyServiceException(httpClientResponse.StatusCode + 16000, "Download migration data failed");
				}
				return new global::Unity.Services.Lobbies.Response<byte[]>(httpClientResponse, httpClientResponse.Data);
			}
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response> UploadMigrationDataAsyncFunc(global::Unity.Services.Lobbies.Internal.WrappedLobbyService.UploadMigrationDataRequest param, global::Unity.Services.Lobbies.Configuration _)
		{
			global::System.Collections.Generic.Dictionary<string, string> headers = new global::System.Collections.Generic.Dictionary<string, string>
			{
				{ "Content-Type", "application/octet-stream" },
				{
					"x-goog-content-length-range",
					$"0,{param.MigrationDataInfo.MaxSize}"
				}
			};
			global::System.TimeSpan timeSpan = param.Options?.Timeout ?? global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataOptions.DefaultTimeout;
			global::Unity.Services.Lobbies.Http.HttpClientResponse httpClientResponse = await m_HttpClient.MakeRequestAsync("PUT", param.MigrationDataInfo.Write, param.Data, headers, SafeCast(timeSpan.TotalSeconds));
			if (httpClientResponse.StatusCode == 408)
			{
				throw new global::Unity.Services.Lobbies.LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason.MigrationDataRequestTimeout, "Upload migration data failed due to request timeout");
			}
			if (httpClientResponse.StatusCode != 200)
			{
				throw new global::Unity.Services.Lobbies.LobbyServiceException(httpClientResponse.StatusCode + 16000, "Upload migration data failed");
			}
			return new global::Unity.Services.Lobbies.Response(httpClientResponse);
		}

		private static int SafeCast(double value)
		{
			if (!(value > 2147483647.0))
			{
				if (value < -2147483648.0)
				{
					return int.MinValue;
				}
				return (int)global::System.Math.Round(value, global::System.MidpointRounding.AwayFromZero);
			}
			return int.MaxValue;
		}

		private static void ValidateStringParam(string paramName, string paramValue)
		{
			if (string.IsNullOrWhiteSpace(paramValue))
			{
				throw new global::System.ArgumentNullException(paramName, "Argument should be non-null, non-empty & not only whitespaces.");
			}
		}
	}
}
