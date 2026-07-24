namespace UnityEngine.AdaptivePerformance.Provider
{
	public struct PerformanceDataRecord
	{
		public global::UnityEngine.AdaptivePerformance.Provider.Feature ChangeFlags { get; set; }

		public float TemperatureLevel { get; set; }

		public float TemperatureTrend { get; set; }

		public global::UnityEngine.AdaptivePerformance.WarningLevel WarningLevel { get; set; }

		public int CpuPerformanceLevel { get; set; }

		public int GpuPerformanceLevel { get; set; }

		public bool PerformanceLevelControlAvailable { get; set; }

		public float CpuFrameTime { get; set; }

		public float GpuFrameTime { get; set; }

		public float OverallFrameTime { get; set; }

		public bool CpuPerformanceBoost { get; set; }

		public bool GpuPerformanceBoost { get; set; }

		public global::UnityEngine.AdaptivePerformance.ClusterInfo ClusterInfo { get; set; }

		public global::UnityEngine.AdaptivePerformance.PerformanceMode PerformanceMode { get; set; }
	}
}
