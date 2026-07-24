namespace UnityEngine.AdaptivePerformance
{
	public class AdaptivePerformanceGeneralSettings : global::UnityEngine.ScriptableObject
	{
		public static string k_SettingsKey = "com.unity.adaptiveperformance.loader_settings";

		internal static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings s_RuntimeSettingsInstance = null;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.AdaptivePerformance.AdaptivePerformanceManagerSettings m_LoaderManagerInstance = null;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Enable this to automatically start up Adaptive Performance at runtime.")]
		internal bool m_InitManagerOnStart = true;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.AdaptivePerformanceModule" })]
		internal string m_LastSelectedProvider = "";

		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceManagerSettings m_AdaptivePerformanceManager = null;

		private bool m_ProviderIntialized = false;

		private bool m_ProviderStarted = false;

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceManagerSettings Manager
		{
			get
			{
				return m_LoaderManagerInstance;
			}
			set
			{
				m_LoaderManagerInstance = value;
			}
		}

		public bool IsProviderInitialized => m_ProviderIntialized;

		public bool IsProviderStarted => m_ProviderStarted;

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings Instance
		{
			get
			{
				return s_RuntimeSettingsInstance;
			}
			set
			{
				s_RuntimeSettingsInstance = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceManagerSettings AssignedSettings
		{
			get
			{
				return m_LoaderManagerInstance;
			}
			set
			{
				m_LoaderManagerInstance = value;
			}
		}

		public bool InitManagerOnStart
		{
			get
			{
				return m_InitManagerOnStart;
			}
			set
			{
				m_InitManagerOnStart = value;
			}
		}

		private void Awake()
		{
			s_RuntimeSettingsInstance = this;
			global::UnityEngine.Application.quitting += Quit;
			global::UnityEngine.Object.DontDestroyOnLoad(s_RuntimeSettingsInstance);
		}

		private static void Quit()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings instance = Instance;
			if (!(instance == null))
			{
				instance.DeInitAdaptivePerformance();
			}
		}

		private void OnDestroy()
		{
			DeInitAdaptivePerformance();
			s_RuntimeSettingsInstance = null;
		}

		[global::UnityEngine.Scripting.RequiredByNativeCode(true)]
		internal static void AttemptInitializeAdaptivePerformanceGeneralSettingsOnLoad()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings instance = Instance;
			if (!(instance == null) && instance.InitManagerOnStart)
			{
				instance.InitAdaptivePerformance();
			}
		}

		[global::UnityEngine.Scripting.RequiredByNativeCode(true)]
		internal static void AttemptStartAdaptivePerformanceGeneralSettingsOnBeforeSplashScreen()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceGeneralSettings instance = Instance;
			if (!(instance == null) && instance.InitManagerOnStart)
			{
				instance.StartAdaptivePerformance();
			}
		}

		internal void InitAdaptivePerformance()
		{
			if (m_ProviderIntialized || Instance == null)
			{
				return;
			}
			m_AdaptivePerformanceManager = Instance.m_LoaderManagerInstance;
			if (m_AdaptivePerformanceManager == null)
			{
				global::UnityEngine.Debug.LogError("Assigned GameObject for Adaptive Performance Management loading is invalid. No Adaptive Performance Providers will be automatically loaded.");
				return;
			}
			m_AdaptivePerformanceManager.automaticLoading = false;
			m_AdaptivePerformanceManager.automaticRunning = false;
			m_AdaptivePerformanceManager.InitializeLoaderSync();
			if (!(m_AdaptivePerformanceManager.activeLoader == null))
			{
				m_ProviderIntialized = true;
			}
		}

		internal void StartAdaptivePerformance()
		{
			if (m_ProviderIntialized && !m_ProviderStarted && !(m_AdaptivePerformanceManager == null) && !(m_AdaptivePerformanceManager.activeLoader == null))
			{
				m_AdaptivePerformanceManager.StartSubsystems();
				m_ProviderStarted = true;
			}
		}

		internal void StopAdaptivePerformance()
		{
			if (m_ProviderIntialized && m_ProviderStarted && !(m_AdaptivePerformanceManager == null) && !(m_AdaptivePerformanceManager.activeLoader == null))
			{
				m_AdaptivePerformanceManager.StopSubsystems();
				m_ProviderStarted = false;
			}
		}

		internal void DeInitAdaptivePerformance()
		{
			if (m_ProviderIntialized)
			{
				if (m_ProviderStarted)
				{
					StopAdaptivePerformance();
				}
				if (m_AdaptivePerformanceManager != null)
				{
					m_AdaptivePerformanceManager.DeinitializeLoader();
					m_AdaptivePerformanceManager = null;
				}
				m_ProviderIntialized = false;
			}
		}
	}
}
