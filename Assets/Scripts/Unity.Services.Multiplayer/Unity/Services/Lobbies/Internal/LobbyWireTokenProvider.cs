namespace Unity.Services.Lobbies.Internal
{
	internal class LobbyWireTokenProvider : global::Unity.Services.Wire.Internal.IChannelTokenProvider
	{
		private string lobbyId;

		private global::Unity.Services.Lobbies.Internal.WrappedLobbyService lobbyService;

		internal LobbyWireTokenProvider(string lobbyId, global::Unity.Services.Lobbies.Internal.WrappedLobbyService lobbyService)
		{
			if (lobbyId == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("LobbyWireTokenProvider is invalid as its lobbyId is null!");
			}
			if (lobbyService == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("LobbyWireTokenProvider is invalid as its lobbyService is null!");
			}
			this.lobbyId = lobbyId;
			this.lobbyService = lobbyService;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Wire.Internal.ChannelToken> GetTokenAsync()
		{
			if ((await lobbyService.RequestTokensAsync(lobbyId, global::Unity.Services.Lobbies.Models.TokenRequest.TokenTypeOptions.WireJoin)).TryGetValue("wireJoin", out var value))
			{
				return new global::Unity.Services.Wire.Internal.ChannelToken
				{
					ChannelName = value.Uri,
					Token = value.TokenValue
				};
			}
			return default(global::Unity.Services.Wire.Internal.ChannelToken);
		}
	}
}
