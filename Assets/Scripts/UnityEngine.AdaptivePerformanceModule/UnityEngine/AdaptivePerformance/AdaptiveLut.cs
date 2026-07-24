namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveLut : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private float m_DefaultLutBias;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveLut);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.LutBias = m_DefaultLutBias;
		}

		protected override void OnEnabled()
		{
			m_DefaultLutBias = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.LutBias;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.LutBias = Scale;
			}
		}
	}
}
