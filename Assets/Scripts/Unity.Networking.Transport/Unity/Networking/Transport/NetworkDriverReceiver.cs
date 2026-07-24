namespace Unity.Networking.Transport
{
	internal struct NetworkDriverReceiver : global::System.IDisposable
	{
		private const int k_InitialDataStreamSize = 4096;

		private global::Unity.Networking.Transport.PacketsQueue m_ReceiveQueue;

		private global::Unity.Collections.NativeList<byte> m_DataStream;

		private global::Unity.Networking.Transport.OperationResult m_Result;

		internal global::Unity.Networking.Transport.OperationResult Result => m_Result;

		internal global::Unity.Networking.Transport.PacketsQueue ReceiveQueue => m_ReceiveQueue;

		internal NetworkDriverReceiver(global::Unity.Networking.Transport.PacketsQueue receiveQueue)
		{
			m_ReceiveQueue = receiveQueue;
			m_DataStream = new global::Unity.Collections.NativeList<byte>(4096, global::Unity.Collections.Allocator.Persistent);
			m_Result = new global::Unity.Networking.Transport.OperationResult("receive", global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			if (m_DataStream.IsCreated)
			{
				m_ReceiveQueue.Dispose();
				m_DataStream.Dispose();
				m_Result.Dispose();
			}
		}

		internal global::Unity.Collections.NativeArray<byte> GetDataStreamSubArray(int offset, int size)
		{
			return m_DataStream.AsArray().GetSubArray(offset, size);
		}

		internal void ClearStream()
		{
			m_DataStream.Clear();
		}

		private unsafe int AppendToStream(byte* dataPtr, int dataLength)
		{
			global::Unity.Networking.Transport.Utilities.NativeListExt.ResizeUninitializedTillPowerOf2(m_DataStream, m_DataStream.Length + dataLength);
			int length = m_DataStream.Length;
			m_DataStream.Length = length + dataLength;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_DataStream) + length, dataPtr, dataLength);
			return length;
		}

		internal unsafe int AppendToStream(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
		{
			global::Unity.Networking.Transport.Utilities.NativeListExt.ResizeUninitializedTillPowerOf2(m_DataStream, m_DataStream.Length + packetProcessor.Length);
			int length = m_DataStream.Length;
			m_DataStream.Length = length + packetProcessor.Length;
			packetProcessor.CopyPayload(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_DataStream) + length, packetProcessor.Length);
			return length;
		}

		internal int AppendToStream(byte value)
		{
			global::Unity.Networking.Transport.Utilities.NativeListExt.ResizeUninitializedTillPowerOf2(m_DataStream, m_DataStream.Length + 1);
			int length = m_DataStream.Length;
			m_DataStream.Length = length + 1;
			m_DataStream[length] = value;
			return length;
		}

		internal unsafe void PushDataEvent(global::Unity.Networking.Transport.NetworkConnection con, int pipelineId, byte* dataPtr, int dataLength, ref global::Unity.Networking.Transport.NetworkEventQueue eventQueue)
		{
			int offset = AppendToStream(dataPtr, dataLength);
			eventQueue.PushEvent(new global::Unity.Networking.Transport.NetworkEvent
			{
				pipelineId = (short)pipelineId,
				connectionId = con.InternalId,
				type = global::Unity.Networking.Transport.NetworkEvent.Type.Data,
				offset = offset,
				size = dataLength
			});
		}
	}
}
