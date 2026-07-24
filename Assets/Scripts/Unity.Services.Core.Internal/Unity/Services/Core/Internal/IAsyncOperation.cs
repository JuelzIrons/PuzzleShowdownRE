namespace Unity.Services.Core.Internal
{
	internal interface IAsyncOperation : global::System.Collections.IEnumerator
	{
		bool IsDone { get; }

		global::Unity.Services.Core.Internal.AsyncOperationStatus Status { get; }

		global::System.Exception Exception { get; }

		event global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation> Completed;
	}
	internal interface IAsyncOperation<out T> : global::System.Collections.IEnumerator
	{
		bool IsDone { get; }

		global::Unity.Services.Core.Internal.AsyncOperationStatus Status { get; }

		global::System.Exception Exception { get; }

		T Result { get; }

		event global::System.Action<global::Unity.Services.Core.Internal.IAsyncOperation<T>> Completed;
	}
}
