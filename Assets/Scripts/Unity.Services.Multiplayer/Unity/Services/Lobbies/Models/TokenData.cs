namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "TokenData")]
	public class TokenData
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "tokenValue", EmitDefaultValue = false)]
		public string TokenValue { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "uri", EmitDefaultValue = false)]
		public string Uri { get; }

		[global::UnityEngine.Scripting.Preserve]
		public TokenData(string tokenValue = null, string uri = null)
		{
			TokenValue = tokenValue;
			Uri = uri;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (TokenValue != null)
			{
				text = text + "tokenValue," + TokenValue + ",";
			}
			if (Uri != null)
			{
				text = text + "uri," + Uri;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (TokenValue != null)
			{
				string value = TokenValue.ToString();
				dictionary.Add("tokenValue", value);
			}
			if (Uri != null)
			{
				string value2 = Uri.ToString();
				dictionary.Add("uri", value2);
			}
			return dictionary;
		}
	}
}
