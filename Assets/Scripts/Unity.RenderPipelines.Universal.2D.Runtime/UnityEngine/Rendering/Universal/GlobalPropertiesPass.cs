namespace UnityEngine.Rendering.Universal
{
	internal class GlobalPropertiesPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Vector2Int screenParams;
		}

		private static readonly string k_SetGlobalProperties = "SetGlobalProperties";

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_SetGlobalPropertiesProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(k_SetGlobalProperties);

		internal static void Setup(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool useLights)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			global::UnityEngine.Rendering.Universal.GlobalPropertiesPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.GlobalPropertiesPass.PassData>(k_SetGlobalProperties, out passData, m_SetGlobalPropertiesProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\GlobalPropertiesPass.cs", 19);
			passData.screenParams = global::UnityEngine.Vector2Int.zero;
			cameraData.camera.TryGetComponent<global::UnityEngine.Rendering.Universal.PixelPerfectCamera>(out var component);
			if (component != null && component.enabled && component.offscreenRTSize != global::UnityEngine.Vector2Int.zero)
			{
				passData.screenParams = component.offscreenRTSize;
			}
			if (useLights)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle = graph.ImportTexture(global::UnityEngine.Rendering.Universal.Light2DLookupTexture.GetLightLookupTexture_Rendergraph());
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle2 = graph.ImportTexture(global::UnityEngine.Rendering.Universal.Light2DLookupTexture.GetFallOffLookupTexture_Rendergraph());
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in textureHandle, global::UnityEngine.Rendering.Universal.Light2DLookupTexture.k_LightLookupID);
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in textureHandle2, global::UnityEngine.Rendering.Universal.Light2DLookupTexture.k_FalloffLookupID);
			}
			if (rendererData.useCameraSortingLayerTexture)
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(universal2DResourceData.cameraSortingLayerTexture, global::UnityEngine.Rendering.Universal.CopyCameraSortingLayerPass.k_CameraSortingLayerTextureId);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.GlobalPropertiesPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				if (data.screenParams != global::UnityEngine.Vector2Int.zero)
				{
					int x = data.screenParams.x;
					int y = data.screenParams.y;
					context.cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.screenParams, new global::UnityEngine.Vector4(x, y, 1f + 1f / (float)x, 1f + 1f / (float)y));
				}
			});
		}
	}
}
