namespace UnityEngine.Rendering.Universal.Internal
{
	internal class DeferredPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.Universal.UniversalLightData lightData;

			internal global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] gbuffer;

			internal global::UnityEngine.Rendering.Universal.Internal.DeferredLights deferredLights;
		}

		private global::UnityEngine.Rendering.Universal.Internal.DeferredLights m_DeferredLights;

		public DeferredPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.Universal.Internal.DeferredLights deferredLights)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Render Deferred Lighting");
			base.renderPassEvent = evt;
			m_DeferredLights = deferredLights;
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle color, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depth, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] gbuffer)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>();
			global::UnityEngine.Rendering.Universal.Internal.DeferredPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.DeferredPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DeferredPass.cs", 77);
			passData.cameraData = cameraData;
			passData.lightData = lightData;
			passData.shadowData = shadowData;
			rasterRenderGraphBuilder.SetRenderAttachment(color, 0);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(depth);
			passData.deferredLights = m_DeferredLights;
			if (!m_DeferredLights.UseFramebufferFetch)
			{
				for (int i = 0; i < gbuffer.Length; i++)
				{
					if (i != m_DeferredLights.GBufferLightingIndex)
					{
						rasterRenderGraphBuilder.UseTexture(in gbuffer[i]);
					}
				}
			}
			else
			{
				int num = 0;
				for (int j = 0; j < gbuffer.Length; j++)
				{
					if (j != m_DeferredLights.GBufferLightingIndex)
					{
						rasterRenderGraphBuilder.SetInputAttachment(gbuffer[j], num);
						num++;
					}
				}
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.DeferredPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				data.deferredLights.ExecuteDeferredPass(context.cmd, data.cameraData, data.lightData, data.shadowData);
			});
		}

		public override void OnCameraCleanup(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			m_DeferredLights.OnCameraCleanup(cmd);
		}
	}
}
