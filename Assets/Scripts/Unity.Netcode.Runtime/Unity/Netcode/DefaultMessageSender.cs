namespace Unity.Netcode
{
	internal class DefaultMessageSender : global::Unity.Netcode.INetworkMessageSender
	{
		private global::Unity.Netcode.NetworkTransport m_NetworkTransport;

		private global::Unity.Netcode.NetworkConnectionManager m_ConnectionManager;

		public DefaultMessageSender(global::Unity.Netcode.NetworkManager manager)
		{
			m_NetworkTransport = manager.NetworkConfig.NetworkTransport;
			m_ConnectionManager = manager.ConnectionManager;
		}

		public void Send(ulong clientId, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.FastBufferWriter batchData)
		{
			global::System.ArraySegment<byte> payload = batchData.ToTempByteArray();
			(ulong, bool) tuple = m_ConnectionManager.ClientIdToTransportId(clientId);
			var (clientId2, _) = tuple;
			if (!tuple.Item2)
			{
				if (m_ConnectionManager.NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("Trying to send a message to a client who doesn't have a transport connection");
				}
			}
			else
			{
				m_NetworkTransport.Send(clientId2, payload, delivery);
			}
		}
	}
}
