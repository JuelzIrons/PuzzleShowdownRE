namespace Unity.Services.Multiplayer
{
	internal class WrappedMultiplayerService : global::Unity.Services.Multiplayer.IMultiplayerService
	{
		private readonly global::Unity.Services.Multiplayer.ISessionQuerier m_SessionQuerier;

		private readonly global::Unity.Services.Multiplayer.ISessionManager m_SessionManager;

		private readonly global::Unity.Services.Multiplayer.IMatchmakerManager m_MatchmakerManager;

		private readonly global::Unity.Services.Multiplayer.IModuleRegistry m_ModuleRegistry;

		global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.ISession> global::Unity.Services.Multiplayer.IMultiplayerService.Sessions => m_SessionManager.Sessions;

		public event global::System.Action<global::Unity.Services.Multiplayer.AddingSessionOptions> AddingSessionStarted;

		public event global::System.Action<global::Unity.Services.Multiplayer.AddingSessionOptions, global::Unity.Services.Multiplayer.SessionException> AddingSessionFailed;

		public event global::System.Action<global::Unity.Services.Multiplayer.ISession> SessionAdded
		{
			add
			{
				m_SessionManager.SessionAdded += value;
			}
			remove
			{
				m_SessionManager.SessionAdded -= value;
			}
		}

		public event global::System.Action<global::Unity.Services.Multiplayer.ISession> SessionRemoved
		{
			add
			{
				m_SessionManager.SessionRemoved += value;
			}
			remove
			{
				m_SessionManager.SessionRemoved -= value;
			}
		}

		internal WrappedMultiplayerService(global::Unity.Services.Multiplayer.ISessionQuerier sessionQuerier, global::Unity.Services.Multiplayer.ISessionManager sessionManager, global::Unity.Services.Multiplayer.IMatchmakerManager matchmakerManager, global::Unity.Services.Multiplayer.IModuleRegistry moduleRegistry)
		{
			m_SessionQuerier = sessionQuerier;
			m_SessionManager = sessionManager;
			m_MatchmakerManager = matchmakerManager;
			m_ModuleRegistry = moduleRegistry;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.IHostSession> CreateSessionAsync(global::Unity.Services.Multiplayer.SessionOptions sessionOptions)
		{
			global::Unity.Services.Multiplayer.AddingSessionOptions addingSessionOptions = new global::Unity.Services.Multiplayer.AddingSessionOptions(sessionOptions?.Type);
			this.AddingSessionStarted?.Invoke(addingSessionOptions);
			try
			{
				return (await m_SessionManager.CreateAsync(sessionOptions)).AsHost();
			}
			catch (global::System.Exception e)
			{
				throw HandleException(addingSessionOptions, e);
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> CreateOrJoinSessionAsync(string sessionId, global::Unity.Services.Multiplayer.SessionOptions sessionOptions)
		{
			global::Unity.Services.Multiplayer.AddingSessionOptions addingSessionOptions = new global::Unity.Services.Multiplayer.AddingSessionOptions(sessionOptions?.Type);
			this.AddingSessionStarted?.Invoke(addingSessionOptions);
			try
			{
				return await m_SessionManager.CreateOrJoinAsync(sessionId, sessionOptions);
			}
			catch (global::System.Exception e)
			{
				throw HandleException(addingSessionOptions, e);
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinSessionByIdAsync(string sessionId, global::Unity.Services.Multiplayer.JoinSessionOptions sessionOptions)
		{
			global::Unity.Services.Multiplayer.AddingSessionOptions addingSessionOptions = new global::Unity.Services.Multiplayer.AddingSessionOptions(sessionOptions?.Type);
			this.AddingSessionStarted?.Invoke(addingSessionOptions);
			try
			{
				return await m_SessionManager.JoinByIdAsync(sessionId, sessionOptions);
			}
			catch (global::System.Exception e)
			{
				throw HandleException(addingSessionOptions, e);
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinSessionByCodeAsync(string sessionCode, global::Unity.Services.Multiplayer.JoinSessionOptions sessionOptions)
		{
			global::Unity.Services.Multiplayer.AddingSessionOptions addingSessionOptions = new global::Unity.Services.Multiplayer.AddingSessionOptions(sessionOptions?.Type);
			this.AddingSessionStarted?.Invoke(addingSessionOptions);
			try
			{
				return await m_SessionManager.JoinByCodeAsync(sessionCode, sessionOptions);
			}
			catch (global::System.Exception e)
			{
				throw HandleException(addingSessionOptions, e);
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> ReconnectToSessionAsync(string sessionId, global::Unity.Services.Multiplayer.ReconnectSessionOptions options = null)
		{
			global::Unity.Services.Multiplayer.AddingSessionOptions addingSessionOptions = new global::Unity.Services.Multiplayer.AddingSessionOptions(options?.Type);
			this.AddingSessionStarted?.Invoke(addingSessionOptions);
			try
			{
				return await m_SessionManager.ReconnectAsync(sessionId, options);
			}
			catch (global::System.Exception e)
			{
				throw HandleException(addingSessionOptions, e);
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> MatchmakeSessionAsync(global::Unity.Services.Multiplayer.QuickJoinOptions quickJoinOptions, global::Unity.Services.Multiplayer.SessionOptions sessionOptions)
		{
			global::Unity.Services.Multiplayer.AddingSessionOptions addingSessionOptions = new global::Unity.Services.Multiplayer.AddingSessionOptions(sessionOptions?.Type);
			this.AddingSessionStarted?.Invoke(addingSessionOptions);
			try
			{
				return await m_SessionManager.QuickJoinAsync(quickJoinOptions, sessionOptions);
			}
			catch (global::System.Exception e)
			{
				throw HandleException(addingSessionOptions, e);
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> MatchmakeSessionAsync(global::Unity.Services.Multiplayer.MatchmakerOptions matchOptions, global::Unity.Services.Multiplayer.SessionOptions sessionOptions, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			global::Unity.Services.Multiplayer.AddingSessionOptions addingSessionOptions = new global::Unity.Services.Multiplayer.AddingSessionOptions(sessionOptions?.Type);
			this.AddingSessionStarted?.Invoke(addingSessionOptions);
			try
			{
				if (matchOptions == null)
				{
					throw new global::Unity.Services.Multiplayer.SessionException("MatchmakerOptions cannot be null.", global::Unity.Services.Multiplayer.SessionError.InvalidMatchmakerOptions);
				}
				return await m_MatchmakerManager.StartAsync(matchOptions, sessionOptions?.WithMatchmaker(), cancellationToken);
			}
			catch (global::System.Exception e)
			{
				throw HandleException(addingSessionOptions, e);
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.QuerySessionsResults> QuerySessionsAsync(global::Unity.Services.Multiplayer.QuerySessionsOptions queryOptions)
		{
			try
			{
				return await m_SessionQuerier.QueryAsync(queryOptions);
			}
			catch (global::System.Exception ex) when (!(ex is global::Unity.Services.Multiplayer.SessionException))
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<string>> GetJoinedSessionIdsAsync()
		{
			try
			{
				return await m_SessionManager.GetJoinedSessionIdsAsync();
			}
			catch (global::System.Exception ex) when (!(ex is global::Unity.Services.Multiplayer.SessionException))
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		private global::Unity.Services.Multiplayer.SessionException HandleException(global::Unity.Services.Multiplayer.AddingSessionOptions addingSessionOptions, global::System.Exception e)
		{
			if (e is global::Unity.Services.Multiplayer.SessionException ex)
			{
				this.AddingSessionFailed?.Invoke(addingSessionOptions, ex);
				return ex;
			}
			global::Unity.Services.Multiplayer.SessionException ex2 = new global::Unity.Services.Multiplayer.SessionException(e.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			this.AddingSessionFailed?.Invoke(addingSessionOptions, ex2);
			return ex2;
		}
	}
}
