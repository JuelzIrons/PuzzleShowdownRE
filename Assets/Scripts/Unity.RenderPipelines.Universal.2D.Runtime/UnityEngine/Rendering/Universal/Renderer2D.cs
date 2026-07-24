namespace UnityEngine.Rendering.Universal
{
	internal sealed class Renderer2D : global::UnityEngine.Rendering.Universal.ScriptableRenderer
	{
		private struct RenderPassInputSummary
		{
			internal bool requiresDepthTexture;

			internal bool requiresColorTexture;
		}

		private struct ImportResourceSummary
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo importInfo;

			internal global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo importInfoDepth;

			internal global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams cameraColorParams;

			internal global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams cameraDepthParams;

			internal global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams backBufferColorParams;

			internal global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams backBufferDepthParams;
		}

		private const int k_FinalBlitPassQueueOffset = 1;

		private const int k_AfterFinalBlitPassQueueOffset = 2;

		private static int m_CurrentColorHandle;

		internal global::UnityEngine.Rendering.RTHandle[] m_RenderGraphCameraColorHandles = new global::UnityEngine.Rendering.RTHandle[2];

		internal global::UnityEngine.Rendering.RTHandle m_RenderGraphCameraDepthHandle;

		private global::UnityEngine.Rendering.RTHandle m_RenderGraphBackbufferColorHandle;

		private global::UnityEngine.Rendering.RTHandle m_RenderGraphBackbufferDepthHandle;

		private global::UnityEngine.Rendering.RTHandle m_CameraSortingLayerHandle;

		private static global::UnityEngine.Rendering.RTHandle m_OffscreenUIColorHandle;

		private global::UnityEngine.Material m_BlitMaterial;

		private global::UnityEngine.Material m_BlitHDRMaterial;

		private global::UnityEngine.Material m_BlitOffscreenUICoverMaterial;

		private global::UnityEngine.Material m_SamplingMaterial;

		private global::UnityEngine.Rendering.Universal.DrawNormal2DPass m_NormalPass = new global::UnityEngine.Rendering.Universal.DrawNormal2DPass();

		private global::UnityEngine.Rendering.Universal.DrawLight2DPass m_LightPass = new global::UnityEngine.Rendering.Universal.DrawLight2DPass();

		private global::UnityEngine.Rendering.Universal.DrawShadow2DPass m_ShadowPass = new global::UnityEngine.Rendering.Universal.DrawShadow2DPass();

		private global::UnityEngine.Rendering.Universal.DrawRenderer2DPass m_RendererPass = new global::UnityEngine.Rendering.Universal.DrawRenderer2DPass();

		private global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass m_CopyDepthPass;

		private global::UnityEngine.Rendering.Universal.UpscalePass m_UpscalePass;

		private global::UnityEngine.Rendering.Universal.CopyCameraSortingLayerPass m_CopyCameraSortingLayerPass;

		private global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass m_FinalBlitPass;

		private global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass m_OffscreenUICoverPrepass;

		private global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass m_DrawOffscreenUIPass;

		private global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass m_DrawOverlayUIPass;

		private global::UnityEngine.Rendering.Universal.Renderer2DData m_Renderer2DData;

		private global::UnityEngine.Rendering.Universal.LayerBatch[] m_LayerBatches;

		private int m_BatchCount;

		internal bool m_CreateColorTexture;

		internal bool m_CreateDepthTexture;

		private bool ppcUpscaleRT;

		private global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph m_PostProcessPassRenderGraph;

		private global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass m_ColorGradingLutPassRenderGraph;

		private global::UnityEngine.Rendering.RTHandle currentRenderGraphCameraColorHandle => m_RenderGraphCameraColorHandles[m_CurrentColorHandle];

		private global::UnityEngine.Rendering.RTHandle nextRenderGraphCameraColorHandle
		{
			get
			{
				m_CurrentColorHandle = (m_CurrentColorHandle + 1) % 2;
				return currentRenderGraphCameraColorHandle;
			}
		}

		internal static bool supportsMRT => !IsGLESDevice();

		internal override bool supportsNativeRenderPassRendergraphCompiler => true;

		public override int SupportedCameraStackingTypes()
		{
			return 3;
		}

		public Renderer2D(global::UnityEngine.Rendering.Universal.Renderer2DData data)
			: base(data)
		{
			if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeShaders>(out var settings))
			{
				m_BlitMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.coreBlitPS);
				m_BlitHDRMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.blitHDROverlay);
				m_BlitOffscreenUICoverMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.blitHDROverlay);
				m_SamplingMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.samplingPS);
			}
			if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.Renderer2DResources>(out var settings2))
			{
				m_CopyDepthPass = new global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingTransparents, settings2.copyDepthPS, shouldClear: true, copyToDepth: false, global::UnityEngine.Rendering.Universal.RenderingUtils.MultisampleDepthResolveSupported());
			}
			m_UpscalePass = new global::UnityEngine.Rendering.Universal.UpscalePass(global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingPostProcessing, m_BlitMaterial);
			m_CopyCameraSortingLayerPass = new global::UnityEngine.Rendering.Universal.CopyCameraSortingLayerPass(m_BlitMaterial);
			m_FinalBlitPass = new global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass((global::UnityEngine.Rendering.Universal.RenderPassEvent)1001, m_BlitMaterial, m_BlitHDRMaterial);
			m_OffscreenUICoverPrepass = new global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing, m_BlitMaterial, m_BlitOffscreenUICoverMaterial);
			m_DrawOffscreenUIPass = new global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing, renderOffscreen: true);
			m_DrawOverlayUIPass = new global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass((global::UnityEngine.Rendering.Universal.RenderPassEvent)1002, renderOffscreen: false);
			m_Renderer2DData = data;
			m_Renderer2DData.lightCullResult = new global::UnityEngine.Rendering.Universal.Light2DCullResult();
			base.supportedRenderingFeatures = new global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderingFeatures();
			global::UnityEngine.Rendering.LensFlareCommonSRP.mergeNeeded = 0;
			global::UnityEngine.Rendering.LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample = 1;
			global::UnityEngine.Rendering.LensFlareCommonSRP.Initialize();
			global::UnityEngine.Rendering.Universal.Light2DManager.Initialize();
			if (data.postProcessData != null)
			{
				m_PostProcessPassRenderGraph = new global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph(data.postProcessData, global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32);
				m_ColorGradingLutPassRenderGraph = new global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses, data.postProcessData);
			}
			global::UnityEngine.Rendering.Universal.PlatformAutoDetect.Initialize();
			if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeXRResources>(out var settings3))
			{
				global::UnityEngine.Experimental.Rendering.XRSystem.Initialize(global::UnityEngine.Rendering.Universal.XRPassUniversal.Create, settings3.xrOcclusionMeshPS, settings3.xrMirrorViewPS);
			}
		}

		internal static bool IsDepthUsageAllowed(global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			bool flag = rendererData.useDepthStencilBuffer;
			if (universalCameraData.targetTexture != null)
			{
				flag &= universalCameraData.targetTexture.dimension != global::UnityEngine.Rendering.TextureDimension.Tex3D;
			}
			return flag;
		}

		private bool IsPixelPerfectCameraEnabled(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, out global::UnityEngine.Rendering.Universal.PixelPerfectCamera ppc)
		{
			ppc = null;
			if (cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base && cameraData.resolveFinalTarget)
			{
				cameraData.camera.TryGetComponent<global::UnityEngine.Rendering.Universal.PixelPerfectCamera>(out ppc);
			}
			if (ppc != null)
			{
				return ppc.enabled;
			}
			return false;
		}

		private global::UnityEngine.Rendering.Universal.Renderer2D.RenderPassInputSummary GetRenderPassInputs(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.Universal.Renderer2D.RenderPassInputSummary result = default(global::UnityEngine.Rendering.Universal.Renderer2D.RenderPassInputSummary);
			for (int i = 0; i < base.activeRenderPassQueue.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.ScriptableRenderPass scriptableRenderPass = base.activeRenderPassQueue[i];
				bool flag = (scriptableRenderPass.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth) != 0;
				bool flag2 = (scriptableRenderPass.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Color) != 0;
				result.requiresDepthTexture |= flag;
				result.requiresColorTexture |= flag2;
			}
			result.requiresColorTexture |= cameraData.postProcessEnabled || cameraData.isHdrEnabled || cameraData.isSceneViewCamera || !cameraData.isDefaultViewport || cameraData.requireSrgbConversion || !cameraData.resolveFinalTarget || (cameraData.cameraTargetDescriptor.msaaSamples > 1 && global::UnityEngine.Rendering.Universal.UniversalRenderer.PlatformRequiresExplicitMsaaResolve()) || m_Renderer2DData.useCameraSortingLayerTexture || !global::UnityEngine.Mathf.Approximately(cameraData.renderScale, 1f) || (base.DebugHandler != null && base.DebugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget));
			return result;
		}

		private global::UnityEngine.Rendering.Universal.Renderer2D.ImportResourceSummary GetImportResourceSummary(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.Universal.Renderer2D.ImportResourceSummary result = default(global::UnityEngine.Rendering.Universal.Renderer2D.ImportResourceSummary);
			bool clearOnFirstUse = cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base;
			bool clearOnFirstUse2 = cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base || cameraData.clearDepth;
			global::UnityEngine.Rendering.Universal.PixelPerfectCamera ppc;
			bool flag = IsPixelPerfectCameraEnabled(cameraData, out ppc) && ppc.cropFrame != global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.None;
			bool clearOnFirstUse3 = cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base && (!m_CreateColorTexture || flag);
			bool clearOnFirstUse4 = cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base && !m_CreateColorTexture;
			global::UnityEngine.Color color = ((cameraData.camera.clearFlags == global::UnityEngine.CameraClearFlags.Nothing) ? global::UnityEngine.Color.yellow : cameraData.backgroundColor);
			global::UnityEngine.Color clearColor = (flag ? global::UnityEngine.Color.black : color);
			if (IsSceneFilteringEnabled(cameraData.camera))
			{
				color.a = 0f;
				clearOnFirstUse2 = false;
			}
			global::UnityEngine.Rendering.Universal.DebugHandler debugHandler = cameraData.renderer.DebugHandler;
			if (debugHandler != null && debugHandler.IsActiveForCamera(cameraData.isPreviewCamera) && debugHandler.IsScreenClearNeeded)
			{
				clearOnFirstUse = true;
				clearOnFirstUse2 = true;
				debugHandler.TryGetScreenClearColor(ref color);
			}
			result.cameraColorParams.clearOnFirstUse = clearOnFirstUse;
			result.cameraColorParams.clearColor = color;
			result.cameraColorParams.discardOnLastUse = false;
			result.cameraDepthParams.clearOnFirstUse = clearOnFirstUse2;
			result.cameraDepthParams.clearColor = color;
			result.cameraDepthParams.discardOnLastUse = false;
			result.backBufferColorParams.clearOnFirstUse = clearOnFirstUse3;
			result.backBufferColorParams.clearColor = clearColor;
			result.backBufferColorParams.discardOnLastUse = false;
			result.backBufferDepthParams.clearOnFirstUse = clearOnFirstUse4;
			result.backBufferDepthParams.clearColor = clearColor;
			result.backBufferDepthParams.discardOnLastUse = true;
			bool flag2 = cameraData.targetTexture == null;
			global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin textureUVOrigin = ((!cameraData.isSceneViewCamera && !cameraData.isPreviewCamera && cameraData.targetTexture == null) ? (global::UnityEngine.SystemInfo.graphicsUVStartsAtTop ? global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.TopLeft : global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft) : global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft);
			result.backBufferColorParams.textureUVOrigin = textureUVOrigin;
			result.backBufferDepthParams.textureUVOrigin = textureUVOrigin;
			if (cameraData.xr.enabled)
			{
				flag2 = false;
			}
			if (!flag2)
			{
				if (cameraData.xr.enabled)
				{
					result.importInfo.width = cameraData.xr.renderTargetDesc.width;
					result.importInfo.height = cameraData.xr.renderTargetDesc.height;
					result.importInfo.volumeDepth = cameraData.xr.renderTargetDesc.volumeDepth;
					result.importInfo.msaaSamples = cameraData.xr.renderTargetDesc.msaaSamples;
					result.importInfo.format = cameraData.xr.renderTargetDesc.graphicsFormat;
					if (!global::UnityEngine.Rendering.Universal.UniversalRenderer.PlatformRequiresExplicitMsaaResolve())
					{
						result.importInfo.bindMS = result.importInfo.msaaSamples > 1;
					}
					result.importInfoDepth = result.importInfo;
					result.importInfoDepth.format = cameraData.xr.renderTargetDesc.depthStencilFormat;
				}
				else
				{
					result.importInfo.width = cameraData.targetTexture.width;
					result.importInfo.height = cameraData.targetTexture.height;
					result.importInfo.volumeDepth = cameraData.targetTexture.volumeDepth;
					result.importInfo.msaaSamples = cameraData.targetTexture.antiAliasing;
					result.importInfo.format = cameraData.targetTexture.graphicsFormat;
					result.importInfoDepth = result.importInfo;
					result.importInfoDepth.format = cameraData.targetTexture.depthStencilFormat;
					if (result.importInfoDepth.format == global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
					{
						result.importInfoDepth.format = global::UnityEngine.SystemInfo.GetGraphicsFormat(global::UnityEngine.Experimental.Rendering.DefaultFormat.DepthStencil);
						global::UnityEngine.Debug.LogWarning("In the render graph API, the output Render Texture must have a depth buffer. When you select a Render Texture in any camera's Output Texture property, the Depth Stencil Format property of the texture must be set to a value other than None.");
					}
				}
			}
			else
			{
				int msaaSamples = AdjustAndGetScreenMSAASamples(renderGraph, m_CreateColorTexture);
				result.importInfo.width = global::UnityEngine.Screen.width;
				result.importInfo.height = global::UnityEngine.Screen.height;
				result.importInfo.volumeDepth = 1;
				result.importInfo.msaaSamples = msaaSamples;
				result.importInfo.format = cameraData.cameraTargetDescriptor.graphicsFormat;
				result.importInfoDepth = result.importInfo;
				result.importInfoDepth.format = global::UnityEngine.SystemInfo.GetGraphicsFormat(global::UnityEngine.Experimental.Rendering.DefaultFormat.DepthStencil);
			}
			return result;
		}

		public override void SetupCullingParameters(ref global::UnityEngine.Rendering.ScriptableCullingParameters cullingParameters, ref global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			cullingParameters.cullingOptions = global::UnityEngine.Rendering.CullingOptions.None;
			cullingParameters.isOrthographic = cameraData.camera.orthographic;
			cullingParameters.shadowDistance = 0f;
			(m_Renderer2DData.lightCullResult as global::UnityEngine.Rendering.Universal.Light2DCullResult).SetupCulling(ref cullingParameters, cameraData.camera);
		}

		private void InitializeLayerBatches()
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			m_LayerBatches = global::UnityEngine.Rendering.Universal.LayerUtility.CalculateBatches(m_Renderer2DData, out m_BatchCount);
			if (universal2DResourceData.normalsTexture.Length != m_BatchCount)
			{
				universal2DResourceData.normalsTexture = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[m_BatchCount];
			}
			if (universal2DResourceData.shadowTextures.Length != m_BatchCount)
			{
				universal2DResourceData.shadowTextures = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[m_BatchCount][];
			}
			if (universal2DResourceData.lightTextures.Length != m_BatchCount)
			{
				universal2DResourceData.lightTextures = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[m_BatchCount][];
			}
			for (int i = 0; i < universal2DResourceData.lightTextures.Length; i++)
			{
				if (universal2DResourceData.lightTextures[i] == null || universal2DResourceData.lightTextures[i].Length != m_LayerBatches[i].activeBlendStylesIndices.Length)
				{
					universal2DResourceData.lightTextures[i] = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[m_LayerBatches[i].activeBlendStylesIndices.Length];
				}
			}
			for (int j = 0; j < universal2DResourceData.shadowTextures.Length; j++)
			{
				if (universal2DResourceData.shadowTextures[j] == null || universal2DResourceData.shadowTextures[j].Length != m_LayerBatches[j].shadowIndices.Count)
				{
					universal2DResourceData.shadowTextures[j] = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[m_LayerBatches[j].shadowIndices.Count];
				}
			}
		}

		private void CreateResources(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			ref global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor = ref universalCameraData.cameraTargetDescriptor;
			global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Bilinear;
			bool resolveFinalTarget = universalCameraData.resolveFinalTarget;
			bool flag = false;
			if (universalCameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base && resolveFinalTarget)
			{
				universalCameraData.camera.TryGetComponent<global::UnityEngine.Rendering.Universal.PixelPerfectCamera>(out var component);
				if (component != null && component.enabled)
				{
					if (component.offscreenRTSize != global::UnityEngine.Vector2Int.zero)
					{
						flag = true;
						cameraTargetDescriptor.width = component.offscreenRTSize.x;
						cameraTargetDescriptor.height = component.offscreenRTSize.y;
					}
					filterMode = global::UnityEngine.FilterMode.Point;
					ppcUpscaleRT = component.gridSnapping == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping.UpscaleRenderTexture || component.requiresUpscalePass;
					if (component.requiresUpscalePass)
					{
						global::UnityEngine.RenderTextureDescriptor desc = cameraTargetDescriptor;
						desc.width = component.refResolutionX * component.pixelRatio;
						desc.height = component.refResolutionY * component.pixelRatio;
						desc.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
						universal2DResourceData.upscaleTexture = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_UpscaleTexture", clear: true, component.finalBlitFilterMode);
					}
				}
			}
			float lightRenderTextureScale = m_Renderer2DData.lightRenderTextureScale;
			int width = (int)global::UnityEngine.Mathf.Max(1f, (float)universalCameraData.cameraTargetDescriptor.width * lightRenderTextureScale);
			int height = (int)global::UnityEngine.Mathf.Max(1f, (float)universalCameraData.cameraTargetDescriptor.height * lightRenderTextureScale);
			CreateCameraNormalsTextures(renderGraph, cameraTargetDescriptor, width, height);
			CreateLightTextures(renderGraph, width, height);
			CreateShadowTextures(renderGraph, width, height);
			if (m_Renderer2DData.useCameraSortingLayerTexture)
			{
				CreateCameraSortingLayerTexture(renderGraph, cameraTargetDescriptor);
			}
			if (universalCameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
			{
				global::UnityEngine.Rendering.Universal.Renderer2D.RenderPassInputSummary renderPassInputs = GetRenderPassInputs(universalCameraData);
				m_CreateColorTexture = renderPassInputs.requiresColorTexture;
				m_CreateDepthTexture = renderPassInputs.requiresDepthTexture;
				m_CreateColorTexture |= flag;
				m_CreateDepthTexture |= m_CreateColorTexture;
				if (m_CreateColorTexture)
				{
					cameraTargetDescriptor.useMipMap = false;
					cameraTargetDescriptor.autoGenerateMips = false;
					cameraTargetDescriptor.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
					global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_RenderGraphCameraColorHandles[0], in cameraTargetDescriptor, filterMode, global::UnityEngine.TextureWrapMode.Clamp, 1, 0f, "_CameraTargetAttachmentA");
					global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_RenderGraphCameraColorHandles[1], in cameraTargetDescriptor, filterMode, global::UnityEngine.TextureWrapMode.Clamp, 1, 0f, "_CameraTargetAttachmentB");
					universalResourceData.activeColorID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.Camera;
				}
				else
				{
					universalResourceData.activeColorID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
				}
				if (m_CreateDepthTexture)
				{
					global::UnityEngine.RenderTextureDescriptor descriptor = universalCameraData.cameraTargetDescriptor;
					descriptor.useMipMap = false;
					descriptor.autoGenerateMips = false;
					bool flag2 = descriptor.msaaSamples > 1 && global::UnityEngine.SystemInfo.supportsMultisampledTextures != 0;
					bool flag3 = global::UnityEngine.Rendering.Universal.RenderingUtils.MultisampleDepthResolveSupported() && renderGraph.nativeRenderPassesEnabled;
					descriptor.bindMS = !flag3 && flag2;
					if (IsGLESDevice())
					{
						descriptor.bindMS = false;
					}
					if (m_CopyDepthPass != null)
					{
						m_CopyDepthPass.MsaaSamples = descriptor.msaaSamples;
						m_CopyDepthPass.m_CopyResolvedDepth = !descriptor.bindMS;
					}
					descriptor.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
					descriptor.depthStencilFormat = global::UnityEngine.Rendering.CoreUtils.GetDefaultDepthStencilFormat();
					global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_RenderGraphCameraDepthHandle, in descriptor, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Clamp, 1, 0f, "_CameraDepthAttachment");
					universalResourceData.activeDepthID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.Camera;
				}
				else
				{
					universalResourceData.activeDepthID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
				}
			}
			else
			{
				universalCameraData.baseCamera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component2);
				global::UnityEngine.Rendering.Universal.Renderer2D renderer2D = (global::UnityEngine.Rendering.Universal.Renderer2D)component2.scriptableRenderer;
				m_RenderGraphCameraColorHandles = renderer2D.m_RenderGraphCameraColorHandles;
				m_RenderGraphCameraDepthHandle = renderer2D.m_RenderGraphCameraDepthHandle;
				m_RenderGraphBackbufferColorHandle = renderer2D.m_RenderGraphBackbufferColorHandle;
				m_RenderGraphBackbufferDepthHandle = renderer2D.m_RenderGraphBackbufferDepthHandle;
				m_CreateColorTexture = renderer2D.m_CreateColorTexture;
				m_CreateDepthTexture = renderer2D.m_CreateDepthTexture;
				universalResourceData.activeColorID = ((!m_CreateColorTexture) ? global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer : global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.Camera);
				universalResourceData.activeDepthID = ((!m_CreateDepthTexture) ? global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer : global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.Camera);
			}
			global::UnityEngine.Rendering.Universal.Renderer2D.ImportResourceSummary importResourceSummary = GetImportResourceSummary(renderGraph, universalCameraData);
			if (m_CreateColorTexture)
			{
				importResourceSummary.cameraColorParams.discardOnLastUse = resolveFinalTarget;
				importResourceSummary.cameraDepthParams.discardOnLastUse = resolveFinalTarget;
				universalResourceData.cameraColor = renderGraph.ImportTexture(currentRenderGraphCameraColorHandle, importResourceSummary.cameraColorParams);
				universalResourceData.cameraDepth = renderGraph.ImportTexture(m_RenderGraphCameraDepthHandle, importResourceSummary.cameraDepthParams);
			}
			global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier = ((universalCameraData.targetTexture != null) ? new global::UnityEngine.Rendering.RenderTargetIdentifier(universalCameraData.targetTexture) : ((global::UnityEngine.Rendering.RenderTargetIdentifier)global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget));
			global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier2 = ((universalCameraData.targetTexture != null) ? new global::UnityEngine.Rendering.RenderTargetIdentifier(universalCameraData.targetTexture) : ((global::UnityEngine.Rendering.RenderTargetIdentifier)global::UnityEngine.Rendering.BuiltinRenderTextureType.Depth));
			if (universalCameraData.xr.enabled)
			{
				renderTargetIdentifier = universalCameraData.xr.renderTarget;
				renderTargetIdentifier2 = universalCameraData.xr.renderTarget;
			}
			if (m_RenderGraphBackbufferColorHandle == null)
			{
				m_RenderGraphBackbufferColorHandle = global::UnityEngine.Rendering.RTHandles.Alloc(renderTargetIdentifier, "Backbuffer color");
			}
			else if (m_RenderGraphBackbufferColorHandle.nameID != renderTargetIdentifier)
			{
				global::UnityEngine.Rendering.RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref m_RenderGraphBackbufferColorHandle, renderTargetIdentifier);
			}
			if (m_RenderGraphBackbufferDepthHandle == null)
			{
				m_RenderGraphBackbufferDepthHandle = global::UnityEngine.Rendering.RTHandles.Alloc(renderTargetIdentifier2, "Backbuffer depth");
			}
			else if (m_RenderGraphBackbufferDepthHandle.nameID != renderTargetIdentifier2)
			{
				global::UnityEngine.Rendering.RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref m_RenderGraphBackbufferDepthHandle, renderTargetIdentifier2);
			}
			universalResourceData.backBufferColor = renderGraph.ImportTexture(m_RenderGraphBackbufferColorHandle, importResourceSummary.importInfo, importResourceSummary.backBufferColorParams);
			universalResourceData.backBufferDepth = renderGraph.ImportTexture(m_RenderGraphBackbufferDepthHandle, importResourceSummary.importInfoDepth, importResourceSummary.backBufferDepthParams);
			global::UnityEngine.RenderTextureDescriptor compatibleDescriptor = global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.GetCompatibleDescriptor(cameraTargetDescriptor, cameraTargetDescriptor.width, cameraTargetDescriptor.height, cameraTargetDescriptor.graphicsFormat);
			universalResourceData.afterPostProcessColor = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, compatibleDescriptor, "_AfterPostProcessTexture", clear: true);
			if (RequiresDepthCopyPass(universalCameraData))
			{
				CreateCameraDepthCopyTexture(renderGraph, cameraTargetDescriptor);
			}
			if (universalCameraData.isHDROutputActive && universalCameraData.rendersOverlayUI)
			{
				CreateOffscreenUITexture(renderGraph, in cameraTargetDescriptor);
			}
		}

		private void CreateCameraNormalsTextures(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.RenderTextureDescriptor descriptor, int width, int height)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.RenderTextureDescriptor desc = new global::UnityEngine.RenderTextureDescriptor(width, height);
			desc.graphicsFormat = global::UnityEngine.Rendering.Universal.RendererLighting.GetRenderTextureFormat();
			desc.autoGenerateMips = false;
			desc.msaaSamples = descriptor.msaaSamples;
			for (int i = 0; i < universal2DResourceData.normalsTexture.Length; i++)
			{
				universal2DResourceData.normalsTexture[i] = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, in desc, "_NormalMap", clear: true, global::UnityEngine.Rendering.Universal.RendererLighting.k_NormalClearColor);
			}
			if (IsDepthUsageAllowed(base.frameData, m_Renderer2DData))
			{
				global::UnityEngine.RenderTextureDescriptor desc2 = new global::UnityEngine.RenderTextureDescriptor(width, height);
				desc2.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				desc2.autoGenerateMips = false;
				desc2.msaaSamples = descriptor.msaaSamples;
				desc2.depthStencilFormat = global::UnityEngine.Rendering.CoreUtils.GetDefaultDepthStencilFormat();
				universal2DResourceData.normalsDepth = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc2, "_NormalDepth", clear: false, global::UnityEngine.FilterMode.Bilinear);
			}
		}

		private void CreateLightTextures(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, int width, int height)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.RenderTextureDescriptor desc = new global::UnityEngine.RenderTextureDescriptor(width, height);
			desc.graphicsFormat = global::UnityEngine.Rendering.Universal.RendererLighting.GetRenderTextureFormat();
			desc.autoGenerateMips = false;
			for (int i = 0; i < universal2DResourceData.lightTextures.Length; i++)
			{
				for (int j = 0; j < m_LayerBatches[i].activeBlendStylesIndices.Length; j++)
				{
					int num = m_LayerBatches[i].activeBlendStylesIndices[j];
					if (!global::UnityEngine.Rendering.Universal.Light2DManager.GetGlobalColor(m_LayerBatches[i].startLayerID, num, out var color))
					{
						color = global::UnityEngine.Color.black;
					}
					universal2DResourceData.lightTextures[i][j] = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, in desc, global::UnityEngine.Rendering.Universal.RendererLighting.k_ShapeLightTextureIDs[num], clear: true, color, global::UnityEngine.FilterMode.Bilinear);
				}
			}
		}

		private void CreateShadowTextures(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, int width, int height)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.RenderTextureDescriptor desc = new global::UnityEngine.RenderTextureDescriptor(width, height);
			desc.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32;
			desc.autoGenerateMips = false;
			for (int i = 0; i < universal2DResourceData.shadowTextures.Length; i++)
			{
				for (int j = 0; j < m_LayerBatches[i].shadowIndices.Count; j++)
				{
					universal2DResourceData.shadowTextures[i][j] = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_ShadowTex", clear: false, global::UnityEngine.FilterMode.Bilinear);
				}
			}
			global::UnityEngine.RenderTextureDescriptor desc2 = new global::UnityEngine.RenderTextureDescriptor(width, height);
			desc2.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			desc2.autoGenerateMips = false;
			desc2.depthStencilFormat = global::UnityEngine.Rendering.CoreUtils.GetDefaultDepthStencilFormat();
			universal2DResourceData.shadowDepth = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc2, "_ShadowDepth", clear: false, global::UnityEngine.FilterMode.Bilinear);
		}

		private void CreateCameraSortingLayerTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.RenderTextureDescriptor descriptor)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			descriptor.msaaSamples = 1;
			global::UnityEngine.Rendering.Universal.CopyCameraSortingLayerPass.ConfigureDescriptor(m_Renderer2DData.cameraSortingLayerDownsamplingMethod, ref descriptor, out var filterMode);
			global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_CameraSortingLayerHandle, in descriptor, filterMode, global::UnityEngine.TextureWrapMode.Clamp, 1, 0f, global::UnityEngine.Rendering.Universal.CopyCameraSortingLayerPass.k_CameraSortingLayerTexture);
			universal2DResourceData.cameraSortingLayerTexture = renderGraph.ImportTexture(m_CameraSortingLayerHandle);
		}

		private bool RequiresDepthCopyPass(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.Universal.Renderer2D.RenderPassInputSummary renderPassInputs = GetRenderPassInputs(cameraData);
			bool flag = cameraData.requiresDepthTexture || renderPassInputs.requiresDepthTexture;
			if ((cameraData.postProcessEnabled && m_PostProcessPassRenderGraph != null && cameraData.postProcessingRequiresDepthTexture) || flag)
			{
				return m_CreateDepthTexture;
			}
			return false;
		}

		private void CreateCameraDepthCopyTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.RenderTextureDescriptor descriptor)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.RenderTextureDescriptor desc = descriptor;
			desc.msaaSamples = 1;
			desc.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat;
			desc.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			universalResourceData.cameraDepthTexture = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_CameraDepthTexture", clear: true);
		}

		private void CreateOffscreenUITexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.RenderTextureDescriptor descriptor)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureDesc = new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(descriptor);
			global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.ConfigureOffscreenUITextureDesc(ref textureDesc);
			global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_OffscreenUIColorHandle, textureDesc, "_OverlayUITexture");
			universalResourceData.overlayUITexture = renderGraph.ImportTexture(m_OffscreenUIColorHandle);
		}

		public override void OnBeginRenderGraphFrame()
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = base.frameData.Create<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData orCreate = base.frameData.GetOrCreate<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			universal2DResourceData.InitFrame();
			orCreate.InitFrame();
		}

		internal void RecordCustomRenderGraphPasses(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent2D activeRPEvent)
		{
			foreach (global::UnityEngine.Rendering.Universal.ScriptableRenderPass item in base.activeRenderPassQueue)
			{
				item.GetInjectionPoint2D(out var rpEvent, out var _);
				if (rpEvent == activeRPEvent)
				{
					item.RecordRenderGraph(renderGraph, base.frameData);
				}
			}
		}

		internal override void OnRecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ScriptableRenderContext context)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData orCreate = base.frameData.GetOrCreate<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			InitializeLayerBatches();
			CreateResources(renderGraph);
			SetupRenderGraphCameraProperties(renderGraph, orCreate.activeColorTexture);
			OnBeforeRendering(renderGraph);
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent2D.BeforeRendering);
			BeginRenderGraphXRRendering(renderGraph);
			OnMainRendering(renderGraph);
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent2D.BeforeRenderingPostProcessing);
			OnAfterRendering(renderGraph);
			EndRenderGraphXRRendering(renderGraph);
		}

		public override void OnEndRenderGraphFrame()
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			universal2DResourceData.EndFrame();
			universalResourceData.EndFrame();
		}

		internal override void OnFinishRenderGraphRendering(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			m_CopyDepthPass?.OnCameraCleanup(cmd);
		}

		private void OnBeforeRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			m_LightPass.Setup(renderGraph, ref m_Renderer2DData);
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D> visibleLights = m_Renderer2DData.lightCullResult.visibleLights;
			for (int i = 0; i < visibleLights.Count; i++)
			{
				visibleLights[i].CacheValues();
			}
			global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.CacheValues();
			global::UnityEngine.Rendering.Universal.ShadowRendering.CallOnBeforeRender(universalCameraData.camera, m_Renderer2DData.lightCullResult);
			global::UnityEngine.Rendering.Universal.RendererLighting.lightBatch.Reset();
		}

		private void OnMainRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			base.frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			if (universalCameraData.postProcessEnabled && m_PostProcessPassRenderGraph != null)
			{
				m_ColorGradingLutPassRenderGraph.Render(renderGraph, base.frameData, out var internalColorLut);
				universalResourceData.internalColorLut = internalColorLut;
			}
			short cameraSortingLayerBoundsIndex = m_Renderer2DData.GetCameraSortingLayerBoundsIndex();
			bool flag = false;
			for (int i = 0; i < m_BatchCount; i++)
			{
				flag |= m_LayerBatches[i].lightStats.useLights;
			}
			global::UnityEngine.Rendering.Universal.GlobalPropertiesPass.Setup(renderGraph, base.frameData, m_Renderer2DData, universalCameraData, flag);
			for (int j = 0; j < m_BatchCount; j++)
			{
				m_NormalPass.Render(renderGraph, base.frameData, m_Renderer2DData, ref m_LayerBatches[j], j);
			}
			for (int k = 0; k < m_BatchCount; k++)
			{
				m_ShadowPass.Render(renderGraph, base.frameData, m_Renderer2DData, ref m_LayerBatches[k], k);
			}
			for (int l = 0; l < m_BatchCount; l++)
			{
				m_LightPass.Render(renderGraph, base.frameData, m_Renderer2DData, ref m_LayerBatches[l], l);
			}
			for (int m = 0; m < m_BatchCount; m++)
			{
				if (!renderGraph.nativeRenderPassesEnabled && m == 0)
				{
					global::UnityEngine.Rendering.RTClearFlags cameraClearFlag = (global::UnityEngine.Rendering.RTClearFlags)global::UnityEngine.Rendering.Universal.ScriptableRenderer.GetCameraClearFlag(universalCameraData);
					if (cameraClearFlag != global::UnityEngine.Rendering.RTClearFlags.None)
					{
						global::UnityEngine.Rendering.Universal.ClearTargetsPass.Render(renderGraph, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, cameraClearFlag, universalCameraData.backgroundColor);
					}
				}
				ref global::UnityEngine.Rendering.Universal.LayerBatch reference = ref m_LayerBatches[m];
				global::UnityEngine.Rendering.Universal.LayerUtility.GetFilterSettings(m_Renderer2DData, ref m_LayerBatches[m], out var filterSettings);
				m_RendererPass.Render(renderGraph, base.frameData, m_Renderer2DData, ref m_LayerBatches, m, ref filterSettings);
				m_ShadowPass.Render(renderGraph, base.frameData, m_Renderer2DData, ref m_LayerBatches[m], m, isVolumetric: true);
				m_LightPass.Render(renderGraph, base.frameData, m_Renderer2DData, ref m_LayerBatches[m], m, isVolumetric: true);
				if (m_Renderer2DData.useCameraSortingLayerTexture && cameraSortingLayerBoundsIndex >= reference.layerRange.lowerBound && cameraSortingLayerBoundsIndex <= reference.layerRange.upperBound)
				{
					m_CopyCameraSortingLayerPass.Render(renderGraph, base.frameData);
				}
			}
			if (RequiresDepthCopyPass(universalCameraData))
			{
				m_CopyDepthPass?.Render(renderGraph, base.frameData, universalResourceData.cameraDepthTexture, universalResourceData.activeDepthTexture, bindAsCameraDepth: true);
			}
			bool rendersOverlayUI = universalCameraData.rendersOverlayUI;
			bool isHDROutputActive = universalCameraData.isHDROutputActive;
			if (!(rendersOverlayUI && isHDROutputActive))
			{
				return;
			}
			if (universalCameraData.rendersOffscreenUI)
			{
				m_DrawOffscreenUIPass.RenderOffscreen(renderGraph, base.frameData, global::UnityEngine.Rendering.CoreUtils.GetDefaultDepthStencilFormat(), universalResourceData.overlayUITexture);
				if (universalCameraData.blitsOffscreenUICover)
				{
					global::UnityEngine.RenderTextureDescriptor desc = new global::UnityEngine.RenderTextureDescriptor(1, 1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, 0);
					global::UnityEngine.Rendering.RenderGraphModule.TextureHandle src = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "BlackTexture", clear: false);
					m_OffscreenUICoverPrepass.Render(renderGraph, base.frameData, universalCameraData, in src, universalResourceData.backBufferColor, universalResourceData.overlayUITexture, useFullScreenViewport: true);
				}
			}
			else
			{
				global::UnityEngine.Rendering.Universal.RenderGraphUtils.SetGlobalTexture(renderGraph, global::UnityEngine.Rendering.Universal.ShaderPropertyId.overlayUITexture, universalResourceData.overlayUITexture, "Set Global Texture", ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\Renderer2DRendergraph.cs", 851);
			}
		}

		private void OnAfterRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalPostProcessingData universalPostProcessingData = base.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();
			bool flag = global::UnityEngine.Rendering.DebugDisplaySettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplaySettings>.Instance.renderingSettings.sceneOverrideMode == global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.None;
			if (flag)
			{
				DrawRenderGraphGizmos(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, global::UnityEngine.Rendering.GizmoSubset.PreImageEffects);
			}
			bool flag2 = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(universalCameraData)?.WriteToDebugScreenTexture(universalCameraData.resolveFinalTarget) ?? false;
			if (flag2)
			{
				global::UnityEngine.RenderTextureDescriptor descriptor = universalCameraData.cameraTargetDescriptor;
				global::UnityEngine.Rendering.Universal.DebugHandler.ConfigureColorDescriptorForDebugScreen(ref descriptor, universalCameraData.pixelWidth, universalCameraData.pixelHeight);
				universalResourceData.debugScreenColor = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_DebugScreenColor", clear: false);
				global::UnityEngine.RenderTextureDescriptor descriptor2 = universalCameraData.cameraTargetDescriptor;
				global::UnityEngine.Rendering.Universal.DebugHandler.ConfigureDepthDescriptorForDebugScreen(ref descriptor2, global::UnityEngine.Rendering.CoreUtils.GetDefaultDepthStencilFormat(), universalCameraData.pixelWidth, universalCameraData.pixelHeight);
				universalResourceData.debugScreenDepth = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor2, "_DebugScreenDepth", clear: false);
			}
			bool flag3 = universalCameraData.postProcessEnabled && m_PostProcessPassRenderGraph != null;
			bool flag4 = universalPostProcessingData.isEnabled && m_PostProcessPassRenderGraph != null;
			global::UnityEngine.Rendering.Universal.PixelPerfectCamera ppc;
			bool num = IsPixelPerfectCameraEnabled(universalCameraData, out ppc) && ppc.requiresUpscalePass;
			bool flag5 = universalCameraData.resolveFinalTarget && !ppcUpscaleRT && flag4 && universalCameraData.antialiasing == global::UnityEngine.Rendering.Universal.AntialiasingMode.FastApproximateAntialiasing;
			bool flag6 = base.activeRenderPassQueue.Find((global::UnityEngine.Rendering.Universal.ScriptableRenderPass x) => x.renderPassEvent == global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingPostProcessing) != null;
			bool flag7 = base.DebugHandler == null || !base.DebugHandler.HDRDebugViewIsActive(universalCameraData.resolveFinalTarget);
			bool flag8 = ppc != null && ppc.enabled;
			bool flag9 = universalCameraData.captureActions != null && universalCameraData.resolveFinalTarget;
			bool flag10 = universalCameraData.resolveFinalTarget && !flag9 && !flag6 && !flag5 && !flag8;
			bool enableColorEndingIfNeeded = flag10 && flag7;
			if (flag3)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle activeCameraColorTexture = universalResourceData.activeColorTexture;
				bool flag11 = flag10;
				if (!flag11)
				{
					universalResourceData.cameraColor = renderGraph.ImportTexture(importParams: new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
					{
						clearOnFirstUse = true,
						clearColor = global::UnityEngine.Color.black,
						discardOnLastUse = universalCameraData.resolveFinalTarget
					}, rt: nextRenderGraphCameraColorHandle);
				}
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle postProcessingTarget = (flag11 ? universalResourceData.backBufferColor : universalResourceData.cameraColor);
				if (flag2 && flag11)
				{
					postProcessingTarget = universalResourceData.debugScreenColor;
				}
				m_PostProcessPassRenderGraph.RenderPostProcessingRenderGraph(renderGraph, base.frameData, in activeCameraColorTexture, universalResourceData.internalColorLut, universalResourceData.overlayUITexture, in postProcessingTarget, flag5, flag2, enableColorEndingIfNeeded);
				if (flag11)
				{
					universalResourceData.activeColorID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
					universalResourceData.activeDepthID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
				}
			}
			RecordCustomRenderGraphPasses(renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent2D.AfterRenderingPostProcessing);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraColorAttachment = universalResourceData.activeColorTexture;
			if (num)
			{
				m_UpscalePass.Render(renderGraph, universalCameraData.camera, in cameraColorAttachment, universal2DResourceData.upscaleTexture);
				cameraColorAttachment = universal2DResourceData.upscaleTexture;
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle postProcessingTarget2 = (flag2 ? universalResourceData.debugScreenColor : universalResourceData.backBufferColor);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthBuffer = (flag2 ? universalResourceData.debugScreenDepth : universalResourceData.backBufferDepth);
			if (flag5)
			{
				m_PostProcessPassRenderGraph.RenderFinalPassRenderGraph(renderGraph, base.frameData, in cameraColorAttachment, universalResourceData.overlayUITexture, in postProcessingTarget2, flag7);
				cameraColorAttachment = postProcessingTarget2;
				universalResourceData.activeColorID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
				universalResourceData.activeDepthID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
			}
			bool flag12 = flag5 || (flag3 && !flag6 && !flag9 && !flag8);
			if (!universalResourceData.isActiveTargetBackBuffer && universalCameraData.resolveFinalTarget && !flag12)
			{
				m_FinalBlitPass.Render(renderGraph, base.frameData, universalCameraData, in cameraColorAttachment, in postProcessingTarget2, universalResourceData.overlayUITexture);
				cameraColorAttachment = postProcessingTarget2;
				universalResourceData.activeColorID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
				universalResourceData.activeDepthID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
			}
			bool num2 = universalCameraData.rendersOverlayUI && universalCameraData.isLastBaseCamera;
			bool isHDROutputActive = universalCameraData.isHDROutputActive;
			if (num2 && !isHDROutputActive)
			{
				m_DrawOverlayUIPass.RenderOverlay(renderGraph, base.frameData, in cameraColorAttachment, in depthBuffer);
			}
			if (universalCameraData.resolveFinalTarget)
			{
				if (universalCameraData.isSceneViewCamera)
				{
					DrawRenderGraphWireOverlay(renderGraph, base.frameData, universalResourceData.backBufferColor);
				}
				if (flag)
				{
					DrawRenderGraphGizmos(renderGraph, base.frameData, universalResourceData.activeColorTexture, universalResourceData.activeDepthTexture, global::UnityEngine.Rendering.GizmoSubset.PostImageEffects);
				}
			}
		}

		public global::UnityEngine.Rendering.Universal.Renderer2DData GetRenderer2DData()
		{
			return m_Renderer2DData;
		}

		protected override void Dispose(bool disposing)
		{
			CleanupRenderGraphResources();
			base.Dispose(disposing);
		}

		private void CleanupRenderGraphResources()
		{
			m_Renderer2DData.Dispose();
			m_UpscalePass.Dispose();
			m_CopyDepthPass?.Dispose();
			m_FinalBlitPass?.Dispose();
			m_OffscreenUICoverPrepass?.Dispose();
			m_DrawOffscreenUIPass?.Dispose();
			m_DrawOverlayUIPass?.Dispose();
			m_PostProcessPassRenderGraph?.Cleanup();
			m_ColorGradingLutPassRenderGraph?.Cleanup();
			m_RenderGraphCameraColorHandles[0]?.Release();
			m_RenderGraphCameraColorHandles[1]?.Release();
			m_RenderGraphCameraDepthHandle?.Release();
			m_RenderGraphBackbufferColorHandle?.Release();
			m_RenderGraphBackbufferDepthHandle?.Release();
			m_CameraSortingLayerHandle?.Release();
			global::UnityEngine.Rendering.Universal.Light2DManager.Dispose();
			global::UnityEngine.Rendering.Universal.Light2DLookupTexture.Release();
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_BlitMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_BlitHDRMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_BlitOffscreenUICoverMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_SamplingMaterial);
			global::UnityEngine.Experimental.Rendering.XRSystem.Dispose();
		}

		internal static bool IsGLESDevice()
		{
			return global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3;
		}
	}
}
