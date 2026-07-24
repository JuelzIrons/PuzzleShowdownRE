namespace TMPro
{
	[global::System.Serializable]
	public struct MarkToBaseAdjustmentRecord
	{
		[global::UnityEngine.SerializeField]
		private uint m_BaseGlyphID;

		[global::UnityEngine.SerializeField]
		private global::TMPro.GlyphAnchorPoint m_BaseGlyphAnchorPoint;

		[global::UnityEngine.SerializeField]
		private uint m_MarkGlyphID;

		[global::UnityEngine.SerializeField]
		private global::TMPro.MarkPositionAdjustment m_MarkPositionAdjustment;

		public uint baseGlyphID
		{
			get
			{
				return m_BaseGlyphID;
			}
			set
			{
				m_BaseGlyphID = value;
			}
		}

		public global::TMPro.GlyphAnchorPoint baseGlyphAnchorPoint
		{
			get
			{
				return m_BaseGlyphAnchorPoint;
			}
			set
			{
				m_BaseGlyphAnchorPoint = value;
			}
		}

		public uint markGlyphID
		{
			get
			{
				return m_MarkGlyphID;
			}
			set
			{
				m_MarkGlyphID = value;
			}
		}

		public global::TMPro.MarkPositionAdjustment markPositionAdjustment
		{
			get
			{
				return m_MarkPositionAdjustment;
			}
			set
			{
				m_MarkPositionAdjustment = value;
			}
		}
	}
}
