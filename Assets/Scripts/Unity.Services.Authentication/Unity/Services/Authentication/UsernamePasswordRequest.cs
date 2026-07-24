namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class UsernamePasswordRequest
	{
		[global::Newtonsoft.Json.JsonProperty("username")]
		public string Username;

		[global::Newtonsoft.Json.JsonProperty("password")]
		public string Password;

		[global::UnityEngine.Scripting.Preserve]
		internal UsernamePasswordRequest()
		{
		}
	}
}
