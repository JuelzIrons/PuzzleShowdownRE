namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMapDebuggerTypeProxy<, >))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct UnsafeHashMap<TKey, TValue> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<global::Unity.Collections.KVPair<TKey, TValue>>, global::System.Collections.IEnumerable where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<global::Unity.Collections.KVPair<TKey, TValue>>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
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

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ReadOnly : global::System.Collections.Generic.IEnumerable<global::Unity.Collections.KVPair<TKey, TValue>>, global::System.Collections.IEnumerable
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey> m_Data;

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

			public readonly TValue this[TKey key]
			{
				get
				{
					m_Data.TryGetValue<TValue>(key, out var item);
					return item;
				}
			}

			internal ReadOnly(ref global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey> data)
			{
				m_Data = data;
			}

			public readonly bool TryGetValue(TKey key, out TValue item)
			{
				return m_Data.TryGetValue<TValue>(key, out item);
			}

			public readonly bool ContainsKey(TKey key)
			{
				return -1 != m_Data.Find(key);
			}

			public readonly global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_Data.GetKeyArray(allocator);
			}

			public readonly global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_Data.GetValueArray<TValue>(allocator);
			}

			public readonly global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_Data.GetKeyValueArrays<TValue>(allocator);
			}

			public unsafe readonly global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				fixed (global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* data = &m_Data)
				{
					return new global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<TKey, TValue>.Enumerator
					{
						m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>.Enumerator(data)
					};
				}
			}

			global::System.Collections.Generic.IEnumerator<global::Unity.Collections.KVPair<TKey, TValue>> global::System.Collections.Generic.IEnumerable<global::Unity.Collections.KVPair<TKey, TValue>>.GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey> m_Data;

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

		public unsafe TValue this[TKey key]
		{
			get
			{
				m_Data.TryGetValue<TValue>(key, out var item);
				return item;
			}
			set
			{
				int num = m_Data.Find(key);
				if (-1 != num)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(m_Data.Ptr, num, value);
				}
				else
				{
					TryAdd(key, value);
				}
			}
		}

		public unsafe UnsafeHashMap(int initialCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_Data = default(global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>);
			m_Data.Init(initialCapacity, sizeof(TValue), 256, allocator);
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
			m_Data = default(global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>);
			return result;
		}

		public void Clear()
		{
			m_Data.Clear();
		}

		public unsafe bool TryAdd(TKey key, TValue item)
		{
			int num = m_Data.TryAdd(in key);
			if (-1 != num)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(m_Data.Ptr, num, item);
				return true;
			}
			return false;
		}

		public void Add(TKey key, TValue item)
		{
			TryAdd(key, item);
		}

		public bool Remove(TKey key)
		{
			return -1 != m_Data.TryRemove(key);
		}

		public bool TryGetValue(TKey key, out TValue item)
		{
			return m_Data.TryGetValue<TValue>(key, out item);
		}

		public bool ContainsKey(TKey key)
		{
			return -1 != m_Data.Find(key);
		}

		public void TrimExcess()
		{
			m_Data.TrimExcess();
		}

		public global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Data.GetKeyArray(allocator);
		}

		public global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Data.GetValueArray<TValue>(allocator);
		}

		public global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_Data.GetKeyValueArrays<TValue>(allocator);
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			fixed (global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* data = &m_Data)
			{
				return new global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<TKey, TValue>.Enumerator
				{
					m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>.Enumerator(data)
				};
			}
		}

		global::System.Collections.Generic.IEnumerator<global::Unity.Collections.KVPair<TKey, TValue>> global::System.Collections.Generic.IEnumerable<global::Unity.Collections.KVPair<TKey, TValue>>.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<TKey, TValue>.ReadOnly(ref m_Data);
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
