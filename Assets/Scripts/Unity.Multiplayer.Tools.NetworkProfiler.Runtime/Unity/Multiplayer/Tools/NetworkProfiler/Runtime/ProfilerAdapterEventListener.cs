namespace Unity.Multiplayer.Tools.NetworkProfiler.Runtime
{
	internal static class ProfilerAdapterEventListener
	{
		private static bool s_Initialized;

		private static readonly global::Unity.Multiplayer.Tools.NetStats.NetStatSerializer s_NetStatSerializer = new global::Unity.Multiplayer.Tools.NetStats.NetStatSerializer();

		[global::UnityEngine.RuntimeInitializeOnLoadMethod]
		private static void SubscribeToAdapterAndMetricEvents()
		{
			if (!s_Initialized)
			{
				s_Initialized = true;
				global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.SubscribeToAll(OnAdapterAdded, OnAdapterRemoved);
			}
		}

		private static void OnAdapterAdded(global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter)
		{
			global::Unity.Multiplayer.Tools.Adapters.IMetricCollectionEvent component = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.IMetricCollectionEvent>();
			if (component != null)
			{
				component.MetricCollectionEvent += OnMetricsReceived;
			}
		}

		private static void OnAdapterRemoved(global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter)
		{
			global::Unity.Multiplayer.Tools.Adapters.IMetricCollectionEvent component = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.IMetricCollectionEvent>();
			if (component != null)
			{
				component.MetricCollectionEvent -= OnMetricsReceived;
			}
		}

		private static void OnMetricsReceived(global::Unity.Multiplayer.Tools.NetStats.MetricCollection metricCollection)
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		private static void PopulateProfilerIfEnabled(global::Unity.Multiplayer.Tools.NetStats.MetricCollection collection)
		{
			global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ProfilerCounters.Instance.UpdateFromMetrics(collection);
			using (s_NetStatSerializer.Serialize(collection))
			{
			}
		}
	}
}
