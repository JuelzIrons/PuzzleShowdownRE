namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct NativeHashMapDispose
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<int, int>* m_HashMapData;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_Allocator;

		internal unsafe void Dispose()
		{
			global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<int>* hashMapData = (global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<int>*)m_HashMapData;
			global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<int>.Free(hashMapData);
		}
	}
}
