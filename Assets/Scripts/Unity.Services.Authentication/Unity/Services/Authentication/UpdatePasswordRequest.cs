namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class UpdatePasswordRequest
	{
		[global::Newtonsoft.Json.JsonProperty("password")]
		public string Password;

		[global::Newtonsoft.Json.JsonProperty("newPassword")]
		public string NewPassword;

		[global::UnityEngine.Scripting.Preserve]
		internal UpdatePasswordRequest()
		{
		}
	}
}
