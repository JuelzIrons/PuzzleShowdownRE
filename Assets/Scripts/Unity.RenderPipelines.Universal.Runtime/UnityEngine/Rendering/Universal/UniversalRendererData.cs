namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.ReloadGroup]
	[global::UnityEngine.ExcludeFromPreset]
	public class UniversalRendererData : global::UnityEngine.Rendering.Universal.ScriptableRendererData, global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeXRResources on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Rendering.Universal.XRSystemData xrSystemData;

		public global::UnityEngine.Rendering.Universal.PostProcessData postProcessData;

		private const int k_LatestAssetVersion = 3;

		[global::UnityEngine.SerializeField]
		private int m_AssetVersion;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.LayerMask m_PrepassLayerMask = -1;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.LayerMask m_OpaqueLayerMask = -1;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.LayerMask m_TransparentLayerMask = -1;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.StencilStateData m_DefaultStencilState = new global::UnityEngine.Rendering.Universal.StencilStateData
		{
			passOperation = global::UnityEngine.Rendering.StencilOp.Replace
		};

		[global::UnityEngine.SerializeField]
		private bool m_ShadowTransparentReceive = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.RenderingMode m_RenderingMode;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.DepthPrimingMode m_DepthPrimingMode;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.CopyDepthMode m_CopyDepthMode = global::UnityEngine.Rendering.Universal.CopyDepthMode.AfterTransparents;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.DepthFormat m_DepthAttachmentFormat;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.DepthFormat m_DepthTextureFormat;

		[global::UnityEngine.SerializeField]
		private bool m_AccurateGbufferNormals;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.IntermediateTextureMode m_IntermediateTextureMode = global::UnityEngine.Rendering.Universal.IntermediateTextureMode.Always;

		[global::System.NonSerialized]
		private bool m_StripShadowsOffVariants = true;

		[global::System.NonSerialized]
		private bool m_StripAdditionalLightOffVariants = true;

		public global::UnityEngine.LayerMask prepassLayerMask
		{
			get
			{
				return m_PrepassLayerMask;
			}
			set
			{
				SetDirty();
				m_PrepassLayerMask = value;
			}
		}

		public global::UnityEngine.LayerMask opaqueLayerMask
		{
			get
			{
				return m_OpaqueLayerMask;
			}
			set
			{
				SetDirty();
				m_OpaqueLayerMask = value;
			}
		}

		public global::UnityEngine.LayerMask transparentLayerMask
		{
			get
			{
				return m_TransparentLayerMask;
			}
			set
			{
				SetDirty();
				m_TransparentLayerMask = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.StencilStateData defaultStencilState
		{
			get
			{
				return m_DefaultStencilState;
			}
			set
			{
				SetDirty();
				m_DefaultStencilState = value;
			}
		}

		public bool shadowTransparentReceive
		{
			get
			{
				return m_ShadowTransparentReceive;
			}
			set
			{
				SetDirty();
				m_ShadowTransparentReceive = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.RenderingMode renderingMode
		{
			get
			{
				return m_RenderingMode;
			}
			set
			{
				SetDirty();
				m_RenderingMode = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.DepthPrimingMode depthPrimingMode
		{
			get
			{
				return m_DepthPrimingMode;
			}
			set
			{
				SetDirty();
				m_DepthPrimingMode = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.CopyDepthMode copyDepthMode
		{
			get
			{
				return m_CopyDepthMode;
			}
			set
			{
				SetDirty();
				m_CopyDepthMode = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.DepthFormat depthAttachmentFormat
		{
			get
			{
				if (m_DepthAttachmentFormat != global::UnityEngine.Rendering.Universal.DepthFormat.Default && !global::UnityEngine.SystemInfo.IsFormatSupported((global::UnityEngine.Experimental.Rendering.GraphicsFormat)m_DepthAttachmentFormat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render))
				{
					global::UnityEngine.Debug.LogWarning("Selected Depth Attachment Format is not supported on this platform, falling back to Default");
					return global::UnityEngine.Rendering.Universal.DepthFormat.Default;
				}
				return m_DepthAttachmentFormat;
			}
			set
			{
				SetDirty();
				if (renderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.Deferred && !global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsStencilFormat((global::UnityEngine.Experimental.Rendering.GraphicsFormat)value))
				{
					global::UnityEngine.Debug.LogWarning("Depth format without stencil is not supported on Deferred renderer, falling back to Default");
					m_DepthAttachmentFormat = global::UnityEngine.Rendering.Universal.DepthFormat.Default;
				}
				else
				{
					m_DepthAttachmentFormat = value;
				}
			}
		}

		public global::UnityEngine.Rendering.Universal.DepthFormat depthTextureFormat
		{
			get
			{
				if (m_DepthTextureFormat != global::UnityEngine.Rendering.Universal.DepthFormat.Default && !global::UnityEngine.SystemInfo.IsFormatSupported((global::UnityEngine.Experimental.Rendering.GraphicsFormat)m_DepthTextureFormat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render))
				{
					global::UnityEngine.Debug.LogWarning("Selected Depth Texture Format " + m_DepthTextureFormat.ToString() + " is not supported on this platform, falling back to Default");
					return global::UnityEngine.Rendering.Universal.DepthFormat.Default;
				}
				return m_DepthTextureFormat;
			}
			set
			{
				SetDirty();
				m_DepthTextureFormat = value;
			}
		}

		public bool accurateGbufferNormals
		{
			get
			{
				return m_AccurateGbufferNormals;
			}
			set
			{
				SetDirty();
				m_AccurateGbufferNormals = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.IntermediateTextureMode intermediateTextureMode
		{
			get
			{
				return m_IntermediateTextureMode;
			}
			set
			{
				SetDirty();
				m_IntermediateTextureMode = value;
			}
		}

		public bool usesDeferredLighting
		{
			get
			{
				if (m_RenderingMode != global::UnityEngine.Rendering.Universal.RenderingMode.Deferred)
				{
					return m_RenderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus;
				}
				return true;
			}
		}

		public bool usesClusterLightLoop
		{
			get
			{
				if (m_RenderingMode != global::UnityEngine.Rendering.Universal.RenderingMode.ForwardPlus)
				{
					return m_RenderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus;
				}
				return true;
			}
		}

		internal override bool stripShadowsOffVariants
		{
			get
			{
				return m_StripShadowsOffVariants;
			}
			set
			{
				m_StripShadowsOffVariants = value;
			}
		}

		internal override bool stripAdditionalLightOffVariants
		{
			get
			{
				return m_StripAdditionalLightOffVariants;
			}
			set
			{
				m_StripAdditionalLightOffVariants = value;
			}
		}

		protected override global::UnityEngine.Rendering.Universal.ScriptableRenderer Create()
		{
			if (!global::UnityEngine.Application.isPlaying)
			{
				ReloadAllNullProperties();
			}
			return new global::UnityEngine.Rendering.Universal.UniversalRenderer(this);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			ReloadAllNullProperties();
		}

		private void ReloadAllNullProperties()
		{
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			m_AssetVersion = 3;
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (m_AssetVersion <= 1)
			{
				m_CopyDepthMode = global::UnityEngine.Rendering.Universal.CopyDepthMode.AfterOpaques;
			}
			if (m_AssetVersion <= 2)
			{
				m_PrepassLayerMask = m_OpaqueLayerMask;
			}
			m_AssetVersion = 3;
		}
	}
}
