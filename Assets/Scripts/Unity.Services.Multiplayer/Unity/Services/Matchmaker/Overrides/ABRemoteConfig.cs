namespace Unity.Services.Matchmaker.Overrides
{
	internal class ABRemoteConfig : global::Unity.Services.Matchmaker.Overrides.IABRemoteConfig
	{
		private const string StagingEnvironment = "staging";

		private const string ConfigType = "matchmaker";

		private const string StagingURL = "https://remote-config-stg.uca.cloud.unity3d.com/api/v1/settings";

		private const string ProdURL = "https://config.unity3d.com/api/v1/settings";

		private string _projectId;

		private string _environmentId;

		private readonly global::Unity.Services.Matchmaker.Http.IHttpClient _client;

		private readonly string _userId;

		private readonly string _url;

		private bool _needRefresh = true;

		public global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Override> Overrides { get; private set; }

		public string AssignmentId { get; private set; }

		public ABRemoteConfig(global::Unity.Services.Matchmaker.Http.IHttpClient client, string installationId, string cloudEnvironment, string projectId, string environmentId)
		{
			Overrides = new global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Override>();
			_client = client;
			_userId = installationId;
			_projectId = projectId;
			_environmentId = environmentId;
			_url = ((cloudEnvironment == "staging") ? "https://remote-config-stg.uca.cloud.unity3d.com/api/v1/settings" : "https://config.unity3d.com/api/v1/settings");
		}

		public async global::System.Threading.Tasks.Task RefreshGameOverridesAsync()
		{
			if (!_needRefresh)
			{
				return;
			}
			var value = new
			{
				projectId = _projectId,
				environmentId = _environmentId,
				userId = _userId,
				configType = "matchmaker",
				attributes = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<string, string>>
				{
					["unity"] = new global::System.Collections.Generic.Dictionary<string, string> { 
					{
						"platform",
						global::UnityEngine.Application.platform.ToString()
					} },
					["app"] = new global::System.Collections.Generic.Dictionary<string, string>(),
					["user"] = new global::System.Collections.Generic.Dictionary<string, string>()
				}
			};
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(global::Newtonsoft.Json.JsonConvert.SerializeObject(value));
			global::System.Collections.Generic.Dictionary<string, string> headers = new global::System.Collections.Generic.Dictionary<string, string>();
			global::Unity.Services.Matchmaker.Http.HttpClientResponse httpClientResponse = await _client.MakeRequestAsync("POST", _url, bytes, headers, 10);
			if (httpClientResponse.IsHttpError || httpClientResponse.IsNetworkError)
			{
				global::Unity.Services.Multiplayer.Logger.LogError($"Error while fetching overrides: code={httpClientResponse.StatusCode}, message='{httpClientResponse.ErrorMessage}'");
				return;
			}
			string text = global::System.Text.Encoding.UTF8.GetString(httpClientResponse.Data);
			global::Newtonsoft.Json.Linq.JObject jObject = global::Newtonsoft.Json.Linq.JObject.Parse(text);
			global::Newtonsoft.Json.Linq.JToken? jToken = jObject["configs"];
			if (jToken == null || jToken["matchmaker"]?.Type != global::Newtonsoft.Json.Linq.JTokenType.Object)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Error while fetching overrides: 'matchmaker' missing in " + text);
				return;
			}
			global::Newtonsoft.Json.Linq.JToken? jToken2 = jObject["metadata"];
			if (jToken2 == null || jToken2["assignmentId"]?.Type != global::Newtonsoft.Json.Linq.JTokenType.String)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Error while fetching overrides: 'assignmentId' missing in " + text);
				return;
			}
			AssignmentId = global::Newtonsoft.Json.Linq.Extensions.Value<string>(jObject["metadata"]?["assignmentId"]);
			foreach (global::Newtonsoft.Json.Linq.JProperty item in (global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)jObject["configs"]["matchmaker"])
			{
				if (item == null)
				{
					continue;
				}
				global::Newtonsoft.Json.Linq.JToken jToken3 = item.Value["variantId"];
				if (jToken3 != null)
				{
					global::Newtonsoft.Json.Linq.JToken jToken4 = item.Value["overrideId"];
					if (jToken4 != null)
					{
						Overrides.Add(new global::Unity.Services.Matchmaker.Models.Override(item.Name, global::Newtonsoft.Json.Linq.Extensions.Value<string>(jToken3), global::Newtonsoft.Json.Linq.Extensions.Value<string>(jToken4)));
					}
				}
			}
			_needRefresh = false;
		}
	}
}
