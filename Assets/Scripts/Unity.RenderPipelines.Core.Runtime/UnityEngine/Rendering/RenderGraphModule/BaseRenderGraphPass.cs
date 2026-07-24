namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal abstract class BaseRenderGraphPass<PassData, TRenderGraphContext> : global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass where PassData : class, new()
	{
		internal PassData data;

		internal global::UnityEngine.Rendering.RenderGraphModule.BaseRenderFunc<PassData, TRenderGraphContext> renderFunc;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void Initialize(int passIndex, PassData passData, string passName, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType passType, global::UnityEngine.Rendering.ProfilingSampler sampler)
		{
			Clear();
			base.index = passIndex;
			data = passData;
			base.name = passName;
			base.type = passType;
			base.customSampler = sampler;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override void Release(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool pool)
		{
			pool.Release(data);
			data = null;
			renderFunc = null;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override bool HasRenderFunc()
		{
			return renderFunc != null;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override int GetRenderFuncHash()
		{
			if (renderFunc == null)
			{
				return 0;
			}
			return global::UnityEngine.Rendering.DelegateHashCodeUtils.GetFuncHashCode(renderFunc);
		}
	}
}
