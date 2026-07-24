namespace UnityEngine.Rendering.Universal
{
	internal class DBufferCopyDepthPass : global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass
	{
		public DBufferCopyDepthPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Shader copyDepthShader, bool shouldClear = false, bool copyToDepth = false, bool copyResolvedDepth = false)
			: base(evt, copyDepthShader, shouldClear, copyToDepth, copyResolvedDepth)
		{
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderer universalRenderer = universalCameraData.renderer as global::UnityEngine.Rendering.Universal.UniversalRenderer;
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo = renderGraph.GetRenderTargetInfo(universalResourceData.activeDepthTexture);
			bool useDepthPriming = universalRenderer.useDepthPriming;
			bool flag = renderTargetInfo.msaaSamples > 1;
			if (!useDepthPriming || flag)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source = ((useDepthPriming || universalRenderer.usesDeferredLighting) ? universalResourceData.cameraDepth : universalResourceData.cameraDepthTexture);
				global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor = universalCameraData.cameraTargetDescriptor;
				cameraTargetDescriptor.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				cameraTargetDescriptor.depthStencilFormat = universalCameraData.cameraTargetDescriptor.depthStencilFormat;
				cameraTargetDescriptor.msaaSamples = 1;
				universalResourceData.dBufferDepth = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, cameraTargetDescriptor, global::UnityEngine.Rendering.Universal.DBufferRenderPass.s_DBufferDepthName, clear: true);
				base.CopyToDepth = true;
				Render(renderGraph, universalResourceData.dBufferDepth, source, universalResourceData, universalCameraData, bindAsCameraDepth: false, "Copy DBuffer Depth");
			}
		}
	}
}
