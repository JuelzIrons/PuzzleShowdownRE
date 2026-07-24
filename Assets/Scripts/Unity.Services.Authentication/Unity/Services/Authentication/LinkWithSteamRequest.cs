namespace Unity.Services.Authentication
{
	internal class LinkWithSteamRequest : global::Unity.Services.Authentication.LinkWithExternalTokenRequest
	{
		[global::Newtonsoft.Json.JsonProperty("steamConfig")]
		public global::Unity.Services.Authentication.SteamConfig SteamConfig;

		[global::UnityEngine.Scripting.Preserve]
		internal LinkWithSteamRequest()
		{
		}
	}
}
