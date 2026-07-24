namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	internal struct NativeQueueDisposeJob : global::Unity.Jobs.IJob
	{
		public global::Unity.Collections.NativeQueueDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
