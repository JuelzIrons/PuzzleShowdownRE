namespace TMPro
{
	[global::System.Serializable]
	public class TMP_TextElement
	{
		[global::UnityEngine.SerializeField]
		internal global::TMPro.TextElementType m_ElementType;

		[global::UnityEngine.SerializeField]
		internal uint m_Unicode;

		internal global::TMPro.TMP_Asset m_TextAsset;

		internal global::UnityEngine.TextCore.Glyph m_Glyph;

		[global::UnityEngine.SerializeField]
		internal uint m_GlyphIndex;

		[global::UnityEngine.SerializeField]
		internal float m_Scale;

		public global::TMPro.TextElementType elementType => m_ElementType;

		public uint unicode
		{
			get
			{
				return m_Unicode;
			}
			set
			{
				m_Unicode = value;
			}
		}

		public global::TMPro.TMP_Asset textAsset
		{
			get
			{
				return m_TextAsset;
			}
			set
			{
				m_TextAsset = value;
			}
		}

		public global::UnityEngine.TextCore.Glyph glyph
		{
			get
			{
				return m_Glyph;
			}
			set
			{
				m_Glyph = value;
			}
		}

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

		public float scale
		{
			get
			{
				return m_Scale;
			}
			set
			{
				m_Scale = value;
			}
		}
	}
}
