namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Burst.BurstCompile]
	internal struct UnsafeParallelHashMapDataDisposeJob : global::Unity.Jobs.IJob
	{
		internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
