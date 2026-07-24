namespace Unity.Services.Lobbies.ErrorMitigation
{
	internal class RetryPolicyProvider : global::Unity.Services.Lobbies.ErrorMitigation.IRetryPolicyProvider
	{
		public global::Unity.Services.Lobbies.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.Lobbies.ErrorMitigation.RetryPolicy<T>.ForOperation(operation);
		}

		public global::Unity.Services.Lobbies.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.Lobbies.ErrorMitigation.RetryPolicy<T>.ForOperation(operation);
		}
	}
}
