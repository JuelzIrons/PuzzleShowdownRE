namespace Unity.Services.Multiplayer
{
	internal class DaBuilder : global::Unity.Services.Multiplayer.IDaBuilder
	{
		private readonly global::Unity.Services.Relay.IRelayService m_RelayService;

		private readonly global::Unity.Services.DistributedAuthority.IDistributedAuthorityService m_DaService;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerId m_PlayerId;

		public DaBuilder(global::Unity.Services.Relay.IRelayService mRelayService, global::Unity.Services.DistributedAuthority.IDistributedAuthorityService mDaService, global::Unity.Services.Authentication.Internal.IPlayerId playerId)
		{
			m_RelayService = mRelayService;
			m_DaService = mDaService;
			m_PlayerId = playerId;
		}

		public global::Unity.Services.Multiplayer.IDaHandler Build()
		{
			return new global::Unity.Services.Multiplayer.DaHandler(m_RelayService, m_DaService, m_PlayerId);
		}
	}
}
