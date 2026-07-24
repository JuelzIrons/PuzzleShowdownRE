namespace Unity.Services.Qos.V2.ErrorMitigation
{
	internal interface IRetryPolicyProvider
	{
		global::Unity.Services.Qos.V2.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation);

		global::Unity.Services.Qos.V2.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation);
	}
}
