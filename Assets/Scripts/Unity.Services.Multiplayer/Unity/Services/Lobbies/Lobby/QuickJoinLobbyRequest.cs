namespace Unity.Services.Lobbies.Lobby
{
	[global::UnityEngine.Scripting.Preserve]
	internal class QuickJoinLobbyRequest : global::Unity.Services.Lobbies.Lobby.LobbyApiBaseRequest
	{
		private string PathAndQueryParams;

		[global::UnityEngine.Scripting.Preserve]
		public string ServiceId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string ImpersonatedUserId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public global::Unity.Services.Lobbies.Models.QuickJoinRequest QuickJoinRequest { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QuickJoinLobbyRequest(string serviceId = null, string impersonatedUserId = null, global::Unity.Services.Lobbies.Models.QuickJoinRequest quickJoinRequest = null)
		{
			ServiceId = serviceId;
			ImpersonatedUserId = impersonatedUserId;
			QuickJoinRequest = quickJoinRequest;
			PathAndQueryParams = "/quickjoin";
		}

		public string ConstructUrl(string requestBasePath)
		{
			return requestBasePath + PathAndQueryParams;
		}

		public byte[] ConstructBody()
		{
			if (QuickJoinRequest != null)
			{
				return ConstructBody(QuickJoinRequest);
			}
			return null;
		}

		public global::System.Collections.Generic.Dictionary<string, string> ConstructHeaders(global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Lobbies.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (!string.IsNullOrEmpty(accessToken.AccessToken))
			{
				dictionary.Add("authorization", "Bearer " + accessToken.AccessToken);
			}
			dictionary.Add("Unity-Client-Version", global::UnityEngine.Application.unityVersion);
			dictionary.Add("Unity-Client-Mode", global::Unity.Services.Lobbies.Scheduler.EngineStateHelper.IsPlaying ? "play" : "edit");
			string[] contentTypes = new string[1] { "application/json" };
			string[] accepts = new string[2] { "application/json", "application/problem+json" };
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
			if (!string.IsNullOrEmpty(ServiceId))
			{
				dictionary.Add("Service-id", ServiceId);
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
