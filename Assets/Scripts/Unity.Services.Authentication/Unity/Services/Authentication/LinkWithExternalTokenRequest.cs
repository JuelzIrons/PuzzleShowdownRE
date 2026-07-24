namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class LinkWithExternalTokenRequest
	{
		[global::Newtonsoft.Json.JsonProperty("idProvider")]
		public string IdProvider;

		[global::Newtonsoft.Json.JsonProperty("token")]
		public string Token;

		[global::Newtonsoft.Json.JsonProperty("forceLink")]
		public bool ForceLink;

		[global::UnityEngine.Scripting.Preserve]
		internal LinkWithExternalTokenRequest()
		{
		}
	}
}
