namespace Unity.Services.Multiplayer
{
	internal static class Compatibility
	{
		private static readonly global::UnityEngine.WaitForEndOfFrame k_WaitForEndOfFrame = new global::UnityEngine.WaitForEndOfFrame();

		internal static global::System.Threading.Tasks.Task WaitForEndOfFrameAsync(global::UnityEngine.MonoBehaviour behaviour)
		{
			global::System.Threading.Tasks.TaskCompletionSource<object> taskCompletionSource = new global::System.Threading.Tasks.TaskCompletionSource<object>();
			behaviour.StartCoroutine(WaitForEndOfFrameCoroutine(taskCompletionSource));
			return taskCompletionSource.Task;
			static global::System.Collections.IEnumerator WaitForEndOfFrameCoroutine(global::System.Threading.Tasks.TaskCompletionSource<object> tcs)
			{
				yield return k_WaitForEndOfFrame;
				tcs.SetResult(null);
			}
		}

		internal static async global::System.Threading.Tasks.Task WaitForSecondsRealtimeAsync(global::System.TimeSpan duration)
		{
			float end = global::UnityEngine.Time.realtimeSinceStartup + (float)duration.TotalSeconds;
			while (global::UnityEngine.Time.realtimeSinceStartup < end)
			{
				await global::System.Threading.Tasks.Task.Yield();
			}
		}
	}
}
