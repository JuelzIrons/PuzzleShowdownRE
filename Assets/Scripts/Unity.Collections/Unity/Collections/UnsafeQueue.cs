namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct UnsafeQueue<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable where T : unmanaged
	{
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.UnsafeQueueBlockHeader* m_FirstBlock;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.UnsafeQueueBlockHeader* m_Block;

			internal int m_Index;

			private T value;

			public T Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return value;
				}
			}

			object global::System.Collections.IEnumerator.Current => Current;

			public void Dispose()
			{
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public unsafe bool MoveNext()
			{
				m_Index++;
				while (m_Block != null)
				{
					int numItems = m_Block->m_NumItems;
					if (m_Index < numItems)
					{
						value = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(m_Block + 1, m_Index);
						return true;
					}
					m_Index -= numItems;
					m_Block = m_Block->m_NextBlock;
				}
				value = default(T);
				return false;
			}

			public unsafe void Reset()
			{
				m_Block = m_FirstBlock;
				m_Index = -1;
			}
		}

		public struct ReadOnly : global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			private unsafe global::Unity.Collections.UnsafeQueueData* m_Buffer;

			public unsafe readonly bool IsCreated
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Buffer != null;
				}
			}

			public unsafe readonly int Count
			{
				get
				{
					int num = 0;
					for (global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock; ptr != null; ptr = ptr->m_NextBlock)
					{
						num += ptr->m_NumItems;
					}
					return num - m_Buffer->m_CurrentRead;
				}
			}

			public readonly T this[int index]
			{
				get
				{
					TryGetValue(index, out var item);
					return item;
				}
			}

			internal unsafe ReadOnly(ref global::Unity.Collections.UnsafeQueue<T> data)
			{
				m_Buffer = data.m_Buffer;
			}

			public unsafe readonly bool IsEmpty()
			{
				int num = 0;
				int currentRead = m_Buffer->m_CurrentRead;
				for (global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock; ptr != null; ptr = ptr->m_NextBlock)
				{
					num += ptr->m_NumItems;
					if (num > currentRead)
					{
						return false;
					}
				}
				return num == currentRead;
			}

			private unsafe readonly bool TryGetValue(int index, out T item)
			{
				if (index >= 0)
				{
					int num = index;
					for (global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock; ptr != null; ptr = ptr->m_NextBlock)
					{
						int numItems = ptr->m_NumItems;
						if (num < numItems)
						{
							item = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(ptr + 1, num);
							return true;
						}
						num -= numItems;
					}
				}
				item = default(T);
				return false;
			}

			public unsafe readonly global::Unity.Collections.UnsafeQueue<T>.Enumerator GetEnumerator()
			{
				return new global::Unity.Collections.UnsafeQueue<T>.Enumerator
				{
					m_FirstBlock = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock,
					m_Block = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock,
					m_Index = -1
				};
			}

			global::System.Collections.Generic.IEnumerator<T> global::System.Collections.Generic.IEnumerable<T>.GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private readonly void ThrowIndexOutOfRangeException(int index)
			{
				throw new global::System.IndexOutOfRangeException($"Index {index} is out of bounds [0-{Count}].");
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.UnsafeQueueData* m_Buffer;

			internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;

			[global::Unity.Collections.LowLevel.Unsafe.NativeSetThreadIndex]
			internal int m_ThreadIndex;

			public unsafe void Enqueue(T value)
			{
				global::Unity.Collections.UnsafeQueueBlockHeader* ptr = global::Unity.Collections.UnsafeQueueData.AllocateWriteBlockMT<T>(m_Buffer, m_AllocatorLabel, m_ThreadIndex);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(ptr + 1, ptr->m_NumItems, value);
				ptr->m_NumItems++;
			}

			public unsafe void Enqueue(T value, int threadIndexOverride)
			{
				global::Unity.Collections.UnsafeQueueBlockHeader* ptr = global::Unity.Collections.UnsafeQueueData.AllocateWriteBlockMT<T>(m_Buffer, m_AllocatorLabel, threadIndexOverride);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(ptr + 1, ptr->m_NumItems, value);
				ptr->m_NumItems++;
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.UnsafeQueueData* m_Buffer;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;

		public unsafe readonly int Count
		{
			get
			{
				int num = 0;
				for (global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock; ptr != null; ptr = ptr->m_NextBlock)
				{
					num += ptr->m_NumItems;
				}
				return num - m_Buffer->m_CurrentRead;
			}
		}

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Buffer != null;
			}
		}

		public unsafe UnsafeQueue(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_AllocatorLabel = allocator;
			global::Unity.Collections.UnsafeQueueData.AllocateQueue<T>(allocator, out m_Buffer);
		}

		internal unsafe static global::Unity.Collections.UnsafeQueue<T>* Alloc(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return (global::Unity.Collections.UnsafeQueue<T>*)global::Unity.Collections.Memory.Unmanaged.Allocate(sizeof(global::Unity.Collections.UnsafeQueue<T>), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<global::Unity.Collections.UnsafeQueue<T>>(), allocator);
		}

		internal unsafe static void Free(global::Unity.Collections.UnsafeQueue<T>* data)
		{
			if (data == null)
			{
				throw new global::System.InvalidOperationException("UnsafeQueue has yet to be created or has been destroyed!");
			}
			global::Unity.Collections.AllocatorManager.AllocatorHandle allocatorLabel = data->m_AllocatorLabel;
			data->Dispose();
			global::Unity.Collections.Memory.Unmanaged.Free(data, allocatorLabel);
		}

		public unsafe readonly bool IsEmpty()
		{
			if (IsCreated)
			{
				int num = 0;
				int currentRead = m_Buffer->m_CurrentRead;
				for (global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock; ptr != null; ptr = ptr->m_NextBlock)
				{
					num += ptr->m_NumItems;
					if (num > currentRead)
					{
						return false;
					}
				}
				return num == currentRead;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe T Peek()
		{
			global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock;
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(ptr + 1, m_Buffer->m_CurrentRead);
		}

		public unsafe void Enqueue(T value)
		{
			global::Unity.Collections.UnsafeQueueBlockHeader* ptr = global::Unity.Collections.UnsafeQueueData.AllocateWriteBlockMT<T>(m_Buffer, m_AllocatorLabel, 0);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(ptr + 1, ptr->m_NumItems, value);
			ptr->m_NumItems++;
		}

		public T Dequeue()
		{
			TryDequeue(out var item);
			return item;
		}

		public unsafe bool TryDequeue(out T item)
		{
			global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock;
			if (ptr != null)
			{
				int num = m_Buffer->m_CurrentRead++;
				int numItems = ptr->m_NumItems;
				item = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(ptr + 1, num);
				if (num + 1 >= numItems)
				{
					m_Buffer->m_CurrentRead = 0;
					m_Buffer->m_FirstBlock = (global::System.IntPtr)ptr->m_NextBlock;
					if (m_Buffer->m_FirstBlock == global::System.IntPtr.Zero)
					{
						m_Buffer->m_LastBlock = global::System.IntPtr.Zero;
					}
					int threadIndexCount = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ThreadIndexCount;
					for (int i = 0; i < threadIndexCount; i++)
					{
						if (m_Buffer->GetCurrentWriteBlockTLS(i) == ptr)
						{
							m_Buffer->SetCurrentWriteBlockTLS(i, null);
						}
					}
					global::Unity.Collections.Memory.Unmanaged.Free(ptr, m_AllocatorLabel);
				}
				return true;
			}
			item = default(T);
			return false;
		}

		public unsafe global::Unity.Collections.NativeArray<T> ToArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock;
			global::Unity.Collections.NativeArray<T> nativeArray = global::Unity.Collections.CollectionHelper.CreateNativeArray<T>(Count, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.UnsafeQueueBlockHeader* ptr2 = ptr;
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			int num2 = 0;
			int num3 = m_Buffer->m_CurrentRead * num;
			int num4 = m_Buffer->m_CurrentRead;
			while (ptr2 != null)
			{
				int num5 = (ptr2->m_NumItems - num4) * num;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(unsafePtr + num2, (byte*)(ptr2 + 1) + num3, num5);
				num3 = (num4 = 0);
				num2 += num5;
				ptr2 = ptr2->m_NextBlock;
			}
			return nativeArray;
		}

		public unsafe void Clear()
		{
			global::Unity.Collections.UnsafeQueueBlockHeader* ptr = (global::Unity.Collections.UnsafeQueueBlockHeader*)(void*)m_Buffer->m_FirstBlock;
			while (ptr != null)
			{
				global::Unity.Collections.UnsafeQueueBlockHeader* nextBlock = ptr->m_NextBlock;
				global::Unity.Collections.Memory.Unmanaged.Free(ptr, m_AllocatorLabel);
				ptr = nextBlock;
			}
			m_Buffer->m_FirstBlock = global::System.IntPtr.Zero;
			m_Buffer->m_LastBlock = global::System.IntPtr.Zero;
			m_Buffer->m_CurrentRead = 0;
			int threadIndexCount = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ThreadIndexCount;
			for (int i = 0; i < threadIndexCount; i++)
			{
				m_Buffer->SetCurrentWriteBlockTLS(i, null);
			}
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.UnsafeQueueData.DeallocateQueue(m_Buffer, m_AllocatorLabel);
				m_Buffer = null;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.UnsafeQueueDisposeJob
			{
				Data = new global::Unity.Collections.UnsafeQueueDispose
				{
					m_Buffer = m_Buffer,
					m_AllocatorLabel = m_AllocatorLabel
				}
			}, inputDeps);
			m_Buffer = null;
			return result;
		}

		public global::Unity.Collections.UnsafeQueue<T>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.UnsafeQueue<T>.ReadOnly(ref this);
		}

		public unsafe global::Unity.Collections.UnsafeQueue<T>.ParallelWriter AsParallelWriter()
		{
			global::Unity.Collections.UnsafeQueue<T>.ParallelWriter result = default(global::Unity.Collections.UnsafeQueue<T>.ParallelWriter);
			result.m_Buffer = m_Buffer;
			result.m_AllocatorLabel = m_AllocatorLabel;
			result.m_ThreadIndex = 0;
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private unsafe void CheckNotEmpty()
		{
			_ = m_Buffer->m_FirstBlock == (global::System.IntPtr)0;
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void ThrowEmpty()
		{
			throw new global::System.InvalidOperationException("Trying to read from an empty queue.");
		}
	}
}
