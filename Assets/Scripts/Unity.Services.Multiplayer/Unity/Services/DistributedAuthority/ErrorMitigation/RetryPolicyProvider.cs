namespace Unity.Services.DistributedAuthority.ErrorMitigation
{
	internal class RetryPolicyProvider : global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicyProvider
	{
		private global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		public RetryPolicyProvider(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler)
		{
			m_ActionScheduler = actionScheduler;
		}

		public global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicy<T>.ForOperation(m_ActionScheduler, operation);
		}

		public global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> ForOperation<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> operation)
		{
			return global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicy<T>.ForOperation(m_ActionScheduler, operation);
		}
	}
}
