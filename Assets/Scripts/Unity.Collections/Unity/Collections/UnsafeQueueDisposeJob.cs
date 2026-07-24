namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	internal struct UnsafeQueueDisposeJob : global::Unity.Jobs.IJob
	{
		public global::Unity.Collections.UnsafeQueueDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
