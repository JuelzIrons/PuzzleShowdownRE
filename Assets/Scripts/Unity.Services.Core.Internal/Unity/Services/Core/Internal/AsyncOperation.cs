namespace Unity.Services.Core.Internal
{
	internal class AsyncOperation : global::Unity.Services.Core.Internal.IAsyncOperation, global::System.Collections.IEnumerator
	{
		protected global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation> m_CompletedCallback;

		public bool IsDone { get; protected set; }

		public global::Unity.Services.Core.Internal.AsyncOperationStatus Status { get; protected set; }

		public global::System.Exception Exception { get; protected set; }

		object global::System.Collections.IEnumerator.Current => null;

		public event global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation> Completed
		{
			add
			{
				if (IsDone)
				{
					value(this);
				}
				else
				{
					m_CompletedCallback = (global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation>)global::System.Delegate.Combine(m_CompletedCallback, value);
				}
			}
			remove
			{
				m_CompletedCallback = (global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation>)global::System.Delegate.Remove(m_CompletedCallback, value);
			}
		}

		public void SetInProgress()
		{
			Status = global::Unity.Services.Core.Internal.AsyncOperationStatus.InProgress;
		}

		public void Succeed()
		{
			if (!IsDone)
			{
				IsDone = true;
				Status = global::Unity.Services.Core.Internal.AsyncOperationStatus.Succeeded;
				m_CompletedCallback?.Invoke(this);
				m_CompletedCallback = null;
			}
		}

		public void Fail(global::System.Exception reason)
		{
			if (!IsDone)
			{
				Exception = reason;
				IsDone = true;
				Status = global::Unity.Services.Core.Internal.AsyncOperationStatus.Failed;
				m_CompletedCallback?.Invoke(this);
				m_CompletedCallback = null;
			}
		}

		public void Cancel()
		{
			if (!IsDone)
			{
				Exception = new global::System.OperationCanceledException();
				IsDone = true;
				Status = global::Unity.Services.Core.Internal.AsyncOperationStatus.Cancelled;
				m_CompletedCallback?.Invoke(this);
				m_CompletedCallback = null;
			}
		}

		bool global::System.Collections.IEnumerator.MoveNext()
		{
			return !IsDone;
		}

		void global::System.Collections.IEnumerator.Reset()
		{
		}
	}
	internal class AsyncOperation<T> : global::Unity.Services.Core.Internal.IAsyncOperation<T>, global::System.Collections.IEnumerator
	{
		protected global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation<T>> m_CompletedCallback;

		public bool IsDone { get; protected set; }

		public global::Unity.Services.Core.Internal.AsyncOperationStatus Status { get; protected set; }

		public global::System.Exception Exception { get; protected set; }

		public T Result { get; protected set; }

		object global::System.Collections.IEnumerator.Current => null;

		public event global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation<T>> Completed
		{
			add
			{
				if (IsDone)
				{
					value(this);
				}
				else
				{
					m_CompletedCallback = (global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation<T>>)global::System.Delegate.Combine(m_CompletedCallback, value);
				}
			}
			remove
			{
				m_CompletedCallback = (global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation<T>>)global::System.Delegate.Remove(m_CompletedCallback, value);
			}
		}

		public void SetInProgress()
		{
			Status = global::Unity.Services.Core.Internal.AsyncOperationStatus.InProgress;
		}

		public void Succeed(T result)
		{
			if (!IsDone)
			{
				Result = result;
				IsDone = true;
				Status = global::Unity.Services.Core.Internal.AsyncOperationStatus.Succeeded;
				m_CompletedCallback?.Invoke(this);
				m_CompletedCallback = null;
			}
		}

		public void Fail(global::System.Exception reason)
		{
			if (!IsDone)
			{
				Exception = reason;
				IsDone = true;
				Status = global::Unity.Services.Core.Internal.AsyncOperationStatus.Failed;
				m_CompletedCallback?.Invoke(this);
				m_CompletedCallback = null;
			}
		}

		public void Cancel()
		{
			if (!IsDone)
			{
				Exception = new global::System.OperationCanceledException();
				IsDone = true;
				Status = global::Unity.Services.Core.Internal.AsyncOperationStatus.Cancelled;
				m_CompletedCallback?.Invoke(this);
				m_CompletedCallback = null;
			}
		}

		bool global::System.Collections.IEnumerator.MoveNext()
		{
			return !IsDone;
		}

		void global::System.Collections.IEnumerator.Reset()
		{
		}
	}
}
