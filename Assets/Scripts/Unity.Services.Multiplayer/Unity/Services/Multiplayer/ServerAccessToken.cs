namespace Unity.Services.Multiplayer
{
	internal class ServerAccessToken : global::Unity.Services.Multiplayer.BaseJwt
	{
		[global::Newtonsoft.Json.JsonProperty("aud")]
		public string[] Audience;

		[global::Newtonsoft.Json.JsonProperty("iss")]
		public string Issuer;

		[global::Newtonsoft.Json.JsonProperty("jti")]
		public string JwtId;

		[global::Newtonsoft.Json.JsonProperty("scopes")]
		public string[] Scope;

		[global::Newtonsoft.Json.JsonProperty("sub")]
		public string Subject;

		[global::UnityEngine.Scripting.Preserve]
		public ServerAccessToken()
		{
		}
	}
}
