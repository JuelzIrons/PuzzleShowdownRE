namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveTransparency : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveTransparency);
			}
		}

		protected override void OnDisabled()
		{
			OnDestroy();
		}

		private void OnDestroy()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipTransparentObjects = false;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipTransparentObjects = Scale < 1f;
			}
		}
	}
}
