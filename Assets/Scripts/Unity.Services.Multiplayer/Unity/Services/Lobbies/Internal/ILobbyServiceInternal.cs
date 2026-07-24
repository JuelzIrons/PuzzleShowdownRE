namespace Unity.Services.Lobbies.Internal
{
	internal interface ILobbyServiceInternal : global::Unity.Services.Lobbies.ILobbyService
	{
		global::System.Threading.Tasks.Task<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.TokenData>> RequestTokensAsync(string lobbyId, params global::Unity.Services.Lobbies.Models.TokenRequest.TokenTypeOptions[] tokenOptions);

		global::Lobbies.SDK.LobbyCacher.LobbyCacher GetLobbyCacher();

		global::Unity.Services.Lobbies.ILobbyEvents SetCacherLobbyCallbacks(string lobbyId, LobbyEventCallbacks lobbyEventCallbacks);

		global::System.Threading.Tasks.Task DeleteLobbyAsync(string lobbyId, bool applyIfMatch);

		global::System.Threading.Tasks.Task RemovePlayerAsync(string lobbyId, string playerId, bool applyIfMatch);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> UpdateLobbyAsync(string lobbyId, global::Unity.Services.Lobbies.UpdateLobbyOptions options, bool applyIfMatch);

		global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> UpdatePlayerAsync(string lobbyId, string playerId, global::Unity.Services.Lobbies.UpdatePlayerOptions options, bool applyIfMatch);
	}
}
