namespace Unity.Networking.Transport
{
	[global::Unity.Burst.BurstCompile]
	internal struct TCPNetworkInterface : global::Unity.Networking.Transport.INetworkInterface, global::System.IDisposable
	{
		private class AllSockets
		{
			private AllSockets()
			{
			}

			public static void Add(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socket)
			{
			}

			public static void Remove(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socket)
			{
			}
		}

		private static class TCPSocket
		{
			public unsafe static global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle Listen(ref global::Unity.Networking.Transport.NetworkEndpoint localEndpoint, out global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState errorState)
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Networking.Transport.NetworkEndpoint networkEndpoint = localEndpoint;
				global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress* baselibAddressPtr = networkEndpoint.BaselibAddressPtr;
				global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle baselib_Socket_Handle = global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Create((global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family)baselibAddressPtr->family, global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Protocol.TCP, &baselib_ErrorState);
				if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Bind(baselib_Socket_Handle, baselibAddressPtr, global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_AddressReuse.Allow, &baselib_ErrorState);
					if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_GetAddress(baselib_Socket_Handle, baselibAddressPtr, &baselib_ErrorState);
						global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_TCP_Listen(baselib_Socket_Handle, &baselib_ErrorState);
					}
					if (baselib_ErrorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Close(baselib_Socket_Handle);
						baselib_Socket_Handle = InvalidSocket;
					}
				}
				localEndpoint = networkEndpoint;
				errorState = baselib_ErrorState;
				return baselib_Socket_Handle;
			}

			public unsafe static global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle Accept(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle listenSocket, out global::Unity.Networking.Transport.NetworkEndpoint localEndpoint)
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle baselib_Socket_Handle = global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_TCP_Accept(listenSocket, &error);
				localEndpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
				if (IsValid(baselib_Socket_Handle) && error.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress baselibAddress = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
					global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_GetAddress(baselib_Socket_Handle, &baselibAddress, &error);
					if (error.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						global::UnityEngine.Debug.LogError($"Baselib operation failed. Failed to get local endpoint. (error {(int)error.code}: {(global::Unity.Networking.Transport.UDPNetworkInterface.GetBaselibErrorMessage(error))})");
						global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Close(baselib_Socket_Handle);
						baselib_Socket_Handle = InvalidSocket;
					}
					localEndpoint = new global::Unity.Networking.Transport.NetworkEndpoint(baselibAddress);
				}
				return baselib_Socket_Handle;
			}

			public unsafe static global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle Connect(global::Unity.Networking.Transport.NetworkEndpoint remoteEndoint)
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress* baselibAddressPtr = remoteEndoint.BaselibAddressPtr;
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle baselib_Socket_Handle = global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Create((global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family)baselibAddressPtr->family, global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Protocol.TCP, &baselib_ErrorState);
				if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_TCP_Connect(baselib_Socket_Handle, baselibAddressPtr, global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_AddressReuse.Allow, &baselib_ErrorState);
				}
				if (baselib_ErrorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Close(baselib_Socket_Handle);
					baselib_Socket_Handle = InvalidSocket;
				}
				return baselib_Socket_Handle;
			}

			public unsafe static bool IsConnectionReady(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socket, out global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState errorState)
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState2 = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_PollFd baselib_Socket_PollFd = new global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_PollFd
				{
					handle = socket,
					requestedEvents = global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_PollEvents.Connected,
					errorState = &baselib_ErrorState2
				};
				global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Poll(&baselib_Socket_PollFd, 1u, 0u, &baselib_ErrorState);
				errorState = baselib_ErrorState;
				if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					if (baselib_Socket_PollFd.errorState->code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						errorState = *baselib_Socket_PollFd.errorState;
						return false;
					}
					return (baselib_Socket_PollFd.resultEvents & global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_PollEvents.Connected) != 0;
				}
				return false;
			}

			public static void Close(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socket)
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Close(socket);
			}

			public unsafe static int Send(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socket, byte* data, int length, out global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState errorState)
			{
				int result = 0;
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				if (length > 0)
				{
					result = (int)global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_TCP_Send(socket, (global::System.IntPtr)data, (uint)length, &baselib_ErrorState);
				}
				errorState = baselib_ErrorState;
				return result;
			}

			public unsafe static int Receive(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socket, byte* data, int capacity, out global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState errorState)
			{
				int result = 0;
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				if (capacity > 0)
				{
					result = (int)global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_TCP_Recv(socket, (global::System.IntPtr)data, (uint)capacity, &baselib_ErrorState);
				}
				errorState = baselib_ErrorState;
				return result;
			}
		}

		private struct InternalData
		{
			public global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle ListenSocket;

			public global::Unity.Networking.Transport.NetworkEndpoint ListenEndpoint;

			public int ConnectTimeoutMS;

			public int MaxConnectAttempts;
		}

		private struct ConnectionData
		{
			public global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle Socket;

			public long ConnectTime;

			public long LastConnectAttemptTime;

			public int LastConnectAttempt;

			public bool HasPendingSends;
		}

		private struct PendingSend
		{
			public global::Unity.Networking.Transport.ConnectionId Connection;

			public int BufferIndex;
		}

		[global::Unity.Burst.BurstCompile]
		private struct ReceiveJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			public global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.TCPNetworkInterface.InternalData> InternalData;

			public global::Unity.Networking.Transport.ConnectionList ConnectionList;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData> ConnectionMap;

			public long Time;

			private void Abort(ref global::Unity.Networking.Transport.ConnectionId connectionId, ref global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData connectionData)
			{
				ConnectionList.FinishDisconnecting(ref connectionId);
				ConnectionMap.ClearData(ref connectionId);
				global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Close(connectionData.Socket);
			}

			public unsafe void Execute()
			{
				if (IsValid(InternalData.Value.ListenSocket))
				{
					global::Unity.Networking.Transport.NetworkEndpoint localEndpoint;
					global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socket = global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Accept(InternalData.Value.ListenSocket, out localEndpoint);
					if (IsValid(socket))
					{
						global::Unity.Networking.Transport.ConnectionId connectionId = ConnectionList.StartConnecting(ref localEndpoint);
						ConnectionList.FinishConnectingFromRemote(ref connectionId);
						ConnectionMap[connectionId] = new global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData
						{
							Socket = socket
						};
					}
				}
				int count = ConnectionList.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connectionId2 = ConnectionList.ConnectionAt(i);
					global::Unity.Networking.Transport.NetworkConnection.State connectionState = ConnectionList.GetConnectionState(connectionId2);
					if (connectionState == global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
					{
						continue;
					}
					global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData connectionData = ConnectionMap[connectionId2];
					switch (connectionState)
					{
					case global::Unity.Networking.Transport.NetworkConnection.State.Connecting:
						if (connectionData.ConnectTime == 0L)
						{
							connectionData.ConnectTime = Time;
							connectionData.LastConnectAttemptTime = global::System.Math.Max(0L, Time - InternalData.Value.ConnectTimeoutMS);
						}
						if (connectionData.LastConnectAttempt >= InternalData.Value.MaxConnectAttempts)
						{
							ConnectionList.StartDisconnecting(ref connectionId2, global::Unity.Networking.Transport.Error.DisconnectReason.MaxConnectionAttempts);
							Abort(ref connectionId2, ref connectionData);
							continue;
						}
						if (Time - connectionData.LastConnectAttemptTime >= InternalData.Value.ConnectTimeoutMS)
						{
							global::Unity.Networking.Transport.NetworkEndpoint connectionEndpoint = ConnectionList.GetConnectionEndpoint(connectionId2);
							if (!IsValid(connectionData.Socket))
							{
								connectionData.Socket = global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Connect(connectionEndpoint);
							}
							connectionData.LastConnectAttempt++;
							connectionData.LastConnectAttemptTime = Time;
						}
						if (IsValid(connectionData.Socket))
						{
							if (global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.IsConnectionReady(connectionData.Socket, out var errorState))
							{
								ConnectionList.FinishConnectingFromLocal(ref connectionId2);
							}
							else if (errorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
							{
								global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Close(connectionData.Socket);
								connectionData.Socket = InvalidSocket;
							}
						}
						ConnectionMap[connectionId2] = connectionData;
						continue;
					case global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting:
						Abort(ref connectionId2, ref connectionData);
						continue;
					}
					global::Unity.Networking.Transport.NetworkEndpoint connectionEndpoint2 = ConnectionList.GetConnectionEndpoint(connectionId2);
					global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState errorState2 = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
					while (ReceiveQueue.Count < ReceiveQueue.Capacity / 2)
					{
						ReceiveQueue.EnqueuePacket(out var packetProcessor);
						packetProcessor.ConnectionRef = connectionId2;
						packetProcessor.EndpointRef = connectionEndpoint2;
						int num = global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Receive(connectionData.Socket, (byte*)packetProcessor.GetUnsafePayloadPtr(), packetProcessor.BytesAvailableAtEnd, out errorState2);
						if (errorState2.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success || num <= 0)
						{
							packetProcessor.Drop();
							break;
						}
						packetProcessor.SetUnsafeMetadata(num);
					}
					if (errorState2.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						ConnectionList.StartDisconnecting(ref connectionId2, global::Unity.Networking.Transport.Error.DisconnectReason.ProtocolError);
						Abort(ref connectionId2, ref connectionData);
						return;
					}
					ConnectionMap[connectionId2] = connectionData;
				}
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct SendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.TCPNetworkInterface.InternalData> InternalData;

			public global::Unity.Networking.Transport.ConnectionList ConnectionList;

			public global::Unity.Collections.NativeList<global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend> PendingSends;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData> ConnectionMap;

			public void Execute()
			{
				ProcessPendingSends();
				ProcessSendQueue();
			}

			private unsafe void ProcessPendingSends()
			{
				int length = PendingSends.Length;
				global::Unity.Collections.NativeList<global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend> list = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend>(length, global::Unity.Collections.Allocator.Temp);
				ResetHasPendingSendsFlag();
				for (int i = 0; i < length; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connection = PendingSends[i].Connection;
					int bufferIndex = PendingSends[i].BufferIndex;
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue.GetPacketProcessor(bufferIndex);
					if (ConnectionList.GetConnectionState(connection) == global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
					{
						SendQueue.EnqueuePacket(bufferIndex, out packetProcessor);
						packetProcessor.Drop();
						continue;
					}
					global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData value = ConnectionMap[connection];
					if (value.HasPendingSends)
					{
						list.Add(PendingSends[i]);
						continue;
					}
					byte* data = (byte*)packetProcessor.GetUnsafePayloadPtr() + packetProcessor.Offset;
					global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState errorState;
					int num = global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Send(value.Socket, data, packetProcessor.Length, out errorState);
					if (num != packetProcessor.Length)
					{
						int offset = packetProcessor.Offset + num;
						int size = packetProcessor.Length - num;
						packetProcessor.SetUnsafeMetadata(size, offset);
						value.HasPendingSends = true;
						ConnectionMap[connection] = value;
						list.Add(PendingSends[i]);
					}
					else if (errorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						global::Unity.Networking.Transport.Error.DisconnectReason reason = ((errorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Disconnected) ? global::Unity.Networking.Transport.Error.DisconnectReason.ClosedByRemote : global::Unity.Networking.Transport.Error.DisconnectReason.ProtocolError);
						Abort(connection, reason);
					}
					else
					{
						SendQueue.EnqueuePacket(bufferIndex, out packetProcessor);
						packetProcessor.Drop();
					}
				}
				PendingSends.Clear();
				PendingSends.AddRangeNoResize(list);
			}

			private unsafe void ProcessSendQueue()
			{
				int count = SendQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue[i];
					if (packetProcessor.Length == 0)
					{
						continue;
					}
					global::Unity.Networking.Transport.ConnectionId connectionRef = packetProcessor.ConnectionRef;
					if (ConnectionList.GetConnectionState(connectionRef) == global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
					{
						continue;
					}
					global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData value = ConnectionMap[connectionRef];
					if (value.HasPendingSends)
					{
						int bufferIndex = SendQueue.DequeuePacketNoRelease(i);
						ref global::Unity.Collections.NativeList<global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend> pendingSends = ref PendingSends;
						global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend value2 = new global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend
						{
							Connection = connectionRef,
							BufferIndex = bufferIndex
						};
						pendingSends.Add(in value2);
						continue;
					}
					byte* data = (byte*)packetProcessor.GetUnsafePayloadPtr() + packetProcessor.Offset;
					global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState errorState;
					int num = global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Send(value.Socket, data, packetProcessor.Length, out errorState);
					if (num != packetProcessor.Length)
					{
						int offset = packetProcessor.Offset + num;
						int size = packetProcessor.Length - num;
						packetProcessor.SetUnsafeMetadata(size, offset);
						value.HasPendingSends = true;
						ConnectionMap[connectionRef] = value;
						int bufferIndex2 = SendQueue.DequeuePacketNoRelease(i);
						ref global::Unity.Collections.NativeList<global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend> pendingSends2 = ref PendingSends;
						global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend value2 = new global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend
						{
							Connection = connectionRef,
							BufferIndex = bufferIndex2
						};
						pendingSends2.Add(in value2);
					}
					else if (errorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						global::Unity.Networking.Transport.Error.DisconnectReason reason = ((errorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Disconnected) ? global::Unity.Networking.Transport.Error.DisconnectReason.ClosedByRemote : global::Unity.Networking.Transport.Error.DisconnectReason.ProtocolError);
						Abort(connectionRef, reason);
					}
				}
			}

			private void ResetHasPendingSendsFlag()
			{
				int count = ConnectionList.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connection = ConnectionList.ConnectionAt(i);
					global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData value = ConnectionMap[connection];
					value.HasPendingSends = false;
					ConnectionMap[connection] = value;
				}
			}

			private void Abort(global::Unity.Networking.Transport.ConnectionId connectionId, global::Unity.Networking.Transport.Error.DisconnectReason reason)
			{
				global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData connectionData = ConnectionMap[connectionId];
				ConnectionList.StartDisconnecting(ref connectionId, reason);
				ConnectionList.FinishDisconnecting(ref connectionId);
				ConnectionMap.ClearData(ref connectionId);
				global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Close(connectionData.Socket);
			}
		}

		private static readonly global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle InvalidSocket = global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle_Invalid;

		private global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.TCPNetworkInterface.InternalData> m_InternalData;

		private global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData> m_ConnectionMap;

		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend> m_PendingSends;

		private global::Unity.Networking.Transport.ConnectionList m_ConnectionList;

		public unsafe global::Unity.Networking.Transport.NetworkEndpoint LocalEndpoint
		{
			get
			{
				for (int i = 0; i < m_ConnectionList.Count; i++)
				{
					global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData connectionData = m_ConnectionMap[m_ConnectionList.ConnectionAt(i)];
					if (connectionData.Socket.handle != global::System.IntPtr.Zero)
					{
						global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress baselibAddress = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
						global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
						global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_GetAddress(connectionData.Socket, &baselibAddress, &baselib_ErrorState);
						global::Unity.Networking.Transport.NetworkEndpoint result = new global::Unity.Networking.Transport.NetworkEndpoint(baselibAddress);
						if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success && result.Port != 0)
						{
							return result;
						}
					}
				}
				return m_InternalData.Value.ListenEndpoint;
			}
		}

		private static bool IsValid(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socket)
		{
			if (socket.handle != (global::System.IntPtr)0)
			{
				return socket.handle != InvalidSocket.handle;
			}
			return false;
		}

		internal global::Unity.Networking.Transport.ConnectionList CreateConnectionList()
		{
			m_ConnectionList = global::Unity.Networking.Transport.ConnectionList.Create();
			return m_ConnectionList;
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref int packetPadding)
		{
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = settings.GetNetworkConfigParameters();
			global::Unity.Networking.Transport.TCPNetworkInterface.InternalData value = new global::Unity.Networking.Transport.TCPNetworkInterface.InternalData
			{
				ListenEndpoint = global::Unity.Networking.Transport.NetworkEndpoint.AnyIpv4,
				ListenSocket = InvalidSocket,
				ConnectTimeoutMS = global::System.Math.Max(0, networkConfigParameters.connectTimeoutMS),
				MaxConnectAttempts = global::System.Math.Max(1, networkConfigParameters.maxConnectAttempts)
			};
			m_InternalData = new global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.TCPNetworkInterface.InternalData>(value, global::Unity.Collections.Allocator.Persistent);
			m_ConnectionMap = new global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData>(1, default(global::Unity.Networking.Transport.TCPNetworkInterface.ConnectionData), global::Unity.Collections.Allocator.Persistent);
			m_PendingSends = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.TCPNetworkInterface.PendingSend>(1, global::Unity.Collections.Allocator.Persistent);
			return 0;
		}

		public int Bind(global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			global::Unity.Networking.Transport.TCPNetworkInterface.InternalData value = m_InternalData.Value;
			value.ListenEndpoint = endpoint;
			m_InternalData.Value = value;
			return 0;
		}

		public int Listen()
		{
			global::Unity.Networking.Transport.TCPNetworkInterface.InternalData value = m_InternalData.Value;
			value.ListenSocket = global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Listen(ref value.ListenEndpoint, out var errorState);
			if (errorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
			{
				if (errorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.AddressInUse)
				{
					global::UnityEngine.Debug.LogError($"Failed to listen on TCP socket because the address is already in use. Likely because there is another process listening on port {value.ListenEndpoint.Port}.");
				}
				else
				{
					global::UnityEngine.Debug.LogError($"Baselib operation failed. Failed to listen on TCP socket. (error {(int)errorState.code}: {(global::Unity.Networking.Transport.UDPNetworkInterface.GetBaselibErrorMessage(errorState))})");
				}
				return -11;
			}
			global::Unity.Networking.Transport.TCPNetworkInterface.AllSockets.Add(value.ListenSocket);
			m_InternalData.Value = value;
			return 0;
		}

		public void Dispose()
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle listenSocket = m_InternalData.Value.ListenSocket;
			if (IsValid(listenSocket))
			{
				global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Close(listenSocket);
				global::Unity.Networking.Transport.TCPNetworkInterface.AllSockets.Remove(listenSocket);
			}
			m_InternalData.Dispose();
			for (int i = 0; i < m_ConnectionMap.Length; i++)
			{
				listenSocket = m_ConnectionMap.DataAt(i).Socket;
				if (IsValid(listenSocket))
				{
					global::Unity.Networking.Transport.TCPNetworkInterface.TCPSocket.Close(listenSocket);
				}
				global::Unity.Networking.Transport.TCPNetworkInterface.AllSockets.Remove(listenSocket);
			}
			m_ConnectionMap.Dispose();
			m_ConnectionList.Dispose();
			m_PendingSends.Dispose();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.TCPNetworkInterface.ReceiveJob
			{
				ReceiveQueue = arguments.ReceiveQueue,
				InternalData = m_InternalData,
				ConnectionList = m_ConnectionList,
				ConnectionMap = m_ConnectionMap,
				Time = arguments.Time
			}, dep);
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.TCPNetworkInterface.SendJob
			{
				SendQueue = arguments.SendQueue,
				InternalData = m_InternalData,
				ConnectionMap = m_ConnectionMap,
				ConnectionList = m_ConnectionList,
				PendingSends = m_PendingSends
			}, dep);
		}
	}
}
