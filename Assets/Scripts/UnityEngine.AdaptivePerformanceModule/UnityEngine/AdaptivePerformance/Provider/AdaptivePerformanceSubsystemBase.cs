namespace UnityEngine.AdaptivePerformance.Provider
{
	public abstract class AdaptivePerformanceSubsystemBase<TSubsystem, TSubsystemDescriptor, TProvider> : global::UnityEngine.SubsystemsImplementation.SubsystemWithProvider<TSubsystem, TSubsystemDescriptor, TProvider> where TSubsystem : global::UnityEngine.SubsystemsImplementation.SubsystemWithProvider, new() where TSubsystemDescriptor : global::UnityEngine.SubsystemsImplementation.SubsystemDescriptorWithProvider where TProvider : global::UnityEngine.SubsystemsImplementation.SubsystemProvider<TSubsystem>
	{
		public abstract global::UnityEngine.AdaptivePerformance.Provider.Feature Capabilities { get; protected set; }

		public abstract global::UnityEngine.AdaptivePerformance.Provider.IApplicationLifecycle ApplicationLifecycle { get; }

		public abstract global::UnityEngine.AdaptivePerformance.Provider.IDevicePerformanceLevelControl PerformanceLevelControl { get; }

		public abstract global::System.Version Version { get; }

		public abstract string Stats { get; }

		public abstract bool Initialized { get; protected set; }

		public abstract global::UnityEngine.AdaptivePerformance.Provider.PerformanceDataRecord Update();
	}
}
