namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct NativeBitArrayDispose
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray* m_BitArrayData;

		public global::Unity.Collections.AllocatorManager.AllocatorHandle m_Allocator;

		public unsafe void Dispose()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray.Free(m_BitArrayData, m_Allocator);
		}
	}
}
