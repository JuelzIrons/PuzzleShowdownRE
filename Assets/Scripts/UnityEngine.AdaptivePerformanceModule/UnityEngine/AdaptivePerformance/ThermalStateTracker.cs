namespace UnityEngine.AdaptivePerformance
{
	internal class ThermalStateTracker
	{
		private float warningTemp = 1f;

		private float throttlingTemp = 1f;

		public global::UnityEngine.AdaptivePerformance.StateAction Update()
		{
			if (!global::UnityEngine.AdaptivePerformance.Holder.Instance.SupportedFeature(global::UnityEngine.AdaptivePerformance.Provider.Feature.TemperatureLevel))
			{
				return global::UnityEngine.AdaptivePerformance.StateAction.Stale;
			}
			float temperatureTrend = global::UnityEngine.AdaptivePerformance.Holder.Instance.ThermalStatus.ThermalMetrics.TemperatureTrend;
			float temperatureLevel = global::UnityEngine.AdaptivePerformance.Holder.Instance.ThermalStatus.ThermalMetrics.TemperatureLevel;
			global::UnityEngine.AdaptivePerformance.WarningLevel warningLevel = global::UnityEngine.AdaptivePerformance.Holder.Instance.ThermalStatus.ThermalMetrics.WarningLevel;
			if (warningLevel == global::UnityEngine.AdaptivePerformance.WarningLevel.ThrottlingImminent && warningTemp == 1f)
			{
				warningTemp = temperatureLevel;
			}
			if (warningLevel == global::UnityEngine.AdaptivePerformance.WarningLevel.Throttling && throttlingTemp == 1f)
			{
				throttlingTemp = temperatureLevel;
			}
			if (warningLevel == global::UnityEngine.AdaptivePerformance.WarningLevel.Throttling || temperatureLevel >= throttlingTemp)
			{
				return global::UnityEngine.AdaptivePerformance.StateAction.FastDecrease;
			}
			if (warningLevel == global::UnityEngine.AdaptivePerformance.WarningLevel.ThrottlingImminent || temperatureLevel >= warningTemp)
			{
				if (temperatureLevel > (warningTemp + throttlingTemp) / 2f)
				{
					return global::UnityEngine.AdaptivePerformance.StateAction.Decrease;
				}
				if (temperatureTrend <= 0f)
				{
					return global::UnityEngine.AdaptivePerformance.StateAction.Stale;
				}
				if ((double)temperatureTrend > 0.5)
				{
					return global::UnityEngine.AdaptivePerformance.StateAction.FastDecrease;
				}
				return global::UnityEngine.AdaptivePerformance.StateAction.Decrease;
			}
			if (warningLevel == global::UnityEngine.AdaptivePerformance.WarningLevel.NoWarning && temperatureLevel < warningTemp)
			{
				if (temperatureTrend <= 0f)
				{
					return global::UnityEngine.AdaptivePerformance.StateAction.Increase;
				}
				if ((double)temperatureTrend > 0.5)
				{
					return global::UnityEngine.AdaptivePerformance.StateAction.FastDecrease;
				}
				if ((double)temperatureTrend > 0.1)
				{
					return global::UnityEngine.AdaptivePerformance.StateAction.Decrease;
				}
			}
			return global::UnityEngine.AdaptivePerformance.StateAction.Stale;
		}
	}
}
