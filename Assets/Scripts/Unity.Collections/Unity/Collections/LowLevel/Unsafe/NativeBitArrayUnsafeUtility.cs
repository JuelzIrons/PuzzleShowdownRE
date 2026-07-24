namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class NativeBitArrayUnsafeUtility
	{
		public unsafe static global::Unity.Collections.NativeBitArray ConvertExistingDataToNativeBitArray(void* ptr, int sizeInBytes, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray* ptr2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray.Alloc(global::Unity.Collections.Allocator.Persistent);
			*ptr2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray(ptr, sizeInBytes, allocator);
			return new global::Unity.Collections.NativeBitArray
			{
				m_BitArray = ptr2,
				m_Allocator = global::Unity.Collections.Allocator.Persistent
			};
		}
	}
}
