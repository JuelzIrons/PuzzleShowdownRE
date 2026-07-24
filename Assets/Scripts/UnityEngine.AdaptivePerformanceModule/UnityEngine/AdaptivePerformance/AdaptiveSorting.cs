namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveSorting : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private bool m_DefaultSorting;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveSorting);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipFrontToBackSorting = m_DefaultSorting;
		}

		protected override void OnEnabled()
		{
			m_DefaultSorting = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipFrontToBackSorting;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipFrontToBackSorting = Scale < 1f;
			}
		}
	}
}
