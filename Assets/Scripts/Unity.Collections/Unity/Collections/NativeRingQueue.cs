namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.NativeRingQueueDebugView<>))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct NativeRingQueue<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable where T : unmanaged
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<T>* m_RingQueue;

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (m_RingQueue != null)
				{
					return m_RingQueue->IsCreated;
				}
				return false;
			}
		}

		public unsafe readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (m_RingQueue != null)
				{
					return m_RingQueue->Length == 0;
				}
				return true;
			}
		}

		public unsafe readonly int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return global::Unity.Collections.CollectionHelper.AssumePositive(m_RingQueue->Length);
			}
		}

		public unsafe readonly int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return global::Unity.Collections.CollectionHelper.AssumePositive(m_RingQueue->Capacity);
			}
		}

		public unsafe NativeRingQueue(int capacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory)
		{
			m_RingQueue = global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<T>.Alloc(allocator);
			*m_RingQueue = new global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<T>(capacity, allocator, options);
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<T>.Free(m_RingQueue);
				m_RingQueue = null;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeRingQueueDisposeJob
			{
				Data = new global::Unity.Collections.NativeRingQueueDispose
				{
					m_QueueData = (global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<int>*)m_RingQueue
				}
			}, inputDeps);
			m_RingQueue = null;
			return result;
		}

		public unsafe bool TryEnqueue(T value)
		{
			return m_RingQueue->TryEnqueue(value);
		}

		public unsafe void Enqueue(T value)
		{
			m_RingQueue->Enqueue(value);
		}

		public unsafe bool TryDequeue(out T item)
		{
			return m_RingQueue->TryDequeue(out item);
		}

		public unsafe T Dequeue()
		{
			return m_RingQueue->Dequeue();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckRead()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckWrite()
		{
		}
	}
}
