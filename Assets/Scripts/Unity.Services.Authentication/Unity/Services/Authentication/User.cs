namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class User
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

		[global::Newtonsoft.Json.JsonProperty("UsernameInfo")]
		[global::JetBrains.Annotations.CanBeNull]
		public global::Unity.Services.Authentication.UsernameInfo UsernameInfo;

		[global::UnityEngine.Scripting.Preserve]
		public User()
		{
		}
	}
}
