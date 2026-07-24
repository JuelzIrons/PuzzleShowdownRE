namespace Unity.Services.Authentication.PlayerAccounts
{
	public class IdToken : global::Unity.Services.Authentication.PlayerAccounts.BaseJwt
	{
		[global::Newtonsoft.Json.JsonProperty("aud")]
		public string[] Audience;

		[global::Newtonsoft.Json.JsonProperty("iss")]
		public string Issuer;

		[global::Newtonsoft.Json.JsonProperty("jti")]
		public string JwtId;

		[global::Newtonsoft.Json.JsonProperty("sub")]
		public string Subject;

		[global::Newtonsoft.Json.JsonProperty("email")]
		public string Email { get; set; }

		[global::Newtonsoft.Json.JsonProperty("email_verified")]
		public bool EmailVerified { get; set; }

		[global::Newtonsoft.Json.JsonProperty("is_private_email")]
		public bool IsPrivateEmail { get; set; }

		[global::Newtonsoft.Json.JsonProperty("nonce")]
		public string Nonce { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		internal IdToken()
		{
		}
	}
}
