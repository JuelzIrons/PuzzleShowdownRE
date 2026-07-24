namespace UnityEngine.Rendering
{
	public static class STP
	{
		public struct PerViewConfig
		{
			public global::UnityEngine.Matrix4x4 currentProj;

			public global::UnityEngine.Matrix4x4 lastProj;

			public global::UnityEngine.Matrix4x4 lastLastProj;

			public global::UnityEngine.Matrix4x4 currentView;

			public global::UnityEngine.Matrix4x4 lastView;

			public global::UnityEngine.Matrix4x4 lastLastView;
		}

		public struct Config
		{
			public global::UnityEngine.Texture2D noiseTexture;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputColor;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputDepth;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputMotion;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputStencil;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle debugView;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;

			public global::UnityEngine.Rendering.STP.HistoryContext historyContext;

			public bool enableHwDrs;

			public bool enableTexArray;

			public bool enableMotionScaling;

			public float nearPlane;

			public float farPlane;

			public int frameIndex;

			public bool hasValidHistory;

			public int stencilMask;

			public int debugViewIndex;

			public float deltaTime;

			public float lastDeltaTime;

			public global::UnityEngine.Vector2Int currentImageSize;

			public global::UnityEngine.Vector2Int priorImageSize;

			public global::UnityEngine.Vector2Int outputImageSize;

			public int numActiveViews;

			public global::UnityEngine.Rendering.STP.PerViewConfig[] perViewConfigs;
		}

		internal enum HistoryTextureType
		{
			DepthMotion = 0,
			Luma = 1,
			Convergence = 2,
			Feedback = 3,
			Count = 4
		}

		public struct HistoryUpdateInfo
		{
			public global::UnityEngine.Vector2Int preUpscaleSize;

			public global::UnityEngine.Vector2Int postUpscaleSize;

			public bool useHwDrs;

			public bool useTexArray;
		}

		public sealed class HistoryContext : global::System.IDisposable
		{
			private global::UnityEngine.Rendering.RTHandle[] m_textures = new global::UnityEngine.Rendering.RTHandle[8];

			private global::UnityEngine.Hash128 m_hash = global::UnityEngine.Hash128.Compute(0);

			public bool Update(ref global::UnityEngine.Rendering.STP.HistoryUpdateInfo info)
			{
				bool result = true;
				global::UnityEngine.Hash128 hash = ComputeHistoryHash(ref info);
				if (hash != m_hash)
				{
					result = false;
					Dispose();
					m_hash = hash;
					global::UnityEngine.Vector2Int historyTextureSize = (info.useHwDrs ? info.postUpscaleSize : info.preUpscaleSize);
					global::UnityEngine.Rendering.TextureDimension dimension = (info.useTexArray ? global::UnityEngine.Rendering.TextureDimension.Tex2DArray : global::UnityEngine.Rendering.TextureDimension.Tex2D);
					int slices = ((!info.useTexArray) ? 1 : global::UnityEngine.Rendering.TextureXR.slices);
					int num = 0;
					int num2 = 0;
					global::UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
					bool useDynamicScaleExplicit = false;
					string text = "";
					for (int i = 0; i < 4; i++)
					{
						switch ((global::UnityEngine.Rendering.STP.HistoryTextureType)i)
						{
						case global::UnityEngine.Rendering.STP.HistoryTextureType.DepthMotion:
							num = historyTextureSize.x;
							num2 = historyTextureSize.y;
							graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_UInt;
							useDynamicScaleExplicit = info.useHwDrs;
							text = "STP Depth & Motion";
							break;
						case global::UnityEngine.Rendering.STP.HistoryTextureType.Luma:
							num = historyTextureSize.x;
							num2 = historyTextureSize.y;
							graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8_UNorm;
							useDynamicScaleExplicit = info.useHwDrs;
							text = "STP Luma";
							break;
						case global::UnityEngine.Rendering.STP.HistoryTextureType.Convergence:
						{
							global::UnityEngine.Vector2Int vector2Int = CalculateConvergenceTextureSize(historyTextureSize);
							num = vector2Int.x;
							num2 = vector2Int.y;
							graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm;
							useDynamicScaleExplicit = info.useHwDrs;
							text = "STP Convergence";
							break;
						}
						case global::UnityEngine.Rendering.STP.HistoryTextureType.Feedback:
							num = info.postUpscaleSize.x;
							num2 = info.postUpscaleSize.y;
							graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.A2B10G10R10_UNormPack32;
							useDynamicScaleExplicit = false;
							text = "STP Feedback";
							break;
						}
						for (int j = 0; j < 2; j++)
						{
							int num3 = j * 4 + i;
							global::UnityEngine.Rendering.RTHandle[] textures = m_textures;
							int width = num;
							int height = num2;
							global::UnityEngine.Experimental.Rendering.GraphicsFormat format = graphicsFormat;
							string name = text;
							textures[num3] = global::UnityEngine.Rendering.RTHandles.Alloc(width, height, format, slices, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Repeat, dimension, enableRandomWrite: true, useMipMap: false, autoGenerateMips: true, isShadowMap: false, 1, 0f, global::UnityEngine.Rendering.MSAASamples.None, bindTextureMS: false, useDynamicScale: false, useDynamicScaleExplicit, global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage.None, name);
						}
					}
				}
				return result;
			}

			internal global::UnityEngine.Rendering.RTHandle GetCurrentHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType historyType, int frameIndex)
			{
				return m_textures[(int)((frameIndex & 1) * 4 + historyType)];
			}

			internal global::UnityEngine.Rendering.RTHandle GetPreviousHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType historyType, int frameIndex)
			{
				return m_textures[(int)(((frameIndex & 1) ^ 1) * 4 + historyType)];
			}

			public void Dispose()
			{
				for (int i = 0; i < m_textures.Length; i++)
				{
					if (m_textures[i] != null)
					{
						m_textures[i].Release();
						m_textures[i] = null;
					}
				}
				m_hash = global::UnityEngine.Hash128.Compute(0);
			}
		}

		[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\STP\\STP.cs")]
		private enum StpSetupPerViewConstants
		{
			Count = 8
		}

		[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\STP\\STP.cs", needAccessors = false, generateCBuffer = true)]
		private struct StpConstantBufferData
		{
			public global::UnityEngine.Vector4 _StpCommonConstant;

			public global::UnityEngine.Vector4 _StpSetupConstants0;

			public global::UnityEngine.Vector4 _StpSetupConstants1;

			public global::UnityEngine.Vector4 _StpSetupConstants2;

			public global::UnityEngine.Vector4 _StpSetupConstants3;

			public global::UnityEngine.Vector4 _StpSetupConstants4;

			public global::UnityEngine.Vector4 _StpSetupConstants5;

			[global::UnityEngine.Rendering.HLSLArray(16, typeof(global::UnityEngine.Vector4))]
			public unsafe fixed float _StpSetupPerViewConstants[64];

			public global::UnityEngine.Vector4 _StpDilConstants0;

			public global::UnityEngine.Vector4 _StpTaaConstants0;

			public global::UnityEngine.Vector4 _StpTaaConstants1;

			public global::UnityEngine.Vector4 _StpTaaConstants2;

			public global::UnityEngine.Vector4 _StpTaaConstants3;
		}

		private static class ShaderResources
		{
			public static readonly int _StpConstantBufferData = global::UnityEngine.Shader.PropertyToID("StpConstantBufferData");

			public static readonly int _StpBlueNoiseIn = global::UnityEngine.Shader.PropertyToID("_StpBlueNoiseIn");

			public static readonly int _StpDebugOut = global::UnityEngine.Shader.PropertyToID("_StpDebugOut");

			public static readonly int _StpInputColor = global::UnityEngine.Shader.PropertyToID("_StpInputColor");

			public static readonly int _StpInputDepth = global::UnityEngine.Shader.PropertyToID("_StpInputDepth");

			public static readonly int _StpInputMotion = global::UnityEngine.Shader.PropertyToID("_StpInputMotion");

			public static readonly int _StpInputStencil = global::UnityEngine.Shader.PropertyToID("_StpInputStencil");

			public static readonly int _StpIntermediateColor = global::UnityEngine.Shader.PropertyToID("_StpIntermediateColor");

			public static readonly int _StpIntermediateConvergence = global::UnityEngine.Shader.PropertyToID("_StpIntermediateConvergence");

			public static readonly int _StpIntermediateWeights = global::UnityEngine.Shader.PropertyToID("_StpIntermediateWeights");

			public static readonly int _StpPriorLuma = global::UnityEngine.Shader.PropertyToID("_StpPriorLuma");

			public static readonly int _StpLuma = global::UnityEngine.Shader.PropertyToID("_StpLuma");

			public static readonly int _StpPriorDepthMotion = global::UnityEngine.Shader.PropertyToID("_StpPriorDepthMotion");

			public static readonly int _StpDepthMotion = global::UnityEngine.Shader.PropertyToID("_StpDepthMotion");

			public static readonly int _StpPriorFeedback = global::UnityEngine.Shader.PropertyToID("_StpPriorFeedback");

			public static readonly int _StpFeedback = global::UnityEngine.Shader.PropertyToID("_StpFeedback");

			public static readonly int _StpPriorConvergence = global::UnityEngine.Shader.PropertyToID("_StpPriorConvergence");

			public static readonly int _StpConvergence = global::UnityEngine.Shader.PropertyToID("_StpConvergence");

			public static readonly int _StpOutput = global::UnityEngine.Shader.PropertyToID("_StpOutput");
		}

		private static class ShaderKeywords
		{
			public static readonly string EnableDebugMode = "ENABLE_DEBUG_MODE";

			public static readonly string EnableLargeKernel = "ENABLE_LARGE_KERNEL";

			public static readonly string EnableStencilResponsive = "ENABLE_STENCIL_RESPONSIVE";

			public static readonly string DisableTexture2DXArray = "DISABLE_TEXTURE2D_X_ARRAY";
		}

		[global::System.Serializable]
		[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
		[global::UnityEngine.Categorization.CategoryInfo(Name = "R: STP", Order = 1000)]
		[global::UnityEngine.Categorization.ElementInfo(Order = 0)]
		[global::UnityEngine.HideInInspector]
		internal class RuntimeResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
		{
			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Rendering.ResourcePath("Runtime/STP/StpSetup.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			private global::UnityEngine.ComputeShader m_setupCS;

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Rendering.ResourcePath("Runtime/STP/StpPreTaa.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			private global::UnityEngine.ComputeShader m_preTaaCS;

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Rendering.ResourcePath("Runtime/STP/StpTaa.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			private global::UnityEngine.ComputeShader m_taaCS;

			public int version => 0;

			public global::UnityEngine.ComputeShader setupCS
			{
				get
				{
					return m_setupCS;
				}
				set
				{
					this.SetValueAndNotify(ref m_setupCS, value, "setupCS");
				}
			}

			public global::UnityEngine.ComputeShader preTaaCS
			{
				get
				{
					return m_preTaaCS;
				}
				set
				{
					this.SetValueAndNotify(ref m_preTaaCS, value, "preTaaCS");
				}
			}

			public global::UnityEngine.ComputeShader taaCS
			{
				get
				{
					return m_taaCS;
				}
				set
				{
					this.SetValueAndNotify(ref m_taaCS, value, "taaCS");
				}
			}
		}

		private enum ProfileId
		{
			StpSetup = 0,
			StpPreTaa = 1,
			StpTaa = 2
		}

		private class SetupData
		{
			public global::UnityEngine.ComputeShader cs;

			public int kernelIndex;

			public int viewCount;

			public global::UnityEngine.Vector2Int dispatchSize;

			public global::UnityEngine.Rendering.STP.StpConstantBufferData constantBufferData;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle noiseTexture;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle debugView;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputColor;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputDepth;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputMotion;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputStencil;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle intermediateColor;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle intermediateConvergence;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle priorDepthMotion;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthMotion;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle priorLuma;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle luma;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle priorFeedback;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle priorConvergence;
		}

		private class PreTaaData
		{
			public global::UnityEngine.ComputeShader cs;

			public int kernelIndex;

			public int viewCount;

			public global::UnityEngine.Vector2Int dispatchSize;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle noiseTexture;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle debugView;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle intermediateConvergence;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle intermediateWeights;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle luma;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle convergence;
		}

		private class TaaData
		{
			public global::UnityEngine.ComputeShader cs;

			public int kernelIndex;

			public int viewCount;

			public global::UnityEngine.Vector2Int dispatchSize;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle noiseTexture;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle debugView;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle intermediateColor;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle intermediateWeights;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle priorFeedback;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthMotion;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle convergence;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle feedback;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle output;
		}

		private const int kNumDebugViews = 6;

		private static readonly global::UnityEngine.GUIContent[] s_DebugViewDescriptions = new global::UnityEngine.GUIContent[6]
		{
			new global::UnityEngine.GUIContent("Clipped Input Color", "Shows input color clipped to {0 to 1}"),
			new global::UnityEngine.GUIContent("Log Input Depth", "Shows input depth in log scale"),
			new global::UnityEngine.GUIContent("Reversible Tonemapped Input Color", "Shows input color after conversion to reversible tonemaped space"),
			new global::UnityEngine.GUIContent("Shaped Absolute Input Motion", "Visualizes input motion vectors"),
			new global::UnityEngine.GUIContent("Motion Reprojection {R=Prior G=This Sqrt Luma Feedback Diff, B=Offscreen}", "Visualizes reprojected frame difference"),
			new global::UnityEngine.GUIContent("Sensitivity {G=No motion match, R=Responsive, B=Luma}", "Visualize pixel sensitivities")
		};

		private static readonly int[] s_DebugViewIndices = new int[6] { 0, 1, 2, 3, 4, 5 };

		private const int kMaxPerViewConfigs = 2;

		private static global::UnityEngine.Rendering.STP.PerViewConfig[] s_PerViewConfigs = new global::UnityEngine.Rendering.STP.PerViewConfig[2];

		private const int kNumHistoryTextureTypes = 4;

		private const int kTotalSetupViewConstantsCount = 16;

		private static readonly int kQualcommVendorId = 20803;

		public static global::UnityEngine.GUIContent[] debugViewDescriptions => s_DebugViewDescriptions;

		public static int[] debugViewIndices => s_DebugViewIndices;

		public static global::UnityEngine.Rendering.STP.PerViewConfig[] perViewConfigs
		{
			get
			{
				return s_PerViewConfigs;
			}
			set
			{
				s_PerViewConfigs = value;
			}
		}

		public static bool IsSupported()
		{
			return (byte)(1u & (global::UnityEngine.SystemInfo.supportsComputeShaders ? 1u : 0u) & ((global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3) ? 1u : 0u)) != 0;
		}

		public static global::UnityEngine.Vector2 Jit16(int frameIndex)
		{
			global::UnityEngine.Vector2 result = default(global::UnityEngine.Vector2);
			result.x = global::UnityEngine.Rendering.HaltonSequence.Get(frameIndex, 2) - 0.5f;
			result.y = global::UnityEngine.Rendering.HaltonSequence.Get(frameIndex, 3) - 0.5f;
			return result;
		}

		private static global::UnityEngine.Hash128 ComputeHistoryHash(ref global::UnityEngine.Rendering.STP.HistoryUpdateInfo info)
		{
			global::UnityEngine.Hash128 result = default(global::UnityEngine.Hash128);
			result.Append(ref info.useHwDrs);
			result.Append(ref info.useTexArray);
			result.Append(ref info.postUpscaleSize);
			if (!info.useHwDrs)
			{
				result.Append(ref info.preUpscaleSize);
			}
			return result;
		}

		private static global::UnityEngine.Vector2Int CalculateConvergenceTextureSize(global::UnityEngine.Vector2Int historyTextureSize)
		{
			return new global::UnityEngine.Vector2Int(global::UnityEngine.Rendering.CoreUtils.DivRoundUp(historyTextureSize.x, 4), global::UnityEngine.Rendering.CoreUtils.DivRoundUp(historyTextureSize.y, 4));
		}

		private static float CalculateMotionScale(float deltaTime, float lastDeltaTime)
		{
			float result = 1f;
			if (!global::UnityEngine.Mathf.Approximately(lastDeltaTime, 0f))
			{
				result = deltaTime / lastDeltaTime;
			}
			return result;
		}

		private static global::UnityEngine.Matrix4x4 ExtractRotation(global::UnityEngine.Matrix4x4 input)
		{
			global::UnityEngine.Matrix4x4 result = input;
			result[0, 3] = 0f;
			result[1, 3] = 0f;
			result[2, 3] = 0f;
			result[3, 3] = 1f;
			return result;
		}

		private static int PackVector2ToInt(global::UnityEngine.Vector2 value)
		{
			ushort num = global::UnityEngine.Mathf.FloatToHalf(value.x);
			uint num2 = global::UnityEngine.Mathf.FloatToHalf(value.y);
			return (int)(num | (num2 << 16));
		}

		private unsafe static void PopulateConstantData(ref global::UnityEngine.Rendering.STP.Config config, ref global::UnityEngine.Rendering.STP.StpConstantBufferData constants)
		{
			int num = (config.noiseTexture.width - 1) & 0xFF;
			int num2 = (config.hasValidHistory ? 1 : 0) << 8;
			int num3 = (config.stencilMask & 0xFF) << 16;
			int num4 = (config.debugViewIndex & 0xFF) << 24;
			int value = num3 | num2 | num | num4;
			float y = (config.farPlane - config.nearPlane) / (config.nearPlane * config.farPlane);
			float z = 1f / config.farPlane;
			constants._StpCommonConstant = new global::UnityEngine.Vector4(global::System.BitConverter.Int32BitsToSingle(value), y, z, 0f);
			constants._StpSetupConstants0.x = 1f / (float)config.currentImageSize.x;
			constants._StpSetupConstants0.y = 1f / (float)config.currentImageSize.y;
			constants._StpSetupConstants0.z = 0.5f / (float)config.currentImageSize.x;
			constants._StpSetupConstants0.w = 0.5f / (float)config.currentImageSize.y;
			global::UnityEngine.Vector2 vector = Jit16(config.frameIndex - 1);
			global::UnityEngine.Vector2 vector2 = Jit16(config.frameIndex);
			constants._StpSetupConstants1.x = vector2.x / (float)config.currentImageSize.x - vector.x / (float)config.priorImageSize.x;
			constants._StpSetupConstants1.y = vector2.y / (float)config.currentImageSize.y - vector.y / (float)config.priorImageSize.y;
			constants._StpSetupConstants1.z = vector2.x / (float)config.currentImageSize.x;
			constants._StpSetupConstants1.w = vector2.y / (float)config.currentImageSize.y;
			constants._StpSetupConstants2.x = config.outputImageSize.x;
			constants._StpSetupConstants2.y = config.outputImageSize.y;
			float num5 = 1f / config.nearPlane;
			float w = 1f / global::UnityEngine.Mathf.Log(num5 * config.farPlane, 2f);
			constants._StpSetupConstants2.z = num5;
			constants._StpSetupConstants2.w = w;
			global::UnityEngine.Vector2 vector3 = default(global::UnityEngine.Vector2);
			vector3.x = 2f;
			vector3.y = 2f;
			vector3.x *= (float)config.priorImageSize.x / ((float)config.priorImageSize.x + 4f);
			vector3.y *= (float)config.priorImageSize.y / ((float)config.priorImageSize.y + 4f);
			constants._StpSetupConstants3.x = vector3[0];
			constants._StpSetupConstants3.y = vector3[1];
			constants._StpSetupConstants3.z = -0.5f * vector3[0];
			constants._StpSetupConstants3.w = -0.5f * vector3[1];
			constants._StpSetupConstants4.x = global::UnityEngine.Mathf.Log(config.farPlane / config.nearPlane, 2f);
			constants._StpSetupConstants4.y = config.nearPlane;
			constants._StpSetupConstants4.z = (config.enableMotionScaling ? CalculateMotionScale(config.deltaTime, config.lastDeltaTime) : 1f);
			constants._StpSetupConstants4.w = 0f;
			constants._StpSetupConstants5.x = config.currentImageSize.x;
			constants._StpSetupConstants5.y = config.currentImageSize.y;
			constants._StpSetupConstants5.z = (float)config.outputImageSize.x / (global::UnityEngine.Mathf.Ceil((float)config.outputImageSize.x / 4f) * 4f);
			constants._StpSetupConstants5.w = (float)config.outputImageSize.y / (global::UnityEngine.Mathf.Ceil((float)config.outputImageSize.y / 4f) * 4f);
			global::UnityEngine.Vector4 vector4 = default(global::UnityEngine.Vector4);
			global::UnityEngine.Vector4 vector5 = default(global::UnityEngine.Vector4);
			global::UnityEngine.Vector4 vector6 = default(global::UnityEngine.Vector4);
			global::UnityEngine.Vector4 vector7 = default(global::UnityEngine.Vector4);
			global::UnityEngine.Vector4 vector8 = default(global::UnityEngine.Vector4);
			global::UnityEngine.Vector4 vector9 = default(global::UnityEngine.Vector4);
			for (uint num6 = 0u; num6 < config.numActiveViews; num6++)
			{
				uint num7 = num6 * 8 * 4;
				global::UnityEngine.Rendering.STP.PerViewConfig perViewConfig = config.perViewConfigs[num6];
				vector4.x = perViewConfig.lastProj[0, 0];
				vector4.y = global::UnityEngine.Mathf.Abs(perViewConfig.lastProj[1, 1]);
				vector4.z = 0f - perViewConfig.lastProj[0, 2];
				vector4.w = 0f - perViewConfig.lastProj[1, 2];
				vector5.x = perViewConfig.lastProj[2, 2];
				vector5.y = perViewConfig.lastProj[2, 3];
				vector5.z = perViewConfig.lastProj[3, 2];
				vector5.w = perViewConfig.lastProj[3, 3];
				vector6.x = perViewConfig.currentProj[0, 0];
				vector6.y = global::UnityEngine.Mathf.Abs(perViewConfig.currentProj[1, 1]);
				vector6.z = perViewConfig.currentProj[0, 2];
				vector6.w = perViewConfig.currentProj[1, 2];
				vector7.x = perViewConfig.currentProj[2, 2];
				vector7.y = perViewConfig.currentProj[2, 3];
				vector7.z = perViewConfig.currentProj[3, 2];
				vector7.w = perViewConfig.currentProj[3, 3];
				global::UnityEngine.Matrix4x4 matrix4x = ExtractRotation(perViewConfig.currentView) * global::UnityEngine.Matrix4x4.Translate(-perViewConfig.currentView.GetColumn(3)) * global::UnityEngine.Matrix4x4.Translate(perViewConfig.lastView.GetColumn(3)) * ExtractRotation(perViewConfig.lastView).transpose;
				global::UnityEngine.Vector4 row = matrix4x.GetRow(0);
				global::UnityEngine.Vector4 row2 = matrix4x.GetRow(1);
				global::UnityEngine.Vector4 row3 = matrix4x.GetRow(2);
				vector8.x = perViewConfig.lastLastProj[0, 0];
				vector8.y = global::UnityEngine.Mathf.Abs(perViewConfig.lastLastProj[1, 1]);
				vector8.z = perViewConfig.lastLastProj[0, 2];
				vector8.w = perViewConfig.lastLastProj[1, 2];
				vector9.x = perViewConfig.lastLastProj[2, 2];
				vector9.y = perViewConfig.lastLastProj[2, 3];
				vector9.z = perViewConfig.lastLastProj[3, 2];
				vector9.w = perViewConfig.lastLastProj[3, 3];
				global::UnityEngine.Matrix4x4 matrix4x2 = ExtractRotation(perViewConfig.lastLastView) * global::UnityEngine.Matrix4x4.Translate(-perViewConfig.lastLastView.GetColumn(3)) * global::UnityEngine.Matrix4x4.Translate(perViewConfig.lastView.GetColumn(3)) * ExtractRotation(perViewConfig.lastView).transpose;
				global::UnityEngine.Vector4 row4 = matrix4x2.GetRow(0);
				global::UnityEngine.Vector4 row5 = matrix4x2.GetRow(1);
				global::UnityEngine.Vector4 row6 = matrix4x2.GetRow(2);
				constants._StpSetupPerViewConstants[num7] = vector5.z / vector4.x;
				constants._StpSetupPerViewConstants[num7 + 1] = vector5.w / vector4.x;
				constants._StpSetupPerViewConstants[num7 + 2] = vector4.z / vector4.x;
				constants._StpSetupPerViewConstants[num7 + 3] = vector5.z / vector4.y;
				constants._StpSetupPerViewConstants[num7 + 4] = vector5.w / vector4.y;
				constants._StpSetupPerViewConstants[num7 + 5] = vector4.w / vector4.y;
				constants._StpSetupPerViewConstants[num7 + 6] = row.x * vector6.x + row3.x * vector6.z;
				constants._StpSetupPerViewConstants[num7 + 7] = row.y * vector6.x + row3.y * vector6.z;
				constants._StpSetupPerViewConstants[num7 + 8] = row.z * vector6.x + row3.z * vector6.z;
				constants._StpSetupPerViewConstants[num7 + 9] = row.w * vector6.x + row3.w * vector6.z;
				constants._StpSetupPerViewConstants[num7 + 10] = row2.x * vector6.y + row3.x * vector6.w;
				constants._StpSetupPerViewConstants[num7 + 11] = row2.y * vector6.y + row3.y * vector6.w;
				constants._StpSetupPerViewConstants[num7 + 12] = row2.z * vector6.y + row3.z * vector6.w;
				constants._StpSetupPerViewConstants[num7 + 13] = row2.w * vector6.y + row3.w * vector6.w;
				constants._StpSetupPerViewConstants[num7 + 14] = row3.x * vector7.z;
				constants._StpSetupPerViewConstants[num7 + 15] = row3.y * vector7.z;
				constants._StpSetupPerViewConstants[num7 + 16] = row3.z * vector7.z;
				constants._StpSetupPerViewConstants[num7 + 17] = row3.w * vector7.z + vector7.w;
				constants._StpSetupPerViewConstants[num7 + 18] = row4.x * vector8.x + row6.x * vector8.z;
				constants._StpSetupPerViewConstants[num7 + 19] = row4.y * vector8.x + row6.y * vector8.z;
				constants._StpSetupPerViewConstants[num7 + 20] = row4.z * vector8.x + row6.z * vector8.z;
				constants._StpSetupPerViewConstants[num7 + 21] = row4.w * vector8.x + row6.w * vector8.z;
				constants._StpSetupPerViewConstants[num7 + 22] = row5.x * vector8.y + row6.x * vector8.w;
				constants._StpSetupPerViewConstants[num7 + 23] = row5.y * vector8.y + row6.y * vector8.w;
				constants._StpSetupPerViewConstants[num7 + 24] = row5.z * vector8.y + row6.z * vector8.w;
				constants._StpSetupPerViewConstants[num7 + 25] = row5.w * vector8.y + row6.w * vector8.w;
				constants._StpSetupPerViewConstants[num7 + 26] = row6.x * vector9.z;
				constants._StpSetupPerViewConstants[num7 + 27] = row6.y * vector9.z;
				constants._StpSetupPerViewConstants[num7 + 28] = row6.z * vector9.z;
				constants._StpSetupPerViewConstants[num7 + 29] = row6.w * vector9.z + vector9.w;
				constants._StpSetupPerViewConstants[num7 + 30] = 0f;
				constants._StpSetupPerViewConstants[num7 + 31] = 0f;
			}
			constants._StpDilConstants0.x = 4f / (float)config.currentImageSize.x;
			constants._StpDilConstants0.y = 4f / (float)config.currentImageSize.y;
			constants._StpDilConstants0.z = global::System.BitConverter.Int32BitsToSingle(config.currentImageSize.x >> 2);
			constants._StpDilConstants0.w = global::System.BitConverter.Int32BitsToSingle(config.currentImageSize.y >> 2);
			constants._StpTaaConstants0.x = (float)config.currentImageSize.x / (float)config.outputImageSize.x;
			constants._StpTaaConstants0.y = (float)config.currentImageSize.y / (float)config.outputImageSize.y;
			constants._StpTaaConstants0.z = 0.5f * (float)config.currentImageSize.x / (float)config.outputImageSize.x - vector2.x;
			constants._StpTaaConstants0.w = 0.5f * (float)config.currentImageSize.y / (float)config.outputImageSize.y - vector2.y;
			constants._StpTaaConstants1.x = 1f / (float)config.currentImageSize.x;
			constants._StpTaaConstants1.y = 1f / (float)config.currentImageSize.y;
			constants._StpTaaConstants1.z = 1f / (float)config.outputImageSize.x;
			constants._StpTaaConstants1.w = 1f / (float)config.outputImageSize.y;
			constants._StpTaaConstants2.x = 0.5f / (float)config.outputImageSize.x;
			constants._StpTaaConstants2.y = 0.5f / (float)config.outputImageSize.y;
			constants._StpTaaConstants2.z = vector2.x / (float)config.currentImageSize.x - 0.5f / (float)config.currentImageSize.x;
			constants._StpTaaConstants2.w = vector2.y / (float)config.currentImageSize.y + 0.5f / (float)config.currentImageSize.y;
			constants._StpTaaConstants3.x = 0.5f / (float)config.currentImageSize.x;
			constants._StpTaaConstants3.y = 0.5f / (float)config.currentImageSize.y;
			constants._StpTaaConstants3.z = config.outputImageSize.x;
			constants._StpTaaConstants3.w = config.outputImageSize.y;
		}

		private static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle UseTexture(global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder builder, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read)
		{
			builder.UseTexture(in texture, flags);
			return texture;
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle Execute(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ref global::UnityEngine.Rendering.STP.Config config)
		{
			global::UnityEngine.Rendering.STP.RuntimeResources renderPipelineSettings = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.STP.RuntimeResources>();
			global::UnityEngine.Texture2D noiseTexture = config.noiseTexture;
			global::UnityEngine.Rendering.RTHandleStaticHelpers.SetRTHandleStaticWrapper(noiseTexture);
			global::UnityEngine.Rendering.RTHandle s_RTHandleWrapper = global::UnityEngine.Rendering.RTHandleStaticHelpers.s_RTHandleWrapper;
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo info = default(global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo);
			info.width = noiseTexture.width;
			info.height = noiseTexture.height;
			info.volumeDepth = 1;
			info.msaaSamples = 1;
			info.format = noiseTexture.graphicsFormat;
			info.bindMS = false;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture = renderGraph.ImportTexture(s_RTHandleWrapper, info);
			global::UnityEngine.Rendering.RTHandle previousHistoryTexture = config.historyContext.GetPreviousHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType.DepthMotion, config.frameIndex);
			global::UnityEngine.Rendering.RTHandle previousHistoryTexture2 = config.historyContext.GetPreviousHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType.Luma, config.frameIndex);
			global::UnityEngine.Rendering.RTHandle previousHistoryTexture3 = config.historyContext.GetPreviousHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType.Convergence, config.frameIndex);
			global::UnityEngine.Rendering.RTHandle previousHistoryTexture4 = config.historyContext.GetPreviousHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType.Feedback, config.frameIndex);
			global::UnityEngine.Rendering.RTHandle currentHistoryTexture = config.historyContext.GetCurrentHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType.DepthMotion, config.frameIndex);
			global::UnityEngine.Rendering.RTHandle currentHistoryTexture2 = config.historyContext.GetCurrentHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType.Luma, config.frameIndex);
			global::UnityEngine.Rendering.RTHandle currentHistoryTexture3 = config.historyContext.GetCurrentHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType.Convergence, config.frameIndex);
			global::UnityEngine.Rendering.RTHandle currentHistoryTexture4 = config.historyContext.GetCurrentHistoryTexture(global::UnityEngine.Rendering.STP.HistoryTextureType.Feedback, config.frameIndex);
			if (config.enableHwDrs)
			{
				currentHistoryTexture.rt.ApplyDynamicScale();
				currentHistoryTexture2.rt.ApplyDynamicScale();
				currentHistoryTexture3.rt.ApplyDynamicScale();
			}
			global::UnityEngine.Vector2Int historyTextureSize = (config.enableHwDrs ? config.outputImageSize : config.currentImageSize);
			bool flag = global::UnityEngine.SystemInfo.graphicsDeviceVendorID == kQualcommVendorId;
			global::UnityEngine.Vector2Int vector2Int = new global::UnityEngine.Vector2Int(8, flag ? 16 : 8);
			global::UnityEngine.Rendering.STP.SetupData passData;
			global::UnityEngine.Rendering.STP.SetupData setupData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IComputeRenderGraphBuilder computeRenderGraphBuilder = renderGraph.AddComputePass<global::UnityEngine.Rendering.STP.SetupData>("STP Setup", out passData, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.STP.ProfileId.StpSetup), ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\STP\\STP.cs", 1109))
			{
				passData.cs = renderPipelineSettings.setupCS;
				passData.cs.shaderKeywords = null;
				if (flag)
				{
					passData.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.EnableLargeKernel);
				}
				if (!config.enableTexArray)
				{
					passData.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.DisableTexture2DXArray);
				}
				PopulateConstantData(ref config, ref passData.constantBufferData);
				passData.noiseTexture = UseTexture(computeRenderGraphBuilder, in texture);
				if (config.debugView.IsValid())
				{
					passData.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.EnableDebugMode);
					passData.debugView = UseTexture(computeRenderGraphBuilder, in config.debugView, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				}
				passData.kernelIndex = passData.cs.FindKernel("StpSetup");
				passData.viewCount = config.numActiveViews;
				passData.dispatchSize = new global::UnityEngine.Vector2Int(global::UnityEngine.Rendering.CoreUtils.DivRoundUp(config.currentImageSize.x, vector2Int.x), global::UnityEngine.Rendering.CoreUtils.DivRoundUp(config.currentImageSize.y, vector2Int.y));
				passData.inputColor = UseTexture(computeRenderGraphBuilder, in config.inputColor);
				passData.inputDepth = UseTexture(computeRenderGraphBuilder, in config.inputDepth);
				passData.inputMotion = UseTexture(computeRenderGraphBuilder, in config.inputMotion);
				if (config.inputStencil.IsValid())
				{
					passData.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.EnableStencilResponsive);
					passData.inputStencil = UseTexture(computeRenderGraphBuilder, in config.inputStencil);
				}
				passData.intermediateColor = UseTexture(computeRenderGraphBuilder, renderGraph.CreateTexture(new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(historyTextureSize.x, historyTextureSize.y, config.enableHwDrs, config.enableTexArray)
				{
					name = "STP Intermediate Color",
					format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.A2B10G10R10_UNormPack32,
					enableRandomWrite = true
				}), global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				global::UnityEngine.Vector2Int vector2Int2 = CalculateConvergenceTextureSize(historyTextureSize);
				passData.intermediateConvergence = UseTexture(computeRenderGraphBuilder, renderGraph.CreateTexture(new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(vector2Int2.x, vector2Int2.y, config.enableHwDrs, config.enableTexArray)
				{
					name = "STP Intermediate Convergence",
					format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm,
					enableRandomWrite = true
				}), global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				passData.priorDepthMotion = UseTexture(computeRenderGraphBuilder, renderGraph.ImportTexture(previousHistoryTexture));
				passData.depthMotion = UseTexture(computeRenderGraphBuilder, renderGraph.ImportTexture(currentHistoryTexture), global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				passData.priorLuma = UseTexture(computeRenderGraphBuilder, renderGraph.ImportTexture(previousHistoryTexture2));
				passData.luma = UseTexture(computeRenderGraphBuilder, renderGraph.ImportTexture(currentHistoryTexture2), global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				passData.priorFeedback = UseTexture(computeRenderGraphBuilder, renderGraph.ImportTexture(previousHistoryTexture4));
				passData.priorConvergence = UseTexture(computeRenderGraphBuilder, renderGraph.ImportTexture(previousHistoryTexture3));
				computeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.STP.SetupData data, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext ctx)
				{
					global::UnityEngine.Rendering.ConstantBuffer.UpdateData(ctx.cmd.m_WrappedCommandBuffer, in data.constantBufferData);
					global::UnityEngine.Rendering.ConstantBuffer.Set<global::UnityEngine.Rendering.STP.StpConstantBufferData>(data.cs, global::UnityEngine.Rendering.STP.ShaderResources._StpConstantBufferData);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpBlueNoiseIn, data.noiseTexture);
					if (data.debugView.IsValid())
					{
						ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpDebugOut, data.debugView);
					}
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpInputColor, data.inputColor);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpInputDepth, data.inputDepth);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpInputMotion, data.inputMotion);
					if (data.inputStencil.IsValid())
					{
						ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpInputStencil, data.inputStencil, 0, global::UnityEngine.Rendering.RenderTextureSubElement.Stencil);
					}
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpIntermediateColor, data.intermediateColor);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpIntermediateConvergence, data.intermediateConvergence);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpPriorDepthMotion, data.priorDepthMotion);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpDepthMotion, data.depthMotion);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpPriorLuma, data.priorLuma);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpLuma, data.luma);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpPriorFeedback, data.priorFeedback);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpPriorConvergence, data.priorConvergence);
					ctx.cmd.DispatchCompute(data.cs, data.kernelIndex, data.dispatchSize.x, data.dispatchSize.y, data.viewCount);
				});
				setupData = passData;
			}
			global::UnityEngine.Rendering.STP.PreTaaData passData2;
			global::UnityEngine.Rendering.STP.PreTaaData preTaaData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IComputeRenderGraphBuilder computeRenderGraphBuilder2 = renderGraph.AddComputePass<global::UnityEngine.Rendering.STP.PreTaaData>("STP Pre-TAA", out passData2, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.STP.ProfileId.StpPreTaa), ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\STP\\STP.cs", 1212))
			{
				passData2.cs = renderPipelineSettings.preTaaCS;
				passData2.cs.shaderKeywords = null;
				if (flag)
				{
					passData2.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.EnableLargeKernel);
				}
				if (!config.enableTexArray)
				{
					passData2.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.DisableTexture2DXArray);
				}
				passData2.noiseTexture = UseTexture(computeRenderGraphBuilder2, in texture);
				if (config.debugView.IsValid())
				{
					passData2.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.EnableDebugMode);
					passData2.debugView = UseTexture(computeRenderGraphBuilder2, in config.debugView, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
				}
				passData2.kernelIndex = passData2.cs.FindKernel("StpPreTaa");
				passData2.viewCount = config.numActiveViews;
				passData2.dispatchSize = new global::UnityEngine.Vector2Int(global::UnityEngine.Rendering.CoreUtils.DivRoundUp(config.currentImageSize.x, vector2Int.x), global::UnityEngine.Rendering.CoreUtils.DivRoundUp(config.currentImageSize.y, vector2Int.y));
				passData2.intermediateConvergence = UseTexture(computeRenderGraphBuilder2, in setupData.intermediateConvergence);
				passData2.intermediateWeights = UseTexture(computeRenderGraphBuilder2, renderGraph.CreateTexture(new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(historyTextureSize.x, historyTextureSize.y, config.enableHwDrs, config.enableTexArray)
				{
					name = "STP Intermediate Weights",
					format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm,
					enableRandomWrite = true
				}), global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				passData2.luma = UseTexture(computeRenderGraphBuilder2, renderGraph.ImportTexture(currentHistoryTexture2));
				passData2.convergence = UseTexture(computeRenderGraphBuilder2, renderGraph.ImportTexture(currentHistoryTexture3), global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				computeRenderGraphBuilder2.SetRenderFunc(delegate(global::UnityEngine.Rendering.STP.PreTaaData data, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext ctx)
				{
					global::UnityEngine.Rendering.ConstantBuffer.Set<global::UnityEngine.Rendering.STP.StpConstantBufferData>(data.cs, global::UnityEngine.Rendering.STP.ShaderResources._StpConstantBufferData);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpBlueNoiseIn, data.noiseTexture);
					if (data.debugView.IsValid())
					{
						ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpDebugOut, data.debugView);
					}
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpIntermediateConvergence, data.intermediateConvergence);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpIntermediateWeights, data.intermediateWeights);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpLuma, data.luma);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpConvergence, data.convergence);
					ctx.cmd.DispatchCompute(data.cs, data.kernelIndex, data.dispatchSize.x, data.dispatchSize.y, data.viewCount);
				});
				preTaaData = passData2;
			}
			global::UnityEngine.Rendering.STP.TaaData passData3;
			global::UnityEngine.Rendering.STP.TaaData taaData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IComputeRenderGraphBuilder computeRenderGraphBuilder3 = renderGraph.AddComputePass<global::UnityEngine.Rendering.STP.TaaData>("STP TAA", out passData3, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.STP.ProfileId.StpTaa), ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\STP\\STP.cs", 1275))
			{
				passData3.cs = renderPipelineSettings.taaCS;
				passData3.cs.shaderKeywords = null;
				if (flag)
				{
					passData3.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.EnableLargeKernel);
				}
				if (!config.enableTexArray)
				{
					passData3.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.DisableTexture2DXArray);
				}
				passData3.noiseTexture = UseTexture(computeRenderGraphBuilder3, in texture);
				if (config.debugView.IsValid())
				{
					passData3.cs.EnableKeyword(global::UnityEngine.Rendering.STP.ShaderKeywords.EnableDebugMode);
					passData3.debugView = UseTexture(computeRenderGraphBuilder3, in config.debugView, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
				}
				passData3.kernelIndex = passData3.cs.FindKernel("StpTaa");
				passData3.viewCount = config.numActiveViews;
				passData3.dispatchSize = new global::UnityEngine.Vector2Int(global::UnityEngine.Rendering.CoreUtils.DivRoundUp(config.outputImageSize.x, vector2Int.x), global::UnityEngine.Rendering.CoreUtils.DivRoundUp(config.outputImageSize.y, vector2Int.y));
				passData3.intermediateColor = UseTexture(computeRenderGraphBuilder3, in setupData.intermediateColor);
				passData3.intermediateWeights = UseTexture(computeRenderGraphBuilder3, in preTaaData.intermediateWeights);
				passData3.priorFeedback = UseTexture(computeRenderGraphBuilder3, renderGraph.ImportTexture(previousHistoryTexture4));
				passData3.depthMotion = UseTexture(computeRenderGraphBuilder3, renderGraph.ImportTexture(currentHistoryTexture));
				passData3.convergence = UseTexture(computeRenderGraphBuilder3, renderGraph.ImportTexture(currentHistoryTexture3));
				passData3.feedback = UseTexture(computeRenderGraphBuilder3, renderGraph.ImportTexture(currentHistoryTexture4), global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				passData3.output = UseTexture(computeRenderGraphBuilder3, in config.destination, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				computeRenderGraphBuilder3.SetRenderFunc(delegate(global::UnityEngine.Rendering.STP.TaaData data, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext ctx)
				{
					global::UnityEngine.Rendering.ConstantBuffer.Set<global::UnityEngine.Rendering.STP.StpConstantBufferData>(data.cs, global::UnityEngine.Rendering.STP.ShaderResources._StpConstantBufferData);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpBlueNoiseIn, data.noiseTexture);
					if (data.debugView.IsValid())
					{
						ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpDebugOut, data.debugView);
					}
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpIntermediateColor, data.intermediateColor);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpIntermediateWeights, data.intermediateWeights);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpPriorFeedback, data.priorFeedback);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpDepthMotion, data.depthMotion);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpConvergence, data.convergence);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpFeedback, data.feedback);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, global::UnityEngine.Rendering.STP.ShaderResources._StpOutput, data.output);
					ctx.cmd.DispatchCompute(data.cs, data.kernelIndex, data.dispatchSize.x, data.dispatchSize.y, data.viewCount);
				});
				taaData = passData3;
			}
			return taaData.output;
		}
	}
}
