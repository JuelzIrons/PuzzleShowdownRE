namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class GraphLegend : global::UnityEngine.UIElements.VisualElement
	{
		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LegendKey> m_LegendKeys = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LegendKey>();

		public GraphLegend()
		{
			AddToClassList("rnsm-graph-legend");
		}

		public void UpdateConfiguration(global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration configuration)
		{
			global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats = configuration.Stats;
			if (stats.Count < m_LegendKeys.Count)
			{
				_ = m_LegendKeys.Count;
				_ = stats.Count;
				int num = m_LegendKeys.Count - 1;
				while (m_LegendKeys.Count != stats.Count)
				{
					RemoveAt(num);
					m_LegendKeys.RemoveAt(num);
					num--;
				}
			}
			global::Unity.Multiplayer.Tools.Common.ListUtil.Resize(m_LegendKeys, stats.Count, () => new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LegendKey());
			int num2 = global::System.Linq.Enumerable.Count(Children());
			global::System.Collections.Generic.List<global::UnityEngine.Color> variableColors = configuration.GraphConfiguration.VariableColors;
			for (int num3 = 0; num3 < stats.Count; num3++)
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LegendKey legendKey = m_LegendKeys[num3];
				global::Unity.Multiplayer.Tools.NetStats.MetricId metricId = stats[num3];
				global::UnityEngine.Color color = ((variableColors != null && num3 < variableColors.Count) ? variableColors[num3] : global::Unity.Multiplayer.Tools.Common.Visualization.CategoricalColorPalette.GetColor(num3));
				legendKey.Update(metricId.ToString(), color);
				if (num3 >= num2)
				{
					Add(legendKey);
				}
			}
		}
	}
}
