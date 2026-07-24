namespace Unity.Services.Core.Internal
{
	internal interface IAsyncOperationAwaiter : global::System.Runtime.CompilerServices.ICriticalNotifyCompletion, global::System.Runtime.CompilerServices.INotifyCompletion
	{
		bool IsCompleted { get; }

		void GetResult();
	}
	internal interface IAsyncOperationAwaiter<out T> : global::System.Runtime.CompilerServices.ICriticalNotifyCompletion, global::System.Runtime.CompilerServices.INotifyCompletion
	{
		bool IsCompleted { get; }

		T GetResult();
	}
}
