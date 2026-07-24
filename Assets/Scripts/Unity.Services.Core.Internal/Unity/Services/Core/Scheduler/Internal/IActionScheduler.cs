namespace Unity.Services.Core.Scheduler.Internal
{
	public interface IActionScheduler : global::Unity.Services.Core.Internal.IServiceComponent
	{
		long ScheduleAction(global::System.Action action, double delaySeconds = 0.0);

		void CancelAction(long actionId);
	}
}
