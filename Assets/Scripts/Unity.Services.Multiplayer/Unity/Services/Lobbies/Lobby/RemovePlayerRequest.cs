namespace Unity.Services.Lobbies.Lobby
{
	[global::UnityEngine.Scripting.Preserve]
	internal class RemovePlayerRequest : global::Unity.Services.Lobbies.Lobby.LobbyApiBaseRequest
	{
		private string PathAndQueryParams;

		[global::UnityEngine.Scripting.Preserve]
		public string LobbyId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string PlayerId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string ServiceId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string ImpersonatedUserId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string IfMatch { get; }

		[global::UnityEngine.Scripting.Preserve]
		public RemovePlayerRequest(string lobbyId, string playerId, string serviceId = null, string impersonatedUserId = null, string ifMatch = null)
		{
			LobbyId = lobbyId;
			PlayerId = playerId;
			ServiceId = serviceId;
			ImpersonatedUserId = impersonatedUserId;
			IfMatch = ifMatch;
			PathAndQueryParams = "/" + lobbyId + "/players/" + playerId;
		}

		public string ConstructUrl(string requestBasePath)
		{
			return requestBasePath + PathAndQueryParams;
		}

		public byte[] ConstructBody()
		{
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
			string[] contentTypes = new string[0];
			string[] accepts = new string[1] { "application/problem+json" };
			string value = GenerateAcceptHeader(accepts);
			if (!string.IsNullOrEmpty(value))
			{
				dictionary.Add("Accept", value);
			}
			string text = "DELETE";
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
			if (!string.IsNullOrEmpty(IfMatch))
			{
				dictionary.Add("If-match", IfMatch);
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
