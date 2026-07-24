namespace UnityEngine.Rendering.RenderGraphModule
{
	internal interface IDerivedRendergraphContext
	{
		void FromInternalContext(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext context);

		global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin GetTextureUVOrigin(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle);
	}
}
