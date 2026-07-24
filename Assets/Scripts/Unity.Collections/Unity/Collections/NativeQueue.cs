namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct NativeQueue<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable where T : unmanaged
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			internal global::Unity.Collections.UnsafeQueue<T>.Enumerator m_Enumerator;

			public T Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Enumerator.Current;
				}
			}

			object global::System.Collections.IEnumerator.Current => Current;

			public void Dispose()
			{
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return m_Enumerator.MoveNext();
			}

			public void Reset()
			{
				m_Enumerator.Reset();
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		public struct ReadOnly : global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
		{
			private global::Unity.Collections.UnsafeQueue<T>.ReadOnly m_ReadOnly;

			public readonly bool IsCreated
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_ReadOnly.IsCreated;
				}
			}

			public readonly int Count => m_ReadOnly.Count;

			public readonly T this[int index] => m_ReadOnly[index];

			internal unsafe ReadOnly(ref global::Unity.Collections.NativeQueue<T> data)
			{
				m_ReadOnly = new global::Unity.Collections.UnsafeQueue<T>.ReadOnly(ref *data.m_Queue);
			}

			public readonly bool IsEmpty()
			{
				return m_ReadOnly.IsEmpty();
			}

			public readonly global::Unity.Collections.NativeQueue<T>.Enumerator GetEnumerator()
			{
				return new global::Unity.Collections.NativeQueue<T>.Enumerator
				{
					m_Enumerator = m_ReadOnly.GetEnumerator()
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
			private readonly void CheckRead()
			{
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsAtomicWriteOnly]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			internal global::Unity.Collections.UnsafeQueue<T>.ParallelWriter unsafeWriter;

			public void Enqueue(T value)
			{
				unsafeWriter.Enqueue(value);
			}

			public void Enqueue(T value, int threadIndexOverride)
			{
				unsafeWriter.Enqueue(value, threadIndexOverride);
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private unsafe global::Unity.Collections.UnsafeQueue<T>* m_Queue;

		public unsafe readonly int Count => m_Queue->Count;

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (m_Queue != null)
				{
					return m_Queue->IsCreated;
				}
				return false;
			}
		}

		public unsafe NativeQueue(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_Queue = global::Unity.Collections.UnsafeQueue<T>.Alloc(allocator);
			*m_Queue = new global::Unity.Collections.UnsafeQueue<T>(allocator);
		}

		public unsafe readonly bool IsEmpty()
		{
			if (IsCreated)
			{
				return m_Queue->IsEmpty();
			}
			return true;
		}

		public unsafe T Peek()
		{
			return m_Queue->Peek();
		}

		public unsafe void Enqueue(T value)
		{
			m_Queue->Enqueue(value);
		}

		public unsafe T Dequeue()
		{
			return m_Queue->Dequeue();
		}

		public unsafe bool TryDequeue(out T item)
		{
			return m_Queue->TryDequeue(out item);
		}

		public unsafe global::Unity.Collections.NativeArray<T> ToArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Queue->ToArray(allocator);
		}

		public unsafe void Clear()
		{
			m_Queue->Clear();
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.UnsafeQueue<T>.Free(m_Queue);
				m_Queue = null;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeQueueDisposeJob
			{
				Data = new global::Unity.Collections.NativeQueueDispose
				{
					m_QueueData = (global::Unity.Collections.UnsafeQueue<int>*)m_Queue
				}
			}, inputDeps);
			m_Queue = null;
			return result;
		}

		public global::Unity.Collections.NativeQueue<T>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.NativeQueue<T>.ReadOnly(ref this);
		}

		public unsafe global::Unity.Collections.NativeQueue<T>.ParallelWriter AsParallelWriter()
		{
			global::Unity.Collections.NativeQueue<T>.ParallelWriter result = default(global::Unity.Collections.NativeQueue<T>.ParallelWriter);
			result.unsafeWriter = m_Queue->AsParallelWriter();
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckRead()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}
	}
}
