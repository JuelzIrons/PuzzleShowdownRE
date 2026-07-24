namespace Unity.Networking.Transport
{
	internal struct NetworkStack : global::System.IDisposable
	{
		private struct NetworkInterfaceFunctions
		{
			private struct BindArguments
			{
				public global::Unity.Networking.Transport.NetworkStack Stack;

				public global::Unity.Networking.Transport.NetworkEndpoint Endpoint;

				public int Return;
			}

			private struct GetLocalEndpointArguments
			{
				public global::Unity.Networking.Transport.NetworkStack Stack;

				public global::Unity.Networking.Transport.NetworkEndpoint Return;
			}

			private struct ListenArguments
			{
				public global::Unity.Networking.Transport.NetworkStack Stack;

				public int Return;
			}

			private global::Unity.Networking.Transport.ManagedCallWrapper m_NetworkInterface_Bind_FPtr;

			private global::Unity.Networking.Transport.ManagedCallWrapper m_NetworkInterface_Listen_FPtr;

			private global::Unity.Networking.Transport.ManagedCallWrapper m_NetworkInterface_GetLocalEndpoint_FPtr;

			internal unsafe static global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions Create<N>() where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
			{
				return new global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions
				{
					m_NetworkInterface_Bind_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&BindWrapper<N>)),
					m_NetworkInterface_Listen_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&ListenWrapper<N>)),
					m_NetworkInterface_GetLocalEndpoint_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&GetLocalEndpointWrapper<N>))
				};
			}

			internal int Bind(ref global::Unity.Networking.Transport.NetworkStack stack, ref global::Unity.Networking.Transport.NetworkEndpoint endpoint)
			{
				global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.BindArguments arguments = new global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.BindArguments
				{
					Stack = stack,
					Endpoint = endpoint
				};
				m_NetworkInterface_Bind_FPtr.Invoke(ref arguments);
				return arguments.Return;
			}

			internal int Listen(ref global::Unity.Networking.Transport.NetworkStack stack)
			{
				global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.ListenArguments arguments = new global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.ListenArguments
				{
					Stack = stack
				};
				m_NetworkInterface_Listen_FPtr.Invoke(ref arguments);
				return arguments.Return;
			}

			internal global::Unity.Networking.Transport.NetworkEndpoint GetLocalEndpoint(ref global::Unity.Networking.Transport.NetworkStack stack)
			{
				global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.GetLocalEndpointArguments arguments = new global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.GetLocalEndpointArguments
				{
					Stack = stack
				};
				m_NetworkInterface_GetLocalEndpoint_FPtr.Invoke(ref arguments);
				return arguments.Return;
			}

			private unsafe static void BindWrapper<N>(void* argumentsPtr, int size) where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
			{
				ref global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.BindArguments reference = ref global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.BindArguments>(argumentsPtr, size);
				if (reference.Stack.TryGetLayer<global::Unity.Networking.Transport.NetworkInterfaceLayer<N>>(out var layer))
				{
					reference.Return = layer.Bind(ref reference.Endpoint);
				}
				else
				{
					reference.Return = -1;
				}
			}

			private unsafe static void GetLocalEndpointWrapper<N>(void* argumentsPtr, int size) where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
			{
				ref global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.GetLocalEndpointArguments reference = ref global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.GetLocalEndpointArguments>(argumentsPtr, size);
				if (reference.Stack.TryGetLayer<global::Unity.Networking.Transport.NetworkInterfaceLayer<N>>(out var layer))
				{
					reference.Return = layer.GetLocalEndpoint();
				}
				else
				{
					reference.Return = default(global::Unity.Networking.Transport.NetworkEndpoint);
				}
			}

			private unsafe static void ListenWrapper<N>(void* argumentsPtr, int size) where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
			{
				ref global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.ListenArguments reference = ref global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.ListenArguments>(argumentsPtr, size);
				if (reference.Stack.TryGetLayer<global::Unity.Networking.Transport.NetworkInterfaceLayer<N>>(out var layer))
				{
					reference.Return = layer.Listen();
				}
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkLayerWrapper> m_Layers;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		private global::Unity.Collections.NativeList<int> m_AccumulatedPacketPadding;

		private int m_TotalPacketPadding;

		private global::Unity.Networking.Transport.ConnectionList m_Connections;

		private global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions m_NetworkInterfaceFunctions;

		internal int PacketPadding => m_TotalPacketPadding + 1;

		internal global::Unity.Networking.Transport.ConnectionList Connections => m_Connections;

		internal static void Initialize(out global::Unity.Networking.Transport.NetworkStack stack)
		{
			stack = default(global::Unity.Networking.Transport.NetworkStack);
			stack.m_Layers = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkLayerWrapper>(0, global::Unity.Collections.Allocator.Persistent);
			stack.m_AccumulatedPacketPadding = new global::Unity.Collections.NativeList<int>(0, global::Unity.Collections.Allocator.Persistent);
		}

		internal static void InitializeForSettings<N>(out global::Unity.Networking.Transport.NetworkStack stack, ref N networkInterface, ref global::Unity.Networking.Transport.NetworkSettings networkSettings, out global::Unity.Networking.Transport.PacketsQueue sendQueue, out global::Unity.Networking.Transport.PacketsQueue receiveQueue) where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
		{
			Initialize(out stack);
			stack.m_NetworkInterfaceFunctions = global::Unity.Networking.Transport.NetworkStack.NetworkInterfaceFunctions.Create<N>();
			stack.AddLayer(default(global::Unity.Networking.Transport.BottomLayer), ref networkSettings);
			stack.AddLayer(new global::Unity.Networking.Transport.NetworkInterfaceLayer<N>(networkInterface), ref networkSettings);
			ref N networkInterface2 = ref stack.m_Layers.ElementAt(stack.m_Layers.Length - 1).CastRef<global::Unity.Networking.Transport.NetworkInterfaceLayer<N>>().m_NetworkInterface;
			CreateQueues(ref networkInterface2, ref networkSettings, out sendQueue, out receiveQueue);
			networkInterface = networkInterface2;
			global::Unity.Networking.Transport.Relay.RelayNetworkParameter parameter;
			bool num = networkSettings.TryGet<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>(out parameter);
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter2;
			bool flag = (num ? (global::Unity.Networking.Transport.Relay.RelayParameterExtensions.GetRelayParameters(ref networkSettings).ServerData.IsSecure == 1) : networkSettings.TryGet<global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter>(out parameter2));
			if (networkInterface is global::Unity.Networking.Transport.TCPNetworkInterface || networkInterface is global::Unity.Networking.Transport.WebSocketNetworkInterface)
			{
				if (networkSettings.TryGet<global::Unity.Networking.Transport.StreamSegmentationParameter>(out var _))
				{
					stack.AddLayer(default(global::Unity.Networking.Transport.StreamSegmentationLayer), ref networkSettings);
				}
				if (flag)
				{
					stack.AddLayer(default(global::Unity.Networking.Transport.TLSLayer), ref networkSettings);
				}
				if (networkInterface is global::Unity.Networking.Transport.TCPNetworkInterface)
				{
					stack.AddLayer(default(global::Unity.Networking.Transport.StreamToDatagramLayer), ref networkSettings);
				}
				if (networkInterface is global::Unity.Networking.Transport.WebSocketNetworkInterface)
				{
					stack.AddLayer(default(global::Unity.Networking.Transport.WebSocketLayer), ref networkSettings);
				}
			}
			if (networkSettings.TryGet<global::Unity.Networking.Transport.NetworkSimulatorParameter>(out var _))
			{
				stack.AddLayer(default(global::Unity.Networking.Transport.SimulatorLayer), ref networkSettings);
			}
			if (flag && !(networkInterface is global::Unity.Networking.Transport.TCPNetworkInterface) && !(networkInterface is global::Unity.Networking.Transport.WebSocketNetworkInterface))
			{
				stack.AddLayer(default(global::Unity.Networking.Transport.DTLSLayer), ref networkSettings);
			}
			if (num)
			{
				if (networkInterface is global::Unity.Networking.Transport.IPCNetworkInterface)
				{
					throw new global::System.InvalidOperationException("Relay cannot be used with the IPC interface");
				}
				stack.AddLayer(default(global::Unity.Networking.Transport.RelayLayer), ref networkSettings);
			}
			stack.AddLayer(default(global::Unity.Networking.Transport.SimpleConnectionLayer), ref networkSettings);
			stack.AddLayer(default(global::Unity.Networking.Transport.TopLayer), ref networkSettings);
		}

		public void Dispose()
		{
			int length = m_Layers.Length;
			for (int i = 0; i < length; i++)
			{
				m_Layers.ElementAt(i).Dispose();
			}
			m_Layers.Dispose();
			m_AccumulatedPacketPadding.Dispose();
		}

		internal void AddLayer<T>(T layer, ref global::Unity.Networking.Transport.NetworkSettings settings) where T : unmanaged, global::Unity.Networking.Transport.INetworkLayer
		{
			AddLayer(ref layer, ref settings);
		}

		internal void AddLayer<T>(ref T layer, ref global::Unity.Networking.Transport.NetworkSettings settings) where T : unmanaged, global::Unity.Networking.Transport.INetworkLayer
		{
			global::Unity.Networking.Transport.ConnectionList connections = m_Connections;
			int num = layer.Initialize(ref settings, ref m_Connections, ref m_TotalPacketPadding);
			if (num != 0)
			{
				global::UnityEngine.Debug.LogError($"Failed to initialize the NetworkStack. Layer {typeof(T).ToString()} with error code: {num}.");
			}
			m_Layers.Add(global::Unity.Networking.Transport.NetworkLayerWrapper.Create(ref layer));
			m_AccumulatedPacketPadding.Add(in m_TotalPacketPadding);
			if (m_Connections.IsCreated && connections != m_Connections)
			{
				m_Layers.ElementAt(0).CastRef<global::Unity.Networking.Transport.BottomLayer>().AddConnectionList(ref m_Connections);
			}
		}

		internal bool TryGetLayer<T>(out T layer) where T : unmanaged, global::Unity.Networking.Transport.INetworkLayer
		{
			foreach (global::Unity.Networking.Transport.NetworkLayerWrapper layer2 in m_Layers)
			{
				if (layer2.IsType<T>())
				{
					layer = layer2.CastRef<T>();
					return true;
				}
			}
			layer = default(T);
			return false;
		}

		internal unsafe static void CreateQueues<N>(ref N networkInterface, ref global::Unity.Networking.Transport.NetworkSettings settings, out global::Unity.Networking.Transport.PacketsQueue sendQueue, out global::Unity.Networking.Transport.PacketsQueue receiveQueue) where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
		{
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = settings.GetNetworkConfigParameters();
			int sendQueueCapacity = networkConfigParameters.sendQueueCapacity;
			int receiveQueueCapacity = networkConfigParameters.receiveQueueCapacity;
			int maxMessageSize = networkConfigParameters.maxMessageSize;
			if (global::Unity.Burst.BurstRuntime.GetHashCode64<N>() == global::Unity.Burst.BurstRuntime.GetHashCode64<global::Unity.Networking.Transport.UDPNetworkInterface>())
			{
				fixed (N* ptr = &networkInterface)
				{
					void* ptr2 = ptr;
					((global::Unity.Networking.Transport.UDPNetworkInterface*)ptr2)->CreateQueues(sendQueueCapacity, receiveQueueCapacity, maxMessageSize, out sendQueue, out receiveQueue);
				}
			}
			else
			{
				receiveQueue = new global::Unity.Networking.Transport.PacketsQueue(receiveQueueCapacity, maxMessageSize);
				sendQueue = new global::Unity.Networking.Transport.PacketsQueue(sendQueueCapacity, maxMessageSize);
			}
			if (sendQueue.Capacity != networkConfigParameters.sendQueueCapacity)
			{
				sendQueue.Dispose();
				global::UnityEngine.Debug.LogError($"The provided buffers count ({sendQueue.Capacity}) must be equal to the sendQueueCapacity ({networkConfigParameters.sendQueueCapacity})");
			}
			if (receiveQueue.Capacity != networkConfigParameters.receiveQueueCapacity)
			{
				receiveQueue.Dispose();
				global::UnityEngine.Debug.LogError($"The provided buffers count ({receiveQueue.Capacity}) must be equal to the receiveQueueCapacity ({networkConfigParameters.receiveQueueCapacity})");
			}
		}

		internal int Bind(ref global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			return m_NetworkInterfaceFunctions.Bind(ref this, ref endpoint);
		}

		internal int Listen()
		{
			return m_NetworkInterfaceFunctions.Listen(ref this);
		}

		internal global::Unity.Networking.Transport.NetworkEndpoint GetLocalEndpoint()
		{
			return m_NetworkInterfaceFunctions.GetLocalEndpoint(ref this);
		}

		internal global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.NetworkDriverReceiver driverReceiver, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref global::Unity.Networking.Transport.NetworkEventQueue eventQueue, ref global::Unity.Networking.Transport.NetworkPipelineProcessor pipelineProcessor, ref global::Unity.Collections.NativeHashMap<global::Unity.Networking.Transport.ConnectionId, global::Unity.Networking.Transport.ConnectionPayload> connectionPayloads, long time, global::Unity.Jobs.JobHandle dependency)
		{
			global::Unity.Networking.Transport.ReceiveJobArguments jobArguments = new global::Unity.Networking.Transport.ReceiveJobArguments
			{
				ReceiveQueue = driverReceiver.ReceiveQueue,
				DriverReceiver = driverReceiver,
				ReceiveResult = driverReceiver.Result,
				EventQueue = eventQueue,
				PipelineProcessor = pipelineProcessor,
				ConnectionPayloads = connectionPayloads,
				Time = time
			};
			int length = m_Layers.Length;
			for (int i = 0; i < length; i++)
			{
				dependency = m_Layers.ElementAt(i).ScheduleReceive(ref jobArguments, dependency);
			}
			return dependency;
		}

		internal global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.NetworkDriverSender driverSender, long time, global::Unity.Jobs.JobHandle dependency)
		{
			global::Unity.Networking.Transport.SendJobArguments jobArguments = new global::Unity.Networking.Transport.SendJobArguments
			{
				SendQueue = driverSender.SendQueue,
				Time = time
			};
			dependency = driverSender.FlushPackets(dependency);
			for (int num = m_Layers.Length - 1; num >= 0; num--)
			{
				jobArguments.SendQueue.SetDefaultDataOffset(m_AccumulatedPacketPadding[num]);
				dependency = m_Layers.ElementAt(num).ScheduleSend(ref jobArguments, dependency);
			}
			return dependency;
		}
	}
}
