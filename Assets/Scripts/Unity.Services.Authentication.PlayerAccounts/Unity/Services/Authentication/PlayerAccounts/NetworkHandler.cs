namespace Unity.Services.Authentication.PlayerAccounts
{
	internal class NetworkHandler : global::Unity.Services.Authentication.PlayerAccounts.INetworkHandler
	{
		public static class ContentType
		{
			public const string Json = "application/json";
		}

		private readonly global::Newtonsoft.Json.JsonSerializerSettings m_JsonSerializerSettings = new global::Newtonsoft.Json.JsonSerializerSettings();

		private global::Unity.Services.Authentication.PlayerAccounts.INetworkConfiguration Configuration { get; }

		public NetworkHandler()
		{
			Configuration = new global::Unity.Services.Authentication.PlayerAccounts.NetworkConfiguration();
		}

		public global::System.Threading.Tasks.Task<T> GetAsync<T>(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			return new global::Unity.Services.Authentication.PlayerAccounts.WebRequest(Configuration, global::Unity.Services.Authentication.PlayerAccounts.WebRequestVerb.Get, url, headers, null, "application/json").SendAsync<T>();
		}

		public global::System.Threading.Tasks.Task<T> PostAsync<T>(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			return new global::Unity.Services.Authentication.PlayerAccounts.WebRequest(Configuration, global::Unity.Services.Authentication.PlayerAccounts.WebRequestVerb.Post, url, headers, null, "application/json").SendAsync<T>();
		}

		public global::System.Threading.Tasks.Task<T> PostAsync<T>(string url, string payload, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			return new global::Unity.Services.Authentication.PlayerAccounts.WebRequest(Configuration, global::Unity.Services.Authentication.PlayerAccounts.WebRequestVerb.Post, url, headers, payload, "application/x-www-form-urlencoded").SendAsync<T>();
		}

		public global::System.Threading.Tasks.Task<T> PutAsync<T>(string url, object payload, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			string payload2 = ((payload != null) ? global::Newtonsoft.Json.JsonConvert.SerializeObject(payload, m_JsonSerializerSettings) : null);
			return new global::Unity.Services.Authentication.PlayerAccounts.WebRequest(Configuration, global::Unity.Services.Authentication.PlayerAccounts.WebRequestVerb.Put, url, headers, payload2, "application/json").SendAsync<T>();
		}

		public global::System.Threading.Tasks.Task DeleteAsync(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			return new global::Unity.Services.Authentication.PlayerAccounts.WebRequest(Configuration, global::Unity.Services.Authentication.PlayerAccounts.WebRequestVerb.Delete, url, headers, null, "application/json").SendAsync();
		}

		public global::System.Threading.Tasks.Task<T> DeleteAsync<T>(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			return new global::Unity.Services.Authentication.PlayerAccounts.WebRequest(Configuration, global::Unity.Services.Authentication.PlayerAccounts.WebRequestVerb.Delete, url, headers, null, "application/json").SendAsync<T>();
		}
	}
}
