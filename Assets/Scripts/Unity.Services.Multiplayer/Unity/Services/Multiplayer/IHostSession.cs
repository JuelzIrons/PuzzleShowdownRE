namespace Unity.Services.Multiplayer
{
	public interface IHostSession : global::Unity.Services.Multiplayer.ISession
	{
		new string Name { get; set; }

		new bool IsPrivate { get; set; }

		new bool IsLocked { get; set; }

		new string Host { get; set; }

		string Password { set; }

		new global::Unity.Services.Multiplayer.IHostSessionNetwork Network { get; }

		new global::System.Collections.Generic.IReadOnlyList<global::Unity.Services.Multiplayer.IPlayer> Players { get; }

		global::System.Threading.Tasks.Task RemovePlayerAsync(string playerId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.SessionMigrationData> GetHostMigrationDataAsync(global::System.TimeSpan timeout);

		global::System.Threading.Tasks.Task SetHostMigrationDataAsync(byte[] data, global::System.TimeSpan timeout);

		void SetProperties(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.SessionProperty> properties);

		void SetProperty(string key, global::Unity.Services.Multiplayer.SessionProperty property);

		global::System.Threading.Tasks.Task SavePropertiesAsync();

		global::System.Threading.Tasks.Task SavePlayerDataAsync(string playerId);

		global::System.Threading.Tasks.Task DeleteAsync();

		new global::Unity.Services.Multiplayer.IPlayer GetPlayer(string playerId);
	}
}
