namespace UnityEngine.AdaptivePerformance
{
	internal class AdaptivePerformanceManager : global::UnityEngine.MonoBehaviour, global::UnityEngine.AdaptivePerformance.IAdaptivePerformance, global::UnityEngine.AdaptivePerformance.IThermalStatus, global::UnityEngine.AdaptivePerformance.IPerformanceStatus, global::UnityEngine.AdaptivePerformance.IDevicePerformanceControl, global::UnityEngine.AdaptivePerformance.IDevelopmentSettings, global::UnityEngine.AdaptivePerformance.IPerformanceModeStatus
	{
		private bool m_JustResumed = false;

		private int m_RequestedCpuLevel = -1;

		private int m_RequestedGpuLevel = -1;

		private bool m_NewUserPerformanceLevelRequest = false;

		private bool m_RequestedCpuBoost = false;

		private bool m_RequestedGpuBoost = false;

		private bool m_NewUserCpuPerformanceBoostRequest = false;

		private bool m_NewUserGpuPerformanceBoostRequest = false;

		private global::UnityEngine.AdaptivePerformance.ThermalMetrics m_ThermalMetrics = new global::UnityEngine.AdaptivePerformance.ThermalMetrics
		{
			WarningLevel = global::UnityEngine.AdaptivePerformance.WarningLevel.NoWarning,
			TemperatureLevel = -1f,
			TemperatureTrend = 0f
		};

		private global::UnityEngine.AdaptivePerformance.PerformanceMetrics m_PerformanceMetrics = new global::UnityEngine.AdaptivePerformance.PerformanceMetrics
		{
			CurrentCpuLevel = -1,
			CurrentGpuLevel = -1,
			PerformanceBottleneck = global::UnityEngine.AdaptivePerformance.PerformanceBottleneck.Unknown
		};

		private global::UnityEngine.AdaptivePerformance.FrameTiming m_FrameTiming = new global::UnityEngine.AdaptivePerformance.FrameTiming
		{
			CurrentFrameTime = -1f,
			AverageFrameTime = -1f,
			CurrentGpuFrameTime = -1f,
			AverageGpuFrameTime = -1f,
			CurrentCpuFrameTime = -1f,
			AverageCpuFrameTime = -1f
		};

		private global::UnityEngine.AdaptivePerformance.PerformanceMode m_PerformanceMode = global::UnityEngine.AdaptivePerformance.PerformanceMode.Unknown;

		private bool m_AutomaticPerformanceControl;

		private bool m_AutomaticPerformanceControlChanged;

		private global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings m_Settings;

		private global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem m_Subsystem = null;

		private global::UnityEngine.AdaptivePerformance.DevicePerformanceControlImpl m_DevicePerfControl;

		private global::UnityEngine.AdaptivePerformance.AutoPerformanceLevelController m_AutoPerformanceLevelController;

		private global::UnityEngine.AdaptivePerformance.AutoPerformanceModeController m_AutoPerformanceModeController;

		private global::UnityEngine.AdaptivePerformance.CpuTimeProvider m_CpuFrameTimeProvider;

		private global::UnityEngine.AdaptivePerformance.GpuTimeProvider m_GpuFrameTimeProvider;

		private global::UnityEngine.AdaptivePerformance.Provider.IApplicationLifecycle m_AppLifecycle;

		private global::UnityEngine.AdaptivePerformance.TemperatureTrend m_TemperatureTrend;

		private bool m_UseProviderOverallFrameTime = false;

		private global::UnityEngine.WaitForEndOfFrame m_WaitForEndOfFrame = new global::UnityEngine.WaitForEndOfFrame();

		private int m_FrameCount = 0;

		private global::UnityEngine.AdaptivePerformance.RunningAverage m_OverallFrameTime = new global::UnityEngine.AdaptivePerformance.RunningAverage();

		private float m_OverallFrameTimeAccu = 0f;

		private global::UnityEngine.AdaptivePerformance.RunningAverage m_GpuFrameTime = new global::UnityEngine.AdaptivePerformance.RunningAverage();

		private global::UnityEngine.AdaptivePerformance.RunningAverage m_CpuFrameTime = new global::UnityEngine.AdaptivePerformance.RunningAverage();

