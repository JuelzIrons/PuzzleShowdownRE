namespace Unity.Services.Authentication
{
	internal class SteamConfig
	{
		[global::Newtonsoft.Json.JsonProperty("identity")]
		public string identity;

		[global::Newtonsoft.Json.JsonProperty("appId")]
		[global::JetBrains.Annotations.CanBeNull]
		public string appId;

		[global::UnityEngine.Scripting.Preserve]
		internal SteamConfig()
		{
		}
	}
}
