namespace Unity.Services.Matchmaker.ErrorMitigation
{
	internal interface IRetryPolicyProvider
	{
		global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation);

		global::Unity.Services.Matchmaker.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation);
	}
}
