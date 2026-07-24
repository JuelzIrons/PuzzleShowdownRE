namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal struct BlockAllocator : global::System.IDisposable
	{
		public struct Block
		{
			public int offset;

			public int count;

			public static readonly global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block Invalid = new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block
			{
				offset = 0,
				count = 0
			};
		}

		public struct Allocation
		{
			public int handle;

			public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block block;

			public static readonly global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation Invalid = new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation
			{
				handle = -1
			};

			public readonly bool valid => handle != -1;
		}

		private int m_FreeElementCount;

		private int m_MaxElementCount;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block> m_freeBlocks;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block> m_usedBlocks;

		private global::Unity.Collections.NativeList<int> m_freeSlots;

		public int freeElementsCount => m_FreeElementCount;

		public int freeBlocks => m_freeBlocks.Length;

		public int capacity => m_MaxElementCount;

		public int allocatedSize => m_MaxElementCount - m_FreeElementCount;

		public void Initialize(int maxElementCounts)
		{
			m_MaxElementCount = maxElementCounts;
			m_FreeElementCount = maxElementCounts;
			if (!m_freeBlocks.IsCreated)
			{
				m_freeBlocks = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block>(global::Unity.Collections.Allocator.Persistent);
			}
			else
			{
				m_freeBlocks.Clear();
			}
			m_freeBlocks.Add(new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block
			{
				offset = 0,
				count = m_FreeElementCount
			});
			if (!m_usedBlocks.IsCreated)
			{
				m_usedBlocks = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block>(global::Unity.Collections.Allocator.Persistent);
			}
			else
			{
				m_usedBlocks.Clear();
			}
			if (!m_freeSlots.IsCreated)
			{
				m_freeSlots = new global::Unity.Collections.NativeList<int>(global::Unity.Collections.Allocator.Persistent);
			}
			else
			{
				m_freeSlots.Clear();
			}
		}

		private int CalculateGeometricGrowthCapacity(int desiredNewCapacity, int maxAllowedNewCapacity)
		{
			int num = capacity;
			if (num > maxAllowedNewCapacity - num / 2)
			{
				return maxAllowedNewCapacity;
			}
			int num2 = num + num / 2;
			if (num2 < desiredNewCapacity)
			{
				return desiredNewCapacity;
			}
			return num2;
		}

		public int Grow(int newDesiredCapacity, int maxAllowedCapacity = int.MaxValue)
		{
			int num = CalculateGeometricGrowthCapacity(newDesiredCapacity, maxAllowedCapacity);
			int maxElementCount = m_MaxElementCount;
			int num2 = num - maxElementCount;
			m_FreeElementCount += num2;
			m_MaxElementCount = num;
			int num3 = m_freeBlocks.Length;
			ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block> reference = ref m_freeBlocks;
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block value = new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block
			{
				offset = maxElementCount,
				count = num2
			};
			reference.Add(in value);
			while (num3 != -1)
			{
				num3 = MergeBlockFrontBack(num3);
			}
			return m_MaxElementCount;
		}

		public bool GetExpectedGrowthToFitAllocation(int elementCounts, int maxAllowedCapacity, out int newCapacity)
		{
			newCapacity = 0;
			int num = (m_freeBlocks.IsEmpty ? elementCounts : global::Unity.Mathematics.math.max(elementCounts - m_freeBlocks[m_freeBlocks.Length - 1].count, 0));
			if (maxAllowedCapacity < capacity || maxAllowedCapacity - capacity < num)
			{
				return false;
			}
			newCapacity = ((num > 0) ? CalculateGeometricGrowthCapacity(capacity + num, maxAllowedCapacity) : capacity);
			return true;
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation GrowAndAllocate(int elementCounts, out int oldCapacity, out int newCapacity)
		{
			return GrowAndAllocate(elementCounts, int.MaxValue, out oldCapacity, out newCapacity);
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation GrowAndAllocate(int elementCounts, int maxAllowedCapacity, out int oldCapacity, out int newCapacity)
		{
			oldCapacity = capacity;
			int num = (m_freeBlocks.IsEmpty ? elementCounts : global::Unity.Mathematics.math.max(elementCounts - m_freeBlocks[m_freeBlocks.Length - 1].count, 0));
			if (maxAllowedCapacity < capacity || maxAllowedCapacity - capacity < num)
			{
				newCapacity = capacity;
				return global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid;
			}
			newCapacity = ((num > 0) ? Grow(capacity + num, maxAllowedCapacity) : capacity);
			return Allocate(elementCounts);
		}

		public void Dispose()
		{
			m_MaxElementCount = 0;
			m_FreeElementCount = 0;
			if (m_freeBlocks.IsCreated)
			{
				m_freeBlocks.Dispose();
			}
			if (m_usedBlocks.IsCreated)
			{
				m_usedBlocks.Dispose();
			}
			if (m_freeSlots.IsCreated)
			{
				m_freeSlots.Dispose();
			}
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation Allocate(int elementCounts)
		{
			if (elementCounts > m_FreeElementCount || m_freeBlocks.IsEmpty)
			{
				return global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid;
			}
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < m_freeBlocks.Length; i++)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block block = m_freeBlocks[i];
				if (elementCounts <= block.count && (num == -1 || block.count < num2))
				{
					num2 = block.count;
					num = i;
				}
			}
			if (num == -1)
			{
				return global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid;
			}
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block value = m_freeBlocks[num];
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block value2 = value;
			value2.offset += elementCounts;
			value2.count -= elementCounts;
			value.count = elementCounts;
			if (value2.count > 0)
			{
				m_freeBlocks[num] = value2;
			}
			else
			{
				m_freeBlocks.RemoveAtSwapBack(num);
			}
			int num3;
			if (m_freeSlots.IsEmpty)
			{
				num3 = m_usedBlocks.Length;
				m_usedBlocks.Add(in value);
			}
			else
			{
				num3 = m_freeSlots[m_freeSlots.Length - 1];
				m_freeSlots.RemoveAtSwapBack(m_freeSlots.Length - 1);
				m_usedBlocks[num3] = value;
			}
			m_FreeElementCount -= elementCounts;
			return new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation
			{
				handle = num3,
				block = value
			};
		}

		private int MergeBlockFrontBack(int freeBlockId)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block block = m_freeBlocks[freeBlockId];
			for (int i = 0; i < m_freeBlocks.Length; i++)
			{
				if (i == freeBlockId)
				{
					continue;
				}
				global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block value = m_freeBlocks[i];
				bool flag = false;
				if (block.offset == value.offset + value.count)
				{
					value.count += block.count;
					flag = true;
				}
				else if (value.offset == block.offset + block.count)
				{
					value.offset = block.offset;
					value.count += block.count;
					flag = true;
				}
				if (flag)
				{
					m_freeBlocks[i] = value;
					m_freeBlocks.RemoveAtSwapBack(freeBlockId);
					if (i != m_freeBlocks.Length)
					{
						return i;
					}
					return freeBlockId;
				}
			}
			return -1;
		}

		public void FreeAllocation(in global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation allocation)
		{
			m_freeSlots.Add(in allocation.handle);
			m_usedBlocks[allocation.handle] = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block.Invalid;
			int num = m_freeBlocks.Length;
			m_freeBlocks.Add(in allocation.block);
			while (num != -1)
			{
				num = MergeBlockFrontBack(num);
			}
			m_FreeElementCount += allocation.block.count;
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation[] SplitAllocation(in global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation allocation, int count)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation[] array = new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation[count];
			int num = allocation.block.count / count;
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block block = new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block
			{
				offset = allocation.block.offset,
				count = num
			};
			m_usedBlocks[allocation.handle] = block;
			array[0] = new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation
			{
				handle = allocation.handle,
				block = block
			};
			for (int i = 1; i < count; i++)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block value = new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Block
				{
					offset = allocation.block.offset + i * num,
					count = num
				};
				int num2;
				if (m_freeSlots.IsEmpty)
				{
					num2 = m_usedBlocks.Length;
					m_usedBlocks.Add(in value);
				}
				else
				{
					num2 = m_freeSlots[m_freeSlots.Length - 1];
					m_freeSlots.RemoveAtSwapBack(m_freeSlots.Length - 1);
					m_usedBlocks[num2] = value;
				}
				array[i] = new global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation
				{
					handle = num2,
					block = value
				};
			}
			return array;
		}
	}
}
