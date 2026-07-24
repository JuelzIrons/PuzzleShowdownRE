namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "UpdateRequest")]
	public class UpdateRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "maxPlayers", EmitDefaultValue = false)]
		public int? MaxPlayers { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "isPrivate", EmitDefaultValue = true)]
		public bool? IsPrivate { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "isLocked", EmitDefaultValue = true)]
		public bool? IsLocked { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "password", EmitDefaultValue = false)]
		public string Password { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Lobbies.Http.JsonObjectCollectionConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "data", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Http.JsonObject Data { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "hostId", EmitDefaultValue = false)]
		public string HostId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public UpdateRequest(string name = null, int? maxPlayers = null, bool? isPrivate = null, bool? isLocked = null, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> data = null, string hostId = null, string password = null)
		{
			Name = name;
			MaxPlayers = maxPlayers;
			IsPrivate = isPrivate;
			IsLocked = isLocked;
			Password = password;
			Data = new global::Unity.Services.Lobbies.Http.JsonObject(data);
			HostId = hostId;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Name != null)
			{
				text = text + "name," + Name + ",";
			}
			if (MaxPlayers.HasValue)
			{
				text = text + "maxPlayers," + MaxPlayers + ",";
			}
			if (IsPrivate.HasValue)
			{
				text = text + "isPrivate," + IsPrivate + ",";
			}
			if (IsLocked.HasValue)
			{
				text = text + "isLocked," + IsLocked + ",";
			}
			if (Password != null)
			{
				text = text + "password," + Password + ",";
			}
			if (Data != null)
			{
				text = text + "data," + Data.ToString() + ",";
			}
			if (HostId != null)
			{
				text = text + "hostId," + HostId;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Name != null)
			{
				string value = Name.ToString();
				dictionary.Add("name", value);
			}
			if (MaxPlayers.HasValue)
			{
				string value2 = MaxPlayers.ToString();
				dictionary.Add("maxPlayers", value2);
			}
			if (IsPrivate.HasValue)
			{
				string value3 = IsPrivate.ToString();
				dictionary.Add("isPrivate", value3);
			}
			if (IsLocked.HasValue)
			{
				string value4 = IsLocked.ToString();
				dictionary.Add("isLocked", value4);
			}
			if (Password != null)
			{
				string value5 = Password.ToString();
				dictionary.Add("password", value5);
			}
			if (HostId != null)
			{
				string value6 = HostId.ToString();
				dictionary.Add("hostId", value6);
			}
			return dictionary;
		}
	}
}
