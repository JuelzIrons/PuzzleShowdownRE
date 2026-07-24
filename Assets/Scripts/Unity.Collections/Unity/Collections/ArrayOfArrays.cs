namespace Unity.Collections
{
	internal struct ArrayOfArrays<T> : global::System.IDisposable where T : unmanaged
	{
		private global::Unity.Collections.AllocatorManager.AllocatorHandle m_backingAllocatorHandle;

		private int m_lengthInElements;

		private int m_capacityInElements;

		private int m_log2BlockSizeInElements;

		private int m_blocks;

		private unsafe global::System.IntPtr* m_block;

		private int BlockSizeInElements => 1 << m_log2BlockSizeInElements;

		private unsafe int BlockSizeInBytes => BlockSizeInElements * sizeof(T);

		private int BlockMask => BlockSizeInElements - 1;

		public int Length => m_lengthInElements;

		public int Capacity => m_capacityInElements;

		public unsafe ref T this[int elementIndex]
		{
			get
			{
				int num = BlockIndexOfElement(elementIndex);
				global::System.IntPtr intPtr = m_block[num];
				int num2 = elementIndex & BlockMask;
				T* ptr = (T*)(void*)intPtr;
				return ref ptr[num2];
			}
		}

		public unsafe ArrayOfArrays(int capacityInElements, global::Unity.Collections.AllocatorManager.AllocatorHandle backingAllocatorHandle, int log2BlockSizeInElements = 12)
		{
			this = default(global::Unity.Collections.ArrayOfArrays<T>);
			m_backingAllocatorHandle = backingAllocatorHandle;
			m_lengthInElements = 0;
			m_capacityInElements = capacityInElements;
			m_log2BlockSizeInElements = log2BlockSizeInElements;
			m_blocks = capacityInElements + BlockMask >> m_log2BlockSizeInElements;
			m_block = (global::System.IntPtr*)global::Unity.Collections.Memory.Unmanaged.Allocate(sizeof(global::System.IntPtr) * m_blocks, 16, m_backingAllocatorHandle);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemSet(m_block, 0, sizeof(global::System.IntPtr) * m_blocks);
		}

		public unsafe void LockfreeAdd(T t)
		{
			int elementIndex = global::System.Threading.Interlocked.Increment(ref m_lengthInElements) - 1;
			int i = BlockIndexOfElement(elementIndex);
			if (m_block[i] == global::System.IntPtr.Zero)
			{
				void* ptr = global::Unity.Collections.Memory.Unmanaged.Allocate(BlockSizeInBytes, 16, m_backingAllocatorHandle);
				int num;
				for (num = global::Unity.Mathematics.math.min(m_blocks, i + 4); i < num && !(global::System.IntPtr.Zero == global::System.Threading.Interlocked.CompareExchange(ref m_block[i], (global::System.IntPtr)ptr, global::System.IntPtr.Zero)); i++)
				{
				}
				if (i == num)
				{
					global::Unity.Collections.Memory.Unmanaged.Free(ptr, m_backingAllocatorHandle);
				}
			}
			this[elementIndex] = t;
		}

		public void Rewind()
		{
			m_lengthInElements = 0;
		}

		public unsafe void Clear()
		{
			Rewind();
			for (int i = 0; i < m_blocks; i++)
			{
				if (m_block[i] != global::System.IntPtr.Zero)
				{
					global::Unity.Collections.Memory.Unmanaged.Free((void*)m_block[i], m_backingAllocatorHandle);
					m_block[i] = global::System.IntPtr.Zero;
				}
			}
		}

		public unsafe void Dispose()
		{
			Clear();
			global::Unity.Collections.Memory.Unmanaged.Free(m_block, m_backingAllocatorHandle);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckElementIndex(int elementIndex)
		{
			if (elementIndex >= m_lengthInElements)
			{
				throw new global::System.ArgumentException($"Element index {elementIndex} must be less than length in elements {m_lengthInElements}.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckBlockIndex(int blockIndex)
		{
			if (blockIndex >= m_blocks)
			{
				throw new global::System.ArgumentException($"Block index {blockIndex} must be less than number of blocks {m_blocks}.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private unsafe void CheckBlockIsNotNull(int blockIndex)
		{
			if (m_block[blockIndex] == global::System.IntPtr.Zero)
			{
				throw new global::System.ArgumentException($"Block index {blockIndex} is a null pointer.");
			}
		}

		public void RemoveAtSwapBack(int elementIndex)
		{
			this[elementIndex] = this[Length - 1];
			m_lengthInElements--;
		}

		private int BlockIndexOfElement(int elementIndex)
		{
			return elementIndex >> m_log2BlockSizeInElements;
		}

		public unsafe void TrimExcess()
		{
			for (int i = BlockIndexOfElement(m_lengthInElements + BlockMask); i < m_blocks; i++)
			{
				if (m_block[i] != global::System.IntPtr.Zero)
				{
					global::Unity.Collections.Memory.Unmanaged.Free((void*)m_block[i], m_backingAllocatorHandle);
					m_block[i] = global::System.IntPtr.Zero;
				}
			}
		}
	}
}
