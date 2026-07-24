namespace Unity.Collections.LowLevel.Unsafe
{
	internal struct UnsafeParallelHashMapDataEnumerator
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;

		internal int m_Index;

		internal int m_BucketIndex;

		internal int m_NextIndex;

		internal unsafe UnsafeParallelHashMapDataEnumerator(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data)
		{
			m_Buffer = data;
			m_Index = -1;
			m_BucketIndex = 0;
			m_NextIndex = -1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe bool MoveNext()
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.MoveNext(m_Buffer, ref m_BucketIndex, ref m_NextIndex, out m_Index);
		}

		internal void Reset()
		{
			m_Index = -1;
			m_BucketIndex = 0;
			m_NextIndex = -1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue> GetCurrent<TKey, TValue>() where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged
		{
			return new global::Unity.Collections.LowLevel.Unsafe.KeyValue<TKey, TValue>
			{
				m_Buffer = m_Buffer,
				m_Index = m_Index
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe TKey GetCurrentKey<TKey>() where TKey : unmanaged, global::System.IEquatable<TKey>
		{
			if (m_Index != -1)
			{
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<TKey>(m_Buffer->keys, m_Index);
			}
			return default(TKey);
		}
	}
}
