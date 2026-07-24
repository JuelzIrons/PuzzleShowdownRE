namespace Unity.Netcode
{
	internal class MetricHooks : global::Unity.Netcode.INetworkHooks
	{
		private readonly global::Unity.Netcode.NetworkManager m_NetworkManager;

		public MetricHooks(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_NetworkManager = networkManager;
		}

		public void OnBeforeSendMessage<T>(ulong clientId, ref T message, global::Unity.Netcode.NetworkDelivery delivery) where T : global::Unity.Netcode.INetworkMessage
		{
		}

		public void OnAfterSendMessage<T>(ulong clientId, ref T message, global::Unity.Netcode.NetworkDelivery delivery, int messageSizeBytes) where T : global::Unity.Netcode.INetworkMessage
		{
			m_NetworkManager.NetworkMetrics.TrackNetworkMessageSent(clientId, typeof(T).Name, messageSizeBytes);
		}

		public void OnBeforeReceiveMessage(ulong senderId, global::System.Type messageType, int messageSizeBytes)
		{
			m_NetworkManager.NetworkMetrics.TrackNetworkMessageReceived(senderId, messageType.Name, messageSizeBytes);
		}

		public void OnAfterReceiveMessage(ulong senderId, global::System.Type messageType, int messageSizeBytes)
		{
		}

		public void OnBeforeSendBatch(ulong clientId, int messageCount, int batchSizeInBytes, global::Unity.Netcode.NetworkDelivery delivery)
		{
		}

		public void OnAfterSendBatch(ulong clientId, int messageCount, int batchSizeInBytes, global::Unity.Netcode.NetworkDelivery delivery)
		{
			m_NetworkManager.NetworkMetrics.TrackTransportBytesSent(batchSizeInBytes);
		}

		public void OnBeforeReceiveBatch(ulong senderId, int messageCount, int batchSizeInBytes)
		{
			m_NetworkManager.NetworkMetrics.TrackTransportBytesReceived(batchSizeInBytes);
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
