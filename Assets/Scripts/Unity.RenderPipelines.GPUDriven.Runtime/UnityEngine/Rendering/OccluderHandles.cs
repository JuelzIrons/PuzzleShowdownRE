namespace UnityEngine.Rendering
{
	internal struct OccluderHandles
	{
		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle occluderDepthPyramid;

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle occlusionDebugOverlay;

		public bool IsValid()
		{
			return occluderDepthPyramid.IsValid();
		}

		public void UseForOcclusionTest(global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder builder)
		{
			builder.UseTexture(in occluderDepthPyramid);
			if (occlusionDebugOverlay.IsValid())
			{
				builder.UseBuffer(in occlusionDebugOverlay, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			}
		}

		public void UseForOccluderUpdate(global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder builder)
		{
			builder.UseTexture(in occluderDepthPyramid, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			if (occlusionDebugOverlay.IsValid())
			{
				builder.UseBuffer(in occlusionDebugOverlay, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			}
		}
	}
}
