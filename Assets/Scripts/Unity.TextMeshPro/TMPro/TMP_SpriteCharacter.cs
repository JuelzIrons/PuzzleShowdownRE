namespace TMPro
{
	[global::System.Serializable]
	public class TMP_SpriteCharacter : global::TMPro.TMP_TextElement
	{
		[global::UnityEngine.SerializeField]
		private string m_Name;

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

		public TMP_SpriteCharacter()
		{
			m_ElementType = global::TMPro.TextElementType.Sprite;
		}

		public TMP_SpriteCharacter(uint unicode, global::TMPro.TMP_SpriteGlyph glyph)
		{
			m_ElementType = global::TMPro.TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}

		public TMP_SpriteCharacter(uint unicode, global::TMPro.TMP_SpriteAsset spriteAsset, global::TMPro.TMP_SpriteGlyph glyph)
		{
			m_ElementType = global::TMPro.TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = spriteAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		internal TMP_SpriteCharacter(uint unicode, uint glyphIndex)
		{
			m_ElementType = global::TMPro.TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}
	}
}
