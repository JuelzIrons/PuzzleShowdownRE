namespace UnityEngine.Rendering.Universal
{
	internal class DecalGBufferRenderPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.DecalDrawGBufferSystem drawSystem;

			internal global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings settings;

			internal bool decalLayers;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;
		}

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> m_ShaderTagIdList;

		private global::UnityEngine.Rendering.Universal.DecalDrawGBufferSystem m_DrawSystem;

		private global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings m_Settings;

		private global::UnityEngine.Rendering.Universal.Internal.DeferredLights m_DeferredLights;

		private bool m_DecalLayers;

		public DecalGBufferRenderPass(global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings settings, global::UnityEngine.Rendering.Universal.DecalDrawGBufferSystem drawSystem, bool decalLayers)
		{
			base.renderPassEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingGbuffer;
			m_DrawSystem = drawSystem;
			m_Settings = settings;
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw Decal To GBuffer");
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(global::UnityEngine.Rendering.RenderQueueRange.opaque);
			m_DecalLayers = decalLayers;
			m_ShaderTagIdList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>();
			if (drawSystem == null)
			{
				m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("DecalGBufferProjector"));
			}
			else
			{
				m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("DecalGBufferMesh"));
			}
		}

		internal void Setup(global::UnityEngine.Rendering.Universal.Internal.DeferredLights deferredLights)
		{
			m_DeferredLights = deferredLights;
		}

		private void InitPassData(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, ref global::UnityEngine.Rendering.Universal.DecalGBufferRenderPass.PassData passData)
		{
			passData.drawSystem = m_DrawSystem;
			passData.settings = m_Settings;
			passData.decalLayers = m_DecalLayers;
			passData.cameraData = cameraData;
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DecalGBufferRenderPass.PassData passData, global::UnityEngine.Rendering.RendererList rendererList)
		{
			global::UnityEngine.Rendering.Universal.Internal.NormalReconstruction.SetupProperties(cmd, in passData.cameraData);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalNormalBlendLow, passData.settings.normalBlend == global::UnityEngine.Rendering.Universal.DecalNormalBlend.Low);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalNormalBlendMedium, passData.settings.normalBlend == global::UnityEngine.Rendering.Universal.DecalNormalBlend.Medium);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalNormalBlendHigh, passData.settings.normalBlend == global::UnityEngine.Rendering.Universal.DecalNormalBlend.High);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalLayers, passData.decalLayers);
			passData.drawSystem?.Execute(cmd);
			cmd.DrawRendererList(rendererList);
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepthTexture = universalResourceData.cameraDepthTexture;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle renderingLayersTexture = universalResourceData.renderingLayersTexture;
			global::UnityEngine.Rendering.Universal.DecalGBufferRenderPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DecalGBufferRenderPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Decal\\ScreenSpace\\DecalGBufferRenderPass.cs", 173);
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			InitPassData(cameraData, ref passData);
			for (int i = 0; i <= m_DeferredLights.GBufferLightingIndex; i++)
			{
				if (universalResourceData.gBuffer[i].IsValid())
				{
					rasterRenderGraphBuilder.SetRenderAttachment(universalResourceData.gBuffer[i], i);
				}
			}
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(universalResourceData.activeDepthTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);
			if (renderGraph.nativeRenderPassesEnabled)
			{
				if (universalResourceData.gBuffer[4].IsValid())
				{
					rasterRenderGraphBuilder.SetInputAttachment(universalResourceData.gBuffer[4], 0);
				}
				if (m_DecalLayers && universalResourceData.gBuffer[5].IsValid())
				{
					rasterRenderGraphBuilder.SetInputAttachment(universalResourceData.gBuffer[5], 1);
				}
			}
			else
			{
				if (cameraDepthTexture.IsValid())
				{
					rasterRenderGraphBuilder.UseTexture(in cameraDepthTexture);
				}
				if (m_DecalLayers && renderingLayersTexture.IsValid())
				{
					rasterRenderGraphBuilder.UseTexture(in renderingLayersTexture);
				}
			}
			global::UnityEngine.Rendering.SortingCriteria defaultOpaqueSortFlags = passData.cameraData.defaultOpaqueSortFlags;
			global::UnityEngine.Rendering.DrawingSettings drawSettings = global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(m_ShaderTagIdList, universalRenderingData, passData.cameraData, lightData, defaultOpaqueSortFlags);
			global::UnityEngine.Rendering.RendererListParams desc = new global::UnityEngine.Rendering.RendererListParams(universalRenderingData.cullResults, drawSettings, m_FilteringSettings);
			passData.rendererList = renderGraph.CreateRendererList(in desc);
			rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DecalGBufferRenderPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
			{
				ExecutePass(rgContext.cmd, data, data.rendererList);
			});
		}

		public override void OnCameraCleanup(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new global::System.ArgumentNullException("cmd");
			}
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalNormalBlendLow, value: false);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalNormalBlendMedium, value: false);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalNormalBlendHigh, value: false);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalLayers, value: false);
		}
	}
}
