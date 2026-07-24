namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	internal struct ProbeVolumeBakingProcessSettings
	{
		internal enum SettingsVersion
		{
			Initial = 0,
			ThreadedVirtualOffset = 1,
			Max = 2,
			Current = 1
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion m_Version;

		public global::UnityEngine.Rendering.ProbeDilationSettings dilationSettings;

		public global::UnityEngine.Rendering.VirtualOffsetSettings virtualOffsetSettings;

		internal static global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings Default
		{
			get
			{
				global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings result = default(global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings);
				result.SetDefaults();
				return result;
			}
		}

		internal ProbeVolumeBakingProcessSettings(global::UnityEngine.Rendering.ProbeDilationSettings dilationSettings, global::UnityEngine.Rendering.VirtualOffsetSettings virtualOffsetSettings)
		{
			m_Version = global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset;
			this.dilationSettings = dilationSettings;
			this.virtualOffsetSettings = virtualOffsetSettings;
		}

		internal void SetDefaults()
		{
			m_Version = global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset;
			dilationSettings.SetDefaults();
			virtualOffsetSettings.SetDefaults();
		}

		internal void Upgrade()
		{
			if (m_Version != global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset)
			{
				dilationSettings.UpgradeFromTo(m_Version, global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset);
				virtualOffsetSettings.UpgradeFromTo(m_Version, global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset);
				m_Version = global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset;
			}
		}
	}
}
