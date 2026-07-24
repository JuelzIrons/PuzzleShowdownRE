namespace Unity.Services.Lobbies.Models
{
	public class LobbyDownloadMigrationDataOptions
	{
		public static readonly global::System.TimeSpan DefaultTimeout = global::System.TimeSpan.FromSeconds(50.0);

		public global::System.TimeSpan Timeout { get; set; } = DefaultTimeout;
	}
}
