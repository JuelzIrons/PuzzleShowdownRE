namespace Unity.Services.Authentication.PlayerAccounts
{
	[global::System.Serializable]
	internal class PlayerAccountsErrorResponse
	{
		[global::Newtonsoft.Json.JsonProperty("error")]
		public string Error;

		[global::Newtonsoft.Json.JsonProperty("error_description")]
		public string Description;

		[global::UnityEngine.Scripting.Preserve]
		public PlayerAccountsErrorResponse()
		{
		}
	}
}
