namespace TMPro
{
	[global::System.Serializable]
	public class TMP_GlyphPairAdjustmentRecord
	{
		[global::UnityEngine.SerializeField]
		internal global::TMPro.TMP_GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		[global::UnityEngine.SerializeField]
		internal global::TMPro.TMP_GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		[global::UnityEngine.SerializeField]
		internal global::TMPro.FontFeatureLookupFlags m_FeatureLookupFlags;

		public global::TMPro.TMP_GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return m_FirstAdjustmentRecord;
			}
			set
			{
				m_FirstAdjustmentRecord = value;
			}
		}

		public global::TMPro.TMP_GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return m_SecondAdjustmentRecord;
			}
			set
			{
				m_SecondAdjustmentRecord = value;
			}
		}

		public global::TMPro.FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return m_FeatureLookupFlags;
			}
			set
			{
				m_FeatureLookupFlags = value;
			}
		}

		public TMP_GlyphPairAdjustmentRecord(global::TMPro.TMP_GlyphAdjustmentRecord firstAdjustmentRecord, global::TMPro.TMP_GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			m_FirstAdjustmentRecord = firstAdjustmentRecord;
			m_SecondAdjustmentRecord = secondAdjustmentRecord;
			m_FeatureLookupFlags = global::TMPro.FontFeatureLookupFlags.None;
		}

		internal TMP_GlyphPairAdjustmentRecord(global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord glyphPairAdjustmentRecord)
		{
			m_FirstAdjustmentRecord = new global::TMPro.TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.firstAdjustmentRecord);
			m_SecondAdjustmentRecord = new global::TMPro.TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.secondAdjustmentRecord);
			m_FeatureLookupFlags = global::TMPro.FontFeatureLookupFlags.None;
		}
	}
}
