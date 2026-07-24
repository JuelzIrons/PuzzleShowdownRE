namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class NotificationResponse
	{
		[global::Newtonsoft.Json.JsonProperty("id")]
		public string Id;

		[global::Newtonsoft.Json.JsonProperty("caseID")]
		public string CaseId;

		[global::Newtonsoft.Json.JsonProperty("message")]
		public string Message;

		[global::Newtonsoft.Json.JsonProperty("playerId")]
		public string PlayerId;

		[global::Newtonsoft.Json.JsonProperty("projectId")]
		public string ProjectId;

		[global::Newtonsoft.Json.JsonProperty("type")]
		public string Type;

		[global::Newtonsoft.Json.JsonProperty("createdAt")]
		public string CreatedAt;

		[global::UnityEngine.Scripting.Preserve]
		public NotificationResponse()
		{
		}
	}
}
