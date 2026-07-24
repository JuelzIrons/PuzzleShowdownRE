namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class UnlinkResponse
	{
		[global::Newtonsoft.Json.JsonProperty("user")]
		public global::Unity.Services.Authentication.User User;

		[global::UnityEngine.Scripting.Preserve]
		public UnlinkResponse()
		{
		}
	}
}
