namespace Unity.Collections
{
	internal sealed class NativeParallelHashMapDebuggerTypeProxy<TKey, TValue> where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		private global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey, TValue> m_Target;

		public global::System.Collections.Generic.List<global::Unity.Collections.Pair<TKey, TValue>> Items
		{
			get
			{
				global::System.Collections.Generic.List<global::Unity.Collections.Pair<TKey, TValue>> list = new global::System.Collections.Generic.List<global::Unity.Collections.Pair<TKey, TValue>>();
				global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> keyValueArrays = m_Target.GetKeyValueArrays(global::Unity.Collections.Allocator.Temp);
				try
				{
					for (int i = 0; i < keyValueArrays.Length; i++)
					{
						global::Unity.Collections.NativeArray<TKey> keys = keyValueArrays.Keys;
						TKey k = keys[i];
						global::Unity.Collections.NativeArray<TValue> values = keyValueArrays.Values;
						list.Add(new global::Unity.Collections.Pair<TKey, TValue>(k, values[i]));
					}
					return list;
				}
				finally
				{
					((global::System.IDisposable)keyValueArrays/*cast due to .constrained prefix*/).Dispose();
				}
			}
		}

		public NativeParallelHashMapDebuggerTypeProxy(global::Unity.Collections.NativeParallelHashMap<TKey, TValue> target)
		{
			m_Target = target.m_HashMapData;
		}

		internal NativeParallelHashMapDebuggerTypeProxy(global::Unity.Collections.NativeParallelHashMap<TKey, TValue>.ReadOnly target)
		{
			m_Target = target.m_HashMapData;
		}
	}
}
