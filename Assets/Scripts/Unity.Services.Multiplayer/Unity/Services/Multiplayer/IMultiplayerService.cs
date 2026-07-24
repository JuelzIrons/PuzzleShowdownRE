namespace Unity.Services.Multiplayer
{
	public interface IMultiplayerService
	{
		global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.ISession> Sessions { get; }

		event global::System.Action<global::Unity.Services.Multiplayer.AddingSessionOptions> AddingSessionStarted;

		event global::System.Action<global::Unity.Services.Multiplayer.AddingSessionOptions, global::Unity.Services.Multiplayer.SessionException> AddingSessionFailed;

		event global::System.Action<global::Unity.Services.Multiplayer.ISession> SessionAdded;

		event global::System.Action<global::Unity.Services.Multiplayer.ISession> SessionRemoved;

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.IHostSession> CreateSessionAsync(global::Unity.Services.Multiplayer.SessionOptions sessionOptions);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> CreateOrJoinSessionAsync(string sessionId, global::Unity.Services.Multiplayer.SessionOptions sessionOptions);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinSessionByIdAsync(string sessionId, global::Unity.Services.Multiplayer.JoinSessionOptions sessionOptions = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinSessionByCodeAsync(string sessionCode, global::Unity.Services.Multiplayer.JoinSessionOptions sessionOptions = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> ReconnectToSessionAsync(string sessionId, global::Unity.Services.Multiplayer.ReconnectSessionOptions options = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> MatchmakeSessionAsync(global::Unity.Services.Multiplayer.MatchmakerOptions matchOptions, global::Unity.Services.Multiplayer.SessionOptions sessionOptions, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> MatchmakeSessionAsync(global::Unity.Services.Multiplayer.QuickJoinOptions quickJoinOptions, global::Unity.Services.Multiplayer.SessionOptions sessionOptions);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.QuerySessionsResults> QuerySessionsAsync(global::Unity.Services.Multiplayer.QuerySessionsOptions queryOptions);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<string>> GetJoinedSessionIdsAsync();
	}
}
