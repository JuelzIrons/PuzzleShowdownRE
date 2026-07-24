namespace TMPro
{
	[global::System.Serializable]
	public class TMP_Character : global::TMPro.TMP_TextElement
	{
		public TMP_Character()
		{
			m_ElementType = global::TMPro.TextElementType.Character;
			base.scale = 1f;
		}

		public TMP_Character(uint unicode, global::UnityEngine.TextCore.Glyph glyph)
		{
			m_ElementType = global::TMPro.TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		public TMP_Character(uint unicode, global::TMPro.TMP_FontAsset fontAsset, global::UnityEngine.TextCore.Glyph glyph)
		{
			m_ElementType = global::TMPro.TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = fontAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		internal TMP_Character(uint unicode, uint glyphIndex)
		{
			m_ElementType = global::TMPro.TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}
	}
}
