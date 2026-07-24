namespace UnityEngine.Rendering.RenderGraphModule
{
	public interface IRenderAttachmentRenderGraphBuilder : global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder, global::System.IDisposable
	{
		void SetRenderAttachment(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write)
		{
			SetRenderAttachment(tex, index, flags, 0, -1);
		}

		void SetRenderAttachment(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags, int mipLevel, int depthSlice);

		void SetRenderAttachmentDepth(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write)
		{
			SetRenderAttachmentDepth(tex, flags, 0, -1);
		}

		void SetRenderAttachmentDepth(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags, int mipLevel, int depthSlice);

		global::UnityEngine.Rendering.RenderGraphModule.TextureHandle SetRandomAccessAttachment(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);

		global::UnityEngine.Rendering.RenderGraphModule.BufferHandle UseBufferRandomAccess(global::UnityEngine.Rendering.RenderGraphModule.BufferHandle tex, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);

		global::UnityEngine.Rendering.RenderGraphModule.BufferHandle UseBufferRandomAccess(global::UnityEngine.Rendering.RenderGraphModule.BufferHandle tex, int index, bool preserveCounterValue, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);
	}
}
