namespace Unity.Networking.Transport
{
	[global::Unity.Burst.BurstCompile]
	public struct IPCNetworkInterface : global::Unity.Networking.Transport.INetworkInterface, global::System.IDisposable
	{
		[global::Unity.Burst.BurstCompile]
		private struct SendUpdate : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.IPCManager ipcManager;

			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public global::Unity.Networking.Transport.NetworkEndpoint localEndPoint;

			public void Execute()
			{
				ipcManager.Update(localEndPoint, ref SendQueue);
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct ReceiveJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			public global::Unity.Networking.Transport.OperationResult ReceiveResult;

			public global::Unity.Networking.Transport.IPCManager ipcManager;

			public global::Unity.Networking.Transport.NetworkEndpoint localEndPoint;

			public unsafe void Execute()
			{
				while (ipcManager.HasDataAvailable(localEndPoint))
				{
					if (!ReceiveQueue.EnqueuePacket(out var packetProcessor))
					{
						ReceiveResult.ErrorCode = -10;
						break;
					}
					void* unsafePayloadPtr = packetProcessor.GetUnsafePayloadPtr();
					global::Unity.Networking.Transport.NetworkEndpoint address = default(global::Unity.Networking.Transport.NetworkEndpoint);
					int num = NativeReceive(unsafePayloadPtr, packetProcessor.Capacity, ref address);
					packetProcessor.EndpointRef = address;
					if (num <= 0)
					{
						if (num != 0)
						{
							ReceiveResult.ErrorCode = -num;
						}
						break;
					}
					packetProcessor.SetUnsafeMetadata(num);
				}
			}

			private unsafe int NativeReceive(void* data, int length, ref global::Unity.Networking.Transport.NetworkEndpoint address)
			{
				return ipcManager.ReceiveMessageEx(localEndPoint, data, length, ref address);
			}
		}

		[global::Unity.Collections.ReadOnly]
		private global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkEndpoint> m_LocalEndpoint;

		public global::Unity.Networking.Transport.NetworkEndpoint LocalEndpoint => m_LocalEndpoint[0];

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref int packetPadding)
		{
			global::Unity.Networking.Transport.IPCManager.Instance.AddRef();
			m_LocalEndpoint = new global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkEndpoint>(1, global::Unity.Collections.Allocator.Persistent);
			return 0;
		}

		public void Dispose()
		{
			m_LocalEndpoint.Dispose();
			global::Unity.Networking.Transport.IPCManager.Instance.Release();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			dep = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.IPCNetworkInterface.ReceiveJob
			{
				ReceiveQueue = arguments.ReceiveQueue,
				ipcManager = global::Unity.Networking.Transport.IPCManager.Instance,
				localEndPoint = m_LocalEndpoint[0],
				ReceiveResult = arguments.ReceiveResult
			}, global::Unity.Jobs.JobHandle.CombineDependencies(dep, global::Unity.Networking.Transport.IPCManager.ManagerAccessHandle));
			global::Unity.Networking.Transport.IPCManager.ManagerAccessHandle = dep;
			return dep;
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			dep = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.IPCNetworkInterface.SendUpdate
			{
				ipcManager = global::Unity.Networking.Transport.IPCManager.Instance,
				SendQueue = arguments.SendQueue,
				localEndPoint = m_LocalEndpoint[0]
			}, global::Unity.Jobs.JobHandle.CombineDependencies(dep, global::Unity.Networking.Transport.IPCManager.ManagerAccessHandle));
			global::Unity.Networking.Transport.IPCManager.ManagerAccessHandle = dep;
			return dep;
		}

		public int Bind(global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			m_LocalEndpoint[0] = global::Unity.Networking.Transport.IPCManager.Instance.CreateEndpoint(endpoint.Port);
			return 0;
		}

		public int Listen()
		{
			return 0;
		}
	}
}
