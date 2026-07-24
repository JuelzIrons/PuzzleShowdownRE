namespace Unity.Services.Multiplayer
{
	internal interface ISessionManager
	{
		global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.ISession> Sessions { get; }

		event global::System.Action<global::Unity.Services.Multiplayer.ISession> SessionAdded;

		event global::System.Action<global::Unity.Services.Multiplayer.ISession> SessionRemoved;

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> GetAsync(string sessionId, global::Unity.Services.Multiplayer.SessionOptions sessionOptions = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> CreateAsync(global::Unity.Services.Multiplayer.SessionOptions sessionOptions);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> CreateOrJoinAsync(string sessionId, global::Unity.Services.Multiplayer.SessionOptions sessionOptions);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> JoinByCodeAsync(string sessionCode, global::Unity.Services.Multiplayer.JoinSessionOptions sessionOptions);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> JoinByIdAsync(string sessionCode, global::Unity.Services.Multiplayer.JoinSessionOptions sessionOptions);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> QuickJoinAsync(global::Unity.Services.Multiplayer.QuickJoinOptions quickJoinOptions, global::Unity.Services.Multiplayer.SessionOptions sessionOptions);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionHandler> ReconnectAsync(string sessionId, global::Unity.Services.Multiplayer.ReconnectSessionOptions options = null);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<string>> GetJoinedSessionIdsAsync();
	}
}
