namespace Unity.Multiplayer.Tools.NetVis.Configuration.Analytics
{
	[global::UnityEngine.Analytics.AnalyticInfo("mpToolsNetSceneVisMetricChanged", "unity.multiplayer.tools", 1, 100, 1000)]
	internal class MetricChangedAnalytic : global::UnityEngine.Analytics.IAnalytic
	{
		private string m_Metric;

		public MetricChangedAnalytic(string metric)
		{
			m_Metric = metric;
		}

		public bool TryGatherData(out global::UnityEngine.Analytics.IAnalytic.IData data, out global::System.Exception error)
		{
			error = null;
			data = new global::Unity.Multiplayer.Tools.NetVis.Configuration.Analytics.MetricChangedData
			{
				metric = m_Metric
			};
			return true;
		}
	}
}
