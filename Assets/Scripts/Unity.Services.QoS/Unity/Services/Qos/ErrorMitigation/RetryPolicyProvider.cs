namespace Unity.Services.Qos.ErrorMitigation
{
	internal class RetryPolicyProvider : global::Unity.Services.Qos.ErrorMitigation.IRetryPolicyProvider
	{
		public global::Unity.Services.Qos.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.Qos.ErrorMitigation.RetryPolicy<T>.ForOperation(operation);
		}

		public global::Unity.Services.Qos.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.Qos.ErrorMitigation.RetryPolicy<T>.ForOperation(operation);
		}
	}
}
