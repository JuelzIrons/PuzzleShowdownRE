namespace UnityEngine.Rendering.Universal
{
	internal class DrawRenderer2DPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class SetGlobalPassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] lightTextures;
		}

		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.Light2DBlendStyle[] lightBlendStyles;

			internal int[] blendStyleIndices;

			internal float hdrEmulationScale;

			internal bool isSceneLit;

			internal bool layerUseLights;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] lightTextures;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;

			internal global::UnityEngine.Rendering.Universal.DebugRendererLists debugRendererLists;

			internal bool activeDebugHandler;
		}

		private static readonly string k_RenderPass = "Renderer2D Pass";

		private static readonly string k_SetLightBlendTexture = "SetLightBlendTextures";

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(k_RenderPass);

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_SetLightBlendTextureProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(k_SetLightBlendTexture);

		private static readonly global::UnityEngine.Rendering.ShaderTagId k_CombinedRenderingPassName = new global::UnityEngine.Rendering.ShaderTagId("Universal2D");

		private static readonly global::UnityEngine.Rendering.ShaderTagId k_LegacyPassName = new global::UnityEngine.Rendering.ShaderTagId("SRPDefaultUnlit");

		private static readonly global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> k_ShaderTags = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> { k_LegacyPassName, k_CombinedRenderingPassName };

		private static readonly int k_HDREmulationScaleID = global::UnityEngine.Shader.PropertyToID("_HDREmulationScale");

		private static readonly int k_RendererColorID = global::UnityEngine.Shader.PropertyToID("_RendererColor");

		private static void Execute(global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context, global::UnityEngine.Rendering.Universal.DrawRenderer2DPass.PassData passData)
		{
			global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
			int num = passData.blendStyleIndices.Length;
			cmd.SetGlobalFloat(k_HDREmulationScaleID, passData.hdrEmulationScale);
			cmd.SetGlobalColor(k_RendererColorID, global::UnityEngine.Color.white);
			global::UnityEngine.Rendering.Universal.RendererLighting.SetLightShaderGlobals(cmd, passData.lightBlendStyles, passData.blendStyleIndices);
			if (passData.layerUseLights)
			{
				for (int i = 0; i < num; i++)
				{
					int blendStyleIndex = passData.blendStyleIndices[i];
					global::UnityEngine.Rendering.Universal.RendererLighting.EnableBlendStyle(cmd, blendStyleIndex, enabled: true);
				}
			}
			else if (passData.isSceneLit)
			{
				global::UnityEngine.Rendering.Universal.RendererLighting.EnableBlendStyle(cmd, 0, enabled: true);
			}
			if (passData.activeDebugHandler)
			{
				passData.debugRendererLists.DrawWithRendererList(cmd);
			}
			else
			{
				cmd.DrawRendererList(passData.rendererList);
			}
			global::UnityEngine.Rendering.Universal.RendererLighting.DisableAllKeywords(cmd);
		}

		public void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, ref global::UnityEngine.Rendering.Universal.LayerBatch[] layerBatches, int batchIndex, ref global::UnityEngine.Rendering.FilteringSettings filterSettings)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.LayerBatch layerBatch = layerBatches[batchIndex];
			bool isLitView = true;
			if (batchIndex == 0)
			{
				global::UnityEngine.Rendering.Universal.DrawRenderer2DPass.SetGlobalPassData passData;
				using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DrawRenderer2DPass.SetGlobalPassData>(k_SetLightBlendTexture, out passData, m_SetLightBlendTextureProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\DrawRenderer2DPass.cs", 118);
				if (layerBatch.lightStats.useLights)
				{
					passData.lightTextures = universal2DResourceData.lightTextures[batchIndex];
					for (int i = 0; i < passData.lightTextures.Length; i++)
					{
						rasterRenderGraphBuilder.UseTexture(in passData.lightTextures[i]);
					}
				}
				SetGlobalLightTextures(graph, rasterRenderGraphBuilder, passData.lightTextures, ref layerBatch, rendererData, isLitView);
				rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
				rasterRenderGraphBuilder.SetRenderFunc<global::UnityEngine.Rendering.Universal.DrawRenderer2DPass.SetGlobalPassData>(delegate
				{
				});
			}
			global::UnityEngine.Rendering.Universal.DrawRenderer2DPass.PassData passData2;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder2 = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DrawRenderer2DPass.PassData>(k_RenderPass, out passData2, m_ProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\DrawRenderer2DPass.cs", 138);
			passData2.lightBlendStyles = rendererData.lightBlendStyles;
			passData2.blendStyleIndices = layerBatch.activeBlendStylesIndices;
			passData2.hdrEmulationScale = rendererData.hdrEmulationScale;
			passData2.isSceneLit = rendererData.lightCullResult.IsSceneLit();
			passData2.layerUseLights = layerBatch.lightStats.useLights;
			global::UnityEngine.Rendering.DrawingSettings drawingSettings = CreateDrawingSettings(k_ShaderTags, universalRenderingData, universalCameraData, lightData, global::UnityEngine.Rendering.SortingCriteria.CommonTransparent);
			global::UnityEngine.Rendering.SortingSettings sortingSettings = drawingSettings.sortingSettings;
			global::UnityEngine.Rendering.Universal.RendererLighting.GetTransparencySortingMode(rendererData, universalCameraData.camera, ref sortingSettings);
			drawingSettings.sortingSettings = sortingSettings;
			global::UnityEngine.Rendering.Universal.DebugHandler activeDebugHandler = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(universalCameraData);
			passData2.activeDebugHandler = activeDebugHandler != null;
			if (activeDebugHandler != null)
			{
				global::UnityEngine.Rendering.RenderStateBlock renderStateBlock = new global::UnityEngine.Rendering.RenderStateBlock(global::UnityEngine.Rendering.RenderStateMask.Nothing);
				passData2.debugRendererLists = activeDebugHandler.CreateRendererListsWithDebugRenderState(graph, ref universalRenderingData.cullResults, ref drawingSettings, ref filterSettings, ref renderStateBlock);
				passData2.debugRendererLists.PrepareRendererListForRasterPass(rasterRenderGraphBuilder2);
			}
			else
			{
				global::UnityEngine.Rendering.RendererListParams desc = new global::UnityEngine.Rendering.RendererListParams(universalRenderingData.cullResults, drawingSettings, filterSettings);
				passData2.rendererList = graph.CreateRendererList(in desc);
				rasterRenderGraphBuilder2.UseRendererList(in passData2.rendererList);
			}
			if (passData2.layerUseLights)
			{
				passData2.lightTextures = universal2DResourceData.lightTextures[batchIndex];
				for (int num = 0; num < passData2.lightTextures.Length; num++)
				{
					rasterRenderGraphBuilder2.UseTexture(in passData2.lightTextures[num]);
				}
			}
			if (rendererData.useCameraSortingLayerTexture)
			{
				rasterRenderGraphBuilder2.UseTexture(universal2DResourceData.cameraSortingLayerTexture);
			}
			rasterRenderGraphBuilder2.SetRenderAttachment(universalResourceData.activeColorTexture, 0);
			if (global::UnityEngine.Rendering.Universal.Renderer2D.IsDepthUsageAllowed(frameData, rendererData))
			{
				rasterRenderGraphBuilder2.SetRenderAttachmentDepth(universalResourceData.activeDepthTexture);
			}
			rasterRenderGraphBuilder2.AllowGlobalStateModification(value: true);
			int num2 = batchIndex + 1;
			if (num2 < universal2DResourceData.lightTextures.Length)
			{
				SetGlobalLightTextures(graph, rasterRenderGraphBuilder2, universal2DResourceData.lightTextures[num2], ref layerBatches[num2], rendererData, isLitView);
			}
			rasterRenderGraphBuilder2.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawRenderer2DPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				Execute(context, data);
			});
		}

		private void SetGlobalLightTextures(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder builder, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] lightTextures, ref global::UnityEngine.Rendering.Universal.LayerBatch layerBatch, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, bool isLitView)
		{
			if (!isLitView)
			{
				return;
			}
			if (layerBatch.lightStats.useLights)
			{
				for (int i = 0; i < lightTextures.Length; i++)
				{
					int num = layerBatch.activeBlendStylesIndices[i];
					builder.SetGlobalTextureAfterPass(in lightTextures[i], global::UnityEngine.Shader.PropertyToID(global::UnityEngine.Rendering.Universal.RendererLighting.k_ShapeLightTextureIDs[num]));
				}
			}
			else if (rendererData.lightCullResult.IsSceneLit())
			{
				for (int j = 0; j < global::UnityEngine.Rendering.Universal.RendererLighting.k_ShapeLightTextureIDs.Length; j++)
				{
					builder.SetGlobalTextureAfterPass(graph.defaultResources.blackTexture, global::UnityEngine.Shader.PropertyToID(global::UnityEngine.Rendering.Universal.RendererLighting.k_ShapeLightTextureIDs[j]));
				}
			}
		}
	}
}
