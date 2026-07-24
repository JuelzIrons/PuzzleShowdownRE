namespace Unity.Services.Lobbies.Lobby
{
	[global::UnityEngine.Scripting.Preserve]
	internal class GetLobbyRequest : global::Unity.Services.Lobbies.Lobby.LobbyApiBaseRequest
	{
		private string PathAndQueryParams;

		[global::UnityEngine.Scripting.Preserve]
		public string LobbyId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string ServiceId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string ImpersonatedUserId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string IfNoneMatch { get; }

		[global::UnityEngine.Scripting.Preserve]
		public GetLobbyRequest(string lobbyId, string serviceId = null, string impersonatedUserId = null, string ifNoneMatch = null)
		{
			LobbyId = lobbyId;
			ServiceId = serviceId;
			ImpersonatedUserId = impersonatedUserId;
			IfNoneMatch = ifNoneMatch;
			PathAndQueryParams = "/" + lobbyId;
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
			string[] accepts = new string[2] { "application/json", "application/problem+json" };
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
			if (!string.IsNullOrEmpty(ServiceId))
			{
				dictionary.Add("Service-id", ServiceId);
			}
			if (!string.IsNullOrEmpty(ImpersonatedUserId))
			{
				dictionary.Add("Impersonated-user-id", ImpersonatedUserId);
			}
			if (!string.IsNullOrEmpty(IfNoneMatch))
			{
				dictionary.Add("If-none-match", IfNoneMatch);
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
