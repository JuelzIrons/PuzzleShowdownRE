namespace Unity.Services.DistributedAuthority.Http
{
	internal static class UnityWebRequestHelpers
	{
		public static global::System.Runtime.CompilerServices.TaskAwaiter<global::Unity.Services.DistributedAuthority.Http.HttpClientResponse> GetAwaiter(this global::UnityEngine.Networking.UnityWebRequestAsyncOperation asyncOp)
		{
			global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.DistributedAuthority.Http.HttpClientResponse> tcs = new global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.DistributedAuthority.Http.HttpClientResponse>();
			asyncOp.completed += delegate(global::UnityEngine.AsyncOperation obj)
			{
				global::Unity.Services.DistributedAuthority.Http.HttpClientResponse result = CreateHttpClientResponse((global::UnityEngine.Networking.UnityWebRequestAsyncOperation)obj);
				tcs.SetResult(result);
			};
			return tcs.Task.GetAwaiter();
		}

		internal static global::Unity.Services.DistributedAuthority.Http.HttpClientResponse CreateHttpClientResponse(global::UnityEngine.Networking.UnityWebRequestAsyncOperation unityResponse)
		{
			global::UnityEngine.Networking.UnityWebRequest webRequest = unityResponse.webRequest;
			return new global::Unity.Services.DistributedAuthority.Http.HttpClientResponse(webRequest.GetResponseHeaders(), webRequest.responseCode, webRequest.result == global::UnityEngine.Networking.UnityWebRequest.Result.ProtocolError, webRequest.result == global::UnityEngine.Networking.UnityWebRequest.Result.ConnectionError, webRequest.downloadHandler.data, webRequest.error);
		}
	}
}
