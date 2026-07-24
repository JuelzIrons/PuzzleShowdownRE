namespace Unity.Services.Relay.ErrorMitigation
{
	internal interface IRetryPolicyProvider
	{
		global::Unity.Services.Relay.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation);

		global::Unity.Services.Relay.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation);
	}
}
