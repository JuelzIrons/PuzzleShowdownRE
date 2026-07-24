namespace Unity.Netcode
{
	internal class NetworkManagerHooks : global::Unity.Netcode.INetworkHooks
	{
		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		internal NetworkManagerHooks(global::Unity.Netcode.NetworkManager manager)
		{
			m_NetworkManager = manager;
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
			return !m_NetworkManager.MessageManager.StopProcessing;
		}

		public bool OnVerifyCanReceive(ulong senderId, global::System.Type messageType, global::Unity.Netcode.FastBufferReader messageContent, ref global::Unity.Netcode.NetworkContext context)
		{
			if (m_NetworkManager.IsServer)
			{
				if (messageType == typeof(global::Unity.Netcode.ConnectionApprovedMessage))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						string transportErrorMessage = GetTransportErrorMessage(messageContent, m_NetworkManager);
						global::Unity.Netcode.NetworkLog.LogError("A ConnectionApprovedMessage was received from a client on the server side. " + transportErrorMessage);
					}
					return false;
				}
				if (m_NetworkManager.ConnectionManager.PendingClients.TryGetValue(senderId, out var value) && (value.ConnectionState == global::Unity.Netcode.PendingClient.State.PendingApproval || (value.ConnectionState == global::Unity.Netcode.PendingClient.State.PendingConnection && messageType != typeof(global::Unity.Netcode.ConnectionRequestMessage))))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						global::Unity.Netcode.NetworkLog.LogWarning(string.Format("Message received from {0}={1} before it has been accepted.", "senderId", senderId));
					}
					return false;
				}
				if (m_NetworkManager.ConnectedClients.TryGetValue(senderId, out var _) && messageType == typeof(global::Unity.Netcode.ConnectionRequestMessage))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						string transportErrorMessage2 = GetTransportErrorMessage(messageContent, m_NetworkManager);
						global::Unity.Netcode.NetworkLog.LogError("A ConnectionRequestMessage was received from a client when the connection has already been established. " + transportErrorMessage2);
					}
					return false;
				}
			}
			else
			{
				if (messageType == typeof(global::Unity.Netcode.ConnectionRequestMessage))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						string transportErrorMessage3 = GetTransportErrorMessage(messageContent, m_NetworkManager);
						global::Unity.Netcode.NetworkLog.LogError("A ConnectionRequestMessage was received from the server on the client side. " + transportErrorMessage3);
					}
					return false;
				}
				if (m_NetworkManager.IsConnectedClient && messageType == typeof(global::Unity.Netcode.ConnectionApprovedMessage) && (!m_NetworkManager.CMBServiceConnection || !m_NetworkManager.LocalClient.IsSessionOwner || !m_NetworkManager.NetworkConfig.EnableSceneManagement))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						string transportErrorMessage4 = GetTransportErrorMessage(messageContent, m_NetworkManager);
						global::Unity.Netcode.NetworkLog.LogError("A ConnectionApprovedMessage was received from the server when a connection has already been established. " + transportErrorMessage4);
					}
					return false;
				}
			}
			return !m_NetworkManager.MessageManager.StopProcessing;
		}

		private static string GetTransportErrorMessage(global::Unity.Netcode.FastBufferReader messageContent, global::Unity.Netcode.NetworkManager networkManager)
		{
			if (!(networkManager.NetworkConfig.NetworkTransport is global::Unity.Netcode.Transports.UTP.UnityTransport))
			{
				return $"NetworkTransport: {networkManager.NetworkConfig.NetworkTransport.GetType()}. Please report this to the maintainer of the transport layer.";
			}
			string transportVersion = GetTransportVersion(networkManager);
			return $"{transportVersion}. This should not happen. Further information: Message Size: {messageContent.Length}. Message Content: {(global::Unity.Netcode.NetworkMessageManager.ByteArrayToString(messageContent.ToArray(), 0, messageContent.Length))}";
		}

		private static string GetTransportVersion(global::Unity.Netcode.NetworkManager networkManager)
		{
			string text = "NetworkTransport: " + networkManager.NetworkConfig.NetworkTransport.GetType();
			if (networkManager.NetworkConfig.NetworkTransport is global::Unity.Netcode.Transports.UTP.UnityTransport unityTransport)
			{
				text = text + " UnityTransportProtocol: " + unityTransport.Protocol;
			}
			return text;
		}

		public void OnBeforeHandleMessage<T>(ref T message, ref global::Unity.Netcode.NetworkContext context) where T : global::Unity.Netcode.INetworkMessage
		{
		}

		public void OnAfterHandleMessage<T>(ref T message, ref global::Unity.Netcode.NetworkContext context) where T : global::Unity.Netcode.INetworkMessage
		{
		}
	}
}
