namespace Unity.Networking.Transport
{
	internal struct WebSocketLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		private struct ConnectionData
		{
			public global::Unity.Networking.Transport.ConnectionId UnderlyingConnectionId;

			public global::Unity.Networking.Transport.WebSocket.State WebSocketState;

			public global::Unity.Networking.Transport.WebSocket.Role Role;

			public global::Unity.Networking.Transport.WebSocket.Buffer SendBuffer;

			public global::Unity.Networking.Transport.WebSocket.Buffer RecvBuffer;

			public global::Unity.Networking.Transport.WebSocket.Payload RecvPayload;

			private byte isReceivingPayload;

			private byte isWaitingForPong;

			public global::Unity.Networking.Transport.WebSocket.Keys Keys;

			public long CreateTimeStamp;

			public long CloseTimeStamp;

			public long ReceiveTimeStamp;

			public bool IsReceivingPayload
			{
				get
				{
					return isReceivingPayload > 0;
				}
				set
				{
					isReceivingPayload = (byte)(value ? 1u : 0u);
				}
			}

			public bool IsWaitingForPong
			{
				get
				{
					return isWaitingForPong > 0;
				}
				set
				{
					isWaitingForPong = (byte)(value ? 1u : 0u);
				}
			}

			public bool IsClient => Role == global::Unity.Networking.Transport.WebSocket.Role.Client;
		}

		[global::Unity.Burst.BurstCompile]
		private struct SendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public global::Unity.Networking.Transport.ConnectionList UnderlyingConnectionList;

			public global::Unity.Networking.Transport.ConnectionList ConnectionList;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.WebSocketLayer.ConnectionData> ConnectionMap;

			public global::Unity.Mathematics.Random Rand;