		public global::UnityEngine.AdaptivePerformance.ThermalMetrics ThermalMetrics => m_ThermalMetrics;

		public global::UnityEngine.AdaptivePerformance.PerformanceMetrics PerformanceMetrics => m_PerformanceMetrics;

		public global::UnityEngine.AdaptivePerformance.FrameTiming FrameTiming => m_FrameTiming;

		public global::UnityEngine.AdaptivePerformance.PerformanceMode PerformanceMode => m_PerformanceMode;

		public bool Logging
		{
			get
			{
				return global::UnityEngine.AdaptivePerformance.APLog.enabled;
			}
			set
			{
				global::UnityEngine.AdaptivePerformance.APLog.enabled = value;
			}
		}

		public int LoggingFrequencyInFrames { get; set; }

		public bool Initialized => m_Subsystem != null && m_Subsystem.Initialized && global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance != null && global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.IsProviderInitialized;

		public bool Active => m_Subsystem != null && m_Subsystem.running && global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance != null && global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.IsProviderInitialized && global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.IsProviderStarted;

		public int MaxCpuPerformanceLevel => (m_DevicePerfControl != null) ? m_DevicePerfControl.MaxCpuPerformanceLevel : (-1);

		public int MaxGpuPerformanceLevel => (m_DevicePerfControl != null) ? m_DevicePerfControl.MaxGpuPerformanceLevel : (-1);

		public bool AutomaticPerformanceControl
		{
			get
			{
				return m_AutomaticPerformanceControl;
			}
			set
			{
				m_AutomaticPerformanceControl = value;
				m_AutomaticPerformanceControlChanged = true;
			}
		}

		public global::UnityEngine.AdaptivePerformance.PerformanceControlMode PerformanceControlMode => (m_DevicePerfControl != null) ? m_DevicePerfControl.PerformanceControlMode : global::UnityEngine.AdaptivePerformance.PerformanceControlMode.System;

		public int CpuLevel
		{
			get
			{
				return m_RequestedCpuLevel;
			}
			set
			{
				m_RequestedCpuLevel = value;
				m_NewUserPerformanceLevelRequest = true;
			}
		}

		public int GpuLevel
		{
			get
			{
				return m_RequestedGpuLevel;
			}
			set
			{
				m_RequestedGpuLevel = value;
				m_NewUserPerformanceLevelRequest = true;
			}
		}

		public bool CpuPerformanceBoost
		{
			get
			{
				return m_RequestedCpuBoost;
			}
			set
			{
				m_RequestedCpuBoost = value;
				m_NewUserCpuPerformanceBoostRequest = true;
			}
		}

		public bool GpuPerformanceBoost
		{
			get
			{
				return m_RequestedGpuBoost;
			}
			set
			{
				m_RequestedGpuBoost = value;
				m_NewUserGpuPerformanceBoostRequest = true;
			}
		}

		public global::UnityEngine.AdaptivePerformance.IDevelopmentSettings DevelopmentSettings => this;

		public global::UnityEngine.AdaptivePerformance.IThermalStatus ThermalStatus => this;

		public global::UnityEngine.AdaptivePerformance.IPerformanceStatus PerformanceStatus => this;

		public global::UnityEngine.AdaptivePerformance.IDevicePerformanceControl DevicePerformanceControl => this;

		public global::UnityEngine.AdaptivePerformance.IPerformanceModeStatus PerformanceModeStatus => this;

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceIndexer Indexer { get; private set; }

