namespace Unity.Multiplayer.Tools.Adapters.Ngo1
{
	internal class ObjectRpcCountCache
	{
		private global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, int> m_MostRecentRpcCount = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.Adapters.ObjectId, int>();

		public int GetRpcCount(global::Unity.Multiplayer.Tools.Adapters.ObjectId objectId)
		{
			if (!m_MostRecentRpcCount.TryGetValue(objectId, out var value))
			{
				return 0;
			}
			return value;
		}

		public void Update(global::Unity.Multiplayer.Tools.NetStats.MetricCollection collection)
		{
			m_MostRecentRpcCount.Clear();
			LookupAndCountRpcs(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.RpcSent);
			LookupAndCountRpcs(collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType.RpcReceived);
		}

		private void LookupAndCountRpcs(global::Unity.Multiplayer.Tools.NetStats.MetricCollection collection, global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType metricType)
		{
			global::Unity.Multiplayer.Tools.NetStats.MetricId metricId = global::Unity.Multiplayer.Tools.NetStats.MetricId.Create(metricType);
			global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent> eventValues = global::Unity.Multiplayer.Tools.NetStats.MetricsCollectionExtensions.GetEventValues<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>(collection, metricId);
			CountRpcs(eventValues);
		}

		private void CountRpcs(global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent> rpcs)
		{
			foreach (global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent rpc in rpcs)
			{
				global::Unity.Multiplayer.Tools.Adapters.ObjectId networkId = (global::Unity.Multiplayer.Tools.Adapters.ObjectId)rpc.NetworkId.NetworkId;
				if (m_MostRecentRpcCount.ContainsKey(networkId))
				{
					m_MostRecentRpcCount[networkId]++;
				}
				else
				{
					m_MostRecentRpcCount[networkId] = 1;
				}
			}
		}
	}
}
