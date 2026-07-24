namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	internal struct NativeRingQueueDisposeJob : global::Unity.Jobs.IJob
	{
		public global::Unity.Collections.NativeRingQueueDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
