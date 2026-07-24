namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class GenerateSignInCodeRequest
	{
		[global::Newtonsoft.Json.JsonProperty("identifier")]
		public string Identifier;

		[global::Newtonsoft.Json.JsonProperty("codeChallenge")]
		public string CodeChallenge;
	}
}