			public unsafe void Execute()
			{
				if (!UnderlyingConnectionList.IsCreated)
				{
					return;
				}
				int count = SendQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packet = SendQueue[i];
					if (packet.Length == 0 || (ushort)packet.Length > SendQueue.PayloadCapacity - 14)
					{
						continue;
					}
					global::Unity.Networking.Transport.ConnectionId connectionRef = packet.ConnectionRef;
					global::Unity.Networking.Transport.NetworkConnection.State connectionState = ConnectionList.GetConnectionState(connectionRef);
					global::Unity.Networking.Transport.WebSocketLayer.ConnectionData connectionData = ConnectionMap[connectionRef];
					packet.ConnectionRef = connectionData.UnderlyingConnectionId;
					if ((connectionState == global::Unity.Networking.Transport.NetworkConnection.State.Connected && connectionData.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.Open) || (connectionState == global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting && connectionData.WebSocketState != global::Unity.Networking.Transport.WebSocket.State.Closing))
					{
						if (!global::Unity.Networking.Transport.WebSocket.Binary(ref packet, connectionData.IsClient, Rand.NextUInt()))
						{
							packet.Drop();
						}
					}
					else
					{
						packet.Drop();
					}
				}
				count = ConnectionList.Count;
				for (int j = 0; j < count; j++)
				{
					global::Unity.Networking.Transport.ConnectionId connectionId = ConnectionList.ConnectionAt(j);
					if (ConnectionList.GetConnectionState(connectionId) != global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
					{
						global::Unity.Networking.Transport.WebSocketLayer.ConnectionData value = ConnectionMap[connectionId];
						int length = value.SendBuffer.Length;
						int num = length;
						global::Unity.Networking.Transport.NetworkEndpoint connectionEndpoint = ConnectionList.GetConnectionEndpoint(connectionId);
						global::Unity.Networking.Transport.PacketProcessor packetProcessor;
						while (num > 0 && SendQueue.EnqueuePacket(out packetProcessor))
						{
							packetProcessor.ConnectionRef = value.UnderlyingConnectionId;
							packetProcessor.EndpointRef = connectionEndpoint;
							int offset = packetProcessor.Offset - 14;
							int num2 = global::System.Math.Min(num, packetProcessor.BytesAvailableAtEnd + 14);
							packetProcessor.SetUnsafeMetadata(0, offset);
							packetProcessor.AppendToPayload(value.SendBuffer.Data + length - num, num2);
							num -= num2;
						}
						if (num > 0)
						{
							global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(value.SendBuffer.Data, value.SendBuffer.Data + length - num, num);
						}
						value.SendBuffer.Length = num;
						ConnectionMap[connectionId] = value;
					}
				}
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct ReceiveJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			public global::Unity.Networking.Transport.ConnectionList ConnectionList;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.WebSocketLayer.ConnectionData> ConnectionMap;

			public global::Unity.Networking.Transport.UnderlyingConnectionList UnderlyingConnectionList;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.ConnectionId> UnderlyingConnectionMap;

			public global::Unity.Networking.Transport.WebSocket.Settings Settings;

			public global::Unity.Mathematics.Random Rand;

			public long Time;

			private void ProcessUnderlyingDisconnections()
			{
				global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> nativeArray = UnderlyingConnectionList.QueryIncomingDisconnections(global::Unity.Collections.Allocator.Temp);
				int length = nativeArray.Length;
				for (int i = 0; i < length; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connection = nativeArray[i].Connection;
					global::Unity.Networking.Transport.ConnectionId connectionId = UnderlyingConnectionMap[connection];
					if (ConnectionList.ConnectionAt(connectionId.Id) == connectionId)
					{
						global::Unity.Networking.Transport.NetworkConnection.State connectionState = ConnectionList.GetConnectionState(connectionId);
						if (connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting && connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
						{
							ConnectionList.StartDisconnecting(ref connectionId, nativeArray[i].Reason);
							ConnectionList.FinishDisconnecting(ref connectionId);
						}
					}
				}
			}

			private unsafe void ProcessConnectionStates()
			{
				int count = ConnectionList.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connectionId = ConnectionList.ConnectionAt(i);
					switch (ConnectionList.GetConnectionState(connectionId))
					{
					case global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting:
					{
						global::Unity.Networking.Transport.WebSocketLayer.ConnectionData value3 = ConnectionMap[connectionId];
						switch (value3.WebSocketState)
						{
						case global::Unity.Networking.Transport.WebSocket.State.Closed:
							if (value3.SendBuffer.Length == 0)
							{
								value3.WebSocketState = global::Unity.Networking.Transport.WebSocket.State.ClosedAndFlushed;
							}
							break;
						case global::Unity.Networking.Transport.WebSocket.State.Closing:
							if (Time - value3.CloseTimeStamp > Settings.DisconnectTimeoutMS)
							{
								value3.WebSocketState = global::Unity.Networking.Transport.WebSocket.State.ClosedAndFlushed;
							}
							break;
						case global::Unity.Networking.Transport.WebSocket.State.Opening:
							value3.WebSocketState = global::Unity.Networking.Transport.WebSocket.State.ClosedAndFlushed;
							break;
						case global::Unity.Networking.Transport.WebSocket.State.Open:
							if (global::Unity.Networking.Transport.WebSocket.Close(ref value3.SendBuffer, global::Unity.Networking.Transport.WebSocket.StatusCode.Normal, value3.IsClient, Rand.NextUInt()))
							{
								value3.WebSocketState = global::Unity.Networking.Transport.WebSocket.State.Closing;
								value3.CloseTimeStamp = Time;
							}
							break;
						}
						if (value3.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.ClosedAndFlushed)
						{
							UnderlyingConnectionList.Disconnect(ref value3.UnderlyingConnectionId);
							ConnectionList.FinishDisconnecting(ref connectionId);
						}
						ConnectionMap[connectionId] = value3;
						break;
					}
					case global::Unity.Networking.Transport.NetworkConnection.State.Connecting:
					{
						global::Unity.Networking.Transport.WebSocketLayer.ConnectionData value2 = ConnectionMap[connectionId];
						switch (value2.WebSocketState)
						{
						case global::Unity.Networking.Transport.WebSocket.State.None:
						{
							global::Unity.Networking.Transport.NetworkEndpoint endpoint = ConnectionList.GetConnectionEndpoint(connectionId);
							if (!UnderlyingConnectionList.TryConnect(ref endpoint, ref value2.UnderlyingConnectionId))
							{
								ConnectionMap[connectionId] = value2;
								if (value2.UnderlyingConnectionId.IsCreated)
								{
									UnderlyingConnectionMap[value2.UnderlyingConnectionId] = connectionId;
								}
								goto end_IL_002e;
							}
							value2.WebSocketState = global::Unity.Networking.Transport.WebSocket.State.Opening;
							value2.Role = global::Unity.Networking.Transport.WebSocket.Role.Client;
							ref uint key = ref value2.Keys.Key[0];
							key = Rand.NextUInt();
							value2.Keys.Key[1] = Rand.NextUInt();
							value2.Keys.Key[2] = Rand.NextUInt();
							value2.Keys.Key[3] = Rand.NextUInt();
							value2.CreateTimeStamp = Time;
							global::Unity.Networking.Transport.WebSocket.Connect(ref value2.SendBuffer, ref endpoint, ref value2.Keys, ref Settings.Path);
							break;
						}
						case global::Unity.Networking.Transport.WebSocket.State.Opening:
							if (Time - value2.CreateTimeStamp > Settings.ConnectTimeoutMS)
							{
								ConnectionList.StartDisconnecting(ref connectionId, global::Unity.Networking.Transport.Error.DisconnectReason.MaxConnectionAttempts);
								value2.WebSocketState = global::Unity.Networking.Transport.WebSocket.State.ClosedAndFlushed;
							}
							break;
						}
						ConnectionMap[connectionId] = value2;
						break;
					}
					case global::Unity.Networking.Transport.NetworkConnection.State.Connected:
						{
							global::Unity.Networking.Transport.WebSocketLayer.ConnectionData value = ConnectionMap[connectionId];
							if (value.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.Open && Settings.HeartbeatTimeoutMS > 0 && !value.IsWaitingForPong && Time - value.ReceiveTimeStamp > Settings.HeartbeatTimeoutMS && global::Unity.Networking.Transport.WebSocket.Ping(ref value.SendBuffer, value.IsClient, Rand.NextUInt()))
							{
								value.IsWaitingForPong = true;
							}
							ConnectionMap[connectionId] = value;
							break;
						}
						end_IL_002e:
						break;
					}
				}
			}

			private unsafe void ProcessReceivedMessages()
			{
				int count = ReceiveQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = ReceiveQueue[i];
					if (packetProcessor.Length == 0 || packetProcessor.Length > ReceiveQueue.PayloadCapacity)
					{
						packetProcessor.Drop();
						continue;
					}
					global::Unity.Networking.Transport.ConnectionId connectionRef = packetProcessor.ConnectionRef;
					global::Unity.Networking.Transport.ConnectionId connectionId = UnderlyingConnectionMap[connectionRef];
					if (!connectionId.IsCreated)
					{
						connectionId = ConnectionList.StartConnecting(ref packetProcessor.EndpointRef);
						ConnectionMap[connectionId] = new global::Unity.Networking.Transport.WebSocketLayer.ConnectionData
						{
							UnderlyingConnectionId = connectionRef,
							WebSocketState = global::Unity.Networking.Transport.WebSocket.State.Opening,
							Role = global::Unity.Networking.Transport.WebSocket.Role.Server,
							CreateTimeStamp = Time
						};
						UnderlyingConnectionMap[connectionRef] = connectionId;
					}
					if (ConnectionList.GetConnectionState(connectionId) != global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
					{
						global::Unity.Networking.Transport.WebSocketLayer.ConnectionData connectionData = ConnectionMap[connectionId];
						if (connectionData.WebSocketState != global::Unity.Networking.Transport.WebSocket.State.Closed && connectionData.WebSocketState != global::Unity.Networking.Transport.WebSocket.State.ClosedAndFlushed)
						{
							int available = connectionData.RecvBuffer.Available;
							if (packetProcessor.Length <= available)
							{
								packetProcessor.CopyPayload(connectionData.RecvBuffer.Data + connectionData.RecvBuffer.Length, packetProcessor.Length);
								connectionData.RecvBuffer.Length += packetProcessor.Length;
								if (connectionData.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.Opening)
								{
									connectionData.WebSocketState = global::Unity.Networking.Transport.WebSocket.Handshake(ref connectionData.RecvBuffer, ref connectionData.SendBuffer, connectionData.IsClient, ref connectionData.Keys, ref Settings.Path);
									if (connectionData.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.Closed)
									{
										ConnectionList.StartDisconnecting(ref connectionId, global::Unity.Networking.Transport.Error.DisconnectReason.ProtocolError);
									}
									else if (connectionData.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.Open)
									{
										connectionData.ReceiveTimeStamp = Time;
										if (connectionData.IsClient)
										{
											ConnectionList.FinishConnectingFromLocal(ref connectionId);
										}
										else
										{
											ConnectionList.FinishConnectingFromRemote(ref connectionId);
										}
									}
								}
								if (connectionData.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.Open || connectionData.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.Closing)
								{
									ProcessWebSocketFrames(ref connectionId, ref connectionData);
								}
							}
							else
							{
								ConnectionList.StartDisconnecting(ref connectionId, global::Unity.Networking.Transport.Error.DisconnectReason.ProtocolError);
								connectionData.WebSocketState = global::Unity.Networking.Transport.WebSocket.State.Closed;
							}
						}
						ConnectionMap[connectionId] = connectionData;
					}
					packetProcessor.Drop();
				}
			}

			private unsafe void ProcessWebSocketFrames(ref global::Unity.Networking.Transport.ConnectionId connectionId, ref global::Unity.Networking.Transport.WebSocketLayer.ConnectionData connectionData)
			{
				int length = connectionData.RecvBuffer.Length;
				if (length <= 0)
				{
					return;
				}
				int num = ReceiveQueue.PayloadCapacity - 14;
				int num2 = length;
				fixed (byte* data = connectionData.RecvBuffer.Data)
				{
					byte* ptr = data + length;
					int num3;
					int num7;
					byte* ptr2;
					for (ptr2 = data; ptr2 < ptr && ptr - ptr2 >= 2; ptr2 += num7, num2 -= num3, num2 -= num7, connectionData.ReceiveTimeStamp = Time)
					{
						num3 = 2;
						int num4 = ptr2[1] & 0x7F;
						if ((ptr2[1] & 0x80) != 0)
						{
							num3 += 4;
						}
						switch (num4)
						{
						case 126:
							num3 += 2;
							break;
						case 127:
							num3 += 8;
							break;
						}
						if (ptr - ptr2 < num3)
						{
							break;
						}
						bool isClient = connectionData.IsClient;
						bool flag = (*ptr2 & 0x80) == 0;
						int num5 = *ptr2 & 0xF;
						bool flag2 = (ptr2[1] & 0x80) != 0;
						bool flag3 = (*ptr2 & 0x70) != 0;
						bool flag4 = flag2 == isClient;
						bool flag5 = flag && num5 != 0 && num5 != 2;
						if (flag3 || flag4 || flag5)
						{
							Abort(ref connectionId, ref connectionData, global::Unity.Networking.Transport.WebSocket.StatusCode.ProtocolError, isClient);
							return;
						}
						ulong num6 = 0uL;
						num6 = num4 switch
						{
							127 => ((ulong)ptr2[6] << 56) + ((ulong)ptr2[7] << 48) + ((ulong)ptr2[6] << 40) + ((ulong)ptr2[7] << 32) + ((ulong)ptr2[6] << 24) + ((ulong)ptr2[7] << 16) + ((ulong)ptr2[8] << 8) + ptr2[9], 
							126 => ((ulong)ptr2[2] << 8) + ptr2[3], 
							_ => (ulong)num4, 
						};
						if (num6 > (ulong)num)
						{
							Abort(ref connectionId, ref connectionData, global::Unity.Networking.Transport.WebSocket.StatusCode.MessageTooBig, isClient);
							return;
						}
						num7 = (int)num6;
						ptr2 += num3;
						if (ptr - ptr2 < num7)
						{
							break;
						}
						if (flag2)
						{
							byte* ptr3 = ptr2 - 4;
							for (int i = 0; i < num7; i++)
							{
								ptr2[i] ^= ptr3[i & 3];
							}
						}
						switch (num5)
						{
						case 0:
						{
							bool flag6 = !connectionData.IsReceivingPayload;
							bool flag7 = connectionData.RecvPayload.Length + num7 > num;
							if (flag6 || flag7)
							{
								global::Unity.Networking.Transport.WebSocket.StatusCode status2 = (flag6 ? global::Unity.Networking.Transport.WebSocket.StatusCode.ProtocolError : global::Unity.Networking.Transport.WebSocket.StatusCode.MessageTooBig);
								Abort(ref connectionId, ref connectionData, status2, isClient);
								return;
							}
							if (num7 <= 0)
							{
								continue;
							}
							fixed (byte* data3 = connectionData.RecvPayload.Data)
							{
								global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(data3 + connectionData.RecvPayload.Length, ptr2, num7);
								connectionData.RecvPayload.Length += num7;
								if (!flag)
								{
									if (!ReceiveQueue.EnqueuePacket(out var packetProcessor2))
									{
										break;
									}
									packetProcessor2.ConnectionRef = connectionId;
									packetProcessor2.EndpointRef = ConnectionList.GetConnectionEndpoint(connectionId);
									packetProcessor2.AppendToPayload(data3, connectionData.RecvPayload.Length);
									connectionData.RecvPayload.Length = 0;
									connectionData.IsReceivingPayload = false;
								}
							}
							continue;
						}
						case 1:
							Abort(ref connectionId, ref connectionData, global::Unity.Networking.Transport.WebSocket.StatusCode.UnsupportedDataType, isClient);
							return;
						case 2:
						{
							if (num7 <= 0)
							{
								continue;
							}
							if (flag)
							{
								fixed (byte* data2 = connectionData.RecvPayload.Data)
								{
									global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(data2 + connectionData.RecvPayload.Length, ptr2, num7);
									connectionData.RecvPayload.Length = num7;
									connectionData.IsReceivingPayload = true;
								}
								continue;
							}
							if (connectionData.IsReceivingPayload)
							{
								Abort(ref connectionId, ref connectionData, global::Unity.Networking.Transport.WebSocket.StatusCode.ProtocolError, isClient);
								return;
							}
							if (ReceiveQueue.EnqueuePacket(out var packetProcessor))
							{
								packetProcessor.ConnectionRef = connectionId;
								packetProcessor.EndpointRef = ConnectionList.GetConnectionEndpoint(connectionId);
								packetProcessor.AppendToPayload(ptr2, num7);
								continue;
							}
							break;
						}
						case 8:
							if (connectionData.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.Closing)
							{
								connectionData.WebSocketState = global::Unity.Networking.Transport.WebSocket.State.ClosedAndFlushed;
								UnderlyingConnectionList.Disconnect(ref connectionData.UnderlyingConnectionId);
								ConnectionList.FinishDisconnecting(ref connectionId);
							}
							else
							{
								global::Unity.Networking.Transport.WebSocket.StatusCode status = ((num7 > 1) ? ((global::Unity.Networking.Transport.WebSocket.StatusCode)((*ptr2 << 8) + ptr2[1])) : global::Unity.Networking.Transport.WebSocket.StatusCode.Normal);
								Abort(ref connectionId, ref connectionData, status, isClient);
							}
							return;
						case 9:
							if (connectionData.WebSocketState == global::Unity.Networking.Transport.WebSocket.State.Closing || global::Unity.Networking.Transport.WebSocket.Pong(ref connectionData.SendBuffer, ptr2, num7, isClient, Rand.NextUInt()))
							{
								continue;
							}
							Abort(ref connectionId, ref connectionData, global::Unity.Networking.Transport.WebSocket.StatusCode.InternalError, isClient);
							return;
						case 10:
							connectionData.IsWaitingForPong = false;
							continue;
						default:
							Abort(ref connectionId, ref connectionData, global::Unity.Networking.Transport.WebSocket.StatusCode.ProtocolError, isClient);
							return;
						}
						break;
					}
					if (num2 > 0 && num2 != length)
					{
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(data, data + length - num2, num2);
					}
				}
				connectionData.RecvBuffer.Length = num2;
			}

			private void Abort(ref global::Unity.Networking.Transport.ConnectionId connectionId, ref global::Unity.Networking.Transport.WebSocketLayer.ConnectionData connectionData, global::Unity.Networking.Transport.WebSocket.StatusCode status, bool isClient)
			{
				if (connectionData.WebSocketState != global::Unity.Networking.Transport.WebSocket.State.Closing)
				{
					global::Unity.Networking.Transport.WebSocket.Close(ref connectionData.SendBuffer, status, isClient, Rand.NextUInt());
				}
				global::Unity.Networking.Transport.NetworkConnection.State connectionState = ConnectionList.GetConnectionState(connectionId);
				if (connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnected && connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting)
				{
					ConnectionList.StartDisconnecting(ref connectionId, global::Unity.Networking.Transport.Error.DisconnectReason.ProtocolError);
				}
				connectionData.WebSocketState = global::Unity.Networking.Transport.WebSocket.State.Closed;
			}

			public void Execute()
			{
				ProcessReceivedMessages();
				ProcessUnderlyingDisconnections();
				ProcessConnectionStates();
			}
		}

