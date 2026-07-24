namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class SignInWithOculusRequest : global::Unity.Services.Authentication.SignInWithExternalTokenRequest
	{
		[global::Newtonsoft.Json.JsonProperty("oculusConfig")]
		public global::Unity.Services.Authentication.OculusConfig OculusConfig;

		[global::UnityEngine.Scripting.Preserve]
		internal SignInWithOculusRequest()
		{
		}
	}
}
