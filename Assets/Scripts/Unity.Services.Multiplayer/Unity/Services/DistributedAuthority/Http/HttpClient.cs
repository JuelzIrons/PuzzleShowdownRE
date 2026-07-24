namespace Unity.Services.DistributedAuthority.Http
{
	internal class HttpClient : global::Unity.Services.DistributedAuthority.Http.IHttpClient
	{
		public async global::System.Threading.Tasks.Task<global::Unity.Services.DistributedAuthority.Http.HttpClientResponse> MakeRequestAsync(string method, string url, byte[] body, global::System.Collections.Generic.Dictionary<string, string> headers, int requestTimeout)
		{
			return await CreateWebRequestAsync(method.ToUpper(), url, body, headers, requestTimeout);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.DistributedAuthority.Http.HttpClientResponse> MakeRequestAsync(string method, string url, global::System.Collections.Generic.List<global::UnityEngine.Networking.IMultipartFormSection> body, global::System.Collections.Generic.Dictionary<string, string> headers, int requestTimeout, string boundary = null)
		{
			return await CreateWebRequestAsync(method.ToUpper(), url, body, headers, requestTimeout, boundary);
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.DistributedAuthority.Http.HttpClientResponse> CreateWebRequestAsync(string method, string url, byte[] body, global::System.Collections.Generic.IDictionary<string, string> headers, int requestTimeout)
		{
			return await CreateHttpClientResponse(method, url, body, headers, requestTimeout);
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.DistributedAuthority.Http.HttpClientResponse> CreateHttpClientResponse(string method, string url, byte[] body, global::System.Collections.Generic.IDictionary<string, string> headers, int requestTimeout)
		{
			return await (await global::System.Threading.Tasks.Task.Factory.StartNew(async delegate
			{
				using global::UnityEngine.Networking.UnityWebRequest request = new global::UnityEngine.Networking.UnityWebRequest(url, method);
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> header in headers)
				{
					request.SetRequestHeader(header.Key, header.Value);
				}
				request.timeout = requestTimeout;
				if (body != null && (method == "POST" || method == "PUT" || method == "PATCH"))
				{
					request.uploadHandler = new global::UnityEngine.Networking.UploadHandlerRaw(body);
				}
				request.downloadHandler = new global::UnityEngine.Networking.DownloadHandlerBuffer();
				return await SendWebRequest(request);
			}, global::System.Threading.CancellationToken.None, global::System.Threading.Tasks.TaskCreationOptions.None, global::Unity.Services.DistributedAuthority.Scheduler.ThreadHelper.TaskScheduler));
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.DistributedAuthority.Http.HttpClientResponse> CreateWebRequestAsync(string method, string url, global::System.Collections.Generic.List<global::UnityEngine.Networking.IMultipartFormSection> body, global::System.Collections.Generic.IDictionary<string, string> headers, int requestTimeout, string boundary = null)
		{
			return await (await global::System.Threading.Tasks.Task.Factory.StartNew(async delegate
			{
				byte[] boundary2 = (string.IsNullOrEmpty(boundary) ? global::UnityEngine.Networking.UnityWebRequest.GenerateBoundary() : global::System.Text.Encoding.Default.GetBytes(boundary));
				global::UnityEngine.Networking.UnityWebRequest unityWebRequest = new global::UnityEngine.Networking.UnityWebRequest(url, method);
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> header in headers)
				{
					unityWebRequest.SetRequestHeader(header.Key, header.Value);
				}
				unityWebRequest.timeout = requestTimeout;
				unityWebRequest = SetupMultipartRequest(unityWebRequest, body, boundary2);
				unityWebRequest.downloadHandler = new global::UnityEngine.Networking.DownloadHandlerBuffer();
				return await SendWebRequest(unityWebRequest);
			}, global::System.Threading.CancellationToken.None, global::System.Threading.Tasks.TaskCreationOptions.None, global::Unity.Services.DistributedAuthority.Scheduler.ThreadHelper.TaskScheduler));
		}

		private static global::UnityEngine.Networking.UnityWebRequest SetupMultipartRequest(global::UnityEngine.Networking.UnityWebRequest request, global::System.Collections.Generic.List<global::UnityEngine.Networking.IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			byte[] data = null;
			if (multipartFormSections != null && multipartFormSections.Count != 0)
			{
				data = global::UnityEngine.Networking.UnityWebRequest.SerializeFormSections(multipartFormSections, boundary);
			}
			global::UnityEngine.Networking.UploadHandler uploadHandler = new global::UnityEngine.Networking.UploadHandlerRaw(data);
			uploadHandler.contentType = "multipart/form-data; boundary=" + global::System.Text.Encoding.UTF8.GetString(boundary, 0, boundary.Length);
			request.uploadHandler = uploadHandler;
			request.downloadHandler = new global::UnityEngine.Networking.DownloadHandlerBuffer();
			return request;
		}

		private global::UnityEngine.Networking.UnityWebRequestAsyncOperation SendWebRequest(global::UnityEngine.Networking.UnityWebRequest request)
		{
			return request.SendWebRequest();
		}
	}
}
