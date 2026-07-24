namespace Unity.Multiplayer.Tools.MetricTypes
{
	internal static class DirectedMetricTypeExtensions
	{
		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType, string> s_Identifiers;

		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType, string> s_DisplayNames;

		static DirectedMetricTypeExtensions()
		{
			s_Identifiers = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType, string>();
			s_DisplayNames = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType, string>();
			global::Unity.Multiplayer.Tools.MetricTypes.MetricType[] values = global::Unity.Multiplayer.Tools.Common.EnumUtil.GetValues<global::Unity.Multiplayer.Tools.MetricTypes.MetricType>();
			global::Unity.Multiplayer.Tools.Common.NetworkDirection[] values2 = global::Unity.Multiplayer.Tools.Common.EnumUtil.GetValues<global::Unity.Multiplayer.Tools.Common.NetworkDirection>();
			global::Unity.Multiplayer.Tools.MetricTypes.MetricType[] array = values;
			for (int i = 0; i < array.Length; i++)
			{
				global::Unity.Multiplayer.Tools.MetricTypes.MetricType metricType = array[i];
				global::Unity.Multiplayer.Tools.Common.NetworkDirection[] array2 = values2;
				for (int j = 0; j < array2.Length; j++)
				{
					global::Unity.Multiplayer.Tools.Common.NetworkDirection direction = array2[j];
					global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType directedMetric = metricType.GetDirectedMetric(direction);
					string text = metricType.ToString() + direction;
					s_Identifiers[directedMetric] = text;
					s_DisplayNames[directedMetric] = global::Unity.Multiplayer.Tools.Common.StringUtil.AddSpacesToCamelCase(text);
				}
			}
		}

		internal static global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType GetDirectedMetric(this global::Unity.Multiplayer.Tools.MetricTypes.MetricType metricType, global::Unity.Multiplayer.Tools.Common.NetworkDirection direction)
		{
			return (global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType)(((int)metricType << 2) | (int)(direction & global::Unity.Multiplayer.Tools.Common.NetworkDirection.SentAndReceived));
		}

		internal static global::Unity.Multiplayer.Tools.MetricTypes.MetricType GetMetric(this global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType directedMetric)
		{
			return (global::Unity.Multiplayer.Tools.MetricTypes.MetricType)((int)directedMetric >> 2);
		}

		internal static global::Unity.Multiplayer.Tools.Common.NetworkDirection GetDirection(this global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType directedMetric)
		{
			return (global::Unity.Multiplayer.Tools.Common.NetworkDirection)(directedMetric & (global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType)3);
		}

		internal static global::Unity.Multiplayer.Tools.NetStats.MetricId GetId(this global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType directedMetric)
		{
			return global::Unity.Multiplayer.Tools.NetStats.MetricId.Create(directedMetric);
		}

		internal static string GetDisplayName(this global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType directedMetric)
		{
			if (s_DisplayNames.TryGetValue(directedMetric, out var value))
			{
				return value;
			}
			return directedMetric.ToString();
		}
	}
}
