namespace TMPro
{
	[global::System.Serializable]
	public struct GlyphAnchorPoint
	{
		[global::UnityEngine.SerializeField]
		private float m_XCoordinate;

		[global::UnityEngine.SerializeField]
		private float m_YCoordinate;

		public float xCoordinate
		{
			get
			{
				return m_XCoordinate;
			}
			set
			{
				m_XCoordinate = value;
			}
		}

		public float yCoordinate
		{
			get
			{
				return m_YCoordinate;
			}
			set
			{
				m_YCoordinate = value;
			}
		}
	}
}
