namespace Unity.Multiplayer.Tools.MetricTestData
{
	internal class TestDataTracker : global::Unity.Multiplayer.Tools.MetricTestData.ITestDataTracker
	{
		private readonly global::Unity.Multiplayer.Tools.NetStats.Counter m_TransportBytesSent = new global::Unity.Multiplayer.Tools.NetStats.Counter(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.TotalBytesSent), 0L)
		{
			ShouldResetOnDispatch = true
		};

		private readonly global::Unity.Multiplayer.Tools.NetStats.Counter m_TransportBytesReceived = new global::Unity.Multiplayer.Tools.NetStats.Counter(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.TotalBytesReceived), 0L)
		{
			ShouldResetOnDispatch = true
		};

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent> m_NetworkMessageSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NetworkMessageSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent> m_NetworkMessageReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NetworkMessageReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent> m_NamedMessageSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NamedMessageSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent> m_NamedMessageReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NamedMessageReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent> m_UnnamedMessageSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.UnnamedMessageSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent> m_UnnamedMessageReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.UnnamedMessageReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent> m_NetworkVariableDeltaSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NetworkVariableDeltaSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent> m_NetworkVariableDeltaReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NetworkVariableDeltaReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent> m_OwnershipChangeSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.OwnershipChangeSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent> m_OwnershipChangeReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.OwnershipChangeReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent> m_ObjectSpawnSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ObjectSpawnedSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent> m_ObjectSpawnReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ObjectSpawnedReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent> m_ObjectDestroySentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ObjectDestroyedSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent> m_ObjectDestroyReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ObjectDestroyedReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent> m_RpcSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.RpcSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent> m_RpcReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.RpcReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent> m_ServerLogSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ServerLogSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent> m_ServerLogReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ServerLogReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric> m_SceneEventSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.SceneEventSent));

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric> m_SceneEventReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric>(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.SceneEventReceived));

		private readonly global::Unity.Multiplayer.Tools.NetStats.Counter m_PacketSentCounter = new global::Unity.Multiplayer.Tools.NetStats.Counter(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.PacketsSent.Id, 0L)
		{
			ShouldResetOnDispatch = true
		};

		private readonly global::Unity.Multiplayer.Tools.NetStats.Counter m_PacketReceivedCounter = new global::Unity.Multiplayer.Tools.NetStats.Counter(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.PacketsReceived.Id, 0L)
		{
			ShouldResetOnDispatch = true
		};

		private readonly global::Unity.Multiplayer.Tools.NetStats.Gauge m_RttToServerGauge = new global::Unity.Multiplayer.Tools.NetStats.Gauge(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.RttToServer.Id)
		{
			ShouldResetOnDispatch = true
		};

		private readonly global::Unity.Multiplayer.Tools.NetStats.Gauge m_NetworkObjectsGauge = new global::Unity.Multiplayer.Tools.NetStats.Gauge(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.NetworkObjects.Id)
		{
			ShouldResetOnDispatch = true
		};

		private readonly global::Unity.Multiplayer.Tools.NetStats.Gauge m_ConnectionsGauge = new global::Unity.Multiplayer.Tools.NetStats.Gauge(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.ConnectedClients.Id)
		{
			ShouldResetOnDispatch = true
		};

		private readonly global::Unity.Multiplayer.Tools.NetStats.Gauge m_PacketLoss = new global::Unity.Multiplayer.Tools.NetStats.Gauge(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.PacketLoss.Id)
		{
			ShouldResetOnDispatch = true
		};

		public global::Unity.Multiplayer.Tools.NetStats.IMetricDispatcher Dispatcher { get; }

		public TestDataTracker()
		{
			Dispatcher = new global::Unity.Multiplayer.Tools.NetStats.MetricDispatcherBuilder().WithCounters(m_TransportBytesSent, m_TransportBytesReceived).WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent>(m_NetworkMessageSentEvent, m_NetworkMessageReceivedEvent).WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>(m_NamedMessageSentEvent, m_NamedMessageReceivedEvent)
				.WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>(m_UnnamedMessageSentEvent, m_UnnamedMessageReceivedEvent)
				.WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent>(m_NetworkVariableDeltaSentEvent, m_NetworkVariableDeltaReceivedEvent)
				.WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent>(m_OwnershipChangeSentEvent, m_OwnershipChangeReceivedEvent)
				.WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent>(m_ObjectSpawnSentEvent, m_ObjectSpawnReceivedEvent)
				.WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent>(m_ObjectDestroySentEvent, m_ObjectDestroyReceivedEvent)
				.WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>(m_RpcSentEvent, m_RpcReceivedEvent)
				.WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent>(m_ServerLogSentEvent, m_ServerLogReceivedEvent)
				.WithMetricEvents<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric>(m_SceneEventSentEvent, m_SceneEventReceivedEvent)
				.WithCounters(m_PacketSentCounter, m_PacketReceivedCounter)
				.WithGauges(m_RttToServerGauge)
				.WithGauges(m_NetworkObjectsGauge)
				.WithGauges(m_ConnectionsGauge)
				.WithGauges(m_PacketLoss)
				.Build();
		}

		public void SetConnectionId(ulong connectionId)
		{
			Dispatcher.SetConnectionId(connectionId);
		}

		public void TrackTransportBytesSent(long bytesCount)
		{
			m_TransportBytesSent.Increment(bytesCount);
		}

		public void TrackTransportBytesReceived(long bytesCount)
		{
			m_TransportBytesReceived.Increment(bytesCount);
		}

		public void TrackNetworkMessageSent(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent networkMessageEvent)
		{
			m_NetworkMessageSentEvent.Mark(networkMessageEvent);
		}

		public void TrackNetworkMessageReceived(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent networkMessageEvent)
		{
			m_NetworkMessageReceivedEvent.Mark(networkMessageEvent);
		}

		public void TrackNamedMessageSent(global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent namedMessageEvent)
		{
			m_NamedMessageSentEvent.Mark(namedMessageEvent);
		}

		public void TrackNamedMessageReceived(global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent namedMessageEvent)
		{
			m_NamedMessageReceivedEvent.Mark(namedMessageEvent);
		}

		public void TrackUnnamedMessageSent(global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent unnamedMessageEvent)
		{
			m_UnnamedMessageSentEvent.Mark(unnamedMessageEvent);
		}

		public void TrackUnnamedMessageReceived(global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent unnamedMessageEvent)
		{
			m_UnnamedMessageReceivedEvent.Mark(unnamedMessageEvent);
		}

		public void TrackNetworkVariableDeltaSent(global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent networkVariableEvent)
		{
			m_NetworkVariableDeltaSentEvent.Mark(networkVariableEvent);
		}

		public void TrackNetworkVariableDeltaReceived(global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent networkVariableEvent)
		{
			m_NetworkVariableDeltaReceivedEvent.Mark(networkVariableEvent);
		}

		public void TrackOwnershipChangeSent(global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent ownershipChangeEvent)
		{
			m_OwnershipChangeSentEvent.Mark(ownershipChangeEvent);
		}

		public void TrackOwnershipChangeReceived(global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent ownershipChangeEvent)
		{
			m_OwnershipChangeReceivedEvent.Mark(ownershipChangeEvent);
		}

		public void TrackObjectSpawnSent(global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent objectSpawnedEvent)
		{
			m_ObjectSpawnSentEvent.Mark(objectSpawnedEvent);
		}

		public void TrackObjectSpawnReceived(global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent objectSpawnedEvent)
		{
			m_ObjectSpawnReceivedEvent.Mark(objectSpawnedEvent);
		}

		public void TrackObjectDestroySent(global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent objectDestroyedEvent)
		{
			m_ObjectDestroySentEvent.Mark(objectDestroyedEvent);
		}

		public void TrackObjectDestroyReceived(global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent objectDestroyedEvent)
		{
			m_ObjectDestroyReceivedEvent.Mark(objectDestroyedEvent);
		}

		public void TrackRpcSent(global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent rpcEvent)
		{
			m_RpcSentEvent.Mark(rpcEvent);
		}

		public void TrackRpcReceived(global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent rpcEvent)
		{
			m_RpcReceivedEvent.Mark(rpcEvent);
		}

		public void TrackServerLogSent(global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent serverLogEvent)
		{
			m_ServerLogSentEvent.Mark(serverLogEvent);
		}

		public void TrackServerLogReceived(global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent serverLogEvent)
		{
			m_ServerLogReceivedEvent.Mark(serverLogEvent);
		}

		public void TrackSceneEventSent(global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric sceneEvent)
		{
			m_SceneEventSentEvent.Mark(sceneEvent);
		}

		public void TrackSceneEventReceived(global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric sceneEvent)
		{
			m_SceneEventReceivedEvent.Mark(sceneEvent);
		}

		public void TrackPacketSent(int packetCount)
		{
			m_PacketSentCounter.Increment(packetCount);
		}

		public void TrackPacketReceived(int packetCount)
		{
			m_PacketReceivedCounter.Increment(packetCount);
		}

		public void TrackRttToServer(int rtt)
		{
			m_RttToServerGauge.Set(rtt);
		}

		public void UpdateNetworkObjectsCount(int count)
		{
			m_NetworkObjectsGauge.Set(count);
		}

		public void UpdateConnectionsCount(int count)
		{
			m_ConnectionsGauge.Set(count);
		}

		public void UpdatePacketLoss(float count)
		{
			m_PacketLoss.Set(count);
		}
	}
}
