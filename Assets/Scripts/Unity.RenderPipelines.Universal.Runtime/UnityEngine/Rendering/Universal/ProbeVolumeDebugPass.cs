namespace UnityEngine.Rendering.Universal
{
	internal class ProbeVolumeDebugPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class WriteApvData
		{
			public global::UnityEngine.ComputeShader computeShader;

			public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle resultBuffer;

			public global::UnityEngine.Vector2 clickCoordinates;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthBuffer;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle normalBuffer;
		}

		private global::UnityEngine.ComputeShader m_ComputeShader;

		public ProbeVolumeDebugPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.ComputeShader computeShader)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Dispatch APV Debug");
			base.renderPassEvent = evt;
			m_ComputeShader = computeShader;
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthPyramidBuffer, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle normalBuffer)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			if (!global::UnityEngine.Rendering.ProbeReferenceVolume.instance.isInitialized || !global::UnityEngine.Rendering.ProbeReferenceVolume.instance.GetProbeSamplingDebugResources(universalCameraData.camera, out var resultBuffer, out var coords))
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.ProbeVolumeDebugPass.WriteApvData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IComputeRenderGraphBuilder computeRenderGraphBuilder = renderGraph.AddComputePass<global::UnityEngine.Rendering.Universal.ProbeVolumeDebugPass.WriteApvData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\ProbeVolumeDebugPass.cs", 82);
			passData.clickCoordinates = coords;
			passData.computeShader = m_ComputeShader;
			passData.resultBuffer = renderGraph.ImportBuffer(resultBuffer);
			passData.depthBuffer = depthPyramidBuffer;
			passData.normalBuffer = normalBuffer;
			computeRenderGraphBuilder.UseBuffer(in passData.resultBuffer, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			computeRenderGraphBuilder.UseTexture(in passData.depthBuffer);
			computeRenderGraphBuilder.UseTexture(in passData.normalBuffer);
			computeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ProbeVolumeDebugPass.WriteApvData data, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext ctx)
			{
				int kernelIndex = data.computeShader.FindKernel("ComputePositionNormal");
				ctx.cmd.SetComputeTextureParam(data.computeShader, kernelIndex, "_CameraDepthTexture", data.depthBuffer);
				ctx.cmd.SetComputeTextureParam(data.computeShader, kernelIndex, "_NormalBufferTexture", data.normalBuffer);
				ctx.cmd.SetComputeVectorParam(data.computeShader, "_positionSS", new global::UnityEngine.Vector4(data.clickCoordinates.x, data.clickCoordinates.y, 0f, 0f));
				ctx.cmd.SetComputeBufferParam(data.computeShader, kernelIndex, "_ResultBuffer", data.resultBuffer);
				ctx.cmd.DispatchCompute(data.computeShader, kernelIndex, 1, 1, 1);
			});
		}
	}
}
