namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal sealed class ComputeRenderGraphPass<PassData> : global::UnityEngine.Rendering.RenderGraphModule.BaseRenderGraphPass<PassData, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext> where PassData : class, new()
	{
		internal static global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext c = new global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext();

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override void Execute(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext renderGraphContext)
		{
			c.FromInternalContext(renderGraphContext);
			renderFunc(data, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override void Release(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool pool)
		{
			base.Release(pool);
			pool.Release(this);
		}
	}
}
