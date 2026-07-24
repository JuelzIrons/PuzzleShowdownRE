namespace Unity.Services.Multiplayer
{
	public class SessionObserver : global::System.IDisposable
	{
		private global::Unity.Services.Core.ServiceObserver<global::Unity.Services.Multiplayer.IMultiplayerService> m_ServiceObserver;

		private global::Unity.Services.Multiplayer.IMultiplayerService m_MultiplayerService;

		public readonly string SessionType;

		public global::Unity.Services.Multiplayer.ISession Session { get; private set; }

		public event global::System.Action<global::Unity.Services.Multiplayer.ISession> SessionAdded;

		public event global::System.Action<global::Unity.Services.Multiplayer.AddingSessionOptions> AddingSessionStarted;

		public event global::System.Action<global::Unity.Services.Multiplayer.AddingSessionOptions, global::Unity.Services.Multiplayer.SessionException> AddingSessionFailed;

		public SessionObserver(string sessionType, global::Unity.Services.Core.IUnityServices registry)
		{
			SessionType = sessionType;
			if (registry != null)
			{
				m_ServiceObserver = new global::Unity.Services.Core.ServiceObserver<global::Unity.Services.Multiplayer.IMultiplayerService>(registry);
				if (m_ServiceObserver.Service == null)
				{
					m_ServiceObserver.Initialized += OnServiceInitialized;
				}
				else
				{
					OnServiceInitialized(m_ServiceObserver.Service);
				}
			}
		}

		public SessionObserver(string sessionType)
			: this(sessionType, global::Unity.Services.Core.UnityServices.Instance)
		{
		}

		private void OnServiceInitialized(global::Unity.Services.Multiplayer.IMultiplayerService multiplayerService)
		{
			CleanupObserver();
			m_MultiplayerService = multiplayerService;
			m_MultiplayerService.SessionAdded += OnSessionAdded;
			m_MultiplayerService.AddingSessionStarted += OnAddingSessionStarted;
			m_MultiplayerService.AddingSessionFailed += OnAddingSessionFailed;
			foreach (var (_, session2) in m_MultiplayerService.Sessions)
			{
				OnSessionAdded(session2);
			}
		}

		private void OnAddingSessionStarted(global::Unity.Services.Multiplayer.AddingSessionOptions options)
		{
			if (!(SessionType != options.Type))
			{
				this.AddingSessionStarted?.Invoke(options);
			}
		}

		private void OnAddingSessionFailed(global::Unity.Services.Multiplayer.AddingSessionOptions options, global::Unity.Services.Multiplayer.SessionException sessionException)
		{
			if (!(SessionType != options.Type))
			{
				this.AddingSessionFailed?.Invoke(options, sessionException);
			}
		}

		private void OnSessionAdded(global::Unity.Services.Multiplayer.ISession session)
		{
			if (!(session.Type != SessionType))
			{
				Session = session;
				Session.RemovedFromSession += CleanupSession;
				Session.Deleted += CleanupSession;
				this.SessionAdded?.Invoke(session);
			}
		}

		private void CleanupObserver()
		{
			m_ServiceObserver.Initialized -= OnServiceInitialized;
			m_ServiceObserver.Dispose();
			m_ServiceObserver = null;
		}

		private void CleanupMultiplayerServices()
		{
			m_MultiplayerService.SessionAdded -= OnSessionAdded;
			m_MultiplayerService.AddingSessionStarted -= OnAddingSessionStarted;
			m_MultiplayerService.AddingSessionFailed -= OnAddingSessionFailed;
			m_MultiplayerService = null;
		}

		private void CleanupSession()
		{
			Session.RemovedFromSession -= CleanupSession;
			Session.Deleted -= CleanupSession;
			Session = null;
		}

		public void Dispose()
		{
			if (m_ServiceObserver != null)
			{
				CleanupObserver();
			}
			if (m_MultiplayerService != null)
			{
				CleanupMultiplayerServices();
			}
			if (Session != null)
			{
				CleanupSession();
			}
		}
	}
}
