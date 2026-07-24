namespace Unity.Multiplayer.Tools.NetworkProfiler.Runtime
{
	internal class ProfilerCounters
	{
		private static global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ProfilerCounters s_Singleton;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricByteCounters totalBytes;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters rpc;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters namedMessage;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters unnamedMessage;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters networkVariableDelta;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters objectSpawned;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters objectDestroyed;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters serverLog;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters sceneEvent;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters ownershipChange;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters customMessage;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters networkMessage;

		private global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounterFactory m_ByteCounterFactory;

		private global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounterFactory m_EventCounterFactory;

		public static global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ProfilerCounters Instance => s_Singleton ?? (s_Singleton = new global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ProfilerCounters());

		public ProfilerCounters(global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounterFactory byteCounterFactory = null, global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounterFactory eventCounterFactory = null)
		{
			m_ByteCounterFactory = byteCounterFactory ?? new global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ByteCounterFactory();
			m_EventCounterFactory = eventCounterFactory ?? new global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.EventCounterFactory();
			totalBytes = ConstructMetricByteCounters("Total");
			rpc = ConstructMetricCounters(global::Unity.Multiplayer.Tools.MetricTypes.MetricType.Rpc);
			namedMessage = ConstructMetricCounters(global::Unity.Multiplayer.Tools.MetricTypes.MetricType.NamedMessage);
			unnamedMessage = ConstructMetricCounters(global::Unity.Multiplayer.Tools.MetricTypes.MetricType.UnnamedMessage);
			networkVariableDelta = ConstructMetricCounters("Network Variable");
			objectSpawned = ConstructMetricCounters(global::Unity.Multiplayer.Tools.MetricTypes.MetricType.ObjectSpawned);
			objectDestroyed = ConstructMetricCounters(global::Unity.Multiplayer.Tools.MetricTypes.MetricType.ObjectDestroyed);
			serverLog = ConstructMetricCounters(global::Unity.Multiplayer.Tools.MetricTypes.MetricType.ServerLog);
			sceneEvent = ConstructMetricCounters(global::Unity.Multiplayer.Tools.MetricTypes.MetricType.SceneEvent);
			ownershipChange = ConstructMetricCounters(global::Unity.Multiplayer.Tools.MetricTypes.MetricType.OwnershipChange);
			customMessage = ConstructMetricCounters("Custom");
			networkMessage = ConstructMetricCounters("Network Messages");
		}

		private global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricByteCounters ConstructMetricByteCounters(string name)
		{
			return new global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricByteCounters(name, m_ByteCounterFactory);
		}

		private global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters ConstructMetricCounters(global::Unity.Multiplayer.Tools.MetricTypes.MetricType metricType)
		{
			return ConstructMetricCounters(global::Unity.Multiplayer.Tools.NetStats.MetricTypeExtensions.GetDisplayNameString(metricType));
		}

		private global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters ConstructMetricCounters(string name)
		{
			return new global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricCounters(name, m_ByteCounterFactory, m_EventCounterFactory);
		}

		public void UpdateFromMetrics(global::Unity.Multiplayer.Tools.NetStats.MetricCollection collection)
		{
			totalBytes.Sample(collection.TryGetCounter(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.TotalBytesSent), out var counter) ? counter.Value : 0, collection.TryGetCounter(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.TotalBytesReceived), out var counter2) ? counter2.Value : 0);
			rpc.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.RpcSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.RpcReceived)));
			namedMessage.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NamedMessageSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NamedMessageReceived)));
			unnamedMessage.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.UnnamedMessageSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.UnnamedMessageReceived)));
			customMessage.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NamedMessageSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NamedMessageReceived)));
			customMessage.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.UnnamedMessageSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.UnnamedMessageReceived)));
			networkVariableDelta.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NetworkVariableDeltaSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NetworkVariableDeltaReceived)));
			objectSpawned.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ObjectSpawnedSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ObjectSpawnedReceived)));
			objectDestroyed.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ObjectDestroyedSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ObjectDestroyedReceived)));
			serverLog.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ServerLogSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.ServerLogReceived)));
			sceneEvent.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.SceneEventSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.SceneEventReceived)));
			ownershipChange.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.OwnershipChangeSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.OwnershipChangeReceived)));
			networkMessage.Sample(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NetworkMessageSent)), global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent>(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetId(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.NetworkMessageReceived)));
		}
	}
}
