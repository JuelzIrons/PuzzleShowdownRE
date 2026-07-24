namespace Unity.Multiplayer.Tools.Adapters.Ngo1
{
	internal class ObjectBandwidthCache
	{
		private readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived> m_OtherBandwidth = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived>();

		private readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived> m_NetVarBandwidth = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived>();

		private readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived> m_RpcBandwidth = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived>();

		private static readonly global::Unity.Multiplayer.Tools.Common.NetworkDirection[] k_SentAndReceived = new global::Unity.Multiplayer.Tools.Common.NetworkDirection[2]
		{
			global::Unity.Multiplayer.Tools.Common.NetworkDirection.Sent,
			global::Unity.Multiplayer.Tools.Common.NetworkDirection.Received
		};

		public bool IsCold { get; private set; } = true;

		public float GetBandwidth(global::Unity.Multiplayer.Tools.Adapters.ObjectId objectId, global::Unity.Multiplayer.Tools.Common.BandwidthTypes bandwidthTypes, global::Unity.Multiplayer.Tools.Common.NetworkDirection networkDirection)
		{
			global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived bytesSentAndReceived = default(global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived);
			if (global::Unity.Multiplayer.Tools.Common.EnumUtil.ContainsAny(bandwidthTypes, global::Unity.Multiplayer.Tools.Common.BandwidthTypes.Other) && m_OtherBandwidth.TryGetValue(objectId, out var value))
			{
				bytesSentAndReceived += value;
			}
			if (global::Unity.Multiplayer.Tools.Common.EnumUtil.ContainsAny(bandwidthTypes, global::Unity.Multiplayer.Tools.Common.BandwidthTypes.NetVar) && m_NetVarBandwidth.TryGetValue(objectId, out var value2))
			{
				bytesSentAndReceived += value2;
			}
			if (global::Unity.Multiplayer.Tools.Common.EnumUtil.ContainsAny(bandwidthTypes, global::Unity.Multiplayer.Tools.Common.BandwidthTypes.Rpc) && m_RpcBandwidth.TryGetValue(objectId, out var value3))
			{
				bytesSentAndReceived += value3;
			}
			return bytesSentAndReceived[networkDirection];
		}

		public void Update(global::Unity.Multiplayer.Tools.NetStats.MetricCollection collection)
		{
			IsCold = false;
			m_OtherBandwidth.Clear();
			m_NetVarBandwidth.Clear();
			m_RpcBandwidth.Clear();
			global::Unity.Multiplayer.Tools.Common.NetworkDirection[] array = k_SentAndReceived;
			foreach (global::Unity.Multiplayer.Tools.Common.NetworkDirection direction in array)
			{
				LookupAndCountBytes<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>(collection, direction, global::Unity.Multiplayer.Tools.MetricTypes.MetricType.Rpc, m_RpcBandwidth);
				LookupAndCountBytes<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent>(collection, direction, global::Unity.Multiplayer.Tools.MetricTypes.MetricType.NetworkVariableDelta, m_NetVarBandwidth);
				LookupAndCountBytes<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent>(collection, direction, global::Unity.Multiplayer.Tools.MetricTypes.MetricType.ObjectSpawned, m_OtherBandwidth);
				LookupAndCountBytes<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent>(collection, direction, global::Unity.Multiplayer.Tools.MetricTypes.MetricType.ObjectDestroyed, m_OtherBandwidth);
				LookupAndCountBytes<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent>(collection, direction, global::Unity.Multiplayer.Tools.MetricTypes.MetricType.OwnershipChange, m_OtherBandwidth);
			}
		}

		private static void LookupAndCountBytes<TEvent>(global::Unity.Multiplayer.Tools.NetStats.MetricCollection collection, global::Unity.Multiplayer.Tools.Common.NetworkDirection direction, global::Unity.Multiplayer.Tools.MetricTypes.MetricType metricType, global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived> bandwidthBuffer) where TEvent : global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent, global::Unity.Multiplayer.Tools.MetricTypes.INetworkObjectEvent
		{
			global::Unity.Multiplayer.Tools.NetStats.MetricId metricId = global::Unity.Multiplayer.Tools.NetStats.MetricId.Create(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricTypeExtensions.GetDirectedMetric(metricType, direction));
			CountEventBytesForObjects(global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<TEvent>(collection, metricId), direction, bandwidthBuffer);
		}

		private static void CountEventBytesForObjects<TEvent>(global::System.Collections.Generic.IReadOnlyList<TEvent> events, global::Unity.Multiplayer.Tools.Common.NetworkDirection direction, global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived> bandwidthBuffer) where TEvent : global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent, global::Unity.Multiplayer.Tools.MetricTypes.INetworkObjectEvent
		{
			foreach (TEvent @event in events)
			{
				global::Unity.Multiplayer.Tools.Adapters.ObjectId networkId = (global::Unity.Multiplayer.Tools.Adapters.ObjectId)@event.NetworkId.NetworkId;
				global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived bytesSentAndReceived = new global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived(@event.BytesCount, direction);
				if (bandwidthBuffer.TryGetValue(networkId, out var value))
				{
					bandwidthBuffer[networkId] = value + bytesSentAndReceived;
				}
				else
				{
					bandwidthBuffer[networkId] = bytesSentAndReceived;
				}
			}
		}
	}
}
