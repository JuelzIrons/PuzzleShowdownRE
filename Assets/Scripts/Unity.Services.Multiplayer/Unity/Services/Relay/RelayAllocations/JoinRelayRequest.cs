namespace Unity.Services.Relay.RelayAllocations
{
	[global::UnityEngine.Scripting.Preserve]
	internal class JoinRelayRequest : global::Unity.Services.Relay.RelayAllocations.RelayAllocationsApiBaseRequest
	{
		private string PathAndQueryParams;

		[global::UnityEngine.Scripting.Preserve]
		public global::Unity.Services.Relay.Models.JoinRequest JoinRequest { get; }

		[global::UnityEngine.Scripting.Preserve]
		public JoinRelayRequest(global::Unity.Services.Relay.Models.JoinRequest joinRequest)
		{
			JoinRequest = joinRequest;
			PathAndQueryParams = "/v1/join";
		}

		public string ConstructUrl(string requestBasePath)
		{
			return requestBasePath + PathAndQueryParams;
		}

		public byte[] ConstructBody()
		{
			return ConstructBody(JoinRequest);
		}

		public global::System.Collections.Generic.Dictionary<string, string> ConstructHeaders(global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Relay.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (!string.IsNullOrEmpty(accessToken.AccessToken))
			{
				dictionary.Add("authorization", "Bearer " + accessToken.AccessToken);
			}
			dictionary.Add("Unity-Client-Version", global::UnityEngine.Application.unityVersion);
			dictionary.Add("Unity-Client-Mode", global::Unity.Services.Relay.Scheduler.EngineStateHelper.IsPlaying ? "play" : "edit");
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
