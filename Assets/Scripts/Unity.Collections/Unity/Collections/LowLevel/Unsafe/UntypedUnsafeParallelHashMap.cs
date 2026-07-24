namespace Unity.Collections.LowLevel.Unsafe
{
	public struct UntypedUnsafeParallelHashMap
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;

		private global::Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
