namespace Unity.Services.Core.Threading.Internal
{
	internal class UnityThreadUtilsInternal : global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils, global::Unity.Services.Core.Internal.IServiceComponent
	{
		bool global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils.IsRunningOnUnityThread => global::Unity.Services.Core.UnityThreadUtils.IsRunningOnUnityThread;

		public static global::System.Threading.Tasks.Task PostAsync(global::System.Action action)
		{
			return global::System.Threading.Tasks.Task.Factory.StartNew(action, global::System.Threading.CancellationToken.None, global::System.Threading.Tasks.TaskCreationOptions.None, global::Unity.Services.Core.UnityThreadUtils.UnityThreadScheduler);
		}

		public static global::System.Threading.Tasks.Task PostAsync(global::System.Action<object> action, object state)
		{
			return global::System.Threading.Tasks.Task.Factory.StartNew(action, state, global::System.Threading.CancellationToken.None, global::System.Threading.Tasks.TaskCreationOptions.None, global::Unity.Services.Core.UnityThreadUtils.UnityThreadScheduler);
		}

		public static global::System.Threading.Tasks.Task<T> PostAsync<T>(global::System.Func<T> action)
		{
			return global::System.Threading.Tasks.Task<T>.Factory.StartNew(action, global::System.Threading.CancellationToken.None, global::System.Threading.Tasks.TaskCreationOptions.None, global::Unity.Services.Core.UnityThreadUtils.UnityThreadScheduler);
		}

		public static global::System.Threading.Tasks.Task<T> PostAsync<T>(global::System.Func<object, T> action, object state)
		{
			return global::System.Threading.Tasks.Task<T>.Factory.StartNew(action, state, global::System.Threading.CancellationToken.None, global::System.Threading.Tasks.TaskCreationOptions.None, global::Unity.Services.Core.UnityThreadUtils.UnityThreadScheduler);
		}

		public static void Send(global::System.Action action)
		{
			if (global::Unity.Services.Core.UnityThreadUtils.IsRunningOnUnityThread)
			{
				action();
			}
			else
			{
				PostAsync(action).Wait();
			}
		}

		public static void Send(global::System.Action<object> action, object state)
		{
			if (global::Unity.Services.Core.UnityThreadUtils.IsRunningOnUnityThread)
			{
				action(state);
			}
			else
			{
				PostAsync(action, state).Wait();
			}
		}

		public static T Send<T>(global::System.Func<T> action)
		{
			if (global::Unity.Services.Core.UnityThreadUtils.IsRunningOnUnityThread)
			{
				return action();
			}
			global::System.Threading.Tasks.Task<T> task = PostAsync(action);
			task.Wait();
			return task.Result;
		}

		public static T Send<T>(global::System.Func<object, T> action, object state)
		{
			if (global::Unity.Services.Core.UnityThreadUtils.IsRunningOnUnityThread)
			{
				return action(state);
			}
			global::System.Threading.Tasks.Task<T> task = PostAsync(action, state);
			task.Wait();
			return task.Result;
		}

		global::System.Threading.Tasks.Task global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils.PostAsync(global::System.Action action)
		{
			return PostAsync(action);
		}

		global::System.Threading.Tasks.Task global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils.PostAsync(global::System.Action<object> action, object state)
		{
			return PostAsync(action, state);
		}

		global::System.Threading.Tasks.Task<T> global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils.PostAsync<T>(global::System.Func<T> action)
		{
			return PostAsync(action);
		}

		global::System.Threading.Tasks.Task<T> global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils.PostAsync<T>(global::System.Func<object, T> action, object state)
		{
			return PostAsync(action, state);
		}

		void global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils.Send(global::System.Action action)
		{
			Send(action);
		}

		void global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils.Send(global::System.Action<object> action, object state)
		{
			Send(action, state);
		}

		T global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils.Send<T>(global::System.Func<T> action)
		{
			return Send(action);
		}

		T global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils.Send<T>(global::System.Func<object, T> action, object state)
		{
			return Send(action, state);
		}
	}
}
