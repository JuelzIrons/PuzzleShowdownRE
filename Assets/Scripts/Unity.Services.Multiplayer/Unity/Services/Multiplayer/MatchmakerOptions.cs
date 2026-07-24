namespace Unity.Services.Multiplayer
{
	public class MatchmakerOptions
	{
		public string QueueName { get; set; }

		public global::System.Collections.Generic.Dictionary<string, object> TicketAttributes { get; set; } = new global::System.Collections.Generic.Dictionary<string, object>();

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> PlayerProperties { get; set; } = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty>();
	}
}
