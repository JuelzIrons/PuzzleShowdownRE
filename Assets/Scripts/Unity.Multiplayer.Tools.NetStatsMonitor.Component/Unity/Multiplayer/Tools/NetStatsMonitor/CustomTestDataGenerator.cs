namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::UnityEngine.AddComponentMenu("")]
	internal class CustomTestDataGenerator : global::UnityEngine.MonoBehaviour
	{
		private global::Unity.Multiplayer.Tools.NetStatsMonitor.RuntimeNetStatsMonitor m_Rnsm;

		private global::System.Random m_Random = new global::System.Random();

		[field: global::UnityEngine.Tooltip("Pairs of metrics and trends to generate test data for")]
		[field: global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.MetricTrend> MetricTrends { get; set; } = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.MetricTrend>
		{
			new global::Unity.Multiplayer.Tools.NetStatsMonitor.MetricTrend
			{
				Trend = new global::Unity.Multiplayer.Tools.Common.LogNormalRandomWalk()
			}
		};

		private void Start()
		{
			m_Rnsm = global::UnityEngine.Object.FindFirstObjectByType<global::Unity.Multiplayer.Tools.NetStatsMonitor.RuntimeNetStatsMonitor>();
		}

		private void Update()
		{
			if (!m_Rnsm)
			{
				return;
			}
			foreach (global::Unity.Multiplayer.Tools.NetStatsMonitor.MetricTrend metricTrend in MetricTrends)
			{
				float value = metricTrend.Trend.NextFloat(m_Random);
				m_Rnsm.AddCustomValue(metricTrend.Metric, value);
			}
		}
	}
}
