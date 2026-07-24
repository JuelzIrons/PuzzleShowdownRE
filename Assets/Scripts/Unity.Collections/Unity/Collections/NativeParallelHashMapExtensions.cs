namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class NativeParallelHashMapExtensions
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static int Unique<T>(this global::Unity.Collections.NativeArray<T> array) where T : unmanaged, global::System.IEquatable<T>
		{
			if (array.Length == 0)
			{
				return 0;
			}
			int num = 0;
			int length = array.Length;
			int num2 = num;
			while (++num != length)
			{
				if (!array[num2].Equals(array[num]))
				{
					array[++num2] = array[num];
				}
			}
			return ++num2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static (global::Unity.Collections.NativeArray<TKey>, int) GetUniqueKeyArray<TKey, TValue>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue> container, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where TKey : unmanaged, global::System.IEquatable<TKey>, global::System.IComparable<TKey> where TValue : unmanaged
		{
			global::Unity.Collections.NativeArray<TKey> keyArray = container.GetKeyArray(allocator);
			keyArray.Sort();
			int item = keyArray.Unique();
			return (keyArray, item);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static (global::Unity.Collections.NativeArray<TKey>, int) GetUniqueKeyArray<TKey, TValue>(this global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue> container, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where TKey : unmanaged, global::System.IEquatable<TKey>, global::System.IComparable<TKey> where TValue : unmanaged
		{
			global::Unity.Collections.NativeArray<TKey> keyArray = container.GetKeyArray(allocator);
			keyArray.Sort();
			int item = keyArray.Unique();
			return (keyArray, item);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBucketData GetUnsafeBucketData<TKey, TValue>(this global::Unity.Collections.NativeParallelHashMap<TKey, TValue> container) where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
		{
			return container.m_HashMapData.m_Buffer->GetBucketData();
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBucketData GetUnsafeBucketData<TKey, TValue>(this global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue> container) where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
		{
			return container.m_MultiHashMapData.m_Buffer->GetBucketData();
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static void Remove<TKey, TValue>(this global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue> container, TKey key, TValue value) where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged, global::System.IEquatable<TValue>
		{
			container.m_MultiHashMapData.Remove(key, value);
		}
	}
}
