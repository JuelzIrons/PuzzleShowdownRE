namespace Unity.Services.Multiplayer
{
	internal class RelayBuilder : global::Unity.Services.Multiplayer.IRelayBuilder
	{
		private readonly global::Unity.Services.Relay.IRelayService m_RelayService;

		public RelayBuilder(global::Unity.Services.Relay.IRelayService mRelayService)
		{
			m_RelayService = mRelayService;
		}

		public global::Unity.Services.Multiplayer.IRelayHandler Build()
		{
			return new global::Unity.Services.Multiplayer.RelayHandler(m_RelayService);
		}
	}
}
