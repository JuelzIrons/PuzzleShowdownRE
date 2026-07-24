namespace Unity.Services.Lobbies.Http
{
	internal static class UnityWebRequestHelpers
	{
		public static global::System.Runtime.CompilerServices.TaskAwaiter<global::Unity.Services.Lobbies.Http.HttpClientResponse> GetAwaiter(this global::UnityEngine.Networking.UnityWebRequestAsyncOperation asyncOp)
		{
			global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Lobbies.Http.HttpClientResponse> tcs = new global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Lobbies.Http.HttpClientResponse>();
			asyncOp.completed += delegate(global::UnityEngine.AsyncOperation obj)
			{
				global::Unity.Services.Lobbies.Http.HttpClientResponse result = CreateHttpClientResponse((global::UnityEngine.Networking.UnityWebRequestAsyncOperation)obj);
				tcs.SetResult(result);
			};
			return tcs.Task.GetAwaiter();
		}

		internal static global::Unity.Services.Lobbies.Http.HttpClientResponse CreateHttpClientResponse(global::UnityEngine.Networking.UnityWebRequestAsyncOperation unityResponse)
		{
			global::UnityEngine.Networking.UnityWebRequest webRequest = unityResponse.webRequest;
			return new global::Unity.Services.Lobbies.Http.HttpClientResponse(webRequest.GetResponseHeaders(), webRequest.responseCode, webRequest.result == global::UnityEngine.Networking.UnityWebRequest.Result.ProtocolError, webRequest.result == global::UnityEngine.Networking.UnityWebRequest.Result.ConnectionError, webRequest.downloadHandler.data, webRequest.error);
		}
	}
}
