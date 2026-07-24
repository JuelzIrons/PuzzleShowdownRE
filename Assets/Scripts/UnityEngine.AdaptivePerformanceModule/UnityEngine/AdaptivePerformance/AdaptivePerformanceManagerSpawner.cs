namespace UnityEngine.AdaptivePerformance
{
	internal class AdaptivePerformanceManagerSpawner : global::UnityEngine.ScriptableObject
	{
		public const string AdaptivePerformanceManagerObjectName = "AdaptivePerformanceManager";

		private global::UnityEngine.GameObject m_ManagerGameObject;

		public global::UnityEngine.GameObject ManagerGameObject => m_ManagerGameObject;

		private void OnEnable()
		{
			if (!(m_ManagerGameObject != null))
			{
				m_ManagerGameObject = global::UnityEngine.GameObject.Find("AdaptivePerformanceManager");
			}
		}

		public void Initialize(bool isCheckingProvider)
		{
			if (m_ManagerGameObject != null)
			{
				return;
			}
			m_ManagerGameObject = new global::UnityEngine.GameObject("AdaptivePerformanceManager");
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceManager adaptivePerformanceManager = m_ManagerGameObject.AddComponent<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceManager>();
			if (isCheckingProvider && adaptivePerformanceManager.Indexer == null)
			{
				Deinitialize();
				return;
			}
			global::UnityEngine.AdaptivePerformance.Holder.Instance = adaptivePerformanceManager;
			global::UnityEngine.Object.DontDestroyOnLoad(m_ManagerGameObject);
			global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings settings = adaptivePerformanceManager.Settings;
			if (!(settings == null))
			{
				string[] availableScalerProfiles = settings.GetAvailableScalerProfiles();
				if (availableScalerProfiles.Length == 0)
				{
					global::UnityEngine.AdaptivePerformance.APLog.Debug("No Scaler Profiles available. Did you remove all profiles manually from the provider Settings?");
					return;
				}
				settings.LoadScalerProfile(availableScalerProfiles[settings.defaultScalerProfilerIndex]);
				InstallScalers(settings.ScalerProfiles[settings.defaultScalerProfilerIndex], settings);
			}
		}

		public void Deinitialize()
		{
			if (!(m_ManagerGameObject == null))
			{
				global::UnityEngine.Object.DestroyImmediate(m_ManagerGameObject);
				m_ManagerGameObject = null;
			}
		}

		private void InstallScalers(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile profile, global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings settings)
		{
			foreach (global::System.Type k_DefaultScalerName in global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettings.k_DefaultScalerNames)
			{
				global::UnityEngine.ScriptableObject.CreateInstance(k_DefaultScalerName);
			}
			if (profile.AddedScalers != null && profile.AddedScalers.Count > 0)
			{
				profile.EnableAddedScalers();
			}
			else if (settings.AddedScalerViaScan != null && settings.AddedScalerViaScan.Count > 0)
			{
				for (int i = 0; i < settings.AddedScalerViaScan.Count; i++)
				{
					settings.AddedScalerViaScan[i].InitializeScaler();
				}
			}
		}
	}
}
