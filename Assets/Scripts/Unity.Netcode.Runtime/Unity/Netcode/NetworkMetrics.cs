namespace Unity.Netcode
{
	internal class NetworkMetrics : global::Unity.Netcode.INetworkMetrics
	{
		private const ulong k_MaxMetricsPerFrame = 1000uL;

		private static global::System.Collections.Generic.Dictionary<uint, string> s_SceneEventTypeNames;

		private static global::Unity.Profiling.ProfilerMarker s_FrameDispatch;

		private readonly global::Unity.Multiplayer.Tools.NetStats.Counter m_TransportBytesSent = new global::Unity.Multiplayer.Tools.NetStats.Counter(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.TotalBytesSent.Id, 0L)
		{
			ShouldResetOnDispatch = true
		};

		private readonly global::Unity.Multiplayer.Tools.NetStats.Counter m_TransportBytesReceived = new global::Unity.Multiplayer.Tools.NetStats.Counter(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.TotalBytesReceived.Id, 0L)
		{
			ShouldResetOnDispatch = true
		};

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent> m_NetworkMessageSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.NetworkMessageSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent> m_NetworkMessageReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.NetworkMessageReceived.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent> m_NamedMessageSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.NamedMessageSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent> m_NamedMessageReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.NamedMessageReceived.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent> m_UnnamedMessageSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.UnnamedMessageSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent> m_UnnamedMessageReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.UnnamedMessageReceived.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent> m_NetworkVariableDeltaSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.NetworkVariableDeltaSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent> m_NetworkVariableDeltaReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.NetworkVariableDeltaReceived.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent> m_OwnershipChangeSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.OwnershipChangeSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent> m_OwnershipChangeReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.OwnershipChangeReceived.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent> m_ObjectSpawnSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.ObjectSpawnedSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent> m_ObjectSpawnReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.ObjectSpawnedReceived.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent> m_ObjectDestroySentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.ObjectDestroyedSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent> m_ObjectDestroyReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.ObjectDestroyedReceived.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent> m_RpcSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.RpcSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent> m_RpcReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.RpcReceived.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent> m_ServerLogSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.ServerLogSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent> m_ServerLogReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.ServerLogReceived.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric> m_SceneEventSentEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.SceneEventSent.Id);

		private readonly global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric> m_SceneEventReceivedEvent = new global::Unity.Multiplayer.Tools.NetStats.EventMetric<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric>(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.SceneEventReceived.Id);

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

		private readonly global::Unity.Multiplayer.Tools.NetStats.Gauge m_PacketLossGauge = new global::Unity.Multiplayer.Tools.NetStats.Gauge(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMetricTypes.PacketLoss.Id);

		private ulong m_NumberOfMetricsThisFrame;

		internal global::Unity.Multiplayer.Tools.NetStats.IMetricDispatcher Dispatcher { get; }

		private bool CanSendMetrics => m_NumberOfMetricsThisFrame < 1000;

		static NetworkMetrics()
		{
			s_FrameDispatch = new global::Unity.Profiling.ProfilerMarker("NetworkMetrics.DispatchFrame");
			s_SceneEventTypeNames = new global::System.Collections.Generic.Dictionary<uint, string>();
			foreach (global::Unity.Netcode.SceneEventType value in global::System.Enum.GetValues(typeof(global::Unity.Netcode.SceneEventType)))
			{
				s_SceneEventTypeNames[(uint)value] = value.ToString();
			}
		}

		private static string GetSceneEventTypeName(uint typeCode)
		{
			if (!s_SceneEventTypeNames.TryGetValue(typeCode, out var value))
			{
				return "Unknown";
			}
			return value;
		}

		public NetworkMetrics()
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
				.WithGauges(m_PacketLossGauge)
				.Build();
			Dispatcher.RegisterObserver(global::Unity.Netcode.NetcodeObserver.Observer);
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

		public void TrackNetworkMessageSent(ulong receivedClientId, string messageType, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_NetworkMessageSentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receivedClientId), messageType, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackNetworkMessageReceived(ulong senderClientId, string messageType, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_NetworkMessageReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), messageType, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackNamedMessageSent(ulong receiverClientId, string messageName, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_NamedMessageSentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receiverClientId), messageName, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackNamedMessageSent(global::System.Collections.Generic.IReadOnlyCollection<ulong> receiverClientIds, string messageName, long bytesCount)
		{
			foreach (ulong receiverClientId in receiverClientIds)
			{
				TrackNamedMessageSent(receiverClientId, messageName, bytesCount);
			}
		}

		public void TrackNamedMessageReceived(ulong senderClientId, string messageName, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_NamedMessageReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), messageName, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackUnnamedMessageSent(ulong receiverClientId, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_UnnamedMessageSentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receiverClientId), bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackUnnamedMessageSent(global::System.Collections.Generic.IReadOnlyCollection<ulong> receiverClientIds, long bytesCount)
		{
			foreach (ulong receiverClientId in receiverClientIds)
			{
				TrackUnnamedMessageSent(receiverClientId, bytesCount);
			}
		}

		public void TrackUnnamedMessageReceived(ulong senderClientId, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_UnnamedMessageReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackNetworkVariableDeltaSent(ulong receiverClientId, global::Unity.Netcode.NetworkObject networkObject, string variableName, string networkBehaviourName, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_NetworkVariableDeltaSentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receiverClientId), GetObjectIdentifier(networkObject), variableName, networkBehaviourName, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackNetworkVariableDeltaReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, string variableName, string networkBehaviourName, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_NetworkVariableDeltaReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), GetObjectIdentifier(networkObject), variableName, networkBehaviourName, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackOwnershipChangeSent(ulong receiverClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_OwnershipChangeSentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receiverClientId), GetObjectIdentifier(networkObject), bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackOwnershipChangeReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_OwnershipChangeReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), GetObjectIdentifier(networkObject), bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackObjectSpawnSent(ulong receiverClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_ObjectSpawnSentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receiverClientId), GetObjectIdentifier(networkObject), bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackObjectSpawnReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_ObjectSpawnReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), GetObjectIdentifier(networkObject), bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackObjectDestroySent(ulong receiverClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_ObjectDestroySentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receiverClientId), GetObjectIdentifier(networkObject), bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackObjectDestroyReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_ObjectDestroyReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), GetObjectIdentifier(networkObject), bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackRpcSent(ulong receiverClientId, global::Unity.Netcode.NetworkObject networkObject, string rpcName, string networkBehaviourName, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_RpcSentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receiverClientId), GetObjectIdentifier(networkObject), rpcName, networkBehaviourName, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackRpcSent(ulong[] receiverClientIds, global::Unity.Netcode.NetworkObject networkObject, string rpcName, string networkBehaviourName, long bytesCount)
		{
			foreach (ulong receiverClientId in receiverClientIds)
			{
				TrackRpcSent(receiverClientId, networkObject, rpcName, networkBehaviourName, bytesCount);
			}
		}

		public void TrackRpcReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, string rpcName, string networkBehaviourName, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_RpcReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), GetObjectIdentifier(networkObject), rpcName, networkBehaviourName, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackServerLogSent(ulong receiverClientId, uint logType, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_ServerLogSentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receiverClientId), (global::Unity.Multiplayer.Tools.MetricTypes.LogLevel)logType, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackServerLogReceived(ulong senderClientId, uint logType, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_ServerLogReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), (global::Unity.Multiplayer.Tools.MetricTypes.LogLevel)logType, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackSceneEventSent(global::System.Collections.Generic.IReadOnlyList<ulong> receiverClientIds, uint sceneEventType, string sceneName, long bytesCount)
		{
			foreach (ulong receiverClientId in receiverClientIds)
			{
				TrackSceneEventSent(receiverClientId, sceneEventType, sceneName, bytesCount);
			}
		}

		public void TrackSceneEventSent(ulong receiverClientId, uint sceneEventType, string sceneName, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_SceneEventSentEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(receiverClientId), GetSceneEventTypeName(sceneEventType), sceneName, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackSceneEventReceived(ulong senderClientId, uint sceneEventType, string sceneName, long bytesCount)
		{
			if (CanSendMetrics)
			{
				m_SceneEventReceivedEvent.Mark(new global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric(new global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo(senderClientId), GetSceneEventTypeName(sceneEventType), sceneName, bytesCount));
				IncrementMetricCount();
			}
		}

		public void TrackPacketSent(uint packetCount)
		{
			if (CanSendMetrics)
			{
				m_PacketSentCounter.Increment(packetCount);
				IncrementMetricCount();
			}
		}

		public void TrackPacketReceived(uint packetCount)
		{
			if (CanSendMetrics)
			{
				m_PacketReceivedCounter.Increment(packetCount);
				IncrementMetricCount();
			}
		}

		public void UpdateRttToServer(int rttMilliseconds)
		{
			if (CanSendMetrics)
			{
				double value = (double)rttMilliseconds * 0.001;
				m_RttToServerGauge.Set(value);
			}
		}

		public void UpdateNetworkObjectsCount(int count)
		{
			if (CanSendMetrics)
			{
				m_NetworkObjectsGauge.Set(count);
			}
		}

		public void UpdateConnectionsCount(int count)
		{
			if (CanSendMetrics)
			{
				m_ConnectionsGauge.Set(count);
			}
		}

		public void UpdatePacketLoss(float packetLoss)
		{
			if (CanSendMetrics)
			{
				m_PacketLossGauge.Set(packetLoss);
			}
		}

		public void DispatchFrame()
		{
			Dispatcher.Dispatch();
			m_NumberOfMetricsThisFrame = 0uL;
		}

		private void IncrementMetricCount()
		{
			m_NumberOfMetricsThisFrame++;
		}

		private static global::Unity.Multiplayer.Tools.MetricTypes.NetworkObjectIdentifier GetObjectIdentifier(global::Unity.Netcode.NetworkObject networkObject)
		{
			return new global::Unity.Multiplayer.Tools.MetricTypes.NetworkObjectIdentifier(networkObject.GetNameForMetrics(), networkObject.NetworkObjectId);
		}
	}
}
