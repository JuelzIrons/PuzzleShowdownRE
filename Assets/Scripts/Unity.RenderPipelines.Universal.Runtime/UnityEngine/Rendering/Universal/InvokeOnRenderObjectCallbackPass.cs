namespace UnityEngine.Rendering.Universal
{
	internal class InvokeOnRenderObjectCallbackPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorTarget;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTarget;
		}

		public InvokeOnRenderObjectCallbackPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Invoke OnRenderObject Callback");
			base.renderPassEvent = evt;
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorTarget, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTarget)
		{
			global::UnityEngine.Rendering.Universal.InvokeOnRenderObjectCallbackPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.InvokeOnRenderObjectCallbackPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\InvokeOnRenderObjectCallbackPass.cs", 42);
			passData.colorTarget = colorTarget;
			unsafeRenderGraphBuilder.UseTexture(in colorTarget, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			passData.depthTarget = depthTarget;
			unsafeRenderGraphBuilder.UseTexture(in depthTarget, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.InvokeOnRenderObjectCallbackPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				context.cmd.SetRenderTarget(data.colorTarget, data.depthTarget);
				context.cmd.InvokeOnRenderObjectCallbacks();
			});
		}
	}
}
