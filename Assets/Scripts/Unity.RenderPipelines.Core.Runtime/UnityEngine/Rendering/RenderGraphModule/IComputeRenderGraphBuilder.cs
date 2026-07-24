namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public interface IComputeRenderGraphBuilder : global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder, global::System.IDisposable
	{
		void SetRenderFunc<PassData>(global::UnityEngine.Rendering.RenderGraphModule.BaseRenderFunc<PassData, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext> renderFunc) where PassData : class, new();
	}
}
