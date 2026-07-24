namespace TMPro
{
	[global::System.Diagnostics.DebuggerDisplay("{DebuggerDisplay()}")]
	internal struct TextProcessingElement
	{
		private global::TMPro.TextProcessingElementType m_ElementType;

		private int m_StartIndex;

		private int m_Length;

		private global::TMPro.CharacterElement m_CharacterElement;

		private global::TMPro.MarkupElement m_MarkupElement;

		public global::TMPro.TextProcessingElementType ElementType
		{
			get
			{
				return m_ElementType;
			}
			set
			{
				m_ElementType = value;
			}
		}

		public int StartIndex
		{
			get
			{
				return m_StartIndex;
			}
			set
			{
				m_StartIndex = value;
			}
		}

		public int Length
		{
			get
			{
				return m_Length;
			}
			set
			{
				m_Length = value;
			}
		}

		public global::TMPro.CharacterElement CharacterElement => m_CharacterElement;

		public global::TMPro.MarkupElement MarkupElement
		{
			get
			{
				return m_MarkupElement;
			}
			set
			{
				m_MarkupElement = value;
			}
		}

		public static global::TMPro.TextProcessingElement Undefined => new global::TMPro.TextProcessingElement
		{
			ElementType = global::TMPro.TextProcessingElementType.Undefined
		};

		public TextProcessingElement(global::TMPro.TextProcessingElementType elementType, int startIndex, int length)
		{
			m_ElementType = elementType;
			m_StartIndex = startIndex;
			m_Length = length;
			m_CharacterElement = default(global::TMPro.CharacterElement);
			m_MarkupElement = default(global::TMPro.MarkupElement);
		}

		public TextProcessingElement(global::TMPro.TMP_TextElement textElement, int startIndex, int length)
		{
			m_ElementType = global::TMPro.TextProcessingElementType.TextCharacterElement;
			m_StartIndex = startIndex;
			m_Length = length;
			m_CharacterElement = new global::TMPro.CharacterElement(textElement);
			m_MarkupElement = default(global::TMPro.MarkupElement);
		}

		public TextProcessingElement(global::TMPro.CharacterElement characterElement, int startIndex, int length)
		{
			m_ElementType = global::TMPro.TextProcessingElementType.TextCharacterElement;
			m_StartIndex = startIndex;
			m_Length = length;
			m_CharacterElement = characterElement;
			m_MarkupElement = default(global::TMPro.MarkupElement);
		}

		public TextProcessingElement(global::TMPro.MarkupElement markupElement)
		{
			m_ElementType = global::TMPro.TextProcessingElementType.TextMarkupElement;
			m_StartIndex = markupElement.ValueStartIndex;
			m_Length = markupElement.ValueLength;
			m_CharacterElement = default(global::TMPro.CharacterElement);
			m_MarkupElement = markupElement;
		}

		private string DebuggerDisplay()
		{
			if (m_ElementType != global::TMPro.TextProcessingElementType.TextCharacterElement)
			{
				return $"Markup = {(global::TMPro.MarkupTag)m_MarkupElement.NameHashCode}";
			}
			return $"Unicode ({m_CharacterElement.Unicode})   '{(char)m_CharacterElement.Unicode}' ";
		}
	}
}
