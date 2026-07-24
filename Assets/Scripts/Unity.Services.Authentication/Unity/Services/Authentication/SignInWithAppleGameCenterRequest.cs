namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class SignInWithAppleGameCenterRequest : global::Unity.Services.Authentication.SignInWithExternalTokenRequest
	{
		[global::Newtonsoft.Json.JsonProperty("appleGameCenterConfig")]
		public global::Unity.Services.Authentication.AppleGameCenterConfig AppleGameCenterConfig;

		[global::UnityEngine.Scripting.Preserve]
		internal SignInWithAppleGameCenterRequest()
		{
		}
	}
}
