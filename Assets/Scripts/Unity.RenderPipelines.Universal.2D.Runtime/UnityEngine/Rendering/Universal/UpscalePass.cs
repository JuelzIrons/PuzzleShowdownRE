namespace UnityEngine.Rendering.Universal
{
	internal class UpscalePass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;
		}

		private static readonly string k_UpscalePass = "Upscale2D Pass";

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(k_UpscalePass);

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ExecuteProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw Upscale");

		private static global::UnityEngine.Material m_BlitMaterial;

		private global::UnityEngine.Rendering.RTHandle source;

		private global::UnityEngine.Rendering.RTHandle destination;

		public UpscalePass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Material blitMaterial)
		{
			base.renderPassEvent = evt;
			m_BlitMaterial = blitMaterial;
		}

		public void Setup(global::UnityEngine.Rendering.RTHandle colorTargetHandle, int width, int height, global::UnityEngine.FilterMode mode, global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor, out global::UnityEngine.Rendering.RTHandle upscaleHandle)
		{
			source = colorTargetHandle;
			global::UnityEngine.RenderTextureDescriptor descriptor = cameraTargetDescriptor;
			descriptor.width = width;
			descriptor.height = height;
			descriptor.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref destination, in descriptor, mode, global::UnityEngine.TextureWrapMode.Clamp, 1, 0f, "_UpscaleTexture");
			upscaleHandle = destination;
		}

		public void Dispose()
		{
			destination?.Release();
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, m_ExecuteProfilingSampler))
			{
				global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, vector, m_BlitMaterial, (source.rt.filterMode == global::UnityEngine.FilterMode.Bilinear) ? 1 : 0);
			}
		}

		public void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Camera camera, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraColorAttachment, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle upscaleHandle)
		{
			camera.TryGetComponent<global::UnityEngine.Rendering.Universal.PixelPerfectCamera>(out var component);
			if (component == null || !component.enabled || !component.requiresUpscalePass)
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.UpscalePass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.UpscalePass.PassData>(k_UpscalePass, out passData, m_ProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\UpscalePass.cs", 73);
			passData.source = cameraColorAttachment;
			rasterRenderGraphBuilder.SetRenderAttachment(upscaleHandle, 0);
			rasterRenderGraphBuilder.UseTexture(in cameraColorAttachment);
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.UpscalePass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				ExecutePass(context.cmd, data.source);
			});
		}
	}
}
