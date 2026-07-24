namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class AuthenticationErrorResponse
	{
		[global::Newtonsoft.Json.JsonProperty("title")]
		public string Title;

		[global::Newtonsoft.Json.JsonProperty("detail")]
		public string Detail;

		[global::Newtonsoft.Json.JsonProperty("status")]
		public int Status;

		[global::Newtonsoft.Json.JsonProperty("details")]
		public global::System.Collections.Generic.List<object> Details;

		[global::UnityEngine.Scripting.Preserve]
		public AuthenticationErrorResponse()
		{
		}
	}
}
