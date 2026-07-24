public class OnTilePostProcessPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
{
	private enum UberShaderPasses
	{
		Normal = 0,
		MSAASoftwareResolve = 1,
		TextureRead = 2,
		NormalVisMesh = 3,
		MSAASoftwareResolveVisMesh = 4,
		TextureReadVisMesh = 5
	}

	private class PassData
	{
		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle lutTexture;

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle userLutTexture;

		internal global::UnityEngine.Material material;

		internal OnTilePostProcessPass.UberShaderPasses shaderPass;

		internal global::UnityEngine.Vector4 scaleBias;

		internal bool useXRVisibilityMesh;

		internal global::UnityEngine.Experimental.Rendering.XRPass xr;

		internal int msaaSamples;
	}

	private static class ShaderConstants
	{
		public static readonly int _Vignette_Params1 = global::UnityEngine.Shader.PropertyToID("_Vignette_Params1");

		public static readonly int _Vignette_Params2 = global::UnityEngine.Shader.PropertyToID("_Vignette_Params2");

		public static readonly int _Vignette_ParamsXR = global::UnityEngine.Shader.PropertyToID("_Vignette_ParamsXR");

		public static readonly int _Lut_Params = global::UnityEngine.Shader.PropertyToID("_Lut_Params");

		public static readonly int _UserLut_Params = global::UnityEngine.Shader.PropertyToID("_UserLut_Params");

		public static readonly int _InternalLut = global::UnityEngine.Shader.PropertyToID("_InternalLut");

		public static readonly int _UserLut = global::UnityEngine.Shader.PropertyToID("_UserLut");
	}

	internal bool m_UseMultisampleShaderResolve;

	internal bool m_UseTextureReadFallback;

	private global::UnityEngine.Rendering.RTHandle m_UserLut;

	private global::UnityEngine.Material m_OnTileUberMaterial;

	private static readonly int s_BlitScaleBias = global::UnityEngine.Shader.PropertyToID("_BlitScaleBias");

	private static readonly int s_BlitTexture = global::UnityEngine.Shader.PropertyToID("_BlitTexture");

	private int m_DitheringTextureIndex;

	private global::UnityEngine.Rendering.Universal.PostProcessData m_PostProcessData;

	private const string m_PassName = "On Tile Post Processing";

	private const string m_FallbackPassName = "On Tile Post Processing (sampling fallback) ";

	internal OnTilePostProcessPass(global::UnityEngine.Rendering.Universal.PostProcessData postProcessData)
	{
		m_PostProcessData = postProcessData;
		m_UseMultisampleShaderResolve = global::UnityEngine.SystemInfo.supportsMultisampledShaderResolve;
	}

	internal void Setup(ref global::UnityEngine.Material onTileUberMaterial)
	{
		m_OnTileUberMaterial = onTileUberMaterial;
	}

	public void Dispose()
	{
		m_UserLut?.Release();
		global::UnityEngine.Rendering.CoreUtils.Destroy(m_OnTileUberMaterial);
	}

	public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
	{
		if (m_OnTileUberMaterial == null)
		{
			return;
		}
		global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
		frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
		global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
		global::UnityEngine.Rendering.Universal.UniversalPostProcessingData universalPostProcessingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();
		if (global::UnityEngine.SystemInfo.graphicsShaderLevel < 30)
		{
			global::UnityEngine.Debug.LogError("DrawProcedural is required for the On-Tile post processing feature but it is not supported by the platform. Pass will not execute.");
			return;
		}
		int lutSize = universalPostProcessingData.lutSize;
		global::UnityEngine.Rendering.VolumeStack stack = global::UnityEngine.Rendering.VolumeManager.instance.stack;
		global::UnityEngine.Rendering.Universal.Vignette component = stack.GetComponent<global::UnityEngine.Rendering.Universal.Vignette>();
		global::UnityEngine.Rendering.Universal.ColorLookup component2 = stack.GetComponent<global::UnityEngine.Rendering.Universal.ColorLookup>();
		global::UnityEngine.Rendering.Universal.ColorAdjustments component3 = stack.GetComponent<global::UnityEngine.Rendering.Universal.ColorAdjustments>();
		global::UnityEngine.Rendering.Universal.Tonemapping component4 = stack.GetComponent<global::UnityEngine.Rendering.Universal.Tonemapping>();
		global::UnityEngine.Rendering.Universal.FilmGrain component5 = stack.GetComponent<global::UnityEngine.Rendering.Universal.FilmGrain>();
		bool flag = universalCameraData.xr.enabled && universalCameraData.xr.hasValidVisibleMesh;
		global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture = universalResourceData.activeColorTexture;
		global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureDesc = renderGraph.GetTextureDesc(in texture);
		global::UnityEngine.Rendering.RenderGraphModule.TextureHandle backBufferColor = universalResourceData.backBufferColor;
		SetupVignette(m_OnTileUberMaterial, universalCameraData.xr, textureDesc.width, textureDesc.height, component);
		SetupLut(m_OnTileUberMaterial, component2, component3, lutSize);
		SetupTonemapping(m_OnTileUberMaterial, component4, universalPostProcessingData.gradingMode == global::UnityEngine.Rendering.Universal.ColorGradingMode.HighDynamicRange);
		SetupGrain(m_OnTileUberMaterial, universalCameraData, component5, m_PostProcessData);
		SetupDithering(m_OnTileUberMaterial, universalCameraData, m_PostProcessData);
		global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_OnTileUberMaterial, "_ENABLE_ALPHA_OUTPUT", universalCameraData.isAlphaOutputEnabled);
		OnTilePostProcessPass.UberShaderPasses uberShaderPasses = (flag ? OnTilePostProcessPass.UberShaderPasses.NormalVisMesh : OnTilePostProcessPass.UberShaderPasses.Normal);
		bool flag2 = false;
		if (textureDesc.msaaSamples != global::UnityEngine.Rendering.MSAASamples.None)
		{
			if (textureDesc.msaaSamples == global::UnityEngine.Rendering.MSAASamples.MSAA8x)
			{
				global::UnityEngine.Debug.LogError("MSAA8x is enabled in Universal Render Pipeline Asset but it is not supported by the on-tile post-processing feature yet. Please use MSAA4x or MSAA2x instead.");
				return;
			}
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo = renderGraph.GetRenderTargetInfo(backBufferColor);
			if (!m_UseMultisampleShaderResolve)
			{
				uberShaderPasses = ((renderTargetInfo.msaaSamples != (int)textureDesc.msaaSamples) ? (flag ? OnTilePostProcessPass.UberShaderPasses.TextureReadVisMesh : OnTilePostProcessPass.UberShaderPasses.TextureRead) : ((!flag) ? OnTilePostProcessPass.UberShaderPasses.MSAASoftwareResolve : OnTilePostProcessPass.UberShaderPasses.MSAASoftwareResolveVisMesh));
			}
			else
			{
				uberShaderPasses = ((!flag) ? OnTilePostProcessPass.UberShaderPasses.MSAASoftwareResolve : OnTilePostProcessPass.UberShaderPasses.MSAASoftwareResolveVisMesh);
				if (global::UnityEngine.SystemInfo.supportsMultisampleAutoResolve)
				{
					flag2 = true;
				}
			}
		}
		if (m_UseTextureReadFallback)
		{
			uberShaderPasses = (flag ? OnTilePostProcessPass.UberShaderPasses.TextureReadVisMesh : OnTilePostProcessPass.UberShaderPasses.TextureRead);
			flag2 = false;
		}
		global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo2 = renderGraph.GetRenderTargetInfo(backBufferColor);
		global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureDesc2 = new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(renderTargetInfo2.width, renderTargetInfo2.height);
		textureDesc2.format = renderTargetInfo2.format;
		textureDesc2.msaaSamples = (global::UnityEngine.Rendering.MSAASamples)renderTargetInfo2.msaaSamples;
		textureDesc2.bindTextureMS = renderTargetInfo2.bindMS;
		textureDesc2.slices = renderTargetInfo2.volumeDepth;
		textureDesc2.dimension = ((renderTargetInfo2.volumeDepth > 1) ? global::UnityEngine.Rendering.TextureDimension.Tex2DArray : global::UnityEngine.Rendering.TextureDimension.Tex2D);
		if (textureDesc.width != textureDesc2.width || textureDesc.height != textureDesc2.height || textureDesc.slices != textureDesc2.slices)
		{
			uberShaderPasses = (flag ? OnTilePostProcessPass.UberShaderPasses.TextureReadVisMesh : OnTilePostProcessPass.UberShaderPasses.TextureRead);
			flag2 = false;
		}
		global::UnityEngine.Rendering.RenderGraphModule.TextureHandle internalColorLut = universalResourceData.internalColorLut;
		string text = (m_UseTextureReadFallback ? "On Tile Post Processing (sampling fallback) " : "On Tile Post Processing");
		OnTilePostProcessPass.PassData passData;
		using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<OnTilePostProcessPass.PassData>(text, out passData, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\RendererFeatures\\OnTilePostProcessPass.cs", 173))
		{
			passData.source = texture;
			passData.destination = backBufferColor;
			passData.material = m_OnTileUberMaterial;
			passData.shaderPass = uberShaderPasses;
			if (uberShaderPasses == OnTilePostProcessPass.UberShaderPasses.TextureRead || uberShaderPasses == OnTilePostProcessPass.UberShaderPasses.TextureReadVisMesh)
			{
				rasterRenderGraphBuilder.UseTexture(in texture);
			}
			else
			{
				rasterRenderGraphBuilder.SetInputAttachment(texture, 0);
				rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			}
			rasterRenderGraphBuilder.UseTexture(in internalColorLut);
			passData.lutTexture = internalColorLut;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle = (passData.userLutTexture = TryGetCachedUserLutTextureHandle(component2, renderGraph));
			if (textureHandle.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in textureHandle);
			}
			rasterRenderGraphBuilder.SetRenderAttachment(backBufferColor, 0, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(OnTilePostProcessPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				ExecuteFBFetchPass(data, context);
			});
			passData.useXRVisibilityMesh = false;
			passData.msaaSamples = (int)textureDesc.msaaSamples;
			if (universalCameraData.xr.enabled)
			{
				global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags extendedFeatureFlags = global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible;
				if (flag2)
				{
					extendedFeatureFlags |= global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultisampledShaderResolve;
				}
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(extendedFeatureFlags);
				bool flag3 = universalCameraData.xrUniversal.canFoveateIntermediatePasses || universalResourceData.isActiveTargetBackBuffer;
				rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering && flag3);
				passData.useXRVisibilityMesh = flag;
				passData.xr = universalCameraData.xr;
			}
		}
		universalResourceData.activeColorID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
		universalResourceData.activeDepthID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
	}

	private static void ExecuteFBFetchPass(OnTilePostProcessPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
	{
		global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
		data.material.SetTexture(OnTilePostProcessPass.ShaderConstants._InternalLut, data.lutTexture);
		if (data.userLutTexture.IsValid())
		{
			data.material.SetTexture(OnTilePostProcessPass.ShaderConstants._UserLut, data.userLutTexture);
		}
		bool flag = global::UnityEngine.Rendering.Universal.RenderingUtils.IsHandleYFlipped(in context, in data.destination);
		data.material.SetVector(s_BlitScaleBias, (!flag) ? new global::UnityEngine.Vector4(1f, -1f, 0f, 1f) : new global::UnityEngine.Vector4(1f, 1f, 0f, 0f));
		if (data.shaderPass == OnTilePostProcessPass.UberShaderPasses.TextureRead || data.shaderPass == OnTilePostProcessPass.UberShaderPasses.TextureReadVisMesh)
		{
			data.material.SetTexture(s_BlitTexture, data.source);
		}
		else if (data.shaderPass == OnTilePostProcessPass.UberShaderPasses.MSAASoftwareResolve || data.shaderPass == OnTilePostProcessPass.UberShaderPasses.MSAASoftwareResolveVisMesh)
		{
			switch (data.msaaSamples)
			{
			case 4:
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, "_MSAA_2", state: false);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, "_MSAA_4", state: true);
				break;
			case 2:
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, "_MSAA_2", state: true);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, "_MSAA_4", state: false);
				break;
			default:
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, "_MSAA_2", state: false);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, "_MSAA_4", state: false);
				break;
			}
		}
		if (data.useXRVisibilityMesh)
		{
			global::UnityEngine.MaterialPropertyBlock materialPropertyBlock = global::UnityEngine.Rendering.Universal.XRSystemUniversal.GetMaterialPropertyBlock();
			data.xr.RenderVisibleMeshCustomMaterial(cmd, data.xr.occlusionMeshScale, data.material, materialPropertyBlock, (int)data.shaderPass);
		}
		else
		{
			cmd.DrawProcedural(global::UnityEngine.Matrix4x4.identity, data.material, (int)data.shaderPass, global::UnityEngine.MeshTopology.Triangles, 3, 1);
		}
	}

	private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle TryGetCachedUserLutTextureHandle(global::UnityEngine.Rendering.Universal.ColorLookup colorLookup, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
	{
		if (colorLookup.texture.value == null)
		{
			if (m_UserLut != null)
			{
				m_UserLut.Release();
				m_UserLut = null;
			}
		}
		else if (m_UserLut == null || m_UserLut.externalTexture != colorLookup.texture.value)
		{
			m_UserLut?.Release();
			m_UserLut = global::UnityEngine.Rendering.RTHandles.Alloc(colorLookup.texture.value);
		}
		if (m_UserLut == null)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
		}
		return renderGraph.ImportTexture(m_UserLut);
	}

	private void SetupLut(global::UnityEngine.Material material, global::UnityEngine.Rendering.Universal.ColorLookup colorLookup, global::UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments, int lutSize)
	{
		int num = lutSize * lutSize;
		float w = global::UnityEngine.Mathf.Pow(2f, colorAdjustments.postExposure.value);
		global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(1f / (float)num, 1f / (float)lutSize, (float)lutSize - 1f, w);
		global::UnityEngine.Vector4 value2 = ((!colorLookup.IsActive()) ? global::UnityEngine.Vector4.zero : new global::UnityEngine.Vector4(1f / (float)colorLookup.texture.value.width, 1f / (float)colorLookup.texture.value.height, (float)colorLookup.texture.value.height - 1f, colorLookup.contribution.value));
		material.SetVector(OnTilePostProcessPass.ShaderConstants._Lut_Params, value);
		material.SetVector(OnTilePostProcessPass.ShaderConstants._UserLut_Params, value2);
	}

	private void SetupVignette(global::UnityEngine.Material material, global::UnityEngine.Experimental.Rendering.XRPass xrPass, int width, int height, global::UnityEngine.Rendering.Universal.Vignette vignette)
	{
		global::UnityEngine.Color value = vignette.color.value;
		global::UnityEngine.Vector2 center = vignette.center.value;
		float num = (float)width / (float)height;
		if (xrPass != null && xrPass.enabled)
		{
			if (xrPass.singlePassEnabled)
			{
				material.SetVector(OnTilePostProcessPass.ShaderConstants._Vignette_ParamsXR, xrPass.ApplyXRViewCenterOffset(center));
			}
			else
			{
				center = xrPass.ApplyXRViewCenterOffset(center);
			}
		}
		global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4(value.r, value.g, value.b, vignette.rounded.value ? num : 1f);
		global::UnityEngine.Vector4 value3 = new global::UnityEngine.Vector4(center.x, center.y, vignette.intensity.value * 3f, vignette.smoothness.value * 5f);
		material.SetVector(OnTilePostProcessPass.ShaderConstants._Vignette_Params1, value2);
		material.SetVector(OnTilePostProcessPass.ShaderConstants._Vignette_Params2, value3);
	}

	private void SetupTonemapping(global::UnityEngine.Material onTileUberMaterial, global::UnityEngine.Rendering.Universal.Tonemapping tonemapping, bool isHdrGrading)
	{
		if (isHdrGrading)
		{
			global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_OnTileUberMaterial, "_HDR_GRADING", isHdrGrading);
			return;
		}
		global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_OnTileUberMaterial, "_TONEMAP_NEUTRAL", tonemapping.mode.value == global::UnityEngine.Rendering.Universal.TonemappingMode.Neutral);
		global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_OnTileUberMaterial, "_TONEMAP_ACES", tonemapping.mode.value == global::UnityEngine.Rendering.Universal.TonemappingMode.ACES);
	}

	private void SetupGrain(global::UnityEngine.Material onTileUberMaterial, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.FilmGrain filmgrain, global::UnityEngine.Rendering.Universal.PostProcessData data)
	{
		if (filmgrain.IsActive())
		{
			onTileUberMaterial.EnableKeyword("_FILM_GRAIN");
			global::UnityEngine.Rendering.Universal.PostProcessUtils.ConfigureFilmGrain(data, filmgrain, cameraData.pixelWidth, cameraData.pixelHeight, onTileUberMaterial);
		}
	}

	private void SetupDithering(global::UnityEngine.Material onTileUberMaterial, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.PostProcessData data)
	{
		if (cameraData.isDitheringEnabled)
		{
			onTileUberMaterial.EnableKeyword("_DITHERING");
			m_DitheringTextureIndex = global::UnityEngine.Rendering.Universal.PostProcessUtils.ConfigureDithering(data, m_DitheringTextureIndex, cameraData.pixelWidth, cameraData.pixelHeight, onTileUberMaterial);
		}
	}
}
