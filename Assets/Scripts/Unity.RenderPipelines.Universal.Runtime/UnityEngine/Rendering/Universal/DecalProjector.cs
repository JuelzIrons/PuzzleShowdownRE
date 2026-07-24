namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.AddComponentMenu("Rendering/URP Decal Projector")]
	public class DecalProjector : global::UnityEngine.MonoBehaviour, global::UnityEngine.ISerializationCallbackReceiver
	{
		internal delegate void DecalProjectorAction(global::UnityEngine.Rendering.Universal.DecalProjector decalProjector);

		private enum Version
		{
			Initial = 0,
			RenderingLayerMask = 1,
			Count = 2
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Material m_Material;

		[global::UnityEngine.SerializeField]
		private float m_DrawDistance = 1000f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0f, 1f)]
		private float m_FadeScale = 0.9f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0f, 180f)]
		private float m_StartAngleFade = 180f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0f, 180f)]
		private float m_EndAngleFade = 180f;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_UVScale = new global::UnityEngine.Vector2(1f, 1f);

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_UVBias = new global::UnityEngine.Vector2(0f, 0f);

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RenderingLayerMask m_RenderingLayerMask = global::UnityEngine.RenderingLayerMask.defaultRenderingLayerMask;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.DecalScaleMode m_ScaleMode;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Vector3 m_Offset = new global::UnityEngine.Vector3(0f, 0f, 0.5f);

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Vector3 m_Size = new global::UnityEngine.Vector3(1f, 1f, 1f);

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0f, 1f)]
		private float m_FadeFactor = 1f;

		private global::UnityEngine.Material m_OldMaterial;

		private float m_OldDrawDistance = 1000f;

		private float m_OldFadeScale = 0.9f;

		private float m_OldStartAngleFade = 180f;

		private float m_OldEndAngleFade = 180f;

		private global::UnityEngine.Vector2 m_OldUVScale = new global::UnityEngine.Vector2(1f, 1f);

		private global::UnityEngine.Vector2 m_OldUVBias = new global::UnityEngine.Vector2(0f, 0f);

		private global::UnityEngine.Rendering.Universal.DecalScaleMode m_OldScaleMode;

		private global::UnityEngine.Vector3 m_OldOffset = new global::UnityEngine.Vector3(0f, 0f, 0.5f);

		private global::UnityEngine.Vector3 m_OldSize = new global::UnityEngine.Vector3(1f, 1f, 1f);

		private float m_OldFadeFactor = 1f;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.DecalProjector.Version version = global::UnityEngine.Rendering.Universal.DecalProjector.Version.Count;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("This field is only kept for migration purpose. Use m_RenderingLayersMask instead. #from(6000.2)")]
		private uint m_DecalLayerMask = 1u;

		internal static global::UnityEngine.Material defaultMaterial { get; set; }

		internal static bool isSupported => global::UnityEngine.Rendering.Universal.DecalProjector.onDecalAdd != null;

		internal global::UnityEngine.Rendering.Universal.DecalEntity decalEntity { get; set; }

		public global::UnityEngine.Material material
		{
			get
			{
				return m_Material;
			}
			set
			{
				m_Material = value;
				OnValidate();
			}
		}

		public float drawDistance
		{
			get
			{
				return m_DrawDistance;
			}
			set
			{
				m_DrawDistance = global::UnityEngine.Mathf.Max(0f, value);
				OnValidate();
			}
		}

		public float fadeScale
		{
			get
			{
				return m_FadeScale;
			}
			set
			{
				m_FadeScale = global::UnityEngine.Mathf.Clamp01(value);
				OnValidate();
			}
		}

		public float startAngleFade
		{
			get
			{
				return m_StartAngleFade;
			}
			set
			{
				m_StartAngleFade = global::UnityEngine.Mathf.Clamp(value, 0f, 180f);
				OnValidate();
			}
		}

		public float endAngleFade
		{
			get
			{
				return m_EndAngleFade;
			}
			set
			{
				m_EndAngleFade = global::UnityEngine.Mathf.Clamp(value, m_StartAngleFade, 180f);
				OnValidate();
			}
		}

		public global::UnityEngine.Vector2 uvScale
		{
			get
			{
				return m_UVScale;
			}
			set
			{
				m_UVScale = value;
				OnValidate();
			}
		}

		public global::UnityEngine.Vector2 uvBias
		{
			get
			{
				return m_UVBias;
			}
			set
			{
				m_UVBias = value;
				OnValidate();
			}
		}

		public global::UnityEngine.RenderingLayerMask renderingLayerMask
		{
			get
			{
				return m_RenderingLayerMask;
			}
			set
			{
				m_RenderingLayerMask = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.DecalScaleMode scaleMode
		{
			get
			{
				return m_ScaleMode;
			}
			set
			{
				m_ScaleMode = value;
				OnValidate();
			}
		}

		public global::UnityEngine.Vector3 pivot
		{
			get
			{
				return m_Offset;
			}
			set
			{
				m_Offset = value;
				OnValidate();
			}
		}

		public global::UnityEngine.Vector3 size
		{
			get
			{
				return m_Size;
			}
			set
			{
				m_Size = value;
				OnValidate();
			}
		}

		public float fadeFactor
		{
			get
			{
				return m_FadeFactor;
			}
			set
			{
				m_FadeFactor = global::UnityEngine.Mathf.Clamp01(value);
				OnValidate();
			}
		}

		internal global::UnityEngine.Vector3 effectiveScale
		{
			get
			{
				if (m_ScaleMode != global::UnityEngine.Rendering.Universal.DecalScaleMode.InheritFromHierarchy)
				{
					return global::UnityEngine.Vector3.one;
				}
				return base.transform.lossyScale;
			}
		}

		internal global::UnityEngine.Vector3 decalSize => new global::UnityEngine.Vector3(m_Size.x, m_Size.z, m_Size.y);

		internal global::UnityEngine.Vector3 decalOffset => new global::UnityEngine.Vector3(m_Offset.x, 0f - m_Offset.z, m_Offset.y);

		internal global::UnityEngine.Vector4 uvScaleBias => new global::UnityEngine.Vector4(m_UVScale.x, m_UVScale.y, m_UVBias.x, m_UVBias.y);

		internal static event global::UnityEngine.Rendering.Universal.DecalProjector.DecalProjectorAction onDecalAdd;

		internal static event global::UnityEngine.Rendering.Universal.DecalProjector.DecalProjectorAction onDecalRemove;

		internal static event global::UnityEngine.Rendering.Universal.DecalProjector.DecalProjectorAction onDecalPropertyChange;

		internal static event global::System.Action onAllDecalPropertyChange;

		internal static event global::UnityEngine.Rendering.Universal.DecalProjector.DecalProjectorAction onDecalMaterialChange;

		private void InitMaterial()
		{
			_ = m_Material == null;
		}

		private void OnEnable()
		{
			InitMaterial();
			m_OldMaterial = m_Material;
			global::UnityEngine.Rendering.Universal.DecalProjector.onDecalAdd?.Invoke(this);
		}

		private void OnDisable()
		{
			global::UnityEngine.Rendering.Universal.DecalProjector.onDecalRemove?.Invoke(this);
		}

		internal void OnValidate()
		{
			if (base.isActiveAndEnabled)
			{
				if (m_Material != m_OldMaterial)
				{
					global::UnityEngine.Rendering.Universal.DecalProjector.onDecalMaterialChange?.Invoke(this);
					m_OldMaterial = m_Material;
				}
				else
				{
					global::UnityEngine.Rendering.Universal.DecalProjector.onDecalPropertyChange?.Invoke(this);
				}
				m_OldDrawDistance = m_DrawDistance;
				m_OldFadeScale = m_FadeScale;
				m_OldStartAngleFade = m_StartAngleFade;
				m_OldEndAngleFade = m_EndAngleFade;
				m_OldUVScale = m_UVScale;
				m_OldUVBias = m_UVBias;
				m_OldScaleMode = m_ScaleMode;
				m_OldOffset = m_Offset;
				m_OldSize = m_Size;
				m_OldFadeFactor = m_FadeFactor;
			}
		}

		private void OnDidApplyAnimationProperties()
		{
			if (m_OldMaterial != m_Material || global::UnityEngine.Mathf.Abs(m_OldDrawDistance - m_DrawDistance) > global::UnityEngine.Mathf.Epsilon || global::UnityEngine.Mathf.Abs(m_OldFadeScale - m_FadeScale) > global::UnityEngine.Mathf.Epsilon || global::UnityEngine.Mathf.Abs(m_OldStartAngleFade - m_StartAngleFade) > global::UnityEngine.Mathf.Epsilon || global::UnityEngine.Mathf.Abs(m_OldEndAngleFade - m_EndAngleFade) > global::UnityEngine.Mathf.Epsilon || m_OldUVScale != m_UVScale || m_OldUVBias != m_UVBias || m_OldScaleMode != m_ScaleMode || m_OldOffset != m_Offset || m_OldSize != m_Size || global::UnityEngine.Mathf.Abs(m_OldFadeFactor - m_FadeFactor) > global::UnityEngine.Mathf.Epsilon)
			{
				OnValidate();
			}
		}

		public bool IsValid()
		{
			if (material == null)
			{
				return false;
			}
			if (material.FindPass("DBufferProjector") != -1)
			{
				return true;
			}
			if (material.FindPass("DecalProjectorForwardEmissive") != -1)
			{
				return true;
			}
			if (material.FindPass("DecalScreenSpaceProjector") != -1)
			{
				return true;
			}
			if (material.FindPass("DecalGBufferProjector") != -1)
			{
				return true;
			}
			return false;
		}

		internal static void UpdateAllDecalProperties()
		{
			global::UnityEngine.Rendering.Universal.DecalProjector.onAllDecalPropertyChange?.Invoke();
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (version == global::UnityEngine.Rendering.Universal.DecalProjector.Version.Count)
			{
				version = global::UnityEngine.Rendering.Universal.DecalProjector.Version.RenderingLayerMask;
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (version == global::UnityEngine.Rendering.Universal.DecalProjector.Version.Count)
			{
				version = global::UnityEngine.Rendering.Universal.DecalProjector.Version.Initial;
			}
			if (version < global::UnityEngine.Rendering.Universal.DecalProjector.Version.RenderingLayerMask)
			{
				m_RenderingLayerMask = m_DecalLayerMask;
				version = global::UnityEngine.Rendering.Universal.DecalProjector.Version.RenderingLayerMask;
			}
		}
	}
}
