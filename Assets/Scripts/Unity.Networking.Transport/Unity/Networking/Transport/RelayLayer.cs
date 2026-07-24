namespace Unity.Networking.Transport
{
	internal struct RelayLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		internal struct ProtocolData
		{
			public global::Unity.Networking.Transport.Relay.RelayConnectionStatus ConnectionStatus;

			public global::Unity.Networking.Transport.Relay.RelayServerData ServerData;

			public global::Unity.Networking.Transport.ConnectionId UnderlyingConnection;

			public long LastSentTime;

			public long LastReceiveTime;

			public long ConnectStartTime;

			public int ConnectAttemptTimeout;

			public int MaxConnectTime;

			public int HeartbeatTime;
		}

		internal struct ConnectionData
		{
			public long LastConnectAttempt;
		}

		[global::Unity.Burst.BurstCompile]
		private struct SendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.ConnectionList Connections;

			public global::Unity.Networking.Transport.PacketsQueue DeferredSendQueue;

			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.RelayLayer.ProtocolData> RelayProtocolData;

			public long Time;

			public void Execute()
			{
				global::Unity.Networking.Transport.Relay.RelayAllocationId fromAllocationId = RelayProtocolData.Value.ServerData.AllocationId;
				global::Unity.Networking.Transport.ConnectionId underlyingConnection = RelayProtocolData.Value.UnderlyingConnection;
				global::Unity.Networking.Transport.NetworkEndpoint endpoint = RelayProtocolData.Value.ServerData.Endpoint;
				int count = SendQueue.Count;
				bool flag = DeferredSendQueue.Count > 0;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue[i];
					if (packetProcessor.Length != 0)
					{
						global::Unity.Networking.Transport.ConnectionId connectionRef = packetProcessor.ConnectionRef;
						global::Unity.Networking.Transport.NetworkEndpoint address = Connections.GetConnectionEndpoint(connectionRef);
						global::Unity.Networking.Transport.Relay.RelayMessageRelay.Write(ref packetProcessor, ref fromAllocationId, ref global::Unity.Networking.Transport.Relay.RelayAllocationIdExtensions.AsRelayAllocationId(ref address), (ushort)packetProcessor.Length);
						packetProcessor.ConnectionRef = underlyingConnection;
						packetProcessor.EndpointRef = endpoint;
						flag = true;
					}
				}
				SendQueue.EnqueuePackets(ref DeferredSendQueue);
				DeferredSendQueue.Clear();
				if (flag)
				{
					global::Unity.Networking.Transport.RelayLayer.ProtocolData value = RelayProtocolData.Value;
					value.LastSentTime = Time;
					RelayProtocolData.Value = value;
				}
			}
		}

		[global::Unity.Burst.BurstCompile]
		internal struct ReceiveJob<T> : global::Unity.Jobs.IJob where T : unmanaged, global::Unity.Networking.Transport.IUnderlyingConnectionList
		{
			public global::Unity.Networking.Transport.ConnectionList Connections;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.RelayLayer.ConnectionData> ConnectionsData;

			public global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.NetworkEndpoint, global::Unity.Networking.Transport.ConnectionId> EndpointsHashmap;

			public global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.RelayLayer.ProtocolData> RelayProtocolData;

			public T UnderlyingConnections;

			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			public global::Unity.Networking.Transport.PacketsQueue DeferredSendQueue;

			public long Time;

			public void Execute()
			{
				ProcessReceivedMessages();
				ProcessRelayServerConnection();
				ProcessConnectionStates();
			}

			private void ProcessReceivedMessages()
			{
				global::Unity.Networking.Transport.RelayLayer.ProtocolData value = RelayProtocolData.Value;
				int count = ReceiveQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = ReceiveQueue[i];
					if (packetProcessor.Length < 4)
					{
						packetProcessor.Drop();
						continue;
					}
					global::Unity.Networking.Transport.Relay.RelayMessageHeader payloadDataRef = packetProcessor.GetPayloadDataRef<global::Unity.Networking.Transport.Relay.RelayMessageHeader>();
					if (!payloadDataRef.IsValid())
					{
						packetProcessor.Drop();
						continue;
					}
					switch (payloadDataRef.Type)
					{
					case global::Unity.Networking.Transport.Relay.RelayMessageType.BindReceived:
						packetProcessor.Drop();
						value.ConnectionStatus = global::Unity.Networking.Transport.Relay.RelayConnectionStatus.Established;
						break;
					case global::Unity.Networking.Transport.Relay.RelayMessageType.Accepted:
					{
						global::Unity.Networking.Transport.Relay.RelayMessageAccepted payloadDataRef3 = packetProcessor.GetPayloadDataRef<global::Unity.Networking.Transport.Relay.RelayMessageAccepted>();
						if (Connections.Count == 1)
						{
							global::Unity.Networking.Transport.ConnectionId connectionId2 = Connections.ConnectionAt(0);
							if (Connections.GetConnectionState(connectionId2) == global::Unity.Networking.Transport.NetworkConnection.State.Connecting)
							{
								global::Unity.Networking.Transport.NetworkEndpoint address2 = payloadDataRef3.FromAllocationId.ToNetworkEndpoint();
								Connections.FinishConnectingFromLocal(ref connectionId2);
								Connections.UpdateConnectionAddress(ref connectionId2, ref address2);
								EndpointsHashmap.Add(address2, connectionId2);
							}
						}
						packetProcessor.Drop();
						break;
					}
					case global::Unity.Networking.Transport.Relay.RelayMessageType.Rejected:
						global::UnityEngine.Debug.LogError("Relay allocation maximum connected players limit reached.");
						if (Connections.Count == 1)
						{
							global::Unity.Networking.Transport.ConnectionId connectionId = Connections.ConnectionAt(0);
							if (Connections.GetConnectionState(connectionId) == global::Unity.Networking.Transport.NetworkConnection.State.Connecting)
							{
								Connections.StartDisconnecting(ref connectionId, global::Unity.Networking.Transport.Error.DisconnectReason.ClosedByRemote);
								Connections.FinishDisconnecting(ref connectionId);
								value.ConnectionStatus = global::Unity.Networking.Transport.Relay.RelayConnectionStatus.AllocationInvalid;
							}
						}
						packetProcessor.Drop();
						break;
					case global::Unity.Networking.Transport.Relay.RelayMessageType.Disconnect:
					{
						global::Unity.Networking.Transport.Relay.RelayMessageDisconnect payloadDataRef4 = packetProcessor.GetPayloadDataRef<global::Unity.Networking.Transport.Relay.RelayMessageDisconnect>();
						if (payloadDataRef4.ToAllocationId == RelayProtocolData.Value.ServerData.AllocationId)
						{
							global::Unity.Networking.Transport.NetworkEndpoint key = payloadDataRef4.FromAllocationId.ToNetworkEndpoint();
							if (EndpointsHashmap.TryGetValue(key, out var item2))
							{
								Connections.StartDisconnecting(ref item2, global::Unity.Networking.Transport.Error.DisconnectReason.ProtocolError);
								Connections.FinishDisconnecting(ref item2);
								EndpointsHashmap.Remove(key);
							}
						}
						packetProcessor.Drop();
						break;
					}
					case global::Unity.Networking.Transport.Relay.RelayMessageType.Relay:
					{
						global::Unity.Networking.Transport.Relay.RelayMessageRelay relayMessageRelay = packetProcessor.RemoveFromPayloadStart<global::Unity.Networking.Transport.Relay.RelayMessageRelay>();
						if (relayMessageRelay.DataLength != packetProcessor.Length)
						{
							packetProcessor.Drop();
							break;
						}
						if (relayMessageRelay.ToAllocationId != RelayProtocolData.Value.ServerData.AllocationId)
						{
							packetProcessor.Drop();
							break;
						}
						global::Unity.Networking.Transport.NetworkEndpoint address = relayMessageRelay.FromAllocationId.ToNetworkEndpoint();
						if (!EndpointsHashmap.TryGetValue(address, out var item))
						{
							item = Connections.StartConnecting(ref address);
							Connections.FinishConnectingFromRemote(ref item);
							EndpointsHashmap.TryAdd(address, item);
						}
						packetProcessor.EndpointRef = address;
						packetProcessor.ConnectionRef = item;
						break;
					}
					case global::Unity.Networking.Transport.Relay.RelayMessageType.Error:
					{
						global::Unity.Networking.Transport.Relay.RelayMessageError payloadDataRef2 = packetProcessor.GetPayloadDataRef<global::Unity.Networking.Transport.Relay.RelayMessageError>();
						payloadDataRef2.LogError();
						if (payloadDataRef2.ErrorCode == 3)
						{
							value.ServerData.IncrementNonce();
							if (DeferredSendQueue.EnqueuePacket(out var packetProcessor2))
							{
								global::Unity.Networking.Transport.Relay.RelayMessageBind.Write(ref packetProcessor2, ref value.ServerData);
								packetProcessor2.ConnectionRef = value.UnderlyingConnection;
								packetProcessor2.EndpointRef = value.ServerData.Endpoint;
							}
						}
						else if (payloadDataRef2.ErrorCode == 1 || payloadDataRef2.ErrorCode == 4)
						{
							value.ConnectionStatus = global::Unity.Networking.Transport.Relay.RelayConnectionStatus.AllocationInvalid;
						}
						packetProcessor.Drop();
						break;
					}
					default:
						packetProcessor.Drop();
						break;
					}
					value.LastReceiveTime = Time;
				}
				RelayProtocolData.Value = value;
			}

			private void ProcessRelayServerConnection()
			{
				global::Unity.Networking.Transport.RelayLayer.ProtocolData value = RelayProtocolData.Value;
				if (value.ConnectionStatus == global::Unity.Networking.Transport.Relay.RelayConnectionStatus.NotEstablished)
				{
					if (value.ConnectStartTime == 0L)
					{
						value.ConnectStartTime = Time;
					}
					bool num = value.UnderlyingConnection == default(global::Unity.Networking.Transport.ConnectionId);
					bool flag = value.LastSentTime == 0;
					bool flag2 = Time - value.LastSentTime > value.ConnectAttemptTimeout;
					if (num && (flag || flag2))
					{
						value.LastSentTime = Time;
					}
					if ((!num || flag || flag2) && UnderlyingConnections.TryConnect(ref value.ServerData.Endpoint, ref value.UnderlyingConnection))
					{
						if (Time - value.ConnectStartTime > value.MaxConnectTime)
						{
							global::UnityEngine.Debug.LogError("Failed to establish connection with the Relay server (server didn't answer any BIND message).");
							value.ConnectionStatus = global::Unity.Networking.Transport.Relay.RelayConnectionStatus.AllocationInvalid;
						}
						else if (flag || flag2)
						{
							value.LastSentTime = Time;
							if (DeferredSendQueue.EnqueuePacket(out var packetProcessor))
							{
								global::Unity.Networking.Transport.Relay.RelayMessageBind.Write(ref packetProcessor, ref value.ServerData);
								packetProcessor.ConnectionRef = value.UnderlyingConnection;
								packetProcessor.EndpointRef = value.ServerData.Endpoint;
								value.ServerData.IncrementNonce();
							}
						}
					}
				}
				if (value.ConnectionStatus == global::Unity.Networking.Transport.Relay.RelayConnectionStatus.Established)
				{
					int heartbeatTime = value.HeartbeatTime;
					if (heartbeatTime > 0 && Time - value.LastSentTime >= heartbeatTime && DeferredSendQueue.EnqueuePacket(out var packetProcessor2))
					{
						global::Unity.Networking.Transport.Relay.RelayMessagePing.Write(ref packetProcessor2, ref value.ServerData.AllocationId);
						packetProcessor2.ConnectionRef = value.UnderlyingConnection;
						packetProcessor2.EndpointRef = value.ServerData.Endpoint;
					}
					int num2 = heartbeatTime * 3;
					if (heartbeatTime > 0 && value.LastReceiveTime > 0 && Time - value.LastReceiveTime >= num2 && DeferredSendQueue.EnqueuePacket(out var packetProcessor3))
					{
						global::Unity.Networking.Transport.Relay.RelayMessageBind.Write(ref packetProcessor3, ref value.ServerData);
						packetProcessor3.ConnectionRef = value.UnderlyingConnection;
						packetProcessor3.EndpointRef = value.ServerData.Endpoint;
						value.LastReceiveTime = Time;
					}
				}
				if (UnderlyingConnectionFailed(ref value.UnderlyingConnection))
				{
					if (Time - value.ConnectStartTime < value.MaxConnectTime)
					{
						value.UnderlyingConnection = default(global::Unity.Networking.Transport.ConnectionId);
					}
					else
					{
						global::UnityEngine.Debug.LogError("Failed to establish connection with the Relay server.");
						value.ConnectionStatus = global::Unity.Networking.Transport.Relay.RelayConnectionStatus.AllocationInvalid;
					}
				}
				RelayProtocolData.Value = value;
			}

			private void ProcessConnectionStates()
			{
				int count = Connections.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connectionId = Connections.ConnectionAt(i);
					switch (Connections.GetConnectionState(connectionId))
					{
					case global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting:
						ProcessDisconnecting(ref connectionId);
						break;
					case global::Unity.Networking.Transport.NetworkConnection.State.Connecting:
						ProcessConnecting(ref connectionId);
						break;
					}
				}
			}

			private void ProcessDisconnecting(ref global::Unity.Networking.Transport.ConnectionId connectionId)
			{
				_ = ConnectionsData[connectionId];
				global::Unity.Networking.Transport.RelayLayer.ProtocolData value = RelayProtocolData.Value;
				if (value.ConnectionStatus == global::Unity.Networking.Transport.Relay.RelayConnectionStatus.Established && DeferredSendQueue.EnqueuePacket(out var packetProcessor))
				{
					global::Unity.Networking.Transport.NetworkEndpoint address = Connections.GetConnectionEndpoint(connectionId);
					global::Unity.Networking.Transport.Relay.RelayMessageDisconnect.Write(ref packetProcessor, ref value.ServerData.AllocationId, ref global::Unity.Networking.Transport.Relay.RelayAllocationIdExtensions.AsRelayAllocationId(ref address));
					packetProcessor.ConnectionRef = value.UnderlyingConnection;
					packetProcessor.EndpointRef = value.ServerData.Endpoint;
				}
				Connections.FinishDisconnecting(ref connectionId);
				ConnectionsData.ClearData(ref connectionId);
				EndpointsHashmap.Remove(Connections.GetConnectionEndpoint(connectionId));
			}

			private void ProcessConnecting(ref global::Unity.Networking.Transport.ConnectionId connectionId)
			{
				if (Connections.Count > 1)
				{
					Connections.StartDisconnecting(ref connectionId, global::Unity.Networking.Transport.Error.DisconnectReason.ProtocolError);
					Connections.FinishDisconnecting(ref connectionId);
					return;
				}
				global::Unity.Networking.Transport.RelayLayer.ProtocolData value = RelayProtocolData.Value;
				if (value.ConnectionStatus != global::Unity.Networking.Transport.Relay.RelayConnectionStatus.Established)
				{
					return;
				}
				global::Unity.Networking.Transport.RelayLayer.ConnectionData value2 = ConnectionsData[connectionId];
				if (Time - value2.LastConnectAttempt >= RelayProtocolData.Value.ConnectAttemptTimeout)
				{
					value2.LastConnectAttempt = Time;
					ConnectionsData[connectionId] = value2;
					if (DeferredSendQueue.EnqueuePacket(out var packetProcessor))
					{
						global::Unity.Networking.Transport.Relay.RelayMessageConnectRequest.Write(ref packetProcessor, ref value.ServerData.AllocationId, ref value.ServerData.HostConnectionData);
						packetProcessor.ConnectionRef = value.UnderlyingConnection;
						packetProcessor.EndpointRef = value.ServerData.Endpoint;
					}
				}
			}

			private bool UnderlyingConnectionFailed(ref global::Unity.Networking.Transport.ConnectionId underlyingConnection)
			{
				global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> nativeArray = UnderlyingConnections.QueryIncomingDisconnections(global::Unity.Collections.Allocator.Temp);
				int length = nativeArray.Length;
				for (int i = 0; i < length; i++)
				{
					if (nativeArray[i].Connection == underlyingConnection)
					{
						return true;
					}
				}
				return false;
			}
		}

		private const int k_DeferredSendQueueSize = 10;

		private global::Unity.Networking.Transport.ConnectionList m_Connections;

		private global::Unity.Networking.Transport.ConnectionList m_UnderlyingConnections;

		private global::Unity.Networking.Transport.PacketsQueue m_DeferredSendQueue;

		private global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.RelayLayer.ProtocolData> m_ProtocolData;

		private global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.RelayLayer.ConnectionData> m_ConnectionsData;

		private global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.NetworkEndpoint, global::Unity.Networking.Transport.ConnectionId> m_EndpointsHashMap;

		public global::Unity.Networking.Transport.Relay.RelayConnectionStatus ConnectionStatus => m_ProtocolData.Value.ConnectionStatus;

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = settings.GetNetworkConfigParameters();
			global::Unity.Networking.Transport.RelayLayer.ProtocolData value = new global::Unity.Networking.Transport.RelayLayer.ProtocolData
			{
				ConnectionStatus = global::Unity.Networking.Transport.Relay.RelayConnectionStatus.NotEstablished,
				ConnectAttemptTimeout = networkConfigParameters.connectTimeoutMS,
				MaxConnectTime = networkConfigParameters.maxConnectAttempts * networkConfigParameters.connectTimeoutMS,
				HeartbeatTime = global::Unity.Networking.Transport.Relay.RelayParameterExtensions.GetRelayParameters(ref settings).RelayConnectionTimeMS,
				ServerData = global::Unity.Networking.Transport.Relay.RelayParameterExtensions.GetRelayParameters(ref settings).ServerData
			};
			m_ProtocolData = new global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.RelayLayer.ProtocolData>(value, global::Unity.Collections.Allocator.Persistent);
			m_DeferredSendQueue = new global::Unity.Networking.Transport.PacketsQueue(10, networkConfigParameters.maxMessageSize);
			m_ConnectionsData = new global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.RelayLayer.ConnectionData>(1, default(global::Unity.Networking.Transport.RelayLayer.ConnectionData), global::Unity.Collections.Allocator.Persistent);
			m_EndpointsHashMap = new global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.NetworkEndpoint, global::Unity.Networking.Transport.ConnectionId>(1, global::Unity.Collections.Allocator.Persistent);
			m_DeferredSendQueue.SetDefaultDataOffset(packetPadding);
			if (connectionList.IsCreated)
			{
				m_UnderlyingConnections = connectionList;
			}
			connectionList = (m_Connections = global::Unity.Networking.Transport.ConnectionList.Create());
			packetPadding += 38;
			return 0;
		}

		public void Dispose()
		{
			m_Connections.Dispose();
			m_ProtocolData.Dispose();
			m_ConnectionsData.Dispose();
			m_EndpointsHashMap.Dispose();
			m_DeferredSendQueue.Dispose();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			if (m_UnderlyingConnections.IsCreated)
			{
				return ScheduleReceive(default(global::Unity.Networking.Transport.RelayLayer.ReceiveJob<global::Unity.Networking.Transport.UnderlyingConnectionList>), new global::Unity.Networking.Transport.UnderlyingConnectionList(ref m_UnderlyingConnections), ref arguments, dependency);
			}
			return ScheduleReceive(default(global::Unity.Networking.Transport.RelayLayer.ReceiveJob<global::Unity.Networking.Transport.NullUnderlyingConnectionList>), default(global::Unity.Networking.Transport.NullUnderlyingConnectionList), ref arguments, dependency);
		}

		private global::Unity.Jobs.JobHandle ScheduleReceive<T>(global::Unity.Networking.Transport.RelayLayer.ReceiveJob<T> job, T underlyingConnectionList, ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency) where T : unmanaged, global::Unity.Networking.Transport.IUnderlyingConnectionList
		{
			job.Connections = m_Connections;
			job.ConnectionsData = m_ConnectionsData;
			job.EndpointsHashmap = m_EndpointsHashMap;
			job.ReceiveQueue = arguments.ReceiveQueue;
			job.UnderlyingConnections = underlyingConnectionList;
			job.DeferredSendQueue = m_DeferredSendQueue;
			job.RelayProtocolData = m_ProtocolData;
			job.Time = arguments.Time;
			return global::Unity.Jobs.IJobExtensions.Schedule(job, dependency);
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.RelayLayer.SendJob
			{
				Connections = m_Connections,
				SendQueue = arguments.SendQueue,
				DeferredSendQueue = m_DeferredSendQueue,
				RelayProtocolData = m_ProtocolData,
				Time = arguments.Time
			}, dependency);
		}
	}
}
