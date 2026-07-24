namespace Unity.Services.Multiplayer
{
	internal class LobbyHandler : global::Unity.Services.Multiplayer.ILobbyHandler
	{
		internal long? m_HeartbeatActionId;

		internal long? m_PollingActionId;

		internal LobbyEventCallbacks m_Callbacks;

		internal global::Unity.Services.Lobbies.ILobbyEvents m_Events;

		private const int k_PollingDelaySeconds = 1;

		private const double k_MigrationDataExpiryOffsetSeconds = 5.0;

		private readonly global::Unity.Services.Multiplayer.LobbySettings Settings = new global::Unity.Services.Multiplayer.LobbySettings();

		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private readonly global::Unity.Services.Lobbies.Internal.ILobbyServiceInternal m_LobbyService;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerId m_PlayerId;

		private readonly global::Unity.Services.Multiplayer.IServiceID m_ServiceID;

		private readonly global::Unity.Services.Authentication.Internal.IAccessToken m_AccessToken;

		private readonly global::Unity.Services.Authentication.Internal.IAccessTokenObserver m_AccessTokenObserver;

		private readonly bool m_UsePolling;

		internal global::System.Threading.CancellationTokenSource LobbyEventTokenSource;

		public bool IsAuthorized => m_AccessToken.AccessToken != null;

		public bool IsHost
		{
			get
			{
				if (Lobby == null)
				{
					return false;
				}
				if (string.IsNullOrEmpty(Lobby.HostId))
				{
					return false;
				}
				if (m_PlayerId != null)
				{
					return Lobby.HostId.Equals(m_PlayerId.PlayerId);
				}
				if (m_ServiceID != null)
				{
					return Lobby.HostId.Equals(m_ServiceID.ServiceID);
				}
				return false;
			}
		}

		public bool IsServer => m_ServiceID?.ServiceID != null;

		public bool IsMember => Lobby?.Players?.Exists((global::Unity.Services.Lobbies.Models.Player player) => player.Id.Equals(m_PlayerId?.PlayerId)) == true;

		public global::Unity.Services.Lobbies.Models.Lobby Lobby { get; private set; }

		public global::Unity.Services.Multiplayer.LobbyState State { get; private set; }

		public global::Unity.Services.Lobbies.Models.MigrationDataInfo MigrationDataInfo { get; private set; }

		public bool ConcurrencyControlEnabled { get; set; }

		public event global::System.Action LobbyChanged;

		public event global::System.Action LobbyExit;

		public event global::System.Action<string> PlayerJoined;

		public event global::System.Action<string> PlayerLeft;

		public event global::System.Action<string> PlayerLeaving;

		public event global::System.Action<string> PlayerHasLeft;

		public event global::System.Action DataChanged;

		public event global::System.Action PlayerDataChanged;

		public event global::System.Action KickedFromLobby;

		public event global::System.Action LobbyDeleted;

		public event global::System.Action<string> LobbyHostChanged;

		internal LobbyHandler(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::Unity.Services.Lobbies.Internal.ILobbyServiceInternal lobbyService, global::Unity.Services.Authentication.Internal.IPlayerId playerId, global::Unity.Services.Multiplayer.IServiceID serviceID, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Authentication.Internal.IAccessTokenObserver accessTokenObserver, bool usePolling)
		{
			m_ActionScheduler = actionScheduler;
			m_LobbyService = lobbyService;
			m_PlayerId = playerId;
			m_ServiceID = serviceID;
			m_AccessToken = accessToken;
			m_AccessTokenObserver = accessTokenObserver;
			m_UsePolling = usePolling;
		}

		public void AssignLobby(global::Unity.Services.Lobbies.Models.Lobby lobby, global::Unity.Services.Multiplayer.LobbyState state)
		{
			ValidateAuthorization();
			ValidateNoActiveLobby();
			Lobby = lobby;
			State = state;
			RegisterPlayerEvents();
			if (IsHost)
			{
				ScheduleHeartbeat();
			}
			this.LobbyChanged?.Invoke();
		}

		internal LobbyEventCallbacks LobbyCallbacks()
		{
			m_Callbacks = new LobbyEventCallbacks();
			m_Callbacks.LobbyChanged += OnLobbyChanged;
			m_Callbacks.PlayerJoined += OnPlayerJoined;
			m_Callbacks.DataChanged += OnDataChanged;
			m_Callbacks.DataRemoved += OnDataChanged;
			m_Callbacks.PlayerDataChanged += OnPlayerDataChanged;
			m_Callbacks.PlayerDataRemoved += OnPlayerDataChanged;
			m_Callbacks.LobbyDeleted += OnLobbyDeleted;
			m_Callbacks.KickedFromLobby += OnKickedFromLobby;
			return m_Callbacks;
		}

		private async global::System.Threading.Tasks.Task LobbyUnsubscribeCallbacksAsync()
		{
			if (m_Callbacks != null)
			{
				m_Callbacks.KickedFromLobby -= OnKickedFromLobby;
				m_Callbacks.LobbyChanged -= OnLobbyChanged;
				m_Callbacks.PlayerJoined -= OnPlayerJoined;
				m_Callbacks.DataChanged -= OnDataChanged;
				m_Callbacks.DataRemoved -= OnDataChanged;
				m_Callbacks.PlayerDataChanged -= OnPlayerDataChanged;
				m_Callbacks.PlayerDataRemoved -= OnPlayerDataChanged;
				m_Callbacks.LobbyDeleted -= OnLobbyDeleted;
				m_Callbacks.KickedFromLobby -= OnKickedFromLobby;
				if (m_Events != null)
				{
					await m_Events.UnsubscribeAsync();
				}
			}
		}

		internal void OnKickedFromLobby()
		{
			CancelPolling();
			this.KickedFromLobby?.Invoke();
		}

		internal void OnLobbyDeleted()
		{
			CancelPolling();
			this.LobbyDeleted?.Invoke();
		}

		internal void OnLobbyChanged(global::Unity.Services.Lobbies.ILobbyChanges changes)
		{
			if (changes != null)
			{
				global::System.Collections.Generic.List<string> playerIdsLeaving = new global::System.Collections.Generic.List<string>();
				if (changes.PlayerLeft.Changed || changes.PlayerLeft.Added)
				{
					playerIdsLeaving = OnPlayerLeaving(changes.PlayerLeft.Value);
				}
				if (changes.PlayerData.Changed)
				{
					OnPlayerDataChanged(null);
				}
				if (changes.Version.Changed && changes.Version.Value > Lobby.Version)
				{
					changes.ApplyToLobby(Lobby);
					this.LobbyChanged?.Invoke();
				}
				if (changes.HostId.Changed || changes.HostId.Added)
				{
					this.LobbyHostChanged?.Invoke(changes.HostId.Value);
				}
				OnPlayerHasLeft(playerIdsLeaving);
				if (IsHost && !m_HeartbeatActionId.HasValue)
				{
					ScheduleHeartbeat();
				}
			}
		}

		internal void OnDataChanged(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>> _)
		{
			this.DataChanged?.Invoke();
		}

		internal void OnPlayerDataChanged(global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>> _)
		{
			this.PlayerDataChanged?.Invoke();
		}

		internal void OnPlayerJoined(global::System.Collections.Generic.List<global::Unity.Services.Lobbies.LobbyPlayerJoined> players)
		{
			if (this.PlayerJoined == null)
			{
				return;
			}
			foreach (global::Unity.Services.Lobbies.LobbyPlayerJoined player in players)
			{
				this.PlayerJoined(player.Player.Id);
			}
		}

		internal global::System.Collections.Generic.List<string> OnPlayerLeaving(global::System.Collections.Generic.List<int> players)
		{
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			foreach (int item in global::System.Linq.Enumerable.Where(players, delegate(int playerIndex)
			{
				global::Unity.Services.Lobbies.Models.Lobby lobby = Lobby;
				return lobby != null && lobby.Players?.Count > playerIndex;
			}))
			{
				this.PlayerLeft?.Invoke(Lobby.Players[item].Id);
				this.PlayerLeaving?.Invoke(Lobby.Players[item].Id);
				list.Add(Lobby.Players[item].Id);
			}
			return list;
		}

		internal void OnPlayerHasLeft(global::System.Collections.Generic.List<string> playerIdsLeaving)
		{
			foreach (string item in playerIdsLeaving)
			{
				this.PlayerHasLeft?.Invoke(item);
			}
		}

		public async global::System.Threading.Tasks.Task CreateLobbyAsync(string lobbyName, int maxPlayers, global::Unity.Services.Lobbies.CreateLobbyOptions options = null)
		{
			ValidateAuthorization();
			ValidateNoActiveLobby();
			try
			{
				AssignLobby(await m_LobbyService.CreateLobbyAsync(lobbyName, maxPlayers, options), global::Unity.Services.Multiplayer.LobbyState.Joined);
				await InitLobbyEventsAsync();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		private async global::System.Threading.Tasks.Task InitLobbyEventsAsync()
		{
			if (m_UsePolling)
			{
				global::System.Threading.CancellationTokenSource internalTokenSource = new global::System.Threading.CancellationTokenSource();
				global::UnityEngine.Application.wantsToQuit += delegate
				{
					internalTokenSource.Cancel();
					return true;
				};
				global::System.Threading.CancellationToken exitCancellationToken = global::UnityEngine.Application.exitCancellationToken;
				LobbyEventTokenSource = global::System.Threading.CancellationTokenSource.CreateLinkedTokenSource(internalTokenSource.Token, exitCancellationToken);
				await global::System.Threading.Tasks.Task.Run(delegate
				{
					PollForLobbyEvents(LobbyCallbacks());
				}, LobbyEventTokenSource.Token);
			}
			else
			{
				await SubscribeToLobbyEventsAsync(LobbyCallbacks());
			}
		}

		public async global::System.Threading.Tasks.Task CreateOrJoinLobbyAsync(string id, string name, int maxPlayers, global::Unity.Services.Lobbies.CreateLobbyOptions options = null)
		{
			ValidateAuthorization();
			ValidateNoActiveLobby();
			try
			{
				AssignLobby(await m_LobbyService.CreateOrJoinLobbyAsync(id, name, maxPlayers, options), global::Unity.Services.Multiplayer.LobbyState.Joined);
				await InitLobbyEventsAsync();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task RefreshLobbyAsync()
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			try
			{
				Lobby = await m_LobbyService.GetLobbyAsync(Lobby.Id);
				this.LobbyChanged?.Invoke();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task GetLobbyAsync(string lobbyId)
		{
			ValidateAuthorization();
			ValidateNoActiveLobby();
			try
			{
				AssignLobby(await m_LobbyService.GetLobbyAsync(lobbyId), global::Unity.Services.Multiplayer.LobbyState.Joined);
				await InitLobbyEventsAsync();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task UpdateLobbyAsync(global::Unity.Services.Lobbies.UpdateLobbyOptions updateLobbyOptions)
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			try
			{
				if ((await m_LobbyService.UpdateLobbyAsync(Lobby.Id, updateLobbyOptions, ConcurrencyControlEnabled)).Version > Lobby.Version)
				{
					this.LobbyChanged?.Invoke();
				}
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task UpdateLobbyDataAsync(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> data)
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			try
			{
				Lobby = await m_LobbyService.UpdateLobbyAsync(Lobby.Id, new global::Unity.Services.Lobbies.UpdateLobbyOptions
				{
					Data = data
				}, ConcurrencyControlEnabled);
				this.LobbyChanged?.Invoke();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task UpdateCurrentPlayerAsync(global::Unity.Services.Lobbies.UpdatePlayerOptions updatePlayerOptions)
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			try
			{
				Lobby = await m_LobbyService.UpdatePlayerAsync(Lobby.Id, m_PlayerId?.PlayerId, updatePlayerOptions, ConcurrencyControlEnabled);
				this.LobbyChanged?.Invoke();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task UpdatePlayerAsync(string playerId, global::Unity.Services.Lobbies.UpdatePlayerOptions updatePlayerOptions)
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			try
			{
				Lobby = await m_LobbyService.UpdatePlayerAsync(Lobby.Id, playerId, updatePlayerOptions, ConcurrencyControlEnabled);
				this.LobbyChanged?.Invoke();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task DeleteLobbyAsync()
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			try
			{
				await m_LobbyService.DeleteLobbyAsync(Lobby.Id, ConcurrencyControlEnabled);
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException ex)
			{
				if (ex.Reason != global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyNotFound)
				{
					throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(ex);
				}
			}
			catch (global::System.Exception ex2)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex2.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
			try
			{
				await ResetAsync();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex3)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex3.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task JoinLobbyByIdAsync(string lobbyId, global::Unity.Services.Lobbies.JoinLobbyByIdOptions options = null)
		{
			ValidateAuthorization();
			ValidateNoActiveLobby();
			try
			{
				AssignLobby(await m_LobbyService.JoinLobbyByIdAsync(lobbyId, options), global::Unity.Services.Multiplayer.LobbyState.Joined);
				await InitLobbyEventsAsync();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task JoinLobbyByCodeAsync(string lobbyCode, global::Unity.Services.Lobbies.JoinLobbyByCodeOptions options = null)
		{
			ValidateAuthorization();
			ValidateNoActiveLobby();
			try
			{
				AssignLobby(await m_LobbyService.JoinLobbyByCodeAsync(lobbyCode, options), global::Unity.Services.Multiplayer.LobbyState.Joined);
				await InitLobbyEventsAsync();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task QuickJoinLobbyAsync(global::Unity.Services.Lobbies.QuickJoinLobbyOptions options)
		{
			ValidateAuthorization();
			ValidateNoActiveLobby();
			try
			{
				AssignLobby(await m_LobbyService.QuickJoinLobbyAsync(options), global::Unity.Services.Multiplayer.LobbyState.Joined);
				await InitLobbyEventsAsync();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<string>> GetJoinedLobbiesAsync()
		{
			ValidateAuthorization();
			ValidateNoActiveLobby();
			try
			{
				return await m_LobbyService.GetJoinedLobbiesAsync();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task ReconnectToLobbyAsync(string lobbyId = null)
		{
			ValidateAuthorization();
			ValidateNoActiveLobby();
			try
			{
				AssignLobby(await m_LobbyService.ReconnectToLobbyAsync(lobbyId ?? Lobby.Id), global::Unity.Services.Multiplayer.LobbyState.Joined);
				await InitLobbyEventsAsync();
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public global::System.Threading.Tasks.Task RemovePlayerAsync(string playerId)
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			return RemovePlayerTask();
			async global::System.Threading.Tasks.Task RemovePlayerTask()
			{
				try
				{
					await m_LobbyService.RemovePlayerAsync(Lobby.Id, playerId, ConcurrencyControlEnabled);
				}
				catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
				{
					throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
				}
				catch (global::System.Exception ex)
				{
					throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
				}
			}
		}

		private bool IsMigrationDataCacheValid()
		{
			if (MigrationDataInfo != null)
			{
				return MigrationDataInfo.Expires >= global::System.DateTime.UtcNow.Add(global::System.TimeSpan.FromSeconds(5.0));
			}
			return false;
		}

		private async global::System.Threading.Tasks.Task GetMigrationDataInfoAsync()
		{
			try
			{
				MigrationDataInfo = await m_LobbyService.GetMigrationDataInfoAsync(Lobby.Id);
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.LobbyMigrationData> DownloadMigrationDataAsync(global::System.TimeSpan timeout)
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			return DownloadTask();
			async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.LobbyMigrationData> DownloadTask()
			{
				if (!IsMigrationDataCacheValid())
				{
					await GetMigrationDataInfoAsync();
				}
				try
				{
					return await m_LobbyService.DownloadMigrationDataAsync(MigrationDataInfo, new global::Unity.Services.Lobbies.Models.LobbyDownloadMigrationDataOptions
					{
						Timeout = timeout
					});
				}
				catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
				{
					throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
				}
				catch (global::System.Exception ex)
				{
					throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
				}
			}
		}

		public global::System.Threading.Tasks.Task UploadMigrationDataAsync(byte[] data, global::System.TimeSpan timeout)
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			return UploadTask();
			async global::System.Threading.Tasks.Task UploadTask()
			{
				if (!IsMigrationDataCacheValid())
				{
					await GetMigrationDataInfoAsync();
				}
				if (MigrationDataInfo != null && data.Length > MigrationDataInfo.MaxSize)
				{
					throw new global::System.ArgumentOutOfRangeException("data", $"data length must be less than {MigrationDataInfo.MaxSize}");
				}
				try
				{
					global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataOptions options = new global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataOptions
					{
						Timeout = timeout
					};
					await m_LobbyService.UploadMigrationDataAsync(MigrationDataInfo, data, options);
				}
				catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
				{
					throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
				}
				catch (global::System.Exception ex)
				{
					throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
				}
			}
		}

		private void RegisterPlayerEvents()
		{
			if (m_PlayerId != null)
			{
				m_PlayerId.PlayerIdChanged -= OnPlayerIdChanged;
				m_PlayerId.PlayerIdChanged += OnPlayerIdChanged;
			}
			if (m_AccessTokenObserver != null)
			{
				m_AccessTokenObserver.AccessTokenChanged -= OnAccessTokenChanged;
				m_AccessTokenObserver.AccessTokenChanged += OnAccessTokenChanged;
			}
		}

		private void UnregisterPlayerEvents()
		{
			if (m_PlayerId != null)
			{
				m_PlayerId.PlayerIdChanged -= OnPlayerIdChanged;
			}
			if (m_AccessTokenObserver != null)
			{
				m_AccessTokenObserver.AccessTokenChanged -= OnAccessTokenChanged;
			}
		}

		internal bool PollForLobbyEvents(LobbyEventCallbacks callbacks)
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			m_Events = m_LobbyService.SetCacherLobbyCallbacks(Lobby.Id, callbacks);
			if (m_Events == null)
			{
				return false;
			}
			SchedulePolling(0);
			return true;
		}

		internal void SchedulePolling(int seconds)
		{
			if (!LobbyEventTokenSource.IsCancellationRequested && !m_PollingActionId.HasValue)
			{
				m_PollingActionId = m_ActionScheduler.ScheduleAction(RunScheduledPolling, seconds);
			}
			if (LobbyEventTokenSource.IsCancellationRequested)
			{
				CancelPolling();
			}
		}

		internal void CancelPolling()
		{
			if (m_PollingActionId.HasValue)
			{
				m_ActionScheduler.CancelAction(m_PollingActionId.Value);
				m_PollingActionId = null;
			}
		}

		internal async void RunScheduledPolling()
		{
			m_PollingActionId = null;
			try
			{
				await RefreshLobbyAsync();
			}
			catch (global::Unity.Services.Multiplayer.SessionException ex)
			{
				if (ex.Error == global::Unity.Services.Multiplayer.SessionError.NotInLobby)
				{
					global::Unity.Services.Multiplayer.Logger.LogWarning("No longer in a lobby. Cancelling polling loop.");
					return;
				}
			}
			SchedulePolling(1);
		}

		public async global::System.Threading.Tasks.Task SubscribeToLobbyEventsAsync(LobbyEventCallbacks callbacks)
		{
			ValidateAuthorization();
			ValidateInActiveLobby();
			try
			{
				m_Events = await m_LobbyService.SubscribeToLobbyEventsAsync(Lobby.Id, callbacks);
				if (m_Events != null)
				{
					global::Unity.Services.Lobbies.Models.Lobby lobby = await m_LobbyService.GetLobbyAsync(Lobby.Id, Lobby.Version.ToString());
					if (lobby != null)
					{
						Lobby = lobby;
					}
				}
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException ex)
			{
				switch (ex.Reason)
				{
				case global::Unity.Services.Lobbies.LobbyExceptionReason.AlreadySubscribedToLobby:
					global::Unity.Services.Multiplayer.Logger.LogWarning("Already subscribed to lobby[" + Lobby.Id + "]. We did not need to try and subscribe again. Exception Message: " + ex.Message);
					break;
				case global::Unity.Services.Lobbies.LobbyExceptionReason.SubscriptionToLobbyLostWhileBusy:
					global::Unity.Services.Multiplayer.Logger.LogError("Subscription to lobby events was lost while it was busy trying to subscribe. Exception Message: " + ex.Message);
					throw;
				case global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyEventServiceConnectionError:
					global::Unity.Services.Multiplayer.Logger.LogError("Failed to connect to lobby events. Exception Message: " + ex.Message);
					throw;
				default:
					throw;
				}
			}
		}

		public async global::System.Threading.Tasks.Task ResetAsync()
		{
			if (State != global::Unity.Services.Multiplayer.LobbyState.None)
			{
				CancelHeartbeat();
				await LobbyUnsubscribeCallbacksAsync();
				m_Events = null;
				Lobby = null;
				UnregisterPlayerEvents();
				State = global::Unity.Services.Multiplayer.LobbyState.None;
				this.LobbyExit?.Invoke();
				this.LobbyChanged?.Invoke();
			}
		}

		private void ValidateAuthorization()
		{
			if (!IsAuthorized)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Player is not authorized.", global::Unity.Services.Multiplayer.SessionError.NotAuthorized);
			}
		}

		private void ValidateNoActiveLobby()
		{
			if (Lobby != null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Player is already in a lobby", global::Unity.Services.Multiplayer.SessionError.LobbyAlreadyExists);
			}
		}

		private void ValidateInActiveLobby()
		{
			if (Lobby == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Player is not part of an active lobby.", global::Unity.Services.Multiplayer.SessionError.NotInLobby);
			}
		}

		private async void OnPlayerIdChanged(string obj)
		{
			await ResetAsync();
		}

		private async void OnAccessTokenChanged(string accessToken)
		{
			if (accessToken == null)
			{
				await ResetAsync();
			}
		}

		private void ScheduleHeartbeat()
		{
			if (global::UnityEngine.Application.isPlaying)
			{
				m_HeartbeatActionId = m_ActionScheduler.ScheduleAction(RunScheduledHeartbeat, Settings.HeartbeatSeconds);
			}
		}

		private void CancelHeartbeat()
		{
			if (m_HeartbeatActionId.HasValue)
			{
				m_ActionScheduler.CancelAction(m_HeartbeatActionId.Value);
				m_HeartbeatActionId = null;
			}
		}

		private async void RunScheduledHeartbeat()
		{
			if (global::UnityEngine.Application.isPlaying)
			{
				try
				{
					m_HeartbeatActionId = null;
					await SendHeartbeatAsync();
				}
				catch (global::System.Exception)
				{
				}
				ScheduleHeartbeat();
			}
		}

		private async global::System.Threading.Tasks.Task SendHeartbeatAsync()
		{
			await m_LobbyService.SendHeartbeatPingAsync(Lobby.Id);
		}
	}
}
