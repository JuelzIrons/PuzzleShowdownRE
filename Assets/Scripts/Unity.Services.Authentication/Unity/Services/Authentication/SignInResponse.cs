namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class SignInResponse
	{
		[global::Newtonsoft.Json.JsonProperty("userId")]
		public string UserId;

		[global::Newtonsoft.Json.JsonProperty("idToken")]
		public string IdToken;

		[global::Newtonsoft.Json.JsonProperty("sessionToken")]
		public string SessionToken;

		[global::Newtonsoft.Json.JsonProperty("expiresIn")]
		public int ExpiresIn;

		[global::Newtonsoft.Json.JsonProperty("user")]
		public global::Unity.Services.Authentication.User User;

		[global::Newtonsoft.Json.JsonProperty("lastNotificationDate")]
		[global::JetBrains.Annotations.CanBeNull]
		public string LastNotificationDate;

		[global::UnityEngine.Scripting.Preserve]
		public SignInResponse()
		{
		}
	}
}
