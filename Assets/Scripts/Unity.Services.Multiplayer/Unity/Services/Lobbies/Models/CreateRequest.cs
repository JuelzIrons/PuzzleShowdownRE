namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "CreateRequest")]
	public class CreateRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
		public string Name { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "maxPlayers", IsRequired = true, EmitDefaultValue = true)]
		public int MaxPlayers { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "isPrivate", EmitDefaultValue = true)]
		public bool? IsPrivate { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "isLocked", EmitDefaultValue = true)]
		public bool? IsLocked { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "player", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Models.Player Player { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "password", EmitDefaultValue = false)]
		public string Password { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "data", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> Data { get; }

		[global::UnityEngine.Scripting.Preserve]
		public CreateRequest(string name, int maxPlayers, bool? isPrivate = false, bool? isLocked = false, global::Unity.Services.Lobbies.Models.Player player = null, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject> data = null, string password = null)
		{
			Name = name;
			MaxPlayers = maxPlayers;
			IsPrivate = isPrivate;
			IsLocked = isLocked;
			Player = player;
			Password = password;
			Data = data;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Name != null)
			{
				text = text + "name," + Name + ",";
			}
			text = text + "maxPlayers," + MaxPlayers + ",";
			if (IsPrivate.HasValue)
			{
				text = text + "isPrivate," + IsPrivate + ",";
			}
			if (IsLocked.HasValue)
			{
				text = text + "isLocked," + IsLocked + ",";
			}
			if (Player != null)
			{
				text = text + "player," + Player.ToString() + ",";
			}
			if (Password != null)
			{
				text = text + "password," + Password + ",";
			}
			if (Data != null)
			{
				text = text + "data," + Data.ToString();
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
			string value2 = MaxPlayers.ToString();
			dictionary.Add("maxPlayers", value2);
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
			return dictionary;
		}
	}
}
