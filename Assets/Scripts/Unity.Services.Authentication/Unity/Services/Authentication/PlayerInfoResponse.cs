namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class PlayerInfoResponse
	{
		[global::Newtonsoft.Json.JsonProperty("id")]
		public string Id;

		[global::Newtonsoft.Json.JsonProperty("createdAt")]
		public string CreatedAt;

		[global::Newtonsoft.Json.JsonProperty("externalIds")]
		public global::System.Collections.Generic.List<global::Unity.Services.Authentication.ExternalIdentity> ExternalIds;

		[global::Newtonsoft.Json.JsonProperty("username")]
		[global::JetBrains.Annotations.CanBeNull]
		public string Username;

		[global::Newtonsoft.Json.JsonProperty("usernamepassword")]
		[global::JetBrains.Annotations.CanBeNull]
		public global::Unity.Services.Authentication.UsernameInfo UsernamePassword;

		[global::UnityEngine.Scripting.Preserve]
		public PlayerInfoResponse()
		{
		}
	}
}
