namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct UnsafeParallelHashMapData
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		internal unsafe byte* values;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		internal unsafe byte* keys;

		[global::System.Runtime.InteropServices.FieldOffset(16)]
		internal unsafe byte* next;

		[global::System.Runtime.InteropServices.FieldOffset(24)]
		internal unsafe byte* buckets;

		[global::System.Runtime.InteropServices.FieldOffset(32)]
		internal int keyCapacity;

		[global::System.Runtime.InteropServices.FieldOffset(36)]
		internal int bucketCapacityMask;

		[global::System.Runtime.InteropServices.FieldOffset(40)]
		internal int allocatedIndexLength;

		private const int kFirstFreeTLSOffset = 64;

		internal const int IntsPerCacheLine = 16;

		internal unsafe int* firstFreeTLS => (int*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref this) + 16;

		internal static int GetBucketSize(int capacity)
		{
			return capacity * 2;
		}

		internal static int GrowCapacity(int capacity)
		{
			if (capacity == 0)
			{
				return 1;
			}
			return capacity * 2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal unsafe static void AllocateHashMap<TKey, TValue>(int length, int bucketLength, global::Unity.Collections.AllocatorManager.AllocatorHandle label, out global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* outBuf) where TKey : unmanaged where TValue : unmanaged
		{
			int threadIndexCount = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ThreadIndexCount;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* ptr = (global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*)global::Unity.Collections.Memory.Unmanaged.Allocate(64 + 64 * threadIndexCount, 64, label);
			bucketLength = global::Unity.Mathematics.math.ceilpow2(bucketLength);
			ptr->keyCapacity = length;
			ptr->bucketCapacityMask = bucketLength - 1;
			int keyOffset;
			int nextOffset;
			int bucketOffset;
			int num = CalculateDataSize<TKey, TValue>(length, bucketLength, out keyOffset, out nextOffset, out bucketOffset);
			ptr->values = (byte*)global::Unity.Collections.Memory.Unmanaged.Allocate(num, 64, label);
			ptr->keys = ptr->values + keyOffset;
			ptr->next = ptr->values + nextOffset;
			ptr->buckets = ptr->values + bucketOffset;
			outBuf = ptr;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal unsafe static void ReallocateHashMap<TKey, TValue>(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, int newCapacity, int newBucketCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle label) where TKey : unmanaged where TValue : unmanaged
		{
			newBucketCapacity = global::Unity.Mathematics.math.ceilpow2(newBucketCapacity);
			if (data->keyCapacity == newCapacity && data->bucketCapacityMask + 1 == newBucketCapacity)
			{
				return;
			}
			int keyOffset;
			int nextOffset;
			int bucketOffset;
			byte* ptr = (byte*)global::Unity.Collections.Memory.Unmanaged.Allocate(CalculateDataSize<TKey, TValue>(newCapacity, newBucketCapacity, out keyOffset, out nextOffset, out bucketOffset), 64, label);
			byte* destination = ptr + keyOffset;
			byte* ptr2 = ptr + nextOffset;
			byte* ptr3 = ptr + bucketOffset;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, data->values, data->keyCapacity * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>());
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, data->keys, data->keyCapacity * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TKey>());
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr2, data->next, data->keyCapacity * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<int>());
			for (int i = data->keyCapacity; i < newCapacity; i++)
			{
				((int*)ptr2)[i] = -1;
			}
			for (int j = 0; j < newBucketCapacity; j++)
			{
				((int*)ptr3)[j] = -1;
			}
			for (int k = 0; k <= data->bucketCapacityMask; k++)
			{
				int* ptr4 = (int*)data->buckets;
				int* ptr5 = (int*)ptr2;
				while (ptr4[k] >= 0)
				{
					int num = ptr4[k];
					ptr4[k] = ptr5[num];
					int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(data->keys, num).GetHashCode() & (newBucketCapacity - 1);
					ptr5[num] = ((int*)ptr3)[num2];
					((int*)ptr3)[num2] = num;
				}
			}
			global::Unity.Collections.Memory.Unmanaged.Free(data->values, label);
			if (data->allocatedIndexLength > data->keyCapacity)
			{
				data->allocatedIndexLength = data->keyCapacity;
			}
			data->values = ptr;
			data->keys = destination;
			data->next = ptr2;
			data->buckets = ptr3;
			data->keyCapacity = newCapacity;
			data->bucketCapacityMask = newBucketCapacity - 1;
		}

		internal unsafe static void DeallocateHashMap(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.Memory.Unmanaged.Free(data->values, allocator);
			global::Unity.Collections.Memory.Unmanaged.Free(data, allocator);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal static int CalculateDataSize<TKey, TValue>(int length, int bucketLength, out int keyOffset, out int nextOffset, out int bucketOffset) where TKey : unmanaged where TValue : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>();
			int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TKey>();
			int num3 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<int>();
			int num4 = global::Unity.Collections.CollectionHelper.Align(num * length, 64);
			int num5 = global::Unity.Collections.CollectionHelper.Align(num2 * length, 64);
			int num6 = global::Unity.Collections.CollectionHelper.Align(num3 * length, 64);
			int num7 = global::Unity.Collections.CollectionHelper.Align(num3 * bucketLength, 64);
			int result = num4 + num5 + num6 + num7;
			keyOffset = num4;
			nextOffset = keyOffset + num5;
			bucketOffset = nextOffset + num6;
			return result;
		}

		internal unsafe static bool IsEmpty(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data)
		{
			if (data->allocatedIndexLength <= 0)
			{
				return true;
			}
			int* ptr = (int*)data->buckets;
			int* ptr2 = (int*)data->next;
			int num = data->bucketCapacityMask;
			for (int i = 0; i <= num; i++)
			{
				if (ptr[i] != -1)
				{
					return false;
				}
			}
			return true;
		}

		internal unsafe static int GetCount(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data)
		{
			if (data->allocatedIndexLength <= 0)
			{
				return 0;
			}
			int* ptr = (int*)data->next;
			int num = 0;
			int threadIndexCount = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ThreadIndexCount;
			for (int i = 0; i < threadIndexCount; i++)
			{
				for (int num2 = data->firstFreeTLS[i * 16]; num2 >= 0; num2 = ptr[num2])
				{
					num++;
				}
			}
			return global::Unity.Mathematics.math.min(data->keyCapacity, data->allocatedIndexLength) - num;
		}

		internal unsafe static bool MoveNextSearch(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, ref int bucketIndex, ref int nextIndex, out int index)
		{
			int* ptr = (int*)data->buckets;
			int num = data->bucketCapacityMask;
			for (int i = bucketIndex; i <= num; i++)
			{
				int num2 = ptr[i];
				if (num2 != -1)
				{
					int* ptr2 = (int*)data->next;
					index = num2;
					bucketIndex = i + 1;
					nextIndex = ptr2[num2];
					return true;
				}
			}
			index = -1;
			bucketIndex = num + 1;
			nextIndex = -1;
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe static bool MoveNext(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, ref int bucketIndex, ref int nextIndex, out int index)
		{
			if (nextIndex != -1)
			{
				int* ptr = (int*)data->next;
				index = nextIndex;
				nextIndex = ptr[nextIndex];
				return true;
			}
			return MoveNextSearch(data, ref bucketIndex, ref nextIndex, out index);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		internal unsafe static void GetKeyArray<TKey>(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, global::Unity.Collections.NativeArray<TKey> result) where TKey : unmanaged
		{
			int* ptr = (int*)data->buckets;
			int* ptr2 = (int*)data->next;
			int i = 0;
			int num = 0;
			int length = result.Length;
			for (; i <= data->bucketCapacityMask; i++)
			{
				if (num >= length)
				{
					break;
				}
				for (int num2 = ptr[i]; num2 != -1; num2 = ptr2[num2])
				{
					result[num++] = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(data->keys, num2);
				}
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		internal unsafe static void GetValueArray<TValue>(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, global::Unity.Collections.NativeArray<TValue> result) where TValue : unmanaged
		{
			int* ptr = (int*)data->buckets;
			int* ptr2 = (int*)data->next;
			int i = 0;
			int num = 0;
			int length = result.Length;
			for (int num2 = data->bucketCapacityMask; i <= num2; i++)
			{
				if (num >= length)
				{
					break;
				}
				for (int num3 = ptr[i]; num3 != -1; num3 = ptr2[num3])
				{
					result[num++] = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TValue>(data->values, num3);
				}
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal unsafe static void GetKeyValueArrays<TKey, TValue>(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> result) where TKey : unmanaged where TValue : unmanaged
		{
			int* ptr = (int*)data->buckets;
			int* ptr2 = (int*)data->next;
			int i = 0;
			int num = 0;
			int length = result.Length;
			for (int num2 = data->bucketCapacityMask; i <= num2; i++)
			{
				if (num >= length)
				{
					break;
				}
				for (int num3 = ptr[i]; num3 != -1; num3 = ptr2[num3])
				{
					result.Keys[num] = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(data->keys, num3);
					result.Values[num] = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TValue>(data->values, num3);
					num++;
				}
			}
		}

		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBucketData GetBucketData()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBucketData(values, keys, next, buckets, bucketCapacityMask);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private unsafe static void CheckHashMapReallocateDoesNotShrink(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, int newCapacity)
		{
			if (data->keyCapacity > newCapacity)
			{
				throw new global::System.InvalidOperationException("Shrinking a hash map is not supported");
			}
		}
	}
}
