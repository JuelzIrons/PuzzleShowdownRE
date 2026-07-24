namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class UnlinkRequest
	{
		[global::Newtonsoft.Json.JsonProperty("idProvider")]
		public string IdProvider;

		[global::Newtonsoft.Json.JsonProperty("externalId")]
		public string ExternalId;

		[global::UnityEngine.Scripting.Preserve]
		internal UnlinkRequest()
		{
		}
	}
}
