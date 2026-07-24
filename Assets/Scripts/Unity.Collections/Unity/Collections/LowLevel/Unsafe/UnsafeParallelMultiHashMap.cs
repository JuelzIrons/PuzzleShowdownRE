namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMapDebuggerTypeProxy<, >))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct UnsafeParallelMultiHashMap<TKey, TValue> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>, global::System.Collections.IEnumerable where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<TValue>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue> hashmap;

			internal TKey key;

			internal bool isFirst;

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
				if (isFirst)
				{
					isFirst = false;
					return hashmap.TryGetFirstValue(key, out value, out iterator);
				}
				return hashmap.TryGetNextValue(out value, ref iterator);
			}

			public void Reset()
			{
				isFirst = true;
			}

			public global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;

			[global::Unity.Collections.LowLevel.Unsafe.NativeSetThreadIndex]
			internal int m_ThreadIndex;

			public unsafe readonly int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Buffer->keyCapacity;
				}
			}

			public unsafe void Add(TKey key, TValue item)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.AddAtomicMulti(m_Buffer, key, item, m_ThreadIndex);
			}

			public unsafe void Add(TKey key, TValue item, int threadIndexOverride)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.AddAtomicMulti(m_Buffer, key, item, threadIndexOverride);
			}
		}

		public struct KeyValueEnumerator : global::System.Collections.Generic.IEnumerator<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>, global::System.Collections.IEnumerator, global::System.IDisposable
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

			public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
			{
				return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
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

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;

		public unsafe readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (IsCreated)
				{
					return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.IsEmpty(m_Buffer);
				}
				return true;
			}
		}

		public unsafe int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return m_Buffer->keyCapacity;
			}
			set
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.ReallocateHashMap<TKey, TValue>(m_Buffer, value, global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetBucketSize(value), m_AllocatorLabel);
			}
		}

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Buffer != null;
			}
		}

		public unsafe UnsafeParallelMultiHashMap(int capacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_AllocatorLabel = allocator;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out m_Buffer);
			Clear();
		}

		public unsafe readonly int Count()
		{
			if (m_Buffer->allocatedIndexLength <= 0)
			{
				return 0;
			}
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetCount(m_Buffer);
		}

		public unsafe void Clear()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.Clear(m_Buffer);
		}

		public unsafe void Add(TKey key, TValue item)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(m_Buffer, key, item, isMultiHashMap: true, m_AllocatorLabel);
		}

		public unsafe int Remove(TKey key)
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.Remove(m_Buffer, key, isMultiHashMap: true);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe void Remove<TValueEQ>(TKey key, TValueEQ value) where TValueEQ : unmanaged, global::System.IEquatable<TValueEQ>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValueEQ>.RemoveKeyValue(m_Buffer, key, value);
		}

		public unsafe void Remove(global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.Remove(m_Buffer, it);
		}

		public unsafe readonly bool TryGetFirstValue(TKey key, out TValue item, out global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(m_Buffer, key, out item, out it);
		}

		public unsafe readonly bool TryGetNextValue(out TValue item, ref global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryGetNextValueAtomic(m_Buffer, out item, ref it);
		}

		public readonly bool ContainsKey(TKey key)
		{
			TValue item;
			global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it;
			return TryGetFirstValue(key, out item, out it);
		}

		public readonly int CountValuesForKey(TKey key)
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

		public unsafe bool SetValue(TValue item, global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.SetValue(m_Buffer, ref it, ref item);
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.DeallocateHashMap(m_Buffer, m_AllocatorLabel);
				m_Buffer = null;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDisposeJob
			{
				Data = m_Buffer,
				Allocator = m_AllocatorLabel
			}, inputDeps);
			m_Buffer = null;
			return result;
		}

		public unsafe readonly global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeArray<TKey> result = global::Unity.Collections.CollectionHelper.CreateNativeArray<TKey>(Count(), allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetKeyArray(m_Buffer, result);
			return result;
		}

		public unsafe readonly global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeArray<TValue> result = global::Unity.Collections.CollectionHelper.CreateNativeArray<TValue>(Count(), allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetValueArray(m_Buffer, result);
			return result;
		}

		public unsafe readonly global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> result = new global::Unity.Collections.NativeKeyValueArrays<TKey, TValue>(Count(), allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetKeyValueArrays(m_Buffer, result);
			return result;
		}

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = true
			};
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter result = default(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter);
			result.m_ThreadIndex = 0;
			result.m_Buffer = m_Buffer;
			return result;
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator(m_Buffer)
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

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue>.ReadOnly(this);
		}
	}
}
