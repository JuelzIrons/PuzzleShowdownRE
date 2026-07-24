namespace Unity.Networking.Transport
{
	internal struct TopLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		[global::Unity.Burst.BurstCompile]
		private struct CompleteReceiveJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.ConnectionList Connections;

			public global::Unity.Networking.Transport.NetworkPipelineProcessor PipelineProcessor;

			public global::Unity.Networking.Transport.NetworkDriverReceiver Receiver;

			public global::Unity.Networking.Transport.NetworkEventQueue EventQueue;

			public void Execute()
			{
				GenerateConnectionEvents();
				global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionId> nativeArray = Connections.QueryIncomingConnections(global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < nativeArray.Length; i++)
				{
					global::Unity.Networking.Transport.ConnectionId connectionId = nativeArray[i];
					PipelineProcessor.InitializeConnection(new global::Unity.Networking.Transport.NetworkConnection(connectionId));
				}
				int count = Receiver.ReceiveQueue.Count;
				for (int j = 0; j < count; j++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = Receiver.ReceiveQueue[j];
					if (packetProcessor.Length > 0)
					{
						AppendToStream(ref packetProcessor);
					}
				}
				if (Receiver.ReceiveQueue.Count == Receiver.ReceiveQueue.Capacity)
				{
					global::UnityEngine.Debug.LogWarning($"Receive queue is full, some packets could be dropped, consider increase its size ({Receiver.ReceiveQueue.Capacity}).");
				}
				Receiver.ReceiveQueue.Clear();
				GenerateDisconnectionEvents();
			}

			private unsafe void AppendToStream(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
			{
				if (!(packetProcessor.ConnectionRef == default(global::Unity.Networking.Transport.ConnectionId)))
				{
					byte b = packetProcessor.RemoveFromPayloadStart<byte>();
					if (b > 0)
					{
						global::Unity.Networking.Transport.NetworkConnection connection = new global::Unity.Networking.Transport.NetworkConnection(packetProcessor.ConnectionRef);
						byte* buffer = (byte*)packetProcessor.GetUnsafePayloadPtr() + packetProcessor.Offset;
						PipelineProcessor.Receive(b, ref Receiver, ref EventQueue, ref connection, buffer, packetProcessor.Length);
						return;
					}
					int offset = Receiver.AppendToStream(ref packetProcessor);
					EventQueue.PushEvent(new global::Unity.Networking.Transport.NetworkEvent
					{
						pipelineId = b,
						connectionId = packetProcessor.ConnectionRef.Id,
						type = global::Unity.Networking.Transport.NetworkEvent.Type.Data,
						offset = offset,
						size = packetProcessor.Length
					});
				}
			}

			private void GenerateConnectionEvents()
			{
				global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionId> nativeArray = Connections.QueryFinishedConnections(global::Unity.Collections.Allocator.Temp);
				int length = nativeArray.Length;
				for (int i = 0; i < length; i++)
				{
					EventQueue.PushEvent(new global::Unity.Networking.Transport.NetworkEvent
					{
						connectionId = nativeArray[i].Id,
						type = global::Unity.Networking.Transport.NetworkEvent.Type.Connect
					});
				}
			}

			private void GenerateDisconnectionEvents()
			{
				global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> nativeArray = Connections.QueryIncomingDisconnections(global::Unity.Collections.Allocator.Temp);
				int length = nativeArray.Length;
				for (int i = 0; i < length; i++)
				{
					global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection incomingDisconnection = nativeArray[i];
					if (incomingDisconnection.Reason != global::Unity.Networking.Transport.Error.DisconnectReason.Default)
					{
						int offset = Receiver.AppendToStream((byte)incomingDisconnection.Reason);
						EventQueue.PushEvent(new global::Unity.Networking.Transport.NetworkEvent
						{
							connectionId = incomingDisconnection.Connection.Id,
							type = global::Unity.Networking.Transport.NetworkEvent.Type.Disconnect,
							offset = offset,
							size = 1
						});
					}
				}
			}
		}

		private global::Unity.Networking.Transport.ConnectionList connections;

		public void Dispose()
		{
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			connections = connectionList;
			return 0;
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.TopLayer.CompleteReceiveJob
			{
				Connections = connections,
				PipelineProcessor = arguments.PipelineProcessor,
				Receiver = arguments.DriverReceiver,
				EventQueue = arguments.EventQueue
			}, dependency);
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return dependency;
		}
	}
}
