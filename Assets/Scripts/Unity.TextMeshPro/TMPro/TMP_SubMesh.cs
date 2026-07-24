namespace TMPro
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.MeshRenderer))]
	[global::UnityEngine.ExecuteAlways]
	public class TMP_SubMesh : global::UnityEngine.MonoBehaviour
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

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Renderer m_renderer;

		private global::UnityEngine.MeshFilter m_meshFilter;

		private global::UnityEngine.Mesh m_mesh;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TextMeshPro m_TextComponent;

		[global::System.NonSerialized]
		private bool m_isRegisteredForEvents;

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

		public global::UnityEngine.Material material
		{
			get
			{
				return GetMaterial(m_sharedMaterial);
			}
			set
			{
				if (m_sharedMaterial.GetInstanceID() != value.GetInstanceID())
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

		public global::UnityEngine.Renderer renderer
		{
			get
			{
				if (m_renderer == null)
				{
					m_renderer = GetComponent<global::UnityEngine.Renderer>();
				}
				return m_renderer;
			}
		}

		public global::UnityEngine.MeshFilter meshFilter
		{
			get
			{
				if (m_meshFilter == null)
				{
					m_meshFilter = GetComponent<global::UnityEngine.MeshFilter>();
					if (m_meshFilter == null)
					{
						m_meshFilter = base.gameObject.AddComponent<global::UnityEngine.MeshFilter>();
						m_meshFilter.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave | global::UnityEngine.HideFlags.HideInInspector;
					}
				}
				return m_meshFilter;
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
					m_TextComponent = GetComponentInParent<global::TMPro.TextMeshPro>();
				}
				return m_TextComponent;
			}
		}

		public static global::TMPro.TMP_SubMesh AddSubTextObject(global::TMPro.TextMeshPro textComponent, global::TMPro.MaterialReference materialReference)
		{
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject();
			obj.hideFlags = (global::TMPro.TMP_Settings.hideSubTextObjects ? global::UnityEngine.HideFlags.HideAndDontSave : global::UnityEngine.HideFlags.DontSave);
			obj.transform.SetParent(textComponent.transform, worldPositionStays: false);
			obj.transform.localPosition = global::UnityEngine.Vector3.zero;
			obj.transform.localRotation = global::UnityEngine.Quaternion.identity;
			obj.transform.localScale = global::UnityEngine.Vector3.one;
			obj.layer = textComponent.gameObject.layer;
			global::TMPro.TMP_SubMesh tMP_SubMesh = obj.AddComponent<global::TMPro.TMP_SubMesh>();
			tMP_SubMesh.m_TextComponent = textComponent;
			tMP_SubMesh.m_fontAsset = materialReference.fontAsset;
			tMP_SubMesh.m_spriteAsset = materialReference.spriteAsset;
			tMP_SubMesh.m_isDefaultMaterial = materialReference.isDefaultMaterial;
			tMP_SubMesh.SetSharedMaterial(materialReference.material);
			tMP_SubMesh.renderer.sortingLayerID = textComponent.renderer.sortingLayerID;
			tMP_SubMesh.renderer.sortingOrder = textComponent.renderer.sortingOrder;
			return tMP_SubMesh;
		}

		private void OnEnable()
		{
			if (!m_isRegisteredForEvents)
			{
				m_isRegisteredForEvents = true;
			}
			if (base.hideFlags != global::UnityEngine.HideFlags.DontSave)
			{
				base.hideFlags = global::UnityEngine.HideFlags.DontSave;
			}
			meshFilter.sharedMesh = mesh;
			if (m_sharedMaterial != null)
			{
				m_sharedMaterial.SetVector(global::TMPro.ShaderUtilities.ID_ClipRect, new global::UnityEngine.Vector4(-32767f, -32767f, 32767f, 32767f));
			}
		}

		private void OnDisable()
		{
			m_meshFilter.sharedMesh = null;
			if (m_fallbackMaterial != null)
			{
				global::TMPro.TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
				m_fallbackMaterial = null;
			}
		}

		private void OnDestroy()
		{
			if (m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(m_mesh);
			}
			if (m_fallbackMaterial != null)
			{
				global::TMPro.TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
				m_fallbackMaterial = null;
			}
			m_isRegisteredForEvents = false;
			if (m_TextComponent != null)
			{
				m_TextComponent.havePropertiesChanged = true;
				m_TextComponent.SetAllDirty();
			}
		}

		public void DestroySelf()
		{
			global::UnityEngine.Object.Destroy(base.gameObject, 1f);
		}

		private global::UnityEngine.Material GetMaterial(global::UnityEngine.Material mat)
		{
			if (m_renderer == null)
			{
				m_renderer = GetComponent<global::UnityEngine.Renderer>();
			}
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
			if (m_renderer == null)
			{
				m_renderer = GetComponent<global::UnityEngine.Renderer>();
			}
			return m_renderer.sharedMaterial;
		}

		private void SetSharedMaterial(global::UnityEngine.Material mat)
		{
			m_sharedMaterial = mat;
			m_padding = GetPaddingForMaterial();
			SetMaterialDirty();
		}

		public float GetPaddingForMaterial()
		{
			return global::TMPro.ShaderUtilities.GetPadding(m_sharedMaterial, m_TextComponent.extraPadding, m_TextComponent.isUsingBold);
		}

		public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
		{
			m_padding = global::TMPro.ShaderUtilities.GetPadding(m_sharedMaterial, isExtraPadding, isUsingBold);
		}

		public void SetVerticesDirty()
		{
		}

		public void SetMaterialDirty()
		{
			UpdateMaterial();
		}

		protected void UpdateMaterial()
		{
			if (!(renderer == null) && !(m_sharedMaterial == null))
			{
				m_renderer.sharedMaterial = m_sharedMaterial;
				if (m_sharedMaterial.HasProperty(global::TMPro.ShaderUtilities.ShaderTag_CullMode) && textComponent.fontSharedMaterial != null)
				{
					float value = textComponent.fontSharedMaterial.GetFloat(global::TMPro.ShaderUtilities.ShaderTag_CullMode);
					m_sharedMaterial.SetFloat(global::TMPro.ShaderUtilities.ShaderTag_CullMode, value);
				}
			}
		}
	}
}
