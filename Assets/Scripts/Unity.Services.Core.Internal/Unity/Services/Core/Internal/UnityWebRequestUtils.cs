namespace Unity.Services.Core.Internal
{
	internal static class UnityWebRequestUtils
	{
		public const string JsonContentType = "application/json";

		public static bool HasSucceeded(this global::UnityEngine.Networking.UnityWebRequest self)
		{
			return self.result == global::UnityEngine.Networking.UnityWebRequest.Result.Success;
		}

		public static global::System.Threading.Tasks.Task<string> GetTextAsync(string uri)
		{
			global::System.Threading.Tasks.TaskCompletionSource<string> completionSource = new global::System.Threading.Tasks.TaskCompletionSource<string>();
			global::UnityEngine.Networking.UnityWebRequest.Get(uri).SendWebRequest().completed += CompleteFetchTaskOnRequestCompleted;
			return completionSource.Task;
			void CompleteFetchTaskOnRequestCompleted(global::UnityEngine.AsyncOperation rawOperation)
			{
				try
				{
					using global::UnityEngine.Networking.UnityWebRequest unityWebRequest = ((global::UnityEngine.Networking.UnityWebRequestAsyncOperation)rawOperation).webRequest;
					if (unityWebRequest.HasSucceeded())
					{
						completionSource.TrySetResult(unityWebRequest.downloadHandler.text);
					}
					else
					{
						string message = "Couldn't fetch config file.\nURL: " + unityWebRequest.url + "\nReason: " + unityWebRequest.error;
						completionSource.TrySetException(new global::System.Exception(message));
					}
				}
				catch (global::System.Exception exception)
				{
					completionSource.TrySetException(exception);
				}
			}
		}
	}
}
