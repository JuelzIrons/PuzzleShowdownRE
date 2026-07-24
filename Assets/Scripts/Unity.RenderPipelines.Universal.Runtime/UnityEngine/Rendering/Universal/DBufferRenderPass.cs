namespace UnityEngine.Rendering.Universal
{
	internal class DBufferRenderPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.DecalDrawDBufferSystem drawSystem;

			internal global::UnityEngine.Rendering.Universal.DBufferSettings settings;

			internal bool decalLayers;

			internal global::UnityEngine.Rendering.RTHandle dBufferDepth;

			internal global::UnityEngine.Rendering.RTHandle[] dBufferColorHandles;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;
		}

		internal static string[] s_DBufferNames = new string[4] { "_DBufferTexture0", "_DBufferTexture1", "_DBufferTexture2", "_DBufferTexture3" };

		internal static string s_DBufferDepthName = "DBufferDepth";

		private static readonly int s_SSAOTextureID = global::UnityEngine.Shader.PropertyToID("_ScreenSpaceOcclusionTexture");

		private global::UnityEngine.Rendering.Universal.DecalDrawDBufferSystem m_DrawSystem;

		private global::UnityEngine.Rendering.Universal.DBufferSettings m_Settings;

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> m_ShaderTagIdList;

		private bool m_DecalLayers;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] dbufferHandles;

		public DBufferRenderPass(global::UnityEngine.Material dBufferClear, global::UnityEngine.Rendering.Universal.DBufferSettings settings, global::UnityEngine.Rendering.Universal.DecalDrawDBufferSystem drawSystem, bool decalLayers)
		{
			base.renderPassEvent = (global::UnityEngine.Rendering.Universal.RenderPassEvent)201;
			global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput passInput = global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth | global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Normal;
			ConfigureInput(passInput);
			base.requiresIntermediateTexture = true;
			m_DrawSystem = drawSystem;
			m_Settings = settings;
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw DBuffer");
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(global::UnityEngine.Rendering.RenderQueueRange.opaque);
			m_DecalLayers = decalLayers;
			m_ShaderTagIdList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>();
			m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("DBufferMesh"));
			m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("DBufferProjectorVFX"));
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DBufferRenderPass.PassData passData, global::UnityEngine.Rendering.RendererList rendererList, bool renderGraph)
		{
			passData.drawSystem.Execute(cmd);
			cmd.DrawRendererList(rendererList);
		}

		private static void SetKeywords(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DBufferRenderPass.PassData passData)
		{
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DBufferMRT1, passData.settings.surfaceData == global::UnityEngine.Rendering.Universal.DecalSurfaceData.Albedo);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DBufferMRT2, passData.settings.surfaceData == global::UnityEngine.Rendering.Universal.DecalSurfaceData.AlbedoNormal);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DBufferMRT3, passData.settings.surfaceData == global::UnityEngine.Rendering.Universal.DecalSurfaceData.AlbedoNormalMAOS);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalLayers, passData.decalLayers);
		}

		private void InitPassData(ref global::UnityEngine.Rendering.Universal.DBufferRenderPass.PassData passData)
		{
			passData.drawSystem = m_DrawSystem;
			passData.settings = m_Settings;
			passData.decalLayers = m_DecalLayers;
		}

		private global::UnityEngine.Rendering.RendererListParams InitRendererListParams(global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			global::UnityEngine.Rendering.SortingCriteria defaultOpaqueSortFlags = cameraData.defaultOpaqueSortFlags;
			global::UnityEngine.Rendering.DrawingSettings drawSettings = global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(m_ShaderTagIdList, renderingData, cameraData, lightData, defaultOpaqueSortFlags);
			return new global::UnityEngine.Rendering.RendererListParams(renderingData.cullResults, drawSettings, m_FilteringSettings);
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepthTexture = universalResourceData.cameraDepthTexture;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraNormalsTexture = universalResourceData.cameraNormalsTexture;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex = (universalResourceData.dBufferDepth.IsValid() ? universalResourceData.dBufferDepth : universalResourceData.activeDepthTexture);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle renderingLayersTexture = universalResourceData.renderingLayersTexture;
			global::UnityEngine.Rendering.Universal.DBufferRenderPass.PassData passData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DBufferRenderPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Decal\\DBuffer\\DBufferRenderPass.cs", 240))
			{
				InitPassData(ref passData);
				if (dbufferHandles == null)
				{
					dbufferHandles = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[3];
				}
				global::UnityEngine.RenderTextureDescriptor desc = universalCameraData.cameraTargetDescriptor;
				desc.graphicsFormat = ((global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear) ? global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB : global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm);
				desc.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				desc.msaaSamples = 1;
				dbufferHandles[0] = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, in desc, s_DBufferNames[0], clear: true, new global::UnityEngine.Color(0f, 0f, 0f, 1f));
				rasterRenderGraphBuilder.SetRenderAttachment(dbufferHandles[0], 0);
				if (m_Settings.surfaceData == global::UnityEngine.Rendering.Universal.DecalSurfaceData.AlbedoNormal || m_Settings.surfaceData == global::UnityEngine.Rendering.Universal.DecalSurfaceData.AlbedoNormalMAOS)
				{
					global::UnityEngine.RenderTextureDescriptor desc2 = universalCameraData.cameraTargetDescriptor;
					desc2.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
					desc2.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
					desc2.msaaSamples = 1;
					dbufferHandles[1] = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, in desc2, s_DBufferNames[1], clear: true, new global::UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f));
					rasterRenderGraphBuilder.SetRenderAttachment(dbufferHandles[1], 1);
				}
				if (m_Settings.surfaceData == global::UnityEngine.Rendering.Universal.DecalSurfaceData.AlbedoNormalMAOS)
				{
					global::UnityEngine.RenderTextureDescriptor desc3 = universalCameraData.cameraTargetDescriptor;
					desc3.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
					desc3.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
					desc3.msaaSamples = 1;
					dbufferHandles[2] = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, in desc3, s_DBufferNames[2], clear: true, new global::UnityEngine.Color(0f, 0f, 0f, 1f));
					rasterRenderGraphBuilder.SetRenderAttachment(dbufferHandles[2], 2);
				}
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(tex, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);
				if (cameraDepthTexture.IsValid())
				{
					rasterRenderGraphBuilder.UseTexture(in cameraDepthTexture);
				}
				if (cameraNormalsTexture.IsValid())
				{
					rasterRenderGraphBuilder.UseTexture(in cameraNormalsTexture);
				}
				if (passData.decalLayers && renderingLayersTexture.IsValid())
				{
					rasterRenderGraphBuilder.UseTexture(in renderingLayersTexture);
				}
				if (universalResourceData.ssaoTexture.IsValid())
				{
					rasterRenderGraphBuilder.UseGlobalTexture(s_SSAOTextureID);
				}
				global::UnityEngine.Rendering.RendererListParams desc4 = InitRendererListParams(renderingData, universalCameraData, lightData);
				passData.rendererList = renderGraph.CreateRendererList(in desc4);
				rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
				for (int i = 0; i < 3; i++)
				{
					if (dbufferHandles[i].IsValid())
					{
						rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in dbufferHandles[i], global::UnityEngine.Shader.PropertyToID(s_DBufferNames[i]));
					}
				}
				rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DBufferRenderPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
				{
					SetKeywords(rgContext.cmd, data);
					ExecutePass(rgContext.cmd, data, data.rendererList, renderGraph: true);
				});
			}
			universalResourceData.dBuffer = dbufferHandles;
		}

		public override void OnCameraCleanup(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new global::System.ArgumentNullException("cmd");
			}
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DBufferMRT1, value: false);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DBufferMRT2, value: false);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DBufferMRT3, value: false);
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DecalLayers, value: false);
		}
	}
}
