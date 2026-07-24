namespace Unity.Services.Core.Networking
{
	internal class UnityWebRequestClient : global::Unity.Services.Core.Networking.Internal.IHttpClient, global::Unity.Services.Core.Internal.IServiceComponent
	{
		private readonly global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Core.Networking.HttpServiceConfig> m_ServiceIdToConfig = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Core.Networking.HttpServiceConfig>();

		public string GetBaseUrlFor(string serviceId)
		{
			return m_ServiceIdToConfig[serviceId].BaseUrl;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpOptions GetDefaultOptionsFor(string serviceId)
		{
			return m_ServiceIdToConfig[serviceId].DefaultOptions;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpRequest CreateRequestForService(string serviceId, string resourcePath)
		{
			global::Unity.Services.Core.Networking.HttpServiceConfig httpServiceConfig = m_ServiceIdToConfig[serviceId];
			string url = CombinePaths(httpServiceConfig.BaseUrl, resourcePath);
			return new global::Unity.Services.Core.Networking.Internal.HttpRequest().SetUrl(url).SetOptions(httpServiceConfig.DefaultOptions);
		}

		internal static string CombinePaths(string path1, string path2)
		{
			return global::System.IO.Path.Combine(path1, path2).Replace('\\', '/');
		}

		public global::Unity.Services.Core.Internal.IAsyncOperation<global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpResponse> Send(global::Unity.Services.Core.Networking.Internal.HttpRequest request)
		{
			global::Unity.Services.Core.Internal.AsyncOperation<global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpResponse> operation = new global::Unity.Services.Core.Internal.AsyncOperation<global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpResponse>();
			operation.SetInProgress();
			try
			{
				ConvertToWebRequest(request).SendWebRequest().completed += OnWebRequestCompleted;
			}
			catch (global::System.Exception reason)
			{
				operation.Fail(reason);
			}
			return operation;
			void OnWebRequestCompleted(global::UnityEngine.AsyncOperation unityOperation)
			{
				global::UnityEngine.Networking.UnityWebRequest webRequest = ((global::UnityEngine.Networking.UnityWebRequestAsyncOperation)unityOperation).webRequest;
				global::Unity.Services.Core.Networking.Internal.HttpResponse response = ConvertToResponse(webRequest).SetRequest(request);
				global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpResponse result = new global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpResponse(response);
				webRequest.Dispose();
				operation.Succeed(result);
			}
		}

		private static global::UnityEngine.Networking.UnityWebRequest ConvertToWebRequest(global::Unity.Services.Core.Networking.Internal.HttpRequest request)
		{
			global::UnityEngine.Networking.UnityWebRequest unityWebRequest = new global::UnityEngine.Networking.UnityWebRequest(request.Url, request.Method)
			{
				downloadHandler = new global::UnityEngine.Networking.DownloadHandlerBuffer(),
				redirectLimit = request.Options.RedirectLimit,
				timeout = request.Options.RequestTimeoutInSeconds
			};
			if (request.Body != null && request.Body.Length != 0)
			{
				unityWebRequest.uploadHandler = new global::UnityEngine.Networking.UploadHandlerRaw(request.Body);
			}
			if (request.Headers != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> header in request.Headers)
				{
					unityWebRequest.SetRequestHeader(header.Key, header.Value);
				}
			}
			return unityWebRequest;
		}

		private static global::Unity.Services.Core.Networking.Internal.HttpResponse ConvertToResponse(global::UnityEngine.Networking.UnityWebRequest webRequest)
		{
			return new global::Unity.Services.Core.Networking.Internal.HttpResponse().SetHeaders(webRequest.GetResponseHeaders()).SetData(webRequest.downloadHandler?.data).SetStatusCode(webRequest.responseCode)
				.SetErrorMessage(webRequest.error)
				.SetIsHttpError(webRequest.result == global::UnityEngine.Networking.UnityWebRequest.Result.ProtocolError)
				.SetIsNetworkError(webRequest.result == global::UnityEngine.Networking.UnityWebRequest.Result.ConnectionError);
		}

		internal void SetServiceConfig(global::Unity.Services.Core.Networking.HttpServiceConfig config)
		{
			m_ServiceIdToConfig[config.ServiceId] = config;
		}
	}
}
