namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class CodeLinkInfoResponse
	{
		[global::Newtonsoft.Json.JsonProperty("identifier")]
		public string Identifier;

		[global::Newtonsoft.Json.JsonProperty("expiration")]
		public string Expiration;

		[global::UnityEngine.Scripting.Preserve]
		public CodeLinkInfoResponse()
		{
		}
	}
}
