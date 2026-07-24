namespace Unity.Services.Relay.ErrorMitigation
{
	internal class RetryPolicyProvider : global::Unity.Services.Relay.ErrorMitigation.IRetryPolicyProvider
	{
		public global::Unity.Services.Relay.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.Relay.ErrorMitigation.RetryPolicy<T>.ForOperation(operation);
		}

		public global::Unity.Services.Relay.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.Relay.ErrorMitigation.RetryPolicy<T>.ForOperation(operation);
		}
	}
}
