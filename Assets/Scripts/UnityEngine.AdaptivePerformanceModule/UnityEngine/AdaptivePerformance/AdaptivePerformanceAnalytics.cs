namespace UnityEngine.AdaptivePerformance
{
	internal static class AdaptivePerformanceAnalytics
	{
		internal static class AnalyticsLog
		{
			[global::System.Diagnostics.Conditional("ADAPTIVE_PERFORMANCE_ANALYTICS_LOGGING")]
			public static void Debug(string format, params object[] args)
			{
			}
		}

		[global::System.Diagnostics.Conditional("UNITY_ANALYTICS")]
		public static void RegisterFeature(string feature, bool status)
		{
		}

		[global::System.Diagnostics.Conditional("UNITY_ANALYTICS")]
		public static void SendAdaptiveStartupEvent(global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem subsystem)
		{
		}

		[global::System.Diagnostics.Conditional("UNITY_ANALYTICS")]
		public static void SendAdaptiveFeatureUpdateEvent(string feature, bool status)
		{
		}

		[global::System.Diagnostics.Conditional("UNITY_ANALYTICS")]
		public static void SendAdaptivePerformanceThermalEvent(global::UnityEngine.AdaptivePerformance.ThermalMetrics thermalMetrics)
		{
		}
	}
}
