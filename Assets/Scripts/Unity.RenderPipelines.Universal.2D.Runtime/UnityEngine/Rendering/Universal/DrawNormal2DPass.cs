namespace UnityEngine.Rendering.Universal
{
	internal class DrawNormal2DPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;
		}

		private static readonly string k_NormalPass = "Normal2D Pass";

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(k_NormalPass);

		private static readonly global::UnityEngine.Rendering.ShaderTagId k_NormalsRenderingPassName = new global::UnityEngine.Rendering.ShaderTagId("NormalsRendering");

		private static void Execute(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DrawNormal2DPass.PassData passData)
		{
			cmd.DrawRendererList(passData.rendererList);
		}

		public void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, ref global::UnityEngine.Rendering.Universal.LayerBatch layerBatch, int batchIndex)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			if (!layerBatch.useNormals)
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.DrawNormal2DPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DrawNormal2DPass.PassData>(k_NormalPass, out passData, m_ProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\DrawNormal2DPass.cs", 44);
			global::UnityEngine.Rendering.Universal.LayerUtility.GetFilterSettings(rendererData, ref layerBatch, out var filterSettings);
			global::UnityEngine.Rendering.DrawingSettings drawSettings = CreateDrawingSettings(k_NormalsRenderingPassName, universalRenderingData, universalCameraData, lightData, global::UnityEngine.Rendering.SortingCriteria.CommonTransparent);
			global::UnityEngine.Rendering.SortingSettings sortingSettings = drawSettings.sortingSettings;
			global::UnityEngine.Rendering.Universal.RendererLighting.GetTransparencySortingMode(rendererData, universalCameraData.camera, ref sortingSettings);
			drawSettings.sortingSettings = sortingSettings;
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			rasterRenderGraphBuilder.SetRenderAttachment(universal2DResourceData.normalsTexture[batchIndex], 0);
			if (global::UnityEngine.Rendering.Universal.Renderer2D.IsDepthUsageAllowed(frameData, rendererData))
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex = (universal2DResourceData.normalsDepth.IsValid() ? universal2DResourceData.normalsDepth : universalResourceData.activeDepthTexture);
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(tex);
			}
			global::UnityEngine.Rendering.RendererListParams desc = new global::UnityEngine.Rendering.RendererListParams(universalRenderingData.cullResults, drawSettings, filterSettings);
			passData.rendererList = graph.CreateRendererList(in desc);
			rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawNormal2DPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				Execute(context.cmd, data);
			});
		}
	}
}
