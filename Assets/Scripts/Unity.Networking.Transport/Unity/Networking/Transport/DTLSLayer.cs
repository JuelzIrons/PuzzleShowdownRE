namespace Unity.Networking.Transport
{
	internal struct DTLSLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		private struct DTLSConnectionData
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.TLS.LowLevel.Binding.unitytls_client* UnityTLSClientPtr;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.TLS.LowLevel.Binding.unitytls_client* ReconnectionClientPtr;

			public long LastHandshakeUpdate;

			public long LastReceive;
		}

		[global::Unity.Burst.BurstCompile]
		private struct ReceiveJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.ConnectionList Connections;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData> ConnectionsData;

			public global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.NetworkEndpoint, global::Unity.Networking.Transport.ConnectionId> EndpointToConnection;

			public global::Unity.Networking.Transport.PacketsQueue DeferredSends;

			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			public long Time;

			public long HalfOpenDisconnectTimeout;

			public long ReconnectionTimeout;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.TLS.LowLevel.Binding.unitytls_client_config* UnityTLSConfig;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext* UnityTLSCallbackContext;

			public unsafe void Execute()
			{
				UnityTLSCallbackContext->ReceivedPacket = default(global::Unity.Networking.Transport.PacketProcessor);
				UnityTLSCallbackContext->SendQueue = DeferredSends;
				UnityTLSCallbackContext->SendQueueIndex = -1;
				UnityTLSCallbackContext->PacketPadding = 0;
				ProcessReceivedMessages();
				ProcessConnectionList();
			}

			private unsafe void ProcessReceivedMessages()
			{
				int count = ReceiveQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = ReceiveQueue[i];
					if (packetProcessor.Length == 0)
					{
						continue;
					}
					UnityTLSCallbackContext->ReceivedPacket = packetProcessor;
					if (global::Unity.Networking.Transport.TLS.DTLSUtilities.IsClientHello(ref packetProcessor))
					{
						ProcessClientHello(packetProcessor.EndpointRef);
					}
					if (!EndpointToConnection.TryGetValue(packetProcessor.EndpointRef, out var item))
					{
						packetProcessor.Drop();
						continue;
					}
					UpdateLastReceiveTime(item);
					HandlePossibleReconnection(item, ref packetProcessor);
					packetProcessor.ConnectionRef = item;
					uint num = global::Unity.TLS.LowLevel.Binding.unitytls_client_get_state(ConnectionsData[item].UnityTLSClientPtr);
					if (num == 1 || num == 2)
					{
						ProcessHandshakeMessage(ref packetProcessor);
						packetProcessor.Drop();
					}
					else
					{
						ProcessDataMessage(ref packetProcessor);
					}
				}
			}

			private unsafe void ProcessClientHello(global::Unity.Networking.Transport.NetworkEndpoint fromEndpoint)
			{
				if (!EndpointToConnection.ContainsKey(fromEndpoint))
				{
					global::Unity.Networking.Transport.ConnectionId connectionId = Connections.StartConnecting(ref fromEndpoint);
					EndpointToConnection.Add(fromEndpoint, connectionId);
					global::Unity.TLS.LowLevel.Binding.unitytls_client* ptr = global::Unity.TLS.LowLevel.Binding.unitytls_client_create(1u, UnityTLSConfig);
					global::Unity.TLS.LowLevel.Binding.unitytls_client_init(ptr);
					ConnectionsData[connectionId] = new global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData
					{
						UnityTLSClientPtr = ptr
					};
				}
			}

			private unsafe void ProcessHandshakeMessage(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
			{
				global::Unity.Networking.Transport.ConnectionId connectionId = packetProcessor.ConnectionRef;
				global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData value = ConnectionsData[connectionId];
				UnityTLSCallbackContext->NewPacketsEndpoint = packetProcessor.EndpointRef;
				AdvanceHandshake(value.UnityTLSClientPtr);
				value.LastHandshakeUpdate = Time;
				ConnectionsData[connectionId] = value;
				if (global::Unity.TLS.LowLevel.Binding.unitytls_client_get_state(value.UnityTLSClientPtr) == 3)
				{
					if (global::Unity.TLS.LowLevel.Binding.unitytls_client_get_role(value.UnityTLSClientPtr) == 2)
					{
						Connections.FinishConnectingFromLocal(ref connectionId);
					}
					else
					{
						Connections.FinishConnectingFromRemote(ref connectionId);
					}
				}
			}

			private unsafe void ProcessDataMessage(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
			{
				global::Unity.Networking.Transport.ConnectionId connectionRef = packetProcessor.ConnectionRef;
				int offset = packetProcessor.Offset;
				global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>(ReceiveQueue.PayloadCapacity, global::Unity.Collections.Allocator.Temp);
				global::System.UIntPtr uIntPtr = default(global::System.UIntPtr);
				if (global::Unity.TLS.LowLevel.Binding.unitytls_client_read_data(ConnectionsData[connectionRef].UnityTLSClientPtr, (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), new global::System.UIntPtr((uint)nativeArray.Length), &uIntPtr) == 0)
				{
					packetProcessor.SetUnsafeMetadata(0, offset);
					packetProcessor.AppendToPayload(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), (int)uIntPtr.ToUInt32());
				}
				else
				{
					packetProcessor.Drop();
				}
			}

			private unsafe void HandlePossibleReconnection(global::Unity.Networking.Transport.ConnectionId connection, ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
			{
				global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData value = ConnectionsData[connection];
				if (value.ReconnectionClientPtr != null)
				{
					if (global::Unity.Networking.Transport.TLS.DTLSUtilities.IsServerHello(ref packetProcessor))
					{
						global::Unity.TLS.LowLevel.Binding.unitytls_client_destroy(value.UnityTLSClientPtr);
						value.UnityTLSClientPtr = value.ReconnectionClientPtr;
						value.ReconnectionClientPtr = null;
					}
					else
					{
						global::Unity.TLS.LowLevel.Binding.unitytls_client_destroy(value.ReconnectionClientPtr);
						value.ReconnectionClientPtr = null;
					}
					ConnectionsData[connection] = value;
				}
			}

			private void ProcessConnectionList()
			{
				int count = Connections.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connection = Connections.ConnectionAt(i);
					HandleConnectionState(connection);
					CheckForFailedClient(connection);
					CheckForHalfOpenConnection(connection);
					CheckForReconnection(connection);
				}
			}

			private unsafe void HandleConnectionState(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.Networking.Transport.NetworkEndpoint connectionEndpoint = Connections.GetConnectionEndpoint(connection);
				switch (Connections.GetConnectionState(connection))
				{
				case global::Unity.Networking.Transport.NetworkConnection.State.Connecting:
					if (EndpointToConnection.TryAdd(connectionEndpoint, connection))
					{
						global::Unity.TLS.LowLevel.Binding.unitytls_client* ptr = global::Unity.TLS.LowLevel.Binding.unitytls_client_create(2u, UnityTLSConfig);
						global::Unity.TLS.LowLevel.Binding.unitytls_client_init(ptr);
						ConnectionsData[connection] = new global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData
						{
							UnityTLSClientPtr = ptr,
							LastHandshakeUpdate = Time
						};
					}
					UnityTLSCallbackContext->ReceivedPacket = default(global::Unity.Networking.Transport.PacketProcessor);
					UnityTLSCallbackContext->NewPacketsEndpoint = connectionEndpoint;
					AdvanceHandshake(ConnectionsData[connection].UnityTLSClientPtr);
					break;
				case global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting:
					Disconnect(connection);
					break;
				}
			}

			private unsafe void CheckForFailedClient(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.TLS.LowLevel.Binding.unitytls_client* unityTLSClientPtr = ConnectionsData[connection].UnityTLSClientPtr;
				if (unityTLSClientPtr == null)
				{
					return;
				}
				ulong num2 = default(ulong);
				uint num = global::Unity.TLS.LowLevel.Binding.unitytls_client_get_errorsState(unityTLSClientPtr, &num2);
				uint num3 = global::Unity.TLS.LowLevel.Binding.unitytls_client_get_state(unityTLSClientPtr);
				if (num != 0 || num3 == 64)
				{
					if (num3 == 64)
					{
						uint num4 = global::Unity.TLS.LowLevel.Binding.unitytls_client_get_handshake_state(unityTLSClientPtr);
						global::UnityEngine.Debug.LogError($"DTLS handshake failed at step {num4}. Closing connection.");
					}
					Connections.StartDisconnecting(ref connection, global::Unity.Networking.Transport.Error.DisconnectReason.AuthenticationFailure);
					Disconnect(connection);
				}
			}

			private unsafe void CheckForHalfOpenConnection(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.TLS.LowLevel.Binding.unitytls_client* unityTLSClientPtr = ConnectionsData[connection].UnityTLSClientPtr;
				if (unityTLSClientPtr == null)
				{
					return;
				}
				uint num = global::Unity.TLS.LowLevel.Binding.unitytls_client_get_state(unityTLSClientPtr);
				if (num == 1 || num == 2)
				{
					long lastHandshakeUpdate = ConnectionsData[connection].LastHandshakeUpdate;
					if (Time - lastHandshakeUpdate > HalfOpenDisconnectTimeout)
					{
						Connections.StartDisconnecting(ref connection, global::Unity.Networking.Transport.Error.DisconnectReason.Timeout);
						Disconnect(connection);
					}
				}
			}

			private unsafe void CheckForReconnection(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData value = ConnectionsData[connection];
				if (value.UnityTLSClientPtr != null && value.ReconnectionClientPtr == null && global::Unity.TLS.LowLevel.Binding.unitytls_client_get_role(value.UnityTLSClientPtr) != 1 && value.LastReceive > 0 && ReconnectionTimeout > 0 && Time - value.LastReceive > ReconnectionTimeout)
				{
					value.ReconnectionClientPtr = global::Unity.TLS.LowLevel.Binding.unitytls_client_create(2u, UnityTLSConfig);
					global::Unity.TLS.LowLevel.Binding.unitytls_client_init(value.ReconnectionClientPtr);
					UnityTLSCallbackContext->NewPacketsEndpoint = Connections.GetConnectionEndpoint(connection);
					AdvanceHandshake(value.ReconnectionClientPtr);
					ConnectionsData[connection] = value;
				}
			}

			private unsafe void AdvanceHandshake(global::Unity.TLS.LowLevel.Binding.unitytls_client* clientPtr)
			{
				while (global::Unity.TLS.LowLevel.Binding.unitytls_client_handshake(clientPtr) == 1048584)
				{
				}
			}

			private void UpdateLastReceiveTime(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData value = ConnectionsData[connection];
				value.LastReceive = Time;
				ConnectionsData[connection] = value;
			}

			private unsafe void Disconnect(global::Unity.Networking.Transport.ConnectionId connection)
			{
				EndpointToConnection.Remove(Connections.GetConnectionEndpoint(connection));
				Connections.FinishDisconnecting(ref connection);
				global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData dTLSConnectionData = ConnectionsData[connection];
				if (dTLSConnectionData.UnityTLSClientPtr != null)
				{
					global::Unity.TLS.LowLevel.Binding.unitytls_client_destroy(dTLSConnectionData.UnityTLSClientPtr);
				}
				if (dTLSConnectionData.ReconnectionClientPtr != null)
				{
					global::Unity.TLS.LowLevel.Binding.unitytls_client_destroy(dTLSConnectionData.ReconnectionClientPtr);
				}
				ConnectionsData.ClearData(ref connection);
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct SendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData> ConnectionsData;

			public global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.NetworkEndpoint, global::Unity.Networking.Transport.ConnectionId> EndpointToConnection;

			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public global::Unity.Networking.Transport.PacketsQueue DeferredSends;

			public int DTLSPadding;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext* UnityTLSCallbackContext;

			public unsafe void Execute()
			{
				UnityTLSCallbackContext->SendQueue = SendQueue;
				UnityTLSCallbackContext->PacketPadding = DTLSPadding;
				int count = SendQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue[i];
					if (packetProcessor.Length == 0)
					{
						continue;
					}
					UnityTLSCallbackContext->SendQueueIndex = i;
					if (!EndpointToConnection.TryGetValue(packetProcessor.EndpointRef, out var item))
					{
						packetProcessor.Drop();
						continue;
					}
					global::Unity.TLS.LowLevel.Binding.unitytls_client* unityTLSClientPtr = ConnectionsData[item].UnityTLSClientPtr;
					byte* data = (byte*)packetProcessor.GetUnsafePayloadPtr() + packetProcessor.Offset;
					if (global::Unity.TLS.LowLevel.Binding.unitytls_client_get_state(unityTLSClientPtr) != 3)
					{
						packetProcessor.Drop();
						continue;
					}
					uint num = global::Unity.TLS.LowLevel.Binding.unitytls_client_send_data(unityTLSClientPtr, data, new global::System.UIntPtr((uint)packetProcessor.Length));
					if (num != 0)
					{
						global::UnityEngine.Debug.LogError($"Failed to encrypt packet (error: {num}). Likely internal DTLS failure. Closing connection.");
						packetProcessor.Drop();
					}
				}
				int count2 = DeferredSends.Count;
				for (int j = 0; j < count2; j++)
				{
					global::Unity.Networking.Transport.PacketProcessor processor = DeferredSends[j];
					if (processor.Length != 0 && SendQueue.EnqueuePacket(out var packetProcessor2))
					{
						packetProcessor2.EndpointRef = processor.EndpointRef;
						packetProcessor2.ConnectionRef = processor.ConnectionRef;
						packetProcessor2.SetUnsafeMetadata(0, packetProcessor2.Offset - DTLSPadding);
						packetProcessor2.AppendToPayload(processor);
					}
				}
				DeferredSends.Clear();
			}
		}

		private const int k_DeferredSendsQueueSize = 64;

		private const int k_DTLSPaddingWithoutRelay = 29;

		private const int k_DTLSPaddingWithRelay = 37;

		internal global::Unity.Networking.Transport.ConnectionList m_ConnectionList;

		private global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData> m_ConnectionsData;

		private global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.NetworkEndpoint, global::Unity.Networking.Transport.ConnectionId> m_EndpointToConnectionMap;

		private global::Unity.Networking.Transport.TLS.UnityTLSConfiguration m_UnityTLSConfiguration;

		private global::Unity.Networking.Transport.PacketsQueue m_DeferredSends;

		private long m_HalfOpenDisconnectTimeout;

		private long m_ReconnectionTimeout;

		private int m_DTLSPadding;

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = settings.GetNetworkConfigParameters();
			ushort mtu = (ushort)(networkConfigParameters.maxMessageSize - packetPadding);
			connectionList = (m_ConnectionList = global::Unity.Networking.Transport.ConnectionList.Create());
			m_ConnectionsData = new global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData>(1, default(global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData), global::Unity.Collections.Allocator.Persistent);
			m_EndpointToConnectionMap = new global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.NetworkEndpoint, global::Unity.Networking.Transport.ConnectionId>(1, global::Unity.Collections.Allocator.Persistent);
			m_UnityTLSConfiguration = new global::Unity.Networking.Transport.TLS.UnityTLSConfiguration(ref settings, global::Unity.Networking.Transport.TLS.SecureTransportProtocol.DTLS, mtu);
			m_DeferredSends = new global::Unity.Networking.Transport.PacketsQueue(64, networkConfigParameters.maxMessageSize);
			m_DeferredSends.SetDefaultDataOffset(packetPadding);
			m_HalfOpenDisconnectTimeout = (networkConfigParameters.maxConnectAttempts + 1) * networkConfigParameters.connectTimeoutMS;
			m_ReconnectionTimeout = networkConfigParameters.reconnectionTimeoutMS;
			m_DTLSPadding = (settings.TryGet<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>(out var _) ? 37 : 29);
			packetPadding += m_DTLSPadding;
			return 0;
		}

		public unsafe void Dispose()
		{
			for (int i = 0; i < m_ConnectionsData.Length; i++)
			{
				global::Unity.Networking.Transport.DTLSLayer.DTLSConnectionData dTLSConnectionData = m_ConnectionsData.DataAt(i);
				if (dTLSConnectionData.UnityTLSClientPtr != null)
				{
					global::Unity.TLS.LowLevel.Binding.unitytls_client_destroy(dTLSConnectionData.UnityTLSClientPtr);
				}
				if (dTLSConnectionData.ReconnectionClientPtr != null)
				{
					global::Unity.TLS.LowLevel.Binding.unitytls_client_destroy(dTLSConnectionData.ReconnectionClientPtr);
				}
			}
			m_ConnectionList.Dispose();
			m_ConnectionsData.Dispose();
			m_EndpointToConnectionMap.Dispose();
			m_UnityTLSConfiguration.Dispose();
			m_DeferredSends.Dispose();
		}

		public unsafe global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.DTLSLayer.ReceiveJob
			{
				Connections = m_ConnectionList,
				ConnectionsData = m_ConnectionsData,
				EndpointToConnection = m_EndpointToConnectionMap,
				DeferredSends = m_DeferredSends,
				ReceiveQueue = arguments.ReceiveQueue,
				Time = arguments.Time,
				HalfOpenDisconnectTimeout = m_HalfOpenDisconnectTimeout,
				ReconnectionTimeout = m_ReconnectionTimeout,
				UnityTLSConfig = m_UnityTLSConfiguration.ConfigPtr,
				UnityTLSCallbackContext = m_UnityTLSConfiguration.CallbackContextPtr
			}, dependency);
		}

		public unsafe global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.DTLSLayer.SendJob
			{
				ConnectionsData = m_ConnectionsData,
				EndpointToConnection = m_EndpointToConnectionMap,
				SendQueue = arguments.SendQueue,
				DeferredSends = m_DeferredSends,
				DTLSPadding = m_DTLSPadding,
				UnityTLSCallbackContext = m_UnityTLSConfiguration.CallbackContextPtr
			}, dependency);
		}
	}
}
