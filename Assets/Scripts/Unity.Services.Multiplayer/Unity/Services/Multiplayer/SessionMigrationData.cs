namespace Unity.Services.Multiplayer
{
	public class SessionMigrationData : global::Unity.Services.Lobbies.Models.MigrationData
	{
		private readonly global::Unity.Services.Lobbies.Models.LobbyMigrationData m_migrationData;

		public override byte[] Data => m_migrationData.Data;

		internal SessionMigrationData(global::Unity.Services.Lobbies.Models.LobbyMigrationData lobbyMigrationData)
		{
			m_migrationData = lobbyMigrationData;
		}
	}
}
