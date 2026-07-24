namespace TMPro
{
	[global::System.Serializable]
	public struct TMP_GlyphAdjustmentRecord
	{
		[global::UnityEngine.SerializeField]
		internal uint m_GlyphIndex;

		[global::UnityEngine.SerializeField]
		internal global::TMPro.TMP_GlyphValueRecord m_GlyphValueRecord;

		public uint glyphIndex
		{
			get
			{
				return m_GlyphIndex;
			}
			set
			{
				m_GlyphIndex = value;
			}
		}

		public global::TMPro.TMP_GlyphValueRecord glyphValueRecord
		{
			get
			{
				return m_GlyphValueRecord;
			}
			set
			{
				m_GlyphValueRecord = value;
			}
		}

		public TMP_GlyphAdjustmentRecord(uint glyphIndex, global::TMPro.TMP_GlyphValueRecord glyphValueRecord)
		{
			m_GlyphIndex = glyphIndex;
			m_GlyphValueRecord = glyphValueRecord;
		}

		internal TMP_GlyphAdjustmentRecord(global::UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord adjustmentRecord)
		{
			m_GlyphIndex = adjustmentRecord.glyphIndex;
			m_GlyphValueRecord = new global::TMPro.TMP_GlyphValueRecord(adjustmentRecord.glyphValueRecord);
		}
	}
}
