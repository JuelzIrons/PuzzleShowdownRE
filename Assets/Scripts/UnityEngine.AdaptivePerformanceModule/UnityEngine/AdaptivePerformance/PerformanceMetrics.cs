namespace UnityEngine.AdaptivePerformance
{
	public struct PerformanceMetrics
	{
		public int CurrentCpuLevel { get; set; }

		public int CurrentGpuLevel { get; set; }

		public global::UnityEngine.AdaptivePerformance.PerformanceBottleneck PerformanceBottleneck { get; set; }

		public bool CpuPerformanceBoost { get; set; }

		public bool GpuPerformanceBoost { get; set; }

		public global::UnityEngine.AdaptivePerformance.ClusterInfo ClusterInfo { get; set; }
	}
}
