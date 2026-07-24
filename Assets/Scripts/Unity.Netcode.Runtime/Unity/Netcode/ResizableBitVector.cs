namespace Unity.Netcode
{
	internal struct ResizableBitVector : global::Unity.Netcode.INetworkSerializable, global::System.IDisposable
	{
		private global::Unity.Collections.NativeList<byte> m_Bits;

		private const int k_Divisor = 8;

		public ResizableBitVector(global::Unity.Collections.Allocator allocator)
		{
			m_Bits = new global::Unity.Collections.NativeList<byte>(allocator);
		}

		public void Dispose()
		{
			m_Bits.Dispose();
		}

		public int GetSerializedSize()
		{
			return 4 + m_Bits.Length;
		}

		private (int, int) GetBitData(int i)
		{
			int item = i / 8;
			int item2 = i % 8;
			return (item, item2);
		}

		public void Set(int i)
		{
			var (num, num2) = GetBitData(i);
			if (num >= m_Bits.Length)
			{
				m_Bits.Resize(num + 1, global::Unity.Collections.NativeArrayOptions.ClearMemory);
			}
			m_Bits[num] |= (byte)(1 << num2);
		}

		public void Unset(int i)
		{
			var (num, num2) = GetBitData(i);
			if (num < m_Bits.Length)
			{
				m_Bits[num] &= (byte)(~(1 << num2));
			}
		}

		public bool IsSet(int i)
		{
			var (num, num2) = GetBitData(i);
			if (num >= m_Bits.Length)
			{
				return false;
			}
			return (m_Bits[num] & (byte)(1 << num2)) != 0;
		}

		public unsafe void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
			int value = m_Bits.Length;
			serializer.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			m_Bits.ResizeUninitialized(value);
			byte* unsafePtr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_Bits);
			if (serializer.IsReader)
			{
				serializer.GetFastBufferReader().ReadBytesSafe(unsafePtr, value);
			}
			else
			{
				serializer.GetFastBufferWriter().WriteBytesSafe(unsafePtr, value);
			}
		}
	}
}
