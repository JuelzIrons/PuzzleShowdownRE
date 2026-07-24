namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class SignInWithExternalTokenRequest
	{
		[global::Newtonsoft.Json.JsonProperty("idProvider")]
		public string IdProvider;

		[global::Newtonsoft.Json.JsonProperty("token")]
		public string Token;

		[global::Newtonsoft.Json.JsonProperty("signInOnly")]
		public bool SignInOnly;

		[global::UnityEngine.Scripting.Preserve]
		internal SignInWithExternalTokenRequest()
		{
		}
	}
}
