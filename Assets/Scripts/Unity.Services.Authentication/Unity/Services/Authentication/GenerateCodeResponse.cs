namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class GenerateCodeResponse
	{
		[global::Newtonsoft.Json.JsonProperty("codeLinkSessionId")]
		public string CodeLinkSessionId;

		[global::Newtonsoft.Json.JsonProperty("signInCode")]
		public string SignInCode;

		[global::Newtonsoft.Json.JsonProperty("expiration")]
		public string Expiration;

		[global::UnityEngine.Scripting.Preserve]
		public GenerateCodeResponse()
		{
		}
	}
}
