namespace Unity.Collections
{
	internal sealed class NativeParallelMultiHashMapDebuggerTypeProxy<TKey, TValue> where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		private global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue> m_Target;

		public global::System.Collections.Generic.List<global::Unity.Collections.ListPair<TKey, global::System.Collections.Generic.List<TValue>>> Items
		{
			get
			{
				global::System.Collections.Generic.List<global::Unity.Collections.ListPair<TKey, global::System.Collections.Generic.List<TValue>>> list = new global::System.Collections.Generic.List<global::Unity.Collections.ListPair<TKey, global::System.Collections.Generic.List<TValue>>>();
				(global::Unity.Collections.NativeArray<TKey>, int) tuple = default((global::Unity.Collections.NativeArray<TKey>, int));
				using (global::Unity.Collections.NativeParallelHashMap<TKey, TValue> nativeParallelHashMap = new global::Unity.Collections.NativeParallelHashMap<TKey, TValue>(m_Target.Count(), global::Unity.Collections.Allocator.Temp))
				{
					global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator enumerator = m_Target.GetEnumerator();
					while (enumerator.MoveNext())
					{
						nativeParallelHashMap.TryAdd(enumerator.Current.Key, default(TValue));
					}
					tuple.Item1 = nativeParallelHashMap.GetKeyArray(global::Unity.Collections.Allocator.Temp);
					tuple.Item2 = tuple.Item1.Length;
				}
				using (tuple.Item1)
				{
					for (int i = 0; i < tuple.Item2; i++)
					{
						global::System.Collections.Generic.List<TValue> list2 = new global::System.Collections.Generic.List<TValue>();
						if (m_Target.TryGetFirstValue(tuple.Item1[i], out var item, out var it))
						{
							do
							{
								list2.Add(item);
							}
							while (m_Target.TryGetNextValue(out item, ref it));
						}
						list.Add(new global::Unity.Collections.ListPair<TKey, global::System.Collections.Generic.List<TValue>>(tuple.Item1[i], list2));
					}
					return list;
				}
			}
		}

		public NativeParallelMultiHashMapDebuggerTypeProxy(global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue> target)
		{
			m_Target = target;
		}
	}
}
