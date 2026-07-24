namespace Unity.Services.Matchmaker.Tickets
{
	[global::UnityEngine.Scripting.Preserve]
	internal class CreateTicketRequest : global::Unity.Services.Matchmaker.Tickets.TicketsApiBaseRequest
	{
		private string PathAndQueryParams;

		[global::UnityEngine.Scripting.Preserve]
		public string ImpersonatedUserId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public global::Unity.Services.Matchmaker.Models.CreateTicketRequest CreateTicketRequestParameter { get; }

		[global::UnityEngine.Scripting.Preserve]
		public CreateTicketRequest(string impersonatedUserId = null, global::Unity.Services.Matchmaker.Models.CreateTicketRequest createTicketRequestParameter = null)
		{
			ImpersonatedUserId = impersonatedUserId;
			CreateTicketRequestParameter = createTicketRequestParameter;
			PathAndQueryParams = "/v2/tickets";
		}

		public string ConstructUrl(string requestBasePath)
		{
			return requestBasePath + PathAndQueryParams;
		}

		public byte[] ConstructBody()
		{
			if (CreateTicketRequestParameter != null)
			{
				return ConstructBody(CreateTicketRequestParameter);
			}
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
			string[] contentTypes = new string[1] { "application/json" };
			string[] accepts = new string[1] { "application/json" };
			string value = GenerateAcceptHeader(accepts);
			if (!string.IsNullOrEmpty(value))
			{
				dictionary.Add("Accept", value);
			}
			string text = "POST";
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
