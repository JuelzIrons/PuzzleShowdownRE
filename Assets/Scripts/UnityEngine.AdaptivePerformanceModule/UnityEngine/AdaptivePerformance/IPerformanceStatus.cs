namespace UnityEngine.AdaptivePerformance
{
	public interface IPerformanceStatus
	{
		global::UnityEngine.AdaptivePerformance.PerformanceMetrics PerformanceMetrics { get; }

		global::UnityEngine.AdaptivePerformance.FrameTiming FrameTiming { get; }

		global::UnityEngine.AdaptivePerformance.PerformanceMode PerformanceMode { get; }

		event global::UnityEngine.AdaptivePerformance.PerformanceBottleneckChangeHandler PerformanceBottleneckChangeEvent;

		event global::UnityEngine.AdaptivePerformance.PerformanceLevelChangeHandler PerformanceLevelChangeEvent;

		event global::UnityEngine.AdaptivePerformance.PerformanceBoostChangeHandler PerformanceBoostChangeEvent;
	}
}
