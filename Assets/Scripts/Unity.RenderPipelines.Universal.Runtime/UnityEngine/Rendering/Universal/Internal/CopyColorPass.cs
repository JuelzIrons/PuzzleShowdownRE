namespace UnityEngine.Rendering.Universal.Internal
{
	public class CopyColorPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;

			internal bool useProceduralBlit;

			internal global::UnityEngine.Material samplingMaterial;

			internal global::UnityEngine.Material copyColorMaterial;

			internal global::UnityEngine.Rendering.Universal.Downsampling downsamplingMethod;

			internal int sampleOffsetShaderHandle;
		}

		private int m_SampleOffsetShaderHandle;

		private global::UnityEngine.Material m_SamplingMaterial;

		private global::UnityEngine.Rendering.Universal.Downsampling m_DownsamplingMethod;

		private global::UnityEngine.Material m_CopyColorMaterial;

		private static readonly string k_CopyColorPassName = "Copy Color";

		private static readonly string k_DownsampleAndCopyPassName = "Downsample Color";

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void OnCameraSetup(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public CopyColorPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Material samplingMaterial, global::UnityEngine.Material copyColorMaterial = null, string customPassName = null)
		{
			base.profilingSampler = ((customPassName != null) ? new global::UnityEngine.Rendering.ProfilingSampler(customPassName) : global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.CopyColor));
			m_SamplingMaterial = samplingMaterial;
			m_CopyColorMaterial = copyColorMaterial;
			m_SampleOffsetShaderHandle = global::UnityEngine.Shader.PropertyToID("_SampleOffset");
			base.renderPassEvent = evt;
			m_DownsamplingMethod = global::UnityEngine.Rendering.Universal.Downsampling.None;
		}

		public static void ConfigureDescriptor(global::UnityEngine.Rendering.Universal.Downsampling downsamplingMethod, ref global::UnityEngine.RenderTextureDescriptor descriptor, out global::UnityEngine.FilterMode filterMode)
		{
			descriptor.msaaSamples = 1;
			descriptor.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			switch (downsamplingMethod)
			{
			case global::UnityEngine.Rendering.Universal.Downsampling._2xBilinear:
				descriptor.width = global::UnityEngine.Mathf.Max(1, descriptor.width / 2);
				descriptor.height = global::UnityEngine.Mathf.Max(1, descriptor.height / 2);
				break;
			case global::UnityEngine.Rendering.Universal.Downsampling._4xBox:
			case global::UnityEngine.Rendering.Universal.Downsampling._4xBilinear:
				descriptor.width = global::UnityEngine.Mathf.Max(1, descriptor.width / 4);
				descriptor.height = global::UnityEngine.Mathf.Max(1, descriptor.height / 4);
				break;
			}
			filterMode = ((downsamplingMethod != global::UnityEngine.Rendering.Universal.Downsampling.None) ? global::UnityEngine.FilterMode.Bilinear : global::UnityEngine.FilterMode.Point);
		}

		[global::System.Obsolete("Use RTHandles for source and destination #from(2022.1) #breakingFrom(2023.1).", true)]
		public void Setup(global::UnityEngine.Rendering.RenderTargetIdentifier source, global::UnityEngine.Rendering.Universal.RenderTargetHandle destination, global::UnityEngine.Rendering.Universal.Downsampling downsampling)
		{
			throw new global::System.NotSupportedException("Setup with RenderTargetIdentifier has been deprecated. Use it with RTHandles instead.");
		}

		public void Setup(global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Rendering.Universal.Downsampling downsampling)
		{
			m_DownsamplingMethod = downsampling;
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Internal.CopyColorPass.PassData passData, global::UnityEngine.Rendering.RTHandle source, bool useDrawProceduralBlit)
		{
			global::UnityEngine.Material samplingMaterial = passData.samplingMaterial;
			global::UnityEngine.Material copyColorMaterial = passData.copyColorMaterial;
			global::UnityEngine.Rendering.Universal.Downsampling downsamplingMethod = passData.downsamplingMethod;
			int sampleOffsetShaderHandle = passData.sampleOffsetShaderHandle;
			if (samplingMaterial == null)
			{
				global::UnityEngine.Debug.LogErrorFormat("Missing {0}. Copy Color render pass will not execute. Check for missing reference in the renderer resources.", samplingMaterial);
				return;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.CopyColor)))
			{
				global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
				switch (downsamplingMethod)
				{
				case global::UnityEngine.Rendering.Universal.Downsampling.None:
					global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, vector, copyColorMaterial, 0);
					break;
				case global::UnityEngine.Rendering.Universal.Downsampling._2xBilinear:
					global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, vector, copyColorMaterial, 1);
					break;
				case global::UnityEngine.Rendering.Universal.Downsampling._4xBox:
					samplingMaterial.SetFloat(sampleOffsetShaderHandle, 2f);
					global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, vector, samplingMaterial, 0);
					break;
				case global::UnityEngine.Rendering.Universal.Downsampling._4xBilinear:
					global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, vector, copyColorMaterial, 1);
					break;
				}
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.Universal.Downsampling downsampling)
		{
			m_DownsamplingMethod = downsampling;
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.RenderTextureDescriptor descriptor = universalCameraData.cameraTargetDescriptor;
			ConfigureDescriptor(downsampling, ref descriptor, out var filterMode);
			destination = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_CameraOpaqueTexture", clear: true, filterMode);
			RenderInternal(renderGraph, in destination, in source, universalCameraData.xr.enabled);
			return destination;
		}

		internal void RenderToExistingTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.Universal.Downsampling downsampling = global::UnityEngine.Rendering.Universal.Downsampling.None)
		{
			m_DownsamplingMethod = downsampling;
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			RenderInternal(renderGraph, in destination, in source, universalCameraData.xr.enabled);
		}

		private void RenderInternal(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, bool useProceduralBlit)
		{
			bool flag = global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3;
			if (m_DownsamplingMethod != global::UnityEngine.Rendering.Universal.Downsampling.None || flag)
			{
				AddDownsampleAndCopyColorRenderPass(renderGraph, in destination, in source, useProceduralBlit, k_DownsampleAndCopyPassName);
				return;
			}
			using global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder baseRenderGraphBuilder = global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.AddBlitPass(renderGraph, source, destination, global::UnityEngine.Vector2.one, global::UnityEngine.Vector2.zero, 0, 0, -1, 0, 0, 1, global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitFilterMode.ClampBilinear, k_CopyColorPassName, returnBuilder: true, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\CopyColorPass.cs", 232);
			baseRenderGraphBuilder.SetGlobalTextureAfterPass(in destination, global::UnityEngine.Shader.PropertyToID("_CameraOpaqueTexture"));
		}

		private void AddDownsampleAndCopyColorRenderPass(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, bool useProceduralBlit, string passName)
		{
			global::UnityEngine.Rendering.Universal.Internal.CopyColorPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.CopyColorPass.PassData>(passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\CopyColorPass.cs", 241);
			rasterRenderGraphBuilder.SetRenderAttachment(destination, 0, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
			passData.source = source;
			rasterRenderGraphBuilder.UseTexture(in source);
			passData.useProceduralBlit = useProceduralBlit;
			passData.samplingMaterial = m_SamplingMaterial;
			passData.copyColorMaterial = m_CopyColorMaterial;
			passData.downsamplingMethod = m_DownsamplingMethod;
			passData.sampleOffsetShaderHandle = m_SampleOffsetShaderHandle;
			rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in destination, global::UnityEngine.Shader.PropertyToID("_CameraOpaqueTexture"));
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.CopyColorPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				ExecutePass(context.cmd, data, data.source, data.useProceduralBlit);
			});
		}
	}
}
