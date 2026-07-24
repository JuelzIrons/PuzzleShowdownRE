namespace UnityEngine.Rendering.Universal.Internal
{
	public class ForwardLights
	{
		private static class LightConstantBuffer
		{
			public static int _MainLightPosition;

			public static int _MainLightColor;

			public static int _MainLightOcclusionProbesChannel;

			public static int _MainLightLayerMask;

			public static int _AdditionalLightsCount;

			public static int _AdditionalLightsPosition;

			public static int _AdditionalLightsColor;

			public static int _AdditionalLightsAttenuation;

			public static int _AdditionalLightsSpotDir;

			public static int _AdditionalLightOcclusionProbeChannel;

			public static int _AdditionalLightsLayerMasks;
		}

		internal struct InitParams
		{
			public global::UnityEngine.Rendering.Universal.LightCookieManager lightCookieManager;

			public bool forwardPlus;

			internal static global::UnityEngine.Rendering.Universal.Internal.ForwardLights.InitParams Create()
			{
				global::UnityEngine.Rendering.Universal.LightCookieManager.Settings settings = global::UnityEngine.Rendering.Universal.LightCookieManager.Settings.Create();
				global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset;
				if ((bool)asset)
				{
					settings.atlas.format = asset.additionalLightsCookieFormat;
					settings.atlas.resolution = asset.additionalLightsCookieResolution;
				}
				global::UnityEngine.Rendering.Universal.Internal.ForwardLights.InitParams result = default(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.InitParams);
				result.lightCookieManager = new global::UnityEngine.Rendering.Universal.LightCookieManager(ref settings);
				result.forwardPlus = false;
				return result;
			}
		}

		private class SetupLightPassData
		{
			internal global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.Universal.UniversalLightData lightData;

			internal global::UnityEngine.Rendering.Universal.Internal.ForwardLights forwardLights;
		}

		private int m_AdditionalLightsBufferId;

		private int m_AdditionalLightsIndicesId;

		private const string k_SetupLightConstants = "Setup Light Constants";

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Setup Light Constants");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerFPSetup = new global::UnityEngine.Rendering.ProfilingSampler("Forward+ Setup");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerFPComplete = new global::UnityEngine.Rendering.ProfilingSampler("Forward+ Complete");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerFPUpload = new global::UnityEngine.Rendering.ProfilingSampler("Forward+ Upload");

		private global::UnityEngine.Rendering.Universal.MixedLightingSetup m_MixedLightingSetup;

		private global::UnityEngine.Vector4[] m_AdditionalLightPositions;

		private global::UnityEngine.Vector4[] m_AdditionalLightColors;

		private global::UnityEngine.Vector4[] m_AdditionalLightAttenuations;

		private global::UnityEngine.Vector4[] m_AdditionalLightSpotDirections;

		private global::UnityEngine.Vector4[] m_AdditionalLightOcclusionProbeChannels;

		private float[] m_AdditionalLightsLayerMasks;

		private bool m_UseStructuredBuffer;

		private bool m_UseForwardPlus;

		private int m_DirectionalLightCount;

		private int m_ActualTileWidth;

		private global::Unity.Mathematics.int2 m_TileResolution;

		private global::Unity.Jobs.JobHandle m_CullingHandle;

		private global::Unity.Collections.NativeArray<uint> m_ZBins;

		private global::UnityEngine.GraphicsBuffer m_ZBinsBuffer;

		private global::Unity.Collections.NativeArray<uint> m_TileMasks;

		private global::UnityEngine.GraphicsBuffer m_TileMasksBuffer;

		private global::UnityEngine.Rendering.Universal.LightCookieManager m_LightCookieManager;

		private global::UnityEngine.Rendering.Universal.ReflectionProbeManager m_ReflectionProbeManager;

		private int m_WordsPerTile;

		private float m_ZBinScale;

		private float m_ZBinOffset;

		private int m_LightCount;

		private int m_BinCount;

		private static global::UnityEngine.Rendering.ProfilingSampler s_SetupForwardLights = new global::UnityEngine.Rendering.ProfilingSampler("Setup Forward Lights");

		internal global::UnityEngine.Rendering.Universal.ReflectionProbeManager reflectionProbeManager => m_ReflectionProbeManager;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void Setup(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public ForwardLights()
			: this(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.InitParams.Create())
		{
		}

		internal ForwardLights(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.InitParams initParams)
		{
			m_UseStructuredBuffer = global::UnityEngine.Rendering.Universal.RenderingUtils.useStructuredBuffer;
			m_UseForwardPlus = initParams.forwardPlus;
			global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._MainLightPosition = global::UnityEngine.Shader.PropertyToID("_MainLightPosition");
			global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._MainLightColor = global::UnityEngine.Shader.PropertyToID("_MainLightColor");
			global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._MainLightOcclusionProbesChannel = global::UnityEngine.Shader.PropertyToID("_MainLightOcclusionProbes");
			global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._MainLightLayerMask = global::UnityEngine.Shader.PropertyToID("_MainLightLayerMask");
			global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsCount = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsCount");
			if (m_UseStructuredBuffer)
			{
				m_AdditionalLightsBufferId = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsBuffer");
				m_AdditionalLightsIndicesId = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsIndices");
			}
			else
			{
				global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsPosition = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsPosition");
				global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsColor = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsColor");
				global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsAttenuation = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsAttenuation");
				global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsSpotDir = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsSpotDir");
				global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightOcclusionProbeChannel = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsOcclusionProbes");
				global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsLayerMasks = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsLayerMasks");
				int maxVisibleAdditionalLights = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights;
				m_AdditionalLightPositions = new global::UnityEngine.Vector4[maxVisibleAdditionalLights];
				m_AdditionalLightColors = new global::UnityEngine.Vector4[maxVisibleAdditionalLights];
				m_AdditionalLightAttenuations = new global::UnityEngine.Vector4[maxVisibleAdditionalLights];
				m_AdditionalLightSpotDirections = new global::UnityEngine.Vector4[maxVisibleAdditionalLights];
				m_AdditionalLightOcclusionProbeChannels = new global::UnityEngine.Vector4[maxVisibleAdditionalLights];
				m_AdditionalLightsLayerMasks = new float[maxVisibleAdditionalLights];
			}
			if (m_UseForwardPlus)
			{
				CreateForwardPlusBuffers();
				m_ReflectionProbeManager = global::UnityEngine.Rendering.Universal.ReflectionProbeManager.Create();
			}
			m_LightCookieManager = initParams.lightCookieManager;
		}

		private void CreateForwardPlusBuffers()
		{
			m_ZBins = new global::Unity.Collections.NativeArray<uint>(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxZBinWords, global::Unity.Collections.Allocator.Persistent);
			m_ZBinsBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Constant, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxZBinWords / 4, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Mathematics.float4>());
			m_ZBinsBuffer.name = "URP Z-Bin Buffer";
			m_TileMasks = new global::Unity.Collections.NativeArray<uint>(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxTileWords, global::Unity.Collections.Allocator.Persistent);
			m_TileMasksBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Constant, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxTileWords / 4, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Mathematics.float4>());
			m_TileMasksBuffer.name = "URP Tile Buffer";
		}

		private static int AlignByteCount(int count, int align)
		{
			return align * ((count + align - 1) / align);
		}

		private static void GetViewParams(bool isOrthographic, global::Unity.Mathematics.float4x4 viewToClip, out float viewPlaneBot, out float viewPlaneTop, out global::Unity.Mathematics.float4 viewToViewportScaleBias)
		{
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.float2(viewToClip[0][0], viewToClip[1][1]);
			global::Unity.Mathematics.float2 float6 = global::Unity.Mathematics.math.rcp(float5);
			global::Unity.Mathematics.float2 float7 = (isOrthographic ? (-global::Unity.Mathematics.math.float2(viewToClip[3][0], viewToClip[3][1])) : global::Unity.Mathematics.math.float2(viewToClip[2][0], viewToClip[2][1]));
			viewPlaneBot = float7.y * float6.y - float6.y;
			viewPlaneTop = float7.y * float6.y + float6.y;
			viewToViewportScaleBias = global::Unity.Mathematics.math.float4(float5 * 0.5f, -float7 * 0.5f + 0.5f);
		}

		internal static global::Unity.Jobs.JobHandle ScheduleClusteringJobs(bool hasMainLight, bool supportsAdditionalLights, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> lights, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleReflectionProbe> probes, global::Unity.Collections.NativeArray<uint> zBins, global::Unity.Collections.NativeArray<uint> tileMasks, global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4x4> worldToViews, global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4x4> viewToClips, int viewCount, global::Unity.Mathematics.int2 screenResolution, float nearClipPlane, float farClipPlane, bool isOrthographic, out int localLightCount, out int directionalLightCount, out int binCount, out float zBinScale, out float zBinOffset, out global::Unity.Mathematics.int2 tileResolution, out int actualTileWidth, out int wordsPerTile)
		{
			localLightCount = (supportsAdditionalLights ? lights.Length : 0);
			int i;
			for (i = 0; i < localLightCount && lights[i].lightType == global::UnityEngine.LightType.Directional; i++)
			{
			}
			localLightCount -= i;
			if (i > 0)
			{
				directionalLightCount = i;
				if (hasMainLight)
				{
					directionalLightCount--;
				}
			}
			else
			{
				directionalLightCount = 0;
			}
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> subArray = lights.GetSubArray(i, localLightCount);
			int num = global::Unity.Mathematics.math.min(probes.Length, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleReflectionProbes);
			for (int j = 0; j < probes.Length; j++)
			{
				if (!probes[j].texture)
				{
					num--;
				}
			}
			int num2 = subArray.Length + num;
			wordsPerTile = (num2 + 31) / 32;
			actualTileWidth = 4;
			do
			{
				actualTileWidth <<= 1;
				tileResolution = (screenResolution + actualTileWidth - 1) / actualTileWidth;
			}
			while (tileResolution.x * tileResolution.y * wordsPerTile * viewCount > global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxTileWords);
			if (!isOrthographic)
			{
				zBinScale = (float)(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxZBinWords / viewCount) / ((global::Unity.Mathematics.math.log2(farClipPlane) - global::Unity.Mathematics.math.log2(nearClipPlane)) * (float)(2 + wordsPerTile));
				zBinOffset = (0f - global::Unity.Mathematics.math.log2(nearClipPlane)) * zBinScale;
				binCount = (int)(global::Unity.Mathematics.math.log2(farClipPlane) * zBinScale + zBinOffset);
			}
			else
			{
				zBinScale = (float)(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxZBinWords / viewCount) / ((farClipPlane - nearClipPlane) * (float)(2 + wordsPerTile));
				zBinOffset = (0f - nearClipPlane) * zBinScale;
				binCount = (int)(farClipPlane * zBinScale + zBinOffset);
			}
			binCount = global::System.Math.Max(binCount, 0);
			for (int k = 1; k < probes.Length; k++)
			{
				global::UnityEngine.Rendering.VisibleReflectionProbe visibleReflectionProbe = probes[k];
				int num3 = k - 1;
				while (num3 >= 0 && IsProbeGreater(probes[num3], visibleReflectionProbe))
				{
					probes[num3 + 1] = probes[num3];
					num3--;
				}
				probes[num3 + 1] = visibleReflectionProbe;
			}
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> minMaxZs = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(num2 * viewCount, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Jobs.JobHandle dependency = global::Unity.Jobs.IJobForExtensions.ScheduleParallel(new global::UnityEngine.Rendering.Universal.LightMinMaxZJob
			{
				worldToViews = worldToViews,
				lights = subArray,
				minMaxZs = minMaxZs.GetSubArray(0, localLightCount * viewCount)
			}, localLightCount * viewCount, 32, default(global::Unity.Jobs.JobHandle));
			global::UnityEngine.Rendering.URPReflectionProbeSettings settings;
			bool reflectionProbeRotation = !global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.URPReflectionProbeSettings>(out settings) || settings.UseReflectionProbeRotation;
			global::Unity.Jobs.JobHandle dependency2 = global::Unity.Jobs.IJobForExtensions.ScheduleParallel(new global::UnityEngine.Rendering.Universal.ReflectionProbeMinMaxZJob
			{
				worldToViews = worldToViews,
				reflectionProbes = probes,
				reflectionProbeRotation = reflectionProbeRotation,
				minMaxZs = minMaxZs.GetSubArray(localLightCount * viewCount, num * viewCount)
			}, num * viewCount, 32, dependency);
			int num4 = (binCount + 128 - 1) / 128;
			global::Unity.Jobs.JobHandle inputDeps = global::Unity.Jobs.IJobForExtensions.ScheduleParallel(new global::UnityEngine.Rendering.Universal.ZBinningJob
			{
				bins = zBins,
				minMaxZs = minMaxZs,
				zBinScale = zBinScale,
				zBinOffset = zBinOffset,
				binCount = binCount,
				wordsPerTile = wordsPerTile,
				lightCount = localLightCount,
				reflectionProbeCount = num,
				batchCount = num4,
				viewCount = viewCount,
				isOrthographic = isOrthographic
			}, num4 * viewCount, 1, dependency2);
			dependency2.Complete();
			GetViewParams(isOrthographic, viewToClips[0], out var viewPlaneBot, out var viewPlaneTop, out var viewToViewportScaleBias);
			GetViewParams(isOrthographic, viewToClips[1], out var viewPlaneBot2, out var viewPlaneTop2, out var viewToViewportScaleBias2);
			int num5 = AlignByteCount((1 + tileResolution.y) * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Rendering.Universal.InclusiveRange>(), 128) / global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Rendering.Universal.InclusiveRange>();
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.InclusiveRange> tileRanges = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.InclusiveRange>(num5 * num2 * viewCount, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Jobs.JobHandle dependency3 = global::Unity.Jobs.IJobForExtensions.ScheduleParallel(new global::UnityEngine.Rendering.Universal.TilingJob
			{
				lights = subArray,
				reflectionProbes = probes,
				reflectionProbeRotation = reflectionProbeRotation,
				tileRanges = tileRanges,
				itemsPerTile = num2,
				rangesPerItem = num5,
				worldToViews = worldToViews,
				tileScale = (global::Unity.Mathematics.float2)screenResolution / (float)actualTileWidth,
				tileScaleInv = (float)actualTileWidth / (global::Unity.Mathematics.float2)screenResolution,
				viewPlaneBottoms = new global::UnityEngine.Rendering.Universal.Fixed2<float>(viewPlaneBot, viewPlaneBot2),
				viewPlaneTops = new global::UnityEngine.Rendering.Universal.Fixed2<float>(viewPlaneTop, viewPlaneTop2),
				viewToViewportScaleBiases = new global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4>(viewToViewportScaleBias, viewToViewportScaleBias2),
				tileCount = tileResolution,
				near = nearClipPlane,
				isOrthographic = isOrthographic
			}, num2 * viewCount, 1, dependency2);
			global::Unity.Jobs.JobHandle inputDeps2 = global::Unity.Jobs.IJobForExtensions.ScheduleParallel(new global::UnityEngine.Rendering.Universal.TileRangeExpansionJob
			{
				tileRanges = tileRanges,
				tileMasks = tileMasks,
				rangesPerItem = num5,
				itemsPerTile = num2,
				wordsPerTile = wordsPerTile,
				tileResolution = tileResolution
			}, tileResolution.y * viewCount, 1, dependency3);
			return global::Unity.Jobs.JobHandle.CombineDependencies(minMaxZs.Dispose(inputDeps), tileRanges.Dispose(inputDeps2));
			static bool IsProbeGreater(global::UnityEngine.Rendering.VisibleReflectionProbe probe, global::UnityEngine.Rendering.VisibleReflectionProbe otherProbe)
			{
				if (otherProbe.texture != null)
				{
					if (!(probe.texture == null) && probe.importance >= otherProbe.importance)
					{
						if (probe.importance == otherProbe.importance)
						{
							return probe.bounds.extents.sqrMagnitude > otherProbe.bounds.extents.sqrMagnitude;
						}
						return false;
					}
					return true;
				}
				return false;
			}
		}

		internal unsafe void PreSetup(global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			if (!m_UseForwardPlus)
			{
				return;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(m_ProfilingSamplerFPSetup))
			{
				if (!m_CullingHandle.IsCompleted)
				{
					throw new global::System.InvalidOperationException("Forward+ jobs have not completed yet.");
				}
				if (m_TileMasks.Length != global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxTileWords)
				{
					m_ZBins.Dispose();
					m_ZBinsBuffer.Dispose();
					m_TileMasks.Dispose();
					m_TileMasksBuffer.Dispose();
					CreateForwardPlusBuffers();
				}
				else
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_ZBins), m_ZBins.Length * 4);
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_TileMasks), m_TileMasks.Length * 4);
				}
				int num = ((!cameraData.xr.enabled || !cameraData.xr.singlePassEnabled) ? 1 : 2);
				global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4x4> worldToViews = new global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4x4>(cameraData.GetViewMatrix(), cameraData.GetViewMatrix(global::Unity.Mathematics.math.min(1, num - 1)));
				global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4x4> viewToClips = new global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4x4>(cameraData.GetProjectionMatrix(), cameraData.GetProjectionMatrix(global::Unity.Mathematics.math.min(1, num - 1)));
				m_CullingHandle = ScheduleClusteringJobs(lightData.mainLightIndex != -1, lightData.supportsAdditionalLights, lightData.visibleLights, renderingData.cullResults.visibleReflectionProbes, m_ZBins, m_TileMasks, worldToViews, viewToClips, num, global::Unity.Mathematics.math.int2(cameraData.pixelWidth, cameraData.pixelHeight), cameraData.camera.nearClipPlane, cameraData.camera.farClipPlane, cameraData.camera.orthographic, out m_LightCount, out m_DirectionalLightCount, out m_BinCount, out m_ZBinScale, out m_ZBinOffset, out m_TileResolution, out m_ActualTileWidth, out m_WordsPerTile);
				global::Unity.Jobs.JobHandle.ScheduleBatchedJobs();
			}
		}

		internal void SetupRenderGraphLights(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			global::UnityEngine.Rendering.Universal.Internal.ForwardLights.SetupLightPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.Internal.ForwardLights.SetupLightPassData>(s_SetupForwardLights.name, out passData, s_SetupForwardLights, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\ForwardLights.cs", 476);
			passData.renderingData = renderingData;
			passData.cameraData = cameraData;
			passData.lightData = lightData;
			passData.forwardLights = this;
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.SetupLightPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext rgContext)
			{
				data.forwardLights.SetupLights(rgContext.cmd, data.renderingData, data.cameraData, data.lightData);
			});
		}

		internal void SetupLights(global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			int additionalLightsCount = lightData.additionalLightsCount;
			bool shadeAdditionalLightsPerVertex = lightData.shadeAdditionalLightsPerVertex;
			using (new global::UnityEngine.Rendering.ProfilingScope(m_ProfilingSampler))
			{
				if (m_UseForwardPlus)
				{
					if (lightData.reflectionProbeAtlas)
					{
						m_ReflectionProbeManager.UpdateGpuData(global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(cmd), ref renderingData.cullResults);
					}
					using (new global::UnityEngine.Rendering.ProfilingScope(m_ProfilingSamplerFPComplete))
					{
						m_CullingHandle.Complete();
					}
					using (new global::UnityEngine.Rendering.ProfilingScope(m_ProfilingSamplerFPUpload))
					{
						m_ZBinsBuffer.SetData(m_ZBins.Reinterpret<global::Unity.Mathematics.float4>(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<uint>()));
						m_TileMasksBuffer.SetData(m_TileMasks.Reinterpret<global::Unity.Mathematics.float4>(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<uint>()));
						cmd.SetGlobalConstantBuffer(m_ZBinsBuffer, "urp_ZBinBuffer", 0, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxZBinWords * 4);
						cmd.SetGlobalConstantBuffer(m_TileMasksBuffer, "urp_TileBuffer", 0, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxTileWords * 4);
					}
					cmd.SetGlobalVector("_FPParams0", global::Unity.Mathematics.math.float4(m_ZBinScale, m_ZBinOffset, m_LightCount, m_DirectionalLightCount));
					cmd.SetGlobalVector("_FPParams1", global::Unity.Mathematics.math.float4(cameraData.pixelRect.size / m_ActualTileWidth, m_TileResolution.x, m_WordsPerTile));
					cmd.SetGlobalVector("_FPParams2", global::Unity.Mathematics.math.float4(m_BinCount, m_TileResolution.x * m_TileResolution.y, 0f, 0f));
				}
				SetupShaderLightConstants(cmd, ref renderingData.cullResults, lightData);
				bool flag = (cameraData.renderer.stripAdditionalLightOffVariants && lightData.supportsAdditionalLights) || additionalLightsCount > 0;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.AdditionalLightsVertex, flag && shadeAdditionalLightsPerVertex && !m_UseForwardPlus);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.AdditionalLightsPixel, flag && !shadeAdditionalLightsPerVertex && !m_UseForwardPlus);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ClusterLightLoop, m_UseForwardPlus);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ForwardPlus, m_UseForwardPlus);
				bool flag2 = lightData.supportsMixedLighting && m_MixedLightingSetup == global::UnityEngine.Rendering.Universal.MixedLightingSetup.ShadowMask;
				bool flag3 = flag2 && global::UnityEngine.QualitySettings.shadowmaskMode == global::UnityEngine.ShadowmaskMode.Shadowmask;
				bool flag4 = lightData.supportsMixedLighting && m_MixedLightingSetup == global::UnityEngine.Rendering.Universal.MixedLightingSetup.Subtractive;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightmapShadowMixing, flag4 || flag3);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ShadowsShadowMask, flag2);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MixedLightingSubtractive, flag4);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ReflectionProbeBlending, lightData.reflectionProbeBlending);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ReflectionProbeBoxProjection, lightData.reflectionProbeBoxProjection);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ReflectionProbeAtlas, lightData.reflectionProbeAtlas && m_UseForwardPlus && lightData.reflectionProbeBlending);
				global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset;
				bool flag5 = asset != null && asset.lightProbeSystem == global::UnityEngine.Rendering.Universal.LightProbeSystem.ProbeVolumes;
				global::UnityEngine.Rendering.ProbeVolumeSHBands probeVolumeSHBands = asset.probeVolumeSHBands;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ProbeVolumeL1, flag5 && probeVolumeSHBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL1);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ProbeVolumeL2, flag5 && probeVolumeSHBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2);
				global::UnityEngine.Rendering.Universal.ShEvalMode shEvalMode = global::UnityEngine.Rendering.Universal.PlatformAutoDetect.ShAutoDetect(asset.shEvalMode);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.EVALUATE_SH_MIXED, shEvalMode == global::UnityEngine.Rendering.Universal.ShEvalMode.Mixed);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.EVALUATE_SH_VERTEX, shEvalMode == global::UnityEngine.Rendering.Universal.ShEvalMode.PerVertex);
				global::UnityEngine.Rendering.VolumeStack stack = global::UnityEngine.Rendering.VolumeManager.instance.stack;
				bool flag6 = global::UnityEngine.Rendering.ProbeReferenceVolume.instance.UpdateShaderVariablesProbeVolumes(global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(cmd), stack.GetComponent<global::UnityEngine.Rendering.ProbeVolumesOptions>(), cameraData.IsTemporalAAEnabled() ? global::UnityEngine.Time.frameCount : 0, lightData.supportsLightLayers);
				cmd.SetGlobalInt("_EnableProbeVolumes", flag6 ? 1 : 0);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightLayers, lightData.supportsLightLayers && !global::UnityEngine.Rendering.CoreUtils.IsSceneLightingDisabled(cameraData.camera));
				if (m_LightCookieManager != null)
				{
					m_LightCookieManager.Setup(global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(cmd), lightData);
				}
				else
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightCookies, value: false);
				}
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.LightmapSamplingSettings>(out var settings))
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LIGHTMAP_BICUBIC_SAMPLING, settings.useBicubicLightmapSampling);
				}
				else
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LIGHTMAP_BICUBIC_SAMPLING, value: false);
				}
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.URPReflectionProbeSettings>(out var settings2))
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ReflectionProbeRotation, settings2.UseReflectionProbeRotation);
				}
				else
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ReflectionProbeRotation, value: false);
				}
			}
		}

		internal void Cleanup()
		{
			if (m_UseForwardPlus)
			{
				m_CullingHandle.Complete();
				m_ZBins.Dispose();
				m_TileMasks.Dispose();
				m_ZBinsBuffer.Dispose();
				m_ZBinsBuffer = null;
				m_TileMasksBuffer.Dispose();
				m_TileMasksBuffer = null;
				m_ReflectionProbeManager.Dispose();
			}
			m_LightCookieManager?.Dispose();
			m_LightCookieManager = null;
		}

		private void InitializeLightConstants(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> lights, int lightIndex, bool supportsLightLayers, out global::UnityEngine.Vector4 lightPos, out global::UnityEngine.Vector4 lightColor, out global::UnityEngine.Vector4 lightAttenuation, out global::UnityEngine.Vector4 lightSpotDir, out global::UnityEngine.Vector4 lightOcclusionProbeChannel, out uint lightLayerMask, out bool isSubtractive)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.InitializeLightConstants_Common(lights, lightIndex, out lightPos, out lightColor, out lightAttenuation, out lightSpotDir, out lightOcclusionProbeChannel);
			lightLayerMask = 0u;
			isSubtractive = false;
			if (lightIndex < 0)
			{
				return;
			}
			ref global::UnityEngine.Rendering.VisibleLight reference = ref lights.UnsafeElementAtMutable(lightIndex);
			global::UnityEngine.Light light = reference.light;
			global::UnityEngine.LightBakingOutput bakingOutput = light.bakingOutput;
			isSubtractive = bakingOutput.isBaked && bakingOutput.lightmapBakeType == global::UnityEngine.LightmapBakeType.Mixed && bakingOutput.mixedLightingMode == global::UnityEngine.MixedLightingMode.Subtractive;
			if (light == null)
			{
				return;
			}
			if (bakingOutput.lightmapBakeType == global::UnityEngine.LightmapBakeType.Mixed && reference.light.shadows != global::UnityEngine.LightShadows.None && m_MixedLightingSetup == global::UnityEngine.Rendering.Universal.MixedLightingSetup.None)
			{
				switch (bakingOutput.mixedLightingMode)
				{
				case global::UnityEngine.MixedLightingMode.Subtractive:
					m_MixedLightingSetup = global::UnityEngine.Rendering.Universal.MixedLightingSetup.Subtractive;
					break;
				case global::UnityEngine.MixedLightingMode.Shadowmask:
					m_MixedLightingSetup = global::UnityEngine.Rendering.Universal.MixedLightingSetup.ShadowMask;
					break;
				}
			}
			if (supportsLightLayers)
			{
				global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData universalAdditionalLightData = light.GetUniversalAdditionalLightData();
				lightLayerMask = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.ToValidRenderingLayers(universalAdditionalLightData.renderingLayers);
			}
		}

		private void SetupShaderLightConstants(global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			m_MixedLightingSetup = global::UnityEngine.Rendering.Universal.MixedLightingSetup.None;
			SetupMainLightConstants(cmd, lightData);
			SetupAdditionalLightConstants(cmd, ref cullResults, lightData);
		}

		private void SetupMainLightConstants(global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			bool supportsLightLayers = lightData.supportsLightLayers;
			InitializeLightConstants(lightData.visibleLights, lightData.mainLightIndex, supportsLightLayers, out var lightPos, out var lightColor, out var _, out var _, out var lightOcclusionProbeChannel, out var lightLayerMask, out var isSubtractive);
			lightColor.w = (isSubtractive ? 0f : 1f);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._MainLightPosition, lightPos);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._MainLightColor, lightColor);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._MainLightOcclusionProbesChannel, lightOcclusionProbeChannel);
			if (supportsLightLayers)
			{
				cmd.SetGlobalInt(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._MainLightLayerMask, (int)lightLayerMask);
			}
		}

		private void SetupAdditionalLightConstants(global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			bool supportsLightLayers = lightData.supportsLightLayers;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = lightData.visibleLights;
			int maxVisibleAdditionalLights = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights;
			int num = SetupPerObjectLightIndices(cullResults, lightData);
			if (num > 0)
			{
				int mainLightIndex = lightData.mainLightIndex;
				if (m_UseStructuredBuffer)
				{
					global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShaderInput.LightData> data = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShaderInput.LightData>(num, global::Unity.Collections.Allocator.Temp);
					int i = 0;
					int num2 = 0;
					global::UnityEngine.Rendering.Universal.ShaderInput.LightData value = default(global::UnityEngine.Rendering.Universal.ShaderInput.LightData);
					for (; i < visibleLights.Length; i++)
					{
						if (num2 >= maxVisibleAdditionalLights)
						{
							break;
						}
						if (mainLightIndex != i)
						{
							InitializeLightConstants(visibleLights, i, supportsLightLayers, out value.position, out value.color, out value.attenuation, out value.spotDirection, out value.occlusionProbeChannels, out value.layerMask, out var _);
							data[num2] = value;
							num2++;
						}
					}
					global::UnityEngine.ComputeBuffer lightDataBuffer = global::UnityEngine.Rendering.Universal.ShaderData.instance.GetLightDataBuffer(num);
					lightDataBuffer.SetData(data);
					int lightAndReflectionProbeIndexCount = cullResults.lightAndReflectionProbeIndexCount;
					global::UnityEngine.ComputeBuffer lightIndicesBuffer = global::UnityEngine.Rendering.Universal.ShaderData.instance.GetLightIndicesBuffer(lightAndReflectionProbeIndexCount);
					cmd.SetGlobalBuffer(m_AdditionalLightsBufferId, lightDataBuffer);
					cmd.SetGlobalBuffer(m_AdditionalLightsIndicesId, lightIndicesBuffer);
					data.Dispose();
				}
				else
				{
					int j = 0;
					int num3 = 0;
					for (; j < visibleLights.Length; j++)
					{
						if (num3 >= maxVisibleAdditionalLights)
						{
							break;
						}
						if (mainLightIndex != j)
						{
							InitializeLightConstants(visibleLights, j, supportsLightLayers, out m_AdditionalLightPositions[num3], out m_AdditionalLightColors[num3], out m_AdditionalLightAttenuations[num3], out m_AdditionalLightSpotDirections[num3], out m_AdditionalLightOcclusionProbeChannels[num3], out var lightLayerMask, out var isSubtractive2);
							if (supportsLightLayers)
							{
								m_AdditionalLightsLayerMasks[num3] = global::Unity.Mathematics.math.asfloat(lightLayerMask);
							}
							m_AdditionalLightColors[num3].w = (isSubtractive2 ? 1f : 0f);
							num3++;
						}
					}
					cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsPosition, m_AdditionalLightPositions);
					cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsColor, m_AdditionalLightColors);
					cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsAttenuation, m_AdditionalLightAttenuations);
					cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsSpotDir, m_AdditionalLightSpotDirections);
					cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightOcclusionProbeChannel, m_AdditionalLightOcclusionProbeChannels);
					if (supportsLightLayers)
					{
						cmd.SetGlobalFloatArray(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsLayerMasks, m_AdditionalLightsLayerMasks);
					}
				}
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsCount, new global::UnityEngine.Vector4(lightData.maxPerObjectAdditionalLightsCount, 0f, 0f, 0f));
			}
			else
			{
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.ForwardLights.LightConstantBuffer._AdditionalLightsCount, global::UnityEngine.Vector4.zero);
			}
		}

		private int SetupPerObjectLightIndices(global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			if (lightData.additionalLightsCount == 0 || m_UseForwardPlus)
			{
				return lightData.additionalLightsCount;
			}
			global::Unity.Collections.NativeArray<int> lightIndexMap = cullResults.GetLightIndexMap(global::Unity.Collections.Allocator.Temp);
			int num = 0;
			int num2 = 0;
			int maxVisibleAdditionalLights = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights;
			int length = lightData.visibleLights.Length;
			for (int i = 0; i < length; i++)
			{
				if (num2 >= maxVisibleAdditionalLights)
				{
					break;
				}
				if (i == lightData.mainLightIndex)
				{
					lightIndexMap[i] = -1;
					num++;
					continue;
				}
				if (lightData.visibleLights[i].lightType == global::UnityEngine.LightType.Directional || lightData.visibleLights[i].lightType == global::UnityEngine.LightType.Spot || lightData.visibleLights[i].lightType == global::UnityEngine.LightType.Point)
				{
					lightIndexMap[i] -= num;
				}
				else
				{
					lightIndexMap[i] = -1;
				}
				num2++;
			}
			for (int j = num + num2; j < lightIndexMap.Length; j++)
			{
				lightIndexMap[j] = -1;
			}
			cullResults.SetLightIndexMap(lightIndexMap);
			if (m_UseStructuredBuffer && num2 > 0)
			{
				int lightAndReflectionProbeIndexCount = cullResults.lightAndReflectionProbeIndexCount;
				cullResults.FillLightAndReflectionProbeIndices(global::UnityEngine.Rendering.Universal.ShaderData.instance.GetLightIndicesBuffer(lightAndReflectionProbeIndexCount));
			}
			lightIndexMap.Dispose();
			return num2;
		}
	}
}
