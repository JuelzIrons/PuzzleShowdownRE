namespace UnityEngine.Rendering.Universal
{
	public static class TemporalAA
	{
		internal static class ShaderConstants
		{
			public static readonly int _TaaAccumulationTex = global::UnityEngine.Shader.PropertyToID("_TaaAccumulationTex");

			public static readonly int _TaaMotionVectorTex = global::UnityEngine.Shader.PropertyToID("_TaaMotionVectorTex");

			public static readonly int _TaaFilterWeights = global::UnityEngine.Shader.PropertyToID("_TaaFilterWeights");

			public static readonly int _TaaFrameInfluence = global::UnityEngine.Shader.PropertyToID("_TaaFrameInfluence");

			public static readonly int _TaaVarianceClampScale = global::UnityEngine.Shader.PropertyToID("_TaaVarianceClampScale");

			public static readonly int _CameraDepthTexture = global::UnityEngine.Shader.PropertyToID("_CameraDepthTexture");
		}

		internal static class ShaderKeywords
		{
			public static readonly string TAA_LOW_PRECISION_SOURCE = "TAA_LOW_PRECISION_SOURCE";
		}

		[global::System.Serializable]
		public struct Settings
		{
			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Serialization.FormerlySerializedAs("quality")]
			internal global::UnityEngine.Rendering.Universal.TemporalAAQuality m_Quality;

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Serialization.FormerlySerializedAs("frameInfluence")]
			internal float m_FrameInfluence;

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Serialization.FormerlySerializedAs("jitterScale")]
			internal float m_JitterScale;

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Serialization.FormerlySerializedAs("mipBias")]
			internal float m_MipBias;

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Serialization.FormerlySerializedAs("varianceClampScale")]
			internal float m_VarianceClampScale;

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Serialization.FormerlySerializedAs("contrastAdaptiveSharpening")]
			internal float m_ContrastAdaptiveSharpening;

			[global::System.NonSerialized]
			internal int resetHistoryFrames;

			[global::System.NonSerialized]
			internal int jitterFrameCountOffset;

			public global::UnityEngine.Rendering.Universal.TemporalAAQuality quality
			{
				get
				{
					return m_Quality;
				}
				set
				{
					m_Quality = (global::UnityEngine.Rendering.Universal.TemporalAAQuality)global::UnityEngine.Mathf.Clamp((int)value, 0, 4);
				}
			}

			public float baseBlendFactor
			{
				get
				{
					return 1f - m_FrameInfluence;
				}
				set
				{
					m_FrameInfluence = global::UnityEngine.Mathf.Clamp01(1f - value);
				}
			}

			public float jitterScale
			{
				get
				{
					return m_JitterScale;
				}
				set
				{
					m_JitterScale = global::UnityEngine.Mathf.Clamp01(value);
				}
			}

			public float mipBias
			{
				get
				{
					return m_MipBias;
				}
				set
				{
					m_MipBias = global::UnityEngine.Mathf.Clamp(value, -1f, 0f);
				}
			}

			public float varianceClampScale
			{
				get
				{
					return m_VarianceClampScale;
				}
				set
				{
					m_VarianceClampScale = global::UnityEngine.Mathf.Clamp(value, 0.001f, 10f);
				}
			}

			public float contrastAdaptiveSharpening
			{
				get
				{
					return m_ContrastAdaptiveSharpening;
				}
				set
				{
					m_ContrastAdaptiveSharpening = global::UnityEngine.Mathf.Clamp01(value);
				}
			}

			public static global::UnityEngine.Rendering.Universal.TemporalAA.Settings Create()
			{
				global::UnityEngine.Rendering.Universal.TemporalAA.Settings result = default(global::UnityEngine.Rendering.Universal.TemporalAA.Settings);
				result.m_Quality = global::UnityEngine.Rendering.Universal.TemporalAAQuality.High;
				result.m_FrameInfluence = 0.1f;
				result.m_JitterScale = 1f;
				result.m_MipBias = 0f;
				result.m_VarianceClampScale = 0.9f;
				result.m_ContrastAdaptiveSharpening = 0f;
				result.resetHistoryFrames = 0;
				result.jitterFrameCountOffset = 0;
				return result;
			}
		}

		internal delegate void JitterFunc(int frameIndex, out global::UnityEngine.Vector2 jitter, out bool allowScaling);

		private class TaaPassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dstTex;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcColorTex;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcDepthTex;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcMotionVectorTex;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcTaaAccumTex;

			internal global::UnityEngine.Material material;

			internal int passIndex;

			internal float taaFrameInfluence;

			internal float taaVarianceClampScale;

			internal float[] taaFilterWeights;

			internal bool taaLowPrecisionSource;

			internal bool taaAlphaOutput;
		}

		internal static global::UnityEngine.Rendering.Universal.TemporalAA.JitterFunc s_JitterFunc = CalculateJitter;

		private static readonly global::UnityEngine.Vector2[] taaFilterOffsets = new global::UnityEngine.Vector2[9]
		{
			new global::UnityEngine.Vector2(0f, 0f),
			new global::UnityEngine.Vector2(0f, 1f),
			new global::UnityEngine.Vector2(1f, 0f),
			new global::UnityEngine.Vector2(-1f, 0f),
			new global::UnityEngine.Vector2(0f, -1f),
			new global::UnityEngine.Vector2(-1f, 1f),
			new global::UnityEngine.Vector2(1f, -1f),
			new global::UnityEngine.Vector2(1f, 1f),
			new global::UnityEngine.Vector2(-1f, -1f)
		};

		private static readonly float[] taaFilterWeights = new float[taaFilterOffsets.Length + 1];

		internal static global::UnityEngine.Experimental.Rendering.GraphicsFormat[] AccumulationFormatList = new global::UnityEngine.Experimental.Rendering.GraphicsFormat[4]
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat,
			global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32,
			global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm,
			global::UnityEngine.Experimental.Rendering.GraphicsFormat.B8G8R8A8_UNorm
		};

		private static uint s_warnCounter = 0u;

		internal static int CalculateTaaFrameIndex(ref global::UnityEngine.Rendering.Universal.TemporalAA.Settings settings)
		{
			int jitterFrameCountOffset = settings.jitterFrameCountOffset;
			return global::UnityEngine.Time.frameCount + jitterFrameCountOffset;
		}

		internal static global::UnityEngine.Matrix4x4 CalculateJitterMatrix(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.TemporalAA.JitterFunc jitterFunc)
		{
			global::UnityEngine.Matrix4x4 result = global::UnityEngine.Matrix4x4.identity;
			if (cameraData.IsTemporalAAEnabled())
			{
				int frameIndex = CalculateTaaFrameIndex(ref cameraData.taaSettings);
				float num = cameraData.cameraTargetDescriptor.width;
				float num2 = cameraData.cameraTargetDescriptor.height;
				float jitterScale = cameraData.taaSettings.jitterScale;
				jitterFunc(frameIndex, out var jitter, out var allowScaling);
				if (allowScaling)
				{
					jitter *= jitterScale;
				}
				float x = jitter.x * (2f / num);
				float y = jitter.y * (2f / num2);
				result = global::UnityEngine.Matrix4x4.Translate(new global::UnityEngine.Vector3(x, y, 0f));
			}
			return result;
		}

		internal static void CalculateJitter(int frameIndex, out global::UnityEngine.Vector2 jitter, out bool allowScaling)
		{
			float x = global::UnityEngine.Rendering.HaltonSequence.Get((frameIndex & 0x3FF) + 1, 2) - 0.5f;
			float y = global::UnityEngine.Rendering.HaltonSequence.Get((frameIndex & 0x3FF) + 1, 3) - 0.5f;
			jitter = new global::UnityEngine.Vector2(x, y);
			allowScaling = true;
		}

		internal static float[] CalculateFilterWeights(ref global::UnityEngine.Rendering.Universal.TemporalAA.Settings settings)
		{
			int frameIndex = CalculateTaaFrameIndex(ref settings);
			float num = 0f;
			for (int i = 0; i < 9; i++)
			{
				CalculateJitter(frameIndex, out var jitter, out var _);
				jitter *= settings.jitterScale;
				float num2 = taaFilterOffsets[i].x - jitter.x;
				float num3 = taaFilterOffsets[i].y - jitter.y;
				float num4 = num2 * num2 + num3 * num3;
				taaFilterWeights[i] = global::UnityEngine.Mathf.Exp(-2.2727273f * num4);
				num += taaFilterWeights[i];
			}
			for (int j = 0; j < 9; j++)
			{
				taaFilterWeights[j] /= num;
			}
			return taaFilterWeights;
		}

		internal static global::UnityEngine.RenderTextureDescriptor TemporalAADescFromCameraDesc(ref global::UnityEngine.RenderTextureDescriptor cameraDesc)
		{
			global::UnityEngine.RenderTextureDescriptor result = cameraDesc;
			result.width = cameraDesc.width;
			result.height = cameraDesc.height;
			result.msaaSamples = 1;
			result.volumeDepth = cameraDesc.volumeDepth;
			result.mipCount = 0;
			result.graphicsFormat = cameraDesc.graphicsFormat;
			result.sRGB = false;
			result.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			result.dimension = cameraDesc.dimension;
			result.vrUsage = cameraDesc.vrUsage;
			result.memoryless = global::UnityEngine.RenderTextureMemoryless.None;
			result.useMipMap = false;
			result.autoGenerateMips = false;
			result.enableRandomWrite = false;
			result.bindMS = false;
			result.useDynamicScale = false;
			if (!global::UnityEngine.SystemInfo.IsFormatSupported(result.graphicsFormat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render))
			{
				result.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				for (int i = 0; i < AccumulationFormatList.Length; i++)
				{
					if (global::UnityEngine.SystemInfo.IsFormatSupported(AccumulationFormatList[i], global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render))
					{
						result.graphicsFormat = AccumulationFormatList[i];
						break;
					}
				}
			}
			return result;
		}

		internal static string ValidateAndWarn(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool isSTPRequested = false)
		{
			string text = null;
			if (text == null && !cameraData.postProcessEnabled)
			{
				text = "because camera has post-processing disabled.";
			}
			if (cameraData.taaHistory == null)
			{
				text = "due to invalid persistent data.";
			}
			if (text == null && cameraData.cameraTargetDescriptor.msaaSamples != 1)
			{
				text = ((cameraData.xr == null || !cameraData.xr.enabled) ? "because MSAA is on. Turn MSAA off on the camera or current URP Asset." : "because MSAA is on. MSAA must be disabled globally for all cameras in XR mode.");
			}
			if (text == null && cameraData.camera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component) && (component.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay || component.cameraStack.Count > 0))
			{
				text = "because camera is stacked.";
			}
			if (text == null && cameraData.camera.allowDynamicResolution)
			{
				text = "because camera has dynamic resolution enabled. You can use a constant render scale instead.";
			}
			if (text == null && !cameraData.renderer.SupportsMotionVectors())
			{
				text = "because the renderer does not implement motion vectors. Motion vectors are required.";
			}
			if (text != null)
			{
				if (s_warnCounter % 60 == 0)
				{
					global::UnityEngine.Debug.LogWarning("Disabling TAA " + (isSTPRequested ? "and STP " : "") + text);
				}
				s_warnCounter++;
			}
			return text;
		}

		internal static void ExecutePass(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Material taaMaterial, ref global::UnityEngine.Rendering.Universal.CameraData cameraData, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.RenderTexture motionVectors)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.TemporalAA)))
			{
				int num = 0;
				num = cameraData.xr.multipassId;
				bool flag = cameraData.taaHistory.GetAccumulationVersion(num) != global::UnityEngine.Time.frameCount;
				global::UnityEngine.Rendering.RTHandle accumulationTexture = cameraData.taaHistory.GetAccumulationTexture(num);
				taaMaterial.SetTexture(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaAccumulationTex, accumulationTexture);
				taaMaterial.SetTexture(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaMotionVectorTex, flag ? ((global::UnityEngine.Texture)motionVectors) : ((global::UnityEngine.Texture)global::UnityEngine.Texture2D.blackTexture));
				ref global::UnityEngine.Rendering.Universal.TemporalAA.Settings taaSettings = ref cameraData.taaSettings;
				float value = ((taaSettings.resetHistoryFrames == 0) ? taaSettings.m_FrameInfluence : 1f);
				taaMaterial.SetFloat(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaFrameInfluence, value);
				taaMaterial.SetFloat(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaVarianceClampScale, taaSettings.varianceClampScale);
				if (taaSettings.quality == global::UnityEngine.Rendering.Universal.TemporalAAQuality.VeryHigh)
				{
					taaMaterial.SetFloatArray(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaFilterWeights, CalculateFilterWeights(ref taaSettings));
				}
				global::UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat = accumulationTexture.rt.graphicsFormat;
				if (graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm || graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.B8G8R8A8_UNorm || graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32)
				{
					taaMaterial.EnableKeyword(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderKeywords.TAA_LOW_PRECISION_SOURCE);
				}
				else
				{
					taaMaterial.DisableKeyword(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderKeywords.TAA_LOW_PRECISION_SOURCE);
				}
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(taaMaterial, "_ENABLE_ALPHA_OUTPUT", cameraData.isAlphaOutputEnabled);
				global::UnityEngine.Rendering.Blitter.BlitCameraTexture(cmd, source, destination, global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, taaMaterial, (int)taaSettings.quality);
				if (flag)
				{
					int pass = taaMaterial.shader.passCount - 1;
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(cmd, destination, accumulationTexture, global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, taaMaterial, pass);
					cameraData.taaHistory.SetAccumulationVersion(num, global::UnityEngine.Time.frameCount);
				}
			}
		}

		internal static void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Material taaMaterial, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcColor, ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcDepth, ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcMotionVectors, ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dstColor)
		{
			int num = 0;
			num = cameraData.xr.multipassId;
			ref global::UnityEngine.Rendering.Universal.TemporalAA.Settings taaSettings = ref cameraData.taaSettings;
			bool flag = cameraData.taaHistory.GetAccumulationVersion(num) != global::UnityEngine.Time.frameCount;
			float taaFrameInfluence = ((taaSettings.resetHistoryFrames == 0) ? taaSettings.m_FrameInfluence : 1f);
			global::UnityEngine.Rendering.RTHandle accumulationTexture = cameraData.taaHistory.GetAccumulationTexture(num);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = renderGraph.ImportTexture(accumulationTexture);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input2 = (flag ? srcMotionVectors : renderGraph.defaultResources.blackTexture);
			global::UnityEngine.Rendering.Universal.TemporalAA.TaaPassData passData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.TemporalAA.TaaPassData>("Temporal Anti-aliasing", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_TAA), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\TemporalAA.cs", 487))
			{
				passData.dstTex = dstColor;
				rasterRenderGraphBuilder.SetRenderAttachment(dstColor, 0);
				passData.srcColorTex = srcColor;
				rasterRenderGraphBuilder.UseTexture(in srcColor);
				passData.srcDepthTex = srcDepth;
				rasterRenderGraphBuilder.UseTexture(in srcDepth);
				passData.srcMotionVectorTex = input2;
				rasterRenderGraphBuilder.UseTexture(in input2);
				passData.srcTaaAccumTex = input;
				rasterRenderGraphBuilder.UseTexture(in input);
				if (cameraData.xr.enabled)
				{
					rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
				}
				passData.material = taaMaterial;
				passData.passIndex = (int)taaSettings.quality;
				passData.taaFrameInfluence = taaFrameInfluence;
				passData.taaVarianceClampScale = taaSettings.varianceClampScale;
				if (taaSettings.quality == global::UnityEngine.Rendering.Universal.TemporalAAQuality.VeryHigh)
				{
					passData.taaFilterWeights = CalculateFilterWeights(ref taaSettings);
				}
				else
				{
					passData.taaFilterWeights = null;
				}
				global::UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat = accumulationTexture.rt.graphicsFormat;
				if (graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm || graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.B8G8R8A8_UNorm || graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32)
				{
					passData.taaLowPrecisionSource = true;
				}
				else
				{
					passData.taaLowPrecisionSource = false;
				}
				passData.taaAlphaOutput = cameraData.isAlphaOutputEnabled;
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.TemporalAA.TaaPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
				{
					data.material.SetFloat(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaFrameInfluence, data.taaFrameInfluence);
					data.material.SetFloat(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaVarianceClampScale, data.taaVarianceClampScale);
					data.material.SetTexture(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaAccumulationTex, data.srcTaaAccumTex);
					data.material.SetTexture(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaMotionVectorTex, data.srcMotionVectorTex);
					data.material.SetTexture(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._CameraDepthTexture, data.srcDepthTex);
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, global::UnityEngine.Rendering.Universal.TemporalAA.ShaderKeywords.TAA_LOW_PRECISION_SOURCE, data.taaLowPrecisionSource);
					global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, "_ENABLE_ALPHA_OUTPUT", data.taaAlphaOutput);
					if (data.taaFilterWeights != null)
					{
						data.material.SetFloatArray(global::UnityEngine.Rendering.Universal.TemporalAA.ShaderConstants._TaaFilterWeights, data.taaFilterWeights);
					}
					global::UnityEngine.Rendering.Blitter.BlitTexture(context.cmd, data.srcColorTex, global::UnityEngine.Vector2.one, data.material, data.passIndex);
				});
			}
			if (!flag)
			{
				return;
			}
			int passIndex = taaMaterial.shader.passCount - 1;
			global::UnityEngine.Rendering.Universal.TemporalAA.TaaPassData passData2;
			using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder2 = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.TemporalAA.TaaPassData>("Temporal Anti-aliasing Copy History", out passData2, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RG_TAACopyHistory), ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\TemporalAA.cs", 551))
			{
				passData2.dstTex = input;
				rasterRenderGraphBuilder2.SetRenderAttachment(input, 0);
				passData2.srcColorTex = dstColor;
				rasterRenderGraphBuilder2.UseTexture(in dstColor);
				if (cameraData.xr.enabled)
				{
					rasterRenderGraphBuilder2.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
				}
				passData2.material = taaMaterial;
				passData2.passIndex = passIndex;
				rasterRenderGraphBuilder2.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.TemporalAA.TaaPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
				{
					global::UnityEngine.Rendering.Blitter.BlitTexture(context.cmd, data.srcColorTex, global::UnityEngine.Vector2.one, data.material, data.passIndex);
				});
			}
			cameraData.taaHistory.SetAccumulationVersion(num, global::UnityEngine.Time.frameCount);
		}
	}
}
