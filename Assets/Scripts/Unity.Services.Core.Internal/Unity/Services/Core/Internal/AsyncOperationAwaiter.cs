namespace Unity.Services.Core.Internal
{
	internal struct AsyncOperationAwaiter : global::Unity.Services.Core.Internal.IAsyncOperationAwaiter, global::System.Runtime.CompilerServices.ICriticalNotifyCompletion, global::System.Runtime.CompilerServices.INotifyCompletion
	{
		private global::Unity.Services.Core.Internal.IAsyncOperation m_Operation;

		public bool IsCompleted => m_Operation.IsDone;

		public AsyncOperationAwaiter(global::Unity.Services.Core.Internal.IAsyncOperation asyncOperation)
		{
			m_Operation = asyncOperation;
		}

		public void OnCompleted(global::System.Action continuation)
		{
			m_Operation.Completed += delegate
			{
				continuation();
			};
		}

		public void UnsafeOnCompleted(global::System.Action continuation)
		{
			m_Operation.Completed += delegate
			{
				continuation();
			};
		}

		public void GetResult()
		{
			if (m_Operation.Status == global::Unity.Services.Core.Internal.AsyncOperationStatus.Failed || m_Operation.Status == global::Unity.Services.Core.Internal.AsyncOperationStatus.Cancelled)
			{
				throw m_Operation.Exception;
			}
		}
	}
	internal struct AsyncOperationAwaiter<T> : global::Unity.Services.Core.Internal.IAsyncOperationAwaiter<T>, global::System.Runtime.CompilerServices.ICriticalNotifyCompletion, global::System.Runtime.CompilerServices.INotifyCompletion
	{
		private global::Unity.Services.Core.Internal.IAsyncOperation<T> m_Operation;

		public bool IsCompleted => m_Operation.IsDone;

		public AsyncOperationAwaiter(global::Unity.Services.Core.Internal.IAsyncOperation<T> asyncOperation)
		{
			m_Operation = asyncOperation;
		}

		public void OnCompleted(global::System.Action continuation)
		{
			m_Operation.Completed += delegate
			{
				continuation();
			};
		}

		public void UnsafeOnCompleted(global::System.Action continuation)
		{
			m_Operation.Completed += delegate
			{
				continuation();
			};
		}

		public T GetResult()
		{
			if (m_Operation.Status == global::Unity.Services.Core.Internal.AsyncOperationStatus.Failed || m_Operation.Status == global::Unity.Services.Core.Internal.AsyncOperationStatus.Cancelled)
			{
				throw m_Operation.Exception;
			}
			return m_Operation.Result;
		}
	}
}
