namespace UnityEngine.AdaptivePerformance.Provider
{
	public class AdaptivePerformanceSubsystem : global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemBase<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem, global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor, global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem.APProvider>
	{
		public abstract class APProvider : global::UnityEngine.SubsystemsImplementation.SubsystemProvider<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem>
		{
			protected new bool m_Running;

			public abstract global::UnityEngine.AdaptivePerformance.Provider.Feature Capabilities { get; set; }

			public abstract global::UnityEngine.AdaptivePerformance.Provider.IApplicationLifecycle ApplicationLifecycle { get; }

			public abstract global::UnityEngine.AdaptivePerformance.Provider.IDevicePerformanceLevelControl PerformanceLevelControl { get; }

			public abstract global::System.Version Version { get; }

			public virtual string Stats => "";

			public abstract bool Initialized { get; set; }

			public new bool running => m_Running;

			public abstract global::UnityEngine.AdaptivePerformance.Provider.PerformanceDataRecord Update();
		}

		public override global::UnityEngine.AdaptivePerformance.Provider.IApplicationLifecycle ApplicationLifecycle => base.provider.ApplicationLifecycle;

		public override global::UnityEngine.AdaptivePerformance.Provider.IDevicePerformanceLevelControl PerformanceLevelControl => base.provider.PerformanceLevelControl;

		public override global::System.Version Version => base.provider.Version;

		public override global::UnityEngine.AdaptivePerformance.Provider.Feature Capabilities
		{
			get
			{
				return base.provider.Capabilities;
			}
			protected set
			{
				base.provider.Capabilities = value;
			}
		}

		public override string Stats => base.provider.Stats;

		public override bool Initialized
		{
			get
			{
				return base.provider.Initialized;
			}
			protected set
			{
				base.provider.Initialized = value;
			}
		}

		public override global::UnityEngine.AdaptivePerformance.Provider.PerformanceDataRecord Update()
		{
			return base.provider.Update();
		}
	}
}
