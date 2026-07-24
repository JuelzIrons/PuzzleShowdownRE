namespace Unity.Services.Matchmaker.ErrorMitigation
{
	internal class RetryPolicyProvider : global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicyProvider
	{
		public global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.Matchmaker.ErrorMitigation.RetryPolicy<T>.ForOperation(operation);
		}

		public global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.Matchmaker.ErrorMitigation.RetryPolicy<T>.ForOperation(operation);
		}
	}
}
