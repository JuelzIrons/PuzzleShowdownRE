namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.ExcludeFromPreset]
	public class UniversalRenderPipelineAsset : global::UnityEngine.Rendering.RenderPipelineAsset<global::UnityEngine.Rendering.Universal.UniversalRenderPipeline>, global::UnityEngine.ISerializationCallbackReceiver, global::UnityEngine.Rendering.IProbeVolumeEnabledRenderPipeline, global::UnityEngine.Rendering.IGPUResidentRenderPipeline, global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphEnabledRenderPipeline, global::UnityEngine.Rendering.ISTPEnabledRenderPipeline
	{
		private static class Strings
		{
			public static readonly string notURPRenderer = "GPUResidentDrawer Disabled due to some configured Universal Renderers not being UniversalRendererData.";

			public static readonly string renderingModeIncompatible = "GPUResidentDrawer Disabled due to some configured Universal Renderers not using the Forward+ or Deferred+ rendering paths.";
		}

		[global::System.Serializable]
		[global::UnityEngine.Rendering.ReloadGroup]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeTextures on GraphicsSettings. #from(2023.3)")]
		public sealed class TextureResources
		{
			[global::UnityEngine.Rendering.Reload("Textures/BlueNoise64/L/LDR_LLL1_0.png", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Texture2D blueNoise64LTex;

			[global::UnityEngine.Rendering.Reload("Textures/BayerMatrix.png", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Texture2D bayerMatrixTex;

			public bool NeedsReload()
			{
				if (!(blueNoise64LTex == null))
				{
					return bayerMatrixTex == null;
				}
				return true;
			}
		}

		private global::UnityEngine.Rendering.Universal.ScriptableRenderer[] m_Renderers = new global::UnityEngine.Rendering.Universal.ScriptableRenderer[1];

		private const int k_LastVersion = 13;

		[global::UnityEngine.SerializeField]
		private int k_AssetVersion = 13;

		[global::UnityEngine.SerializeField]
		private int k_AssetPreviousVersion = 13;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.RendererType m_RendererType = global::UnityEngine.Rendering.Universal.RendererType.UniversalRenderer;

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("Use m_RendererDataList instead. #from(2023.1)")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.ScriptableRendererData m_RendererData;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.ScriptableRendererData[] m_RendererDataList = new global::UnityEngine.Rendering.Universal.ScriptableRendererData[1];

		[global::UnityEngine.SerializeField]
		internal int m_DefaultRendererIndex;

		[global::UnityEngine.SerializeField]
		private bool m_RequireDepthTexture;

		[global::UnityEngine.SerializeField]
		private bool m_RequireOpaqueTexture;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.Downsampling m_OpaqueDownsampling = global::UnityEngine.Rendering.Universal.Downsampling._2xBilinear;

		[global::UnityEngine.SerializeField]
		private bool m_SupportsTerrainHoles = true;

		[global::UnityEngine.SerializeField]
		private bool m_SupportsHDR = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision m_HDRColorBufferPrecision;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.MsaaQuality m_MSAA = global::UnityEngine.Rendering.Universal.MsaaQuality.Disabled;

		[global::UnityEngine.SerializeField]
		private float m_RenderScale = 1f;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.UpscalingFilterSelection m_UpscalingFilter;

		[global::UnityEngine.SerializeField]
		private bool m_FsrOverrideSharpness;

		[global::UnityEngine.SerializeField]
		private float m_FsrSharpness = 0.92f;

		[global::UnityEngine.SerializeField]
		private bool m_EnableLODCrossFade = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.LODCrossFadeDitheringType m_LODCrossFadeDitheringType = global::UnityEngine.Rendering.Universal.LODCrossFadeDitheringType.BlueNoise;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShEvalMode m_ShEvalMode;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.LightProbeSystem m_LightProbeSystem;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget m_ProbeVolumeMemoryBudget = global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget.MemoryBudgetMedium;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.ProbeVolumeBlendingTextureMemoryBudget m_ProbeVolumeBlendingMemoryBudget = global::UnityEngine.Rendering.ProbeVolumeBlendingTextureMemoryBudget.MemoryBudgetMedium;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_SupportProbeVolumeStreaming")]
		private bool m_SupportProbeVolumeGPUStreaming;

		[global::UnityEngine.SerializeField]
		private bool m_SupportProbeVolumeDiskStreaming;

		[global::UnityEngine.SerializeField]
		private bool m_SupportProbeVolumeScenarios;

		[global::UnityEngine.SerializeField]
		private bool m_SupportProbeVolumeScenarioBlending;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.ProbeVolumeSHBands m_ProbeVolumeSHBands = global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL1;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.LightRenderingMode m_MainLightRenderingMode = global::UnityEngine.Rendering.Universal.LightRenderingMode.PerPixel;

		[global::UnityEngine.SerializeField]
		private bool m_MainLightShadowsSupported = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShadowResolution m_MainLightShadowmapResolution = global::UnityEngine.Rendering.Universal.ShadowResolution._2048;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.LightRenderingMode m_AdditionalLightsRenderingMode = global::UnityEngine.Rendering.Universal.LightRenderingMode.PerPixel;

		[global::UnityEngine.SerializeField]
		private int m_AdditionalLightsPerObjectLimit = 4;

		[global::UnityEngine.SerializeField]
		private bool m_AdditionalLightShadowsSupported;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShadowResolution m_AdditionalLightsShadowmapResolution = global::UnityEngine.Rendering.Universal.ShadowResolution._2048;

		[global::UnityEngine.SerializeField]
		private int m_AdditionalLightsShadowResolutionTierLow = AdditionalLightsDefaultShadowResolutionTierLow;

		[global::UnityEngine.SerializeField]
		private int m_AdditionalLightsShadowResolutionTierMedium = AdditionalLightsDefaultShadowResolutionTierMedium;

		[global::UnityEngine.SerializeField]
		private int m_AdditionalLightsShadowResolutionTierHigh = AdditionalLightsDefaultShadowResolutionTierHigh;

		[global::UnityEngine.SerializeField]
		private bool m_ReflectionProbeBlending;

		[global::UnityEngine.SerializeField]
		private bool m_ReflectionProbeBoxProjection;

		[global::UnityEngine.SerializeField]
		private bool m_ReflectionProbeAtlas = true;

		[global::UnityEngine.SerializeField]
		private float m_ShadowDistance = 50f;

		[global::UnityEngine.SerializeField]
		private int m_ShadowCascadeCount = 1;

		[global::UnityEngine.SerializeField]
		private float m_Cascade2Split = 0.25f;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_Cascade3Split = new global::UnityEngine.Vector2(0.1f, 0.3f);

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector3 m_Cascade4Split = new global::UnityEngine.Vector3(0.067f, 0.2f, 0.467f);

		[global::UnityEngine.SerializeField]
		private float m_CascadeBorder = 0.2f;

		[global::UnityEngine.SerializeField]
		private float m_ShadowDepthBias = 1f;

		[global::UnityEngine.SerializeField]
		private float m_ShadowNormalBias = 1f;

		[global::UnityEngine.SerializeField]
		private bool m_SoftShadowsSupported;

		[global::UnityEngine.SerializeField]
		private bool m_ConservativeEnclosingSphere;

		[global::UnityEngine.SerializeField]
		private int m_NumIterationsEnclosingSphere = 64;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.SoftShadowQuality m_SoftShadowQuality = global::UnityEngine.Rendering.Universal.SoftShadowQuality.Medium;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.LightCookieResolution m_AdditionalLightsCookieResolution = global::UnityEngine.Rendering.Universal.LightCookieResolution._2048;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.LightCookieFormat m_AdditionalLightsCookieFormat = global::UnityEngine.Rendering.Universal.LightCookieFormat.ColorHigh;

		[global::UnityEngine.SerializeField]
		private bool m_UseSRPBatcher = true;

		[global::UnityEngine.SerializeField]
		private bool m_SupportsDynamicBatching;

		[global::UnityEngine.SerializeField]
		private bool m_MixedLightingSupported = true;

		[global::UnityEngine.SerializeField]
		private bool m_SupportsLightCookies = true;

		[global::UnityEngine.SerializeField]
		private bool m_SupportsLightLayers;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("#from(2022.1) #breakingFrom(2023.1)", true)]
		private global::UnityEngine.Rendering.Universal.PipelineDebugLevel m_DebugLevel;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.StoreActionsOptimization m_StoreActionsOptimization;

		[global::UnityEngine.SerializeField]
		private bool m_UseAdaptivePerformance = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ColorGradingMode m_ColorGradingMode;

		[global::UnityEngine.SerializeField]
		private int m_ColorGradingLutSize = 32;

		[global::UnityEngine.SerializeField]
		private bool m_AllowPostProcessAlphaOutput;

		[global::UnityEngine.SerializeField]
		private bool m_UseFastSRGBLinearConversion;

		[global::UnityEngine.SerializeField]
		private bool m_SupportDataDrivenLensFlare = true;

		[global::UnityEngine.SerializeField]
		private bool m_SupportScreenSpaceLensFlare = true;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_MacroBatcherMode")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.GPUResidentDrawerMode m_GPUResidentDrawerMode;

		[global::UnityEngine.SerializeField]
		private float m_SmallMeshScreenPercentage;

		[global::UnityEngine.SerializeField]
		private bool m_GPUResidentDrawerEnableOcclusionCullingInCameras;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShadowQuality m_ShadowType = global::UnityEngine.Rendering.Universal.ShadowQuality.HardShadows;

		[global::UnityEngine.SerializeField]
		private bool m_LocalShadowsSupported;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShadowResolution m_LocalShadowsAtlasResolution = global::UnityEngine.Rendering.Universal.ShadowResolution._256;

		[global::UnityEngine.SerializeField]
		private int m_MaxPixelLights;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShadowResolution m_ShadowAtlasResolution = global::UnityEngine.Rendering.Universal.ShadowResolution._256;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode m_VolumeFrameworkUpdateMode;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.VolumeProfile m_VolumeProfile;

		public const int k_MinLutSize = 16;

		public const int k_MaxLutSize = 65;

		internal const int k_ShadowCascadeMinCount = 1;

		internal const int k_ShadowCascadeMaxCount = 4;

		public static readonly int AdditionalLightsDefaultShadowResolutionTierLow = 256;

		public static readonly int AdditionalLightsDefaultShadowResolutionTierMedium = 512;

		public static readonly int AdditionalLightsDefaultShadowResolutionTierHigh = 1024;

		private static string[] s_Names;

		private static int[] s_Values;

		private static global::UnityEngine.Experimental.Rendering.GraphicsFormat[][] s_LightCookieFormatList = new global::UnityEngine.Experimental.Rendering.GraphicsFormat[5][]
		{
			new global::UnityEngine.Experimental.Rendering.GraphicsFormat[1] { global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm },
			new global::UnityEngine.Experimental.Rendering.GraphicsFormat[1] { global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_UNorm },
			new global::UnityEngine.Experimental.Rendering.GraphicsFormat[4]
			{
				global::UnityEngine.Experimental.Rendering.GraphicsFormat.R5G6B5_UNormPack16,
				global::UnityEngine.Experimental.Rendering.GraphicsFormat.B5G6R5_UNormPack16,
				global::UnityEngine.Experimental.Rendering.GraphicsFormat.R5G5B5A1_UNormPack16,
				global::UnityEngine.Experimental.Rendering.GraphicsFormat.B5G5R5A1_UNormPack16
			},
			new global::UnityEngine.Experimental.Rendering.GraphicsFormat[3]
			{
				global::UnityEngine.Experimental.Rendering.GraphicsFormat.A2B10G10R10_UNormPack32,
				global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB,
				global::UnityEngine.Experimental.Rendering.GraphicsFormat.B8G8R8A8_SRGB
			},
			new global::UnityEngine.Experimental.Rendering.GraphicsFormat[1] { global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32 }
		};

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Kept for migration. #from(2023.3")]
		internal global::UnityEngine.Rendering.ProbeVolumeSceneData apvScenesData;

		private global::UnityEngine.Shader m_DefaultShader;

		[global::UnityEngine.SerializeField]
		private int m_ShaderVariantLogLevel;

		[global::System.Obsolete("This is obsolete, please use shadowCascadeCount instead. #from(2021.1)")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShadowCascadesOption m_ShadowCascades;

		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeTextures on GraphicsSettings. #from(2023.3)")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset.TextureResources m_Textures;

		global::UnityEngine.Rendering.GPUResidentDrawerSettings global::UnityEngine.Rendering.IGPUResidentRenderPipeline.gpuResidentDrawerSettings => new global::UnityEngine.Rendering.GPUResidentDrawerSettings
		{
			mode = m_GPUResidentDrawerMode,
			enableOcclusionCulling = m_GPUResidentDrawerEnableOcclusionCullingInCameras,
			supportDitheringCrossFade = m_EnableLODCrossFade,
			allowInEditMode = true,
			smallMeshScreenPercentage = m_SmallMeshScreenPercentage,
			errorShader = global::UnityEngine.Shader.Find("Hidden/Universal Render Pipeline/FallbackError"),
			loadingShader = global::UnityEngine.Shader.Find("Hidden/Universal Render Pipeline/FallbackLoading")
		};

		public global::System.ReadOnlySpan<global::UnityEngine.Rendering.Universal.ScriptableRendererData> rendererDataList => m_RendererDataList;

		public global::System.ReadOnlySpan<global::UnityEngine.Rendering.Universal.ScriptableRenderer> renderers => m_Renderers;

		public bool isImmediateModeSupported => false;

		public global::UnityEngine.Rendering.Universal.ScriptableRenderer scriptableRenderer
		{
			get
			{
				if (m_RendererDataList?.Length > m_DefaultRendererIndex && m_RendererDataList[m_DefaultRendererIndex] == null)
				{
					global::UnityEngine.Debug.LogError("Default renderer is missing from the current Pipeline Asset.", this);
					return null;
				}
				if (scriptableRendererData.isInvalidated || m_Renderers[m_DefaultRendererIndex] == null)
				{
					DestroyRenderer(ref m_Renderers[m_DefaultRendererIndex]);
					m_Renderers[m_DefaultRendererIndex] = scriptableRendererData.InternalCreateRenderer();
					if (gpuResidentDrawerMode != global::UnityEngine.Rendering.GPUResidentDrawerMode.Disabled)
					{
						global::UnityEngine.Rendering.IGPUResidentRenderPipeline.ReinitializeGPUResidentDrawer();
					}
				}
				return m_Renderers[m_DefaultRendererIndex];
			}
		}

		internal global::UnityEngine.Rendering.Universal.ScriptableRendererData scriptableRendererData
		{
			get
			{
				if (m_RendererDataList[m_DefaultRendererIndex] == null)
				{
					CreatePipeline();
				}
				return m_RendererDataList[m_DefaultRendererIndex];
			}
		}

		internal global::UnityEngine.Experimental.Rendering.GraphicsFormat additionalLightsCookieFormat
		{
			get
			{
				global::UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				global::UnityEngine.Experimental.Rendering.GraphicsFormat[] array = s_LightCookieFormatList[(int)m_AdditionalLightsCookieFormat];
				foreach (global::UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat2 in array)
				{
					if (global::UnityEngine.SystemInfo.IsFormatSupported(graphicsFormat2, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render))
					{
						graphicsFormat = graphicsFormat2;
						break;
					}
				}
				if (global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Gamma)
				{
					graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetLinearFormat(graphicsFormat);
				}
				if (graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
				{
					graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
					global::UnityEngine.Debug.LogWarning($"Additional Lights Cookie Format ({m_AdditionalLightsCookieFormat.ToString()}) is not supported by the platform. Falling back to {(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetBlockSize(graphicsFormat) * 8)}-bit format ({(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetFormatString(graphicsFormat))})");
				}
				return graphicsFormat;
			}
		}

		internal global::UnityEngine.Vector2Int additionalLightsCookieResolution => new global::UnityEngine.Vector2Int((int)m_AdditionalLightsCookieResolution, (int)m_AdditionalLightsCookieResolution);

		internal int[] rendererIndexList
		{
			get
			{
				int[] array = new int[m_RendererDataList.Length + 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i - 1;
				}
				return array;
			}
		}

		public bool supportsCameraDepthTexture
		{
			get
			{
				return m_RequireDepthTexture;
			}
			set
			{
				m_RequireDepthTexture = value;
			}
		}

		public bool supportsCameraOpaqueTexture
		{
			get
			{
				return m_RequireOpaqueTexture;
			}
			set
			{
				m_RequireOpaqueTexture = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.Downsampling opaqueDownsampling => m_OpaqueDownsampling;

		public bool supportsTerrainHoles => m_SupportsTerrainHoles;

		public global::UnityEngine.Rendering.Universal.StoreActionsOptimization storeActionsOptimization
		{
			get
			{
				return m_StoreActionsOptimization;
			}
			set
			{
				m_StoreActionsOptimization = value;
			}
		}

		public bool supportsHDR
		{
			get
			{
				return m_SupportsHDR;
			}
			set
			{
				m_SupportsHDR = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision hdrColorBufferPrecision
		{
			get
			{
				return m_HDRColorBufferPrecision;
			}
			set
			{
				m_HDRColorBufferPrecision = value;
			}
		}

		public int msaaSampleCount
		{
			get
			{
				return (int)m_MSAA;
			}
			set
			{
				m_MSAA = (global::UnityEngine.Rendering.Universal.MsaaQuality)value;
			}
		}

		public float renderScale
		{
			get
			{
				return m_RenderScale;
			}
			set
			{
				m_RenderScale = ValidateRenderScale(value);
			}
		}

		public bool enableLODCrossFade => m_EnableLODCrossFade;

		public global::UnityEngine.Rendering.Universal.LODCrossFadeDitheringType lodCrossFadeDitheringType => m_LODCrossFadeDitheringType;

		public global::UnityEngine.Rendering.Universal.UpscalingFilterSelection upscalingFilter
		{
			get
			{
				return m_UpscalingFilter;
			}
			set
			{
				m_UpscalingFilter = value;
			}
		}

		public string upscalerName => string.Empty;

		public bool fsrOverrideSharpness
		{
			get
			{
				return m_FsrOverrideSharpness;
			}
			set
			{
				m_FsrOverrideSharpness = value;
			}
		}

		public float fsrSharpness
		{
			get
			{
				return m_FsrSharpness;
			}
			set
			{
				m_FsrSharpness = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.ShEvalMode shEvalMode
		{
			get
			{
				return m_ShEvalMode;
			}
			internal set
			{
				m_ShEvalMode = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.LightProbeSystem lightProbeSystem
		{
			get
			{
				return m_LightProbeSystem;
			}
			internal set
			{
				m_LightProbeSystem = value;
			}
		}

		public global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget probeVolumeMemoryBudget
		{
			get
			{
				return m_ProbeVolumeMemoryBudget;
			}
			internal set
			{
				m_ProbeVolumeMemoryBudget = value;
			}
		}

		public global::UnityEngine.Rendering.ProbeVolumeBlendingTextureMemoryBudget probeVolumeBlendingMemoryBudget
		{
			get
			{
				return m_ProbeVolumeBlendingMemoryBudget;
			}
			internal set
			{
				m_ProbeVolumeBlendingMemoryBudget = value;
			}
		}

		[global::System.Obsolete("This is obsolete, use supportProbeVolumeGPUStreaming instead. #from(2023.3)")]
		public bool supportProbeVolumeStreaming
		{
			get
			{
				return m_SupportProbeVolumeGPUStreaming;
			}
			internal set
			{
				m_SupportProbeVolumeGPUStreaming = value;
			}
		}

		public bool supportProbeVolumeGPUStreaming
		{
			get
			{
				return m_SupportProbeVolumeGPUStreaming;
			}
			internal set
			{
				m_SupportProbeVolumeGPUStreaming = value;
			}
		}

		public bool supportProbeVolumeDiskStreaming
		{
			get
			{
				return m_SupportProbeVolumeDiskStreaming;
			}
			internal set
			{
				m_SupportProbeVolumeDiskStreaming = value;
			}
		}

		public bool supportProbeVolumeScenarios
		{
			get
			{
				return m_SupportProbeVolumeScenarios;
			}
			internal set
			{
				m_SupportProbeVolumeScenarios = value;
			}
		}

		public bool supportProbeVolumeScenarioBlending
		{
			get
			{
				return m_SupportProbeVolumeScenarioBlending;
			}
			internal set
			{
				m_SupportProbeVolumeScenarioBlending = value;
			}
		}

		public global::UnityEngine.Rendering.ProbeVolumeSHBands probeVolumeSHBands
		{
			get
			{
				return m_ProbeVolumeSHBands;
			}
			internal set
			{
				m_ProbeVolumeSHBands = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.LightRenderingMode mainLightRenderingMode
		{
			get
			{
				return m_MainLightRenderingMode;
			}
			internal set
			{
				m_MainLightRenderingMode = value;
			}
		}

		public bool supportsMainLightShadows
		{
			get
			{
				return m_MainLightShadowsSupported;
			}
			internal set
			{
				m_MainLightShadowsSupported = value;
			}
		}

		public int mainLightShadowmapResolution
		{
			get
			{
				return (int)m_MainLightShadowmapResolution;
			}
			set
			{
				m_MainLightShadowmapResolution = (global::UnityEngine.Rendering.Universal.ShadowResolution)value;
			}
		}

		public global::UnityEngine.Rendering.Universal.LightRenderingMode additionalLightsRenderingMode
		{
			get
			{
				return m_AdditionalLightsRenderingMode;
			}
			internal set
			{
				m_AdditionalLightsRenderingMode = value;
			}
		}

		public int maxAdditionalLightsCount
		{
			get
			{
				return m_AdditionalLightsPerObjectLimit;
			}
			set
			{
				m_AdditionalLightsPerObjectLimit = ValidatePerObjectLights(value);
			}
		}

		public bool supportsAdditionalLightShadows
		{
			get
			{
				return m_AdditionalLightShadowsSupported;
			}
			internal set
			{
				m_AdditionalLightShadowsSupported = value;
			}
		}

		public int additionalLightsShadowmapResolution
		{
			get
			{
				return (int)m_AdditionalLightsShadowmapResolution;
			}
			set
			{
				m_AdditionalLightsShadowmapResolution = (global::UnityEngine.Rendering.Universal.ShadowResolution)value;
			}
		}

		public int additionalLightsShadowResolutionTierLow
		{
			get
			{
				return m_AdditionalLightsShadowResolutionTierLow;
			}
			internal set
			{
				m_AdditionalLightsShadowResolutionTierLow = value;
			}
		}

		public int additionalLightsShadowResolutionTierMedium
		{
			get
			{
				return m_AdditionalLightsShadowResolutionTierMedium;
			}
			internal set
			{
				m_AdditionalLightsShadowResolutionTierMedium = value;
			}
		}

		public int additionalLightsShadowResolutionTierHigh
		{
			get
			{
				return m_AdditionalLightsShadowResolutionTierHigh;
			}
			internal set
			{
				m_AdditionalLightsShadowResolutionTierHigh = value;
			}
		}

		public bool reflectionProbeBlending
		{
			get
			{
				return m_ReflectionProbeBlending;
			}
			internal set
			{
				m_ReflectionProbeBlending = value;
			}
		}

		public bool reflectionProbeBoxProjection
		{
			get
			{
				return m_ReflectionProbeBoxProjection;
			}
			internal set
			{
				m_ReflectionProbeBoxProjection = value;
			}
		}

		public bool reflectionProbeAtlas
		{
			get
			{
				return m_ReflectionProbeAtlas;
			}
			internal set
			{
				m_ReflectionProbeAtlas = value;
			}
		}

		public float shadowDistance
		{
			get
			{
				return m_ShadowDistance;
			}
			set
			{
				m_ShadowDistance = global::UnityEngine.Mathf.Max(0f, value);
			}
		}

		public int shadowCascadeCount
		{
			get
			{
				return m_ShadowCascadeCount;
			}
			set
			{
				if (value < 1 || value > 4)
				{
					throw new global::System.ArgumentException($"Value ({value}) needs to be between {1} and {4}.");
				}
				m_ShadowCascadeCount = value;
			}
		}

		public float cascade2Split
		{
			get
			{
				return m_Cascade2Split;
			}
			set
			{
				m_Cascade2Split = value;
			}
		}

		public global::UnityEngine.Vector2 cascade3Split
		{
			get
			{
				return m_Cascade3Split;
			}
			set
			{
				m_Cascade3Split = value;
			}
		}

		public global::UnityEngine.Vector3 cascade4Split
		{
			get
			{
				return m_Cascade4Split;
			}
			set
			{
				m_Cascade4Split = value;
			}
		}

		public float cascadeBorder
		{
			get
			{
				return m_CascadeBorder;
			}
			set
			{
				m_CascadeBorder = value;
			}
		}

		public float shadowDepthBias
		{
			get
			{
				return m_ShadowDepthBias;
			}
			set
			{
				m_ShadowDepthBias = ValidateShadowBias(value);
			}
		}

		public float shadowNormalBias
		{
			get
			{
				return m_ShadowNormalBias;
			}
			set
			{
				m_ShadowNormalBias = ValidateShadowBias(value);
			}
		}

		public bool supportsSoftShadows
		{
			get
			{
				return m_SoftShadowsSupported;
			}
			internal set
			{
				m_SoftShadowsSupported = value;
			}
		}

		internal global::UnityEngine.Rendering.Universal.SoftShadowQuality softShadowQuality
		{
			get
			{
				return m_SoftShadowQuality;
			}
			set
			{
				m_SoftShadowQuality = value;
			}
		}

		public bool supportsDynamicBatching
		{
			get
			{
				return m_SupportsDynamicBatching;
			}
			set
			{
				m_SupportsDynamicBatching = value;
			}
		}

		public bool supportsMixedLighting => m_MixedLightingSupported;

		public bool supportsLightCookies => m_SupportsLightCookies;

		[global::System.Obsolete("This is obsolete, use useRenderingLayers instead. #from(2023.1) #breakingFrom(2023.1)", true)]
		public bool supportsLightLayers => m_SupportsLightLayers;

		public bool useRenderingLayers => m_SupportsLightLayers;

		public global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode volumeFrameworkUpdateMode => m_VolumeFrameworkUpdateMode;

		public global::UnityEngine.Rendering.VolumeProfile volumeProfile
		{
			get
			{
				return m_VolumeProfile;
			}
			set
			{
				m_VolumeProfile = value;
			}
		}

		[global::System.Obsolete("PipelineDebugLevel is deprecated and replaced to use the profiler. Calling debugLevel is not necessary. #from(2022.2) #breakingFrom(2023.1)", true)]
		public global::UnityEngine.Rendering.Universal.PipelineDebugLevel debugLevel => global::UnityEngine.Rendering.Universal.PipelineDebugLevel.Disabled;

		public bool useSRPBatcher
		{
			get
			{
				return m_UseSRPBatcher;
			}
			set
			{
				m_UseSRPBatcher = value;
			}
		}

		[global::System.Obsolete("This has been deprecated, please use GraphicsSettings.GetRenderPipelineSettings<RenderGraphSettings>().enableRenderCompatibilityMode instead. #from(2023.3)")]
		public bool enableRenderGraph => true;

		public global::UnityEngine.Rendering.Universal.ColorGradingMode colorGradingMode
		{
			get
			{
				return m_ColorGradingMode;
			}
			set
			{
				m_ColorGradingMode = value;
			}
		}

		public int colorGradingLutSize
		{
			get
			{
				return m_ColorGradingLutSize;
			}
			set
			{
				m_ColorGradingLutSize = global::UnityEngine.Mathf.Clamp(value, 16, 65);
			}
		}

		public bool allowPostProcessAlphaOutput => m_AllowPostProcessAlphaOutput;

		public bool useFastSRGBLinearConversion => m_UseFastSRGBLinearConversion;

		public bool supportScreenSpaceLensFlare => m_SupportScreenSpaceLensFlare;

		public bool supportDataDrivenLensFlare => m_SupportDataDrivenLensFlare;

		public bool useAdaptivePerformance
		{
			get
			{
				return m_UseAdaptivePerformance;
			}
			set
			{
				m_UseAdaptivePerformance = value;
			}
		}

		public bool conservativeEnclosingSphere
		{
			get
			{
				return m_ConservativeEnclosingSphere;
			}
			set
			{
				m_ConservativeEnclosingSphere = value;
			}
		}

		public int numIterationsEnclosingSphere
		{
			get
			{
				return m_NumIterationsEnclosingSphere;
			}
			set
			{
				m_NumIterationsEnclosingSphere = value;
			}
		}

		public override string renderPipelineShaderTag => "UniversalPipeline";

		protected override bool requiresCompatibleRenderPipelineGlobalSettings => true;

		[global::System.Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(2023.3)")]
		public override string[] renderingLayerMaskNames => global::UnityEngine.RenderingLayerMask.GetDefinedRenderingLayerNames();

		[global::System.Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(2023.3)")]
		public override string[] prefixedRenderingLayerMaskNames => global::System.Array.Empty<string>();

		[global::System.Obsolete("This is obsolete, please use renderingLayerMaskNames instead. #from(2023.1) #breakingFrom(2023.1)", true)]
		public string[] lightLayerMaskNames => new string[0];

		public global::UnityEngine.Rendering.GPUResidentDrawerMode gpuResidentDrawerMode
		{
			get
			{
				return m_GPUResidentDrawerMode;
			}
			set
			{
				if (value != m_GPUResidentDrawerMode)
				{
					m_GPUResidentDrawerMode = value;
					OnValidate();
				}
			}
		}

		public bool gpuResidentDrawerEnableOcclusionCullingInCameras
		{
			get
			{
				return m_GPUResidentDrawerEnableOcclusionCullingInCameras;
			}
			set
			{
				if (value != m_GPUResidentDrawerEnableOcclusionCullingInCameras)
				{
					m_GPUResidentDrawerEnableOcclusionCullingInCameras = value;
					OnValidate();
				}
			}
		}

		public float smallMeshScreenPercentage
		{
			get
			{
				return m_SmallMeshScreenPercentage;
			}
			set
			{
				if (!(global::System.Math.Abs(value - m_SmallMeshScreenPercentage) < float.Epsilon))
				{
					m_SmallMeshScreenPercentage = global::UnityEngine.Mathf.Clamp(value, 0f, 20f);
					OnValidate();
				}
			}
		}

		public bool supportProbeVolume => lightProbeSystem == global::UnityEngine.Rendering.Universal.LightProbeSystem.ProbeVolumes;

		public global::UnityEngine.Rendering.ProbeVolumeSHBands maxSHBands
		{
			get
			{
				if (lightProbeSystem == global::UnityEngine.Rendering.Universal.LightProbeSystem.ProbeVolumes)
				{
					return probeVolumeSHBands;
				}
				return global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL1;
			}
		}

		[global::System.Obsolete("This property is no longer necessary. #from(2023.3)")]
		public global::UnityEngine.Rendering.ProbeVolumeSceneData probeVolumeSceneData => null;

		public bool isStpUsed => m_UpscalingFilter == global::UnityEngine.Rendering.Universal.UpscalingFilterSelection.STP;

		public override global::UnityEngine.Material defaultMaterial => GetMaterial(global::UnityEngine.Rendering.Universal.DefaultMaterialType.Default);

		public override global::UnityEngine.Material defaultParticleMaterial => GetMaterial(global::UnityEngine.Rendering.Universal.DefaultMaterialType.Particle);

		public override global::UnityEngine.Material defaultLineMaterial => GetMaterial(global::UnityEngine.Rendering.Universal.DefaultMaterialType.Particle);

		public override global::UnityEngine.Material defaultTerrainMaterial => GetMaterial(global::UnityEngine.Rendering.Universal.DefaultMaterialType.Terrain);

		public override global::UnityEngine.Material default2DMaterial => GetMaterial(global::UnityEngine.Rendering.Universal.DefaultMaterialType.Sprite);

		public override global::UnityEngine.Material default2DMaskMaterial => GetMaterial(global::UnityEngine.Rendering.Universal.DefaultMaterialType.SpriteMask);

		public global::UnityEngine.Material decalMaterial => GetMaterial(global::UnityEngine.Rendering.Universal.DefaultMaterialType.Decal);

		public override global::UnityEngine.Shader defaultShader
		{
			get
			{
				if (m_DefaultShader == null)
				{
					m_DefaultShader = global::UnityEngine.Shader.Find(global::UnityEngine.Rendering.Universal.ShaderUtils.GetShaderPath(global::UnityEngine.Rendering.Universal.ShaderPathID.Lit));
				}
				return m_DefaultShader;
			}
		}

		public override global::UnityEngine.Shader terrainDetailLitShader
		{
			get
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTerrainShaders>(out var settings))
				{
					return settings.terrainDetailLitShader;
				}
				return null;
			}
		}

		public override global::UnityEngine.Shader terrainDetailGrassShader
		{
			get
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTerrainShaders>(out var settings))
				{
					return settings.terrainDetailGrassShader;
				}
				return null;
			}
		}

		public override global::UnityEngine.Shader terrainDetailGrassBillboardShader
		{
			get
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTerrainShaders>(out var settings))
				{
					return settings.terrainDetailGrassBillboardShader;
				}
				return null;
			}
		}

		[global::System.Obsolete("Use GraphicsSettings.GetRenderPipelineSettings<ShaderStrippingSetting>().shaderVariantLogLevel instead. #from(2022.2)")]
		public global::UnityEngine.Rendering.Universal.ShaderVariantLogLevel shaderVariantLogLevel
		{
			get
			{
				return (global::UnityEngine.Rendering.Universal.ShaderVariantLogLevel)global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.ShaderStrippingSetting>().shaderVariantLogLevel;
			}
			set
			{
				global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.ShaderStrippingSetting>().shaderVariantLogLevel = (global::UnityEngine.Rendering.ShaderVariantLogLevel)value;
			}
		}

		[global::System.Obsolete("This is obsolete, please use shadowCascadeCount instead. #from(2021.1) #breakingFrom(2023.1)", true)]
		public global::UnityEngine.Rendering.Universal.ShadowCascadesOption shadowCascadeOption
		{
			get
			{
				return shadowCascadeCount switch
				{
					1 => global::UnityEngine.Rendering.Universal.ShadowCascadesOption.NoCascades, 
					2 => global::UnityEngine.Rendering.Universal.ShadowCascadesOption.TwoCascades, 
					4 => global::UnityEngine.Rendering.Universal.ShadowCascadesOption.FourCascades, 
					_ => throw new global::System.InvalidOperationException("Cascade count is not compatible with obsolete API, please use shadowCascadeCount instead."), 
				};
			}
			set
			{
				switch (value)
				{
				case global::UnityEngine.Rendering.Universal.ShadowCascadesOption.NoCascades:
					shadowCascadeCount = 1;
					break;
				case global::UnityEngine.Rendering.Universal.ShadowCascadesOption.TwoCascades:
					shadowCascadeCount = 2;
					break;
				case global::UnityEngine.Rendering.Universal.ShadowCascadesOption.FourCascades:
					shadowCascadeCount = 4;
					break;
				default:
					throw new global::System.InvalidOperationException("Cascade count is not compatible with obsolete API, please use shadowCascadeCount instead.");
				}
			}
		}

		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeTextures on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset.TextureResources textures
		{
			get
			{
				if (m_Textures == null)
				{
					m_Textures = new global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset.TextureResources();
				}
				return m_Textures;
			}
		}

		[global::System.Obsolete("This property is not used. #from(6000.3)", false)]
		public global::UnityEngine.Rendering.Universal.IntermediateTextureMode intermediateTextureMode
		{
			get
			{
				return global::UnityEngine.Rendering.Universal.IntermediateTextureMode.Auto;
			}
			set
			{
			}
		}

		internal bool IsAtLastVersion()
		{
			return 13 == k_AssetVersion;
		}

		public global::UnityEngine.Rendering.Universal.ScriptableRendererData LoadBuiltinRendererData(global::UnityEngine.Rendering.Universal.RendererType type = global::UnityEngine.Rendering.Universal.RendererType.UniversalRenderer)
		{
			m_RendererDataList[0] = null;
			return m_RendererDataList[0];
		}

		protected override void EnsureGlobalSettings()
		{
			base.EnsureGlobalSettings();
		}

		protected override global::UnityEngine.Rendering.RenderPipeline CreatePipeline()
		{
			if (m_RendererDataList == null)
			{
				m_RendererDataList = new global::UnityEngine.Rendering.Universal.ScriptableRendererData[1];
			}
			if (m_DefaultRendererIndex >= m_RendererDataList.Length || m_RendererDataList[m_DefaultRendererIndex] == null)
			{
				if (k_AssetPreviousVersion != k_AssetVersion)
				{
					return null;
				}
				global::UnityEngine.Debug.LogError("Default Renderer is missing, make sure there is a Renderer assigned as the default on the current Universal RP asset:" + global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.name, this);
				return null;
			}
			DestroyRenderers();
			global::UnityEngine.Rendering.Universal.UniversalRenderPipeline result = new global::UnityEngine.Rendering.Universal.UniversalRenderPipeline(this);
			CreateRenderers();
			global::UnityEngine.Rendering.IGPUResidentRenderPipeline.ReinitializeGPUResidentDrawer();
			return result;
		}

		internal void DestroyRenderers()
		{
			if (m_Renderers != null)
			{
				for (int i = 0; i < m_Renderers.Length; i++)
				{
					DestroyRenderer(ref m_Renderers[i]);
				}
			}
		}

		private void DestroyRenderer(ref global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer)
		{
			if (renderer != null)
			{
				renderer.Dispose();
				renderer = null;
			}
		}

		protected override void OnDisable()
		{
			DestroyRenderers();
			base.OnDisable();
		}

		private void CreateRenderers()
		{
			if (m_Renderers != null)
			{
				for (int i = 0; i < m_Renderers.Length; i++)
				{
					if (m_Renderers[i] != null)
					{
						global::UnityEngine.Debug.LogError($"Creating renderers but previous instance wasn't properly destroyed: m_Renderers[{i}]");
					}
				}
			}
			if (m_Renderers == null || m_Renderers.Length != m_RendererDataList.Length)
			{
				m_Renderers = new global::UnityEngine.Rendering.Universal.ScriptableRenderer[m_RendererDataList.Length];
			}
			for (int j = 0; j < m_RendererDataList.Length; j++)
			{
				if (m_RendererDataList[j] != null)
				{
					m_Renderers[j] = m_RendererDataList[j].InternalCreateRenderer();
				}
			}
		}

		public global::UnityEngine.Rendering.Universal.ScriptableRenderer GetRenderer(int index)
		{
			if (index == -1)
			{
				index = m_DefaultRendererIndex;
			}
			if (index >= m_RendererDataList.Length || index < 0 || m_RendererDataList[index] == null)
			{
				global::UnityEngine.Debug.LogWarning("Renderer at index " + index + " is missing, falling back to Default Renderer " + m_RendererDataList[m_DefaultRendererIndex].name, this);
				index = m_DefaultRendererIndex;
			}
			if (m_Renderers == null || m_Renderers.Length < m_RendererDataList.Length)
			{
				DestroyRenderers();
				CreateRenderers();
			}
			if (m_RendererDataList[index].isInvalidated || m_Renderers[index] == null)
			{
				DestroyRenderer(ref m_Renderers[index]);
				m_Renderers[index] = m_RendererDataList[index].InternalCreateRenderer();
				if (gpuResidentDrawerMode != global::UnityEngine.Rendering.GPUResidentDrawerMode.Disabled)
				{
					global::UnityEngine.Rendering.IGPUResidentRenderPipeline.ReinitializeGPUResidentDrawer();
				}
			}
			return m_Renderers[index];
		}

		internal int GetAdditionalLightsShadowResolution(int additionalLightsShadowResolutionTier)
		{
			if (additionalLightsShadowResolutionTier <= global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierLow)
			{
				return additionalLightsShadowResolutionTierLow;
			}
			if (additionalLightsShadowResolutionTier == global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierMedium)
			{
				return additionalLightsShadowResolutionTierMedium;
			}
			if (additionalLightsShadowResolutionTier >= global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierHigh)
			{
				return additionalLightsShadowResolutionTierHigh;
			}
			return additionalLightsShadowResolutionTierMedium;
		}

		internal bool ShouldUseReflectionProbeBlending()
		{
			if (gpuResidentDrawerMode != global::UnityEngine.Rendering.GPUResidentDrawerMode.Disabled)
			{
				return true;
			}
			return reflectionProbeBlending;
		}

		internal bool ShouldUseReflectionProbeAtlasBlending(global::UnityEngine.Rendering.Universal.RenderingMode renderingMode)
		{
			bool flag = ShouldUseReflectionProbeBlending();
			if (gpuResidentDrawerMode != global::UnityEngine.Rendering.GPUResidentDrawerMode.Disabled)
			{
				return true;
			}
			if (flag)
			{
				if (!reflectionProbeAtlas)
				{
					return renderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus;
				}
				return true;
			}
			return false;
		}

		internal void OnEnableRenderGraphChanged()
		{
			OnValidate();
		}

		public bool IsGPUResidentDrawerSupportedBySRP(out string message, out global::UnityEngine.LogType severity)
		{
			message = string.Empty;
			severity = global::UnityEngine.LogType.Warning;
			global::UnityEngine.Rendering.Universal.ScriptableRendererData[] array = m_RendererDataList;
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i] is global::UnityEngine.Rendering.Universal.UniversalRendererData universalRendererData))
				{
					message = global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset.Strings.notURPRenderer;
					return false;
				}
				if (!universalRendererData.usesClusterLightLoop)
				{
					message = global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset.Strings.renderingModeIncompatible;
					return false;
				}
			}
			return true;
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			if (k_AssetVersion < 3)
			{
				m_SoftShadowsSupported = m_ShadowType == global::UnityEngine.Rendering.Universal.ShadowQuality.SoftShadows;
				k_AssetPreviousVersion = k_AssetVersion;
				k_AssetVersion = 3;
			}
			if (k_AssetVersion < 4)
			{
				m_AdditionalLightShadowsSupported = m_LocalShadowsSupported;
				m_AdditionalLightsShadowmapResolution = m_LocalShadowsAtlasResolution;
				m_AdditionalLightsPerObjectLimit = m_MaxPixelLights;
				m_MainLightShadowmapResolution = m_ShadowAtlasResolution;
				k_AssetPreviousVersion = k_AssetVersion;
				k_AssetVersion = 4;
			}
			if (k_AssetVersion < 5)
			{
				if (m_RendererType == global::UnityEngine.Rendering.Universal.RendererType.Custom)
				{
					m_RendererDataList[0] = m_RendererData;
				}
				k_AssetPreviousVersion = k_AssetVersion;
				k_AssetVersion = 5;
			}
			if (k_AssetVersion < 6)
			{
				int shadowCascades = (int)m_ShadowCascades;
				if (shadowCascades == 2)
				{
					m_ShadowCascadeCount = 4;
				}
				else
				{
					m_ShadowCascadeCount = shadowCascades + 1;
				}
				k_AssetVersion = 6;
			}
			if (k_AssetVersion < 7)
			{
				k_AssetPreviousVersion = k_AssetVersion;
				k_AssetVersion = 7;
			}
			if (k_AssetVersion < 8)
			{
				k_AssetPreviousVersion = k_AssetVersion;
				m_CascadeBorder = 0.1f;
				k_AssetVersion = 8;
			}
			if (k_AssetVersion < 9)
			{
				if (m_AdditionalLightsShadowResolutionTierHigh == AdditionalLightsDefaultShadowResolutionTierHigh && m_AdditionalLightsShadowResolutionTierMedium == AdditionalLightsDefaultShadowResolutionTierMedium && m_AdditionalLightsShadowResolutionTierLow == AdditionalLightsDefaultShadowResolutionTierLow)
				{
					m_AdditionalLightsShadowResolutionTierHigh = (int)m_AdditionalLightsShadowmapResolution;
					m_AdditionalLightsShadowResolutionTierMedium = global::UnityEngine.Mathf.Max(m_AdditionalLightsShadowResolutionTierHigh / 2, global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowMinimumResolution);
					m_AdditionalLightsShadowResolutionTierLow = global::UnityEngine.Mathf.Max(m_AdditionalLightsShadowResolutionTierMedium / 2, global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowMinimumResolution);
				}
				k_AssetPreviousVersion = k_AssetVersion;
				k_AssetVersion = 9;
			}
			if (k_AssetVersion < 10)
			{
				k_AssetPreviousVersion = k_AssetVersion;
				k_AssetVersion = 10;
			}
			if (k_AssetVersion < 11)
			{
				k_AssetPreviousVersion = k_AssetVersion;
				k_AssetVersion = 11;
			}
			if (k_AssetVersion < 12)
			{
				k_AssetPreviousVersion = k_AssetVersion;
				k_AssetVersion = 12;
			}
			if (k_AssetVersion < 13)
			{
				k_AssetPreviousVersion = k_AssetVersion;
				k_AssetVersion = 13;
			}
		}

		private float ValidateShadowBias(float value)
		{
			return global::UnityEngine.Mathf.Max(0f, global::UnityEngine.Mathf.Min(value, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxShadowBias));
		}

		private int ValidatePerObjectLights(int value)
		{
			return global::System.Math.Max(0, global::System.Math.Min(value, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxPerObjectLights));
		}

		private float ValidateRenderScale(float value)
		{
			return global::UnityEngine.Mathf.Max(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.minRenderScale, global::UnityEngine.Mathf.Min(value, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxRenderScale));
		}

		internal bool ValidateRendererDataList(bool partial = false)
		{
			int num = 0;
			for (int i = 0; i < m_RendererDataList.Length; i++)
			{
				num += ((!ValidateRendererData(i)) ? 1 : 0);
			}
			if (partial)
			{
				return num == 0;
			}
			return num != m_RendererDataList.Length;
		}

		internal bool ValidateRendererData(int index)
		{
			if (index == -1)
			{
				index = m_DefaultRendererIndex;
			}
			if (index >= m_RendererDataList.Length)
			{
				return false;
			}
			return m_RendererDataList[index] != null;
		}

		private global::UnityEngine.Material GetMaterial(global::UnityEngine.Rendering.Universal.DefaultMaterialType materialType)
		{
			return null;
		}
	}
}
