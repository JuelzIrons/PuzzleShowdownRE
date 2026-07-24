namespace UnityEngine.Rendering.Universal
{
	internal class CapturePass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class UnsafePassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;

			public global::System.Collections.Generic.IEnumerator<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>> captureActions;
		}

		public CapturePass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Capture Camera output");
			base.renderPassEvent = evt;
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.CapturePass.UnsafePassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.CapturePass.UnsafePassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\CapturePass.cs", 59);
			passData.source = universalResourceData.cameraColor;
			passData.captureActions = universalCameraData.captureActions;
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.UseTexture(universalResourceData.cameraColor);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.CapturePass.UnsafePassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext unsafeContext)
			{
				global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(unsafeContext.cmd);
				global::System.Collections.Generic.IEnumerator<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>> captureActions = data.captureActions;
				data.captureActions.Reset();
				while (data.captureActions.MoveNext())
				{
					captureActions.Current(data.source, nativeCommandBuffer);
				}
			});
		}
	}
}
