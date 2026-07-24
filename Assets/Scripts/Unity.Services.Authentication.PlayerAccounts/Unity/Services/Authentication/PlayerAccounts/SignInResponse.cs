namespace Unity.Services.Authentication.PlayerAccounts
{
	[global::System.Serializable]
	internal class SignInResponse
	{
		[global::Newtonsoft.Json.JsonProperty("passport")]
		public string PassportId;

		[global::Newtonsoft.Json.JsonProperty("userId")]
		public string UserId;

		[global::Newtonsoft.Json.JsonProperty("access_token")]
		public string AccessToken;

		[global::Newtonsoft.Json.JsonProperty("id_token")]
		public string IdToken;

		[global::Newtonsoft.Json.JsonProperty("token_type")]
		public string tokenType;

		[global::Newtonsoft.Json.JsonProperty("expires_in")]
		public int ExpiresIn;

		[global::Newtonsoft.Json.JsonProperty("refresh_token")]
		public string RefreshToken;

		[global::UnityEngine.Scripting.Preserve]
		public SignInResponse()
		{
		}
	}
}
