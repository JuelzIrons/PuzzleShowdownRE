namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.NativeParallelMultiHashMapDebuggerTypeProxy<, >))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeParallelMultiHashMap<TKey, TValue> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>, global::System.Collections.IEnumerable where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsAtomicWriteOnly]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter m_Writer;

			public int m_ThreadIndex => m_Writer.m_ThreadIndex;

			public readonly int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Writer.Capacity;
				}
			}

			public void Add(TKey key, TValue item)
			{
				m_Writer.Add(key, item);
			}

			public void Add(TKey key, TValue item, int threadIndexOverride)
			{
				m_Writer.Add(key, item, threadIndexOverride);
			}
		}

		public struct Enumerator : global::System.Collections.Generic.IEnumerator<TValue>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			internal global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue> hashmap;

			internal TKey key;

			internal byte isFirst;

			private TValue value;

			private global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> iterator;

			public TValue Current
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
			public bool MoveNext()
			{
				if (isFirst == 1)
				{
					isFirst = 0;
					return hashmap.TryGetFirstValue(key, out value, out iterator);
				}
				return hashmap.TryGetNextValue(out value, ref iterator);
			}

			public void Reset()
			{
				isFirst = 1;
			}

			public global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		public struct KeyValueEnumerator : global::System.Collections.Generic.IEnumerator<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator m_Enumerator;

			public readonly global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue> Current
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
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue> m_MultiHashMapData;

			public readonly bool IsCreated
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_MultiHashMapData.IsCreated;
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
					return m_MultiHashMapData.IsEmpty;
				}
			}

			public readonly int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_MultiHashMapData.Capacity;
				}
			}

			internal ReadOnly(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue> container)
			{
				m_MultiHashMapData = container;
			}

			public readonly int Count()
			{
				return m_MultiHashMapData.Count();
			}

			public readonly bool TryGetFirstValue(TKey key, out TValue item, out global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
			{
				return m_MultiHashMapData.TryGetFirstValue(key, out item, out it);
			}

			public readonly bool TryGetNextValue(out TValue item, ref global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
			{
				return m_MultiHashMapData.TryGetNextValue(out item, ref it);
			}

			public readonly bool ContainsKey(TKey key)
			{
				return m_MultiHashMapData.ContainsKey(key);
			}

			public readonly global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_MultiHashMapData.GetKeyArray(allocator);
			}

			public readonly global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_MultiHashMapData.GetValueArray(allocator);
			}

			public readonly global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return m_MultiHashMapData.GetKeyValueArrays(allocator);
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

			public unsafe global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
			{
				return new global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
				{
					m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator(m_MultiHashMapData.m_Buffer)
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

		internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue> m_MultiHashMapData;

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_MultiHashMapData.IsEmpty;
			}
		}

		public int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return m_MultiHashMapData.Capacity;
			}
			set
			{
				m_MultiHashMapData.Capacity = value;
			}
		}

		public readonly bool IsCreated => m_MultiHashMapData.IsCreated;

		public NativeParallelMultiHashMap(int capacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			this = default(global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>);
			Initialize(capacity, ref allocator);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle) })]
		internal void Initialize<U>(int capacity, ref U allocator) where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			m_MultiHashMapData = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>(capacity, allocator.Handle);
		}

		public readonly int Count()
		{
			return m_MultiHashMapData.Count();
		}

		public void Clear()
		{
			m_MultiHashMapData.Clear();
		}

		public void Add(TKey key, TValue item)
		{
			m_MultiHashMapData.Add(key, item);
		}

		public int Remove(TKey key)
		{
			return m_MultiHashMapData.Remove(key);
		}

		public void Remove(global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
		{
			m_MultiHashMapData.Remove(it);
		}

		public bool TryGetFirstValue(TKey key, out TValue item, out global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
		{
			return m_MultiHashMapData.TryGetFirstValue(key, out item, out it);
		}

		public bool TryGetNextValue(out TValue item, ref global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
		{
			return m_MultiHashMapData.TryGetNextValue(out item, ref it);
		}

		public bool ContainsKey(TKey key)
		{
			TValue item;
			global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it;
			return TryGetFirstValue(key, out item, out it);
		}

		public int CountValuesForKey(TKey key)
		{
			if (!TryGetFirstValue(key, out var item, out var it))
			{
				return 0;
			}
			int num = 1;
			while (TryGetNextValue(out item, ref it))
			{
				num++;
			}
			return num;
		}

		public bool SetValue(TValue item, global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
		{
			return m_MultiHashMapData.SetValue(item, it);
		}

		public void Dispose()
		{
			if (IsCreated)
			{
				m_MultiHashMapData.Dispose();
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
					m_Buffer = m_MultiHashMapData.m_Buffer,
					m_AllocatorLabel = m_MultiHashMapData.m_AllocatorLabel
				}
			}, inputDeps);
			m_MultiHashMapData.m_Buffer = null;
			return result;
		}

		public global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_MultiHashMapData.GetKeyArray(allocator);
		}

		public global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_MultiHashMapData.GetValueArray(allocator);
		}

		public global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return m_MultiHashMapData.GetKeyValueArrays(allocator);
		}

		public global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.ParallelWriter result = default(global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.ParallelWriter);
			result.m_Writer = m_MultiHashMapData.AsParallelWriter();
			return result;
		}

		public global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = 1
			};
		}

		public unsafe global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator(m_MultiHashMapData.m_Buffer)
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

		public global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.ReadOnly(m_MultiHashMapData);
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
