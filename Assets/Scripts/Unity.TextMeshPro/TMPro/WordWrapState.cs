namespace TMPro
{
	internal struct WordWrapState
	{
		public int previous_WordBreak;

		public int total_CharacterCount;

		public int visible_CharacterCount;

		public int visibleSpaceCount;

		public int visible_SpriteCount;

		public int visible_LinkCount;

		public int firstCharacterIndex;

		public int firstVisibleCharacterIndex;

		public int lastCharacterIndex;

		public int lastVisibleCharIndex;

		public int lineNumber;

		public float maxCapHeight;

		public float maxAscender;

		public float maxDescender;

		public float startOfLineAscender;

		public float maxLineAscender;

		public float maxLineDescender;

		public float pageAscender;

		public global::TMPro.HorizontalAlignmentOptions horizontalAlignment;

		public float marginLeft;

		public float marginRight;

		public float xAdvance;

		public float preferredWidth;

		public float preferredHeight;

		public float renderedWidth;

		public float renderedHeight;

		public float previousLineScale;

		public int wordCount;

		public global::TMPro.FontStyles fontStyle;

		public int italicAngle;

		public float fontScaleMultiplier;

		public float currentFontSize;

		public float baselineOffset;

		public float lineOffset;

		public bool isDrivenLineSpacing;

		public int lastBaseGlyphIndex;

		public float cSpace;

		public float mSpace;

		public global::TMPro.TMP_TextInfo textInfo;

		public global::TMPro.TMP_LineInfo lineInfo;

		public global::UnityEngine.Color32 vertexColor;

		public global::UnityEngine.Color32 underlineColor;

		public global::UnityEngine.Color32 strikethroughColor;

		public global::TMPro.HighlightState highlightState;

		public global::TMPro.TMP_FontStyleStack basicStyleStack;

		public global::TMPro.TMP_TextProcessingStack<int> italicAngleStack;

		public global::TMPro.TMP_TextProcessingStack<global::UnityEngine.Color32> colorStack;

		public global::TMPro.TMP_TextProcessingStack<global::UnityEngine.Color32> underlineColorStack;

		public global::TMPro.TMP_TextProcessingStack<global::UnityEngine.Color32> strikethroughColorStack;

		public global::TMPro.TMP_TextProcessingStack<global::UnityEngine.Color32> highlightColorStack;

		public global::TMPro.TMP_TextProcessingStack<global::TMPro.HighlightState> highlightStateStack;

		public global::TMPro.TMP_TextProcessingStack<global::TMPro.TMP_ColorGradient> colorGradientStack;

		public global::TMPro.TMP_TextProcessingStack<float> sizeStack;

		public global::TMPro.TMP_TextProcessingStack<float> indentStack;

		public global::TMPro.TMP_TextProcessingStack<global::TMPro.FontWeight> fontWeightStack;

		public global::TMPro.TMP_TextProcessingStack<int> styleStack;

		public global::TMPro.TMP_TextProcessingStack<float> baselineStack;

		public global::TMPro.TMP_TextProcessingStack<int> actionStack;

		public global::TMPro.TMP_TextProcessingStack<global::TMPro.MaterialReference> materialReferenceStack;

		public global::TMPro.TMP_TextProcessingStack<global::TMPro.HorizontalAlignmentOptions> lineJustificationStack;

		public int spriteAnimationID;

		public global::TMPro.TMP_FontAsset currentFontAsset;

		public global::TMPro.TMP_SpriteAsset currentSpriteAsset;

		public global::UnityEngine.Material currentMaterial;

		public int currentMaterialIndex;

		public global::TMPro.Extents meshExtents;

		public bool tagNoParsing;

		public bool isNonBreakingSpace;

		public global::UnityEngine.Quaternion fxRotation;

		public global::UnityEngine.Vector3 fxScale;
	}
}
