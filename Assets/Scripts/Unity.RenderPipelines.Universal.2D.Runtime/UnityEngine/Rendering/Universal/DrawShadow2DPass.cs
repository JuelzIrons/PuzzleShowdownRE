namespace UnityEngine.Rendering.Universal
{
	internal class DrawShadow2DPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		internal class PassData
		{
			internal global::UnityEngine.Rendering.Universal.LayerBatch layerBatch;

			internal global::UnityEngine.Rendering.Universal.Renderer2DData rendererData;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] shadowTextures;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle shadowDepth;
		}

		private static readonly string k_ShadowPass = "Shadow2D UnsafePass";

		private static readonly string k_ShadowVolumetricPass = "Shadow2D Volumetric UnsafePass";

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(k_ShadowPass);

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerVolume = new global::UnityEngine.Rendering.ProfilingSampler(k_ShadowVolumetricPass);

		private static void ExecuteShadowPass(global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DrawShadow2DPass.PassData passData, global::UnityEngine.Rendering.Universal.Light2D light, int batchIndex)
		{
			cmd.SetRenderTarget(passData.shadowTextures[batchIndex], passData.shadowDepth);
			cmd.ClearRenderTarget(global::UnityEngine.Rendering.RTClearFlags.All, global::UnityEngine.Color.clear, 1f, 0u);
			passData.rendererData.GetProjectedShadowMaterial();
			passData.rendererData.GetProjectedUnshadowMaterial();
			global::UnityEngine.Rendering.Universal.ShadowRendering.PrerenderShadows(cmd, passData.rendererData, ref passData.layerBatch, light, 0, light.shadowIntensity);
		}

		public void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, ref global::UnityEngine.Rendering.Universal.LayerBatch layerBatch, int batchIndex, bool isVolumetric = false)
		{
			global::UnityEngine.Rendering.Universal.Universal2DResourceData universal2DResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.Universal2DResourceData>();
			frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			if (!layerBatch.lightStats.useShadows || (isVolumetric && !layerBatch.lightStats.useVolumetricShadowLights))
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.DrawShadow2DPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = graph.AddUnsafePass<global::UnityEngine.Rendering.Universal.DrawShadow2DPass.PassData>((!isVolumetric) ? k_ShadowPass : k_ShadowVolumetricPass, out passData, (!isVolumetric) ? m_ProfilingSampler : m_ProfilingSamplerVolume, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\2D\\Rendergraph\\DrawShadow2DPass.cs", 55);
			passData.layerBatch = layerBatch;
			passData.rendererData = rendererData;
			passData.shadowTextures = universal2DResourceData.shadowTextures[batchIndex];
			passData.shadowDepth = universal2DResourceData.shadowDepth;
			for (int i = 0; i < passData.shadowTextures.Length; i++)
			{
				unsafeRenderGraphBuilder.UseTexture(in passData.shadowTextures[i], global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			}
			unsafeRenderGraphBuilder.UseTexture(in passData.shadowDepth, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			unsafeRenderGraphBuilder.AllowGlobalStateModification(value: true);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.DrawShadow2DPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				for (int j = 0; j < data.layerBatch.shadowIndices.Count; j++)
				{
					global::UnityEngine.Rendering.UnsafeCommandBuffer cmd = context.cmd;
					int index = data.layerBatch.shadowIndices[j];
					global::UnityEngine.Rendering.Universal.Light2D light = data.layerBatch.lights[index];
					ExecuteShadowPass(cmd, data, light, j);
				}
			});
		}
	}
}
