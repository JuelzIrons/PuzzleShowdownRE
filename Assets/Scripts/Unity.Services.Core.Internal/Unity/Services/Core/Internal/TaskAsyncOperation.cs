namespace Unity.Services.Core.Internal
{
	internal class TaskAsyncOperation : global::Unity.Services.Core.Internal.AsyncOperationBase, global::System.Runtime.CompilerServices.INotifyCompletion
	{
		internal static global::System.Threading.Tasks.TaskScheduler Scheduler;

		private global::System.Threading.Tasks.Task m_Task;

		public override bool IsCompleted => m_Task.IsCompleted;

		public override global::Unity.Services.Core.Internal.AsyncOperationStatus Status
		{
			get
			{
				if (m_Task == null)
				{
					return global::Unity.Services.Core.Internal.AsyncOperationStatus.None;
				}
				if (!m_Task.IsCompleted)
				{
					return global::Unity.Services.Core.Internal.AsyncOperationStatus.InProgress;
				}
				if (m_Task.IsCanceled)
				{
					return global::Unity.Services.Core.Internal.AsyncOperationStatus.Cancelled;
				}
				if (m_Task.IsFaulted)
				{
					return global::Unity.Services.Core.Internal.AsyncOperationStatus.Failed;
				}
				return global::Unity.Services.Core.Internal.AsyncOperationStatus.Succeeded;
			}
		}

		public override global::System.Exception Exception => m_Task?.Exception;

		public override void GetResult()
		{
		}

		public override global::Unity.Services.Core.Internal.AsyncOperationBase GetAwaiter()
		{
			return this;
		}

		public TaskAsyncOperation(global::System.Threading.Tasks.Task task)
		{
			if (Scheduler == null)
			{
				SetScheduler();
			}
			m_Task = task;
			task.ContinueWith(delegate(global::System.Threading.Tasks.Task t, object state)
			{
				((global::Unity.Services.Core.Internal.TaskAsyncOperation)state).DidComplete();
			}, this, global::System.Threading.CancellationToken.None, global::System.Threading.Tasks.TaskContinuationOptions.None, Scheduler);
		}

		public static global::Unity.Services.Core.Internal.TaskAsyncOperation Run(global::System.Action action)
		{
			global::System.Threading.Tasks.Task task = new global::System.Threading.Tasks.Task(action);
			global::Unity.Services.Core.Internal.TaskAsyncOperation result = new global::Unity.Services.Core.Internal.TaskAsyncOperation(task);
			task.Start();
			return result;
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		internal static void SetScheduler()
		{
			Scheduler = global::System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext();
		}
	}
	internal class TaskAsyncOperation<T> : global::Unity.Services.Core.Internal.AsyncOperationBase<T>
	{
		private global::System.Threading.Tasks.Task<T> m_Task;

		public override bool IsCompleted => m_Task.IsCompleted;

		public override T Result => m_Task.Result;

		public override global::Unity.Services.Core.Internal.AsyncOperationStatus Status
		{
			get
			{
				if (m_Task == null)
				{
					return global::Unity.Services.Core.Internal.AsyncOperationStatus.None;
				}
				if (!m_Task.IsCompleted)
				{
					return global::Unity.Services.Core.Internal.AsyncOperationStatus.InProgress;
				}
				if (m_Task.IsCanceled)
				{
					return global::Unity.Services.Core.Internal.AsyncOperationStatus.Cancelled;
				}
				if (m_Task.IsFaulted)
				{
					return global::Unity.Services.Core.Internal.AsyncOperationStatus.Failed;
				}
				return global::Unity.Services.Core.Internal.AsyncOperationStatus.Succeeded;
			}
		}

		public override global::System.Exception Exception => m_Task?.Exception;

		public override T GetResult()
		{
			return m_Task.GetAwaiter().GetResult();
		}

		public override global::Unity.Services.Core.Internal.AsyncOperationBase<T> GetAwaiter()
		{
			return this;
		}

		public TaskAsyncOperation(global::System.Threading.Tasks.Task<T> task)
		{
			if (global::Unity.Services.Core.Internal.TaskAsyncOperation.Scheduler == null)
			{
				global::Unity.Services.Core.Internal.TaskAsyncOperation.SetScheduler();
			}
			m_Task = task;
			task.ContinueWith(delegate(global::System.Threading.Tasks.Task<T> t, object state)
			{
				((global::Unity.Services.Core.Internal.TaskAsyncOperation<T>)state).DidComplete();
			}, this, global::System.Threading.CancellationToken.None, global::System.Threading.Tasks.TaskContinuationOptions.None, global::Unity.Services.Core.Internal.TaskAsyncOperation.Scheduler);
		}

		public static global::Unity.Services.Core.Internal.TaskAsyncOperation<T> Run(global::System.Func<T> func)
		{
			global::System.Threading.Tasks.Task<T> task = new global::System.Threading.Tasks.Task<T>(func);
			global::Unity.Services.Core.Internal.TaskAsyncOperation<T> result = new global::Unity.Services.Core.Internal.TaskAsyncOperation<T>(task);
			task.Start();
			return result;
		}
	}
}
