namespace UnityEngine.Rendering.Universal
{
	internal class DrawScreenSpaceUIPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;
		}

		private class UnsafePassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorTarget;
		}

		private global::UnityEngine.Rendering.RTHandle m_ColorTarget;

		private global::UnityEngine.Rendering.RTHandle m_DepthTarget;

		private bool m_RenderOffscreen;

		public DrawScreenSpaceUIPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, bool renderOffscreen)
		{
			base.profilingSampler = global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.DrawScreenSpaceUI);
			base.renderPassEvent = evt;
			m_RenderOffscreen = renderOffscreen;
		}

		private static void ConfigureColorDescriptor(ref global::UnityEngine.RenderTextureDescriptor descriptor, int cameraWidth, int cameraHeight)
		{
			descriptor.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB;
			descriptor.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			descriptor.width = cameraWidth;
			descriptor.height = cameraHeight;
		}

		internal static void ConfigureOffscreenUITextureDesc(ref global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureDesc)
		{
			textureDesc.format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB;
			textureDesc.depthBufferBits = global::UnityEngine.Rendering.DepthBits.None;
			textureDesc.width = global::UnityEngine.Screen.width;
			textureDesc.height = global::UnityEngine.Screen.height;
		}

		private static void ConfigureDepthDescriptor(ref global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencilFormat, int targetWidth, int targetHeight)
		{
			descriptor.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			descriptor.depthStencilFormat = depthStencilFormat;
			descriptor.width = targetWidth;
			descriptor.height = targetHeight;
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer commandBuffer, global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.PassData passData, global::UnityEngine.Rendering.RendererList rendererList)
		{
			commandBuffer.DrawRendererList(rendererList);
		}

		private static void ExecutePass(global::UnityEngine.Rendering.UnsafeCommandBuffer commandBuffer, global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.UnsafePassData passData, global::UnityEngine.Rendering.RendererList rendererList)
		{
			commandBuffer.DrawRendererList(rendererList);
		}

		public void Dispose()
		{
			m_ColorTarget?.Release();
			m_DepthTarget?.Release();
		}

		public void Setup(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencilFormat)
		{
			if (m_RenderOffscreen)
			{
				global::UnityEngine.RenderTextureDescriptor descriptor = cameraData.cameraTargetDescriptor;
				ConfigureColorDescriptor(ref descriptor, cameraData.pixelWidth, cameraData.pixelHeight);
				global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_ColorTarget, in descriptor, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Repeat, 1, 0f, "_OverlayUITexture");
				global::UnityEngine.RenderTextureDescriptor descriptor2 = cameraData.cameraTargetDescriptor;
				ConfigureDepthDescriptor(ref descriptor2, depthStencilFormat, cameraData.pixelWidth, cameraData.pixelHeight);
				global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_DepthTarget, in descriptor2, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Repeat, 1, 0f, "_OverlayUITexture_Depth");
			}
		}

		internal void RenderOffscreen(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencilFormat, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.RenderTextureDescriptor descriptor = universalCameraData.cameraTargetDescriptor;
			ConfigureDepthDescriptor(ref descriptor, depthStencilFormat, global::UnityEngine.Screen.width, global::UnityEngine.Screen.height);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_OverlayUITexture_Depth", clear: false);
			global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.PassData passData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.PassData>("Draw Screen Space UIToolkit/uGUI - Offscreen", out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DrawScreenSpaceUIPass.cs", 193))
			{
				rasterRenderGraphBuilder.UseAllGlobalTextures(enable: true);
				rasterRenderGraphBuilder.SetRenderAttachment(overlayUITexture, 0);
				passData.rendererList = renderGraph.CreateUIOverlayRendererList(in universalCameraData.camera, global::UnityEngine.Rendering.UISubset.UIToolkit_UGUI);
				rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(tex, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
				if (overlayUITexture.IsValid())
				{
					rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in overlayUITexture, global::UnityEngine.Rendering.Universal.ShaderPropertyId.overlayUITexture);
				}
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
				{
					context.cmd.ClearRenderTarget(clearDepth: true, clearColor: true, global::UnityEngine.Color.clear);
					ExecutePass(context.cmd, data, data.rendererList);
				});
			}
			global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.UnsafePassData passData2;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.UnsafePassData>("Draw Screen Space IMGUI/SoftwareCursor - Offscreen", out passData2, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DrawScreenSpaceUIPass.cs", 218);
			passData2.colorTarget = overlayUITexture;
			unsafeRenderGraphBuilder.UseTexture(in overlayUITexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			passData2.rendererList = renderGraph.CreateUIOverlayRendererList(in universalCameraData.camera, global::UnityEngine.Rendering.UISubset.LowLevel);
			unsafeRenderGraphBuilder.UseRendererList(in passData2.rendererList);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.UnsafePassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				context.cmd.SetRenderTarget(data.colorTarget);
				ExecutePass(context.cmd, data, data.rendererList);
			});
		}

		internal void RenderOverlay(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorBuffer, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthBuffer)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			_ = universalCameraData.renderer;
			global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.PassData passData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.PassData>("Draw UIToolkit/uGUI Overlay", out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DrawScreenSpaceUIPass.cs", 241))
			{
				rasterRenderGraphBuilder.UseAllGlobalTextures(enable: true);
				rasterRenderGraphBuilder.SetRenderAttachment(colorBuffer, 0);
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(depthBuffer, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
				passData.rendererList = renderGraph.CreateUIOverlayRendererList(in universalCameraData.camera, global::UnityEngine.Rendering.UISubset.UIToolkit_UGUI);
				rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
				{
					ExecutePass(context.cmd, data, data.rendererList);
				});
			}
			global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.UnsafePassData passData2;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.UnsafePassData>("Draw IMGUI/SoftwareCursor Overlay", out passData2, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DrawScreenSpaceUIPass.cs", 261);
			passData2.colorTarget = colorBuffer;
			unsafeRenderGraphBuilder.UseTexture(in colorBuffer, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			passData2.rendererList = renderGraph.CreateUIOverlayRendererList(in universalCameraData.camera, global::UnityEngine.Rendering.UISubset.LowLevel);
			unsafeRenderGraphBuilder.UseRendererList(in passData2.rendererList);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawScreenSpaceUIPass.UnsafePassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				context.cmd.SetRenderTarget(data.colorTarget);
				ExecutePass(context.cmd, data, data.rendererList);
			});
		}
	}
}
