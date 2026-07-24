namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class GraphVisualElement : global::UnityEngine.UIElements.VisualElement
	{
		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> m_Stats;

		private int m_SampleCount;

		private global::Unity.Multiplayer.Tools.NetStatsMonitor.GraphXAxisType m_XAxisType;

		private global::Unity.Multiplayer.Tools.NetStats.BaseUnits m_YAxisUnits;

		private bool m_YAxisDisplayAsPercentage;

		private global::Unity.Multiplayer.Tools.Common.MinAndMax m_PlotRange;

		private global::Unity.Multiplayer.Tools.Common.MinAndMax m_LastYValues;

		private double m_LastTimeSpan = -3.4028234663852886E+38;

		private readonly global::UnityEngine.UIElements.Label m_Label = new global::UnityEngine.UIElements.Label();

		private readonly global::UnityEngine.UIElements.VisualElement m_GraphAndYAxis = new global::UnityEngine.UIElements.VisualElement();

		private readonly global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphContent m_Content = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphContent();

		private readonly global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphAxisLabels m_YAxisLabels = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphAxisLabels();

		private readonly global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphAxisLabels m_XAxisLabels = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphAxisLabels();

		private readonly global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphLegend m_Legend = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphLegend();

		public global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate SampleRate { get; private set; }

		internal GraphVisualElement()
		{
			AddToClassList("rnsm-display-element");
			AddToClassList("rnsm-graph");
			m_Label.AddToClassList("rnsm-display-element-label");
			m_GraphAndYAxis.AddToClassList("rnsm-graph-and-y-axis");
			m_Content.AddToClassList("rnsm-graph-contents");
			m_XAxisLabels.AddToClassList("rnsm-graph-x-axis");
			m_YAxisLabels.AddToClassList("rnsm-graph-y-axis");
			Add(m_Label);
			m_GraphAndYAxis.Add(m_Content);
			m_GraphAndYAxis.Add(m_YAxisLabels);
			Add(m_GraphAndYAxis);
			Add(m_XAxisLabels);
			Add(m_Legend);
			m_XAxisLabels.MaxLabelMarginRight = m_YAxisLabels.contentRect.width;
			m_YAxisLabels.RegisterCallback(delegate(global::UnityEngine.UIElements.GeometryChangedEvent geometryChangeEvent)
			{
				float width = geometryChangeEvent.newRect.width;
				m_XAxisLabels.MaxLabelMarginRight = width;
			});
		}

		internal void UpdateConfiguration(global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration config)
		{
			global::Unity.Multiplayer.Tools.NetStatsMonitor.GraphConfiguration graphConfiguration = config.GraphConfiguration;
			m_Stats = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId>(config.Stats);
			m_SampleCount = graphConfiguration.SampleCount;
			SampleRate = graphConfiguration.SampleRate;
			m_XAxisType = graphConfiguration.XAxisType;
			m_Label.text = config.Label;
			m_Label.EnableInClassList("rnsm-display-element-label-empty", string.IsNullOrWhiteSpace(config.Label));
			m_Content.UpdateConfiguration(config);
			m_YAxisUnits = global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MetricsUtils.GetUnits(m_Stats, m_Label.text);
			m_YAxisDisplayAsPercentage = global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MetricsUtils.ShouldDisplayAsPercentage(m_Stats, m_Label.text);
			m_YAxisLabels.MinLabel = $"0\u2009{m_YAxisUnits}";
			switch (m_XAxisType)
			{
			case global::Unity.Multiplayer.Tools.NetStatsMonitor.GraphXAxisType.Samples:
				m_XAxisLabels.SetLabels($"-{m_SampleCount}", "0");
				break;
			case global::Unity.Multiplayer.Tools.NetStatsMonitor.GraphXAxisType.Time:
				m_XAxisLabels.MaxLabel = "0\u2009s";
				break;
			default:
				throw new global::System.ArgumentException(string.Format("Unhandled {0} {1}", "GraphXAxisType", m_XAxisType));
			}
			m_Legend.UpdateConfiguration(config);
		}

		private (string Label, float Value) ComputeYAxisBound(float plotBound, float currentValue, string currentStringValue)
		{
			global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase = global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphScalingUtils.NextLargestRoundNumber(plotBound);
			float mantissa = inputBase.Mantissa;
			float value = inputBase.GetValue(10f);
			if (value == currentValue)
			{
				return (Label: currentStringValue, Value: value);
			}
			return (Label: global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.Base10ToDisplayNotation(inputBase, (mantissa == global::System.MathF.Floor(mantissa)) ? 1 : 2, m_YAxisUnits, m_YAxisDisplayAsPercentage), Value: value);
		}

		internal void UpdateDisplayData(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistory history)
		{
			var (minLabel, num) = ComputeYAxisBound(m_PlotRange.Min, m_LastYValues.Min, m_YAxisLabels.MinLabel);
			var (maxLabel, num2) = ComputeYAxisBound(m_PlotRange.Max, m_LastYValues.Max, m_YAxisLabels.MaxLabel);
			m_LastYValues.Min = num;
			m_LastYValues.Max = num2;
			m_PlotRange = m_Content.UpdateDisplayData(history, m_Stats, SampleRate, num, num2);
			m_YAxisLabels.SetLabels(minLabel, maxLabel);
			if (m_XAxisType == global::Unity.Multiplayer.Tools.NetStatsMonitor.GraphXAxisType.Time)
			{
				double num3 = history.TimeSpanOfLastNSamples(SampleRate, m_SampleCount);
				if (!global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.Approximately(num3, m_LastTimeSpan, 0.001))
				{
					m_LastTimeSpan = num3;
					m_XAxisLabels.MinLabel = $"-{num3:0.00} s";
				}
			}
		}
	}
}
