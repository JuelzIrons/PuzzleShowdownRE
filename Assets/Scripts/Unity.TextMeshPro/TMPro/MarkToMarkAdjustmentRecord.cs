namespace TMPro
{
	[global::System.Serializable]
	public struct MarkToMarkAdjustmentRecord
	{
		[global::UnityEngine.SerializeField]
		private uint m_BaseMarkGlyphID;

		[global::UnityEngine.SerializeField]
		private global::TMPro.GlyphAnchorPoint m_BaseMarkGlyphAnchorPoint;

		[global::UnityEngine.SerializeField]
		private uint m_CombiningMarkGlyphID;

		[global::UnityEngine.SerializeField]
		private global::TMPro.MarkPositionAdjustment m_CombiningMarkPositionAdjustment;

		public uint baseMarkGlyphID
		{
			get
			{
				return m_BaseMarkGlyphID;
			}
			set
			{
				m_BaseMarkGlyphID = value;
			}
		}

		public global::TMPro.GlyphAnchorPoint baseMarkGlyphAnchorPoint
		{
			get
			{
				return m_BaseMarkGlyphAnchorPoint;
			}
			set
			{
				m_BaseMarkGlyphAnchorPoint = value;
			}
		}

		public uint combiningMarkGlyphID
		{
			get
			{
				return m_CombiningMarkGlyphID;
			}
			set
			{
				m_CombiningMarkGlyphID = value;
			}
		}

		public global::TMPro.MarkPositionAdjustment combiningMarkPositionAdjustment
		{
			get
			{
				return m_CombiningMarkPositionAdjustment;
			}
			set
			{
				m_CombiningMarkPositionAdjustment = value;
			}
		}
	}
}
