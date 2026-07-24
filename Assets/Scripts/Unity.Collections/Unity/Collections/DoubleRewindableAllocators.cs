namespace Unity.Collections
{
	public struct DoubleRewindableAllocators : global::System.IDisposable
	{
		private unsafe global::Unity.Collections.RewindableAllocator* Pointer;

		private global::Unity.Collections.AllocatorHelper<global::Unity.Collections.RewindableAllocator> UpdateAllocatorHelper0;

		private global::Unity.Collections.AllocatorHelper<global::Unity.Collections.RewindableAllocator> UpdateAllocatorHelper1;

		public unsafe ref global::Unity.Collections.RewindableAllocator Allocator => ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<global::Unity.Collections.RewindableAllocator>(Pointer);

		public unsafe bool IsCreated => Pointer != null;

		internal bool EnableBlockFree
		{
			get
			{
				return UpdateAllocatorHelper0.Allocator.EnableBlockFree;
			}
			set
			{
				UpdateAllocatorHelper0.Allocator.EnableBlockFree = value;
				UpdateAllocatorHelper1.Allocator.EnableBlockFree = value;
			}
		}

		public unsafe void Update()
		{
			global::Unity.Collections.RewindableAllocator* ptr = (global::Unity.Collections.RewindableAllocator*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref UpdateAllocatorHelper0.Allocator);
			global::Unity.Collections.RewindableAllocator* ptr2 = (global::Unity.Collections.RewindableAllocator*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref UpdateAllocatorHelper1.Allocator);
			Pointer = ((Pointer == ptr) ? ptr2 : ptr);
			Allocator.Rewind();
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckIsCreated()
		{
			if (!IsCreated)
			{
				throw new global::System.InvalidOperationException("DoubleRewindableAllocators is not created.");
			}
		}

		public DoubleRewindableAllocators(global::Unity.Collections.AllocatorManager.AllocatorHandle backingAllocator, int initialSizeInBytes)
		{
			this = default(global::Unity.Collections.DoubleRewindableAllocators);
			Initialize(backingAllocator, initialSizeInBytes);
		}

		public unsafe void Initialize(global::Unity.Collections.AllocatorManager.AllocatorHandle backingAllocator, int initialSizeInBytes)
		{
			UpdateAllocatorHelper0 = new global::Unity.Collections.AllocatorHelper<global::Unity.Collections.RewindableAllocator>(backingAllocator);
			UpdateAllocatorHelper1 = new global::Unity.Collections.AllocatorHelper<global::Unity.Collections.RewindableAllocator>(backingAllocator);
			UpdateAllocatorHelper0.Allocator.Initialize(initialSizeInBytes);
			UpdateAllocatorHelper1.Allocator.Initialize(initialSizeInBytes);
			Pointer = null;
			Update();
		}

		public void Dispose()
		{
			if (IsCreated)
			{
				UpdateAllocatorHelper0.Allocator.Dispose();
				UpdateAllocatorHelper1.Allocator.Dispose();
				UpdateAllocatorHelper0.Dispose();
				UpdateAllocatorHelper1.Dispose();
			}
		}
	}
}
