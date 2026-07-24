namespace Unity.Services.Multiplayer
{
	internal class NetworkProvider : global::Unity.Services.Multiplayer.IModuleProvider
	{
		private readonly global::Unity.Services.Multiplayer.INetworkBuilder m_NetworkBuilder;

		private readonly global::Unity.Services.Multiplayer.IRelayBuilder m_RelayBuilder;

		private readonly global::Unity.Services.Multiplayer.IDaBuilder m_DaBuilder;

		public global::System.Type Type => typeof(global::Unity.Services.Multiplayer.NetworkModule);

		public int Priority => 1000;

		internal NetworkProvider(global::Unity.Services.Multiplayer.INetworkBuilder networkBuilder, global::Unity.Services.Multiplayer.IDaBuilder daBuilder, global::Unity.Services.Multiplayer.IRelayBuilder relayBuilder)
		{
			m_NetworkBuilder = networkBuilder;
			m_DaBuilder = daBuilder;
			m_RelayBuilder = relayBuilder;
		}

		public global::Unity.Services.Multiplayer.IModule Build(global::Unity.Services.Multiplayer.ISession session)
		{
			return new global::Unity.Services.Multiplayer.NetworkModule(session, m_NetworkBuilder, m_DaBuilder, m_RelayBuilder);
		}
	}
}
