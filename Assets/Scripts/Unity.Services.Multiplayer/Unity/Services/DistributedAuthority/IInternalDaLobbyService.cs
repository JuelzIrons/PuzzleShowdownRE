namespace Unity.Services.DistributedAuthority
{
	internal interface IInternalDaLobbyService
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> JoinLobbyByIdAsync(string lobbyId, global::Unity.Services.Lobbies.JoinLobbyByIdOptions options = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> GetLobbyAsync(string lobbyIy);
	}
}
