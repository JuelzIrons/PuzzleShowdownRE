namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class SignInWithSteamRequest : global::Unity.Services.Authentication.SignInWithExternalTokenRequest
	{
		[global::Newtonsoft.Json.JsonProperty("steamConfig")]
		public global::Unity.Services.Authentication.SteamConfig SteamConfig;

		[global::UnityEngine.Scripting.Preserve]
		internal SignInWithSteamRequest()
		{
		}
	}
}
