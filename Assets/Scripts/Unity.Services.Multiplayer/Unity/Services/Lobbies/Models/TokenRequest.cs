namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "TokenRequest")]
	public class TokenRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		public enum TokenTypeOptions
		{
			[global::System.Runtime.Serialization.EnumMember(Value = "vivoxJoin")]
			VivoxJoin = 1,
			[global::System.Runtime.Serialization.EnumMember(Value = "wireJoin")]
			WireJoin = 2
		}

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "tokenType", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Lobbies.Models.TokenRequest.TokenTypeOptions TokenType { get; }

		[global::UnityEngine.Scripting.Preserve]
		public TokenRequest(global::Unity.Services.Lobbies.Models.TokenRequest.TokenTypeOptions tokenType)
		{
			TokenType = tokenType;
		}

		internal string SerializeAsPathParam()
		{
			return "" + "tokenType," + TokenType;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			string value = TokenType.ToString();
			dictionary.Add("tokenType", value);
			return dictionary;
		}
	}
}
