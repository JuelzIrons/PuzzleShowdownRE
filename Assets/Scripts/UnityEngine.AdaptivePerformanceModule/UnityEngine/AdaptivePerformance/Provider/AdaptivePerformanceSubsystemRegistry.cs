namespace UnityEngine.AdaptivePerformance.Provider
{
	internal static class AdaptivePerformanceSubsystemRegistry
	{
		public static global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor RegisterDescriptor(global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor.Cinfo cinfo)
		{
			global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor adaptivePerformanceSubsystemDescriptor = new global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor(cinfo);
			global::UnityEngine.SubsystemsImplementation.SubsystemDescriptorStore.RegisterDescriptor(adaptivePerformanceSubsystemDescriptor);
			return adaptivePerformanceSubsystemDescriptor;
		}

		public static global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor> GetRegisteredDescriptors()
		{
			global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor> list = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor>();
			global::UnityEngine.SubsystemManager.GetSubsystemDescriptors(list);
			return list;
		}
	}
}
