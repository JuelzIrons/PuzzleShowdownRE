namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public interface IBaseRenderGraphBuilder : global::System.IDisposable
	{
		void UseTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);

		void UseGlobalTexture(int propertyId, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);

		void UseAllGlobalTextures(bool enable);

		void SetGlobalTextureAfterPass(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input, int propertyId);

		global::UnityEngine.Rendering.RenderGraphModule.BufferHandle UseBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);

		global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc);

		global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture);

		global::UnityEngine.Rendering.RenderGraphModule.BufferHandle CreateTransientBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc);

		global::UnityEngine.Rendering.RenderGraphModule.BufferHandle CreateTransientBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle computebuffer);

		void UseRendererList(in global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle input);

		void EnableAsyncCompute(bool value);

		void AllowPassCulling(bool value);

		void AllowGlobalStateModification(bool value);

		void EnableFoveatedRasterization(bool value);

		void GenerateDebugData(bool value);
	}
}
