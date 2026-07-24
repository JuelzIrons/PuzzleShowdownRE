namespace UnityEngine.AdaptivePerformance.Provider
{
	public sealed class AdaptivePerformanceSubsystemDescriptor : global::UnityEngine.SubsystemsImplementation.SubsystemDescriptorWithProvider<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem, global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem.APProvider>
	{
		public struct Cinfo
		{
			public string id { get; set; }

			public global::System.Type providerType { get; set; }

			public global::System.Type subsystemTypeOverride { get; set; }

			[global::System.Obsolete("AdaptivePerformanceSubsystem no longer supports the deprecated set of base classes for subsystems as of Unity 2023.1. Use providerType and, optionally, subsystemTypeOverride instead.", true)]
			public global::System.Type subsystemImplementationType { get; set; }
		}

		public AdaptivePerformanceSubsystemDescriptor(global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor.Cinfo cinfo)
		{
			base.id = cinfo.id;
			base.providerType = cinfo.providerType;
			base.subsystemTypeOverride = cinfo.subsystemTypeOverride;
		}

		public static global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor RegisterDescriptor(global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor.Cinfo cinfo)
		{
			global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor> registeredDescriptors = global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemRegistry.GetRegisteredDescriptors();
			foreach (global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor item in registeredDescriptors)
			{
				if (item.id == cinfo.id)
				{
					return item;
				}
			}
			return global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemRegistry.RegisterDescriptor(cinfo);
		}
	}
}