		public global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings Settings
		{
			get
			{
				return m_Settings;
			}
			private set
			{
				m_Settings = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem Subsystem => m_Subsystem;

		public event global::UnityEngine.AdaptivePerformance.ThermalEventHandler ThermalEvent;

		public event global::UnityEngine.AdaptivePerformance.PerformanceBottleneckChangeHandler PerformanceBottleneckChangeEvent;

		public event global::UnityEngine.AdaptivePerformance.PerformanceLevelChangeHandler PerformanceLevelChangeEvent;

		public event global::UnityEngine.AdaptivePerformance.PerformanceBoostChangeHandler PerformanceBoostChangeEvent;

		public event global::UnityEngine.AdaptivePerformance.PerformanceModeEventHandler PerformanceModeEvent;

		public bool SupportedFeature(global::UnityEngine.AdaptivePerformance.Provider.Feature feature)
		{
			return m_Subsystem != null && m_Subsystem.Capabilities.HasFlag(feature);
		}

		public void Awake()
		{
			global::UnityEngine.AdaptivePerformance.APLog.enabled = true;
			if (!(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance == null))
			{
				if (!global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.InitManagerOnStart)
				{
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Adaptive Performance is disabled via Settings.");
				}
				else
				{
					InitializeAdaptivePerformance();
				}
			}
		}

		private void LogThermalEvent(global::UnityEngine.AdaptivePerformance.ThermalMetrics ev)
		{
			global::UnityEngine.AdaptivePerformance.APLog.Debug("[thermal event] temperature level: {0}, warning level: {1}, thermal trend: {2}", ev.TemperatureLevel, ev.WarningLevel, ev.TemperatureTrend);
		}

		private void LogBottleneckEvent(global::UnityEngine.AdaptivePerformance.PerformanceBottleneckChangeEventArgs ev)
		{
			global::UnityEngine.AdaptivePerformance.APLog.Debug("[perf event] bottleneck: {0}", ev.PerformanceBottleneck);
		}

		private void LogBoostEvent(global::UnityEngine.AdaptivePerformance.PerformanceBoostChangeEventArgs ev)
		{
			global::UnityEngine.AdaptivePerformance.APLog.Debug("[perf event] CPU boost: {0}, GPU boost: {1}", ev.CpuBoost, ev.GpuBoost);
		}

		private void LogPerformanceModeEvent(global::UnityEngine.AdaptivePerformance.PerformanceMode performanceMode)
		{
			global::UnityEngine.AdaptivePerformance.APLog.Debug("[performance mode event] performance mode: {0}", performanceMode);
		}

		private static string ToStringWithSign(int x)
		{
			return x.ToString("+#;-#;0");
		}

		private void LogPerformanceLevelEvent(global::UnityEngine.AdaptivePerformance.PerformanceLevelChangeEventArgs ev)
		{
			global::UnityEngine.AdaptivePerformance.APLog.Debug("[perf level change] cpu: {0}({1}) gpu: {2}({3}) control mode: {4} manual override: {5}", ev.CpuLevel, ToStringWithSign(ev.CpuLevelDelta), ev.GpuLevel, ToStringWithSign(ev.GpuLevelDelta), ev.PerformanceControlMode, ev.ManualOverride);
		}

		private void AddNonNegativeValue(global::UnityEngine.AdaptivePerformance.RunningAverage runningAverage, float value)
		{
			if (value >= 0f && value < 1f)
			{
				runningAverage.AddValue(value);
			}
		}

		public void LateUpdate()
		{
			if (Active && (m_CpuFrameTimeProvider != null || m_GpuFrameTimeProvider != null) && WillCurrentFrameRender())
			{
				if (m_CpuFrameTimeProvider != null)
				{
					m_CpuFrameTimeProvider.Measure();
				}
				if (m_GpuFrameTimeProvider != null)
				{
					m_GpuFrameTimeProvider.Measure();
				}
			}
		}

		public void Update()
		{
			if (!Active)
			{
				return;
			}
			UpdateSubsystem();
			Indexer.Update();
			if (global::UnityEngine.Profiling.Profiler.enabled)
			{
				CollectProfilerStats();
			}
			if (global::UnityEngine.AdaptivePerformance.APLog.enabled && LoggingFrequencyInFrames > 0)
			{
				m_FrameCount++;
				if (m_FrameCount % LoggingFrequencyInFrames == 0)
				{
					global::UnityEngine.AdaptivePerformance.APLog.Debug(m_Subsystem.Stats);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Performance level CPU={0}/{1} GPU={2}/{3} thermal warn={4}({5}) thermal level={6} mode={7}", m_PerformanceMetrics.CurrentCpuLevel, MaxCpuPerformanceLevel, m_PerformanceMetrics.CurrentGpuLevel, MaxGpuPerformanceLevel, m_ThermalMetrics.WarningLevel, (int)m_ThermalMetrics.WarningLevel, m_ThermalMetrics.TemperatureLevel, m_DevicePerfControl.PerformanceControlMode);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Average GPU frametime = {0} ms (Current = {1} ms)", m_FrameTiming.AverageGpuFrameTime * 1000f, m_FrameTiming.CurrentGpuFrameTime * 1000f);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Average CPU frametime = {0} ms (Current = {1} ms)", m_FrameTiming.AverageCpuFrameTime * 1000f, m_FrameTiming.CurrentCpuFrameTime * 1000f);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Average frametime = {0} ms (Current = {1} ms)", m_FrameTiming.AverageFrameTime * 1000f, m_FrameTiming.CurrentFrameTime * 1000f);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Bottleneck {0}, ThermalTrend {1}", m_PerformanceMetrics.PerformanceBottleneck, m_ThermalMetrics.TemperatureTrend);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("CPU Boost Mode {0}, GPU Boost Mode {1}", m_PerformanceMetrics.CpuPerformanceBoost, m_PerformanceMetrics.GpuPerformanceBoost);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Cluster Info = Big Cores: {0} Medium Cores: {1} Little Cores: {2}", m_PerformanceMetrics.ClusterInfo.BigCore, m_PerformanceMetrics.ClusterInfo.MediumCore, m_PerformanceMetrics.ClusterInfo.LittleCore);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("FPS = {0}", 1f / m_FrameTiming.AverageFrameTime);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Performance Mode = {0}", m_PerformanceMode);
				}
			}
		}

