namespace UnityEngine.Rendering.Universal.Internal
{
	internal class GBufferPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.Internal.DeferredLights deferredLights;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererListHdl;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle objectsWithErrorRendererListHdl;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle screenSpaceIrradianceHdl;
		}

		private static readonly int s_CameraNormalsTextureID = global::UnityEngine.Shader.PropertyToID("_CameraNormalsTexture");

		private static readonly int s_CameraRenderingLayersTextureID = global::UnityEngine.Shader.PropertyToID("_CameraRenderingLayersTexture");

		private static readonly global::UnityEngine.Rendering.ShaderTagId s_ShaderTagLit = new global::UnityEngine.Rendering.ShaderTagId("Lit");

		private static readonly global::UnityEngine.Rendering.ShaderTagId s_ShaderTagSimpleLit = new global::UnityEngine.Rendering.ShaderTagId("SimpleLit");

		private static readonly global::UnityEngine.Rendering.ShaderTagId s_ShaderTagUnlit = new global::UnityEngine.Rendering.ShaderTagId("Unlit");

		private static readonly global::UnityEngine.Rendering.ShaderTagId s_ShaderTagComplexLit = new global::UnityEngine.Rendering.ShaderTagId("ComplexLit");

		private static readonly global::UnityEngine.Rendering.ShaderTagId s_ShaderTagUniversalGBuffer = new global::UnityEngine.Rendering.ShaderTagId("UniversalGBuffer");

		private static readonly global::UnityEngine.Rendering.ShaderTagId s_ShaderTagUniversalMaterialType = new global::UnityEngine.Rendering.ShaderTagId("UniversalMaterialType");

		private global::UnityEngine.Rendering.Universal.Internal.DeferredLights m_DeferredLights;

		private static global::UnityEngine.Rendering.ShaderTagId[] s_ShaderTagValues;

		private static global::UnityEngine.Rendering.RenderStateBlock[] s_RenderStateBlocks;

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private global::UnityEngine.Rendering.RenderStateBlock m_RenderStateBlock;

		public GBufferPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.RenderQueueRange renderQueueRange, global::UnityEngine.LayerMask layerMask, global::UnityEngine.Rendering.StencilState stencilState, int stencilReference, global::UnityEngine.Rendering.Universal.Internal.DeferredLights deferredLights)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw GBuffer");
			base.renderPassEvent = evt;
			m_DeferredLights = deferredLights;
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(renderQueueRange, layerMask);
			m_RenderStateBlock = new global::UnityEngine.Rendering.RenderStateBlock(global::UnityEngine.Rendering.RenderStateMask.Nothing);
			m_RenderStateBlock.stencilState = stencilState;
			m_RenderStateBlock.stencilReference = stencilReference;
			m_RenderStateBlock.mask = global::UnityEngine.Rendering.RenderStateMask.Stencil;
			if (s_ShaderTagValues == null)
			{
				s_ShaderTagValues = new global::UnityEngine.Rendering.ShaderTagId[5];
				s_ShaderTagValues[0] = s_ShaderTagLit;
				s_ShaderTagValues[1] = s_ShaderTagSimpleLit;
				s_ShaderTagValues[2] = s_ShaderTagUnlit;
				s_ShaderTagValues[3] = s_ShaderTagComplexLit;
				s_ShaderTagValues[4] = default(global::UnityEngine.Rendering.ShaderTagId);
			}
			if (s_RenderStateBlocks == null)
			{
				s_RenderStateBlocks = new global::UnityEngine.Rendering.RenderStateBlock[5];
				s_RenderStateBlocks[0] = global::UnityEngine.Rendering.Universal.Internal.DeferredLights.OverwriteStencil(m_RenderStateBlock, 96, 32);
				s_RenderStateBlocks[1] = global::UnityEngine.Rendering.Universal.Internal.DeferredLights.OverwriteStencil(m_RenderStateBlock, 96, 64);
				s_RenderStateBlocks[2] = global::UnityEngine.Rendering.Universal.Internal.DeferredLights.OverwriteStencil(m_RenderStateBlock, 96, 0);
				s_RenderStateBlocks[3] = global::UnityEngine.Rendering.Universal.Internal.DeferredLights.OverwriteStencil(m_RenderStateBlock, 96, 0);
				s_RenderStateBlocks[4] = s_RenderStateBlocks[0];
			}
		}

		public void Dispose()
		{
			m_DeferredLights?.ReleaseGbufferResources();
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Internal.GBufferPass.PassData data, global::UnityEngine.Rendering.RendererList rendererList, global::UnityEngine.Rendering.RendererList errorRendererList)
		{
			int num;
			if (data.deferredLights.UseRenderingLayers)
			{
				num = ((!data.deferredLights.HasRenderingLayerPrepass) ? 1 : 0);
				if (num != 0)
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.WriteRenderingLayers, value: true);
				}
			}
			else
			{
				num = 0;
			}
			bool flag = data.screenSpaceIrradianceHdl.IsValid();
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ScreenSpaceIrradiance, flag);
			if (flag)
			{
				cmd.SetGlobalTexture(global::UnityEngine.Rendering.Universal.ShaderPropertyId.screenSpaceIrradiance, data.screenSpaceIrradianceHdl);
			}
			cmd.DrawRendererList(rendererList);
			if (num != 0)
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.WriteRenderingLayers, value: false);
			}
		}

		private void InitRendererLists(ref global::UnityEngine.Rendering.Universal.Internal.GBufferPass.PassData passData, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, bool useRenderGraph, uint batchLayerMask = uint.MaxValue)
		{
			global::UnityEngine.Rendering.ShaderTagId shaderTagId = s_ShaderTagUniversalGBuffer;
			global::UnityEngine.Rendering.DrawingSettings drawSettings = CreateDrawingSettings(shaderTagId, renderingData, cameraData, lightData, cameraData.defaultOpaqueSortFlags);
			global::UnityEngine.Rendering.FilteringSettings filteringSettings = m_FilteringSettings;
			filteringSettings.batchLayerMask = batchLayerMask;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ShaderTagId> value = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ShaderTagId>(s_ShaderTagValues, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderStateBlock> value2 = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderStateBlock>(s_RenderStateBlocks, global::Unity.Collections.Allocator.Temp);
			global::UnityEngine.Rendering.RendererListParams rendererListParams = new global::UnityEngine.Rendering.RendererListParams(renderingData.cullResults, drawSettings, filteringSettings);
			rendererListParams.tagValues = value;
			rendererListParams.stateBlocks = value2;
			rendererListParams.tagName = s_ShaderTagUniversalMaterialType;
			rendererListParams.isPassTagName = false;
			global::UnityEngine.Rendering.RendererListParams desc = rendererListParams;
			if (useRenderGraph)
			{
				passData.rendererListHdl = renderGraph.CreateRendererList(in desc);
			}
			value.Dispose();
			value2.Dispose();
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraColor, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepth, bool setGlobalTextures, uint batchLayerMask = uint.MaxValue)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.Internal.GBufferPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.GBufferPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\GBufferPass.cs", 248);
			bool flag = m_DeferredLights.UseRenderingLayers && !m_DeferredLights.UseLightLayers;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] gbufferTextureHandles = m_DeferredLights.GbufferTextureHandles;
			for (int i = 0; i < m_DeferredLights.GBufferSliceCount; i++)
			{
				rasterRenderGraphBuilder.SetRenderAttachment(gbufferTextureHandles[i], i);
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle irradianceTexture = universalResourceData.irradianceTexture;
			if (irradianceTexture.IsValid())
			{
				passData.screenSpaceIrradianceHdl = irradianceTexture;
				rasterRenderGraphBuilder.UseTexture(in irradianceTexture);
			}
			global::UnityEngine.Rendering.Universal.RenderGraphUtils.UseDBufferIfValid(rasterRenderGraphBuilder, universalResourceData);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(cameraDepth);
			passData.deferredLights = m_DeferredLights;
			InitRendererLists(ref passData, default(global::UnityEngine.Rendering.ScriptableRenderContext), renderGraph, renderingData, cameraData, lightData, useRenderGraph: true);
			rasterRenderGraphBuilder.UseRendererList(in passData.rendererListHdl);
			rasterRenderGraphBuilder.UseRendererList(in passData.objectsWithErrorRendererListHdl);
			if (setGlobalTextures)
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(universalResourceData.cameraNormalsTexture, s_CameraNormalsTextureID);
				if (flag)
				{
					rasterRenderGraphBuilder.SetGlobalTextureAfterPass(universalResourceData.renderingLayersTexture, s_CameraRenderingLayersTextureID);
				}
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.GBufferPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				ExecutePass(context.cmd, data, data.rendererListHdl, data.objectsWithErrorRendererListHdl);
			});
		}
	}
}
