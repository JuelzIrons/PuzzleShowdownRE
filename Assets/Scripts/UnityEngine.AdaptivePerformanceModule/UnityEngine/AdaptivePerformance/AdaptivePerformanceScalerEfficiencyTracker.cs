#define UNITY_ASSERTIONS
namespace UnityEngine.AdaptivePerformance
{
	internal class AdaptivePerformanceScalerEfficiencyTracker
	{
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler m_Scaler;

		private float m_LastAverageGpuFrameTime;

		private float m_LastAverageCpuFrameTime;

		private bool m_IsApplied;

		public bool IsRunning => m_Scaler != null;

		public void Start(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler scaler, bool isApply)
		{
			global::UnityEngine.Debug.Assert(!IsRunning, "AdaptivePerformanceScalerEfficiencyTracker is already running");
			m_Scaler = scaler;
			m_LastAverageGpuFrameTime = global::UnityEngine.AdaptivePerformance.Holder.Instance.PerformanceStatus.FrameTiming.AverageGpuFrameTime;
			m_LastAverageCpuFrameTime = global::UnityEngine.AdaptivePerformance.Holder.Instance.PerformanceStatus.FrameTiming.AverageCpuFrameTime;
			m_IsApplied = true;
		}

		public void Stop()
		{
			float num = global::UnityEngine.AdaptivePerformance.Holder.Instance.PerformanceStatus.FrameTiming.AverageGpuFrameTime - m_LastAverageGpuFrameTime;
			float num2 = global::UnityEngine.AdaptivePerformance.Holder.Instance.PerformanceStatus.FrameTiming.AverageCpuFrameTime - m_LastAverageCpuFrameTime;
			int num3 = (m_IsApplied ? 1 : (-1));
			m_Scaler.GpuImpact = num3 * (int)(num * 1000f);
			m_Scaler.CpuImpact = num3 * (int)(num2 * 1000f);
			m_Scaler = null;
		}
	}
}
