namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class SignInWithCodeRequest
	{
		[global::Newtonsoft.Json.JsonProperty("codeLinkSessionId")]
		public string CodeLinkSessionId;

		[global::Newtonsoft.Json.JsonProperty("codeVerifier")]
		public string CodeVerifier;
	}
}
