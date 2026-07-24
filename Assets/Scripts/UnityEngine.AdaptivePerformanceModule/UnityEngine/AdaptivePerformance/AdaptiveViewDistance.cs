namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveViewDistance : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private float m_DefaultFarClipPlane = -1f;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveViewDistance);
			}
		}

		protected override void OnDisabled()
		{
			if ((bool)global::UnityEngine.Camera.main && m_DefaultFarClipPlane != -1f)
			{
				global::UnityEngine.Camera.main.farClipPlane = m_DefaultFarClipPlane;
			}
		}

		protected override void OnEnabled()
		{
			if ((bool)global::UnityEngine.Camera.main)
			{
				m_DefaultFarClipPlane = global::UnityEngine.Camera.main.farClipPlane;
			}
		}

		protected override void OnLevel()
		{
			if ((bool)global::UnityEngine.Camera.main)
			{
				if (m_DefaultFarClipPlane == -1f)
				{
					m_DefaultFarClipPlane = global::UnityEngine.Camera.main.farClipPlane;
				}
				if (ScaleChanged())
				{
					global::UnityEngine.Camera.main.farClipPlane = Scale;
				}
			}
		}
	}
}
