namespace UnityEngine.AdaptivePerformance
{
	[global::System.Serializable]
	public class AdaptivePerformanceScalerSettingsBase
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Name of the scaler.")]
		private string m_Name = "Base Scaler";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Active")]
		private bool m_Enabled = false;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Scale to control the quality impact for the scaler. No quality change when 1, improved quality when >1, and lowered quality when <1.")]
		private float m_Scale = -1f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Visual impact the scaler has on the application. The higher the value, the more impact the scaler has on the visuals.")]
		private global::UnityEngine.AdaptivePerformance.ScalerVisualImpact m_VisualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Low;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Application bottleneck that the scaler targets. The target selected has the most impact on the quality control of this scaler.")]
		private global::UnityEngine.AdaptivePerformance.ScalerTarget m_Target = global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU;

		[global::UnityEngine.Tooltip("Maximum level for the scaler. This is tied to the implementation of the scaler to divide the levels into concrete steps.")]
		[global::UnityEngine.SerializeField]
		private int m_MaxLevel = 1;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Minimum value for the scale boundary.")]
		private float m_MinBound = -1f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Maximum value for the scale boundary.")]
		private float m_MaxBound = -1f;

		public string name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
			}
		}

		public bool enabled
		{
			get
			{
				return m_Enabled;
			}
			set
			{
				m_Enabled = value;
			}
		}

		public float scale
		{
			get
			{
				return m_Scale;
			}
			set
			{
				m_Scale = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.ScalerVisualImpact visualImpact
		{
			get
			{
				return m_VisualImpact;
			}
			set
			{
				m_VisualImpact = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.ScalerTarget target
		{
			get
			{
				return m_Target;
			}
			set
			{
				m_Target = value;
			}
		}

		public int maxLevel
		{
			get
			{
				return m_MaxLevel;
			}
			set
			{
				m_MaxLevel = value;
			}
		}

		public float minBound
		{
			get
			{
				return m_MinBound;
			}
			set
			{
				m_MinBound = value;
			}
		}

		public float maxBound
		{
			get
			{
				return m_MaxBound;
			}
			set
			{
				m_MaxBound = value;
			}
		}
	}
}
