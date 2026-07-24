namespace Unity.Networking.Transport
{
	internal struct BottomLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		[global::Unity.Burst.BurstCompile]
		private struct ConnectionListCleanup : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.ConnectionList Connections;

			public void Execute()
			{
				Connections.Cleanup();
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct ClearJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public void Execute()
			{
				SendQueue.Clear();
			}
		}

		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.ConnectionList> m_ConnectionLists;

		internal void AddConnectionList(ref global::Unity.Networking.Transport.ConnectionList connections)
		{
			m_ConnectionLists.Add(in connections);
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			m_ConnectionLists = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.ConnectionList>(1, global::Unity.Collections.Allocator.Persistent);
			return 0;
		}

		public void Dispose()
		{
			m_ConnectionLists.Dispose();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			global::Unity.Jobs.JobHandle jobHandle = dependency;
			foreach (global::Unity.Networking.Transport.ConnectionList connectionList in m_ConnectionLists)
			{
				jobHandle = global::Unity.Jobs.JobHandle.CombineDependencies(jobHandle, global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.BottomLayer.ConnectionListCleanup
				{
					Connections = connectionList
				}, dependency));
			}
			return jobHandle;
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.BottomLayer.ClearJob
			{
				SendQueue = arguments.SendQueue
			}, dependency);
		}
	}
}
