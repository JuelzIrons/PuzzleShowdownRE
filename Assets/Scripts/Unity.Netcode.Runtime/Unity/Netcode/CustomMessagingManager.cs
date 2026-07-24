namespace Unity.Netcode
{
	public class CustomMessagingManager
	{
		public delegate void UnnamedMessageDelegate(ulong clientId, global::Unity.Netcode.FastBufferReader reader);

		public delegate void HandleNamedMessageDelegate(ulong senderClientId, global::Unity.Netcode.FastBufferReader messagePayload);

		private readonly global::Unity.Netcode.NetworkManager m_NetworkManager;

		private global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.CustomMessagingManager.HandleNamedMessageDelegate> m_NamedMessageHandlers32 = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.CustomMessagingManager.HandleNamedMessageDelegate>();

		private global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.CustomMessagingManager.HandleNamedMessageDelegate> m_NamedMessageHandlers64 = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.CustomMessagingManager.HandleNamedMessageDelegate>();

		private global::System.Collections.Generic.Dictionary<ulong, string> m_MessageHandlerNameLookup32 = new global::System.Collections.Generic.Dictionary<ulong, string>();

		private global::System.Collections.Generic.Dictionary<ulong, string> m_MessageHandlerNameLookup64 = new global::System.Collections.Generic.Dictionary<ulong, string>();

		public event global::Unity.Netcode.CustomMessagingManager.UnnamedMessageDelegate OnUnnamedMessage;

		internal CustomMessagingManager(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_NetworkManager = networkManager;
		}

		internal void InvokeUnnamedMessage(ulong clientId, global::Unity.Netcode.FastBufferReader reader, int serializedHeaderSize)
		{
			if (this.OnUnnamedMessage != null)
			{
				int position = reader.Position;
				global::System.Delegate[] invocationList = this.OnUnnamedMessage.GetInvocationList();
				foreach (global::System.Delegate obj in invocationList)
				{
					reader.Seek(position);
					((global::Unity.Netcode.CustomMessagingManager.UnnamedMessageDelegate)obj)(clientId, reader);
				}
			}
			m_NetworkManager.NetworkMetrics.TrackUnnamedMessageReceived(clientId, reader.Length + serializedHeaderSize);
		}

		public void SendUnnamedMessageToAll(global::Unity.Netcode.FastBufferWriter messageBuffer, global::Unity.Netcode.NetworkDelivery networkDelivery = global::Unity.Netcode.NetworkDelivery.ReliableSequenced)
		{
			SendUnnamedMessage(m_NetworkManager.ConnectedClientsIds, messageBuffer, networkDelivery);
		}

		public void SendUnnamedMessage(global::System.Collections.Generic.IReadOnlyList<ulong> clientIds, global::Unity.Netcode.FastBufferWriter messageBuffer, global::Unity.Netcode.NetworkDelivery networkDelivery = global::Unity.Netcode.NetworkDelivery.ReliableSequenced)
		{
			if (clientIds == null)
			{
				throw new global::System.ArgumentNullException("clientIds", "You must pass in a valid clientId List!");
			}
			if (!m_NetworkManager.DistributedAuthorityMode && !m_NetworkManager.IsServer)
			{
				if (clientIds.Count > 1 || (clientIds.Count == 1 && clientIds[0] != 0L))
				{
					global::UnityEngine.Debug.LogError("Clients cannot send unnamed messages to other clients!");
					return;
				}
				if (clientIds.Count == 1)
				{
					SendUnnamedMessage(clientIds[0], messageBuffer, networkDelivery);
				}
			}
			else if (m_NetworkManager.DistributedAuthorityMode && !m_NetworkManager.DAHost && clientIds.Count > 1)
			{
				global::UnityEngine.Debug.LogError("Sending an unnamed message to multiple clients is not yet supported in distributed authority.");
				return;
			}
			if (clientIds.Count == 0)
			{
				global::UnityEngine.Debug.LogError("clientIds is empty! No clients to send to.");
				return;
			}
			ValidateMessageSize(messageBuffer, networkDelivery, isNamed: false);
			if (m_NetworkManager.IsHost)
			{
				for (int i = 0; i < clientIds.Count; i++)
				{
					if (clientIds[i] == m_NetworkManager.LocalClientId)
					{
						InvokeUnnamedMessage(m_NetworkManager.LocalClientId, new global::Unity.Netcode.FastBufferReader(messageBuffer, global::Unity.Collections.Allocator.None), 0);
					}
				}
			}
			global::Unity.Netcode.UnnamedMessage message = new global::Unity.Netcode.UnnamedMessage
			{
				SendData = messageBuffer
			};
			int num = m_NetworkManager.ConnectionManager.SendMessage(ref message, networkDelivery, in clientIds);
			if (num != 0)
			{
				m_NetworkManager.NetworkMetrics.TrackUnnamedMessageSent(clientIds, num);
			}
		}

		public void SendUnnamedMessage(ulong clientId, global::Unity.Netcode.FastBufferWriter messageBuffer, global::Unity.Netcode.NetworkDelivery networkDelivery = global::Unity.Netcode.NetworkDelivery.ReliableSequenced)
		{
			ValidateMessageSize(messageBuffer, networkDelivery, isNamed: false);
			if (m_NetworkManager.IsHost && clientId == m_NetworkManager.LocalClientId)
			{
				InvokeUnnamedMessage(m_NetworkManager.LocalClientId, new global::Unity.Netcode.FastBufferReader(messageBuffer, global::Unity.Collections.Allocator.None), 0);
				return;
			}
			global::Unity.Netcode.UnnamedMessage message = new global::Unity.Netcode.UnnamedMessage
			{
				SendData = messageBuffer
			};
			int num = m_NetworkManager.ConnectionManager.SendMessage(ref message, networkDelivery, clientId);
			if (num != 0)
			{
				m_NetworkManager.NetworkMetrics.TrackUnnamedMessageSent(clientId, num);
			}
		}

		internal void InvokeNamedMessage(ulong hash, ulong sender, global::Unity.Netcode.FastBufferReader reader, int serializedHeaderSize)
		{
			int num = reader.Length + serializedHeaderSize;
			if (m_NetworkManager == null)
			{
				if (m_NamedMessageHandlers32.TryGetValue(hash, out var value))
				{
					string messageName = m_MessageHandlerNameLookup32[hash];
					value(sender, reader);
					m_NetworkManager.NetworkMetrics.TrackNamedMessageReceived(sender, messageName, num);
				}
				if (m_NamedMessageHandlers64.TryGetValue(hash, out var value2))
				{
					string messageName2 = m_MessageHandlerNameLookup64[hash];
					value2(sender, reader);
					m_NetworkManager.NetworkMetrics.TrackNamedMessageReceived(sender, messageName2, num);
				}
				return;
			}
			switch (m_NetworkManager.NetworkConfig.RpcHashSize)
			{
			case global::Unity.Netcode.HashSize.VarIntFourBytes:
			{
				if (m_NamedMessageHandlers32.TryGetValue(hash, out var value4))
				{
					string messageName4 = m_MessageHandlerNameLookup32[hash];
					value4(sender, reader);
					m_NetworkManager.NetworkMetrics.TrackNamedMessageReceived(sender, messageName4, num);
				}
				break;
			}
			case global::Unity.Netcode.HashSize.VarIntEightBytes:
			{
				if (m_NamedMessageHandlers64.TryGetValue(hash, out var value3))
				{
					string messageName3 = m_MessageHandlerNameLookup64[hash];
					value3(sender, reader);
					m_NetworkManager.NetworkMetrics.TrackNamedMessageReceived(sender, messageName3, num);
				}
				break;
			}
			}
		}

		public void RegisterNamedMessageHandler(string name, global::Unity.Netcode.CustomMessagingManager.HandleNamedMessageDelegate callback)
		{
			if (string.IsNullOrEmpty(name))
			{
				if (m_NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::UnityEngine.Debug.LogError("[RegisterNamedMessageHandler] Cannot register a named message of type null or empty!");
				}
				return;
			}
			uint num = name.Hash32();
			ulong key = name.Hash64();
			if (m_NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer && (m_MessageHandlerNameLookup32.ContainsKey(num) || m_MessageHandlerNameLookup64.ContainsKey(key)))
			{
				global::UnityEngine.Debug.LogWarning("Registering " + name + " named message over existing registration! Your previous registration's callback is being overwritten!");
			}
			m_NamedMessageHandlers32[num] = callback;
			m_NamedMessageHandlers64[key] = callback;
			m_MessageHandlerNameLookup32[num] = name;
			m_MessageHandlerNameLookup64[key] = name;
		}

		public void UnregisterNamedMessageHandler(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				if (m_NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::UnityEngine.Debug.LogError("[UnregisterNamedMessageHandler] Cannot unregister a named message of type null or empty!");
				}
				return;
			}
			uint num = name.Hash32();
			ulong key = name.Hash64();
			m_NamedMessageHandlers32.Remove(num);
			m_NamedMessageHandlers64.Remove(key);
			m_MessageHandlerNameLookup32.Remove(num);
			m_MessageHandlerNameLookup64.Remove(key);
		}

		public void SendNamedMessageToAll(string messageName, global::Unity.Netcode.FastBufferWriter messageStream, global::Unity.Netcode.NetworkDelivery networkDelivery = global::Unity.Netcode.NetworkDelivery.ReliableSequenced)
		{
			SendNamedMessage(messageName, m_NetworkManager.ConnectedClientsIds, messageStream, networkDelivery);
		}

		public void SendNamedMessage(string messageName, ulong clientId, global::Unity.Netcode.FastBufferWriter messageStream, global::Unity.Netcode.NetworkDelivery networkDelivery = global::Unity.Netcode.NetworkDelivery.ReliableSequenced)
		{
			ValidateMessageSize(messageStream, networkDelivery, isNamed: true);
			ulong hash = 0uL;
			switch (m_NetworkManager.NetworkConfig.RpcHashSize)
			{
			case global::Unity.Netcode.HashSize.VarIntFourBytes:
				hash = messageName.Hash32();
				break;
			case global::Unity.Netcode.HashSize.VarIntEightBytes:
				hash = messageName.Hash64();
				break;
			}
			if (m_NetworkManager.IsHost && clientId == m_NetworkManager.LocalClientId)
			{
				InvokeNamedMessage(hash, m_NetworkManager.LocalClientId, new global::Unity.Netcode.FastBufferReader(messageStream, global::Unity.Collections.Allocator.None), 0);
				return;
			}
			global::Unity.Netcode.NamedMessage message = new global::Unity.Netcode.NamedMessage
			{
				Hash = hash,
				SendData = messageStream
			};
			int num = m_NetworkManager.ConnectionManager.SendMessage(ref message, networkDelivery, clientId);
			if (num != 0)
			{
				m_NetworkManager.NetworkMetrics.TrackNamedMessageSent(clientId, messageName, num);
			}
		}

		public void SendNamedMessage(string messageName, global::System.Collections.Generic.IReadOnlyList<ulong> clientIds, global::Unity.Netcode.FastBufferWriter messageStream, global::Unity.Netcode.NetworkDelivery networkDelivery = global::Unity.Netcode.NetworkDelivery.ReliableSequenced)
		{
			if (clientIds == null)
			{
				throw new global::System.ArgumentNullException("clientIds", "Client list is null! You must pass in a valid clientId list to send a named message.");
			}
			if (!m_NetworkManager.DistributedAuthorityMode && !m_NetworkManager.IsServer)
			{
				if (clientIds.Count > 1 || (clientIds.Count == 1 && clientIds[0] != 0L))
				{
					global::UnityEngine.Debug.LogError("Clients cannot send named messages to other clients!");
					return;
				}
				if (clientIds.Count == 1)
				{
					SendNamedMessage(messageName, clientIds[0], messageStream, networkDelivery);
					return;
				}
			}
			else if (m_NetworkManager.DistributedAuthorityMode && !m_NetworkManager.DAHost && clientIds.Count > 1)
			{
				global::UnityEngine.Debug.LogError("Sending a named message to multiple clients is not yet supported in distributed authority.");
				return;
			}
			if (clientIds.Count == 0)
			{
				global::UnityEngine.Debug.LogError("clientIds is empty! No clients to send the named message " + messageName + " to!");
				return;
			}
			ValidateMessageSize(messageStream, networkDelivery, isNamed: true);
			ulong hash = 0uL;
			switch (m_NetworkManager.NetworkConfig.RpcHashSize)
			{
			case global::Unity.Netcode.HashSize.VarIntFourBytes:
				hash = messageName.Hash32();
				break;
			case global::Unity.Netcode.HashSize.VarIntEightBytes:
				hash = messageName.Hash64();
				break;
			}
			if (m_NetworkManager.IsHost)
			{
				for (int i = 0; i < clientIds.Count; i++)
				{
					if (clientIds[i] == m_NetworkManager.LocalClientId)
					{
						InvokeNamedMessage(hash, m_NetworkManager.LocalClientId, new global::Unity.Netcode.FastBufferReader(messageStream, global::Unity.Collections.Allocator.None), 0);
					}
				}
			}
			global::Unity.Netcode.NamedMessage message = new global::Unity.Netcode.NamedMessage
			{
				Hash = hash,
				SendData = messageStream
			};
			int num = m_NetworkManager.ConnectionManager.SendMessage(ref message, networkDelivery, in clientIds);
			if (num != 0)
			{
				m_NetworkManager.NetworkMetrics.TrackNamedMessageSent(clientIds, messageName, num);
			}
		}

		private void ValidateMessageSize(global::Unity.Netcode.FastBufferWriter messageStream, global::Unity.Netcode.NetworkDelivery networkDelivery, bool isNamed)
		{
		}
	}
}
