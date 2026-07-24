namespace Unity.Collections
{
	internal sealed class NativeHashMapDebuggerTypeProxy<TKey, TValue> where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		private unsafe global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* Data;

		public unsafe global::System.Collections.Generic.List<global::Unity.Collections.Pair<TKey, TValue>> Items
		{
			get
			{
				if (Data == null)
				{
					return null;
				}
				global::System.Collections.Generic.List<global::Unity.Collections.Pair<TKey, TValue>> list = new global::System.Collections.Generic.List<global::Unity.Collections.Pair<TKey, TValue>>();
				global::Unity.Collections.NativeKeyValueArrays<TKey, TValue> keyValueArrays = Data->GetKeyValueArrays<TValue>(global::Unity.Collections.Allocator.Temp);
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

		public unsafe NativeHashMapDebuggerTypeProxy(global::Unity.Collections.NativeHashMap<TKey, TValue> target)
		{
			Data = target.m_Data;
		}

		public unsafe NativeHashMapDebuggerTypeProxy(global::Unity.Collections.NativeHashMap<TKey, TValue>.ReadOnly target)
		{
			Data = target.m_Data;
		}
	}
}
