namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Diagnostics.DebuggerDisplay("Key = {Key}, Value = {Value}")]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct KeyValue<TKey, TValue> where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
	{
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;

		internal int m_Index;

		internal int m_Next;

		public static global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue> Null => new global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>
		{
			m_Index = -1
		};

		public unsafe TKey Key
		{
			get
			{
				if (m_Index != -1)
				{
					return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(m_Buffer->keys, m_Index);
				}
				return default(TKey);
			}
		}

		public unsafe ref TValue Value => ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<TValue>(m_Buffer->values + global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>() * m_Index);

		public unsafe bool GetKeyValue(out TKey key, out TValue value)
		{
			if (m_Index != -1)
			{
				key = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(m_Buffer->keys, m_Index);
				value = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TValue>(m_Buffer->values, m_Index);
				return true;
			}
			key = default(TKey);
			value = default(TValue);
			return false;
		}
	}
}
