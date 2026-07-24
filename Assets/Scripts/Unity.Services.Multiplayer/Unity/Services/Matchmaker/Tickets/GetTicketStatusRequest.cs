namespace Unity.Services.Matchmaker.Tickets
{
	[global::UnityEngine.Scripting.Preserve]
	internal class GetTicketStatusRequest : global::Unity.Services.Matchmaker.Tickets.TicketsApiBaseRequest
	{
		private string PathAndQueryParams;

		[global::UnityEngine.Scripting.Preserve]
		public string Id { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string ImpersonatedUserId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public GetTicketStatusRequest(string id, string impersonatedUserId = null)
		{
			Id = id;
			ImpersonatedUserId = impersonatedUserId;
			PathAndQueryParams = "/v2/tickets/status";
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			if (!string.IsNullOrEmpty(Id))
			{
				list = AddParamsToQueryParams(list, "id", Id);
			}
			if (list.Count > 0)
			{
				PathAndQueryParams = PathAndQueryParams + "?" + string.Join("&", list);
			}
		}

		public string ConstructUrl(string requestBasePath)
		{
			return requestBasePath + PathAndQueryParams;
		}

		public byte[] ConstructBody()
		{
			return null;
		}

		public global::System.Collections.Generic.Dictionary<string, string> ConstructHeaders(global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (!string.IsNullOrEmpty(accessToken.AccessToken))
			{
				dictionary.Add("authorization", "Bearer " + accessToken.AccessToken);
			}
			dictionary.Add("Unity-Client-Version", global::UnityEngine.Application.unityVersion);
			dictionary.Add("Unity-Client-Mode", global::Unity.Services.Matchmaker.Scheduler.EngineStateHelper.IsPlaying ? "play" : "edit");
			string[] contentTypes = new string[0];
			string[] accepts = new string[1] { "application/json" };
			string value = GenerateAcceptHeader(accepts);
			if (!string.IsNullOrEmpty(value))
			{
				dictionary.Add("Accept", value);
			}
			string text = "GET";
			string value2 = GenerateContentTypeHeader(contentTypes);
			if (!string.IsNullOrEmpty(value2))
			{
				dictionary.Add("Content-Type", value2);
			}
			else if (text == "POST" || text == "PATCH")
			{
				dictionary.Add("Content-Type", "application/json");
			}
			if (!string.IsNullOrEmpty(ImpersonatedUserId))
			{
				dictionary.Add("Impersonated-user-id", ImpersonatedUserId);
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
