namespace Unity.Netcode.Transports.UTP
{
	internal class BatchedReceiveQueue
	{
		private byte[] m_Data;

		private int m_Offset;

		private int m_Length;

		public bool IsEmpty => m_Length <= 0;

		public unsafe BatchedReceiveQueue(global::Unity.Collections.DataStreamReader reader)
		{
			m_Data = new byte[reader.Length];
			fixed (byte* data = m_Data)
			{
				global::Unity.Collections.LowLevel.Unsafe.DataStreamExtensions.ReadBytesUnsafe(ref reader, data, reader.Length);
			}
			m_Offset = 0;
			m_Length = reader.Length;
		}

		public unsafe void PushReader(global::Unity.Collections.DataStreamReader reader)
		{
			if (m_Data.Length - (m_Offset + m_Length) < reader.Length)
			{
				if (m_Length > 0)
				{
					global::System.Array.Copy(m_Data, m_Offset, m_Data, 0, m_Length);
				}
				m_Offset = 0;
				while (m_Data.Length - m_Length < reader.Length)
				{
					global::System.Array.Resize(ref m_Data, m_Data.Length * 2);
				}
			}
			fixed (byte* data = m_Data)
			{
				global::Unity.Collections.LowLevel.Unsafe.DataStreamExtensions.ReadBytesUnsafe(ref reader, data + m_Offset + m_Length, reader.Length);
			}
			m_Length += reader.Length;
		}

		public global::System.ArraySegment<byte> PopMessage()
		{
			if (m_Length < 4)
			{
				return default(global::System.ArraySegment<byte>);
			}
			int num = global::System.BitConverter.ToInt32(m_Data, m_Offset);
			if (m_Length - 4 < num)
			{
				return default(global::System.ArraySegment<byte>);
			}
			global::System.ArraySegment<byte> result = new global::System.ArraySegment<byte>(m_Data, m_Offset + 4, num);
			m_Offset += 4 + num;
			m_Length -= 4 + num;
			return result;
		}
	}
}
