namespace Unity.Services.Authentication
{
	internal class AccessToken : global::Unity.Services.Authentication.BaseJwt
	{
		[global::Newtonsoft.Json.JsonProperty("aud")]
		public string[] Audience;

		[global::Newtonsoft.Json.JsonProperty("client_id")]
		public string ClientId;

		[global::Newtonsoft.Json.JsonProperty("ext")]
		public global::Unity.Services.Authentication.AccessTokenExtraClaims Extra;

		[global::Newtonsoft.Json.JsonProperty("iat")]
		public long IssuedAt;

		[global::Newtonsoft.Json.JsonProperty("iss")]
		public string Issuer;

		[global::Newtonsoft.Json.JsonProperty("jti")]
		public string JwtId;

		[global::Newtonsoft.Json.JsonProperty("project_id")]
		public string ProjectId;

		[global::Newtonsoft.Json.JsonProperty("scp")]
		public string[] Scope;

		[global::Newtonsoft.Json.JsonProperty("sub")]
		public string Subject;

		[global::Newtonsoft.Json.JsonProperty("sign_in_provider")]
		public string SignInProvider;

		[global::Newtonsoft.Json.JsonProperty("exp")]
		public long Expiration;

		[global::UnityEngine.Scripting.Preserve]
		public AccessToken()
		{
		}
	}
}
