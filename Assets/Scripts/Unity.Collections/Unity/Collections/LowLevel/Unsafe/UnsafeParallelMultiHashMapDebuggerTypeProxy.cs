namespace Unity.Collections.LowLevel.Unsafe
{
	internal sealed class UnsafeParallelMultiHashMapDebuggerTypeProxy<TKey, TValue> where TKey : unmanaged, global::System.IEquatable<TKey>, global::System.IComparable<TKey> where TValue : unmanaged
	{
		private global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue> m_Target;

		public global::System.Collections.Generic.List<global::Unity.Collections.ListPair<TKey, global::System.Collections.Generic.List<TValue>>> Items
		{
			get
			{
				global::System.Collections.Generic.List<global::Unity.Collections.ListPair<TKey, global::System.Collections.Generic.List<TValue>>> list = new global::System.Collections.Generic.List<global::Unity.Collections.ListPair<TKey, global::System.Collections.Generic.List<TValue>>>();
				(global::Unity.Collections.NativeArray<TKey>, int) uniqueKeyArray = GetUniqueKeyArray(ref m_Target, global::Unity.Collections.Allocator.Temp);
				using (uniqueKeyArray.Item1)
				{
					for (int i = 0; i < uniqueKeyArray.Item2; i++)
					{
						global::System.Collections.Generic.List<TValue> list2 = new global::System.Collections.Generic.List<TValue>();
						if (m_Target.TryGetFirstValue(uniqueKeyArray.Item1[i], out var item, out var it))
						{
							do
							{
								list2.Add(item);
							}
							while (m_Target.TryGetNextValue(out item, ref it));
						}
						list.Add(new global::Unity.Collections.ListPair<TKey, global::System.Collections.Generic.List<TValue>>(uniqueKeyArray.Item1[i], list2));
					}
					return list;
				}
			}
		}

		public UnsafeParallelMultiHashMapDebuggerTypeProxy(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue> target)
		{
			m_Target = target;
		}

		public static (global::Unity.Collections.NativeArray<TKey>, int) GetUniqueKeyArray(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey, TValue> hashMap, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeArray<TKey> keyArray = hashMap.GetKeyArray(allocator);
			keyArray.Sort();
			int item = keyArray.Unique();
			return (keyArray, item);
		}
	}
}
