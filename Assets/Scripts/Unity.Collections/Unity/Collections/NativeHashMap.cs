namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.NativeHashMapDebuggerTypeProxy<, >))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeHashMap<TKey, TValue> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<global::Unity.Collections.KVPair<TKey, TValue>>, global::System.Collections.IEnumerable where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<global::Unity.Collections.KVPair<TKey, TValue>>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>.Enumerator m_Enumerator;

			public global::Unity.Collections.KVPair<TKey, TValue> Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Enumerator.GetCurrent<TValue>();
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
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ReadOnly : global::System.Collections.Generic.IEnumerable<global::Unity.Collections.KVPair<TKey, TValue>>, global::System.Collections.IEnumerable
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* m_Data;

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

			public unsafe readonly TValue this[TKey key]
			{
				get
				{
					m_Data->TryGetValue<TValue>(key, out var item);
					return item;
				}
			}

			internal unsafe ReadOnly(ref global::Unity.Collections.NativeHashMap<TKey, TValue> data)
			{
				m_Data = data.m_Data;
			}

			public unsafe readonly bool TryGetValue(TKey key, out TValue item)
			{
				return m_Data->TryGetValue<TValue>(key, out item);
			}

			public unsafe readonly bool ContainsKey(TKey key)
			{
				return -1 != m_Data->Find(key);
			}

			public unsafe readonly global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_Data->GetKeyArray(allocator);
			}

			public unsafe readonly global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_Data->GetValueArray<TValue>(allocator);
			}

			public unsafe readonly global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_Data->GetKeyValueArrays<TValue>(allocator);
			}

			public unsafe readonly global::Unity.Collections.NativeHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new global::Unity.Collections.NativeHashMap<TKey, TValue>.Enumerator
				{
					m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>.Enumerator(m_Data)
				};
			}

			global::System.Collections.Generic.IEnumerator<global::Unity.Collections.KVPair<TKey, TValue>> global::System.Collections.Generic.IEnumerable<global::Unity.Collections.KVPair<TKey, TValue>>.GetEnumerator()
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

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private readonly void ThrowKeyNotPresent(TKey key)
			{
				throw new global::System.ArgumentException($"Key: {key} is not present.");
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* m_Data;

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

		public unsafe TValue this[TKey key]
		{
			get
			{
				m_Data->TryGetValue<TValue>(key, out var item);
				return item;
			}
			set
			{
				int num = m_Data->Find(key);
				if (-1 == num)
				{
					TryAdd(key, value);
				}
				else
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(m_Data->Ptr, num, value);
				}
			}
		}

		public unsafe NativeHashMap(int initialCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_Data = global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>.Alloc(initialCapacity, sizeof(TValue), 256, allocator);
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>.Free(m_Data);
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

		public unsafe bool TryAdd(TKey key, TValue item)
		{
			int num = m_Data->TryAdd(in key);
			if (-1 != num)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(m_Data->Ptr, num, item);
				return true;
			}
			return false;
		}

		public void Add(TKey key, TValue item)
		{
			TryAdd(key, item);
		}

		public unsafe bool Remove(TKey key)
		{
			return -1 != m_Data->TryRemove(key);
		}

		public unsafe bool TryGetValue(TKey key, out TValue item)
		{
			return m_Data->TryGetValue<TValue>(key, out item);
		}

		public unsafe bool ContainsKey(TKey key)
		{
			return -1 != m_Data->Find(key);
		}

		public unsafe void TrimExcess()
		{
			m_Data->TrimExcess();
		}

		public unsafe global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Data->GetKeyArray(allocator);
		}

		public unsafe global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Data->GetValueArray<TValue>(allocator);
		}

		public unsafe global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Data->GetKeyValueArrays<TValue>(allocator);
		}

		public unsafe global::Unity.Collections.NativeHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new global::Unity.Collections.NativeHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>.Enumerator(m_Data)
			};
		}

		global::System.Collections.Generic.IEnumerator<global::Unity.Collections.KVPair<TKey, TValue>> global::System.Collections.Generic.IEnumerable<global::Unity.Collections.KVPair<TKey, TValue>>.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		public global::Unity.Collections.NativeHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.NativeHashMap<TKey, TValue>.ReadOnly(ref this);
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

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyNotPresent(TKey key)
		{
			throw new global::System.ArgumentException($"Key: {key} is not present.");
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyAlreadyAdded(TKey key)
		{
			throw new global::System.ArgumentException($"An item with the same key has already been added: {key}");
		}
	}
}
