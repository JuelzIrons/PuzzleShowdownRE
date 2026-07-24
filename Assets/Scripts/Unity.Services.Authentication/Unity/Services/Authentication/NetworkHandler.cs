namespace Unity.Services.Authentication
{
	internal class NetworkHandler : global::Unity.Services.Authentication.INetworkHandler
	{
		public static class ContentType
		{
			public const string Json = "application/json";
		}

		private global::Unity.Services.Authentication.INetworkConfiguration Configuration { get; }

		public NetworkHandler(global::Unity.Services.Authentication.INetworkConfiguration configuration)
		{
			Configuration = configuration;
		}

		public global::System.Threading.Tasks.Task<T> GetAsync<T>(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			return new global::Unity.Services.Authentication.WebRequest(Configuration, global::Unity.Services.Authentication.WebRequestVerb.Get, url, headers, null, "application/json").SendAsync<T>();
		}

		public global::System.Threading.Tasks.Task<T> PostAsync<T>(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			return new global::Unity.Services.Authentication.WebRequest(Configuration, global::Unity.Services.Authentication.WebRequestVerb.Post, url, headers, null, "application/json").SendAsync<T>();
		}

		public global::System.Threading.Tasks.Task<T> PostAsync<T>(string url, object payload, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			string payload2 = ((payload != null) ? global::Unity.Services.Authentication.IsolatedJsonConvert.SerializeObject(payload, global::Unity.Services.Authentication.SerializerSettings.DefaultSerializerSettings) : null);
			return new global::Unity.Services.Authentication.WebRequest(Configuration, global::Unity.Services.Authentication.WebRequestVerb.Post, url, headers, payload2, "application/json").SendAsync<T>();
		}

		public global::System.Threading.Tasks.Task<T> PutAsync<T>(string url, object payload, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			string payload2 = ((payload != null) ? global::Unity.Services.Authentication.IsolatedJsonConvert.SerializeObject(payload, global::Unity.Services.Authentication.SerializerSettings.DefaultSerializerSettings) : null);
			return new global::Unity.Services.Authentication.WebRequest(Configuration, global::Unity.Services.Authentication.WebRequestVerb.Put, url, headers, payload2, "application/json").SendAsync<T>();
		}

		public global::System.Threading.Tasks.Task DeleteAsync(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			return new global::Unity.Services.Authentication.WebRequest(Configuration, global::Unity.Services.Authentication.WebRequestVerb.Delete, url, headers, null, "application/json").SendAsync();
		}

		public global::System.Threading.Tasks.Task<T> DeleteAsync<T>(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null)
		{
			return new global::Unity.Services.Authentication.WebRequest(Configuration, global::Unity.Services.Authentication.WebRequestVerb.Delete, url, headers, null, "application/json").SendAsync<T>();
		}
	}
}
