namespace UnityEngine.AdaptivePerformance.Basic
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.AdaptivePerformanceModule" })]
	internal class BasicProviderLoader : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoaderHelper
	{
		private static global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor> s_BasicSubsystemDescriptors = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor>();

		public override bool Initialized => BasicSubsystem != null;

		public override bool Running => BasicSubsystem != null && BasicSubsystem.running;

		public global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem BasicSubsystem => GetLoadedSubsystem<global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem>();

		public override global::UnityEngine.ISubsystem GetDefaultSubsystem()
		{
			return BasicSubsystem;
		}

		public override global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings GetSettings()
		{
			return global::UnityEngine.AdaptivePerformance.Basic.BasicProviderSettings.GetSettings();
		}

		public override bool Initialize()
		{
			CreateSubsystem<global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor, global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem>(s_BasicSubsystemDescriptors, "BasicAdaptivePerformanceSubsystem");
			if (BasicSubsystem == null)
			{
				global::UnityEngine.Debug.LogError("Unable to start the Basic subsystem.");
			}
			return BasicSubsystem != null;
		}

		public override bool Start()
		{
			StartSubsystem<global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem>();
			return true;
		}

		public override bool Stop()
		{
			StopSubsystem<global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem>();
			return true;
		}

		public override bool Deinitialize()
		{
			DestroySubsystem<global::UnityEngine.AdaptivePerformance.Basic.BasicAdaptivePerformanceSubsystem>();
			return base.Deinitialize();
		}
	}
}
