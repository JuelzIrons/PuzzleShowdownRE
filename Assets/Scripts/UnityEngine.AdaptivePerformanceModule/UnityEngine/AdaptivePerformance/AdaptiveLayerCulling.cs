namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveLayerCulling : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private float[] m_defaultDistances = new float[32];

		private float[] m_scaledDistances = new float[32];

		private bool init = false;

		private global::UnityEngine.Camera m_cachedCamera;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveLayerCulling);
			}
		}

		protected override void OnDisabled()
		{
			init = false;
			if ((bool)global::UnityEngine.Camera.main && m_defaultDistances != null)
			{
				global::UnityEngine.Camera.main.layerCullDistances = m_defaultDistances;
			}
		}

		protected override void OnEnabled()
		{
			AsignDefaultValues();
		}

		protected override void OnLevel()
		{
			if (!global::UnityEngine.Camera.main)
			{
				return;
			}
			AsignDefaultValues();
			if (!ScaleChanged())
			{
				return;
			}
			for (int num = 31; num >= 0; num--)
			{
				if (m_defaultDistances[num] != 0f)
				{
					m_scaledDistances[num] = m_defaultDistances[num] * Scale;
				}
			}
			global::UnityEngine.Camera.main.layerCullDistances = m_scaledDistances;
		}

		private void AsignDefaultValues()
		{
			if (m_cachedCamera == null || m_cachedCamera != global::UnityEngine.Camera.main)
			{
				m_cachedCamera = global::UnityEngine.Camera.main;
				init = false;
			}
			if (!init && (bool)global::UnityEngine.Camera.main)
			{
				m_defaultDistances = global::UnityEngine.Camera.main.layerCullDistances;
				init = true;
			}
		}
	}
}
