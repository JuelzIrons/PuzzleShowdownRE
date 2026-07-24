[global::UnityEngine.Rendering.Universal.DisallowMultipleRendererFeature("On Tile Post Processing (Untethered XR)")]
public class OnTilePostProcessFeature : global::UnityEngine.Rendering.Universal.ScriptableRendererFeature
{
	[global::UnityEngine.SerializeField]
	[global::UnityEngine.HideInInspector]
	private global::UnityEngine.Rendering.Universal.PostProcessData m_PostProcessData;

	private global::UnityEngine.Shader m_UberPostShader;

	private global::UnityEngine.Rendering.Universal.RenderPassEvent postProcessingEvent = (global::UnityEngine.Rendering.Universal.RenderPassEvent)599;

	private global::UnityEngine.Material m_OnTilePostProcessMaterial;

	private global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass m_ColorGradingLutPass;

	private OnTilePostProcessPass m_OnTilePostProcessPass;

	private bool TryLoadResources()
	{
		if (m_UberPostShader == null || m_OnTilePostProcessMaterial == null)
		{
			if (!global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.OnTilePostProcessResource>(out var settings))
			{
				global::UnityEngine.Debug.LogErrorFormat("Couldn't find the required resources for the OnTilePostProcessFeature render feature.");
				return false;
			}
			m_UberPostShader = settings.uberPostShader;
			m_OnTilePostProcessMaterial = new global::UnityEngine.Material(m_UberPostShader);
		}
		return true;
	}

	public override void Create()
	{
		_ = m_PostProcessData == null;
		if (m_PostProcessData != null)
		{
			m_ColorGradingLutPass = new global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass(global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses, m_PostProcessData);
			m_OnTilePostProcessPass = new OnTilePostProcessPass(m_PostProcessData);
			m_OnTilePostProcessPass.requiresIntermediateTexture = true;
		}
	}

	private bool IsRuntimePlatformUntetheredXR()
	{
		return global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android;
	}

	public override void AddRenderPasses(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
	{
		bool flag = true;
		if (renderingData.cameraData.xr.enabled && IsRuntimePlatformUntetheredXR())
		{
			flag = false;
		}
		if (!renderingData.cameraData.postProcessEnabled)
		{
			return;
		}
		if ((renderer as global::UnityEngine.Rendering.Universal.UniversalRenderer).isPostProcessPassRenderGraphActive)
		{
			global::UnityEngine.Debug.LogError("URP renderer(Universal Renderer Data) has post processing enabled, which conflicts with the On-Tile post processing feature. Only one of the post processing should be enabled. On-Tile post processing feature will not be added.");
		}
		else
		{
			if (m_ColorGradingLutPass == null || m_OnTilePostProcessPass == null || !TryLoadResources())
			{
				return;
			}
			global::UnityEngine.Rendering.GraphicsDeviceType graphicsDeviceType = global::UnityEngine.SystemInfo.graphicsDeviceType;
			if (graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.Vulkan && graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.Metal && graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.Direct3D12)
			{
				global::UnityEngine.Debug.LogError("The On-Tile post processing feature is not supported on the graphics devices that don't support frame buffer fetch.");
				return;
			}
			global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.renderTextureUVOriginStrategy = global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy.PropagateAttachmentOrientation;
			m_ColorGradingLutPass.renderPassEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses;
			m_OnTilePostProcessPass.Setup(ref m_OnTilePostProcessMaterial);
			m_OnTilePostProcessPass.renderPassEvent = postProcessingEvent;
			if (flag)
			{
				m_OnTilePostProcessPass.m_UseTextureReadFallback = true;
				global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.renderTextureUVOriginStrategy = global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy.BottomLeft;
			}
			else
			{
				m_OnTilePostProcessPass.m_UseTextureReadFallback = false;
			}
			renderer.EnqueuePass(m_ColorGradingLutPass);
			renderer.EnqueuePass(m_OnTilePostProcessPass);
		}
	}

	protected override void Dispose(bool disposing)
	{
		m_ColorGradingLutPass?.Cleanup();
	}
}
