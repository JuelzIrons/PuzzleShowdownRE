namespace Unity.Multiplayer.Widgets
{
	[global::UnityEngine.DefaultExecutionOrder(-100)]
	internal class SessionManager : global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.SessionManager>
	{
		private bool m_Initialized;

		private global::Unity.Services.Multiplayer.QuerySessionsResults m_SessionQueryResults;

		private global::Unity.Multiplayer.Widgets.WidgetEventDispatcher m_WidgetEventDispatcher;

		private global::Unity.Services.Multiplayer.ISession m_ActiveSession;

		internal global::Unity.Services.Multiplayer.ISession ActiveSession
		{
			get
			{
				return m_ActiveSession;
			}
			private set
			{
				if (value != null)
				{
					m_ActiveSession = value;
					RegisterSessionEvents();
					global::UnityEngine.Debug.Log("Joined Session " + m_ActiveSession.Id);
					m_WidgetEventDispatcher.OnSessionJoined(m_ActiveSession, EnterSessionData.WidgetConfiguration);
				}
				else if (m_ActiveSession != null)
				{
					m_ActiveSession = null;
					m_WidgetEventDispatcher.OnSessionLeft();
				}
			}
		}

		private global::Unity.Multiplayer.Widgets.EnterSessionData EnterSessionData { get; set; }

		private async void Awake()
		{
			if (!m_Initialized)
			{
				global::Unity.Multiplayer.Widgets.WidgetDependencies instance = global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance;
				global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.WidgetEventDispatcher>.Instance.OnServicesInitializedEvent.AddListener(OnServicesInitialized);
				await instance.ServiceInitialization.InitializeAsync();
			}
		}

		private void Start()
		{
			m_WidgetEventDispatcher = global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.WidgetEventDispatcher>.Instance;
		}

		private void OnServicesInitialized()
		{
			m_Initialized = true;
		}

		internal async global::System.Threading.Tasks.Task EnterSession(global::Unity.Multiplayer.Widgets.EnterSessionData enterSessionData)
		{
			_ = 6;
			try
			{
				if (!m_Initialized)
				{
					throw new global::System.InvalidOperationException("Services are not initialized. If you manually initialize Services please call ServicesInitialized afterwards.");
				}
				EnterSessionData = enterSessionData;
				if (m_ActiveSession != null)
				{
					await LeaveSession();
				}
				global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> playerProperties = await GetPlayerProperties();
				global::UnityEngine.Debug.Log("Joining Session...");
				m_WidgetEventDispatcher.OnSessionJoining();
				global::Unity.Services.Multiplayer.JoinSessionOptions joinSessionOptions = new global::Unity.Services.Multiplayer.JoinSessionOptions
				{
					PlayerProperties = playerProperties
				};
				global::Unity.Services.Multiplayer.SessionOptions options = new global::Unity.Services.Multiplayer.SessionOptions
				{
					MaxPlayers = enterSessionData.WidgetConfiguration.MaxPlayers,
					IsLocked = false,
					IsPrivate = false,
					PlayerProperties = playerProperties,
					Name = ((enterSessionData.SessionAction == global::Unity.Multiplayer.Widgets.SessionAction.Create) ? enterSessionData.SessionName : global::System.Guid.NewGuid().ToString())
				};
				SetConnection(ref options, enterSessionData.WidgetConfiguration);
				if (enterSessionData.WidgetConfiguration.ConnectionType != global::Unity.Multiplayer.Widgets.ConnectionType.None && enterSessionData.WidgetConfiguration.NetworkHandler != null)
				{
					global::Unity.Services.Multiplayer.SessionOptionsExtensions.WithNetworkHandler(options, enterSessionData.WidgetConfiguration.NetworkHandler);
					global::Unity.Services.Multiplayer.SessionOptionsExtensions.WithNetworkHandler(joinSessionOptions, enterSessionData.WidgetConfiguration.NetworkHandler);
				}
				switch (enterSessionData.SessionAction)
				{
				case global::Unity.Multiplayer.Widgets.SessionAction.Create:
					ActiveSession = await global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance.MultiplayerService.CreateSessionAsync(options);
					break;
				case global::Unity.Multiplayer.Widgets.SessionAction.StartMatchmaking:
					ActiveSession = await global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance.MultiplayerService.MatchmakeSessionAsync(enterSessionData.AdditionalOptions.MatchmakerOptions, options);
					break;
				case global::Unity.Multiplayer.Widgets.SessionAction.QuickJoin:
				{
					global::Unity.Services.Multiplayer.QuickJoinOptions quickJoinOptions = new global::Unity.Services.Multiplayer.QuickJoinOptions
					{
						CreateSession = enterSessionData.AdditionalOptions.AutoCreateSession
					};
					ActiveSession = await global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance.MultiplayerService.MatchmakeSessionAsync(quickJoinOptions, options);
					break;
				}
				case global::Unity.Multiplayer.Widgets.SessionAction.JoinByCode:
					ActiveSession = await global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance.MultiplayerService.JoinSessionByCodeAsync(enterSessionData.JoinCode, joinSessionOptions);
					break;
				case global::Unity.Multiplayer.Widgets.SessionAction.JoinById:
					ActiveSession = await global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance.MultiplayerService.JoinSessionByIdAsync(enterSessionData.Id, joinSessionOptions);
					break;
				default:
					throw new global::System.ArgumentOutOfRangeException();
				}
			}
			catch (global::Unity.Services.Multiplayer.SessionException sessionException)
			{
				HandleSessionException(sessionException);
			}
			catch (global::System.AggregateException ex)
			{
				ex.Handle(delegate(global::System.Exception ex2)
				{
					if (ex2 is global::Unity.Services.Multiplayer.SessionException sessionException2)
					{
						HandleSessionException(sessionException2);
						return true;
					}
					return false;
				});
			}
		}

		private void HandleSessionException(global::Unity.Services.Multiplayer.SessionException sessionException)
		{
			global::UnityEngine.Debug.LogException(sessionException);
			m_WidgetEventDispatcher.OnSessionFailedToJoin(sessionException);
			ActiveSession = null;
		}

		private async global::System.Threading.Tasks.Task<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty>> GetPlayerProperties()
		{
			global::Unity.Services.Multiplayer.PlayerProperty value = new global::Unity.Services.Multiplayer.PlayerProperty(await global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance.AuthenticationService.GetPlayerNameAsync(), global::Unity.Services.Multiplayer.VisibilityPropertyOptions.Member);
			return new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> { { "w_PlayerName", value } };
		}

		private static void SetConnection(ref global::Unity.Services.Multiplayer.SessionOptions options, global::Unity.Multiplayer.Widgets.WidgetConfiguration config)
		{
			switch (config.ConnectionType)
			{
			case global::Unity.Multiplayer.Widgets.ConnectionType.Direct:
				global::Unity.Services.Multiplayer.SessionOptionsExtensions.WithDirectNetwork(options, config.ListenIpAddress, (config.ConnectionMode == global::Unity.Multiplayer.Widgets.ConnectionMode.Listen) ? config.ListenIpAddress : config.PublishIpAddress, config.Port);
				break;
			case global::Unity.Multiplayer.Widgets.ConnectionType.DistributedAuthority:
				global::Unity.Services.Multiplayer.SessionOptionsExtensions.WithDistributedAuthorityNetwork(options);
				break;
			default:
				global::Unity.Services.Multiplayer.SessionOptionsExtensions.WithRelayNetwork(options);
				break;
			case global::Unity.Multiplayer.Widgets.ConnectionType.None:
				break;
			}
		}

		internal async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Multiplayer.ISessionInfo>> QuerySessions()
		{
			global::Unity.Services.Multiplayer.QuerySessionsOptions queryOptions = new global::Unity.Services.Multiplayer.QuerySessionsOptions();
			m_SessionQueryResults = await global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance.MultiplayerService.QuerySessionsAsync(queryOptions);
			return m_SessionQueryResults.Sessions;
		}

		internal async global::System.Threading.Tasks.Task LeaveSession()
		{
			if (ActiveSession == null)
			{
				return;
			}
			UnregisterPlayerEvents();
			try
			{
				await ActiveSession.LeaveAsync();
			}
			catch
			{
			}
			finally
			{
				ActiveSession = null;
			}
		}

		internal async void KickPlayer(string playerId)
		{
			if (ActiveSession.IsHost)
			{
				await ActiveSession.AsHost().RemovePlayerAsync(playerId);
			}
		}

		private void RegisterSessionEvents()
		{
			ActiveSession.Changed += m_WidgetEventDispatcher.OnSessionChanged;
			ActiveSession.StateChanged += m_WidgetEventDispatcher.OnSessionStateChanged;
			ActiveSession.PlayerJoined += m_WidgetEventDispatcher.OnPlayerJoinedSession;
			ActiveSession.PlayerLeaving += m_WidgetEventDispatcher.OnPlayerLeftSession;
			ActiveSession.SessionPropertiesChanged += m_WidgetEventDispatcher.OnSessionPropertiesChanged;
			ActiveSession.PlayerPropertiesChanged += m_WidgetEventDispatcher.OnPlayerPropertiesChanged;
			ActiveSession.RemovedFromSession += m_WidgetEventDispatcher.OnRemovedFromSession;
			ActiveSession.Deleted += m_WidgetEventDispatcher.OnSessionDeleted;
			ActiveSession.RemovedFromSession += OnRemovedFromSession;
		}

		private void UnregisterPlayerEvents()
		{
			ActiveSession.Changed -= m_WidgetEventDispatcher.OnSessionChanged;
			ActiveSession.StateChanged -= m_WidgetEventDispatcher.OnSessionStateChanged;
			ActiveSession.PlayerJoined -= m_WidgetEventDispatcher.OnPlayerJoinedSession;
			ActiveSession.PlayerLeaving -= m_WidgetEventDispatcher.OnPlayerLeftSession;
			ActiveSession.SessionPropertiesChanged -= m_WidgetEventDispatcher.OnSessionPropertiesChanged;
			ActiveSession.PlayerPropertiesChanged -= m_WidgetEventDispatcher.OnPlayerPropertiesChanged;
			ActiveSession.RemovedFromSession -= m_WidgetEventDispatcher.OnRemovedFromSession;
			ActiveSession.Deleted -= m_WidgetEventDispatcher.OnSessionDeleted;
			ActiveSession.RemovedFromSession -= OnRemovedFromSession;
		}

		private async void OnRemovedFromSession()
		{
			await LeaveSession();
		}
	}
}
