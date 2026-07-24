namespace Unity.Services.Core.Internal
{
	internal static class AsyncOperationExtensions
	{
		public static global::Unity.Services.Core.Internal.AsyncOperationAwaiter GetAwaiter(this global::Unity.Services.Core.Internal.IAsyncOperation self)
		{
			return new global::Unity.Services.Core.Internal.AsyncOperationAwaiter(self);
		}

		public static global::System.Threading.Tasks.Task AsTask(this global::Unity.Services.Core.Internal.IAsyncOperation self)
		{
			if (self.Status == global::Unity.Services.Core.Internal.AsyncOperationStatus.Succeeded)
			{
				return global::System.Threading.Tasks.Task.CompletedTask;
			}
			global::System.Threading.Tasks.TaskCompletionSource<object> taskCompletionSource = new global::System.Threading.Tasks.TaskCompletionSource<object>();
			if (self.IsDone)
			{
				CompleteTask(self);
			}
			else
			{
				self.Completed += CompleteTask;
			}
			return taskCompletionSource.Task;
			void CompleteTask(global::Unity.Services.Core.Internal.IAsyncOperation operation)
			{
				switch (operation.Status)
				{
				case global::Unity.Services.Core.Internal.AsyncOperationStatus.Failed:
					taskCompletionSource.TrySetException(operation.Exception);
					break;
				case global::Unity.Services.Core.Internal.AsyncOperationStatus.Cancelled:
					taskCompletionSource.TrySetCanceled();
					break;
				case global::Unity.Services.Core.Internal.AsyncOperationStatus.Succeeded:
					taskCompletionSource.TrySetResult(null);
					break;
				default:
					throw new global::System.ArgumentOutOfRangeException();
				}
			}
		}

		public static global::Unity.Services.Core.Internal.AsyncOperationAwaiter<T> GetAwaiter<T>(this global::Unity.Services.Core.Internal.IAsyncOperation<T> self)
		{
			return new global::Unity.Services.Core.Internal.AsyncOperationAwaiter<T>(self);
		}

		public static global::System.Threading.Tasks.Task<T> AsTask<T>(this global::Unity.Services.Core.Internal.IAsyncOperation<T> self)
		{
			global::System.Threading.Tasks.TaskCompletionSource<T> taskCompletionSource = new global::System.Threading.Tasks.TaskCompletionSource<T>();
			if (self.IsDone)
			{
				CompleteTask(self);
			}
			else
			{
				self.Completed += CompleteTask;
			}
			return taskCompletionSource.Task;
			void CompleteTask(global::Unity.Services.Core.Internal.IAsyncOperation<T> operation)
			{
				switch (operation.Status)
				{
				case global::Unity.Services.Core.Internal.AsyncOperationStatus.Succeeded:
					taskCompletionSource.TrySetResult(operation.Result);
					break;
				case global::Unity.Services.Core.Internal.AsyncOperationStatus.Failed:
					taskCompletionSource.TrySetException(operation.Exception);
					break;
				case global::Unity.Services.Core.Internal.AsyncOperationStatus.Cancelled:
					taskCompletionSource.TrySetCanceled();
					break;
				default:
					throw new global::System.ArgumentOutOfRangeException();
				}
			}
		}
	}
}
