namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSetDebuggerTypeProxy<>))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct UnsafeParallelHashSet<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable where T : unmanaged, global::System.IEquatable<T>
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsAtomicWriteOnly]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<T, bool>.ParallelWriter m_Data;

			public readonly int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Data.Capacity;
				}
			}

			public bool Add(T item)
			{
				return m_Data.TryAdd(item, item: false);
			}

			public bool Add(T item, int threadIndexOverride)
			{
				return m_Data.TryAdd(item, item: false, threadIndexOverride);
			}
		}

		public struct Enumerator : global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator m_Enumerator;

			public T Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Enumerator.GetCurrentKey<T>();
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

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ReadOnly : global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<T, bool> m_Data;

			public readonly bool IsCreated
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Data.IsCreated;
				}
			}

			public readonly bool IsEmpty
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					if (m_Data.IsCreated)
					{
						return m_Data.IsEmpty;
					}
					return true;
				}
			}

			public readonly int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Data.Capacity;
				}
			}

			internal ReadOnly(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T> data)
			{
				m_Data = data.m_Data;
			}

			public readonly int Count()
			{
				return m_Data.Count();
			}

			public readonly bool Contains(T item)
			{
				return m_Data.ContainsKey(item);
			}

			public readonly global::Unity.Collections.NativeArray<T> ToNativeArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_Data.GetKeyArray(allocator);
			}

			public unsafe readonly global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T>.Enumerator GetEnumerator()
			{
				return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T>.Enumerator
				{
					m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator(m_Data.m_Buffer)
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
		}

		internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<T, bool> m_Data;

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Data.IsEmpty;
			}
		}

		public int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return m_Data.Capacity;
			}
			set
			{
				m_Data.Capacity = value;
			}
		}

		public readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Data.IsCreated;
			}
		}

		public UnsafeParallelHashSet(int capacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_Data = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<T, bool>(capacity, allocator);
		}

		public int Count()
		{
			return m_Data.Count();
		}

		public void Dispose()
		{
			m_Data.Dispose();
		}

		public global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			return m_Data.Dispose(inputDeps);
		}

		public void Clear()
		{
			m_Data.Clear();
		}

		public bool Add(T item)
		{
			return m_Data.TryAdd(item, item: false);
		}

		public bool Remove(T item)
		{
			return m_Data.Remove(item);
		}

		public bool Contains(T item)
		{
			return m_Data.ContainsKey(item);
		}

		public global::Unity.Collections.NativeArray<T> ToNativeArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Data.GetKeyArray(allocator);
		}

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T>.ParallelWriter AsParallelWriter()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T>.ParallelWriter
			{
				m_Data = m_Data.AsParallelWriter()
			};
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T>.Enumerator GetEnumerator()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T>.Enumerator
			{
				m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator(m_Data.m_Buffer)
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

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T>.ReadOnly(ref this);
		}
	}
}
