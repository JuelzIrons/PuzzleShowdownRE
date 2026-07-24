namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveShadowmapResolution : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private float m_DefaultShadowmapResolution;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveShadowmapResolution);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MainLightShadowmapResolutionMultiplier = m_DefaultShadowmapResolution;
		}

		protected override void OnEnabled()
		{
			m_DefaultShadowmapResolution = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MainLightShadowmapResolutionMultiplier;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MainLightShadowmapResolutionMultiplier = 1f * Scale;
			}
		}
	}
}
