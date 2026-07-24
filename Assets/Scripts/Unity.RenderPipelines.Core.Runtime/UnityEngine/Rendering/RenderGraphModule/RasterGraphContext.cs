namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RasterGraphContext : global::UnityEngine.Rendering.RenderGraphModule.IDerivedRendergraphContext
	{
		private global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext wrappedContext;

		public global::UnityEngine.Rendering.RasterCommandBuffer cmd;

		internal static global::UnityEngine.Rendering.RasterCommandBuffer rastercmd = new global::UnityEngine.Rendering.RasterCommandBuffer(null, null, isAsync: false);

		public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDefaultResources defaultResources => wrappedContext.defaultResources;

		public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool renderGraphPool => wrappedContext.renderGraphPool;

		public void FromInternalContext(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext context)
		{
			wrappedContext = context;
			rastercmd.m_WrappedCommandBuffer = wrappedContext.cmd;
			rastercmd.m_ExecutingPass = context.executingPass;
			cmd = rastercmd;
		}

		public readonly global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin GetTextureUVOrigin(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle)
		{
			if (!global::UnityEngine.SystemInfo.graphicsUVStartsAtTop)
			{
				return global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft;
			}
			if (wrappedContext.compilerContext != null)
			{
				return wrappedContext.compilerContext.GetTextureUVOrigin(in textureHandle);
			}
			return global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft;
		}

		global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin global::UnityEngine.Rendering.RenderGraphModule.IDerivedRendergraphContext.GetTextureUVOrigin(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle)
		{
			return GetTextureUVOrigin(in textureHandle);
		}
	}
}
