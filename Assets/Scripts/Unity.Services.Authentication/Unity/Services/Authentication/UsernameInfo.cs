namespace Unity.Services.Authentication
{
	internal class UsernameInfo
	{
		[global::Newtonsoft.Json.JsonProperty("username")]
		public string Username;

		[global::Newtonsoft.Json.JsonProperty("createdAt")]
		public string CreatedAt;

		[global::Newtonsoft.Json.JsonProperty("lastLoginAt")]
		public string LastLoginAt;

		[global::Newtonsoft.Json.JsonProperty("passwordUpdatedAt")]
		public string PasswordUpdatedAt;

		[global::UnityEngine.Scripting.Preserve]
		public UsernameInfo()
		{
		}
	}
}
