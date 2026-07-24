namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveShadowDistance : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private float m_DefaultShadowDistance;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveShadowDistance);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MaxShadowDistanceMultiplier = m_DefaultShadowDistance;
		}

		protected override void OnEnabled()
		{
			m_DefaultShadowDistance = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MaxShadowDistanceMultiplier;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MaxShadowDistanceMultiplier = 1f * Scale;
			}
		}
	}
}
