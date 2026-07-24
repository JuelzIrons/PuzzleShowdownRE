namespace UnityEngine.Rendering.Universal
{
	internal class DecalScreenSpaceRenderPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.DecalDrawScreenSpaceSystem drawSystem;

			internal global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings settings;

			internal bool decalLayers;

			internal bool isGLDevice;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorTarget;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;
		}

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> m_ShaderTagIdList;

		private global::UnityEngine.Rendering.Universal.DecalDrawScreenSpaceSystem m_DrawSystem;

		private global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings m_Settings;

		private bool m_DecalLayers;

		public DecalScreenSpaceRenderPass(global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings settings, global::UnityEngine.Rendering.Universal.DecalDrawScreenSpaceSystem drawSystem, bool decalLayers)
		{
			base.renderPassEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingSkybox;
			global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput passInput = global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth;
			ConfigureInput(passInput);
			m_DrawSystem = drawSystem;
			m_Settings = settings;
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw Decal Screen Space");
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(global::UnityEngine.Rendering.RenderQueueRange.opaque);
			m_DecalLayers = decalLayers;
			m_ShaderTagIdList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>();
			if (m_DrawSystem == null)
			{
				m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("DecalScreenSpaceProjector"));
			}
			else
			{
				m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("DecalScreenSpaceMesh"));
			}
		}

		private global::UnityEngine.Rendering.RendererListParams CreateRenderListParams(global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			global::UnityEngine.Rendering.SortingCriteria sortingCriteria = global::UnityEngine.Rendering.SortingCriteria.None;
			global::UnityEngine.Rendering.DrawingSettings drawSettings = global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(m_ShaderTagIdList, renderingData, cameraData, lightData, sortingCriteria);
			return new global::UnityEngine.Rendering.RendererListParams(renderingData.cullResults, drawSettings, m_FilteringSettings);
		}

		private void InitPassData(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, ref global::UnityEngine.Rendering.Universal.DecalScreenSpaceRenderPass.PassData passData)
		{
			passData.drawSystem = m_DrawSystem;
			passData.settings = m_Settings;
			passData.decalLayers = m_DecalLayers;
			passData.isGLDevice = global::UnityEngine.Rendering.Universal.DecalRendererFeature.isGLDevice;
			passData.cameraData = cameraData;
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DecalScreenSpaceRenderPass.PassData passData, global::UnityEngine.Rendering.RendererList rendererList)
		{
			global::UnityEngine.Rendering.Universal.Internal.NormalReconstruction.SetupProperties(cmd, in passData.cameraData);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalNormalBlendLow, passData.settings.normalBlend == global::UnityEngine.Rendering.Universal.DecalNormalBlend.Low);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalNormalBlendMedium, passData.settings.normalBlend == global::UnityEngine.Rendering.Universal.DecalNormalBlend.Medium);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalNormalBlendHigh, passData.settings.normalBlend == global::UnityEngine.Rendering.Universal.DecalNormalBlend.High);
			if (!passData.isGLDevice)
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalLayers, passData.decalLayers);
			}
			passData.drawSystem?.Execute(cmd);
			cmd.DrawRendererList(rendererList);
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepthTexture = universalResourceData.cameraDepthTexture;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle renderingLayersTexture = universalResourceData.renderingLayersTexture;
			global::UnityEngine.Rendering.Universal.DecalScreenSpaceRenderPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DecalScreenSpaceRenderPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Decal\\ScreenSpace\\DecalScreenSpaceRenderPass.cs", 121);
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			InitPassData(universalCameraData, ref passData);
			passData.colorTarget = universalResourceData.cameraColor;
			rasterRenderGraphBuilder.SetRenderAttachment(universalResourceData.activeColorTexture, 0);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(universalResourceData.activeDepthTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);
			if (universalCameraData.xr.enabled)
			{
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			global::UnityEngine.Rendering.RendererListParams desc = CreateRenderListParams(renderingData, passData.cameraData, lightData);
			passData.rendererList = renderGraph.CreateRendererList(in desc);
			rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
			if (cameraDepthTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in cameraDepthTexture);
			}
			if (passData.decalLayers && renderingLayersTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in renderingLayersTexture);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DecalScreenSpaceRenderPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
			{
				global::UnityEngine.Rendering.Universal.RenderingUtils.SetScaleBiasRt(rgContext.cmd, in data.cameraData, data.colorTarget);
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
