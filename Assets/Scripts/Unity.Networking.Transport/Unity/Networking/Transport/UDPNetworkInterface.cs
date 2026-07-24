namespace Unity.Networking.Transport
{
	[global::Unity.Burst.BurstCompile]
	public struct UDPNetworkInterface : global::Unity.Networking.Transport.INetworkInterface, global::System.IDisposable
	{
		private struct PacketBufferLayout
		{
			public uint MetadataOffset;

			public uint EndpointOffset;

			public uint PayloadOffset;
		}

		internal enum SocketStatus
		{
			SocketNormal = 0,
			SocketNeedsRecreate = 1,
			SocketFailed = 2
		}

		internal struct InternalState
		{
			public global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP Socket;

			public global::Unity.Networking.Transport.UDPNetworkInterface.SocketStatus SocketStatus;

			public global::Unity.Networking.Transport.NetworkEndpoint BindEndpoint;

			public int ReceiveQueueCapacity;

			public int SendQueueCapacity;

			public long LastUpdateTime;

			public long LastSocketRecreateTime;

			public uint NumSocketRecreate;

			public bool SetDontFragmentBit;
		}

		[global::Unity.Burst.BurstCompile]
		private struct FlushSendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			public global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.UDPNetworkInterface.InternalState> InternalState;

			public global::Unity.Networking.Transport.UnsafeBaselibNetworkArray SendBuffers;

			public global::Unity.Networking.Transport.UDPNetworkInterface.PacketBufferLayout PacketBufferLayout;

			public void Execute()
			{
				ScheduleSendRequests();
				ProcessSendRequests();
				ProcessSendResults();
			}

			private unsafe void ScheduleSendRequests()
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				int count = SendQueue.Count;
				global::Unity.Collections.NativeList<global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Request> list = new global::Unity.Collections.NativeList<global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Request>(count, global::Unity.Collections.Allocator.Temp);
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Request* unsafePtr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(list);
				global::Unity.Collections.NativeList<int> nativeList = new global::Unity.Collections.NativeList<int>(count, global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue[i];
					if (packetProcessor.Length != 0)
					{
						global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Request value = GetRequest(SendQueue.GetPacketBufferIndex(i), ref SendBuffers, ref PacketBufferLayout);
						value.payload.offset += (uint)packetProcessor.Offset;
						value.payload.data += packetProcessor.Offset;
						value.payload.size = (uint)packetProcessor.Length;
						global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress baselib_NetworkAddress = *(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress*)(void*)value.remoteEndpoint.slice.data;
						global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Endpoint_Create(&baselib_NetworkAddress, value.remoteEndpoint.slice, &error);
						if (error.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
						{
							global::UnityEngine.Debug.LogError($"Baselib operation failed. Unexpected endpoint format. (error {(int)error.code}: {GetBaselibErrorMessage(error)})");
							packetProcessor.Drop();
						}
						else
						{
							list.Add(in value);
							nativeList.Add(in i);
						}
					}
				}
				int num = (int)global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend(InternalState.Value.Socket, unsafePtr, (uint)list.Length, &error);
				if (error.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					global::UnityEngine.Debug.LogError($"Baselib operation failed. Couldn't schedule send requests. (error {(int)error.code}: {GetBaselibErrorMessage(error)})");
					MarkSocketAsNeedingRecreate(ref InternalState);
					return;
				}
				for (int j = 0; j < num; j++)
				{
					SendQueue.DequeuePacketNoRelease(nativeList[j]);
				}
			}

			private unsafe void ProcessSendRequests()
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP socket = InternalState.Value.Socket;
				int capacity = SendQueue.Capacity;
				for (int i = 0; i < capacity; i++)
				{
					global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_ProcessStatus baselib_RegisteredNetwork_ProcessStatus = global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_ProcessSend(socket, &error);
					if (error.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						global::UnityEngine.Debug.LogError($"Baselib operation failed. Couldn't process scheduled send request. (error {(int)error.code}: {GetBaselibErrorMessage(error)})");
						MarkSocketAsNeedingRecreate(ref InternalState);
						break;
					}
					if (baselib_RegisteredNetwork_ProcessStatus != global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_ProcessStatus.Pending)
					{
						break;
					}
				}
			}

			private unsafe void ProcessSendResults()
			{
				global::Unity.Collections.NativeArray<global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_CompletionResult> nativeArray = new global::Unity.Collections.NativeArray<global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_CompletionResult>(SendQueue.Capacity, global::Unity.Collections.Allocator.Temp);
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_CompletionResult* unsafePtr = (global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_CompletionResult*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				int num = (int)global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_DequeueSend(InternalState.Value.Socket, unsafePtr, (uint)nativeArray.Length, &error);
				if (error.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					global::UnityEngine.Debug.LogError($"Baselib operation failed. Couldn't dequeue send results. (error {(int)error.code}: {GetBaselibErrorMessage(error)})");
					MarkSocketAsNeedingRecreate(ref InternalState);
					return;
				}
				for (int i = 0; i < num; i++)
				{
					int bufferIndex = nativeArray[i].requestUserdata.ToInt32() - 1;
					SendQueue.EnqueuePacket(bufferIndex, out var _);
				}
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct ReceiveJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			public global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.UDPNetworkInterface.InternalState> InternalState;

			public global::Unity.Networking.Transport.UnsafeBaselibNetworkArray ReceiveBuffers;

			public global::Unity.Networking.Transport.OperationResult Result;

			public global::Unity.Networking.Transport.UDPNetworkInterface.PacketBufferLayout PacketBufferLayout;

			public long UpdateTime;

			public unsafe void Execute()
			{
				global::Unity.Networking.Transport.UDPNetworkInterface.InternalState value = InternalState.Value;
				value.LastUpdateTime = UpdateTime;
				InternalState.Value = value;
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP socket = InternalState.Value.Socket;
				if (InternalState.Value.LastSocketRecreateTime == UpdateTime)
				{
					ResetReceiveQueue(ref ReceiveQueue);
				}
				if (ScheduleAllReceives(socket, ref ReceiveQueue, ref ReceiveBuffers, ref PacketBufferLayout) != 0)
				{
					MarkSocketAsNeedingRecreate(ref InternalState);
					return;
				}
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				int num = 0;
				while (global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv(socket, &baselib_ErrorState) == global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_ProcessStatus.Pending && num++ < ReceiveQueue.Capacity)
				{
				}
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_CompletionResult* ptr = stackalloc global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_CompletionResult[64];
				bool flag = true;
				int num2 = 0;
				int num3 = 0;
				while (flag)
				{
					int num4 = (int)global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv(socket, ptr, 64u, &baselib_ErrorState);
					if (baselib_ErrorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						MarkSocketAsNeedingRecreate(ref InternalState);
						Result.ErrorCode = (int)baselib_ErrorState.code;
						return;
					}
					num2 += num4;
					for (int i = 0; i < num4; i++)
					{
						int bufferIndex = (int)ptr[i].requestUserdata - 1;
						if (ptr[i].status == global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_CompletionStatus.Failed)
						{
							num3++;
							continue;
						}
						int bytesTransferred = (int)ptr[i].bytesTransferred;
						if (bytesTransferred > 0 && bytesTransferred <= ReceiveQueue.PayloadCapacity)
						{
							if (!ReceiveQueue.EnqueuePacket(bufferIndex, out var packetProcessor))
							{
								Result.ErrorCode = -10;
								global::UnityEngine.Debug.LogError("Could not enqueue received packet.");
								return;
							}
							packetProcessor.SetUnsafeMetadata(bytesTransferred);
						}
					}
					flag = num4 == 64;
				}
				ConvertEndpointsToGeneric();
			}

			private void ConvertEndpointsToGeneric()
			{
				int count = ReceiveQueue.Count;
				for (int i = 0; i < count; i++)
				{
					if (!ConvertEndpointBufferToGeneric(GetRequest(ReceiveQueue.GetPacketBufferIndex(i), ref ReceiveBuffers, ref PacketBufferLayout).remoteEndpoint.slice))
					{
						ReceiveQueue[i].Drop();
					}
				}
			}
		}

		private const uint k_RequestsBatchSize = 64u;

		private const uint k_MaxNumSocketRecreate = 1000u;

		private global::Unity.Networking.Transport.PacketsQueue m_ReceiveQueue;

		private global::Unity.Networking.Transport.UnsafeBaselibNetworkArray m_SendBuffers;

		private global::Unity.Networking.Transport.UnsafeBaselibNetworkArray m_ReceiveBuffers;

		private global::Unity.Networking.Transport.UDPNetworkInterface.PacketBufferLayout m_PacketBufferLayout;

		internal global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.UDPNetworkInterface.InternalState> m_InternalState;

		public unsafe global::Unity.Networking.Transport.NetworkEndpoint LocalEndpoint
		{
			get
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP socket = m_InternalState.Value.Socket;
				global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress baselibAddress = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress(socket, &baselibAddress, &baselib_ErrorState);
				if (baselib_ErrorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					return m_InternalState.Value.BindEndpoint;
				}
				return new global::Unity.Networking.Transport.NetworkEndpoint(baselibAddress);
			}
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref int packetPadding)
		{
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = settings.GetNetworkConfigParameters();
			global::Unity.Networking.Transport.UDPNetworkInterface.InternalState value = new global::Unity.Networking.Transport.UDPNetworkInterface.InternalState
			{
				ReceiveQueueCapacity = networkConfigParameters.receiveQueueCapacity,
				SendQueueCapacity = networkConfigParameters.sendQueueCapacity,
				SetDontFragmentBit = networkConfigParameters.performPathMtuDiscovery
			};
			m_InternalState = new global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.UDPNetworkInterface.InternalState>(value, global::Unity.Collections.Allocator.Persistent);
			int num = networkConfigParameters.maxMessageSize + global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.PacketMetadata>() + global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkEndpoint>();
			int typeSize = num + 1;
			m_ReceiveBuffers = new global::Unity.Networking.Transport.UnsafeBaselibNetworkArray(value.ReceiveQueueCapacity, typeSize);
			m_SendBuffers = new global::Unity.Networking.Transport.UnsafeBaselibNetworkArray(value.SendQueueCapacity, num);
			return 0;
		}

		internal void CreateQueues(int sendQueueCapacity, int receiveQueueCapacity, int payloadSize, out global::Unity.Networking.Transport.PacketsQueue sendQueue, out global::Unity.Networking.Transport.PacketsQueue receiveQueue)
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.PacketMetadata>();
			int x = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkEndpoint>();
			x = global::Unity.Mathematics.math.max(x, 28);
			m_PacketBufferLayout = new global::Unity.Networking.Transport.UDPNetworkInterface.PacketBufferLayout
			{
				MetadataOffset = 0u,
				EndpointOffset = (uint)num,
				PayloadOffset = (uint)(num + x)
			};
			receiveQueue = new global::Unity.Networking.Transport.PacketsQueue(num, payloadSize, x, receiveQueueCapacity, GetTempPacketBuffersArray(receiveQueueCapacity, ref m_ReceiveBuffers));
			m_ReceiveQueue = receiveQueue;
			sendQueue = new global::Unity.Networking.Transport.PacketsQueue(num, payloadSize, x, sendQueueCapacity, GetTempPacketBuffersArray(sendQueueCapacity, ref m_SendBuffers));
			if (m_SendBuffers.ElementSize < num + payloadSize + x)
			{
				global::UnityEngine.Debug.LogError($"The required buffer size ({num + payloadSize + x}) does not fit in the allocated send buffers ({m_SendBuffers.ElementSize})");
			}
			if (m_ReceiveBuffers.ElementSize < num + payloadSize + x)
			{
				global::UnityEngine.Debug.LogError($"The required buffer size ({num + payloadSize + x}) does not fit in the allocated receive buffers ({m_ReceiveBuffers.ElementSize})");
			}
		}

		private global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.PacketBuffer> GetTempPacketBuffersArray(int capacity, ref global::Unity.Networking.Transport.UnsafeBaselibNetworkArray buffers)
		{
			global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.PacketBuffer> result = new global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.PacketBuffer>(capacity, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < capacity; i++)
			{
				global::System.IntPtr bufferPtr = buffers.GetBufferPtr(i);
				result[i] = new global::Unity.Networking.Transport.PacketBuffer
				{
					Metadata = bufferPtr + (int)m_PacketBufferLayout.MetadataOffset,
					Endpoint = bufferPtr + (int)m_PacketBufferLayout.EndpointOffset,
					Payload = bufferPtr + (int)m_PacketBufferLayout.PayloadOffset
				};
			}
			return result;
		}

		public void Dispose()
		{
			CloseSocket(m_InternalState.Value.Socket);
			m_SendBuffers.Dispose();
			m_ReceiveBuffers.Dispose();
			m_InternalState.Dispose();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			if (m_InternalState.Value.SocketStatus == global::Unity.Networking.Transport.UDPNetworkInterface.SocketStatus.SocketNeedsRecreate)
			{
				RecreateSocket(arguments.Time);
			}
			if (m_InternalState.Value.SocketStatus == global::Unity.Networking.Transport.UDPNetworkInterface.SocketStatus.SocketFailed)
			{
				arguments.ReceiveResult.ErrorCode = -11;
				return dep;
			}
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.UDPNetworkInterface.ReceiveJob
			{
				InternalState = m_InternalState,
				ReceiveQueue = arguments.ReceiveQueue,
				ReceiveBuffers = m_ReceiveBuffers,
				Result = arguments.ReceiveResult,
				PacketBufferLayout = m_PacketBufferLayout,
				UpdateTime = arguments.Time
			}, dep);
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			if (m_InternalState.Value.SocketStatus != global::Unity.Networking.Transport.UDPNetworkInterface.SocketStatus.SocketNormal)
			{
				return dep;
			}
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.UDPNetworkInterface.FlushSendJob
			{
				InternalState = m_InternalState,
				SendQueue = arguments.SendQueue,
				SendBuffers = m_SendBuffers,
				PacketBufferLayout = m_PacketBufferLayout
			}, dep);
		}

		public int Bind(global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			global::Unity.Networking.Transport.UDPNetworkInterface.InternalState value = m_InternalState.Value;
			CloseSocket(value.Socket);
			value.SetDontFragmentBit = value.SetDontFragmentBit && endpoint.Family == global::Unity.Networking.Transport.NetworkFamily.Ipv4;
			global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP socket;
			int num = CreateSocket(value.SendQueueCapacity, value.ReceiveQueueCapacity, endpoint, out socket, value.SetDontFragmentBit);
			if (num == 0)
			{
				value.Socket = socket;
				value.SocketStatus = global::Unity.Networking.Transport.UDPNetworkInterface.SocketStatus.SocketNormal;
				value.BindEndpoint = endpoint;
				ResetReceiveQueue(ref m_ReceiveQueue);
				num = ScheduleAllReceives(socket, ref m_ReceiveQueue, ref m_ReceiveBuffers, ref m_PacketBufferLayout);
			}
			else
			{
				value.Socket = default(global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP);
				value.SocketStatus = global::Unity.Networking.Transport.UDPNetworkInterface.SocketStatus.SocketFailed;
			}
			m_InternalState.Value = value;
			return num;
		}

		public int Listen()
		{
			return 0;
		}

		private unsafe static int CreateSocket(int sendQueueCapacity, int receiveQueueCapacity, global::Unity.Networking.Transport.NetworkEndpoint endpoint, out global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP socket, bool setDontFragmentBit)
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
			socket = checked(global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_Create(endpoint.BaselibAddressPtr, global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_AddressReuse.DoNotAllow, (uint)sendQueueCapacity, (uint)receiveQueueCapacity, &error));
			if (error.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
			{
				if (error.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.AddressInUse)
				{
					global::UnityEngine.Debug.LogError($"Failed to bind UDP socket because the address is already in use. Likely because there is another process using port {endpoint.Port}.");
				}
				else
				{
					global::UnityEngine.Debug.LogError($"Baselib operation failed. Failed to create UDP socket. (error {(int)error.code}: {GetBaselibErrorMessage(error)})");
				}
				return -11;
			}
			if (setDontFragmentBit)
			{
				error = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_SetIPv4DontFragHeader(socket, set: true, &error);
			}
			return 0;
		}

		private static void CloseSocket(global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP socket)
		{
			if (socket.handle != global::System.IntPtr.Zero)
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_Close(socket);
			}
		}

		private void RecreateSocket(long updateTime)
		{
			global::Unity.Networking.Transport.UDPNetworkInterface.InternalState value = m_InternalState.Value;
			if (value.LastSocketRecreateTime == value.LastUpdateTime || value.NumSocketRecreate >= 1000)
			{
				global::UnityEngine.Debug.LogError("Unrecoverable socket failure. An unknown condition is preventing the application from reliably creating sockets.");
				value.SocketStatus = global::Unity.Networking.Transport.UDPNetworkInterface.SocketStatus.SocketFailed;
			}
			else
			{
				global::UnityEngine.Debug.LogWarning("Socket error encountered; attempting recovery by creating a new one.");
				value.LastSocketRecreateTime = updateTime;
				value.NumSocketRecreate++;
				CloseSocket(value.Socket);
				if (CreateSocket(value.SendQueueCapacity, value.ReceiveQueueCapacity, value.BindEndpoint, out var socket, value.SetDontFragmentBit) == 0)
				{
					value.Socket = socket;
					value.SocketStatus = global::Unity.Networking.Transport.UDPNetworkInterface.SocketStatus.SocketNormal;
				}
			}
			m_InternalState.Value = value;
		}

		private static void MarkSocketAsNeedingRecreate(ref global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.UDPNetworkInterface.InternalState> internalState)
		{
			global::Unity.Networking.Transport.UDPNetworkInterface.InternalState value = internalState.Value;
			value.SocketStatus = global::Unity.Networking.Transport.UDPNetworkInterface.SocketStatus.SocketNeedsRecreate;
			internalState.Value = value;
		}

		private static void ResetReceiveQueue(ref global::Unity.Networking.Transport.PacketsQueue receiveQueue)
		{
			if (receiveQueue.BuffersInUse != 0)
			{
				receiveQueue.Clear();
				receiveQueue.UnsafeResetAcquisitionState();
			}
		}

		private unsafe static bool ConvertEndpointBufferToGeneric(global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_BufferSlice endpointSlice)
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress baselibAddress = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
			global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Endpoint endpoint = new global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Endpoint
			{
				slice = endpointSlice
			};
			global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
			global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress(endpoint, &baselibAddress, &error);
			if (error.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
			{
				global::UnityEngine.Debug.LogError($"Baselib operation failed. Couldn't create registered endpoint. (error {(int)error.code}: {GetBaselibErrorMessage(error)})");
				return false;
			}
			*(global::Unity.Networking.Transport.NetworkEndpoint*)(void*)endpointSlice.data = new global::Unity.Networking.Transport.NetworkEndpoint(baselibAddress);
			return true;
		}

		private unsafe static global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Request GetRequest(int bufferIndex, ref global::Unity.Networking.Transport.UnsafeBaselibNetworkArray buffers, ref global::Unity.Networking.Transport.UDPNetworkInterface.PacketBufferLayout bufferLayout)
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_BufferSlice baselib_RegisteredNetwork_BufferSlice = buffers.AtIndexAsSlice(bufferIndex);
			global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Request result = new global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Request
			{
				payload = baselib_RegisteredNetwork_BufferSlice,
				remoteEndpoint = new global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Endpoint
				{
					slice = baselib_RegisteredNetwork_BufferSlice
				},
				requestUserdata = new global::System.IntPtr(bufferIndex + 1)
			};
			result.payload.offset = bufferLayout.PayloadOffset;
			result.payload.data = new global::System.IntPtr((byte*)(void*)result.payload.data + bufferLayout.PayloadOffset);
			result.payload.size -= bufferLayout.PayloadOffset;
			result.remoteEndpoint.slice.offset = bufferLayout.EndpointOffset;
			result.remoteEndpoint.slice.data = new global::System.IntPtr((byte*)(void*)result.remoteEndpoint.slice.data + bufferLayout.EndpointOffset);
			result.remoteEndpoint.slice.size = 28u;
			return result;
		}

		private unsafe static int ScheduleAllReceives(global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP socket, ref global::Unity.Networking.Transport.PacketsQueue receiveQueue, ref global::Unity.Networking.Transport.UnsafeBaselibNetworkArray receiveBuffers, ref global::Unity.Networking.Transport.UDPNetworkInterface.PacketBufferLayout packetBufferLayout)
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
			global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Request* ptr = stackalloc global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Request[64];
			int num = 0;
			do
			{
				num = 0;
				int bufferIndex;
				while ((long)num < 64L && receiveQueue.TryAcquireBuffer(out bufferIndex))
				{
					ptr[num++] = GetRequest(bufferIndex, ref receiveBuffers, ref packetBufferLayout);
				}
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv(socket, ptr, (uint)num, &baselib_ErrorState);
				if (baselib_ErrorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					return -11;
				}
			}
			while ((long)num == 64);
			return 0;
		}

		internal unsafe static global::Unity.Collections.FixedString512Bytes GetBaselibErrorMessage(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error)
		{
			global::Unity.Collections.FixedString512Bytes result = default(global::Unity.Collections.FixedString512Bytes);
			result.Length = (int)global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState_Explain(&error, result.GetUnsafePtr(), (uint)result.Capacity, global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState_ExplainVerbosity.ErrorType_SourceLocation_Explanation);
			return result;
		}
	}
}
