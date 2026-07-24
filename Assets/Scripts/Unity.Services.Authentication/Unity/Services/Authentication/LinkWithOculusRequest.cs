namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class LinkWithOculusRequest : global::Unity.Services.Authentication.LinkWithExternalTokenRequest
	{
		[global::Newtonsoft.Json.JsonProperty("oculusConfig")]
		public global::Unity.Services.Authentication.OculusConfig OculusConfig;

		[global::UnityEngine.Scripting.Preserve]
		internal LinkWithOculusRequest()
		{
		}
	}
}
