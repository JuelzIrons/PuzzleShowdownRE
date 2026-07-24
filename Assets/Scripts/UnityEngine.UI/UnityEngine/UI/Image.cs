namespace UnityEngine.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.CanvasRenderer))]
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Image", 11)]
	public class Image : global::UnityEngine.UI.MaskableGraphic, global::UnityEngine.ISerializationCallbackReceiver, global::UnityEngine.UI.ILayoutElement, global::UnityEngine.ICanvasRaycastFilter
	{
		public enum Type
		{
			Simple = 0,
			Sliced = 1,
			Tiled = 2,
			Filled = 3
		}

		public enum FillMethod
		{
			Horizontal = 0,
			Vertical = 1,
			Radial90 = 2,
			Radial180 = 3,
			Radial360 = 4
		}

		public enum OriginHorizontal
		{
			Left = 0,
			Right = 1
		}

		public enum OriginVertical
		{
			Bottom = 0,
			Top = 1
		}

		public enum Origin90
		{
			BottomLeft = 0,
			TopLeft = 1,
			TopRight = 2,
			BottomRight = 3
		}

		public enum Origin180
		{
			Bottom = 0,
			Left = 1,
			Top = 2,
			Right = 3
		}

		public enum Origin360
		{
			Bottom = 0,
			Right = 1,
			Top = 2,
			Left = 3
		}

		protected static global::UnityEngine.Material s_ETC1DefaultUI = null;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_Frame")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_Sprite;

		[global::System.NonSerialized]
		private global::UnityEngine.Sprite m_OverrideSprite;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Image.Type m_Type;

		[global::UnityEngine.SerializeField]
		private bool m_PreserveAspect;

		[global::UnityEngine.SerializeField]
		private bool m_FillCenter = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Image.FillMethod m_FillMethod = global::UnityEngine.UI.Image.FillMethod.Radial360;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_FillAmount = 1f;

		[global::UnityEngine.SerializeField]
		private bool m_FillClockwise = true;

		[global::UnityEngine.SerializeField]
		private int m_FillOrigin;

		private float m_AlphaHitTestMinimumThreshold;

		private bool m_Tracked;

		[global::UnityEngine.SerializeField]
		private bool m_UseSpriteMesh;

		[global::UnityEngine.SerializeField]
		private float m_PixelsPerUnitMultiplier = 1f;

		private float m_CachedReferencePixelsPerUnit = 100f;

		private static global::UnityEngine.SecondarySpriteTexture[] s_TempNewSecondaryTextures = new global::UnityEngine.SecondarySpriteTexture[0];

		private global::UnityEngine.SecondarySpriteTexture[] m_SecondaryTextures;

		private static readonly global::UnityEngine.Vector2[] s_VertScratch = new global::UnityEngine.Vector2[4];

		private static readonly global::UnityEngine.Vector2[] s_UVScratch = new global::UnityEngine.Vector2[4];

		private static readonly global::UnityEngine.Vector3[] s_Xy = new global::UnityEngine.Vector3[4];

		private static readonly global::UnityEngine.Vector3[] s_Uv = new global::UnityEngine.Vector3[4];

		private static global::System.Collections.Generic.List<global::UnityEngine.UI.Image> m_TrackedTexturelessImages = new global::System.Collections.Generic.List<global::UnityEngine.UI.Image>();

		private static bool s_Initialized;

		public global::UnityEngine.Sprite sprite
		{
			get
			{
				return m_Sprite;
			}
			set
			{
				if (m_Sprite != null)
				{
					if (m_Sprite != value)
					{
						m_SkipLayoutUpdate = m_Sprite.rect.size.Equals(value ? value.rect.size : global::UnityEngine.Vector2.zero);
						m_SkipMaterialUpdate = m_Sprite.texture == (value ? value.texture : null) && !CheckSecondaryTexturesChanged(value);
						m_Sprite = value;
						ResetAlphaHitThresholdIfNeeded();
						SetAllDirty();
						TrackSprite();
					}
				}
				else if (value != null)
				{
					m_SkipLayoutUpdate = value.rect.size == global::UnityEngine.Vector2.zero;
					m_SkipMaterialUpdate = value.texture == null && value.GetSecondaryTextureCount() == 0;
					m_Sprite = value;
					ResetAlphaHitThresholdIfNeeded();
					SetAllDirty();
					TrackSprite();
				}
				void ResetAlphaHitThresholdIfNeeded()
				{
					if (!SpriteSupportsAlphaHitTest() && m_AlphaHitTestMinimumThreshold > 0f)
					{
						global::UnityEngine.Debug.LogWarning("Sprite was changed for one not readable or with Crunch Compression. Resetting the AlphaHitThreshold to 0.", this);
						m_AlphaHitTestMinimumThreshold = 0f;
					}
				}
				bool SpriteSupportsAlphaHitTest()
				{
					if (m_Sprite != null && m_Sprite.texture != null && !global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsCrunchFormat(m_Sprite.texture.format))
					{
						return m_Sprite.texture.isReadable;
					}
					return false;
				}
			}
		}

		public global::UnityEngine.Sprite overrideSprite
		{
			get
			{
				return activeSprite;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_OverrideSprite, value))
				{
					SetAllDirty();
					TrackSprite();
				}
			}
		}

		private global::UnityEngine.Sprite activeSprite
		{
			get
			{
				if (!(m_OverrideSprite != null))
				{
					return sprite;
				}
				return m_OverrideSprite;
			}
		}

		public global::UnityEngine.UI.Image.Type type
		{
			get
			{
				return m_Type;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_Type, value))
				{
					SetVerticesDirty();
				}
			}
		}

		public bool preserveAspect
		{
			get
			{
				return m_PreserveAspect;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_PreserveAspect, value))
				{
					SetVerticesDirty();
				}
			}
		}

		public bool fillCenter
		{
			get
			{
				return m_FillCenter;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_FillCenter, value))
				{
					SetVerticesDirty();
				}
			}
		}

		public global::UnityEngine.UI.Image.FillMethod fillMethod
		{
			get
			{
				return m_FillMethod;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_FillMethod, value))
				{
					SetVerticesDirty();
					m_FillOrigin = 0;
				}
			}
		}

		public float fillAmount
		{
			get
			{
				return m_FillAmount;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_FillAmount, global::UnityEngine.Mathf.Clamp01(value)))
				{
					SetVerticesDirty();
				}
			}
		}

		public bool fillClockwise
		{
			get
			{
				return m_FillClockwise;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_FillClockwise, value))
				{
					SetVerticesDirty();
				}
			}
		}

		public int fillOrigin
		{
			get
			{
				return m_FillOrigin;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_FillOrigin, value))
				{
					SetVerticesDirty();
				}
			}
		}

		[global::System.Obsolete("eventAlphaThreshold has been deprecated. Use eventMinimumAlphaThreshold instead (UnityUpgradable) -> alphaHitTestMinimumThreshold")]
		public float eventAlphaThreshold
		{
			get
			{
				return 1f - alphaHitTestMinimumThreshold;
			}
			set
			{
				alphaHitTestMinimumThreshold = 1f - value;
			}
		}

		public float alphaHitTestMinimumThreshold
		{
			get
			{
				return m_AlphaHitTestMinimumThreshold;
			}
			set
			{
				if (sprite != null && (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsCrunchFormat(sprite.texture.format) || !sprite.texture.isReadable))
				{
					throw new global::System.InvalidOperationException("alphaHitTestMinimumThreshold should not be modified on a texture not readeable or not using Crunch Compression.");
				}
				m_AlphaHitTestMinimumThreshold = value;
			}
		}

		public bool useSpriteMesh
		{
			get
			{
				return m_UseSpriteMesh;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_UseSpriteMesh, value))
				{
					SetVerticesDirty();
				}
			}
		}

		public static global::UnityEngine.Material defaultETC1GraphicMaterial
		{
			get
			{
				if (s_ETC1DefaultUI == null)
				{
					s_ETC1DefaultUI = global::UnityEngine.Canvas.GetETC1SupportedCanvasMaterial();
				}
				return s_ETC1DefaultUI;
			}
		}

		public override global::UnityEngine.Texture mainTexture
		{
			get
			{
				if (activeSprite == null)
				{
					if (material != null && material.mainTexture != null)
					{
						return material.mainTexture;
					}
					return global::UnityEngine.UI.Graphic.s_WhiteTexture;
				}
				return activeSprite.texture;
			}
		}

		public bool hasBorder
		{
			get
			{
				if (activeSprite != null)
				{
					return activeSprite.border.sqrMagnitude > 0f;
				}
				return false;
			}
		}

		public float pixelsPerUnitMultiplier
		{
			get
			{
				return m_PixelsPerUnitMultiplier;
			}
			set
			{
				m_PixelsPerUnitMultiplier = global::UnityEngine.Mathf.Max(0.01f, value);
				SetVerticesDirty();
			}
		}

		public float pixelsPerUnit
		{
			get
			{
				float num = 100f;
				if ((bool)activeSprite)
				{
					num = activeSprite.pixelsPerUnit;
				}
				if ((bool)base.canvas)
				{
					m_CachedReferencePixelsPerUnit = base.canvas.referencePixelsPerUnit;
				}
				return num / m_CachedReferencePixelsPerUnit;
			}
		}

		protected float multipliedPixelsPerUnit => pixelsPerUnit * m_PixelsPerUnitMultiplier;

		public override global::UnityEngine.Material material
		{
			get
			{
				if (m_Material != null)
				{
					return m_Material;
				}
				if ((bool)activeSprite && activeSprite.associatedAlphaSplitTexture != null)
				{
					return defaultETC1GraphicMaterial;
				}
				return defaultMaterial;
			}
			set
			{
				base.material = value;
			}
		}

		internal global::UnityEngine.SecondarySpriteTexture[] secondaryTextures => m_SecondaryTextures;

		public virtual float minWidth => 0f;

		public virtual float preferredWidth
		{
			get
			{
				if (activeSprite == null)
				{
					return 0f;
				}
				if (type == global::UnityEngine.UI.Image.Type.Sliced || type == global::UnityEngine.UI.Image.Type.Tiled)
				{
					return global::UnityEngine.Sprites.DataUtility.GetMinSize(activeSprite).x / pixelsPerUnit;
				}
				return activeSprite.rect.size.x / pixelsPerUnit;
			}
		}

		public virtual float flexibleWidth => -1f;

		public virtual float minHeight => 0f;

		public virtual float preferredHeight
		{
			get
			{
				if (activeSprite == null)
				{
					return 0f;
				}
				if (type == global::UnityEngine.UI.Image.Type.Sliced || type == global::UnityEngine.UI.Image.Type.Tiled)
				{
					return global::UnityEngine.Sprites.DataUtility.GetMinSize(activeSprite).y / pixelsPerUnit;
				}
				return activeSprite.rect.size.y / pixelsPerUnit;
			}
		}

		public virtual float flexibleHeight => -1f;

		public virtual int layoutPriority => 0;

		public void DisableSpriteOptimizations()
		{
			m_SkipLayoutUpdate = false;
			m_SkipMaterialUpdate = false;
		}

		protected Image()
		{
			base.useLegacyMeshGeneration = false;
		}

		public virtual void OnBeforeSerialize()
		{
		}

		public virtual void OnAfterDeserialize()
		{
			if (m_FillOrigin < 0)
			{
				m_FillOrigin = 0;
			}
			else if (m_FillMethod == global::UnityEngine.UI.Image.FillMethod.Horizontal && m_FillOrigin > 1)
			{
				m_FillOrigin = 0;
			}
			else if (m_FillMethod == global::UnityEngine.UI.Image.FillMethod.Vertical && m_FillOrigin > 1)
			{
				m_FillOrigin = 0;
			}
			else if (m_FillOrigin > 3)
			{
				m_FillOrigin = 0;
			}
			m_FillAmount = global::UnityEngine.Mathf.Clamp(m_FillAmount, 0f, 1f);
		}

		private void PreserveSpriteAspectRatio(ref global::UnityEngine.Rect rect, global::UnityEngine.Vector2 spriteSize)
		{
			float num = spriteSize.x / spriteSize.y;
			float num2 = rect.width / rect.height;
			if (num > num2)
			{
				float height = rect.height;
				rect.height = rect.width * (1f / num);
				rect.y += (height - rect.height) * base.rectTransform.pivot.y;
			}
			else
			{
				float width = rect.width;
				rect.width = rect.height * num;
				rect.x += (width - rect.width) * base.rectTransform.pivot.x;
			}
		}

		private global::UnityEngine.Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
		{
			global::UnityEngine.Vector4 vector = ((activeSprite == null) ? global::UnityEngine.Vector4.zero : global::UnityEngine.Sprites.DataUtility.GetPadding(activeSprite));
			global::UnityEngine.Vector2 spriteSize = ((activeSprite == null) ? global::UnityEngine.Vector2.zero : new global::UnityEngine.Vector2(activeSprite.rect.width, activeSprite.rect.height));
			global::UnityEngine.Rect rect = GetPixelAdjustedRect();
			int num = global::UnityEngine.Mathf.RoundToInt(spriteSize.x);
			int num2 = global::UnityEngine.Mathf.RoundToInt(spriteSize.y);
			global::UnityEngine.Vector4 vector2 = new global::UnityEngine.Vector4(vector.x / (float)num, vector.y / (float)num2, ((float)num - vector.z) / (float)num, ((float)num2 - vector.w) / (float)num2);
			if (shouldPreserveAspect && spriteSize.sqrMagnitude > 0f)
			{
				PreserveSpriteAspectRatio(ref rect, spriteSize);
			}
			return new global::UnityEngine.Vector4(rect.x + rect.width * vector2.x, rect.y + rect.height * vector2.y, rect.x + rect.width * vector2.z, rect.y + rect.height * vector2.w);
		}

		public override void SetNativeSize()
		{
			if (activeSprite != null)
			{
				float x = activeSprite.rect.width / pixelsPerUnit;
				float y = activeSprite.rect.height / pixelsPerUnit;
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new global::UnityEngine.Vector2(x, y);
				SetAllDirty();
			}
		}

		protected override void OnPopulateMesh(global::UnityEngine.UI.VertexHelper toFill)
		{
			if (activeSprite == null)
			{
				base.OnPopulateMesh(toFill);
				return;
			}
			switch (type)
			{
			case global::UnityEngine.UI.Image.Type.Simple:
				if (!useSpriteMesh)
				{
					GenerateSimpleSprite(toFill, m_PreserveAspect);
				}
				else
				{
					GenerateSprite(toFill, m_PreserveAspect);
				}
				break;
			case global::UnityEngine.UI.Image.Type.Sliced:
				GenerateSlicedSprite(toFill);
				break;
			case global::UnityEngine.UI.Image.Type.Tiled:
				GenerateTiledSprite(toFill);
				break;
			case global::UnityEngine.UI.Image.Type.Filled:
				GenerateFilledSprite(toFill, m_PreserveAspect);
				break;
			}
		}

		private void TrackSprite()
		{
			if (activeSprite != null && activeSprite.texture == null)
			{
				TrackImage(this);
				m_Tracked = true;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			TrackSprite();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (m_Tracked)
			{
				UnTrackImage(this);
			}
		}

		private static void ClearArray(ref global::UnityEngine.SecondarySpriteTexture[] array)
		{
			array = global::System.Array.Empty<global::UnityEngine.SecondarySpriteTexture>();
		}

		private bool CheckSecondaryTexturesChanged(global::UnityEngine.Sprite sprite)
		{
			bool result = CheckSecondaryTexturesChanged(sprite, ref s_TempNewSecondaryTextures);
			ClearArray(ref s_TempNewSecondaryTextures);
			return result;
		}

		private bool CheckSecondaryTexturesChanged(global::UnityEngine.Sprite sprite, ref global::UnityEngine.SecondarySpriteTexture[] newSecondaryTextures)
		{
			if (newSecondaryTextures == null)
			{
				newSecondaryTextures = new global::UnityEngine.SecondarySpriteTexture[0];
			}
			int num = ((m_SecondaryTextures != null) ? m_SecondaryTextures.Length : 0);
			int num2 = ((sprite != null) ? sprite.GetSecondaryTextureCount() : 0);
			if (num == 0 && num2 == 0)
			{
				return false;
			}
			if (sprite != null)
			{
				global::System.Array.Resize(ref newSecondaryTextures, num2);
				sprite.GetSecondaryTextures(newSecondaryTextures);
			}
			else
			{
				ClearArray(ref newSecondaryTextures);
			}
			if (m_SecondaryTextures != null && Compare(m_SecondaryTextures, newSecondaryTextures))
			{
				return false;
			}
			return true;
			static bool Compare(global::UnityEngine.SecondarySpriteTexture[] array1, global::UnityEngine.SecondarySpriteTexture[] array2)
			{
				if (array1.Length != array2.Length)
				{
					return false;
				}
				for (int i = 0; i < array1.Length; i++)
				{
					if (array1[i] != array2[i])
					{
						return false;
					}
				}
				return true;
			}
		}

		internal void SetSecondaryTextures(global::UnityEngine.CanvasRenderer renderer)
		{
			if (CheckSecondaryTexturesChanged(activeSprite, ref s_TempNewSecondaryTextures))
			{
				if (s_TempNewSecondaryTextures.Length == 0)
				{
					m_SecondaryTextures = null;
				}
				else
				{
					if (m_SecondaryTextures == null)
					{
						m_SecondaryTextures = new global::UnityEngine.SecondarySpriteTexture[s_TempNewSecondaryTextures.Length];
					}
					else
					{
						global::System.Array.Resize(ref m_SecondaryTextures, s_TempNewSecondaryTextures.Length);
					}
					global::System.Array.Copy(s_TempNewSecondaryTextures, m_SecondaryTextures, s_TempNewSecondaryTextures.Length);
				}
			}
			global::UnityEngine.SecondarySpriteTexture[] array = m_SecondaryTextures;
			renderer.SetSecondaryTextureCount((array != null) ? array.Length : 0);
			if (m_SecondaryTextures != null)
			{
				for (int i = 0; i < m_SecondaryTextures.Length; i++)
				{
					global::UnityEngine.SecondarySpriteTexture secondarySpriteTexture = m_SecondaryTextures[i];
					renderer.SetSecondaryTexture(i, secondarySpriteTexture.name, secondarySpriteTexture.texture);
				}
			}
			ClearArray(ref s_TempNewSecondaryTextures);
		}

		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			if (activeSprite == null)
			{
				base.canvasRenderer.SetAlphaTexture(null);
				return;
			}
			global::UnityEngine.Texture2D associatedAlphaSplitTexture = activeSprite.associatedAlphaSplitTexture;
			if (associatedAlphaSplitTexture != null)
			{
				base.canvasRenderer.SetAlphaTexture(associatedAlphaSplitTexture);
			}
			SetSecondaryTextures(base.canvasRenderer);
		}

		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			if (base.canvas == null)
			{
				m_CachedReferencePixelsPerUnit = 100f;
			}
			else if (base.canvas.referencePixelsPerUnit != m_CachedReferencePixelsPerUnit)
			{
				m_CachedReferencePixelsPerUnit = base.canvas.referencePixelsPerUnit;
				if (type == global::UnityEngine.UI.Image.Type.Sliced || type == global::UnityEngine.UI.Image.Type.Tiled)
				{
					SetVerticesDirty();
					SetLayoutDirty();
				}
			}
		}

		private void GenerateSimpleSprite(global::UnityEngine.UI.VertexHelper vh, bool lPreserveAspect)
		{
			global::UnityEngine.Vector4 drawingDimensions = GetDrawingDimensions(lPreserveAspect);
			global::UnityEngine.Vector4 vector = ((activeSprite != null) ? global::UnityEngine.Sprites.DataUtility.GetOuterUV(activeSprite) : global::UnityEngine.Vector4.zero);
			global::UnityEngine.Color color = this.color;
			vh.Clear();
			vh.AddVert(new global::UnityEngine.Vector3(drawingDimensions.x, drawingDimensions.y), color, new global::UnityEngine.Vector2(vector.x, vector.y));
			vh.AddVert(new global::UnityEngine.Vector3(drawingDimensions.x, drawingDimensions.w), color, new global::UnityEngine.Vector2(vector.x, vector.w));
			vh.AddVert(new global::UnityEngine.Vector3(drawingDimensions.z, drawingDimensions.w), color, new global::UnityEngine.Vector2(vector.z, vector.w));
			vh.AddVert(new global::UnityEngine.Vector3(drawingDimensions.z, drawingDimensions.y), color, new global::UnityEngine.Vector2(vector.z, vector.y));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		private void GenerateSprite(global::UnityEngine.UI.VertexHelper vh, bool lPreserveAspect)
		{
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(activeSprite.rect.width, activeSprite.rect.height);
			global::UnityEngine.Vector2 vector2 = activeSprite.pivot / vector;
			global::UnityEngine.Vector2 pivot = base.rectTransform.pivot;
			global::UnityEngine.Rect rect = GetPixelAdjustedRect();
			if (lPreserveAspect & (vector.sqrMagnitude > 0f))
			{
				PreserveSpriteAspectRatio(ref rect, vector);
			}
			global::UnityEngine.Vector2 vector3 = new global::UnityEngine.Vector2(rect.width, rect.height);
			global::UnityEngine.Vector3 size = activeSprite.bounds.size;
			global::UnityEngine.Vector2 vector4 = (pivot - vector2) * vector3;
			global::UnityEngine.Color color = this.color;
			vh.Clear();
			global::UnityEngine.Vector2[] vertices = activeSprite.vertices;
			global::UnityEngine.Vector2[] uv = activeSprite.uv;
			for (int i = 0; i < vertices.Length; i++)
			{
				vh.AddVert(new global::UnityEngine.Vector3(vertices[i].x / size.x * vector3.x - vector4.x, vertices[i].y / size.y * vector3.y - vector4.y), color, new global::UnityEngine.Vector2(uv[i].x, uv[i].y));
			}
			ushort[] triangles = activeSprite.triangles;
			for (int j = 0; j < triangles.Length; j += 3)
			{
				vh.AddTriangle(triangles[j], triangles[j + 1], triangles[j + 2]);
			}
		}

		private void GenerateSlicedSprite(global::UnityEngine.UI.VertexHelper toFill)
		{
			if (!hasBorder)
			{
				GenerateSimpleSprite(toFill, lPreserveAspect: false);
				return;
			}
			global::UnityEngine.Vector4 vector;
			global::UnityEngine.Vector4 vector2;
			global::UnityEngine.Vector4 vector3;
			global::UnityEngine.Vector4 vector4;
			if (activeSprite != null)
			{
				vector = global::UnityEngine.Sprites.DataUtility.GetOuterUV(activeSprite);
				vector2 = global::UnityEngine.Sprites.DataUtility.GetInnerUV(activeSprite);
				vector3 = global::UnityEngine.Sprites.DataUtility.GetPadding(activeSprite);
				vector4 = activeSprite.border;
			}
			else
			{
				vector = global::UnityEngine.Vector4.zero;
				vector2 = global::UnityEngine.Vector4.zero;
				vector3 = global::UnityEngine.Vector4.zero;
				vector4 = global::UnityEngine.Vector4.zero;
			}
			global::UnityEngine.Rect pixelAdjustedRect = GetPixelAdjustedRect();
			global::UnityEngine.Vector4 adjustedBorders = GetAdjustedBorders(vector4 / multipliedPixelsPerUnit, pixelAdjustedRect);
			vector3 /= multipliedPixelsPerUnit;
			s_VertScratch[0] = new global::UnityEngine.Vector2(vector3.x, vector3.y);
			s_VertScratch[3] = new global::UnityEngine.Vector2(pixelAdjustedRect.width - vector3.z, pixelAdjustedRect.height - vector3.w);
			s_VertScratch[1].x = adjustedBorders.x;
			s_VertScratch[1].y = adjustedBorders.y;
			s_VertScratch[2].x = pixelAdjustedRect.width - adjustedBorders.z;
			s_VertScratch[2].y = pixelAdjustedRect.height - adjustedBorders.w;
			for (int i = 0; i < 4; i++)
			{
				s_VertScratch[i].x += pixelAdjustedRect.x;
				s_VertScratch[i].y += pixelAdjustedRect.y;
			}
			s_UVScratch[0] = new global::UnityEngine.Vector2(vector.x, vector.y);
			s_UVScratch[1] = new global::UnityEngine.Vector2(vector2.x, vector2.y);
			s_UVScratch[2] = new global::UnityEngine.Vector2(vector2.z, vector2.w);
			s_UVScratch[3] = new global::UnityEngine.Vector2(vector.z, vector.w);
			toFill.Clear();
			for (int j = 0; j < 3; j++)
			{
				int num = j + 1;
				for (int k = 0; k < 3; k++)
				{
					if (m_FillCenter || j != 1 || k != 1)
					{
						int num2 = k + 1;
						if (!(s_VertScratch[num].x - s_VertScratch[j].x <= 0f) && !(s_VertScratch[num2].y - s_VertScratch[k].y <= 0f))
						{
							AddQuad(toFill, new global::UnityEngine.Vector2(s_VertScratch[j].x, s_VertScratch[k].y), new global::UnityEngine.Vector2(s_VertScratch[num].x, s_VertScratch[num2].y), color, new global::UnityEngine.Vector2(s_UVScratch[j].x, s_UVScratch[k].y), new global::UnityEngine.Vector2(s_UVScratch[num].x, s_UVScratch[num2].y));
						}
					}
				}
			}
		}

		private void GenerateTiledSprite(global::UnityEngine.UI.VertexHelper toFill)
		{
			global::UnityEngine.Vector4 vector;
			global::UnityEngine.Vector4 vector2;
			global::UnityEngine.Vector2 vector4;
			global::UnityEngine.Vector4 vector3;
			if (activeSprite != null)
			{
				vector = global::UnityEngine.Sprites.DataUtility.GetOuterUV(activeSprite);
				vector2 = global::UnityEngine.Sprites.DataUtility.GetInnerUV(activeSprite);
				vector3 = activeSprite.border;
				vector4 = activeSprite.rect.size;
			}
			else
			{
				vector = global::UnityEngine.Vector4.zero;
				vector2 = global::UnityEngine.Vector4.zero;
				vector3 = global::UnityEngine.Vector4.zero;
				vector4 = global::UnityEngine.Vector2.one * 100f;
			}
			global::UnityEngine.Rect pixelAdjustedRect = GetPixelAdjustedRect();
			float num = (vector4.x - vector3.x - vector3.z) / multipliedPixelsPerUnit;
			float num2 = (vector4.y - vector3.y - vector3.w) / multipliedPixelsPerUnit;
			vector3 = GetAdjustedBorders(vector3 / multipliedPixelsPerUnit, pixelAdjustedRect);
			global::UnityEngine.Vector2 vector5 = new global::UnityEngine.Vector2(vector2.x, vector2.y);
			global::UnityEngine.Vector2 vector6 = new global::UnityEngine.Vector2(vector2.z, vector2.w);
			float x = vector3.x;
			float num3 = pixelAdjustedRect.width - vector3.z;
			float y = vector3.y;
			float num4 = pixelAdjustedRect.height - vector3.w;
			toFill.Clear();
			global::UnityEngine.Vector2 uvMax = vector6;
			if (num <= 0f)
			{
				num = num3 - x;
			}
			if (num2 <= 0f)
			{
				num2 = num4 - y;
			}
			if (activeSprite != null && (hasBorder || activeSprite.packed || (activeSprite.texture != null && activeSprite.texture.wrapMode != global::UnityEngine.TextureWrapMode.Repeat)))
			{
				long num5 = 0L;
				long num6 = 0L;
				if (m_FillCenter)
				{
					num5 = (long)global::System.Math.Ceiling((num3 - x) / num);
					num6 = (long)global::System.Math.Ceiling((num4 - y) / num2);
					double num7 = 0.0;
					num7 = ((!hasBorder) ? ((double)(num5 * num6) * 4.0) : (((double)num5 + 2.0) * ((double)num6 + 2.0) * 4.0));
					if (num7 > 65000.0)
					{
						global::UnityEngine.Debug.LogError("Too many sprite tiles on Image \"" + base.name + "\". The tile size will be increased. To remove the limit on the number of tiles, set the Wrap mode to Repeat in the Image Import Settings", this);
						double num8 = ((!hasBorder) ? ((double)num5 / (double)num6) : (((double)num5 + 2.0) / ((double)num6 + 2.0)));
						double num9 = global::System.Math.Sqrt(16250.0 / num8);
						double num10 = num9 * num8;
						if (hasBorder)
						{
							num9 -= 2.0;
							num10 -= 2.0;
						}
						num5 = (long)global::System.Math.Floor(num9);
						num6 = (long)global::System.Math.Floor(num10);
						num = (num3 - x) / (float)num5;
						num2 = (num4 - y) / (float)num6;
					}
				}
				else if (hasBorder)
				{
					num5 = (long)global::System.Math.Ceiling((num3 - x) / num);
					num6 = (long)global::System.Math.Ceiling((num4 - y) / num2);
					if (((double)(num6 + num5) + 2.0) * 2.0 * 4.0 > 65000.0)
					{
						global::UnityEngine.Debug.LogError("Too many sprite tiles on Image \"" + base.name + "\". The tile size will be increased. To remove the limit on the number of tiles, set the Wrap mode to Repeat in the Image Import Settings", this);
						double num11 = (double)num5 / (double)num6;
						double num12 = (16250.0 - 4.0) / (2.0 * (1.0 + num11));
						double d = num12 * num11;
						num5 = (long)global::System.Math.Floor(num12);
						num6 = (long)global::System.Math.Floor(d);
						num = (num3 - x) / (float)num5;
						num2 = (num4 - y) / (float)num6;
					}
				}
				else
				{
					num6 = (num5 = 0L);
				}
				if (m_FillCenter)
				{
					for (long num13 = 0L; num13 < num6; num13++)
					{
						float num14 = y + (float)num13 * num2;
						float num15 = y + (float)(num13 + 1) * num2;
						if (num15 > num4)
						{
							uvMax.y = vector5.y + (vector6.y - vector5.y) * (num4 - num14) / (num15 - num14);
							num15 = num4;
						}
						uvMax.x = vector6.x;
						for (long num16 = 0L; num16 < num5; num16++)
						{
							float num17 = x + (float)num16 * num;
							float num18 = x + (float)(num16 + 1) * num;
							if (num18 > num3)
							{
								uvMax.x = vector5.x + (vector6.x - vector5.x) * (num3 - num17) / (num18 - num17);
								num18 = num3;
							}
							AddQuad(toFill, new global::UnityEngine.Vector2(num17, num14) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(num18, num15) + pixelAdjustedRect.position, color, vector5, uvMax);
						}
					}
				}
				if (!hasBorder)
				{
					return;
				}
				uvMax = vector6;
				for (long num19 = 0L; num19 < num6; num19++)
				{
					float num20 = y + (float)num19 * num2;
					float num21 = y + (float)(num19 + 1) * num2;
					if (num21 > num4)
					{
						uvMax.y = vector5.y + (vector6.y - vector5.y) * (num4 - num20) / (num21 - num20);
						num21 = num4;
					}
					AddQuad(toFill, new global::UnityEngine.Vector2(0f, num20) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(x, num21) + pixelAdjustedRect.position, color, new global::UnityEngine.Vector2(vector.x, vector5.y), new global::UnityEngine.Vector2(vector5.x, uvMax.y));
					AddQuad(toFill, new global::UnityEngine.Vector2(num3, num20) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(pixelAdjustedRect.width, num21) + pixelAdjustedRect.position, color, new global::UnityEngine.Vector2(vector6.x, vector5.y), new global::UnityEngine.Vector2(vector.z, uvMax.y));
				}
				uvMax = vector6;
				for (long num22 = 0L; num22 < num5; num22++)
				{
					float num23 = x + (float)num22 * num;
					float num24 = x + (float)(num22 + 1) * num;
					if (num24 > num3)
					{
						uvMax.x = vector5.x + (vector6.x - vector5.x) * (num3 - num23) / (num24 - num23);
						num24 = num3;
					}
					AddQuad(toFill, new global::UnityEngine.Vector2(num23, 0f) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(num24, y) + pixelAdjustedRect.position, color, new global::UnityEngine.Vector2(vector5.x, vector.y), new global::UnityEngine.Vector2(uvMax.x, vector5.y));
					AddQuad(toFill, new global::UnityEngine.Vector2(num23, num4) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(num24, pixelAdjustedRect.height) + pixelAdjustedRect.position, color, new global::UnityEngine.Vector2(vector5.x, vector6.y), new global::UnityEngine.Vector2(uvMax.x, vector.w));
				}
				AddQuad(toFill, new global::UnityEngine.Vector2(0f, 0f) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(x, y) + pixelAdjustedRect.position, color, new global::UnityEngine.Vector2(vector.x, vector.y), new global::UnityEngine.Vector2(vector5.x, vector5.y));
				AddQuad(toFill, new global::UnityEngine.Vector2(num3, 0f) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(pixelAdjustedRect.width, y) + pixelAdjustedRect.position, color, new global::UnityEngine.Vector2(vector6.x, vector.y), new global::UnityEngine.Vector2(vector.z, vector5.y));
				AddQuad(toFill, new global::UnityEngine.Vector2(0f, num4) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(x, pixelAdjustedRect.height) + pixelAdjustedRect.position, color, new global::UnityEngine.Vector2(vector.x, vector6.y), new global::UnityEngine.Vector2(vector5.x, vector.w));
				AddQuad(toFill, new global::UnityEngine.Vector2(num3, num4) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(pixelAdjustedRect.width, pixelAdjustedRect.height) + pixelAdjustedRect.position, color, new global::UnityEngine.Vector2(vector6.x, vector6.y), new global::UnityEngine.Vector2(vector.z, vector.w));
			}
			else
			{
				global::UnityEngine.Vector2 b = new global::UnityEngine.Vector2((num3 - x) / num, (num4 - y) / num2);
				if (m_FillCenter)
				{
					AddQuad(toFill, new global::UnityEngine.Vector2(x, y) + pixelAdjustedRect.position, new global::UnityEngine.Vector2(num3, num4) + pixelAdjustedRect.position, color, global::UnityEngine.Vector2.Scale(vector5, b), global::UnityEngine.Vector2.Scale(vector6, b));
				}
			}
		}

		private static void AddQuad(global::UnityEngine.UI.VertexHelper vertexHelper, global::UnityEngine.Vector3[] quadPositions, global::UnityEngine.Color32 color, global::UnityEngine.Vector3[] quadUVs)
		{
			int currentVertCount = vertexHelper.currentVertCount;
			for (int i = 0; i < 4; i++)
			{
				vertexHelper.AddVert(quadPositions[i], color, quadUVs[i]);
			}
			vertexHelper.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vertexHelper.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		private static void AddQuad(global::UnityEngine.UI.VertexHelper vertexHelper, global::UnityEngine.Vector2 posMin, global::UnityEngine.Vector2 posMax, global::UnityEngine.Color32 color, global::UnityEngine.Vector2 uvMin, global::UnityEngine.Vector2 uvMax)
		{
			int currentVertCount = vertexHelper.currentVertCount;
			vertexHelper.AddVert(new global::UnityEngine.Vector3(posMin.x, posMin.y, 0f), color, new global::UnityEngine.Vector2(uvMin.x, uvMin.y));
			vertexHelper.AddVert(new global::UnityEngine.Vector3(posMin.x, posMax.y, 0f), color, new global::UnityEngine.Vector2(uvMin.x, uvMax.y));
			vertexHelper.AddVert(new global::UnityEngine.Vector3(posMax.x, posMax.y, 0f), color, new global::UnityEngine.Vector2(uvMax.x, uvMax.y));
			vertexHelper.AddVert(new global::UnityEngine.Vector3(posMax.x, posMin.y, 0f), color, new global::UnityEngine.Vector2(uvMax.x, uvMin.y));
			vertexHelper.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vertexHelper.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		private global::UnityEngine.Vector4 GetAdjustedBorders(global::UnityEngine.Vector4 border, global::UnityEngine.Rect adjustedRect)
		{
			global::UnityEngine.Rect rect = base.rectTransform.rect;
			for (int i = 0; i <= 1; i++)
			{
				if (rect.size[i] != 0f)
				{
					float num = adjustedRect.size[i] / rect.size[i];
					border[i] *= num;
					border[i + 2] *= num;
				}
				float num2 = border[i] + border[i + 2];
				if (adjustedRect.size[i] < num2 && num2 != 0f)
				{
					float num = adjustedRect.size[i] / num2;
					border[i] *= num;
					border[i + 2] *= num;
				}
			}
			return border;
		}

		private void GenerateFilledSprite(global::UnityEngine.UI.VertexHelper toFill, bool preserveAspect)
		{
			toFill.Clear();
			if (m_FillAmount < 0.001f)
			{
				return;
			}
			global::UnityEngine.Vector4 drawingDimensions = GetDrawingDimensions(preserveAspect);
			global::UnityEngine.Vector4 obj = ((activeSprite != null) ? global::UnityEngine.Sprites.DataUtility.GetOuterUV(activeSprite) : global::UnityEngine.Vector4.zero);
			global::UnityEngine.UIVertex simpleVert = global::UnityEngine.UIVertex.simpleVert;
			simpleVert.color = color;
			float num = obj.x;
			float num2 = obj.y;
			float num3 = obj.z;
			float num4 = obj.w;
			if (m_FillMethod == global::UnityEngine.UI.Image.FillMethod.Horizontal || m_FillMethod == global::UnityEngine.UI.Image.FillMethod.Vertical)
			{
				if (fillMethod == global::UnityEngine.UI.Image.FillMethod.Horizontal)
				{
					float num5 = (num3 - num) * m_FillAmount;
					if (m_FillOrigin == 1)
					{
						drawingDimensions.x = drawingDimensions.z - (drawingDimensions.z - drawingDimensions.x) * m_FillAmount;
						num = num3 - num5;
					}
					else
					{
						drawingDimensions.z = drawingDimensions.x + (drawingDimensions.z - drawingDimensions.x) * m_FillAmount;
						num3 = num + num5;
					}
				}
				else if (fillMethod == global::UnityEngine.UI.Image.FillMethod.Vertical)
				{
					float num6 = (num4 - num2) * m_FillAmount;
					if (m_FillOrigin == 1)
					{
						drawingDimensions.y = drawingDimensions.w - (drawingDimensions.w - drawingDimensions.y) * m_FillAmount;
						num2 = num4 - num6;
					}
					else
					{
						drawingDimensions.w = drawingDimensions.y + (drawingDimensions.w - drawingDimensions.y) * m_FillAmount;
						num4 = num2 + num6;
					}
				}
			}
			s_Xy[0] = new global::UnityEngine.Vector2(drawingDimensions.x, drawingDimensions.y);
			s_Xy[1] = new global::UnityEngine.Vector2(drawingDimensions.x, drawingDimensions.w);
			s_Xy[2] = new global::UnityEngine.Vector2(drawingDimensions.z, drawingDimensions.w);
			s_Xy[3] = new global::UnityEngine.Vector2(drawingDimensions.z, drawingDimensions.y);
			s_Uv[0] = new global::UnityEngine.Vector2(num, num2);
			s_Uv[1] = new global::UnityEngine.Vector2(num, num4);
			s_Uv[2] = new global::UnityEngine.Vector2(num3, num4);
			s_Uv[3] = new global::UnityEngine.Vector2(num3, num2);
			if (m_FillAmount < 1f && m_FillMethod != global::UnityEngine.UI.Image.FillMethod.Horizontal && m_FillMethod != global::UnityEngine.UI.Image.FillMethod.Vertical)
			{
				if (fillMethod == global::UnityEngine.UI.Image.FillMethod.Radial90)
				{
					if (RadialCut(s_Xy, s_Uv, m_FillAmount, m_FillClockwise, m_FillOrigin))
					{
						AddQuad(toFill, s_Xy, color, s_Uv);
					}
				}
				else if (fillMethod == global::UnityEngine.UI.Image.FillMethod.Radial180)
				{
					for (int i = 0; i < 2; i++)
					{
						int num7 = ((m_FillOrigin > 1) ? 1 : 0);
						float t;
						float t2;
						float t3;
						float t4;
						if (m_FillOrigin == 0 || m_FillOrigin == 2)
						{
							t = 0f;
							t2 = 1f;
							if (i == num7)
							{
								t3 = 0f;
								t4 = 0.5f;
							}
							else
							{
								t3 = 0.5f;
								t4 = 1f;
							}
						}
						else
						{
							t3 = 0f;
							t4 = 1f;
							if (i == num7)
							{
								t = 0.5f;
								t2 = 1f;
							}
							else
							{
								t = 0f;
								t2 = 0.5f;
							}
						}
						s_Xy[0].x = global::UnityEngine.Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t3);
						s_Xy[1].x = s_Xy[0].x;
						s_Xy[2].x = global::UnityEngine.Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t4);
						s_Xy[3].x = s_Xy[2].x;
						s_Xy[0].y = global::UnityEngine.Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t);
						s_Xy[1].y = global::UnityEngine.Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t2);
						s_Xy[2].y = s_Xy[1].y;
						s_Xy[3].y = s_Xy[0].y;
						s_Uv[0].x = global::UnityEngine.Mathf.Lerp(num, num3, t3);
						s_Uv[1].x = s_Uv[0].x;
						s_Uv[2].x = global::UnityEngine.Mathf.Lerp(num, num3, t4);
						s_Uv[3].x = s_Uv[2].x;
						s_Uv[0].y = global::UnityEngine.Mathf.Lerp(num2, num4, t);
						s_Uv[1].y = global::UnityEngine.Mathf.Lerp(num2, num4, t2);
						s_Uv[2].y = s_Uv[1].y;
						s_Uv[3].y = s_Uv[0].y;
						float value = (m_FillClockwise ? (fillAmount * 2f - (float)i) : (m_FillAmount * 2f - (float)(1 - i)));
						if (RadialCut(s_Xy, s_Uv, global::UnityEngine.Mathf.Clamp01(value), m_FillClockwise, (i + m_FillOrigin + 3) % 4))
						{
							AddQuad(toFill, s_Xy, color, s_Uv);
						}
					}
				}
				else
				{
					if (fillMethod != global::UnityEngine.UI.Image.FillMethod.Radial360)
					{
						return;
					}
					for (int j = 0; j < 4; j++)
					{
						float t5;
						float t6;
						if (j < 2)
						{
							t5 = 0f;
							t6 = 0.5f;
						}
						else
						{
							t5 = 0.5f;
							t6 = 1f;
						}
						float t7;
						float t8;
						if (j == 0 || j == 3)
						{
							t7 = 0f;
							t8 = 0.5f;
						}
						else
						{
							t7 = 0.5f;
							t8 = 1f;
						}
						s_Xy[0].x = global::UnityEngine.Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t5);
						s_Xy[1].x = s_Xy[0].x;
						s_Xy[2].x = global::UnityEngine.Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t6);
						s_Xy[3].x = s_Xy[2].x;
						s_Xy[0].y = global::UnityEngine.Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t7);
						s_Xy[1].y = global::UnityEngine.Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t8);
						s_Xy[2].y = s_Xy[1].y;
						s_Xy[3].y = s_Xy[0].y;
						s_Uv[0].x = global::UnityEngine.Mathf.Lerp(num, num3, t5);
						s_Uv[1].x = s_Uv[0].x;
						s_Uv[2].x = global::UnityEngine.Mathf.Lerp(num, num3, t6);
						s_Uv[3].x = s_Uv[2].x;
						s_Uv[0].y = global::UnityEngine.Mathf.Lerp(num2, num4, t7);
						s_Uv[1].y = global::UnityEngine.Mathf.Lerp(num2, num4, t8);
						s_Uv[2].y = s_Uv[1].y;
						s_Uv[3].y = s_Uv[0].y;
						float value2 = (m_FillClockwise ? (m_FillAmount * 4f - (float)((j + m_FillOrigin) % 4)) : (m_FillAmount * 4f - (float)(3 - (j + m_FillOrigin) % 4)));
						if (RadialCut(s_Xy, s_Uv, global::UnityEngine.Mathf.Clamp01(value2), m_FillClockwise, (j + 2) % 4))
						{
							AddQuad(toFill, s_Xy, color, s_Uv);
						}
					}
				}
			}
			else
			{
				AddQuad(toFill, s_Xy, color, s_Uv);
			}
		}

		private static bool RadialCut(global::UnityEngine.Vector3[] xy, global::UnityEngine.Vector3[] uv, float fill, bool invert, int corner)
		{
			if (fill < 0.001f)
			{
				return false;
			}
			if ((corner & 1) == 1)
			{
				invert = !invert;
			}
			if (!invert && fill > 0.999f)
			{
				return true;
			}
			float num = global::UnityEngine.Mathf.Clamp01(fill);
			if (invert)
			{
				num = 1f - num;
			}
			num *= global::System.MathF.PI / 2f;
			float cos = global::UnityEngine.Mathf.Cos(num);
			float sin = global::UnityEngine.Mathf.Sin(num);
			RadialCut(xy, cos, sin, invert, corner);
			RadialCut(uv, cos, sin, invert, corner);
			return true;
		}

		private static void RadialCut(global::UnityEngine.Vector3[] xy, float cos, float sin, bool invert, int corner)
		{
			int num = (corner + 1) % 4;
			int num2 = (corner + 2) % 4;
			int num3 = (corner + 3) % 4;
			if ((corner & 1) == 1)
			{
				if (sin > cos)
				{
					cos /= sin;
					sin = 1f;
					if (invert)
					{
						xy[num].x = global::UnityEngine.Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
						xy[num2].x = xy[num].x;
					}
				}
				else if (cos > sin)
				{
					sin /= cos;
					cos = 1f;
					if (!invert)
					{
						xy[num2].y = global::UnityEngine.Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
						xy[num3].y = xy[num2].y;
					}
				}
				else
				{
					cos = 1f;
					sin = 1f;
				}
				if (!invert)
				{
					xy[num3].x = global::UnityEngine.Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
				}
				else
				{
					xy[num].y = global::UnityEngine.Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
				}
				return;
			}
			if (cos > sin)
			{
				sin /= cos;
				cos = 1f;
				if (!invert)
				{
					xy[num].y = global::UnityEngine.Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
					xy[num2].y = xy[num].y;
				}
			}
			else if (sin > cos)
			{
				cos /= sin;
				sin = 1f;
				if (invert)
				{
					xy[num2].x = global::UnityEngine.Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
					xy[num3].x = xy[num2].x;
				}
			}
			else
			{
				cos = 1f;
				sin = 1f;
			}
			if (invert)
			{
				xy[num3].y = global::UnityEngine.Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
			}
			else
			{
				xy[num].x = global::UnityEngine.Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
			}
		}

		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		public virtual void CalculateLayoutInputVertical()
		{
		}

		public virtual bool IsRaycastLocationValid(global::UnityEngine.Vector2 screenPoint, global::UnityEngine.Camera eventCamera)
		{
			if (alphaHitTestMinimumThreshold <= 0f)
			{
				return true;
			}
			if (alphaHitTestMinimumThreshold > 1f)
			{
				return false;
			}
			if (activeSprite == null)
			{
				return true;
			}
			if (!global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, screenPoint, eventCamera, out var localPoint))
			{
				return false;
			}
			global::UnityEngine.Rect rect = GetPixelAdjustedRect();
			if (m_PreserveAspect)
			{
				PreserveSpriteAspectRatio(ref rect, new global::UnityEngine.Vector2(activeSprite.texture.width, activeSprite.texture.height));
			}
			localPoint.x += base.rectTransform.pivot.x * rect.width;
			localPoint.y += base.rectTransform.pivot.y * rect.height;
			localPoint = MapCoordinate(localPoint, rect);
			float u = localPoint.x / (float)activeSprite.texture.width;
			float v = localPoint.y / (float)activeSprite.texture.height;
			try
			{
				return activeSprite.texture.GetPixelBilinear(u, v).a >= alphaHitTestMinimumThreshold;
			}
			catch (global::UnityEngine.UnityException ex)
			{
				global::UnityEngine.Debug.LogError("Using alphaHitTestMinimumThreshold greater than 0 on Image whose sprite texture cannot be read. " + ex.Message + " Also make sure to disable sprite packing for this sprite.", this);
				return true;
			}
		}

		private global::UnityEngine.Vector2 MapCoordinate(global::UnityEngine.Vector2 local, global::UnityEngine.Rect rect)
		{
			global::UnityEngine.Rect rect2 = activeSprite.rect;
			if (type == global::UnityEngine.UI.Image.Type.Simple || type == global::UnityEngine.UI.Image.Type.Filled)
			{
				return new global::UnityEngine.Vector2(rect2.position.x + local.x * rect2.width / rect.width, rect2.position.y + local.y * rect2.height / rect.height);
			}
			global::UnityEngine.Vector4 border = activeSprite.border;
			global::UnityEngine.Vector4 adjustedBorders = GetAdjustedBorders(border / pixelsPerUnit, rect);
			for (int i = 0; i < 2; i++)
			{
				if (!(local[i] <= adjustedBorders[i]))
				{
					if (rect.size[i] - local[i] <= adjustedBorders[i + 2])
					{
						local[i] -= rect.size[i] - rect2.size[i];
					}
					else if (type == global::UnityEngine.UI.Image.Type.Sliced)
					{
						float t = global::UnityEngine.Mathf.InverseLerp(adjustedBorders[i], rect.size[i] - adjustedBorders[i + 2], local[i]);
						local[i] = global::UnityEngine.Mathf.Lerp(border[i], rect2.size[i] - border[i + 2], t);
					}
					else
					{
						local[i] -= adjustedBorders[i];
						local[i] = global::UnityEngine.Mathf.Repeat(local[i], rect2.size[i] - border[i] - border[i + 2]);
						local[i] += border[i];
					}
				}
			}
			return local + rect2.position;
		}

		private static void RebuildImage(global::UnityEngine.U2D.SpriteAtlas spriteAtlas)
		{
			for (int num = m_TrackedTexturelessImages.Count - 1; num >= 0; num--)
			{
				global::UnityEngine.UI.Image image = m_TrackedTexturelessImages[num];
				if (null != image.activeSprite && spriteAtlas.CanBindTo(image.activeSprite))
				{
					image.SetAllDirty();
					m_TrackedTexturelessImages.RemoveAt(num);
				}
			}
		}

		private static void TrackImage(global::UnityEngine.UI.Image g)
		{
			if (!s_Initialized)
			{
				global::UnityEngine.U2D.SpriteAtlasManager.atlasRegistered += RebuildImage;
				s_Initialized = true;
			}
			m_TrackedTexturelessImages.Add(g);
		}

		private static void UnTrackImage(global::UnityEngine.UI.Image g)
		{
			m_TrackedTexturelessImages.Remove(g);
		}

		protected override void OnDidApplyAnimationProperties()
		{
			SetMaterialDirty();
			SetVerticesDirty();
			SetRaycastDirty();
		}
	}
}
