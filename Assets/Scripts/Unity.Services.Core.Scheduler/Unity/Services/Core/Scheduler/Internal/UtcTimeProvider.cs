namespace Unity.Services.Core.Scheduler.Internal
{
	internal class UtcTimeProvider : global::Unity.Services.Core.Scheduler.Internal.ITimeProvider
	{
		public global::System.DateTime Now => global::System.DateTime.UtcNow;
	}
}
