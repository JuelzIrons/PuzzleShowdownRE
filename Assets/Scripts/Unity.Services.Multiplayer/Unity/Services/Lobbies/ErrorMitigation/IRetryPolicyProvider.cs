namespace Unity.Services.Lobbies.ErrorMitigation
{
	internal interface IRetryPolicyProvider
	{
		global::Unity.Services.Lobbies.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation);

		global::Unity.Services.Lobbies.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation);
	}
}
