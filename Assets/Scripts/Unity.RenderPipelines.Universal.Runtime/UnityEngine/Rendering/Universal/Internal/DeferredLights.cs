namespace UnityEngine.Rendering.Universal.Internal
{
	internal class DeferredLights
	{
		internal static class ShaderConstants
		{
			public static readonly int _LitStencilRef = global::UnityEngine.Shader.PropertyToID("_LitStencilRef");

			public static readonly int _LitStencilReadMask = global::UnityEngine.Shader.PropertyToID("_LitStencilReadMask");

			public static readonly int _LitStencilWriteMask = global::UnityEngine.Shader.PropertyToID("_LitStencilWriteMask");

			public static readonly int _SimpleLitStencilRef = global::UnityEngine.Shader.PropertyToID("_SimpleLitStencilRef");

			public static readonly int _SimpleLitStencilReadMask = global::UnityEngine.Shader.PropertyToID("_SimpleLitStencilReadMask");

			public static readonly int _SimpleLitStencilWriteMask = global::UnityEngine.Shader.PropertyToID("_SimpleLitStencilWriteMask");

			public static readonly int _StencilRef = global::UnityEngine.Shader.PropertyToID("_StencilRef");

			public static readonly int _StencilReadMask = global::UnityEngine.Shader.PropertyToID("_StencilReadMask");

			public static readonly int _StencilWriteMask = global::UnityEngine.Shader.PropertyToID("_StencilWriteMask");

			public static readonly int _LitPunctualStencilRef = global::UnityEngine.Shader.PropertyToID("_LitPunctualStencilRef");

			public static readonly int _LitPunctualStencilReadMask = global::UnityEngine.Shader.PropertyToID("_LitPunctualStencilReadMask");

			public static readonly int _LitPunctualStencilWriteMask = global::UnityEngine.Shader.PropertyToID("_LitPunctualStencilWriteMask");

			public static readonly int _SimpleLitPunctualStencilRef = global::UnityEngine.Shader.PropertyToID("_SimpleLitPunctualStencilRef");

			public static readonly int _SimpleLitPunctualStencilReadMask = global::UnityEngine.Shader.PropertyToID("_SimpleLitPunctualStencilReadMask");

			public static readonly int _SimpleLitPunctualStencilWriteMask = global::UnityEngine.Shader.PropertyToID("_SimpleLitPunctualStencilWriteMask");

			public static readonly int _LitDirStencilRef = global::UnityEngine.Shader.PropertyToID("_LitDirStencilRef");

			public static readonly int _LitDirStencilReadMask = global::UnityEngine.Shader.PropertyToID("_LitDirStencilReadMask");

			public static readonly int _LitDirStencilWriteMask = global::UnityEngine.Shader.PropertyToID("_LitDirStencilWriteMask");

			public static readonly int _SimpleLitDirStencilRef = global::UnityEngine.Shader.PropertyToID("_SimpleLitDirStencilRef");

			public static readonly int _SimpleLitDirStencilReadMask = global::UnityEngine.Shader.PropertyToID("_SimpleLitDirStencilReadMask");

			public static readonly int _SimpleLitDirStencilWriteMask = global::UnityEngine.Shader.PropertyToID("_SimpleLitDirStencilWriteMask");

			public static readonly int _ScreenToWorld = global::UnityEngine.Shader.PropertyToID("_ScreenToWorld");

			public static int _MainLightPosition = global::UnityEngine.Shader.PropertyToID("_MainLightPosition");

			public static int _MainLightColor = global::UnityEngine.Shader.PropertyToID("_MainLightColor");

			public static int _MainLightLayerMask = global::UnityEngine.Shader.PropertyToID("_MainLightLayerMask");

			public static int _SpotLightScale = global::UnityEngine.Shader.PropertyToID("_SpotLightScale");

			public static int _SpotLightBias = global::UnityEngine.Shader.PropertyToID("_SpotLightBias");

			public static int _SpotLightGuard = global::UnityEngine.Shader.PropertyToID("_SpotLightGuard");

			public static int _LightPosWS = global::UnityEngine.Shader.PropertyToID("_LightPosWS");

			public static int _LightColor = global::UnityEngine.Shader.PropertyToID("_LightColor");

			public static int _LightAttenuation = global::UnityEngine.Shader.PropertyToID("_LightAttenuation");

			public static int _LightOcclusionProbInfo = global::UnityEngine.Shader.PropertyToID("_LightOcclusionProbInfo");

			public static int _LightDirection = global::UnityEngine.Shader.PropertyToID("_LightDirection");

			public static int _LightFlags = global::UnityEngine.Shader.PropertyToID("_LightFlags");

			public static int _ShadowLightIndex = global::UnityEngine.Shader.PropertyToID("_ShadowLightIndex");

			public static int _LightLayerMask = global::UnityEngine.Shader.PropertyToID("_LightLayerMask");

			public static int _CookieLightIndex = global::UnityEngine.Shader.PropertyToID("_CookieLightIndex");
		}

		internal enum StencilDeferredPasses
		{
			StencilVolume = 0,
			PunctualLit = 1,
			PunctualSimpleLit = 2,
			DirectionalLit = 3,
			DirectionalSimpleLit = 4,
			Fog = 5,
			SSAOOnly = 6
		}

		internal enum ClusterDeferredPasses
		{
			ClusteredLightsLit = 0,
			ClusteredLightsSimpleLit = 1,
			Fog = 2
		}

		internal struct InitParams
		{
			public global::UnityEngine.Material stencilDeferredMaterial;

			public global::UnityEngine.Material clusterDeferredMaterial;

			public global::UnityEngine.Rendering.Universal.LightCookieManager lightCookieManager;

			public bool deferredPlus;
		}

		private class SetupLightPassData
		{
			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.Universal.UniversalLightData lightData;

			internal global::UnityEngine.Rendering.Universal.Internal.DeferredLights deferredLights;

			internal global::UnityEngine.Vector2Int cameraTargetSizeCopy;
		}

		internal static readonly string[] k_GBufferNames = new string[7] { "_GBuffer0", "_GBuffer1", "_GBuffer2", "_GBuffer3", "_GBuffer4", "_GBuffer5", "_GBuffer6" };

		internal static readonly int[] k_GBufferShaderPropertyIDs = new int[7]
		{
			global::UnityEngine.Shader.PropertyToID(k_GBufferNames[0]),
			global::UnityEngine.Shader.PropertyToID(k_GBufferNames[1]),
			global::UnityEngine.Shader.PropertyToID(k_GBufferNames[2]),
			global::UnityEngine.Shader.PropertyToID(k_GBufferNames[3]),
			global::UnityEngine.Shader.PropertyToID(k_GBufferNames[4]),
			global::UnityEngine.Shader.PropertyToID(k_GBufferNames[5]),
			global::UnityEngine.Shader.PropertyToID(k_GBufferNames[6])
		};

		private static readonly string[] k_StencilDeferredPassNames = new string[7] { "Stencil Volume", "Deferred Punctual Light (Lit)", "Deferred Punctual Light (SimpleLit)", "Deferred Directional Light (Lit)", "Deferred Directional Light (SimpleLit)", "Fog", "SSAOOnly" };

		private static readonly string[] k_ClusterDeferredPassNames = new string[3] { "Deferred Clustered Lights (Lit)", "Deferred Clustered Lights (SimpleLit)", "Fog" };

		private static readonly ushort k_InvalidLightOffset = ushort.MaxValue;

		private static readonly string k_SetupLights = "SetupLights";

		private static readonly string k_DeferredPass = "Deferred Pass";

		private static readonly string k_DeferredShadingPass = "Deferred Shading";

		private static readonly string k_DeferredStencilPass = "Deferred Shading (Stencil)";

		private static readonly string k_DeferredFogPass = "Deferred Fog";

		private static readonly string k_SetupLightConstants = "Setup Light Constants";

		private static readonly float kStencilShapeGuard = 1.06067f;

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSetupLights = new global::UnityEngine.Rendering.ProfilingSampler(k_SetupLights);

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingDeferredPass = new global::UnityEngine.Rendering.ProfilingSampler(k_DeferredPass);

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSetupLightConstants = new global::UnityEngine.Rendering.ProfilingSampler(k_SetupLightConstants);

		private global::UnityEngine.Rendering.RTHandle[] GbufferRTHandles;

		private global::Unity.Collections.NativeArray<ushort> m_stencilVisLights;

		private global::Unity.Collections.NativeArray<ushort> m_stencilVisLightOffsets;

		private global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass m_AdditionalLightsShadowCasterPass;

		private global::UnityEngine.Mesh m_SphereMesh;

		private global::UnityEngine.Mesh m_HemisphereMesh;

		private global::UnityEngine.Mesh m_FullscreenMesh;

		private global::UnityEngine.Material m_StencilDeferredMaterial;

		private global::UnityEngine.Material m_ClusterDeferredMaterial;

		private int[] m_StencilDeferredPasses;

		private int[] m_ClusterDeferredPasses;

		private global::UnityEngine.Matrix4x4[] m_ScreenToWorld = new global::UnityEngine.Matrix4x4[2];

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerDeferredShadingPass = new global::UnityEngine.Rendering.ProfilingSampler(k_DeferredShadingPass);

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerDeferredStencilPass = new global::UnityEngine.Rendering.ProfilingSampler(k_DeferredStencilPass);

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerDeferredFogPass = new global::UnityEngine.Rendering.ProfilingSampler(k_DeferredFogPass);

		private global::UnityEngine.Rendering.Universal.LightCookieManager m_LightCookieManager;

		private bool m_UseDeferredPlus;

		private static global::UnityEngine.Rendering.ProfilingSampler s_SetupDeferredLights = new global::UnityEngine.Rendering.ProfilingSampler("Setup Deferred lights");

		internal int GBufferAlbedoIndex => 0;

		internal int GBufferSpecularMetallicIndex => 1;

		internal int GBufferNormalSmoothnessIndex => 2;

		internal int GBufferLightingIndex => 3;

		internal int GbufferDepthIndex
		{
			get
			{
				if (!UseFramebufferFetch)
				{
					return -1;
				}
				return GBufferLightingIndex + 1;
			}
		}

		internal int GBufferRenderingLayers
		{
			get
			{
				if (!UseRenderingLayers)
				{
					return -1;
				}
				return GBufferLightingIndex + (UseFramebufferFetch ? 1 : 0) + 1;
			}
		}

		internal int GBufferShadowMask
		{
			get
			{
				if (!UseShadowMask)
				{
					return -1;
				}
				return GBufferLightingIndex + (UseFramebufferFetch ? 1 : 0) + (UseRenderingLayers ? 1 : 0) + 1;
			}
		}

		internal int GBufferSliceCount => 4 + (UseFramebufferFetch ? 1 : 0) + (UseShadowMask ? 1 : 0) + (UseRenderingLayers ? 1 : 0);

		internal int GBufferInputAttachmentCount => 4 + (UseShadowMask ? 1 : 0);

		internal bool UseShadowMask => MixedLightingSetup != global::UnityEngine.Rendering.Universal.MixedLightingSetup.None;

		internal bool UseRenderingLayers
		{
			get
			{
				if (!UseLightLayers)
				{
					return UseDecalLayers;
				}
				return true;
			}
		}

		internal global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize RenderingLayerMaskSize { get; set; }

		internal bool UseDecalLayers { get; set; }

		internal bool UseLightLayers => global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.useRenderingLayers;

		internal bool UseFramebufferFetch { get; set; }

		internal bool HasDepthPrepass { get; set; }

		internal bool HasNormalPrepass { get; set; }

		internal bool HasRenderingLayerPrepass { get; set; }

		internal bool AccurateGbufferNormals { get; set; }

		internal global::UnityEngine.Rendering.Universal.MixedLightingSetup MixedLightingSetup { get; set; }

		internal bool UseJobSystem { get; set; }

		internal int RenderWidth { get; set; }

		internal int RenderHeight { get; set; }

		internal global::UnityEngine.Rendering.RTHandle[] GbufferAttachments { get; set; }

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] GbufferTextureHandles { get; set; }

		internal global::UnityEngine.Rendering.RTHandle[] DeferredInputAttachments { get; set; }

		internal bool[] DeferredInputIsTransient { get; set; }

		internal global::UnityEngine.Rendering.RTHandle DepthAttachment { get; set; }

		internal global::UnityEngine.Rendering.RTHandle DepthCopyTexture { get; set; }

		internal global::UnityEngine.Experimental.Rendering.GraphicsFormat[] GbufferFormats { get; set; }

		internal global::UnityEngine.Rendering.RTHandle DepthAttachmentHandle { get; set; }

		internal global::UnityEngine.Experimental.Rendering.GraphicsFormat GetGBufferFormat(int index)
		{
			if (index == GBufferAlbedoIndex)
			{
				if (global::UnityEngine.QualitySettings.activeColorSpace != global::UnityEngine.ColorSpace.Linear)
				{
					return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
				}
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB;
			}
			if (index == GBufferSpecularMetallicIndex)
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
			}
			if (index == GBufferNormalSmoothnessIndex)
			{
				if (!AccurateGbufferNormals)
				{
					return global::UnityEngine.Rendering.Universal.Internal.DepthNormalOnlyPass.GetGraphicsFormat();
				}
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
			}
			if (index == GBufferLightingIndex)
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			}
			if (index == GbufferDepthIndex)
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat;
			}
			if (index == GBufferShadowMask)
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.B8G8R8A8_UNorm;
			}
			if (index == GBufferRenderingLayers)
			{
				return global::UnityEngine.Rendering.Universal.RenderingLayerUtils.GetFormat(RenderingLayerMaskSize);
			}
			return global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
		}

		internal DeferredLights(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.InitParams initParams, bool useNativeRenderPass = false)
		{
			global::UnityEngine.Rendering.Universal.Internal.DeferredConfig.IsOpenGL = global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore || global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3;
			global::UnityEngine.Rendering.Universal.Internal.DeferredConfig.IsDX10 = global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.Direct3D11 && global::UnityEngine.SystemInfo.graphicsShaderLevel <= 40;
			m_StencilDeferredMaterial = initParams.stencilDeferredMaterial;
			m_ClusterDeferredMaterial = initParams.clusterDeferredMaterial;
			if (initParams.deferredPlus)
			{
				m_ClusterDeferredPasses = new int[k_ClusterDeferredPassNames.Length];
				InitClusterDeferredMaterial();
			}
			else
			{
				m_StencilDeferredPasses = new int[k_StencilDeferredPassNames.Length];
				InitStencilDeferredMaterial();
			}
			AccurateGbufferNormals = true;
			UseJobSystem = true;
			UseFramebufferFetch = useNativeRenderPass;
			m_LightCookieManager = initParams.lightCookieManager;
			m_UseDeferredPlus = initParams.deferredPlus;
		}

		internal void SetupRenderGraphLights(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			global::UnityEngine.Rendering.Universal.Internal.DeferredLights.SetupLightPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.Internal.DeferredLights.SetupLightPassData>(s_SetupDeferredLights.name, out passData, s_SetupDeferredLights, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\DeferredLights.cs", 313);
			passData.cameraData = cameraData;
			passData.cameraTargetSizeCopy = new global::UnityEngine.Vector2Int(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
			passData.lightData = lightData;
			passData.deferredLights = this;
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.SetupLightPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext rgContext)
			{
				data.deferredLights.SetupLights(global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(rgContext.cmd), data.cameraData, data.cameraTargetSizeCopy, data.lightData, isRenderGraph: true);
			});
		}

		internal void SetupLights(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Vector2Int cameraTargetSizeCopy, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, bool isRenderGraph = false)
		{
			global::UnityEngine.Camera camera = cameraData.camera;
			RenderWidth = (camera.allowDynamicResolution ? global::UnityEngine.Mathf.CeilToInt(global::UnityEngine.ScalableBufferManager.widthScaleFactor * (float)cameraTargetSizeCopy.x) : cameraTargetSizeCopy.x);
			RenderHeight = (camera.allowDynamicResolution ? global::UnityEngine.Mathf.CeilToInt(global::UnityEngine.ScalableBufferManager.heightScaleFactor * (float)cameraTargetSizeCopy.y) : cameraTargetSizeCopy.y);
			if (!m_UseDeferredPlus)
			{
				PrecomputeLights(out m_stencilVisLights, out m_stencilVisLightOffsets, ref lightData.visibleLights, lightData.additionalLightsCount != 0 || lightData.mainLightIndex >= 0);
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, m_ProfilingSetupLightConstants))
			{
				if (!m_UseDeferredPlus)
				{
					SetupShaderLightConstants(cmd, lightData);
				}
				bool supportsMixedLighting = lightData.supportsMixedLighting;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._GBUFFER_NORMALS_OCT, AccurateGbufferNormals);
				bool flag = supportsMixedLighting && MixedLightingSetup == global::UnityEngine.Rendering.Universal.MixedLightingSetup.ShadowMask;
				bool flag2 = flag && global::UnityEngine.QualitySettings.shadowmaskMode == global::UnityEngine.ShadowmaskMode.Shadowmask;
				bool flag3 = supportsMixedLighting && MixedLightingSetup == global::UnityEngine.Rendering.Universal.MixedLightingSetup.Subtractive;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightmapShadowMixing, flag3 || flag2);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ShadowsShadowMask, flag);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MixedLightingSubtractive, flag3);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.RenderPassEnabled, UseFramebufferFetch && (cameraData.cameraType == global::UnityEngine.CameraType.Game || camera.cameraType == global::UnityEngine.CameraType.SceneView || isRenderGraph));
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightLayers, UseLightLayers && !global::UnityEngine.Rendering.CoreUtils.IsSceneLightingDisabled(camera));
				global::UnityEngine.Rendering.Universal.RenderingLayerUtils.SetupProperties(cmd, RenderingLayerMaskSize);
			}
		}

		internal void ResolveMixedLightingMode(global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			MixedLightingSetup = global::UnityEngine.Rendering.Universal.MixedLightingSetup.None;
			if (!lightData.supportsMixedLighting)
			{
				return;
			}
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = lightData.visibleLights;
			for (int i = 0; i < lightData.visibleLights.Length; i++)
			{
				if (MixedLightingSetup != global::UnityEngine.Rendering.Universal.MixedLightingSetup.None)
				{
					break;
				}
				global::UnityEngine.Light light = visibleLights.UnsafeElementAt(i).light;
				if (light != null && light.bakingOutput.lightmapBakeType == global::UnityEngine.LightmapBakeType.Mixed && light.shadows != global::UnityEngine.LightShadows.None)
				{
					switch (light.bakingOutput.mixedLightingMode)
					{
					case global::UnityEngine.MixedLightingMode.Subtractive:
						MixedLightingSetup = global::UnityEngine.Rendering.Universal.MixedLightingSetup.Subtractive;
						break;
					case global::UnityEngine.MixedLightingMode.Shadowmask:
						MixedLightingSetup = global::UnityEngine.Rendering.Universal.MixedLightingSetup.ShadowMask;
						break;
					}
				}
			}
		}

		internal void DisableFramebufferFetchInput()
		{
			UseFramebufferFetch = false;
			CreateGbufferResources();
		}

		internal void ReleaseGbufferResources()
		{
			if (GbufferRTHandles == null)
			{
				return;
			}
			for (int i = 0; i < GbufferRTHandles.Length; i++)
			{
				if (i != GBufferLightingIndex)
				{
					GbufferRTHandles[i].Release();
					GbufferAttachments[i].Release();
				}
			}
		}

		internal void ReAllocateGBufferIfNeeded(global::UnityEngine.RenderTextureDescriptor gbufferSlice, int gbufferIndex)
		{
			if (GbufferRTHandles != null && GbufferRTHandles[gbufferIndex].GetInstanceID() == GbufferAttachments[gbufferIndex].GetInstanceID())
			{
				gbufferSlice.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				gbufferSlice.stencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				gbufferSlice.graphicsFormat = GetGBufferFormat(gbufferIndex);
				global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref GbufferRTHandles[gbufferIndex], in gbufferSlice, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Clamp, 1, 0f, k_GBufferNames[gbufferIndex]);
				GbufferAttachments[gbufferIndex] = GbufferRTHandles[gbufferIndex];
			}
		}

		internal void CreateGbufferResources()
		{
			int gBufferSliceCount = GBufferSliceCount;
			if (GbufferRTHandles == null || GbufferRTHandles.Length != gBufferSliceCount)
			{
				ReleaseGbufferResources();
				GbufferAttachments = new global::UnityEngine.Rendering.RTHandle[gBufferSliceCount];
				GbufferRTHandles = new global::UnityEngine.Rendering.RTHandle[gBufferSliceCount];
				GbufferFormats = new global::UnityEngine.Experimental.Rendering.GraphicsFormat[gBufferSliceCount];
				GbufferTextureHandles = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[gBufferSliceCount];
				for (int i = 0; i < gBufferSliceCount; i++)
				{
					GbufferRTHandles[i] = global::UnityEngine.Rendering.RTHandles.Alloc(k_GBufferNames[i], k_GBufferNames[i]);
					GbufferAttachments[i] = GbufferRTHandles[i];
					GbufferFormats[i] = GetGBufferFormat(i);
				}
			}
		}

		internal void CreateGbufferResourcesRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData)
		{
			int gBufferSliceCount = GBufferSliceCount;
			if (GbufferTextureHandles == null || GbufferTextureHandles.Length != gBufferSliceCount)
			{
				GbufferFormats = new global::UnityEngine.Experimental.Rendering.GraphicsFormat[gBufferSliceCount];
				GbufferTextureHandles = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[gBufferSliceCount];
			}
			bool flag = UseRenderingLayers && !UseLightLayers;
			for (int i = 0; i < gBufferSliceCount; i++)
			{
				GbufferFormats[i] = GetGBufferFormat(i);
				if (i == GBufferNormalSmoothnessIndex && HasNormalPrepass)
				{
					GbufferTextureHandles[i] = resourceData.cameraNormalsTexture;
				}
				else if (i == GBufferRenderingLayers && flag)
				{
					GbufferTextureHandles[i] = resourceData.renderingLayersTexture;
				}
				else if (i != GBufferLightingIndex)
				{
					global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = resourceData.cameraColor.GetDescriptor(renderGraph);
					desc.format = GetGBufferFormat(i);
					desc.name = k_GBufferNames[i];
					desc.clearBuffer = true;
					GbufferTextureHandles[i] = renderGraph.CreateTexture(in desc);
				}
				else
				{
					GbufferTextureHandles[i] = resourceData.cameraColor;
				}
			}
		}

		internal void UpdateDeferredInputAttachments()
		{
			DeferredInputAttachments[0] = GbufferAttachments[0];
			DeferredInputAttachments[1] = GbufferAttachments[1];
			DeferredInputAttachments[2] = GbufferAttachments[2];
			DeferredInputAttachments[3] = GbufferAttachments[4];
			if (UseShadowMask && UseRenderingLayers)
			{
				DeferredInputAttachments[4] = GbufferAttachments[GBufferShadowMask];
				DeferredInputAttachments[5] = GbufferAttachments[GBufferRenderingLayers];
			}
			else if (UseShadowMask)
			{
				DeferredInputAttachments[4] = GbufferAttachments[GBufferShadowMask];
			}
			else if (UseRenderingLayers)
			{
				DeferredInputAttachments[4] = GbufferAttachments[GBufferRenderingLayers];
			}
		}

		internal bool IsRuntimeSupportedThisFrame()
		{
			if (GBufferSliceCount <= global::UnityEngine.SystemInfo.supportedRenderTargetCount && !global::UnityEngine.Rendering.Universal.Internal.DeferredConfig.IsOpenGL)
			{
				return !global::UnityEngine.Rendering.Universal.Internal.DeferredConfig.IsDX10;
			}
			return false;
		}

		public void Setup(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass additionalLightsShadowCasterPass, bool hasDepthPrepass, bool hasNormalPrepass, bool hasRenderingLayerPrepass, global::UnityEngine.Rendering.RTHandle depthCopyTexture, global::UnityEngine.Rendering.RTHandle depthAttachment, global::UnityEngine.Rendering.RTHandle colorAttachment)
		{
			m_AdditionalLightsShadowCasterPass = additionalLightsShadowCasterPass;
			HasDepthPrepass = hasDepthPrepass;
			HasNormalPrepass = hasNormalPrepass;
			HasRenderingLayerPrepass = hasRenderingLayerPrepass;
			DepthCopyTexture = depthCopyTexture;
			GbufferAttachments[GBufferLightingIndex] = colorAttachment;
			DepthAttachment = depthAttachment;
			int num = 4 + (UseShadowMask ? 1 : 0) + (UseRenderingLayers ? 1 : 0);
			if ((DeferredInputAttachments == null && UseFramebufferFetch && GbufferAttachments.Length >= 3) || (DeferredInputAttachments != null && num != DeferredInputAttachments.Length))
			{
				DeferredInputAttachments = new global::UnityEngine.Rendering.RTHandle[num];
				DeferredInputIsTransient = new bool[num];
				int num2 = 0;
				int num3 = 0;
				while (num3 < num)
				{
					if (num2 == GBufferLightingIndex)
					{
						num2++;
					}
					DeferredInputAttachments[num3] = GbufferAttachments[num2];
					DeferredInputIsTransient[num3] = num2 != GbufferDepthIndex;
					num3++;
					num2++;
				}
			}
			DepthAttachmentHandle = DepthAttachment;
		}

		internal void Setup(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass additionalLightsShadowCasterPass)
		{
			m_AdditionalLightsShadowCasterPass = additionalLightsShadowCasterPass;
		}

		public void OnCameraCleanup(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._GBUFFER_NORMALS_OCT, value: false);
			if (m_stencilVisLights.IsCreated)
			{
				m_stencilVisLights.Dispose();
			}
			if (m_stencilVisLightOffsets.IsCreated)
			{
				m_stencilVisLightOffsets.Dispose();
			}
		}

		internal static global::UnityEngine.Rendering.StencilState OverwriteStencil(global::UnityEngine.Rendering.StencilState s, int stencilWriteMask)
		{
			if (!s.enabled)
			{
				return new global::UnityEngine.Rendering.StencilState(enabled: true, 0, (byte)stencilWriteMask, global::UnityEngine.Rendering.CompareFunction.Always, global::UnityEngine.Rendering.StencilOp.Replace, global::UnityEngine.Rendering.StencilOp.Keep, global::UnityEngine.Rendering.StencilOp.Keep, global::UnityEngine.Rendering.CompareFunction.Always, global::UnityEngine.Rendering.StencilOp.Replace, global::UnityEngine.Rendering.StencilOp.Keep, global::UnityEngine.Rendering.StencilOp.Keep);
			}
			global::UnityEngine.Rendering.CompareFunction compareFunctionFront = ((s.compareFunctionFront != global::UnityEngine.Rendering.CompareFunction.Disabled) ? s.compareFunctionFront : global::UnityEngine.Rendering.CompareFunction.Always);
			global::UnityEngine.Rendering.CompareFunction compareFunctionBack = ((s.compareFunctionBack != global::UnityEngine.Rendering.CompareFunction.Disabled) ? s.compareFunctionBack : global::UnityEngine.Rendering.CompareFunction.Always);
			global::UnityEngine.Rendering.StencilOp passOperationFront = s.passOperationFront;
			global::UnityEngine.Rendering.StencilOp failOperationFront = s.failOperationFront;
			global::UnityEngine.Rendering.StencilOp zFailOperationFront = s.zFailOperationFront;
			global::UnityEngine.Rendering.StencilOp passOperationBack = s.passOperationBack;
			global::UnityEngine.Rendering.StencilOp failOperationBack = s.failOperationBack;
			global::UnityEngine.Rendering.StencilOp zFailOperationBack = s.zFailOperationBack;
			return new global::UnityEngine.Rendering.StencilState(enabled: true, (byte)(s.readMask & 0xF), (byte)(s.writeMask | stencilWriteMask), compareFunctionFront, passOperationFront, failOperationFront, zFailOperationFront, compareFunctionBack, passOperationBack, failOperationBack, zFailOperationBack);
		}

		internal static global::UnityEngine.Rendering.RenderStateBlock OverwriteStencil(global::UnityEngine.Rendering.RenderStateBlock block, int stencilWriteMask, int stencilRef)
		{
			if (!block.stencilState.enabled)
			{
				block.stencilState = new global::UnityEngine.Rendering.StencilState(enabled: true, 0, (byte)stencilWriteMask, global::UnityEngine.Rendering.CompareFunction.Always, global::UnityEngine.Rendering.StencilOp.Replace, global::UnityEngine.Rendering.StencilOp.Keep, global::UnityEngine.Rendering.StencilOp.Keep, global::UnityEngine.Rendering.CompareFunction.Always, global::UnityEngine.Rendering.StencilOp.Replace, global::UnityEngine.Rendering.StencilOp.Keep, global::UnityEngine.Rendering.StencilOp.Keep);
			}
			else
			{
				global::UnityEngine.Rendering.StencilState stencilState = block.stencilState;
				global::UnityEngine.Rendering.CompareFunction compareFunctionFront = ((stencilState.compareFunctionFront != global::UnityEngine.Rendering.CompareFunction.Disabled) ? stencilState.compareFunctionFront : global::UnityEngine.Rendering.CompareFunction.Always);
				global::UnityEngine.Rendering.CompareFunction compareFunctionBack = ((stencilState.compareFunctionBack != global::UnityEngine.Rendering.CompareFunction.Disabled) ? stencilState.compareFunctionBack : global::UnityEngine.Rendering.CompareFunction.Always);
				global::UnityEngine.Rendering.StencilOp passOperationFront = stencilState.passOperationFront;
				global::UnityEngine.Rendering.StencilOp failOperationFront = stencilState.failOperationFront;
				global::UnityEngine.Rendering.StencilOp zFailOperationFront = stencilState.zFailOperationFront;
				global::UnityEngine.Rendering.StencilOp passOperationBack = stencilState.passOperationBack;
				global::UnityEngine.Rendering.StencilOp failOperationBack = stencilState.failOperationBack;
				global::UnityEngine.Rendering.StencilOp zFailOperationBack = stencilState.zFailOperationBack;
				block.stencilState = new global::UnityEngine.Rendering.StencilState(enabled: true, (byte)(stencilState.readMask & 0xF), (byte)(stencilState.writeMask | stencilWriteMask), compareFunctionFront, passOperationFront, failOperationFront, zFailOperationFront, compareFunctionBack, passOperationBack, failOperationBack, zFailOperationBack);
			}
			block.mask |= global::UnityEngine.Rendering.RenderStateMask.Stencil;
			block.stencilReference = (block.stencilReference & 0xF) | stencilRef;
			return block;
		}

		internal void ExecuteDeferredPass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			if (m_UseDeferredPlus)
			{
				if (m_ClusterDeferredPasses[0] < 0)
				{
					InitClusterDeferredMaterial();
				}
			}
			else if (m_StencilDeferredPasses[0] < 0)
			{
				InitStencilDeferredMaterial();
			}
			if (!UseFramebufferFetch)
			{
				global::UnityEngine.Material material = (m_UseDeferredPlus ? m_ClusterDeferredMaterial : m_StencilDeferredMaterial);
				for (int i = 0; i < GbufferRTHandles.Length; i++)
				{
					if (i != GBufferLightingIndex)
					{
						material.SetTexture(k_GBufferShaderPropertyIDs[i], GbufferRTHandles[i]);
					}
				}
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, m_ProfilingDeferredPass))
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._DEFERRED_MIXED_LIGHTING, UseShadowMask);
				SetupMatrixConstants(cmd, cameraData);
				if (!m_UseDeferredPlus && !HasStencilLightsOfType(global::UnityEngine.LightType.Directional))
				{
					RenderSSAOBeforeShading(cmd);
				}
				if (m_UseDeferredPlus)
				{
					RenderClusterLights(cmd, shadowData);
				}
				else
				{
					RenderStencilLights(cmd, lightData, shadowData, cameraData.renderer.stripShadowsOffVariants);
				}
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._DEFERRED_MIXED_LIGHTING, value: false);
				RenderFog(cmd, cameraData.camera.orthographic);
			}
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.AdditionalLightShadows, shadowData.isKeywordAdditionalLightShadowsEnabled);
			global::UnityEngine.Rendering.Universal.ShadowUtils.SetSoftShadowQualityShaderKeywords(cmd, shadowData);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightCookies, m_LightCookieManager != null && m_LightCookieManager.IsKeywordLightCookieEnabled);
		}

		private void SetupShaderLightConstants(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			SetupMainLightConstants(cmd, lightData);
		}

		private void SetupMainLightConstants(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			if (lightData.mainLightIndex >= 0)
			{
				global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.InitializeLightConstants_Common(lightData.visibleLights, lightData.mainLightIndex, out var lightPos, out var lightColor, out var _, out var _, out var _);
				if (lightData.supportsLightLayers)
				{
					global::UnityEngine.Light light = lightData.visibleLights[lightData.mainLightIndex].light;
					SetRenderingLayersMask(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd), light, global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._MainLightLayerMask);
				}
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._MainLightPosition, lightPos);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._MainLightColor, lightColor);
			}
		}

		internal global::UnityEngine.Matrix4x4[] GetScreenToWorldMatrix(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			int num = ((!cameraData.xr.enabled || !cameraData.xr.singlePassEnabled) ? 1 : 2);
			global::UnityEngine.Matrix4x4[] screenToWorld = m_ScreenToWorld;
			global::UnityEngine.Matrix4x4 matrix4x = new global::UnityEngine.Matrix4x4(new global::UnityEngine.Vector4(2f / (float)RenderWidth, 0f, 0f, 0f), new global::UnityEngine.Vector4(0f, 2f / (float)RenderHeight, 0f, 0f), new global::UnityEngine.Vector4(0f, 0f, 1f, 0f), new global::UnityEngine.Vector4(-1f, -1f, 0f, 1f));
			if (global::UnityEngine.Rendering.Universal.Internal.DeferredConfig.IsOpenGL)
			{
				matrix4x = new global::UnityEngine.Matrix4x4(new global::UnityEngine.Vector4(1f, 0f, 0f, 0f), new global::UnityEngine.Vector4(0f, 1f, 0f, 0f), new global::UnityEngine.Vector4(0f, 0f, 2f, 0f), new global::UnityEngine.Vector4(0f, 0f, -1f, 1f)) * matrix4x;
			}
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.Matrix4x4 viewMatrix = cameraData.GetViewMatrix(i);
				global::UnityEngine.Matrix4x4 gPUProjectionMatrix = cameraData.GetGPUProjectionMatrix(renderIntoTexture: false, i);
				screenToWorld[i] = global::UnityEngine.Matrix4x4.Inverse(gPUProjectionMatrix * viewMatrix) * matrix4x;
			}
			return screenToWorld;
		}

		private void SetupMatrixConstants(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			cmd.SetGlobalMatrixArray(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._ScreenToWorld, GetScreenToWorldMatrix(cameraData));
		}

		private void PrecomputeLights(out global::Unity.Collections.NativeArray<ushort> stencilVisLights, out global::Unity.Collections.NativeArray<ushort> stencilVisLightOffsets, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights, bool hasAdditionalLights)
		{
			if (!hasAdditionalLights)
			{
				stencilVisLights = new global::Unity.Collections.NativeArray<ushort>(0, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				stencilVisLightOffsets = new global::Unity.Collections.NativeArray<ushort>(8, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				for (int i = 0; i < 8; i++)
				{
					stencilVisLightOffsets[i] = k_InvalidLightOffset;
				}
				return;
			}
			global::Unity.Collections.NativeArray<int> nativeArray = new global::Unity.Collections.NativeArray<int>(8, global::Unity.Collections.Allocator.Temp);
			stencilVisLightOffsets = new global::Unity.Collections.NativeArray<ushort>(8, global::Unity.Collections.Allocator.Temp);
			int length = visibleLights.Length;
			for (ushort num = 0; num < length; num++)
			{
				int lightType = (int)visibleLights.UnsafeElementAtMutable(num).lightType;
				ushort value = (ushort)(stencilVisLightOffsets[lightType] + 1);
				stencilVisLightOffsets[lightType] = value;
			}
			int length2 = stencilVisLightOffsets[0] + stencilVisLightOffsets[1] + stencilVisLightOffsets[2];
			stencilVisLights = new global::Unity.Collections.NativeArray<ushort>(length2, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int j = 0;
			int num2 = 0;
			for (; j < stencilVisLightOffsets.Length; j++)
			{
				if (stencilVisLightOffsets[j] == 0)
				{
					stencilVisLightOffsets[j] = k_InvalidLightOffset;
					continue;
				}
				int num3 = stencilVisLightOffsets[j];
				stencilVisLightOffsets[j] = (ushort)num2;
				num2 += num3;
			}
			for (ushort num4 = 0; num4 < length; num4++)
			{
				ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAtMutable(num4);
				if (reference.lightType == global::UnityEngine.LightType.Spot || reference.lightType == global::UnityEngine.LightType.Directional || reference.lightType == global::UnityEngine.LightType.Point)
				{
					int num5 = nativeArray[(int)reference.lightType]++;
					stencilVisLights[stencilVisLightOffsets[(int)reference.lightType] + num5] = num4;
				}
			}
			nativeArray.Dispose();
		}

		private bool HasStencilLightsOfType(global::UnityEngine.LightType type)
		{
			return m_stencilVisLightOffsets[(int)type] != k_InvalidLightOffset;
		}

		private void RenderClusterLights(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			if (m_ClusterDeferredMaterial == null)
			{
				global::UnityEngine.Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", m_ClusterDeferredMaterial, GetType().Name);
				return;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, m_ProfilingSamplerDeferredShadingPass))
			{
				if (m_FullscreenMesh == null)
				{
					m_FullscreenMesh = CreateFullscreenMesh();
				}
				global::UnityEngine.Rendering.Universal.ShadowUtils.SetSoftShadowQualityShaderKeywords(cmd, shadowData);
				cmd.DrawMesh(m_FullscreenMesh, global::UnityEngine.Matrix4x4.identity, m_ClusterDeferredMaterial, 0, m_ClusterDeferredPasses[0]);
				cmd.DrawMesh(m_FullscreenMesh, global::UnityEngine.Matrix4x4.identity, m_ClusterDeferredMaterial, 0, m_ClusterDeferredPasses[1]);
			}
		}

		private void RenderStencilLights(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, bool stripShadowsOffVariants)
		{
			if (m_stencilVisLights.Length == 0)
			{
				return;
			}
			if (m_StencilDeferredMaterial == null)
			{
				global::UnityEngine.Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", m_StencilDeferredMaterial, GetType().Name);
				return;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, m_ProfilingSamplerDeferredStencilPass))
			{
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = lightData.visibleLights;
				bool hasLightCookieManager = m_LightCookieManager != null;
				bool hasAdditionalLightPass = m_AdditionalLightsShadowCasterPass != null;
				if (HasStencilLightsOfType(global::UnityEngine.LightType.Directional))
				{
					RenderStencilDirectionalLights(cmd, stripShadowsOffVariants, lightData, shadowData, visibleLights, hasAdditionalLightPass, hasLightCookieManager, lightData.mainLightIndex);
				}
				if (lightData.supportsAdditionalLights)
				{
					if (HasStencilLightsOfType(global::UnityEngine.LightType.Point))
					{
						RenderStencilPointLights(cmd, stripShadowsOffVariants, lightData, shadowData, visibleLights, hasAdditionalLightPass, hasLightCookieManager);
					}
					if (HasStencilLightsOfType(global::UnityEngine.LightType.Spot))
					{
						RenderStencilSpotLights(cmd, stripShadowsOffVariants, lightData, shadowData, visibleLights, hasAdditionalLightPass, hasLightCookieManager);
					}
				}
			}
		}

		private void RenderStencilDirectionalLights(global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool stripShadowsOffVariants, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights, bool hasAdditionalLightPass, bool hasLightCookieManager, int mainLightIndex)
		{
			if (m_FullscreenMesh == null)
			{
				m_FullscreenMesh = CreateFullscreenMesh();
			}
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._DIRECTIONAL, value: true);
			int lastCookieLightIndex = -1;
			bool flag = true;
			bool lastLightCookieState = false;
			bool lastShadowsKeyword = false;
			bool lastHasSoftShadow = false;
			for (int i = m_stencilVisLightOffsets[1]; i < m_stencilVisLights.Length; i++)
			{
				ushort num = m_stencilVisLights[i];
				ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAtMutable(num);
				if (reference.lightType != global::UnityEngine.LightType.Directional)
				{
					break;
				}
				global::UnityEngine.Light light = reference.light;
				global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, num, out var lightPos, out var lightColor, out var _, out var _, out var _);
				int num2 = 0;
				if (light.bakingOutput.lightmapBakeType == global::UnityEngine.LightmapBakeType.Mixed)
				{
					num2 |= 4;
				}
				if (lightData.supportsLightLayers)
				{
					SetRenderingLayersMask(cmd, light, global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightLayerMask);
				}
				bool hasDeferredShadows = (bool)light && light.shadows != global::UnityEngine.LightShadows.None;
				bool flag2 = num == mainLightIndex;
				if (!flag2)
				{
					int num3 = (hasAdditionalLightPass ? m_AdditionalLightsShadowCasterPass.GetShadowLightIndexFromLightIndex(num) : (-1));
					hasDeferredShadows = (bool)light && light.shadows != global::UnityEngine.LightShadows.None && num3 >= 0;
					cmd.SetGlobalInt(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._ShadowLightIndex, num3);
					SetLightCookiesKeyword(cmd, num, hasLightCookieManager, flag, ref lastLightCookieState, ref lastCookieLightIndex);
				}
				SetAdditionalLightsShadowsKeyword(ref cmd, stripShadowsOffVariants, shadowData.additionalLightShadowsEnabled, hasDeferredShadows, flag, ref lastShadowsKeyword);
				SetSoftShadowsKeyword(cmd, shadowData, light, hasDeferredShadows, flag, ref lastHasSoftShadow);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._DEFERRED_FIRST_LIGHT, flag);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._DEFERRED_MAIN_LIGHT, flag2);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightColor, lightColor);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightDirection, lightPos);
				cmd.SetGlobalInt(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightFlags, num2);
				cmd.DrawMesh(m_FullscreenMesh, global::UnityEngine.Matrix4x4.identity, m_StencilDeferredMaterial, 0, m_StencilDeferredPasses[3]);
				cmd.DrawMesh(m_FullscreenMesh, global::UnityEngine.Matrix4x4.identity, m_StencilDeferredMaterial, 0, m_StencilDeferredPasses[4]);
				flag = false;
			}
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._DIRECTIONAL, value: false);
		}

		private void RenderStencilPointLights(global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool stripShadowsOffVariants, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights, bool hasAdditionalLightPass, bool hasLightCookieManager)
		{
			if (m_SphereMesh == null)
			{
				m_SphereMesh = CreateSphereMesh();
			}
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._POINT, value: true);
			int lastCookieLightIndex = -1;
			bool shouldOverride = true;
			bool lastLightCookieState = false;
			bool lastShadowsKeyword = false;
			bool lastHasSoftShadow = false;
			for (int i = m_stencilVisLightOffsets[2]; i < m_stencilVisLights.Length; i++)
			{
				ushort num = m_stencilVisLights[i];
				ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAtMutable(num);
				if (reference.lightType != global::UnityEngine.LightType.Point)
				{
					break;
				}
				global::UnityEngine.Light light = reference.light;
				global::UnityEngine.Vector3 vector = reference.localToWorldMatrix.GetColumn(3);
				global::UnityEngine.Matrix4x4 matrix = new global::UnityEngine.Matrix4x4(new global::UnityEngine.Vector4(reference.range, 0f, 0f, 0f), new global::UnityEngine.Vector4(0f, reference.range, 0f, 0f), new global::UnityEngine.Vector4(0f, 0f, reference.range, 0f), new global::UnityEngine.Vector4(vector.x, vector.y, vector.z, 1f));
				global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, num, out var lightPos, out var lightColor, out var lightAttenuation, out var _, out var lightOcclusionProbeChannel);
				if (lightData.supportsLightLayers)
				{
					SetRenderingLayersMask(cmd, light, global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightLayerMask);
				}
				int num2 = 0;
				if (light.bakingOutput.lightmapBakeType == global::UnityEngine.LightmapBakeType.Mixed)
				{
					num2 |= 4;
				}
				int num3 = (hasAdditionalLightPass ? m_AdditionalLightsShadowCasterPass.GetShadowLightIndexFromLightIndex(num) : (-1));
				bool hasDeferredShadows = (bool)light && light.shadows != global::UnityEngine.LightShadows.None && num3 >= 0;
				SetAdditionalLightsShadowsKeyword(ref cmd, stripShadowsOffVariants, shadowData.additionalLightShadowsEnabled, hasDeferredShadows, shouldOverride, ref lastShadowsKeyword);
				SetSoftShadowsKeyword(cmd, shadowData, light, hasDeferredShadows, shouldOverride, ref lastHasSoftShadow);
				SetLightCookiesKeyword(cmd, num, hasLightCookieManager, shouldOverride, ref lastLightCookieState, ref lastCookieLightIndex);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightPosWS, lightPos);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightColor, lightColor);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightAttenuation, lightAttenuation);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightOcclusionProbInfo, lightOcclusionProbeChannel);
				cmd.SetGlobalInt(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightFlags, num2);
				cmd.SetGlobalInt(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._ShadowLightIndex, num3);
				cmd.DrawMesh(m_SphereMesh, matrix, m_StencilDeferredMaterial, 0, m_StencilDeferredPasses[0]);
				cmd.DrawMesh(m_SphereMesh, matrix, m_StencilDeferredMaterial, 0, m_StencilDeferredPasses[1]);
				cmd.DrawMesh(m_SphereMesh, matrix, m_StencilDeferredMaterial, 0, m_StencilDeferredPasses[2]);
				shouldOverride = false;
			}
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._POINT, value: false);
		}

		private void RenderStencilSpotLights(global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool stripShadowsOffVariants, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights, bool hasAdditionalLightPass, bool hasLightCookieManager)
		{
			if (m_HemisphereMesh == null)
			{
				m_HemisphereMesh = CreateHemisphereMesh();
			}
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._SPOT, value: true);
			int lastCookieLightIndex = -1;
			bool shouldOverride = true;
			bool lastLightCookieState = false;
			bool lastShadowsKeyword = false;
			bool lastHasSoftShadow = false;
			for (int i = m_stencilVisLightOffsets[0]; i < m_stencilVisLights.Length; i++)
			{
				ushort num = m_stencilVisLights[i];
				ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAtMutable(num);
				if (reference.lightType != global::UnityEngine.LightType.Spot)
				{
					break;
				}
				global::UnityEngine.Light light = reference.light;
				float f = global::System.MathF.PI / 180f * reference.spotAngle * 0.5f;
				float num2 = global::UnityEngine.Mathf.Cos(f);
				float num3 = global::UnityEngine.Mathf.Sin(f);
				float num4 = global::UnityEngine.Mathf.Lerp(1f, kStencilShapeGuard, num3);
				global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, num, out var lightPos, out var lightColor, out var lightAttenuation, out var lightSpotDir, out var lightOcclusionProbeChannel);
				if (lightData.supportsLightLayers)
				{
					SetRenderingLayersMask(cmd, light, global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightLayerMask);
				}
				int num5 = 0;
				if (light.bakingOutput.lightmapBakeType == global::UnityEngine.LightmapBakeType.Mixed)
				{
					num5 |= 4;
				}
				int num6 = (hasAdditionalLightPass ? m_AdditionalLightsShadowCasterPass.GetShadowLightIndexFromLightIndex(num) : (-1));
				bool hasDeferredShadows = (bool)light && light.shadows != global::UnityEngine.LightShadows.None && num6 >= 0;
				SetAdditionalLightsShadowsKeyword(ref cmd, stripShadowsOffVariants, shadowData.additionalLightShadowsEnabled, hasDeferredShadows, shouldOverride, ref lastShadowsKeyword);
				SetSoftShadowsKeyword(cmd, shadowData, light, hasDeferredShadows, shouldOverride, ref lastHasSoftShadow);
				SetLightCookiesKeyword(cmd, num, hasLightCookieManager, shouldOverride, ref lastLightCookieState, ref lastCookieLightIndex);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SpotLightScale, new global::UnityEngine.Vector4(num3, num3, 1f - num2, reference.range));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SpotLightBias, new global::UnityEngine.Vector4(0f, 0f, num2, 0f));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SpotLightGuard, new global::UnityEngine.Vector4(num4, num4, num4, num2 * reference.range));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightPosWS, lightPos);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightColor, lightColor);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightAttenuation, lightAttenuation);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightDirection, new global::UnityEngine.Vector3(lightSpotDir.x, lightSpotDir.y, lightSpotDir.z));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightOcclusionProbInfo, lightOcclusionProbeChannel);
				cmd.SetGlobalInt(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LightFlags, num5);
				cmd.SetGlobalInt(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._ShadowLightIndex, num6);
				cmd.DrawMesh(m_HemisphereMesh, reference.localToWorldMatrix, m_StencilDeferredMaterial, 0, m_StencilDeferredPasses[0]);
				cmd.DrawMesh(m_HemisphereMesh, reference.localToWorldMatrix, m_StencilDeferredMaterial, 0, m_StencilDeferredPasses[1]);
				cmd.DrawMesh(m_HemisphereMesh, reference.localToWorldMatrix, m_StencilDeferredMaterial, 0, m_StencilDeferredPasses[2]);
				shouldOverride = false;
			}
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._SPOT, value: false);
		}

		private void RenderSSAOBeforeShading(global::UnityEngine.Rendering.RasterCommandBuffer cmd)
		{
			if (m_FullscreenMesh == null)
			{
				m_FullscreenMesh = CreateFullscreenMesh();
			}
			cmd.DrawMesh(m_FullscreenMesh, global::UnityEngine.Matrix4x4.identity, m_StencilDeferredMaterial, 0, m_StencilDeferredPasses[6]);
		}

		private void RenderFog(global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool isOrthographic)
		{
			if (!global::UnityEngine.RenderSettings.fog || isOrthographic)
			{
				return;
			}
			if (m_FullscreenMesh == null)
			{
				m_FullscreenMesh = CreateFullscreenMesh();
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, m_ProfilingSamplerDeferredFogPass))
			{
				global::UnityEngine.Material material = (m_UseDeferredPlus ? m_ClusterDeferredMaterial : m_StencilDeferredMaterial);
				int shaderPass = (m_UseDeferredPlus ? m_ClusterDeferredPasses[2] : m_StencilDeferredPasses[5]);
				cmd.DrawMesh(m_FullscreenMesh, global::UnityEngine.Matrix4x4.identity, material, 0, shaderPass);
			}
		}

		private void InitStencilDeferredMaterial()
		{
			if (!(m_StencilDeferredMaterial == null))
			{
				for (int i = 0; i < k_StencilDeferredPassNames.Length; i++)
				{
					m_StencilDeferredPasses[i] = m_StencilDeferredMaterial.FindPass(k_StencilDeferredPassNames[i]);
				}
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._StencilRef, 0f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._StencilReadMask, 96f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._StencilWriteMask, 16f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LitPunctualStencilRef, 48f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LitPunctualStencilReadMask, 112f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LitPunctualStencilWriteMask, 16f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SimpleLitPunctualStencilRef, 80f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SimpleLitPunctualStencilReadMask, 112f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SimpleLitPunctualStencilWriteMask, 16f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LitDirStencilRef, 32f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LitDirStencilReadMask, 96f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LitDirStencilWriteMask, 0f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SimpleLitDirStencilRef, 64f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SimpleLitDirStencilReadMask, 96f);
				m_StencilDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SimpleLitDirStencilWriteMask, 0f);
			}
		}

		private void InitClusterDeferredMaterial()
		{
			if (!(m_ClusterDeferredMaterial == null))
			{
				for (int i = 0; i < k_ClusterDeferredPassNames.Length; i++)
				{
					m_ClusterDeferredPasses[i] = m_ClusterDeferredMaterial.FindPass(k_ClusterDeferredPassNames[i]);
				}
				m_ClusterDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LitStencilRef, 32f);
				m_ClusterDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LitStencilReadMask, 96f);
				m_ClusterDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._LitStencilWriteMask, 0f);
				m_ClusterDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SimpleLitStencilRef, 64f);
				m_ClusterDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SimpleLitStencilReadMask, 96f);
				m_ClusterDeferredMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._SimpleLitStencilWriteMask, 0f);
			}
		}

		private static global::UnityEngine.Mesh CreateSphereMesh()
		{
			global::UnityEngine.Vector3[] vertices = new global::UnityEngine.Vector3[42]
			{
				new global::UnityEngine.Vector3(0f, 0f, -1.07f),
				new global::UnityEngine.Vector3(0.174f, -0.535f, -0.91f),
				new global::UnityEngine.Vector3(-0.455f, -0.331f, -0.91f),
				new global::UnityEngine.Vector3(0.562f, 0f, -0.91f),
				new global::UnityEngine.Vector3(-0.455f, 0.331f, -0.91f),
				new global::UnityEngine.Vector3(0.174f, 0.535f, -0.91f),
				new global::UnityEngine.Vector3(-0.281f, -0.865f, -0.562f),
				new global::UnityEngine.Vector3(0.736f, -0.535f, -0.562f),
				new global::UnityEngine.Vector3(0.296f, -0.91f, -0.468f),
				new global::UnityEngine.Vector3(-0.91f, 0f, -0.562f),
				new global::UnityEngine.Vector3(-0.774f, -0.562f, -0.478f),
				new global::UnityEngine.Vector3(0f, -1.07f, 0f),
				new global::UnityEngine.Vector3(-0.629f, -0.865f, 0f),
				new global::UnityEngine.Vector3(0.629f, -0.865f, 0f),
				new global::UnityEngine.Vector3(-1.017f, -0.331f, 0f),
				new global::UnityEngine.Vector3(0.957f, 0f, -0.478f),
				new global::UnityEngine.Vector3(0.736f, 0.535f, -0.562f),
				new global::UnityEngine.Vector3(1.017f, -0.331f, 0f),
				new global::UnityEngine.Vector3(1.017f, 0.331f, 0f),
				new global::UnityEngine.Vector3(-0.296f, -0.91f, 0.478f),
				new global::UnityEngine.Vector3(0.281f, -0.865f, 0.562f),
				new global::UnityEngine.Vector3(0.774f, -0.562f, 0.478f),
				new global::UnityEngine.Vector3(-0.736f, -0.535f, 0.562f),
				new global::UnityEngine.Vector3(0.91f, 0f, 0.562f),
				new global::UnityEngine.Vector3(0.455f, -0.331f, 0.91f),
				new global::UnityEngine.Vector3(-0.174f, -0.535f, 0.91f),
				new global::UnityEngine.Vector3(0.629f, 0.865f, 0f),
				new global::UnityEngine.Vector3(0.774f, 0.562f, 0.478f),
				new global::UnityEngine.Vector3(0.455f, 0.331f, 0.91f),
				new global::UnityEngine.Vector3(0f, 0f, 1.07f),
				new global::UnityEngine.Vector3(-0.562f, 0f, 0.91f),
				new global::UnityEngine.Vector3(-0.957f, 0f, 0.478f),
				new global::UnityEngine.Vector3(0.281f, 0.865f, 0.562f),
				new global::UnityEngine.Vector3(-0.174f, 0.535f, 0.91f),
				new global::UnityEngine.Vector3(0.296f, 0.91f, -0.478f),
				new global::UnityEngine.Vector3(-1.017f, 0.331f, 0f),
				new global::UnityEngine.Vector3(-0.736f, 0.535f, 0.562f),
				new global::UnityEngine.Vector3(-0.296f, 0.91f, 0.478f),
				new global::UnityEngine.Vector3(0f, 1.07f, 0f),
				new global::UnityEngine.Vector3(-0.281f, 0.865f, -0.562f),
				new global::UnityEngine.Vector3(-0.774f, 0.562f, -0.478f),
				new global::UnityEngine.Vector3(-0.629f, 0.865f, 0f)
			};
			int[] triangles = new int[240]
			{
				0, 1, 2, 0, 3, 1, 2, 4, 0, 0,
				5, 3, 0, 4, 5, 1, 6, 2, 3, 7,
				1, 1, 8, 6, 1, 7, 8, 9, 4, 2,
				2, 6, 10, 10, 9, 2, 8, 11, 6, 6,
				12, 10, 11, 12, 6, 7, 13, 8, 8, 13,
				11, 10, 14, 9, 10, 12, 14, 3, 15, 7,
				5, 16, 3, 3, 16, 15, 15, 17, 7, 17,
				13, 7, 16, 18, 15, 15, 18, 17, 11, 19,
				12, 13, 20, 11, 11, 20, 19, 17, 21, 13,
				13, 21, 20, 12, 19, 22, 12, 22, 14, 17,
				23, 21, 18, 23, 17, 21, 24, 20, 23, 24,
				21, 20, 25, 19, 19, 25, 22, 24, 25, 20,
				26, 18, 16, 18, 27, 23, 26, 27, 18, 28,
				24, 23, 27, 28, 23, 24, 29, 25, 28, 29,
				24, 25, 30, 22, 25, 29, 30, 14, 22, 31,
				22, 30, 31, 32, 28, 27, 26, 32, 27, 33,
				29, 28, 30, 29, 33, 33, 28, 32, 34, 26,
				16, 5, 34, 16, 14, 31, 35, 14, 35, 9,
				31, 30, 36, 30, 33, 36, 35, 31, 36, 37,
				33, 32, 36, 33, 37, 38, 32, 26, 34, 38,
				26, 38, 37, 32, 5, 39, 34, 39, 38, 34,
				4, 39, 5, 9, 40, 4, 9, 35, 40, 4,
				40, 39, 35, 36, 41, 41, 36, 37, 41, 37,
				38, 40, 35, 41, 40, 41, 39, 41, 38, 39
			};
			return new global::UnityEngine.Mesh
			{
				indexFormat = global::UnityEngine.Rendering.IndexFormat.UInt16,
				vertices = vertices,
				triangles = triangles
			};
		}

		private static global::UnityEngine.Mesh CreateHemisphereMesh()
		{
			global::UnityEngine.Vector3[] vertices = new global::UnityEngine.Vector3[42]
			{
				new global::UnityEngine.Vector3(0f, 0f, 0f),
				new global::UnityEngine.Vector3(1f, 0f, 0f),
				new global::UnityEngine.Vector3(0.92388f, 0.382683f, 0f),
				new global::UnityEngine.Vector3(0.707107f, 0.707107f, 0f),
				new global::UnityEngine.Vector3(0.382683f, 0.92388f, 0f),
				new global::UnityEngine.Vector3(-0f, 1f, 0f),
				new global::UnityEngine.Vector3(-0.382684f, 0.92388f, 0f),
				new global::UnityEngine.Vector3(-0.707107f, 0.707107f, 0f),
				new global::UnityEngine.Vector3(-0.92388f, 0.382683f, 0f),
				new global::UnityEngine.Vector3(-1f, -0f, 0f),
				new global::UnityEngine.Vector3(-0.92388f, -0.382683f, 0f),
				new global::UnityEngine.Vector3(-0.707107f, -0.707107f, 0f),
				new global::UnityEngine.Vector3(-0.382683f, -0.92388f, 0f),
				new global::UnityEngine.Vector3(0f, -1f, 0f),
				new global::UnityEngine.Vector3(0.382684f, -0.923879f, 0f),
				new global::UnityEngine.Vector3(0.707107f, -0.707107f, 0f),
				new global::UnityEngine.Vector3(0.92388f, -0.382683f, 0f),
				new global::UnityEngine.Vector3(0f, 0f, 1f),
				new global::UnityEngine.Vector3(0.707107f, 0f, 0.707107f),
				new global::UnityEngine.Vector3(0f, -0.707107f, 0.707107f),
				new global::UnityEngine.Vector3(0f, 0.707107f, 0.707107f),
				new global::UnityEngine.Vector3(-0.707107f, 0f, 0.707107f),
				new global::UnityEngine.Vector3(0.816497f, -0.408248f, 0.408248f),
				new global::UnityEngine.Vector3(0.408248f, -0.408248f, 0.816497f),
				new global::UnityEngine.Vector3(0.408248f, -0.816497f, 0.408248f),
				new global::UnityEngine.Vector3(0.408248f, 0.816497f, 0.408248f),
				new global::UnityEngine.Vector3(0.408248f, 0.408248f, 0.816497f),
				new global::UnityEngine.Vector3(0.816497f, 0.408248f, 0.408248f),
				new global::UnityEngine.Vector3(-0.816497f, 0.408248f, 0.408248f),
				new global::UnityEngine.Vector3(-0.408248f, 0.408248f, 0.816497f),
				new global::UnityEngine.Vector3(-0.408248f, 0.816497f, 0.408248f),
				new global::UnityEngine.Vector3(-0.408248f, -0.816497f, 0.408248f),
				new global::UnityEngine.Vector3(-0.408248f, -0.408248f, 0.816497f),
				new global::UnityEngine.Vector3(-0.816497f, -0.408248f, 0.408248f),
				new global::UnityEngine.Vector3(0f, -0.92388f, 0.382683f),
				new global::UnityEngine.Vector3(0.92388f, 0f, 0.382683f),
				new global::UnityEngine.Vector3(0f, -0.382683f, 0.92388f),
				new global::UnityEngine.Vector3(0.382683f, 0f, 0.92388f),
				new global::UnityEngine.Vector3(0f, 0.92388f, 0.382683f),
				new global::UnityEngine.Vector3(0f, 0.382683f, 0.92388f),
				new global::UnityEngine.Vector3(-0.92388f, 0f, 0.382683f),
				new global::UnityEngine.Vector3(-0.382683f, 0f, 0.92388f)
			};
			int[] triangles = new int[240]
			{
				0, 2, 1, 0, 3, 2, 0, 4, 3, 0,
				5, 4, 0, 6, 5, 0, 7, 6, 0, 8,
				7, 0, 9, 8, 0, 10, 9, 0, 11, 10,
				0, 12, 11, 0, 13, 12, 0, 14, 13, 0,
				15, 14, 0, 16, 15, 0, 1, 16, 22, 23,
				24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
				14, 24, 34, 35, 22, 16, 36, 23, 37, 2,
				27, 35, 38, 25, 4, 37, 26, 39, 6, 30,
				38, 40, 28, 8, 39, 29, 41, 10, 33, 40,
				34, 31, 12, 41, 32, 36, 15, 22, 24, 18,
				23, 22, 19, 24, 23, 3, 25, 27, 20, 26,
				25, 18, 27, 26, 7, 28, 30, 21, 29, 28,
				20, 30, 29, 11, 31, 33, 19, 32, 31, 21,
				33, 32, 13, 14, 34, 15, 24, 14, 19, 34,
				24, 1, 35, 16, 18, 22, 35, 15, 16, 22,
				17, 36, 37, 19, 23, 36, 18, 37, 23, 1,
				2, 35, 3, 27, 2, 18, 35, 27, 5, 38,
				4, 20, 25, 38, 3, 4, 25, 17, 37, 39,
				18, 26, 37, 20, 39, 26, 5, 6, 38, 7,
				30, 6, 20, 38, 30, 9, 40, 8, 21, 28,
				40, 7, 8, 28, 17, 39, 41, 20, 29, 39,
				21, 41, 29, 9, 10, 40, 11, 33, 10, 21,
				40, 33, 13, 34, 12, 19, 31, 34, 11, 12,
				31, 17, 41, 36, 21, 32, 41, 19, 36, 32
			};
			return new global::UnityEngine.Mesh
			{
				indexFormat = global::UnityEngine.Rendering.IndexFormat.UInt16,
				vertices = vertices,
				triangles = triangles
			};
		}

		private static global::UnityEngine.Mesh CreateFullscreenMesh()
		{
			global::UnityEngine.Vector3[] vertices = new global::UnityEngine.Vector3[3]
			{
				new global::UnityEngine.Vector3(-1f, 1f, 0f),
				new global::UnityEngine.Vector3(-1f, -3f, 0f),
				new global::UnityEngine.Vector3(3f, 1f, 0f)
			};
			int[] triangles = new int[3] { 0, 1, 2 };
			return new global::UnityEngine.Mesh
			{
				indexFormat = global::UnityEngine.Rendering.IndexFormat.UInt16,
				vertices = vertices,
				triangles = triangles
			};
		}

		private void SetRenderingLayersMask(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Light light, int shaderPropertyID)
		{
			uint value = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.ToValidRenderingLayers(light.GetUniversalAdditionalLightData().renderingLayers);
			cmd.SetGlobalInt(shaderPropertyID, (int)value);
		}

		private void SetAdditionalLightsShadowsKeyword(ref global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool stripShadowsOffVariants, bool additionalLightShadowsEnabled, bool hasDeferredShadows, bool shouldOverride, ref bool lastShadowsKeyword)
		{
			bool flag = !stripShadowsOffVariants;
			bool flag2 = additionalLightShadowsEnabled && (!flag || hasDeferredShadows);
			if (shouldOverride || lastShadowsKeyword != flag2)
			{
				lastShadowsKeyword = flag2;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.AdditionalLightShadows, flag2);
			}
		}

		private void SetSoftShadowsKeyword(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, global::UnityEngine.Light light, bool hasDeferredShadows, bool shouldOverride, ref bool lastHasSoftShadow)
		{
			bool flag = hasDeferredShadows && shadowData.supportsSoftShadows && light.shadows == global::UnityEngine.LightShadows.Soft;
			if (shouldOverride || lastHasSoftShadow != flag)
			{
				lastHasSoftShadow = flag;
				global::UnityEngine.Rendering.Universal.ShadowUtils.SetPerLightSoftShadowKeyword(cmd, flag);
			}
		}

		private void SetLightCookiesKeyword(global::UnityEngine.Rendering.RasterCommandBuffer cmd, int visLightIndex, bool hasLightCookieManager, bool shouldOverride, ref bool lastLightCookieState, ref int lastCookieLightIndex)
		{
			if (hasLightCookieManager)
			{
				int lightCookieShaderDataIndex = m_LightCookieManager.GetLightCookieShaderDataIndex(visLightIndex);
				bool flag = lightCookieShaderDataIndex >= 0;
				if (shouldOverride || flag != lastLightCookieState)
				{
					lastLightCookieState = flag;
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightCookies, flag);
				}
				if (shouldOverride || lightCookieShaderDataIndex != lastCookieLightIndex)
				{
					lastCookieLightIndex = lightCookieShaderDataIndex;
					cmd.SetGlobalInt(global::UnityEngine.Rendering.Universal.Internal.DeferredLights.ShaderConstants._CookieLightIndex, lightCookieShaderDataIndex);
				}
			}
		}
	}
}
