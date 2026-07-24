namespace Unity.Services.Lobbies.Apis.Lobby
{
	internal interface ILobbyApiClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> BulkUpdateLobbyAsync(global::Unity.Services.Lobbies.Lobby.BulkUpdateLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> CreateLobbyAsync(global::Unity.Services.Lobbies.Lobby.CreateLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> CreateOrJoinLobbyAsync(global::Unity.Services.Lobbies.Lobby.CreateOrJoinLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response> DeleteLobbyAsync(global::Unity.Services.Lobbies.Lobby.DeleteLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.List<string>>> GetHostedLobbiesAsync(global::Unity.Services.Lobbies.Lobby.GetHostedLobbiesRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.List<string>>> GetJoinedLobbiesAsync(global::Unity.Services.Lobbies.Lobby.GetJoinedLobbiesRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> GetLobbyAsync(global::Unity.Services.Lobbies.Lobby.GetLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.MigrationDataInfo>> GetMigrationDataInfoAsync(global::Unity.Services.Lobbies.Lobby.GetMigrationDataInfoRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response> HeartbeatAsync(global::Unity.Services.Lobbies.Lobby.HeartbeatRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> JoinLobbyByCodeAsync(global::Unity.Services.Lobbies.Lobby.JoinLobbyByCodeRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> JoinLobbyByIdAsync(global::Unity.Services.Lobbies.Lobby.JoinLobbyByIdRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.QueryResponse>> QueryLobbiesAsync(global::Unity.Services.Lobbies.Lobby.QueryLobbiesRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> QuickJoinLobbyAsync(global::Unity.Services.Lobbies.Lobby.QuickJoinLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> ReconnectAsync(global::Unity.Services.Lobbies.Lobby.ReconnectRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response> RemovePlayerAsync(global::Unity.Services.Lobbies.Lobby.RemovePlayerRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.TokenData>>> RequestTokensAsync(global::Unity.Services.Lobbies.Lobby.RequestTokensRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> UpdateLobbyAsync(global::Unity.Services.Lobbies.Lobby.UpdateLobbyRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Response<global::Unity.Services.Lobbies.Models.Lobby>> UpdatePlayerAsync(global::Unity.Services.Lobbies.Lobby.UpdatePlayerRequest request, global::Unity.Services.Lobbies.Configuration operationConfiguration = null);
	}
}
