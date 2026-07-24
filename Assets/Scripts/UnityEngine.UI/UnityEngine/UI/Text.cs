namespace UnityEngine.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.CanvasRenderer))]
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Legacy/Text", 100)]
	public class Text : global::UnityEngine.UI.MaskableGraphic, global::UnityEngine.UI.ILayoutElement
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.FontData m_FontData = global::UnityEngine.UI.FontData.defaultFontData;

		[global::UnityEngine.TextArea(3, 10)]
		[global::UnityEngine.SerializeField]
		protected string m_Text = string.Empty;

		private global::UnityEngine.TextGenerator m_TextCache;

		private global::UnityEngine.TextGenerator m_TextCacheForLayout;

		protected static global::UnityEngine.Material s_DefaultText;

		[global::System.NonSerialized]
		protected bool m_DisableFontTextureRebuiltCallback;

		private readonly global::UnityEngine.UIVertex[] m_TempVerts = new global::UnityEngine.UIVertex[4];

		public global::UnityEngine.TextGenerator cachedTextGenerator => m_TextCache ?? (m_TextCache = ((m_Text.Length != 0) ? new global::UnityEngine.TextGenerator(m_Text.Length) : new global::UnityEngine.TextGenerator()));

		public global::UnityEngine.TextGenerator cachedTextGeneratorForLayout => m_TextCacheForLayout ?? (m_TextCacheForLayout = new global::UnityEngine.TextGenerator());

		public override global::UnityEngine.Texture mainTexture
		{
			get
			{
				if (font != null && font.material != null && font.material.mainTexture != null)
				{
					return font.material.mainTexture;
				}
				if (m_Material != null)
				{
					return m_Material.mainTexture;
				}
				return base.mainTexture;
			}
		}

		public global::UnityEngine.Font font
		{
			get
			{
				return m_FontData.font;
			}
			set
			{
				if (!(m_FontData.font == value))
				{
					if (base.isActiveAndEnabled)
					{
						global::UnityEngine.UI.FontUpdateTracker.UntrackText(this);
					}
					m_FontData.font = value;
					if (base.isActiveAndEnabled)
					{
						global::UnityEngine.UI.FontUpdateTracker.TrackText(this);
					}
					SetAllDirty();
				}
			}
		}

		public virtual string text
		{
			get
			{
				return m_Text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					if (!string.IsNullOrEmpty(m_Text))
					{
						m_Text = "";
						SetVerticesDirty();
					}
				}
				else if (m_Text != value)
				{
					m_Text = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public bool supportRichText
		{
			get
			{
				return m_FontData.richText;
			}
			set
			{
				if (m_FontData.richText != value)
				{
					m_FontData.richText = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public bool resizeTextForBestFit
		{
			get
			{
				return m_FontData.bestFit;
			}
			set
			{
				if (m_FontData.bestFit != value)
				{
					m_FontData.bestFit = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public int resizeTextMinSize
		{
			get
			{
				return m_FontData.minSize;
			}
			set
			{
				if (m_FontData.minSize != value)
				{
					m_FontData.minSize = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public int resizeTextMaxSize
		{
			get
			{
				return m_FontData.maxSize;
			}
			set
			{
				if (m_FontData.maxSize != value)
				{
					m_FontData.maxSize = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public global::UnityEngine.TextAnchor alignment
		{
			get
			{
				return m_FontData.alignment;
			}
			set
			{
				if (m_FontData.alignment != value)
				{
					m_FontData.alignment = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public bool alignByGeometry
		{
			get
			{
				return m_FontData.alignByGeometry;
			}
			set
			{
				if (m_FontData.alignByGeometry != value)
				{
					m_FontData.alignByGeometry = value;
					SetVerticesDirty();
				}
			}
		}

		public int fontSize
		{
			get
			{
				return m_FontData.fontSize;
			}
			set
			{
				if (m_FontData.fontSize != value)
				{
					m_FontData.fontSize = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public global::UnityEngine.HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return m_FontData.horizontalOverflow;
			}
			set
			{
				if (m_FontData.horizontalOverflow != value)
				{
					m_FontData.horizontalOverflow = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public global::UnityEngine.VerticalWrapMode verticalOverflow
		{
			get
			{
				return m_FontData.verticalOverflow;
			}
			set
			{
				if (m_FontData.verticalOverflow != value)
				{
					m_FontData.verticalOverflow = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public float lineSpacing
		{
			get
			{
				return m_FontData.lineSpacing;
			}
			set
			{
				if (m_FontData.lineSpacing != value)
				{
					m_FontData.lineSpacing = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public global::UnityEngine.FontStyle fontStyle
		{
			get
			{
				return m_FontData.fontStyle;
			}
			set
			{
				if (m_FontData.fontStyle != value)
				{
					m_FontData.fontStyle = value;
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		public float pixelsPerUnit
		{
			get
			{
				global::UnityEngine.Canvas canvas = base.canvas;
				if (!canvas)
				{
					return 1f;
				}
				if (!font || font.dynamic)
				{
					return canvas.scaleFactor;
				}
				if (m_FontData.fontSize <= 0 || font.fontSize <= 0)
				{
					return 1f;
				}
				return (float)font.fontSize / (float)m_FontData.fontSize;
			}
		}

		public virtual float minWidth => 0f;

		public virtual float preferredWidth
		{
			get
			{
				global::UnityEngine.TextGenerationSettings generationSettings = GetGenerationSettings(global::UnityEngine.Vector2.zero);
				return cachedTextGeneratorForLayout.GetPreferredWidth(m_Text, generationSettings) / pixelsPerUnit;
			}
		}

		public virtual float flexibleWidth => -1f;

		public virtual float minHeight => 0f;

		public virtual float preferredHeight
		{
			get
			{
				global::UnityEngine.TextGenerationSettings generationSettings = GetGenerationSettings(new global::UnityEngine.Vector2(GetPixelAdjustedRect().size.x, 0f));
				return cachedTextGeneratorForLayout.GetPreferredHeight(m_Text, generationSettings) / pixelsPerUnit;
			}
		}

		public virtual float flexibleHeight => -1f;

		public virtual int layoutPriority => 0;

		protected Text()
		{
			base.useLegacyMeshGeneration = false;
		}

		public void FontTextureChanged()
		{
			if (!this || m_DisableFontTextureRebuiltCallback)
			{
				return;
			}
			cachedTextGenerator.Invalidate();
			if (IsActive())
			{
				if (global::UnityEngine.UI.CanvasUpdateRegistry.IsRebuildingGraphics() || global::UnityEngine.UI.CanvasUpdateRegistry.IsRebuildingLayout())
				{
					UpdateGeometry();
				}
				else
				{
					SetAllDirty();
				}
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			cachedTextGenerator.Invalidate();
			global::UnityEngine.UI.FontUpdateTracker.TrackText(this);
		}

		protected override void OnDisable()
		{
			global::UnityEngine.UI.FontUpdateTracker.UntrackText(this);
			base.OnDisable();
		}

		protected override void UpdateGeometry()
		{
			if (font != null)
			{
				base.UpdateGeometry();
			}
		}

		internal void AssignDefaultFont()
		{
			font = global::UnityEngine.Resources.GetBuiltinResource<global::UnityEngine.Font>("LegacyRuntime.ttf");
		}

		internal void AssignDefaultFontIfNecessary()
		{
			if (font == null)
			{
				font = global::UnityEngine.Resources.GetBuiltinResource<global::UnityEngine.Font>("LegacyRuntime.ttf");
			}
		}

		public global::UnityEngine.TextGenerationSettings GetGenerationSettings(global::UnityEngine.Vector2 extents)
		{
			global::UnityEngine.TextGenerationSettings result = new global::UnityEngine.TextGenerationSettings
			{
				generationExtents = extents
			};
			if (font != null && font.dynamic)
			{
				result.fontSize = m_FontData.fontSize;
				result.resizeTextMinSize = m_FontData.minSize;
				result.resizeTextMaxSize = m_FontData.maxSize;
			}
			result.textAnchor = m_FontData.alignment;
			result.alignByGeometry = m_FontData.alignByGeometry;
			result.scaleFactor = pixelsPerUnit;
			result.color = color;
			result.font = font;
			result.pivot = base.rectTransform.pivot;
			result.richText = m_FontData.richText;
			result.lineSpacing = m_FontData.lineSpacing;
			result.fontStyle = m_FontData.fontStyle;
			result.resizeTextForBestFit = m_FontData.bestFit;
			result.updateBounds = false;
			result.horizontalOverflow = m_FontData.horizontalOverflow;
			result.verticalOverflow = m_FontData.verticalOverflow;
			return result;
		}

		public static global::UnityEngine.Vector2 GetTextAnchorPivot(global::UnityEngine.TextAnchor anchor)
		{
			return anchor switch
			{
				global::UnityEngine.TextAnchor.LowerLeft => new global::UnityEngine.Vector2(0f, 0f), 
				global::UnityEngine.TextAnchor.LowerCenter => new global::UnityEngine.Vector2(0.5f, 0f), 
				global::UnityEngine.TextAnchor.LowerRight => new global::UnityEngine.Vector2(1f, 0f), 
				global::UnityEngine.TextAnchor.MiddleLeft => new global::UnityEngine.Vector2(0f, 0.5f), 
				global::UnityEngine.TextAnchor.MiddleCenter => new global::UnityEngine.Vector2(0.5f, 0.5f), 
				global::UnityEngine.TextAnchor.MiddleRight => new global::UnityEngine.Vector2(1f, 0.5f), 
				global::UnityEngine.TextAnchor.UpperLeft => new global::UnityEngine.Vector2(0f, 1f), 
				global::UnityEngine.TextAnchor.UpperCenter => new global::UnityEngine.Vector2(0.5f, 1f), 
				global::UnityEngine.TextAnchor.UpperRight => new global::UnityEngine.Vector2(1f, 1f), 
				_ => global::UnityEngine.Vector2.zero, 
			};
		}

		protected override void OnPopulateMesh(global::UnityEngine.UI.VertexHelper toFill)
		{
			if (font == null)
			{
				return;
			}
			m_DisableFontTextureRebuiltCallback = true;
			global::UnityEngine.Vector2 size = base.rectTransform.rect.size;
			global::UnityEngine.TextGenerationSettings generationSettings = GetGenerationSettings(size);
			cachedTextGenerator.PopulateWithErrors(text, generationSettings, base.gameObject);
			global::System.Collections.Generic.IList<global::UnityEngine.UIVertex> verts = cachedTextGenerator.verts;
			float num = 1f / pixelsPerUnit;
			int count = verts.Count;
			if (count <= 0)
			{
				toFill.Clear();
				return;
			}
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(verts[0].position.x, verts[0].position.y) * num;
			vector = PixelAdjustPoint(vector) - vector;
			toFill.Clear();
			if (vector != global::UnityEngine.Vector2.zero)
			{
				for (int i = 0; i < count; i++)
				{
					int num2 = i & 3;
					m_TempVerts[num2] = verts[i];
					m_TempVerts[num2].position *= num;
					m_TempVerts[num2].position.x += vector.x;
					m_TempVerts[num2].position.y += vector.y;
					if (num2 == 3)
					{
						toFill.AddUIVertexQuad(m_TempVerts);
					}
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					int num3 = j & 3;
					m_TempVerts[num3] = verts[j];
					m_TempVerts[num3].position *= num;
					if (num3 == 3)
					{
						toFill.AddUIVertexQuad(m_TempVerts);
					}
				}
			}
			m_DisableFontTextureRebuiltCallback = false;
		}

		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		public virtual void CalculateLayoutInputVertical()
		{
		}
	}
}
