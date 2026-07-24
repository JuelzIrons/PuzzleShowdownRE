namespace Unity.Collections.LowLevel.Unsafe
{
	internal struct UntypedUnsafeList
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe readonly void* Ptr;

		internal readonly int m_length;

		internal readonly int m_capacity;

		internal readonly global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

		internal readonly int padding;
	}
}
