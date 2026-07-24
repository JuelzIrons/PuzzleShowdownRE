namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Diagnostics.DebuggerDisplay("Count = {Count()}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDebuggerTypeProxy<, >))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct UnsafeParallelHashMap<TKey, TValue> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>>, global::System.Collections.IEnumerable where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
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
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private readonly void ThrowKeyNotPresent(TKey key)
			{
				throw new global::System.ArgumentException($"Key: {key} is not present in the NativeParallelHashMap.");
			}

			public unsafe readonly global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.Enumerator
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

			public int ThreadIndex => m_ThreadIndex;

			public unsafe readonly int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Buffer->keyCapacity;
				}
			}

			public unsafe bool TryAdd(TKey key, TValue item)
			{
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryAddAtomic(m_Buffer, key, item, m_ThreadIndex);
			}

			public unsafe bool TryAdd(TKey key, TValue item, int threadIndexOverride)
			{
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryAddAtomic(m_Buffer, key, item, threadIndexOverride);
			}
		}

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

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Buffer != null;
			}
		}

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

		public unsafe TValue this[TKey key]
		{
			get
			{
				TryGetValue(key, out var item);
				return item;
			}
			set
			{
				if (global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(m_Buffer, key, out var _, out var it))
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.SetValue(m_Buffer, ref it, ref value);
				}
				else
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(m_Buffer, key, value, isMultiHashMap: false, m_AllocatorLabel);
				}
			}
		}

		public unsafe UnsafeParallelHashMap(int capacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_AllocatorLabel = allocator;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out m_Buffer);
			Clear();
		}

		public unsafe readonly int Count()
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetCount(m_Buffer);
		}

		public unsafe void Clear()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.Clear(m_Buffer);
		}

		public unsafe bool TryAdd(TKey key, TValue item)
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(m_Buffer, key, item, isMultiHashMap: false, m_AllocatorLabel);
		}

		public unsafe void Add(TKey key, TValue item)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(m_Buffer, key, item, isMultiHashMap: false, m_AllocatorLabel);
		}

		public unsafe bool Remove(TKey key)
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.Remove(m_Buffer, key, isMultiHashMap: false) != 0;
		}

		public unsafe bool TryGetValue(TKey key, out TValue item)
		{
			global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it;
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(m_Buffer, key, out item, out it);
		}

		public unsafe bool ContainsKey(TKey key)
		{
			TValue item;
			global::Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it;
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(m_Buffer, key, out item, out it);
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

		public unsafe global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeArray<TKey> result = global::Unity.Collections.CollectionHelper.CreateNativeArray<TKey>(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetCount(m_Buffer), allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetKeyArray(m_Buffer, result);
			return result;
		}

		public unsafe global::Unity.Collections.NativeArray<TValue> GetValueArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeArray<TValue> result = global::Unity.Collections.CollectionHelper.CreateNativeArray<TValue>(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetCount(m_Buffer), allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetValueArray(m_Buffer, result);
			return result;
		}

		public unsafe global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> result = new global::Unity.Collections.NativeKeyValueArrays<TKey, TValue>(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetCount(m_Buffer), allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.GetKeyValueArrays(m_Buffer, result);
			return result;
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.ParallelWriter result = default(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.ParallelWriter);
			result.m_ThreadIndex = 0;
			result.m_Buffer = m_Buffer;
			return result;
		}

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.ReadOnly(this);
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue>.Enumerator
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
	}
}
