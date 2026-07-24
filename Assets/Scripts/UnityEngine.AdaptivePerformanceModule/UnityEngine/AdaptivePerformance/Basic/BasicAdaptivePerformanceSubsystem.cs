namespace UnityEngine.AdaptivePerformance.Basic
{
	internal class BasicAdaptivePerformanceSubsystem : global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem
	{
		internal class BasicProvider : global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem.APProvider, global::UnityEngine.AdaptivePerformance.Provider.IApplicationLifecycle, global::UnityEngine.AdaptivePerformance.Provider.IDevicePerformanceLevelControl
		{
			private global::UnityEngine.AdaptivePerformance.Provider.PerformanceDataRecord m_UpdatedPerfRecord;

			public override global::UnityEngine.AdaptivePerformance.Provider.IApplicationLifecycle ApplicationLifecycle => this;

			public override global::UnityEngine.AdaptivePerformance.Provider.IDevicePerformanceLevelControl PerformanceLevelControl => this;

			public override string Stats => "Basic provider";

			public override bool Initialized { get; set; }

			public override global::UnityEngine.AdaptivePerformance.Provider.Feature Capabilities { get; set; }

			public override global::System.Version Version => new global::System.Version(6, 0, 0);

			public int MaxCpuPerformanceLevel => -1;

			public int MaxGpuPerformanceLevel => -1;

			public BasicProvider()
			{
				Capabilities = global::UnityEngine.AdaptivePerformance.Provider.Feature.None;
				m_UpdatedPerfRecord.PerformanceLevelControlAvailable = false;
				m_UpdatedPerfRecord.CpuPerformanceBoost = false;
				m_UpdatedPerfRecord.GpuPerformanceBoost = false;
				m_UpdatedPerfRecord.TemperatureLevel = -1f;
				m_UpdatedPerfRecord.TemperatureTrend = -1f;
			}

			protected internal override bool TryInitialize()
			{
				Initialized = true;
				return Initialized;
			}

			public override void Start()
			{
				m_Running = true;
			}

			public override void Stop()
			{
				m_Running = false;
			}

			public override void Destroy()
			{
				Initialized = false;
			}

			public override global::UnityEngine.AdaptivePerformance.Provider.PerformanceDataRecord Update()
			{
				m_UpdatedPerfRecord.ChangeFlags &= Capabilities;
				global::UnityEngine.AdaptivePerformance.Provider.PerformanceDataRecord updatedPerfRecord = m_UpdatedPerfRecord;
				m_UpdatedPerfRecord.ChangeFlags = global::UnityEngine.AdaptivePerformance.Provider.Feature.None;
				return updatedPerfRecord;
			}

			public void ApplicationPause()
			{
			}

			public void ApplicationResume()
			{
			}

			public bool SetPerformanceLevel(ref int cpuLevel, ref int gpuLevel)
			{
				if (!m_UpdatedPerfRecord.PerformanceLevelControlAvailable)
				{
					m_UpdatedPerfRecord.CpuPerformanceLevel = -1;
					m_UpdatedPerfRecord.ChangeFlags |= global::UnityEngine.AdaptivePerformance.Provider.Feature.CpuPerformanceLevel;
					m_UpdatedPerfRecord.GpuPerformanceLevel = -1;
					m_UpdatedPerfRecord.ChangeFlags |= global::UnityEngine.AdaptivePerformance.Provider.Feature.GpuPerformanceLevel;
					return false;
				}
				return cpuLevel >= 0 && gpuLevel >= 0 && cpuLevel <= MaxCpuPerformanceLevel && gpuLevel <= MaxGpuPerformanceLevel;
			}

			public bool EnableCpuBoost()
			{
				return false;
			}

			public bool EnableGpuBoost()
			{
				return false;
			}
		}
	}
}
