namespace Unity.Services.Lobbies.Models
{
	public class LobbyMigrationData : global::Unity.Services.Lobbies.Models.MigrationData
	{
		public override byte[] Data { get; }

		public LobbyMigrationData(byte[] data)
		{
			Data = data;
		}
	}
}
