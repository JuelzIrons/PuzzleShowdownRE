namespace Unity.Services.Multiplayer
{
	internal class SessionManager : global::Unity.Services.Multiplayer.ISessionManager
	{
		private readonly global::Unity.Services.Authentication.Internal.IPlayerId m_PlayerId;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerNameComponent m_PlayerName;

		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private readonly global::Unity.Services.Multiplayer.IModuleRegistry m_ModuleRegistry;

		private readonly global::Unity.Services.Multiplayer.ILobbyBuilder m_LobbyBuilder;

		private readonly global::Unity.Services.Authentication.Internal.IAccessTokenObserver m_AccessTokenObserver;

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.ISession> Sessions { get; } = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.ISession>();

		public event global::System.Action<global::Unity.Services.Multiplayer.ISession> SessionAdded;

		public event global::System.Action<global::Unity.Services.Multiplayer.ISession> SessionRemoved;

		public SessionManager(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::Unity.Services.Multiplayer.IModuleRegistry moduleRegistry, global::Unity.Services.Multiplayer.ILobbyBuilder lobbyBuilder, global::Unity.Services.Authentication.Internal.IPlayerId playerId, global::Unity.Services.Authentication.Internal.IPlayerNameComponent playerName, global::Unity.Services.Authentication.Internal.IAccessTokenObserver accessTokenObserver)
		{
			m_ActionScheduler = actionScheduler;
			m_ModuleRegistry = moduleRegistry;
			m_LobbyBuilder = lobbyBuilder;
			m_PlayerId = playerId;
			m_PlayerName = playerName;
			m_AccessTokenObserver = accessTokenObserver;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> GetAsync(string sessionId, global::Unity.Services.Multiplayer.SessionOptions sessionOptions = null)
		{
			global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler = m_LobbyBuilder.Build();
			await lobbyHandler.GetLobbyAsync(sessionId);
			return await SetupSessionAsync(lobbyHandler, sessionOptions ?? new global::Unity.Services.Multiplayer.SessionOptions());
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> CreateAsync(global::Unity.Services.Multiplayer.SessionOptions sessionOptions)
		{
			ValidateOptions(sessionOptions);
			ValidateCreationOptions(sessionOptions);
			PreprocessOptions(sessionOptions);
			global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler = m_LobbyBuilder.Build();
			await lobbyHandler.CreateLobbyAsync(sessionOptions.Name, sessionOptions.MaxPlayers, new global::Unity.Services.Lobbies.CreateLobbyOptions
			{
				IsPrivate = sessionOptions.IsPrivate,
				Password = sessionOptions.Password,
				IsLocked = sessionOptions.IsLocked,
				Data = global::System.Linq.Enumerable.ToDictionary(sessionOptions.SessionProperties?, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.SessionProperty> propertyData) => propertyData.Key, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.SessionProperty> propertyData) => global::Unity.Services.Multiplayer.LobbyConverter.ToSessionDataObject(propertyData.Value)),
				Player = global::Unity.Services.Multiplayer.LobbyConverter.ToLobbyPlayer(m_PlayerId, sessionOptions.PlayerProperties)
			});
			return await SetupSessionAsync(lobbyHandler, sessionOptions, sessionOptions.SessionProperties);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> CreateOrJoinAsync(string sessionId, global::Unity.Services.Multiplayer.SessionOptions sessionOptions)
		{
			ValidateOptions(sessionOptions);
			ValidateCreationOptions(sessionOptions);
			PreprocessOptions(sessionOptions);
			global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler = m_LobbyBuilder.Build();
			await lobbyHandler.CreateOrJoinLobbyAsync(sessionId, sessionOptions.Name, sessionOptions.MaxPlayers, new global::Unity.Services.Lobbies.CreateLobbyOptions
			{
				IsPrivate = sessionOptions.IsPrivate,
				Password = sessionOptions.Password,
				IsLocked = sessionOptions.IsLocked,
				Data = global::System.Linq.Enumerable.ToDictionary(sessionOptions.SessionProperties?, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.SessionProperty> propertyData) => propertyData.Key, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.SessionProperty> propertyData) => global::Unity.Services.Multiplayer.LobbyConverter.ToSessionDataObject(propertyData.Value)),
				Player = global::Unity.Services.Multiplayer.LobbyConverter.ToLobbyPlayer(m_PlayerId, sessionOptions.PlayerProperties)
			});
			return await SetupSessionAsync(lobbyHandler, sessionOptions, sessionOptions.SessionProperties);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> JoinByCodeAsync(string sessionCode, global::Unity.Services.Multiplayer.JoinSessionOptions sessionOptions)
		{
			if (sessionOptions == null)
			{
				sessionOptions = new global::Unity.Services.Multiplayer.JoinSessionOptions();
			}
			ValidateOptions(sessionOptions);
			sessionCode = sessionCode.Trim();
			PreprocessOptions(sessionOptions);
			global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler = m_LobbyBuilder.Build();
			await lobbyHandler.JoinLobbyByCodeAsync(sessionCode, new global::Unity.Services.Lobbies.JoinLobbyByCodeOptions
			{
				Player = global::Unity.Services.Multiplayer.LobbyConverter.ToLobbyPlayer(m_PlayerId, sessionOptions?.PlayerProperties),
				Password = sessionOptions.Password
			});
			return await SetupSessionAsync(lobbyHandler, sessionOptions);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> JoinByIdAsync(string sessionId, global::Unity.Services.Multiplayer.JoinSessionOptions sessionOptions)
		{
			if (sessionOptions == null)
			{
				sessionOptions = new global::Unity.Services.Multiplayer.JoinSessionOptions();
			}
			ValidateOptions(sessionOptions);
			PreprocessOptions(sessionOptions);
			sessionId = sessionId.Trim();
			global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler = m_LobbyBuilder.Build();
			await lobbyHandler.JoinLobbyByIdAsync(sessionId, new global::Unity.Services.Lobbies.JoinLobbyByIdOptions
			{
				Player = global::Unity.Services.Multiplayer.LobbyConverter.ToLobbyPlayer(m_PlayerId, sessionOptions?.PlayerProperties),
				Password = sessionOptions.Password
			});
			return await SetupSessionAsync(lobbyHandler, sessionOptions);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> QuickJoinAsync(global::Unity.Services.Multiplayer.QuickJoinOptions quickJoinOptions, global::Unity.Services.Multiplayer.SessionOptions sessionOptions)
		{
			if (quickJoinOptions == null)
			{
				quickJoinOptions = new global::Unity.Services.Multiplayer.QuickJoinOptions();
			}
			if (sessionOptions == null)
			{
				sessionOptions = new global::Unity.Services.Multiplayer.SessionOptions();
			}
			ValidateOptions(sessionOptions);
			if (quickJoinOptions.CreateSession)
			{
				ValidateCreationOptions(sessionOptions);
			}
			PreprocessOptions(sessionOptions);
			global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler = m_LobbyBuilder.Build();
			global::Unity.Services.Lobbies.QuickJoinLobbyOptions quickJoinLobbyOptions = new global::Unity.Services.Lobbies.QuickJoinLobbyOptions
			{
				Player = global::Unity.Services.Multiplayer.LobbyConverter.ToLobbyPlayer(m_PlayerId, sessionOptions.PlayerProperties),
				Filter = global::Unity.Services.Multiplayer.LobbyConverter.ToQueryFilters(quickJoinOptions.Filters)
			};
			global::System.Diagnostics.Stopwatch stopwatch = new global::System.Diagnostics.Stopwatch();
			stopwatch.Start();
			while (true)
			{
				try
				{
					await lobbyHandler.QuickJoinLobbyAsync(quickJoinLobbyOptions);
					return await SetupSessionAsync(lobbyHandler, sessionOptions);
				}
				catch (global::System.Exception)
				{
					if (!(stopwatch.Elapsed < quickJoinOptions.Timeout))
					{
						if (quickJoinOptions.CreateSession)
						{
							return await CreateAsync(sessionOptions);
						}
						throw;
					}
					await WaitForSeconds(1.0);
				}
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> ReconnectAsync(string sessionId, global::Unity.Services.Multiplayer.ReconnectSessionOptions options = null)
		{
			global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler = m_LobbyBuilder.Build();
			global::Unity.Services.Multiplayer.SessionOptions reconnectOptions = new global::Unity.Services.Multiplayer.SessionOptions();
			if (options != null)
			{
				reconnectOptions.Type = options.Type;
			}
			if (string.IsNullOrWhiteSpace(reconnectOptions.Type))
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Session type is required if ReconnectSessionOptions is provided.", global::Unity.Services.Multiplayer.SessionError.InvalidParameter);
			}
			if (string.IsNullOrWhiteSpace(sessionId))
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Session Id is required.", global::Unity.Services.Multiplayer.SessionError.InvalidParameter);
			}
			await lobbyHandler.ReconnectToLobbyAsync(sessionId);
			if (options?.NetworkHandler != null)
			{
				reconnectOptions.WithNetworkHandler(options.NetworkHandler);
			}
			return await SetupSessionAsync(lobbyHandler, reconnectOptions);
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<string>> GetJoinedSessionIdsAsync()
		{
			return await m_LobbyBuilder.Build().GetJoinedLobbiesAsync();
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> SetupSessionAsync(global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler, global::Unity.Services.Multiplayer.BaseSessionOptions options, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.SessionProperty> sessionProperties = null)
		{
			global::Unity.Services.Multiplayer.SessionHandler session = CreateSessionHandler(lobbyHandler, options);
			if (session.IsHost)
			{
				session.SetProperties(sessionProperties);
			}
			ProcessOptions(session, options);
			await InitializeModulesAsync(session);
			if (session.IsHost)
			{
				await session.SavePropertiesAsync();
			}
			if (session.CurrentPlayer != null)
			{
				await session.SaveCurrentPlayerDataAsync();
			}
			RegisterSession(session, options.Type);
			return session;
		}

		internal void PreprocessOptions(global::Unity.Services.Multiplayer.BaseSessionOptions options)
		{
			if (options.HasOption<global::Unity.Services.Multiplayer.PlayerNameSessionOption>())
			{
				if (string.IsNullOrEmpty(m_PlayerName.PlayerName))
				{
					throw new global::Unity.Services.Multiplayer.SessionException("Trying to set the player name in the session but it is not set.", global::Unity.Services.Multiplayer.SessionError.InvalidPlayerName);
				}
				global::Unity.Services.Multiplayer.PlayerNameSessionOption option = options.GetOption<global::Unity.Services.Multiplayer.PlayerNameSessionOption>();
				if (options.PlayerProperties == null)
				{
					global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> dictionary = (options.PlayerProperties = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty>());
				}
				options.PlayerProperties["_player_name"] = new global::Unity.Services.Multiplayer.PlayerProperty(m_PlayerName.PlayerName, option.Visibility);
			}
		}

		private global::Unity.Services.Multiplayer.SessionHandler CreateSessionHandler(global::Unity.Services.Multiplayer.ILobbyHandler lobbyHandler, global::Unity.Services.Multiplayer.BaseSessionOptions options)
		{
			global::Unity.Services.Multiplayer.SessionHandler session = new global::Unity.Services.Multiplayer.SessionHandler(options.Type, m_ActionScheduler, lobbyHandler, m_AccessTokenObserver, m_PlayerId);
			global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.IModuleProvider> list = m_ModuleRegistry?.ModuleProviders;
			if (list != null)
			{
				foreach (global::Unity.Services.Multiplayer.IModuleProvider item in list)
				{
					global::Unity.Services.Multiplayer.IModule module = item.Build(session);
					session.RegisterModule(item.Type, module);
				}
			}
			session.Deleted += delegate
			{
				OnSessionDeleted(session);
			};
			session.RemovedFromSession += async delegate
			{
				await OnRemovedFromSession(session);
			};
			return session;
		}

		private async global::System.Threading.Tasks.Task InitializeModulesAsync(global::Unity.Services.Multiplayer.SessionHandler session)
		{
			try
			{
				await session.InitializeModulesAsync();
			}
			catch (global::System.Exception)
			{
				await CleanupSessionAsync(session);
				throw;
			}
		}

		private async global::System.Threading.Tasks.Task CleanupSessionAsync(global::Unity.Services.Multiplayer.SessionHandler session, bool cleanupAndLeave = true)
		{
			_ = 1;
			try
			{
				if (cleanupAndLeave)
				{
					await session.LeaveAsync();
				}
				else
				{
					await session.CleanupAsync();
				}
			}
			catch (global::System.Exception)
			{
			}
		}

		private void ValidateOptions(global::Unity.Services.Multiplayer.BaseSessionOptions options)
		{
			if (options == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Missing session options.", global::Unity.Services.Multiplayer.SessionError.InvalidParameter);
			}
			if (Sessions.ContainsKey(options.Type))
			{
				throw new global::Unity.Services.Multiplayer.SessionException("A session for type: " + options.Type + " is already registered.", global::Unity.Services.Multiplayer.SessionError.SessionTypeAlreadyExists);
			}
		}

		private void ValidateCreationOptions(global::Unity.Services.Multiplayer.SessionOptions options)
		{
			if (options == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Session options are required.", global::Unity.Services.Multiplayer.SessionError.InvalidParameter);
			}
			if (options.MaxPlayers <= 0)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("A valid MaxPlayers is required for session create.", global::Unity.Services.Multiplayer.SessionError.InvalidParameter);
			}
		}

		private void ProcessOptions(global::Unity.Services.Multiplayer.SessionHandler session, global::Unity.Services.Multiplayer.BaseSessionOptions sessionOptions)
		{
			if (sessionOptions == null)
			{
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::Unity.Services.Multiplayer.IModuleOption> option in sessionOptions.Options)
			{
				try
				{
					option.Value.Process(session);
				}
				catch (global::System.Exception ex)
				{
					global::Unity.Services.Multiplayer.Logger.LogError("Process Options Exception: " + ex.Message);
				}
			}
		}

		private void RegisterSession(global::Unity.Services.Multiplayer.ISession session, string sessionType)
		{
			Sessions[sessionType] = session;
			m_ActionScheduler.ScheduleAction(delegate
			{
				if (session.State != global::Unity.Services.Multiplayer.SessionState.Deleted)
				{
					session.RefreshAsync();
				}
			}, 2.0);
			m_ActionScheduler.ScheduleAction(delegate
			{
				this.SessionAdded?.Invoke(session);
			});
		}

		private async global::System.Threading.Tasks.Task WaitForSeconds(double seconds)
		{
			global::System.Threading.Tasks.TaskCompletionSource<object> tcs = new global::System.Threading.Tasks.TaskCompletionSource<object>();
			m_ActionScheduler.ScheduleAction(delegate
			{
				tcs.SetResult(null);
			}, seconds);
			await tcs.Task;
		}

		private void OnSessionDeleted(global::Unity.Services.Multiplayer.ISession session)
		{
			Sessions.Remove(session.Type);
			this.SessionRemoved?.Invoke(session);
		}

		private async global::System.Threading.Tasks.Task OnRemovedFromSession(global::Unity.Services.Multiplayer.ISession session)
		{
			await CleanupSessionAsync((global::Unity.Services.Multiplayer.SessionHandler)session, cleanupAndLeave: false);
			Sessions.Remove(session.Type);
			this.SessionRemoved?.Invoke(session);
		}
	}
}
