namespace Unity.Networking.Transport
{
	public struct NetworkDriver : global::System.IDisposable
	{
		public struct Concurrent
		{
			internal struct PendingSend
			{
				public global::Unity.Networking.Transport.NetworkPipeline Pipeline;

				public global::Unity.Networking.Transport.NetworkConnection Connection;

				public global::Unity.Networking.Transport.NetworkInterfaceSendHandle SendHandle;
			}

			internal global::Unity.Networking.Transport.NetworkEventQueue.Concurrent m_EventQueue;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Networking.Transport.ConnectionList m_ConnectionList;

			internal global::Unity.Networking.Transport.NetworkPipelineProcessor.Concurrent m_PipelineProcessor;

			internal global::Unity.Networking.Transport.NetworkDriverSender.Concurrent m_DriverSender;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Networking.Transport.NetworkDriverReceiver m_DriverReceiver;

			internal int m_PacketPadding;

			public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader)
			{
				global::Unity.Networking.Transport.NetworkPipeline pipe;
				return PopEventForConnection(connection, out reader, out pipe);
			}

			public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader, out global::Unity.Networking.Transport.NetworkPipeline pipe)
			{
				pipe = default(global::Unity.Networking.Transport.NetworkPipeline);
				reader = default(global::Unity.Collections.DataStreamReader);
				if (m_ConnectionList.ConnectionAt(connection.InternalId) != connection.ConnectionId)
				{
					return global::Unity.Networking.Transport.NetworkEvent.Type.Empty;
				}
				int offset;
				int size;
				int pipelineId;
				global::Unity.Networking.Transport.NetworkEvent.Type result = m_EventQueue.PopEventForConnection(connection.InternalId, out offset, out size, out pipelineId);
				pipe = new global::Unity.Networking.Transport.NetworkPipeline
				{
					Id = pipelineId
				};
				if (size > 0)
				{
					reader = new global::Unity.Collections.DataStreamReader(m_DriverReceiver.GetDataStreamSubArray(offset, size));
				}
				return result;
			}

			public int MaxHeaderSize(global::Unity.Networking.Transport.NetworkPipeline pipe)
			{
				int num = m_PipelineProcessor.m_MaxPacketHeaderSize;
				if (pipe.Id > 0)
				{
					num += m_PipelineProcessor.SendHeaderCapacity(pipe);
				}
				return num;
			}

			public int BeginSend(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamWriter writer, int requiredPayloadSize = 0)
			{
				return BeginSend(global::Unity.Networking.Transport.NetworkPipeline.Null, connection, out writer, requiredPayloadSize);
			}

