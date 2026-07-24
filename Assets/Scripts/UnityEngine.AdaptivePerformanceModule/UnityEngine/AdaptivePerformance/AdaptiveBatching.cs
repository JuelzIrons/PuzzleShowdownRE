namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveBatching : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private bool m_DefaultState;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveBatching);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipDynamicBatching = m_DefaultState;
		}

		protected override void OnEnabled()
		{
			m_DefaultState = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipDynamicBatching;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipDynamicBatching = Scale < 1f;
			}
		}
	}
}
