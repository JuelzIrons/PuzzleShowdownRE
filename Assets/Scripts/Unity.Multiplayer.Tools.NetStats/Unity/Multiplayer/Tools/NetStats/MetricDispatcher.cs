namespace Unity.Multiplayer.Tools.NetStats
{
	internal class MetricDispatcher : global::Unity.Multiplayer.Tools.NetStats.IMetricDispatcher
	{
		private readonly global::Unity.Multiplayer.Tools.NetStats.MetricCollection m_Collection;

		private readonly global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.NetStats.IResettable> m_Resettables;

		private readonly global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.NetStats.IEventMetric> m_EventMetrics;

		private readonly global::System.Collections.Generic.IList<global::Unity.Multiplayer.Tools.NetStats.IMetricObserver> m_Observers = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IMetricObserver>();

		[global::JetBrains.Annotations.CanBeNull]
		private global::System.Text.StringBuilder m_OverLimitMessageStringBuilder;

		internal MetricDispatcher(global::Unity.Multiplayer.Tools.NetStats.MetricCollection collection, global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.NetStats.IResettable> resettables, global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.NetStats.IEventMetric> eventMetrics)
		{
			m_Collection = collection;
			m_Resettables = resettables;
			m_EventMetrics = eventMetrics;
		}

		public void RegisterObserver(global::Unity.Multiplayer.Tools.NetStats.IMetricObserver observer)
		{
			m_Observers.Add(observer);
		}

		public void SetConnectionId(ulong connectionId)
		{
			m_Collection.ConnectionId = connectionId;
		}

		public void Dispatch()
		{
			for (int i = 0; i < m_EventMetrics.Count; i++)
			{
				global::Unity.Multiplayer.Tools.NetStats.IEventMetric metric = m_EventMetrics[i];
				if (metric.WentOverLimit())
				{
					if (m_OverLimitMessageStringBuilder == null)
					{
						m_OverLimitMessageStringBuilder = new global::System.Text.StringBuilder();
					}
					m_OverLimitMessageStringBuilder.AppendLine(metric.WentOverLimitMessage());
				}
			}
			global::System.Text.StringBuilder overLimitMessageStringBuilder = m_OverLimitMessageStringBuilder;
			if (overLimitMessageStringBuilder != null && overLimitMessageStringBuilder.Length > 0)
			{
				global::UnityEngine.Debug.LogWarning(m_OverLimitMessageStringBuilder);
				m_OverLimitMessageStringBuilder.Clear();
			}
			for (int j = 0; j < m_Observers.Count; j++)
			{
				m_Observers[j].Observe(m_Collection);
			}
			for (int k = 0; k < m_Resettables.Count; k++)
			{
				global::Unity.Multiplayer.Tools.NetStats.IResettable resettable = m_Resettables[k];
				if (resettable.ShouldResetOnDispatch)
				{
					resettable.Reset();
				}
			}
		}
	}
}
