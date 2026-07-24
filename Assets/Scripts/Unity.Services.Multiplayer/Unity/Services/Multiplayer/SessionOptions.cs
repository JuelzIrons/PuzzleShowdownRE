namespace Unity.Services.Multiplayer
{
	public class SessionOptions : global::Unity.Services.Multiplayer.BaseSessionOptions
	{
		public string Name { get; set; } = global::System.Guid.NewGuid().ToString();

		public int MaxPlayers { get; set; }

		public bool IsLocked { get; set; }

		public bool IsPrivate { get; set; }

		public string Password { get; set; }

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.SessionProperty> SessionProperties { get; set; } = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.SessionProperty>();

		internal global::Unity.Services.Multiplayer.JoinSessionOptions ToJoinOptions()
		{
			return new global::Unity.Services.Multiplayer.JoinSessionOptions
			{
				Type = base.Type,
				PlayerProperties = base.PlayerProperties,
				Password = Password,
				Options = base.Options
			};
		}
	}
}
