namespace UnityEngine.AdaptivePerformance
{
	public interface IPerformanceModeStatus
	{
		global::UnityEngine.AdaptivePerformance.PerformanceMode PerformanceMode { get; }

		event global::UnityEngine.AdaptivePerformance.PerformanceModeEventHandler PerformanceModeEvent;
	}
}
