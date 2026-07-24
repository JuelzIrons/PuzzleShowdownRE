namespace Unity.Services.Core.Scheduler.Internal
{
	internal class ScheduledInvocationComparer : global::System.Collections.Generic.IComparer<global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation>
	{
		public int Compare(global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation x, global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation y)
		{
			if (x == y)
			{
				return 0;
			}
			if (y == null)
			{
				return 1;
			}
			if (x == null)
			{
				return -1;
			}
			int num = x.InvocationTime.CompareTo(y.InvocationTime);
			if (num == 0)
			{
				num = x.ActionId.CompareTo(y.ActionId);
			}
			return num;
		}
	}
}
