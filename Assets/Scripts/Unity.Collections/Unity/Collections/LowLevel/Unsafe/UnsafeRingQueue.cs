namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueueDebugView<>))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct UnsafeRingQueue<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable where T : unmanaged
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe T* Ptr;

		public global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

		internal readonly int m_Capacity;

		internal int m_Filled;

		internal int m_Write;

		internal int m_Read;

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Filled == 0;
			}
		}

		public readonly int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Filled;
			}
		}

		public readonly int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Capacity;
			}
		}

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Ptr != null;
			}
		}

		public unsafe UnsafeRingQueue(T* ptr, int capacity)
		{
			Ptr = ptr;
			Allocator = global::Unity.Collections.AllocatorManager.None;
			m_Capacity = capacity;
			m_Filled = 0;
			m_Write = 0;
			m_Read = 0;
		}

		public unsafe UnsafeRingQueue(int capacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory)
		{
			Allocator = allocator;
			m_Capacity = capacity;
			m_Filled = 0;
			m_Write = 0;
			m_Read = 0;
			int num = capacity * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			Ptr = (T*)global::Unity.Collections.Memory.Unmanaged.Allocate(num, 16, allocator);
			if (options == global::Unity.Collections.NativeArrayOptions.ClearMemory)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(Ptr, num);
			}
		}

		internal unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<T>* Alloc(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return (global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<T>*)global::Unity.Collections.Memory.Unmanaged.Allocate(sizeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<T>), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<T>>(), allocator);
		}

		internal unsafe static void Free(global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<T>* data)
		{
			if (data == null)
			{
				throw new global::System.InvalidOperationException("UnsafeRingQueue has yet to be created or has been destroyed!");
			}
			global::Unity.Collections.AllocatorManager.AllocatorHandle allocator = data->Allocator;
			data->Dispose();
			global::Unity.Collections.Memory.Unmanaged.Free(data, allocator);
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				if (global::Unity.Collections.CollectionHelper.ShouldDeallocate(Allocator))
				{
					global::Unity.Collections.Memory.Unmanaged.Free(Ptr, Allocator);
					Allocator = global::Unity.Collections.AllocatorManager.Invalid;
				}
				Ptr = null;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			if (global::Unity.Collections.CollectionHelper.ShouldDeallocate(Allocator))
			{
				global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.LowLevel.Unsafe.UnsafeDisposeJob
				{
					Ptr = Ptr,
					Allocator = Allocator
				}, inputDeps);
				Ptr = null;
				Allocator = global::Unity.Collections.AllocatorManager.Invalid;
				return result;
			}
			Ptr = null;
			return inputDeps;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private unsafe bool TryEnqueueInternal(T value)
		{
			if (m_Filled == m_Capacity)
			{
				return false;
			}
			Ptr[m_Write] = value;
			m_Write++;
			if (m_Write == m_Capacity)
			{
				m_Write = 0;
			}
			m_Filled++;
			return true;
		}

		public bool TryEnqueue(T value)
		{
			return TryEnqueueInternal(value);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void ThrowQueueFull()
		{
			throw new global::System.InvalidOperationException("Trying to enqueue into full queue.");
		}

		public void Enqueue(T value)
		{
			TryEnqueueInternal(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private unsafe bool TryDequeueInternal(out T item)
		{
			item = Ptr[m_Read];
			if (m_Filled == 0)
			{
				return false;
			}
			m_Read++;
			if (m_Read == m_Capacity)
			{
				m_Read = 0;
			}
			m_Filled--;
			return true;
		}

		public bool TryDequeue(out T item)
		{
			return TryDequeueInternal(out item);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void ThrowQueueEmpty()
		{
			throw new global::System.InvalidOperationException("Trying to dequeue from an empty queue");
		}

		public T Dequeue()
		{
			TryDequeueInternal(out var item);
			return item;
		}
	}
}
