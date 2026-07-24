namespace UnityEngine.AdaptivePerformance
{
	public class AdaptivePhysics : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private float m_fixedDeltaTimeDefault;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptivePhysics);
			}
		}

		protected override void OnDisabled()
		{
			global::UnityEngine.Time.fixedDeltaTime = m_fixedDeltaTimeDefault;
		}

		protected override void OnEnabled()
		{
			m_fixedDeltaTimeDefault = global::UnityEngine.Time.fixedDeltaTime;
		}

		protected override void OnLevel()
		{
			if (ScaleChanged())
			{
				global::UnityEngine.Time.fixedDeltaTime = m_fixedDeltaTimeDefault / Scale;
			}
		}
	}
}
