namespace TMPro
{
	[global::System.Serializable]
	public class TMP_FontFeatureTable
	{
		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::TMPro.MultipleSubstitutionRecord> m_MultipleSubstitutionRecords;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::TMPro.LigatureSubstitutionRecord> m_LigatureSubstitutionRecords;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::TMPro.MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecords;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::TMPro.MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecords;

		internal global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.List<global::TMPro.LigatureSubstitutionRecord>> m_LigatureSubstitutionRecordLookup;

		internal global::System.Collections.Generic.Dictionary<uint, global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookup;

		internal global::System.Collections.Generic.Dictionary<uint, global::TMPro.MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecordLookup;

		internal global::System.Collections.Generic.Dictionary<uint, global::TMPro.MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecordLookup;

		public global::System.Collections.Generic.List<global::TMPro.MultipleSubstitutionRecord> multipleSubstitutionRecords
		{
			get
			{
				return m_MultipleSubstitutionRecords;
			}
			set
			{
				m_MultipleSubstitutionRecords = value;
			}
		}

		public global::System.Collections.Generic.List<global::TMPro.LigatureSubstitutionRecord> ligatureRecords
		{
			get
			{
				return m_LigatureSubstitutionRecords;
			}
			set
			{
				m_LigatureSubstitutionRecords = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
		{
			get
			{
				return m_GlyphPairAdjustmentRecords;
			}
			set
			{
				m_GlyphPairAdjustmentRecords = value;
			}
		}

		public global::System.Collections.Generic.List<global::TMPro.MarkToBaseAdjustmentRecord> MarkToBaseAdjustmentRecords
		{
			get
			{
				return m_MarkToBaseAdjustmentRecords;
			}
			set
			{
				m_MarkToBaseAdjustmentRecords = value;
			}
		}

		public global::System.Collections.Generic.List<global::TMPro.MarkToMarkAdjustmentRecord> MarkToMarkAdjustmentRecords
		{
			get
			{
				return m_MarkToMarkAdjustmentRecords;
			}
			set
			{
				m_MarkToMarkAdjustmentRecords = value;
			}
		}

		public TMP_FontFeatureTable()
		{
			m_LigatureSubstitutionRecords = new global::System.Collections.Generic.List<global::TMPro.LigatureSubstitutionRecord>();
			m_LigatureSubstitutionRecordLookup = new global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.List<global::TMPro.LigatureSubstitutionRecord>>();
			m_GlyphPairAdjustmentRecords = new global::System.Collections.Generic.List<global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord>();
			m_GlyphPairAdjustmentRecordLookup = new global::System.Collections.Generic.Dictionary<uint, global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord>();
			m_MarkToBaseAdjustmentRecords = new global::System.Collections.Generic.List<global::TMPro.MarkToBaseAdjustmentRecord>();
			m_MarkToBaseAdjustmentRecordLookup = new global::System.Collections.Generic.Dictionary<uint, global::TMPro.MarkToBaseAdjustmentRecord>();
			m_MarkToMarkAdjustmentRecords = new global::System.Collections.Generic.List<global::TMPro.MarkToMarkAdjustmentRecord>();
			m_MarkToMarkAdjustmentRecordLookup = new global::System.Collections.Generic.Dictionary<uint, global::TMPro.MarkToMarkAdjustmentRecord>();
		}

		public void SortGlyphPairAdjustmentRecords()
		{
			if (m_GlyphPairAdjustmentRecords.Count > 0)
			{
				m_GlyphPairAdjustmentRecords = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(m_GlyphPairAdjustmentRecords, (global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord s) => s.firstAdjustmentRecord.glyphIndex), (global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord s) => s.secondAdjustmentRecord.glyphIndex));
			}
		}

		public void SortMarkToBaseAdjustmentRecords()
		{
			if (m_MarkToBaseAdjustmentRecords.Count > 0)
			{
				m_MarkToBaseAdjustmentRecords = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(m_MarkToBaseAdjustmentRecords, (global::TMPro.MarkToBaseAdjustmentRecord s) => s.baseGlyphID), (global::TMPro.MarkToBaseAdjustmentRecord s) => s.markGlyphID));
			}
		}

		public void SortMarkToMarkAdjustmentRecords()
		{
			if (m_MarkToMarkAdjustmentRecords.Count > 0)
			{
				m_MarkToMarkAdjustmentRecords = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(m_MarkToMarkAdjustmentRecords, (global::TMPro.MarkToMarkAdjustmentRecord s) => s.baseMarkGlyphID), (global::TMPro.MarkToMarkAdjustmentRecord s) => s.combiningMarkGlyphID));
			}
		}
	}
}
