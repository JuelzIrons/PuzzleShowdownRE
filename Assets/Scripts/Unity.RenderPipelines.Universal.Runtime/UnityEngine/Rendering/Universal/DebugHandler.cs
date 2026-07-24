namespace UnityEngine.Rendering.Universal
{
	internal class DebugHandler : global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		private class DebugFinalValidationPassData
		{
			public bool isFinalPass;

			public bool resolveFinalTarget;

			public bool isActiveForCamera;

			public bool hasDebugRenderTarget;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle debugRenderTargetHandle;

			public int debugTexturePropertyId;

			public global::UnityEngine.Vector4 debugRenderTargetPixelRect;

			public int debugRenderTargetSupportsStereo;

			public global::UnityEngine.Vector4 debugRenderTargetRangeRemap;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle debugFontTextureHandle;

			public global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering renderingSettings;
		}

		private class DebugSetupPassData
		{
			public bool isActiveForCamera;

			public global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial materialSettings;

			public global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering renderingSettings;

			public global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting lightingSettings;
		}

		private static readonly int k_DebugColorInvalidModePropertyId = global::UnityEngine.Shader.PropertyToID("_DebugColorInvalidMode");

		private static readonly int k_DebugCurrentRealTimeId = global::UnityEngine.Shader.PropertyToID("_DebugCurrentRealTime");

		private static readonly int k_DebugColorPropertyId = global::UnityEngine.Shader.PropertyToID("_DebugColor");

		private static readonly int k_DebugTexturePropertyId = global::UnityEngine.Shader.PropertyToID("_DebugTexture");

		private static readonly int k_DebugFontId = global::UnityEngine.Shader.PropertyToID("_DebugFont");

		private static readonly int k_DebugTextureNoStereoPropertyId = global::UnityEngine.Shader.PropertyToID("_DebugTextureNoStereo");

		private static readonly int k_DebugTextureDisplayRect = global::UnityEngine.Shader.PropertyToID("_DebugTextureDisplayRect");

		private static readonly int k_DebugRenderTargetSupportsStereo = global::UnityEngine.Shader.PropertyToID("_DebugRenderTargetSupportsStereo");

		private static readonly int k_DebugRenderTargetRangeRemap = global::UnityEngine.Shader.PropertyToID("_DebugRenderTargetRangeRemap");

		private static readonly int k_DebugMaterialModeId = global::UnityEngine.Shader.PropertyToID("_DebugMaterialMode");

		private static readonly int k_DebugVertexAttributeModeId = global::UnityEngine.Shader.PropertyToID("_DebugVertexAttributeMode");

		private static readonly int k_DebugMaterialValidationModeId = global::UnityEngine.Shader.PropertyToID("_DebugMaterialValidationMode");

		private static readonly int k_DebugMipInfoModeId = global::UnityEngine.Shader.PropertyToID("_DebugMipInfoMode");

		private static readonly int k_DebugMipMapStatusModeId = global::UnityEngine.Shader.PropertyToID("_DebugMipMapStatusMode");

		private static readonly int k_DebugMipMapShowStatusCodeId = global::UnityEngine.Shader.PropertyToID("_DebugMipMapShowStatusCode");

		private static readonly int k_DebugMipMapOpacityId = global::UnityEngine.Shader.PropertyToID("_DebugMipMapOpacity");

		private static readonly int k_DebugMipMapRecentlyUpdatedCooldownId = global::UnityEngine.Shader.PropertyToID("_DebugMipMapRecentlyUpdatedCooldown");

		private static readonly int k_DebugMipMapTerrainTextureModeId = global::UnityEngine.Shader.PropertyToID("_DebugMipMapTerrainTextureMode");

		private static readonly int k_DebugSceneOverrideModeId = global::UnityEngine.Shader.PropertyToID("_DebugSceneOverrideMode");

		private static readonly int k_DebugFullScreenModeId = global::UnityEngine.Shader.PropertyToID("_DebugFullScreenMode");

		private static readonly int k_DebugValidationModeId = global::UnityEngine.Shader.PropertyToID("_DebugValidationMode");

		private static readonly int k_DebugValidateBelowMinThresholdColorPropertyId = global::UnityEngine.Shader.PropertyToID("_DebugValidateBelowMinThresholdColor");

		private static readonly int k_DebugValidateAboveMaxThresholdColorPropertyId = global::UnityEngine.Shader.PropertyToID("_DebugValidateAboveMaxThresholdColor");

		private static readonly int k_DebugMaxPixelCost = global::UnityEngine.Shader.PropertyToID("_DebugMaxPixelCost");

		private static readonly int k_DebugLightingModeId = global::UnityEngine.Shader.PropertyToID("_DebugLightingMode");

		private static readonly int k_DebugLightingFeatureFlagsId = global::UnityEngine.Shader.PropertyToID("_DebugLightingFeatureFlags");

		private static readonly int k_DebugValidateAlbedoMinLuminanceId = global::UnityEngine.Shader.PropertyToID("_DebugValidateAlbedoMinLuminance");

		private static readonly int k_DebugValidateAlbedoMaxLuminanceId = global::UnityEngine.Shader.PropertyToID("_DebugValidateAlbedoMaxLuminance");

		private static readonly int k_DebugValidateAlbedoSaturationToleranceId = global::UnityEngine.Shader.PropertyToID("_DebugValidateAlbedoSaturationTolerance");

		private static readonly int k_DebugValidateAlbedoHueToleranceId = global::UnityEngine.Shader.PropertyToID("_DebugValidateAlbedoHueTolerance");

		private static readonly int k_DebugValidateAlbedoCompareColorId = global::UnityEngine.Shader.PropertyToID("_DebugValidateAlbedoCompareColor");

		private static readonly int k_DebugValidateMetallicMinValueId = global::UnityEngine.Shader.PropertyToID("_DebugValidateMetallicMinValue");

		private static readonly int k_DebugValidateMetallicMaxValueId = global::UnityEngine.Shader.PropertyToID("_DebugValidateMetallicMaxValue");

		private static readonly int k_ValidationChannelsId = global::UnityEngine.Shader.PropertyToID("_ValidationChannels");

		private static readonly int k_RangeMinimumId = global::UnityEngine.Shader.PropertyToID("_RangeMinimum");

		private static readonly int k_RangeMaximumId = global::UnityEngine.Shader.PropertyToID("_RangeMaximum");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler s_DebugSetupSampler = new global::UnityEngine.Rendering.ProfilingSampler("Setup Debug Properties");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler s_DebugFinalValidationSampler = new global::UnityEngine.Rendering.ProfilingSampler("UpdateShaderGlobalPropertiesForFinalValidationPass");

		private global::UnityEngine.Rendering.Universal.DebugHandler.DebugSetupPassData s_DebugSetupPassData = new global::UnityEngine.Rendering.Universal.DebugHandler.DebugSetupPassData();

		private global::UnityEngine.Rendering.Universal.DebugHandler.DebugFinalValidationPassData s_DebugFinalValidationPassData = new global::UnityEngine.Rendering.Universal.DebugHandler.DebugFinalValidationPassData();

		private readonly global::UnityEngine.Material m_ReplacementMaterial;

		private readonly global::UnityEngine.Material m_HDRDebugViewMaterial;

		private global::UnityEngine.Rendering.Universal.HDRDebugViewPass m_HDRDebugViewPass;

		private global::UnityEngine.Rendering.RTHandle m_DebugScreenColorHandle;

		private global::UnityEngine.Rendering.RTHandle m_DebugScreenDepthHandle;

		private readonly global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTextures m_RuntimeTextures;

		private bool m_HasDebugRenderTarget;

		private bool m_DebugRenderTargetSupportsStereo;

		private global::UnityEngine.Vector4 m_DebugRenderTargetPixelRect;

		private global::UnityEngine.Vector4 m_DebugRenderTargetRangeRemap;

		private global::UnityEngine.Rendering.RTHandle m_DebugRenderTarget;

		private global::UnityEngine.Rendering.RTHandle m_DebugFontTexture;

		private global::UnityEngine.GraphicsBuffer m_debugDisplayConstant;

		private readonly global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplaySettings m_DebugDisplaySettings;

		private global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting LightingSettings => m_DebugDisplaySettings.lightingSettings;

		private global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial MaterialSettings => m_DebugDisplaySettings.materialSettings;

		private global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering RenderingSettings => m_DebugDisplaySettings.renderingSettings;

		public bool AreAnySettingsActive => m_DebugDisplaySettings.AreAnySettingsActive;

		public bool IsPostProcessingAllowed => m_DebugDisplaySettings.IsPostProcessingAllowed;

		public bool IsLightingActive => m_DebugDisplaySettings.IsLightingActive;

		internal bool IsActiveModeUnsupportedForDeferred
		{
			get
			{
				if (m_DebugDisplaySettings.lightingSettings.lightingDebugMode == global::UnityEngine.Rendering.Universal.DebugLightingMode.None && m_DebugDisplaySettings.lightingSettings.lightingFeatureFlags == global::UnityEngine.Rendering.Universal.DebugLightingFeatureFlags.None && m_DebugDisplaySettings.renderingSettings.sceneOverrideMode == global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.None && m_DebugDisplaySettings.materialSettings.materialDebugMode == global::UnityEngine.Rendering.Universal.DebugMaterialMode.None && m_DebugDisplaySettings.materialSettings.vertexAttributeDebugMode == global::UnityEngine.Rendering.Universal.DebugVertexAttributeMode.None && m_DebugDisplaySettings.materialSettings.materialValidationMode == global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode.None)
				{
					return m_DebugDisplaySettings.renderingSettings.mipInfoMode != global::UnityEngine.Rendering.Universal.DebugMipInfoMode.None;
				}
				return true;
			}
		}

		internal global::UnityEngine.Material ReplacementMaterial => m_ReplacementMaterial;

		internal global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplaySettings DebugDisplaySettings => m_DebugDisplaySettings;

		internal ref global::UnityEngine.Rendering.RTHandle DebugScreenColorHandle => ref m_DebugScreenColorHandle;

		internal ref global::UnityEngine.Rendering.RTHandle DebugScreenDepthHandle => ref m_DebugScreenDepthHandle;

		internal global::UnityEngine.Rendering.Universal.HDRDebugViewPass hdrDebugViewPass => m_HDRDebugViewPass;

		internal bool IsScreenClearNeeded
		{
			get
			{
				global::UnityEngine.Color color = global::UnityEngine.Color.black;
				return TryGetScreenClearColor(ref color);
			}
		}

		internal bool IsRenderPassSupported
		{
			get
			{
				if (RenderingSettings.sceneOverrideMode != global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.None)
				{
					return RenderingSettings.sceneOverrideMode == global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Overdraw;
				}
				return true;
			}
		}

		internal bool IsDepthPrimingCompatible => RenderingSettings.sceneOverrideMode != global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Wireframe;

		internal int stpDebugViewIndex => RenderingSettings.stpDebugViewIndex;

		public bool TryGetScreenClearColor(ref global::UnityEngine.Color color)
		{
			return m_DebugDisplaySettings.TryGetScreenClearColor(ref color);
		}

		internal bool HDRDebugViewIsActive(bool resolveFinalTarget)
		{
			return DebugDisplaySettings.lightingSettings.hdrDebugMode != global::UnityEngine.Rendering.Universal.HDRDebugMode.None && resolveFinalTarget;
		}

		internal bool WriteToDebugScreenTexture(bool resolveFinalTarget)
		{
			return HDRDebugViewIsActive(resolveFinalTarget);
		}

		internal DebugHandler()
		{
			m_DebugDisplaySettings = global::UnityEngine.Rendering.DebugDisplaySettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplaySettings>.Instance;
			if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugShaders>(out var settings))
			{
				m_ReplacementMaterial = ((settings.debugReplacementPS != null) ? global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.debugReplacementPS) : null);
				m_HDRDebugViewMaterial = ((settings.hdrDebugViewPS != null) ? global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.hdrDebugViewPS) : null);
			}
			m_HDRDebugViewPass = new global::UnityEngine.Rendering.Universal.HDRDebugViewPass(m_HDRDebugViewMaterial);
			m_RuntimeTextures = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTextures>();
			if (m_RuntimeTextures != null)
			{
				m_DebugFontTexture = global::UnityEngine.Rendering.RTHandles.Alloc(m_RuntimeTextures.debugFontTexture);
			}
			m_debugDisplayConstant = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Constant, 32, global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::UnityEngine.Vector4)));
		}

		public void Dispose()
		{
			m_HDRDebugViewPass.Dispose();
			m_DebugScreenColorHandle?.Release();
			m_DebugScreenDepthHandle?.Release();
			m_DebugFontTexture?.Release();
			m_debugDisplayConstant.Dispose();
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_HDRDebugViewMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_ReplacementMaterial);
		}

		internal bool IsActiveForCamera(bool isPreviewCamera)
		{
			if (!isPreviewCamera)
			{
				return AreAnySettingsActive;
			}
			return false;
		}

		internal bool TryGetFullscreenDebugMode(out global::UnityEngine.Rendering.Universal.DebugFullScreenMode debugFullScreenMode)
		{
			int textureHeightPercent;
			return TryGetFullscreenDebugMode(out debugFullScreenMode, out textureHeightPercent);
		}

		internal bool TryGetFullscreenDebugMode(out global::UnityEngine.Rendering.Universal.DebugFullScreenMode debugFullScreenMode, out int textureHeightPercent)
		{
			debugFullScreenMode = RenderingSettings.fullScreenDebugMode;
			textureHeightPercent = RenderingSettings.fullScreenDebugModeOutputSizeScreenPercent;
			return debugFullScreenMode != global::UnityEngine.Rendering.Universal.DebugFullScreenMode.None;
		}

		internal static void ConfigureColorDescriptorForDebugScreen(ref global::UnityEngine.RenderTextureDescriptor descriptor, int cameraWidth, int cameraHeight)
		{
			descriptor.width = cameraWidth;
			descriptor.height = cameraHeight;
			descriptor.useMipMap = false;
			descriptor.autoGenerateMips = false;
			descriptor.useDynamicScale = true;
			descriptor.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
		}

		internal static void ConfigureDepthDescriptorForDebugScreen(ref global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencilFormat, int cameraWidth, int cameraHeight)
		{
			descriptor.width = cameraWidth;
			descriptor.height = cameraHeight;
			descriptor.useMipMap = false;
			descriptor.autoGenerateMips = false;
			descriptor.useDynamicScale = true;
			descriptor.depthStencilFormat = depthStencilFormat;
			descriptor.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void SetupShaderProperties(global::UnityEngine.Rendering.RasterCommandBuffer cmd, int passIndex = 0)
		{
			if (LightingSettings.lightingDebugMode == global::UnityEngine.Rendering.Universal.DebugLightingMode.ShadowCascades)
			{
				cmd.EnableShaderKeyword("_DEBUG_ENVIRONMENTREFLECTIONS_OFF");
			}
			else
			{
				cmd.DisableShaderKeyword("_DEBUG_ENVIRONMENTREFLECTIONS_OFF");
			}
			m_debugDisplayConstant.SetData(MaterialSettings.debugRenderingLayersColors, 0, 0, 32);
			cmd.SetGlobalConstantBuffer(m_debugDisplayConstant, "_DebugDisplayConstant", 0, m_debugDisplayConstant.count * m_debugDisplayConstant.stride);
			if (MaterialSettings.renderingLayersSelectedLight)
			{
				cmd.SetGlobalInt("_DebugRenderingLayerMask", (int)MaterialSettings.GetDebugLightLayersMask());
			}
			else
			{
				cmd.SetGlobalInt("_DebugRenderingLayerMask", (int)MaterialSettings.renderingLayerMask);
			}
			switch (RenderingSettings.sceneOverrideMode)
			{
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Overdraw:
			{
				float num = 1f / (float)RenderingSettings.maxOverdrawCount;
				cmd.SetGlobalColor(k_DebugColorPropertyId, new global::UnityEngine.Color(num, num, num, 1f));
				break;
			}
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Wireframe:
				cmd.SetGlobalColor(k_DebugColorPropertyId, global::UnityEngine.Color.black);
				break;
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.SolidWireframe:
				cmd.SetGlobalColor(k_DebugColorPropertyId, (passIndex == 0) ? global::UnityEngine.Color.white : global::UnityEngine.Color.black);
				break;
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.ShadedWireframe:
				switch (passIndex)
				{
				case 0:
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DEBUG_DISPLAY, value: false);
					break;
				case 1:
					cmd.SetGlobalColor(k_DebugColorPropertyId, global::UnityEngine.Color.black);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DEBUG_DISPLAY, value: true);
					break;
				}
				break;
			}
			switch (MaterialSettings.materialValidationMode)
			{
			case global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode.Albedo:
				cmd.SetGlobalFloat(k_DebugValidateAlbedoMinLuminanceId, MaterialSettings.albedoMinLuminance);
				cmd.SetGlobalFloat(k_DebugValidateAlbedoMaxLuminanceId, MaterialSettings.albedoMaxLuminance);
				cmd.SetGlobalFloat(k_DebugValidateAlbedoSaturationToleranceId, MaterialSettings.albedoSaturationTolerance);
				cmd.SetGlobalFloat(k_DebugValidateAlbedoHueToleranceId, MaterialSettings.albedoHueTolerance);
				cmd.SetGlobalColor(k_DebugValidateAlbedoCompareColorId, MaterialSettings.albedoCompareColor.linear);
				break;
			case global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode.Metallic:
				cmd.SetGlobalFloat(k_DebugValidateMetallicMinValueId, MaterialSettings.metallicMinValue);
				cmd.SetGlobalFloat(k_DebugValidateMetallicMaxValueId, MaterialSettings.metallicMaxValue);
				break;
			}
		}

		internal void SetDebugRenderTarget(global::UnityEngine.Rendering.RTHandle renderTarget, global::UnityEngine.Rect displayRect, bool supportsStereo, global::UnityEngine.Vector4 dataRangeRemap)
		{
			m_HasDebugRenderTarget = true;
			m_DebugRenderTargetSupportsStereo = supportsStereo;
			m_DebugRenderTarget = renderTarget;
			m_DebugRenderTargetPixelRect = new global::UnityEngine.Vector4(displayRect.x, displayRect.y, displayRect.width, displayRect.height);
			m_DebugRenderTargetRangeRemap = dataRangeRemap;
		}

		internal void ResetDebugRenderTarget()
		{
			m_HasDebugRenderTarget = false;
		}

		private global::UnityEngine.Rendering.Universal.DebugHandler.DebugFinalValidationPassData InitDebugFinalValidationPassData(global::UnityEngine.Rendering.Universal.DebugHandler.DebugFinalValidationPassData passData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool isFinalPass)
		{
			passData.isFinalPass = isFinalPass;
			passData.resolveFinalTarget = cameraData.resolveFinalTarget;
			passData.isActiveForCamera = IsActiveForCamera(cameraData.isPreviewCamera);
			passData.hasDebugRenderTarget = m_HasDebugRenderTarget;
			passData.debugRenderTargetHandle = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			passData.debugTexturePropertyId = (m_DebugRenderTargetSupportsStereo ? k_DebugTexturePropertyId : k_DebugTextureNoStereoPropertyId);
			passData.debugRenderTargetPixelRect = m_DebugRenderTargetPixelRect;
			passData.debugRenderTargetSupportsStereo = (m_DebugRenderTargetSupportsStereo ? 1 : 0);
			passData.debugRenderTargetRangeRemap = m_DebugRenderTargetRangeRemap;
			passData.debugFontTextureHandle = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			passData.renderingSettings = RenderingSettings;
			return passData;
		}

		private static void UpdateShaderGlobalPropertiesForFinalValidationPass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DebugHandler.DebugFinalValidationPassData data)
		{
			if (!data.isFinalPass || !data.resolveFinalTarget)
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DEBUG_DISPLAY, value: false);
				return;
			}
			if (data.isActiveForCamera)
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DEBUG_DISPLAY, value: true);
			}
			else
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DEBUG_DISPLAY, value: false);
			}
			if (data.hasDebugRenderTarget)
			{
				if (data.debugRenderTargetHandle.IsValid())
				{
					cmd.SetGlobalTexture(data.debugTexturePropertyId, data.debugRenderTargetHandle);
				}
				cmd.SetGlobalVector(k_DebugTextureDisplayRect, data.debugRenderTargetPixelRect);
				cmd.SetGlobalInteger(k_DebugRenderTargetSupportsStereo, data.debugRenderTargetSupportsStereo);
				cmd.SetGlobalVector(k_DebugRenderTargetRangeRemap, data.debugRenderTargetRangeRemap);
			}
			global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering renderingSettings = data.renderingSettings;
			if (renderingSettings.validationMode == global::UnityEngine.Rendering.Universal.DebugValidationMode.HighlightOutsideOfRange)
			{
				cmd.SetGlobalInteger(k_ValidationChannelsId, (int)renderingSettings.validationChannels);
				cmd.SetGlobalFloat(k_RangeMinimumId, renderingSettings.validationRangeMin);
				cmd.SetGlobalFloat(k_RangeMaximumId, renderingSettings.validationRangeMax);
			}
			if (renderingSettings.mipInfoMode != global::UnityEngine.Rendering.Universal.DebugMipInfoMode.None)
			{
				cmd.SetGlobalTexture(k_DebugFontId, data.debugFontTextureHandle);
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void UpdateShaderGlobalPropertiesForFinalValidationPass(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool isFinalPass)
		{
			UpdateShaderGlobalPropertiesForFinalValidationPass(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd), InitDebugFinalValidationPassData(s_DebugFinalValidationPassData, cameraData, isFinalPass));
			cmd.SetGlobalTexture(s_DebugFinalValidationPassData.debugTexturePropertyId, m_DebugRenderTarget);
			if (RenderingSettings.mipInfoMode != global::UnityEngine.Rendering.Universal.DebugMipInfoMode.None)
			{
				cmd.SetGlobalTexture(k_DebugFontId, m_RuntimeTextures.debugFontTexture);
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void UpdateShaderGlobalPropertiesForFinalValidationPass(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool isFinalPass)
		{
			global::UnityEngine.Rendering.Universal.DebugHandler.DebugFinalValidationPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DebugHandler.DebugFinalValidationPassData>("UpdateShaderGlobalPropertiesForFinalValidationPass", out passData, s_DebugFinalValidationSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Debug\\DebugHandler.cs", 434);
			InitDebugFinalValidationPassData(passData, cameraData, isFinalPass);
			if (m_DebugRenderTarget != null)
			{
				passData.debugRenderTargetHandle = renderGraph.ImportTexture(m_DebugRenderTarget);
			}
			if (m_DebugFontTexture != null)
			{
				passData.debugFontTextureHandle = renderGraph.ImportTexture(m_DebugFontTexture);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			if (passData.debugRenderTargetHandle.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in passData.debugRenderTargetHandle);
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in passData.debugRenderTargetHandle, passData.debugTexturePropertyId);
			}
			if (passData.debugFontTextureHandle.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in passData.debugFontTextureHandle);
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in passData.debugFontTextureHandle, k_DebugFontId);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DebugHandler.DebugFinalValidationPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				UpdateShaderGlobalPropertiesForFinalValidationPass(context.cmd, data);
			});
		}

		private global::UnityEngine.Rendering.Universal.DebugHandler.DebugSetupPassData InitDebugSetupPassData(global::UnityEngine.Rendering.Universal.DebugHandler.DebugSetupPassData passData, bool isPreviewCamera)
		{
			passData.isActiveForCamera = IsActiveForCamera(isPreviewCamera);
			passData.materialSettings = MaterialSettings;
			passData.renderingSettings = RenderingSettings;
			passData.lightingSettings = LightingSettings;
			return passData;
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private static void Setup(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DebugHandler.DebugSetupPassData passData)
		{
			if (passData.isActiveForCamera)
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DEBUG_DISPLAY, value: true);
				cmd.SetGlobalFloat(k_DebugMaterialModeId, (float)passData.materialSettings.materialDebugMode);
				cmd.SetGlobalFloat(k_DebugVertexAttributeModeId, (float)passData.materialSettings.vertexAttributeDebugMode);
				cmd.SetGlobalInteger(k_DebugMaterialValidationModeId, (int)passData.materialSettings.materialValidationMode);
				cmd.SetGlobalInteger(k_DebugMipInfoModeId, (int)passData.renderingSettings.mipInfoMode);
				cmd.SetGlobalInteger(k_DebugMipMapStatusModeId, (int)passData.renderingSettings.mipDebugStatusMode);
				cmd.SetGlobalInteger(k_DebugMipMapShowStatusCodeId, passData.renderingSettings.mipDebugStatusShowCode ? 1 : 0);
				cmd.SetGlobalFloat(k_DebugMipMapOpacityId, passData.renderingSettings.mipDebugOpacity);
				cmd.SetGlobalFloat(k_DebugMipMapRecentlyUpdatedCooldownId, passData.renderingSettings.mipDebugRecentUpdateCooldown);
				cmd.SetGlobalFloat(k_DebugMipMapTerrainTextureModeId, (float)passData.renderingSettings.mipDebugTerrainTexture);
				cmd.SetGlobalInteger(k_DebugSceneOverrideModeId, (int)passData.renderingSettings.sceneOverrideMode);
				cmd.SetGlobalInteger(k_DebugFullScreenModeId, (int)passData.renderingSettings.fullScreenDebugMode);
				cmd.SetGlobalInteger(k_DebugMaxPixelCost, passData.renderingSettings.maxOverdrawCount);
				cmd.SetGlobalInteger(k_DebugValidationModeId, (int)passData.renderingSettings.validationMode);
				cmd.SetGlobalColor(k_DebugValidateBelowMinThresholdColorPropertyId, global::UnityEngine.Color.red);
				cmd.SetGlobalColor(k_DebugValidateAboveMaxThresholdColorPropertyId, global::UnityEngine.Color.blue);
				cmd.SetGlobalFloat(k_DebugLightingModeId, (float)passData.lightingSettings.lightingDebugMode);
				cmd.SetGlobalInteger(k_DebugLightingFeatureFlagsId, (int)passData.lightingSettings.lightingFeatureFlags);
				cmd.SetGlobalColor(k_DebugColorInvalidModePropertyId, global::UnityEngine.Color.red);
				cmd.SetGlobalFloat(k_DebugCurrentRealTimeId, global::UnityEngine.Time.realtimeSinceStartup);
			}
			else
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DEBUG_DISPLAY, value: false);
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void Setup(global::UnityEngine.Rendering.CommandBuffer cmd, bool isPreviewCamera)
		{
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void Setup(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, bool isPreviewCamera)
		{
			global::UnityEngine.Rendering.Universal.DebugHandler.DebugSetupPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DebugHandler.DebugSetupPassData>(s_DebugSetupSampler.name, out passData, s_DebugSetupSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Debug\\DebugHandler.cs", 540);
			InitDebugSetupPassData(passData, isPreviewCamera);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc<global::UnityEngine.Rendering.Universal.DebugHandler.DebugSetupPassData>(delegate
			{
			});
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcColor, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayTexture, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dstColor)
		{
			if (IsActiveForCamera(cameraData.isPreviewCamera) && HDRDebugViewIsActive(cameraData.resolveFinalTarget))
			{
				m_HDRDebugViewPass.RenderHDRDebug(renderGraph, cameraData, srcColor, overlayTexture, dstColor, LightingSettings.hdrDebugMode);
			}
		}

		internal global::UnityEngine.Rendering.Universal.DebugRendererLists CreateRendererListsWithDebugRenderState(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.DrawingSettings drawingSettings, ref global::UnityEngine.Rendering.FilteringSettings filteringSettings, ref global::UnityEngine.Rendering.RenderStateBlock renderStateBlock)
		{
			global::UnityEngine.Rendering.Universal.DebugRendererLists debugRendererLists = new global::UnityEngine.Rendering.Universal.DebugRendererLists(this, filteringSettings);
			debugRendererLists.CreateRendererListsWithDebugRenderState(context, ref cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
			return debugRendererLists;
		}

		internal global::UnityEngine.Rendering.Universal.DebugRendererLists CreateRendererListsWithDebugRenderState(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.DrawingSettings drawingSettings, ref global::UnityEngine.Rendering.FilteringSettings filteringSettings, ref global::UnityEngine.Rendering.RenderStateBlock renderStateBlock)
		{
			global::UnityEngine.Rendering.Universal.DebugRendererLists debugRendererLists = new global::UnityEngine.Rendering.Universal.DebugRendererLists(this, filteringSettings);
			debugRendererLists.CreateRendererListsWithDebugRenderState(renderGraph, ref cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
			return debugRendererLists;
		}
	}
}
