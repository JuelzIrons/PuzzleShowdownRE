namespace Unity.Networking.Transport
{
	internal struct SimpleConnectionLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		internal enum ConnectionState
		{
			Default = 0,
			AwaitingAccept = 1,
			PathMtuDiscovery = 2,
			PathMtuDiscoveryStageTwo = 3,
			Established = 4,
			Disconnected = 5
		}

		internal enum HandshakeType : byte
		{
			ConnectionRequest = 1,
			ConnectionAccept = 2
		}

		internal enum MessageType : byte
		{
			Data = 1,
			Disconnect = 2,
			Heartbeat = 3,
			MtuCheck = 4,
			MtuAck = 5
		}

		internal struct SimpleConnectionData
		{
			public global::Unity.Networking.Transport.ConnectionId UnderlyingConnection;

			public global::Unity.Networking.Transport.ConnectionToken Token;

			public global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState State;

			public long LastReceiveTime;

			public long LastSendTime;

			public long LastMtuSendTime;

			public int ConnectionAttempts;

			public bool IsLocal;

			public bool ReceivedMtuAck;
		}

		[global::Unity.Burst.BurstCompile]
		private struct SendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.ConnectionList Connections;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData> ConnectionsData;

			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public global::Unity.Networking.Transport.PacketsQueue DeferredSends;

			public long Time;

			public void Execute()
			{
				int count = SendQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue[i];
					if (packetProcessor.Length != 0)
					{
						global::Unity.Networking.Transport.ConnectionId connectionRef = packetProcessor.ConnectionRef;
						global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData value = ConnectionsData[connectionRef];
						global::Unity.Networking.Transport.ConnectionToken token = value.Token;
						packetProcessor.PrependToPayload(token);
						packetProcessor.PrependToPayload((byte)1);
						packetProcessor.ConnectionRef = value.UnderlyingConnection;
						value.LastSendTime = Time;
						ConnectionsData[connectionRef] = value;
					}
				}
				int count2 = DeferredSends.Count;
				for (int j = 0; j < count2; j++)
				{
					if (SendQueue.EnqueuePacket(out var packetProcessor2))
					{
						global::Unity.Networking.Transport.PacketProcessor processor = DeferredSends[j];
						global::Unity.Networking.Transport.ConnectionId connectionRef2 = processor.ConnectionRef;
						global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData value2 = ConnectionsData[connectionRef2];
						packetProcessor2.ConnectionRef = value2.UnderlyingConnection;
						packetProcessor2.EndpointRef = Connections.GetConnectionEndpoint(connectionRef2);
						packetProcessor2.SetUnsafeMetadata(0, packetProcessor2.Offset - 9);
						packetProcessor2.AppendToPayload(processor);
						value2.LastSendTime = Time;
						ConnectionsData[connectionRef2] = value2;
					}
				}
				DeferredSends.Clear();
			}
		}

		[global::Unity.Burst.BurstCompile]
		internal struct ReceiveJob<T> : global::Unity.Jobs.IJob where T : unmanaged, global::Unity.Networking.Transport.IUnderlyingConnectionList
		{
			public global::Unity.Networking.Transport.ConnectionList Connections;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData> ConnectionsData;

			public T UnderlyingConnections;

			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			public global::Unity.Networking.Transport.PacketsQueue DeferredSends;

			public global::Unity.Collections.NativeHashMap<global::Unity.Networking.Transport.ConnectionToken, global::Unity.Networking.Transport.ConnectionId> TokensHashMap;

			public global::Unity.Collections.NativeHashMap<global::Unity.Networking.Transport.ConnectionId, global::Unity.Networking.Transport.ConnectionPayload> ConnectionPayloads;

			public long Time;

			public int ConnectTimeout;

			public int DisconnectTimeout;

			public int HeartbeatTimeout;

			public int MaxConnectionAttempts;

			public int MaxMessageSize;

			public bool PerformMtuDiscovery;

			public int DownStreamPadding;

			public void Execute()
			{
				ProcessReceivedMessages();
				ProcessConnectionStates();
			}

			private void ProcessConnectionStates()
			{
				global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> nativeArray = UnderlyingConnections.QueryIncomingDisconnections(global::Unity.Collections.Allocator.Temp);
				int length = nativeArray.Length;
				for (int i = 0; i < length; i++)
				{
					global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection incomingDisconnection = nativeArray[i];
					global::Unity.Networking.Transport.ConnectionId connectionId = FindConnectionByUnderlyingConnection(ref incomingDisconnection.Connection);
					if (connectionId.IsCreated)
					{
						global::Unity.Networking.Transport.NetworkConnection.State connectionState = Connections.GetConnectionState(connectionId);
						if (connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnected && connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting)
						{
							global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData value = ConnectionsData[connectionId];
							Connections.StartDisconnecting(ref connectionId, incomingDisconnection.Reason);
							value.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Default;
							ConnectionsData[connectionId] = value;
						}
					}
				}
				length = Connections.Count;
				for (int j = 0; j < length; j++)
				{
					global::Unity.Networking.Transport.ConnectionId connectionId2 = Connections.ConnectionAt(j);
					switch (Connections.GetConnectionState(connectionId2))
					{
					case global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting:
						ProcessDisconnecting(ref connectionId2);
						break;
					case global::Unity.Networking.Transport.NetworkConnection.State.Connecting:
						ProcessConnecting(ref connectionId2);
						break;
					case global::Unity.Networking.Transport.NetworkConnection.State.Connected:
						ProcessConnected(ref connectionId2);
						break;
					}
				}
			}

			private void ProcessDisconnecting(ref global::Unity.Networking.Transport.ConnectionId connectionId)
			{
				global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData value = ConnectionsData[connectionId];
				if (value.State == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Established || value.State == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscovery || value.State == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscoveryStageTwo)
				{
					value.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Disconnected;
					EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.Disconnect, ref value.Token);
				}
				else
				{
					value.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Disconnected;
					UnderlyingConnections.Disconnect(ref value.UnderlyingConnection);
					Connections.FinishDisconnecting(ref connectionId);
					TokensHashMap.Remove(value.Token);
				}
				ConnectionsData[connectionId] = value;
			}

			private void ProcessConnecting(ref global::Unity.Networking.Transport.ConnectionId connectionId)
			{
				global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData connectionData = ConnectionsData[connectionId];
				global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState state = connectionData.State;
				switch (state)
				{
				case global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscovery:
					if (Time - connectionData.LastMtuSendTime > 300)
					{
						SendMtuDiscoveryMessages(connectionId, ref connectionData);
						connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscoveryStageTwo;
						ConnectionsData[connectionId] = connectionData;
					}
					ProcessConnected(ref connectionId);
					break;
				case global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscoveryStageTwo:
					if (Time - connectionData.LastMtuSendTime > 300)
					{
						if (!connectionData.ReceivedMtuAck)
						{
							Connections.SetConnectionPathMtu(connectionId, MaxMessageSize);
						}
						connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Established;
						if (connectionData.IsLocal)
						{
							Connections.FinishConnectingFromLocal(ref connectionId);
							ConnectionPayloads.Remove(connectionId);
						}
						else
						{
							Connections.FinishConnectingFromRemote(ref connectionId);
						}
						connectionData.LastReceiveTime = Time;
						ConnectionsData[connectionId] = connectionData;
						ProcessConnected(ref connectionId);
					}
					break;
				case global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Default:
				{
					global::Unity.Networking.Transport.NetworkEndpoint endpoint = Connections.GetConnectionEndpoint(connectionId);
					if (UnderlyingConnections.TryConnect(ref endpoint, ref connectionData.UnderlyingConnection))
					{
						connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.AwaitingAccept;
						connectionData.Token = global::Unity.Networking.Transport.Utilities.RandomHelpers.GetRandomConnectionToken();
						connectionData.LastSendTime = Time;
						connectionData.ConnectionAttempts++;
						TokensHashMap.Add(connectionData.Token, connectionId);
						EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.HandshakeType.ConnectionRequest, ref connectionData.Token);
						ConnectionsData[connectionId] = connectionData;
						return;
					}
					ConnectionsData[connectionId] = connectionData;
					break;
				}
				}
				if (Time - connectionData.LastSendTime <= ConnectTimeout)
				{
					return;
				}
				if (connectionData.ConnectionAttempts >= MaxConnectionAttempts)
				{
					Connections.StartDisconnecting(ref connectionId, global::Unity.Networking.Transport.Error.DisconnectReason.MaxConnectionAttempts);
					ProcessDisconnecting(ref connectionId);
					return;
				}
				connectionData.ConnectionAttempts++;
				connectionData.LastSendTime = Time;
				ConnectionsData[connectionId] = connectionData;
				if (state == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.AwaitingAccept)
				{
					EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.HandshakeType.ConnectionRequest, ref connectionData.Token);
				}
			}

			private void ProcessConnected(ref global::Unity.Networking.Transport.ConnectionId connectionId)
			{
				global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData simpleConnectionData = ConnectionsData[connectionId];
				if (DisconnectTimeout > 0 && Time - simpleConnectionData.LastReceiveTime > DisconnectTimeout)
				{
					Connections.StartDisconnecting(ref connectionId, global::Unity.Networking.Transport.Error.DisconnectReason.Timeout);
					ProcessDisconnecting(ref connectionId);
				}
				if (HeartbeatTimeout > 0 && Time - simpleConnectionData.LastSendTime > HeartbeatTimeout)
				{
					EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.Heartbeat, ref simpleConnectionData.Token);
				}
			}

			private unsafe void SendMtuDiscoveryMessages(global::Unity.Networking.Transport.ConnectionId connectionId, ref global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData connectionData)
			{
				connectionData.LastSendTime = Time;
				connectionData.LastMtuSendTime = Time;
				global::Unity.Networking.Transport.ConnectionPayload payload = default(global::Unity.Networking.Transport.ConnectionPayload);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(payload.Data, 1472L);
				int num = DownStreamPadding + 2 + 9;
				int num2 = 1024;
				payload.Length = num2 - num;
				EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.MtuCheck, ref connectionData.Token, payload);
				if (MaxMessageSize - 100 > num2)
				{
					payload.Length = MaxMessageSize - 100 - num;
					EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.MtuCheck, ref connectionData.Token, payload);
				}
				if (MaxMessageSize - 20 > num2)
				{
					payload.Length = MaxMessageSize - 20 - num;
					EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.MtuCheck, ref connectionData.Token, payload);
				}
				payload.Length = MaxMessageSize - num;
				EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.MtuCheck, ref connectionData.Token, payload);
			}

			private unsafe void SendMtuAck(global::Unity.Networking.Transport.ConnectionId connectionId, ushort size)
			{
				size += 9;
				size += (ushort)DownStreamPadding;
				size += 2;
				global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData simpleConnectionData = ConnectionsData[connectionId];
				simpleConnectionData.LastSendTime = Time;
				global::Unity.Networking.Transport.ConnectionPayload payload = default(global::Unity.Networking.Transport.ConnectionPayload);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(payload.Data, 1472L);
				payload.Length = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<ushort>();
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(payload.Data, &size, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<ushort>());
				EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.MtuAck, ref simpleConnectionData.Token, payload);
			}

			private void ProcessReceivedMessages()
			{
				int count = ReceiveQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = ReceiveQueue[i];
					if (packetProcessor.Length == 0)
					{
						continue;
					}
					if (ProcessHandshakeReceive(ref packetProcessor))
					{
						packetProcessor.Drop();
						continue;
					}
					if (packetProcessor.Length < 9)
					{
						packetProcessor.Drop();
						continue;
					}
					global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType messageType = (global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType)packetProcessor.RemoveFromPayloadStart<byte>();
					global::Unity.Networking.Transport.ConnectionToken token = packetProcessor.RemoveFromPayloadStart<global::Unity.Networking.Transport.ConnectionToken>();
					global::Unity.Networking.Transport.ConnectionId connectionId = FindConnectionByToken(ref token);
					if (!connectionId.IsCreated)
					{
						packetProcessor.Drop();
						continue;
					}
					global::Unity.Networking.Transport.NetworkConnection.State connectionState = Connections.GetConnectionState(connectionId);
					global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData connectionData = ConnectionsData[connectionId];
					if (connectionData.State == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Disconnected)
					{
						packetProcessor.Drop();
						continue;
					}
					if (connectionState == global::Unity.Networking.Transport.NetworkConnection.State.Connecting && connectionData.State == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.AwaitingAccept)
					{
						if (MaxMessageSize <= 1024 || !PerformMtuDiscovery)
						{
							Connections.SetConnectionPathMtu(connectionId, MaxMessageSize);
							connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Established;
							if (connectionData.IsLocal)
							{
								Connections.FinishConnectingFromLocal(ref connectionId);
								ConnectionPayloads.Remove(connectionId);
							}
							else
							{
								Connections.FinishConnectingFromRemote(ref connectionId);
							}
							ConnectionsData[connectionId] = connectionData;
						}
						else
						{
							connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscovery;
							SendMtuDiscoveryMessages(connectionId, ref connectionData);
						}
					}
					else if (connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Connected && connectionData.State != global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscovery && connectionData.State != global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscoveryStageTwo)
					{
						packetProcessor.Drop();
						continue;
					}
					switch (messageType)
					{
					case global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.MtuCheck:
					{
						if (connectionState == global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting || connectionState == global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
						{
							packetProcessor.Drop();
							break;
						}
						PreprocessMessage(ref connectionId, ref packetProcessor.EndpointRef);
						ushort size = packetProcessor.RemoveFromPayloadStart<ushort>();
						SendMtuAck(connectionId, size);
						packetProcessor.Drop();
						break;
					}
					case global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.MtuAck:
						if (connectionState == global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting || connectionState == global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
						{
							packetProcessor.Drop();
							break;
						}
						PreprocessMessage(ref connectionId, ref packetProcessor.EndpointRef);
						connectionData = ConnectionsData[connectionId];
						connectionData.ReceivedMtuAck = true;
						ConnectionsData[connectionId] = connectionData;
						if (connectionData.State == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscovery || connectionData.State == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscoveryStageTwo)
						{
							packetProcessor.RemoveFromPayloadStart<ushort>();
							ushort num = packetProcessor.RemoveFromPayloadStart<ushort>();
							int connectionPathMtu = Connections.GetConnectionPathMtu(connectionId);
							if (num > connectionPathMtu)
							{
								Connections.SetConnectionPathMtu(connectionId, num);
							}
							if (num == MaxMessageSize)
							{
								connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Established;
								if (connectionData.IsLocal)
								{
									Connections.FinishConnectingFromLocal(ref connectionId);
									ConnectionPayloads.Remove(connectionId);
								}
								else
								{
									Connections.FinishConnectingFromRemote(ref connectionId);
								}
								ConnectionsData[connectionId] = connectionData;
							}
						}
						packetProcessor.Drop();
						break;
					case global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.Disconnect:
						Connections.StartDisconnecting(ref connectionId, global::Unity.Networking.Transport.Error.DisconnectReason.ClosedByRemote);
						connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Disconnected;
						ConnectionsData[connectionId] = connectionData;
						ProcessDisconnecting(ref connectionId);
						packetProcessor.Drop();
						break;
					case global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.Data:
						PreprocessMessage(ref connectionId, ref packetProcessor.EndpointRef);
						packetProcessor.ConnectionRef = connectionId;
						break;
					case global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType.Heartbeat:
						PreprocessMessage(ref connectionId, ref packetProcessor.EndpointRef);
						packetProcessor.Drop();
						break;
					default:
						global::UnityEngine.Debug.LogWarning($"Received message with type {(byte)messageType} was not processed.");
						packetProcessor.Drop();
						break;
					}
				}
			}

			private void PreprocessMessage(ref global::Unity.Networking.Transport.ConnectionId connectionId, ref global::Unity.Networking.Transport.NetworkEndpoint endpoint)
			{
				global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData value = ConnectionsData[connectionId];
				if (value.State == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Established)
				{
					Connections.UpdateConnectionAddress(ref connectionId, ref endpoint);
				}
				value.LastReceiveTime = Time;
				ConnectionsData[connectionId] = value;
			}

			private global::Unity.Networking.Transport.ConnectionId FindConnectionByToken(ref global::Unity.Networking.Transport.ConnectionToken token)
			{
				if (TokensHashMap.TryGetValue(token, out var item))
				{
					return item;
				}
				return default(global::Unity.Networking.Transport.ConnectionId);
			}

			private global::Unity.Networking.Transport.ConnectionId FindConnectionByUnderlyingConnection(ref global::Unity.Networking.Transport.ConnectionId underlyingConnection)
			{
				int length = ConnectionsData.Length;
				for (int i = 0; i < length; i++)
				{
					if (ConnectionsData.DataAt(i).UnderlyingConnection == underlyingConnection)
					{
						return ConnectionsData.ConnectionAt(i);
					}
				}
				return default(global::Unity.Networking.Transport.ConnectionId);
			}

			private unsafe bool ProcessHandshakeReceive(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
			{
				if (packetProcessor.Length < 13)
				{
					return false;
				}
				if ((packetProcessor.GetPayloadDataRef<uint>() & 0xFFFFFF) == 5264469)
				{
					if ((byte)(packetProcessor.RemoveFromPayloadStart<uint>() >> 24) != 1)
					{
						return true;
					}
					global::Unity.Networking.Transport.SimpleConnectionLayer.HandshakeType handshakeType = (global::Unity.Networking.Transport.SimpleConnectionLayer.HandshakeType)packetProcessor.RemoveFromPayloadStart<byte>();
					global::Unity.Networking.Transport.ConnectionToken token = packetProcessor.RemoveFromPayloadStart<global::Unity.Networking.Transport.ConnectionToken>();
					global::Unity.Networking.Transport.ConnectionId connectionId = FindConnectionByToken(ref token);
					global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData connectionData = ConnectionsData[connectionId];
					switch (handshakeType)
					{
					case global::Unity.Networking.Transport.SimpleConnectionLayer.HandshakeType.ConnectionRequest:
					{
						bool flag = false;
						if (!connectionId.IsCreated)
						{
							connectionId = Connections.StartConnecting(ref packetProcessor.EndpointRef);
							connectionData = new global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData
							{
								State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscovery,
								Token = token,
								UnderlyingConnection = packetProcessor.ConnectionRef,
								IsLocal = false
							};
							TokensHashMap.Add(token, connectionId);
							flag = true;
						}
						if (packetProcessor.Length > 2)
						{
							ushort num = packetProcessor.RemoveFromPayloadStart<ushort>();
							if (num != packetProcessor.Length)
							{
								break;
							}
							global::Unity.Networking.Transport.ConnectionPayload value = new global::Unity.Networking.Transport.ConnectionPayload
							{
								Length = num
							};
							packetProcessor.CopyPayload(value.Data, num);
							ConnectionPayloads[connectionId] = value;
						}
						connectionData.LastSendTime = Time;
						ConnectionsData[connectionId] = connectionData;
						EnqueueDeferredMessage(connectionId, global::Unity.Networking.Transport.SimpleConnectionLayer.HandshakeType.ConnectionAccept, ref token);
						if (flag)
						{
							if (MaxMessageSize <= 1024 || !PerformMtuDiscovery)
							{
								Connections.SetConnectionPathMtu(connectionId, MaxMessageSize);
								connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Established;
								Connections.FinishConnectingFromRemote(ref connectionId);
								ConnectionsData[connectionId] = connectionData;
							}
							else
							{
								SendMtuDiscoveryMessages(connectionId, ref connectionData);
							}
							ConnectionsData[connectionId] = connectionData;
						}
						break;
					}
					case global::Unity.Networking.Transport.SimpleConnectionLayer.HandshakeType.ConnectionAccept:
						if (connectionId.IsCreated && connectionData.State == global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.AwaitingAccept)
						{
							connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.PathMtuDiscovery;
							connectionData.IsLocal = true;
							ConnectionsData[connectionId] = connectionData;
							if (MaxMessageSize <= 1024 || !PerformMtuDiscovery)
							{
								Connections.SetConnectionPathMtu(connectionId, MaxMessageSize);
								connectionData.State = global::Unity.Networking.Transport.SimpleConnectionLayer.ConnectionState.Established;
								Connections.FinishConnectingFromLocal(ref connectionId);
								ConnectionPayloads.Remove(connectionId);
							}
							else
							{
								SendMtuDiscoveryMessages(connectionId, ref connectionData);
							}
							ConnectionsData[connectionId] = connectionData;
							break;
						}
						return true;
					default:
						return true;
					}
					connectionData = ConnectionsData[connectionId];
					connectionData.LastReceiveTime = Time;
					ConnectionsData[connectionId] = connectionData;
					return true;
				}
				return false;
			}

			private unsafe void EnqueueDeferredMessage(global::Unity.Networking.Transport.ConnectionId connection, global::Unity.Networking.Transport.SimpleConnectionLayer.HandshakeType type, ref global::Unity.Networking.Transport.ConnectionToken token)
			{
				if (DeferredSends.EnqueuePacket(out var packetProcessor))
				{
					packetProcessor.ConnectionRef = connection;
					packetProcessor.AppendToPayload(22041685u);
					packetProcessor.AppendToPayload(type);
					packetProcessor.AppendToPayload(token);
					if (type == global::Unity.Networking.Transport.SimpleConnectionLayer.HandshakeType.ConnectionRequest && ConnectionPayloads.TryGetValue(connection, out var item) && item.Length > 0)
					{
						packetProcessor.AppendToPayload((ushort)item.Length);
						packetProcessor.AppendToPayload(item.Data, item.Length);
					}
				}
			}

			private void EnqueueDeferredMessage(global::Unity.Networking.Transport.ConnectionId connection, global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType type, ref global::Unity.Networking.Transport.ConnectionToken token)
			{
				if (DeferredSends.EnqueuePacket(out var packetProcessor))
				{
					packetProcessor.ConnectionRef = connection;
					packetProcessor.AppendToPayload(type);
					packetProcessor.AppendToPayload(token);
				}
			}

			private unsafe void EnqueueDeferredMessage(global::Unity.Networking.Transport.ConnectionId connection, global::Unity.Networking.Transport.SimpleConnectionLayer.MessageType type, ref global::Unity.Networking.Transport.ConnectionToken token, global::Unity.Networking.Transport.ConnectionPayload payload)
			{
				if (DeferredSends.EnqueuePacket(out var packetProcessor))
				{
					packetProcessor.ConnectionRef = connection;
					packetProcessor.AppendToPayload(type);
					packetProcessor.AppendToPayload(token);
					packetProcessor.AppendToPayload((ushort)payload.Length);
					packetProcessor.AppendToPayload(payload.Data, payload.Length);
				}
			}
		}

		internal const byte k_ProtocolVersion = 1;

		internal const int k_HeaderSize = 9;

		internal const int k_HandshakeSize = 13;

		internal const uint k_ProtocolSignatureAndVersion = 22041685u;

		private const int k_DeferredSendsQueueSize = 64;

		private global::Unity.Networking.Transport.ConnectionList m_ConnectionList;

		private global::Unity.Networking.Transport.ConnectionList m_UnderlyingConnectionList;

		private global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData> m_ConnectionsData;

		private global::Unity.Collections.NativeHashMap<global::Unity.Networking.Transport.ConnectionToken, global::Unity.Networking.Transport.ConnectionId> m_TokensHashMap;

		private global::Unity.Networking.Transport.PacketsQueue m_DeferredSends;

		private int m_ConnectTimeout;

		private int m_DisconnectTimeout;

		private int m_HeartbeatTimeout;

		private int m_MaxConnectionAttempts;

		private int m_MaxMessageSize;

		private bool m_PerformMtuDiscovery;

		private int m_DownStreamPacketPadding;

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			m_DownStreamPacketPadding = packetPadding;
			packetPadding += 9;
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = settings.GetNetworkConfigParameters();
			if (connectionList.IsCreated)
			{
				m_UnderlyingConnectionList = connectionList;
			}
			m_ConnectTimeout = networkConfigParameters.connectTimeoutMS;
			m_DisconnectTimeout = networkConfigParameters.disconnectTimeoutMS;
			m_HeartbeatTimeout = networkConfigParameters.heartbeatTimeoutMS;
			m_MaxConnectionAttempts = networkConfigParameters.maxConnectAttempts;
			connectionList = (m_ConnectionList = global::Unity.Networking.Transport.ConnectionList.Create());
			m_ConnectionsData = new global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData>(1, default(global::Unity.Networking.Transport.SimpleConnectionLayer.SimpleConnectionData), global::Unity.Collections.Allocator.Persistent);
			m_TokensHashMap = new global::Unity.Collections.NativeHashMap<global::Unity.Networking.Transport.ConnectionToken, global::Unity.Networking.Transport.ConnectionId>(1, global::Unity.Collections.Allocator.Persistent);
			m_DeferredSends = new global::Unity.Networking.Transport.PacketsQueue(64, networkConfigParameters.maxMessageSize);
			m_MaxMessageSize = networkConfigParameters.maxMessageSize;
			m_PerformMtuDiscovery = networkConfigParameters.performPathMtuDiscovery;
			return 0;
		}

		public void Dispose()
		{
			m_ConnectionList.Dispose();
			m_ConnectionsData.Dispose();
			m_TokensHashMap.Dispose();
			m_DeferredSends.Dispose();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			if (m_UnderlyingConnectionList.IsCreated)
			{
				global::Unity.Networking.Transport.UnderlyingConnectionList underlyingConnectionList = new global::Unity.Networking.Transport.UnderlyingConnectionList(ref m_UnderlyingConnectionList);
				return ScheduleReceive(default(global::Unity.Networking.Transport.SimpleConnectionLayer.ReceiveJob<global::Unity.Networking.Transport.UnderlyingConnectionList>), underlyingConnectionList, ref arguments, dependency);
			}
			return ScheduleReceive(default(global::Unity.Networking.Transport.SimpleConnectionLayer.ReceiveJob<global::Unity.Networking.Transport.NullUnderlyingConnectionList>), default(global::Unity.Networking.Transport.NullUnderlyingConnectionList), ref arguments, dependency);
		}

		private global::Unity.Jobs.JobHandle ScheduleReceive<T>(global::Unity.Networking.Transport.SimpleConnectionLayer.ReceiveJob<T> job, T underlyingConnectionList, ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency) where T : unmanaged, global::Unity.Networking.Transport.IUnderlyingConnectionList
		{
			job.Connections = m_ConnectionList;
			job.ConnectionsData = m_ConnectionsData;
			job.UnderlyingConnections = underlyingConnectionList;
			job.ReceiveQueue = arguments.ReceiveQueue;
			job.DeferredSends = m_DeferredSends;
			job.TokensHashMap = m_TokensHashMap;
			job.ConnectionPayloads = arguments.ConnectionPayloads;
			job.Time = arguments.Time;
			job.ConnectTimeout = m_ConnectTimeout;
			job.MaxConnectionAttempts = m_MaxConnectionAttempts;
			job.DisconnectTimeout = m_DisconnectTimeout;
			job.HeartbeatTimeout = m_HeartbeatTimeout;
			job.MaxMessageSize = m_MaxMessageSize;
			job.PerformMtuDiscovery = m_PerformMtuDiscovery;
			job.DownStreamPadding = m_DownStreamPacketPadding;
			return global::Unity.Jobs.IJobExtensions.Schedule(job, dependency);
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.SimpleConnectionLayer.SendJob
			{
				Connections = m_ConnectionList,
				ConnectionsData = m_ConnectionsData,
				SendQueue = arguments.SendQueue,
				DeferredSends = m_DeferredSends,
				Time = arguments.Time
			}, dependency);
		}
	}
}
