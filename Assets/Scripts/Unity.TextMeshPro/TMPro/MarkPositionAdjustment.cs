namespace TMPro
{
	[global::System.Serializable]
	public struct MarkPositionAdjustment
	{
		[global::UnityEngine.SerializeField]
		private float m_XPositionAdjustment;

		[global::UnityEngine.SerializeField]
		private float m_YPositionAdjustment;

		public float xPositionAdjustment
		{
			get
			{
				return m_XPositionAdjustment;
			}
			set
			{
				m_XPositionAdjustment = value;
			}
		}

		public float yPositionAdjustment
		{
			get
			{
				return m_YPositionAdjustment;
			}
			set
			{
				m_YPositionAdjustment = value;
			}
		}

		public MarkPositionAdjustment(float x, float y)
		{
			m_XPositionAdjustment = x;
			m_YPositionAdjustment = y;
		}
	}
}
