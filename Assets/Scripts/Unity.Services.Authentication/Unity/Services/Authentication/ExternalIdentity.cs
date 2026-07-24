namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class ExternalIdentity
	{
		[global::Newtonsoft.Json.JsonProperty("providerId")]
		public string ProviderId;

		[global::Newtonsoft.Json.JsonProperty("externalId")]
		public string ExternalId;

		[global::UnityEngine.Scripting.Preserve]
		public ExternalIdentity()
		{
		}
	}
}
