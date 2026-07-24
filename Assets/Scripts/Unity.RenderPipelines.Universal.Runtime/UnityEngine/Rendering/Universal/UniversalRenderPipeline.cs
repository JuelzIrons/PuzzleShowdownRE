namespace UnityEngine.Rendering.Universal
{
	public sealed class UniversalRenderPipeline : global::UnityEngine.Rendering.RenderPipeline
	{
		internal static class CameraMetadataCache
		{
			public class CameraMetadataCacheEntry
			{
				public global::UnityEngine.Rendering.ProfilingSampler sampler;
			}

			private static global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry> s_MetadataCache = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry>();

			private static readonly global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry k_NoAllocEntry = new global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry
			{
				sampler = new global::UnityEngine.Rendering.ProfilingSampler("Unknown")
			};

			public static global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry GetCached(global::UnityEngine.Camera camera)
			{
				int hashCode = camera.GetHashCode();
				if (!s_MetadataCache.TryGetValue(hashCode, out var value))
				{
					string name = camera.name;
					value = new global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry
					{
						sampler = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.RenderSingleCameraInternal: " + name)
					};
					s_MetadataCache.Add(hashCode, value);
				}
				return value;
			}
		}

		internal static class Profiling
		{
			public static class Pipeline
			{
				public static class Renderer
				{
					private const string k_Name = "ScriptableRenderer";

					public static readonly global::UnityEngine.Rendering.ProfilingSampler setupCullingParameters = new global::UnityEngine.Rendering.ProfilingSampler("ScriptableRenderer.SetupCullingParameters");
				}

				public static class Context
				{
					private const string k_Name = "ScriptableRenderContext";

					public static readonly global::UnityEngine.Rendering.ProfilingSampler submit = new global::UnityEngine.Rendering.ProfilingSampler("ScriptableRenderContext.Submit");
				}

				private const string k_Name = "UniversalRenderPipeline";

				public static readonly global::UnityEngine.Rendering.ProfilingSampler initializeCameraData = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.CreateCameraData");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler initializeStackedCameraData = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.InitializeStackedCameraData");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler initializeAdditionalCameraData = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.InitializeAdditionalCameraData");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler initializeRenderingData = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.CreateRenderingData");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler initializeShadowData = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.CreateShadowData");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler initializeLightData = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.CreateLightData");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler buildAdditionalLightsShadowAtlasLayout = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.BuildAdditionalLightsShadowAtlasLayout");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler getPerObjectLightFlags = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.GetPerObjectLightFlags");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler getMainLightIndex = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.GetMainLightIndex");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler setupPerFrameShaderConstants = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.SetupPerFrameShaderConstants");

				public static readonly global::UnityEngine.Rendering.ProfilingSampler setupPerCameraShaderConstants = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.SetupPerCameraShaderConstants");
			}
		}

		private readonly struct CameraRenderingScope : global::System.IDisposable
		{
			private static readonly global::UnityEngine.Rendering.ProfilingSampler beginCameraRenderingSampler = new global::UnityEngine.Rendering.ProfilingSampler("RenderPipeline.BeginCameraRendering");

			private static readonly global::UnityEngine.Rendering.ProfilingSampler endCameraRenderingSampler = new global::UnityEngine.Rendering.ProfilingSampler("RenderPipeline.EndCameraRendering");

			private readonly global::UnityEngine.Rendering.ScriptableRenderContext m_Context;

			private readonly global::UnityEngine.Camera m_Camera;

			public CameraRenderingScope(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera camera)
			{
				using (new global::UnityEngine.Rendering.ProfilingScope(beginCameraRenderingSampler))
				{
					m_Context = context;
					m_Camera = camera;
					global::UnityEngine.Rendering.RenderPipeline.BeginCameraRendering(context, camera);
				}
			}

			public void Dispose()
			{
				using (new global::UnityEngine.Rendering.ProfilingScope(endCameraRenderingSampler))
				{
					global::UnityEngine.Rendering.RenderPipeline.EndCameraRendering(m_Context, m_Camera);
				}
			}
		}

		private readonly struct ContextRenderingScope : global::System.IDisposable
		{
			private static readonly global::UnityEngine.Rendering.ProfilingSampler beginContextRenderingSampler = new global::UnityEngine.Rendering.ProfilingSampler("RenderPipeline.BeginContextRendering");

			private static readonly global::UnityEngine.Rendering.ProfilingSampler endContextRenderingSampler = new global::UnityEngine.Rendering.ProfilingSampler("RenderPipeline.EndContextRendering");

			private readonly global::UnityEngine.Rendering.ScriptableRenderContext m_Context;

			private readonly global::System.Collections.Generic.List<global::UnityEngine.Camera> m_Cameras;

			public ContextRenderingScope(global::UnityEngine.Rendering.ScriptableRenderContext context, global::System.Collections.Generic.List<global::UnityEngine.Camera> cameras)
			{
				m_Context = context;
				m_Cameras = cameras;
				using (new global::UnityEngine.Rendering.ProfilingScope(beginContextRenderingSampler))
				{
					global::UnityEngine.Rendering.RenderPipeline.BeginContextRendering(m_Context, m_Cameras);
				}
			}

			public void Dispose()
			{
				using (new global::UnityEngine.Rendering.ProfilingScope(endContextRenderingSampler))
				{
					global::UnityEngine.Rendering.RenderPipeline.EndContextRendering(m_Context, m_Cameras);
				}
			}
		}

		public class SingleCameraRequest
		{
			public global::UnityEngine.RenderTexture destination;

			public int mipLevel;

			public global::UnityEngine.CubemapFace face = global::UnityEngine.CubemapFace.Unknown;

			public int slice;
		}

		public const string k_ShaderTagName = "UniversalPipeline";

		internal const int k_DefaultRenderingLayerMask = 1;

		private readonly global::UnityEngine.Rendering.DebugDisplaySettingsUI m_DebugDisplaySettingsUI = new global::UnityEngine.Rendering.DebugDisplaySettingsUI();

		private global::UnityEngine.Rendering.Universal.UniversalRenderPipelineGlobalSettings m_GlobalSettings;

		internal static bool stackedOverlayCamerasRequireDepthForPostProcessing = false;

		internal static global::UnityEngine.Rendering.RenderGraphModule.RenderGraph s_RenderGraph;

		internal static global::UnityEngine.Rendering.Universal.RTHandleResourcePool s_RTHandlePool;

		internal bool apvIsEnabled;

		internal static bool requireOffscreenUICoverPrepass;

		internal static bool offscreenUIRenderedInCurrentFrame;

		private readonly global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset pipelineAsset;

		internal bool enableHDROutputOnce = true;

		internal bool warnedRuntimeSwitchHDROutputToSDROutput;

		private static global::UnityEngine.Vector4 k_DefaultLightPosition = new global::UnityEngine.Vector4(0f, 0f, 1f, 0f);

		private static global::UnityEngine.Vector4 k_DefaultLightColor = global::UnityEngine.Color.black;

		private static global::UnityEngine.Vector4 k_DefaultLightAttenuation = new global::UnityEngine.Vector4(0f, 1f, 0f, 1f);

		private static global::UnityEngine.Vector4 k_DefaultLightSpotDirection = new global::UnityEngine.Vector4(0f, 0f, 1f, 0f);

		private static global::UnityEngine.Vector4 k_DefaultLightsProbeChannel = new global::UnityEngine.Vector4(0f, 0f, 0f, 0f);

		private static global::System.Collections.Generic.List<global::UnityEngine.Vector4> m_ShadowBiasData = new global::System.Collections.Generic.List<global::UnityEngine.Vector4>();

		private static global::System.Collections.Generic.List<int> m_ShadowResolutionData = new global::System.Collections.Generic.List<int>();

		private global::System.Comparison<global::UnityEngine.Camera> cameraComparison = (global::UnityEngine.Camera camera1, global::UnityEngine.Camera camera2) => (int)camera1.depth - (int)camera2.depth;

		private static global::UnityEngine.Experimental.GlobalIllumination.Lightmapping.RequestLightsDelegate lightsDelegate = delegate(global::UnityEngine.Light[] requests, global::Unity.Collections.NativeArray<global::UnityEngine.Experimental.GlobalIllumination.LightDataGI> lightsOutput)
		{
			global::UnityEngine.Experimental.GlobalIllumination.LightDataGI value = default(global::UnityEngine.Experimental.GlobalIllumination.LightDataGI);
			if (!global::UnityEngine.Rendering.SupportedRenderingFeatures.active.enlighten || (global::UnityEngine.Rendering.SupportedRenderingFeatures.active.lightmapBakeTypes | global::UnityEngine.LightmapBakeType.Realtime) == (global::UnityEngine.LightmapBakeType)0)
			{
				for (int i = 0; i < requests.Length; i++)
				{
					global::UnityEngine.Light light = requests[i];
					value.InitNoBake(light.GetEntityId());
					lightsOutput[i] = value;
				}
			}
			else
			{
				for (int j = 0; j < requests.Length; j++)
				{
					global::UnityEngine.Light light2 = requests[j];
					switch (light2.type)
					{
					case global::UnityEngine.LightType.Directional:
					{
						global::UnityEngine.Experimental.GlobalIllumination.DirectionalLight dir = default(global::UnityEngine.Experimental.GlobalIllumination.DirectionalLight);
						global::UnityEngine.Experimental.GlobalIllumination.LightmapperUtils.Extract(light2, ref dir);
						value.Init(ref dir);
						break;
					}
					case global::UnityEngine.LightType.Point:
					{
						global::UnityEngine.Experimental.GlobalIllumination.PointLight point = default(global::UnityEngine.Experimental.GlobalIllumination.PointLight);
						global::UnityEngine.Experimental.GlobalIllumination.LightmapperUtils.Extract(light2, ref point);
						value.Init(ref point);
						break;
					}
					case global::UnityEngine.LightType.Spot:
					{
						global::UnityEngine.Experimental.GlobalIllumination.SpotLight spot = default(global::UnityEngine.Experimental.GlobalIllumination.SpotLight);
						global::UnityEngine.Experimental.GlobalIllumination.LightmapperUtils.Extract(light2, ref spot);
						spot.innerConeAngle = light2.innerSpotAngle * (global::System.MathF.PI / 180f);
						spot.angularFalloff = global::UnityEngine.Experimental.GlobalIllumination.AngularFalloffType.AnalyticAndInnerAngle;
						value.Init(ref spot);
						break;
					}
					case global::UnityEngine.LightType.Area:
						value.InitNoBake(light2.GetEntityId());
						break;
					case global::UnityEngine.LightType.Disc:
						value.InitNoBake(light2.GetEntityId());
						break;
					default:
						value.InitNoBake(light2.GetEntityId());
						break;
					}
					value.falloff = global::UnityEngine.Experimental.GlobalIllumination.FalloffType.InverseSquared;
					lightsOutput[j] = value;
				}
			}
		};

		public static float maxShadowBias => 10f;

		public static float minRenderScale => 0.1f;

		public static float maxRenderScale => 3f;

		public static int maxNumIterationsEnclosingSphere => 1000;

		public static int maxPerObjectLights => 8;

		public static int maxVisibleAdditionalLights
		{
			get
			{
				bool isShaderAPIMobileDefined = global::UnityEngine.Rendering.Universal.PlatformAutoDetect.isShaderAPIMobileDefined;
				if (isShaderAPIMobileDefined && global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 && global::UnityEngine.Graphics.minOpenGLESVersion <= global::UnityEngine.Rendering.OpenGLESVersion.OpenGLES30)
				{
					return 16;
				}
				if (!isShaderAPIMobileDefined && global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore && global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 && global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.WebGPU)
				{
					return 256;
				}
				return 32;
			}
		}

		internal static int lightsPerTile => (maxVisibleAdditionalLights + 31) / 32 * 32;

		internal static int maxZBinWords => 4096;

		internal static int maxTileWords => ((maxVisibleAdditionalLights <= 32) ? 1024 : 4096) * 4;

		internal static int maxVisibleReflectionProbes => global::System.Math.Min(maxVisibleAdditionalLights, 64);

		internal global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTextures runtimeTextures { get; private set; }

		internal static global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy renderTextureUVOriginStrategy { private get; set; }

		public override global::UnityEngine.Rendering.RenderPipelineGlobalSettings defaultSettings => m_GlobalSettings;

		internal static bool canOptimizeScreenMSAASamples { get; private set; }

		internal static int startFrameScreenMSAASamples { get; private set; }

		public static global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset => global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline as global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset;

		public override string ToString()
		{
			return pipelineAsset?.ToString();
		}

		public UniversalRenderPipeline(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset)
		{
			pipelineAsset = asset;
			m_GlobalSettings = global::UnityEngine.Rendering.RenderPipelineGlobalSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineGlobalSettings, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline>.instance;
			runtimeTextures = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTextures>();
			global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeShaders renderPipelineSettings = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeShaders>();
			global::UnityEngine.Rendering.Blitter.Initialize(renderPipelineSettings.coreBlitPS, renderPipelineSettings.coreBlitColorAndDepthPS);
			SetSupportedRenderingFeatures(pipelineAsset);
			global::UnityEngine.Rendering.RTHandles.Initialize(global::UnityEngine.Screen.width, global::UnityEngine.Screen.height);
			global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.InitializeShaderGlobalKeywords();
			global::UnityEngine.Rendering.GraphicsSettings.useScriptableRenderPipelineBatching = asset.useSRPBatcher;
			if (((global::UnityEngine.QualitySettings.antiAliasing <= 0) ? 1 : global::UnityEngine.QualitySettings.antiAliasing) != asset.msaaSampleCount)
			{
				global::UnityEngine.QualitySettings.antiAliasing = asset.msaaSampleCount;
			}
			global::UnityEngine.Rendering.Universal.URPDefaultVolumeProfileSettings renderPipelineSettings2 = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.URPDefaultVolumeProfileSettings>();
			global::UnityEngine.Rendering.VolumeManager.instance.Initialize(renderPipelineSettings2.volumeProfile, asset.volumeProfile);
			global::UnityEngine.Experimental.Rendering.XRSystem.SetDisplayMSAASamples((global::UnityEngine.Rendering.MSAASamples)global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.NextPowerOfTwo(global::UnityEngine.QualitySettings.antiAliasing), 1, 8));
			global::UnityEngine.Experimental.Rendering.XRSystem.SetRenderScale(asset.renderScale);
			global::UnityEngine.Experimental.GlobalIllumination.Lightmapping.SetDelegate(lightsDelegate);
			global::UnityEngine.Rendering.CameraCaptureBridge.enabled = true;
			global::UnityEngine.Rendering.Universal.RenderingUtils.ClearSystemInfoCache();
			global::UnityEngine.Rendering.Universal.DecalProjector.defaultMaterial = asset.decalMaterial;
			s_RenderGraph = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph("URPRenderGraph");
			s_RTHandlePool = new global::UnityEngine.Rendering.Universal.RTHandleResourcePool();
			global::UnityEngine.Rendering.DebugManager.instance.RefreshEditor();
			global::UnityEngine.QualitySettings.enableLODCrossFade = asset.enableLODCrossFade;
			apvIsEnabled = asset != null && asset.lightProbeSystem == global::UnityEngine.Rendering.Universal.LightProbeSystem.ProbeVolumes;
			global::UnityEngine.Rendering.SupportedRenderingFeatures.active.overridesLightProbeSystem = apvIsEnabled;
			global::UnityEngine.Rendering.SupportedRenderingFeatures.active.skyOcclusion = apvIsEnabled;
			if (apvIsEnabled)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume instance = global::UnityEngine.Rendering.ProbeReferenceVolume.instance;
				global::UnityEngine.Rendering.ProbeVolumeSystemParameters parameters = new global::UnityEngine.Rendering.ProbeVolumeSystemParameters
				{
					memoryBudget = asset.probeVolumeMemoryBudget,
					blendingMemoryBudget = asset.probeVolumeBlendingMemoryBudget,
					shBands = asset.probeVolumeSHBands,
					supportGPUStreaming = asset.supportProbeVolumeGPUStreaming,
					supportDiskStreaming = asset.supportProbeVolumeDiskStreaming,
					supportScenarios = asset.supportProbeVolumeScenarios,
					supportScenarioBlending = asset.supportProbeVolumeScenarioBlending,
					sceneData = m_GlobalSettings.GetOrCreateAPVSceneData()
				};
				instance.Initialize(in parameters);
			}
			global::UnityEngine.Rendering.Vrs.InitializeResources();
		}

		protected override void Dispose(bool disposing)
		{
			global::UnityEngine.Rendering.Vrs.DisposeResources();
			if (apvIsEnabled)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.instance.Cleanup();
			}
			global::UnityEngine.Rendering.Blitter.Cleanup();
			base.Dispose(disposing);
			pipelineAsset.DestroyRenderers();
			global::UnityEngine.Rendering.SupportedRenderingFeatures.active = new global::UnityEngine.Rendering.SupportedRenderingFeatures();
			global::UnityEngine.Rendering.Universal.ShaderData.instance.Dispose();
			global::UnityEngine.Experimental.Rendering.XRSystem.Dispose();
			s_RenderGraph.Cleanup();
			s_RenderGraph = null;
			s_RTHandlePool.Cleanup();
			s_RTHandlePool = null;
			global::UnityEngine.Experimental.GlobalIllumination.Lightmapping.ResetDelegate();
			global::UnityEngine.Rendering.CameraCaptureBridge.enabled = false;
			global::UnityEngine.Rendering.ConstantBuffer.ReleaseAll();
			global::UnityEngine.Rendering.VolumeManager.instance.Deinitialize();
			DisposeAdditionalCameraData();
			global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ClearStaticCaches();
		}

		private void DisposeAdditionalCameraData()
		{
			global::UnityEngine.Camera[] allCameras = global::UnityEngine.Camera.allCameras;
			for (int i = 0; i < allCameras.Length; i++)
			{
				if (allCameras[i].TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component))
				{
					component.historyManager.Dispose();
				}
			}
		}

		protected override void Render(global::UnityEngine.Rendering.ScriptableRenderContext renderContext, global::System.Collections.Generic.List<global::UnityEngine.Camera> cameras)
		{
			SetHDRState(cameras);
			int count = cameras.Count;
			AdjustUIOverlayOwnership(count);
			requireOffscreenUICoverPrepass = HDROutputForMainDisplayIsActive() && asset.supportsHDR && global::UnityEngine.Rendering.SupportedRenderingFeatures.active.rendersUIOverlay && !global::UnityEngine.Rendering.CoreUtils.IsScreenFullyCoveredByCameras(cameras);
			SetupScreenMSAASamplesState(count);
			global::UnityEngine.Rendering.GPUResidentDrawer.ReinitializeIfNeeded();
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.UniversalRenderTotal)))
			{
				using (new global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.ContextRenderingScope(renderContext, cameras))
				{
					global::UnityEngine.Rendering.GraphicsSettings.lightsUseLinearIntensity = global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear;
					global::UnityEngine.Rendering.GraphicsSettings.lightsUseColorTemperature = true;
					SetupPerFrameShaderConstants();
					global::UnityEngine.Experimental.Rendering.XRSystem.SetDisplayMSAASamples((global::UnityEngine.Rendering.MSAASamples)asset.msaaSampleCount);
					global::UnityEngine.Rendering.RTHandles.SetHardwareDynamicResolutionState(hwDynamicResRequested: true);
					SortCameras(cameras);
					int lastBaseCameraIndex = GetLastBaseCameraIndex(cameras);
					offscreenUIRenderedInCurrentFrame = false;
					for (int i = 0; i < count; i++)
					{
						global::UnityEngine.Camera camera = cameras[i];
						bool isLastBaseCamera = i == lastBaseCameraIndex;
						if (IsGameCamera(camera))
						{
							RenderCameraStack(renderContext, camera, isLastBaseCamera);
							continue;
						}
						using (new global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraRenderingScope(renderContext, camera))
						{
							UpdateVolumeFramework(camera, null);
							RenderSingleCameraInternal(renderContext, camera, isLastBaseCamera);
						}
					}
					s_RenderGraph.EndFrame();
					s_RTHandlePool.PurgeUnusedResources(global::UnityEngine.Time.frameCount);
				}
			}
		}

		protected override bool IsRenderRequestSupported<RequestData>(global::UnityEngine.Camera camera, RequestData data)
		{
			if (data is global::UnityEngine.Rendering.RenderPipeline.StandardRequest)
			{
				return true;
			}
			if (data is global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.SingleCameraRequest)
			{
				return true;
			}
			return false;
		}

		protected override void ProcessRenderRequests<RequestData>(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera camera, RequestData renderRequest)
		{
			global::UnityEngine.Rendering.RenderPipeline.StandardRequest standardRequest = renderRequest as global::UnityEngine.Rendering.RenderPipeline.StandardRequest;
			global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.SingleCameraRequest singleCameraRequest = renderRequest as global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.SingleCameraRequest;
			if (standardRequest != null || singleCameraRequest != null)
			{
				global::UnityEngine.RenderTexture renderTexture = ((standardRequest != null) ? standardRequest.destination : singleCameraRequest.destination);
				if (renderTexture == null)
				{
					global::UnityEngine.Debug.LogError("RenderRequest has no destination texture, set one before sending request");
					return;
				}
				int num = standardRequest?.mipLevel ?? singleCameraRequest.mipLevel;
				int num2 = standardRequest?.slice ?? singleCameraRequest.slice;
				int num3 = (int)(standardRequest?.face ?? singleCameraRequest.face);
				global::UnityEngine.RenderTexture targetTexture = camera.targetTexture;
				global::UnityEngine.RenderTexture renderTexture2 = null;
				global::UnityEngine.RenderTextureDescriptor desc = renderTexture.descriptor;
				if (renderTexture.dimension == global::UnityEngine.Rendering.TextureDimension.Cube)
				{
					desc = default(global::UnityEngine.RenderTextureDescriptor);
				}
				desc.colorFormat = renderTexture.format;
				desc.volumeDepth = 1;
				desc.msaaSamples = renderTexture.descriptor.msaaSamples;
				desc.dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D;
				desc.width = renderTexture.width / (int)global::System.Math.Pow(2.0, num);
				desc.height = renderTexture.height / (int)global::System.Math.Pow(2.0, num);
				desc.width = global::UnityEngine.Mathf.Max(1, desc.width);
				desc.height = global::UnityEngine.Mathf.Max(1, desc.height);
				if (renderTexture.dimension != global::UnityEngine.Rendering.TextureDimension.Tex2D || num != 0)
				{
					renderTexture2 = global::UnityEngine.RenderTexture.GetTemporary(desc);
				}
				camera.targetTexture = (renderTexture2 ? renderTexture2 : renderTexture);
				if (standardRequest != null)
				{
					Render(context, new global::System.Collections.Generic.List<global::UnityEngine.Camera> { camera });
				}
				else
				{
					global::System.Collections.Generic.List<global::UnityEngine.Camera> value;
					using (global::UnityEngine.Rendering.ListPool<global::UnityEngine.Camera>.Get(out value))
					{
						value.Add(camera);
						using (new global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.ContextRenderingScope(context, value))
						{
							using (new global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraRenderingScope(context, camera))
							{
								camera.gameObject.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component);
								RenderSingleCameraInternal(context, camera, ref component);
							}
						}
					}
				}
				if ((bool)renderTexture2)
				{
					bool flag = false;
					switch (renderTexture.dimension)
					{
					case global::UnityEngine.Rendering.TextureDimension.Tex2D:
						if ((global::UnityEngine.SystemInfo.copyTextureSupport & global::UnityEngine.Rendering.CopyTextureSupport.Basic) != global::UnityEngine.Rendering.CopyTextureSupport.None)
						{
							flag = true;
							global::UnityEngine.Graphics.CopyTexture(renderTexture2, 0, 0, renderTexture, 0, num);
						}
						break;
					case global::UnityEngine.Rendering.TextureDimension.Tex2DArray:
						if ((global::UnityEngine.SystemInfo.copyTextureSupport & global::UnityEngine.Rendering.CopyTextureSupport.DifferentTypes) != global::UnityEngine.Rendering.CopyTextureSupport.None)
						{
							flag = true;
							global::UnityEngine.Graphics.CopyTexture(renderTexture2, 0, 0, renderTexture, num2, num);
						}
						break;
					case global::UnityEngine.Rendering.TextureDimension.Tex3D:
						if ((global::UnityEngine.SystemInfo.copyTextureSupport & global::UnityEngine.Rendering.CopyTextureSupport.DifferentTypes) != global::UnityEngine.Rendering.CopyTextureSupport.None)
						{
							flag = true;
							global::UnityEngine.Graphics.CopyTexture(renderTexture2, 0, 0, renderTexture, num2, num);
						}
						break;
					case global::UnityEngine.Rendering.TextureDimension.Cube:
						if ((global::UnityEngine.SystemInfo.copyTextureSupport & global::UnityEngine.Rendering.CopyTextureSupport.DifferentTypes) != global::UnityEngine.Rendering.CopyTextureSupport.None)
						{
							flag = true;
							global::UnityEngine.Graphics.CopyTexture(renderTexture2, 0, 0, renderTexture, num3, num);
						}
						break;
					case global::UnityEngine.Rendering.TextureDimension.CubeArray:
						if ((global::UnityEngine.SystemInfo.copyTextureSupport & global::UnityEngine.Rendering.CopyTextureSupport.DifferentTypes) != global::UnityEngine.Rendering.CopyTextureSupport.None)
						{
							flag = true;
							global::UnityEngine.Graphics.CopyTexture(renderTexture2, 0, 0, renderTexture, num3 + num2 * 6, num);
						}
						break;
					}
					if (!flag)
					{
						global::UnityEngine.Debug.LogError("RenderRequest cannot have destination texture of this format: " + global::System.Enum.GetName(typeof(global::UnityEngine.Rendering.TextureDimension), renderTexture.dimension));
					}
				}
				camera.targetTexture = targetTexture;
				global::UnityEngine.Graphics.SetRenderTarget(targetTexture);
				global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture2);
			}
			else
			{
				global::UnityEngine.Debug.LogWarning("RenderRequest type: " + typeof(RequestData).FullName + " is either invalid or unsupported by the current pipeline");
			}
		}

		[global::System.Obsolete("RenderSingleCamera is obsolete, please use RenderPipeline.SubmitRenderRequest with UniversalRenderer.SingleCameraRequest as RequestData type. #from(2023.1)")]
		public static void RenderSingleCamera(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera camera)
		{
			RenderSingleCameraInternal(context, camera);
		}

		internal static void RenderSingleCameraInternal(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera camera, bool isLastBaseCamera = true)
		{
			global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData component = null;
			if (IsGameCamera(camera))
			{
				camera.gameObject.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out component);
			}
			RenderSingleCameraInternal(context, camera, ref component, isLastBaseCamera);
		}

		internal static void RenderSingleCameraInternal(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera camera, ref global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData, bool isLastBaseCamera = true)
		{
			if (additionalCameraData != null && additionalCameraData.renderType != global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
			{
				global::UnityEngine.Debug.LogWarning("Only Base cameras can be rendered with standalone RenderSingleCamera. Camera will be skipped.");
				return;
			}
			if (camera.targetTexture.width == 0 || camera.targetTexture.height == 0 || camera.pixelWidth == 0 || camera.pixelHeight == 0)
			{
				global::UnityEngine.Debug.LogWarning($"Camera '{camera.name}' has an invalid render target size (width: {camera.targetTexture.width}, height: {camera.targetTexture.height}) or pixel dimensions (width: {camera.pixelWidth}, height: {camera.pixelHeight}). Camera will be skipped.");
				return;
			}
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = CreateCameraData(GetRenderer(camera, additionalCameraData).frameData, camera, additionalCameraData);
			InitializeAdditionalCameraData(camera, additionalCameraData, resolveFinalTarget: true, isLastBaseCamera, cameraData);
			global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset universalRenderPipelineAsset = asset;
			if ((object)universalRenderPipelineAsset != null && universalRenderPipelineAsset.useAdaptivePerformance)
			{
				ApplyAdaptivePerformance(cameraData);
			}
			RenderSingleCamera(context, cameraData);
		}

		private static bool TryGetCullingParameters(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, out global::UnityEngine.Rendering.ScriptableCullingParameters cullingParams)
		{
			if (cameraData.xr.enabled)
			{
				cullingParams = cameraData.xr.cullingParams;
				if (!cameraData.camera.usePhysicalProperties && !global::UnityEngine.Rendering.XRGraphicsAutomatedTests.enabled)
				{
					cameraData.camera.fieldOfView = 57.29578f * global::UnityEngine.Mathf.Atan(1f / cullingParams.stereoProjectionMatrix.m11) * 2f;
				}
				return true;
			}
			return cameraData.camera.TryGetCullingParameters(stereoAware: false, out cullingParams);
		}

		private static void RenderSingleCamera(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Camera camera = cameraData.camera;
			global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer = cameraData.renderer;
			if (renderer == null)
			{
				global::UnityEngine.Debug.LogWarning($"Trying to render {camera.name} with an invalid renderer. Camera rendering will be skipped.");
				return;
			}
			using global::UnityEngine.Rendering.ContextContainer contextContainer = renderer.frameData;
			if (!TryGetCullingParameters(cameraData, out var cullingParams))
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.ScriptableRenderer.current = renderer;
			_ = cameraData.isSceneViewCamera;
			global::UnityEngine.Rendering.CommandBuffer commandBuffer = global::UnityEngine.Rendering.CommandBufferPool.Get();
			global::UnityEngine.Rendering.CommandBuffer cmd = (cameraData.xr.enabled ? null : commandBuffer);
			global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry cached = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraMetadataCache.GetCached(camera);
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, cached.sampler))
			{
				renderer.Clear(cameraData.renderType);
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.Renderer.setupCullingParameters))
				{
					global::UnityEngine.Rendering.Universal.CameraData cameraData2 = new global::UnityEngine.Rendering.Universal.CameraData(contextContainer);
					renderer.OnPreCullRenderPasses(in cameraData2);
					renderer.SetupCullingParameters(ref cullingParams, ref cameraData2);
				}
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				SetupPerCameraShaderConstants(commandBuffer);
				global::UnityEngine.Rendering.ProbeVolumesOptions options = null;
				if (camera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component))
				{
					options = component.volumeStack?.GetComponent<global::UnityEngine.Rendering.ProbeVolumesOptions>();
				}
				bool flag = asset != null && asset.lightProbeSystem == global::UnityEngine.Rendering.Universal.LightProbeSystem.ProbeVolumes;
				global::UnityEngine.Rendering.ProbeReferenceVolume.instance.SetEnableStateFromSRP(flag);
				global::UnityEngine.Rendering.ProbeReferenceVolume.instance.SetVertexSamplingEnabled(asset.shEvalMode == global::UnityEngine.Rendering.Universal.ShEvalMode.PerVertex || asset.shEvalMode == global::UnityEngine.Rendering.Universal.ShEvalMode.Mixed);
				if (flag && global::UnityEngine.Rendering.ProbeReferenceVolume.instance.isInitialized)
				{
					global::UnityEngine.Rendering.ProbeReferenceVolume.instance.PerformPendingOperations();
					if (camera.cameraType != global::UnityEngine.CameraType.Reflection && camera.cameraType != global::UnityEngine.CameraType.Preview)
					{
						global::UnityEngine.Rendering.ProbeReferenceVolume.instance.UpdateCellStreaming(commandBuffer, camera, options);
					}
				}
				if (camera.cameraType == global::UnityEngine.CameraType.Reflection || camera.cameraType == global::UnityEngine.CameraType.Preview)
				{
					global::UnityEngine.Rendering.ScriptableRenderContext.EmitGeometryForCamera(camera);
				}
				if (flag)
				{
					global::UnityEngine.Rendering.ProbeReferenceVolume.instance.BindAPVRuntimeResources(commandBuffer, isProbeVolumeEnabled: true);
				}
				global::UnityEngine.Rendering.ProbeReferenceVolume.instance.RenderDebug(camera, options, global::UnityEngine.Texture2D.whiteTexture);
				if (component != null)
				{
					component.motionVectorsPersistentData.Update(cameraData);
				}
				if (cameraData.taaHistory != null)
				{
					UpdateTemporalAATargets(cameraData);
				}
				global::UnityEngine.Rendering.RTHandles.SetReferenceSize(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
				global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = contextContainer.Create<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
				universalRenderingData.cullResults = context.Cull(ref cullingParams);
				global::UnityEngine.Rendering.GPUResidentDrawer.PostCullBeginCameraRendering(new global::UnityEngine.Rendering.RenderRequestBatcherContext
				{
					commandBuffer = commandBuffer
				});
				global::UnityEngine.Rendering.Universal.RenderingMode? renderingMode = (cameraData.renderer as global::UnityEngine.Rendering.Universal.UniversalRenderer)?.renderingModeActual;
				global::UnityEngine.Rendering.Universal.UniversalLightData lightData;
				global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData;
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.initializeRenderingData))
				{
					CreateUniversalResourceData(contextContainer);
					lightData = CreateLightData(contextContainer, asset, universalRenderingData.cullResults.visibleLights, renderingMode);
					shadowData = CreateShadowData(contextContainer, asset, renderingMode);
					CreatePostProcessingData(contextContainer, asset);
					CreateRenderingData(contextContainer, asset, commandBuffer, renderingMode, cameraData.renderer);
					CreateCullContextData(contextContainer, context);
				}
				global::UnityEngine.Rendering.Universal.RenderingData renderingData = new global::UnityEngine.Rendering.Universal.RenderingData(contextContainer);
				CheckAndApplyDebugSettings(ref renderingData);
				global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset universalRenderPipelineAsset = asset;
				if ((object)universalRenderPipelineAsset != null && universalRenderPipelineAsset.useAdaptivePerformance)
				{
					ApplyAdaptivePerformance(contextContainer);
				}
				renderTextureUVOriginStrategy = global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy.BottomLeft;
				CreateShadowAtlasAndCullShadowCasters(lightData, shadowData, cameraData, ref universalRenderingData.cullResults, ref context);
				renderer.AddRenderPasses(ref renderingData);
				global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy uvOriginStrategy = renderTextureUVOriginStrategy;
				RecordAndExecuteRenderGraph(s_RenderGraph, context, renderer, commandBuffer, cameraData.camera, uvOriginStrategy);
				renderer.FinishRenderGraphRendering(commandBuffer);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			global::UnityEngine.Rendering.CommandBufferPool.Release(commandBuffer);
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.Context.submit))
			{
				context.Submit();
			}
			global::UnityEngine.Rendering.Universal.ScriptableRenderer.current = null;
		}

		private static void CreateShadowAtlasAndCullShadowCasters(global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.ScriptableRenderContext context)
		{
			if (shadowData.supportsMainLightShadows || shadowData.supportsAdditionalLightShadows)
			{
				if (shadowData.supportsMainLightShadows)
				{
					InitializeMainLightShadowResolution(shadowData);
				}
				if (shadowData.supportsAdditionalLightShadows)
				{
					shadowData.shadowAtlasLayout = BuildAdditionalLightsShadowAtlasLayout(lightData, shadowData, cameraData);
				}
				shadowData.visibleLightsShadowCullingInfos = global::UnityEngine.Rendering.Universal.ShadowCulling.CullShadowCasters(ref context, shadowData, ref shadowData.shadowAtlasLayout, ref cullResults);
			}
		}

		private static void RenderCameraStack(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera baseCamera, bool isLastBaseCamera)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RenderCameraStack)))
			{
				baseCamera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component);
				if (component != null && component.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay)
				{
					return;
				}
				global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer = GetRenderer(baseCamera, component);
				global::System.Collections.Generic.List<global::UnityEngine.Camera> list = ((renderer == null || !renderer.SupportsCameraStackingType(global::UnityEngine.Rendering.Universal.CameraRenderType.Base)) ? null : component?.cameraStack);
				bool flag = component != null && component.renderPostProcessing;
				bool flag2 = HDROutputForMainDisplayIsActive();
				int num = -1;
				if (list != null)
				{
					global::System.Type type = renderer.GetType();
					bool flag3 = false;
					stackedOverlayCamerasRequireDepthForPostProcessing = false;
					for (int i = 0; i < list.Count; i++)
					{
						global::UnityEngine.Camera camera = list[i];
						if (camera == null)
						{
							flag3 = true;
						}
						else if (camera.isActiveAndEnabled)
						{
							camera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component2);
							global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer2 = GetRenderer(camera, component2);
							global::System.Type type2 = renderer2.GetType();
							if (type2 != type)
							{
								global::UnityEngine.Debug.LogWarning("Only cameras with compatible renderer types can be stacked. The camera: " + camera.name + " are using the renderer " + type2.Name + ", but the base camera: " + baseCamera.name + " are using " + type.Name + ". Will skip rendering");
							}
							else if ((renderer2.SupportedCameraStackingTypes() & 2) == 0)
							{
								global::UnityEngine.Debug.LogWarning("The camera: " + camera.name + " is using a renderer of type " + renderer.GetType().Name + " which does not support Overlay cameras in it's current state.");
							}
							else if (component2 == null || component2.renderType != global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay)
							{
								global::UnityEngine.Debug.LogWarning("Stack can only contain Overlay cameras. The camera: " + camera.name + " " + $"has a type {component2.renderType} that is not supported. Will skip rendering.");
							}
							else
							{
								stackedOverlayCamerasRequireDepthForPostProcessing |= CheckPostProcessForDepth();
								flag |= component2.renderPostProcessing;
								num = i;
							}
						}
					}
					if (flag3)
					{
						component.UpdateCameraStack();
					}
				}
				bool flag4 = num != -1;
				bool flag5 = false;
				bool enableXR = component?.allowXRRendering ?? true;
				global::UnityEngine.Experimental.Rendering.XRLayout xRLayout = global::UnityEngine.Experimental.Rendering.XRSystem.NewLayout();
				xRLayout.AddCamera(baseCamera, enableXR);
				foreach (var activePass in xRLayout.GetActivePasses())
				{
					global::UnityEngine.Experimental.Rendering.XRPass xr = activePass.Item2;
					global::UnityEngine.Rendering.Universal.XRPassUniversal xrPass = xr as global::UnityEngine.Rendering.Universal.XRPassUniversal;
					if (xr.enabled)
					{
						flag5 = true;
						UpdateCameraStereoMatrices(baseCamera, xr);
						float renderViewportScale = global::UnityEngine.Experimental.Rendering.XRSystem.GetRenderViewportScale();
						global::UnityEngine.ScalableBufferManager.ResizeBuffers(renderViewportScale, renderViewportScale);
					}
					bool flag6 = false;
					using (new global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraRenderingScope(context, baseCamera))
					{
						UpdateVolumeFramework(baseCamera, component);
						global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = CreateCameraData(renderer.frameData, baseCamera, component);
						if (xr.enabled)
						{
							universalCameraData.xr = xr;
							UpdateCameraData(universalCameraData, in xr);
							xRLayout.ReconfigurePass(xr, baseCamera);
							global::UnityEngine.Rendering.Universal.XRSystemUniversal.BeginLateLatching(baseCamera, xrPass);
						}
						InitializeAdditionalCameraData(baseCamera, component, !flag4, isLastBaseCamera, universalCameraData);
						global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset universalRenderPipelineAsset = asset;
						if ((object)universalRenderPipelineAsset != null && universalRenderPipelineAsset.useAdaptivePerformance)
						{
							ApplyAdaptivePerformance(universalCameraData);
						}
						universalCameraData.postProcessingRequiresDepthTexture |= stackedOverlayCamerasRequireDepthForPostProcessing;
						bool flag7 = flag2;
						if (xr.enabled)
						{
							flag7 = xr.isHDRDisplayOutputActive;
						}
						flag6 = asset.supportsHDR && flag7 && baseCamera.targetTexture == null && (baseCamera.cameraType == global::UnityEngine.CameraType.Game || baseCamera.cameraType == global::UnityEngine.CameraType.VR) && universalCameraData.allowHDROutput;
						universalCameraData.stackAnyPostProcessingEnabled = flag;
						universalCameraData.stackLastCameraOutputToHDR = flag6;
						bool flag8 = universalCameraData.rendersOverlayUI && flag6 && !offscreenUIRenderedInCurrentFrame;
						if (flag8)
						{
							offscreenUIRenderedInCurrentFrame = true;
						}
						universalCameraData.rendersOffscreenUI = flag8;
						universalCameraData.blitsOffscreenUICover = flag8 && requireOffscreenUICoverPrepass;
						RenderSingleCamera(context, universalCameraData);
					}
					if (xr.enabled)
					{
						global::UnityEngine.Rendering.Universal.XRSystemUniversal.EndLateLatching(baseCamera, xrPass);
					}
					if (!flag4)
					{
						continue;
					}
					for (int j = 0; j < list.Count; j++)
					{
						global::UnityEngine.Camera camera2 = list[j];
						if (!camera2.isActiveAndEnabled)
						{
							continue;
						}
						camera2.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component3);
						if (component3 != null)
						{
							global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData2 = CreateCameraData(GetRenderer(camera2, component3).frameData, baseCamera, component);
							if (xr.enabled)
							{
								universalCameraData2.xr = xr;
								UpdateCameraData(universalCameraData2, in xr);
							}
							InitializeAdditionalCameraData(camera2, component3, resolveFinalTarget: false, isLastBaseCamera, universalCameraData2);
							universalCameraData2.camera = camera2;
							universalCameraData2.baseCamera = baseCamera;
							UpdateCameraStereoMatrices(component3.camera, xr);
							using (new global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.CameraRenderingScope(context, camera2))
							{
								UpdateVolumeFramework(camera2, component3);
								bool resolveFinalTarget = j == num;
								InitializeAdditionalCameraData(camera2, component3, resolveFinalTarget, isLastBaseCamera, universalCameraData2);
								universalCameraData2.stackAnyPostProcessingEnabled = flag;
								universalCameraData2.stackLastCameraOutputToHDR = flag6;
								xRLayout.ReconfigurePass(universalCameraData2.xr, camera2);
								RenderSingleCamera(context, universalCameraData2);
							}
						}
					}
				}
				if (flag5)
				{
					global::UnityEngine.Rendering.CommandBuffer commandBuffer = global::UnityEngine.Rendering.CommandBufferPool.Get();
					global::UnityEngine.Experimental.Rendering.XRSystem.RenderMirrorView(commandBuffer, baseCamera);
					context.ExecuteCommandBuffer(commandBuffer);
					context.Submit();
					global::UnityEngine.Rendering.CommandBufferPool.Release(commandBuffer);
				}
				global::UnityEngine.Experimental.Rendering.XRSystem.EndLayout();
			}
		}

		private static void UpdateCameraData(global::UnityEngine.Rendering.Universal.UniversalCameraData baseCameraData, in global::UnityEngine.Experimental.Rendering.XRPass xr)
		{
			global::UnityEngine.Rect rect = baseCameraData.camera.rect;
			global::UnityEngine.Rect viewport = xr.GetViewport();
			baseCameraData.pixelRect = new global::UnityEngine.Rect(rect.x * viewport.width + viewport.x, rect.y * viewport.height + viewport.y, rect.width * viewport.width, rect.height * viewport.height);
			global::UnityEngine.Rect pixelRect = baseCameraData.pixelRect;
			baseCameraData.pixelWidth = (int)global::System.Math.Round(pixelRect.width + pixelRect.x) - (int)global::System.Math.Round(pixelRect.x);
			baseCameraData.pixelHeight = (int)global::System.Math.Round(pixelRect.height + pixelRect.y) - (int)global::System.Math.Round(pixelRect.y);
			baseCameraData.aspectRatio = (float)baseCameraData.pixelWidth / (float)baseCameraData.pixelHeight;
			global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor = baseCameraData.cameraTargetDescriptor;
			baseCameraData.cameraTargetDescriptor = xr.renderTargetDesc;
			if (baseCameraData.isHdrEnabled)
			{
				baseCameraData.cameraTargetDescriptor.graphicsFormat = cameraTargetDescriptor.graphicsFormat;
			}
			baseCameraData.cameraTargetDescriptor.msaaSamples = cameraTargetDescriptor.msaaSamples;
			if (baseCameraData.isDefaultViewport)
			{
				baseCameraData.cameraTargetDescriptor.useDynamicScale = true;
			}
			else
			{
				baseCameraData.cameraTargetDescriptor.width = baseCameraData.pixelWidth;
				baseCameraData.cameraTargetDescriptor.height = baseCameraData.pixelHeight;
				baseCameraData.cameraTargetDescriptor.useDynamicScale = false;
			}
			baseCameraData.scaledWidth = global::UnityEngine.Mathf.Max(1, (int)((float)baseCameraData.pixelWidth * baseCameraData.renderScale));
			baseCameraData.scaledHeight = global::UnityEngine.Mathf.Max(1, (int)((float)baseCameraData.pixelHeight * baseCameraData.renderScale));
		}

		private static void UpdateVolumeFramework(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.UpdateVolumeFramework)))
			{
				if (!((camera.cameraType == global::UnityEngine.CameraType.SceneView) | (additionalCameraData != null && additionalCameraData.requiresVolumeFrameworkUpdate)) && (bool)additionalCameraData)
				{
					if (additionalCameraData.volumeStack != null && !additionalCameraData.volumeStack.isValid)
					{
						camera.DestroyVolumeStack(additionalCameraData);
					}
					if (additionalCameraData.volumeStack == null)
					{
						camera.UpdateVolumeStack(additionalCameraData);
					}
					global::UnityEngine.Rendering.VolumeManager.instance.stack = additionalCameraData.volumeStack;
				}
				else
				{
					if ((bool)additionalCameraData && additionalCameraData.volumeStack != null)
					{
						camera.DestroyVolumeStack(additionalCameraData);
					}
					camera.GetVolumeLayerMaskAndTrigger(additionalCameraData, out var layerMask, out var trigger);
					global::UnityEngine.Rendering.VolumeManager.instance.ResetMainStack();
					global::UnityEngine.Rendering.VolumeManager.instance.Update(trigger, layerMask);
				}
			}
		}

		private static bool CheckPostProcessForDepth(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (!cameraData.postProcessEnabled)
			{
				return false;
			}
			if (cameraData.IsTemporalAAEnabled() && cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
			{
				return true;
			}
			return CheckPostProcessForDepth();
		}

		private static bool CheckPostProcessForDepth()
		{
			global::UnityEngine.Rendering.VolumeStack stack = global::UnityEngine.Rendering.VolumeManager.instance.stack;
			if (stack.GetComponent<global::UnityEngine.Rendering.Universal.DepthOfField>().IsActive())
			{
				return true;
			}
			if (stack.GetComponent<global::UnityEngine.Rendering.Universal.MotionBlur>().IsActive())
			{
				return true;
			}
			return false;
		}

		private static void SetSupportedRenderingFeatures(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset pipelineAsset)
		{
			global::UnityEngine.Rendering.SupportedRenderingFeatures.active.supportsHDR = pipelineAsset.supportsHDR;
			global::UnityEngine.Rendering.SupportedRenderingFeatures.active.rendersUIOverlay = true;
		}

		private static global::UnityEngine.Rendering.Universal.ScriptableRenderer GetRenderer(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData)
		{
			global::UnityEngine.Rendering.Universal.ScriptableRenderer scriptableRenderer = ((additionalCameraData != null) ? additionalCameraData.scriptableRenderer : null);
			if (scriptableRenderer == null || camera.cameraType == global::UnityEngine.CameraType.SceneView)
			{
				scriptableRenderer = asset.scriptableRenderer;
			}
			return scriptableRenderer;
		}

		internal static void InitializeScaledDimensions(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			cameraData.scaledWidth = global::UnityEngine.Mathf.Max(1, (int)((float)camera.pixelWidth * cameraData.renderScale));
			cameraData.scaledHeight = global::UnityEngine.Mathf.Max(1, (int)((float)camera.pixelHeight * cameraData.renderScale));
		}

		private static global::UnityEngine.Rendering.Universal.UniversalCameraData CreateCameraData(global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.initializeCameraData))
			{
				global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer = GetRenderer(camera, additionalCameraData);
				global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Create<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
				InitializeStackedCameraData(camera, additionalCameraData, universalCameraData);
				universalCameraData.camera = camera;
				universalCameraData.historyManager = additionalCameraData?.historyManager;
				InitializeScaledDimensions(camera, universalCameraData);
				bool flag = renderer?.supportedRenderingFeatures.msaa ?? false;
				int msaaSamples = 1;
				if (camera.allowMSAA && asset.msaaSampleCount > 1 && flag)
				{
					msaaSamples = ((camera.targetTexture != null) ? camera.targetTexture.antiAliasing : asset.msaaSampleCount);
				}
				if (universalCameraData.xrRendering && flag && camera.targetTexture == null)
				{
					msaaSamples = (int)global::UnityEngine.Experimental.Rendering.XRSystem.GetDisplayMSAASamples();
				}
				bool preserveFramebufferAlpha = global::UnityEngine.Graphics.preserveFramebufferAlpha;
				universalCameraData.hdrColorBufferPrecision = (asset ? asset.hdrColorBufferPrecision : global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision._32Bits);
				universalCameraData.cameraTargetDescriptor = CreateRenderTextureDescriptor(camera, universalCameraData, universalCameraData.isHdrEnabled, universalCameraData.hdrColorBufferPrecision, msaaSamples, preserveFramebufferAlpha, universalCameraData.requiresOpaqueTexture);
				global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetAlphaComponentCount(universalCameraData.cameraTargetDescriptor.graphicsFormat);
				universalCameraData.isAlphaOutputEnabled = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.HasAlphaChannel(universalCameraData.cameraTargetDescriptor.graphicsFormat);
				if (universalCameraData.camera.cameraType == global::UnityEngine.CameraType.SceneView && global::UnityEngine.Rendering.CoreUtils.IsSceneFilteringEnabled())
				{
					universalCameraData.isAlphaOutputEnabled = true;
				}
				return universalCameraData;
			}
		}

		private static void InitializeStackedCameraData(global::UnityEngine.Camera baseCamera, global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData baseAdditionalCameraData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.initializeStackedCameraData))
			{
				global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset universalRenderPipelineAsset = asset;
				cameraData.targetTexture = baseCamera.targetTexture;
				cameraData.cameraType = baseCamera.cameraType;
				if (cameraData.isSceneViewCamera)
				{
					cameraData.volumeLayerMask = 1;
					cameraData.volumeTrigger = null;
					cameraData.isStopNaNEnabled = false;
					cameraData.isDitheringEnabled = false;
					cameraData.antialiasing = global::UnityEngine.Rendering.Universal.AntialiasingMode.None;
					cameraData.antialiasingQuality = global::UnityEngine.Rendering.Universal.AntialiasingQuality.High;
					cameraData.xrRendering = false;
					cameraData.allowHDROutput = false;
				}
				else if (baseAdditionalCameraData != null)
				{
					cameraData.volumeLayerMask = baseAdditionalCameraData.volumeLayerMask;
					cameraData.volumeTrigger = ((baseAdditionalCameraData.volumeTrigger == null) ? baseCamera.transform : baseAdditionalCameraData.volumeTrigger);
					cameraData.isStopNaNEnabled = baseAdditionalCameraData.stopNaN && global::UnityEngine.SystemInfo.graphicsShaderLevel >= 35;
					cameraData.isDitheringEnabled = baseAdditionalCameraData.dithering;
					cameraData.antialiasing = baseAdditionalCameraData.antialiasing;
					cameraData.antialiasingQuality = baseAdditionalCameraData.antialiasingQuality;
					cameraData.xrRendering = baseAdditionalCameraData.allowXRRendering && global::UnityEngine.Experimental.Rendering.XRSystem.displayActive;
					cameraData.allowHDROutput = baseAdditionalCameraData.allowHDROutput;
				}
				else
				{
					cameraData.volumeLayerMask = 1;
					cameraData.volumeTrigger = null;
					cameraData.isStopNaNEnabled = false;
					cameraData.isDitheringEnabled = false;
					cameraData.antialiasing = global::UnityEngine.Rendering.Universal.AntialiasingMode.None;
					cameraData.antialiasingQuality = global::UnityEngine.Rendering.Universal.AntialiasingQuality.High;
					cameraData.xrRendering = global::UnityEngine.Experimental.Rendering.XRSystem.displayActive;
					cameraData.allowHDROutput = true;
				}
				cameraData.isHdrEnabled = baseCamera.allowHDR && universalRenderPipelineAsset.supportsHDR;
				cameraData.allowHDROutput &= universalRenderPipelineAsset.supportsHDR;
				global::UnityEngine.Rect rect = baseCamera.rect;
				cameraData.pixelRect = baseCamera.pixelRect;
				cameraData.pixelWidth = baseCamera.pixelWidth;
				cameraData.pixelHeight = baseCamera.pixelHeight;
				cameraData.aspectRatio = (float)cameraData.pixelWidth / (float)cameraData.pixelHeight;
				cameraData.isDefaultViewport = !(global::System.Math.Abs(rect.x) > 0f) && !(global::System.Math.Abs(rect.y) > 0f) && !(global::System.Math.Abs(rect.width) < 1f) && !(global::System.Math.Abs(rect.height) < 1f);
				bool flag = cameraData.cameraType == global::UnityEngine.CameraType.SceneView || cameraData.cameraType == global::UnityEngine.CameraType.Preview || cameraData.cameraType == global::UnityEngine.CameraType.Reflection;
				bool flag2 = !flag;
				bool flag3 = global::UnityEngine.Mathf.Abs(1f - universalRenderPipelineAsset.renderScale) < 0.05f || flag;
				cameraData.renderScale = (flag3 ? 1f : universalRenderPipelineAsset.renderScale);
				cameraData.upscalingFilter = ResolveUpscalingFilterSelection(new global::UnityEngine.Vector2(cameraData.pixelWidth, cameraData.pixelHeight), cameraData.renderScale, universalRenderPipelineAsset.upscalingFilter, enableRenderGraph: true);
				bool flag4 = cameraData.upscalingFilter == global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.STP;
				bool flag5 = cameraData.upscalingFilter == global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.FSR;
				if (cameraData.renderScale > 1f)
				{
					cameraData.imageScalingMode = global::UnityEngine.Rendering.Universal.ImageScalingMode.Downscaling;
				}
				else if (cameraData.renderScale < 1f || (flag2 && (flag4 || flag5)))
				{
					cameraData.imageScalingMode = global::UnityEngine.Rendering.Universal.ImageScalingMode.Upscaling;
					if (flag4)
					{
						cameraData.antialiasing = global::UnityEngine.Rendering.Universal.AntialiasingMode.TemporalAntiAliasing;
					}
				}
				else
				{
					cameraData.imageScalingMode = global::UnityEngine.Rendering.Universal.ImageScalingMode.None;
				}
				cameraData.fsrOverrideSharpness = universalRenderPipelineAsset.fsrOverrideSharpness;
				cameraData.fsrSharpness = universalRenderPipelineAsset.fsrSharpness;
				cameraData.xr = global::UnityEngine.Experimental.Rendering.XRSystem.emptyPass;
				global::UnityEngine.Experimental.Rendering.XRSystem.SetRenderScale(cameraData.renderScale);
				global::UnityEngine.Rendering.SortingCriteria sortingCriteria = global::UnityEngine.Rendering.SortingCriteria.CommonOpaque;
				global::UnityEngine.Rendering.SortingCriteria sortingCriteria2 = global::UnityEngine.Rendering.SortingCriteria.SortingLayer | global::UnityEngine.Rendering.SortingCriteria.RenderQueue | global::UnityEngine.Rendering.SortingCriteria.OptimizeStateChanges | global::UnityEngine.Rendering.SortingCriteria.CanvasOrder;
				bool hasHiddenSurfaceRemovalOnGPU = global::UnityEngine.SystemInfo.hasHiddenSurfaceRemovalOnGPU;
				bool flag6 = (baseCamera.opaqueSortMode == global::UnityEngine.Rendering.OpaqueSortMode.Default && hasHiddenSurfaceRemovalOnGPU) || baseCamera.opaqueSortMode == global::UnityEngine.Rendering.OpaqueSortMode.NoDistanceSort;
				cameraData.defaultOpaqueSortFlags = (flag6 ? sortingCriteria2 : sortingCriteria);
				cameraData.captureActions = global::Unity.RenderPipelines.Core.Runtime.Shared.CameraCaptureBridge.GetCachedCaptureActionsEnumerator(baseCamera);
			}
		}

		private static void InitializeAdditionalCameraData(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData, bool resolveFinalTarget, bool isLastBaseCamera, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.initializeAdditionalCameraData))
			{
				global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer = GetRenderer(camera, additionalCameraData);
				global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset universalRenderPipelineAsset = asset;
				bool flag = universalRenderPipelineAsset.supportsMainLightShadows || universalRenderPipelineAsset.supportsAdditionalLightShadows;
				cameraData.maxShadowDistance = global::UnityEngine.Mathf.Min(universalRenderPipelineAsset.shadowDistance, camera.farClipPlane);
				cameraData.maxShadowDistance = ((flag && cameraData.maxShadowDistance >= camera.nearClipPlane) ? cameraData.maxShadowDistance : 0f);
				if (cameraData.isSceneViewCamera)
				{
					cameraData.renderType = global::UnityEngine.Rendering.Universal.CameraRenderType.Base;
					cameraData.clearDepth = true;
					cameraData.postProcessEnabled = global::UnityEngine.Rendering.CoreUtils.ArePostProcessesEnabled(camera);
					cameraData.requiresDepthTexture = universalRenderPipelineAsset.supportsCameraDepthTexture;
					cameraData.requiresOpaqueTexture = universalRenderPipelineAsset.supportsCameraOpaqueTexture;
					cameraData.useScreenCoordOverride = false;
					cameraData.screenSizeOverride = cameraData.pixelRect.size;
					cameraData.screenCoordScaleBias = global::UnityEngine.Vector2.one;
				}
				else if (additionalCameraData != null)
				{
					cameraData.renderType = additionalCameraData.renderType;
					cameraData.clearDepth = additionalCameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base || additionalCameraData.clearDepth;
					cameraData.postProcessEnabled = additionalCameraData.renderPostProcessing;
					cameraData.maxShadowDistance = (additionalCameraData.renderShadows ? cameraData.maxShadowDistance : 0f);
					cameraData.requiresDepthTexture = additionalCameraData.requiresDepthTexture;
					cameraData.requiresOpaqueTexture = additionalCameraData.requiresColorTexture;
					cameraData.useScreenCoordOverride = additionalCameraData.useScreenCoordOverride;
					cameraData.screenSizeOverride = additionalCameraData.screenSizeOverride;
					cameraData.screenCoordScaleBias = additionalCameraData.screenCoordScaleBias;
				}
				else
				{
					cameraData.renderType = global::UnityEngine.Rendering.Universal.CameraRenderType.Base;
					cameraData.clearDepth = true;
					cameraData.postProcessEnabled = false;
					cameraData.requiresDepthTexture = universalRenderPipelineAsset.supportsCameraDepthTexture;
					cameraData.requiresOpaqueTexture = universalRenderPipelineAsset.supportsCameraOpaqueTexture;
					cameraData.useScreenCoordOverride = false;
					cameraData.screenSizeOverride = cameraData.pixelRect.size;
					cameraData.screenCoordScaleBias = global::UnityEngine.Vector2.one;
				}
				cameraData.renderer = renderer;
				cameraData.postProcessingRequiresDepthTexture = CheckPostProcessForDepth(cameraData);
				cameraData.resolveFinalTarget = resolveFinalTarget;
				cameraData.isLastBaseCamera = isLastBaseCamera;
				int useGPUOcclusionCulling;
				if (global::UnityEngine.Rendering.GPUResidentDrawer.IsInstanceOcclusionCullingEnabled() && renderer.supportsGPUOcclusion)
				{
					global::UnityEngine.CameraType cameraType = camera.cameraType;
					useGPUOcclusionCulling = ((cameraType == global::UnityEngine.CameraType.SceneView || cameraType == global::UnityEngine.CameraType.Game || cameraType == global::UnityEngine.CameraType.Preview) ? 1 : 0);
				}
				else
				{
					useGPUOcclusionCulling = 0;
				}
				cameraData.useGPUOcclusionCulling = (byte)useGPUOcclusionCulling != 0;
				cameraData.requiresDepthTexture |= cameraData.useGPUOcclusionCulling;
				bool num = cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay;
				if (num)
				{
					cameraData.requiresOpaqueTexture = false;
				}
				if (additionalCameraData != null)
				{
					UpdateTemporalAAData(cameraData, additionalCameraData);
				}
				global::UnityEngine.Matrix4x4 projectionMatrix = camera.projectionMatrix;
				if (num && !camera.orthographic && cameraData.pixelRect != camera.pixelRect)
				{
					float m = camera.projectionMatrix.m00 * camera.aspect / cameraData.aspectRatio;
					projectionMatrix.m00 = m;
				}
				ApplyTaaRenderingDebugOverrides(ref cameraData.taaSettings);
				global::UnityEngine.Rendering.Universal.TemporalAA.JitterFunc jitterFunc = ((!cameraData.IsSTPEnabled()) ? global::UnityEngine.Rendering.Universal.TemporalAA.s_JitterFunc : global::UnityEngine.Rendering.Universal.StpUtils.s_JitterFunc);
				global::UnityEngine.Matrix4x4 jitterMatrix = global::UnityEngine.Rendering.Universal.TemporalAA.CalculateJitterMatrix(cameraData, jitterFunc);
				cameraData.SetViewProjectionAndJitterMatrix(camera.worldToCameraMatrix, projectionMatrix, jitterMatrix);
				cameraData.worldSpaceCameraPos = camera.transform.position;
				global::UnityEngine.Color backgroundColor = camera.backgroundColor;
				cameraData.backgroundColor = global::UnityEngine.Rendering.CoreUtils.ConvertSRGBToActiveColorSpace(backgroundColor);
				cameraData.stackAnyPostProcessingEnabled = cameraData.postProcessEnabled;
				cameraData.stackLastCameraOutputToHDR = cameraData.isHDROutputActive;
				bool flag2 = !cameraData.postProcessEnabled || (cameraData.postProcessEnabled && universalRenderPipelineAsset.allowPostProcessAlphaOutput);
				cameraData.isAlphaOutputEnabled &= flag2;
			}
		}

		private static global::UnityEngine.Rendering.Universal.UniversalRenderingData CreateRenderingData(global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset settings, global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.RenderingMode? renderingMode, global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer)
		{
			global::UnityEngine.Rendering.Universal.UniversalLightData universalLightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			universalRenderingData.supportsDynamicBatching = settings.supportsDynamicBatching;
			universalRenderingData.perObjectData = GetPerObjectLightFlags(universalLightData, settings, renderingMode);
			if (renderer is global::UnityEngine.Rendering.Universal.UniversalRenderer universalRenderer)
			{
				universalRenderingData.renderingMode = universalRenderer.renderingModeActual;
				universalRenderingData.prepassLayerMask = universalRenderer.prepassLayerMask;
				universalRenderingData.opaqueLayerMask = universalRenderer.opaqueLayerMask;
				universalRenderingData.transparentLayerMask = universalRenderer.transparentLayerMask;
			}
			universalRenderingData.stencilLodCrossFadeEnabled = settings.enableLODCrossFade && settings.lodCrossFadeDitheringType == global::UnityEngine.Rendering.Universal.LODCrossFadeDitheringType.Stencil;
			return universalRenderingData;
		}

		private static global::UnityEngine.Rendering.Universal.UniversalShadowData CreateShadowData(global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset urpAsset, global::UnityEngine.Rendering.Universal.RenderingMode? renderingMode)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.initializeShadowData))
			{
				global::UnityEngine.Rendering.Universal.UniversalShadowData universalShadowData = frameData.Create<global::UnityEngine.Rendering.Universal.UniversalShadowData>();
				global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
				global::UnityEngine.Rendering.Universal.UniversalLightData universalLightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
				m_ShadowBiasData.Clear();
				m_ShadowResolutionData.Clear();
				universalShadowData.shadowmapDepthBufferBits = 16;
				universalShadowData.mainLightShadowCascadeBorder = urpAsset.cascadeBorder;
				universalShadowData.mainLightShadowCascadesCount = urpAsset.shadowCascadeCount;
				universalShadowData.mainLightShadowCascadesSplit = GetMainLightCascadeSplit(universalShadowData.mainLightShadowCascadesCount, urpAsset);
				universalShadowData.mainLightShadowmapWidth = urpAsset.mainLightShadowmapResolution;
				universalShadowData.mainLightShadowmapHeight = urpAsset.mainLightShadowmapResolution;
				universalShadowData.additionalLightsShadowmapWidth = (universalShadowData.additionalLightsShadowmapHeight = urpAsset.additionalLightsShadowmapResolution);
				universalShadowData.isKeywordAdditionalLightShadowsEnabled = false;
				universalShadowData.isKeywordSoftShadowsEnabled = false;
				universalShadowData.mainLightShadowResolution = 0;
				universalShadowData.mainLightRenderTargetWidth = 0;
				universalShadowData.mainLightRenderTargetHeight = 0;
				universalShadowData.shadowAtlasLayout = default(global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout);
				universalShadowData.visibleLightsShadowCullingInfos = default(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos>);
				int mainLightIndex = universalLightData.mainLightIndex;
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = universalLightData.visibleLights;
				bool flag = universalCameraData.maxShadowDistance > 0f;
				universalShadowData.mainLightShadowsEnabled = urpAsset.supportsMainLightShadows && urpAsset.mainLightRenderingMode == global::UnityEngine.Rendering.Universal.LightRenderingMode.PerPixel;
				universalShadowData.supportsMainLightShadows = global::UnityEngine.SystemInfo.supportsShadows && universalShadowData.mainLightShadowsEnabled && flag;
				bool flag2 = renderingMode.HasValue && renderingMode.Value == global::UnityEngine.Rendering.Universal.RenderingMode.ForwardPlus;
				universalShadowData.additionalLightShadowsEnabled = urpAsset.supportsAdditionalLightShadows && (urpAsset.additionalLightsRenderingMode == global::UnityEngine.Rendering.Universal.LightRenderingMode.PerPixel || flag2);
				universalShadowData.supportsAdditionalLightShadows = global::UnityEngine.SystemInfo.supportsShadows && universalShadowData.additionalLightShadowsEnabled && !universalLightData.shadeAdditionalLightsPerVertex && flag;
				if (!universalShadowData.supportsMainLightShadows && !universalShadowData.supportsAdditionalLightShadows)
				{
					return universalShadowData;
				}
				universalShadowData.supportsMainLightShadows &= mainLightIndex != -1 && visibleLights[mainLightIndex].light != null && visibleLights[mainLightIndex].light.shadows != global::UnityEngine.LightShadows.None;
				if (universalShadowData.supportsAdditionalLightShadows)
				{
					bool flag3 = false;
					for (int i = 0; i < visibleLights.Length; i++)
					{
						if (i == mainLightIndex)
						{
							continue;
						}
						ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAtMutable(i);
						if (reference.lightType == global::UnityEngine.LightType.Spot || reference.lightType == global::UnityEngine.LightType.Point)
						{
							global::UnityEngine.Light light = reference.light;
							if (!(light == null) && light.shadows != global::UnityEngine.LightShadows.None)
							{
								flag3 = true;
								break;
							}
						}
					}
					universalShadowData.supportsAdditionalLightShadows &= flag3;
				}
				if (!universalShadowData.supportsMainLightShadows && !universalShadowData.supportsAdditionalLightShadows)
				{
					return universalShadowData;
				}
				for (int j = 0; j < visibleLights.Length; j++)
				{
					if (!universalShadowData.supportsMainLightShadows && j == mainLightIndex)
					{
						m_ShadowBiasData.Add(global::UnityEngine.Vector4.zero);
						m_ShadowResolutionData.Add(0);
						continue;
					}
					if (!universalShadowData.supportsAdditionalLightShadows && j != mainLightIndex)
					{
						m_ShadowBiasData.Add(global::UnityEngine.Vector4.zero);
						m_ShadowResolutionData.Add(0);
						continue;
					}
					global::UnityEngine.Light light2 = visibleLights.UnsafeElementAtMutable(j).light;
					global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData component = null;
					if (light2 != null)
					{
						light2.gameObject.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData>(out component);
					}
					if ((bool)component && !component.usePipelineSettings)
					{
						m_ShadowBiasData.Add(new global::UnityEngine.Vector4(light2.shadowBias, light2.shadowNormalBias, 0f, 0f));
					}
					else
					{
						m_ShadowBiasData.Add(new global::UnityEngine.Vector4(urpAsset.shadowDepthBias, urpAsset.shadowNormalBias, 0f, 0f));
					}
					if ((bool)component && component.additionalLightsShadowResolutionTier == global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierCustom)
					{
						m_ShadowResolutionData.Add((int)light2.shadowResolution);
					}
					else if ((bool)component && component.additionalLightsShadowResolutionTier != global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierCustom)
					{
						int additionalLightsShadowResolutionTier = global::UnityEngine.Mathf.Clamp(component.additionalLightsShadowResolutionTier, global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierLow, global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierHigh);
						m_ShadowResolutionData.Add(urpAsset.GetAdditionalLightsShadowResolution(additionalLightsShadowResolutionTier));
					}
					else
					{
						m_ShadowResolutionData.Add(urpAsset.GetAdditionalLightsShadowResolution(global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.AdditionalLightsShadowDefaultResolutionTier));
					}
				}
				universalShadowData.bias = m_ShadowBiasData;
				universalShadowData.resolution = m_ShadowResolutionData;
				universalShadowData.supportsSoftShadows = urpAsset.supportsSoftShadows && (universalShadowData.supportsMainLightShadows || universalShadowData.supportsAdditionalLightShadows);
				return universalShadowData;
			}
		}

		private static global::UnityEngine.Rendering.Universal.CullContextData CreateCullContextData(global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.ScriptableRenderContext context)
		{
			global::UnityEngine.Rendering.Universal.CullContextData cullContextData = frameData.Create<global::UnityEngine.Rendering.Universal.CullContextData>();
			cullContextData.SetRenderContext(in context);
			return cullContextData;
		}

		private static global::UnityEngine.Vector3 GetMainLightCascadeSplit(int mainLightShadowCascadesCount, global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset urpAsset)
		{
			return mainLightShadowCascadesCount switch
			{
				1 => new global::UnityEngine.Vector3(1f, 0f, 0f), 
				2 => new global::UnityEngine.Vector3(urpAsset.cascade2Split, 1f, 0f), 
				3 => urpAsset.cascade3Split, 
				_ => urpAsset.cascade4Split, 
			};
		}

		private static void InitializeMainLightShadowResolution(global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			shadowData.mainLightShadowResolution = global::UnityEngine.Rendering.Universal.ShadowUtils.GetMaxTileResolutionInAtlas(shadowData.mainLightShadowmapWidth, shadowData.mainLightShadowmapHeight, shadowData.mainLightShadowCascadesCount);
			shadowData.mainLightRenderTargetWidth = shadowData.mainLightShadowmapWidth;
			shadowData.mainLightRenderTargetHeight = ((shadowData.mainLightShadowCascadesCount == 2) ? (shadowData.mainLightShadowmapHeight >> 1) : shadowData.mainLightShadowmapHeight);
		}

		private static global::UnityEngine.Rendering.Universal.UniversalPostProcessingData CreatePostProcessingData(global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset settings)
		{
			global::UnityEngine.Rendering.Universal.UniversalPostProcessingData universalPostProcessingData = frameData.Create<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			universalPostProcessingData.isEnabled = universalCameraData.stackAnyPostProcessingEnabled;
			universalPostProcessingData.gradingMode = (settings.supportsHDR ? settings.colorGradingMode : global::UnityEngine.Rendering.Universal.ColorGradingMode.LowDynamicRange);
			if (universalCameraData.stackLastCameraOutputToHDR)
			{
				universalPostProcessingData.gradingMode = global::UnityEngine.Rendering.Universal.ColorGradingMode.HighDynamicRange;
			}
			universalPostProcessingData.lutSize = settings.colorGradingLutSize;
			universalPostProcessingData.useFastSRGBLinearConversion = settings.useFastSRGBLinearConversion;
			universalPostProcessingData.supportScreenSpaceLensFlare = settings.supportScreenSpaceLensFlare;
			universalPostProcessingData.supportDataDrivenLensFlare = settings.supportDataDrivenLensFlare;
			return universalPostProcessingData;
		}

		private static global::UnityEngine.Rendering.Universal.UniversalResourceData CreateUniversalResourceData(global::UnityEngine.Rendering.ContextContainer frameData)
		{
			return frameData.Create<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
		}

		private static global::UnityEngine.Rendering.Universal.UniversalLightData CreateLightData(global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset settings, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights, global::UnityEngine.Rendering.Universal.RenderingMode? renderingMode)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.initializeLightData))
			{
				global::UnityEngine.Rendering.Universal.UniversalLightData universalLightData = frameData.Create<global::UnityEngine.Rendering.Universal.UniversalLightData>();
				universalLightData.visibleLights = visibleLights;
				universalLightData.mainLightIndex = GetMainLightIndex(settings, visibleLights);
				if (settings.additionalLightsRenderingMode != global::UnityEngine.Rendering.Universal.LightRenderingMode.Disabled)
				{
					universalLightData.additionalLightsCount = global::System.Math.Min((universalLightData.mainLightIndex != -1) ? (visibleLights.Length - 1) : visibleLights.Length, maxVisibleAdditionalLights);
					universalLightData.maxPerObjectAdditionalLightsCount = global::System.Math.Min(settings.maxAdditionalLightsCount, maxPerObjectLights);
				}
				else
				{
					universalLightData.additionalLightsCount = 0;
					universalLightData.maxPerObjectAdditionalLightsCount = 0;
				}
				universalLightData.supportsAdditionalLights = settings.additionalLightsRenderingMode != global::UnityEngine.Rendering.Universal.LightRenderingMode.Disabled;
				universalLightData.shadeAdditionalLightsPerVertex = settings.additionalLightsRenderingMode == global::UnityEngine.Rendering.Universal.LightRenderingMode.PerVertex;
				universalLightData.supportsMixedLighting = settings.supportsMixedLighting;
				universalLightData.reflectionProbeBoxProjection = settings.reflectionProbeBoxProjection;
				universalLightData.supportsLightLayers = global::UnityEngine.Rendering.Universal.RenderingUtils.SupportsLightLayers(global::UnityEngine.SystemInfo.graphicsDeviceType) && settings.useRenderingLayers;
				universalLightData.reflectionProbeBlending = settings.ShouldUseReflectionProbeBlending();
				universalLightData.reflectionProbeAtlas = renderingMode.HasValue && settings.ShouldUseReflectionProbeAtlasBlending(renderingMode.Value);
				return universalLightData;
			}
		}

		private static void ApplyTaaRenderingDebugOverrides(ref global::UnityEngine.Rendering.Universal.TemporalAA.Settings taaSettings)
		{
			switch (global::UnityEngine.Rendering.DebugDisplaySettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplaySettings>.Instance.renderingSettings.taaDebugMode)
			{
			case global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.TaaDebugMode.ShowClampedHistory:
				taaSettings.m_FrameInfluence = 0f;
				break;
			case global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.TaaDebugMode.ShowRawFrame:
				taaSettings.m_FrameInfluence = 1f;
				break;
			case global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.TaaDebugMode.ShowRawFrameNoJitter:
				taaSettings.m_FrameInfluence = 1f;
				taaSettings.jitterScale = 0f;
				break;
			}
		}

		private static void UpdateTemporalAAData(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData)
		{
			additionalCameraData.historyManager.RequestAccess<global::UnityEngine.Rendering.Universal.TaaHistory>();
			cameraData.taaHistory = additionalCameraData.historyManager.GetHistoryForWrite<global::UnityEngine.Rendering.Universal.TaaHistory>();
			if (cameraData.IsSTPEnabled())
			{
				additionalCameraData.historyManager.RequestAccess<global::UnityEngine.Rendering.Universal.StpHistory>();
				cameraData.stpHistory = additionalCameraData.historyManager.GetHistoryForWrite<global::UnityEngine.Rendering.Universal.StpHistory>();
			}
			ref global::UnityEngine.Rendering.Universal.TemporalAA.Settings taaSettings = ref additionalCameraData.taaSettings;
			cameraData.taaSettings = taaSettings;
			taaSettings.resetHistoryFrames -= ((taaSettings.resetHistoryFrames > 0) ? 1 : 0);
		}

		private static void UpdateTemporalAATargets(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (cameraData.IsTemporalAAEnabled())
			{
				bool flag = false;
				flag = cameraData.xr.enabled && !cameraData.xr.singlePassEnabled;
				bool flag2;
				if (cameraData.IsSTPRequested())
				{
					cameraData.taaHistory.Reset();
					flag2 = cameraData.stpHistory.Update(cameraData);
				}
				else
				{
					flag2 = cameraData.taaHistory.Update(ref cameraData.cameraTargetDescriptor, flag);
				}
				if (flag2)
				{
					cameraData.taaSettings.resetHistoryFrames += ((!flag) ? 1 : 2);
				}
			}
			else
			{
				cameraData.taaHistory.Reset();
				if (cameraData.IsSTPRequested())
				{
					cameraData.stpHistory?.Reset();
				}
			}
		}

		private static void UpdateCameraStereoMatrices(global::UnityEngine.Camera camera, global::UnityEngine.Experimental.Rendering.XRPass xr)
		{
			if (!xr.enabled)
			{
				return;
			}
			if (xr.singlePassEnabled)
			{
				for (int i = 0; i < global::UnityEngine.Mathf.Min(2, xr.viewCount); i++)
				{
					camera.SetStereoProjectionMatrix((global::UnityEngine.Camera.StereoscopicEye)i, xr.GetProjMatrix(i));
					camera.SetStereoViewMatrix((global::UnityEngine.Camera.StereoscopicEye)i, xr.GetViewMatrix(i));
				}
			}
			else
			{
				camera.SetStereoProjectionMatrix((global::UnityEngine.Camera.StereoscopicEye)xr.multipassId, xr.GetProjMatrix());
				camera.SetStereoViewMatrix((global::UnityEngine.Camera.StereoscopicEye)xr.multipassId, xr.GetViewMatrix());
			}
		}

		private static global::UnityEngine.Rendering.PerObjectData GetPerObjectLightFlags(global::UnityEngine.Rendering.Universal.UniversalLightData universalLightData, global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset settings, global::UnityEngine.Rendering.Universal.RenderingMode? renderingMode)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.getPerObjectLightFlags))
			{
				bool flag = settings.ShouldUseReflectionProbeBlending();
				bool flag2 = false;
				if (renderingMode.HasValue)
				{
					flag2 = renderingMode.Value == global::UnityEngine.Rendering.Universal.RenderingMode.ForwardPlus;
				}
				global::UnityEngine.Rendering.PerObjectData perObjectData = global::UnityEngine.Rendering.PerObjectData.LightProbe | global::UnityEngine.Rendering.PerObjectData.Lightmaps | global::UnityEngine.Rendering.PerObjectData.OcclusionProbe | global::UnityEngine.Rendering.PerObjectData.ShadowMask;
				if (!flag2)
				{
					perObjectData |= global::UnityEngine.Rendering.PerObjectData.ReflectionProbes | global::UnityEngine.Rendering.PerObjectData.LightData;
				}
				else if (!flag)
				{
					perObjectData |= global::UnityEngine.Rendering.PerObjectData.ReflectionProbes;
				}
				if (universalLightData.additionalLightsCount > 0 && !flag2 && !global::UnityEngine.Rendering.Universal.RenderingUtils.useStructuredBuffer)
				{
					perObjectData |= global::UnityEngine.Rendering.PerObjectData.LightIndices;
				}
				return perObjectData;
			}
		}

		private static int GetBrightestDirectionalLightIndex(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset settings, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights)
		{
			global::UnityEngine.Light sun = global::UnityEngine.RenderSettings.sun;
			int result = -1;
			float num = 0f;
			int length = visibleLights.Length;
			for (int i = 0; i < length; i++)
			{
				ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAtMutable(i);
				global::UnityEngine.Light light = reference.light;
				if (light == null)
				{
					break;
				}
				if (reference.lightType == global::UnityEngine.LightType.Directional)
				{
					if (light == sun)
					{
						return i;
					}
					if (light.intensity > num)
					{
						num = light.intensity;
						result = i;
					}
				}
			}
			return result;
		}

		private static int GetMainLightIndex(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset settings, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.getMainLightIndex))
			{
				if (visibleLights.Length == 0 || settings.mainLightRenderingMode != global::UnityEngine.Rendering.Universal.LightRenderingMode.PerPixel)
				{
					return -1;
				}
				return GetBrightestDirectionalLightIndex(settings, visibleLights);
			}
		}

		private void SetupPerFrameShaderConstants()
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.setupPerFrameShaderConstants))
			{
				global::UnityEngine.Shader.SetGlobalColor(global::UnityEngine.Rendering.Universal.ShaderPropertyId.rendererColor, global::UnityEngine.Color.white);
				global::UnityEngine.Texture2D texture2D = null;
				switch (asset.lodCrossFadeDitheringType)
				{
				case global::UnityEngine.Rendering.Universal.LODCrossFadeDitheringType.BayerMatrix:
					texture2D = runtimeTextures.bayerMatrixTex;
					break;
				case global::UnityEngine.Rendering.Universal.LODCrossFadeDitheringType.BlueNoise:
					texture2D = runtimeTextures.blueNoise64LTex;
					break;
				case global::UnityEngine.Rendering.Universal.LODCrossFadeDitheringType.Stencil:
					texture2D = runtimeTextures.stencilDitherTex;
					break;
				default:
					global::UnityEngine.Debug.LogWarning($"This Lod Cross Fade Dithering Type is not supported: {asset.lodCrossFadeDitheringType}");
					break;
				}
				if (texture2D != null)
				{
					global::UnityEngine.Shader.SetGlobalFloat(global::UnityEngine.Rendering.Universal.ShaderPropertyId.ditheringTextureInvSize, 1f / (float)texture2D.width);
					global::UnityEngine.Shader.SetGlobalTexture(global::UnityEngine.Rendering.Universal.ShaderPropertyId.ditheringTexture, texture2D);
				}
			}
		}

		private static void SetupPerCameraShaderConstants(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.setupPerCameraShaderConstants))
			{
				global::UnityEngine.Rendering.SphericalHarmonicsL2 ambientProbe = global::UnityEngine.RenderSettings.ambientProbe;
				global::UnityEngine.Color color = global::UnityEngine.Rendering.CoreUtils.ConvertLinearToActiveColorSpace(new global::UnityEngine.Color(ambientProbe[0, 0], ambientProbe[1, 0], ambientProbe[2, 0]) * global::UnityEngine.RenderSettings.reflectionIntensity);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.glossyEnvironmentColor, color);
				cmd.SetGlobalTexture(global::UnityEngine.Rendering.Universal.ShaderPropertyId.glossyEnvironmentCubeMap, global::UnityEngine.ReflectionProbe.defaultTexture);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.glossyEnvironmentCubeMapHDR, global::UnityEngine.ReflectionProbe.defaultTextureHDRDecodeValues);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.ambientSkyColor, global::UnityEngine.Rendering.CoreUtils.ConvertSRGBToActiveColorSpace(global::UnityEngine.RenderSettings.ambientSkyColor));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.ambientEquatorColor, global::UnityEngine.Rendering.CoreUtils.ConvertSRGBToActiveColorSpace(global::UnityEngine.RenderSettings.ambientEquatorColor));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.ambientGroundColor, global::UnityEngine.Rendering.CoreUtils.ConvertSRGBToActiveColorSpace(global::UnityEngine.RenderSettings.ambientGroundColor));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.subtractiveShadowColor, global::UnityEngine.Rendering.CoreUtils.ConvertSRGBToActiveColorSpace(global::UnityEngine.RenderSettings.subtractiveShadowColor));
			}
		}

		private static void CheckAndApplyDebugSettings(ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplaySettings instance = global::UnityEngine.Rendering.DebugDisplaySettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplaySettings>.Instance;
			ref global::UnityEngine.Rendering.Universal.CameraData cameraData = ref renderingData.cameraData;
			if (instance.AreAnySettingsActive && !cameraData.isPreviewCamera)
			{
				global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering renderingSettings = instance.renderingSettings;
				int msaaSamples = cameraData.cameraTargetDescriptor.msaaSamples;
				if (!renderingSettings.enableMsaa)
				{
					msaaSamples = 1;
				}
				if (!renderingSettings.enableHDR)
				{
					cameraData.isHdrEnabled = false;
				}
				if (!instance.IsPostProcessingAllowed)
				{
					cameraData.postProcessEnabled = false;
				}
				cameraData.hdrColorBufferPrecision = (asset ? asset.hdrColorBufferPrecision : global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision._32Bits);
				cameraData.cameraTargetDescriptor.graphicsFormat = MakeRenderTextureGraphicsFormat(cameraData.isHdrEnabled, cameraData.hdrColorBufferPrecision, needsAlpha: true);
				cameraData.cameraTargetDescriptor.msaaSamples = msaaSamples;
			}
		}

		private static global::UnityEngine.Rendering.Universal.ImageUpscalingFilter ResolveUpscalingFilterSelection(global::UnityEngine.Vector2 imageSize, float renderScale, global::UnityEngine.Rendering.Universal.UpscalingFilterSelection selection, bool enableRenderGraph)
		{
			global::UnityEngine.Rendering.Universal.ImageUpscalingFilter result = global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.Linear;
			if ((selection == global::UnityEngine.Rendering.Universal.UpscalingFilterSelection.FSR && !global::UnityEngine.Rendering.FSRUtils.IsSupported()) || (selection == global::UnityEngine.Rendering.Universal.UpscalingFilterSelection.STP && (!global::UnityEngine.Rendering.STP.IsSupported() || !enableRenderGraph)))
			{
				selection = global::UnityEngine.Rendering.Universal.UpscalingFilterSelection.Auto;
			}
			switch (selection)
			{
			case global::UnityEngine.Rendering.Universal.UpscalingFilterSelection.Auto:
			{
				float num = 1f / renderScale;
				if (global::UnityEngine.Mathf.Approximately(num - global::UnityEngine.Mathf.Floor(num), 0f))
				{
					float num2 = imageSize.x / num;
					float num3 = imageSize.y / num;
					if (global::UnityEngine.Mathf.Approximately(num2 - global::UnityEngine.Mathf.Floor(num2), 0f) && global::UnityEngine.Mathf.Approximately(num3 - global::UnityEngine.Mathf.Floor(num3), 0f))
					{
						result = global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.Point;
					}
				}
				break;
			}
			case global::UnityEngine.Rendering.Universal.UpscalingFilterSelection.Point:
				result = global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.Point;
				break;
			case global::UnityEngine.Rendering.Universal.UpscalingFilterSelection.FSR:
				result = global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.FSR;
				break;
			case global::UnityEngine.Rendering.Universal.UpscalingFilterSelection.STP:
				result = global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.STP;
				break;
			}
			return result;
		}

		internal static bool HDROutputForMainDisplayIsActive()
		{
			bool num = global::UnityEngine.SystemInfo.hdrDisplaySupportFlags.HasFlag(global::UnityEngine.HDRDisplaySupportFlags.Supported) && asset.supportsHDR;
			bool flag = global::UnityEngine.HDROutputSettings.main.available && global::UnityEngine.HDROutputSettings.main.active;
			return num && flag;
		}

		internal static bool HDROutputForAnyDisplayIsActive()
		{
			bool flag = HDROutputForMainDisplayIsActive();
			if (global::UnityEngine.Experimental.Rendering.XRSystem.displayActive)
			{
				flag |= global::UnityEngine.Experimental.Rendering.XRSystem.isHDRDisplayOutputActive;
			}
			return flag;
		}

		private void SetHDRState(global::System.Collections.Generic.List<global::UnityEngine.Camera> cameras)
		{
			bool flag = global::UnityEngine.HDROutputSettings.main.available && global::UnityEngine.HDROutputSettings.main.active;
			bool flag2 = flag && global::UnityEngine.HDROutputSettings.main.displayColorGamut != global::UnityEngine.ColorGamut.Rec709;
			bool flag3 = global::UnityEngine.SystemInfo.hdrDisplaySupportFlags.HasFlag(global::UnityEngine.HDRDisplaySupportFlags.RuntimeSwitchable);
			if (!asset.supportsHDR && flag && flag2 && !warnedRuntimeSwitchHDROutputToSDROutput)
			{
				if (flag3)
				{
					global::UnityEngine.Debug.Log("HDR output is being disabled because the current Render Pipeline Asset does not support HDR rendering.");
					global::UnityEngine.HDROutputSettings.main.RequestHDRModeChange(enabled: false);
				}
				else
				{
					global::UnityEngine.Debug.LogWarning("HDR output is active and cannot be switched off at runtime, but the current Render Pipeline Asset does not support HDR rendering. Image may appear underexposed or oversaturated.");
				}
				warnedRuntimeSwitchHDROutputToSDROutput = true;
			}
			if (warnedRuntimeSwitchHDROutputToSDROutput && asset.supportsHDR)
			{
				warnedRuntimeSwitchHDROutputToSDROutput = false;
			}
			if (flag)
			{
				global::UnityEngine.HDROutputSettings.main.automaticHDRTonemapping = false;
			}
		}

		internal static void GetHDROutputLuminanceParameters(global::UnityEngine.Rendering.HDROutputUtils.HDRDisplayInformation hdrDisplayInformation, global::UnityEngine.ColorGamut hdrDisplayColorGamut, global::UnityEngine.Rendering.Universal.Tonemapping tonemapping, out global::UnityEngine.Vector4 hdrOutputParameters)
		{
			float x = hdrDisplayInformation.minToneMapLuminance;
			float y = hdrDisplayInformation.maxToneMapLuminance;
			float num = hdrDisplayInformation.paperWhiteNits;
			if (!tonemapping.detectPaperWhite.value)
			{
				num = tonemapping.paperWhite.value;
			}
			if (!tonemapping.detectBrightnessLimits.value)
			{
				x = tonemapping.minNits.value;
				y = tonemapping.maxNits.value;
			}
			hdrOutputParameters = new global::UnityEngine.Vector4(x, y, num, 1f / num);
		}

		internal static void GetHDROutputGradingParameters(global::UnityEngine.Rendering.Universal.Tonemapping tonemapping, out global::UnityEngine.Vector4 hdrOutputParameters)
		{
			int num = 0;
			float y = 0f;
			switch (tonemapping.mode.value)
			{
			case global::UnityEngine.Rendering.Universal.TonemappingMode.Neutral:
				num = (int)tonemapping.neutralHDRRangeReductionMode.value;
				y = tonemapping.hueShiftAmount.value;
				break;
			case global::UnityEngine.Rendering.Universal.TonemappingMode.ACES:
				num = (int)tonemapping.acesPreset.value;
				break;
			}
			hdrOutputParameters = new global::UnityEngine.Vector4(num, y, 0f, 0f);
		}

		private static void ApplyAdaptivePerformance(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.SortingCriteria defaultOpaqueSortFlags = global::UnityEngine.Rendering.SortingCriteria.SortingLayer | global::UnityEngine.Rendering.SortingCriteria.RenderQueue | global::UnityEngine.Rendering.SortingCriteria.OptimizeStateChanges | global::UnityEngine.Rendering.SortingCriteria.CanvasOrder;
			if (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipFrontToBackSorting)
			{
				cameraData.defaultOpaqueSortFlags = defaultOpaqueSortFlags;
			}
			float maxShadowDistanceMultiplier = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MaxShadowDistanceMultiplier;
			cameraData.maxShadowDistance *= maxShadowDistanceMultiplier;
			float renderScaleMultiplier = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.RenderScaleMultiplier;
			cameraData.renderScale *= renderScaleMultiplier;
			if (!cameraData.xr.enabled)
			{
				cameraData.cameraTargetDescriptor.width = (int)((float)cameraData.camera.pixelWidth * cameraData.renderScale);
				cameraData.cameraTargetDescriptor.height = (int)((float)cameraData.camera.pixelHeight * cameraData.renderScale);
				cameraData.scaledWidth = cameraData.cameraTargetDescriptor.width;
				cameraData.scaledHeight = cameraData.cameraTargetDescriptor.height;
			}
			int num = (int)(cameraData.antialiasingQuality - global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.AntiAliasingQualityBias);
			if (num < 0)
			{
				cameraData.antialiasing = global::UnityEngine.Rendering.Universal.AntialiasingMode.None;
			}
			cameraData.antialiasingQuality = (global::UnityEngine.Rendering.Universal.AntialiasingQuality)global::UnityEngine.Mathf.Clamp(num, 0, 2);
		}

		private static void ApplyAdaptivePerformance(global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalShadowData universalShadowData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>();
			global::UnityEngine.Rendering.Universal.UniversalPostProcessingData universalPostProcessingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();
			if (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.SkipDynamicBatching)
			{
				universalRenderingData.supportsDynamicBatching = false;
			}
			float mainLightShadowmapResolutionMultiplier = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MainLightShadowmapResolutionMultiplier;
			universalShadowData.mainLightShadowmapWidth = (int)((float)universalShadowData.mainLightShadowmapWidth * mainLightShadowmapResolutionMultiplier);
			universalShadowData.mainLightShadowmapHeight = (int)((float)universalShadowData.mainLightShadowmapHeight * mainLightShadowmapResolutionMultiplier);
			int mainLightShadowCascadesCountBias = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.MainLightShadowCascadesCountBias;
			universalShadowData.mainLightShadowCascadesCount = global::UnityEngine.Mathf.Clamp(universalShadowData.mainLightShadowCascadesCount - mainLightShadowCascadesCountBias, 0, 4);
			int shadowQualityBias = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.ShadowQualityBias;
			for (int i = 0; i < shadowQualityBias; i++)
			{
				if (universalShadowData.supportsSoftShadows)
				{
					universalShadowData.supportsSoftShadows = false;
					continue;
				}
				if (universalShadowData.supportsAdditionalLightShadows)
				{
					universalShadowData.supportsAdditionalLightShadows = false;
					continue;
				}
				if (!universalShadowData.supportsMainLightShadows)
				{
					break;
				}
				universalShadowData.supportsMainLightShadows = false;
			}
			if (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.LutBias >= 1f && universalPostProcessingData.lutSize == 32)
			{
				universalPostProcessingData.lutSize = 16;
			}
		}

		private static global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout BuildAdditionalLightsShadowAtlasLayout(global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.Profiling.Pipeline.buildAdditionalLightsShadowAtlasLayout))
			{
				return new global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout(lightData, shadowData, cameraData);
			}
		}

		private static void AdjustUIOverlayOwnership(int cameraCount)
		{
			if (global::UnityEngine.Experimental.Rendering.XRSystem.displayActive || cameraCount == 0)
			{
				global::UnityEngine.Rendering.SupportedRenderingFeatures.active.rendersUIOverlay = false;
			}
			else
			{
				global::UnityEngine.Rendering.SupportedRenderingFeatures.active.rendersUIOverlay = true;
			}
		}

		private static void SetupScreenMSAASamplesState(int cameraCount)
		{
			canOptimizeScreenMSAASamples = cameraCount == 1;
			startFrameScreenMSAASamples = global::UnityEngine.Screen.msaaSamples;
		}

		public static bool IsGameCamera(global::UnityEngine.Camera camera)
		{
			if (camera == null)
			{
				throw new global::System.ArgumentNullException("camera");
			}
			if (camera.cameraType != global::UnityEngine.CameraType.Game)
			{
				return camera.cameraType == global::UnityEngine.CameraType.VR;
			}
			return true;
		}

		private void SortCameras(global::System.Collections.Generic.List<global::UnityEngine.Camera> cameras)
		{
			if (cameras.Count > 1)
			{
				cameras.Sort(cameraComparison);
			}
		}

		private int GetLastBaseCameraIndex(global::System.Collections.Generic.List<global::UnityEngine.Camera> cameras)
		{
			int result = 0;
			for (int i = 0; i < cameras.Count; i++)
			{
				cameras[i].TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component);
				if (component == null || component.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
				{
					result = i;
				}
			}
			return result;
		}

		internal static global::UnityEngine.Experimental.Rendering.GraphicsFormat MakeRenderTextureGraphicsFormat(bool isHdrEnabled, global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision requestHDRColorBufferPrecision, bool needsAlpha)
		{
			if (isHdrEnabled)
			{
				if (!needsAlpha && requestHDRColorBufferPrecision != global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision._64Bits && global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend))
				{
					return global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32;
				}
				if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend))
				{
					return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat;
				}
				return global::UnityEngine.SystemInfo.GetGraphicsFormat(global::UnityEngine.Experimental.Rendering.DefaultFormat.HDR);
			}
			return global::UnityEngine.SystemInfo.GetGraphicsFormat(global::UnityEngine.Experimental.Rendering.DefaultFormat.LDR);
		}

		internal static global::UnityEngine.Experimental.Rendering.GraphicsFormat MakeUnormRenderTextureGraphicsFormat()
		{
			if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.A2B10G10R10_UNormPack32, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend))
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.A2B10G10R10_UNormPack32;
			}
			return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
		}

		internal static global::UnityEngine.RenderTextureDescriptor CreateRenderTextureDescriptor(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool isHdrEnabled, global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision requestHDRColorBufferPrecision, int msaaSamples, bool needsAlpha, bool requiresOpaqueTexture)
		{
			global::UnityEngine.RenderTextureDescriptor renderTextureDescriptor;
			if (camera.targetTexture == null)
			{
				renderTextureDescriptor = new global::UnityEngine.RenderTextureDescriptor(cameraData.scaledWidth, cameraData.scaledHeight);
				renderTextureDescriptor.graphicsFormat = MakeRenderTextureGraphicsFormat(isHdrEnabled, requestHDRColorBufferPrecision, needsAlpha);
				renderTextureDescriptor.depthBufferBits = (int)global::UnityEngine.Rendering.CoreUtils.GetDefaultDepthBufferBits();
				renderTextureDescriptor.depthStencilFormat = global::UnityEngine.SystemInfo.GetGraphicsFormat(global::UnityEngine.Experimental.Rendering.DefaultFormat.DepthStencil);
				renderTextureDescriptor.msaaSamples = msaaSamples;
				renderTextureDescriptor.sRGB = global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear;
			}
			else
			{
				renderTextureDescriptor = camera.targetTexture.descriptor;
				renderTextureDescriptor.msaaSamples = msaaSamples;
				renderTextureDescriptor.width = cameraData.scaledWidth;
				renderTextureDescriptor.height = cameraData.scaledHeight;
				if (camera.cameraType == global::UnityEngine.CameraType.SceneView && !isHdrEnabled)
				{
					renderTextureDescriptor.graphicsFormat = global::UnityEngine.SystemInfo.GetGraphicsFormat(global::UnityEngine.Experimental.Rendering.DefaultFormat.LDR);
				}
			}
			renderTextureDescriptor.enableRandomWrite = false;
			renderTextureDescriptor.bindMS = false;
			renderTextureDescriptor.useDynamicScale = camera.allowDynamicResolution;
			renderTextureDescriptor.msaaSamples = global::UnityEngine.SystemInfo.GetRenderTextureSupportedMSAASampleCount(renderTextureDescriptor);
			if (!global::UnityEngine.SystemInfo.supportsStoreAndResolveAction)
			{
				renderTextureDescriptor.msaaSamples = 1;
			}
			return renderTextureDescriptor;
		}

		public static void GetLightAttenuationAndSpotDirection(global::UnityEngine.LightType lightType, float lightRange, global::UnityEngine.Matrix4x4 lightLocalToWorldMatrix, float spotAngle, float? innerSpotAngle, out global::UnityEngine.Vector4 lightAttenuation, out global::UnityEngine.Vector4 lightSpotDir)
		{
			lightAttenuation = k_DefaultLightAttenuation;
			lightSpotDir = k_DefaultLightSpotDirection;
			if (lightType != global::UnityEngine.LightType.Directional)
			{
				GetPunctualLightDistanceAttenuation(lightRange, ref lightAttenuation);
				if (lightType == global::UnityEngine.LightType.Spot)
				{
					GetSpotDirection(ref lightLocalToWorldMatrix, out lightSpotDir);
					GetSpotAngleAttenuation(spotAngle, innerSpotAngle, ref lightAttenuation);
				}
			}
		}

		internal static void GetPunctualLightDistanceAttenuation(float lightRange, ref global::UnityEngine.Vector4 lightAttenuation)
		{
			float num = lightRange * lightRange;
			float num2 = 0.64000005f * num - num;
			float y = (0f - num) / num2;
			float x = 1f / global::UnityEngine.Mathf.Max(0.0001f, num);
			lightAttenuation.x = x;
			lightAttenuation.y = y;
		}

		internal static void GetSpotAngleAttenuation(float spotAngle, float? innerSpotAngle, ref global::UnityEngine.Vector4 lightAttenuation)
		{
			if ((double)spotAngle < 2.6)
			{
				spotAngle = 2.6f;
				if (innerSpotAngle.HasValue)
				{
					innerSpotAngle = global::UnityEngine.Mathf.Min(innerSpotAngle.Value, 2.6f);
				}
			}
			float num = global::UnityEngine.Mathf.Cos(global::System.MathF.PI / 180f * spotAngle * 0.5f);
			float num2 = ((!innerSpotAngle.HasValue) ? global::UnityEngine.Mathf.Cos(2f * global::UnityEngine.Mathf.Atan(global::UnityEngine.Mathf.Tan(spotAngle * 0.5f * (global::System.MathF.PI / 180f)) * 46f / 64f) * 0.5f) : global::UnityEngine.Mathf.Cos(innerSpotAngle.Value * (global::System.MathF.PI / 180f) * 0.5f));
			float num3 = global::UnityEngine.Mathf.Max(0.001f, num2 - num);
			float num4 = 1f / num3;
			float w = (0f - num) * num4;
			lightAttenuation.z = num4;
			lightAttenuation.w = w;
		}

		internal static void GetSpotDirection(ref global::UnityEngine.Matrix4x4 lightLocalToWorldMatrix, out global::UnityEngine.Vector4 lightSpotDir)
		{
			global::UnityEngine.Vector4 column = lightLocalToWorldMatrix.GetColumn(2);
			lightSpotDir = new global::UnityEngine.Vector4(0f - column.x, 0f - column.y, 0f - column.z, 0f);
		}

		public static void InitializeLightConstants_Common(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> lights, int lightIndex, out global::UnityEngine.Vector4 lightPos, out global::UnityEngine.Vector4 lightColor, out global::UnityEngine.Vector4 lightAttenuation, out global::UnityEngine.Vector4 lightSpotDir, out global::UnityEngine.Vector4 lightOcclusionProbeChannel)
		{
			lightPos = k_DefaultLightPosition;
			lightColor = k_DefaultLightColor;
			lightOcclusionProbeChannel = k_DefaultLightsProbeChannel;
			lightAttenuation = k_DefaultLightAttenuation;
			lightSpotDir = k_DefaultLightSpotDirection;
			if (lightIndex < 0)
			{
				return;
			}
			ref global::UnityEngine.Rendering.VisibleLight reference = ref lights.UnsafeElementAtMutable(lightIndex);
			global::UnityEngine.Light light = reference.light;
			global::UnityEngine.Matrix4x4 lightLocalToWorldMatrix = reference.localToWorldMatrix;
			global::UnityEngine.LightType lightType = reference.lightType;
			if (lightType == global::UnityEngine.LightType.Directional)
			{
				global::UnityEngine.Vector4 vector = -lightLocalToWorldMatrix.GetColumn(2);
				lightPos = new global::UnityEngine.Vector4(vector.x, vector.y, vector.z, 0f);
			}
			else
			{
				global::UnityEngine.Vector4 column = lightLocalToWorldMatrix.GetColumn(3);
				lightPos = new global::UnityEngine.Vector4(column.x, column.y, column.z, 1f);
				GetPunctualLightDistanceAttenuation(reference.range, ref lightAttenuation);
				if (lightType == global::UnityEngine.LightType.Spot)
				{
					GetSpotAngleAttenuation(reference.spotAngle, light?.innerSpotAngle, ref lightAttenuation);
					GetSpotDirection(ref lightLocalToWorldMatrix, out lightSpotDir);
				}
			}
			lightColor = reference.finalColor;
			if (light != null && light.bakingOutput.lightmapBakeType == global::UnityEngine.LightmapBakeType.Mixed && 0 <= light.bakingOutput.occlusionMaskChannel && light.bakingOutput.occlusionMaskChannel < 4)
			{
				lightOcclusionProbeChannel[light.bakingOutput.occlusionMaskChannel] = 1f;
			}
		}

		private static void RecordAndExecuteRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Camera camera, global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy uvOriginStrategy)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraphParameters parameters = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraphParameters
			{
				executionId = camera.GetEntityId(),
				generateDebugData = (camera.cameraType != global::UnityEngine.CameraType.Preview && !camera.isProcessingRenderRequest),
				commandBuffer = cmd,
				scriptableRenderContext = context,
				currentFrameIndex = global::UnityEngine.Time.frameCount,
				renderTextureUVOriginStrategy = uvOriginStrategy
			};
			try
			{
				renderGraph.BeginRecording(in parameters);
				renderer.RecordRenderGraph(renderGraph, context);
				renderGraph.EndRecordingAndExecute();
			}
			catch (global::System.Exception e)
			{
				if (renderGraph.ResetGraphAndLogException(e))
				{
					throw;
				}
			}
		}
	}
}
