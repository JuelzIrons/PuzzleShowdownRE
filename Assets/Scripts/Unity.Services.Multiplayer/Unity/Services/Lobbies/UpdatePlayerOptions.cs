namespace Unity.Services.Lobbies
{
	public class UpdatePlayerOptions
	{
		public string AllocationId { get; set; }

		public string ConnectionInfo { get; set; }

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject> Data { get; set; }
	}
}
