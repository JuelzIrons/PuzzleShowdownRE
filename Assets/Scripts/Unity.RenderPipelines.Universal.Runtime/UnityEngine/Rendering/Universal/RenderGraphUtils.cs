namespace UnityEngine.Rendering.Universal
{
	internal static class RenderGraphUtils
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture;

			internal int nameID;
		}

		private static global::UnityEngine.Rendering.ProfilingSampler s_SetGlobalTextureProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Set Global Texture");

		internal const int GBufferSize = 7;

		internal const int DBufferSize = 3;

		internal const int LightTextureSize = 4;

		internal static void UseDBufferIfValid(global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder builder, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] dBuffer = resourceData.dBuffer;
			for (int i = 0; i < 3; i++)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = dBuffer[i];
				if (input.IsValid())
				{
					builder.UseTexture(in input);
				}
			}
		}

		public static void SetGlobalTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, int nameId, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle handle, string passName = "Set Global Texture", [global::System.Runtime.CompilerServices.CallerFilePath] string file = "", [global::System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
		{
			global::UnityEngine.Rendering.Universal.RenderGraphUtils.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.RenderGraphUtils.PassData>(passName, out passData, s_SetGlobalTextureProfilingSampler, file, line);
			passData.nameID = nameId;
			passData.texture = handle;
			rasterRenderGraphBuilder.UseTexture(in handle);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in handle, nameId);
			rasterRenderGraphBuilder.SetRenderFunc<global::UnityEngine.Rendering.Universal.RenderGraphUtils.PassData>(delegate
			{
			});
		}
	}
}
