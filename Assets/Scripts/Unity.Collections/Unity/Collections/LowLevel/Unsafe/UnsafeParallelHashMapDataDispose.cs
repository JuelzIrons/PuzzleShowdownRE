namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct UnsafeParallelHashMapDataDispose
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;

		public unsafe void Dispose()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.DeallocateHashMap(m_Buffer, m_AllocatorLabel);
		}
	}
}
