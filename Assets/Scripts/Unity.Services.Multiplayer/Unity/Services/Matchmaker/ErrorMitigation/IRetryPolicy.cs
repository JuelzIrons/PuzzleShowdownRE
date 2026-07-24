namespace Unity.Services.Matchmaker.ErrorMitigation
{
	internal interface IRetryPolicy<T>
	{
		global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> WithJitterMagnitude(float magnitude);

		global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> WithDelayScale(float scale);

		global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> WithMaxDelayTime(float time);

		global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> WithRetryCondition(global::System.Func<T, global::System.Threading.Tasks.Task<bool>> shouldRetry);

		global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> UptoMaximumRetries(uint amount);

		global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> HandleException<TException>() where TException : global::System.Exception;

		global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> HandleException<TException>(global::System.Func<TException, bool> condition) where TException : global::System.Exception;

		global::System.Threading.Tasks.Task<T> RunAsync(global::Unity.Services.Matchmaker.ErrorMitigation.RetryPolicyConfig retryPolicyConfig = null);
	}
}
