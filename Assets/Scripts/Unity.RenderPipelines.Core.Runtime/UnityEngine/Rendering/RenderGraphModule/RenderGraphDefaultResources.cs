namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public class RenderGraphDefaultResources
	{
		private global::UnityEngine.Rendering.RTHandle m_BlackTexture2D;

		private global::UnityEngine.Rendering.RTHandle m_WhiteTexture2D;

		private global::UnityEngine.Rendering.RTHandle m_ShadowTexture2D;

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle blackTexture { get; private set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle whiteTexture { get; private set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle clearTextureXR { get; private set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle magentaTextureXR { get; private set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle blackTextureXR { get; private set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle blackTextureArrayXR { get; private set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle blackUIntTextureXR { get; private set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle blackTexture3DXR { get; private set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle whiteTextureXR { get; private set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle defaultShadowTexture { get; private set; }

		internal RenderGraphDefaultResources()
		{
			InitDefaultResourcesIfNeeded();
		}

		private void InitDefaultResourcesIfNeeded()
		{
			if (m_BlackTexture2D == null)
			{
				m_BlackTexture2D = global::UnityEngine.Rendering.RTHandles.Alloc(global::UnityEngine.Texture2D.blackTexture);
			}
			if (m_WhiteTexture2D == null)
			{
				m_WhiteTexture2D = global::UnityEngine.Rendering.RTHandles.Alloc(global::UnityEngine.Texture2D.whiteTexture);
			}
			if (m_ShadowTexture2D == null)
			{
				m_ShadowTexture2D = global::UnityEngine.Rendering.RTHandles.Alloc(1, 1, global::UnityEngine.Rendering.CoreUtils.GetDefaultDepthOnlyFormat(), 1, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension.Tex2D, enableRandomWrite: false, useMipMap: false, autoGenerateMips: true, isShadowMap: true, 1, 0f, global::UnityEngine.Rendering.MSAASamples.None, bindTextureMS: false, useDynamicScale: false, useDynamicScaleExplicit: false, global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage.None, "DefaultShadowTexture");
				global::UnityEngine.Rendering.CommandBuffer commandBuffer = global::UnityEngine.Rendering.CommandBufferPool.Get();
				commandBuffer.SetRenderTarget(m_ShadowTexture2D);
				commandBuffer.ClearRenderTarget(global::UnityEngine.Rendering.RTClearFlags.All, global::UnityEngine.Color.white);
				global::UnityEngine.Graphics.ExecuteCommandBuffer(commandBuffer);
				global::UnityEngine.Rendering.CommandBufferPool.Release(commandBuffer);
			}
		}

		internal void Cleanup()
		{
			m_BlackTexture2D?.Release();
			m_BlackTexture2D = null;
			m_WhiteTexture2D?.Release();
			m_WhiteTexture2D = null;
			m_ShadowTexture2D?.Release();
			m_ShadowTexture2D = null;
		}

		internal void InitializeForRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			InitDefaultResourcesIfNeeded();
			blackTexture = renderGraph.ImportTexture(m_BlackTexture2D, isBuiltin: true);
			whiteTexture = renderGraph.ImportTexture(m_WhiteTexture2D, isBuiltin: true);
			defaultShadowTexture = renderGraph.ImportTexture(m_ShadowTexture2D, isBuiltin: true);
			clearTextureXR = renderGraph.ImportTexture(global::UnityEngine.Rendering.TextureXR.GetClearTexture(), isBuiltin: true);
			magentaTextureXR = renderGraph.ImportTexture(global::UnityEngine.Rendering.TextureXR.GetMagentaTexture(), isBuiltin: true);
			blackTextureXR = renderGraph.ImportTexture(global::UnityEngine.Rendering.TextureXR.GetBlackTexture(), isBuiltin: true);
			blackTextureArrayXR = renderGraph.ImportTexture(global::UnityEngine.Rendering.TextureXR.GetBlackTextureArray(), isBuiltin: true);
			blackUIntTextureXR = renderGraph.ImportTexture(global::UnityEngine.Rendering.TextureXR.GetBlackUIntTexture(), isBuiltin: true);
			blackTexture3DXR = renderGraph.ImportTexture(global::UnityEngine.Rendering.TextureXR.GetBlackTexture3D(), isBuiltin: true);
			whiteTextureXR = renderGraph.ImportTexture(global::UnityEngine.Rendering.TextureXR.GetWhiteTexture(), isBuiltin: true);
		}
	}
}
