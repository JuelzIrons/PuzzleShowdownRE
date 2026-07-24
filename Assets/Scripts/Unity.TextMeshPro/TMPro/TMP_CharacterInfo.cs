namespace TMPro
{
	[global::System.Diagnostics.DebuggerDisplay("Unicode '{character}'  ({((uint)character).ToString(\"X\")})")]
	public struct TMP_CharacterInfo
	{
		public global::TMPro.TMP_TextElementType elementType;

		public char character;

		public int index;

		public int stringLength;

		public global::TMPro.TMP_TextElement textElement;

		public global::UnityEngine.TextCore.Glyph alternativeGlyph;

		public global::TMPro.TMP_FontAsset fontAsset;

		public global::UnityEngine.Material material;

		public int materialReferenceIndex;

		public bool isUsingAlternateTypeface;

		public float pointSize;

		public int lineNumber;

		public int pageNumber;

		public int vertexIndex;

		public global::TMPro.TMP_Vertex vertex_BL;

		public global::TMPro.TMP_Vertex vertex_TL;

		public global::TMPro.TMP_Vertex vertex_TR;

		public global::TMPro.TMP_Vertex vertex_BR;

		public global::UnityEngine.Vector3 topLeft;

		public global::UnityEngine.Vector3 bottomLeft;

		public global::UnityEngine.Vector3 topRight;

		public global::UnityEngine.Vector3 bottomRight;

		public float origin;

		public float xAdvance;

		public float ascender;

		public float baseLine;

		public float descender;

		internal float adjustedAscender;

		internal float adjustedDescender;

		internal float adjustedHorizontalAdvance;

		public float aspectRatio;

		public float scale;

		public global::UnityEngine.Color32 color;

		public global::UnityEngine.Color32 underlineColor;

		public int underlineVertexIndex;

		public global::UnityEngine.Color32 strikethroughColor;

		public int strikethroughVertexIndex;

		public global::UnityEngine.Color32 highlightColor;

		public global::TMPro.HighlightState highlightState;

		public global::TMPro.FontStyles style;

		public bool isVisible;
	}
}
