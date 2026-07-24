namespace UnityEngine.AdaptivePerformance
{
	public interface IThermalStatus
	{
		global::UnityEngine.AdaptivePerformance.ThermalMetrics ThermalMetrics { get; }

		event global::UnityEngine.AdaptivePerformance.ThermalEventHandler ThermalEvent;
	}
}
