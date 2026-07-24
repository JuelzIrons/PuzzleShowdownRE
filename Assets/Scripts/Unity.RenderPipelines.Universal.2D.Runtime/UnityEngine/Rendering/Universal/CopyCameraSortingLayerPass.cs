namespace UnityEngine.Rendering.Universal
{
	internal class CopyCameraSortingLayerPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;
		}

		private static readonly string k_CopyCameraSortingLayerPass = "CopyCameraSortingLayer Pass";

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(k_CopyCameraSortingLayerPass);

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ExecuteProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Copy");

		internal static readonly string k_CameraSortingLayerTexture = "_CameraSortingLayerTexture";

		internal static readonly int k_CameraSortingLayerTextureId = global::UnityEngine.Shader.PropertyToID(k_CameraSortingLayerTexture);

		private static global::UnityEngine.Material m_BlitMaterial;

		public CopyCameraSortingLayerPass(global::UnityEngine.Material blitMaterial)
		{
			m_BlitMaterial = blitMaterial;
		}

		public static void ConfigureDescriptor(global::UnityEngine.Rendering.Universal.Downsampling downsamplingMethod, ref global::UnityEngine.RenderTextureDescriptor descriptor, out global::UnityEngine.FilterMode filterMode)
		{
			descriptor.msaaSamples = 1;
			descriptor.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			switch (downsamplingMethod)
			{
			case global::UnityEngine.Rendering.Universal.Downsampling._2xBilinear:
				descriptor.width /= 2;
				descriptor.height /= 2;
				break;
			case global::UnityEngine.Rendering.Universal.Downsampling._4xBox:
			case global::UnityEngine.Rendering.Universal.Downsampling._4xBilinear:
				descriptor.width /= 4;
				descriptor.height /= 4;
				break;
			}
			filterMode = ((downsamplingMethod != global::UnityEngine.Rendering.Universal.Downsampling.None && downsamplingMethod != global::UnityEngine.Rendering.Universal.Downsampling._4xBox) ? global::UnityEngine.FilterMode.Bilinear : global::UnityEngine.FilterMode.Point);
		}

		private static void Execute(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, m_ExecuteProfilingSampler))
			{
				global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, vector, m_BlitMaterial, (source.rt.filterMode == global::UnityEngine.FilterMode.Bilinear) ? 1 : 0);
			}
		}

		public void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.CopyCameraSortingLayerPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.CopyCameraSortingLayerPass.PassData>(k_CopyCameraSortingLayerPass, out passData, m_ProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\CopyCameraSortingLayerPass.cs", 67);
			passData.source = universalResourceData.activeColorTexture;
			rasterRenderGraphBuilder.SetRenderAttachment(universal2DResourceData.cameraSortingLayerTexture, 0);
			rasterRenderGraphBuilder.UseTexture(in passData.source);
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.CopyCameraSortingLayerPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				Execute(context.cmd, data.source);
			});
		}
	}
}
