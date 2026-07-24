namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct UnsafeStreamBlockData
	{
		internal const int AllocationSize = 4096;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock** Blocks;

		internal int BlockCount;

		internal global::Unity.Collections.AllocatorManager.Block Ranges;

		internal int RangeCount;

		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* Allocate(global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* oldBlock, int threadIndex)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock*)global::Unity.Collections.Memory.Unmanaged.Array.Resize(null, 0L, 4096L, Allocator, 1L, 16);
			ptr->Next = null;
			if (oldBlock == null)
			{
				ptr->Next = Blocks[threadIndex];
				Blocks[threadIndex] = ptr;
			}
			else
			{
				ptr->Next = oldBlock->Next;
				oldBlock->Next = ptr;
			}
			return ptr;
		}

		internal unsafe void Free(global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* oldBlock)
		{
			global::Unity.Collections.Memory.Unmanaged.Array.Resize(oldBlock, 4096L, 0L, Allocator, 1L, 16);
		}
	}
}
