namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public interface IUnsafeRenderGraphBuilder : global::UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder, global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder, global::System.IDisposable
	{
		void SetRenderFunc<PassData>(global::UnityEngine.Rendering.RenderGraphModule.BaseRenderFunc<PassData, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext> renderFunc) where PassData : class, new();
	}
}
