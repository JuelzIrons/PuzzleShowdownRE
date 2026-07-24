namespace UnityEngine.Rendering.Universal
{
	public sealed class UniversalRenderer : global::UnityEngine.Rendering.Universal.ScriptableRenderer
	{
		private struct RenderPassInputSummary
		{
			internal bool requiresDepthTexture;

			internal bool requiresDepthPrepass;

			internal bool requiresNormalsTexture;

			internal bool requiresColorTexture;

			internal bool requiresMotionVectors;

			internal global::UnityEngine.Rendering.Universal.RenderPassEvent requiresDepthNormalAtEvent;

			internal global::UnityEngine.Rendering.Universal.RenderPassEvent requiresDepthTextureEarliestEvent;
		}

		private class CopyToDebugTexturePassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle src;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dest;
		}

		private readonly struct ClearCameraParams
		{
			internal readonly bool mustClearColor;

			internal readonly bool mustClearDepth;

			internal readonly global::UnityEngine.Color clearValue;

			internal ClearCameraParams(bool clearColor, bool clearDepth, global::UnityEngine.Color clearVal)
			{
				mustClearColor = clearColor;
				mustClearDepth = clearDepth;
				clearValue = clearVal;
			}
		}

		private enum OccluderPass
		{
			None = 0,
			DepthPrepass = 1,
			ForwardOpaque = 2,
			GBuffer = 3
		}

		private enum DepthCopySchedule
		{
			DuringPrepass = 0,
			AfterPrepass = 1,
			AfterGBuffer = 2,
			AfterOpaques = 3,
			AfterSkybox = 4,
			AfterTransparents = 5,
			None = 6
		}

		private enum ColorCopySchedule
		{
			AfterSkybox = 0,
			None = 1
		}

		private struct TextureCopySchedules
		{
			internal global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule depth;

			internal global::UnityEngine.Rendering.Universal.UniversalRenderer.ColorCopySchedule color;
		}

		private const int k_FinalBlitPassQueueOffset = 1;

		private const int k_AfterFinalBlitPassQueueOffset = 2;

		private global::UnityEngine.Rendering.Universal.Internal.DepthOnlyPass m_DepthPrepass;

		private global::UnityEngine.Rendering.Universal.Internal.DepthNormalOnlyPass m_DepthNormalPrepass;

		private global::UnityEngine.Rendering.Universal.MotionVectorRenderPass m_MotionVectorPass;

		private global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass m_MainLightShadowCasterPass;

		private global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass m_AdditionalLightsShadowCasterPass;

		private global::UnityEngine.Rendering.Universal.Internal.GBufferPass m_GBufferPass;

		private global::UnityEngine.Rendering.Universal.Internal.DeferredPass m_DeferredPass;

		private global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass m_RenderOpaqueForwardOnlyPass;

		private global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass m_RenderOpaqueForwardPass;

		private global::UnityEngine.Rendering.Universal.Internal.DrawObjectsWithRenderingLayersPass m_RenderOpaqueForwardWithRenderingLayersPass;

		private global::UnityEngine.Rendering.Universal.DrawSkyboxPass m_DrawSkyboxPass;

		private global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass m_CopyDepthPass;

		private global::UnityEngine.Rendering.Universal.Internal.CopyColorPass m_CopyColorPass;

		private global::UnityEngine.Rendering.Universal.TransparentSettingsPass m_TransparentSettingsPass;

		private global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass m_RenderTransparentForwardPass;

		private global::UnityEngine.Rendering.Universal.InvokeOnRenderObjectCallbackPass m_OnRenderObjectCallbackPass;

		private global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass m_FinalBlitPass;

		private global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass m_OffscreenUICoverPrepass;

		private global::UnityEngine.Rendering.Universal.CapturePass m_CapturePass;

		private global::UnityEngine.Rendering.Universal.XROcclusionMeshPass m_XROcclusionMeshPass;

		private global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass m_XRCopyDepthPass;

		private global::UnityEngine.Rendering.Universal.XRDepthMotionPass m_XRDepthMotionPass;

		private global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass m_DrawOffscreenUIPass;

		private global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass m_DrawOverlayUIPass;

		private global::UnityEngine.Rendering.Universal.Internal.CopyColorPass m_HistoryRawColorCopyPass;

		private global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass m_HistoryRawDepthCopyPass;

		private global::UnityEngine.Rendering.Universal.StencilCrossFadeRenderPass m_StencilCrossFadeRenderPass;

		private global::UnityEngine.Rendering.RTHandle m_TargetColorHandle;

		private global::UnityEngine.Rendering.RTHandle m_TargetDepthHandle;

		private global::UnityEngine.Rendering.Universal.Internal.ForwardLights m_ForwardLights;

		private global::UnityEngine.Rendering.Universal.Internal.DeferredLights m_DeferredLights;

		private global::UnityEngine.Rendering.Universal.RenderingMode m_RenderingMode;

		private global::UnityEngine.Rendering.Universal.DepthPrimingMode m_DepthPrimingMode;

		private global::UnityEngine.Rendering.Universal.CopyDepthMode m_CopyDepthMode;

		private global::UnityEngine.Rendering.Universal.DepthFormat m_CameraDepthAttachmentFormat;

		private global::UnityEngine.Rendering.Universal.DepthFormat m_CameraDepthTextureFormat;

		private global::UnityEngine.Rendering.StencilState m_DefaultStencilState;

		private global::UnityEngine.Rendering.Universal.LightCookieManager m_LightCookieManager;

		private global::UnityEngine.Rendering.Universal.IntermediateTextureMode m_IntermediateTextureMode;

		private global::UnityEngine.Material m_BlitMaterial;

		private global::UnityEngine.Material m_BlitHDRMaterial;

		private global::UnityEngine.Material m_SamplingMaterial;

		private global::UnityEngine.Material m_BlitOffscreenUICoverMaterial;

		private global::UnityEngine.Material m_StencilDeferredMaterial;

		private global::UnityEngine.Material m_ClusterDeferredMaterial;

		private global::UnityEngine.Material m_CameraMotionVecMaterial;

		private global::UnityEngine.Material m_DebugBlitMaterial = global::UnityEngine.Rendering.Blitter.GetBlitMaterial(global::UnityEngine.Rendering.TextureXR.dimension);

		private static global::UnityEngine.Rendering.RTHandle[] m_RenderGraphCameraColorHandles = new global::UnityEngine.Rendering.RTHandle[2];

		private static global::UnityEngine.Rendering.RTHandle m_RenderGraphCameraDepthHandle;

		private static int m_CurrentColorHandle = 0;

		private static global::UnityEngine.Rendering.RTHandle m_RenderGraphDebugTextureHandle;

		private static global::UnityEngine.Rendering.RTHandle m_OffscreenUIColorHandle;

		private bool m_RequiresRenderingLayer;

		private global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event m_RenderingLayersEvent;

		private global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize m_RenderingLayersMaskSize;

		private bool m_RenderingLayerProvidesRenderObjectPass;

		private bool m_RenderingLayerProvidesByDepthNormalPass;

		private string m_RenderingLayersTextureName;

		private global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass m_ColorGradingLutPassRenderGraph;

		private global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph m_PostProcessPassRenderGraph;

		private const string _CameraTargetAttachmentAName = "_CameraTargetAttachmentA";

		private const string _CameraTargetAttachmentBName = "_CameraTargetAttachmentB";

		private const string _SingleCameraTargetAttachmentName = "_CameraTargetAttachment";

		private const string _CameraDepthAttachmentName = "_CameraDepthAttachment";

		private const string _CameraColorUpscaled = "_CameraColorUpscaled";

		private const string _CameraColorAfterPostProcessingName = "_CameraColorAfterPostProcessing";

		private bool m_IssuedGPUOcclusionUnsupportedMsg;

		private static bool m_RequiresIntermediateAttachments;

		internal global::UnityEngine.Rendering.Universal.RenderingMode renderingModeRequested => m_RenderingMode;

		private bool deferredModeUnsupported
		{
			get
			{
				if (!global::UnityEngine.GL.wireframe && (base.DebugHandler == null || !base.DebugHandler.IsActiveModeUnsupportedForDeferred) && m_DeferredLights != null)
				{
					return !m_DeferredLights.IsRuntimeSupportedThisFrame();
				}
				return true;
			}
		}

		internal global::UnityEngine.Rendering.Universal.RenderingMode renderingModeActual
		{
			get
			{
				switch (renderingModeRequested)
				{
				case global::UnityEngine.Rendering.Universal.RenderingMode.Deferred:
					if (!deferredModeUnsupported)
					{
						return global::UnityEngine.Rendering.Universal.RenderingMode.Deferred;
					}
					return global::UnityEngine.Rendering.Universal.RenderingMode.Forward;
				case global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus:
					if (!deferredModeUnsupported)
					{
						return global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus;
					}
					return global::UnityEngine.Rendering.Universal.RenderingMode.ForwardPlus;
				default:
					return renderingModeRequested;
				}
			}
		}

		internal bool usesDeferredLighting
		{
			get
			{
				if (renderingModeActual != global::UnityEngine.Rendering.Universal.RenderingMode.Deferred)
				{
					return renderingModeActual == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus;
				}
				return true;
			}
		}

		internal bool usesClusterLightLoop
		{
			get
			{
				if (renderingModeActual != global::UnityEngine.Rendering.Universal.RenderingMode.ForwardPlus)
				{
					return renderingModeActual == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus;
				}
				return true;
			}
		}

		internal bool accurateGbufferNormals
		{
			get
			{
				if (m_DeferredLights == null)
				{
					return false;
				}
				return m_DeferredLights.AccurateGbufferNormals;
			}
		}

		internal bool needTransparencyPass
		{
			get
			{
				global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset;
				if ((object)asset == null || asset.useAdaptivePerformance)
				{
					return !global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipTransparentObjects;
				}
				return true;
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
				m_DepthPrimingMode = value;
			}
		}

		internal bool isPostProcessPassRenderGraphActive => m_PostProcessPassRenderGraph != null;

		internal global::UnityEngine.Rendering.Universal.Internal.DeferredLights deferredLights => m_DeferredLights;

		internal global::UnityEngine.LayerMask prepassLayerMask { get; set; }

		internal global::UnityEngine.LayerMask opaqueLayerMask { get; set; }

		internal global::UnityEngine.LayerMask transparentLayerMask { get; set; }

		internal bool shadowTransparentReceive { get; set; }

		internal global::UnityEngine.Experimental.Rendering.GraphicsFormat cameraDepthTextureFormat
		{
			get
			{
				if (m_CameraDepthTextureFormat == global::UnityEngine.Rendering.Universal.DepthFormat.Default)
				{
					return global::UnityEngine.Rendering.CoreUtils.GetDefaultDepthStencilFormat();
				}
				return (global::UnityEngine.Experimental.Rendering.GraphicsFormat)m_CameraDepthTextureFormat;
			}
		}

		internal global::UnityEngine.Experimental.Rendering.GraphicsFormat cameraDepthAttachmentFormat
		{
			get
			{
				if (m_CameraDepthAttachmentFormat == global::UnityEngine.Rendering.Universal.DepthFormat.Default)
				{
					return global::UnityEngine.Rendering.CoreUtils.GetDefaultDepthStencilFormat();
				}
				return (global::UnityEngine.Experimental.Rendering.GraphicsFormat)m_CameraDepthAttachmentFormat;
			}
		}

		internal override bool supportsNativeRenderPassRendergraphCompiler => true;

		private global::UnityEngine.Rendering.RTHandle currentRenderGraphCameraColorHandle
		{
			get
			{
				if (m_CurrentColorHandle < 0)
				{
					return null;
				}
				return m_RenderGraphCameraColorHandles[m_CurrentColorHandle];
			}
		}

		private global::UnityEngine.Rendering.RTHandle nextRenderGraphCameraColorHandle
		{
			get
			{
				if (m_CurrentColorHandle < 0)
				{
					return null;
				}
				m_CurrentColorHandle = (m_CurrentColorHandle + 1) % 2;
				return currentRenderGraphCameraColorHandle;
			}
		}

		public override bool supportsGPUOcclusion
		{
			get
			{
				bool num = global::UnityEngine.SystemInfo.graphicsDeviceVendorID != 20803;
				if (!num && !m_IssuedGPUOcclusionUnsupportedMsg)
				{
					global::UnityEngine.Debug.LogWarning("The GPU Occlusion Culling feature is currently unavailable on this device due to suspected driver issues.");
					m_IssuedGPUOcclusionUnsupportedMsg = true;
				}
				return num;
			}
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Setup(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void SetupLights(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public override int SupportedCameraStackingTypes()
		{
			switch (m_RenderingMode)
			{
			case global::UnityEngine.Rendering.Universal.RenderingMode.Forward:
			case global::UnityEngine.Rendering.Universal.RenderingMode.ForwardPlus:
				return 3;
			case global::UnityEngine.Rendering.Universal.RenderingMode.Deferred:
			case global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus:
				return 1;
			default:
				return 0;
			}
		}

		protected internal override bool SupportsMotionVectors()
		{
			return true;
		}

		protected internal override bool SupportsCameraOpaque()
		{
			return true;
		}

		protected internal override bool SupportsCameraNormals()
		{
			return true;
		}

		public UniversalRenderer(global::UnityEngine.Rendering.Universal.UniversalRendererData data)
			: base(data)
		{
			global::UnityEngine.Rendering.Universal.PlatformAutoDetect.Initialize();
			if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeXRResources>(out var settings))
			{
				global::UnityEngine.Experimental.Rendering.XRSystem.Initialize(global::UnityEngine.Rendering.Universal.XRPassUniversal.Create, settings.xrOcclusionMeshPS, settings.xrMirrorViewPS);
				m_XRDepthMotionPass = new global::UnityEngine.Rendering.Universal.XRDepthMotionPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses, settings.xrMotionVector);
			}
			if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeShaders>(out var settings2))
			{
				m_BlitMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings2.coreBlitPS);
				m_BlitHDRMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings2.blitHDROverlay);
				m_SamplingMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings2.samplingPS);
				m_BlitOffscreenUICoverMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings2.blitHDROverlay);
			}
			global::UnityEngine.Shader copyDepthShader = null;
			if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRendererResources>(out var settings3))
			{
				copyDepthShader = settings3.copyDepthPS;
				m_StencilDeferredMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings3.stencilDeferredPS);
				m_ClusterDeferredMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings3.clusterDeferred);
				m_CameraMotionVecMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings3.cameraMotionVector);
				m_StencilCrossFadeRenderPass = new global::UnityEngine.Rendering.Universal.StencilCrossFadeRenderPass(settings3.stencilDitherMaskSeedPS);
			}
			global::UnityEngine.Rendering.Universal.StencilStateData defaultStencilState = data.defaultStencilState;
			m_DefaultStencilState = global::UnityEngine.Rendering.StencilState.defaultValue;
			m_DefaultStencilState.enabled = defaultStencilState.overrideStencilState;
			m_DefaultStencilState.SetCompareFunction(defaultStencilState.stencilCompareFunction);
			m_DefaultStencilState.SetPassOperation(defaultStencilState.passOperation);
			m_DefaultStencilState.SetFailOperation(defaultStencilState.failOperation);
			m_DefaultStencilState.SetZFailOperation(defaultStencilState.zFailOperation);
			m_IntermediateTextureMode = data.intermediateTextureMode;
			prepassLayerMask = data.prepassLayerMask;
			opaqueLayerMask = data.opaqueLayerMask;
			transparentLayerMask = data.transparentLayerMask;
			shadowTransparentReceive = data.shadowTransparentReceive;
			global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset;
			if (asset != null && asset.supportsLightCookies)
			{
				global::UnityEngine.Rendering.Universal.LightCookieManager.Settings settings4 = global::UnityEngine.Rendering.Universal.LightCookieManager.Settings.Create();
				if ((bool)asset)
				{
					settings4.atlas.format = asset.additionalLightsCookieFormat;
					settings4.atlas.resolution = asset.additionalLightsCookieResolution;
				}
				m_LightCookieManager = new global::UnityEngine.Rendering.Universal.LightCookieManager(ref settings4);
			}
			base.stripShadowsOffVariants = data.stripShadowsOffVariants;
			base.stripAdditionalLightOffVariants = data.stripAdditionalLightOffVariants;
			global::UnityEngine.Rendering.Universal.Internal.ForwardLights.InitParams initParams = default(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.InitParams);
			initParams.lightCookieManager = m_LightCookieManager;
			initParams.forwardPlus = data.renderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus || data.renderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.ForwardPlus;
			m_ForwardLights = new global::UnityEngine.Rendering.Universal.Internal.ForwardLights(initParams);
			m_RenderingMode = data.renderingMode;
			m_DepthPrimingMode = data.depthPrimingMode;
			m_CopyDepthMode = data.copyDepthMode;
			m_CameraDepthAttachmentFormat = data.depthAttachmentFormat;
			m_CameraDepthTextureFormat = data.depthTextureFormat;
			useRenderPassEnabled = data.useNativeRenderPass;
			m_MainLightShadowCasterPass = new global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingShadows);
			m_AdditionalLightsShadowCasterPass = new global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingShadows);
			m_XROcclusionMeshPass = new global::UnityEngine.Rendering.Universal.XROcclusionMeshPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques);
			m_XRCopyDepthPass = new global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass((global::UnityEngine.Rendering.Universal.RenderPassEvent)1002, copyDepthShader);
			m_DepthPrepass = new global::UnityEngine.Rendering.Universal.Internal.DepthOnlyPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses, global::UnityEngine.Rendering.RenderQueueRange.opaque, prepassLayerMask);
			m_DepthNormalPrepass = new global::UnityEngine.Rendering.Universal.Internal.DepthNormalOnlyPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses, global::UnityEngine.Rendering.RenderQueueRange.opaque, prepassLayerMask);
			if (renderingModeRequested == global::UnityEngine.Rendering.Universal.RenderingMode.Deferred || renderingModeRequested == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus)
			{
				m_DeferredLights = new global::UnityEngine.Rendering.Universal.Internal.DeferredLights(new global::UnityEngine.Rendering.Universal.Internal.DeferredLights.InitParams
				{
					stencilDeferredMaterial = m_StencilDeferredMaterial,
					clusterDeferredMaterial = m_ClusterDeferredMaterial,
					lightCookieManager = m_LightCookieManager,
					deferredPlus = (renderingModeRequested == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus)
				}, useRenderPassEnabled);
				m_DeferredLights.AccurateGbufferNormals = data.accurateGbufferNormals;
				m_GBufferPass = new global::UnityEngine.Rendering.Universal.Internal.GBufferPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingGbuffer, global::UnityEngine.Rendering.RenderQueueRange.opaque, data.opaqueLayerMask, m_DefaultStencilState, defaultStencilState.stencilReference, m_DeferredLights);
				global::UnityEngine.Rendering.StencilState stencilState = global::UnityEngine.Rendering.Universal.Internal.DeferredLights.OverwriteStencil(m_DefaultStencilState, 96);
				global::UnityEngine.Rendering.ShaderTagId[] shaderTagIds = new global::UnityEngine.Rendering.ShaderTagId[3]
				{
					new global::UnityEngine.Rendering.ShaderTagId("UniversalForwardOnly"),
					new global::UnityEngine.Rendering.ShaderTagId("SRPDefaultUnlit"),
					new global::UnityEngine.Rendering.ShaderTagId("LightweightForward")
				};
				int stencilReference = defaultStencilState.stencilReference | 0;
				m_DeferredPass = new global::UnityEngine.Rendering.Universal.Internal.DeferredPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingDeferredLights, m_DeferredLights);
				m_RenderOpaqueForwardOnlyPass = new global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass("Draw Opaques Forward Only", shaderTagIds, opaque: true, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques, global::UnityEngine.Rendering.RenderQueueRange.opaque, data.opaqueLayerMask, stencilState, stencilReference);
			}
			m_RenderOpaqueForwardPass = new global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass(global::UnityEngine.Rendering.Universal.URPProfileId.DrawOpaqueObjects, opaque: true, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques, global::UnityEngine.Rendering.RenderQueueRange.opaque, data.opaqueLayerMask, m_DefaultStencilState, defaultStencilState.stencilReference);
			m_RenderOpaqueForwardWithRenderingLayersPass = new global::UnityEngine.Rendering.Universal.Internal.DrawObjectsWithRenderingLayersPass(global::UnityEngine.Rendering.Universal.URPProfileId.DrawOpaqueObjects, opaque: true, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques, global::UnityEngine.Rendering.RenderQueueRange.opaque, data.opaqueLayerMask, m_DefaultStencilState, defaultStencilState.stencilReference);
			bool flag = m_CopyDepthMode == global::UnityEngine.Rendering.Universal.CopyDepthMode.AfterTransparents;
			global::UnityEngine.Rendering.Universal.RenderPassEvent renderPassEvent = (flag ? global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingTransparents : global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingSkybox);
			m_CopyDepthPass = new global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass(renderPassEvent, copyDepthShader, shouldClear: true, copyToDepth: false, global::UnityEngine.Rendering.Universal.RenderingUtils.MultisampleDepthResolveSupported() && flag);
			m_MotionVectorPass = new global::UnityEngine.Rendering.Universal.MotionVectorRenderPass(renderPassEvent + 1, m_CameraMotionVecMaterial, data.opaqueLayerMask);
			m_DrawSkyboxPass = new global::UnityEngine.Rendering.Universal.DrawSkyboxPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingSkybox);
			m_CopyColorPass = new global::UnityEngine.Rendering.Universal.Internal.CopyColorPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingSkybox, m_SamplingMaterial, m_BlitMaterial);
			if (needTransparencyPass)
			{
				m_TransparentSettingsPass = new global::UnityEngine.Rendering.Universal.TransparentSettingsPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingTransparents, data.shadowTransparentReceive);
				m_RenderTransparentForwardPass = new global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass(global::UnityEngine.Rendering.Universal.URPProfileId.DrawTransparentObjects, opaque: false, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingTransparents, global::UnityEngine.Rendering.RenderQueueRange.transparent, data.transparentLayerMask, m_DefaultStencilState, defaultStencilState.stencilReference);
			}
			m_OnRenderObjectCallbackPass = new global::UnityEngine.Rendering.Universal.InvokeOnRenderObjectCallbackPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing);
			m_HistoryRawColorCopyPass = new global::UnityEngine.Rendering.Universal.Internal.CopyColorPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing, m_SamplingMaterial, m_BlitMaterial, "Copy Color Raw History");
			m_HistoryRawDepthCopyPass = new global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing, copyDepthShader, shouldClear: false, global::UnityEngine.Rendering.Universal.RenderingUtils.MultisampleDepthResolveSupported(), copyResolvedDepth: false, "Copy Depth Raw History");
			m_DrawOffscreenUIPass = new global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing, renderOffscreen: true);
			m_DrawOverlayUIPass = new global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass((global::UnityEngine.Rendering.Universal.RenderPassEvent)1002, renderOffscreen: false);
			if (data.postProcessData != null)
			{
				global::UnityEngine.Experimental.Rendering.GraphicsFormat requestPostProColorFormat = ((asset == null) ? global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32 : global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.MakeRenderTextureGraphicsFormat(asset.supportsHDR, asset.hdrColorBufferPrecision, needsAlpha: false));
				m_PostProcessPassRenderGraph = new global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph(data.postProcessData, requestPostProColorFormat);
				m_ColorGradingLutPassRenderGraph = new global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses, data.postProcessData);
			}
			m_CapturePass = new global::UnityEngine.Rendering.Universal.CapturePass(global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRendering);
			m_FinalBlitPass = new global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass((global::UnityEngine.Rendering.Universal.RenderPassEvent)1001, m_BlitMaterial, m_BlitHDRMaterial);
			m_OffscreenUICoverPrepass = new global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass((global::UnityEngine.Rendering.Universal.RenderPassEvent)551, m_BlitMaterial, m_BlitOffscreenUICoverMaterial);
			base.supportedRenderingFeatures = new global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderingFeatures();
			if (renderingModeRequested == global::UnityEngine.Rendering.Universal.RenderingMode.Deferred || renderingModeRequested == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus)
			{
				base.supportedRenderingFeatures.msaa = false;
			}
			global::UnityEngine.Rendering.LensFlareCommonSRP.mergeNeeded = 0;
			global::UnityEngine.Rendering.LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample = 1;
			global::UnityEngine.Rendering.LensFlareCommonSRP.Initialize();
		}

		protected override void Dispose(bool disposing)
		{
			m_ForwardLights.Cleanup();
			m_GBufferPass?.Dispose();
			m_FinalBlitPass?.Dispose();
			m_OffscreenUICoverPrepass?.Dispose();
			m_DrawOffscreenUIPass?.Dispose();
			m_DrawOverlayUIPass?.Dispose();
			m_CopyDepthPass?.Dispose();
			m_HistoryRawDepthCopyPass?.Dispose();
			m_XRCopyDepthPass?.Dispose();
			m_XRDepthMotionPass?.Dispose();
			m_StencilCrossFadeRenderPass?.Dispose();
			m_PostProcessPassRenderGraph?.Cleanup();
			m_ColorGradingLutPassRenderGraph?.Cleanup();
			m_TargetColorHandle?.Release();
			m_TargetDepthHandle?.Release();
			ReleaseRenderTargets();
			base.Dispose(disposing);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_BlitMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_BlitHDRMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_BlitOffscreenUICoverMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_SamplingMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_StencilDeferredMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_ClusterDeferredMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_CameraMotionVecMaterial);
			CleanupRenderGraphResources();
			global::UnityEngine.Rendering.LensFlareCommonSRP.Dispose();
			global::UnityEngine.Experimental.Rendering.XRSystem.Dispose();
		}

		internal override void ReleaseRenderTargets()
		{
			if (m_DeferredLights != null && !m_DeferredLights.UseFramebufferFetch)
			{
				m_GBufferPass?.Dispose();
			}
			m_MainLightShadowCasterPass?.Dispose();
			m_AdditionalLightsShadowCasterPass?.Dispose();
			hasReleasedRTs = true;
		}

		public static bool IsOffscreenDepthTexture(ref global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			return IsOffscreenDepthTexture(cameraData.universalCameraData);
		}

		public static bool IsOffscreenDepthTexture(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (cameraData.targetTexture != null)
			{
				return cameraData.targetTexture.format == global::UnityEngine.RenderTextureFormat.Depth;
			}
			return false;
		}

		private static bool IsWebGL()
		{
			return false;
		}

		private static bool IsGLESDevice()
		{
			return global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3;
		}

		private static bool IsGLDevice()
		{
			if (!IsGLESDevice())
			{
				return global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore;
			}
			return true;
		}

		private static bool HasActiveRenderFeatures(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRendererFeature> rendererFeatures)
		{
			if (rendererFeatures.Count == 0)
			{
				return false;
			}
			foreach (global::UnityEngine.Rendering.Universal.ScriptableRendererFeature rendererFeature in rendererFeatures)
			{
				if (rendererFeature.isActive)
				{
					return true;
				}
			}
			return false;
		}

		private static bool HasPassesRequiringIntermediateTexture(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRenderPass> activeRenderPassQueue)
		{
			if (activeRenderPassQueue.Count == 0)
			{
				return false;
			}
			foreach (global::UnityEngine.Rendering.Universal.ScriptableRenderPass item in activeRenderPassQueue)
			{
				if (item.requiresIntermediateTexture)
				{
					return true;
				}
			}
			return false;
		}

		private static void SetupVFXCameraBuffer(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (cameraData != null && cameraData.historyManager != null)
			{
				global::UnityEngine.VFX.VFXCameraBufferTypes vFXCameraBufferTypes = global::UnityEngine.VFX.VFXManager.IsCameraBufferNeeded(cameraData.camera);
				if (vFXCameraBufferTypes.HasFlag(global::UnityEngine.VFX.VFXCameraBufferTypes.Color))
				{
					cameraData.historyManager.RequestAccess<global::UnityEngine.Rendering.Universal.RawColorHistory>();
					global::UnityEngine.Rendering.RTHandle rTHandle = cameraData.historyManager.GetHistoryForRead<global::UnityEngine.Rendering.Universal.RawColorHistory>()?.GetCurrentTexture();
					global::UnityEngine.VFX.VFXManager.SetCameraBuffer(cameraData.camera, global::UnityEngine.VFX.VFXCameraBufferTypes.Color, rTHandle, 0, 0, (int)((float)cameraData.pixelWidth * cameraData.renderScale), (int)((float)cameraData.pixelHeight * cameraData.renderScale));
				}
				if (vFXCameraBufferTypes.HasFlag(global::UnityEngine.VFX.VFXCameraBufferTypes.Depth))
				{
					cameraData.historyManager.RequestAccess<global::UnityEngine.Rendering.Universal.RawDepthHistory>();
					global::UnityEngine.Rendering.RTHandle rTHandle2 = cameraData.historyManager.GetHistoryForRead<global::UnityEngine.Rendering.Universal.RawDepthHistory>()?.GetCurrentTexture();
					global::UnityEngine.VFX.VFXManager.SetCameraBuffer(cameraData.camera, global::UnityEngine.VFX.VFXCameraBufferTypes.Depth, rTHandle2, 0, 0, (int)((float)cameraData.pixelWidth * cameraData.renderScale), (int)((float)cameraData.pixelHeight * cameraData.renderScale));
				}
			}
		}

		public override void SetupCullingParameters(ref global::UnityEngine.Rendering.ScriptableCullingParameters cullingParameters, ref global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			bool flag = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.ShouldUseReflectionProbeAtlasBlending(renderingModeActual);
			if (usesClusterLightLoop && flag)
			{
				cullingParameters.cullingOptions |= global::UnityEngine.Rendering.CullingOptions.DisablePerObjectCulling;
			}
			bool num = !global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.supportsMainLightShadows && !global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.supportsAdditionalLightShadows;
			bool flag2 = global::UnityEngine.Mathf.Approximately(cameraData.maxShadowDistance, 0f);
			if (num || flag2)
			{
				cullingParameters.cullingOptions &= ~global::UnityEngine.Rendering.CullingOptions.ShadowCasters;
			}
			if (usesClusterLightLoop)
			{
				cullingParameters.maximumVisibleLights = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights;
				cullingParameters.reflectionProbeSortingCriteria = global::UnityEngine.Rendering.ReflectionProbeSortingCriteria.None;
			}
			else if (renderingModeActual == global::UnityEngine.Rendering.Universal.RenderingMode.Deferred)
			{
				cullingParameters.maximumVisibleLights = 65535;
			}
			else
			{
				cullingParameters.maximumVisibleLights = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights + 1;
			}
			cullingParameters.shadowDistance = cameraData.maxShadowDistance;
			cullingParameters.conservativeEnclosingSphere = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.conservativeEnclosingSphere;
			cullingParameters.numIterationsEnclosingSphere = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.numIterationsEnclosingSphere;
		}

		public override void FinishRendering(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
		}

		private static global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary GetRenderPassInputs(bool isTemporalAAEnabled, bool postProcessingEnabled, bool isSceneViewCamera, bool renderingLayerProvidesByDepthNormalPass, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRenderPass> activeRenderPassQueue, global::UnityEngine.Rendering.Universal.MotionVectorRenderPass motionVectorPass)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary result = new global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary
			{
				requiresDepthNormalAtEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques,
				requiresDepthTextureEarliestEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing
			};
			for (int i = 0; i < activeRenderPassQueue.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.ScriptableRenderPass scriptableRenderPass = activeRenderPassQueue[i];
				bool flag = (scriptableRenderPass.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth) != 0;
				bool flag2 = (scriptableRenderPass.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Normal) != 0;
				bool flag3 = (scriptableRenderPass.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Color) != 0;
				bool flag4 = (scriptableRenderPass.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Motion) != 0;
				bool flag5 = scriptableRenderPass.renderPassEvent < global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques;
				result.requiresDepthTexture |= flag;
				result.requiresDepthPrepass |= flag2 || (flag && flag5);
				result.requiresNormalsTexture |= flag2;
				result.requiresColorTexture |= flag3;
				result.requiresMotionVectors |= flag4;
				if (flag)
				{
					result.requiresDepthTextureEarliestEvent = (global::UnityEngine.Rendering.Universal.RenderPassEvent)global::UnityEngine.Mathf.Min((int)scriptableRenderPass.renderPassEvent, (int)result.requiresDepthTextureEarliestEvent);
				}
				if (flag2 || flag)
				{
					result.requiresDepthNormalAtEvent = (global::UnityEngine.Rendering.Universal.RenderPassEvent)global::UnityEngine.Mathf.Min((int)scriptableRenderPass.renderPassEvent, (int)result.requiresDepthNormalAtEvent);
				}
			}
			if (isTemporalAAEnabled)
			{
				result.requiresMotionVectors = true;
			}
			if (postProcessingEnabled)
			{
				global::UnityEngine.Rendering.Universal.MotionBlur component = global::UnityEngine.Rendering.VolumeManager.instance.stack.GetComponent<global::UnityEngine.Rendering.Universal.MotionBlur>();
				if (component != null && component.IsActive() && component.mode.value == global::UnityEngine.Rendering.Universal.MotionBlurMode.CameraAndObjects)
				{
					result.requiresMotionVectors = true;
				}
			}
			if (result.requiresMotionVectors)
			{
				result.requiresDepthTexture = true;
				result.requiresDepthTextureEarliestEvent = (global::UnityEngine.Rendering.Universal.RenderPassEvent)global::UnityEngine.Mathf.Min((int)motionVectorPass.renderPassEvent, (int)result.requiresDepthTextureEarliestEvent);
			}
			if (renderingLayerProvidesByDepthNormalPass)
			{
				result.requiresNormalsTexture = true;
			}
			return result;
		}

		internal static bool PlatformRequiresExplicitMsaaResolve()
		{
			if (!global::UnityEngine.SystemInfo.supportsMultisampleAutoResolve || !global::UnityEngine.Application.isMobilePlatform)
			{
				return global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.Metal;
			}
			return false;
		}

		private static bool RequiresIntermediateColorTexture(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary renderPassInputs, bool usesDeferredLighting, bool applyPostProcessing)
		{
			if (cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base && !cameraData.resolveFinalTarget)
			{
				return true;
			}
			if (usesDeferredLighting)
			{
				return true;
			}
			bool isSceneViewCamera = cameraData.isSceneViewCamera;
			global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor = cameraData.cameraTargetDescriptor;
			int msaaSamples = cameraTargetDescriptor.msaaSamples;
			bool flag = cameraData.imageScalingMode != global::UnityEngine.Rendering.Universal.ImageScalingMode.None;
			bool flag2 = IsScalableBufferManagerUsed(cameraData);
			bool flag3 = cameraTargetDescriptor.dimension == global::UnityEngine.Rendering.TextureDimension.Tex2D;
			bool flag4 = msaaSamples > 1 && PlatformRequiresExplicitMsaaResolve();
			bool num = cameraData.targetTexture != null && !isSceneViewCamera;
			bool flag5 = cameraData.captureActions != null;
			if (cameraData.xr.enabled)
			{
				flag = false;
				flag2 = false;
				flag3 = cameraData.xr.renderTargetDesc.dimension == cameraTargetDescriptor.dimension;
			}
			bool flag6 = cameraData.requiresOpaqueTexture || renderPassInputs.requiresColorTexture;
			bool flag7 = applyPostProcessing || flag6 || flag4 || !cameraData.isDefaultViewport;
			if (num)
			{
				return flag7;
			}
			if (!(flag7 || flag || flag2 || cameraData.isHdrEnabled || !flag3 || flag5))
			{
				return cameraData.requireSrgbConversion;
			}
			return true;
		}

		private static bool IsScalableBufferManagerUsed(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			bool allowDynamicResolution = cameraData.camera.allowDynamicResolution;
			bool flag = global::UnityEngine.Mathf.Abs(global::UnityEngine.ScalableBufferManager.widthScaleFactor - 1f) > 0.0001f;
			bool flag2 = global::UnityEngine.Mathf.Abs(global::UnityEngine.ScalableBufferManager.heightScaleFactor - 1f) > 0.0001f;
			if (allowDynamicResolution)
			{
				return flag || flag2;
			}
			return false;
		}

		private static bool CanCopyDepth(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			bool num = cameraData.cameraTargetDescriptor.msaaSamples > 1;
			bool flag = global::UnityEngine.SystemInfo.copyTextureSupport != global::UnityEngine.Rendering.CopyTextureSupport.None;
			bool flag2 = global::UnityEngine.Rendering.Universal.RenderingUtils.SupportsRenderTextureFormat(global::UnityEngine.RenderTextureFormat.Depth);
			bool flag3 = !num && (flag2 || flag);
			bool flag4 = num && global::UnityEngine.SystemInfo.supportsMultisampledTextures != 0;
			if (IsGLESDevice() && flag4)
			{
				return false;
			}
			return flag3 || flag4;
		}

		private bool DebugHandlerRequireDepthPass(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera) && base.DebugHandler.TryGetFullscreenDebugMode(out var _))
			{
				return true;
			}
			return false;
		}

		private void CreateDebugTexture(global::UnityEngine.RenderTextureDescriptor descriptor)
		{
			global::UnityEngine.RenderTextureDescriptor descriptor2 = descriptor;
			descriptor2.useMipMap = false;
			descriptor2.autoGenerateMips = false;
			descriptor2.bindMS = false;
			descriptor2.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_RenderGraphDebugTextureHandle, in descriptor2, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Clamp, 1, 0f, "_RenderingDebuggerTexture");
		}

		private global::UnityEngine.Rect CalculateUVRect(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, float width, float height)
		{
			float num = width / (float)cameraData.pixelWidth;
			float num2 = height / (float)cameraData.pixelHeight;
			return new global::UnityEngine.Rect(1f - num, 1f - num2, num, num2);
		}

		private global::UnityEngine.Rect CalculateUVRect(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, int textureHeightPercent)
		{
			float num = global::UnityEngine.Mathf.Clamp01((float)textureHeightPercent / 100f);
			float width = num * (float)cameraData.pixelWidth;
			float height = num * (float)cameraData.pixelHeight;
			return CalculateUVRect(cameraData, width, height);
		}

		private void CorrectForTextureAspectRatio(ref float width, ref float height, float sourceWidth, float sourceHeight)
		{
			if (sourceWidth != 0f && sourceHeight != 0f)
			{
				float num = height * sourceWidth / sourceHeight;
				if (num > width)
				{
					height = width * sourceHeight / sourceWidth;
				}
				else
				{
					width = num;
				}
			}
		}

		private void SetupRenderGraphFinalPassDebug(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(universalCameraData.isPreviewCamera))
			{
				if (base.DebugHandler.TryGetFullscreenDebugMode(out var debugFullScreenMode, out var textureHeightPercent) && (debugFullScreenMode != global::UnityEngine.Rendering.Universal.DebugFullScreenMode.ReflectionProbeAtlas || usesClusterLightLoop) && debugFullScreenMode != global::UnityEngine.Rendering.Universal.DebugFullScreenMode.STP)
				{
					float num = universalCameraData.pixelWidth;
					float num2 = universalCameraData.pixelHeight;
					float num3 = global::UnityEngine.Mathf.Clamp01((float)textureHeightPercent / 100f);
					float height = num3 * num2;
					float width = num3 * num;
					bool supportsStereo = false;
					global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
					global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor = universalCameraData.cameraTargetDescriptor;
					if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Linear | global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render))
					{
						cameraTargetDescriptor.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat;
					}
					CreateDebugTexture(cameraTargetDescriptor);
					global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination = renderGraph.ImportTexture(importParams: new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
					{
						clearOnFirstUse = false,
						discardOnLastUse = false
					}, rt: m_RenderGraphDebugTextureHandle);
					switch (debugFullScreenMode)
					{
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.Depth:
						BlitToDebugTexture(renderGraph, universalResourceData.cameraDepthTexture, destination);
						supportsStereo = true;
						break;
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.MotionVector:
						BlitToDebugTexture(renderGraph, universalResourceData.motionVectorColor, destination, isSourceTextureColor: true);
						supportsStereo = true;
						zero.x = -0.01f;
						zero.y = 0.01f;
						zero.z = 0f;
						zero.w = 1f;
						break;
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.AdditionalLightsShadowMap:
						BlitToDebugTexture(renderGraph, universalResourceData.additionalShadowsTexture, destination);
						break;
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.MainLightShadowMap:
						BlitToDebugTexture(renderGraph, universalResourceData.mainShadowsTexture, destination);
						break;
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.AdditionalLightsCookieAtlas:
					{
						global::UnityEngine.Rendering.Universal.LightCookieManager lightCookieManager = m_LightCookieManager;
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source2 = ((lightCookieManager != null && lightCookieManager.AdditionalLightsCookieAtlasTexture != null) ? renderGraph.ImportTexture(m_LightCookieManager.AdditionalLightsCookieAtlasTexture) : global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle);
						BlitToDebugTexture(renderGraph, source2, destination);
						break;
					}
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.ReflectionProbeAtlas:
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source = ((m_ForwardLights.reflectionProbeManager.atlasRT != null) ? renderGraph.ImportTexture(global::UnityEngine.Rendering.RTHandles.Alloc(m_ForwardLights.reflectionProbeManager.atlasRT, transferOwnership: true)) : global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle);
						BlitToDebugTexture(renderGraph, source, destination);
						break;
					}
					}
					global::UnityEngine.RenderTexture renderTexture = null;
					switch (debugFullScreenMode)
					{
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.AdditionalLightsShadowMap:
						renderTexture = m_AdditionalLightsShadowCasterPass?.m_AdditionalLightsShadowmapHandle?.rt;
						break;
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.MainLightShadowMap:
						renderTexture = m_MainLightShadowCasterPass?.m_MainLightShadowmapTexture?.rt;
						break;
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.AdditionalLightsCookieAtlas:
						renderTexture = m_LightCookieManager?.AdditionalLightsCookieAtlasTexture?.rt;
						break;
					case global::UnityEngine.Rendering.Universal.DebugFullScreenMode.ReflectionProbeAtlas:
						renderTexture = m_ForwardLights?.reflectionProbeManager.atlasRT;
						break;
					}
					if (renderTexture != null)
					{
						CorrectForTextureAspectRatio(ref width, ref height, renderTexture.width, renderTexture.height);
					}
					global::UnityEngine.Rect displayRect = CalculateUVRect(universalCameraData, width, height);
					base.DebugHandler.SetDebugRenderTarget(m_RenderGraphDebugTextureHandle, displayRect, supportsStereo, zero);
				}
				else
				{
					base.DebugHandler.ResetDebugRenderTarget();
				}
			}
			if (base.DebugHandler != null && !base.DebugHandler.TryGetFullscreenDebugMode(out var _, out var textureHeightPercent2))
			{
				global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer gpuResidentDrawerSettings = base.DebugHandler.DebugDisplaySettings.gpuResidentDrawerSettings;
				global::UnityEngine.Rendering.GPUResidentDrawer.RenderDebugOcclusionTestOverlay(renderGraph, gpuResidentDrawerSettings, universalCameraData.camera.GetInstanceID(), universalResourceData.activeColorTexture);
				float num4 = (int)((float)universalCameraData.pixelHeight * universalCameraData.renderScale);
				float num5 = (int)((float)universalCameraData.pixelHeight * universalCameraData.renderScale);
				float num6 = num5 * (float)textureHeightPercent2 / 100f;
				global::UnityEngine.Rendering.GPUResidentDrawer.RenderDebugOccluderOverlay(renderGraph, gpuResidentDrawerSettings, new global::UnityEngine.Vector2(0.25f * num4, num5 - 1.5f * num6), num6, universalResourceData.activeColorTexture);
			}
		}

		private void SetupAfterPostRenderGraphFinalPassDebug(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(universalCameraData.isPreviewCamera) && base.DebugHandler.TryGetFullscreenDebugMode(out var debugFullScreenMode, out var textureHeightPercent) && debugFullScreenMode == global::UnityEngine.Rendering.Universal.DebugFullScreenMode.STP)
			{
				CreateDebugTexture(universalCameraData.cameraTargetDescriptor);
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination = renderGraph.ImportTexture(importParams: new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
				{
					clearOnFirstUse = false,
					discardOnLastUse = false
				}, rt: m_RenderGraphDebugTextureHandle);
				BlitToDebugTexture(renderGraph, universalResourceData.stpDebugView, destination);
				global::UnityEngine.Rect displayRect = CalculateUVRect(universalCameraData, textureHeightPercent);
				global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
				base.DebugHandler.SetDebugRenderTarget(m_RenderGraphDebugTextureHandle, displayRect, supportsStereo: true, zero);
			}
		}

		private void BlitToDebugTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, bool isSourceTextureColor = false)
		{
			if (source.IsValid())
			{
				if (isSourceTextureColor)
				{
					global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.AddCopyPass(renderGraph, source, destination, "Copy Pass Utility", returnBuilder: false, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\UniversalRendererDebug.cs", 251);
					return;
				}
				global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitMaterialParameters blitParameters = new global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitMaterialParameters(source, destination, m_DebugBlitMaterial, 0);
				global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.AddBlitPass(renderGraph, blitParameters, "Blit Pass Utility w. Material", returnBuilder: false, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\UniversalRendererDebug.cs", 260);
			}
			else
			{
				BlitEmptyTexture(renderGraph, destination);
			}
		}

		private void BlitEmptyTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, string passName = "Copy To Debug Texture")
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderer.CopyToDebugTexturePassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.UniversalRenderer.CopyToDebugTexturePassData>(passName, out passData, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\UniversalRendererDebug.cs", 271);
			passData.src = renderGraph.defaultResources.blackTexture;
			passData.dest = destination;
			rasterRenderGraphBuilder.SetRenderAttachment(destination, 0);
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.UniversalRenderer.CopyToDebugTexturePassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.Blitter.BlitTexture(context.cmd, data.src, new global::UnityEngine.Vector4(1f, 1f, 0f, 0f), 0f, bilinear: false);
			});
		}

		private void CleanupRenderGraphResources()
		{
			m_RenderGraphCameraColorHandles[0]?.Release();
			m_RenderGraphCameraColorHandles[1]?.Release();
			m_RenderGraphCameraDepthHandle?.Release();
			m_RenderGraphDebugTextureHandle?.Release();
			m_OffscreenUIColorHandle?.Release();
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateRenderGraphTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.RenderTextureDescriptor desc, string name, bool clear, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Clamp)
		{
			GetTextureDesc(in desc, out var rgDesc);
			rgDesc.clearBuffer = clear;
			rgDesc.name = name;
			rgDesc.filterMode = filterMode;
			rgDesc.wrapMode = wrapMode;
			return renderGraph.CreateTexture(in rgDesc);
		}

		internal static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateRenderGraphTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.RenderTextureDescriptor desc, string name, bool clear, global::UnityEngine.Color color, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Clamp, bool discardOnLastUse = false)
		{
			GetTextureDesc(in desc, out var rgDesc);
			rgDesc.clearBuffer = clear;
			rgDesc.clearColor = color;
			rgDesc.msaaSamples = (global::UnityEngine.Rendering.MSAASamples)desc.msaaSamples;
			rgDesc.name = name;
			rgDesc.filterMode = filterMode;
			rgDesc.wrapMode = wrapMode;
			rgDesc.discardBuffer = discardOnLastUse;
			return renderGraph.CreateTexture(in rgDesc);
		}

		internal static void GetTextureDesc(in global::UnityEngine.RenderTextureDescriptor desc, out global::UnityEngine.Rendering.RenderGraphModule.TextureDesc rgDesc)
		{
			rgDesc = new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(desc.width, desc.height);
			rgDesc.dimension = desc.dimension;
			rgDesc.bindTextureMS = desc.bindMS;
			rgDesc.format = ((desc.depthStencilFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None) ? desc.depthStencilFormat : desc.graphicsFormat);
			rgDesc.isShadowMap = desc.shadowSamplingMode != global::UnityEngine.Rendering.ShadowSamplingMode.None && desc.depthStencilFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			rgDesc.slices = desc.volumeDepth;
			rgDesc.msaaSamples = (global::UnityEngine.Rendering.MSAASamples)desc.msaaSamples;
			rgDesc.enableRandomWrite = desc.enableRandomWrite;
			rgDesc.enableShadingRate = desc.enableShadingRate;
			rgDesc.useDynamicScale = desc.useDynamicScale;
			rgDesc.useDynamicScaleExplicit = desc.useDynamicScaleExplicit;
			rgDesc.vrUsage = desc.vrUsage;
		}

		internal static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateRenderGraphTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc, string name, bool clear, global::UnityEngine.Color clearColor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Clamp, bool discardOnLastUse = false)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc2 = desc;
			desc2.name = name;
			desc2.clearBuffer = clear;
			desc2.clearColor = clearColor;
			desc2.filterMode = filterMode;
			desc2.wrapMode = wrapMode;
			desc2.discardBuffer = discardOnLastUse;
			return renderGraph.CreateTexture(in desc2);
		}

		private bool RequiresIntermediateAttachments(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary renderPassInputs, bool requireCopyFromDepth, bool applyPostProcessing)
		{
			return ((HasActiveRenderFeatures(base.rendererFeatures) && m_IntermediateTextureMode == global::UnityEngine.Rendering.Universal.IntermediateTextureMode.Always) | HasPassesRequiringIntermediateTexture(base.activeRenderPassQueue) | RequiresIntermediateColorTexture(cameraData, in renderPassInputs, usesDeferredLighting, applyPostProcessing)) || requireCopyFromDepth;
		}

		private void UpdateCameraHistory(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (cameraData != null && cameraData.historyManager != null)
			{
				int num = 0;
				bool num2 = cameraData.xr.enabled && !cameraData.xr.singlePassEnabled;
				num = cameraData.xr.multipassId;
				if (!num2 || num == 0)
				{
					global::UnityEngine.Rendering.Universal.UniversalCameraHistory historyManager = cameraData.historyManager;
					historyManager.GatherHistoryRequests();
					historyManager.ReleaseUnusedHistory();
					historyManager.SwapAndSetReferenceSize(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
				}
			}
		}

		private void CreateRenderGraphCameraRenderTargets(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, bool isCameraTargetOffscreenDepth, bool requireIntermediateAttachments, bool depthTextureIsDepthFormat)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderer.ClearCameraParams clearCameraParams = GetClearCameraParams(universalCameraData);
			SetupTargetHandles(universalCameraData);
			UpdateCameraHistory(universalCameraData);
			ImportBackBuffers(renderGraph, universalCameraData, clearCameraParams.clearValue, isCameraTargetOffscreenDepth);
			GetTextureDesc(in universalCameraData.cameraTargetDescriptor, out var rgDesc);
			rgDesc.useMipMap = false;
			rgDesc.autoGenerateMips = false;
			rgDesc.mipMapBias = 0f;
			rgDesc.anisoLevel = 1;
			if (requireIntermediateAttachments)
			{
				rgDesc.format = universalCameraData.cameraTargetDescriptor.graphicsFormat;
				if (!isCameraTargetOffscreenDepth)
				{
					CreateIntermediateCameraColorAttachment(renderGraph, universalCameraData, in rgDesc, clearCameraParams.mustClearColor, clearCameraParams.clearValue);
				}
				rgDesc.format = universalCameraData.cameraTargetDescriptor.depthStencilFormat;
				CreateIntermediateCameraDepthAttachment(renderGraph, universalCameraData, in rgDesc, clearCameraParams.mustClearDepth, clearCameraParams.clearValue, depthTextureIsDepthFormat);
			}
			else
			{
				universalResourceData.SwitchActiveTexturesToBackbuffer();
			}
			CreateCameraDepthCopyTexture(renderGraph, rgDesc, depthTextureIsDepthFormat, clearCameraParams.clearValue);
			CreateCameraNormalsTexture(renderGraph, rgDesc);
			CreateMotionVectorTextures(renderGraph, rgDesc);
			CreateRenderingLayersTexture(renderGraph, rgDesc);
			if (universalCameraData.isHDROutputActive && universalCameraData.rendersOverlayUI)
			{
				CreateOffscreenUITexture(renderGraph, rgDesc);
			}
		}

		private global::UnityEngine.Rendering.Universal.UniversalRenderer.ClearCameraParams GetClearCameraParams(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			bool clearColor = cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base;
			bool clearDepth = cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base || cameraData.clearDepth;
			global::UnityEngine.Color color = ((cameraData.camera.clearFlags == global::UnityEngine.CameraClearFlags.Nothing && cameraData.targetTexture == null) ? global::UnityEngine.Color.yellow : cameraData.backgroundColor);
			if (IsSceneFilteringEnabled(cameraData.camera))
			{
				color.a = 0f;
				clearDepth = false;
			}
			global::UnityEngine.Rendering.Universal.DebugHandler debugHandler = cameraData.renderer.DebugHandler;
			if (debugHandler != null && debugHandler.IsActiveForCamera(cameraData.isPreviewCamera) && debugHandler.IsScreenClearNeeded)
			{
				clearColor = true;
				clearDepth = true;
				if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
				{
					base.DebugHandler.TryGetScreenClearColor(ref color);
				}
			}
			return new global::UnityEngine.Rendering.Universal.UniversalRenderer.ClearCameraParams(clearColor, clearDepth, color);
		}

		private void SetupTargetHandles(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier = ((cameraData.targetTexture != null) ? new global::UnityEngine.Rendering.RenderTargetIdentifier(cameraData.targetTexture) : ((global::UnityEngine.Rendering.RenderTargetIdentifier)global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget));
			global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier2 = ((cameraData.targetTexture != null) ? new global::UnityEngine.Rendering.RenderTargetIdentifier(cameraData.targetTexture) : ((global::UnityEngine.Rendering.RenderTargetIdentifier)global::UnityEngine.Rendering.BuiltinRenderTextureType.Depth));
			if (cameraData.xr.enabled)
			{
				renderTargetIdentifier = cameraData.xr.renderTarget;
				renderTargetIdentifier2 = cameraData.xr.renderTarget;
			}
			if (m_TargetColorHandle == null)
			{
				m_TargetColorHandle = global::UnityEngine.Rendering.RTHandles.Alloc(renderTargetIdentifier, "Backbuffer color");
			}
			else if (m_TargetColorHandle.nameID != renderTargetIdentifier)
			{
				global::UnityEngine.Rendering.RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref m_TargetColorHandle, renderTargetIdentifier);
			}
			if (m_TargetDepthHandle == null)
			{
				m_TargetDepthHandle = global::UnityEngine.Rendering.RTHandles.Alloc(renderTargetIdentifier2, "Backbuffer depth");
			}
			else if (m_TargetDepthHandle.nameID != renderTargetIdentifier2)
			{
				global::UnityEngine.Rendering.RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref m_TargetDepthHandle, renderTargetIdentifier2);
			}
		}

		private void SetupRenderingLayers(int msaaSamples)
		{
			m_RequiresRenderingLayer = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.RequireRenderingLayers(this, base.rendererFeatures, msaaSamples, out m_RenderingLayersEvent, out m_RenderingLayersMaskSize);
			m_RenderingLayerProvidesRenderObjectPass = m_RequiresRenderingLayer && m_RenderingLayersEvent == global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.Opaque;
			m_RenderingLayerProvidesByDepthNormalPass = m_RequiresRenderingLayer && m_RenderingLayersEvent == global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.DepthNormalPrePass;
			if (m_DeferredLights != null)
			{
				m_DeferredLights.RenderingLayerMaskSize = m_RenderingLayersMaskSize;
				m_DeferredLights.UseDecalLayers = m_RequiresRenderingLayer;
			}
		}

		internal void SetupRenderGraphLights(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			m_ForwardLights.SetupRenderGraphLights(renderGraph, renderingData, cameraData, lightData);
			if (usesDeferredLighting)
			{
				m_DeferredLights.UseFramebufferFetch = renderGraph.nativeRenderPassesEnabled;
				m_DeferredLights.SetupRenderGraphLights(renderGraph, cameraData, lightData);
			}
		}

		private void RenderRawColorDepthHistory(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData)
		{
			if (cameraData == null || cameraData.historyManager == null || resourceData == null)
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.UniversalCameraHistory historyManager = cameraData.historyManager;
			bool flag = false;
			int num = 0;
			flag = cameraData.xr.enabled && !cameraData.xr.singlePassEnabled;
			num = cameraData.xr.multipassId;
			if (historyManager.IsAccessRequested<global::UnityEngine.Rendering.Universal.RawColorHistory>() && resourceData.cameraColor.IsValid())
			{
				global::UnityEngine.Rendering.Universal.RawColorHistory historyForWrite = historyManager.GetHistoryForWrite<global::UnityEngine.Rendering.Universal.RawColorHistory>();
				if (historyForWrite != null)
				{
					historyForWrite.Update(ref cameraData.cameraTargetDescriptor, flag);
					if (historyForWrite.GetCurrentTexture(num) != null)
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination = renderGraph.ImportTexture(historyForWrite.GetCurrentTexture(num));
						m_HistoryRawColorCopyPass.RenderToExistingTexture(renderGraph, base.frameData, in destination, resourceData.cameraColor);
					}
				}
			}
			if (!historyManager.IsAccessRequested<global::UnityEngine.Rendering.Universal.RawDepthHistory>() || !resourceData.cameraDepth.IsValid())
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.RawDepthHistory historyForWrite2 = historyManager.GetHistoryForWrite<global::UnityEngine.Rendering.Universal.RawDepthHistory>();
			if (historyForWrite2 != null)
			{
				if (!m_HistoryRawDepthCopyPass.CopyToDepth)
				{
					global::UnityEngine.RenderTextureDescriptor cameraDesc = cameraData.cameraTargetDescriptor;
					cameraDesc.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat;
					cameraDesc.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
					historyForWrite2.Update(ref cameraDesc, flag);
				}
				else
				{
					global::UnityEngine.RenderTextureDescriptor cameraDesc2 = cameraData.cameraTargetDescriptor;
					cameraDesc2.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
					historyForWrite2.Update(ref cameraDesc2, flag);
				}
				if (historyForWrite2.GetCurrentTexture(num) != null)
				{
					global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination2 = renderGraph.ImportTexture(historyForWrite2.GetCurrentTexture(num));
					m_HistoryRawDepthCopyPass.Render(renderGraph, base.frameData, destination2, resourceData.cameraDepth);
				}
			}
		}

		public override void OnBeginRenderGraphFrame()
		{
			base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>().InitFrame();
		}

		internal override void OnRecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ScriptableRenderContext context)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.UniversalPostProcessingData universalPostProcessingData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();
			useRenderPassEnabled = renderGraph.nativeRenderPassesEnabled;
			global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.SetRenderGraphMotionVectorGlobalMatrices(renderGraph, universalCameraData);
			SetupRenderGraphLights(renderGraph, renderingData, universalCameraData, lightData);
			SetupRenderingLayers(universalCameraData.cameraTargetDescriptor.msaaSamples);
			bool flag = universalCameraData.camera.targetTexture != null && universalCameraData.camera.targetTexture.format == global::UnityEngine.RenderTextureFormat.Depth;
			global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary renderPassInputs = GetRenderPassInputs(universalCameraData.IsTemporalAAEnabled(), universalPostProcessingData.isEnabled, universalCameraData.isSceneViewCamera, m_RenderingLayerProvidesByDepthNormalPass, base.activeRenderPassQueue, m_MotionVectorPass);
			bool applyPostProcessing = universalCameraData.postProcessEnabled && m_PostProcessPassRenderGraph != null;
			bool flag2 = RequireDepthTexture(universalCameraData, in renderPassInputs, applyPostProcessing);
			bool flag3 = RequirePrepassForTextures(universalCameraData, in renderPassInputs, flag2);
			base.useDepthPriming = IsDepthPrimingEnabledRenderGraph(universalCameraData, in renderPassInputs, m_DepthPrimingMode, flag2, flag3, usesDeferredLighting);
			bool requiresPrepass = flag3 || base.useDepthPriming;
			bool flag4 = flag3 && !usesDeferredLighting && !base.useDepthPriming;
			bool depthTextureIsDepthFormat = flag4;
			bool requireCopyFromDepth = flag2 && !flag4;
			if (universalCameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
			{
				m_RequiresIntermediateAttachments = RequiresIntermediateAttachments(universalCameraData, in renderPassInputs, requireCopyFromDepth, applyPostProcessing);
			}
			CreateRenderGraphCameraRenderTargets(renderGraph, flag, m_RequiresIntermediateAttachments, depthTextureIsDepthFormat);
			_ = base.DebugHandler;
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRendering);
			SetupRenderGraphCameraProperties(renderGraph, universalResourceData.activeColorTexture.IsValid() ? universalResourceData.activeColorTexture : universalResourceData.activeDepthTexture);
			universalCameraData.renderer.useDepthPriming = base.useDepthPriming;
			if (flag)
			{
				OnOffscreenDepthTextureRendering(renderGraph, context, universalResourceData, universalCameraData);
				return;
			}
			OnBeforeRendering(renderGraph);
			BeginRenderGraphXRRendering(renderGraph);
			OnMainRendering(renderGraph, context, in renderPassInputs, requiresPrepass, flag2);
			OnAfterRendering(renderGraph, applyPostProcessing);
			EndRenderGraphXRRendering(renderGraph);
		}

		public override void OnEndRenderGraphFrame()
		{
			base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>().EndFrame();
		}

		internal override void OnFinishRenderGraphRendering(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (usesDeferredLighting)
			{
				m_DeferredPass.OnCameraCleanup(cmd);
			}
			m_CopyDepthPass.OnCameraCleanup(cmd);
			m_DepthNormalPrepass.OnCameraCleanup(cmd);
		}

		private void OnOffscreenDepthTextureRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (!renderGraph.nativeRenderPassesEnabled)
			{
				global::UnityEngine.Rendering.Universal.ClearTargetsPass.Render(renderGraph, resourceData.activeColorTexture, resourceData.backBufferDepth, global::UnityEngine.Rendering.RTClearFlags.Depth, cameraData.backgroundColor);
			}
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>();
			if (m_MainLightShadowCasterPass.Setup(renderingData, cameraData, lightData, shadowData))
			{
				resourceData.mainShadowsTexture = m_MainLightShadowCasterPass.Render(renderGraph, base.frameData);
			}
			if (m_AdditionalLightsShadowCasterPass.Setup(renderingData, cameraData, lightData, shadowData))
			{
				resourceData.additionalShadowsTexture = m_AdditionalLightsShadowCasterPass.Render(renderGraph, base.frameData);
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingShadows, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques);
			m_RenderOpaqueForwardPass.Render(renderGraph, base.frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle, resourceData.backBufferDepth, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle);
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingTransparents);
			if (needTransparencyPass)
			{
				m_RenderTransparentForwardPass.Render(renderGraph, base.frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle, resourceData.backBufferDepth, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle);
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingTransparents, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRendering);
		}

		private void OnBeforeRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>();
			m_ForwardLights.PreSetup(renderingData, universalCameraData, lightData);
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingShadows);
			bool flag = false;
			if (m_MainLightShadowCasterPass.Setup(renderingData, universalCameraData, lightData, shadowData))
			{
				flag = true;
				universalResourceData.mainShadowsTexture = m_MainLightShadowCasterPass.Render(renderGraph, base.frameData);
			}
			if (m_AdditionalLightsShadowCasterPass.Setup(renderingData, universalCameraData, lightData, shadowData))
			{
				flag = true;
				universalResourceData.additionalShadowsTexture = m_AdditionalLightsShadowCasterPass.Render(renderGraph, base.frameData);
			}
			if (flag)
			{
				SetupRenderGraphCameraProperties(renderGraph, universalResourceData.activeColorTexture.IsValid() ? universalResourceData.activeColorTexture : universalResourceData.activeDepthTexture);
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingShadows);
			if (universalCameraData.postProcessEnabled && m_PostProcessPassRenderGraph != null)
			{
				m_ColorGradingLutPassRenderGraph.Render(renderGraph, base.frameData, out var internalColorLut);
				universalResourceData.internalColorLut = internalColorLut;
			}
		}

		private void UpdateInstanceOccluders(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTexture)
		{
			int x = (int)((float)cameraData.pixelWidth * cameraData.renderScale);
			int y = (int)((float)cameraData.pixelHeight * cameraData.renderScale);
			bool flag = cameraData.xr.enabled && cameraData.xr.singlePassEnabled;
			global::UnityEngine.Rendering.OccluderParameters occluderParameters = new global::UnityEngine.Rendering.OccluderParameters(cameraData.camera.GetInstanceID());
			occluderParameters.subviewCount = ((!flag) ? 1 : 2);
			occluderParameters.depthTexture = depthTexture;
			occluderParameters.depthSize = new global::UnityEngine.Vector2Int(x, y);
			occluderParameters.depthIsArray = flag;
			global::UnityEngine.Rendering.OccluderParameters occluderParameters2 = occluderParameters;
			global::System.Span<global::UnityEngine.Rendering.OccluderSubviewUpdate> span = stackalloc global::UnityEngine.Rendering.OccluderSubviewUpdate[occluderParameters2.subviewCount];
			for (int i = 0; i < occluderParameters2.subviewCount; i++)
			{
				global::UnityEngine.Matrix4x4 viewMatrix = cameraData.GetViewMatrix(i);
				global::UnityEngine.Matrix4x4 projectionMatrix = cameraData.GetProjectionMatrix(i);
				span[i] = new global::UnityEngine.Rendering.OccluderSubviewUpdate(i)
				{
					depthSliceIndex = i,
					viewMatrix = viewMatrix,
					invViewMatrix = viewMatrix.inverse,
					gpuProjMatrix = global::UnityEngine.GL.GetGPUProjectionMatrix(projectionMatrix, renderIntoTexture: true),
					viewOffsetWorldSpace = global::UnityEngine.Vector3.zero
				};
			}
			global::UnityEngine.Rendering.GPUResidentDrawer.UpdateInstanceOccluders(renderGraph, in occluderParameters2, span);
		}

		private void InstanceOcclusionTest(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.OcclusionTest occlusionTest)
		{
			bool flag = cameraData.xr.enabled && cameraData.xr.singlePassEnabled;
			int num = ((!flag) ? 1 : 2);
			global::UnityEngine.Rendering.OcclusionCullingSettings occlusionCullingSettings = new global::UnityEngine.Rendering.OcclusionCullingSettings(cameraData.camera.GetInstanceID(), occlusionTest);
			occlusionCullingSettings.instanceMultiplier = ((!flag || global::UnityEngine.SystemInfo.supportsMultiview) ? 1 : 2);
			global::UnityEngine.Rendering.OcclusionCullingSettings settings = occlusionCullingSettings;
			global::System.Span<global::UnityEngine.Rendering.SubviewOcclusionTest> span = stackalloc global::UnityEngine.Rendering.SubviewOcclusionTest[num];
			for (int i = 0; i < num; i++)
			{
				span[i] = new global::UnityEngine.Rendering.SubviewOcclusionTest
				{
					cullingSplitIndex = 0,
					occluderSubviewIndex = i
				};
			}
			global::UnityEngine.Rendering.GPUResidentDrawer.InstanceOcclusionTest(renderGraph, in settings, span);
		}

		private void RecordCustomPassesWithDepthCopyAndMotion(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.RenderPassEvent earliestDepthReadEvent, global::UnityEngine.Rendering.Universal.RenderPassEvent currentEvent, bool renderMotionVectors)
		{
			CalculateSplitEventRange(currentEvent, earliestDepthReadEvent, out var startEvent, out var splitEvent, out var endEvent);
			RecordCustomRenderGraphPassesInEventRange(renderGraph, startEvent, splitEvent);
			ExecuteScheduledDepthCopyWithMotion(renderGraph, resourceData, renderMotionVectors);
			RecordCustomRenderGraphPassesInEventRange(renderGraph, splitEvent, endEvent);
		}

		private static bool AllowPartialDepthNormalsPrepass(bool isDeferred, global::UnityEngine.Rendering.Universal.RenderPassEvent requiresDepthNormalEvent, bool useDepthPriming)
		{
			if (isDeferred && global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingGbuffer <= requiresDepthNormalEvent && requiresDepthNormalEvent <= global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques)
			{
				return !useDepthPriming;
			}
			return false;
		}

		private global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule CalculateDepthCopySchedule(global::UnityEngine.Rendering.Universal.RenderPassEvent earliestDepthReadEvent, bool hasFullPrepass)
		{
			if (earliestDepthReadEvent < global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques || m_CopyDepthMode == global::UnityEngine.Rendering.Universal.CopyDepthMode.ForcePrepass)
			{
				if (hasFullPrepass)
				{
					return global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterPrepass;
				}
				return global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterGBuffer;
			}
			if (earliestDepthReadEvent < global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingTransparents || m_CopyDepthMode == global::UnityEngine.Rendering.Universal.CopyDepthMode.AfterOpaques)
			{
				if (earliestDepthReadEvent < global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingSkybox)
				{
					return global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterOpaques;
				}
				return global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterSkybox;
			}
			if (earliestDepthReadEvent < global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing || m_CopyDepthMode == global::UnityEngine.Rendering.Universal.CopyDepthMode.AfterTransparents)
			{
				return global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterTransparents;
			}
			return global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.None;
		}

		private global::UnityEngine.Rendering.Universal.UniversalRenderer.TextureCopySchedules CalculateTextureCopySchedules(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary renderPassInputs, bool isDeferred, bool requiresDepthPrepass, bool hasFullPrepass, bool requireDepthTexture)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule depth = global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.None;
			if (requireDepthTexture)
			{
				depth = ((isDeferred || !requiresDepthPrepass || base.useDepthPriming) ? CalculateDepthCopySchedule(renderPassInputs.requiresDepthTextureEarliestEvent, hasFullPrepass) : global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.DuringPrepass);
			}
			global::UnityEngine.Rendering.Universal.UniversalRenderer.ColorCopySchedule color = ((!cameraData.requiresOpaqueTexture && !renderPassInputs.requiresColorTexture) ? global::UnityEngine.Rendering.Universal.UniversalRenderer.ColorCopySchedule.None : global::UnityEngine.Rendering.Universal.UniversalRenderer.ColorCopySchedule.AfterSkybox);
			global::UnityEngine.Rendering.Universal.UniversalRenderer.TextureCopySchedules result = default(global::UnityEngine.Rendering.Universal.UniversalRenderer.TextureCopySchedules);
			result.depth = depth;
			result.color = color;
			return result;
		}

		private void CopyDepthToDepthTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData)
		{
			m_CopyDepthPass.Render(renderGraph, base.frameData, resourceData.cameraDepthTexture, resourceData.activeDepthTexture, bindAsCameraDepth: true);
		}

		private void RenderMotionVectors(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData)
		{
			m_MotionVectorPass.Render(renderGraph, base.frameData, resourceData.cameraDepthTexture, resourceData.motionVectorColor, resourceData.motionVectorDepth);
		}

		private void ExecuteScheduledDepthCopyWithMotion(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, bool renderMotionVectors)
		{
			CopyDepthToDepthTexture(renderGraph, resourceData);
			if (renderMotionVectors)
			{
				RenderMotionVectors(renderGraph, resourceData);
			}
		}

		private void OnMainRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ScriptableRenderContext context, in global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary renderPassInputs, bool requiresPrepass, bool requireDepthTexture)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();
			if (!renderGraph.nativeRenderPassesEnabled)
			{
				global::UnityEngine.Rendering.RTClearFlags cameraClearFlag = (global::UnityEngine.Rendering.RTClearFlags)global::UnityEngine.Rendering.Universal.ScriptableRenderer.GetCameraClearFlag(cameraData);
				if (cameraClearFlag != global::UnityEngine.Rendering.RTClearFlags.None)
				{
					global::UnityEngine.Rendering.Universal.ClearTargetsPass.Render(renderGraph, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, cameraClearFlag, cameraData.backgroundColor);
				}
			}
			if (universalRenderingData.stencilLodCrossFadeEnabled)
			{
				m_StencilCrossFadeRenderPass.Render(renderGraph, context, universalResourceData.activeDepthTexture);
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses);
			bool num = requiresPrepass && !renderPassInputs.requiresNormalsTexture;
			bool flag = requiresPrepass && renderPassInputs.requiresNormalsTexture;
			bool flag2 = num || (flag && !AllowPartialDepthNormalsPrepass(usesDeferredLighting, renderPassInputs.requiresDepthNormalAtEvent, base.useDepthPriming));
			global::UnityEngine.Rendering.Universal.UniversalRenderer.TextureCopySchedules textureCopySchedules = CalculateTextureCopySchedules(cameraData, in renderPassInputs, usesDeferredLighting, requiresPrepass, flag2, requireDepthTexture);
			bool flag3 = global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingGbuffer <= renderPassInputs.requiresDepthNormalAtEvent && renderPassInputs.requiresDepthNormalAtEvent <= global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques;
			bool flag4 = requiresPrepass && (!usesDeferredLighting || !flag3);
			global::UnityEngine.Rendering.Universal.UniversalRenderer.OccluderPass occluderPass = global::UnityEngine.Rendering.Universal.UniversalRenderer.OccluderPass.None;
			if (cameraData.useGPUOcclusionCulling)
			{
				occluderPass = (flag4 ? global::UnityEngine.Rendering.Universal.UniversalRenderer.OccluderPass.DepthPrepass : (usesDeferredLighting ? global::UnityEngine.Rendering.Universal.UniversalRenderer.OccluderPass.GBuffer : global::UnityEngine.Rendering.Universal.UniversalRenderer.OccluderPass.ForwardOpaque));
			}
			if (cameraData.xr.enabled && cameraData.xr.hasMotionVectorPass)
			{
				m_XRDepthMotionPass?.Update(ref cameraData);
				m_XRDepthMotionPass?.Render(renderGraph, base.frameData);
			}
			if (requiresPrepass)
			{
				bool flag5 = usesDeferredLighting || base.useDepthPriming;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTexture = (flag5 ? universalResourceData.activeDepthTexture : universalResourceData.cameraDepthTexture);
				if (universalRenderingData.stencilLodCrossFadeEnabled && flag && !flag5)
				{
					m_StencilCrossFadeRenderPass.Render(renderGraph, context, universalResourceData.cameraDepthTexture);
				}
				bool flag6 = occluderPass == global::UnityEngine.Rendering.Universal.UniversalRenderer.OccluderPass.DepthPrepass;
				int num2 = ((!flag6) ? 1 : 2);
				for (int i = 0; i < num2; i++)
				{
					uint batchLayerMask = uint.MaxValue;
					if (flag6)
					{
						global::UnityEngine.Rendering.OcclusionTest occlusionTest = ((i == 0) ? global::UnityEngine.Rendering.OcclusionTest.TestAll : global::UnityEngine.Rendering.OcclusionTest.TestCulled);
						InstanceOcclusionTest(renderGraph, cameraData, occlusionTest);
						batchLayerMask = occlusionTest.GetBatchLayerMask();
					}
					bool num3 = i == num2 - 1;
					bool setGlobalDepth = num3 && !flag5;
					bool setGlobalTextures = num3 && flag2;
					if (flag)
					{
						if (universalResourceData.isActiveTargetBackBuffer)
						{
							SetupRenderGraphCameraProperties(renderGraph, depthTexture);
						}
						DepthNormalPrepassRender(renderGraph, renderPassInputs, in depthTexture, batchLayerMask, setGlobalDepth, setGlobalTextures, !flag2);
						if (universalResourceData.isActiveTargetBackBuffer)
						{
							SetupRenderGraphCameraProperties(renderGraph, universalResourceData.activeColorTexture.IsValid() ? universalResourceData.activeColorTexture : universalResourceData.activeDepthTexture);
						}
					}
					else
					{
						m_DepthPrepass.Render(renderGraph, base.frameData, in depthTexture, batchLayerMask, setGlobalDepth);
					}
					if (flag6)
					{
						UpdateInstanceOccluders(renderGraph, cameraData, depthTexture);
						if (i != 0)
						{
							InstanceOcclusionTest(renderGraph, cameraData, global::UnityEngine.Rendering.OcclusionTest.TestAll);
						}
					}
				}
			}
			if (textureCopySchedules.depth == global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterPrepass)
			{
				ExecuteScheduledDepthCopyWithMotion(renderGraph, universalResourceData, renderPassInputs.requiresMotionVectors);
			}
			else if (textureCopySchedules.depth == global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.DuringPrepass && renderPassInputs.requiresMotionVectors)
			{
				RenderMotionVectors(renderGraph, universalResourceData);
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingPrePasses);
			if (cameraData.xr.hasValidOcclusionMesh)
			{
				m_XROcclusionMeshPass.Render(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture);
			}
			if (usesDeferredLighting)
			{
				m_DeferredLights.Setup(m_AdditionalLightsShadowCasterPass);
				m_DeferredLights.UseFramebufferFetch = renderGraph.nativeRenderPassesEnabled;
				m_DeferredLights.HasNormalPrepass = flag;
				m_DeferredLights.HasDepthPrepass = requiresPrepass;
				m_DeferredLights.ResolveMixedLightingMode(lightData);
				m_DeferredLights.CreateGbufferResourcesRenderGraph(renderGraph, universalResourceData);
				universalResourceData.gBuffer = m_DeferredLights.GbufferTextureHandles;
				RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingGbuffer);
				bool flag7 = occluderPass == global::UnityEngine.Rendering.Universal.UniversalRenderer.OccluderPass.GBuffer;
				int num4 = ((!flag7) ? 1 : 2);
				for (int j = 0; j < num4; j++)
				{
					uint batchLayerMask2 = uint.MaxValue;
					if (flag7)
					{
						global::UnityEngine.Rendering.OcclusionTest occlusionTest2 = ((j == 0) ? global::UnityEngine.Rendering.OcclusionTest.TestAll : global::UnityEngine.Rendering.OcclusionTest.TestCulled);
						InstanceOcclusionTest(renderGraph, cameraData, occlusionTest2);
						batchLayerMask2 = occlusionTest2.GetBatchLayerMask();
					}
					bool setGlobalTextures2 = flag && !flag2;
					m_GBufferPass.Render(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, setGlobalTextures2, batchLayerMask2);
					if (flag7)
					{
						UpdateInstanceOccluders(renderGraph, cameraData, universalResourceData.activeDepthTexture);
						if (j != 0)
						{
							InstanceOcclusionTest(renderGraph, cameraData, global::UnityEngine.Rendering.OcclusionTest.TestAll);
						}
					}
				}
				if (textureCopySchedules.depth == global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterGBuffer)
				{
					ExecuteScheduledDepthCopyWithMotion(renderGraph, universalResourceData, renderPassInputs.requiresMotionVectors);
				}
				else if (!renderGraph.nativeRenderPassesEnabled)
				{
					CopyDepthToDepthTexture(renderGraph, universalResourceData);
				}
				RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingGbuffer, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingDeferredLights);
				m_DeferredPass.Render(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, universalResourceData.gBuffer);
				RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingDeferredLights, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques);
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle mainShadowsTexture = universalResourceData.mainShadowsTexture;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle additionalShadowsTexture = universalResourceData.additionalShadowsTexture;
				m_RenderOpaqueForwardOnlyPass.Render(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, mainShadowsTexture, additionalShadowsTexture);
			}
			else
			{
				RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingGbuffer, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingOpaques);
				bool flag8 = occluderPass == global::UnityEngine.Rendering.Universal.UniversalRenderer.OccluderPass.ForwardOpaque;
				int num5 = ((!flag8) ? 1 : 2);
				for (int k = 0; k < num5; k++)
				{
					uint batchLayerMask3 = uint.MaxValue;
					if (flag8)
					{
						global::UnityEngine.Rendering.OcclusionTest occlusionTest3 = ((k == 0) ? global::UnityEngine.Rendering.OcclusionTest.TestAll : global::UnityEngine.Rendering.OcclusionTest.TestCulled);
						InstanceOcclusionTest(renderGraph, cameraData, occlusionTest3);
						batchLayerMask3 = occlusionTest3.GetBatchLayerMask();
					}
					if (m_RenderingLayerProvidesRenderObjectPass)
					{
						m_RenderOpaqueForwardWithRenderingLayersPass.Render(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.renderingLayersTexture, universalResourceData.activeDepthTexture, universalResourceData.mainShadowsTexture, universalResourceData.additionalShadowsTexture, m_RenderingLayersMaskSize, batchLayerMask3);
						SetRenderingLayersGlobalTextures(renderGraph);
					}
					else
					{
						m_RenderOpaqueForwardPass.Render(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, universalResourceData.mainShadowsTexture, universalResourceData.additionalShadowsTexture, batchLayerMask3, isMainOpaquePass: true);
					}
					if (flag8)
					{
						UpdateInstanceOccluders(renderGraph, cameraData, universalResourceData.activeDepthTexture);
						if (k != 0)
						{
							InstanceOcclusionTest(renderGraph, cameraData, global::UnityEngine.Rendering.OcclusionTest.TestAll);
						}
					}
				}
			}
			if (textureCopySchedules.depth == global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterOpaques)
			{
				RecordCustomPassesWithDepthCopyAndMotion(renderGraph, universalResourceData, renderPassInputs.requiresDepthTextureEarliestEvent, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques, renderPassInputs.requiresMotionVectors);
			}
			else
			{
				RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques);
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingSkybox);
			if (cameraData.camera.clearFlags == global::UnityEngine.CameraClearFlags.Skybox && cameraData.renderType != global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay)
			{
				cameraData.camera.TryGetComponent<global::UnityEngine.Skybox>(out var component);
				global::UnityEngine.Material material = ((component != null) ? component.material : global::UnityEngine.RenderSettings.skybox);
				if (material != null)
				{
					m_DrawSkyboxPass.Render(renderGraph, base.frameData, context, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, material);
				}
			}
			if (textureCopySchedules.depth == global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterSkybox)
			{
				ExecuteScheduledDepthCopyWithMotion(renderGraph, universalResourceData, renderPassInputs.requiresMotionVectors);
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingSkybox);
			if (textureCopySchedules.color == global::UnityEngine.Rendering.Universal.UniversalRenderer.ColorCopySchedule.AfterSkybox)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source = universalResourceData.cameraColor;
				global::UnityEngine.Rendering.Universal.Downsampling opaqueDownsampling = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.opaqueDownsampling;
				m_CopyColorPass.Render(renderGraph, base.frameData, out var destination, in source, opaqueDownsampling);
				universalResourceData.cameraOpaqueTexture = destination;
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingTransparents);
			if (needTransparencyPass)
			{
				m_RenderTransparentForwardPass.m_ShouldTransparentsReceiveShadows = !m_TransparentSettingsPass.Setup();
				m_RenderTransparentForwardPass.Render(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, universalResourceData.mainShadowsTexture, universalResourceData.additionalShadowsTexture);
			}
			if (textureCopySchedules.depth == global::UnityEngine.Rendering.Universal.UniversalRenderer.DepthCopySchedule.AfterTransparents)
			{
				RecordCustomPassesWithDepthCopyAndMotion(renderGraph, universalResourceData, renderPassInputs.requiresDepthTextureEarliestEvent, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingTransparents, renderPassInputs.requiresMotionVectors);
			}
			else
			{
				RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingTransparents);
			}
			if (context.HasInvokeOnRenderObjectCallbacks())
			{
				m_OnRenderObjectCallbackPass.Render(renderGraph, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture);
			}
			RenderRawColorDepthHistory(renderGraph, cameraData, universalResourceData);
			bool rendersOverlayUI = cameraData.rendersOverlayUI;
			bool isHDROutputActive = cameraData.isHDROutputActive;
			if (!(rendersOverlayUI && isHDROutputActive))
			{
				return;
			}
			if (cameraData.rendersOffscreenUI)
			{
				m_DrawOffscreenUIPass.RenderOffscreen(renderGraph, base.frameData, cameraDepthAttachmentFormat, universalResourceData.overlayUITexture);
				if (cameraData.blitsOffscreenUICover)
				{
					global::UnityEngine.RenderTextureDescriptor desc = new global::UnityEngine.RenderTextureDescriptor(1, 1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, 0);
					global::UnityEngine.Rendering.RenderGraphModule.TextureHandle src = CreateRenderGraphTexture(renderGraph, desc, "BlackTexture", clear: false);
					m_OffscreenUICoverPrepass.Render(renderGraph, base.frameData, cameraData, in src, universalResourceData.backBufferColor, universalResourceData.overlayUITexture, useFullScreenViewport: true);
				}
			}
			else
			{
				global::UnityEngine.Rendering.Universal.RenderGraphUtils.SetGlobalTexture(renderGraph, global::UnityEngine.Rendering.Universal.ShaderPropertyId.overlayUITexture, universalResourceData.overlayUITexture, "Set Global Texture", ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\UniversalRendererRenderGraph.cs", 1348);
			}
		}

		private void OnAfterRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, bool applyPostProcessing)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalPostProcessingData universalPostProcessingData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();
			if (universalCameraData.resolveFinalTarget)
			{
				SetupRenderGraphFinalPassDebug(renderGraph, base.frameData);
			}
			bool flag = global::UnityEngine.Rendering.DebugDisplaySettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplaySettings>.Instance.renderingSettings.sceneOverrideMode == global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.None;
			if (flag)
			{
				DrawRenderGraphGizmos(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, global::UnityEngine.Rendering.GizmoSubset.PreImageEffects);
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing);
			bool flag2 = universalPostProcessingData.isEnabled && m_PostProcessPassRenderGraph != null && universalCameraData.resolveFinalTarget && (universalCameraData.antialiasing == global::UnityEngine.Rendering.Universal.AntialiasingMode.FastApproximateAntialiasing || (universalCameraData.imageScalingMode == global::UnityEngine.Rendering.Universal.ImageScalingMode.Upscaling && universalCameraData.upscalingFilter != global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.Linear) || (universalCameraData.IsTemporalAAEnabled() && universalCameraData.taaSettings.contrastAdaptiveSharpening > 0f));
			bool flag3 = universalCameraData.captureActions != null && universalCameraData.resolveFinalTarget;
			bool flag4 = base.activeRenderPassQueue.Find((global::UnityEngine.Rendering.Universal.ScriptableRenderPass x) => x.renderPassEvent >= global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingPostProcessing && x.renderPassEvent < global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRendering) != null;
			bool flag5 = !flag3 && !flag4 && !flag2;
			bool flag6 = base.DebugHandler == null || !base.DebugHandler.HDRDebugViewIsActive(universalCameraData.resolveFinalTarget);
			bool flag7 = universalResourceData.activeDepthID == global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
			global::UnityEngine.Rendering.Universal.DebugHandler activeDebugHandler = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(universalCameraData);
			bool flag8 = activeDebugHandler?.WriteToDebugScreenTexture(universalCameraData.resolveFinalTarget) ?? false;
			if (flag8)
			{
				global::UnityEngine.RenderTextureDescriptor descriptor = universalCameraData.cameraTargetDescriptor;
				global::UnityEngine.Rendering.Universal.DebugHandler.ConfigureColorDescriptorForDebugScreen(ref descriptor, universalCameraData.pixelWidth, universalCameraData.pixelHeight);
				universalResourceData.debugScreenColor = CreateRenderGraphTexture(renderGraph, descriptor, "_DebugScreenColor", clear: false);
				global::UnityEngine.RenderTextureDescriptor descriptor2 = universalCameraData.cameraTargetDescriptor;
				global::UnityEngine.Rendering.Universal.DebugHandler.ConfigureDepthDescriptorForDebugScreen(ref descriptor2, cameraDepthAttachmentFormat, universalCameraData.pixelWidth, universalCameraData.pixelHeight);
				universalResourceData.debugScreenDepth = CreateRenderGraphTexture(renderGraph, descriptor2, "_DebugScreenDepth", clear: false);
			}
			_ = universalResourceData.afterPostProcessColor;
			if (applyPostProcessing)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle activeCameraColorTexture = universalResourceData.activeColorTexture;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle backBufferColor = universalResourceData.backBufferColor;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle lutTexture = universalResourceData.internalColorLut;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture = universalResourceData.overlayUITexture;
				bool flag9 = universalCameraData.resolveFinalTarget && !flag2 && !flag4;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle postProcessingTarget;
				if (flag9)
				{
					postProcessingTarget = backBufferColor;
				}
				else
				{
					global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams importParams = new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
					{
						clearOnFirstUse = true,
						clearColor = global::UnityEngine.Color.black,
						discardOnLastUse = universalCameraData.resolveFinalTarget
					};
					if (!universalCameraData.IsSTPEnabled())
					{
						if (universalCameraData.IsTemporalAAEnabled())
						{
						}
						bool flag10 = universalCameraData.resolveFinalTarget && universalCameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base;
						universalResourceData.cameraColor = (flag10 ? renderGraph.CreateTexture(activeCameraColorTexture, "_CameraColorAfterPostProcessing") : renderGraph.ImportTexture(nextRenderGraphCameraColorHandle, importParams));
					}
					else
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = universalResourceData.cameraColor.GetDescriptor(renderGraph);
						global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.MakeCompatible(ref desc);
						desc.width = universalCameraData.pixelWidth;
						desc.height = universalCameraData.pixelHeight;
						desc.name = "_CameraColorUpscaled";
						universalResourceData.cameraColor = renderGraph.CreateTexture(in desc);
					}
					postProcessingTarget = universalResourceData.cameraColor;
				}
				if (flag8 && flag9)
				{
					postProcessingTarget = universalResourceData.debugScreenColor;
				}
				bool enableColorEndingIfNeeded = flag5 && flag6;
				m_PostProcessPassRenderGraph.RenderPostProcessingRenderGraph(renderGraph, base.frameData, in activeCameraColorTexture, in lutTexture, in overlayUITexture, in postProcessingTarget, flag2, flag8, enableColorEndingIfNeeded);
				if (universalCameraData.resolveFinalTarget)
				{
					SetupAfterPostRenderGraphFinalPassDebug(renderGraph, base.frameData);
				}
				if (flag9)
				{
					universalResourceData.SwitchActiveTexturesToBackbuffer();
				}
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingPostProcessing);
			if (universalCameraData.captureActions != null)
			{
				m_CapturePass.RecordRenderGraph(renderGraph, base.frameData);
			}
			if (flag2)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle backBufferColor2 = universalResourceData.backBufferColor;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture2 = universalResourceData.overlayUITexture;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle postProcessingTarget2 = backBufferColor2;
				if (flag8)
				{
					postProcessingTarget2 = universalResourceData.debugScreenColor;
				}
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source = universalResourceData.cameraColor;
				m_PostProcessPassRenderGraph.RenderFinalPassRenderGraph(renderGraph, base.frameData, in source, in overlayUITexture2, in postProcessingTarget2, flag6);
				universalResourceData.SwitchActiveTexturesToBackbuffer();
			}
			bool flag11 = flag2 || (applyPostProcessing && !flag4 && !flag3);
			if (!universalResourceData.isActiveTargetBackBuffer && universalCameraData.resolveFinalTarget && !flag11)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle backBufferColor3 = universalResourceData.backBufferColor;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture3 = universalResourceData.overlayUITexture;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dest = backBufferColor3;
				if (flag8)
				{
					dest = universalResourceData.debugScreenColor;
				}
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle src = universalResourceData.cameraColor;
				m_FinalBlitPass.Render(renderGraph, base.frameData, universalCameraData, in src, in dest, overlayUITexture3);
				universalResourceData.SwitchActiveTexturesToBackbuffer();
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRendering);
			bool num = universalCameraData.rendersOverlayUI && universalCameraData.isLastBaseCamera;
			bool isHDROutputActive = universalCameraData.isHDROutputActive;
			if (num && !isHDROutputActive)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthBuffer = universalResourceData.backBufferDepth;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorBuffer = universalResourceData.backBufferColor;
				if (flag8)
				{
					colorBuffer = universalResourceData.debugScreenColor;
					depthBuffer = universalResourceData.debugScreenDepth;
				}
				m_DrawOverlayUIPass.RenderOverlay(renderGraph, base.frameData, in colorBuffer, in depthBuffer);
			}
			if (universalCameraData.xr.enabled && !flag7 && universalCameraData.xr.copyDepth)
			{
				m_XRCopyDepthPass.CopyToDepthXR = true;
				m_XRCopyDepthPass.MsaaSamples = 1;
				m_XRCopyDepthPass.Render(renderGraph, base.frameData, universalResourceData.backBufferDepth, universalResourceData.cameraDepth, bindAsCameraDepth: false, "XR Depth Copy");
			}
			if (activeDebugHandler != null)
			{
				_ = universalResourceData.overlayUITexture;
				_ = universalResourceData.debugScreenColor;
			}
			if (universalCameraData.resolveFinalTarget)
			{
				if (universalCameraData.isSceneViewCamera)
				{
					DrawRenderGraphWireOverlay(renderGraph, base.frameData, universalResourceData.backBufferColor);
				}
				if (flag)
				{
					DrawRenderGraphGizmos(renderGraph, base.frameData, universalResourceData.backBufferColor, universalResourceData.activeDepthTexture, global::UnityEngine.Rendering.GizmoSubset.PostImageEffects);
				}
			}
		}

		private bool RequirePrepassForTextures(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary renderPassInputs, bool requireDepthTexture)
		{
			return (requireDepthTexture && !CanCopyDepth(cameraData)) | (cameraData.requiresDepthTexture && m_CopyDepthMode == global::UnityEngine.Rendering.Universal.CopyDepthMode.ForcePrepass) | renderPassInputs.requiresDepthPrepass | DebugHandlerRequireDepthPass(cameraData) | renderPassInputs.requiresNormalsTexture;
		}

		private static bool RequireDepthTexture(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary renderPassInputs, bool applyPostProcessing)
		{
			bool num = cameraData.requiresDepthTexture || renderPassInputs.requiresDepthTexture;
			bool flag = applyPostProcessing && cameraData.postProcessingRequiresDepthTexture;
			return num || flag;
		}

		private static bool IsDepthPrimingEnabledRenderGraph(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary renderPassInputs, global::UnityEngine.Rendering.Universal.DepthPrimingMode depthPrimingMode, bool requireDepthTexture, bool requirePrepassForTextures, bool usesDeferredLighting)
		{
			bool flag = true;
			if (requireDepthTexture && !CanCopyDepth(cameraData))
			{
				return false;
			}
			bool flag2 = !IsWebGL();
			bool num = (flag && depthPrimingMode == global::UnityEngine.Rendering.Universal.DepthPrimingMode.Auto) || depthPrimingMode == global::UnityEngine.Rendering.Universal.DepthPrimingMode.Forced;
			bool flag3 = cameraData.cameraTargetDescriptor.msaaSamples == 1;
			bool flag4 = cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base || cameraData.clearDepth;
			bool flag5 = !IsOffscreenDepthTexture(cameraData);
			return num && !usesDeferredLighting && flag4 && flag5 && flag2 && flag3;
		}

		internal void SetRenderingLayersGlobalTextures(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			if (universalResourceData.renderingLayersTexture.IsValid() && !usesDeferredLighting)
			{
				global::UnityEngine.Rendering.Universal.RenderGraphUtils.SetGlobalTexture(renderGraph, global::UnityEngine.Shader.PropertyToID(m_RenderingLayersTextureName), universalResourceData.renderingLayersTexture, "Set Global Rendering Layers Texture", ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\UniversalRendererRenderGraph.cs", 1683);
			}
		}

		private void ImportBackBuffers(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Color clearBackgroundColor, bool isCameraTargetOffscreenDepth)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			bool flag = cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base && !m_RequiresIntermediateAttachments;
			flag = flag || isCameraTargetOffscreenDepth;
			bool flag2 = !global::UnityEngine.Rendering.SupportedRenderingFeatures.active.rendersUIOverlay && cameraData.resolveToScreen;
			bool flag3 = global::UnityEngine.Rendering.Watermark.IsVisible() || flag2;
			bool discardOnLastUse = !m_RequiresIntermediateAttachments && !flag3 && cameraData.cameraTargetDescriptor.msaaSamples > 1;
			global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin textureUVOrigin = ((!cameraData.isSceneViewCamera && !cameraData.isPreviewCamera && cameraData.targetTexture == null) ? (global::UnityEngine.SystemInfo.graphicsUVStartsAtTop ? global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.TopLeft : global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft) : global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft);
			global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams importParams = new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
			{
				clearOnFirstUse = flag,
				clearColor = clearBackgroundColor,
				discardOnLastUse = discardOnLastUse,
				textureUVOrigin = textureUVOrigin
			};
			global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams importParams2 = new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
			{
				clearOnFirstUse = flag,
				clearColor = clearBackgroundColor,
				discardOnLastUse = !isCameraTargetOffscreenDepth,
				textureUVOrigin = textureUVOrigin
			};
			if (cameraData.xr.enabled && cameraData.xr.copyDepth)
			{
				importParams2.discardOnLastUse = false;
			}
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo = default(global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo);
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo2 = default(global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo);
			bool flag4 = cameraData.targetTexture == null;
			if (cameraData.xr.enabled)
			{
				flag4 = false;
			}
			if (flag4)
			{
				int msaaSamples = AdjustAndGetScreenMSAASamples(renderGraph, m_RequiresIntermediateAttachments);
				renderTargetInfo.width = global::UnityEngine.Screen.width;
				renderTargetInfo.height = global::UnityEngine.Screen.height;
				renderTargetInfo.volumeDepth = 1;
				renderTargetInfo.msaaSamples = msaaSamples;
				renderTargetInfo.format = cameraData.cameraTargetDescriptor.graphicsFormat;
				renderTargetInfo2 = renderTargetInfo;
				renderTargetInfo2.format = cameraData.cameraTargetDescriptor.depthStencilFormat;
			}
			else
			{
				if (cameraData.xr.enabled)
				{
					renderTargetInfo.width = cameraData.xr.renderTargetDesc.width;
					renderTargetInfo.height = cameraData.xr.renderTargetDesc.height;
					renderTargetInfo.volumeDepth = cameraData.xr.renderTargetDesc.volumeDepth;
					renderTargetInfo.msaaSamples = cameraData.xr.renderTargetDesc.msaaSamples;
					renderTargetInfo.format = cameraData.xr.renderTargetDesc.graphicsFormat;
					if (!PlatformRequiresExplicitMsaaResolve())
					{
						renderTargetInfo.bindMS = renderTargetInfo.msaaSamples > 1;
					}
					renderTargetInfo2 = renderTargetInfo;
					renderTargetInfo2.format = cameraData.xr.renderTargetDesc.depthStencilFormat;
				}
				else
				{
					renderTargetInfo.width = cameraData.targetTexture.width;
					renderTargetInfo.height = cameraData.targetTexture.height;
					renderTargetInfo.volumeDepth = cameraData.targetTexture.volumeDepth;
					renderTargetInfo.msaaSamples = cameraData.targetTexture.antiAliasing;
					renderTargetInfo.format = cameraData.targetTexture.graphicsFormat;
					renderTargetInfo2 = renderTargetInfo;
					renderTargetInfo2.format = cameraData.targetTexture.depthStencilFormat;
				}
				if (renderTargetInfo2.format == global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
				{
					renderTargetInfo2.format = global::UnityEngine.SystemInfo.GetGraphicsFormat(global::UnityEngine.Experimental.Rendering.DefaultFormat.DepthStencil);
					global::UnityEngine.Debug.LogWarning("In the render graph API, the output Render Texture must have a depth buffer. When you select a Render Texture in any camera's Output Texture property, the Depth Stencil Format property of the texture must be set to a value other than None.");
				}
			}
			if (!isCameraTargetOffscreenDepth)
			{
				universalResourceData.backBufferColor = renderGraph.ImportTexture(m_TargetColorHandle, renderTargetInfo, importParams);
			}
			universalResourceData.backBufferDepth = renderGraph.ImportTexture(m_TargetDepthHandle, renderTargetInfo2, importParams2);
		}

		private void CreateIntermediateCameraColorAttachment(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc cameraDescriptor, bool clearColor, global::UnityEngine.Color clearBackgroundColor)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = cameraDescriptor;
			desc.useMipMap = false;
			desc.autoGenerateMips = false;
			desc.filterMode = global::UnityEngine.FilterMode.Bilinear;
			desc.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
			if (cameraData.resolveFinalTarget && cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
			{
				universalResourceData.cameraColor = CreateRenderGraphTexture(renderGraph, in desc, "_CameraTargetAttachment", clearColor, clearBackgroundColor, desc.filterMode, global::UnityEngine.TextureWrapMode.Clamp, cameraData.resolveFinalTarget);
				m_CurrentColorHandle = -1;
			}
			else
			{
				global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_RenderGraphCameraColorHandles[0], desc, "_CameraTargetAttachmentA");
				global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_RenderGraphCameraColorHandles[1], desc, "_CameraTargetAttachmentB");
				if (cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
				{
					m_CurrentColorHandle = 0;
				}
				universalResourceData.cameraColor = renderGraph.ImportTexture(importParams: new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
				{
					clearOnFirstUse = clearColor,
					clearColor = clearBackgroundColor,
					discardOnLastUse = cameraData.resolveFinalTarget
				}, rt: currentRenderGraphCameraColorHandle);
			}
			universalResourceData.activeColorID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.Camera;
		}

		private void CreateIntermediateCameraDepthAttachment(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc cameraDescriptor, bool clearDepth, global::UnityEngine.Color clearBackgroundDepth, bool depthTextureIsDepthFormat)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = cameraDescriptor;
			desc.useMipMap = false;
			desc.autoGenerateMips = false;
			bool flag = desc.msaaSamples != global::UnityEngine.Rendering.MSAASamples.None;
			bool flag2 = global::UnityEngine.Rendering.Universal.RenderingUtils.MultisampleDepthResolveSupported() && renderGraph.nativeRenderPassesEnabled;
			desc.bindTextureMS = !flag2 && flag;
			if (IsGLESDevice())
			{
				desc.bindTextureMS = false;
			}
			desc.format = cameraDepthAttachmentFormat;
			desc.filterMode = global::UnityEngine.FilterMode.Point;
			desc.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
			bool resolveFinalTarget = cameraData.resolveFinalTarget;
			if (cameraData.resolveFinalTarget && cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
			{
				universalResourceData.cameraDepth = CreateRenderGraphTexture(renderGraph, in desc, "_CameraDepthAttachment", clearDepth, clearBackgroundDepth, desc.filterMode, desc.wrapMode, resolveFinalTarget);
			}
			else
			{
				global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_RenderGraphCameraDepthHandle, desc, "_CameraDepthAttachment");
				universalResourceData.cameraDepth = renderGraph.ImportTexture(importParams: new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
				{
					clearOnFirstUse = clearDepth,
					clearColor = clearBackgroundDepth,
					discardOnLastUse = resolveFinalTarget
				}, rt: m_RenderGraphCameraDepthHandle);
			}
			universalResourceData.activeDepthID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.Camera;
			m_CopyDepthPass.MsaaSamples = (int)desc.msaaSamples;
			m_CopyDepthPass.CopyToDepth = depthTextureIsDepthFormat;
			bool copyResolvedDepth = !desc.bindTextureMS;
			m_CopyDepthPass.m_CopyResolvedDepth = copyResolvedDepth;
			m_XRCopyDepthPass.m_CopyResolvedDepth = copyResolvedDepth;
		}

		private void CreateCameraDepthCopyTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor, bool isDepthTexture, global::UnityEngine.Color clearColor)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = descriptor;
			desc.msaaSamples = global::UnityEngine.Rendering.MSAASamples.None;
			if (isDepthTexture)
			{
				desc.format = cameraDepthTextureFormat;
				desc.clearBuffer = true;
			}
			else
			{
				desc.format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat;
				desc.clearBuffer = false;
			}
			universalResourceData.cameraDepthTexture = CreateRenderGraphTexture(renderGraph, in desc, "_CameraDepthTexture", desc.clearBuffer, clearColor);
		}

		private void CreateMotionVectorTextures(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			descriptor.msaaSamples = global::UnityEngine.Rendering.MSAASamples.None;
			descriptor.format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16_SFloat;
			universalResourceData.motionVectorColor = CreateRenderGraphTexture(renderGraph, in descriptor, "_MotionVectorTexture", clear: true, global::UnityEngine.Color.black);
			descriptor.format = cameraDepthAttachmentFormat;
			universalResourceData.motionVectorDepth = CreateRenderGraphTexture(renderGraph, in descriptor, "_MotionVectorDepthTexture", clear: true, global::UnityEngine.Color.black);
		}

		private void CreateCameraNormalsTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			descriptor.msaaSamples = global::UnityEngine.Rendering.MSAASamples.None;
			string name = ((!usesDeferredLighting) ? global::UnityEngine.Rendering.Universal.Internal.DepthNormalOnlyPass.k_CameraNormalsTextureName : global::UnityEngine.Rendering.Universal.Internal.DeferredLights.k_GBufferNames[m_DeferredLights.GBufferNormalSmoothnessIndex]);
			descriptor.format = ((!usesDeferredLighting) ? global::UnityEngine.Rendering.Universal.Internal.DepthNormalOnlyPass.GetGraphicsFormat() : m_DeferredLights.GetGBufferFormat(m_DeferredLights.GBufferNormalSmoothnessIndex));
			universalResourceData.cameraNormalsTexture = CreateRenderGraphTexture(renderGraph, in descriptor, name, clear: true, global::UnityEngine.Color.black);
		}

		private void CreateRenderingLayersTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor)
		{
			if (m_RequiresRenderingLayer)
			{
				global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
				m_RenderingLayersTextureName = "_CameraRenderingLayersTexture";
				if (usesDeferredLighting && m_DeferredLights.UseRenderingLayers)
				{
					m_RenderingLayersTextureName = global::UnityEngine.Rendering.Universal.Internal.DeferredLights.k_GBufferNames[m_DeferredLights.GBufferRenderingLayers];
				}
				if (!m_RenderingLayerProvidesRenderObjectPass)
				{
					descriptor.msaaSamples = global::UnityEngine.Rendering.MSAASamples.None;
				}
				if (usesDeferredLighting && m_RequiresRenderingLayer)
				{
					descriptor.format = m_DeferredLights.GetGBufferFormat(m_DeferredLights.GBufferRenderingLayers);
				}
				else
				{
					descriptor.format = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.GetFormat(m_RenderingLayersMaskSize);
				}
				universalResourceData.renderingLayersTexture = CreateRenderGraphTexture(renderGraph, in descriptor, m_RenderingLayersTextureName, clear: true, descriptor.clearColor);
			}
		}

		private void CreateAfterPostProcessTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.RenderTextureDescriptor descriptor)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.RenderTextureDescriptor compatibleDescriptor = global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.GetCompatibleDescriptor(descriptor, descriptor.width, descriptor.height, descriptor.graphicsFormat);
			universalResourceData.afterPostProcessColor = CreateRenderGraphTexture(renderGraph, compatibleDescriptor, "_AfterPostProcessTexture", clear: true);
		}

		private void CreateOffscreenUITexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.ConfigureOffscreenUITextureDesc(ref descriptor);
			global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_OffscreenUIColorHandle, descriptor, "_OverlayUITexture");
			universalResourceData.overlayUITexture = renderGraph.ImportTexture(m_OffscreenUIColorHandle);
		}

		private void DepthNormalPrepassRender(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalRenderer.RenderPassInputSummary renderPassInputs, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTarget, uint batchLayerMask, bool setGlobalDepth, bool setGlobalTextures, bool partialPass)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			if (m_RenderingLayerProvidesByDepthNormalPass)
			{
				m_DepthNormalPrepass.enableRenderingLayers = true;
				m_DepthNormalPrepass.renderingLayersMaskSize = m_RenderingLayersMaskSize;
			}
			else
			{
				m_DepthNormalPrepass.enableRenderingLayers = false;
			}
			m_DepthNormalPrepass.Render(renderGraph, base.frameData, universalResourceData.cameraNormalsTexture, in depthTarget, universalResourceData.renderingLayersTexture, batchLayerMask, setGlobalDepth, setGlobalTextures, partialPass);
			if (m_RequiresRenderingLayer)
			{
				SetRenderingLayersGlobalTextures(renderGraph);
			}
		}
	}
}
