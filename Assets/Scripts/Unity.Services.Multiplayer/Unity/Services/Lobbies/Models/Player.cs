namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "Player")]
	public class Player
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "profile", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Models.PlayerProfile Profile { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "connectionInfo", EmitDefaultValue = false)]
		public string ConnectionInfo { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "data", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject> Data { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "allocationId", EmitDefaultValue = false)]
		public string AllocationId { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "joined", EmitDefaultValue = false)]
		public global::System.DateTime Joined { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "lastUpdated", EmitDefaultValue = false)]
		public global::System.DateTime LastUpdated { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		public Player(string id = null, string connectionInfo = null, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject> data = null, string allocationId = null, global::System.DateTime joined = default(global::System.DateTime), global::System.DateTime lastUpdated = default(global::System.DateTime), global::Unity.Services.Lobbies.Models.PlayerProfile profile = null)
		{
			Id = id;
			Profile = profile;
			ConnectionInfo = connectionInfo;
			Data = data;
			AllocationId = allocationId;
			Joined = joined;
			LastUpdated = lastUpdated;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Id != null)
			{
				text = text + "id," + Id + ",";
			}
			if (Profile != null)
			{
				text = text + "profile," + Profile.ToString() + ",";
			}
			if (ConnectionInfo != null)
			{
				text = text + "connectionInfo," + ConnectionInfo + ",";
			}
			if (Data != null)
			{
				text = text + "data," + Data.ToString() + ",";
			}
			if (AllocationId != null)
			{
				text = text + "allocationId," + AllocationId + ",";
			}
			_ = Joined;
			text = text + "joined," + Joined.ToString() + ",";
			_ = LastUpdated;
			return text + "lastUpdated," + LastUpdated;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Id != null)
			{
				string value = Id.ToString();
				dictionary.Add("id", value);
			}
			if (ConnectionInfo != null)
			{
				string value2 = ConnectionInfo.ToString();
				dictionary.Add("connectionInfo", value2);
			}
			if (AllocationId != null)
			{
				string value3 = AllocationId.ToString();
				dictionary.Add("allocationId", value3);
			}
			_ = Joined;
			string value4 = Joined.ToString();
			dictionary.Add("joined", value4);
			_ = LastUpdated;
			string value5 = LastUpdated.ToString();
			dictionary.Add("lastUpdated", value5);
			return dictionary;
		}
	}
}
