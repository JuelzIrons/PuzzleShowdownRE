namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class CounterVisualElement : global::UnityEngine.UIElements.VisualElement
	{
		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> m_Stats;

		private global::Unity.Multiplayer.Tools.NetStatsMonitor.SmoothingMethod m_SmoothingMethod;

		private global::Unity.Multiplayer.Tools.NetStatsMonitor.AggregationMethod m_AggregationMethod;

		private double? m_DecayConstant;

		private int m_SampleCount;

		private int m_SignificantDigits;

		private float m_HighlightThresholdMin = float.MinValue;

		private float m_HighlightThresholdMax = float.MaxValue;

		private global::Unity.Multiplayer.Tools.NetStats.BaseUnits m_Units;

		private bool m_DisplayAsPercentage;

		private double m_DisplayValue = double.NaN;

		private readonly global::UnityEngine.UIElements.Label m_Label = new global::UnityEngine.UIElements.Label();

		private readonly global::UnityEngine.UIElements.Label m_Value = new global::UnityEngine.UIElements.Label();

		public global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate SampleRate { get; private set; }

		internal double DisplayValue
		{
			get
			{
				return m_DisplayValue;
			}
			set
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent mantissaAndExponent = (m_DisplayAsPercentage ? global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.ToBase10(value) : global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.Base10ToBase1000(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.ToBase10(value)));
				int digitsAboveDecimal = global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.GetDigitsAboveDecimal(mantissaAndExponent, m_DisplayAsPercentage);
				float num = global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.RoundToSignificantDigits(mantissaAndExponent.Mantissa, m_SignificantDigits, digitsAboveDecimal);
				if ((double)num != m_DisplayValue)
				{
					m_DisplayValue = value;
					m_Value.text = (m_DisplayAsPercentage ? global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.Base10ToPercentageNotation(mantissaAndExponent, m_SignificantDigits, m_Units) : global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.Base1000ToEngineeringNotation(mantissaAndExponent, m_Units, num, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NumericUtils.GetDigitsBelowDecimal(m_SignificantDigits, digitsAboveDecimal)));
					UpdateHighlightUssClasses();
				}
			}
		}

		internal CounterVisualElement()
		{
			AddToClassList("rnsm-display-element");
			AddToClassList("rnsm-counter");
			m_Label.AddToClassList("rnsm-display-element-label");
			Add(m_Label);
			m_Value.AddToClassList("rnsm-counter-value");
			Add(m_Value);
		}

		internal void UpdateConfiguration(global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration config)
		{
			global::Unity.Multiplayer.Tools.NetStatsMonitor.CounterConfiguration counterConfiguration = config.CounterConfiguration;
			m_Label.text = (string.IsNullOrWhiteSpace(config.Label) ? global::Unity.Multiplayer.Tools.NetStatsMonitor.Configuration.LabelGeneration.GenerateLabel(config.Stats) : config.Label);
			m_Stats = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId>(config.Stats);
			m_Units = global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MetricsUtils.GetUnits(m_Stats, config.Label);
			m_DisplayAsPercentage = global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MetricsUtils.ShouldDisplayAsPercentage(m_Stats, config.Label);
			m_SmoothingMethod = counterConfiguration.SmoothingMethod;
			m_AggregationMethod = counterConfiguration.AggregationMethod;
			m_DecayConstant = config.DecayConstant;
			m_SampleCount = global::System.Math.Clamp(config.SampleCount, 8, 512);
			SampleRate = config.SampleRate;
			m_SignificantDigits = global::System.Math.Max(counterConfiguration.SignificantDigits, 1);
			m_HighlightThresholdMin = counterConfiguration.HighlightLowerBound;
			m_HighlightThresholdMax = counterConfiguration.HighlightUpperBound;
			UpdateHighlightUssClasses();
		}

		internal void UpdateDisplayData(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistory history, double time)
		{
			if (m_AggregationMethod == global::Unity.Multiplayer.Tools.NetStatsMonitor.AggregationMethod.Average)
			{
				_ = 1;
			}
			else
				_ = m_AggregationMethod == global::Unity.Multiplayer.Tools.NetStatsMonitor.AggregationMethod.Sum;
			_ = m_SmoothingMethod;
			bool hasValue = m_DecayConstant.HasValue;
			double num = 0.0;
			int num2 = 0;
			switch (m_SmoothingMethod)
			{
			case global::Unity.Multiplayer.Tools.NetStatsMonitor.SmoothingMethod.ExponentialMovingAverage:
			{
				if (!hasValue)
				{
					break;
				}
				double value = m_DecayConstant.Value;
				foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId stat in m_Stats)
				{
					if (!history.Data.TryGetValue(stat, out var value2))
					{
						continue;
					}
					global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage[] continuousExponentialMovingAverages = value2.ContinuousExponentialMovingAverages;
					foreach (global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage continuousExponentialMovingAverage in continuousExponentialMovingAverages)
					{
						if (continuousExponentialMovingAverage.DecayConstant == value)
						{
							global::Unity.Multiplayer.Tools.NetStats.MetricKind metricKind = stat.MetricKind;
							num = metricKind switch
							{
								global::Unity.Multiplayer.Tools.NetStats.MetricKind.Counter => num + continuousExponentialMovingAverage.GetCounterValue(time), 
								global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge => num + continuousExponentialMovingAverage.GetGaugeValue(), 
								_ => throw new global::System.NotSupportedException(string.Format("Unhandled {0} {1}", "MetricKind", metricKind)), 
							};
							num2++;
							break;
						}
					}
				}
				break;
			}
			case global::Unity.Multiplayer.Tools.NetStatsMonitor.SmoothingMethod.SimpleMovingAverage:
				foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId stat2 in m_Stats)
				{
					double? simpleMovingAverage = history.GetSimpleMovingAverage(stat2, SampleRate, m_SampleCount, time);
					if (simpleMovingAverage.HasValue)
					{
						num += simpleMovingAverage.Value;
						num2++;
					}
				}
				break;
			}
			if (m_AggregationMethod == global::Unity.Multiplayer.Tools.NetStatsMonitor.AggregationMethod.Average && num2 > 0)
			{
				num /= (double)num2;
			}
			DisplayValue = num;
		}

		private void UpdateHighlightUssClasses()
		{
			bool flag = m_DisplayValue < (double)m_HighlightThresholdMin;
			bool flag2 = m_DisplayValue > (double)m_HighlightThresholdMax;
			EnableInClassList("rnsm-counter-below-threshold", flag);
			EnableInClassList("rnsm-counter-above-threshold", flag2);
			EnableInClassList("rnsm-counter-out-of-bounds", flag || flag2);
		}
	}
}
