namespace Unity.Services.Multiplayer
{
	public interface ISession
	{
		string Type { get; }

		string Name { get; }

		string Id { get; }

		string Code { get; }

		bool IsHost { get; }

		bool IsServer { get; }

		bool IsPrivate { get; }

		bool IsLocked { get; }

		bool HasPassword { get; }

		int AvailableSlots { get; }

		int MaxPlayers { get; }

		int PlayerCount { get; }

		global::System.Collections.Generic.IReadOnlyList<global::Unity.Services.Multiplayer.IReadOnlyPlayer> Players { get; }

		global::Unity.Services.Multiplayer.IClientSessionNetwork Network { get; }

		internal global::Unity.Services.Multiplayer.NetworkModule NetworkModule { get; }

		global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.SessionProperty> Properties { get; }

		string Host { get; }

		global::Unity.Services.Multiplayer.SessionState State { get; }

		global::Unity.Services.Multiplayer.IPlayer CurrentPlayer { get; }

		bool IsMember { get; }

		bool ConcurrencyControlEnabled { get; set; }

		event global::System.Action Changed;

		event global::System.Action<global::Unity.Services.Multiplayer.SessionState> StateChanged;

		event global::System.Action<string> PlayerJoined;

		[global::System.Obsolete("PlayerLeft has been deprecated. Use PlayerLeaving instead (UnityUpgradable) -> PlayerLeaving")]
		event global::System.Action<string> PlayerLeft;

		event global::System.Action<string> PlayerLeaving;

		event global::System.Action<string> PlayerHasLeft;

		event global::System.Action SessionPropertiesChanged;

		event global::System.Action PlayerPropertiesChanged;

		event global::System.Action RemovedFromSession;

		event global::System.Action Deleted;

		event global::System.Action<string> SessionHostChanged;

		event global::System.Action SessionMigrated;

		global::System.Threading.Tasks.Task SaveCurrentPlayerDataAsync();

		global::System.Threading.Tasks.Task LeaveAsync();

		global::System.Threading.Tasks.Task RefreshAsync();

		global::System.Threading.Tasks.Task ReconnectAsync();

		global::Unity.Services.Multiplayer.IHostSession AsHost();

		global::Unity.Services.Multiplayer.IServerSession AsServer();

		bool HasPlayer(string playerId);

		global::Unity.Services.Multiplayer.IReadOnlyPlayer GetPlayer(string playerId);

		internal T GetModule<T>() where T : class, global::Unity.Services.Multiplayer.IModule;

		internal void OnSessionMigrated();
	}
}
