namespace UnityEngine.Rendering.Universal
{
	public class XROcclusionMeshPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Experimental.Rendering.XRPass xr;

			internal bool isActiveTargetBackBuffer;

			internal bool shouldYFlip;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraColorAttachment;
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public bool m_IsActiveTargetBackBuffer;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public XROcclusionMeshPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw XR Occlusion Mesh");
			base.renderPassEvent = evt;
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.XROcclusionMeshPass.PassData data)
		{
			if (data.xr.hasValidOcclusionMesh)
			{
				if (data.isActiveTargetBackBuffer)
				{
					cmd.SetViewport(data.xr.GetViewport());
				}
				data.xr.RenderOcclusionMesh(cmd, data.shouldYFlip);
			}
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraColorAttachment, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepthAttachment)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.XROcclusionMeshPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.XROcclusionMeshPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\XROcclusionMeshPass.cs", 72);
			passData.xr = universalCameraData.xr;
			passData.cameraColorAttachment = cameraColorAttachment;
			rasterRenderGraphBuilder.SetRenderAttachment(cameraColorAttachment, 0);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(cameraDepthAttachment);
			passData.isActiveTargetBackBuffer = universalResourceData.isActiveTargetBackBuffer;
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			if (universalCameraData.xr.enabled)
			{
				bool flag = universalCameraData.xrUniversal.canFoveateIntermediatePasses || universalResourceData.isActiveTargetBackBuffer;
				rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering && flag);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.XROcclusionMeshPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				passData.shouldYFlip = global::UnityEngine.Rendering.Universal.RenderingUtils.IsHandleYFlipped(in context, in data.cameraColorAttachment);
				ExecutePass(context.cmd, data);
			});
		}
	}
}
