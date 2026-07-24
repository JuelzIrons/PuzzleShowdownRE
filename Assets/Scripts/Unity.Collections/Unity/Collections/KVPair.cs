namespace Unity.Collections
{
	[global::System.Diagnostics.DebuggerDisplay("Key = {Key}, Value = {Value}")]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct KVPair<TKey, TValue> where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<TKey>* m_Data;

		internal int m_Index;

		internal int m_Next;

		public static global::Unity.Collections.KVPair<TKey, TValue> Null => new global::Unity.Collections.KVPair<TKey, TValue>
		{
			m_Index = -1
		};

		public unsafe TKey Key
		{
			get
			{
				if (m_Index != -1)
				{
					return m_Data->Keys[m_Index];
				}
				return default(TKey);
			}
		}

		public unsafe ref TValue Value => ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<TValue>(m_Data->Ptr + sizeof(TValue) * m_Index);

		public unsafe bool GetKeyValue(out TKey key, out TValue value)
		{
			if (m_Index != -1)
			{
				key = m_Data->Keys[m_Index];
				value = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TValue>(m_Data->Ptr, m_Index);
				return true;
			}
			key = default(TKey);
			value = default(TValue);
			return false;
		}
	}
}
