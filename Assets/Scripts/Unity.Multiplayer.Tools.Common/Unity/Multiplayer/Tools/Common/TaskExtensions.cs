namespace Unity.Multiplayer.Tools.Common
{
	internal static class TaskExtensions
	{
		public static void Forget(this global::System.Threading.Tasks.Task task)
		{
			if (!task.IsCompleted || task.IsFaulted)
			{
				ForgetAwaited(task);
			}
			static async global::System.Threading.Tasks.Task ForgetAwaited(global::System.Threading.Tasks.Task task2, bool logCanceledTask = false)
			{
				try
				{
					await task2.ConfigureAwait(continueOnCapturedContext: false);
				}
				catch (global::System.Threading.Tasks.TaskCanceledException exception)
				{
					if (logCanceledTask)
					{
						global::UnityEngine.Debug.LogException(exception);
					}
				}
				catch (global::System.Exception exception2)
				{
					global::UnityEngine.Debug.LogException(exception2);
				}
			}
		}
	}
}
