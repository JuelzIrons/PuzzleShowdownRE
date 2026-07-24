namespace Unity.Services.Authentication
{
	internal class WebRequest
	{
		private readonly global::Unity.Services.Authentication.WebRequestVerb m_Verb;

		private readonly string m_Url;

		private readonly global::System.Collections.Generic.IDictionary<string, string> m_Headers;

		private readonly string m_Payload;

		private readonly string m_PayloadContentType;

		private readonly global::Newtonsoft.Json.JsonSerializerSettings m_JsonSerializerSettings = new global::Newtonsoft.Json.JsonSerializerSettings
		{
			NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore
		};

		internal global::Unity.Services.Authentication.INetworkConfiguration Configuration { get; }

		internal int Retries { get; private set; }

		internal WebRequest(global::Unity.Services.Authentication.INetworkConfiguration configuration, global::Unity.Services.Authentication.WebRequestVerb verb, string url, global::System.Collections.Generic.IDictionary<string, string> headers, string payload, string payloadContentType)
		{
			Configuration = configuration;
			m_Verb = verb;
			m_Url = url;
			m_Headers = headers;
			m_Payload = payload;
			m_PayloadContentType = payloadContentType;
		}

		internal global::System.Threading.Tasks.Task SendAsync()
		{
			return SendAttemptAsync(new global::System.Threading.Tasks.TaskCompletionSource<string>());
		}

		internal async global::System.Threading.Tasks.Task<T> SendAsync<T>()
		{
			string value = await SendAttemptAsync(new global::System.Threading.Tasks.TaskCompletionSource<string>());
			if (string.IsNullOrEmpty(value))
			{
				return default(T);
			}
			try
			{
				return global::Unity.Services.Authentication.IsolatedJsonConvert.DeserializeObject<T>(value, m_JsonSerializerSettings);
			}
			catch (global::System.Exception ex)
			{
				string text = "Failed to deserialize object!";
				global::Unity.Services.Authentication.Logger.Log(text + " " + ex.Message);
				throw new global::Unity.Services.Authentication.WebRequestException(isNetworkError: false, isServerError: false, isDeserializationError: true, 0L, text);
			}
		}

		private global::System.Threading.Tasks.Task<string> SendAttemptAsync(global::System.Threading.Tasks.TaskCompletionSource<string> tcs)
		{
			try
			{
				global::UnityEngine.Networking.UnityWebRequest request = Build();
				request.SendWebRequest().completed += delegate
				{
					RequestCompleted(tcs, request.responseCode, RequestHasNetworkError(request), RequestHasServerError(request), request.error, request.downloadHandler?.text, request.GetResponseHeaders());
					request.Dispose();
				};
			}
			catch (global::System.Exception exception)
			{
				tcs.SetException(exception);
			}
			return tcs.Task;
		}

		internal global::UnityEngine.Networking.UnityWebRequest Build()
		{
			global::UnityEngine.Networking.UnityWebRequest unityWebRequest;
			switch (m_Verb)
			{
			case global::Unity.Services.Authentication.WebRequestVerb.Post:
				unityWebRequest = ((!string.IsNullOrEmpty(m_Payload)) ? new global::UnityEngine.Networking.UnityWebRequest(m_Url, "POST")
				{
					uploadHandler = new global::UnityEngine.Networking.UploadHandlerRaw(global::System.Text.Encoding.UTF8.GetBytes(m_Payload)),
					downloadHandler = new global::UnityEngine.Networking.DownloadHandlerBuffer()
				} : global::UnityEngine.Networking.UnityWebRequest.PostWwwForm(m_Url, string.Empty));
				break;
			case global::Unity.Services.Authentication.WebRequestVerb.Get:
				unityWebRequest = global::UnityEngine.Networking.UnityWebRequest.Get(m_Url);
				break;
			case global::Unity.Services.Authentication.WebRequestVerb.Put:
				if (string.IsNullOrEmpty(m_Payload))
				{
					throw new global::System.ArgumentException("PUT payload cannot be empty.");
				}
				unityWebRequest = new global::UnityEngine.Networking.UnityWebRequest(m_Url, "PUT")
				{
					uploadHandler = new global::UnityEngine.Networking.UploadHandlerRaw(global::System.Text.Encoding.UTF8.GetBytes(m_Payload)),
					downloadHandler = new global::UnityEngine.Networking.DownloadHandlerBuffer()
				};
				break;
			case global::Unity.Services.Authentication.WebRequestVerb.Delete:
				unityWebRequest = global::UnityEngine.Networking.UnityWebRequest.Delete(m_Url);
				unityWebRequest.downloadHandler = new global::UnityEngine.Networking.DownloadHandlerBuffer();
				break;
			default:
				throw new global::System.ArgumentException("Unknown verb " + m_Verb);
			}
			if (!string.IsNullOrEmpty(m_PayloadContentType))
			{
				unityWebRequest.SetRequestHeader("Content-Type", m_PayloadContentType);
			}
			if (m_Headers != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> header in m_Headers)
				{
					unityWebRequest.SetRequestHeader(header.Key, header.Value);
				}
			}
			unityWebRequest.timeout = Configuration.Timeout;
			return unityWebRequest;
		}

		internal void RequestCompleted(global::System.Threading.Tasks.TaskCompletionSource<string> tcs, long responseCode, bool isNetworkError, bool isServerError, string errorText, string bodyText, global::System.Collections.Generic.IDictionary<string, string> headers)
		{
			if (isNetworkError && Retries < Configuration.Retries)
			{
				global::Unity.Services.Authentication.Logger.LogWarning("Network error detected, retrying...");
				Retries++;
				SendAttemptAsync(tcs);
			}
			else if (isNetworkError || isServerError)
			{
				string text = ((isServerError && !string.IsNullOrEmpty(bodyText)) ? bodyText : errorText);
				global::Unity.Services.Authentication.WebRequestException exception = new global::Unity.Services.Authentication.WebRequestException(isNetworkError, isServerError, isDeserializationError: false, responseCode, text, headers);
				tcs.SetException(exception);
				global::Unity.Services.Authentication.Logger.LogWarning("Request completed with error: " + text);
			}
			else
			{
				tcs.SetResult(bodyText);
			}
		}

		private bool RequestHasServerError(global::UnityEngine.Networking.UnityWebRequest request)
		{
			return request.responseCode >= 400;
		}

		private bool RequestHasNetworkError(global::UnityEngine.Networking.UnityWebRequest request)
		{
			if (request.result == global::UnityEngine.Networking.UnityWebRequest.Result.ConnectionError)
			{
				return request.error != "Redirect limit exceeded";
			}
			return false;
		}
	}
}
