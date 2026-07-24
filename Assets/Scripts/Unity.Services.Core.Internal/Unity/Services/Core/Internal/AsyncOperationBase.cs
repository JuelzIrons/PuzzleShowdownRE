namespace Unity.Services.Core.Internal
{
	internal abstract class AsyncOperationBase : global::UnityEngine.CustomYieldInstruction, global::Unity.Services.Core.Internal.IAsyncOperation, global::System.Collections.IEnumerator, global::System.Runtime.CompilerServices.INotifyCompletion
	{
		private global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation> m_CompletedCallback;

		public override bool keepWaiting => !IsCompleted;

		public abstract bool IsCompleted { get; }

		public bool IsDone => IsCompleted;

		public abstract global::Unity.Services.Core.Internal.AsyncOperationStatus Status { get; }

		public abstract global::System.Exception Exception { get; }

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

		public abstract void GetResult();

		public abstract global::Unity.Services.Core.Internal.AsyncOperationBase GetAwaiter();

		protected void DidComplete()
		{
			m_CompletedCallback?.Invoke(this);
		}

		public virtual void OnCompleted(global::System.Action continuation)
		{
			Completed += delegate
			{
				continuation?.Invoke();
			};
		}
	}
	internal abstract class AsyncOperationBase<T> : global::UnityEngine.CustomYieldInstruction, global::Unity.Services.Core.Internal.IAsyncOperation<T>, global::System.Collections.IEnumerator, global::System.Runtime.CompilerServices.INotifyCompletion
	{
		private global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation<T>> m_CompletedCallback;

		public override bool keepWaiting => !IsCompleted;

		public abstract bool IsCompleted { get; }

		public bool IsDone => IsCompleted;

		public abstract global::Unity.Services.Core.Internal.AsyncOperationStatus Status { get; }

		public abstract global::System.Exception Exception { get; }

		public abstract T Result { get; }

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

		public abstract T GetResult();

		public abstract global::Unity.Services.Core.Internal.AsyncOperationBase<T> GetAwaiter();

		protected void DidComplete()
		{
			m_CompletedCallback?.Invoke(this);
		}

		public virtual void OnCompleted(global::System.Action continuation)
		{
			Completed += delegate
			{
				continuation?.Invoke();
			};
		}
	}
}
