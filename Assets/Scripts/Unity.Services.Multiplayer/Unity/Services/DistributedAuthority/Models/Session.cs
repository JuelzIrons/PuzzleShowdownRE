namespace Unity.Services.DistributedAuthority.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "Session")]
	internal class Session
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "lobbyId", IsRequired = true, EmitDefaultValue = true)]
		public string LobbyId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "region", EmitDefaultValue = false)]
		public string Region { get; }

		[global::UnityEngine.Scripting.Preserve]
		public Session(string lobbyId, string region = null)
		{
			LobbyId = lobbyId;
			Region = region;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (LobbyId != null)
			{
				text = text + "lobbyId," + LobbyId + ",";
			}
			if (Region != null)
			{
				text = text + "region," + Region;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (LobbyId != null)
			{
				string value = LobbyId.ToString();
				dictionary.Add("lobbyId", value);
			}
			if (Region != null)
			{
				string value2 = Region.ToString();
				dictionary.Add("region", value2);
			}
			return dictionary;
		}
	}
}
