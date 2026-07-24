namespace Unity.Services.Qos.V2.Scheduler
{
	internal static class ThreadHelper
	{
		private static global::System.Threading.SynchronizationContext _unitySynchronizationContext;

		private static global::System.Threading.Tasks.TaskScheduler _taskScheduler;

		private static int _mainThreadId;

		public static global::System.Threading.SynchronizationContext SynchronizationContext => _unitySynchronizationContext;

		public static global::System.Threading.Tasks.TaskScheduler TaskScheduler => _taskScheduler;

		public static int MainThreadId => _mainThreadId;

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Init()
		{
			_unitySynchronizationContext = global::System.Threading.SynchronizationContext.Current;
			_taskScheduler = global::System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext();
			_mainThreadId = global::System.Threading.Thread.CurrentThread.ManagedThreadId;
		}
	}
}
