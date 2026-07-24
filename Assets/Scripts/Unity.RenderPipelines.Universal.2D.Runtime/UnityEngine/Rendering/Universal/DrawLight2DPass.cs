namespace UnityEngine.Rendering.Universal
{
	internal class DrawLight2DPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		internal class PassData
		{
			internal global::UnityEngine.Rendering.Universal.LayerBatch layerBatch;

			internal global::UnityEngine.Rendering.Universal.Renderer2DData rendererData;

			internal bool isVolumetric;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle normalMap;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] shadowTextures;

			internal int lightTextureIndex;
		}

		private static readonly string k_LightPass = "Light2D Pass";

		private static readonly string k_LightSRTPass = "Light2D SRT Pass";

		private static readonly string k_LightVolumetricPass = "Light2D Volumetric Pass";

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(k_LightPass);

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampleSRT = new global::UnityEngine.Rendering.ProfilingSampler(k_LightSRTPass);

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerVolume = new global::UnityEngine.Rendering.ProfilingSampler(k_LightVolumetricPass);

		internal static readonly int k_InverseHDREmulationScaleID = global::UnityEngine.Shader.PropertyToID("_InverseHDREmulationScale");

		internal static readonly string k_NormalMapID = "_NormalMap";

		internal static readonly string k_ShadowMapID = "_ShadowTex";

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] intermediateTexture = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[1];

		internal static global::UnityEngine.MaterialPropertyBlock s_PropertyBlock = new global::UnityEngine.MaterialPropertyBlock();

		internal void Setup(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ref global::UnityEngine.Rendering.Universal.Renderer2DData rendererData)
		{
			foreach (global::UnityEngine.Rendering.Universal.Light2D visibleLight in rendererData.lightCullResult.visibleLights)
			{
				if (visibleLight.useCookieSprite && visibleLight.m_CookieSpriteTexture != null)
				{
					visibleLight.m_CookieSpriteTextureHandle = renderGraph.ImportTexture(visibleLight.m_CookieSpriteTexture);
				}
			}
		}

		private static bool TryGetShadowIndex(ref global::UnityEngine.Rendering.Universal.LayerBatch layerBatch, int lightIndex, out int shadowIndex)
		{
			shadowIndex = 0;
			for (int i = 0; i < layerBatch.shadowIndices.Count; i++)
			{
				if (layerBatch.shadowIndices[i] == lightIndex)
				{
					shadowIndex = i;
					return true;
				}
			}
			return false;
		}

		private static void Execute(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DrawLight2DPass.PassData passData, ref global::UnityEngine.Rendering.Universal.LayerBatch layerBatch, int lightTextureIndex)
		{
			cmd.SetGlobalFloat(k_InverseHDREmulationScaleID, 1f / passData.rendererData.hdrEmulationScale);
			int num = layerBatch.activeBlendStylesIndices[lightTextureIndex];
			string name = passData.rendererData.lightBlendStyles[num].name;
			cmd.BeginSample(name);
			int blendStyleIndex = (global::UnityEngine.Rendering.Universal.Renderer2D.supportsMRT ? lightTextureIndex : 0);
			if (!passData.isVolumetric)
			{
				global::UnityEngine.Rendering.Universal.RendererLighting.EnableBlendStyle(cmd, blendStyleIndex, enabled: true);
			}
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D> lights = passData.layerBatch.lights;
			for (int i = 0; i < lights.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.Light2D light2D = lights[i];
				if (!(light2D == null) && light2D.lightType != global::UnityEngine.Rendering.Universal.Light2D.LightType.Global && light2D.blendStyleIndex == num && (!passData.isVolumetric || (!(light2D.volumeIntensity <= 0f) && light2D.volumetricEnabled && layerBatch.endLayerValue == light2D.GetTopMostLitLayer())))
				{
					bool flag = passData.layerBatch.lightStats.useShadows && layerBatch.shadowIndices.Contains(i);
					global::UnityEngine.Material lightMaterial = passData.rendererData.GetLightMaterial(light2D, passData.isVolumetric, flag);
					global::UnityEngine.Mesh lightMesh = light2D.lightMesh;
					int batchSlotIndex = light2D.batchSlotIndex;
					int slot = global::UnityEngine.Rendering.Universal.RendererLighting.lightBatch.SlotIndex(batchSlotIndex);
					if (!global::UnityEngine.Rendering.Universal.RendererLighting.lightBatch.CanBatch(light2D, lightMaterial, batchSlotIndex, out var lightHash) && global::UnityEngine.Rendering.Universal.LightBatch.isBatchingSupported)
					{
						global::UnityEngine.Rendering.Universal.RendererLighting.lightBatch.Flush(cmd);
					}
					if (passData.layerBatch.lightStats.useNormalMap)
					{
						s_PropertyBlock.SetTexture(k_NormalMapID, passData.normalMap);
					}
					if (flag && TryGetShadowIndex(ref layerBatch, i, out var shadowIndex))
					{
						s_PropertyBlock.SetTexture(k_ShadowMapID, passData.shadowTextures[shadowIndex]);
					}
					if (!passData.isVolumetric || (passData.isVolumetric && light2D.volumetricEnabled))
					{
						global::UnityEngine.Rendering.Universal.RendererLighting.SetCookieShaderProperties(light2D, s_PropertyBlock);
					}
					global::UnityEngine.Rendering.Universal.RendererLighting.SetPerLightShaderGlobals(cmd, light2D, slot, passData.isVolumetric, flag, global::UnityEngine.Rendering.Universal.LightBatch.isBatchingSupported);
					if (light2D.normalMapQuality != global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality.Disabled || light2D.lightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Point)
					{
						global::UnityEngine.Rendering.Universal.RendererLighting.SetPerPointLightShaderGlobals(cmd, light2D, slot, global::UnityEngine.Rendering.Universal.LightBatch.isBatchingSupported);
					}
					if (global::UnityEngine.Rendering.Universal.LightBatch.isBatchingSupported)
					{
						global::UnityEngine.Rendering.Universal.RendererLighting.lightBatch.AddBatch(light2D, lightMaterial, light2D.GetMatrix(), lightMesh, 0, lightHash, batchSlotIndex);
						global::UnityEngine.Rendering.Universal.RendererLighting.lightBatch.Flush(cmd);
					}
					else
					{
						cmd.DrawMesh(lightMesh, light2D.GetMatrix(), lightMaterial, 0, 0, s_PropertyBlock);
					}
				}
			}
			global::UnityEngine.Rendering.Universal.RendererLighting.EnableBlendStyle(cmd, blendStyleIndex, enabled: false);
			cmd.EndSample(name);
		}

		private void InitializeRenderPass(global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder builder, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.DrawLight2DPass.PassData passData, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, ref global::UnityEngine.Rendering.Universal.LayerBatch layerBatch, int batchIndex, bool isVolumetric = false)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			intermediateTexture[0] = universalResourceData.activeColorTexture;
			if (layerBatch.lightStats.useNormalMap)
			{
				builder.UseTexture(in universal2DResourceData.normalsTexture[batchIndex]);
			}
			if (layerBatch.lightStats.useShadows)
			{
				passData.shadowTextures = universal2DResourceData.shadowTextures[batchIndex];
				for (int i = 0; i < passData.shadowTextures.Length; i++)
				{
					builder.UseTexture(in passData.shadowTextures[i]);
				}
			}
			foreach (global::UnityEngine.Rendering.Universal.Light2D light in layerBatch.lights)
			{
				if (!(light == null) && light.m_CookieSpriteTextureHandle.IsValid() && (!isVolumetric || (isVolumetric && light.volumetricEnabled)))
				{
					builder.UseTexture(in light.m_CookieSpriteTextureHandle);
				}
			}
			passData.layerBatch = layerBatch;
			passData.rendererData = rendererData;
			passData.isVolumetric = isVolumetric;
			passData.normalMap = (layerBatch.lightStats.useNormalMap ? universal2DResourceData.normalsTexture[batchIndex] : global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle);
			builder.AllowGlobalStateModification(value: true);
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, ref global::UnityEngine.Rendering.Universal.LayerBatch layerBatch, int batchIndex, bool isVolumetric = false)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			bool flag = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>())?.IsLightingActive ?? true;
			if (!layerBatch.lightStats.useLights || (isVolumetric && !layerBatch.lightStats.useVolumetricLights) || !flag)
			{
				return;
			}
			if (!isVolumetric && !global::UnityEngine.Rendering.Universal.Renderer2D.supportsMRT)
			{
				for (int i = 0; i < layerBatch.activeBlendStylesIndices.Length; i++)
				{
					global::UnityEngine.Rendering.Universal.DrawLight2DPass.PassData passData;
					using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DrawLight2DPass.PassData>(k_LightSRTPass, out passData, m_ProfilingSampleSRT, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\DrawLight2DPass.cs", 204);
					InitializeRenderPass(rasterRenderGraphBuilder, frameData, passData, rendererData, ref layerBatch, batchIndex, isVolumetric);
					global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] array = universal2DResourceData.lightTextures[batchIndex];
					rasterRenderGraphBuilder.SetRenderAttachment(array[i], 0);
					passData.lightTextureIndex = i;
					rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawLight2DPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
					{
						Execute(context.cmd, data, ref data.layerBatch, data.lightTextureIndex);
					});
				}
				return;
			}
			global::UnityEngine.Rendering.Universal.DrawLight2DPass.PassData passData2;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder2 = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.DrawLight2DPass.PassData>((!isVolumetric) ? k_LightPass : k_LightVolumetricPass, out passData2, (!isVolumetric) ? m_ProfilingSampler : m_ProfilingSamplerVolume, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\DrawLight2DPass.cs", 224);
			InitializeRenderPass(rasterRenderGraphBuilder2, frameData, passData2, rendererData, ref layerBatch, batchIndex, isVolumetric);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] array2 = ((!isVolumetric) ? universal2DResourceData.lightTextures[batchIndex] : intermediateTexture);
			for (int num = 0; num < array2.Length; num++)
			{
				rasterRenderGraphBuilder2.SetRenderAttachment(array2[num], num);
			}
			rasterRenderGraphBuilder2.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawLight2DPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				for (int j = 0; j < data.layerBatch.activeBlendStylesIndices.Length; j++)
				{
					Execute(context.cmd, data, ref data.layerBatch, j);
				}
			});
		}
	}
}
