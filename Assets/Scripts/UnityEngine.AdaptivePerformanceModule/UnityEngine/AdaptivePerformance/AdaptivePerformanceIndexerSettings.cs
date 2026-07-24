namespace UnityEngine.AdaptivePerformance
{
	[global::System.Serializable]
	public class AdaptivePerformanceIndexerSettings
	{
		private const string m_FeatureName = "Indexer";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Active")]
		private bool m_Active = true;

		[global::UnityEngine.Tooltip("Thermal Action Delay")]
		[global::UnityEngine.SerializeField]
		private float m_ThermalActionDelay = 10f;

		[global::UnityEngine.Tooltip("Performance Action Delay")]
		[global::UnityEngine.SerializeField]
		private float m_PerformanceActionDelay = 4f;

		public bool active
		{
			get
			{
				return m_Active;
			}
			set
			{
				if (m_Active != value)
				{
					m_Active = value;
				}
			}
		}

		public float thermalActionDelay
		{
			get
			{
				return m_ThermalActionDelay;
			}
			set
			{
				m_ThermalActionDelay = value;
			}
		}

		public float performanceActionDelay
		{
			get
			{
				return m_PerformanceActionDelay;
			}
			set
			{
				m_PerformanceActionDelay = value;
			}
		}
	}
}
