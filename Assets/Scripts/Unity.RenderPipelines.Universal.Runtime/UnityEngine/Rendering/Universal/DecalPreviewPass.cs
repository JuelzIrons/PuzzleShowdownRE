namespace UnityEngine.Rendering.Universal
{
	internal class DecalPreviewPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;
		}

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> m_ShaderTagIdList;

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler;

		public DecalPreviewPass()
		{
			base.renderPassEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques;
			ConfigureInput(global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth);
			m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Decal Preview Render");
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(global::UnityEngine.Rendering.RenderQueueRange.opaque);
			m_ShaderTagIdList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>();
			m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("DecalScreenSpaceMesh"));
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DecalPreviewPass.PassData passData, global::UnityEngine.Rendering.RendererList rendererList)
		{
			cmd.DrawRendererList(rendererList);
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.DecalPreviewPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DecalPreviewPass.PassData>("Decal Preview Pass", out passData, m_ProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Decal\\DecalPreviewPass.cs", 68);
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			_ = (global::UnityEngine.Rendering.Universal.UniversalRenderer)universalCameraData.renderer;
			rasterRenderGraphBuilder.SetRenderAttachment(universalResourceData.activeColorTexture, 0);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(universalResourceData.activeDepthTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);
			global::UnityEngine.Rendering.SortingCriteria defaultOpaqueSortFlags = universalCameraData.defaultOpaqueSortFlags;
			global::UnityEngine.Rendering.DrawingSettings drawSettings = global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(m_ShaderTagIdList, universalRenderingData, universalCameraData, lightData, defaultOpaqueSortFlags);
			global::UnityEngine.Rendering.RendererListParams desc = new global::UnityEngine.Rendering.RendererListParams(universalRenderingData.cullResults, drawSettings, m_FilteringSettings);
			passData.rendererList = renderGraph.CreateRendererList(in desc);
			rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DecalPreviewPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
			{
				ExecutePass(rgContext.cmd, data, data.rendererList);
			});
		}
	}
}
