namespace UnityEngine.AdaptivePerformance
{
	public interface IDevicePerformanceControl
	{
		bool AutomaticPerformanceControl { get; set; }

		global::UnityEngine.AdaptivePerformance.PerformanceControlMode PerformanceControlMode { get; }

		int MaxCpuPerformanceLevel { get; }

		int MaxGpuPerformanceLevel { get; }

		int CpuLevel { get; set; }

		int GpuLevel { get; set; }

		bool CpuPerformanceBoost { get; set; }

		bool GpuPerformanceBoost { get; set; }
	}
}
