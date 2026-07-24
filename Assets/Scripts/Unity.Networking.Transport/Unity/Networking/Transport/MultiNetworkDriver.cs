namespace Unity.Networking.Transport
{
	public struct MultiNetworkDriver : global::System.IDisposable
	{
		public struct Concurrent
		{
			internal global::Unity.Networking.Transport.NetworkDriver.Concurrent Driver1;

			internal global::Unity.Networking.Transport.NetworkDriver.Concurrent Driver2;

			internal global::Unity.Networking.Transport.NetworkDriver.Concurrent Driver3;

			internal global::Unity.Networking.Transport.NetworkDriver.Concurrent Driver4;

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckConnection(global::Unity.Networking.Transport.NetworkConnection connection)
			{
				if (connection.DriverId == 0)
				{
					throw new global::System.ArgumentException("Invalid NetworkConnection (likely not obtained from MultiNetworkDriver).");
				}
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private unsafe void CheckWriterHandle(global::Unity.Collections.DataStreamWriter writer)
			{
				global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend* ptr = (global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend*)(void*)writer.m_SendHandleData;
				if (ptr == null)
				{
					throw new global::System.ArgumentException("Invalid DataStreamWriter (likely not obtained from BeginSend call).");
				}
				if (ptr->Connection.DriverId == 0)
				{
					throw new global::System.ArgumentException("Invalid DataStreamWriter (likely not obtained from MultiNetworkDriver).");
				}
			}

			public global::Unity.Networking.Transport.NetworkConnection.State GetConnectionState(global::Unity.Networking.Transport.NetworkConnection connection)
			{
				return this.GetDriverRef(connection.DriverId).GetConnectionState(connection);
			}

			public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader)
			{
				global::Unity.Networking.Transport.NetworkPipeline pipe;
				return PopEventForConnection(connection, out reader, out pipe);
			}

			public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader, out global::Unity.Networking.Transport.NetworkPipeline pipe)
			{
				return this.GetDriverRef(connection.DriverId).PopEventForConnection(connection, out reader, out pipe);
			}

			public int BeginSend(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamWriter writer, int requiredPayloadSize = 0)
			{
				return BeginSend(global::Unity.Networking.Transport.NetworkPipeline.Null, connection, out writer, requiredPayloadSize);
			}

			public int BeginSend(global::Unity.Networking.Transport.NetworkPipeline pipe, global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamWriter writer, int requiredPayloadSize = 0)
			{
				return this.GetDriverRef(connection.DriverId).BeginSend(pipe, connection, out writer, requiredPayloadSize);
			}

			public unsafe int EndSend(global::Unity.Collections.DataStreamWriter writer)
			{
				global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend* ptr = (global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend*)(void*)writer.m_SendHandleData;
				if (ptr == null)
				{
					return -8;
				}
				return this.GetDriverRef(ptr->Connection.DriverId).EndSend(writer);
			}

			public unsafe void AbortSend(global::Unity.Collections.DataStreamWriter writer)
			{
				global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend* ptr = (global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend*)(void*)writer.m_SendHandleData;
				if (ptr == null)
				{
					global::UnityEngine.Debug.LogError("Invalid DataStreamWriter (likely not obtained from BeginSend call).");
				}
				else
				{
					this.GetDriverRef(ptr->Connection.DriverId).AbortSend(writer);
				}
			}
		}

		public const int MaxDriverCount = 4;

		internal global::Unity.Networking.Transport.NetworkDriver Driver1;

		internal global::Unity.Networking.Transport.NetworkDriver Driver2;

		internal global::Unity.Networking.Transport.NetworkDriver Driver3;

		internal global::Unity.Networking.Transport.NetworkDriver Driver4;

		public int DriverCount { get; private set; }

		public bool IsCreated => Driver1.IsCreated;

		public static global::Unity.Networking.Transport.MultiNetworkDriver Create()
		{
			return new global::Unity.Networking.Transport.MultiNetworkDriver
			{
				Driver1 = default(global::Unity.Networking.Transport.NetworkDriver),
				Driver2 = default(global::Unity.Networking.Transport.NetworkDriver),
				Driver3 = default(global::Unity.Networking.Transport.NetworkDriver),
				Driver4 = default(global::Unity.Networking.Transport.NetworkDriver),
				DriverCount = 0
			};
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNewDriver(global::Unity.Networking.Transport.NetworkDriver driver)
		{
			if (!driver.IsCreated)
			{
				throw new global::System.ArgumentException("Invalid driver (driver is not created).");
			}
			global::Unity.Networking.Transport.ConnectionList connections = driver.m_NetworkStack.Connections;
			global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionId> nativeArray = connections.QueryIncomingConnections(global::Unity.Collections.Allocator.Temp);
			if (connections.Count - connections.FreeList.Count - nativeArray.Length > 0)
			{
				throw new global::System.ArgumentException("Invalid driver (driver already has active connections).");
			}
			for (int i = 1; i <= DriverCount; i++)
			{
				int pipelineCount = this.GetDriverRef(i).PipelineCount;
				if (pipelineCount != driver.PipelineCount)
				{
					throw new global::System.ArgumentException($"Invalid driver (driver must have {pipelineCount} pipelines, but has {driver.PipelineCount}).");
				}
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckDriverId(int id)
		{
			if (id < 1 || id > DriverCount)
			{
				throw new global::System.ArgumentException($"Invalid driver ID {id} (must be between 1 and {DriverCount}).");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckConnection(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			if (connection.DriverId == 0)
			{
				throw new global::System.ArgumentException("Invalid NetworkConnection (likely not obtained from MultiNetworkDriver).");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe void CheckWriterHandle(global::Unity.Collections.DataStreamWriter writer)
		{
			global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend* ptr = (global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend*)(void*)writer.m_SendHandleData;
			if (ptr == null)
			{
				throw new global::System.ArgumentException("Invalid DataStreamWriter (likely not obtained from BeginSend call).");
			}
			if (ptr->Connection.DriverId == 0)
			{
				throw new global::System.ArgumentException("Invalid DataStreamWriter (likely not obtained from MultiNetworkDriver).");
			}
		}

		public int AddDriver(global::Unity.Networking.Transport.NetworkDriver driver)
		{
			if (DriverCount == 4)
			{
				throw new global::System.InvalidOperationException("Capacity of MultiNetworkDriver has been reached.");
			}
			DriverCount++;
			this.GetDriverRef(DriverCount).Dispose();
			this.GetDriverRef(DriverCount) = driver;
			return DriverCount;
		}

		public global::Unity.Networking.Transport.NetworkDriver GetDriver(int id)
		{
			return this.GetDriverRef(id);
		}

		public global::Unity.Networking.Transport.NetworkDriver GetDriverForConnection(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			return this.GetDriverRef(connection.DriverId);
		}

		public void Dispose()
		{
			DriverCount = 0;
			for (int i = 1; i <= 4; i++)
			{
				this.GetDriverRef(i).Dispose();
			}
		}

		public global::Unity.Networking.Transport.MultiNetworkDriver.Concurrent ToConcurrent()
		{
			return new global::Unity.Networking.Transport.MultiNetworkDriver.Concurrent
			{
				Driver1 = Driver1.ToConcurrent(),
				Driver2 = Driver2.ToConcurrent(),
				Driver3 = Driver3.ToConcurrent(),
				Driver4 = Driver4.ToConcurrent()
			};
		}

		public global::Unity.Jobs.JobHandle ScheduleUpdate(global::Unity.Jobs.JobHandle dependency = default(global::Unity.Jobs.JobHandle))
		{
			global::Unity.Jobs.JobHandle jobHandle = dependency;
			for (int i = 1; i <= DriverCount; i++)
			{
				jobHandle = global::Unity.Jobs.JobHandle.CombineDependencies(jobHandle, this.GetDriverRef(i).ScheduleUpdate(dependency));
			}
			return jobHandle;
		}

		public global::Unity.Jobs.JobHandle ScheduleFlushSend(global::Unity.Jobs.JobHandle dependency = default(global::Unity.Jobs.JobHandle))
		{
			global::Unity.Jobs.JobHandle jobHandle = dependency;
			for (int i = 1; i <= DriverCount; i++)
			{
				jobHandle = global::Unity.Jobs.JobHandle.CombineDependencies(jobHandle, this.GetDriverRef(i).ScheduleFlushSend(dependency));
			}
			return jobHandle;
		}

		public void RegisterPipelineStage<T>(T stage) where T : unmanaged, global::Unity.Networking.Transport.INetworkPipelineStage
		{
			for (int i = 1; i <= DriverCount; i++)
			{
				this.GetDriverRef(i).RegisterPipelineStage(stage);
			}
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
			global::Unity.Networking.Transport.NetworkPipeline result = default(global::Unity.Networking.Transport.NetworkPipeline);
			for (int i = 1; i <= DriverCount; i++)
			{
				result = this.GetDriverRef(i).CreatePipeline(stages);
			}
			return result;
		}

		public global::Unity.Networking.Transport.NetworkConnection Accept(out global::Unity.Collections.NativeArray<byte> payload)
		{
			payload = default(global::Unity.Collections.NativeArray<byte>);
			for (int i = 1; i <= DriverCount; i++)
			{
				if (this.GetDriverRef(i).Listening)
				{
					global::Unity.Networking.Transport.NetworkConnection networkConnection = this.GetDriverRef(i).Accept(out payload);
					if (networkConnection != default(global::Unity.Networking.Transport.NetworkConnection))
					{
						networkConnection.DriverId = i;
						return networkConnection;
					}
				}
			}
			return default(global::Unity.Networking.Transport.NetworkConnection);
		}

		public global::Unity.Networking.Transport.NetworkConnection Accept()
		{
			global::Unity.Collections.NativeArray<byte> payload;
			return Accept(out payload);
		}

		public global::Unity.Networking.Transport.NetworkConnection Connect(int driverId, global::Unity.Networking.Transport.NetworkEndpoint endpoint, global::Unity.Collections.NativeArray<byte> payload)
		{
			global::Unity.Networking.Transport.NetworkConnection networkConnection = this.GetDriverRef(driverId).Connect(endpoint, payload);
			if (networkConnection != default(global::Unity.Networking.Transport.NetworkConnection))
			{
				networkConnection.DriverId = driverId;
			}
			return networkConnection;
		}

		public global::Unity.Networking.Transport.NetworkConnection Connect(int driverId, global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			global::Unity.Networking.Transport.NetworkConnection networkConnection = this.GetDriverRef(driverId).Connect(endpoint);
			if (networkConnection != default(global::Unity.Networking.Transport.NetworkConnection))
			{
				networkConnection.DriverId = driverId;
			}
			return networkConnection;
		}

		public global::Unity.Networking.Transport.NetworkConnection Connect(int driverId, global::Unity.Collections.FixedString512Bytes address, ushort port, global::Unity.Collections.NativeArray<byte> payload)
		{
			global::Unity.Networking.Transport.NetworkConnection networkConnection = this.GetDriverRef(driverId).Connect(address, port, payload);
			if (networkConnection != default(global::Unity.Networking.Transport.NetworkConnection))
			{
				networkConnection.DriverId = driverId;
			}
			return networkConnection;
		}

		public global::Unity.Networking.Transport.NetworkConnection Connect(int driverId, global::Unity.Collections.FixedString512Bytes address, ushort port)
		{
			global::Unity.Networking.Transport.NetworkConnection networkConnection = this.GetDriverRef(driverId).Connect(address, port);
			if (networkConnection != default(global::Unity.Networking.Transport.NetworkConnection))
			{
				networkConnection.DriverId = driverId;
			}
			return networkConnection;
		}

		public void Disconnect(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			this.GetDriverRef(connection.DriverId).Disconnect(connection);
		}

		public global::Unity.Networking.Transport.NetworkConnection.State GetConnectionState(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			return this.GetDriverRef(connection.DriverId).GetConnectionState(connection);
		}

		public global::Unity.Networking.Transport.NetworkEndpoint GetRemoteEndpoint(global::Unity.Networking.Transport.NetworkConnection connection)
		{
			return this.GetDriverRef(connection.DriverId).GetRemoteEndpoint(connection);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEvent(out global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader)
		{
			global::Unity.Networking.Transport.NetworkPipeline pipe;
			return PopEvent(out connection, out reader, out pipe);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEvent(out global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader, out global::Unity.Networking.Transport.NetworkPipeline pipe)
		{
			connection = default(global::Unity.Networking.Transport.NetworkConnection);
			reader = default(global::Unity.Collections.DataStreamReader);
			pipe = default(global::Unity.Networking.Transport.NetworkPipeline);
			for (int i = 1; i <= DriverCount; i++)
			{
				global::Unity.Networking.Transport.NetworkEvent.Type type = this.GetDriverRef(i).PopEvent(out connection, out reader, out pipe);
				if (type != global::Unity.Networking.Transport.NetworkEvent.Type.Empty)
				{
					connection.DriverId = i;
					return type;
				}
			}
			return global::Unity.Networking.Transport.NetworkEvent.Type.Empty;
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader)
		{
			global::Unity.Networking.Transport.NetworkPipeline pipe;
			return PopEventForConnection(connection, out reader, out pipe);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamReader reader, out global::Unity.Networking.Transport.NetworkPipeline pipe)
		{
			return this.GetDriverRef(connection.DriverId).PopEventForConnection(connection, out reader, out pipe);
		}

		public int BeginSend(global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamWriter writer, int requiredPayloadSize = 0)
		{
			return BeginSend(global::Unity.Networking.Transport.NetworkPipeline.Null, connection, out writer, requiredPayloadSize);
		}

		public int BeginSend(global::Unity.Networking.Transport.NetworkPipeline pipe, global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.DataStreamWriter writer, int requiredPayloadSize = 0)
		{
			return this.GetDriverRef(connection.DriverId).BeginSend(pipe, connection, out writer, requiredPayloadSize);
		}

		public unsafe int EndSend(global::Unity.Collections.DataStreamWriter writer)
		{
			global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend* ptr = (global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend*)(void*)writer.m_SendHandleData;
			if (ptr == null)
			{
				return -8;
			}
			return this.GetDriverRef(ptr->Connection.DriverId).EndSend(writer);
		}

		public unsafe void AbortSend(global::Unity.Collections.DataStreamWriter writer)
		{
			global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend* ptr = (global::Unity.Networking.Transport.NetworkDriver.Concurrent.PendingSend*)(void*)writer.m_SendHandleData;
			if (ptr == null)
			{
				global::UnityEngine.Debug.LogError("Invalid DataStreamWriter (likely not obtained from BeginSend call).");
			}
			else
			{
				this.GetDriverRef(ptr->Connection.DriverId).AbortSend(writer);
			}
		}
	}
}
