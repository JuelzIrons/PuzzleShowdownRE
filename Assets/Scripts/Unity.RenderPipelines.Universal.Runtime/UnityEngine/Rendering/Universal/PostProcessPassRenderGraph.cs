namespace UnityEngine.Rendering.Universal
{
	internal class PostProcessPassRenderGraph
	{
		internal static class ShaderConstants
		{
			public static readonly int _CameraDepthTextureID = global::UnityEngine.Shader.PropertyToID("_CameraDepthTexture");

			public static readonly int _StencilRef = global::UnityEngine.Shader.PropertyToID("_StencilRef");

			public static readonly int _StencilMask = global::UnityEngine.Shader.PropertyToID("_StencilMask");

			public static readonly int _ColorTexture = global::UnityEngine.Shader.PropertyToID("_ColorTexture");

			public static readonly int _Params = global::UnityEngine.Shader.PropertyToID("_Params");

			public static readonly int _Params2 = global::UnityEngine.Shader.PropertyToID("_Params2");

			public static readonly int _ViewProjM = global::UnityEngine.Shader.PropertyToID("_ViewProjM");

			public static readonly int _PrevViewProjM = global::UnityEngine.Shader.PropertyToID("_PrevViewProjM");

			public static readonly int _ViewProjMStereo = global::UnityEngine.Shader.PropertyToID("_ViewProjMStereo");

			public static readonly int _PrevViewProjMStereo = global::UnityEngine.Shader.PropertyToID("_PrevViewProjMStereo");

			public static readonly int _FullscreenProjMat = global::UnityEngine.Shader.PropertyToID("_FullscreenProjMat");

			public static readonly int _FullCoCTexture = global::UnityEngine.Shader.PropertyToID("_FullCoCTexture");

			public static readonly int _HalfCoCTexture = global::UnityEngine.Shader.PropertyToID("_HalfCoCTexture");

			public static readonly int _DofTexture = global::UnityEngine.Shader.PropertyToID("_DofTexture");

			public static readonly int _CoCParams = global::UnityEngine.Shader.PropertyToID("_CoCParams");

			public static readonly int _BokehKernel = global::UnityEngine.Shader.PropertyToID("_BokehKernel");

			public static readonly int _BokehConstants = global::UnityEngine.Shader.PropertyToID("_BokehConstants");

			public static readonly int _DownSampleScaleFactor = global::UnityEngine.Shader.PropertyToID("_DownSampleScaleFactor");

			public static readonly int _Metrics = global::UnityEngine.Shader.PropertyToID("_Metrics");

			public static readonly int _AreaTexture = global::UnityEngine.Shader.PropertyToID("_AreaTexture");

			public static readonly int _SearchTexture = global::UnityEngine.Shader.PropertyToID("_SearchTexture");

			public static readonly int _BlendTexture = global::UnityEngine.Shader.PropertyToID("_BlendTexture");

			public static readonly int _SourceTexLowMip = global::UnityEngine.Shader.PropertyToID("_SourceTexLowMip");

			public static readonly int _Bloom_Params = global::UnityEngine.Shader.PropertyToID("_Bloom_Params");

			public static readonly int _Bloom_Texture = global::UnityEngine.Shader.PropertyToID("_Bloom_Texture");

			public static readonly int _LensDirt_Texture = global::UnityEngine.Shader.PropertyToID("_LensDirt_Texture");

			public static readonly int _LensDirt_Params = global::UnityEngine.Shader.PropertyToID("_LensDirt_Params");

			public static readonly int _LensDirt_Intensity = global::UnityEngine.Shader.PropertyToID("_LensDirt_Intensity");

			public static readonly int _Distortion_Params1 = global::UnityEngine.Shader.PropertyToID("_Distortion_Params1");

			public static readonly int _Distortion_Params2 = global::UnityEngine.Shader.PropertyToID("_Distortion_Params2");

			public static readonly int _Chroma_Params = global::UnityEngine.Shader.PropertyToID("_Chroma_Params");

			public static readonly int _Vignette_Params1 = global::UnityEngine.Shader.PropertyToID("_Vignette_Params1");

			public static readonly int _Vignette_Params2 = global::UnityEngine.Shader.PropertyToID("_Vignette_Params2");

			public static readonly int _Vignette_ParamsXR = global::UnityEngine.Shader.PropertyToID("_Vignette_ParamsXR");

			public static readonly int _InternalLut = global::UnityEngine.Shader.PropertyToID("_InternalLut");

			public static readonly int _Lut_Params = global::UnityEngine.Shader.PropertyToID("_Lut_Params");

			public static readonly int _UserLut = global::UnityEngine.Shader.PropertyToID("_UserLut");

			public static readonly int _UserLut_Params = global::UnityEngine.Shader.PropertyToID("_UserLut_Params");
		}

		internal static class Constants
		{
			public const int k_MaxPyramidSize = 16;

			public const int k_GaussianDoFPassComputeCoc = 0;

			public const int k_GaussianDoFPassDownscalePrefilter = 1;

			public const int k_GaussianDoFPassBlurH = 2;

			public const int k_GaussianDoFPassBlurV = 3;

			public const int k_GaussianDoFPassComposite = 4;

			public const int k_BokehDoFPassComputeCoc = 0;

			public const int k_BokehDoFPassDownscalePrefilter = 1;

			public const int k_BokehDoFPassBlur = 2;

			public const int k_BokehDoFPassPostFilter = 3;

			public const int k_BokehDoFPassComposite = 4;
		}

		private class UpdateCameraResolutionPassData
		{
			internal global::UnityEngine.Vector2Int newCameraTargetSize;
		}

		private class StopNaNsPassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Material stopNaN;
		}

		private class SMAASetupPassData
		{
			internal global::UnityEngine.Vector4 metrics;

			internal global::UnityEngine.Texture2D areaTexture;

			internal global::UnityEngine.Texture2D searchTexture;

			internal float stencilRef;

			internal float stencilMask;

			internal global::UnityEngine.Rendering.Universal.AntialiasingQuality antialiasingQuality;

			internal global::UnityEngine.Material material;
		}

		private class SMAAPassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle blendTexture;

			internal global::UnityEngine.Material material;
		}

		private class UberSetupBloomPassData
		{
			internal global::UnityEngine.Vector4 bloomParams;

			internal global::UnityEngine.Vector4 dirtScaleOffset;

			internal float dirtIntensity;

			internal global::UnityEngine.Texture dirtTexture;

			internal bool highQualityFilteringValue;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle bloomTexture;

			internal global::UnityEngine.Material uberMaterial;
		}

		private class BloomPassData
		{
			internal int mipCount;

			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Material[] upsampleMaterials;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] bloomMipUp;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] bloomMipDown;
		}

		internal struct BloomMaterialParams
		{
			internal global::UnityEngine.Vector4 parameters;

			internal global::UnityEngine.Vector4 parameters2;

			internal global::UnityEngine.Rendering.Universal.BloomFilterMode bloomFilter;

			internal bool highQualityFiltering;

			internal bool enableAlphaOutput;

			internal bool Equals(ref global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomMaterialParams other)
			{
				if (parameters == other.parameters && parameters2 == other.parameters2 && highQualityFiltering == other.highQualityFiltering && enableAlphaOutput == other.enableAlphaOutput)
				{
					return bloomFilter == other.bloomFilter;
				}
				return false;
			}
		}

		private class DoFGaussianPassData
		{
			internal int downsample;

			internal global::UnityEngine.Rendering.Universal.RenderingData renderingData;

			internal global::UnityEngine.Vector3 cocParams;

			internal bool highQualitySamplingValue;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTexture;

			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Material materialCoC;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle halfCoCTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle fullCoCTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle pingTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle pongTexture;

			internal global::UnityEngine.Rendering.RenderTargetIdentifier[] multipleRenderTargets = new global::UnityEngine.Rendering.RenderTargetIdentifier[2];

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;
		}

		private class DoFBokehPassData
		{
			internal global::UnityEngine.Vector4[] bokehKernel;

			internal int downSample;

			internal float uvMargin;

			internal global::UnityEngine.Vector4 cocParams;

			internal bool useFastSRGBLinearConversion;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTexture;

			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Material materialCoC;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle halfCoCTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle fullCoCTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle pingTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle pongTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;
		}

		private class PaniniProjectionPassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destinationTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Vector4 paniniParams;

			internal bool isPaniniGeneric;
		}

		private class MotionBlurPassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle motionVectors;

			internal global::UnityEngine.Material material;

			internal int passIndex;

			internal global::UnityEngine.Camera camera;

			internal global::UnityEngine.Experimental.Rendering.XRPass xr;

			internal float intensity;

			internal float clamp;

			internal bool enableAlphaOutput;
		}

		private class LensFlarePassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destinationTexture;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Rect viewport;

			internal float paniniDistance;

			internal float paniniCropToFit;

			internal float width;

			internal float height;

			internal bool usePanini;
		}

		private class LensFlareScreenSpacePassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle streakTmpTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle streakTmpTexture2;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle originalBloomTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle screenSpaceLensFlareBloomMipTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle result;

			internal int actualWidth;

			internal int actualHeight;

			internal global::UnityEngine.Camera camera;

			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Rendering.Universal.ScreenSpaceLensFlare lensFlareScreenSpace;

			internal int downsample;
		}

		private class PostProcessingFinalSetupPassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destinationTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;
		}

		private class PostProcessingFinalFSRScalePassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Material material;

			internal bool enableAlphaOutput;

			internal global::UnityEngine.Vector2 fsrInputSize;

			internal global::UnityEngine.Vector2 fsrOutputSize;
		}

		private class PostProcessingFinalBlitPassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destinationTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.FinalBlitSettings settings;
		}

		public struct FinalBlitSettings
		{
			public bool isFxaaEnabled;

			public bool isFsrEnabled;

			public bool isTaaSharpeningEnabled;

			public bool requireHDROutput;

			public bool isAlphaOutputEnabled;

			public global::UnityEngine.Rendering.HDROutputUtils.Operation hdrOperations;

			public static global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.FinalBlitSettings Create()
			{
				return new global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.FinalBlitSettings
				{
					isFxaaEnabled = false,
					isFsrEnabled = false,
					isTaaSharpeningEnabled = false,
					requireHDROutput = false,
					isAlphaOutputEnabled = false,
					hdrOperations = global::UnityEngine.Rendering.HDROutputUtils.Operation.None
				};
			}
		}

		private class UberPostPassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destinationTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle lutTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle bloomTexture;

			internal global::UnityEngine.Vector4 lutParams;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle userLutTexture;

			internal global::UnityEngine.Vector4 userLutParams;

			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.Universal.TonemappingMode toneMappingMode;

			internal bool isHdrGrading;

			internal bool isBackbuffer;

			internal bool enableAlphaOutput;

			internal bool hasFinalPass;
		}

		private class PostFXSetupPassData
		{
		}

		private global::UnityEngine.Rendering.Universal.PostProcessMaterialLibrary m_Materials;

		private global::UnityEngine.Rendering.Universal.DepthOfField m_DepthOfField;

		private global::UnityEngine.Rendering.Universal.MotionBlur m_MotionBlur;

		private global::UnityEngine.Rendering.Universal.PaniniProjection m_PaniniProjection;

		private global::UnityEngine.Rendering.Universal.Bloom m_Bloom;

		private global::UnityEngine.Rendering.Universal.ScreenSpaceLensFlare m_LensFlareScreenSpace;

		private global::UnityEngine.Rendering.Universal.LensDistortion m_LensDistortion;

		private global::UnityEngine.Rendering.Universal.ChromaticAberration m_ChromaticAberration;

		private global::UnityEngine.Rendering.Universal.Vignette m_Vignette;

		private global::UnityEngine.Rendering.Universal.ColorLookup m_ColorLookup;

		private global::UnityEngine.Rendering.Universal.ColorAdjustments m_ColorAdjustments;

		private global::UnityEngine.Rendering.Universal.Tonemapping m_Tonemapping;

		private global::UnityEngine.Rendering.Universal.FilmGrain m_FilmGrain;

		private string[] m_BloomMipDownName;

		private string[] m_BloomMipUpName;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] _BloomMipUp;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] _BloomMipDown;

		private global::UnityEngine.Rendering.RTHandle m_UserLut;

		private global::UnityEngine.Rendering.RTHandle m_InternalLut;

		private readonly global::UnityEngine.Experimental.Rendering.GraphicsFormat m_SMAAEdgeFormat;

		private readonly global::UnityEngine.Experimental.Rendering.GraphicsFormat m_BloomColorFormat;

		private global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomMaterialParams m_BloomParamsPrev;

		private readonly global::UnityEngine.Experimental.Rendering.GraphicsFormat m_GaussianCoCFormat;

		private readonly global::UnityEngine.Experimental.Rendering.GraphicsFormat m_GaussianDoFColorFormat;

		private global::UnityEngine.Vector4[] m_BokehKernel;

		private int m_BokehHash;

		private float m_BokehMaxRadius;

		private float m_BokehRCPAspect;

		private readonly global::UnityEngine.Experimental.Rendering.GraphicsFormat m_LensFlareScreenSpaceColorFormat;

		private int m_DitheringTextureIndex;

		private bool m_HasFinalPass;

		private bool m_EnableColorEncodingIfNeeded;

		private bool m_UseFastSRGBLinearConversion;

		private bool m_SupportScreenSpaceLensFlare;

		private bool m_SupportDataDrivenLensFlare;

		private const string _TemporalAATargetName = "_TemporalAATarget";

		private const string _UpscaledColorTargetName = "_CameraColorUpscaledSTP";

		public PostProcessPassRenderGraph(global::UnityEngine.Rendering.Universal.PostProcessData data, global::UnityEngine.Experimental.Rendering.GraphicsFormat requestPostProColorFormat)
		{
			m_Materials = new global::UnityEngine.Rendering.Universal.PostProcessMaterialLibrary(data);
			m_BloomMipDownName = new string[16];
			m_BloomMipUpName = new string[16];
			for (int i = 0; i < 16; i++)
			{
				m_BloomMipUpName[i] = "_BloomMipUp" + i;
				m_BloomMipDownName[i] = "_BloomMipDown" + i;
			}
			_BloomMipUp = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[16];
			_BloomMipDown = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[16];
			bool num = IsHDRFormat(requestPostProColorFormat);
			global::UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			graphicsFormat = (m_BloomColorFormat = ((!num) ? ((global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear) ? global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB : global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm) : (global::UnityEngine.SystemInfo.IsFormatSupported(requestPostProColorFormat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend) ? requestPostProColorFormat : ((!global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend)) ? ((global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear) ? global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB : global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm) : global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32))));
			if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8_UNorm, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render) && global::UnityEngine.SystemInfo.graphicsDeviceVendor.ToLowerInvariant().Contains("arm"))
			{
				m_SMAAEdgeFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8_UNorm;
			}
			else
			{
				m_SMAAEdgeFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
			}
			if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_UNorm, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend))
			{
				m_GaussianCoCFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_UNorm;
			}
			else if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend))
			{
				m_GaussianCoCFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat;
			}
			else
			{
				m_GaussianCoCFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm;
			}
			m_GaussianDoFColorFormat = graphicsFormat;
			m_LensFlareScreenSpaceColorFormat = graphicsFormat;
		}

		public void Cleanup()
		{
			m_Materials.Cleanup();
			Dispose();
		}

		public void Dispose()
		{
			m_UserLut?.Release();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static bool IsHDRFormat(global::UnityEngine.Experimental.Rendering.GraphicsFormat format)
		{
			if (format != global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32 && !global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsHalfFormat(format))
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsFloatFormat(format);
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static bool IsAlphaFormat(global::UnityEngine.Experimental.Rendering.GraphicsFormat format)
		{
			return global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.HasAlphaChannel(format);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private bool RequireSRGBConversionBlitToBackBuffer(bool requireSrgbConversion)
		{
			if (requireSrgbConversion)
			{
				return m_EnableColorEncodingIfNeeded;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static bool RequireHDROutput(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (cameraData.isHDROutputActive)
			{
				return cameraData.captureActions == null;
			}
			return false;
		}

		private void UpdateCameraResolution(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Vector2Int newCameraTargetSize)
		{
			cameraData.cameraTargetDescriptor.width = newCameraTargetSize.x;
			cameraData.cameraTargetDescriptor.height = newCameraTargetSize.y;
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.UpdateCameraResolutionPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.UpdateCameraResolutionPassData>("Update Camera Resolution", out passData, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 320);
			passData.newCameraTargetSize = newCameraTargetSize;
			unsafeRenderGraphBuilder.AllowGlobalStateModification(value: true);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.UpdateCameraResolutionPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext ctx)
			{
				ctx.cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.screenSize, new global::UnityEngine.Vector4(data.newCameraTargetSize.x, data.newCameraTargetSize.y, 1f / (float)data.newCameraTargetSize.x, 1f / (float)data.newCameraTargetSize.y));
			});
		}

		internal static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateCompatibleTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, string name, bool clear, global::UnityEngine.FilterMode filterMode)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = source.GetDescriptor(renderGraph);
			MakeCompatible(ref desc);
			desc.name = name;
			desc.clearBuffer = clear;
			desc.filterMode = filterMode;
			return renderGraph.CreateTexture(in desc);
		}

		internal static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateCompatibleTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc, string name, bool clear, global::UnityEngine.FilterMode filterMode)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc2 = GetCompatibleDescriptor(desc);
			desc2.name = name;
			desc2.clearBuffer = clear;
			desc2.filterMode = filterMode;
			return renderGraph.CreateTexture(in desc2);
		}

		internal static global::UnityEngine.Rendering.RenderGraphModule.TextureDesc GetCompatibleDescriptor(global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc, int width, int height, global::UnityEngine.Experimental.Rendering.GraphicsFormat format)
		{
			desc.width = width;
			desc.height = height;
			desc.format = format;
			MakeCompatible(ref desc);
			return desc;
		}

		internal static global::UnityEngine.Rendering.RenderGraphModule.TextureDesc GetCompatibleDescriptor(global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc)
		{
			MakeCompatible(ref desc);
			return desc;
		}

		internal static void MakeCompatible(ref global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc)
		{
			desc.msaaSamples = global::UnityEngine.Rendering.MSAASamples.None;
			desc.useMipMap = false;
			desc.autoGenerateMips = false;
			desc.anisoLevel = 0;
			desc.discardBuffer = false;
		}

		internal static global::UnityEngine.RenderTextureDescriptor GetCompatibleDescriptor(global::UnityEngine.RenderTextureDescriptor desc, int width, int height, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
		{
			desc.depthStencilFormat = depthStencilFormat;
			desc.msaaSamples = 1;
			desc.width = width;
			desc.height = height;
			desc.graphicsFormat = format;
			return desc;
		}

		public void RenderStopNaN(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle activeCameraColor, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle stopNaNTarget)
		{
			stopNaNTarget = CreateCompatibleTexture(renderGraph, in activeCameraColor, "_StopNaNsTarget", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.StopNaNsPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.StopNaNsPassData>("Stop NaNs", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_StopNaNs), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 409);
			rasterRenderGraphBuilder.SetRenderAttachment(stopNaNTarget, 0, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.sourceTexture = activeCameraColor;
			rasterRenderGraphBuilder.UseTexture(in activeCameraColor);
			passData.stopNaN = m_Materials.stopNaN;
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.StopNaNsPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
				global::UnityEngine.Rendering.RTHandle rTHandle = data.sourceTexture;
				global::UnityEngine.Vector2 vector = (rTHandle.useScaling ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, rTHandle, vector, data.stopNaN, 0);
			});
		}

		public void RenderSMAA(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.AntialiasingQuality antialiasingQuality, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle SMAATarget)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = renderGraph.GetTextureDesc(in source);
			SMAATarget = CreateCompatibleTexture(renderGraph, in desc, "_SMAATarget", clear: true, global::UnityEngine.FilterMode.Bilinear);
			desc.clearColor = global::UnityEngine.Color.black;
			desc.clearColor.a = 0f;
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc2 = desc;
			desc2.format = m_SMAAEdgeFormat;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = CreateCompatibleTexture(renderGraph, in desc2, "_EdgeStencilTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc3 = desc;
			desc3.format = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthStencilFormat(24);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex = CreateCompatibleTexture(renderGraph, in desc3, "_EdgeTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc4 = desc;
			desc4.format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input2 = CreateCompatibleTexture(renderGraph, in desc4, "_BlendTexture", clear: true, global::UnityEngine.FilterMode.Point);
			global::UnityEngine.Material subpixelMorphologicalAntialiasing = m_Materials.subpixelMorphologicalAntialiasing;
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAASetupPassData passData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAASetupPassData>("SMAA Material Setup", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_SMAAMaterialSetup), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 470))
			{
				passData.metrics = new global::UnityEngine.Vector4(1f / (float)desc.width, 1f / (float)desc.height, desc.width, desc.height);
				passData.areaTexture = m_Materials.resources.textures.smaaAreaTex;
				passData.searchTexture = m_Materials.resources.textures.smaaSearchTex;
				passData.stencilRef = 64f;
				passData.stencilMask = 64f;
				passData.antialiasingQuality = antialiasingQuality;
				passData.material = subpixelMorphologicalAntialiasing;
				rasterRenderGraphBuilder.AllowPassCulling(value: false);
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAASetupPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
				{
					data.material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Metrics, data.metrics);
					data.material.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._AreaTexture, data.areaTexture);
					data.material.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._SearchTexture, data.searchTexture);
					data.material.SetFloat(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._StencilRef, data.stencilRef);
					data.material.SetFloat(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._StencilMask, data.stencilMask);
					data.material.shaderKeywords = null;
					switch (data.antialiasingQuality)
					{
					case global::UnityEngine.Rendering.Universal.AntialiasingQuality.Low:
						data.material.EnableKeyword("_SMAA_PRESET_LOW");
						break;
					case global::UnityEngine.Rendering.Universal.AntialiasingQuality.Medium:
						data.material.EnableKeyword("_SMAA_PRESET_MEDIUM");
						break;
					case global::UnityEngine.Rendering.Universal.AntialiasingQuality.High:
						data.material.EnableKeyword("_SMAA_PRESET_HIGH");
						break;
					}
				});
			}
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAAPassData passData2;
			using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder2 = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAAPassData>("SMAA Edge Detection", out passData2, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_SMAAEdgeDetection), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 511))
			{
				rasterRenderGraphBuilder2.SetRenderAttachment(input, 0);
				rasterRenderGraphBuilder2.SetRenderAttachmentDepth(tex);
				passData2.sourceTexture = source;
				rasterRenderGraphBuilder2.UseTexture(in source);
				rasterRenderGraphBuilder2.UseTexture(resourceData.cameraDepth);
				passData2.material = subpixelMorphologicalAntialiasing;
				rasterRenderGraphBuilder2.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAAPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
				{
					global::UnityEngine.Material material = data.material;
					global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
					global::UnityEngine.Rendering.RTHandle rTHandle = data.sourceTexture;
					global::UnityEngine.Vector2 vector = (rTHandle.useScaling ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
					global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, rTHandle, vector, material, 0);
				});
			}
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAAPassData passData3;
			using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder3 = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAAPassData>("SMAA Blend weights", out passData3, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_SMAABlendWeight), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 532))
			{
				rasterRenderGraphBuilder3.SetRenderAttachment(input2, 0);
				rasterRenderGraphBuilder3.SetRenderAttachmentDepth(tex, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);
				passData3.sourceTexture = input;
				rasterRenderGraphBuilder3.UseTexture(in input);
				passData3.material = subpixelMorphologicalAntialiasing;
				rasterRenderGraphBuilder3.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAAPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
				{
					global::UnityEngine.Material material = data.material;
					global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
					global::UnityEngine.Rendering.RTHandle rTHandle = data.sourceTexture;
					global::UnityEngine.Vector2 vector = (rTHandle.useScaling ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
					global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, rTHandle, vector, material, 1);
				});
			}
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAAPassData passData4;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder4 = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAAPassData>("SMAA Neighborhood blending", out passData4, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_SMAANeighborhoodBlend), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 552);
			rasterRenderGraphBuilder4.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder4.SetRenderAttachment(SMAATarget, 0);
			passData4.sourceTexture = source;
			rasterRenderGraphBuilder4.UseTexture(in source);
			passData4.blendTexture = input2;
			rasterRenderGraphBuilder4.UseTexture(in input2);
			passData4.material = subpixelMorphologicalAntialiasing;
			rasterRenderGraphBuilder4.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.SMAAPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Material material = data.material;
				global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
				global::UnityEngine.Rendering.RTHandle rTHandle = data.sourceTexture;
				material.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._BlendTexture, data.blendTexture);
				global::UnityEngine.Vector2 vector = (rTHandle.useScaling ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, rTHandle, vector, material, 2);
			});
		}

		public void UberPostSetupBloomPass(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph rendergraph, global::UnityEngine.Material uberMaterial, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc srcDesc)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_UberPostSetupBloomPass)))
			{
				global::UnityEngine.Color color = m_Bloom.tint.value.linear;
				float num = global::UnityEngine.Rendering.ColorUtils.Luminance(in color);
				color = ((num > 0f) ? (color * (1f / num)) : global::UnityEngine.Color.white);
				global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(m_Bloom.intensity.value, color.r, color.g, color.b);
				global::UnityEngine.Texture texture = ((m_Bloom.dirtTexture.value == null) ? global::UnityEngine.Texture2D.blackTexture : m_Bloom.dirtTexture.value);
				float num2 = (float)texture.width / (float)texture.height;
				float num3 = (float)srcDesc.width / (float)srcDesc.height;
				global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4(1f, 1f, 0f, 0f);
				float value3 = m_Bloom.dirtIntensity.value;
				if (num2 > num3)
				{
					value2.x = num3 / num2;
					value2.z = (1f - value2.x) * 0.5f;
				}
				else if (num3 > num2)
				{
					value2.y = num2 / num3;
					value2.w = (1f - value2.y) * 0.5f;
				}
				bool value4 = m_Bloom.highQualityFiltering.value;
				uberMaterial.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Bloom_Params, value);
				uberMaterial.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._LensDirt_Params, value2);
				uberMaterial.SetFloat(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._LensDirt_Intensity, value3);
				uberMaterial.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._LensDirt_Texture, texture);
				if (value4)
				{
					uberMaterial.EnableKeyword((value3 > 0f) ? "_BLOOM_HQ_DIRT" : "_BLOOM_HQ");
				}
				else
				{
					uberMaterial.EnableKeyword((value3 > 0f) ? "_BLOOM_LQ_DIRT" : "_BLOOM_LQ");
				}
			}
		}

		public global::UnityEngine.Vector2Int CalcBloomResolution(global::UnityEngine.Rendering.Universal.Bloom bloom, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc bloomSourceDesc)
		{
			int num = 1;
			num = m_Bloom.downscale.value switch
			{
				global::UnityEngine.Rendering.Universal.BloomDownscaleMode.Half => 1, 
				global::UnityEngine.Rendering.Universal.BloomDownscaleMode.Quarter => 2, 
				_ => throw new global::System.ArgumentOutOfRangeException(), 
			};
			int x = global::UnityEngine.Mathf.Max(1, bloomSourceDesc.width >> num);
			int y = global::UnityEngine.Mathf.Max(1, bloomSourceDesc.height >> num);
			return new global::UnityEngine.Vector2Int(x, y);
		}

		public int CalcBloomMipCount(global::UnityEngine.Rendering.Universal.Bloom bloom, global::UnityEngine.Vector2Int bloomResolution)
		{
			return global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.FloorToInt(global::UnityEngine.Mathf.Log(global::UnityEngine.Mathf.Max(bloomResolution.x, bloomResolution.y), 2f) - 1f), 1, m_Bloom.maxIterations.value);
		}

		public void RenderBloomTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, bool enableAlphaOutput)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc bloomSourceDesc = source.GetDescriptor(renderGraph);
			global::UnityEngine.Vector2Int bloomResolution = CalcBloomResolution(m_Bloom, in bloomSourceDesc);
			int num = CalcBloomMipCount(m_Bloom, bloomResolution);
			int num2 = bloomResolution.x;
			int num3 = bloomResolution.y;
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_BloomSetup)))
			{
				float value = m_Bloom.clamp.value;
				float num4 = global::UnityEngine.Mathf.GammaToLinearSpace(m_Bloom.threshold.value);
				float w = num4 * 0.5f;
				float x = global::UnityEngine.Mathf.Lerp(0.05f, 0.95f, m_Bloom.scatter.value);
				float y = global::UnityEngine.Mathf.Clamp01(m_Bloom.scatter.value);
				float num5 = global::UnityEngine.Mathf.Lerp(0.3f, 1.3f, m_Bloom.scatter.value);
				global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomMaterialParams other = new global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomMaterialParams
				{
					parameters = new global::UnityEngine.Vector4(x, value, num4, w),
					parameters2 = new global::UnityEngine.Vector4(0.5f, y, num5, 0.5f * num5),
					bloomFilter = m_Bloom.filter.value,
					highQualityFiltering = m_Bloom.highQualityFiltering.value,
					enableAlphaOutput = enableAlphaOutput
				};
				global::UnityEngine.Material bloom = m_Materials.bloom;
				bool num6 = !m_BloomParamsPrev.Equals(ref other);
				bool flag = bloom.HasProperty(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Params);
				if (num6 || !flag)
				{
					bloom.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Params, other.parameters);
					bloom.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Params2, other.parameters2);
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(bloom, "_BLOOM_HQ", other.highQualityFiltering);
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(bloom, "_ENABLE_ALPHA_OUTPUT", other.enableAlphaOutput);
					for (uint num7 = 0u; num7 < 16; num7++)
					{
						global::UnityEngine.Material obj = m_Materials.bloomUpsample[num7];
						obj.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Params, other.parameters);
						global::UnityEngine.Rendering.CoreUtils.SetKeyword(obj, "_BLOOM_HQ", other.highQualityFiltering);
						global::UnityEngine.Rendering.CoreUtils.SetKeyword(obj, "_ENABLE_ALPHA_OUTPUT", other.enableAlphaOutput);
						float x2 = 0.5f + (float)((num7 > num / 2) ? (num7 - 1) : num7);
						global::UnityEngine.Vector4 parameters = other.parameters2;
						parameters.x = x2;
						obj.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Params2, parameters);
					}
					m_BloomParamsPrev = other;
				}
				global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = GetCompatibleDescriptor(bloomSourceDesc, num2, num3, m_BloomColorFormat);
				_BloomMipDown[0] = CreateCompatibleTexture(renderGraph, in desc, m_BloomMipDownName[0], clear: false, global::UnityEngine.FilterMode.Bilinear);
				_BloomMipUp[0] = CreateCompatibleTexture(renderGraph, in desc, m_BloomMipUpName[0], clear: false, global::UnityEngine.FilterMode.Bilinear);
				if (other.bloomFilter != global::UnityEngine.Rendering.Universal.BloomFilterMode.Kawase)
				{
					for (int i = 1; i < num; i++)
					{
						num2 = global::UnityEngine.Mathf.Max(1, num2 >> 1);
						num3 = global::UnityEngine.Mathf.Max(1, num3 >> 1);
						ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle reference = ref _BloomMipDown[i];
						ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle reference2 = ref _BloomMipUp[i];
						desc.width = num2;
						desc.height = num3;
						reference = CreateCompatibleTexture(renderGraph, in desc, m_BloomMipDownName[i], clear: false, global::UnityEngine.FilterMode.Bilinear);
						reference2 = CreateCompatibleTexture(renderGraph, in desc, m_BloomMipUpName[i], clear: false, global::UnityEngine.FilterMode.Bilinear);
					}
				}
			}
			switch (m_Bloom.filter.value)
			{
			case global::UnityEngine.Rendering.Universal.BloomFilterMode.Dual:
				destination = BloomDual(renderGraph, source, num);
				break;
			case global::UnityEngine.Rendering.Universal.BloomFilterMode.Kawase:
				destination = BloomKawase(renderGraph, source, num);
				break;
			default:
				destination = BloomGaussian(renderGraph, source, num);
				break;
			}
		}

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle BloomGaussian(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, int mipCount)
		{
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomPassData>("Blit Bloom Mipmaps", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.Bloom), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 803);
			passData.mipCount = mipCount;
			passData.material = m_Materials.bloom;
			passData.upsampleMaterials = m_Materials.bloomUpsample;
			passData.sourceTexture = source;
			passData.bloomMipDown = _BloomMipDown;
			passData.bloomMipUp = _BloomMipUp;
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.UseTexture(in source);
			for (int i = 0; i < mipCount; i++)
			{
				unsafeRenderGraphBuilder.UseTexture(in _BloomMipDown[i], global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
				unsafeRenderGraphBuilder.UseTexture(in _BloomMipUp[i], global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			}
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
				global::UnityEngine.Material material = data.material;
				int mipCount2 = data.mipCount;
				global::UnityEngine.Rendering.RenderBufferLoadAction loadAction = global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare;
				global::UnityEngine.Rendering.RenderBufferStoreAction storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.Store;
				using (new global::UnityEngine.Rendering.ProfilingScope(nativeCommandBuffer, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_BloomPrefilter)))
				{
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.sourceTexture, data.bloomMipDown[0], loadAction, storeAction, material, 0);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(nativeCommandBuffer, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_BloomDownsample)))
				{
					global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle = data.bloomMipDown[0];
					for (int j = 1; j < mipCount2; j++)
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle2 = data.bloomMipDown[j];
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle3 = data.bloomMipUp[j];
						global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, textureHandle, textureHandle3, loadAction, storeAction, material, 1);
						global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, textureHandle3, textureHandle2, loadAction, storeAction, material, 2);
						textureHandle = textureHandle2;
					}
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(nativeCommandBuffer, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_BloomUpsample)))
				{
					for (int num = mipCount2 - 2; num >= 0; num--)
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle4 = ((num == mipCount2 - 2) ? data.bloomMipDown[num + 1] : data.bloomMipUp[num + 1]);
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle5 = data.bloomMipDown[num];
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle6 = data.bloomMipUp[num];
						global::UnityEngine.Material material2 = data.upsampleMaterials[num];
						material2.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._SourceTexLowMip, textureHandle4);
						global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, textureHandle5, textureHandle6, loadAction, storeAction, material2, 3);
					}
				}
			});
			return (mipCount == 1) ? passData.bloomMipDown[0] : passData.bloomMipUp[0];
		}

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle BloomKawase(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, int mipCount)
		{
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomPassData>("Blit Bloom Mipmaps (Kawase)", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.Bloom), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 886);
			passData.mipCount = mipCount;
			passData.material = m_Materials.bloom;
			passData.upsampleMaterials = m_Materials.bloomUpsample;
			passData.sourceTexture = source;
			passData.bloomMipDown = _BloomMipDown;
			passData.bloomMipUp = _BloomMipUp;
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.UseTexture(in source);
			unsafeRenderGraphBuilder.UseTexture(in _BloomMipDown[0], global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			unsafeRenderGraphBuilder.UseTexture(in _BloomMipUp[0], global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
				global::UnityEngine.Material material = data.material;
				int mipCount2 = data.mipCount;
				global::UnityEngine.Rendering.RenderBufferLoadAction loadAction = global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare;
				global::UnityEngine.Rendering.RenderBufferStoreAction storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.Store;
				using (new global::UnityEngine.Rendering.ProfilingScope(nativeCommandBuffer, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_BloomPrefilter)))
				{
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.sourceTexture, data.bloomMipDown[0], loadAction, storeAction, material, 0);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(nativeCommandBuffer, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_BloomDownsample)))
				{
					for (int i = 0; i < mipCount2; i++)
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle = (((i & 1) == 0) ? data.bloomMipDown[0] : data.bloomMipUp[0]);
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle2 = (((i & 1) == 0) ? data.bloomMipUp[0] : data.bloomMipDown[0]);
						global::UnityEngine.Material material2 = data.upsampleMaterials[i];
						global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, textureHandle, textureHandle2, loadAction, storeAction, material2, 4);
					}
				}
			});
			return (((mipCount - 1) & 1) == 0) ? _BloomMipUp[0] : _BloomMipDown[0];
		}

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle BloomDual(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, int mipCount)
		{
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomPassData>("Blit Bloom Mipmaps (Dual)", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.Bloom), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 939);
			passData.mipCount = mipCount;
			passData.material = m_Materials.bloom;
			passData.upsampleMaterials = m_Materials.bloomUpsample;
			passData.sourceTexture = source;
			passData.bloomMipDown = _BloomMipDown;
			passData.bloomMipUp = _BloomMipUp;
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.UseTexture(in source);
			for (int i = 0; i < mipCount; i++)
			{
				unsafeRenderGraphBuilder.UseTexture(in _BloomMipDown[i], global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
				unsafeRenderGraphBuilder.UseTexture(in _BloomMipUp[i], global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			}
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.BloomPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
				global::UnityEngine.Material material = data.material;
				int mipCount2 = data.mipCount;
				global::UnityEngine.Rendering.RenderBufferLoadAction loadAction = global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare;
				global::UnityEngine.Rendering.RenderBufferStoreAction storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.Store;
				using (new global::UnityEngine.Rendering.ProfilingScope(nativeCommandBuffer, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_BloomPrefilter)))
				{
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.sourceTexture, data.bloomMipDown[0], loadAction, storeAction, material, 0);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(nativeCommandBuffer, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_BloomDownsample)))
				{
					_ = ref data.bloomMipDown[0];
					for (int j = 1; j < mipCount2; j++)
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle = data.bloomMipDown[j - 1];
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle2 = data.bloomMipDown[j];
						global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, textureHandle, textureHandle2, loadAction, storeAction, material, 5);
					}
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(nativeCommandBuffer, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_BloomUpsample)))
				{
					for (int num = mipCount2 - 2; num >= 0; num--)
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle3 = ((num == mipCount2 - 2) ? data.bloomMipDown[num + 1] : data.bloomMipUp[num + 1]);
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle4 = data.bloomMipUp[num];
						global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, textureHandle3, textureHandle4, loadAction, storeAction, material, 6);
					}
				}
			});
			return (mipCount == 1) ? passData.bloomMipDown[0] : passData.bloomMipUp[0];
		}

		public void RenderDoF(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
		{
			global::UnityEngine.Material dofMaterial = ((m_DepthOfField.mode.value == global::UnityEngine.Rendering.Universal.DepthOfFieldMode.Gaussian) ? m_Materials.gaussianDepthOfField : m_Materials.bokehDepthOfField);
			destination = CreateCompatibleTexture(renderGraph, in source, "_DoFTarget", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.CoreUtils.SetKeyword(dofMaterial, "_ENABLE_ALPHA_OUTPUT", cameraData.isAlphaOutputEnabled);
			if (m_DepthOfField.mode.value == global::UnityEngine.Rendering.Universal.DepthOfFieldMode.Gaussian)
			{
				RenderDoFGaussian(renderGraph, resourceData, cameraData, in source, destination, ref dofMaterial);
			}
			else if (m_DepthOfField.mode.value == global::UnityEngine.Rendering.Universal.DepthOfFieldMode.Bokeh)
			{
				RenderDoFBokeh(renderGraph, resourceData, cameraData, in source, in destination, ref dofMaterial);
			}
		}

		public void RenderDoFGaussian(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, ref global::UnityEngine.Material dofMaterial)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor = source.GetDescriptor(renderGraph);
			global::UnityEngine.Material material = dofMaterial;
			int num = 2;
			int num2 = descriptor.width / num;
			int height = descriptor.height / num;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = CreateCompatibleTexture(renderGraph, GetCompatibleDescriptor(descriptor, descriptor.width, descriptor.height, m_GaussianCoCFormat), "_FullCoCTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input2 = CreateCompatibleTexture(renderGraph, GetCompatibleDescriptor(descriptor, num2, height, m_GaussianCoCFormat), "_HalfCoCTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input3 = CreateCompatibleTexture(renderGraph, GetCompatibleDescriptor(descriptor, num2, height, m_GaussianDoFColorFormat), "_PingTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input4 = CreateCompatibleTexture(renderGraph, GetCompatibleDescriptor(descriptor, num2, height, m_GaussianDoFColorFormat), "_PongTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.DoFGaussianPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.DoFGaussianPassData>("Depth of Field - Gaussian", out passData, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 1066);
			float value = m_DepthOfField.gaussianStart.value;
			float y = global::UnityEngine.Mathf.Max(value, m_DepthOfField.gaussianEnd.value);
			float a = m_DepthOfField.gaussianMaxRadius.value * ((float)num2 / 1080f);
			a = global::UnityEngine.Mathf.Min(a, 2f);
			passData.downsample = num;
			passData.cocParams = new global::UnityEngine.Vector3(value, y, a);
			passData.highQualitySamplingValue = m_DepthOfField.highQualitySampling.value;
			passData.material = material;
			passData.materialCoC = m_Materials.gaussianDepthOfFieldCoC;
			passData.sourceTexture = source;
			unsafeRenderGraphBuilder.UseTexture(in source);
			passData.depthTexture = resourceData.cameraDepthTexture;
			unsafeRenderGraphBuilder.UseTexture(resourceData.cameraDepthTexture);
			passData.fullCoCTexture = input;
			unsafeRenderGraphBuilder.UseTexture(in input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.halfCoCTexture = input2;
			unsafeRenderGraphBuilder.UseTexture(in input2, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.pingTexture = input3;
			unsafeRenderGraphBuilder.UseTexture(in input3, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.pongTexture = input4;
			unsafeRenderGraphBuilder.UseTexture(in input4, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.destination = destination;
			unsafeRenderGraphBuilder.UseTexture(in destination, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.DoFGaussianPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				global::UnityEngine.Material material2 = data.material;
				global::UnityEngine.Material materialCoC = data.materialCoC;
				global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
				global::UnityEngine.Rendering.RTHandle rTHandle = data.sourceTexture;
				global::UnityEngine.Rendering.RTHandle destination2 = data.destination;
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_SetupDoF)))
				{
					material2.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._CoCParams, data.cocParams);
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(material2, "_HIGH_QUALITY_SAMPLING", data.highQualitySamplingValue);
					materialCoC.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._CoCParams, data.cocParams);
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(materialCoC, "_HIGH_QUALITY_SAMPLING", data.highQualitySamplingValue);
					global::UnityEngine.Rendering.Universal.PostProcessUtils.SetSourceSize(nativeCommandBuffer, data.sourceTexture);
					material2.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._DownSampleScaleFactor, new global::UnityEngine.Vector4(1f / (float)data.downsample, 1f / (float)data.downsample, data.downsample, data.downsample));
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFComputeCOC)))
				{
					material2.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._CameraDepthTextureID, data.depthTexture);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.sourceTexture, data.fullCoCTexture, data.materialCoC, 0);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFDownscalePrefilter)))
				{
					material2.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._FullCoCTexture, data.fullCoCTexture);
					data.multipleRenderTargets[0] = data.halfCoCTexture;
					data.multipleRenderTargets[1] = data.pingTexture;
					global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(nativeCommandBuffer, data.multipleRenderTargets, data.halfCoCTexture);
					global::UnityEngine.Vector2 vector = (rTHandle.useScaling ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
					global::UnityEngine.Rendering.Blitter.BlitTexture(nativeCommandBuffer, data.sourceTexture, vector, material2, 1);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFBlurH)))
				{
					material2.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._HalfCoCTexture, data.halfCoCTexture);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.pingTexture, data.pongTexture, material2, 2);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFBlurV)))
				{
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.pongTexture, data.pingTexture, material2, 3);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFComposite)))
				{
					material2.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._ColorTexture, data.pingTexture);
					material2.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._FullCoCTexture, data.fullCoCTexture);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, rTHandle, destination2, material2, 4);
				}
			});
		}

		private void PrepareBokehKernel(float maxRadius, float rcpAspect)
		{
			if (m_BokehKernel == null)
			{
				m_BokehKernel = new global::UnityEngine.Vector4[42];
			}
			int num = 0;
			float num2 = m_DepthOfField.bladeCount.value;
			float p = 1f - m_DepthOfField.bladeCurvature.value;
			float num3 = m_DepthOfField.bladeRotation.value * (global::System.MathF.PI / 180f);
			for (int i = 1; i < 4; i++)
			{
				float num4 = 1f / 7f;
				float num5 = ((float)i + num4) / (3f + num4);
				int num6 = i * 7;
				for (int j = 0; j < num6; j++)
				{
					float num7 = global::System.MathF.PI * 2f * (float)j / (float)num6;
					float num8 = global::UnityEngine.Mathf.Cos(global::System.MathF.PI / num2);
					float num9 = global::UnityEngine.Mathf.Cos(num7 - global::System.MathF.PI * 2f / num2 * global::UnityEngine.Mathf.Floor((num2 * num7 + global::System.MathF.PI) / (global::System.MathF.PI * 2f)));
					float num10 = num5 * global::UnityEngine.Mathf.Pow(num8 / num9, p);
					float num11 = num10 * global::UnityEngine.Mathf.Cos(num7 - num3);
					float num12 = num10 * global::UnityEngine.Mathf.Sin(num7 - num3);
					float num13 = num11 * maxRadius;
					float num14 = num12 * maxRadius;
					float num15 = num13 * num13;
					float num16 = num14 * num14;
					float z = global::UnityEngine.Mathf.Sqrt(num15 + num16);
					float w = num13 * rcpAspect;
					m_BokehKernel[num] = new global::UnityEngine.Vector4(num13, num14, z, w);
					num++;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static float GetMaxBokehRadiusInPixels(float viewportHeight)
		{
			return global::UnityEngine.Mathf.Min(0.05f, 14f / viewportHeight);
		}

		public void RenderDoFBokeh(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, ref global::UnityEngine.Material dofMaterial)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor = source.GetDescriptor(renderGraph);
			int num = 2;
			global::UnityEngine.Material material = dofMaterial;
			int num2 = descriptor.width / num;
			int num3 = descriptor.height / num;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = CreateCompatibleTexture(renderGraph, GetCompatibleDescriptor(descriptor, descriptor.width, descriptor.height, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm), "_FullCoCTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input2 = CreateCompatibleTexture(renderGraph, GetCompatibleDescriptor(descriptor, num2, num3, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat), "_PingTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input3 = CreateCompatibleTexture(renderGraph, GetCompatibleDescriptor(descriptor, num2, num3, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat), "_PongTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.DoFBokehPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.DoFBokehPassData>("Depth of Field - Bokeh", out passData, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 1278);
			float num4 = m_DepthOfField.focalLength.value / 1000f;
			float num5 = m_DepthOfField.focalLength.value / m_DepthOfField.aperture.value;
			float value = m_DepthOfField.focusDistance.value;
			float y = num5 * num4 / (value - num4);
			float maxBokehRadiusInPixels = GetMaxBokehRadiusInPixels(descriptor.height);
			float num6 = 1f / ((float)num2 / (float)num3);
			int hashCode = m_DepthOfField.GetHashCode();
			if (hashCode != m_BokehHash || maxBokehRadiusInPixels != m_BokehMaxRadius || num6 != m_BokehRCPAspect)
			{
				m_BokehHash = hashCode;
				m_BokehMaxRadius = maxBokehRadiusInPixels;
				m_BokehRCPAspect = num6;
				PrepareBokehKernel(maxBokehRadiusInPixels, num6);
			}
			float uvMargin = 1f / (float)descriptor.height * (float)num;
			passData.bokehKernel = m_BokehKernel;
			passData.downSample = num;
			passData.uvMargin = uvMargin;
			passData.cocParams = new global::UnityEngine.Vector4(value, y, maxBokehRadiusInPixels, num6);
			passData.useFastSRGBLinearConversion = m_UseFastSRGBLinearConversion;
			passData.sourceTexture = source;
			unsafeRenderGraphBuilder.UseTexture(in source);
			passData.depthTexture = resourceData.cameraDepthTexture;
			unsafeRenderGraphBuilder.UseTexture(resourceData.cameraDepthTexture);
			passData.material = material;
			passData.materialCoC = m_Materials.bokehDepthOfFieldCoC;
			passData.fullCoCTexture = input;
			unsafeRenderGraphBuilder.UseTexture(in input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.pingTexture = input2;
			unsafeRenderGraphBuilder.UseTexture(in input2, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.pongTexture = input3;
			unsafeRenderGraphBuilder.UseTexture(in input3, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.destination = destination;
			unsafeRenderGraphBuilder.UseTexture(in destination, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.DoFBokehPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				global::UnityEngine.Material material2 = data.material;
				global::UnityEngine.Material materialCoC = data.materialCoC;
				global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
				global::UnityEngine.Rendering.RTHandle source2 = data.sourceTexture;
				global::UnityEngine.Rendering.RTHandle destination2 = data.destination;
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_SetupDoF)))
				{
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(material2, "_USE_FAST_SRGB_LINEAR_CONVERSION", data.useFastSRGBLinearConversion);
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(materialCoC, "_USE_FAST_SRGB_LINEAR_CONVERSION", data.useFastSRGBLinearConversion);
					material2.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._CoCParams, data.cocParams);
					material2.SetVectorArray(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._BokehKernel, data.bokehKernel);
					material2.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._DownSampleScaleFactor, new global::UnityEngine.Vector4(1f / (float)data.downSample, 1f / (float)data.downSample, data.downSample, data.downSample));
					material2.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._BokehConstants, new global::UnityEngine.Vector4(data.uvMargin, data.uvMargin * 2f));
					global::UnityEngine.Rendering.Universal.PostProcessUtils.SetSourceSize(nativeCommandBuffer, data.sourceTexture);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFComputeCOC)))
				{
					material2.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._CameraDepthTextureID, data.depthTexture);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, source2, data.fullCoCTexture, material2, 0);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFDownscalePrefilter)))
				{
					material2.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._FullCoCTexture, data.fullCoCTexture);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, source2, data.pingTexture, material2, 1);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFBlurBokeh)))
				{
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.pingTexture, data.pongTexture, material2, 2);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFPostFilter)))
				{
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.pongTexture, data.pingTexture, material2, 3);
				}
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_DOFComposite)))
				{
					material2.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._DofTexture, data.pingTexture);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, source2, destination2, material2, 4);
				}
			});
		}

		public void RenderPaniniProjection(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Camera camera, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
		{
			destination = CreateCompatibleTexture(renderGraph, in source, "_PaniniProjectionTarget", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor = source.GetDescriptor(renderGraph);
			float value = m_PaniniProjection.distance.value;
			global::UnityEngine.Vector2 vector = CalcViewExtents(camera, descriptor.width, descriptor.height);
			global::UnityEngine.Vector2 vector2 = CalcCropExtents(camera, value, descriptor.width, descriptor.height);
			float a = vector2.x / vector.x;
			float b = vector2.y / vector.y;
			float value2 = global::UnityEngine.Mathf.Min(a, b);
			float num = value;
			float w = global::UnityEngine.Mathf.Lerp(1f, global::UnityEngine.Mathf.Clamp01(value2), m_PaniniProjection.cropToFit.value);
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PaniniProjectionPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PaniniProjectionPassData>("Panini Projection", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.PaniniProjection), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 1419);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			passData.destinationTexture = destination;
			rasterRenderGraphBuilder.SetRenderAttachment(destination, 0);
			passData.sourceTexture = source;
			rasterRenderGraphBuilder.UseTexture(in source);
			passData.material = m_Materials.paniniProjection;
			passData.paniniParams = new global::UnityEngine.Vector4(vector.x, vector.y, num, w);
			passData.isPaniniGeneric = 1f - global::UnityEngine.Mathf.Abs(num) > float.Epsilon;
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PaniniProjectionPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
				global::UnityEngine.Rendering.RTHandle rTHandle = data.sourceTexture;
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Params, data.paniniParams);
				data.material.EnableKeyword(data.isPaniniGeneric ? "_GENERIC" : "_UNIT_DISTANCE");
				global::UnityEngine.Vector2 vector3 = (rTHandle.useScaling ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, rTHandle, vector3, data.material, 0);
			});
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::UnityEngine.Vector2 CalcViewExtents(global::UnityEngine.Camera camera, int width, int height)
		{
			float num = camera.fieldOfView * (global::System.MathF.PI / 180f);
			float num2 = (float)width / (float)height;
			float num3 = global::UnityEngine.Mathf.Tan(0.5f * num);
			return new global::UnityEngine.Vector2(num2 * num3, num3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::UnityEngine.Vector2 CalcCropExtents(global::UnityEngine.Camera camera, float d, int width, int height)
		{
			float num = 1f + d;
			global::UnityEngine.Vector2 vector = CalcViewExtents(camera, width, height);
			float num2 = global::UnityEngine.Mathf.Sqrt(vector.x * vector.x + 1f);
			float num3 = 1f / num2;
			float num4 = num3 + d;
			return vector * num3 * (num / num4);
		}

		private void RenderTemporalAA(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
		{
			destination = CreateCompatibleTexture(renderGraph, in source, "_TemporalAATarget", clear: false, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcDepth = resourceData.cameraDepth;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcMotionVectors = resourceData.motionVectorColor;
			global::UnityEngine.Rendering.Universal.TemporalAA.Render(renderGraph, m_Materials.temporalAntialiasing, cameraData, ref source, ref srcDepth, ref srcMotionVectors, ref destination);
		}

		private void RenderSTP(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle motionVectorColor = resourceData.motionVectorColor;
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor = source.GetDescriptor(renderGraph);
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = GetCompatibleDescriptor(descriptor, cameraData.pixelWidth, cameraData.pixelHeight, global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetLinearFormat(descriptor.format));
			desc.enableRandomWrite = true;
			destination = CreateCompatibleTexture(renderGraph, in desc, "_CameraColorUpscaledSTP", clear: false, global::UnityEngine.FilterMode.Bilinear);
			int frameCount = global::UnityEngine.Time.frameCount;
			global::UnityEngine.Texture2D noiseTexture = m_Materials.resources.textures.blueNoise16LTex[frameCount & (m_Materials.resources.textures.blueNoise16LTex.Length - 1)];
			global::UnityEngine.Rendering.Universal.StpUtils.Execute(renderGraph, resourceData, cameraData, source, cameraDepthTexture, motionVectorColor, destination, noiseTexture);
			UpdateCameraResolution(renderGraph, cameraData, new global::UnityEngine.Vector2Int(desc.width, desc.height));
		}

		public void RenderMotionBlur(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
		{
			global::UnityEngine.Material motionBlur = m_Materials.motionBlur;
			destination = CreateCompatibleTexture(renderGraph, in source, "_MotionBlurTarget", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = resourceData.motionVectorColor;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input2 = resourceData.cameraDepthTexture;
			global::UnityEngine.Rendering.Universal.MotionBlurMode value = m_MotionBlur.mode.value;
			int value2 = (int)m_MotionBlur.quality.value;
			value2 += ((value == global::UnityEngine.Rendering.Universal.MotionBlurMode.CameraAndObjects) ? 3 : 0);
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.MotionBlurPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.MotionBlurPassData>("Motion Blur", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_MotionBlur), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 1575);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderAttachment(destination, 0);
			passData.sourceTexture = source;
			rasterRenderGraphBuilder.UseTexture(in source);
			if (value == global::UnityEngine.Rendering.Universal.MotionBlurMode.CameraAndObjects)
			{
				passData.motionVectors = input;
				rasterRenderGraphBuilder.UseTexture(in input);
			}
			else
			{
				passData.motionVectors = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			rasterRenderGraphBuilder.UseTexture(in input2);
			passData.material = motionBlur;
			passData.passIndex = value2;
			passData.camera = cameraData.camera;
			passData.xr = cameraData.xr;
			passData.enableAlphaOutput = cameraData.isAlphaOutputEnabled;
			passData.intensity = m_MotionBlur.intensity.value;
			passData.clamp = m_MotionBlur.clamp.value;
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.MotionBlurPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
				global::UnityEngine.Rendering.RTHandle rTHandle = data.sourceTexture;
				UpdateMotionBlurMatrices(ref data.material, data.camera, data.xr);
				data.material.SetFloat("_Intensity", data.intensity);
				data.material.SetFloat("_Clamp", data.clamp);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, "_ENABLE_ALPHA_OUTPUT", data.enableAlphaOutput);
				global::UnityEngine.Rendering.Universal.PostProcessUtils.SetSourceSize(cmd, data.sourceTexture);
				global::UnityEngine.Vector2 vector = (rTHandle.useScaling ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, rTHandle, vector, data.material, data.passIndex);
			});
		}

		internal static void UpdateMotionBlurMatrices(ref global::UnityEngine.Material material, global::UnityEngine.Camera camera, global::UnityEngine.Experimental.Rendering.XRPass xr)
		{
			global::UnityEngine.Rendering.Universal.MotionVectorsPersistentData motionVectorsPersistentData = null;
			if (camera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component))
			{
				motionVectorsPersistentData = component.motionVectorsPersistentData;
			}
			if (motionVectorsPersistentData == null)
			{
				return;
			}
			if (xr.enabled && xr.singlePassEnabled)
			{
				int sourceIndex = xr.viewCount * xr.multipassId;
				global::System.Array.Copy(motionVectorsPersistentData.previousViewProjectionStereo, sourceIndex, motionVectorsPersistentData.stagingMatrixStereo, 0, xr.viewCount);
				material.SetMatrixArray(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._PrevViewProjMStereo, motionVectorsPersistentData.stagingMatrixStereo);
				global::System.Array.Copy(motionVectorsPersistentData.viewProjectionStereo, sourceIndex, motionVectorsPersistentData.stagingMatrixStereo, 0, xr.viewCount);
				material.SetMatrixArray(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._ViewProjMStereo, motionVectorsPersistentData.stagingMatrixStereo);
				return;
			}
			int num = 0;
			if (xr.enabled)
			{
				num = xr.multipassId * xr.viewCount;
			}
			material.SetMatrix(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._PrevViewProjM, motionVectorsPersistentData.previousViewProjectionStereo[num]);
			material.SetMatrix(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._ViewProjM, motionVectorsPersistentData.viewProjectionStereo[num]);
		}

		private void LensFlareDataDrivenComputeOcclusion(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc srcDesc)
		{
			if (!global::UnityEngine.Rendering.LensFlareCommonSRP.IsOcclusionRTCompatible())
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.LensFlarePassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.LensFlarePassData>("Lens Flare Compute Occlusion", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.LensFlareDataDrivenComputeOcclusion), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 1681);
			_ = global::UnityEngine.Rendering.LensFlareCommonSRP.occlusionRT;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = (passData.destinationTexture = renderGraph.ImportTexture(global::UnityEngine.Rendering.LensFlareCommonSRP.occlusionRT));
			unsafeRenderGraphBuilder.UseTexture(in input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			passData.cameraData = cameraData;
			passData.viewport = cameraData.pixelRect;
			passData.material = m_Materials.lensFlareDataDriven;
			passData.width = srcDesc.width;
			passData.height = srcDesc.height;
			if (m_PaniniProjection.IsActive())
			{
				passData.usePanini = true;
				passData.paniniDistance = m_PaniniProjection.distance.value;
				passData.paniniCropToFit = m_PaniniProjection.cropToFit.value;
			}
			else
			{
				passData.usePanini = false;
				passData.paniniDistance = 1f;
				passData.paniniCropToFit = 1f;
			}
			unsafeRenderGraphBuilder.UseTexture(resourceData.cameraDepthTexture);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.LensFlarePassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext ctx)
			{
				global::UnityEngine.Camera camera = data.cameraData.camera;
				global::UnityEngine.Experimental.Rendering.XRPass xr = data.cameraData.xr;
				global::UnityEngine.Matrix4x4 viewProjMatrix;
				if (xr.enabled)
				{
					if (xr.singlePassEnabled)
					{
						viewProjMatrix = global::UnityEngine.GL.GetGPUProjectionMatrix(data.cameraData.GetProjectionMatrixNoJitter(), renderIntoTexture: true) * data.cameraData.GetViewMatrix();
					}
					else
					{
						viewProjMatrix = global::UnityEngine.GL.GetGPUProjectionMatrix(camera.projectionMatrix, renderIntoTexture: true) * camera.worldToCameraMatrix;
						_ = data.cameraData.xr.multipassId;
					}
				}
				else
				{
					viewProjMatrix = global::UnityEngine.GL.GetGPUProjectionMatrix(data.cameraData.GetProjectionMatrixNoJitter(), renderIntoTexture: true) * data.cameraData.GetViewMatrix();
				}
				global::UnityEngine.Rendering.LensFlareCommonSRP.ComputeOcclusion(data.material, camera, xr, xr.multipassId, data.width, data.height, data.usePanini, data.paniniDistance, data.paniniCropToFit, isCameraRelative: true, camera.transform.position, viewProjMatrix, ctx.cmd, taaEnabled: false, hasCloudLayer: false, null, null);
				if (xr.enabled && xr.singlePassEnabled)
				{
					for (int i = 1; i < xr.viewCount; i++)
					{
						global::UnityEngine.Matrix4x4 viewProjMatrix2 = global::UnityEngine.GL.GetGPUProjectionMatrix(data.cameraData.GetProjectionMatrixNoJitter(i), renderIntoTexture: true) * data.cameraData.GetViewMatrix(i);
						global::UnityEngine.Rendering.LensFlareCommonSRP.ComputeOcclusion(data.material, camera, xr, i, data.width, data.height, data.usePanini, data.paniniDistance, data.paniniCropToFit, isCameraRelative: true, camera.transform.position, viewProjMatrix2, ctx.cmd, taaEnabled: false, hasCloudLayer: false, null, null);
					}
				}
			});
		}

		public void RenderLensFlareDataDriven(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc srcDesc)
		{
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.LensFlarePassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.LensFlarePassData>("Lens Flare Data Driven Pass", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.LensFlareDataDriven), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 1779);
			passData.destinationTexture = destination;
			unsafeRenderGraphBuilder.UseTexture(in destination, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			passData.cameraData = cameraData;
			passData.material = m_Materials.lensFlareDataDriven;
			passData.width = srcDesc.width;
			passData.height = srcDesc.height;
			passData.viewport.x = 0f;
			passData.viewport.y = 0f;
			passData.viewport.width = srcDesc.width;
			passData.viewport.height = srcDesc.height;
			if (m_PaniniProjection.IsActive())
			{
				passData.usePanini = true;
				passData.paniniDistance = m_PaniniProjection.distance.value;
				passData.paniniCropToFit = m_PaniniProjection.cropToFit.value;
			}
			else
			{
				passData.usePanini = false;
				passData.paniniDistance = 1f;
				passData.paniniCropToFit = 1f;
			}
			if (global::UnityEngine.Rendering.LensFlareCommonSRP.IsOcclusionRTCompatible())
			{
				unsafeRenderGraphBuilder.UseTexture(renderGraph.ImportTexture(global::UnityEngine.Rendering.LensFlareCommonSRP.occlusionRT));
			}
			else
			{
				unsafeRenderGraphBuilder.UseTexture(resourceData.cameraDepthTexture);
			}
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.LensFlarePassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext ctx)
			{
				global::UnityEngine.Camera camera = data.cameraData.camera;
				global::UnityEngine.Experimental.Rendering.XRPass xr = data.cameraData.xr;
				if (!xr.enabled || (xr.enabled && !xr.singlePassEnabled))
				{
					global::UnityEngine.Matrix4x4 viewProjMatrix = global::UnityEngine.GL.GetGPUProjectionMatrix(camera.projectionMatrix, renderIntoTexture: true) * camera.worldToCameraMatrix;
					global::UnityEngine.Rendering.LensFlareCommonSRP.DoLensFlareDataDrivenCommon(data.material, data.cameraData.camera, data.viewport, xr, data.cameraData.xr.multipassId, data.width, data.height, data.usePanini, data.paniniDistance, data.paniniCropToFit, isCameraRelative: true, camera.transform.position, viewProjMatrix, ctx.cmd, taaEnabled: false, hasCloudLayer: false, null, null, data.destinationTexture, (global::UnityEngine.Light light, global::UnityEngine.Camera cam, global::UnityEngine.Vector3 wo) => GetLensFlareLightAttenuation(light, cam, wo), debugView: false);
				}
				else
				{
					for (int num = 0; num < xr.viewCount; num++)
					{
						global::UnityEngine.Matrix4x4 viewProjMatrix2 = global::UnityEngine.GL.GetGPUProjectionMatrix(data.cameraData.GetProjectionMatrixNoJitter(num), renderIntoTexture: true) * data.cameraData.GetViewMatrix(num);
						global::UnityEngine.Rendering.LensFlareCommonSRP.DoLensFlareDataDrivenCommon(data.material, data.cameraData.camera, data.viewport, xr, data.cameraData.xr.multipassId, data.width, data.height, data.usePanini, data.paniniDistance, data.paniniCropToFit, isCameraRelative: true, camera.transform.position, viewProjMatrix2, ctx.cmd, taaEnabled: false, hasCloudLayer: false, null, null, data.destinationTexture, (global::UnityEngine.Light light, global::UnityEngine.Camera cam, global::UnityEngine.Vector3 wo) => GetLensFlareLightAttenuation(light, cam, wo), debugView: false);
					}
				}
			});
		}

		private static float GetLensFlareLightAttenuation(global::UnityEngine.Light light, global::UnityEngine.Camera cam, global::UnityEngine.Vector3 wo)
		{
			if (light != null)
			{
				return light.type switch
				{
					global::UnityEngine.LightType.Directional => global::UnityEngine.Rendering.LensFlareCommonSRP.ShapeAttenuationDirLight(light.transform.forward, cam.transform.forward), 
					global::UnityEngine.LightType.Point => global::UnityEngine.Rendering.LensFlareCommonSRP.ShapeAttenuationPointLight(), 
					global::UnityEngine.LightType.Spot => global::UnityEngine.Rendering.LensFlareCommonSRP.ShapeAttenuationSpotConeLight(light.transform.forward, wo, light.spotAngle, light.innerSpotAngle / 180f), 
					_ => 1f, 
				};
			}
			return 1f;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle RenderLensFlareScreenSpace(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Camera camera, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc srcDesc, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle originalBloomTexture, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle screenSpaceLensFlareBloomMipTexture, bool sameBloomInputOutputTex)
		{
			int value = (int)m_LensFlareScreenSpace.resolution.value;
			int width = global::System.Math.Max(srcDesc.width / value, 1);
			int height = global::System.Math.Max(srcDesc.height / value, 1);
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = GetCompatibleDescriptor(srcDesc, width, height, m_LensFlareScreenSpaceColorFormat);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = CreateCompatibleTexture(renderGraph, in desc, "_StreakTmpTexture", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input2 = CreateCompatibleTexture(renderGraph, in desc, "_StreakTmpTexture2", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input3 = CreateCompatibleTexture(renderGraph, in desc, "_LensFlareScreenSpace", clear: true, global::UnityEngine.FilterMode.Bilinear);
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.LensFlareScreenSpacePassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.LensFlareScreenSpacePassData>("Blit Lens Flare Screen Space", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.LensFlareScreenSpace), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 1922);
			passData.streakTmpTexture = input;
			unsafeRenderGraphBuilder.UseTexture(in input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.streakTmpTexture2 = input2;
			unsafeRenderGraphBuilder.UseTexture(in input2, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.screenSpaceLensFlareBloomMipTexture = screenSpaceLensFlareBloomMipTexture;
			unsafeRenderGraphBuilder.UseTexture(in screenSpaceLensFlareBloomMipTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.originalBloomTexture = originalBloomTexture;
			if (!sameBloomInputOutputTex)
			{
				unsafeRenderGraphBuilder.UseTexture(in originalBloomTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			}
			passData.actualWidth = srcDesc.width;
			passData.actualHeight = srcDesc.height;
			passData.camera = camera;
			passData.material = m_Materials.lensFlareScreenSpace;
			passData.lensFlareScreenSpace = m_LensFlareScreenSpace;
			passData.downsample = value;
			passData.result = input3;
			unsafeRenderGraphBuilder.UseTexture(in input3, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.LensFlareScreenSpacePassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				global::UnityEngine.Rendering.UnsafeCommandBuffer cmd = context.cmd;
				global::UnityEngine.Camera camera2 = data.camera;
				global::UnityEngine.Rendering.Universal.ScreenSpaceLensFlare lensFlareScreenSpace = data.lensFlareScreenSpace;
				global::UnityEngine.Rendering.LensFlareCommonSRP.DoLensFlareScreenSpaceCommon(data.material, camera2, data.actualWidth, data.actualHeight, data.lensFlareScreenSpace.tintColor.value, data.originalBloomTexture, data.screenSpaceLensFlareBloomMipTexture, null, data.streakTmpTexture, data.streakTmpTexture2, new global::UnityEngine.Vector4(lensFlareScreenSpace.intensity.value, lensFlareScreenSpace.firstFlareIntensity.value, lensFlareScreenSpace.secondaryFlareIntensity.value, lensFlareScreenSpace.warpedFlareIntensity.value), new global::UnityEngine.Vector4(lensFlareScreenSpace.vignetteEffect.value, lensFlareScreenSpace.startingPosition.value, lensFlareScreenSpace.scale.value, 0f), new global::UnityEngine.Vector4(lensFlareScreenSpace.samples.value, lensFlareScreenSpace.sampleDimmer.value, lensFlareScreenSpace.chromaticAbberationIntensity.value, 0f), new global::UnityEngine.Vector4(lensFlareScreenSpace.streaksIntensity.value, lensFlareScreenSpace.streaksLength.value, lensFlareScreenSpace.streaksOrientation.value, lensFlareScreenSpace.streaksThreshold.value), new global::UnityEngine.Vector4(data.downsample, lensFlareScreenSpace.warpedFlareScale.value.x, lensFlareScreenSpace.warpedFlareScale.value.y, 0f), cmd, data.result, debugView: false);
			});
			return originalBloomTexture;
		}

		private static void ScaleViewport(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle sourceTextureHdl, global::UnityEngine.Rendering.RTHandle dest, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool hasFinalPass)
		{
			global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier = global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget;
			if (cameraData.xr.enabled)
			{
				renderTargetIdentifier = cameraData.xr.renderTarget;
			}
			if (dest.nameID == renderTargetIdentifier || cameraData.targetTexture != null)
			{
				if (hasFinalPass || !cameraData.resolveFinalTarget)
				{
					int width = cameraData.cameraTargetDescriptor.width;
					int height = cameraData.cameraTargetDescriptor.height;
					global::UnityEngine.Rect viewport = new global::UnityEngine.Rect(0f, 0f, width, height);
					cmd.SetViewport(viewport);
				}
				else
				{
					cmd.SetViewport(cameraData.pixelRect);
				}
			}
		}

		private static void ScaleViewportAndBlit(in global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Material material, bool hasFinalPass)
		{
			global::UnityEngine.Vector4 finalBlitScaleBias = global::UnityEngine.Rendering.Universal.RenderingUtils.GetFinalBlitScaleBias(in context, in source, in destination);
			ScaleViewport(context.cmd, source, destination, cameraData, hasFinalPass);
			global::UnityEngine.Rendering.Blitter.BlitTexture(context.cmd, source, finalBlitScaleBias, material, 0);
		}

		private static void ScaleViewportAndDrawVisibilityMesh(in global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Material material, bool hasFinalPass)
		{
			global::UnityEngine.Vector4 finalBlitScaleBias = global::UnityEngine.Rendering.Universal.RenderingUtils.GetFinalBlitScaleBias(in context, in source, in destination);
			ScaleViewport(context.cmd, source, destination, cameraData, hasFinalPass);
			global::UnityEngine.MaterialPropertyBlock materialPropertyBlock = global::UnityEngine.Rendering.Universal.XRSystemUniversal.GetMaterialPropertyBlock();
			materialPropertyBlock.SetVector(global::UnityEngine.Shader.PropertyToID("_BlitScaleBias"), finalBlitScaleBias);
			materialPropertyBlock.SetTexture(global::UnityEngine.Shader.PropertyToID("_BlitTexture"), source);
			cameraData.xr.RenderVisibleMeshCustomMaterial(context.cmd, cameraData.xr.occlusionMeshScale, material, materialPropertyBlock, 1, context.GetTextureUVOrigin(in destination) == global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft);
		}

		public void RenderFinalSetup(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, ref global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.FinalBlitSettings settings)
		{
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostProcessingFinalSetupPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostProcessingFinalSetupPassData>("Postprocessing Final Setup Pass", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_FinalSetup), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 2064);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			global::UnityEngine.Material scalingSetup = m_Materials.scalingSetup;
			scalingSetup.shaderKeywords = null;
			scalingSetup.shaderKeywords = null;
			if (settings.isFxaaEnabled)
			{
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(scalingSetup, "_FXAA", settings.isFxaaEnabled);
			}
			if (settings.isFsrEnabled)
			{
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(scalingSetup, settings.hdrOperations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding) ? "_GAMMA_20_AND_HDR_INPUT" : "_GAMMA_20", state: true);
			}
			if (settings.hdrOperations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding))
			{
				SetupHDROutput(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, scalingSetup, settings.hdrOperations, cameraData.rendersOverlayUI);
			}
			if (settings.isAlphaOutputEnabled)
			{
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(scalingSetup, "_ENABLE_ALPHA_OUTPUT", settings.isAlphaOutputEnabled);
			}
			passData.destinationTexture = destination;
			rasterRenderGraphBuilder.SetRenderAttachment(destination, 0);
			passData.sourceTexture = source;
			rasterRenderGraphBuilder.UseTexture(in source);
			passData.cameraData = cameraData;
			passData.material = scalingSetup;
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostProcessingFinalSetupPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.RTHandle source2 = data.sourceTexture;
				global::UnityEngine.Rendering.Universal.PostProcessUtils.SetSourceSize(context.cmd, source2);
				bool hasFinalPass = true;
				ScaleViewportAndBlit(in context, in data.sourceTexture, in data.destinationTexture, data.cameraData, data.material, hasFinalPass);
			});
		}

		public void RenderFinalFSRScale(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc srcDesc, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc dstDesc, bool enableAlphaOutput)
		{
			m_Materials.easu.shaderKeywords = null;
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostProcessingFinalFSRScalePassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostProcessingFinalFSRScalePassData>("Postprocessing Final FSR Scale Pass", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_FinalFSRScale), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 2121);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderAttachment(destination, 0);
			passData.sourceTexture = source;
			rasterRenderGraphBuilder.UseTexture(in source);
			passData.material = m_Materials.easu;
			passData.enableAlphaOutput = enableAlphaOutput;
			passData.fsrInputSize = new global::UnityEngine.Vector2(srcDesc.width, srcDesc.height);
			passData.fsrOutputSize = new global::UnityEngine.Vector2(dstDesc.width, dstDesc.height);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostProcessingFinalFSRScalePassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture = data.sourceTexture;
				global::UnityEngine.Material material = data.material;
				bool enableAlphaOutput2 = data.enableAlphaOutput;
				global::UnityEngine.Rendering.RTHandle rTHandle = sourceTexture;
				global::UnityEngine.Rendering.FSRUtils.SetEasuConstants(cmd, data.fsrInputSize, data.fsrInputSize, data.fsrOutputSize);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", enableAlphaOutput2);
				global::UnityEngine.Vector2 vector = (rTHandle.useScaling ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, rTHandle, vector, material, 0);
			});
		}

		public void RenderFinalBlit(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle postProcessingTarget, ref global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.FinalBlitSettings settings)
		{
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostProcessingFinalBlitPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostProcessingFinalBlitPassData>("Postprocessing Final Blit Pass", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_FinalBlit), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 2200);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			passData.destinationTexture = postProcessingTarget;
			rasterRenderGraphBuilder.SetRenderAttachment(postProcessingTarget, 0);
			passData.sourceTexture = source;
			rasterRenderGraphBuilder.UseTexture(in source);
			passData.cameraData = cameraData;
			passData.material = m_Materials.finalPass;
			passData.settings = settings;
			if (settings.requireHDROutput && m_EnableColorEncodingIfNeeded && cameraData.rendersOverlayUI)
			{
				rasterRenderGraphBuilder.UseTexture(in overlayUITexture);
			}
			if (cameraData.xr.enabled)
			{
				bool flag = !global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps.HasFlag(global::UnityEngine.Rendering.FoveatedRenderingCaps.NonUniformRaster);
				rasterRenderGraphBuilder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && flag);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostProcessingFinalBlitPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
				global::UnityEngine.Material material = data.material;
				bool isFxaaEnabled = data.settings.isFxaaEnabled;
				bool isFsrEnabled = data.settings.isFsrEnabled;
				bool isTaaSharpeningEnabled = data.settings.isTaaSharpeningEnabled;
				bool requireHDROutput = data.settings.requireHDROutput;
				bool isAlphaOutputEnabled = data.settings.isAlphaOutputEnabled;
				global::UnityEngine.Rendering.RTHandle rTHandle = data.sourceTexture;
				_ = (global::UnityEngine.Rendering.RTHandle)data.destinationTexture;
				global::UnityEngine.Rendering.Universal.PostProcessUtils.SetSourceSize(cmd, data.sourceTexture);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_FXAA", isFxaaEnabled);
				if (isFsrEnabled)
				{
					float sharpnessLinear = (data.cameraData.fsrOverrideSharpness ? data.cameraData.fsrSharpness : 0.92f);
					if (data.cameraData.fsrSharpness > 0f)
					{
						global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, requireHDROutput ? "_EASU_RCAS_AND_HDR_INPUT" : "_RCAS", state: true);
						global::UnityEngine.Rendering.FSRUtils.SetRcasConstantsLinear(cmd, sharpnessLinear);
					}
				}
				else if (isTaaSharpeningEnabled)
				{
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_RCAS", state: true);
					global::UnityEngine.Rendering.FSRUtils.SetRcasConstantsLinear(cmd, data.cameraData.taaSettings.contrastAdaptiveSharpening);
				}
				if (isAlphaOutputEnabled)
				{
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", isAlphaOutputEnabled);
				}
				global::UnityEngine.Vector4 finalBlitScaleBias = global::UnityEngine.Rendering.Universal.RenderingUtils.GetFinalBlitScaleBias(in context, in data.sourceTexture, in data.destinationTexture);
				cmd.SetViewport(data.cameraData.pixelRect);
				if (data.cameraData.xr.enabled && data.cameraData.xr.hasValidVisibleMesh)
				{
					global::UnityEngine.MaterialPropertyBlock materialPropertyBlock = global::UnityEngine.Rendering.Universal.XRSystemUniversal.GetMaterialPropertyBlock();
					materialPropertyBlock.SetVector(global::UnityEngine.Shader.PropertyToID("_BlitScaleBias"), finalBlitScaleBias);
					materialPropertyBlock.SetTexture(global::UnityEngine.Shader.PropertyToID("_BlitTexture"), rTHandle);
					data.cameraData.xr.RenderVisibleMeshCustomMaterial(cmd, data.cameraData.xr.occlusionMeshScale, material, materialPropertyBlock, 1, context.GetTextureUVOrigin(in data.sourceTexture) == context.GetTextureUVOrigin(in data.destinationTexture));
				}
				else
				{
					global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, rTHandle, finalBlitScaleBias, material, 0);
				}
			});
		}

		public void RenderFinalPassRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle postProcessingTarget, bool enableColorEncodingIfNeeded)
		{
			global::UnityEngine.Rendering.VolumeStack stack = global::UnityEngine.Rendering.VolumeManager.instance.stack;
			m_Tonemapping = stack.GetComponent<global::UnityEngine.Rendering.Universal.Tonemapping>();
			m_FilmGrain = stack.GetComponent<global::UnityEngine.Rendering.Universal.FilmGrain>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Material finalPass = m_Materials.finalPass;
			finalPass.shaderKeywords = null;
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.FinalBlitSettings settings = global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.FinalBlitSettings.Create();
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc srcDesc = renderGraph.GetTextureDesc(in source);
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = srcDesc;
			desc.width = universalCameraData.pixelWidth;
			desc.height = universalCameraData.pixelHeight;
			m_HasFinalPass = false;
			m_EnableColorEncodingIfNeeded = enableColorEncodingIfNeeded;
			if (m_FilmGrain.IsActive())
			{
				finalPass.EnableKeyword("_FILM_GRAIN");
				global::UnityEngine.Rendering.Universal.PostProcessUtils.ConfigureFilmGrain(m_Materials.resources, m_FilmGrain, desc.width, desc.height, finalPass);
			}
			if (universalCameraData.isDitheringEnabled)
			{
				finalPass.EnableKeyword("_DITHERING");
				m_DitheringTextureIndex = global::UnityEngine.Rendering.Universal.PostProcessUtils.ConfigureDithering(m_Materials.resources, m_DitheringTextureIndex, desc.width, desc.height, finalPass);
			}
			if (RequireSRGBConversionBlitToBackBuffer(universalCameraData.requireSrgbConversion))
			{
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(finalPass, "_LINEAR_TO_SRGB_CONVERSION", state: true);
			}
			settings.hdrOperations = global::UnityEngine.Rendering.HDROutputUtils.Operation.None;
			settings.requireHDROutput = RequireHDROutput(universalCameraData);
			if (settings.requireHDROutput)
			{
				settings.hdrOperations = (m_EnableColorEncodingIfNeeded ? global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding : global::UnityEngine.Rendering.HDROutputUtils.Operation.None);
				if (!universalCameraData.postProcessEnabled)
				{
					settings.hdrOperations |= global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion;
				}
				SetupHDROutput(universalCameraData.hdrDisplayInformation, universalCameraData.hdrDisplayColorGamut, finalPass, settings.hdrOperations, universalCameraData.rendersOverlayUI);
				global::UnityEngine.Rendering.Universal.RenderingUtils.SetupOffscreenUIViewportParams(finalPass, ref universalCameraData.pixelRect, universalCameraData.resolveFinalTarget);
			}
			_ = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(universalCameraData)?.WriteToDebugScreenTexture(universalCameraData.resolveFinalTarget) ?? false;
			settings.isAlphaOutputEnabled = universalCameraData.isAlphaOutputEnabled;
			settings.isFxaaEnabled = universalCameraData.antialiasing == global::UnityEngine.Rendering.Universal.AntialiasingMode.FastApproximateAntialiasing;
			settings.isFsrEnabled = universalCameraData.imageScalingMode == global::UnityEngine.Rendering.Universal.ImageScalingMode.Upscaling && universalCameraData.upscalingFilter == global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.FSR;
			settings.isTaaSharpeningEnabled = universalCameraData.IsTemporalAAEnabled() && universalCameraData.taaSettings.contrastAdaptiveSharpening > 0f && !settings.isFsrEnabled && !universalCameraData.IsSTPEnabled();
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc2 = srcDesc;
			if (!settings.requireHDROutput)
			{
				desc2.format = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.MakeUnormRenderTextureGraphicsFormat();
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination = CreateCompatibleTexture(renderGraph, in desc2, "scalingSetupTarget", clear: true, global::UnityEngine.FilterMode.Point);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination2 = CreateCompatibleTexture(renderGraph, in desc, "_UpscaledTexture", clear: true, global::UnityEngine.FilterMode.Point);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source2 = source;
			if (universalCameraData.imageScalingMode != global::UnityEngine.Rendering.Universal.ImageScalingMode.None)
			{
				if (settings.isFxaaEnabled || settings.isFsrEnabled)
				{
					RenderFinalSetup(renderGraph, universalCameraData, in source2, in destination, ref settings);
					source2 = destination;
					settings.isFxaaEnabled = false;
				}
				switch (universalCameraData.imageScalingMode)
				{
				case global::UnityEngine.Rendering.Universal.ImageScalingMode.Upscaling:
					switch (universalCameraData.upscalingFilter)
					{
					case global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.Point:
						if (!settings.isTaaSharpeningEnabled)
						{
							finalPass.EnableKeyword("_POINT_SAMPLING");
						}
						break;
					case global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.FSR:
						RenderFinalFSRScale(renderGraph, in source2, in srcDesc, in destination2, in desc, settings.isAlphaOutputEnabled);
						source2 = destination2;
						break;
					}
					break;
				case global::UnityEngine.Rendering.Universal.ImageScalingMode.Downscaling:
					settings.isTaaSharpeningEnabled = false;
					break;
				}
			}
			else if (settings.isFxaaEnabled)
			{
				finalPass.EnableKeyword("_FXAA");
			}
			RenderFinalBlit(renderGraph, universalCameraData, in source2, in overlayUITexture, in postProcessingTarget, ref settings);
		}

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle TryGetCachedUserLutTextureHandle(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			if (m_ColorLookup.texture.value == null)
			{
				if (m_UserLut != null)
				{
					m_UserLut.Release();
					m_UserLut = null;
				}
			}
			else if (m_UserLut == null || m_UserLut.externalTexture != m_ColorLookup.texture.value)
			{
				m_UserLut?.Release();
				m_UserLut = global::UnityEngine.Rendering.RTHandles.Alloc(m_ColorLookup.texture.value);
			}
			if (m_UserLut == null)
			{
				return global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			return renderGraph.ImportTexture(m_UserLut);
		}

		public void RenderUberPost(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalPostProcessingData postProcessingData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sourceTexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destTexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle lutTexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle bloomTexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture, bool requireHDROutput, bool enableAlphaOutput, bool hasFinalPass)
		{
			global::UnityEngine.Material uber = m_Materials.uber;
			bool isHdrGrading = postProcessingData.gradingMode == global::UnityEngine.Rendering.Universal.ColorGradingMode.HighDynamicRange;
			int lutSize = postProcessingData.lutSize;
			int num = lutSize * lutSize;
			float w = global::UnityEngine.Mathf.Pow(2f, m_ColorAdjustments.postExposure.value);
			global::UnityEngine.Vector4 lutParams = new global::UnityEngine.Vector4(1f / (float)num, 1f / (float)lutSize, (float)lutSize - 1f, w);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = TryGetCachedUserLutTextureHandle(renderGraph);
			global::UnityEngine.Vector4 userLutParams = ((!m_ColorLookup.IsActive()) ? global::UnityEngine.Vector4.zero : new global::UnityEngine.Vector4(1f / (float)m_ColorLookup.texture.value.width, 1f / (float)m_ColorLookup.texture.value.height, (float)m_ColorLookup.texture.value.height - 1f, m_ColorLookup.contribution.value));
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.UberPostPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.UberPostPassData>("Blit Post Processing", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_UberPost), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 2512);
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			if (cameraData.xr.enabled)
			{
				bool flag = cameraData.xrUniversal.canFoveateIntermediatePasses || universalResourceData.isActiveTargetBackBuffer;
				flag &= !global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps.HasFlag(global::UnityEngine.Rendering.FoveatedRenderingCaps.NonUniformRaster);
				rasterRenderGraphBuilder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && flag);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			passData.destinationTexture = destTexture;
			rasterRenderGraphBuilder.SetRenderAttachment(destTexture, 0);
			passData.sourceTexture = sourceTexture;
			rasterRenderGraphBuilder.UseTexture(in sourceTexture);
			passData.lutTexture = lutTexture;
			rasterRenderGraphBuilder.UseTexture(in lutTexture);
			passData.lutParams = lutParams;
			passData.userLutTexture = input;
			if (input.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in input);
			}
			if (m_Bloom.IsActive())
			{
				rasterRenderGraphBuilder.UseTexture(in bloomTexture);
				passData.bloomTexture = bloomTexture;
			}
			if (requireHDROutput && m_EnableColorEncodingIfNeeded && overlayUITexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in overlayUITexture);
			}
			passData.userLutParams = userLutParams;
			passData.cameraData = cameraData;
			passData.material = uber;
			passData.toneMappingMode = m_Tonemapping.mode.value;
			passData.isHdrGrading = isHdrGrading;
			passData.enableAlphaOutput = enableAlphaOutput;
			passData.hasFinalPass = hasFinalPass;
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.UberPostPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				_ = data.cameraData.camera;
				global::UnityEngine.Material material = data.material;
				material.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._InternalLut, data.lutTexture);
				material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Lut_Params, data.lutParams);
				material.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._UserLut, data.userLutTexture);
				material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._UserLut_Params, data.userLutParams);
				if (data.bloomTexture.IsValid())
				{
					material.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Bloom_Texture, data.bloomTexture);
				}
				if (data.isHdrGrading)
				{
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_HDR_GRADING", state: true);
				}
				else
				{
					switch (data.toneMappingMode)
					{
					case global::UnityEngine.Rendering.Universal.TonemappingMode.Neutral:
						global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_TONEMAP_NEUTRAL", state: true);
						break;
					case global::UnityEngine.Rendering.Universal.TonemappingMode.ACES:
						global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_TONEMAP_ACES", state: true);
						break;
					}
				}
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", data.enableAlphaOutput);
				if (data.cameraData.xr.enabled && data.cameraData.xr.hasValidVisibleMesh)
				{
					ScaleViewportAndDrawVisibilityMesh(in context, in data.sourceTexture, in data.destinationTexture, data.cameraData, material, data.hasFinalPass);
				}
				else
				{
					ScaleViewportAndBlit(in context, in data.sourceTexture, in data.destinationTexture, data.cameraData, material, data.hasFinalPass);
				}
			});
		}

		public void RenderPostProcessingRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle activeCameraColorTexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle lutTexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle postProcessingTarget, bool hasFinalPass, bool resolveToDebugScreen, bool enableColorEndingIfNeeded)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalPostProcessingData universalPostProcessingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();
			global::UnityEngine.Rendering.VolumeStack stack = global::UnityEngine.Rendering.VolumeManager.instance.stack;
			m_DepthOfField = stack.GetComponent<global::UnityEngine.Rendering.Universal.DepthOfField>();
			m_MotionBlur = stack.GetComponent<global::UnityEngine.Rendering.Universal.MotionBlur>();
			m_PaniniProjection = stack.GetComponent<global::UnityEngine.Rendering.Universal.PaniniProjection>();
			m_Bloom = stack.GetComponent<global::UnityEngine.Rendering.Universal.Bloom>();
			m_LensFlareScreenSpace = stack.GetComponent<global::UnityEngine.Rendering.Universal.ScreenSpaceLensFlare>();
			m_LensDistortion = stack.GetComponent<global::UnityEngine.Rendering.Universal.LensDistortion>();
			m_ChromaticAberration = stack.GetComponent<global::UnityEngine.Rendering.Universal.ChromaticAberration>();
			m_Vignette = stack.GetComponent<global::UnityEngine.Rendering.Universal.Vignette>();
			m_ColorLookup = stack.GetComponent<global::UnityEngine.Rendering.Universal.ColorLookup>();
			m_ColorAdjustments = stack.GetComponent<global::UnityEngine.Rendering.Universal.ColorAdjustments>();
			m_Tonemapping = stack.GetComponent<global::UnityEngine.Rendering.Universal.Tonemapping>();
			m_FilmGrain = stack.GetComponent<global::UnityEngine.Rendering.Universal.FilmGrain>();
			m_UseFastSRGBLinearConversion = universalPostProcessingData.useFastSRGBLinearConversion;
			m_SupportDataDrivenLensFlare = universalPostProcessingData.supportDataDrivenLensFlare;
			m_SupportScreenSpaceLensFlare = universalPostProcessingData.supportScreenSpaceLensFlare;
			m_HasFinalPass = hasFinalPass;
			m_EnableColorEncodingIfNeeded = enableColorEndingIfNeeded;
			ref global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer = ref universalCameraData.renderer;
			bool isSceneViewCamera = universalCameraData.isSceneViewCamera;
			bool flag = universalCameraData.isStopNaNEnabled && m_Materials.stopNaN != null;
			bool flag2 = universalCameraData.antialiasing == global::UnityEngine.Rendering.Universal.AntialiasingMode.SubpixelMorphologicalAntiAliasing;
			global::UnityEngine.Material material = ((m_DepthOfField.mode.value == global::UnityEngine.Rendering.Universal.DepthOfFieldMode.Gaussian) ? m_Materials.gaussianDepthOfField : m_Materials.bokehDepthOfField);
			bool flag3 = m_DepthOfField.IsActive() && !isSceneViewCamera && material != null;
			bool flag4 = !global::UnityEngine.Rendering.LensFlareCommonSRP.Instance.IsEmpty() && m_SupportDataDrivenLensFlare;
			bool flag5 = m_LensFlareScreenSpace.IsActive() && m_SupportScreenSpaceLensFlare;
			bool flag6 = m_MotionBlur.IsActive() && !isSceneViewCamera;
			bool flag7 = m_PaniniProjection.IsActive() && !isSceneViewCamera;
			flag6 = flag6 && global::UnityEngine.Application.isPlaying;
			if (flag6 && m_MotionBlur.mode.value == global::UnityEngine.Rendering.Universal.MotionBlurMode.CameraAndObjects)
			{
				flag6 &= renderer.SupportsMotionVectors();
				if (!flag6)
				{
					string message = "Disabling Motion Blur for Camera And Objects because the renderer does not implement motion vectors.";
					if (global::UnityEngine.Time.frameCount % 60 == 0)
					{
						global::UnityEngine.Debug.LogWarning(message);
					}
				}
			}
			bool flag8 = universalCameraData.IsTemporalAAEnabled();
			bool flag9 = universalCameraData.IsSTPRequested();
			bool flag10 = flag8 && flag9;
			if (!flag8 && universalCameraData.IsTemporalAARequested())
			{
				global::UnityEngine.Rendering.Universal.TemporalAA.ValidateAndWarn(universalCameraData, flag9);
			}
			global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostFXSetupPassData passData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostFXSetupPassData>("Setup PostFX passes", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_SetupPostFX), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\PostProcessPassRenderGraph.cs", 2674))
			{
				rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.PostFXSetupPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
				{
					context.cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._FullscreenProjMat, global::UnityEngine.GL.GetGPUProjectionMatrix(global::UnityEngine.Matrix4x4.identity, renderIntoTexture: true));
				});
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle activeCameraColor = activeCameraColorTexture;
			if (flag)
			{
				RenderStopNaN(renderGraph, in activeCameraColor, out var stopNaNTarget);
				activeCameraColor = stopNaNTarget;
			}
			if (flag2)
			{
				RenderSMAA(renderGraph, resourceData, universalCameraData.antialiasingQuality, in activeCameraColor, out var SMAATarget);
				activeCameraColor = SMAATarget;
			}
			if (flag3)
			{
				RenderDoF(renderGraph, resourceData, universalCameraData, in activeCameraColor, out var destination);
				activeCameraColor = destination;
			}
			if (flag8)
			{
				if (flag10)
				{
					RenderSTP(renderGraph, resourceData, universalCameraData, ref activeCameraColor, out var destination2);
					activeCameraColor = destination2;
				}
				else
				{
					RenderTemporalAA(renderGraph, resourceData, universalCameraData, ref activeCameraColor, out var destination3);
					activeCameraColor = destination3;
				}
			}
			if (flag6)
			{
				RenderMotionBlur(renderGraph, resourceData, universalCameraData, in activeCameraColor, out var destination4);
				activeCameraColor = destination4;
			}
			if (flag7)
			{
				RenderPaniniProjection(renderGraph, universalCameraData.camera, in activeCameraColor, out var destination5);
				activeCameraColor = destination5;
			}
			m_Materials.uber.shaderKeywords = null;
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc bloomSourceDesc = activeCameraColor.GetDescriptor(renderGraph);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination6 = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			if (m_Bloom.IsActive() || flag5)
			{
				RenderBloomTexture(renderGraph, in activeCameraColor, out destination6, universalCameraData.isAlphaOutputEnabled);
				if (flag5)
				{
					int num = CalcBloomMipCount(m_Bloom, CalcBloomResolution(m_Bloom, in bloomSourceDesc));
					int max = global::UnityEngine.Mathf.Clamp(num - 1, 0, m_Bloom.maxIterations.value / 2);
					int num2 = global::UnityEngine.Mathf.Clamp(m_LensFlareScreenSpace.bloomMip.value, 0, max);
					global::UnityEngine.Rendering.RenderGraphModule.TextureHandle screenSpaceLensFlareBloomMipTexture = _BloomMipUp[num2];
					bool sameBloomInputOutputTex = false;
					if (num2 == 0)
					{
						if (num == 1 && m_Bloom.filter != global::UnityEngine.Rendering.Universal.BloomFilterMode.Kawase)
						{
							screenSpaceLensFlareBloomMipTexture = _BloomMipDown[0];
						}
						sameBloomInputOutputTex = true;
					}
					if (m_Bloom.filter.value == global::UnityEngine.Rendering.Universal.BloomFilterMode.Kawase)
					{
						screenSpaceLensFlareBloomMipTexture = destination6;
						sameBloomInputOutputTex = true;
					}
					destination6 = RenderLensFlareScreenSpace(renderGraph, universalCameraData.camera, in bloomSourceDesc, destination6, screenSpaceLensFlareBloomMipTexture, sameBloomInputOutputTex);
				}
				UberPostSetupBloomPass(renderGraph, m_Materials.uber, in bloomSourceDesc);
			}
			if (flag4)
			{
				LensFlareDataDrivenComputeOcclusion(renderGraph, resourceData, universalCameraData, in bloomSourceDesc);
				RenderLensFlareDataDriven(renderGraph, resourceData, universalCameraData, in activeCameraColor, in bloomSourceDesc);
			}
			SetupLensDistortion(m_Materials.uber, isSceneViewCamera);
			SetupChromaticAberration(m_Materials.uber);
			SetupVignette(m_Materials.uber, universalCameraData.xr, bloomSourceDesc.width, bloomSourceDesc.height);
			SetupGrain(universalCameraData, m_Materials.uber);
			SetupDithering(universalCameraData, m_Materials.uber);
			if (RequireSRGBConversionBlitToBackBuffer(universalCameraData.requireSrgbConversion))
			{
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Materials.uber, "_LINEAR_TO_SRGB_CONVERSION", state: true);
			}
			if (m_UseFastSRGBLinearConversion)
			{
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Materials.uber, "_USE_FAST_SRGB_LINEAR_CONVERSION", state: true);
			}
			bool flag11 = RequireHDROutput(universalCameraData);
			if (flag11)
			{
				global::UnityEngine.Rendering.HDROutputUtils.Operation hdrOperations = ((!m_HasFinalPass && m_EnableColorEncodingIfNeeded) ? global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding : global::UnityEngine.Rendering.HDROutputUtils.Operation.None);
				SetupHDROutput(universalCameraData.hdrDisplayInformation, universalCameraData.hdrDisplayColorGamut, m_Materials.uber, hdrOperations, universalCameraData.rendersOverlayUI);
				global::UnityEngine.Rendering.Universal.RenderingUtils.SetupOffscreenUIViewportParams(m_Materials.uber, ref universalCameraData.pixelRect, !m_HasFinalPass && universalCameraData.resolveFinalTarget);
			}
			bool isAlphaOutputEnabled = universalCameraData.isAlphaOutputEnabled;
			global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(universalCameraData);
			RenderUberPost(renderGraph, frameData, universalCameraData, universalPostProcessingData, in activeCameraColor, in postProcessingTarget, in lutTexture, in destination6, in overlayUITexture, flag11, isAlphaOutputEnabled, hasFinalPass);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetupLensDistortion(global::UnityEngine.Material material, bool isSceneView)
		{
			float b = 1.6f * global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(m_LensDistortion.intensity.value * 100f), 1f);
			float num = global::System.MathF.PI / 180f * global::UnityEngine.Mathf.Min(160f, b);
			float y = 2f * global::UnityEngine.Mathf.Tan(num * 0.5f);
			global::UnityEngine.Vector2 vector = m_LensDistortion.center.value * 2f - global::UnityEngine.Vector2.one;
			global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(vector.x, vector.y, global::UnityEngine.Mathf.Max(m_LensDistortion.xMultiplier.value, 0.0001f), global::UnityEngine.Mathf.Max(m_LensDistortion.yMultiplier.value, 0.0001f));
			global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4((m_LensDistortion.intensity.value >= 0f) ? num : (1f / num), y, 1f / m_LensDistortion.scale.value, m_LensDistortion.intensity.value * 100f);
			material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Distortion_Params1, value);
			material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Distortion_Params2, value2);
			if (m_LensDistortion.IsActive() && !isSceneView)
			{
				material.EnableKeyword("_DISTORTION");
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetupChromaticAberration(global::UnityEngine.Material material)
		{
			material.SetFloat(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Chroma_Params, m_ChromaticAberration.intensity.value * 0.05f);
			if (m_ChromaticAberration.IsActive())
			{
				material.EnableKeyword("_CHROMATIC_ABERRATION");
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetupVignette(global::UnityEngine.Material material, global::UnityEngine.Experimental.Rendering.XRPass xrPass, int width, int height)
		{
			global::UnityEngine.Color value = m_Vignette.color.value;
			global::UnityEngine.Vector2 center = m_Vignette.center.value;
			float num = (float)width / (float)height;
			if (xrPass != null && xrPass.enabled)
			{
				if (xrPass.singlePassEnabled)
				{
					material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Vignette_ParamsXR, xrPass.ApplyXRViewCenterOffset(center));
				}
				else
				{
					center = xrPass.ApplyXRViewCenterOffset(center);
				}
			}
			global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4(value.r, value.g, value.b, m_Vignette.rounded.value ? num : 1f);
			global::UnityEngine.Vector4 value3 = new global::UnityEngine.Vector4(center.x, center.y, m_Vignette.intensity.value * 3f, m_Vignette.smoothness.value * 5f);
			material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Vignette_Params1, value2);
			material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessPassRenderGraph.ShaderConstants._Vignette_Params2, value3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetupGrain(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Material material)
		{
			if (!m_HasFinalPass && m_FilmGrain.IsActive())
			{
				material.EnableKeyword("_FILM_GRAIN");
				global::UnityEngine.Rendering.Universal.PostProcessUtils.ConfigureFilmGrain(m_Materials.resources, m_FilmGrain, cameraData.pixelWidth, cameraData.pixelHeight, material);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetupDithering(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Material material)
		{
			if (!m_HasFinalPass && cameraData.isDitheringEnabled)
			{
				material.EnableKeyword("_DITHERING");
				m_DitheringTextureIndex = global::UnityEngine.Rendering.Universal.PostProcessUtils.ConfigureDithering(m_Materials.resources, m_DitheringTextureIndex, cameraData.pixelWidth, cameraData.pixelHeight, material);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetupHDROutput(global::UnityEngine.Rendering.HDROutputUtils.HDRDisplayInformation hdrDisplayInformation, global::UnityEngine.ColorGamut hdrDisplayColorGamut, global::UnityEngine.Material material, global::UnityEngine.Rendering.HDROutputUtils.Operation hdrOperations, bool rendersOverlayUI)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.GetHDROutputLuminanceParameters(hdrDisplayInformation, hdrDisplayColorGamut, m_Tonemapping, out var hdrOutputParameters);
			material.SetVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.hdrOutputLuminanceParams, hdrOutputParameters);
			global::UnityEngine.Rendering.HDROutputUtils.ConfigureHDROutput(material, hdrDisplayColorGamut, hdrOperations);
			global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_HDR_OVERLAY", rendersOverlayUI);
		}
	}
}