		private global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.WebSocketLayer.ConnectionData> m_ConnectionMap;

		private global::Unity.Networking.Transport.ConnectionList m_ConnectionList;

		private global::Unity.Networking.Transport.ConnectionList m_UnderlyingConnectionList;

		private global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.ConnectionId> m_UnderlyingConnectionMap;

		private global::Unity.Networking.Transport.WebSocket.Settings m_Settings;

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void Warn(string msg)
		{
			global::UnityEngine.Debug.LogWarning(msg);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void WarnIf(bool condition, string msg)
		{
			if (condition)
			{
				global::UnityEngine.Debug.LogWarning(msg);
			}
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			packetPadding += 14;
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = settings.GetNetworkConfigParameters();
			m_Settings = new global::Unity.Networking.Transport.WebSocket.Settings
			{
				Path = settings.GetWebSocketParameters().Path,
				ConnectTimeoutMS = global::System.Math.Max(0, networkConfigParameters.connectTimeoutMS),
				DisconnectTimeoutMS = global::System.Math.Max(0, networkConfigParameters.disconnectTimeoutMS),
				HeartbeatTimeoutMS = global::System.Math.Max(0, networkConfigParameters.heartbeatTimeoutMS)
			};
			if (connectionList.IsCreated)
			{
				m_UnderlyingConnectionList = connectionList;
			}
			m_ConnectionList = (connectionList = global::Unity.Networking.Transport.ConnectionList.Create());
			m_ConnectionMap = new global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.WebSocketLayer.ConnectionData>(1, default(global::Unity.Networking.Transport.WebSocketLayer.ConnectionData), global::Unity.Collections.Allocator.Persistent);
			m_UnderlyingConnectionMap = new global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.ConnectionId>(1, default(global::Unity.Networking.Transport.ConnectionId), global::Unity.Collections.Allocator.Persistent);
			return 0;
		}

		public void Dispose()
		{
			m_ConnectionList.Dispose();
			m_ConnectionMap.Dispose();
			m_UnderlyingConnectionMap.Dispose();
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.WebSocketLayer.SendJob
			{
				SendQueue = arguments.SendQueue,
				UnderlyingConnectionList = m_UnderlyingConnectionList,
				ConnectionList = m_ConnectionList,
				ConnectionMap = m_ConnectionMap,
				Rand = new global::Unity.Mathematics.Random((uint)global::Unity.Networking.Transport.Utilities.TimerHelpers.GetTicks())
			}, dep);
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.WebSocketLayer.ReceiveJob
			{
				ReceiveQueue = arguments.ReceiveQueue,
				UnderlyingConnectionList = new global::Unity.Networking.Transport.UnderlyingConnectionList(ref m_UnderlyingConnectionList),
				ConnectionList = m_ConnectionList,
				ConnectionMap = m_ConnectionMap,
				UnderlyingConnectionMap = m_UnderlyingConnectionMap,
				Settings = m_Settings,
				Rand = new global::Unity.Mathematics.Random((uint)global::Unity.Networking.Transport.Utilities.TimerHelpers.GetTicks()),
				Time = arguments.Time
			}, dep);
		}
	}
}
