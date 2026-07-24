namespace UnityEngine.AdaptivePerformance.Basic
{
	internal class BasicProviderDescriptorRegistration
	{
		[global::UnityEngine.Scripting.RequiredByNativeCode(false)]
		[global::System.Diagnostics.CodeAnalysis.DynamicDependency("#ctor()", typeof(global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem))]
		[global::System.Diagnostics.CodeAnalysis.DynamicDependency("#ctor()", typeof(global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem.BasicProvider))]
		private static global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor RegisterDescriptor()
		{
			return global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor.RegisterDescriptor(new global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor.Cinfo
			{
				id = "BasicAdaptivePerformanceSubsystem",
				providerType = typeof(global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem.BasicProvider),
				subsystemTypeOverride = typeof(global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem)
			});
		}
	}
}
