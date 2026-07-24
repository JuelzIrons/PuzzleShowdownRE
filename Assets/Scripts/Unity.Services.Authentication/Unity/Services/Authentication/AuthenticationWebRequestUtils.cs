namespace Unity.Services.Authentication
{
	internal static class AuthenticationWebRequestUtils
	{
		public static global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> SendWebRequestAsync(this global::UnityEngine.Networking.UnityWebRequest request)
		{
			global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Authentication.Shared.ApiResponse> tcs = new global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Authentication.Shared.ApiResponse>();
			global::UnityEngine.Networking.UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = request.SendWebRequest();
			if (unityWebRequestAsyncOperation.isDone)
			{
				ProcessResponse(tcs, request);
			}
			else
			{
				unityWebRequestAsyncOperation.completed += delegate
				{
					ProcessResponse(tcs, request);
				};
			}
			return tcs.Task;
		}

		public static global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> SendWebRequestAsync<T>(this global::UnityEngine.Networking.UnityWebRequest request, global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Authentication.Shared.ApiResponse<T>> tcs = new global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Authentication.Shared.ApiResponse<T>>();
			cancellationToken.Register(delegate
			{
				tcs.SetCanceled();
			});
			global::UnityEngine.Networking.UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = request.SendWebRequest();
			if (unityWebRequestAsyncOperation.isDone)
			{
				ProcessResponse(tcs, request);
			}
			else
			{
				unityWebRequestAsyncOperation.completed += delegate
				{
					ProcessResponse(tcs, request);
				};
			}
			return tcs.Task;
		}

		private static void ProcessResponse(global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Authentication.Shared.ApiResponse> tcs, global::UnityEngine.Networking.UnityWebRequest request)
		{
			global::Unity.Services.Authentication.Shared.ApiResponse apiResponse = new global::Unity.Services.Authentication.Shared.ApiResponse
			{
				StatusCode = (int)request.responseCode,
				ErrorText = request.error,
				RawContent = request.downloadHandler?.text
			};
			string message = request.error + "\n" + request.downloadHandler?.text;
			if (IsNetworkError(request))
			{
				tcs.SetException(new global::Unity.Services.Authentication.Shared.ApiException(global::Unity.Services.Authentication.Shared.ApiExceptionType.Network, message, apiResponse));
			}
			else if (IsHttpError(request))
			{
				tcs.SetException(new global::Unity.Services.Authentication.Shared.ApiException(global::Unity.Services.Authentication.Shared.ApiExceptionType.Http, message, apiResponse));
			}
			else
			{
				tcs.SetResult(apiResponse);
			}
		}

		private static void ProcessResponse<T>(global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Authentication.Shared.ApiResponse<T>> tcs, global::UnityEngine.Networking.UnityWebRequest request)
		{
			global::Unity.Services.Authentication.Shared.ApiResponse<T> apiResponse = new global::Unity.Services.Authentication.Shared.ApiResponse<T>
			{
				StatusCode = (int)request.responseCode,
				ErrorText = request.error,
				RawContent = request.downloadHandler?.text
			};
			string message = request.error + "\n" + request.downloadHandler?.text;
			if (IsNetworkError(request))
			{
				tcs.SetException(new global::Unity.Services.Authentication.Shared.ApiException(global::Unity.Services.Authentication.Shared.ApiExceptionType.Network, message, apiResponse));
				return;
			}
			if (IsHttpError(request))
			{
				tcs.SetException(new global::Unity.Services.Authentication.Shared.ApiException(global::Unity.Services.Authentication.Shared.ApiExceptionType.Http, message, apiResponse));
				return;
			}
			try
			{
				if (!string.IsNullOrEmpty(request.downloadHandler?.text))
				{
					apiResponse.Data = global::Unity.Services.Authentication.IsolatedJsonConvert.DeserializeObject<T>(request.downloadHandler?.text, global::Unity.Services.Authentication.SerializerSettings.DefaultSerializerSettings);
				}
			}
			catch (global::System.Exception)
			{
				tcs.SetException(new global::Unity.Services.Authentication.Shared.ApiException(global::Unity.Services.Authentication.Shared.ApiExceptionType.Deserialization, $"Deserialization of type '{typeof(T)}' failed.", apiResponse));
				return;
			}
			tcs.SetResult(apiResponse);
		}

		public static bool IsNetworkError(global::UnityEngine.Networking.UnityWebRequest request)
		{
			return request.responseCode >= 500;
		}

		public static bool IsHttpError(global::UnityEngine.Networking.UnityWebRequest request)
		{
			if (request.responseCode >= 400)
			{
				return request.responseCode < 500;
			}
			return false;
		}
	}
}
