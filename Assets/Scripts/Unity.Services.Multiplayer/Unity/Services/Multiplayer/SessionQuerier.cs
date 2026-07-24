namespace Unity.Services.Multiplayer
{
	internal class SessionQuerier : global::Unity.Services.Multiplayer.ISessionQuerier
	{
		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private readonly global::Unity.Services.Lobbies.ILobbyService m_LobbyService;

		public SessionQuerier(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::Unity.Services.Lobbies.ILobbyService lobbyService)
		{
			m_ActionScheduler = actionScheduler;
			m_LobbyService = lobbyService;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.QuerySessionsResults> QueryAsync(global::Unity.Services.Multiplayer.QuerySessionsOptions options)
		{
			try
			{
				global::Unity.Services.Lobbies.QueryLobbiesOptions options2 = global::Unity.Services.Multiplayer.LobbyConverter.ToQueryLobbiesOptions(options);
				global::Unity.Services.Lobbies.Models.QueryResponse queryResponse = await m_LobbyService.QueryLobbiesAsync(options2);
				global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.ISessionInfo> list = new global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.ISessionInfo>();
				foreach (global::Unity.Services.Lobbies.Models.Lobby result in queryResponse.Results)
				{
					list.Add(new global::Unity.Services.Multiplayer.LobbySessionInfo(result));
				}
				return new global::Unity.Services.Multiplayer.QuerySessionsResults(list, queryResponse.ContinuationToken, options, this, m_ActionScheduler);
			}
			catch (global::Unity.Services.Lobbies.LobbyServiceException exception)
			{
				throw global::Unity.Services.Multiplayer.LobbyConverter.ToSessionException(exception);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}
	}
}
