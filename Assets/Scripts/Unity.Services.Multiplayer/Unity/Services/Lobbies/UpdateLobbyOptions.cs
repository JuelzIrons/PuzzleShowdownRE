namespace Unity.Services.Lobbies
{
	public class UpdateLobbyOptions
	{
		public string Name { get; set; }

		public int? MaxPlayers { get; set; }

		public bool? IsPrivate { get; set; }

		public bool? IsLocked { get; set; }

		public string Password { get; set; }

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> Data { get; set; }

		public string HostId { get; set; }
	}
}
