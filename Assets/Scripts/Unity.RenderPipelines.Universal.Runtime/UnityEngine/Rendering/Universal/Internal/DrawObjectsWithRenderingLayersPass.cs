namespace UnityEngine.Rendering.Universal.Internal
{
	internal class DrawObjectsWithRenderingLayersPass : global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass
	{
		private class RenderingLayersPassData
		{
			internal global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.PassData basePassData;

			internal global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize;

			public RenderingLayersPassData()
			{
				basePassData = new global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.PassData();
			}
		}

		public DrawObjectsWithRenderingLayersPass(global::UnityEngine.Rendering.Universal.URPProfileId profilerTag, bool opaque, global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.RenderQueueRange renderQueueRange, global::UnityEngine.LayerMask layerMask, global::UnityEngine.Rendering.StencilState stencilState, int stencilReference)
			: base(profilerTag, opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference)
		{
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorTarget, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle renderingLayersTexture, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTarget, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle mainShadowsTexture, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle additionalShadowsTexture, global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize, uint batchLayerMask = uint.MaxValue)
		{
			global::UnityEngine.Rendering.Universal.Internal.DrawObjectsWithRenderingLayersPass.RenderingLayersPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.DrawObjectsWithRenderingLayersPass.RenderingLayersPassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DrawObjectsPass.cs", 468);
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			InitPassData(universalCameraData, ref passData.basePassData, batchLayerMask);
			passData.maskSize = maskSize;
			passData.basePassData.albedoHdl = colorTarget;
			rasterRenderGraphBuilder.SetRenderAttachment(colorTarget, 0);
			rasterRenderGraphBuilder.SetRenderAttachment(renderingLayersTexture, 1);
			bool flag = global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.CanDisableZWrite(universalCameraData, passData.basePassData.isOpaque);
			global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = (flag ? global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read : global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.basePassData.depthHdl = depthTarget;
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(depthTarget, flags);
			if (mainShadowsTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in mainShadowsTexture);
			}
			if (additionalShadowsTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in additionalShadowsTexture);
			}
			if (universalCameraData.renderer is global::UnityEngine.Rendering.Universal.UniversalRenderer)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ssaoTexture = universalResourceData.ssaoTexture;
				if (ssaoTexture.IsValid())
				{
					rasterRenderGraphBuilder.UseTexture(in ssaoTexture);
				}
				global::UnityEngine.Rendering.Universal.RenderGraphUtils.UseDBufferIfValid(rasterRenderGraphBuilder, universalResourceData);
			}
			InitRendererLists(renderingData, universalCameraData, lightData, ref passData.basePassData, default(global::UnityEngine.Rendering.ScriptableRenderContext), renderGraph, useRenderGraph: true, flag);
			if (global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(universalCameraData) != null)
			{
				passData.basePassData.debugRendererLists.PrepareRendererListForRasterPass(rasterRenderGraphBuilder);
			}
			else
			{
				rasterRenderGraphBuilder.UseRendererList(in passData.basePassData.rendererListHdl);
				rasterRenderGraphBuilder.UseRendererList(in passData.basePassData.objectsWithErrorRendererListHdl);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			if (universalCameraData.xr.enabled)
			{
				bool flag2 = universalCameraData.xrUniversal.canFoveateIntermediatePasses || universalResourceData.isActiveTargetBackBuffer;
				rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering && flag2);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.DrawObjectsWithRenderingLayersPass.RenderingLayersPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				context.cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.WriteRenderingLayers, value: true);
				global::UnityEngine.Rendering.Universal.RenderingLayerUtils.SetupProperties(context.cmd, data.maskSize);
				if (!data.basePassData.isOpaque && !data.basePassData.shouldTransparentsReceiveShadows)
				{
					global::UnityEngine.Rendering.Universal.TransparentSettingsPass.ExecutePass(context.cmd);
				}
				bool yFlip = global::UnityEngine.Rendering.Universal.RenderingUtils.IsHandleYFlipped(in context, in data.basePassData.albedoHdl.IsValid() ? ref data.basePassData.albedoHdl : ref data.basePassData.depthHdl);
				global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.ExecutePass(context.cmd, data.basePassData, data.basePassData.rendererListHdl, data.basePassData.objectsWithErrorRendererListHdl, yFlip);
				context.cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.WriteRenderingLayers, value: false);
			});
		}
	}
}