		private void CollectProfilerStats()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CurrentCPUMarker.Sample(m_FrameTiming.CurrentCpuFrameTime * 1E+09f);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.AvgCPUMarker.Sample(m_FrameTiming.AverageCpuFrameTime * 1E+09f);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CurrentGPUMarker.Sample(m_FrameTiming.CurrentGpuFrameTime * 1E+09f);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.AvgGPUMarker.Sample(m_FrameTiming.AverageGpuFrameTime * 1E+09f);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CurrentCPULevelMarker.Sample(m_PerformanceMetrics.CurrentCpuLevel);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CurrentGPULevelMarker.Sample(m_PerformanceMetrics.CurrentGpuLevel);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CurrentFrametimeMarker.Sample(m_FrameTiming.CurrentFrameTime * 1E+09f);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.AvgFrametimeMarker.Sample(m_FrameTiming.AverageFrameTime * 1E+09f);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.WarningLevelMarker.Sample((int)m_ThermalMetrics.WarningLevel);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.TemperatureLevelMarker.Sample(m_ThermalMetrics.TemperatureLevel);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.TemperatureTrendMarker.Sample(m_ThermalMetrics.TemperatureTrend);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.BottleneckMarker.Sample((int)m_PerformanceMetrics.PerformanceBottleneck);
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.PerformanceModeMarker.Sample((int)m_PerformanceMode);
		}

		private void AccumulateTimingValue(ref float accu, float newValue)
		{
			if (!(accu < 0f))
			{
				if (newValue >= 0f)
				{
					accu += newValue;
				}
				else
				{
					accu = -1f;
				}
			}
		}

		private void UpdateSubsystem()
		{
			global::UnityEngine.AdaptivePerformance.Provider.PerformanceDataRecord performanceDataRecord = m_Subsystem.Update();
			m_ThermalMetrics.WarningLevel = performanceDataRecord.WarningLevel;
			m_ThermalMetrics.TemperatureLevel = performanceDataRecord.TemperatureLevel;
			if (!m_JustResumed)
			{
				if (!m_UseProviderOverallFrameTime)
				{
					AccumulateTimingValue(ref m_OverallFrameTimeAccu, global::UnityEngine.Time.unscaledDeltaTime);
				}
				if (WillCurrentFrameRender())
				{
					AddNonNegativeValue(m_OverallFrameTime, m_UseProviderOverallFrameTime ? performanceDataRecord.OverallFrameTime : m_OverallFrameTimeAccu);
					AddNonNegativeValue(m_GpuFrameTime, (m_GpuFrameTimeProvider == null) ? performanceDataRecord.GpuFrameTime : m_GpuFrameTimeProvider.GpuFrameTime);
					AddNonNegativeValue(m_CpuFrameTime, (m_CpuFrameTimeProvider == null) ? performanceDataRecord.CpuFrameTime : m_CpuFrameTimeProvider.CpuFrameTime);
					m_OverallFrameTimeAccu = 0f;
				}
				m_TemperatureTrend.Update(performanceDataRecord.TemperatureTrend, performanceDataRecord.TemperatureLevel, performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.TemperatureLevel), global::UnityEngine.Time.time);
			}
			else
			{
				m_TemperatureTrend.Reset();
				m_JustResumed = false;
			}
			m_ThermalMetrics.TemperatureTrend = m_TemperatureTrend.ThermalTrend;
			m_FrameTiming.AverageFrameTime = m_OverallFrameTime.GetAverageOr(-1f);
			m_FrameTiming.CurrentFrameTime = m_OverallFrameTime.GetMostRecentValueOr(-1f);
			m_FrameTiming.AverageGpuFrameTime = m_GpuFrameTime.GetAverageOr(-1f);
			m_FrameTiming.CurrentGpuFrameTime = m_GpuFrameTime.GetMostRecentValueOr(-1f);
			m_FrameTiming.AverageCpuFrameTime = m_CpuFrameTime.GetAverageOr(-1f);
			m_FrameTiming.CurrentCpuFrameTime = m_CpuFrameTime.GetMostRecentValueOr(-1f);
			float num = EffectiveTargetFrameRate();
			float targetFrameTime = -1f;
			if (num > 0f)
			{
				targetFrameTime = 1f / num;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			global::UnityEngine.AdaptivePerformance.PerformanceBottleneckChangeEventArgs bottleneckEventArgs = default(global::UnityEngine.AdaptivePerformance.PerformanceBottleneckChangeEventArgs);
			global::UnityEngine.AdaptivePerformance.PerformanceBoostChangeEventArgs boostEventArgs = default(global::UnityEngine.AdaptivePerformance.PerformanceBoostChangeEventArgs);
			if (m_OverallFrameTime.GetNumValues() == m_OverallFrameTime.GetSampleWindowSize() && m_GpuFrameTime.GetNumValues() == m_GpuFrameTime.GetSampleWindowSize() && m_CpuFrameTime.GetNumValues() == m_CpuFrameTime.GetSampleWindowSize())
			{
				global::UnityEngine.AdaptivePerformance.PerformanceBottleneck performanceBottleneck = global::UnityEngine.AdaptivePerformance.BottleneckUtil.DetermineBottleneck(m_PerformanceMetrics.PerformanceBottleneck, m_FrameTiming.AverageCpuFrameTime, m_FrameTiming.AverageGpuFrameTime, m_FrameTiming.AverageFrameTime, targetFrameTime);
				if (performanceBottleneck != m_PerformanceMetrics.PerformanceBottleneck)
				{
					m_PerformanceMetrics.PerformanceBottleneck = performanceBottleneck;
					bottleneckEventArgs.PerformanceBottleneck = performanceBottleneck;
					flag = this.PerformanceBottleneckChangeEvent != null;
				}
			}
			flag2 = this.ThermalEvent != null && (performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.WarningLevel) || performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.TemperatureLevel) || performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.TemperatureTrend));
			flag4 = this.PerformanceModeEvent != null && performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.PerformanceMode);
			if (performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.CpuPerformanceLevel))
			{
				m_DevicePerfControl.CurrentCpuLevel = performanceDataRecord.CpuPerformanceLevel;
			}
			if (performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.GpuPerformanceLevel))
			{
				m_DevicePerfControl.CurrentGpuLevel = performanceDataRecord.GpuPerformanceLevel;
			}
			if (performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.PerformanceLevelControl) || m_AutomaticPerformanceControlChanged)
			{
				m_AutomaticPerformanceControlChanged = false;
				if (performanceDataRecord.PerformanceLevelControlAvailable)
				{
					if (AutomaticPerformanceControl)
					{
						m_DevicePerfControl.PerformanceControlMode = global::UnityEngine.AdaptivePerformance.PerformanceControlMode.Automatic;
					}
					else
					{
						m_DevicePerfControl.PerformanceControlMode = global::UnityEngine.AdaptivePerformance.PerformanceControlMode.Manual;
					}
				}
				else
				{
					m_DevicePerfControl.PerformanceControlMode = global::UnityEngine.AdaptivePerformance.PerformanceControlMode.System;
				}
			}
			m_AutoPerformanceLevelController.TargetFrameTime = targetFrameTime;
			m_AutoPerformanceLevelController.Enabled = m_DevicePerfControl.PerformanceControlMode == global::UnityEngine.AdaptivePerformance.PerformanceControlMode.Automatic;
			global::UnityEngine.AdaptivePerformance.PerformanceLevelChangeEventArgs changeArgs = default(global::UnityEngine.AdaptivePerformance.PerformanceLevelChangeEventArgs);
			if (m_DevicePerfControl.PerformanceControlMode != global::UnityEngine.AdaptivePerformance.PerformanceControlMode.System)
			{
				if (m_AutoPerformanceLevelController.Enabled)
				{
					if (m_NewUserPerformanceLevelRequest)
					{
						m_AutoPerformanceLevelController.Override(m_RequestedCpuLevel, m_RequestedGpuLevel);
						changeArgs.ManualOverride = true;
					}
					m_AutoPerformanceLevelController.Update();
				}
				else if (m_NewUserPerformanceLevelRequest)
				{
					m_DevicePerfControl.CpuLevel = m_RequestedCpuLevel;
					m_DevicePerfControl.GpuLevel = m_RequestedGpuLevel;
				}
			}
			flag3 = this.PerformanceBoostChangeEvent != null && (performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.CpuPerformanceBoost) || performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.GpuPerformanceBoost));
			if (performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.CpuPerformanceBoost) && m_DevicePerfControl.CpuPerformanceBoost != performanceDataRecord.CpuPerformanceBoost)
			{
				m_DevicePerfControl.CpuPerformanceBoost = performanceDataRecord.CpuPerformanceBoost;
				m_RequestedCpuBoost = performanceDataRecord.CpuPerformanceBoost;
			}
			if (performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.GpuPerformanceBoost) && m_DevicePerfControl.GpuPerformanceBoost != performanceDataRecord.GpuPerformanceBoost)
			{
				m_DevicePerfControl.GpuPerformanceBoost = performanceDataRecord.GpuPerformanceBoost;
				m_RequestedGpuBoost = performanceDataRecord.GpuPerformanceBoost;
			}
			if (m_NewUserCpuPerformanceBoostRequest && this.PerformanceBoostChangeEvent != null)
			{
				m_NewUserCpuPerformanceBoostRequest = false;
				m_Subsystem.PerformanceLevelControl.EnableCpuBoost();
			}
			if (m_NewUserGpuPerformanceBoostRequest && this.PerformanceBoostChangeEvent != null)
			{
				m_NewUserGpuPerformanceBoostRequest = false;
				m_Subsystem.PerformanceLevelControl.EnableGpuBoost();
			}
			if (m_DevicePerfControl.Update(out changeArgs) && this.PerformanceLevelChangeEvent != null)
			{
				this.PerformanceLevelChangeEvent(changeArgs);
			}
			m_PerformanceMetrics.CurrentCpuLevel = m_DevicePerfControl.CurrentCpuLevel;
			m_PerformanceMetrics.CurrentGpuLevel = m_DevicePerfControl.CurrentGpuLevel;
			m_PerformanceMetrics.CpuPerformanceBoost = m_DevicePerfControl.CpuPerformanceBoost;
			m_PerformanceMetrics.GpuPerformanceBoost = m_DevicePerfControl.GpuPerformanceBoost;
			m_NewUserPerformanceLevelRequest = false;
			if (performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.ClusterInfo))
			{
				m_PerformanceMetrics.ClusterInfo = performanceDataRecord.ClusterInfo;
			}
			if (performanceDataRecord.ChangeFlags.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.PerformanceMode))
			{
				m_PerformanceMode = performanceDataRecord.PerformanceMode;
			}
			if (flag2)
			{
				this.ThermalEvent(m_ThermalMetrics);
			}
			if (flag)
			{
				this.PerformanceBottleneckChangeEvent(bottleneckEventArgs);
			}
			if (flag3)
			{
				boostEventArgs.CpuBoost = m_DevicePerfControl.CpuPerformanceBoost;
				boostEventArgs.GpuBoost = m_DevicePerfControl.GpuPerformanceBoost;
				this.PerformanceBoostChangeEvent(boostEventArgs);
			}
			if (flag4)
			{
				this.PerformanceModeEvent(m_PerformanceMode);
			}
		}

		private static bool WillCurrentFrameRender()
		{
			return global::UnityEngine.Rendering.OnDemandRendering.willCurrentFrameRender;
		}

		public static float EffectiveTargetFrameRate()
		{
			return global::UnityEngine.Rendering.OnDemandRendering.effectiveRenderFrameRate;
		}

		public void OnDestroy()
		{
			DeinitializeAdaptivePerformance();
		}

		public void InitializeAdaptivePerformance()
		{
			if (Active || Initialized)
			{
				return;
			}
			global::UnityEngine.AdaptivePerformance.APLog.enabled = true;
			if (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance == null)
			{
				return;
			}
			if (!global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.IsProviderInitialized)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.InitAdaptivePerformance();
			}
			if (!global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.IsProviderInitialized)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("Initialization of Provider was not successful. Are there errors present? Make sure to select your loader in the Adaptive Performance Settings for this platform.");
				return;
			}
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader adaptivePerformanceLoader = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.Manager.ActiveLoaderAs<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader>();
			if (adaptivePerformanceLoader == null)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("No Active Loader was found. Make sure to select your loader in the Adaptive Performance Settings for this platform.");
				return;
			}
			m_Settings = adaptivePerformanceLoader.GetSettings();
			if (m_Settings == null)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("No Settings available. Did the Post Process Buildstep fail?");
				return;
			}
			string[] availableScalerProfiles = m_Settings.GetAvailableScalerProfiles();
			if (availableScalerProfiles.Length == 0)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("No Scaler Profiles available. Did you remove all profiles manually from the provider Settings?");
				return;
			}
			m_Settings.LoadScalerProfile(availableScalerProfiles[m_Settings.defaultScalerProfilerIndex]);
			AutomaticPerformanceControl = m_Settings.automaticPerformanceMode;
			LoggingFrequencyInFrames = m_Settings.statsLoggingFrequencyInFrames;
			global::UnityEngine.AdaptivePerformance.APLog.enabled = m_Settings.logging;
			if (m_Subsystem == null)
			{
				global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem adaptivePerformanceSubsystem = (global::UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystem)adaptivePerformanceLoader.GetDefaultSubsystem();
				if (adaptivePerformanceSubsystem != null)
				{
					if (!adaptivePerformanceSubsystem.Initialized)
					{
						adaptivePerformanceSubsystem.Destroy();
						global::UnityEngine.AdaptivePerformance.APLog.Debug("Subsystem not initialized.");
						return;
					}
					m_Subsystem = adaptivePerformanceSubsystem;
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Subsystem version={0}", m_Subsystem.Version);
				}
			}
			if (m_Subsystem != null)
			{
				m_UseProviderOverallFrameTime = m_Subsystem.Capabilities.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.OverallFrameTime);
				m_DevicePerfControl = new global::UnityEngine.AdaptivePerformance.DevicePerformanceControlImpl(m_Subsystem.PerformanceLevelControl);
				m_AutoPerformanceLevelController = new global::UnityEngine.AdaptivePerformance.AutoPerformanceLevelController(m_DevicePerfControl, PerformanceStatus, ThermalStatus);
				if (m_Settings.automaticGameMode)
				{
					m_AutoPerformanceModeController = new global::UnityEngine.AdaptivePerformance.AutoPerformanceModeController(PerformanceModeStatus);
				}
				m_AppLifecycle = m_Subsystem.ApplicationLifecycle;
				if (!m_Subsystem.Capabilities.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.CpuFrameTime))
				{
					m_CpuFrameTimeProvider = new global::UnityEngine.AdaptivePerformance.CpuTimeProvider();
				}
				if (!m_Subsystem.Capabilities.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.GpuFrameTime))
				{
					m_GpuFrameTimeProvider = new global::UnityEngine.AdaptivePerformance.GpuTimeProvider();
				}
				m_TemperatureTrend = new global::UnityEngine.AdaptivePerformance.TemperatureTrend(m_Subsystem.Capabilities.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.TemperatureTrend));
				if (m_RequestedCpuLevel == -1)
				{
					m_RequestedCpuLevel = m_DevicePerfControl.MaxCpuPerformanceLevel;
				}
				if (m_RequestedGpuLevel == -1)
				{
					m_RequestedGpuLevel = m_DevicePerfControl.MaxGpuPerformanceLevel;
				}
				m_NewUserPerformanceLevelRequest = true;
				if (m_Subsystem.PerformanceLevelControl == null)
				{
					m_DevicePerfControl.PerformanceControlMode = global::UnityEngine.AdaptivePerformance.PerformanceControlMode.System;
				}
				else if (AutomaticPerformanceControl)
				{
					m_DevicePerfControl.PerformanceControlMode = global::UnityEngine.AdaptivePerformance.PerformanceControlMode.Automatic;
				}
				else
				{
					m_DevicePerfControl.PerformanceControlMode = global::UnityEngine.AdaptivePerformance.PerformanceControlMode.Manual;
				}
				ThermalEvent += LogThermalEvent;
				PerformanceBottleneckChangeEvent += LogBottleneckEvent;
				PerformanceLevelChangeEvent += LogPerformanceLevelEvent;
				PerformanceModeEvent += LogPerformanceModeEvent;
				if (m_Subsystem.Capabilities.HasFlag(global::UnityEngine.AdaptivePerformance.Provider.Feature.CpuPerformanceBoost))
				{
					PerformanceBoostChangeEvent += LogBoostEvent;
				}
				Indexer = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceIndexer(ref m_Settings, new global::UnityEngine.AdaptivePerformance.PerformanceStateTracker(120));
				UpdateSubsystem();
			}
		}

		public void StartAdaptivePerformance()
		{
			if (Initialized)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.StartAdaptivePerformance();
			}
		}

		public void StopAdaptivePerformance()
		{
			if (Active)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.StopAdaptivePerformance();
			}
		}

		public void DeinitializeAdaptivePerformance()
		{
			if (Initialized)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings.Instance.DeInitAdaptivePerformance();
				if (Indexer != null)
				{
					Indexer.UnapplyAllScalers();
				}
				ThermalEvent -= LogThermalEvent;
				PerformanceBottleneckChangeEvent -= LogBottleneckEvent;
				PerformanceLevelChangeEvent -= LogPerformanceLevelEvent;
				PerformanceBoostChangeEvent -= LogBoostEvent;
				PerformanceModeEvent -= LogPerformanceModeEvent;
				global::UnityEngine.AdaptivePerformance.APLog.enabled = false;
				m_Settings = null;
				m_Subsystem = null;
				m_DevicePerfControl = null;
				m_AutoPerformanceLevelController = null;
				m_AutoPerformanceModeController = null;
				m_AppLifecycle = null;
				m_CpuFrameTimeProvider = null;
				m_GpuFrameTimeProvider = null;
				m_TemperatureTrend = null;
				Indexer = null;
			}
		}

		public void OnApplicationPause(bool pause)
		{
			if (m_Subsystem == null)
			{
				return;
			}
			if (pause)
			{
				if (m_AppLifecycle != null)
				{
					m_AppLifecycle.ApplicationPause();
				}
				m_OverallFrameTime.Reset();
				m_GpuFrameTime.Reset();
				m_CpuFrameTime.Reset();
			}
			else
			{
				m_ThermalMetrics.WarningLevel = global::UnityEngine.AdaptivePerformance.WarningLevel.NoWarning;
				if (m_AppLifecycle != null)
				{
					m_AppLifecycle.ApplicationResume();
				}
				m_JustResumed = true;
			}
		}
	}
}
