namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal static class MetricsUtils
	{
		public static global::Unity.Multiplayer.Tools.NetStats.BaseUnits GetUnits(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> metrics, string displayElementLabel)
		{
			if (metrics.Count <= 0)
			{
				return default(global::Unity.Multiplayer.Tools.NetStats.BaseUnits);
			}
			global::Unity.Multiplayer.Tools.NetStats.BaseUnits units = metrics[0].Units;
			for (int i = 1; i < metrics.Count; i++)
			{
				if (!metrics[i].Units.Equals(units))
				{
					return HandleInconvertibleUnits(metrics, displayElementLabel);
				}
			}
			return units;
		}

		private static global::Unity.Multiplayer.Tools.NetStats.BaseUnits ChooseMostPopularUnits(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> metrics)
		{
			global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.BaseUnits, int> dictionary = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.BaseUnits, int>();
			global::Unity.Multiplayer.Tools.NetStats.BaseUnits key;
			int value;
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId metric in metrics)
			{
				global::Unity.Multiplayer.Tools.NetStats.BaseUnits units = metric.Units;
				if (dictionary.ContainsKey(units))
				{
					key = units;
					value = dictionary[key]++;
				}
				else
				{
					dictionary.Add(units, 1);
				}
			}
			int num = 0;
			global::Unity.Multiplayer.Tools.NetStats.BaseUnits result = default(global::Unity.Multiplayer.Tools.NetStats.BaseUnits);
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Multiplayer.Tools.NetStats.BaseUnits, int> item in dictionary)
			{
				item.Deconstruct(out key, out value);
				global::Unity.Multiplayer.Tools.NetStats.BaseUnits baseUnits = key;
				int num2 = value;
				if (num2 > num)
				{
					num = num2;
					result = baseUnits;
				}
			}
			return result;
		}

		private static global::Unity.Multiplayer.Tools.NetStats.BaseUnits HandleInconvertibleUnits(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> metrics, string displayElementLabel)
		{
			global::System.Collections.Generic.HashSet<global::Unity.Multiplayer.Tools.NetStats.BaseUnits> hashSet = new global::System.Collections.Generic.HashSet<global::Unity.Multiplayer.Tools.NetStats.BaseUnits>();
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId metric in metrics)
			{
				hashSet.Add(metric.Units);
			}
			string text = string.Join(", ", hashSet);
			global::UnityEngine.Debug.LogWarning("Display Element " + displayElementLabel + " is configured with inconvertible units:\n {" + text + "}");
			return ChooseMostPopularUnits(metrics);
		}

		public static bool ShouldDisplayAsPercentage(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> metrics, string displayElementLabel)
		{
			if (metrics.Count <= 0)
			{
				return false;
			}
			bool displayAsPercentage = metrics[0].DisplayAsPercentage;
			for (int i = 1; i < metrics.Count; i++)
			{
				if (!metrics[i].DisplayAsPercentage.Equals(displayAsPercentage))
				{
					return HandleMismatchedDisplayAsPercentages(metrics, displayElementLabel);
				}
			}
			return displayAsPercentage;
		}

		private static bool HandleMismatchedDisplayAsPercentages(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> metrics, string displayElementLabel)
		{
			global::UnityEngine.Debug.LogWarning("Display Element " + displayElementLabel + " is configured with some stats that should be displayed as percentages and others not. This display element will display its values without percentages.");
			return false;
		}
	}
}
