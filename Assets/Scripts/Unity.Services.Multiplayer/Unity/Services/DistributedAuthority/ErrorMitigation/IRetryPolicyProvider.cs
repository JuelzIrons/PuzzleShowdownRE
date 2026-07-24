namespace Unity.Services.DistributedAuthority.ErrorMitigation
{
	internal interface IRetryPolicyProvider
	{
		global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation);

		global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation);
	}
}
