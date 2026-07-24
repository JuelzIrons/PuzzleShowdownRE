namespace Unity.Services.Multiplayer
{
	internal class LobbyBuilder : global::Unity.Services.Multiplayer.ILobbyBuilder
	{
		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private readonly global::Unity.Services.Lobbies.Internal.ILobbyServiceInternal m_LobbyService;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerId m_PlayerId;

		private readonly global::Unity.Services.Multiplayer.IServiceID m_ServiceId;

		private readonly global::Unity.Services.Authentication.Internal.IAccessToken m_AccessToken;

		private readonly global::Unity.Services.Authentication.Internal.IAccessTokenObserver m_AccessTokenObserver;

		private readonly bool m_UsePolling;

		public LobbyBuilder(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler mActionScheduler, global::Unity.Services.Lobbies.Internal.ILobbyServiceInternal mLobbyService, global::Unity.Services.Authentication.Internal.IPlayerId mPlayerId, global::Unity.Services.Multiplayer.IServiceID mServiceID, global::Unity.Services.Authentication.Internal.IAccessToken mAccessToken, global::Unity.Services.Authentication.Internal.IAccessTokenObserver mAccessTokenObserver, bool usePolling)
		{
			m_ActionScheduler = mActionScheduler;
			m_LobbyService = mLobbyService;
			m_PlayerId = mPlayerId;
			m_ServiceId = mServiceID;
			m_AccessToken = mAccessToken;
			m_AccessTokenObserver = mAccessTokenObserver;
			m_UsePolling = usePolling;
		}

		public global::Unity.Services.Multiplayer.ILobbyHandler Build()
		{
			return new global::Unity.Services.Multiplayer.LobbyHandler(m_ActionScheduler, m_LobbyService, m_PlayerId, m_ServiceId, m_AccessToken, m_AccessTokenObserver, m_UsePolling);
		}
	}
}
