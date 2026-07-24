namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "JoinByCodeRequest")]
	public class JoinByCodeRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "lobbyCode", IsRequired = true, EmitDefaultValue = true)]
		public string LobbyCode { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "password", EmitDefaultValue = false)]
		public string Password { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "player", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Models.Player Player { get; }

		[global::UnityEngine.Scripting.Preserve]
		public JoinByCodeRequest(string lobbyCode, global::Unity.Services.Lobbies.Models.Player player = null, string password = null)
		{
			LobbyCode = lobbyCode;
			Password = password;
			Player = player;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (LobbyCode != null)
			{
				text = text + "lobbyCode," + LobbyCode + ",";
			}
			if (Password != null)
			{
				text = text + "password," + Password + ",";
			}
			if (Player != null)
			{
				text = text + "player," + Player.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (LobbyCode != null)
			{
				string value = LobbyCode.ToString();
				dictionary.Add("lobbyCode", value);
			}
			if (Password != null)
			{
				string value2 = Password.ToString();
				dictionary.Add("password", value2);
			}
			return dictionary;
		}
	}
}
