namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::System.Diagnostics.DebuggerDisplay("Count = {m_HashMapData.Count()}, Capacity = {m_HashMapData.Capacity}, IsCreated = {m_HashMapData.IsCreated}, IsEmpty = {IsEmpty}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.NativeParallelHashMapDebuggerTypeProxy<, >))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeParallelHashMap<TKey, TValue> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>, global::System.Collections.IEnumerable where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.NativeParallelHashMapDebuggerTypeProxy<, >))]
		[global::System.Diagnostics.DebuggerDisplay("Count = {m_HashMapData.Count()}, Capacity = {m_HashMapData.Capacity}, IsCreated = {m_HashMapData.IsCreated}, IsEmpty = {IsEmpty}")]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ReadOnly : global::System.Collections.Generic.IEnumerable<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>, global::System.Collections.IEnumerable
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue> m_HashMapData;

			public readonly bool IsCreated
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_HashMapData.IsCreated;
				}
			}

			public readonly bool IsEmpty
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					if (!IsCreated)
					{
						return true;
					}
					return m_HashMapData.IsEmpty;
				}
			}

			public readonly int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_HashMapData.Capacity;
				}
			}

			public readonly TValue this[TKey key]
			{
				get
				{
					if (m_HashMapData.TryGetValue(key, out var item))
					{
						return item;
					}
					return default(TValue);
				}
			}

			internal ReadOnly(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue> hashMapData)
			{
				m_HashMapData = hashMapData;
			}

			public readonly int Count()
			{
				return m_HashMapData.Count();
			}

			public readonly bool TryGetValue(TKey key, out TValue item)
			{
				return m_HashMapData.TryGetValue(key, out item);
			}

			public readonly bool ContainsKey(TKey key)
			{
				return m_HashMapData.ContainsKey(key);
			}

			public readonly global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_HashMapData.GetKeyArray(allocator);
			}

			public readonly global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_HashMapData.GetValueArray(allocator);
			}

			public readonly global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_HashMapData.GetKeyValueArrays(allocator);
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private readonly void CheckRead()
			{
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private readonly void ThrowKeyNotPresent(TKey key)
			{
				throw new global::System.ArgumentException($"Key: {key} is not present in the NativeParallelHashMap.");
			}

			public unsafe readonly global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.Enumerator
				{
					m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator(m_HashMapData.m_Buffer)
				};
			}

			global::System.Collections.Generic.IEnumerator<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>> global::System.Collections.Generic.IEnumerable<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>.GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsAtomicWriteOnly]
		[global::System.Diagnostics.DebuggerDisplay("Capacity = {m_Writer.Capacity}")]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.ParallelWriter m_Writer;

			public int ThreadIndex => m_Writer.m_ThreadIndex;

			[global::System.Obsolete("'m_ThreadIndex' has been deprecated; use 'ThreadIndex' instead. (UnityUpgradable) -> ThreadIndex")]
			public int m_ThreadIndex => m_Writer.m_ThreadIndex;

			public readonly int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Writer.Capacity;
				}
			}

			public bool TryAdd(TKey key, TValue item)
			{
				return m_Writer.TryAdd(key, item);
			}

			public bool TryAdd(TKey key, TValue item, int threadIndexOverride)
			{
				return m_Writer.TryAdd(key, item, threadIndexOverride);
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator m_Enumerator;

			public global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue> Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Enumerator.GetCurrent<TKey, TValue>();
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

		internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue> m_HashMapData;

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!IsCreated)
				{
					return true;
				}
				return m_HashMapData.IsEmpty;
			}
		}

		public int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return m_HashMapData.Capacity;
			}
			set
			{
				m_HashMapData.Capacity = value;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				if (m_HashMapData.TryGetValue(key, out var item))
				{
					return item;
				}
				return default(TValue);
			}
			set
			{
				m_HashMapData[key] = value;
			}
		}

		public readonly bool IsCreated => m_HashMapData.IsCreated;

		public NativeParallelHashMap(int capacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_HashMapData = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>(capacity, allocator);
		}

		public int Count()
		{
			return m_HashMapData.Count();
		}

		public void Clear()
		{
			m_HashMapData.Clear();
		}

		public unsafe bool TryAdd(TKey key, TValue item)
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(m_HashMapData.m_Buffer, key, item, isMultiHashMap: false, m_HashMapData.m_AllocatorLabel);
		}

		public unsafe void Add(TKey key, TValue item)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(m_HashMapData.m_Buffer, key, item, isMultiHashMap: false, m_HashMapData.m_AllocatorLabel);
		}

		public bool Remove(TKey key)
		{
			return m_HashMapData.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue item)
		{
			return m_HashMapData.TryGetValue(key, out item);
		}

		public bool ContainsKey(TKey key)
		{
			return m_HashMapData.ContainsKey(key);
		}

		public void Dispose()
		{
			if (IsCreated)
			{
				m_HashMapData.Dispose();
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataDisposeJob
			{
				Data = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataDispose
				{
					m_Buffer = m_HashMapData.m_Buffer,
					m_AllocatorLabel = m_HashMapData.m_AllocatorLabel
				}
			}, inputDeps);
			m_HashMapData.m_Buffer = null;
			return result;
		}

		public global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_HashMapData.GetKeyArray(allocator);
		}

		public global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_HashMapData.GetValueArray(allocator);
		}

		public global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_HashMapData.GetKeyValueArrays(allocator);
		}

		public global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.ParallelWriter result = default(global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.ParallelWriter);
			result.m_Writer = m_HashMapData.AsParallelWriter();
			return result;
		}

		public global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.ReadOnly(m_HashMapData);
		}

		public unsafe global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator(m_HashMapData.m_Buffer)
			};
		}

		global::System.Collections.Generic.IEnumerator<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>> global::System.Collections.Generic.IEnumerable<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>.GetEnumerator()
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

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyNotPresent(TKey key)
		{
			throw new global::System.ArgumentException($"Key: {key} is not present in the NativeParallelHashMap.");
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyAlreadyAdded(TKey key)
		{
			throw new global::System.ArgumentException("An item with the same key has already been added", "key");
		}
	}
}
