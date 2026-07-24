namespace Unity.Services.Core
{
	internal static class UnityThreadUtils
	{
		private static int s_UnityThreadId;

		internal static global::System.Threading.Tasks.TaskScheduler UnityThreadScheduler { get; private set; }

		public static bool IsRunningOnUnityThread => global::System.Threading.Thread.CurrentThread.ManagedThreadId == s_UnityThreadId;

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void CaptureUnityThreadInfo()
		{
			s_UnityThreadId = global::System.Threading.Thread.CurrentThread.ManagedThreadId;
			UnityThreadScheduler = global::System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext();
		}
	}
}
