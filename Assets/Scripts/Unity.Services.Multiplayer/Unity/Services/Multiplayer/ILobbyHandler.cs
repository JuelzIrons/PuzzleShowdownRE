namespace Unity.Services.Multiplayer
{
	internal interface ILobbyHandler
	{
		bool ConcurrencyControlEnabled { get; set; }

		bool IsAuthorized { get; }

		bool IsHost { get; }

		bool IsServer { get; }

		bool IsMember { get; }

		global::Unity.Services.Lobbies.Models.Lobby Lobby { get; }

		global::Unity.Services.Multiplayer.LobbyState State { get; }

		global::Unity.Services.Lobbies.Models.MigrationDataInfo MigrationDataInfo { get; }

		event global::System.Action LobbyChanged;

		event global::System.Action LobbyExit;

		event global::System.Action<string> PlayerJoined;

		[global::System.Obsolete("PlayerLeft has been deprecated. Use PlayerLeaving instead")]
		event global::System.Action<string> PlayerLeft;

		event global::System.Action<string> PlayerLeaving;

		event global::System.Action<string> PlayerHasLeft;

		event global::System.Action DataChanged;

		event global::System.Action PlayerDataChanged;

		event global::System.Action KickedFromLobby;

		event global::System.Action LobbyDeleted;

		event global::System.Action<string> LobbyHostChanged;

		void AssignLobby(global::Unity.Services.Lobbies.Models.Lobby lobby, global::Unity.Services.Multiplayer.LobbyState state);

		global::System.Threading.Tasks.Task CreateLobbyAsync(string lobbyName, int maxPlayers, global::Unity.Services.Lobbies.CreateLobbyOptions options = null);

		global::System.Threading.Tasks.Task CreateOrJoinLobbyAsync(string id, string lobbyName, int maxPlayers, global::Unity.Services.Lobbies.CreateLobbyOptions options = null);

		global::System.Threading.Tasks.Task RefreshLobbyAsync();

		global::System.Threading.Tasks.Task GetLobbyAsync(string lobbyId);

		global::System.Threading.Tasks.Task UpdateLobbyAsync(global::Unity.Services.Lobbies.UpdateLobbyOptions updateLobbyOptions);

		global::System.Threading.Tasks.Task UpdateLobbyDataAsync(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> data);

		global::System.Threading.Tasks.Task UpdateCurrentPlayerAsync(global::Unity.Services.Lobbies.UpdatePlayerOptions updatePlayerOptions);

		global::System.Threading.Tasks.Task UpdatePlayerAsync(string playerId, global::Unity.Services.Lobbies.UpdatePlayerOptions updatePlayerOptions);

		global::System.Threading.Tasks.Task DeleteLobbyAsync();

		global::System.Threading.Tasks.Task JoinLobbyByIdAsync(string lobbyId, global::Unity.Services.Lobbies.JoinLobbyByIdOptions options = null);

		global::System.Threading.Tasks.Task JoinLobbyByCodeAsync(string lobbyCode, global::Unity.Services.Lobbies.JoinLobbyByCodeOptions options = null);

		global::System.Threading.Tasks.Task QuickJoinLobbyAsync(global::Unity.Services.Lobbies.QuickJoinLobbyOptions options);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<string>> GetJoinedLobbiesAsync();

		global::System.Threading.Tasks.Task ReconnectToLobbyAsync(string lobbyId = null);

		global::System.Threading.Tasks.Task RemovePlayerAsync(string playerId);

		global::System.Threading.Tasks.Task UploadMigrationDataAsync(byte[] data, global::System.TimeSpan timeout);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.LobbyMigrationData> DownloadMigrationDataAsync(global::System.TimeSpan timeout);

		global::System.Threading.Tasks.Task SubscribeToLobbyEventsAsync(LobbyEventCallbacks callbacks);

		global::System.Threading.Tasks.Task ResetAsync();
	}
}
