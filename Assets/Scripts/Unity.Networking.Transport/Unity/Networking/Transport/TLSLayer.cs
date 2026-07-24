namespace Unity.Networking.Transport
{
	internal struct TLSLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		private struct TLSConnectionData
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.TLS.LowLevel.Binding.unitytls_client* UnityTLSClientPtr;

			public global::Unity.Networking.Transport.ConnectionId UnderlyingConnection;

			public long LastHandshakeUpdate;

			public unsafe fixed byte DecryptBuffer[2944];

			public int DecryptBufferLength;
		}

		[global::Unity.Burst.BurstCompile]
		private struct ReceiveJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.ConnectionList Connections;

			public global::Unity.Networking.Transport.ConnectionList UnderlyingConnections;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.TLSLayer.TLSConnectionData> ConnectionsData;

			public global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.ConnectionId, global::Unity.Networking.Transport.ConnectionId> UnderlyingIdToCurrentId;

			public global::Unity.Networking.Transport.PacketsQueue DeferredSends;

			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			public long Time;

			public long HalfOpenDisconnectTimeout;

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
				ProcessUnderlyingConnectionList();
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
					if (!UnderlyingIdToCurrentId.TryGetValue(packetProcessor.ConnectionRef, out var item))
					{
						item = ProcessClientHello(ref packetProcessor);
					}
					packetProcessor.ConnectionRef = item;
					global::Unity.TLS.LowLevel.Binding.unitytls_client* unityTLSClientPtr = ConnectionsData[item].UnityTLSClientPtr;
					uint num = global::Unity.TLS.LowLevel.Binding.unitytls_client_get_state(unityTLSClientPtr);
					if (num == 1 || num == 2)
					{
						ProcessHandshakeMessage(ref packetProcessor);
						if (packetProcessor.Length == 0)
						{
							continue;
						}
						num = global::Unity.TLS.LowLevel.Binding.unitytls_client_get_state(unityTLSClientPtr);
					}
					if (num == 64)
					{
						packetProcessor.Drop();
					}
					else
					{
						ProcessDataMessage(ref packetProcessor);
					}
				}
			}

			private unsafe global::Unity.Networking.Transport.ConnectionId ProcessClientHello(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
			{
				global::Unity.Networking.Transport.ConnectionId connectionId = Connections.StartConnecting(ref packetProcessor.EndpointRef);
				UnderlyingIdToCurrentId.Add(packetProcessor.ConnectionRef, connectionId);
				global::Unity.TLS.LowLevel.Binding.unitytls_client* ptr = global::Unity.TLS.LowLevel.Binding.unitytls_client_create(1u, UnityTLSConfig);
				global::Unity.TLS.LowLevel.Binding.unitytls_client_init(ptr);
				ConnectionsData[connectionId] = new global::Unity.Networking.Transport.TLSLayer.TLSConnectionData
				{
					UnityTLSClientPtr = ptr,
					UnderlyingConnection = packetProcessor.ConnectionRef
				};
				return connectionId;
			}

			private unsafe void ProcessHandshakeMessage(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
			{
				global::Unity.Networking.Transport.ConnectionId connectionId = packetProcessor.ConnectionRef;
				global::Unity.Networking.Transport.TLSLayer.TLSConnectionData value = ConnectionsData[connectionId];
				UnityTLSCallbackContext->NewPacketsEndpoint = packetProcessor.EndpointRef;
				UnityTLSCallbackContext->NewPacketsConnection = value.UnderlyingConnection;
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
				int offset = packetProcessor.Offset;
				global::Unity.Networking.Transport.ConnectionId connectionRef = packetProcessor.ConnectionRef;
				global::Unity.Networking.Transport.TLSLayer.TLSConnectionData data = ConnectionsData[connectionRef];
				while (packetProcessor.Length > 0)
				{
					byte* buffer = data.DecryptBuffer + data.DecryptBufferLength;
					int value = 2944 - data.DecryptBufferLength;
					global::System.UIntPtr uIntPtr = default(global::System.UIntPtr);
					uint num = global::Unity.TLS.LowLevel.Binding.unitytls_client_read_data(data.UnityTLSClientPtr, buffer, new global::System.UIntPtr((uint)value), &uIntPtr);
					if (num == 1048577 || (num == 0 && uIntPtr.ToUInt32() == 0) || num != 0)
					{
						break;
					}
					data.DecryptBufferLength += (int)uIntPtr.ToUInt32();
				}
				packetProcessor.SetUnsafeMetadata(0, offset);
				CopyDecryptBufferToPacket(ref data, ref packetProcessor);
				ConnectionsData[connectionRef] = data;
			}

			private void ProcessUnderlyingConnectionList()
			{
				global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> nativeArray = UnderlyingConnections.QueryIncomingDisconnections(global::Unity.Collections.Allocator.Temp);
				int length = nativeArray.Length;
				for (int i = 0; i < length; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connection = nativeArray[i].Connection;
					if (UnderlyingIdToCurrentId.TryGetValue(connection, out var item))
					{
						if (Connections.GetConnectionState(item) != global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting)
						{
							Connections.StartDisconnecting(ref item, nativeArray[i].Reason);
						}
						Disconnect(item);
					}
				}
			}

			private void ProcessConnectionList()
			{
				int count = Connections.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connectionId = Connections.ConnectionAt(i);
					switch (Connections.GetConnectionState(connectionId))
					{
					case global::Unity.Networking.Transport.NetworkConnection.State.Connected:
						HandleConnectedState(connectionId);
						break;
					case global::Unity.Networking.Transport.NetworkConnection.State.Connecting:
						HandleConnectingState(connectionId);
						break;
					case global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting:
						HandleDisconnectingState(connectionId);
						break;
					}
					CheckForFailedClient(connectionId);
					CheckForHalfOpenConnection(connectionId);
				}
			}

			private void HandleConnectedState(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.Networking.Transport.TLSLayer.TLSConnectionData data = ConnectionsData[connection];
				if (data.DecryptBufferLength > 0 && ReceiveQueue.EnqueuePacket(out var packetProcessor))
				{
					packetProcessor.ConnectionRef = connection;
					packetProcessor.EndpointRef = Connections.GetConnectionEndpoint(connection);
					CopyDecryptBufferToPacket(ref data, ref packetProcessor);
					ConnectionsData[connection] = data;
				}
			}

			private unsafe void HandleConnectingState(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.Networking.Transport.NetworkEndpoint address = Connections.GetConnectionEndpoint(connection);
				global::Unity.Networking.Transport.ConnectionId connectionId = ConnectionsData[connection].UnderlyingConnection;
				if (connectionId == default(global::Unity.Networking.Transport.ConnectionId))
				{
					connectionId = UnderlyingConnections.StartConnecting(ref address);
					UnderlyingIdToCurrentId.Add(connectionId, connection);
					global::Unity.TLS.LowLevel.Binding.unitytls_client* ptr = global::Unity.TLS.LowLevel.Binding.unitytls_client_create(2u, UnityTLSConfig);
					global::Unity.TLS.LowLevel.Binding.unitytls_client_init(ptr);
					ConnectionsData[connection] = new global::Unity.Networking.Transport.TLSLayer.TLSConnectionData
					{
						UnityTLSClientPtr = ptr,
						UnderlyingConnection = connectionId,
						LastHandshakeUpdate = Time
					};
				}
				if (UnderlyingConnections.GetConnectionState(connectionId) == global::Unity.Networking.Transport.NetworkConnection.State.Connected)
				{
					global::Unity.TLS.LowLevel.Binding.unitytls_client* unityTLSClientPtr = ConnectionsData[connection].UnityTLSClientPtr;
					uint num = global::Unity.TLS.LowLevel.Binding.unitytls_client_get_state(unityTLSClientPtr);
					if (num == 1 || num == 2)
					{
						UnityTLSCallbackContext->ReceivedPacket = default(global::Unity.Networking.Transport.PacketProcessor);
						UnityTLSCallbackContext->NewPacketsEndpoint = address;
						UnityTLSCallbackContext->NewPacketsConnection = connectionId;
						AdvanceHandshake(unityTLSClientPtr);
					}
				}
			}

			private void HandleDisconnectingState(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.Networking.Transport.ConnectionId connectionId = ConnectionsData[connection].UnderlyingConnection;
				UnderlyingConnections.StartDisconnecting(ref connectionId);
				Disconnect(connection);
			}

			private unsafe void CheckForFailedClient(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.Networking.Transport.TLSLayer.TLSConnectionData tLSConnectionData = ConnectionsData[connection];
				global::Unity.TLS.LowLevel.Binding.unitytls_client* unityTLSClientPtr = tLSConnectionData.UnityTLSClientPtr;
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
						global::UnityEngine.Debug.LogError($"TLS handshake failed at step {num4}. Closing connection.");
					}
					UnderlyingConnections.StartDisconnecting(ref tLSConnectionData.UnderlyingConnection);
					Connections.StartDisconnecting(ref connection, global::Unity.Networking.Transport.Error.DisconnectReason.AuthenticationFailure);
					Disconnect(connection);
				}
			}

			private unsafe void CheckForHalfOpenConnection(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.Networking.Transport.TLSLayer.TLSConnectionData tLSConnectionData = ConnectionsData[connection];
				if (tLSConnectionData.UnityTLSClientPtr != null)
				{
					uint num = global::Unity.TLS.LowLevel.Binding.unitytls_client_get_state(tLSConnectionData.UnityTLSClientPtr);
					if ((num == 1 || num == 2) && Time - tLSConnectionData.LastHandshakeUpdate > HalfOpenDisconnectTimeout)
					{
						UnderlyingConnections.StartDisconnecting(ref tLSConnectionData.UnderlyingConnection);
						Connections.StartDisconnecting(ref connection, global::Unity.Networking.Transport.Error.DisconnectReason.Timeout);
						Disconnect(connection);
					}
				}
			}

			private unsafe void CopyDecryptBufferToPacket(ref global::Unity.Networking.Transport.TLSLayer.TLSConnectionData data, ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
			{
				fixed (byte* decryptBuffer = data.DecryptBuffer)
				{
					int num = global::Unity.Mathematics.math.min(packetProcessor.BytesAvailableAtEnd, data.DecryptBufferLength);
					packetProcessor.AppendToPayload(decryptBuffer, num);
					data.DecryptBufferLength -= num;
					if (data.DecryptBufferLength > 0)
					{
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(decryptBuffer, decryptBuffer + num, data.DecryptBufferLength);
					}
				}
			}

			private unsafe void AdvanceHandshake(global::Unity.TLS.LowLevel.Binding.unitytls_client* clientPtr)
			{
				while (global::Unity.TLS.LowLevel.Binding.unitytls_client_handshake(clientPtr) == 1048584)
				{
				}
			}

			private unsafe void Disconnect(global::Unity.Networking.Transport.ConnectionId connection)
			{
				global::Unity.Networking.Transport.TLSLayer.TLSConnectionData tLSConnectionData = ConnectionsData[connection];
				if (tLSConnectionData.UnityTLSClientPtr != null)
				{
					global::Unity.TLS.LowLevel.Binding.unitytls_client_destroy(tLSConnectionData.UnityTLSClientPtr);
				}
				UnderlyingIdToCurrentId.Remove(tLSConnectionData.UnderlyingConnection);
				ConnectionsData.ClearData(ref connection);
				Connections.FinishDisconnecting(ref connection);
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct SendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.ConnectionList Connections;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.TLSLayer.TLSConnectionData> ConnectionsData;

			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public global::Unity.Networking.Transport.PacketsQueue DeferredSends;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext* UnityTLSCallbackContext;

			public unsafe void Execute()
			{
				UnityTLSCallbackContext->SendQueue = SendQueue;
				UnityTLSCallbackContext->PacketPadding = 68;
				int count = SendQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue[i];
					if (packetProcessor.Length == 0)
					{
						continue;
					}
					UnityTLSCallbackContext->SendQueueIndex = i;
					global::Unity.Networking.Transport.ConnectionId connectionRef = packetProcessor.ConnectionRef;
					packetProcessor.ConnectionRef = ConnectionsData[connectionRef].UnderlyingConnection;
					global::Unity.Networking.Transport.NetworkConnection.State connectionState = Connections.GetConnectionState(connectionRef);
					global::Unity.TLS.LowLevel.Binding.unitytls_client* unityTLSClientPtr = ConnectionsData[connectionRef].UnityTLSClientPtr;
					if (connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Connected || unityTLSClientPtr == null)
					{
						packetProcessor.Drop();
						continue;
					}
					byte* data = (byte*)packetProcessor.GetUnsafePayloadPtr() + packetProcessor.Offset;
					uint num = global::Unity.TLS.LowLevel.Binding.unitytls_client_send_data(unityTLSClientPtr, data, new global::System.UIntPtr((uint)packetProcessor.Length));
					if (num != 0)
					{
						global::UnityEngine.Debug.LogError($"Failed to encrypt packet (error: {num}). Likely internal TLS failure. Closing connection.");
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
						packetProcessor2.SetUnsafeMetadata(0, packetProcessor2.Offset - 68);
						packetProcessor2.AppendToPayload(processor);
					}
				}
				DeferredSends.Clear();
			}
		}

		private const int k_DeferredSendsQueueSize = 64;

		private const int k_TLSPadding = 68;

		private const int k_DecryptBufferSize = 2944;

		internal global::Unity.Networking.Transport.ConnectionList m_ConnectionList;

		private global::Unity.Networking.Transport.ConnectionList m_UnderlyingConnectionList;

		private global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.TLSLayer.TLSConnectionData> m_ConnectionsData;

		private global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.ConnectionId, global::Unity.Networking.Transport.ConnectionId> m_UnderlyingIdToCurrentIdMap;

		private global::Unity.Networking.Transport.TLS.UnityTLSConfiguration m_UnityTLSConfiguration;

		private global::Unity.Networking.Transport.PacketsQueue m_DeferredSends;

		private long m_HalfOpenDisconnectTimeout;

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = settings.GetNetworkConfigParameters();
			m_UnderlyingConnectionList = connectionList;
			connectionList = (m_ConnectionList = global::Unity.Networking.Transport.ConnectionList.Create());
			m_ConnectionsData = new global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.TLSLayer.TLSConnectionData>(1, default(global::Unity.Networking.Transport.TLSLayer.TLSConnectionData), global::Unity.Collections.Allocator.Persistent);
			m_UnderlyingIdToCurrentIdMap = new global::Unity.Collections.NativeParallelHashMap<global::Unity.Networking.Transport.ConnectionId, global::Unity.Networking.Transport.ConnectionId>(1, global::Unity.Collections.Allocator.Persistent);
			m_UnityTLSConfiguration = new global::Unity.Networking.Transport.TLS.UnityTLSConfiguration(ref settings, global::Unity.Networking.Transport.TLS.SecureTransportProtocol.TLS, 0);
			m_DeferredSends = new global::Unity.Networking.Transport.PacketsQueue(64, networkConfigParameters.maxMessageSize);
			m_DeferredSends.SetDefaultDataOffset(packetPadding);
			m_HalfOpenDisconnectTimeout = networkConfigParameters.maxConnectAttempts * networkConfigParameters.connectTimeoutMS;
			packetPadding += 68;
			return 0;
		}

		public unsafe void Dispose()
		{
			for (int i = 0; i < m_ConnectionsData.Length; i++)
			{
				global::Unity.Networking.Transport.TLSLayer.TLSConnectionData tLSConnectionData = m_ConnectionsData.DataAt(i);
				if (tLSConnectionData.UnityTLSClientPtr != null)
				{
					global::Unity.TLS.LowLevel.Binding.unitytls_client_destroy(tLSConnectionData.UnityTLSClientPtr);
				}
			}
			m_ConnectionList.Dispose();
			m_ConnectionsData.Dispose();
			m_UnderlyingIdToCurrentIdMap.Dispose();
			m_UnityTLSConfiguration.Dispose();
			m_DeferredSends.Dispose();
		}

		public unsafe global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.TLSLayer.ReceiveJob
			{
				Connections = m_ConnectionList,
				UnderlyingConnections = m_UnderlyingConnectionList,
				ConnectionsData = m_ConnectionsData,
				UnderlyingIdToCurrentId = m_UnderlyingIdToCurrentIdMap,
				DeferredSends = m_DeferredSends,
				ReceiveQueue = arguments.ReceiveQueue,
				Time = arguments.Time,
				HalfOpenDisconnectTimeout = m_HalfOpenDisconnectTimeout,
				UnityTLSConfig = m_UnityTLSConfiguration.ConfigPtr,
				UnityTLSCallbackContext = m_UnityTLSConfiguration.CallbackContextPtr
			}, dependency);
		}

		public unsafe global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.TLSLayer.SendJob
			{
				Connections = m_ConnectionList,
				ConnectionsData = m_ConnectionsData,
				SendQueue = arguments.SendQueue,
				DeferredSends = m_DeferredSends,
				UnityTLSCallbackContext = m_UnityTLSConfiguration.CallbackContextPtr
			}, dependency);
		}
	}
}
