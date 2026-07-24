namespace UnityEngine.Rendering.Universal.Internal
{
	public class DepthOnlyPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;
		}

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private static readonly global::UnityEngine.Rendering.ShaderTagId k_ShaderTagId = new global::UnityEngine.Rendering.ShaderTagId("DepthOnly");

		private static readonly int s_CameraDepthTextureID = global::UnityEngine.Shader.PropertyToID("_CameraDepthTexture");

		internal global::UnityEngine.Rendering.ShaderTagId shaderTagId { get; set; } = k_ShaderTagId;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void OnCameraSetup(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public DepthOnlyPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.RenderQueueRange renderQueueRange, global::UnityEngine.LayerMask layerMask)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw Depth Only");
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(renderQueueRange, layerMask);
			base.renderPassEvent = evt;
			shaderTagId = k_ShaderTagId;
		}

		public void Setup(global::UnityEngine.RenderTextureDescriptor baseDescriptor, global::UnityEngine.Rendering.RTHandle depthAttachmentHandle)
		{
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RendererList rendererList)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.DepthPrepass)))
			{
				cmd.DrawRendererList(rendererList);
			}
		}

		private global::UnityEngine.Rendering.RendererListParams InitRendererListParams(global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			global::UnityEngine.Rendering.SortingCriteria defaultOpaqueSortFlags = cameraData.defaultOpaqueSortFlags;
			global::UnityEngine.Rendering.DrawingSettings drawSettings = global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(shaderTagId, renderingData, cameraData, lightData, defaultOpaqueSortFlags);
			drawSettings.perObjectData = global::UnityEngine.Rendering.PerObjectData.None;
			drawSettings.lodCrossFadeStencilMask = 0;
			return new global::UnityEngine.Rendering.RendererListParams(renderingData.cullResults, drawSettings, m_FilteringSettings);
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTexture, uint batchLayerMask, bool setGlobalDepth)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.Internal.DepthOnlyPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.DepthOnlyPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DepthOnlyPass.cs", 143);
			global::UnityEngine.Rendering.RendererListParams desc = InitRendererListParams(renderingData, universalCameraData, lightData);
			desc.filteringSettings.batchLayerMask = batchLayerMask;
			passData.rendererList = renderGraph.CreateRendererList(in desc);
			rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(depthTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			if (setGlobalDepth)
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in depthTexture, s_CameraDepthTextureID);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			if (universalCameraData.xr.enabled)
			{
				rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering && universalCameraData.xrUniversal.canFoveateIntermediatePasses);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.DepthOnlyPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				ExecutePass(context.cmd, data.rendererList);
			});
		}
	}
}
