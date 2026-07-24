namespace UnityEngine.Rendering.Universal
{
	internal struct BuddyAllocator : global::System.IDisposable
	{
		private struct Header
		{
			public int branchingOrder;

			public int levelCount;

			public int allocationCount;

			public int freeAllocationIdsCount;
		}

		private unsafe void* m_Data;

		private (int, int) m_ActiveFreeMaskCounts;

		private (int, int) m_FreeMasksStorage;

		private (int, int) m_FreeMaskIndicesStorage;

		private global::Unity.Collections.Allocator m_Allocator;

		private unsafe ref global::UnityEngine.Rendering.Universal.BuddyAllocator.Header header => ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<global::UnityEngine.Rendering.Universal.BuddyAllocator.Header>(m_Data);

		private global::Unity.Collections.NativeArray<int> freeMaskCounts => GetNativeArray<int>(m_ActiveFreeMaskCounts.Item1, m_ActiveFreeMaskCounts.Item2);

		private global::Unity.Collections.NativeArray<ulong> freeMasksStorage => GetNativeArray<ulong>(m_FreeMasksStorage.Item1, m_FreeMasksStorage.Item2);

		private global::Unity.Collections.NativeArray<int> freeMaskIndicesStorage => GetNativeArray<int>(m_FreeMaskIndicesStorage.Item1, m_FreeMaskIndicesStorage.Item2);

		public int levelCount => header.levelCount;

		private global::Unity.Collections.NativeArray<ulong> FreeMasks(int level)
		{
			return freeMasksStorage.GetSubArray(LevelOffset64(level, header.branchingOrder), LevelLength64(level, header.branchingOrder));
		}

		private global::Unity.Collections.NativeArray<int> FreeMaskIndices(int level)
		{
			return freeMaskIndicesStorage.GetSubArray(LevelOffset64(level, header.branchingOrder), LevelLength64(level, header.branchingOrder));
		}

		public unsafe BuddyAllocator(int levelCount, int branchingOrder, global::Unity.Collections.Allocator allocator = global::Unity.Collections.Allocator.Persistent)
		{
			int dataSize = sizeof(global::UnityEngine.Rendering.Universal.BuddyAllocator.Header);
			m_ActiveFreeMaskCounts = AllocateRange<int>(levelCount, ref dataSize);
			m_FreeMasksStorage = AllocateRange<ulong>(LevelOffset64(levelCount, branchingOrder), ref dataSize);
			m_FreeMaskIndicesStorage = AllocateRange<int>(LevelOffset64(levelCount, branchingOrder), ref dataSize);
			m_Data = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(dataSize, 64, allocator);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(m_Data, dataSize);
			m_Allocator = allocator;
			header = new global::UnityEngine.Rendering.Universal.BuddyAllocator.Header
			{
				branchingOrder = branchingOrder,
				levelCount = levelCount
			};
			global::Unity.Collections.NativeArray<ulong> nativeArray = FreeMasks(0);
			nativeArray[0] = 15uL;
			global::Unity.Collections.NativeArray<int> nativeArray2 = freeMaskCounts;
			nativeArray2[0] = 1;
		}

		public bool TryAllocate(int requestedLevel, out global::UnityEngine.Rendering.Universal.BuddyAllocation allocation)
		{
			allocation = default(global::UnityEngine.Rendering.Universal.BuddyAllocation);
			int num = requestedLevel;
			global::Unity.Collections.NativeArray<int> nativeArray = freeMaskCounts;
			while (num >= 0 && nativeArray[num] <= 0)
			{
				num--;
			}
			if (num < 0)
			{
				return false;
			}
			global::Unity.Collections.NativeArray<int> nativeArray2 = FreeMaskIndices(num);
			int num2 = nativeArray2[--nativeArray[num]];
			global::Unity.Collections.NativeArray<ulong> nativeArray3 = FreeMasks(num);
			ulong num3 = nativeArray3[num2];
			int num4 = global::Unity.Mathematics.math.tzcnt(num3);
			num3 = (nativeArray3[num2] = num3 ^ (ulong)(1L << num4));
			if (num3 != 0L)
			{
				nativeArray2[nativeArray[num]++] = num2;
			}
			int num6 = num2 * 64 + num4;
			while (num < requestedLevel)
			{
				num++;
				num6 <<= header.branchingOrder;
				int num7 = num6 >> 6;
				int num8 = num6 & 0x3F;
				global::Unity.Collections.NativeArray<ulong> nativeArray4 = FreeMasks(num);
				ulong num9 = nativeArray4[num7];
				if (num9 == 0L)
				{
					global::Unity.Collections.NativeArray<int> nativeArray5 = FreeMaskIndices(num);
					nativeArray5[nativeArray[num]++] = num7;
				}
				num9 |= (ulong)((1L << Pow2(header.branchingOrder)) - 2 << num8);
				nativeArray4[num7] = num9;
			}
			allocation.level = num;
			allocation.index = num6;
			return true;
		}

		public void Free(global::UnityEngine.Rendering.Universal.BuddyAllocation allocation)
		{
			int num = allocation.level;
			int num2 = allocation.index;
			while (num >= 0)
			{
				int num3 = num2 >> 6;
				int num4 = num2 & 0x3F;
				global::Unity.Collections.NativeArray<ulong> nativeArray = FreeMasks(num);
				ulong num5 = nativeArray[num3];
				bool flag = num5 == 0;
				num5 |= (ulong)(1L << num4);
				global::Unity.Collections.NativeArray<int> nativeArray2 = FreeMaskIndices(num);
				global::Unity.Collections.NativeArray<int> nativeArray3 = freeMaskCounts;
				ulong num6 = (ulong)((1L << Pow2(header.branchingOrder)) - 1 << (num4 >> header.branchingOrder) * Pow2(header.branchingOrder));
				if (num == 0 || (~num5 & num6) != 0L)
				{
					nativeArray[num3] = num5;
					if (flag)
					{
						nativeArray2[nativeArray3[num]++] = num3;
					}
					break;
				}
				num5 = (nativeArray[num3] = num5 & ~num6);
				if (!flag && num5 == 0L)
				{
					for (int i = 0; i < nativeArray2.Length; i++)
					{
						if (nativeArray2[i] == num3)
						{
							nativeArray2[i] = nativeArray2[--nativeArray3[num]];
							break;
						}
					}
				}
				num--;
				num2 >>= header.branchingOrder;
			}
		}

		public unsafe void Dispose()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(m_Data, m_Allocator);
			m_Data = default(void*);
			m_Allocator = global::Unity.Collections.Allocator.Invalid;
		}

		private unsafe global::Unity.Collections.NativeArray<T> GetNativeArray<T>(int offset, int length) where T : struct
		{
			return global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(PtrAdd(m_Data, offset), length, m_Allocator);
		}

		private static int LevelOffset(int level, int branchingOrder)
		{
			return Pow2(branchingOrder) * (Pow2(branchingOrder * (level - 1) + branchingOrder) - 1) / (Pow2(branchingOrder) - 1);
		}

		private static int LevelLength(int level, int branchingOrder)
		{
			return Pow2N(branchingOrder, level + 1);
		}

		private static int LevelOffset64(int level, int branchingOrder)
		{
			return global::Unity.Mathematics.math.min(level, 6 / branchingOrder) + LevelOffset(global::Unity.Mathematics.math.max(0, level - 6 / branchingOrder), branchingOrder);
		}

		private static int LevelLength64(int level, int branchingOrder)
		{
			return Pow2N(branchingOrder, global::Unity.Mathematics.math.max(0, level - 6 / branchingOrder + 1));
		}

		private static (int, int) AllocateRange<T>(int length, ref int dataSize) where T : struct
		{
			dataSize = AlignForward(dataSize, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>());
			(int, int) result = (dataSize, length);
			dataSize += length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			return result;
		}

		private static int AlignForward(int offset, int alignment)
		{
			int num = offset % alignment;
			if (num != 0)
			{
				offset += alignment - num;
			}
			return offset;
		}

		private unsafe static void* PtrAdd(void* ptr, int bytes)
		{
			return (void*)((global::System.IntPtr)ptr + bytes);
		}

		private static int Pow2(int n)
		{
			return 1 << n;
		}

		private static int Pow2N(int x, int n)
		{
			return 1 << x * n;
		}
	}
}
