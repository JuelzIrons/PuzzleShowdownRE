namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class LinkWithAppleGameCenterRequest : global::Unity.Services.Authentication.LinkWithExternalTokenRequest
	{
		[global::Newtonsoft.Json.JsonProperty("appleGameCenterConfig")]
		public global::Unity.Services.Authentication.AppleGameCenterConfig AppleGameCenterConfig;

		[global::UnityEngine.Scripting.Preserve]
		internal LinkWithAppleGameCenterRequest()
		{
		}
	}
}
