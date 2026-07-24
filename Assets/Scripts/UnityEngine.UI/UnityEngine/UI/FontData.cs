namespace UnityEngine.UI
{
	[global::System.Serializable]
	public class FontData : global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("font")]
		private global::UnityEngine.Font m_Font;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("fontSize")]
		private int m_FontSize;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("fontStyle")]
		private global::UnityEngine.FontStyle m_FontStyle;

		[global::UnityEngine.SerializeField]
		private bool m_BestFit;

		[global::UnityEngine.SerializeField]
		private int m_MinSize;

		[global::UnityEngine.SerializeField]
		private int m_MaxSize;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("alignment")]
		private global::UnityEngine.TextAnchor m_Alignment;

		[global::UnityEngine.SerializeField]
		private bool m_AlignByGeometry;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("richText")]
		private bool m_RichText;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.HorizontalWrapMode m_HorizontalOverflow;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.VerticalWrapMode m_VerticalOverflow;

		[global::UnityEngine.SerializeField]
		private float m_LineSpacing;

		public static global::UnityEngine.UI.FontData defaultFontData => new global::UnityEngine.UI.FontData
		{
			m_FontSize = 14,
			m_LineSpacing = 1f,
			m_FontStyle = global::UnityEngine.FontStyle.Normal,
			m_BestFit = false,
			m_MinSize = 10,
			m_MaxSize = 40,
			m_Alignment = global::UnityEngine.TextAnchor.UpperLeft,
			m_HorizontalOverflow = global::UnityEngine.HorizontalWrapMode.Wrap,
			m_VerticalOverflow = global::UnityEngine.VerticalWrapMode.Truncate,
			m_RichText = true,
			m_AlignByGeometry = false
		};

		public global::UnityEngine.Font font
		{
			get
			{
				return m_Font;
			}
			set
			{
				m_Font = value;
			}
		}

		public int fontSize
		{
			get
			{
				return m_FontSize;
			}
			set
			{
				m_FontSize = value;
			}
		}

		public global::UnityEngine.FontStyle fontStyle
		{
			get
			{
				return m_FontStyle;
			}
			set
			{
				m_FontStyle = value;
			}
		}

		public bool bestFit
		{
			get
			{
				return m_BestFit;
			}
			set
			{
				m_BestFit = value;
			}
		}

		public int minSize
		{
			get
			{
				return m_MinSize;
			}
			set
			{
				m_MinSize = value;
			}
		}

		public int maxSize
		{
			get
			{
				return m_MaxSize;
			}
			set
			{
				m_MaxSize = value;
			}
		}

		public global::UnityEngine.TextAnchor alignment
		{
			get
			{
				return m_Alignment;
			}
			set
			{
				m_Alignment = value;
			}
		}

		public bool alignByGeometry
		{
			get
			{
				return m_AlignByGeometry;
			}
			set
			{
				m_AlignByGeometry = value;
			}
		}

		public bool richText
		{
			get
			{
				return m_RichText;
			}
			set
			{
				m_RichText = value;
			}
		}

		public global::UnityEngine.HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return m_HorizontalOverflow;
			}
			set
			{
				m_HorizontalOverflow = value;
			}
		}

		public global::UnityEngine.VerticalWrapMode verticalOverflow
		{
			get
			{
				return m_VerticalOverflow;
			}
			set
			{
				m_VerticalOverflow = value;
			}
		}

		public float lineSpacing
		{
			get
			{
				return m_LineSpacing;
			}
			set
			{
				m_LineSpacing = value;
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			m_FontSize = global::UnityEngine.Mathf.Clamp(m_FontSize, 0, 300);
			m_MinSize = global::UnityEngine.Mathf.Clamp(m_MinSize, 0, m_FontSize);
			m_MaxSize = global::UnityEngine.Mathf.Clamp(m_MaxSize, m_FontSize, 300);
		}
	}
}
