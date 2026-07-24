namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveShadowCascade : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private int m_DefaultCascadeCount;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveShadowCascade);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MainLightShadowCascadesCountBias = m_DefaultCascadeCount;
		}

		protected override void OnEnabled()
		{
			m_DefaultCascadeCount = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MainLightShadowCascadesCountBias;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MainLightShadowCascadesCountBias = (int)(2f * Scale);
			}
		}
	}
}
