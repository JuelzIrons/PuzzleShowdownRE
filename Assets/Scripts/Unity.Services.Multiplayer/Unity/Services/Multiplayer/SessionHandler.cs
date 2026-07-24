namespace Unity.Services.Multiplayer
{
	internal class SessionHandler : global::Unity.Services.Multiplayer.IServerSession, global::Unity.Services.Multiplayer.IHostSession, global::Unity.Services.Multiplayer.ISession
	{
		public readonly global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.SessionProperty> Properties = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.SessionProperty>();

		public readonly global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.Player> Players = new global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.Player>();

		internal bool Modified;

		internal readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler ActionScheduler;

		internal readonly global::Unity.Services.Multiplayer.ILobbyHandler LobbyHandler;

		internal readonly global::Unity.Services.Authentication.Internal.IPlayerId PlayerId;

		internal readonly global::Unity.Services.Authentication.Internal.IAccessTokenObserver AccessTokenObserver;

		internal readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Services.Multiplayer.IModule> Modules = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Services.Multiplayer.IModule>();

		private bool _isHost;

		private string _name;

		private bool _isPrivate;

		private bool _isLocked;

		private string _password;

		private string _host;

		public global::Unity.Services.Multiplayer.SessionState State { get; internal set; }

		public bool IsHost { get; set; }

		public bool IsMember => LobbyHandler.IsMember;

		public bool IsServer => LobbyHandler.IsServer;

		public bool IsPrivate
		{
			get
			{
				return _isPrivate;
			}
			set
			{
				if (_isPrivate != value)
				{
					_isPrivate = value;
					Modified = true;
				}
			}
		}

		public bool IsLocked
		{
			get
			{
				return _isLocked;
			}
			set
			{
				if (_isLocked != value)
				{
					_isLocked = value;
					Modified = true;
				}
			}
		}

		public string Type { get; set; }

		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (!(_name == value))
				{
					_name = value;
					Modified = true;
				}
			}
		}

		public string Id { get; set; }

		public string Code { get; set; }

		public string Host
		{
			get
			{
				return _host;
			}
			set
			{
				if (!(_host == value))
				{
					_host = value;
					Modified = true;
				}
			}
		}

		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				if (!(_password == value))
				{
					_password = value;
					Modified = true;
				}
			}
		}

		public bool HasPassword { get; private set; }

		public int AvailableSlots { get; private set; }

		public int MaxPlayers { get; set; }

		public int PlayerCount => Players.Count;

		global::System.Collections.Generic.IReadOnlyList<global::Unity.Services.Multiplayer.IPlayer> global::Unity.Services.Multiplayer.IHostSession.Players => Players;

		global::System.Collections.Generic.IReadOnlyList<global::Unity.Services.Multiplayer.IReadOnlyPlayer> global::Unity.Services.Multiplayer.ISession.Players => Players;

		global::Unity.Services.Multiplayer.IPlayer global::Unity.Services.Multiplayer.ISession.CurrentPlayer => CurrentPlayer;

		global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.SessionProperty> global::Unity.Services.Multiplayer.ISession.Properties => Properties;

		internal global::Unity.Services.Lobbies.Models.Lobby Lobby => LobbyHandler.Lobby;

		internal global::Unity.Services.Multiplayer.Player CurrentPlayer => GetPlayer(PlayerId?.PlayerId);

		public bool ConcurrencyControlEnabled
		{
			get
			{
				return LobbyHandler.ConcurrencyControlEnabled;
			}
			set
			{
				LobbyHandler.ConcurrencyControlEnabled = value;
			}
		}

		global::Unity.Services.Multiplayer.IHostSessionNetwork global::Unity.Services.Multiplayer.IHostSession.Network => NetworkModule.HostNetwork;

		global::Unity.Services.Multiplayer.IClientSessionNetwork global::Unity.Services.Multiplayer.ISession.Network => NetworkModule.ClientNetwork;

		global::Unity.Services.Multiplayer.NetworkModule global::Unity.Services.Multiplayer.ISession.NetworkModule => NetworkModule;

		private global::Unity.Services.Multiplayer.NetworkModule NetworkModule => GetModule<global::Unity.Services.Multiplayer.NetworkModule>();

		public event global::System.Action Changed;

		public event global::System.Action<global::Unity.Services.Multiplayer.SessionState> StateChanged;

		public event global::System.Action<string> PlayerJoined;

		public event global::System.Action<string> PlayerLeft;

		public event global::System.Action<string> PlayerLeaving;

		public event global::System.Action<string> PlayerHasLeft;

		public event global::System.Action SessionPropertiesChanged;

		public event global::System.Action PlayerPropertiesChanged;

		public event global::System.Action Deleted;

		public event global::System.Action RemovedFromSession;

		public event global::System.Action<string> SessionHostChanged;

		public event global::System.Action SessionMigrated;

		public void RegisterModule<T>(T module) where T : class, global::Unity.Services.Multiplayer.IModule
		{
			RegisterModule(typeof(T), module);
		}

		public void RegisterModule(global::System.Type type, global::Unity.Services.Multiplayer.IModule module)
		{
			if (Modules.ContainsKey(type))
			{
				global::Unity.Services.Multiplayer.Logger.LogError($"Module of type '{type}' already registered");
			}
			else
			{
				Modules.Add(type, module);
			}
		}

		T global::Unity.Services.Multiplayer.ISession.GetModule<T>()
		{
			return GetModule<T>();
		}

		public T GetModule<T>() where T : class, global::Unity.Services.Multiplayer.IModule
		{
			global::System.Type typeFromHandle = typeof(T);
			if (Modules.TryGetValue(typeFromHandle, out var value))
			{
				return value as T;
			}
			return null;
		}

		public SessionHandler(string type, global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler, global::Unity.Services.Authentication.Internal.IAccessTokenObserver accessTokenObserver, global::Unity.Services.Authentication.Internal.IPlayerId playerId)
		{
			Type = type;
			ActionScheduler = actionScheduler;
			LobbyHandler = lobbyHandler;
			PlayerId = playerId;
			AccessTokenObserver = accessTokenObserver;
			if (AccessTokenObserver != null)
			{
				AccessTokenObserver.AccessTokenChanged += OnAuthenticationTokenChanged;
			}
			if (Lobby != null)
			{
				UpdateDerivedProperties();
				Modified = false;
			}
			SetState(global::Unity.Services.Multiplayer.SessionState.Connected);
			LobbyHandler.LobbyHostChanged += OnLobbyHostChanged;
			LobbyHandler.LobbyChanged += OnLobbyChanged;
			LobbyHandler.PlayerJoined += OnPlayerJoined;
			LobbyHandler.PlayerLeaving += OnPlayerLeaving;
			LobbyHandler.PlayerHasLeft += OnPlayerHasLeft;
			LobbyHandler.DataChanged += OnSessionPropertiesChanged;
			LobbyHandler.PlayerDataChanged += OnPlayerPropertiesChanged;
			LobbyHandler.KickedFromLobby += OnRemovedFromSession;
			LobbyHandler.LobbyDeleted += OnDeleted;
			LobbyHandler.LobbyExit += OnLobbyExit;
			global::UnityEngine.Application.quitting += OnQuitting;
		}

		public async global::System.Threading.Tasks.Task InitializeModulesAsync()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::Unity.Services.Multiplayer.IModule> module in Modules)
			{
				await module.Value.InitializeAsync();
			}
		}

		public async global::System.Threading.Tasks.Task LeaveModulesAsync()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::Unity.Services.Multiplayer.IModule> module in Modules)
			{
				try
				{
					await module.Value.LeaveAsync();
				}
				catch (global::System.Exception ex)
				{
					global::Unity.Services.Multiplayer.Logger.LogError("LeaveModule " + module.Key.Name + " failed: " + ex.Message);
				}
			}
		}

		public async global::System.Threading.Tasks.Task LeaveAsync()
		{
			if (IsServer)
			{
				await DeleteAsync();
				return;
			}
			if (!LobbyHandler.IsMember)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Only session members can leave a session", global::Unity.Services.Multiplayer.SessionError.InvalidOperation);
			}
			if (State != global::Unity.Services.Multiplayer.SessionState.Deleted)
			{
				LobbyHandler.LobbyHostChanged -= OnLobbyHostChanged;
				LobbyHandler.LobbyChanged -= OnLobbyChanged;
				LobbyHandler.PlayerJoined -= OnPlayerJoined;
				LobbyHandler.PlayerLeaving -= OnPlayerLeaving;
				LobbyHandler.PlayerHasLeft -= OnPlayerHasLeft;
				LobbyHandler.DataChanged -= OnSessionPropertiesChanged;
				LobbyHandler.PlayerDataChanged -= OnPlayerPropertiesChanged;
				LobbyHandler.KickedFromLobby -= OnRemovedFromSession;
				LobbyHandler.LobbyDeleted -= OnDeleted;
				LobbyHandler.LobbyExit -= OnLobbyExit;
				global::UnityEngine.Application.quitting -= OnQuitting;
				await LeaveModulesAsync();
				await LobbyHandler.RemovePlayerAsync(PlayerId.PlayerId);
				await LobbyHandler.ResetAsync();
				SetState(global::Unity.Services.Multiplayer.SessionState.Disconnected);
				this.RemovedFromSession?.Invoke();
			}
		}

		internal async global::System.Threading.Tasks.Task CleanupAsync()
		{
			if (!LobbyHandler.IsMember)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Only session members can leave a session", global::Unity.Services.Multiplayer.SessionError.InvalidOperation);
			}
			if (State != global::Unity.Services.Multiplayer.SessionState.Deleted)
			{
				await LeaveModulesAsync();
				await LobbyHandler.ResetAsync();
				SetState(global::Unity.Services.Multiplayer.SessionState.Disconnected);
			}
		}

		public global::System.Threading.Tasks.Task RefreshAsync()
		{
			if (State == global::Unity.Services.Multiplayer.SessionState.Deleted)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("cannot refresh a deleted session", global::Unity.Services.Multiplayer.SessionError.SessionDeleted);
			}
			return LobbyHandler.RefreshLobbyAsync();
		}

		public async global::System.Threading.Tasks.Task ReconnectAsync()
		{
			if (State != global::Unity.Services.Multiplayer.SessionState.Connected)
			{
				if (State == global::Unity.Services.Multiplayer.SessionState.Deleted)
				{
					throw new global::Unity.Services.Multiplayer.SessionException("cannot reconnect to a deleted session", global::Unity.Services.Multiplayer.SessionError.SessionDeleted);
				}
				await LobbyHandler.ReconnectToLobbyAsync();
				SetState(global::Unity.Services.Multiplayer.SessionState.Connected);
			}
		}

		public global::Unity.Services.Multiplayer.IHostSession AsHost()
		{
			if (!IsHost)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Only the host can perform this operation.", global::Unity.Services.Multiplayer.SessionError.Forbidden);
			}
			return this;
		}

		public global::Unity.Services.Multiplayer.IServerSession AsServer()
		{
			if (!IsServer)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Only a server host can perform this operation.", global::Unity.Services.Multiplayer.SessionError.Forbidden);
			}
			return this;
		}

		public async global::System.Threading.Tasks.Task DeleteAsync()
		{
			if (State != global::Unity.Services.Multiplayer.SessionState.Deleted)
			{
				await LeaveModulesAsync();
				SetState(global::Unity.Services.Multiplayer.SessionState.Disconnected);
				await LobbyHandler.DeleteLobbyAsync();
				SetState(global::Unity.Services.Multiplayer.SessionState.Deleted);
				this.Deleted?.Invoke();
			}
		}

		private void SetState(global::Unity.Services.Multiplayer.SessionState state)
		{
			if (State != state)
			{
				State = state;
				this.StateChanged?.Invoke(State);
				this.Changed?.Invoke();
			}
		}

		public global::System.Threading.Tasks.Task SavePlayerDataAsync(string playerId)
		{
			global::Unity.Services.Multiplayer.Player player = global::System.Linq.Enumerable.FirstOrDefault(Players, (global::Unity.Services.Multiplayer.Player p) => p.Id == playerId);
			if (player == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Cannot save player data. Player not found.", global::Unity.Services.Multiplayer.SessionError.InvalidOperation);
			}
			return SavePlayerDataAsync(player);
		}

		public global::System.Threading.Tasks.Task SaveCurrentPlayerDataAsync()
		{
			if (CurrentPlayer != null)
			{
				return SavePlayerDataAsync(CurrentPlayer);
			}
			global::Unity.Services.Multiplayer.Logger.LogWarning("Cannot save current player whilst not a player in the session");
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		private async global::System.Threading.Tasks.Task SavePlayerDataAsync(global::Unity.Services.Multiplayer.Player player)
		{
			if (!player.Modified)
			{
				return;
			}
			global::Unity.Services.Lobbies.UpdatePlayerOptions updatePlayerOptions = new global::Unity.Services.Lobbies.UpdatePlayerOptions
			{
				Data = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject>()
			};
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.PlayerProperty> property in player.Properties)
			{
				global::Unity.Services.Multiplayer.PlayerProperty value = property.Value;
				global::Unity.Services.Lobbies.Models.PlayerDataObject value2 = ((value != null) ? new global::Unity.Services.Lobbies.Models.PlayerDataObject((global::Unity.Services.Lobbies.Models.PlayerDataObject.VisibilityOptions)value.Visibility, value.Value) : null);
				updatePlayerOptions.Data.Add(property.Key, value2);
			}
			await LobbyHandler.UpdatePlayerAsync(player.Id, updatePlayerOptions);
			player.Modified = false;
		}

		public global::System.Threading.Tasks.Task RemovePlayerAsync(string playerId)
		{
			if (string.IsNullOrEmpty(playerId))
			{
				throw new global::System.ArgumentException("PlayerId cannot be null or empty", "playerId");
			}
			return LobbyHandler.RemovePlayerAsync(playerId);
		}

		global::Unity.Services.Multiplayer.IPlayer global::Unity.Services.Multiplayer.IHostSession.GetPlayer(string playerId)
		{
			return GetPlayer(playerId);
		}

		global::Unity.Services.Multiplayer.IReadOnlyPlayer global::Unity.Services.Multiplayer.ISession.GetPlayer(string playerId)
		{
			return GetPlayer(playerId);
		}

		public global::Unity.Services.Multiplayer.Player GetPlayer(string playerId)
		{
			return Players.Find((global::Unity.Services.Multiplayer.Player player) => player.Id == playerId);
		}

		public bool HasPlayer(string playerId)
		{
			return Players.Exists((global::Unity.Services.Multiplayer.Player player) => player.Id == playerId);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionMigrationData> GetHostMigrationDataAsync(global::System.TimeSpan timeout)
		{
			return new global::Unity.Services.Multiplayer.SessionMigrationData(await LobbyHandler.DownloadMigrationDataAsync(timeout));
		}

		public global::System.Threading.Tasks.Task SetHostMigrationDataAsync(byte[] data, global::System.TimeSpan timeout)
		{
			return LobbyHandler.UploadMigrationDataAsync(data, timeout);
		}

		public void SetProperties(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.SessionProperty> properties)
		{
			if (!IsHost || properties == null || properties.Count == 0)
			{
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.SessionProperty> property in properties)
			{
				Properties[property.Key] = property.Value;
			}
			Modified = true;
		}

		public void SetProperty(string key, global::Unity.Services.Multiplayer.SessionProperty property)
		{
			Properties[key] = property;
			Modified = true;
		}

		public async global::System.Threading.Tasks.Task SavePropertiesAsync()
		{
			if (IsHost && Modified)
			{
				global::Unity.Services.Lobbies.UpdateLobbyOptions updateLobbyOptions = new global::Unity.Services.Lobbies.UpdateLobbyOptions
				{
					Name = Name,
					IsPrivate = IsPrivate,
					IsLocked = IsLocked,
					Password = Password,
					Data = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject>()
				};
				global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.SessionProperty> properties = Properties;
				if (properties == null || properties.Count != 0)
				{
					global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> data = SessionToLobbyProperties();
					updateLobbyOptions.Data = data;
				}
				if (Lobby?.HostId != Host)
				{
					updateLobbyOptions.HostId = Host;
				}
				await LobbyHandler.UpdateLobbyAsync(updateLobbyOptions);
				Modified = false;
			}
		}

		private void UpdateDerivedProperties()
		{
			Id = Lobby.Id;
			Name = Lobby.Name;
			IsHost = LobbyHandler.IsHost;
			Code = Lobby.LobbyCode;
			Host = Lobby.HostId;
			MaxPlayers = Lobby.MaxPlayers;
			IsLocked = Lobby.IsLocked;
			IsPrivate = Lobby.IsPrivate;
			AvailableSlots = Lobby.AvailableSlots;
			HasPassword = Lobby.HasPassword;
			Players?.RemoveAll((global::Unity.Services.Multiplayer.Player player) => global::System.Linq.Enumerable.All(Lobby.Players, (global::Unity.Services.Lobbies.Models.Player lobbyPlayer) => lobbyPlayer?.Id != player?.Id));
			global::System.Collections.Generic.IEnumerable<global::Unity.Services.Lobbies.Models.Player> enumerable = Lobby?.Players;
			foreach (global::Unity.Services.Lobbies.Models.Player item in enumerable ?? global::System.Linq.Enumerable.Empty<global::Unity.Services.Lobbies.Models.Player>())
			{
				if (item == null)
				{
					continue;
				}
				global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> dictionary = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty>();
				global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Lobbies.Models.PlayerDataObject>> enumerable2 = item?.Data;
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Lobbies.Models.PlayerDataObject> item2 in enumerable2 ?? global::System.Linq.Enumerable.Empty<global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Lobbies.Models.PlayerDataObject>>())
				{
					dictionary.Add(item2.Key, new global::Unity.Services.Multiplayer.PlayerProperty(item2.Value.Value, (global::Unity.Services.Multiplayer.VisibilityPropertyOptions)item2.Value.Visibility));
				}
				global::Unity.Services.Multiplayer.Player convertedPlayer = new global::Unity.Services.Multiplayer.Player(this, item?.Id, item?.ConnectionInfo, dictionary, item?.AllocationId, item?.Joined ?? default(global::System.DateTime), item?.LastUpdated ?? default(global::System.DateTime));
				int num = Players.FindIndex((global::Unity.Services.Multiplayer.Player p) => p.Id == convertedPlayer.Id);
				if (num != -1)
				{
					Players[num] = convertedPlayer;
				}
				else
				{
					Players.Add(convertedPlayer);
				}
			}
			if (Properties != null && Lobby?.Data != null)
			{
				for (int num2 = Properties.Count - 1; num2 >= 0; num2--)
				{
					string key = global::System.Linq.Enumerable.ElementAt(Properties.Keys, num2);
					if (!Lobby.Data.ContainsKey(key))
					{
						Properties.Remove(key);
					}
				}
			}
			if (Lobby?.Data == null)
			{
				return;
			}
			foreach (string key2 in Lobby.Data.Keys)
			{
				if (!Properties.TryGetValue(key2, out var value) || value.Value != Lobby.Data[key2].Value)
				{
					Properties[key2] = new global::Unity.Services.Multiplayer.SessionProperty(Lobby.Data[key2].Value, (global::Unity.Services.Multiplayer.VisibilityPropertyOptions)Lobby.Data[key2].Visibility, (global::Unity.Services.Multiplayer.PropertyIndex)Lobby.Data[key2].Index);
				}
			}
		}

		private void OnLobbyExit()
		{
			LobbyHandler.LobbyChanged -= UpdateDerivedProperties;
		}

		private void OnAuthenticationTokenChanged(string accessToken)
		{
			if (accessToken == null)
			{
				SetState(global::Unity.Services.Multiplayer.SessionState.None);
			}
		}

		private void OnPlayerJoined(string playerId)
		{
			this.PlayerJoined?.Invoke(playerId);
		}

		private void OnPlayerLeaving(string playerId)
		{
			this.PlayerLeaving?.Invoke(playerId);
			this.PlayerLeft?.Invoke(playerId);
		}

		private void OnPlayerHasLeft(string playerId)
		{
			this.PlayerHasLeft?.Invoke(playerId);
		}

		private void OnSessionPropertiesChanged()
		{
			this.SessionPropertiesChanged?.Invoke();
		}

		private void OnPlayerPropertiesChanged()
		{
			this.PlayerPropertiesChanged?.Invoke();
		}

		private void OnDeleted()
		{
			this.Deleted?.Invoke();
		}

		private void OnRemovedFromSession()
		{
			this.RemovedFromSession?.Invoke();
		}

		internal void OnLobbyHostChanged(string newHostId)
		{
			this.SessionHostChanged?.Invoke(newHostId);
		}

		internal void OnLobbyChanged()
		{
			if (Lobby == null)
			{
				LobbyHandler.LobbyChanged -= OnLobbyChanged;
				LobbyHandler.LobbyHostChanged -= OnLobbyHostChanged;
			}
			else
			{
				UpdateDerivedProperties();
				this.Changed?.Invoke();
			}
		}

		private async void OnQuitting()
		{
			if (State != global::Unity.Services.Multiplayer.SessionState.Connected)
			{
				return;
			}
			try
			{
				await LeaveAsync();
			}
			catch (global::System.Exception)
			{
			}
		}

		internal global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> SessionToLobbyProperties()
		{
			return global::System.Linq.Enumerable.ToDictionary(Properties, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.SessionProperty> kvp) => kvp.Key, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.SessionProperty> kvp) => global::Unity.Services.Multiplayer.LobbyConverter.ToSessionDataObject(kvp.Value));
		}

		void global::Unity.Services.Multiplayer.ISession.OnSessionMigrated()
		{
			this.SessionMigrated?.Invoke();
		}
	}
}
