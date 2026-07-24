#define ENABLE_ADAPTIVE_PERFORMANCE
namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.Rendering.Universal.SupportedOnRenderer(typeof(global::UnityEngine.Rendering.Universal.UniversalRendererData))]
	[global::UnityEngine.Rendering.Universal.DisallowMultipleRendererFeature("Decal")]
	[global::UnityEngine.Tooltip("With this Renderer Feature, Unity can project specific Materials (decals) onto other objects in the Scene.")]
	public class DecalRendererFeature : global::UnityEngine.Rendering.Universal.ScriptableRendererFeature
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.DecalSettings m_Settings = new global::UnityEngine.Rendering.Universal.DecalSettings();

		private global::UnityEngine.Rendering.Universal.DecalTechnique m_Technique;

		private global::UnityEngine.Rendering.Universal.DBufferSettings m_DBufferSettings;

		private global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings m_ScreenSpaceSettings;

		private bool m_RecreateSystems;

		private global::UnityEngine.Rendering.Universal.DecalPreviewPass m_DecalPreviewPass;

		private global::UnityEngine.Rendering.Universal.DecalEntityManager m_DecalEntityManager;

		private global::UnityEngine.Rendering.Universal.DecalUpdateCachedSystem m_DecalUpdateCachedSystem;

		private global::UnityEngine.Rendering.Universal.DecalUpdateCullingGroupSystem m_DecalUpdateCullingGroupSystem;

		private global::UnityEngine.Rendering.Universal.DecalUpdateCulledSystem m_DecalUpdateCulledSystem;

		private global::UnityEngine.Rendering.Universal.DecalCreateDrawCallSystem m_DecalCreateDrawCallSystem;

		private global::UnityEngine.Rendering.Universal.DecalDrawErrorSystem m_DrawErrorSystem;

		private global::UnityEngine.Rendering.Universal.DBufferCopyDepthPass m_CopyDepthPass;

		private global::UnityEngine.Rendering.Universal.DBufferRenderPass m_DBufferRenderPass;

		private global::UnityEngine.Rendering.Universal.DecalForwardEmissivePass m_ForwardEmissivePass;

		private global::UnityEngine.Rendering.Universal.DecalDrawDBufferSystem m_DecalDrawDBufferSystem;

		private global::UnityEngine.Rendering.Universal.DecalDrawFowardEmissiveSystem m_DecalDrawForwardEmissiveSystem;

		private global::UnityEngine.Material m_DBufferClearMaterial;

		private global::UnityEngine.Rendering.Universal.DecalScreenSpaceRenderPass m_ScreenSpaceDecalRenderPass;

		private global::UnityEngine.Rendering.Universal.DecalDrawScreenSpaceSystem m_DecalDrawScreenSpaceSystem;

		private global::UnityEngine.Rendering.Universal.DecalSkipCulledSystem m_DecalSkipCulledSystem;

		private global::UnityEngine.Rendering.Universal.DecalGBufferRenderPass m_GBufferRenderPass;

		private global::UnityEngine.Rendering.Universal.DecalDrawGBufferSystem m_DrawGBufferSystem;

		private global::UnityEngine.Rendering.Universal.Internal.DeferredLights m_DeferredLights;

		private static global::UnityEngine.Rendering.Universal.SharedDecalEntityManager sharedDecalEntityManager { get; } = new global::UnityEngine.Rendering.Universal.SharedDecalEntityManager();

		internal ref global::UnityEngine.Rendering.Universal.DecalSettings settings => ref m_Settings;

		internal bool intermediateRendering => m_Technique == global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer;

		internal bool requiresDecalLayers => m_Settings.decalLayers;

		internal static bool isGLDevice
		{
			get
			{
				if (global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3)
				{
					return global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore;
				}
				return true;
			}
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void SetupRenderPasses(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, in global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public override void Create()
		{
			m_DecalPreviewPass = new global::UnityEngine.Rendering.Universal.DecalPreviewPass();
			m_RecreateSystems = true;
		}

		internal override bool RequireRenderingLayers(bool isDeferred, bool needsGBufferAccurateNormals, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event atEvent, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize)
		{
			bool isPlaying = global::UnityEngine.Application.isPlaying;
			global::UnityEngine.Rendering.Universal.DecalTechnique technique = GetTechnique(isDeferred, needsGBufferAccurateNormals, isPlaying);
			atEvent = ((technique != global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer) ? global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.Opaque : global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.DepthNormalPrePass);
			maskSize = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits8;
			return requiresDecalLayers;
		}

		internal global::UnityEngine.Rendering.Universal.DBufferSettings GetDBufferSettings()
		{
			if (m_Settings.technique == global::UnityEngine.Rendering.Universal.DecalTechniqueOption.Automatic)
			{
				return new global::UnityEngine.Rendering.Universal.DBufferSettings
				{
					surfaceData = global::UnityEngine.Rendering.Universal.DecalSurfaceData.AlbedoNormalMAOS
				};
			}
			return m_Settings.dBufferSettings;
		}

		internal global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings GetScreenSpaceSettings()
		{
			if (m_Settings.technique == global::UnityEngine.Rendering.Universal.DecalTechniqueOption.Automatic)
			{
				return new global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings
				{
					normalBlend = global::UnityEngine.Rendering.Universal.DecalNormalBlend.Low
				};
			}
			return m_Settings.screenSpaceSettings;
		}

		internal global::UnityEngine.Rendering.Universal.DecalTechnique GetTechnique(global::UnityEngine.Rendering.Universal.ScriptableRendererData renderer)
		{
			global::UnityEngine.Rendering.Universal.UniversalRendererData universalRendererData = renderer as global::UnityEngine.Rendering.Universal.UniversalRendererData;
			if (universalRendererData == null)
			{
				global::UnityEngine.Debug.LogError("Only universal renderer supports Decal renderer feature.");
				return global::UnityEngine.Rendering.Universal.DecalTechnique.Invalid;
			}
			bool flag = universalRendererData.renderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.Deferred;
			flag |= universalRendererData.renderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus;
			return GetTechnique(flag, universalRendererData.accurateGbufferNormals);
		}

		internal global::UnityEngine.Rendering.Universal.DecalTechnique GetTechnique(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer)
		{
			if (!(renderer is global::UnityEngine.Rendering.Universal.UniversalRenderer universalRenderer))
			{
				global::UnityEngine.Debug.LogError("Only universal renderer supports Decal renderer feature.");
				return global::UnityEngine.Rendering.Universal.DecalTechnique.Invalid;
			}
			return GetTechnique(universalRenderer.usesDeferredLighting, universalRenderer.accurateGbufferNormals);
		}

		internal global::UnityEngine.Rendering.Universal.DecalTechnique GetTechnique(bool isDeferred, bool needsGBufferAccurateNormals, bool checkForInvalidTechniques = true)
		{
			global::UnityEngine.Rendering.Universal.DecalTechnique decalTechnique = global::UnityEngine.Rendering.Universal.DecalTechnique.Invalid;
			switch (m_Settings.technique)
			{
			case global::UnityEngine.Rendering.Universal.DecalTechniqueOption.Automatic:
				decalTechnique = ((!isGLDevice) ? ((IsAutomaticDBuffer() || (isDeferred && needsGBufferAccurateNormals)) ? global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer : ((!isDeferred) ? global::UnityEngine.Rendering.Universal.DecalTechnique.ScreenSpace : global::UnityEngine.Rendering.Universal.DecalTechnique.GBuffer)) : (isDeferred ? global::UnityEngine.Rendering.Universal.DecalTechnique.GBuffer : global::UnityEngine.Rendering.Universal.DecalTechnique.ScreenSpace));
				break;
			case global::UnityEngine.Rendering.Universal.DecalTechniqueOption.ScreenSpace:
				decalTechnique = ((!isDeferred) ? global::UnityEngine.Rendering.Universal.DecalTechnique.ScreenSpace : global::UnityEngine.Rendering.Universal.DecalTechnique.GBuffer);
				break;
			case global::UnityEngine.Rendering.Universal.DecalTechniqueOption.DBuffer:
				decalTechnique = global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer;
				break;
			}
			if (!checkForInvalidTechniques)
			{
				return decalTechnique;
			}
			if (decalTechnique == global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer && isGLDevice)
			{
				global::UnityEngine.Debug.LogError("Decal DBuffer technique is not supported with OpenGL.");
				return global::UnityEngine.Rendering.Universal.DecalTechnique.Invalid;
			}
			bool flag = global::UnityEngine.SystemInfo.supportedRenderTargetCount >= 4;
			if (decalTechnique == global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer && !flag)
			{
				global::UnityEngine.Debug.LogError("Decal DBuffer technique requires MRT4 support.");
				return global::UnityEngine.Rendering.Universal.DecalTechnique.Invalid;
			}
			if (decalTechnique == global::UnityEngine.Rendering.Universal.DecalTechnique.GBuffer && !flag)
			{
				global::UnityEngine.Debug.LogError("Decal useGBuffer option requires MRT4 support.");
				return global::UnityEngine.Rendering.Universal.DecalTechnique.Invalid;
			}
			return decalTechnique;
		}

		private bool IsAutomaticDBuffer()
		{
			if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.WebGLPlayer)
			{
				return false;
			}
			return !global::UnityEngine.Rendering.Universal.PlatformAutoDetect.isShaderAPIMobileDefined;
		}

		private bool RecreateSystemsIfNeeded(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, in global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			if (!m_RecreateSystems)
			{
				return true;
			}
			m_Technique = GetTechnique(renderer);
			if (m_Technique == global::UnityEngine.Rendering.Universal.DecalTechnique.Invalid)
			{
				return false;
			}
			m_DBufferSettings = GetDBufferSettings();
			m_ScreenSpaceSettings = GetScreenSpaceSettings();
			global::UnityEngine.Rendering.Universal.UniversalRendererResources renderPipelineSettings = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRendererResources>();
			if (renderPipelineSettings == null)
			{
				return false;
			}
			m_DBufferClearMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(renderPipelineSettings.decalDBufferClear);
			if (m_DecalEntityManager == null)
			{
				m_DecalEntityManager = sharedDecalEntityManager.Get();
			}
			m_DecalUpdateCachedSystem = new global::UnityEngine.Rendering.Universal.DecalUpdateCachedSystem(m_DecalEntityManager);
			m_DecalUpdateCulledSystem = new global::UnityEngine.Rendering.Universal.DecalUpdateCulledSystem(m_DecalEntityManager);
			m_DecalCreateDrawCallSystem = new global::UnityEngine.Rendering.Universal.DecalCreateDrawCallSystem(m_DecalEntityManager, m_Settings.maxDrawDistance);
			if (intermediateRendering)
			{
				m_DecalUpdateCullingGroupSystem = new global::UnityEngine.Rendering.Universal.DecalUpdateCullingGroupSystem(m_DecalEntityManager, m_Settings.maxDrawDistance);
			}
			else
			{
				m_DecalSkipCulledSystem = new global::UnityEngine.Rendering.Universal.DecalSkipCulledSystem(m_DecalEntityManager);
			}
			m_DrawErrorSystem = new global::UnityEngine.Rendering.Universal.DecalDrawErrorSystem(m_DecalEntityManager, m_Technique);
			global::UnityEngine.Rendering.Universal.UniversalRenderer universalRenderer = renderer as global::UnityEngine.Rendering.Universal.UniversalRenderer;
			switch (m_Technique)
			{
			case global::UnityEngine.Rendering.Universal.DecalTechnique.ScreenSpace:
				m_DecalDrawScreenSpaceSystem = new global::UnityEngine.Rendering.Universal.DecalDrawScreenSpaceSystem(m_DecalEntityManager);
				m_ScreenSpaceDecalRenderPass = new global::UnityEngine.Rendering.Universal.DecalScreenSpaceRenderPass(m_ScreenSpaceSettings, intermediateRendering ? m_DecalDrawScreenSpaceSystem : null, m_Settings.decalLayers);
				break;
			case global::UnityEngine.Rendering.Universal.DecalTechnique.GBuffer:
				m_DeferredLights = universalRenderer.deferredLights;
				m_DrawGBufferSystem = new global::UnityEngine.Rendering.Universal.DecalDrawGBufferSystem(m_DecalEntityManager);
				m_GBufferRenderPass = new global::UnityEngine.Rendering.Universal.DecalGBufferRenderPass(m_ScreenSpaceSettings, intermediateRendering ? m_DrawGBufferSystem : null, m_Settings.decalLayers);
				break;
			case global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer:
				m_CopyDepthPass = new global::UnityEngine.Rendering.Universal.DBufferCopyDepthPass((global::UnityEngine.Rendering.Universal.RenderPassEvent)201, renderPipelineSettings.copyDepthPS, shouldClear: false, !universalRenderer.usesDeferredLighting);
				m_DecalDrawDBufferSystem = new global::UnityEngine.Rendering.Universal.DecalDrawDBufferSystem(m_DecalEntityManager);
				m_DBufferRenderPass = new global::UnityEngine.Rendering.Universal.DBufferRenderPass(m_DBufferClearMaterial, m_DBufferSettings, m_DecalDrawDBufferSystem, m_Settings.decalLayers);
				m_DecalDrawForwardEmissiveSystem = new global::UnityEngine.Rendering.Universal.DecalDrawFowardEmissiveSystem(m_DecalEntityManager);
				m_ForwardEmissivePass = new global::UnityEngine.Rendering.Universal.DecalForwardEmissivePass(m_DecalDrawForwardEmissiveSystem);
				break;
			}
			m_RecreateSystems = false;
			return true;
		}

		public override void OnCameraPreCull(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, in global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			if (cameraData.cameraType == global::UnityEngine.CameraType.Preview || !RecreateSystemsIfNeeded(renderer, in cameraData))
			{
				return;
			}
			ChangeAdaptivePerformanceDrawDistances();
			m_DecalEntityManager.Update();
			m_DecalUpdateCachedSystem.Execute();
			if (intermediateRendering)
			{
				m_DecalUpdateCullingGroupSystem.Execute(cameraData.camera);
			}
			else
			{
				m_DecalSkipCulledSystem.Execute(cameraData.camera);
				m_DecalCreateDrawCallSystem.Execute();
				if (m_Technique == global::UnityEngine.Rendering.Universal.DecalTechnique.ScreenSpace)
				{
					m_DecalDrawScreenSpaceSystem.Execute(in cameraData);
				}
				else if (m_Technique == global::UnityEngine.Rendering.Universal.DecalTechnique.GBuffer)
				{
					m_DrawGBufferSystem.Execute(in cameraData);
				}
			}
			m_DrawErrorSystem.Execute(in cameraData);
		}

		public override void AddRenderPasses(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
			if (global::UnityEngine.Rendering.Universal.UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData))
			{
				return;
			}
			if (renderingData.cameraData.cameraType == global::UnityEngine.CameraType.Preview)
			{
				renderer.EnqueuePass(m_DecalPreviewPass);
			}
			else
			{
				if (!RecreateSystemsIfNeeded(renderer, in renderingData.cameraData))
				{
					return;
				}
				ChangeAdaptivePerformanceDrawDistances();
				if (intermediateRendering)
				{
					m_DecalUpdateCulledSystem.Execute();
					m_DecalCreateDrawCallSystem.Execute();
				}
				if (m_Technique == global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer)
				{
					if ((renderer as global::UnityEngine.Rendering.Universal.UniversalRenderer).usesDeferredLighting)
					{
						m_CopyDepthPass.CopyToDepth = false;
					}
					else
					{
						m_CopyDepthPass.CopyToDepth = true;
						m_CopyDepthPass.MsaaSamples = 1;
					}
				}
				switch (m_Technique)
				{
				case global::UnityEngine.Rendering.Universal.DecalTechnique.ScreenSpace:
					renderer.EnqueuePass(m_ScreenSpaceDecalRenderPass);
					break;
				case global::UnityEngine.Rendering.Universal.DecalTechnique.GBuffer:
					m_GBufferRenderPass.Setup(m_DeferredLights);
					renderer.EnqueuePass(m_GBufferRenderPass);
					break;
				case global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer:
					renderer.EnqueuePass(m_CopyDepthPass);
					renderer.EnqueuePass(m_DBufferRenderPass);
					renderer.EnqueuePass(m_ForwardEmissivePass);
					break;
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			m_CopyDepthPass?.Dispose();
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_DBufferClearMaterial);
			if (m_DecalEntityManager != null)
			{
				m_DecalEntityManager = null;
				sharedDecalEntityManager.Release(m_DecalEntityManager);
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_ADAPTIVE_PERFORMANCE")]
		private void ChangeAdaptivePerformanceDrawDistances()
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset;
			if ((object)asset != null && asset.useAdaptivePerformance)
			{
				if (m_DecalCreateDrawCallSystem != null)
				{
					m_DecalCreateDrawCallSystem.maxDrawDistance = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.DecalsDrawDistance;
				}
				if (m_DecalUpdateCullingGroupSystem != null)
				{
					m_DecalUpdateCullingGroupSystem.boundingDistance = global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.DecalsDrawDistance;
				}
			}
		}
	}
}
