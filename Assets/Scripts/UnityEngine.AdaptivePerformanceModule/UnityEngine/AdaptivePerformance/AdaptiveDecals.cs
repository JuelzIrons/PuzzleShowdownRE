namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveDecals : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private float m_DefaultDecalsDistance;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveDecals);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.DecalsDrawDistance = m_DefaultDecalsDistance;
		}

		protected override void OnEnabled()
		{
			m_DefaultDecalsDistance = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.DecalsDrawDistance;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.DecalsDrawDistance = (int)(m_DefaultDecalsDistance * Scale);
			}
		}
	}
}
