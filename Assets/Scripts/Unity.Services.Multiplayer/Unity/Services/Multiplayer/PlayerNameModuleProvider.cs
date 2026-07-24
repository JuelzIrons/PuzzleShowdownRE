namespace Unity.Services.Multiplayer
{
	internal class PlayerNameModuleProvider : global::Unity.Services.Multiplayer.IModuleProvider
	{
		private readonly global::Unity.Services.Authentication.Internal.IPlayerNameComponent m_PlayerName;

		public global::System.Type Type => typeof(global::Unity.Services.Multiplayer.PlayerNameModule);

		public int Priority => 3000;

		public PlayerNameModuleProvider(global::Unity.Services.Authentication.Internal.IPlayerNameComponent playerName)
		{
			m_PlayerName = playerName;
		}

		public global::Unity.Services.Multiplayer.IModule Build(global::Unity.Services.Multiplayer.ISession session)
		{
			return new global::Unity.Services.Multiplayer.PlayerNameModule(m_PlayerName, session);
		}
	}
}
