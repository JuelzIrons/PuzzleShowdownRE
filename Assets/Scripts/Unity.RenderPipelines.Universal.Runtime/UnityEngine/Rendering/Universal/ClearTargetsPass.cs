namespace UnityEngine.Rendering.Universal
{
	internal class ClearTargetsPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle color;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depth;

			internal global::UnityEngine.Rendering.RTClearFlags clearFlags;

			internal global::UnityEngine.Color clearColor;
		}

		private static global::UnityEngine.Rendering.ProfilingSampler s_ClearProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Clear Targets");

		internal static void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorHandle, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthHandle, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.RTClearFlags rTClearFlags = global::UnityEngine.Rendering.RTClearFlags.None;
			if (cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
			{
				rTClearFlags = global::UnityEngine.Rendering.RTClearFlags.All;
			}
			else if (cameraData.clearDepth)
			{
				rTClearFlags = global::UnityEngine.Rendering.RTClearFlags.Depth;
			}
			if (rTClearFlags != global::UnityEngine.Rendering.RTClearFlags.None)
			{
				Render(graph, colorHandle, depthHandle, rTClearFlags, cameraData.backgroundColor);
			}
		}

		internal static void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorHandle, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthHandle, global::UnityEngine.Rendering.RTClearFlags clearFlags, global::UnityEngine.Color clearColor)
		{
			global::UnityEngine.Rendering.Universal.ClearTargetsPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.ClearTargetsPass.PassData>("Clear Targets Pass", out passData, s_ClearProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\UniversalRendererRenderGraph.cs", 2114);
			if (colorHandle.IsValid())
			{
				passData.color = colorHandle;
				rasterRenderGraphBuilder.SetRenderAttachment(colorHandle, 0);
			}
			if (depthHandle.IsValid())
			{
				passData.depth = depthHandle;
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(depthHandle);
			}
			passData.clearFlags = clearFlags;
			passData.clearColor = clearColor;
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ClearTargetsPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				context.cmd.ClearRenderTarget(data.clearFlags, data.clearColor, 1f, 0u);
			});
		}
	}
}
