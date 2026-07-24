namespace Unity.Services.Qos.V2.QosDiscovery
{
	[global::UnityEngine.Scripting.Preserve]
	internal class GetAllServersRequest : global::Unity.Services.Qos.V2.QosDiscovery.QosDiscoveryApiBaseRequest
	{
		private string PathAndQueryParams;

		[global::UnityEngine.Scripting.Preserve]
		public global::System.Guid XRequestId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string XUser { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string XUserType { get; }

		[global::UnityEngine.Scripting.Preserve]
		public GetAllServersRequest(global::System.Guid xRequestId = default(global::System.Guid), string xUser = null, string xUserType = null)
		{
			XRequestId = xRequestId;
			XUser = xUser;
			XUserType = xUserType;
			PathAndQueryParams = "/v2alpha1/servers";
		}

		public string ConstructUrl(string requestBasePath)
		{
			return requestBasePath + PathAndQueryParams;
		}

		public byte[] ConstructBody()
		{
			return null;
		}

		public global::System.Collections.Generic.Dictionary<string, string> ConstructHeaders(global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Qos.V2.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (!string.IsNullOrEmpty(accessToken.AccessToken))
			{
				dictionary.Add("authorization", "Bearer " + accessToken.AccessToken);
			}
			dictionary.Add("Unity-Client-Version", global::UnityEngine.Application.unityVersion);
			dictionary.Add("Unity-Client-Mode", global::Unity.Services.Qos.V2.Scheduler.EngineStateHelper.IsPlaying ? "play" : "edit");
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
			if (!XRequestId.Equals(default(global::System.Guid)))
			{
				dictionary.Add("X-Request-Id", XRequestId.ToString());
			}
			if (!string.IsNullOrEmpty(XUser))
			{
				dictionary.Add("X-User", XUser);
			}
			if (!string.IsNullOrEmpty(XUserType))
			{
				dictionary.Add("X-User-Type", XUserType);
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