			public unsafe int BeginSend(global::Unity.Networking.Transport.NetworkPipeline pipe, global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamWriter writer, int requiredPayloadSize = 0)
			{
				writer = default(global::Unity.Collections.DataStreamWriter);
				int num = GetMaxSupportedPayloadSize(connection, pipe);
				if (num < 0)
				{
					return num;
				}
				if (num < requiredPayloadSize)
				{
					return -4;
				}
				int num2 = ((pipe.Id > 0) ? m_PipelineProcessor.SendHeaderCapacity(pipe) : 0);
				int num3 = num + m_PacketPadding + num2;
				if (requiredPayloadSize > 0 && num > requiredPayloadSize)
				{
					int num4 = num - requiredPayloadSize;
					num -= num4;
					num3 -= num4;
				}
				global::Unity.Networking.Transport.NetworkInterfaceSendHandle sendHandle = default(global::Unity.Networking.Transport.NetworkInterfaceSendHandle);
				if (num3 > GetMaxSupportedMessageSize(connection))
				{
					sendHandle.data = (global::System.IntPtr)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(num3, 8, global::Unity.Collections.Allocator.Temp);
					sendHandle.capacity = num3;
					sendHandle.id = 0;
					sendHandle.size = 0;
					sendHandle.flags = global::Unity.Networking.Transport.SendHandleFlags.AllocatedByDriver;
				}
				else
				{
					int num5 = 0;
					if ((num5 = m_DriverSender.BeginSend(out sendHandle, (uint)num3)) != 0)
					{
						return num5;
					}
				}
				if (sendHandle.capacity < num3)
				{
					return -4;
				}
				global::Unity.Collections.NativeArray<byte> data = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((byte*)(void*)sendHandle.data + m_PacketPadding + num2, num, global::Unity.Collections.Allocator.Invalid);
				writer = new global::Unity.Collections.DataStreamWriter(data);
				writer.m_SendHandleData = (global::System.IntPtr)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend>(), global::Unity.Collections.Allocator.Temp);
				*(global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend*)(void*)writer.m_SendHandleData = new global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend
				{
					Pipeline = pipe,
					Connection = connection,
					SendHandle = sendHandle
				};
				return 0;
			}

			public int EndSend(global::Unity.Collections.DataStreamWriter writer)
			{
				int num = ExtractPendingSendFromWriter(writer, out var pendingSend);
				if (num != 0)
				{
					return num;
				}
				if (writer.HasFailedWrites)
				{
					AbortSend(pendingSend.SendHandle);
					return -4;
				}
				pendingSend.SendHandle.size = m_PacketPadding + writer.Length;
				if (pendingSend.Pipeline.Id > 0)
				{
					pendingSend.SendHandle.size += m_PipelineProcessor.SendHeaderCapacity(pendingSend.Pipeline);
					num = m_PipelineProcessor.Send(this, pendingSend.Pipeline, pendingSend.Connection, pendingSend.SendHandle, m_PacketPadding);
				}
				else
				{
					num = CompleteSend(pendingSend.Connection, ref pendingSend.SendHandle);
					PrependPipelineByte(pendingSend.SendHandle, 0);
				}
				if (num >= 0)
				{
					return writer.Length;
				}
				return num;
			}

			internal unsafe int ExtractPendingSendFromWriter(global::Unity.Collections.DataStreamWriter writer, out global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend pendingSend)
			{
				pendingSend = default(global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend);
				global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend* ptr = (global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend*)(void*)writer.m_SendHandleData;
				if (ptr == null || ptr->Connection == default(global::Unity.Networking.Transport.NetworkConnection))
				{
					return -8;
				}
				if (m_ConnectionList.ConnectionAt(ptr->Connection.InternalId).Version != ptr->Connection.Version)
				{
					return -2;
				}
				pendingSend = *ptr;
				ptr->Connection = default(global::Unity.Networking.Transport.NetworkConnection);
				return 0;
			}

			internal unsafe int CompleteSend(global::Unity.Networking.Transport.NetworkConnection connection, ref global::Unity.Networking.Transport.NetworkInterfaceSendHandle sendHandle)
			{
				if ((sendHandle.flags & global::Unity.Networking.Transport.SendHandleFlags.AllocatedByDriver) != 0)
				{
					global::Unity.Networking.Transport.NetworkInterfaceSendHandle networkInterfaceSendHandle = sendHandle;
					int packetSize = global::Unity.Mathematics.math.max(GetMaxSupportedMessageSize(connection), networkInterfaceSendHandle.size);
					int num = m_DriverSender.BeginSend(out sendHandle, (uint)packetSize);
					if (num != 0)
					{
						return num;
					}
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy((void*)sendHandle.data, (void*)networkInterfaceSendHandle.data, networkInterfaceSendHandle.size);
					sendHandle.size = networkInterfaceSendHandle.size;
				}
				global::Unity.Networking.Transport.NetworkEndpoint destination = m_ConnectionList.GetConnectionEndpoint(connection.ConnectionId);
				sendHandle.size -= m_PacketPadding;
				m_DriverSender.EndSend(ref destination, ref sendHandle, m_PacketPadding, connection.ConnectionId);
				return 0;
			}

			internal void PrependPipelineByte(global::Unity.Networking.Transport.NetworkInterfaceSendHandle sendHandle, byte pipeline)
			{
				m_DriverSender.m_SendQueue.GetPacketProcessor(sendHandle.id).PrependToPayload(pipeline);
			}

			public void AbortSend(global::Unity.Collections.DataStreamWriter writer)
			{
				if (ExtractPendingSendFromWriter(writer, out var pendingSend) != 0)
				{
					global::UnityEngine.Debug.LogError("Invalid call to AbortSend. Either there is no matching BeginSend call or the connection was closed since.");
				}
				AbortSend(pendingSend.SendHandle);
			}

			internal void AbortSend(global::Unity.Networking.Transport.NetworkInterfaceSendHandle sendHandle)
			{
				if ((sendHandle.flags & global::Unity.Networking.Transport.SendHandleFlags.AllocatedByDriver) == 0)
				{
					m_DriverSender.AbortSend(ref sendHandle);
				}
			}

			public global::Unity.Networking.Transport.NetworkConnection.State GetConnectionState(global::Unity.Networking.Transport.NetworkConnection id)
			{
				if (id.InternalId < 0 || id.InternalId >= m_ConnectionList.Count)
				{
					return global::Unity.Networking.Transport.NetworkConnection.State.Disconnected;
				}
				global::Unity.Networking.Transport.ConnectionId connectionId = m_ConnectionList.ConnectionAt(id.InternalId);
				if (connectionId.Version != id.Version)
				{
					return global::Unity.Networking.Transport.NetworkConnection.State.Disconnected;
				}
				global::Unity.Networking.Transport.NetworkConnection.State connectionState = m_ConnectionList.GetConnectionState(connectionId);
				if (connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting)
				{
					return connectionState;
				}
				return global::Unity.Networking.Transport.NetworkConnection.State.Disconnected;
			}

			public int GetMaxSupportedMessageSize(global::Unity.Networking.Transport.NetworkConnection connection)
			{
				if (connection.InternalId < 0 || connection.InternalId >= m_ConnectionList.Count)
				{
					return -1;
				}
				if (m_ConnectionList.ConnectionAt(connection.InternalId).Version != connection.Version)
				{
					return -2;
				}
				if (m_ConnectionList.GetConnectionState(connection.ConnectionId) != global::Unity.Networking.Transport.NetworkConnection.State.Connected)
				{
					return -3;
				}
				return m_ConnectionList.GetConnectionPathMtu(connection.ConnectionId);
			}

			public int GetMaxSupportedPayloadSize(global::Unity.Networking.Transport.NetworkConnection connection, global::Unity.Networking.Transport.NetworkPipeline pipe)
			{
				int maxSupportedMessageSize = GetMaxSupportedMessageSize(connection);
				if (maxSupportedMessageSize < 0)
				{
					return maxSupportedMessageSize;
				}
				int num = ((pipe.Id > 0) ? m_PipelineProcessor.SendHeaderCapacity(pipe) : 0);
				int num2 = m_PipelineProcessor.PayloadCapacity(pipe);
				if (num2 != 0)
				{
					return num2;
				}
				return maxSupportedMessageSize - m_PacketPadding - num;
			}
		}

		private struct InternalState
		{
			public long LastUpdateTime;

			public long UpdateTimeAdjustment;

			public bool Bound;

			public bool Listening;
		}

		[global::Unity.Burst.BurstCompile]
		private struct UpdateJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.NetworkDriver driver;

			public void Execute()
			{
				driver.InternalUpdate();
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct ClearEventQueue : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.NetworkEventQueue eventQueue;

			public global::Unity.Networking.Transport.NetworkDriverReceiver driverReceiver;

			public void Execute()
			{
				eventQueue.Clear();
				driverReceiver.ClearStream();
			}
		}

		internal global::Unity.Networking.Transport.NetworkStack m_NetworkStack;

		private global::Unity.Networking.Transport.NetworkDriverSender m_DriverSender;

		private global::Unity.Networking.Transport.NetworkDriverReceiver m_DriverReceiver;

		private global::Unity.Networking.Transport.NetworkEventQueue m_EventQueue;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		private global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.NetworkDriver.InternalState> m_InternalState;

		private global::Unity.Networking.Transport.NetworkPipelineProcessor m_PipelineProcessor;

		[global::Unity.Collections.ReadOnly]
		internal global::Unity.Networking.Transport.NetworkSettings m_NetworkSettings;

		private global::Unity.Collections.NativeHashMap<global::Unity.Networking.Transport.ConnectionId, global::Unity.Networking.Transport.ConnectionPayload> m_ConnectionPayloads;

		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkConnection> m_HostnameLookups;

		internal global::Unity.Networking.Transport.NetworkDriverReceiver Receiver => m_DriverReceiver;

		internal global::Unity.Networking.Transport.NetworkEventQueue EventQueue => m_EventQueue;

		public global::Unity.Networking.Transport.NetworkSettings CurrentSettings => m_NetworkSettings.AsReadOnly();

		public bool Bound
		{
			get
			{
				if (!IsCreated)
				{
					return false;
				}
				return m_InternalState.Value.Bound;
			}
			private set
			{
				if (IsCreated)
				{
					global::Unity.Networking.Transport.NetworkDriver.InternalState value2 = m_InternalState.Value;
					value2.Bound = value;
					m_InternalState.Value = value2;
				}
			}
		}

		public bool Listening
		{
			get
			{
				if (!IsCreated)
				{
					return false;
				}
				return m_InternalState.Value.Listening;
			}
			private set
			{
				if (IsCreated)
				{
					global::Unity.Networking.Transport.NetworkDriver.InternalState value2 = m_InternalState.Value;
					value2.Listening = value;
					m_InternalState.Value = value2;
				}
			}
		}

		internal long LastUpdateTime => m_InternalState.Value.LastUpdateTime;

		internal int PipelineCount => m_PipelineProcessor.PipelineCount;

		public bool IsCreated => m_InternalState.IsCreated;

		public int ReceiveErrorCode => m_DriverReceiver.Result.ErrorCode;

		public global::Unity.Networking.Transport.NetworkDriver.Concurrent ToConcurrent()
		{
			if (!IsCreated)
			{
				return default(global::Unity.Networking.Transport.NetworkDriver.Concurrent);
			}
			return new global::Unity.Networking.Transport.NetworkDriver.Concurrent
			{
				m_EventQueue = m_EventQueue.ToConcurrent(),
				m_ConnectionList = m_NetworkStack.Connections,
				m_PipelineProcessor = m_PipelineProcessor.ToConcurrent(),
				m_DriverSender = m_DriverSender.ToConcurrent(),
				m_DriverReceiver = m_DriverReceiver,
				m_PacketPadding = m_NetworkStack.PacketPadding
			};
		}

		private global::Unity.Networking.Transport.NetworkDriver.Concurrent ToConcurrentSendOnly()
		{
			return new global::Unity.Networking.Transport.NetworkDriver.Concurrent
			{
				m_EventQueue = default(global::Unity.Networking.Transport.NetworkEventQueue.Concurrent),
				m_ConnectionList = m_NetworkStack.Connections,
				m_PipelineProcessor = m_PipelineProcessor.ToConcurrent(),
				m_DriverSender = m_DriverSender.ToConcurrent(),
				m_DriverReceiver = m_DriverReceiver,
				m_PacketPadding = m_NetworkStack.PacketPadding
			};
		}

		public static global::Unity.Networking.Transport.NetworkDriver Create(global::Unity.Networking.Transport.NetworkSettings settings)
		{
			return Create(default(global::Unity.Networking.Transport.UDPNetworkInterface), settings);
		}

		public static global::Unity.Networking.Transport.NetworkDriver Create()
		{
			return Create(new global::Unity.Networking.Transport.NetworkSettings(global::Unity.Collections.Allocator.Temp));
		}

		public static global::Unity.Networking.Transport.NetworkDriver Create<N>(N networkInterface) where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
		{
			return Create(ref networkInterface);
		}

		public static global::Unity.Networking.Transport.NetworkDriver Create<N>(ref N networkInterface) where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
		{
			return Create(ref networkInterface, new global::Unity.Networking.Transport.NetworkSettings(global::Unity.Collections.Allocator.Temp));
		}

		public static global::Unity.Networking.Transport.NetworkDriver Create<N>(N networkInterface, global::Unity.Networking.Transport.NetworkSettings settings) where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
		{
			return Create(ref networkInterface, settings);
		}

		public static global::Unity.Networking.Transport.NetworkDriver Create<N>(ref N networkInterface, global::Unity.Networking.Transport.NetworkSettings settings) where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
		{
			global::Unity.Networking.Transport.NetworkDriver result = default(global::Unity.Networking.Transport.NetworkDriver);
			if (settings.TryGet<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>(out var parameter))
			{
				long hashCode = global::Unity.Burst.BurstRuntime.GetHashCode64<global::Unity.Networking.Transport.WebSocketNetworkInterface>();
				long hashCode2 = global::Unity.Burst.BurstRuntime.GetHashCode64<N>();
				if (parameter.ServerData.IsWebSocket == 1 && hashCode2 != hashCode)
				{
					global::UnityEngine.Debug.LogError("Relay is configured to use WebSockets, but NetworkDriver uses UDP. Make sure to pass WebSocketNetworkInterface as the first parameter when calling NetworkDriver.Create().");
					throw new global::System.ArgumentException("Mismatched Relay configuration and network interface.");
				}
				if (parameter.ServerData.IsWebSocket == 0 && hashCode2 == hashCode)
				{
					global::UnityEngine.Debug.LogError("Relay is configured to use UDP, but NetworkDriver was created with WebSocketNetworkInterface.If usage of WebSockets is intended, the Relay allocation should be created with the \"wss\" connection type.");
					throw new global::System.ArgumentException("Mismatched Relay configuration and network interface.");
				}
			}
			result.m_NetworkSettings = new global::Unity.Networking.Transport.NetworkSettings(settings, global::Unity.Collections.Allocator.Persistent);
			global::Unity.Networking.Transport.NetworkConfigParameter parameter2 = settings.GetNetworkConfigParameters();
			if (settings.TryGet<global::Unity.Networking.Transport.BaselibNetworkParameter>(out var parameter3) && parameter2.sendQueueCapacity == 512 && parameter2.receiveQueueCapacity == 512)
			{
				parameter2.sendQueueCapacity = parameter3.sendQueueCapacity;
				parameter2.receiveQueueCapacity = parameter3.receiveQueueCapacity;
				result.m_NetworkSettings.AddRawParameterStruct(ref parameter2);
			}
			global::Unity.Networking.Transport.NetworkStack.InitializeForSettings(out result.m_NetworkStack, ref networkInterface, ref settings, out var sendQueue, out var receiveQueue);
			result.m_PipelineProcessor = new global::Unity.Networking.Transport.NetworkPipelineProcessor(settings, result.m_NetworkStack.PacketPadding);
			result.m_DriverSender = new global::Unity.Networking.Transport.NetworkDriverSender(sendQueue);
			result.m_DriverReceiver = new global::Unity.Networking.Transport.NetworkDriverReceiver(receiveQueue);
			result.m_EventQueue = new global::Unity.Networking.Transport.NetworkEventQueue(100);
			long currentTimestampMS = global::Unity.Networking.Transport.Utilities.TimerHelpers.GetCurrentTimestampMS();
			global::Unity.Networking.Transport.NetworkDriver.InternalState value = new global::Unity.Networking.Transport.NetworkDriver.InternalState
			{
				LastUpdateTime = ((parameter2.fixedFrameTimeMS > 0) ? 1 : currentTimestampMS),
				UpdateTimeAdjustment = 0L,
				Bound = false,
				Listening = false
			};
			result.m_InternalState = new global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.NetworkDriver.InternalState>(value, global::Unity.Collections.Allocator.Persistent);
			result.m_ConnectionPayloads = new global::Unity.Collections.NativeHashMap<global::Unity.Networking.Transport.ConnectionId, global::Unity.Networking.Transport.ConnectionPayload>(1, global::Unity.Collections.Allocator.Persistent);
			result.m_HostnameLookups = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkConnection>(1, global::Unity.Collections.Allocator.Persistent);
			return result;
		}

		[global::System.Obsolete("Use NetworkDriver.Create(INetworkInterface networkInterface) instead.", true)]
		public NetworkDriver(global::Unity.Networking.Transport.INetworkInterface netIf)
		{
			throw new global::System.NotImplementedException();
		}

		[global::System.Obsolete("Use NetworkDriver.Create(INetworkInterface networkInterface, NetworkSettings settings) instead.", true)]
		public NetworkDriver(global::Unity.Networking.Transport.INetworkInterface netIf, global::Unity.Networking.Transport.NetworkSettings settings)
		{
			throw new global::System.NotImplementedException();
		}

		public void Dispose()
		{
			if (IsCreated)
			{
				m_NetworkStack.Dispose();
				m_DriverSender.Dispose();
				m_DriverReceiver.Dispose();
				m_NetworkSettings.Dispose();
				m_PipelineProcessor.Dispose();
				m_EventQueue.Dispose();
				m_InternalState.Dispose();
				m_ConnectionPayloads.Dispose();
				m_HostnameLookups.Dispose();
			}
		}

		private void UpdateLastUpdateTime()
		{
			global::Unity.Networking.Transport.NetworkDriver.InternalState value = m_InternalState.Value;
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = m_NetworkSettings.GetNetworkConfigParameters();
			long num = ((networkConfigParameters.fixedFrameTimeMS > 0) ? (LastUpdateTime + networkConfigParameters.fixedFrameTimeMS) : (global::Unity.Networking.Transport.Utilities.TimerHelpers.GetCurrentTimestampMS() - value.UpdateTimeAdjustment));
			long num2 = num - LastUpdateTime;
			if (networkConfigParameters.maxFrameTimeMS > 0 && num2 > networkConfigParameters.maxFrameTimeMS)
			{
				value.UpdateTimeAdjustment += num2 - networkConfigParameters.maxFrameTimeMS;
				num = LastUpdateTime + networkConfigParameters.maxFrameTimeMS;
			}
			value.LastUpdateTime = num;
			m_InternalState.Value = value;
		}

		public global::Unity.Jobs.JobHandle ScheduleUpdate(global::Unity.Jobs.JobHandle dependency = default(global::Unity.Jobs.JobHandle))
		{
			UpdateLastUpdateTime();
			global::Unity.Networking.Transport.NetworkDriver.UpdateJob jobData = new global::Unity.Networking.Transport.NetworkDriver.UpdateJob
			{
				driver = this
			};
			if (Bound)
			{
				global::Unity.Networking.Transport.ConnectionList connectionList = m_NetworkStack.Connections;
				global::Unity.Jobs.JobHandle dependsOn = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.NetworkDriver.ClearEventQueue
				{
					eventQueue = m_EventQueue,
					driverReceiver = m_DriverReceiver
				}, dependency);
				dependsOn = global::Unity.Jobs.IJobExtensions.Schedule(jobData, dependsOn);
				dependsOn = m_NetworkStack.ScheduleReceive(ref m_DriverReceiver, ref connectionList, ref m_EventQueue, ref m_PipelineProcessor, ref m_ConnectionPayloads, LastUpdateTime, dependsOn);
				return m_NetworkStack.ScheduleSend(ref m_DriverSender, LastUpdateTime, dependsOn);
			}
			return global::Unity.Jobs.IJobExtensions.Schedule(jobData, dependency);
		}

		public global::Unity.Jobs.JobHandle ScheduleFlushSend(global::Unity.Jobs.JobHandle dependency = default(global::Unity.Jobs.JobHandle))
		{
			if (!Bound)
			{
				return dependency;
			}
			return m_NetworkStack.ScheduleSend(ref m_DriverSender, LastUpdateTime, dependency);
		}

		private void InternalUpdate()
		{
			m_PipelineProcessor.Timestamp = LastUpdateTime;
			m_PipelineProcessor.UpdateReceive(ref this);
			m_PipelineProcessor.UpdateSend(ToConcurrentSendOnly());
			for (int num = m_HostnameLookups.Length - 1; num >= 0; num--)
			{
				if (CheckHostnameLookupStatus(m_HostnameLookups[num]))
				{
					m_HostnameLookups.RemoveAt(num);
				}
			}
			if (!Listening)
			{
				global::Unity.Networking.Transport.ConnectionId connectionId;
				while ((connectionId = m_NetworkStack.Connections.AcceptConnection()) != default(global::Unity.Networking.Transport.ConnectionId))
				{
					Disconnect(new global::Unity.Networking.Transport.NetworkConnection(connectionId));
				}
			}
		}

		public void RegisterPipelineStage<T>(T stage) where T : unmanaged, global::Unity.Networking.Transport.INetworkPipelineStage
		{
			m_PipelineProcessor.RegisterPipelineStage(stage, m_NetworkSettings);
		}

		public global::Unity.Networking.Transport.NetworkPipeline CreatePipeline(params global::System.Type[] stages)
		{
			global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId> stages2 = new global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId>(stages.Length, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < stages.Length; i++)
			{
				stages2[i] = global::Unity.Networking.Transport.NetworkPipelineStageId.Get(stages[i]);
			}
			return CreatePipeline(stages2);
		}

		public global::Unity.Networking.Transport.NetworkPipeline CreatePipeline(global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId> stages)
		{
			return m_PipelineProcessor.CreatePipeline(stages);
		}

		public int Bind(global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			if (m_NetworkStack.TryGetLayer<global::Unity.Networking.Transport.RelayLayer>(out var _) && endpoint.Port != 0)
			{
				global::UnityEngine.Debug.LogWarning("When using Unity Relay, NetworkDriver should be bound to 0.0.0.0:0 (i.e. NetworkEndpoint.AnyIpv4).");
			}
			int num = m_NetworkStack.Bind(ref endpoint);
			Bound = num == 0;
			return num;
		}

		public int Listen()
		{
			if (!Bound)
			{
				return -1;
			}
			int num = m_NetworkStack.Listen();
			Listening = num == 0;
			return num;
		}

		[global::System.Obsolete("The correct way to stop listening is disposing of the driver (and recreating a new one).")]
		internal void StopListening()
		{
			Listening = false;
		}

		public unsafe global::Unity.Networking.Transport.NetworkConnection Accept(out global::Unity.Collections.NativeArray<byte> payload)
		{
			payload = default(global::Unity.Collections.NativeArray<byte>);
			if (!Listening)
			{
				return default(global::Unity.Networking.Transport.NetworkConnection);
			}
			global::Unity.Networking.Transport.ConnectionId connectionId = m_NetworkStack.Connections.AcceptConnection();
			if (m_ConnectionPayloads.TryGetValue(connectionId, out var item))
			{
				payload = new global::Unity.Collections.NativeArray<byte>(item.Length, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(payload), item.Data, item.Length);
				m_ConnectionPayloads.Remove(connectionId);
			}
			return new global::Unity.Networking.Transport.NetworkConnection(connectionId);
		}

		public global::Unity.Networking.Transport.NetworkConnection Accept()
		{
			global::Unity.Collections.NativeArray<byte> payload;
			return Accept(out payload);
		}

		internal unsafe void SetPendingPayloadData(global::Unity.Networking.Transport.NetworkConnection connection, global::Unity.Collections.NativeArray<byte> payload)
		{
			int num = m_NetworkStack.PacketPadding + 13 + 2;
			int num2 = m_DriverSender.SendQueue.PayloadCapacity - num;
			if (payload.Length > num2)
			{
				global::UnityEngine.Debug.LogError($"Payload provided to Connect() call is too large ({payload.Length} bytes, maximum is {num2}). Ignoring.");
				return;
			}
			global::Unity.Networking.Transport.ConnectionPayload value = new global::Unity.Networking.Transport.ConnectionPayload
			{
				Length = payload.Length
			};
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(value.Data, global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(payload), payload.Length);
			m_ConnectionPayloads[connection.ConnectionId] = value;
		}

		internal bool EnsureBindFromEndpoint(global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			if (!Bound)
			{
				global::Unity.Networking.Transport.NetworkEndpoint networkEndpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
				switch (endpoint.Family)
				{
				case global::Unity.Networking.Transport.NetworkFamily.Ipv4:
					networkEndpoint = global::Unity.Networking.Transport.NetworkEndpoint.AnyIpv4;
					break;
				case global::Unity.Networking.Transport.NetworkFamily.Ipv6:
					networkEndpoint = global::Unity.Networking.Transport.NetworkEndpoint.AnyIpv6;
					break;
				case global::Unity.Networking.Transport.NetworkFamily.Custom:
					networkEndpoint = endpoint;
					break;
				default:
					global::UnityEngine.Debug.LogError("Can't connect to endpoint with invalid address family.");
					return false;
				}
				int num = m_NetworkStack.Bind(ref networkEndpoint);
				Bound = num == 0;
				return num == 0;
			}
			return true;
		}

		public global::Unity.Networking.Transport.NetworkConnection Connect(global::Unity.Networking.Transport.NetworkEndpoint endpoint, global::Unity.Collections.NativeArray<byte> payload)
		{
			global::Unity.Networking.Transport.NetworkConnection networkConnection = Connect(endpoint);
			SetPendingPayloadData(networkConnection, payload);
			return networkConnection;
		}

		public global::Unity.Networking.Transport.NetworkConnection Connect(global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			if (!EnsureBindFromEndpoint(endpoint))
			{
				return default(global::Unity.Networking.Transport.NetworkConnection);
			}
			global::Unity.Networking.Transport.ConnectionId connectionId = m_NetworkStack.Connections.StartConnecting(ref endpoint);
			global::Unity.Networking.Transport.NetworkConnection networkConnection = new global::Unity.Networking.Transport.NetworkConnection(connectionId);
			m_PipelineProcessor.InitializeConnection(networkConnection);
			return networkConnection;
		}

		public global::Unity.Networking.Transport.NetworkConnection Connect(global::Unity.Collections.FixedString512Bytes address, ushort port, global::Unity.Collections.NativeArray<byte> payload)
		{
			global::Unity.Networking.Transport.NetworkConnection networkConnection = Connect(address, port);
			SetPendingPayloadData(networkConnection, payload);
			return networkConnection;
		}

		public global::Unity.Networking.Transport.NetworkConnection Connect(global::Unity.Collections.FixedString512Bytes address, ushort port)
		{
			bool hostnameLookupFinished;
			global::Unity.Networking.Transport.NetworkEndpoint resolvedEndpoint;
			global::Unity.Networking.Transport.Error.DisconnectReason disconnectReason;
			global::Unity.Networking.Transport.ConnectionId connectionId = m_NetworkStack.Connections.StartConnecting(address, port, out hostnameLookupFinished, out resolvedEndpoint, out disconnectReason);
			global::Unity.Networking.Transport.NetworkConnection value = new global::Unity.Networking.Transport.NetworkConnection(connectionId);
			if (hostnameLookupFinished)
			{
				if (disconnectReason != global::Unity.Networking.Transport.Error.DisconnectReason.Default)
				{
					m_NetworkStack.Connections.StartDisconnecting(ref connectionId, disconnectReason);
					m_NetworkStack.Connections.FinishDisconnecting(ref connectionId);
					DisconnectWithReason(value, disconnectReason);
					return default(global::Unity.Networking.Transport.NetworkConnection);
				}
				if (!EnsureBindFromEndpoint(resolvedEndpoint))
				{
					return default(global::Unity.Networking.Transport.NetworkConnection);
				}
				m_PipelineProcessor.InitializeConnection(value);
			}
			else
			{
				m_HostnameLookups.Add(in value);
			}
			return value;
		}

		public bool CheckHostnameLookupStatus(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			global::Unity.Networking.Transport.ConnectionId connectionId = connection.ConnectionId;
			if (m_NetworkStack.Connections.CheckHostnameLookupStatus(ref connectionId, out var resolvedEndpoint, out var disconnectReason))
			{
				if (disconnectReason != global::Unity.Networking.Transport.Error.DisconnectReason.Default)
				{
					m_NetworkStack.Connections.StartDisconnecting(ref connectionId, disconnectReason);
					m_NetworkStack.Connections.FinishDisconnecting(ref connectionId);
					DisconnectWithReason(connection, disconnectReason);
					return true;
				}
				if (!EnsureBindFromEndpoint(resolvedEndpoint))
				{
					return true;
				}
				m_PipelineProcessor.InitializeConnection(connection);
				return true;
			}
			return false;
		}

		private void DisconnectWithReason(global::Unity.Networking.Transport.NetworkConnection connection, global::Unity.Networking.Transport.Error.DisconnectReason reason)
		{
			int offset = Receiver.AppendToStream((byte)reason);
			EventQueue.PushEvent(new global::Unity.Networking.Transport.NetworkEvent
			{
				connectionId = connection.ConnectionId.Id,
				type = global::Unity.Networking.Transport.NetworkEvent.Type.Disconnect,
				offset = offset,
				size = 1
			});
		}

		public int Disconnect(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			if (GetConnectionState(connection) != global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
			{
				global::Unity.Networking.Transport.ConnectionId connectionId = connection.ConnectionId;
				m_NetworkStack.Connections.StartDisconnecting(ref connectionId);
			}
			return 0;
		}

		public void GetPipelineBuffers(global::Unity.Networking.Transport.NetworkPipeline pipeline, global::Unity.Networking.Transport.NetworkPipelineStageId stageId, global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.NativeArray<byte> readProcessingBuffer, out global::Unity.Collections.NativeArray<byte> writeProcessingBuffer, out global::Unity.Collections.NativeArray<byte> sharedBuffer)
		{
			if (m_NetworkStack.Connections.ConnectionAt(connection.InternalId) != connection.ConnectionId)
			{
				global::UnityEngine.Debug.LogError("Trying to get pipeline buffers for invalid connection.");
				readProcessingBuffer = default(global::Unity.Collections.NativeArray<byte>);
				writeProcessingBuffer = default(global::Unity.Collections.NativeArray<byte>);
				sharedBuffer = default(global::Unity.Collections.NativeArray<byte>);
			}
			else
			{
				m_PipelineProcessor.GetPipelineBuffers(pipeline, stageId, connection, out readProcessingBuffer, out writeProcessingBuffer, out sharedBuffer);
			}
		}

		internal unsafe T* GetWriteablePipelineParameter<T>(global::Unity.Networking.Transport.NetworkPipeline pipeline, global::Unity.Networking.Transport.NetworkPipelineStageId stageId) where T : unmanaged, global::Unity.Networking.Transport.INetworkParameter
		{
			return m_PipelineProcessor.GetWriteablePipelineParameter<T>(stageId);
		}

		public global::Unity.Networking.Transport.NetworkConnection.State GetConnectionState(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			global::Unity.Networking.Transport.NetworkConnection.State connectionState = m_NetworkStack.Connections.GetConnectionState(connection.ConnectionId);
			if (connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting)
			{
				return connectionState;
			}
			return global::Unity.Networking.Transport.NetworkConnection.State.Disconnected;
		}

		public int GetMaxSupportedMessageSize(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			return ToConcurrentSendOnly().GetMaxSupportedMessageSize(connection);
		}

		public int GetMaxSupportedPayloadSize(global::Unity.Networking.Transport.NetworkConnection connection, global::Unity.Networking.Transport.NetworkPipeline pipe)
		{
			return ToConcurrentSendOnly().GetMaxSupportedPayloadSize(connection, pipe);
		}

		[global::System.Obsolete("RemoteEndPoint has been renamed to GetRemoteEndpoint. (UnityUpgradable) -> GetRemoteEndpoint(*)", false)]
		public global::Unity.Networking.Transport.NetworkEndpoint RemoteEndPoint(global::Unity.Networking.Transport.NetworkConnection id)
		{
			return m_NetworkStack.Connections.GetConnectionEndpoint(id.ConnectionId);
		}

		public global::Unity.Networking.Transport.NetworkEndpoint GetRemoteEndpoint(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			if (m_NetworkSettings.TryGet<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>(out var parameter))
			{
				return parameter.ServerData.Endpoint;
			}
			return m_NetworkStack.Connections.GetConnectionEndpoint(connection.ConnectionId);
		}

		[global::System.Obsolete("LocalEndPoint has been renamed to GetLocalEndpoint. (UnityUpgradable) -> GetLocalEndpoint()", false)]
		public global::Unity.Networking.Transport.NetworkEndpoint LocalEndPoint()
		{
			return m_NetworkStack.GetLocalEndpoint();
		}

		public global::Unity.Networking.Transport.NetworkEndpoint GetLocalEndpoint()
		{
			return m_NetworkStack.GetLocalEndpoint();
		}

		public int MaxHeaderSize(global::Unity.Networking.Transport.NetworkPipeline pipe)
		{
			return ToConcurrentSendOnly().MaxHeaderSize(pipe);
		}

		public int BeginSend(global::Unity.Networking.Transport.NetworkPipeline pipe, global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamWriter writer, int requiredPayloadSize = 0)
		{
			return ToConcurrentSendOnly().BeginSend(pipe, connection, out writer, requiredPayloadSize);
		}

		public int BeginSend(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamWriter writer, int requiredPayloadSize = 0)
		{
			return ToConcurrentSendOnly().BeginSend(global::Unity.Networking.Transport.NetworkPipeline.Null, connection, out writer, requiredPayloadSize);
		}

		public int EndSend(global::Unity.Collections.DataStreamWriter writer)
		{
			return ToConcurrentSendOnly().EndSend(writer);
		}

		public void AbortSend(global::Unity.Collections.DataStreamWriter writer)
		{
			ToConcurrentSendOnly().AbortSend(writer);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEvent(out global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader)
		{
			global::Unity.Networking.Transport.NetworkPipeline pipe;
			return PopEvent(out connection, out reader, out pipe);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEvent(out global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader, out global::Unity.Networking.Transport.NetworkPipeline pipe)
		{
			reader = default(global::Unity.Collections.DataStreamReader);
			global::Unity.Networking.Transport.NetworkEvent.Type type = global::Unity.Networking.Transport.NetworkEvent.Type.Empty;
			int id = 0;
			int offset = 0;
			int size = 0;
			int pipelineId = 0;
			while (true)
			{
				type = m_EventQueue.PopEvent(out id, out offset, out size, out pipelineId);
				global::Unity.Networking.Transport.ConnectionId connectionId = m_NetworkStack.Connections.ConnectionAt(id);
				if (id < 0 || type != global::Unity.Networking.Transport.NetworkEvent.Type.Data || m_NetworkStack.Connections.IsConnectionAccepted(ref connectionId))
				{
					break;
				}
				global::UnityEngine.Debug.LogWarning("A NetworkEvent.Data event was discarded for a connection that had not been accepted yet. To avoid this, consider calling Accept() prior to PopEvent() in your project's network update loop, or only use PopEventForConnection() in conjunction with Accept().");
			}
			pipe = new global::Unity.Networking.Transport.NetworkPipeline
			{
				Id = pipelineId
			};
			if (size >= 0)
			{
				reader = new global::Unity.Collections.DataStreamReader(m_DriverReceiver.GetDataStreamSubArray(offset, size));
			}
			connection = ((id < 0) ? default(global::Unity.Networking.Transport.NetworkConnection) : new global::Unity.Networking.Transport.NetworkConnection(m_NetworkStack.Connections.ConnectionAt(id)));
			return type;
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader)
		{
			global::Unity.Networking.Transport.NetworkPipeline pipe;
			return PopEventForConnection(connection, out reader, out pipe);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader, out global::Unity.Networking.Transport.NetworkPipeline pipe)
		{
			reader = default(global::Unity.Collections.DataStreamReader);
			pipe = default(global::Unity.Networking.Transport.NetworkPipeline);
			if (connection.InternalId < 0 || connection.InternalId >= m_NetworkStack.Connections.Count || m_NetworkStack.Connections.ConnectionAt(connection.InternalId).Version != connection.Version)
			{
				return global::Unity.Networking.Transport.NetworkEvent.Type.Empty;
			}
			int offset;
			int size;
			int pipelineId;
			global::Unity.Networking.Transport.NetworkEvent.Type result = m_EventQueue.PopEventForConnection(connection.InternalId, out offset, out size, out pipelineId);
			pipe = new global::Unity.Networking.Transport.NetworkPipeline
			{
				Id = pipelineId
			};
			if (size > 0)
			{
				reader = new global::Unity.Collections.DataStreamReader(m_DriverReceiver.GetDataStreamSubArray(offset, size));
			}
			return result;
		}

		public int GetEventQueueSizeForConnection(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			if (connection.InternalId < 0 || connection.InternalId >= m_NetworkStack.Connections.Count || m_NetworkStack.Connections.ConnectionAt(connection.InternalId).Version != connection.Version)
			{
				return 0;
			}
			return m_EventQueue.GetCountForConnection(connection.InternalId);
		}
	}
}
