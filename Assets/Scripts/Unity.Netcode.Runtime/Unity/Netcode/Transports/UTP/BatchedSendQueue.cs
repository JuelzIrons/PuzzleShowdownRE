namespace Unity.Netcode.Transports.UTP
{
	internal struct BatchedSendQueue : global::System.IDisposable
	{
		private global::Unity.Collections.NativeList<byte> m_Data;

		private global::Unity.Collections.NativeArray<int> m_HeadTailIndices;

		private int m_MaximumCapacity;

		private int m_MinimumCapacity;

		public const int PerMessageOverhead = 4;

		internal const int MinimumMinimumCapacity = 4096;

		internal const int MaximumMaximumCapacity = 2147483646;

		private const int k_HeadInternalIndex = 0;

		private const int k_TailInternalIndex = 1;

		private int HeadIndex
		{
			get
			{
				return m_HeadTailIndices[0];
			}
			set
			{
				m_HeadTailIndices[0] = value;
			}
		}

		private int TailIndex
		{
			get
			{
				return m_HeadTailIndices[1];
			}
			set
			{
				m_HeadTailIndices[1] = value;
			}
		}

		public int Length => TailIndex - HeadIndex;

		public int Capacity => m_Data.Length;

		public bool IsEmpty => HeadIndex == TailIndex;

		public bool IsCreated => m_Data.IsCreated;

		public BatchedSendQueue(int capacity)
		{
			m_MaximumCapacity = capacity + (capacity & 1);
			m_MinimumCapacity = m_MaximumCapacity;
			while (m_MinimumCapacity / 2 >= 4096)
			{
				m_MinimumCapacity /= 2;
			}
			m_Data = new global::Unity.Collections.NativeList<byte>(m_MinimumCapacity, global::Unity.Collections.Allocator.Persistent);
			m_HeadTailIndices = new global::Unity.Collections.NativeArray<int>(2, global::Unity.Collections.Allocator.Persistent);
			m_Data.ResizeUninitialized(m_MinimumCapacity);
			HeadIndex = 0;
			TailIndex = 0;
		}

		public void Dispose()
		{
			if (IsCreated)
			{
				m_Data.Dispose();
				m_HeadTailIndices.Dispose();
			}
		}

		private unsafe void WriteBytes(ref global::Unity.Collections.DataStreamWriter writer, byte* data, int length)
		{
			global::Unity.Collections.LowLevel.Unsafe.DataStreamExtensions.WriteBytesUnsafe(ref writer, data, length);
		}

		private unsafe void AppendDataAtTail(global::System.ArraySegment<byte> data)
		{
			global::Unity.Collections.DataStreamWriter writer = new global::Unity.Collections.DataStreamWriter(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_Data) + TailIndex, Capacity - TailIndex);
			writer.WriteInt(data.Count);
			fixed (byte* array = data.Array)
			{
				WriteBytes(ref writer, array + data.Offset, data.Count);
			}
			TailIndex += 4 + data.Count;
		}

		public unsafe bool PushMessage(global::System.ArraySegment<byte> message)
		{
			if (!IsCreated)
			{
				return false;
			}
			if (Capacity - TailIndex >= 4 + message.Count)
			{
				AppendDataAtTail(message);
				return true;
			}
			if (HeadIndex > 0 && Length > 0)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_Data), global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_Data) + HeadIndex, Length);
				TailIndex = Length;
				HeadIndex = 0;
			}
			if (Capacity - TailIndex >= 4 + message.Count)
			{
				AppendDataAtTail(message);
				while (TailIndex < Capacity / 4 && Capacity > m_MinimumCapacity)
				{
					m_Data.ResizeUninitialized(Capacity / 2);
				}
				return true;
			}
			while (Capacity - TailIndex < 4 + message.Count)
			{
				if (Capacity * 2 > m_MaximumCapacity)
				{
					return false;
				}
				m_Data.ResizeUninitialized(Capacity * 2);
			}
			AppendDataAtTail(message);
			return true;
		}

		public unsafe int FillWriterWithMessages(ref global::Unity.Collections.DataStreamWriter writer, int softMaxBytes = 0)
		{
			if (!IsCreated || Length == 0)
			{
				return 0;
			}
			softMaxBytes = ((softMaxBytes == 0) ? writer.Capacity : global::System.Math.Min(softMaxBytes, writer.Capacity));
			global::Unity.Collections.DataStreamReader dataStreamReader = new global::Unity.Collections.DataStreamReader(m_Data.AsArray());
			int num = HeadIndex;
			dataStreamReader.SeekSet(num);
			int num2 = dataStreamReader.ReadInt();
			int num3 = num2 + 4;
			if (num3 > softMaxBytes && num3 <= writer.Capacity)
			{
				writer.WriteInt(num2);
				WriteBytes(ref writer, global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_Data) + dataStreamReader.GetBytesRead(), num2);
				return num3;
			}
			int num4 = 0;
			while (num < TailIndex)
			{
				dataStreamReader.SeekSet(num);
				num2 = dataStreamReader.ReadInt();
				num3 = num2 + 4;
				if (num4 + num3 > softMaxBytes)
				{
					break;
				}
				writer.WriteInt(num2);
				WriteBytes(ref writer, global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_Data) + dataStreamReader.GetBytesRead(), num2);
				num += num3;
				num4 += num3;
			}
			return num4;
		}

		public unsafe int FillWriterWithBytes(ref global::Unity.Collections.DataStreamWriter writer, int maxBytes = 0)
		{
			if (!IsCreated || Length == 0)
			{
				return 0;
			}
			int num = global::System.Math.Min((maxBytes == 0) ? writer.Capacity : global::System.Math.Min(maxBytes, writer.Capacity), Length);
			WriteBytes(ref writer, global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_Data) + HeadIndex, num);
			return num;
		}

		public void Consume(int size)
		{
			if (size >= Length)
			{
				HeadIndex = 0;
				TailIndex = 0;
				m_Data.ResizeUninitialized(m_MinimumCapacity);
			}
			else
			{
				HeadIndex += size;
			}
		}
	}
}
