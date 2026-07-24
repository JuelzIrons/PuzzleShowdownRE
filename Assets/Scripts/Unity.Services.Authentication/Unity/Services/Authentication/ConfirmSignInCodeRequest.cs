namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class ConfirmSignInCodeRequest
	{
		[global::Newtonsoft.Json.JsonProperty("signInCode")]
		public string SignInCode;

		[global::Newtonsoft.Json.JsonProperty("idProvider")]
		public string IdProvider;

		[global::Newtonsoft.Json.JsonProperty("externalToken")]
		public string ExternalToken;

		[global::Newtonsoft.Json.JsonProperty("sessionToken")]
		public string SessionToken;
	}
}
