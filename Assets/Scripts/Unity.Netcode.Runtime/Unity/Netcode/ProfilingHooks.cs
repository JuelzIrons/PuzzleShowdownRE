namespace Unity.Netcode
{
	internal class ProfilingHooks : global::Unity.Netcode.INetworkHooks
	{
		private global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Profiling.ProfilerMarker> m_HandlerProfilerMarkers = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Profiling.ProfilerMarker>();

		private global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Profiling.ProfilerMarker> m_SenderProfilerMarkers = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Profiling.ProfilerMarker>();

		private readonly global::Unity.Profiling.ProfilerMarker m_SendBatch = new global::Unity.Profiling.ProfilerMarker("NetworkMessageManager.SendBatch");

		private readonly global::Unity.Profiling.ProfilerMarker m_ReceiveBatch = new global::Unity.Profiling.ProfilerMarker("NetworkMessageManager.ReceiveBatchBatch");

		private global::Unity.Profiling.ProfilerMarker GetHandlerProfilerMarker(global::System.Type type)
		{
			if (m_HandlerProfilerMarkers.TryGetValue(type, out var value))
			{
				return value;
			}
			value = new global::Unity.Profiling.ProfilerMarker("NetworkMessageManager.DeserializeAndHandle." + type.Name);
			m_HandlerProfilerMarkers[type] = value;
			return value;
		}

		private global::Unity.Profiling.ProfilerMarker GetSenderProfilerMarker(global::System.Type type)
		{
			if (m_SenderProfilerMarkers.TryGetValue(type, out var value))
			{
				return value;
			}
			value = new global::Unity.Profiling.ProfilerMarker("NetworkMessageManager.SerializeAndEnqueue." + type.Name);
			m_SenderProfilerMarkers[type] = value;
			return value;
		}

		public void OnBeforeSendMessage<T>(ulong clientId, ref T message, global::Unity.Netcode.NetworkDelivery delivery) where T : global::Unity.Netcode.INetworkMessage
		{
		}

		public void OnAfterSendMessage<T>(ulong clientId, ref T message, global::Unity.Netcode.NetworkDelivery delivery, int messageSizeBytes) where T : global::Unity.Netcode.INetworkMessage
		{
		}

		public void OnBeforeReceiveMessage(ulong senderId, global::System.Type messageType, int messageSizeBytes)
		{
		}

		public void OnAfterReceiveMessage(ulong senderId, global::System.Type messageType, int messageSizeBytes)
		{
		}

		public void OnBeforeSendBatch(ulong clientId, int messageCount, int batchSizeInBytes, global::Unity.Netcode.NetworkDelivery delivery)
		{
		}

		public void OnAfterSendBatch(ulong clientId, int messageCount, int batchSizeInBytes, global::Unity.Netcode.NetworkDelivery delivery)
		{
		}

		public void OnBeforeReceiveBatch(ulong senderId, int messageCount, int batchSizeInBytes)
		{
		}

		public void OnAfterReceiveBatch(ulong senderId, int messageCount, int batchSizeInBytes)
		{
		}

		public bool OnVerifyCanSend(ulong destinationId, global::System.Type messageType, global::Unity.Netcode.NetworkDelivery delivery)
		{
			return true;
		}

		public bool OnVerifyCanReceive(ulong senderId, global::System.Type messageType, global::Unity.Netcode.FastBufferReader messageContent, ref global::Unity.Netcode.NetworkContext context)
		{
			return true;
		}

		public void OnBeforeHandleMessage<T>(ref T message, ref global::Unity.Netcode.NetworkContext context) where T : global::Unity.Netcode.INetworkMessage
		{
		}

		public void OnAfterHandleMessage<T>(ref T message, ref global::Unity.Netcode.NetworkContext context) where T : global::Unity.Netcode.INetworkMessage
		{
		}
	}
}
