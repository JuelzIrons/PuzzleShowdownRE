namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "JoinByIdRequest")]
	public class JoinByIdRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "password", EmitDefaultValue = false)]
		public string Password { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "player", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Models.Player Player { get; }

		[global::UnityEngine.Scripting.Preserve]
		public JoinByIdRequest(string password = null, global::Unity.Services.Lobbies.Models.Player player = null)
		{
			Password = password;
			Player = player;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
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
			if (Password != null)
			{
				string value = Password.ToString();
				dictionary.Add("password", value);
			}
			return dictionary;
		}
	}
}
