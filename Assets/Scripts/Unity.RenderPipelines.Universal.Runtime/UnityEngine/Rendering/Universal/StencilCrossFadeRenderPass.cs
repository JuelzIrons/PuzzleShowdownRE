namespace UnityEngine.Rendering.Universal
{
	internal sealed class StencilCrossFadeRenderPass
	{
		private class PassData
		{
			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTarget;

			public global::UnityEngine.Material[] stencilDitherMaskSeedMaterials;
		}

		private global::UnityEngine.Material[] m_StencilDitherMaskSeedMaterials;

		private readonly int _StencilDitherPattern = global::UnityEngine.Shader.PropertyToID("_StencilDitherPattern");

		private readonly int _StencilRefDitherMask = global::UnityEngine.Shader.PropertyToID("_StencilRefDitherMask");

		private readonly int _StencilWriteDitherMask = global::UnityEngine.Shader.PropertyToID("_StencilWriteDitherMask");

		private readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler;

		internal StencilCrossFadeRenderPass(global::UnityEngine.Shader shader)
		{
			m_StencilDitherMaskSeedMaterials = new global::UnityEngine.Material[3];
			m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("StencilDitherMaskSeed");
			int[] array = new int[3] { 4, 8, 12 };
			int num = 12;
			for (int i = 0; i < m_StencilDitherMaskSeedMaterials.Length; i++)
			{
				m_StencilDitherMaskSeedMaterials[i] = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(shader);
				m_StencilDitherMaskSeedMaterials[i].SetInteger(_StencilDitherPattern, i + 1);
				m_StencilDitherMaskSeedMaterials[i].SetFloat(_StencilWriteDitherMask, num);
				m_StencilDitherMaskSeedMaterials[i].SetFloat(_StencilRefDitherMask, array[i]);
			}
		}

		public void Dispose()
		{
			global::UnityEngine.Material[] stencilDitherMaskSeedMaterials = m_StencilDitherMaskSeedMaterials;
			for (int i = 0; i < stencilDitherMaskSeedMaterials.Length; i++)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(stencilDitherMaskSeedMaterials[i]);
			}
			m_StencilDitherMaskSeedMaterials = null;
		}

		public void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTarget)
		{
			global::UnityEngine.Rendering.Universal.StencilCrossFadeRenderPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.StencilCrossFadeRenderPass.PassData>("Prepare Cross Fade Stencil", out passData, m_ProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\StencilCrossFadeRenderPass.cs", 61);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(depthTarget);
			passData.stencilDitherMaskSeedMaterials = m_StencilDitherMaskSeedMaterials;
			passData.depthTarget = depthTarget;
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.StencilCrossFadeRenderPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rasterGraphContext)
			{
				ExecutePass(rasterGraphContext.cmd, data.depthTarget, data.stencilDitherMaskSeedMaterials);
			});
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle depthTarget, global::UnityEngine.Material[] stencilDitherMaskSeedMaterials)
		{
			global::UnityEngine.Vector2Int scaledSize = depthTarget.GetScaledSize(depthTarget.rtHandleProperties.currentViewportSize);
			global::UnityEngine.Rect viewport = new global::UnityEngine.Rect(0f, 0f, scaledSize.x, scaledSize.y);
			cmd.SetViewport(viewport);
			for (int i = 0; i < stencilDitherMaskSeedMaterials.Length; i++)
			{
				cmd.DrawProcedural(global::UnityEngine.Matrix4x4.identity, stencilDitherMaskSeedMaterials[i], 0, global::UnityEngine.MeshTopology.Triangles, 3, 1);
			}
		}
	}
}
