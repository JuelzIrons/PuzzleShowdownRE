namespace Unity.Services.Core.Configuration
{
	internal static class StreamingAssetsUtils
	{
		public static global::System.Threading.Tasks.Task<string> GetFileTextFromStreamingAssetsAsync(string path)
		{
			string path2 = global::System.IO.Path.Combine(global::UnityEngine.Application.streamingAssetsPath, path);
			global::System.Threading.Tasks.TaskCompletionSource<string> taskCompletionSource = new global::System.Threading.Tasks.TaskCompletionSource<string>();
			try
			{
				string result = global::System.IO.File.ReadAllText(path2);
				taskCompletionSource.SetResult(result);
			}
			catch (global::System.Exception exception)
			{
				taskCompletionSource.SetException(exception);
			}
			return taskCompletionSource.Task;
		}
	}
}
