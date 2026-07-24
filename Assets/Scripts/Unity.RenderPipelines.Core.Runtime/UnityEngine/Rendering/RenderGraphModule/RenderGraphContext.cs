namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	[global::System.Obsolete("RenderGraphContext is deprecated, use RasterGraphContext/ComputeGraphContext/UnsafeGraphContext instead.")]
	public struct RenderGraphContext : global::UnityEngine.Rendering.RenderGraphModule.IDerivedRendergraphContext
	{
		private global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext wrappedContext;

		public global::UnityEngine.Rendering.ScriptableRenderContext renderContext => wrappedContext.renderContext;

		public global::UnityEngine.Rendering.CommandBuffer cmd => wrappedContext.cmd;

		public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool renderGraphPool => wrappedContext.renderGraphPool;

		public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDefaultResources defaultResources => wrappedContext.defaultResources;

		public void FromInternalContext(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext context)
		{
			wrappedContext = context;
		}

		public readonly global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin GetTextureUVOrigin(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft;
		}

		global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin global::UnityEngine.Rendering.RenderGraphModule.IDerivedRendergraphContext.GetTextureUVOrigin(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle)
		{
			return GetTextureUVOrigin(in textureHandle);
		}
	}
}
