namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Burst.BurstCompile]
	internal struct UnsafeParallelHashMapDisposeJob : global::Unity.Jobs.IJob
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* Data;

		public global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

		public unsafe void Execute()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.DeallocateHashMap(Data, Allocator);
		}
	}
}
