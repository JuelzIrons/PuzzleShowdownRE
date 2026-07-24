namespace Unity.Services.Multiplayer
{
	internal class Player : global::Unity.Services.Multiplayer.IPlayer, global::Unity.Services.Multiplayer.IReadOnlyPlayer
	{
		global::Unity.Services.Multiplayer.ISession global::Unity.Services.Multiplayer.IReadOnlyPlayer.Session => Session;

		public global::Unity.Services.Multiplayer.ISession Session { get; internal set; }

		public bool Modified { get; internal set; }

		public string Id { get; internal set; }

		public string ConnectionInfo { get; internal set; }

		public string AllocationId { get; internal set; }

		public global::System.DateTime Joined { get; internal set; }

		public global::System.DateTime LastUpdated { get; internal set; }

		global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> global::Unity.Services.Multiplayer.IReadOnlyPlayer.Properties => Properties;

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> Properties { get; internal set; } = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty>();

		internal Player(global::Unity.Services.Multiplayer.ISession session, string id = null, string connectionInfo = null, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> properties = null, string allocationId = null, global::System.DateTime joined = default(global::System.DateTime), global::System.DateTime lastUpdated = default(global::System.DateTime))
		{
			Session = session;
			Id = id;
			ConnectionInfo = connectionInfo;
			Properties = properties;
			AllocationId = allocationId;
			Joined = joined;
			LastUpdated = lastUpdated;
		}

		public void SetAllocationId(string allocationId)
		{
			AllocationId = allocationId;
			Modified = true;
		}

		public void SetProperties(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> properties)
		{
			if (properties == null || properties.Count == 0)
			{
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.PlayerProperty> property in properties)
			{
				Properties[property.Key] = property.Value;
			}
			Modified = true;
		}

		public void SetProperty(string key, global::Unity.Services.Multiplayer.PlayerProperty property)
		{
			Properties[key] = property;
			Modified = true;
		}
	}
}
