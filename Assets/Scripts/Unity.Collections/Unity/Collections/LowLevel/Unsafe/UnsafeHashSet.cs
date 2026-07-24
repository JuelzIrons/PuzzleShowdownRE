namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeHashSetDebuggerTypeProxy<>))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct UnsafeHashSet<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable where T : unmanaged, global::System.IEquatable<T>
	{
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			internal global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>.Enumerator m_Enumerator;

			public unsafe T Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Enumerator.m_Data->Keys[m_Enumerator.m_Index];
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
			internal global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T> m_Data;

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
					return m_Data.IsEmpty;
				}
			}

			public readonly int Count
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Data.Count;
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

			internal ReadOnly(ref global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T> data)
			{
				m_Data = data;
			}

			public readonly bool Contains(T item)
			{
				return -1 != m_Data.Find(item);
			}

			public readonly global::Unity.Collections.NativeArray<T> ToNativeArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_Data.GetKeyArray(allocator);
			}

			public unsafe readonly global::Unity.Collections.LowLevel.Unsafe.UnsafeHashSet<T>.Enumerator GetEnumerator()
			{
				fixed (global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>* data = &m_Data)
				{
					return new global::Unity.Collections.LowLevel.Unsafe.UnsafeHashSet<T>.Enumerator
					{
						m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>.Enumerator(data)
					};
				}
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

		internal global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T> m_Data;

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (IsCreated)
				{
					return m_Data.IsEmpty;
				}
				return true;
			}
		}

		public readonly int Count
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Data.Count;
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
				m_Data.Resize(value);
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

		public UnsafeHashSet(int initialCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_Data = default(global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>);
			m_Data.Init(initialCapacity, 0, 256, allocator);
		}

		public void Dispose()
		{
			if (IsCreated)
			{
				m_Data.Dispose();
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.LowLevel.Unsafe.UnsafeDisposeJob
			{
				Ptr = m_Data.Ptr,
				Allocator = m_Data.Allocator
			}, inputDeps);
			m_Data.Ptr = null;
			return result;
		}

		public void Clear()
		{
			m_Data.Clear();
		}

		public bool Add(T item)
		{
			return -1 != m_Data.TryAdd(in item);
		}

		public bool Remove(T item)
		{
			return -1 != m_Data.TryRemove(item);
		}

		public bool Contains(T item)
		{
			return -1 != m_Data.Find(item);
		}

		public void TrimExcess()
		{
			m_Data.TrimExcess();
		}

		public global::Unity.Collections.NativeArray<T> ToNativeArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Data.GetKeyArray(allocator);
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeHashSet<T>.Enumerator GetEnumerator()
		{
			fixed (global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>* data = &m_Data)
			{
				return new global::Unity.Collections.LowLevel.Unsafe.UnsafeHashSet<T>.Enumerator
				{
					m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>.Enumerator(data)
				};
			}
		}

		global::System.Collections.Generic.IEnumerator<T> global::System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeHashSet<T>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeHashSet<T>.ReadOnly(ref m_Data);
		}
	}
}
