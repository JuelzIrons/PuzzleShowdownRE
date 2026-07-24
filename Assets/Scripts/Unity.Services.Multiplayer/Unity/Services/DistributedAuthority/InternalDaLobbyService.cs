namespace Unity.Services.DistributedAuthority
{
	internal class InternalDaLobbyService : global::Unity.Services.DistributedAuthority.IInternalDaLobbyService
	{
		private readonly global::System.Lazy<global::Unity.Services.Lobbies.ILobbyService> _lazyLobbyService;

		public InternalDaLobbyService(global::System.Lazy<global::Unity.Services.Lobbies.ILobbyService> lazyLobbyService)
		{
			_lazyLobbyService = lazyLobbyService;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> JoinLobbyByIdAsync(string lobbyId, global::Unity.Services.Lobbies.JoinLobbyByIdOptions options = null)
		{
			try
			{
				(global::Unity.Services.Lobbies.Models.Lobby, bool) obj = await ReconnectToLobbyAsync(lobbyId);
				var (result, _) = obj;
				if (obj.Item2)
				{
					return result;
				}
				return await _lazyLobbyService.Value.JoinLobbyByIdAsync(lobbyId, options);
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException lex)
			{
				if (!lex.IsAlreadyMemberError())
				{
					throw;
				}
			}
			return await GetLobbyAsync(lobbyId);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Lobbies.Models.Lobby> GetLobbyAsync(string lobbyId)
		{
			return await _lazyLobbyService.Value.GetLobbyAsync(lobbyId);
		}

		private async global::System.Threading.Tasks.Task<(global::Unity.Services.Lobbies.Models.Lobby, bool)> ReconnectToLobbyAsync(string lobbyId)
		{
			try
			{
				return (await _lazyLobbyService.Value.ReconnectToLobbyAsync(lobbyId), true);
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException ex)
			{
				if (ex.Reason == global::Unity.Services.Lobbies.LobbyExceptionReason.Forbidden)
				{
					return (null, false);
				}
				throw;
			}
		}
	}
}
