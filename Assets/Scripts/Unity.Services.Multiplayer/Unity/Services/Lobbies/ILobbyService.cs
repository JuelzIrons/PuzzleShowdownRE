namespace Unity.Services.Lobbies
{
	public interface ILobbyService
	{
		bool ConcurrencyControlEnabled { get; set; }

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> CreateLobbyAsync(string lobbyName, int maxPlayers, global::Unity.Services.Lobbies.CreateLobbyOptions options = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> CreateOrJoinLobbyAsync(string lobbyId, string lobbyName, int maxPlayers, global::Unity.Services.Lobbies.CreateLobbyOptions options = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.ILobbyEvents> SubscribeToLobbyEventsAsync(string lobbyId, LobbyEventCallbacks callbacks);

		global::System.Threading.Tasks.Task DeleteLobbyAsync(string lobbyId);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<string>> GetJoinedLobbiesAsync();

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> GetLobbyAsync(string lobbyId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> GetLobbyAsync(string lobbyId, string ifNoneMatchVersion);

		global::System.Threading.Tasks.Task SendHeartbeatPingAsync(string lobbyId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> JoinLobbyByCodeAsync(string lobbyCode, global::Unity.Services.Lobbies.JoinLobbyByCodeOptions options = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> JoinLobbyByIdAsync(string lobbyId, global::Unity.Services.Lobbies.JoinLobbyByIdOptions options = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.QueryResponse> QueryLobbiesAsync(global::Unity.Services.Lobbies.QueryLobbiesOptions options = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> QuickJoinLobbyAsync(global::Unity.Services.Lobbies.QuickJoinLobbyOptions options = null);

		global::System.Threading.Tasks.Task RemovePlayerAsync(string lobbyId, string playerId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> UpdateLobbyAsync(string lobbyId, global::Unity.Services.Lobbies.UpdateLobbyOptions options);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> UpdatePlayerAsync(string lobbyId, string playerId, global::Unity.Services.Lobbies.UpdatePlayerOptions options);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> ReconnectToLobbyAsync(string lobbyId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.MigrationDataInfo> GetMigrationDataInfoAsync(string lobbyId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.LobbyMigrationData> DownloadMigrationDataAsync(global::Unity.Services.Lobbies.Models.MigrationDataInfo migrationDataInfo, global::Unity.Services.Lobbies.Models.LobbyDownloadMigrationDataOptions options);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataResults> UploadMigrationDataAsync(global::Unity.Services.Lobbies.Models.MigrationDataInfo migrationDataInfo, byte[] data, global::Unity.Services.Lobbies.Models.LobbyUploadMigrationDataOptions options);
	}
}
