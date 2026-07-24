namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class NativeParallelMultiHashMapExtensions
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int),
			typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle)
		})]
		internal static void Initialize<TKey, TValue, U>(this ref global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue> container, int capacity, ref U allocator) where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			container.m_MultiHashMapData = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>(capacity, allocator.Handle);
		}
	}
}
