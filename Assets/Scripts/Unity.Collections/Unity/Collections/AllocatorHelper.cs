namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle) })]
	public struct AllocatorHelper<T> : global::System.IDisposable where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
	{
		private unsafe readonly T* m_allocator;

		private global::Unity.Collections.AllocatorManager.AllocatorHandle m_backingAllocator;

		public unsafe ref T Allocator => ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(m_allocator);

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("CreateAllocator is unburstable")]
		public unsafe AllocatorHelper(global::Unity.Collections.AllocatorManager.AllocatorHandle backingAllocator, bool isGlobal = false, int globalIndex = 0)
		{
			m_allocator = (T*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref global::Unity.Collections.AllocatorManager.CreateAllocator<T>(backingAllocator, isGlobal, globalIndex));
			m_backingAllocator = backingAllocator;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("DestroyAllocator is unburstable")]
		public unsafe void Dispose()
		{
			global::Unity.Collections.AllocatorManager.DestroyAllocator(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(m_allocator), m_backingAllocator);
		}
	}
}
