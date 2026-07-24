namespace Unity.Services.Qos.QosDiscovery
{
	[global::UnityEngine.Scripting.Preserve]
	internal class GetServersRequest : global::Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest
	{
		private string PathAndQueryParams;

		[global::UnityEngine.Scripting.Preserve]
		public global::System.Collections.Generic.List<string> Region { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string Service { get; }

		[global::UnityEngine.Scripting.Preserve]
		public GetServersRequest(global::System.Collections.Generic.List<string> region = null, string service = null)
		{
			Region = region;
			Service = service;
			PathAndQueryParams = "/v1/servers";
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			if (Region != null)
			{
				global::System.Collections.Generic.List<string> values = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(Region, (string v) => v.ToString()));
				list = AddParamsToQueryParams(list, "region", values, "form", explode: true);
			}
			if (!string.IsNullOrEmpty(Service))
			{
				list = AddParamsToQueryParams(list, "service", Service);
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

		public global::System.Collections.Generic.Dictionary<string, string> ConstructHeaders(global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Qos.Configuration operationConfiguration = null)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (!string.IsNullOrEmpty(accessToken.AccessToken))
			{
				dictionary.Add("authorization", "Bearer " + accessToken.AccessToken);
			}
			dictionary.Add("Unity-Client-Version", global::UnityEngine.Application.unityVersion);
			dictionary.Add("Unity-Client-Mode", global::Unity.Services.Qos.Scheduler.EngineStateHelper.IsPlaying ? "play" : "edit");
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
