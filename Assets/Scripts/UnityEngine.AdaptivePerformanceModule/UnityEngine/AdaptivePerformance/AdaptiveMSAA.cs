namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveMSAA : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private int m_DefaultAA;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveMSAA);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.AntiAliasingQualityBias = m_DefaultAA;
		}

		protected override void OnEnabled()
		{
			m_DefaultAA = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.AntiAliasingQualityBias;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.AntiAliasingQualityBias = (int)(2f * Scale);
			}
		}
	}
}
