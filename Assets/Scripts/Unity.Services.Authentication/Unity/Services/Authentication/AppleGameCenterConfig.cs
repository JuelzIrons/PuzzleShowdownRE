namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class AppleGameCenterConfig
	{
		[global::Newtonsoft.Json.JsonProperty("teamPlayerId")]
		public string TeamPlayerId;

		[global::Newtonsoft.Json.JsonProperty("publicKeyUrl")]
		public string PublicKeyURL;

		[global::Newtonsoft.Json.JsonProperty("salt")]
		public string Salt;

		[global::Newtonsoft.Json.JsonProperty("timestamp")]
		public ulong Timestamp;

		[global::UnityEngine.Scripting.Preserve]
		internal AppleGameCenterConfig()
		{
		}
	}
}
