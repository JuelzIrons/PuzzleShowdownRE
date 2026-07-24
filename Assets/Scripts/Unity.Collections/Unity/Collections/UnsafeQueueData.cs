namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct UnsafeQueueData
	{
		internal const int m_BlockSize = 16384;

		public global::System.IntPtr m_FirstBlock;

		public global::System.IntPtr m_LastBlock;

		public int m_MaxItems;

		public int m_CurrentRead;

		public unsafe byte* m_CurrentWriteBlockTLS;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe global::Unity.Collections.UnsafeQueueBlockHeader* GetCurrentWriteBlockTLS(int threadIndex)
		{
			global::Unity.Collections.UnsafeQueueBlockHeader** ptr = (global::Unity.Collections.UnsafeQueueBlockHeader**)(m_CurrentWriteBlockTLS + threadIndex * 64);
			return *ptr;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void SetCurrentWriteBlockTLS(int threadIndex, global::Unity.Collections.UnsafeQueueBlockHeader* currentWriteBlock)
		{
			global::Unity.Collections.UnsafeQueueBlockHeader** ptr = (global::Unity.Collections.UnsafeQueueBlockHeader**)(m_CurrentWriteBlockTLS + threadIndex * 64);
			*ptr = currentWriteBlock;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static global::Unity.Collections.UnsafeQueueBlockHeader* AllocateWriteBlockMT<T>(global::Unity.Collections.UnsafeQueueData* data, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, int threadIndex) where T : unmanaged
		{
			global::Unity.Collections.UnsafeQueueBlockHeader* currentWriteBlockTLS = data->GetCurrentWriteBlockTLS(threadIndex);
			if (currentWriteBlockTLS != null)
			{
				if (currentWriteBlockTLS->m_NumItems != data->m_MaxItems)
				{
					return currentWriteBlockTLS;
				}
				currentWriteBlockTLS = null;
			}
			currentWriteBlockTLS = (global::Unity.Collections.UnsafeQueueBlockHeader*)global::Unity.Collections.Memory.Unmanaged.Allocate(16384L, 16, allocator);
			currentWriteBlockTLS->m_NextBlock = null;
			currentWriteBlockTLS->m_NumItems = 0;
			global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)global::System.Threading.Interlocked.Exchange(ref data->m_LastBlock, (global::System.IntPtr)currentWriteBlockTLS);
			if (ptr == null)
			{
				data->m_FirstBlock = (global::System.IntPtr)currentWriteBlockTLS;
			}
			else
			{
				ptr->m_NextBlock = currentWriteBlockTLS;
			}
			data->SetCurrentWriteBlockTLS(threadIndex, currentWriteBlockTLS);
			return currentWriteBlockTLS;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void AllocateQueue<T>(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, out global::Unity.Collections.UnsafeQueueData* outBuf) where T : unmanaged
		{
			int threadIndexCount = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ThreadIndexCount;
			int num = global::Unity.Collections.CollectionHelper.Align(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Collections.UnsafeQueueData>(), 64);
			global::Unity.Collections.UnsafeQueueData* ptr = (global::Unity.Collections.UnsafeQueueData*)global::Unity.Collections.Memory.Unmanaged.Allocate(num + 64 * threadIndexCount, 64, allocator);
			ptr->m_CurrentWriteBlockTLS = (byte*)ptr + num;
			ptr->m_FirstBlock = global::System.IntPtr.Zero;
			ptr->m_LastBlock = global::System.IntPtr.Zero;
			ptr->m_MaxItems = (16384 - global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Collections.UnsafeQueueBlockHeader>()) / global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			ptr->m_CurrentRead = 0;
			for (int i = 0; i < threadIndexCount; i++)
			{
				ptr->SetCurrentWriteBlockTLS(i, null);
			}
			outBuf = ptr;
		}

		public unsafe static void DeallocateQueue(global::Unity.Collections.UnsafeQueueData* data, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)data->m_FirstBlock;
			while (ptr != null)
			{
				global::Unity.Collections.UnsafeQueueBlockHeader* nextBlock = ptr->m_NextBlock;
				global::Unity.Collections.Memory.Unmanaged.Free(ptr, allocator);
				ptr = nextBlock;
			}
			global::Unity.Collections.Memory.Unmanaged.Free(data, allocator);
		}
	}
}
