namespace Unity.Services.Lobbies
{
	public class CreateLobbyOptions
	{
		public bool? IsPrivate { get; set; }

		public string Password { get; set; }

		public bool? IsLocked { get; set; }

		public global::Unity.Services.Lobbies.Models.Player Player { get; set; }

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> Data { get; set; }
	}
}
