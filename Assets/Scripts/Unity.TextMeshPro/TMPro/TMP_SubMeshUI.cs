namespace TMPro
{
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.CanvasRenderer))]
	public class TMP_SubMeshUI : global::UnityEngine.UI.MaskableGraphic
	{
		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_FontAsset m_fontAsset;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_SpriteAsset m_spriteAsset;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Material m_material;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Material m_sharedMaterial;

		private global::UnityEngine.Material m_fallbackMaterial;

		private global::UnityEngine.Material m_fallbackSourceMaterial;

		[global::UnityEngine.SerializeField]
		private bool m_isDefaultMaterial;

		[global::UnityEngine.SerializeField]
		private float m_padding;

		private global::UnityEngine.Mesh m_mesh;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TextMeshProUGUI m_TextComponent;

		[global::System.NonSerialized]
		private bool m_isRegisteredForEvents;

		private bool m_materialDirty;

		[global::UnityEngine.SerializeField]
		private int m_materialReferenceIndex;

		private global::UnityEngine.Transform m_RootCanvasTransform;

		public global::TMPro.TMP_FontAsset fontAsset
		{
			get
			{
				return m_fontAsset;
			}
			set
			{
				m_fontAsset = value;
			}
		}

		public global::TMPro.TMP_SpriteAsset spriteAsset
		{
			get
			{
				return m_spriteAsset;
			}
			set
			{
				m_spriteAsset = value;
			}
		}

		public override global::UnityEngine.Texture mainTexture
		{
			get
			{
				if (sharedMaterial != null)
				{
					return sharedMaterial.GetTexture(global::TMPro.ShaderUtilities.ID_MainTex);
				}
				return null;
			}
		}

		public override global::UnityEngine.Material material
		{
			get
			{
				return GetMaterial(m_sharedMaterial);
			}
			set
			{
				if (!(m_sharedMaterial != null) || m_sharedMaterial.GetInstanceID() != value.GetInstanceID())
				{
					m_sharedMaterial = (m_material = value);
					m_padding = GetPaddingForMaterial();
					SetVerticesDirty();
					SetMaterialDirty();
				}
			}
		}

		public global::UnityEngine.Material sharedMaterial
		{
			get
			{
				return m_sharedMaterial;
			}
			set
			{
				SetSharedMaterial(value);
			}
		}

		public global::UnityEngine.Material fallbackMaterial
		{
			get
			{
				return m_fallbackMaterial;
			}
			set
			{
				if (!(m_fallbackMaterial == value))
				{
					if (m_fallbackMaterial != null && m_fallbackMaterial != value)
					{
						global::TMPro.TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
					}
					m_fallbackMaterial = value;
					global::TMPro.TMP_MaterialManager.AddFallbackMaterialReference(m_fallbackMaterial);
					SetSharedMaterial(m_fallbackMaterial);
				}
			}
		}

		public global::UnityEngine.Material fallbackSourceMaterial
		{
			get
			{
				return m_fallbackSourceMaterial;
			}
			set
			{
				m_fallbackSourceMaterial = value;
			}
		}

		public override global::UnityEngine.Material materialForRendering => global::TMPro.TMP_MaterialManager.GetMaterialForRendering(this, m_sharedMaterial);

		public bool isDefaultMaterial
		{
			get
			{
				return m_isDefaultMaterial;
			}
			set
			{
				m_isDefaultMaterial = value;
			}
		}

		public float padding
		{
			get
			{
				return m_padding;
			}
			set
			{
				m_padding = value;
			}
		}

		public global::UnityEngine.Mesh mesh
		{
			get
			{
				if (m_mesh == null)
				{
					m_mesh = new global::UnityEngine.Mesh();
					m_mesh.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				}
				return m_mesh;
			}
			set
			{
				m_mesh = value;
			}
		}

		public global::TMPro.TMP_Text textComponent
		{
			get
			{
				if (m_TextComponent == null)
				{
					m_TextComponent = GetComponentInParent<global::TMPro.TextMeshProUGUI>();
				}
				return m_TextComponent;
			}
		}

		public static global::TMPro.TMP_SubMeshUI AddSubTextObject(global::TMPro.TextMeshProUGUI textComponent, global::TMPro.MaterialReference materialReference)
		{
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject();
			obj.hideFlags = (global::TMPro.TMP_Settings.hideSubTextObjects ? global::UnityEngine.HideFlags.HideAndDontSave : global::UnityEngine.HideFlags.DontSave);
			obj.transform.SetParent(textComponent.transform, worldPositionStays: false);
			obj.transform.SetAsFirstSibling();
			obj.layer = textComponent.gameObject.layer;
			global::UnityEngine.RectTransform obj2 = obj.AddComponent<global::UnityEngine.RectTransform>();
			obj2.anchorMin = global::UnityEngine.Vector2.zero;
			obj2.anchorMax = global::UnityEngine.Vector2.one;
			obj2.sizeDelta = global::UnityEngine.Vector2.zero;
			obj2.pivot = textComponent.rectTransform.pivot;
			obj.AddComponent<global::UnityEngine.UI.LayoutElement>().ignoreLayout = true;
			global::TMPro.TMP_SubMeshUI tMP_SubMeshUI = obj.AddComponent<global::TMPro.TMP_SubMeshUI>();
			tMP_SubMeshUI.m_TextComponent = textComponent;
			tMP_SubMeshUI.m_materialReferenceIndex = materialReference.index;
			tMP_SubMeshUI.m_fontAsset = materialReference.fontAsset;
			tMP_SubMeshUI.m_spriteAsset = materialReference.spriteAsset;
			tMP_SubMeshUI.m_isDefaultMaterial = materialReference.isDefaultMaterial;
			tMP_SubMeshUI.SetSharedMaterial(materialReference.material);
			if (!textComponent.maskable)
			{
				tMP_SubMeshUI.maskable = false;
				tMP_SubMeshUI.RecalculateClipping();
			}
			return tMP_SubMeshUI;
		}

		protected override void OnEnable()
		{
			if (!m_isRegisteredForEvents)
			{
				m_isRegisteredForEvents = true;
			}
			if (base.hideFlags != global::UnityEngine.HideFlags.DontSave)
			{
				base.hideFlags = global::UnityEngine.HideFlags.DontSave;
			}
			m_ShouldRecalculateStencil = true;
			RecalculateClipping();
			RecalculateMasking();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (m_fallbackMaterial != null)
			{
				global::TMPro.TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
				m_fallbackMaterial = null;
			}
		}

		protected override void OnDestroy()
		{
			if (m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(m_mesh);
			}
			if (m_MaskMaterial != null)
			{
				global::TMPro.TMP_MaterialManager.ReleaseStencilMaterial(m_MaskMaterial);
			}
			if (m_fallbackMaterial != null)
			{
				global::TMPro.TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
				m_fallbackMaterial = null;
			}
			m_isRegisteredForEvents = false;
			RecalculateClipping();
			if (m_TextComponent != null)
			{
				m_TextComponent.havePropertiesChanged = true;
				m_TextComponent.SetAllDirty();
			}
		}

		protected override void OnTransformParentChanged()
		{
			if (IsActive())
			{
				m_ShouldRecalculateStencil = true;
				RecalculateClipping();
				RecalculateMasking();
			}
		}

		public override global::UnityEngine.Material GetModifiedMaterial(global::UnityEngine.Material baseMaterial)
		{
			global::UnityEngine.Material material = baseMaterial;
			if (m_ShouldRecalculateStencil)
			{
				global::UnityEngine.Transform stopAfter = global::UnityEngine.UI.MaskUtilities.FindRootSortOverrideCanvas(base.transform);
				m_StencilValue = (base.maskable ? global::UnityEngine.UI.MaskUtilities.GetStencilDepth(base.transform, stopAfter) : 0);
				m_ShouldRecalculateStencil = false;
			}
			if (m_StencilValue > 0)
			{
				global::UnityEngine.Material maskMaterial = global::UnityEngine.UI.StencilMaterial.Add(material, (1 << m_StencilValue) - 1, global::UnityEngine.Rendering.StencilOp.Keep, global::UnityEngine.Rendering.CompareFunction.Equal, global::UnityEngine.Rendering.ColorWriteMask.All, (1 << m_StencilValue) - 1, 0);
				global::UnityEngine.UI.StencilMaterial.Remove(m_MaskMaterial);
				m_MaskMaterial = maskMaterial;
				material = m_MaskMaterial;
			}
			return material;
		}

		public float GetPaddingForMaterial()
		{
			return global::TMPro.ShaderUtilities.GetPadding(m_sharedMaterial, m_TextComponent.extraPadding, m_TextComponent.isUsingBold);
		}

		public float GetPaddingForMaterial(global::UnityEngine.Material mat)
		{
			return global::TMPro.ShaderUtilities.GetPadding(mat, m_TextComponent.extraPadding, m_TextComponent.isUsingBold);
		}

		public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
		{
			m_padding = global::TMPro.ShaderUtilities.GetPadding(m_sharedMaterial, isExtraPadding, isUsingBold);
		}

		public override void SetAllDirty()
		{
		}

		public override void SetVerticesDirty()
		{
		}

		public override void SetLayoutDirty()
		{
		}

		public override void SetMaterialDirty()
		{
			m_materialDirty = true;
			UpdateMaterial();
			if (m_OnDirtyMaterialCallback != null)
			{
				m_OnDirtyMaterialCallback();
			}
		}

		public void SetPivotDirty()
		{
			if (IsActive())
			{
				base.rectTransform.pivot = m_TextComponent.rectTransform.pivot;
			}
		}

		private global::UnityEngine.Transform GetRootCanvasTransform()
		{
			if (m_RootCanvasTransform == null)
			{
				m_RootCanvasTransform = m_TextComponent.canvas.rootCanvas.transform;
			}
			return m_RootCanvasTransform;
		}

		public override void Cull(global::UnityEngine.Rect clipRect, bool validRect)
		{
		}

		protected override void UpdateGeometry()
		{
		}

		public override void Rebuild(global::UnityEngine.UI.CanvasUpdate update)
		{
			if (update == global::UnityEngine.UI.CanvasUpdate.PreRender && m_materialDirty)
			{
				UpdateMaterial();
				m_materialDirty = false;
			}
		}

		public void RefreshMaterial()
		{
			UpdateMaterial();
		}

		protected override void UpdateMaterial()
		{
			if (!(m_sharedMaterial == null))
			{
				if (m_sharedMaterial.HasProperty(global::TMPro.ShaderUtilities.ShaderTag_CullMode) && textComponent.fontSharedMaterial != null)
				{
					float value = textComponent.fontSharedMaterial.GetFloat(global::TMPro.ShaderUtilities.ShaderTag_CullMode);
					m_sharedMaterial.SetFloat(global::TMPro.ShaderUtilities.ShaderTag_CullMode, value);
				}
				base.canvasRenderer.materialCount = 1;
				base.canvasRenderer.SetMaterial(materialForRendering, 0);
			}
		}

		public override void RecalculateClipping()
		{
			base.RecalculateClipping();
		}

		private global::UnityEngine.Material GetMaterial()
		{
			return m_sharedMaterial;
		}

		private global::UnityEngine.Material GetMaterial(global::UnityEngine.Material mat)
		{
			if (m_material == null || m_material.GetInstanceID() != mat.GetInstanceID())
			{
				m_material = CreateMaterialInstance(mat);
			}
			m_sharedMaterial = m_material;
			m_padding = GetPaddingForMaterial();
			SetVerticesDirty();
			SetMaterialDirty();
			return m_sharedMaterial;
		}

		private global::UnityEngine.Material CreateMaterialInstance(global::UnityEngine.Material source)
		{
			global::UnityEngine.Material obj = new global::UnityEngine.Material(source)
			{
				shaderKeywords = source.shaderKeywords
			};
			obj.name += " (Instance)";
			return obj;
		}

		private global::UnityEngine.Material GetSharedMaterial()
		{
			return base.canvasRenderer.GetMaterial();
		}

		private void SetSharedMaterial(global::UnityEngine.Material mat)
		{
			m_sharedMaterial = mat;
			m_Material = m_sharedMaterial;
			m_padding = GetPaddingForMaterial();
			SetMaterialDirty();
		}
	}
}
