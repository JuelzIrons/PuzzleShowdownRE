namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveLOD : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private float m_DefaultLodBias;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveLOD);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.QualitySettings.lodBias = m_DefaultLodBias;
		}

		protected override void OnEnabled()
		{
			m_DefaultLodBias = global::UnityEngine.QualitySettings.lodBias;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.QualitySettings.lodBias = m_DefaultLodBias * Scale;
			}
		}
	}
}
