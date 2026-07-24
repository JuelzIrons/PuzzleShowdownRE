namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct UnsafeStream : global::Unity.Collections.INativeDisposable, global::System.IDisposable
	{
		[global::Unity.Burst.BurstCompile]
		private struct DisposeJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeStream Container;

			public void Execute()
			{
				Container.Deallocate();
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct ConstructJobList : global::Unity.Jobs.IJob
		{
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeStream Container;

			[global::Unity.Collections.ReadOnly]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList* List;

			public unsafe void Execute()
			{
				Container.AllocateForEach(List->m_length);
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct ConstructJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeStream Container;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> Length;

			public void Execute()
			{
				Container.AllocateForEach(Length[0]);
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility]
		public struct Writer
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal global::Unity.Collections.AllocatorManager.Block m_BlockData;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			private unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* m_CurrentBlock;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			private unsafe byte* m_CurrentPtr;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			private unsafe byte* m_CurrentBlockEnd;

			internal int m_ForeachIndex;

			private int m_ElementCount;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			private unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* m_FirstBlock;

			private int m_FirstOffset;

			private int m_NumberOfBlocks;

			[global::Unity.Collections.LowLevel.Unsafe.NativeSetThreadIndex]
			private int m_ThreadIndex;

			public unsafe int ForEachCount => ((global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer)->RangeCount;

			internal unsafe Writer(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeStream stream)
			{
				m_BlockData = stream.m_BlockData;
				m_ForeachIndex = int.MinValue;
				m_ElementCount = -1;
				m_CurrentBlock = null;
				m_CurrentBlockEnd = null;
				m_CurrentPtr = null;
				m_FirstBlock = null;
				m_NumberOfBlocks = 0;
				m_FirstOffset = 0;
				m_ThreadIndex = 0;
			}

			public unsafe void BeginForEachIndex(int foreachIndex)
			{
				m_ForeachIndex = foreachIndex;
				m_ElementCount = 0;
				m_NumberOfBlocks = 0;
				m_FirstBlock = m_CurrentBlock;
				m_FirstOffset = (int)(m_CurrentPtr - (byte*)m_CurrentBlock);
			}

			public unsafe void EndForEachIndex()
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange* ptr2 = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange*)(void*)ptr->Ranges.Range.Pointer;
				ptr2[m_ForeachIndex].ElementCount = m_ElementCount;
				ptr2[m_ForeachIndex].OffsetInFirstBlock = m_FirstOffset;
				ptr2[m_ForeachIndex].Block = m_FirstBlock;
				ptr2[m_ForeachIndex].LastOffset = (int)(m_CurrentPtr - (byte*)m_CurrentBlock);
				ptr2[m_ForeachIndex].NumberOfBlocks = m_NumberOfBlocks;
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public void Write<T>(T value) where T : unmanaged
			{
				Allocate<T>() = value;
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe ref T Allocate<T>() where T : unmanaged
			{
				int size = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(Allocate(size));
			}

			public unsafe byte* Allocate(int size)
			{
				byte* currentPtr = m_CurrentPtr;
				m_CurrentPtr += size;
				if (m_CurrentPtr > m_CurrentBlockEnd)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* currentBlock = m_CurrentBlock;
					global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer;
					m_CurrentBlock = ptr->Allocate(currentBlock, m_ThreadIndex);
					m_CurrentPtr = m_CurrentBlock->Data;
					if (m_FirstBlock == null)
					{
						m_FirstOffset = (int)(m_CurrentPtr - (byte*)m_CurrentBlock);
						m_FirstBlock = m_CurrentBlock;
					}
					else
					{
						m_NumberOfBlocks++;
					}
					m_CurrentBlockEnd = (byte*)m_CurrentBlock + 4096;
					currentPtr = m_CurrentPtr;
					m_CurrentPtr += size;
				}
				m_ElementCount++;
				return currentPtr;
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility]
		public struct Reader
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal global::Unity.Collections.AllocatorManager.Block m_BlockData;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* m_CurrentBlock;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe byte* m_CurrentPtr;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe byte* m_CurrentBlockEnd;

			internal int m_RemainingItemCount;

			internal int m_LastBlockSize;

			public unsafe int ForEachCount => ((global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer)->RangeCount;

			public int RemainingItemCount => m_RemainingItemCount;

			internal unsafe Reader(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeStream stream)
			{
				m_BlockData = stream.m_BlockData;
				m_CurrentBlock = null;
				m_CurrentPtr = null;
				m_CurrentBlockEnd = null;
				m_RemainingItemCount = 0;
				m_LastBlockSize = 0;
			}

			public unsafe int BeginForEachIndex(int foreachIndex)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange* ptr2 = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange*)(void*)ptr->Ranges.Range.Pointer;
				m_RemainingItemCount = ptr2[foreachIndex].ElementCount;
				m_LastBlockSize = ptr2[foreachIndex].LastOffset;
				m_CurrentBlock = ptr2[foreachIndex].Block;
				m_CurrentPtr = (byte*)m_CurrentBlock + ptr2[foreachIndex].OffsetInFirstBlock;
				m_CurrentBlockEnd = (byte*)m_CurrentBlock + 4096;
				return m_RemainingItemCount;
			}

			public void EndForEachIndex()
			{
			}

			public unsafe byte* ReadUnsafePtr(int size)
			{
				m_RemainingItemCount--;
				byte* currentPtr = m_CurrentPtr;
				m_CurrentPtr += size;
				if (m_CurrentPtr > m_CurrentBlockEnd)
				{
					m_CurrentBlock = m_CurrentBlock->Next;
					m_CurrentPtr = m_CurrentBlock->Data;
					m_CurrentBlockEnd = (byte*)m_CurrentBlock + 4096;
					currentPtr = m_CurrentPtr;
					m_CurrentPtr += size;
				}
				return currentPtr;
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe ref T Read<T>() where T : unmanaged
			{
				int size = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(ReadUnsafePtr(size));
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe ref T Peek<T>() where T : unmanaged
			{
				int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				byte* ptr = m_CurrentPtr;
				if (ptr + num > m_CurrentBlockEnd)
				{
					ptr = m_CurrentBlock->Next->Data;
				}
				return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(ptr);
			}

			public unsafe int Count()
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange* ptr2 = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange*)(void*)ptr->Ranges.Range.Pointer;
				int num = 0;
				for (int i = 0; i != ptr->RangeCount; i++)
				{
					num += ptr2[i].ElementCount;
				}
				return num;
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal global::Unity.Collections.AllocatorManager.Block m_BlockData;

		public readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_BlockData.Range.Pointer != global::System.IntPtr.Zero;
			}
		}

		public unsafe readonly int ForEachCount => ((global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer)->RangeCount;

		public UnsafeStream(int bufferCount, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			AllocateBlock(out this, allocator);
			AllocateForEach(bufferCount);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static global::Unity.Jobs.JobHandle ScheduleConstruct<T>(out global::Unity.Collections.LowLevel.Unsafe.UnsafeStream stream, global::Unity.Collections.NativeList<T> bufferCount, global::Unity.Jobs.JobHandle dependency, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			AllocateBlock(out stream, allocator);
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.ConstructJobList
			{
				List = (global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList*)bufferCount.GetUnsafeList(),
				Container = stream
			}, dependency);
		}

		public static global::Unity.Jobs.JobHandle ScheduleConstruct(out global::Unity.Collections.LowLevel.Unsafe.UnsafeStream stream, global::Unity.Collections.NativeArray<int> bufferCount, global::Unity.Jobs.JobHandle dependency, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			AllocateBlock(out stream, allocator);
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.ConstructJob
			{
				Length = bufferCount,
				Container = stream
			}, dependency);
		}

		internal unsafe static void AllocateBlock(out global::Unity.Collections.LowLevel.Unsafe.UnsafeStream stream, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int threadIndexCount = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ThreadIndexCount;
			int sizeOf = sizeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData) + sizeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock*) * threadIndexCount;
			global::Unity.Collections.AllocatorManager.Block blockData = global::Unity.Collections.AllocatorManager.AllocateBlock(ref allocator, sizeOf, 16, 1);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear((void*)blockData.Range.Pointer, blockData.AllocatedBytes);
			stream.m_BlockData = blockData;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)blockData.Range.Pointer;
			ptr->Allocator = allocator;
			ptr->BlockCount = threadIndexCount;
			ptr->Blocks = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock**)(void*)(blockData.Range.Pointer + sizeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData));
			ptr->Ranges = default(global::Unity.Collections.AllocatorManager.Block);
			ptr->RangeCount = 0;
		}

		internal unsafe void AllocateForEach(int forEachCount)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer;
			ptr->Ranges = global::Unity.Collections.AllocatorManager.AllocateBlock(ref m_BlockData.Range.Allocator, sizeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange), 16, forEachCount);
			ptr->RangeCount = forEachCount;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear((void*)ptr->Ranges.Range.Pointer, ptr->Ranges.AllocatedBytes);
		}

		public unsafe readonly bool IsEmpty()
		{
			if (!IsCreated)
			{
				return true;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange* ptr2 = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange*)(void*)ptr->Ranges.Range.Pointer;
			for (int i = 0; i != ptr->RangeCount; i++)
			{
				if (ptr2[i].ElementCount > 0)
				{
					return false;
				}
			}
			return true;
		}

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.Reader AsReader()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.Reader(ref this);
		}

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.Writer AsWriter()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.Writer(ref this);
		}

		public unsafe int Count()
		{
			int num = 0;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange* ptr2 = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange*)(void*)ptr->Ranges.Range.Pointer;
			for (int i = 0; i != ptr->RangeCount; i++)
			{
				num += ptr2[i].ElementCount;
			}
			return num;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public global::Unity.Collections.NativeArray<T> ToNativeArray<T>(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			global::Unity.Collections.NativeArray<T> result = global::Unity.Collections.CollectionHelper.CreateNativeArray<T>(Count(), allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.Reader reader = AsReader();
			int num = 0;
			for (int i = 0; i != reader.ForEachCount; i++)
			{
				reader.BeginForEachIndex(i);
				int remainingItemCount = reader.RemainingItemCount;
				for (int j = 0; j < remainingItemCount; j++)
				{
					result[num] = reader.Read<T>();
					num++;
				}
				reader.EndForEachIndex();
			}
			return result;
		}

		private unsafe void Deallocate()
		{
			if (!IsCreated)
			{
				return;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*)(void*)m_BlockData.Range.Pointer;
			for (int i = 0; i != ptr->BlockCount; i++)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* ptr2 = ptr->Blocks[i];
				while (ptr2 != null)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* next = ptr2->Next;
					ptr->Free(ptr2);
					ptr2 = next;
				}
			}
			ptr->Ranges.Dispose();
			m_BlockData.Dispose();
			m_BlockData = default(global::Unity.Collections.AllocatorManager.Block);
		}

		public void Dispose()
		{
			if (IsCreated)
			{
				Deallocate();
			}
		}

		public global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.DisposeJob
			{
				Container = this
			}, inputDeps);
			m_BlockData = default(global::Unity.Collections.AllocatorManager.Block);
			return result;
		}
	}
}
