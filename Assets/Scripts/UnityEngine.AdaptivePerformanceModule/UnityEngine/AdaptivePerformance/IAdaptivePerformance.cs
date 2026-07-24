namespace UnityEngine.AdaptivePerformance
{
	public interface IAdaptivePerformance
	{
		bool Initialized { get; }

		bool Active { get; }

		global::UnityEngine.AdaptivePerformance.IThermalStatus ThermalStatus { get; }

		global::UnityEngine.AdaptivePerformance.IPerformanceStatus PerformanceStatus { get; }

		global::UnityEngine.AdaptivePerformance.IDevicePerformanceControl DevicePerformanceControl { get; }

		global::UnityEngine.AdaptivePerformance.IPerformanceModeStatus PerformanceModeStatus { get; }

		global::UnityEngine.AdaptivePerformance.IDevelopmentSettings DevelopmentSettings { get; }

		global::UnityEngine.AdaptivePerformance.AdaptivePerformanceIndexer Indexer { get; }

		global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings Settings { get; }

		global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem Subsystem { get; }

		bool SupportedFeature(global::UnityEngine.AdaptivePerformance.Provider.Feature feature);

		void InitializeAdaptivePerformance();

		void StartAdaptivePerformance();

		void StopAdaptivePerformance();

		void DeinitializeAdaptivePerformance();
	}
}
