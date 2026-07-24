namespace UnityEngine.AdaptivePerformance
{
	public class IAdaptivePerformanceSettings : global::UnityEngine.ScriptableObject
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Enable Logging in Devmode")]
		private bool m_Logging = true;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Automatic Performance Mode")]
		private bool m_AutomaticPerformanceModeEnabled = true;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Automatic Game Mode")]
		private bool m_AutomaticGameModeEnabled = false;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Enables the CPU and GPU boost mode before engine startup to decrease startup time.")]
		private bool m_EnableBoostOnStartup = true;

		[global::UnityEngine.Tooltip("Logging Frequency (Development mode only)")]
		[global::UnityEngine.SerializeField]
		private int m_StatsLoggingFrequencyInFrames = 50;

		[global::UnityEngine.Tooltip("Indexer Settings")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceIndexerSettings m_IndexerSettings;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Scaler Settings")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettings m_ScalerSettings;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> m_AddedScalerViaScan = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler>();

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile m_ActiveScalerProfile;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile[] m_scalerProfileList = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile[1]
		{
			new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile()
		};

		[global::UnityEngine.SerializeField]
		internal int m_DefaultScalerProfilerIndex = 0;

		[global::UnityEngine.SerializeField]
		private int k_AssetVersion = 3;

		public bool logging
		{
			get
			{
				return m_Logging;
			}
			set
			{
				m_Logging = value;
			}
		}

		public bool automaticPerformanceMode
		{
			get
			{
				return m_AutomaticPerformanceModeEnabled;
			}
			set
			{
				m_AutomaticPerformanceModeEnabled = value;
			}
		}

		public bool automaticGameMode
		{
			get
			{
				return m_AutomaticGameModeEnabled;
			}
			set
			{
				m_AutomaticGameModeEnabled = value;
			}
		}

		public bool enableBoostOnStartup
		{
			get
			{
				return m_EnableBoostOnStartup;
			}
			set
			{
				m_EnableBoostOnStartup = value;
			}
		}

		public int statsLoggingFrequencyInFrames
		{
			get
			{
				return m_StatsLoggingFrequencyInFrames;
			}
			set
			{
				m_StatsLoggingFrequencyInFrames = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceIndexerSettings indexerSettings
		{
			get
			{
				return m_IndexerSettings;
			}
			set
			{
				m_IndexerSettings = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettings scalerSettings
		{
			get
			{
				return m_ScalerSettings;
			}
			set
			{
				m_ScalerSettings = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile[] ScalerProfiles => m_scalerProfileList;

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile ActiveScalerProfile
		{
			get
			{
				if (m_ActiveScalerProfile == null && m_scalerProfileList.Length != 0)
				{
					return m_scalerProfileList[0];
				}
				return m_ActiveScalerProfile;
			}
			set
			{
				m_ActiveScalerProfile = value;
			}
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.AdaptivePerformanceModule" })]
		internal global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> AddedScalerViaScan
		{
			get
			{
				return m_AddedScalerViaScan;
			}
			set
			{
				m_AddedScalerViaScan = value;
			}
		}

		public int defaultScalerProfilerIndex
		{
			get
			{
				return m_DefaultScalerProfilerIndex;
			}
			set
			{
				m_DefaultScalerProfilerIndex = value;
			}
		}

		public void AddScalerProfileWithDefaultScalers(string name = "")
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile[] scalerProfiles = ScalerProfiles;
			foreach (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile adaptivePerformanceScalerProfile in scalerProfiles)
			{
				if (adaptivePerformanceScalerProfile.Name == name)
				{
					global::UnityEngine.Debug.LogWarning(adaptivePerformanceScalerProfile.Name + " already exists in the profile list");
					return;
				}
			}
			global::System.Array.Resize(ref m_scalerProfileList, m_scalerProfileList.Length + 1);
			m_scalerProfileList[^1] = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile();
			if (!string.IsNullOrEmpty(name))
			{
				m_scalerProfileList[^1].Name = name;
			}
		}

		public void DeleteScalerProfileAt(int index)
		{
			if (index >= ScalerProfiles.Length || index < 0)
			{
				return;
			}
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile[] array = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile[ScalerProfiles.Length - 1];
			int num = 0;
			for (int i = 0; i < ScalerProfiles.Length; i++)
			{
				if (i != index)
				{
					array[num] = ScalerProfiles[i];
					num++;
				}
			}
			m_scalerProfileList = array;
		}

		public void LoadScalerProfile(string scalerProfileName)
		{
			if (scalerProfileName == null || scalerProfileName.Length <= 0)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("Scaler profile name empty. Can not load and apply profile.");
				return;
			}
			if (m_scalerProfileList.Length == 0)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("No scaler profiles available. Can not load and apply profile. Add more profiles in the Adaptive Performance settings.");
				return;
			}
			if (ActiveScalerProfile != null && ActiveScalerProfile.Name == scalerProfileName)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("The " + ActiveScalerProfile.Name + " scaler profile is already loaded.");
				return;
			}
			if (m_scalerProfileList.Length == 1)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("Only default scaler profile available. Reset all scalers to default profile.");
			}
			for (int i = 0; i < m_scalerProfileList.Length; i++)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile adaptivePerformanceScalerProfile = m_scalerProfileList[i];
				if (adaptivePerformanceScalerProfile == null)
				{
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Scaler profile is null. Can not load and apply profile. Check Adaptive Performance settings.");
					return;
				}
				if (adaptivePerformanceScalerProfile.Name == null || adaptivePerformanceScalerProfile.Name.Length <= 0)
				{
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Scaler profile name is null or empty. Can not load and apply profile. Check Adaptive Performance settings.");
					return;
				}
				if (!(adaptivePerformanceScalerProfile.Name == scalerProfileName))
				{
					continue;
				}
				if (ActiveScalerProfile != null)
				{
					ActiveScalerProfile.RemoveAllAddedScalersFromIndexer();
				}
				scalerSettings.ApplySettings(adaptivePerformanceScalerProfile);
				if (adaptivePerformanceScalerProfile.AddedScalers != null && adaptivePerformanceScalerProfile.AddedScalers.Count > 0)
				{
					adaptivePerformanceScalerProfile.EnableAddedScalers();
					for (int j = 0; j < AddedScalerViaScan.Count; j++)
					{
						AddedScalerViaScan[j].RemoveScaler();
					}
				}
				ActiveScalerProfile = adaptivePerformanceScalerProfile;
				break;
			}
			if (ApplyScalerProfileToAllScalers())
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("Scaler profile " + scalerProfileName + " loaded.");
			}
		}

		private bool ApplyScalerProfileToAllScalers()
		{
			bool result = false;
			if (global::UnityEngine.AdaptivePerformance.Holder.Instance == null || global::UnityEngine.AdaptivePerformance.Holder.Instance.Indexer == null)
			{
				return result;
			}
			global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> list = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler>();
			global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> scalers = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler>();
			global::UnityEngine.AdaptivePerformance.Holder.Instance.Indexer.GetUnappliedScalers(ref scalers);
			list.AddRange(scalers);
			global::UnityEngine.AdaptivePerformance.Holder.Instance.Indexer.GetAppliedScalers(ref scalers);
			list.AddRange(scalers);
			global::UnityEngine.AdaptivePerformance.Holder.Instance.Indexer.GetDisabledScalers(ref scalers);
			list.AddRange(scalers);
			if (list.Count <= 0)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("No scalers found. No scaler profile applied.");
				return result;
			}
			global::System.Reflection.PropertyInfo[] properties = typeof(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettings).GetProperties();
			global::System.Reflection.PropertyInfo[] array = properties;
			foreach (global::System.Reflection.PropertyInfo property in array)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler adaptivePerformanceScaler = list.Find((global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler s) => s.GetType().ToString().Contains(property.Name));
				if ((bool)adaptivePerformanceScaler)
				{
					global::System.Reflection.PropertyInfo property2 = typeof(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettings).GetProperty(property.Name);
					object value = property2.GetValue(scalerSettings);
					adaptivePerformanceScaler.Deactivate();
					adaptivePerformanceScaler.ApplyDefaultSetting((global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase)value);
					adaptivePerformanceScaler.Activate();
					result = true;
				}
			}
			return result;
		}

		public string[] GetAvailableScalerProfiles()
		{
			string[] array = new string[m_scalerProfileList.Length];
			if (m_scalerProfileList.Length == 0)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("No scaler profiles available. You can not load and apply profiles. Add more profiles in the Adaptive Performance settings.");
				return array;
			}
			for (int i = 0; i < m_scalerProfileList.Length; i++)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerProfile adaptivePerformanceScalerProfile = m_scalerProfileList[i];
				array[i] = adaptivePerformanceScalerProfile.Name;
			}
			return array;
		}

		public void OnEnable()
		{
			if (k_AssetVersion < 3)
			{
				k_AssetVersion = 2;
			}
		}
	}
}
