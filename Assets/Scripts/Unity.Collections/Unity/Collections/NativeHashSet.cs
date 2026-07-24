namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.NativeHashSetDebuggerTypeProxy<>))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct NativeHashSet<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable where T : unmanaged, global::System.IEquatable<T>
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>.Enumerator m_Enumerator;

			public T Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Enumerator.GetCurrentKey();
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
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ReadOnly : global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>* m_Data;

			public unsafe readonly bool IsCreated
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					if (m_Data != null)
					{
						return m_Data->IsCreated;
					}
					return false;
				}
			}

			public unsafe readonly bool IsEmpty
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					if (!IsCreated)
					{
						return true;
					}
					return m_Data->IsEmpty;
				}
			}

			public unsafe readonly int Count
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Data->Count;
				}
			}

			public unsafe readonly int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Data->Capacity;
				}
			}

			internal unsafe ReadOnly(ref global::Unity.Collections.NativeHashSet<T> data)
			{
				m_Data = data.m_Data;
			}

			public unsafe readonly bool Contains(T item)
			{
				return -1 != m_Data->Find(item);
			}

			public unsafe readonly global::Unity.Collections.NativeArray<T> ToNativeArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_Data->GetKeyArray(allocator);
			}

			public unsafe readonly global::Unity.Collections.NativeHashSet<T>.Enumerator GetEnumerator()
			{
				return new global::Unity.Collections.NativeHashSet<T>.Enumerator
				{
					m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>.Enumerator(m_Data)
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

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>* m_Data;

		public unsafe readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!IsCreated)
				{
					return true;
				}
				return m_Data->IsEmpty;
			}
		}

		public unsafe readonly int Count
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Data->Count;
			}
		}

		public unsafe int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return m_Data->Capacity;
			}
			set
			{
				m_Data->Resize(value);
			}
		}

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (m_Data != null)
				{
					return m_Data->IsCreated;
				}
				return false;
			}
		}

		public unsafe NativeHashSet(int initialCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_Data = global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>.Alloc(initialCapacity, 0, 256, allocator);
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>.Free(m_Data);
				m_Data = null;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeHashMapDisposeJob
			{
				Data = new global::Unity.Collections.NativeHashMapDispose
				{
					m_HashMapData = (global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<int, int>*)m_Data
				}
			}, inputDeps);
			m_Data = null;
			return result;
		}

		public unsafe void Clear()
		{
			m_Data->Clear();
		}

		public unsafe bool Add(T item)
		{
			return -1 != m_Data->TryAdd(in item);
		}

		public unsafe bool Remove(T item)
		{
			return -1 != m_Data->TryRemove(item);
		}

		public unsafe bool Contains(T item)
		{
			return -1 != m_Data->Find(item);
		}

		public unsafe void TrimExcess()
		{
			m_Data->TrimExcess();
		}

		public unsafe global::Unity.Collections.NativeArray<T> ToNativeArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Data->GetKeyArray(allocator);
		}

		public unsafe global::Unity.Collections.NativeHashSet<T>.Enumerator GetEnumerator()
		{
			return new global::Unity.Collections.NativeHashSet<T>.Enumerator
			{
				m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>.Enumerator(m_Data)
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

		public global::Unity.Collections.NativeHashSet<T>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.NativeHashSet<T>.ReadOnly(ref this);
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
