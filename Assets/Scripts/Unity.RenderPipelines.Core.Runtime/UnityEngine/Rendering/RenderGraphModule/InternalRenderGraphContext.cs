namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public class InternalRenderGraphContext
	{
		internal global::UnityEngine.Rendering.ScriptableRenderContext renderContext;

		internal global::UnityEngine.Rendering.CommandBuffer cmd;

		internal global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool renderGraphPool;

		internal global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDefaultResources defaultResources;

		internal global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass executingPass;

		internal global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compilerContext;

		internal bool contextlessTesting;

		internal bool forceResourceCreation;
	}
}
