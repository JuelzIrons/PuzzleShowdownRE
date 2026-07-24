namespace Unity.Services.DistributedAuthority.ErrorMitigation
{
	internal interface IRetryPolicy<T>
	{
		global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> WithJitterMagnitude(float magnitude);

		global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> WithDelayScale(float scale);

		global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> WithMaxDelayTime(float time);

		global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> WithRetryCondition(global::System.Func<T, global::System.Threading.Tasks.Task<bool>> shouldRetry);

		global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> UptoMaximumRetries(uint amount);

		global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> HandleException<TException>() where TException : global::System.Exception;

		global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> HandleException<TException>(global::System.Func<TException, bool> condition) where TException : global::System.Exception;

		global::System.Threading.Tasks.Task<T> RunAsync(global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicyConfig retryPolicyConfig = null);
	}
}
