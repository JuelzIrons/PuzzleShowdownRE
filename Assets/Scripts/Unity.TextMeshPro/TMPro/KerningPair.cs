namespace TMPro
{
	[global::System.Serializable]
	public class KerningPair
	{
		[global::UnityEngine.Serialization.FormerlySerializedAs("AscII_Left")]
		[global::UnityEngine.SerializeField]
		private uint m_FirstGlyph;

		[global::UnityEngine.SerializeField]
		private global::TMPro.GlyphValueRecord_Legacy m_FirstGlyphAdjustments;

		[global::UnityEngine.Serialization.FormerlySerializedAs("AscII_Right")]
		[global::UnityEngine.SerializeField]
		private uint m_SecondGlyph;

		[global::UnityEngine.SerializeField]
		private global::TMPro.GlyphValueRecord_Legacy m_SecondGlyphAdjustments;

		[global::UnityEngine.Serialization.FormerlySerializedAs("XadvanceOffset")]
		public float xOffset;

		internal static global::TMPro.KerningPair empty = new global::TMPro.KerningPair(0u, default(global::TMPro.GlyphValueRecord_Legacy), 0u, default(global::TMPro.GlyphValueRecord_Legacy));

		[global::UnityEngine.SerializeField]
		private bool m_IgnoreSpacingAdjustments;

		public uint firstGlyph
		{
			get
			{
				return m_FirstGlyph;
			}
			set
			{
				m_FirstGlyph = value;
			}
		}

		public global::TMPro.GlyphValueRecord_Legacy firstGlyphAdjustments => m_FirstGlyphAdjustments;

		public uint secondGlyph
		{
			get
			{
				return m_SecondGlyph;
			}
			set
			{
				m_SecondGlyph = value;
			}
		}

		public global::TMPro.GlyphValueRecord_Legacy secondGlyphAdjustments => m_SecondGlyphAdjustments;

		public bool ignoreSpacingAdjustments => m_IgnoreSpacingAdjustments;

		public KerningPair()
		{
			m_FirstGlyph = 0u;
			m_FirstGlyphAdjustments = default(global::TMPro.GlyphValueRecord_Legacy);
			m_SecondGlyph = 0u;
			m_SecondGlyphAdjustments = default(global::TMPro.GlyphValueRecord_Legacy);
		}

		public KerningPair(uint left, uint right, float offset)
		{
			firstGlyph = left;
			m_SecondGlyph = right;
			xOffset = offset;
		}

		public KerningPair(uint firstGlyph, global::TMPro.GlyphValueRecord_Legacy firstGlyphAdjustments, uint secondGlyph, global::TMPro.GlyphValueRecord_Legacy secondGlyphAdjustments)
		{
			m_FirstGlyph = firstGlyph;
			m_FirstGlyphAdjustments = firstGlyphAdjustments;
			m_SecondGlyph = secondGlyph;
			m_SecondGlyphAdjustments = secondGlyphAdjustments;
		}

		internal void ConvertLegacyKerningData()
		{
			m_FirstGlyphAdjustments.xAdvance = xOffset;
		}
	}
}
