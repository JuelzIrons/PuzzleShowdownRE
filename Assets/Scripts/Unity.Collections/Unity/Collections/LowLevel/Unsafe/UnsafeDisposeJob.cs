namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Burst.BurstCompile]
	internal struct UnsafeDisposeJob : global::Unity.Jobs.IJob
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe void* Ptr;

		public global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

		public unsafe void Execute()
		{
			global::Unity.Collections.AllocatorManager.Free(Allocator, Ptr);
		}
	}
}
