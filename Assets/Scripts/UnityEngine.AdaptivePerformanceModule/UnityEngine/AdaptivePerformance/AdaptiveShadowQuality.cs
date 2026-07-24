namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveShadowQuality : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private int m_DefaultShadowQualityBias;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveShadowQuality);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.ShadowQualityBias = m_DefaultShadowQualityBias;
		}

		protected override void OnEnabled()
		{
			m_DefaultShadowQualityBias = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.ShadowQualityBias;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.ShadowQualityBias = (int)(3f - 3f * Scale);
			}
		}
	}
}
