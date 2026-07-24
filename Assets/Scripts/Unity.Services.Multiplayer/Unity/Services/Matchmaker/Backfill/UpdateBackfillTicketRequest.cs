namespace Unity.Services.Matchmaker.Backfill
{
	[global::UnityEngine.Scripting.Preserve]
	internal class UpdateBackfillTicketRequest : global::Unity.Services.Matchmaker.Backfill.BackfillApiBaseRequest
	{
		private string PathAndQueryParams;

		[global::UnityEngine.Scripting.Preserve]
		public string Id { get; }

		[global::UnityEngine.Scripting.Preserve]
		public global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket BackfillTicket { get; }

		[global::UnityEngine.Scripting.Preserve]
		public UpdateBackfillTicketRequest(string id, global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket backfillTicket = null)
		{
			Id = id;
			BackfillTicket = backfillTicket;
			PathAndQueryParams = "/v2/backfill/" + id;
		}

		public string ConstructUrl(string requestBasePath)
		{
			return requestBasePath + PathAndQueryParams;
		}

		public byte[] ConstructBody()
		{
			if (BackfillTicket != null)
			{
				return ConstructBody(BackfillTicket);
			}
			return null;
		}

		public global::System.Collections.Generic.Dictionary<string, string> ConstructHeaders(string accessToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (!string.IsNullOrEmpty(accessToken))
			{
				dictionary.Add("authorization", "Bearer " + accessToken);
			}
			dictionary.Add("Unity-Client-Version", global::UnityEngine.Application.unityVersion);
			dictionary.Add("Unity-Client-Mode", global::Unity.Services.Matchmaker.Scheduler.EngineStateHelper.IsPlaying ? "play" : "edit");
			string[] contentTypes = new string[1] { "application/json" };
			string[] accepts = new string[1] { "application/json" };
			string value = GenerateAcceptHeader(accepts);
			if (!string.IsNullOrEmpty(value))
			{
				dictionary.Add("Accept", value);
			}
			string text = "PUT";
			string value2 = GenerateContentTypeHeader(contentTypes);
			if (!string.IsNullOrEmpty(value2))
			{
				dictionary.Add("Content-Type", value2);
			}
			else if (text == "POST" || text == "PATCH")
			{
				dictionary.Add("Content-Type", "application/json");
			}
			if (operationConfiguration != null && operationConfiguration.Headers != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> header in operationConfiguration.Headers)
				{
					dictionary[header.Key] = header.Value;
				}
			}
			return dictionary;
		}
	}
}
