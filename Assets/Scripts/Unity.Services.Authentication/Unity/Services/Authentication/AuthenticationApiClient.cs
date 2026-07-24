namespace Unity.Services.Authentication
{
	internal class AuthenticationApiClient : global::Unity.Services.Authentication.Shared.IApiClient
	{
		private global::Unity.Services.Authentication.INetworkConfiguration Configuration { get; }

		public AuthenticationApiClient(global::Unity.Services.Authentication.INetworkConfiguration configuration)
		{
			Configuration = configuration;
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> GetAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync(path, global::Unity.Services.Authentication.WebRequestVerb.Get, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> GetAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync<T>(path, global::Unity.Services.Authentication.WebRequestVerb.Get, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> PostAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync(path, global::Unity.Services.Authentication.WebRequestVerb.Post, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> PostAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync<T>(path, global::Unity.Services.Authentication.WebRequestVerb.Post, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> PutAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync(path, global::Unity.Services.Authentication.WebRequestVerb.Put, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> PutAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync<T>(path, global::Unity.Services.Authentication.WebRequestVerb.Put, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> DeleteAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync(path, global::Unity.Services.Authentication.WebRequestVerb.Delete, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> DeleteAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync<T>(path, global::Unity.Services.Authentication.WebRequestVerb.Delete, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> HeadAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync(path, global::Unity.Services.Authentication.WebRequestVerb.Head, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> HeadAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync<T>(path, global::Unity.Services.Authentication.WebRequestVerb.Head, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> OptionsAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync(path, global::Unity.Services.Authentication.WebRequestVerb.Options, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> OptionsAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync<T>(path, global::Unity.Services.Authentication.WebRequestVerb.Options, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> PatchAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync(path, global::Unity.Services.Authentication.WebRequestVerb.Patch, options, configuration, cancellationToken);
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> PatchAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return SendAsync<T>(path, global::Unity.Services.Authentication.WebRequestVerb.Patch, options, configuration, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> SendAsync(string path, global::Unity.Services.Authentication.WebRequestVerb method, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			int attempts = 0;
			bool flag;
			do
			{
				try
				{
					using global::UnityEngine.Networking.UnityWebRequest request = BuildWebRequest(path, method, options, configuration);
					return await request.SendWebRequestAsync();
				}
				catch (global::Unity.Services.Authentication.Shared.ApiException ex)
				{
					flag = ex.Type == global::Unity.Services.Authentication.Shared.ApiExceptionType.Network;
					attempts++;
					if (attempts >= Configuration.Retries || !flag)
					{
						throw;
					}
				}
			}
			while (flag);
			return null;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> SendAsync<T>(string path, global::Unity.Services.Authentication.WebRequestVerb method, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			int attempts = 0;
			bool flag;
			do
			{
				try
				{
					using global::UnityEngine.Networking.UnityWebRequest request = BuildWebRequest(path, method, options, configuration);
					return await request.SendWebRequestAsync<T>(cancellationToken);
				}
				catch (global::Unity.Services.Authentication.Shared.ApiException ex)
				{
					flag = ex.Type == global::Unity.Services.Authentication.Shared.ApiExceptionType.Network;
					if (attempts >= Configuration.Retries || !flag)
					{
						throw;
					}
					attempts++;
				}
			}
			while (flag);
			return null;
		}

		internal global::UnityEngine.Networking.UnityWebRequest BuildWebRequest(string path, global::Unity.Services.Authentication.WebRequestVerb method, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration)
		{
			global::Unity.Services.Authentication.Shared.ApiRequestPathBuilder apiRequestPathBuilder = new global::Unity.Services.Authentication.Shared.ApiRequestPathBuilder(configuration.BasePath, path);
			apiRequestPathBuilder.AddPathParameters(options.PathParameters);
			apiRequestPathBuilder.AddQueryParameters(options.QueryParameters);
			global::UnityEngine.Networking.UnityWebRequest unityWebRequest = new global::UnityEngine.Networking.UnityWebRequest(apiRequestPathBuilder.GetFullUri(), method.ToString());
			if (configuration.UserAgent != null)
			{
				unityWebRequest.SetRequestHeader("User-Agent", configuration.UserAgent);
			}
			if (configuration.DefaultHeaders != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> defaultHeader in configuration.DefaultHeaders)
				{
					unityWebRequest.SetRequestHeader(defaultHeader.Key, defaultHeader.Value);
				}
			}
			if (options.HeaderParameters != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.IList<string>> headerParameter in options.HeaderParameters)
				{
					foreach (string item in headerParameter.Value)
					{
						unityWebRequest.SetRequestHeader(headerParameter.Key, item);
					}
				}
			}
			unityWebRequest.timeout = configuration.Timeout;
			if (options.Data != null)
			{
				global::Newtonsoft.Json.JsonSerializerSettings settings = new global::Newtonsoft.Json.JsonSerializerSettings
				{
					ReferenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Ignore
				};
				string s = global::Unity.Services.Authentication.IsolatedJsonConvert.SerializeObject(options.Data, settings);
				unityWebRequest.uploadHandler = new global::UnityEngine.Networking.UploadHandlerRaw(global::System.Text.Encoding.UTF8.GetBytes(s));
			}
			unityWebRequest.downloadHandler = new global::UnityEngine.Networking.DownloadHandlerBuffer();
			return unityWebRequest;
		}
	}
}
