namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "Lobby")]
	public class Lobby
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "lobbyCode", EmitDefaultValue = false)]
		public string LobbyCode { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "upid", EmitDefaultValue = false)]
		public string Upid { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "environmentId", EmitDefaultValue = false)]
		public string EnvironmentId { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "maxPlayers", EmitDefaultValue = false)]
		public int MaxPlayers { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "availableSlots", EmitDefaultValue = false)]
		public int AvailableSlots { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "isPrivate", EmitDefaultValue = true)]
		public bool IsPrivate { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "isLocked", EmitDefaultValue = true)]
		public bool IsLocked { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "hasPassword", EmitDefaultValue = true)]
		public bool HasPassword { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "players", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player> Players { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "data", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> Data { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "hostId", EmitDefaultValue = false)]
		public string HostId { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "created", EmitDefaultValue = false)]
		public global::System.DateTime Created { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "lastUpdated", EmitDefaultValue = false)]
		public global::System.DateTime LastUpdated { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "version", EmitDefaultValue = false)]
		public int Version { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		public Lobby(string id = null, string lobbyCode = null, string upid = null, string environmentId = null, string name = null, int maxPlayers = 0, int availableSlots = 0, bool isPrivate = false, bool isLocked = false, global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player> players = null, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> data = null, string hostId = null, global::System.DateTime created = default(global::System.DateTime), global::System.DateTime lastUpdated = default(global::System.DateTime), int version = 0, bool hasPassword = false)
		{
			Id = id;
			LobbyCode = lobbyCode;
			Upid = upid;
			EnvironmentId = environmentId;
			Name = name;
			MaxPlayers = maxPlayers;
			AvailableSlots = availableSlots;
			IsPrivate = isPrivate;
			IsLocked = isLocked;
			HasPassword = hasPassword;
			Players = players;
			Data = data;
			HostId = hostId;
			Created = created;
			LastUpdated = lastUpdated;
			Version = version;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Id != null)
			{
				text = text + "id," + Id + ",";
			}
			if (LobbyCode != null)
			{
				text = text + "lobbyCode," + LobbyCode + ",";
			}
			if (Upid != null)
			{
				text = text + "upid," + Upid + ",";
			}
			if (EnvironmentId != null)
			{
				text = text + "environmentId," + EnvironmentId + ",";
			}
			if (Name != null)
			{
				text = text + "name," + Name + ",";
			}
			text = text + "maxPlayers," + MaxPlayers + ",";
			text = text + "availableSlots," + AvailableSlots + ",";
			text = text + "isPrivate," + IsPrivate + ",";
			text = text + "isLocked," + IsLocked + ",";
			text = text + "hasPassword," + HasPassword + ",";
			if (Players != null)
			{
				text = text + "players," + Players.ToString() + ",";
			}
			if (Data != null)
			{
				text = text + "data," + Data.ToString() + ",";
			}
			if (HostId != null)
			{
				text = text + "hostId," + HostId + ",";
			}
			_ = Created;
			text = text + "created," + Created.ToString() + ",";
			_ = LastUpdated;
			text = text + "lastUpdated," + LastUpdated.ToString() + ",";
			return text + "version," + Version;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Id != null)
			{
				string value = Id.ToString();
				dictionary.Add("id", value);
			}
			if (LobbyCode != null)
			{
				string value2 = LobbyCode.ToString();
				dictionary.Add("lobbyCode", value2);
			}
			if (Upid != null)
			{
				string value3 = Upid.ToString();
				dictionary.Add("upid", value3);
			}
			if (EnvironmentId != null)
			{
				string value4 = EnvironmentId.ToString();
				dictionary.Add("environmentId", value4);
			}
			if (Name != null)
			{
				string value5 = Name.ToString();
				dictionary.Add("name", value5);
			}
			string value6 = MaxPlayers.ToString();
			dictionary.Add("maxPlayers", value6);
			string value7 = AvailableSlots.ToString();
			dictionary.Add("availableSlots", value7);
			string value8 = IsPrivate.ToString();
			dictionary.Add("isPrivate", value8);
			string value9 = IsLocked.ToString();
			dictionary.Add("isLocked", value9);
			string value10 = HasPassword.ToString();
			dictionary.Add("hasPassword", value10);
			if (HostId != null)
			{
				string value11 = HostId.ToString();
				dictionary.Add("hostId", value11);
			}
			_ = Created;
			string value12 = Created.ToString();
			dictionary.Add("created", value12);
			_ = LastUpdated;
			string value13 = LastUpdated.ToString();
			dictionary.Add("lastUpdated", value13);
			string value14 = Version.ToString();
			dictionary.Add("version", value14);
			return dictionary;
		}
	}
}
