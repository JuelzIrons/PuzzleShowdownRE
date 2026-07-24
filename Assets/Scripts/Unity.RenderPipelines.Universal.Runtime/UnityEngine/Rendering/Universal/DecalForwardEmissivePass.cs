namespace UnityEngine.Rendering.Universal
{
	internal class DecalForwardEmissivePass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.DecalDrawFowardEmissiveSystem drawSystem;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;
		}

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> m_ShaderTagIdList;

		private global::UnityEngine.Rendering.Universal.DecalDrawFowardEmissiveSystem m_DrawSystem;

		public DecalForwardEmissivePass(global::UnityEngine.Rendering.Universal.DecalDrawFowardEmissiveSystem drawSystem)
		{
			base.renderPassEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques;
			ConfigureInput(global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth);
			m_DrawSystem = drawSystem;
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw Decal Forward Emissive");
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(global::UnityEngine.Rendering.RenderQueueRange.opaque);
			m_ShaderTagIdList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>();
			m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("DecalMeshForwardEmissive"));
			m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("DecalProjectorForwardEmissive"));
		}

		private void InitPassData(ref global::UnityEngine.Rendering.Universal.DecalForwardEmissivePass.PassData passData)
		{
			passData.drawSystem = m_DrawSystem;
		}

		private global::UnityEngine.Rendering.RendererListParams InitRendererListParams(global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			global::UnityEngine.Rendering.SortingCriteria defaultOpaqueSortFlags = cameraData.defaultOpaqueSortFlags;
			global::UnityEngine.Rendering.DrawingSettings drawSettings = global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(m_ShaderTagIdList, renderingData, cameraData, lightData, defaultOpaqueSortFlags);
			return new global::UnityEngine.Rendering.RendererListParams(renderingData.cullResults, drawSettings, m_FilteringSettings);
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DecalForwardEmissivePass.PassData passData, global::UnityEngine.Rendering.RendererList rendererList)
		{
			passData.drawSystem.Execute(cmd);
			cmd.DrawRendererList(rendererList);
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.DecalForwardEmissivePass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DecalForwardEmissivePass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Decal\\DBuffer\\DecalForwardEmissivePass.cs", 89);
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			InitPassData(ref passData);
			global::UnityEngine.Rendering.RendererListParams desc = InitRendererListParams(renderingData, universalCameraData, lightData);
			passData.rendererList = renderGraph.CreateRendererList(in desc);
			rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
			_ = (global::UnityEngine.Rendering.Universal.UniversalRenderer)universalCameraData.renderer;
			rasterRenderGraphBuilder.SetRenderAttachment(universalResourceData.activeColorTexture, 0);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(universalResourceData.activeDepthTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DecalForwardEmissivePass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
			{
				ExecutePass(rgContext.cmd, data, data.rendererList);
			});
		}
	}
}
