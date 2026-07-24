namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	internal struct HashMapHelper<TKey> where TKey : unmanaged, global::System.IEquatable<TKey>
	{
		internal struct Enumerator
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* m_Data;

			internal int m_Index;

			internal int m_BucketIndex;

			internal int m_NextIndex;

			internal unsafe Enumerator(global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* data)
			{
				m_Data = data;
				m_Index = -1;
				m_BucketIndex = 0;
				m_NextIndex = -1;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal unsafe bool MoveNext()
			{
				return m_Data->MoveNext(ref m_BucketIndex, ref m_NextIndex, out m_Index);
			}

			internal void Reset()
			{
				m_Index = -1;
				m_BucketIndex = 0;
				m_NextIndex = -1;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal unsafe global::Unity.Collections.KVPair<TKey, TValue> GetCurrent<TValue>() where TValue : unmanaged
			{
				return new global::Unity.Collections.KVPair<TKey, TValue>
				{
					m_Data = m_Data,
					m_Index = m_Index
				};
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal unsafe TKey GetCurrentKey()
			{
				if (m_Index != -1)
				{
					return m_Data->Keys[m_Index];
				}
				return default(TKey);
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe byte* Ptr;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe TKey* Keys;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe int* Next;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe int* Buckets;

		internal int Count;

		internal int Capacity;

		internal int Log2MinGrowth;

		internal int BucketCapacity;

		internal int AllocatedIndex;

		internal int FirstFreeIdx;

		internal int SizeOfTValue;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

		internal const int kMinimumCapacity = 256;

		internal unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Ptr != null;
			}
		}

		internal readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (IsCreated)
				{
					return Count == 0;
				}
				return true;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal int CalcCapacityCeilPow2(int capacity)
		{
			capacity = global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.max(1, Count), capacity);
			return global::Unity.Mathematics.math.ceilpow2(global::Unity.Mathematics.math.max(capacity, 1 << Log2MinGrowth));
		}

		internal static int GetBucketSize(int capacity)
		{
			return capacity * 2;
		}

		internal unsafe void Clear()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemSet(Buckets, byte.MaxValue, BucketCapacity * 4);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemSet(Next, byte.MaxValue, Capacity * 4);
			Count = 0;
			FirstFreeIdx = -1;
			AllocatedIndex = 0;
		}

		internal unsafe void Init(int capacity, int sizeOfValueT, int minGrowth, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			Count = 0;
			Log2MinGrowth = (byte)(32 - global::Unity.Mathematics.math.lzcnt(global::Unity.Mathematics.math.max(1, minGrowth) - 1));
			capacity = CalcCapacityCeilPow2(capacity);
			Capacity = capacity;
			BucketCapacity = GetBucketSize(capacity);
			Allocator = allocator;
			SizeOfTValue = sizeOfValueT;
			int outKeyOffset;
			int outNextOffset;
			int outBucketOffset;
			int num = CalculateDataSize(capacity, BucketCapacity, sizeOfValueT, out outKeyOffset, out outNextOffset, out outBucketOffset);
			Ptr = (byte*)global::Unity.Collections.Memory.Unmanaged.Allocate(num, 64, allocator);
			Keys = (TKey*)(Ptr + outKeyOffset);
			Next = (int*)(Ptr + outNextOffset);
			Buckets = (int*)(Ptr + outBucketOffset);
			Clear();
		}

		internal unsafe void Dispose()
		{
			global::Unity.Collections.Memory.Unmanaged.Free(Ptr, Allocator);
			Ptr = null;
			Keys = null;
			Next = null;
			Buckets = null;
			Count = 0;
			BucketCapacity = 0;
		}

		internal unsafe static global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* Alloc(int capacity, int sizeOfValueT, int minGrowth, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* ptr = (global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>*)global::Unity.Collections.Memory.Unmanaged.Allocate(sizeof(global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>>(), allocator);
			ptr->Init(capacity, sizeOfValueT, minGrowth, allocator);
			return ptr;
		}

		internal unsafe static void Free(global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* data)
		{
			if (data == null)
			{
				throw new global::System.InvalidOperationException("Hash based container has yet to be created or has been destroyed!");
			}
			data->Dispose();
			global::Unity.Collections.Memory.Unmanaged.Free(data, data->Allocator);
		}

		internal void Resize(int newCapacity)
		{
			newCapacity = global::Unity.Mathematics.math.max(newCapacity, Count);
			int num = global::Unity.Mathematics.math.ceilpow2(GetBucketSize(newCapacity));
			if (Capacity != newCapacity || BucketCapacity != num)
			{
				ResizeExact(newCapacity, num);
			}
		}

		internal unsafe void ResizeExact(int newCapacity, int newBucketCapacity)
		{
			int outKeyOffset;
			int outNextOffset;
			int outBucketOffset;
			int num = CalculateDataSize(newCapacity, newBucketCapacity, SizeOfTValue, out outKeyOffset, out outNextOffset, out outBucketOffset);
			byte* ptr = Ptr;
			TKey* keys = Keys;
			int* next = Next;
			int* buckets = Buckets;
			int bucketCapacity = BucketCapacity;
			Ptr = (byte*)global::Unity.Collections.Memory.Unmanaged.Allocate(num, 64, Allocator);
			Keys = (TKey*)(Ptr + outKeyOffset);
			Next = (int*)(Ptr + outNextOffset);
			Buckets = (int*)(Ptr + outBucketOffset);
			Capacity = newCapacity;
			BucketCapacity = newBucketCapacity;
			Clear();
			int i = 0;
			for (int num2 = bucketCapacity; i < num2; i++)
			{
				for (int num3 = buckets[i]; num3 != -1; num3 = next[num3])
				{
					int num4 = TryAdd(in keys[num3]);
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Ptr + SizeOfTValue * num4, ptr + SizeOfTValue * num3, SizeOfTValue);
				}
			}
			global::Unity.Collections.Memory.Unmanaged.Free(ptr, Allocator);
		}

		internal void TrimExcess()
		{
			int num = CalcCapacityCeilPow2(Count);
			ResizeExact(num, GetBucketSize(num));
		}

		internal unsafe static int CalculateDataSize(int capacity, int bucketCapacity, int sizeOfTValue, out int outKeyOffset, out int outNextOffset, out int outBucketOffset)
		{
			int num = 4;
			int num2 = sizeOfTValue * capacity;
			int num3 = sizeof(TKey) * capacity;
			int num4 = num * capacity;
			int num5 = num * bucketCapacity;
			int result = num2 + num3 + num4 + num5;
			outKeyOffset = num2;
			outNextOffset = outKeyOffset + num3;
			outBucketOffset = outNextOffset + num4;
			return result;
		}

		internal unsafe readonly int GetCount()
		{
			if (AllocatedIndex <= 0)
			{
				return 0;
			}
			int num = 0;
			for (int num2 = FirstFreeIdx; num2 >= 0; num2 = Next[num2])
			{
				num++;
			}
			return global::Unity.Mathematics.math.min(Capacity, AllocatedIndex) - num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private int GetBucket(in TKey key)
		{
			return (int)((uint)key.GetHashCode() & (BucketCapacity - 1));
		}

		internal unsafe int TryAdd(in TKey key)
		{
			if (-1 == Find(key))
			{
				if (AllocatedIndex >= Capacity && FirstFreeIdx < 0)
				{
					int newCapacity = CalcCapacityCeilPow2(Capacity + (1 << Log2MinGrowth));
					Resize(newCapacity);
				}
				int num = FirstFreeIdx;
				if (num >= 0)
				{
					FirstFreeIdx = Next[num];
				}
				else
				{
					num = AllocatedIndex++;
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(Keys, num, key);
				int bucket = GetBucket(in key);
				Next[num] = Buckets[bucket];
				Buckets[bucket] = num;
				Count++;
				return num;
			}
			return -1;
		}

		internal unsafe int Find(TKey key)
		{
			if (AllocatedIndex > 0)
			{
				int bucket = GetBucket(in key);
				int num = Buckets[bucket];
				if ((uint)num < (uint)Capacity)
				{
					int* next = Next;
					while (!global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(Keys, num).Equals(key))
					{
						num = next[num];
						if ((uint)num >= (uint)Capacity)
						{
							return -1;
						}
					}
					return num;
				}
			}
			return -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		internal unsafe bool TryGetValue<TValue>(TKey key, out TValue item) where TValue : unmanaged
		{
			int num = Find(key);
			if (-1 != num)
			{
				item = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TValue>(Ptr, num);
				return true;
			}
			item = default(TValue);
			return false;
		}

		internal unsafe int TryRemove(TKey key)
		{
			if (Capacity != 0)
			{
				int num = 0;
				int bucket = GetBucket(in key);
				int num2 = -1;
				int num3 = Buckets[bucket];
				while (num3 >= 0 && num3 < Capacity)
				{
					if (global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(Keys, num3).Equals(key))
					{
						num++;
						if (num2 < 0)
						{
							Buckets[bucket] = Next[num3];
						}
						else
						{
							Next[num2] = Next[num3];
						}
						int num4 = Next[num3];
						Next[num3] = FirstFreeIdx;
						FirstFreeIdx = num3;
						num3 = num4;
						break;
					}
					num2 = num3;
					num3 = Next[num3];
				}
				Count -= num;
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			return -1;
		}

		internal unsafe bool MoveNextSearch(ref int bucketIndex, ref int nextIndex, out int index)
		{
			int i = bucketIndex;
			for (int bucketCapacity = BucketCapacity; i < bucketCapacity; i++)
			{
				int num = Buckets[i];
				if (num != -1)
				{
					index = num;
					bucketIndex = i + 1;
					nextIndex = Next[num];
					return true;
				}
			}
			index = -1;
			bucketIndex = BucketCapacity;
			nextIndex = -1;
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe bool MoveNext(ref int bucketIndex, ref int nextIndex, out int index)
		{
			if (nextIndex != -1)
			{
				index = nextIndex;
				nextIndex = Next[nextIndex];
				return true;
			}
			return MoveNextSearch(ref bucketIndex, ref nextIndex, out index);
		}

		internal unsafe global::Unity.Collections.NativeArray<TKey> GetKeyArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeArray<TKey> result = global::Unity.Collections.CollectionHelper.CreateNativeArray<TKey>(Count, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int i = 0;
			int num = 0;
			int length = result.Length;
			for (int bucketCapacity = BucketCapacity; i < bucketCapacity; i++)
			{
				if (num >= length)
				{
					break;
				}
				for (int num2 = Buckets[i]; num2 != -1; num2 = Next[num2])
				{
					result[num++] = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(Keys, num2);
				}
			}
			return result;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		internal unsafe global::Unity.Collections.NativeArray<TValue> GetValueArray<TValue>(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where TValue : unmanaged
		{
			global::Unity.Collections.NativeArray<TValue> result = global::Unity.Collections.CollectionHelper.CreateNativeArray<TValue>(Count, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int i = 0;
			int num = 0;
			int length = result.Length;
			for (int bucketCapacity = BucketCapacity; i < bucketCapacity; i++)
			{
				if (num >= length)
				{
					break;
				}
				for (int num2 = Buckets[i]; num2 != -1; num2 = Next[num2])
				{
					result[num++] = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TValue>(Ptr, num2);
				}
			}
			return result;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		internal unsafe global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays<TValue>(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where TValue : unmanaged
		{
			global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> result = new global::Unity.Collections.NativeKeyValueArrays<TKey, TValue>(Count, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int i = 0;
			int num = 0;
			int length = result.Length;
			for (int bucketCapacity = BucketCapacity; i < bucketCapacity; i++)
			{
				if (num >= length)
				{
					break;
				}
				for (int num2 = Buckets[i]; num2 != -1; num2 = Next[num2])
				{
					result.Keys[num] = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(Keys, num2);
					result.Values[num] = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TValue>(Ptr, num2);
					num++;
				}
			}
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckIndexOutOfBounds(int idx)
		{
			if ((uint)idx >= (uint)Capacity)
			{
				throw new global::System.InvalidOperationException($"Internal HashMap error. idx {idx}");
			}
		}
	}
}
