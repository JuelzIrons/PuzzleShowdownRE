namespace UnityEngine.Rendering.Universal
{
	public class DrawSkyboxPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Experimental.Rendering.XRPass xr;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle skyRendererListHandle;

			internal global::UnityEngine.Material material;
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public DrawSkyboxPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt)
		{
			base.profilingSampler = global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.DrawSkybox);
			base.renderPassEvent = evt;
		}

		private global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateSkyBoxRendererList(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererListHandle = default(global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle);
			if (cameraData.xr.enabled)
			{
				if (cameraData.xr.singlePassEnabled)
				{
					return renderGraph.CreateSkyboxRendererList(in cameraData.camera, cameraData.GetProjectionMatrix(), cameraData.GetViewMatrix(), cameraData.GetProjectionMatrix(1), cameraData.GetViewMatrix(1));
				}
				return renderGraph.CreateSkyboxRendererList(in cameraData.camera, cameraData.GetProjectionMatrix(), cameraData.GetViewMatrix());
			}
			return renderGraph.CreateSkyboxRendererList(in cameraData.camera);
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Experimental.Rendering.XRPass xr, global::UnityEngine.Rendering.RendererList rendererList)
		{
			if (xr.enabled && xr.singlePassEnabled)
			{
				cmd.SetSinglePassStereo(global::UnityEngine.SystemInfo.supportsMultiview ? global::UnityEngine.Rendering.SinglePassStereoMode.Multiview : global::UnityEngine.Rendering.SinglePassStereoMode.Instancing);
			}
			cmd.DrawRendererList(rendererList);
			if (xr.enabled && xr.singlePassEnabled)
			{
				cmd.SetSinglePassStereo(global::UnityEngine.Rendering.SinglePassStereoMode.None);
			}
		}

		private void InitPassData(ref global::UnityEngine.Rendering.Universal.DrawSkyboxPass.PassData passData, in global::UnityEngine.Experimental.Rendering.XRPass xr, in global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle handle)
		{
			passData.xr = xr;
			passData.skyRendererListHandle = handle;
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorTarget, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTarget, global::UnityEngine.Material skyboxMaterial)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.DebugHandler activeDebugHandler = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(universalCameraData);
			if (activeDebugHandler != null && activeDebugHandler.IsScreenClearNeeded)
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.DrawSkyboxPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DrawSkyboxPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DrawSkyboxPass.cs", 147);
			global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle handle = CreateSkyBoxRendererList(renderGraph, universalCameraData);
			InitPassData(ref passData, universalCameraData.xr, in handle);
			passData.material = skyboxMaterial;
			rasterRenderGraphBuilder.UseRendererList(in handle);
			rasterRenderGraphBuilder.SetRenderAttachment(colorTarget, 0);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(depthTarget);
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			if (universalCameraData.xr.enabled)
			{
				bool flag = universalCameraData.xrUniversal.canFoveateIntermediatePasses || universalResourceData.isActiveTargetBackBuffer;
				rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering && flag);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawSkyboxPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rasterGraphContext)
			{
				ExecutePass(rasterGraphContext.cmd, data.xr, data.skyRendererListHandle);
			});
		}
	}
}
