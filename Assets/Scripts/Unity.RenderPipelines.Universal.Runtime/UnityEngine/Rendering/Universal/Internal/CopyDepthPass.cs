namespace UnityEngine.Rendering.Universal.Internal
{
	public class CopyDepthPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private static class ShaderConstants
		{
			public static readonly int _CameraDepthAttachment = global::UnityEngine.Shader.PropertyToID("_CameraDepthAttachment");

			public static readonly int _CameraDepthTexture = global::UnityEngine.Shader.PropertyToID("_CameraDepthTexture");

			public static readonly int _ZWriteShaderHandle = global::UnityEngine.Shader.PropertyToID("_ZWrite");
		}

		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Material copyDepthMaterial;

			internal int msaaSamples;

			internal bool copyResolvedDepth;

			internal bool copyToDepth;

			internal bool isDstBackbuffer;
		}

		private global::UnityEngine.Material m_CopyDepthMaterial;

		internal bool m_CopyResolvedDepth;

		internal int MsaaSamples { get; set; }

		internal bool CopyToDepth { get; set; }

		internal bool CopyToDepthXR { get; set; }

		internal bool CopyToBackbuffer { get; set; }

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void OnCameraSetup(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public CopyDepthPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Shader copyDepthShader, bool shouldClear = false, bool copyToDepth = false, bool copyResolvedDepth = false, string customPassName = null)
		{
			base.profilingSampler = ((customPassName != null) ? new global::UnityEngine.Rendering.ProfilingSampler(customPassName) : global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.CopyDepth));
			CopyToDepth = copyToDepth;
			m_CopyDepthMaterial = ((copyDepthShader != null) ? global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(copyDepthShader) : null);
			base.renderPassEvent = evt;
			m_CopyResolvedDepth = copyResolvedDepth;
			CopyToDepthXR = false;
			CopyToBackbuffer = false;
		}

		public void Setup(global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination)
		{
			MsaaSamples = -1;
		}

		public void Dispose()
		{
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_CopyDepthMaterial);
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass.PassData passData, global::UnityEngine.Rendering.RTHandle source, bool yflip)
		{
			global::UnityEngine.Material copyDepthMaterial = passData.copyDepthMaterial;
			int msaaSamples = passData.msaaSamples;
			bool copyResolvedDepth = passData.copyResolvedDepth;
			bool copyToDepth = passData.copyToDepth;
			if (copyDepthMaterial == null)
			{
				global::UnityEngine.Debug.LogErrorFormat("Missing {0}. Copy Depth render pass will not execute. Check for missing reference in the renderer resources.", copyDepthMaterial);
				return;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.CopyDepth)))
			{
				int num = 0;
				switch ((copyResolvedDepth || global::UnityEngine.SystemInfo.supportsMultisampledTextures == 0) ? 1 : ((msaaSamples != -1) ? msaaSamples : source.rt.antiAliasing))
				{
				case 8:
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa2, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa4, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa8, value: true);
					break;
				case 4:
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa2, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa4, value: true);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa8, value: false);
					break;
				case 2:
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa2, value: true);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa4, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa8, value: false);
					break;
				default:
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa2, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa4, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.DepthMsaa8, value: false);
					break;
				}
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords._OUTPUT_DEPTH, copyToDepth);
				global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
				global::UnityEngine.Vector4 scaleBias = (yflip ? new global::UnityEngine.Vector4(vector.x, 0f - vector.y, 0f, vector.y) : new global::UnityEngine.Vector4(vector.x, vector.y, 0f, 0f));
				if (passData.isDstBackbuffer)
				{
					cmd.SetViewport(passData.cameraData.pixelRect);
				}
				copyDepthMaterial.SetTexture(global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass.ShaderConstants._CameraDepthAttachment, source);
				copyDepthMaterial.SetFloat(global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass.ShaderConstants._ZWriteShaderHandle, copyToDepth ? 1f : 0f);
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, scaleBias, copyDepthMaterial, 0);
			}
		}

		public override void OnCameraCleanup(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
		}

		public void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, bool bindAsCameraDepth = false, string passName = "Copy Depth")
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			Render(renderGraph, destination, source, resourceData, cameraData, bindAsCameraDepth, passName);
		}

		public void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool bindAsCameraDepth = false, string passName = "Copy Depth")
		{
			MsaaSamples = -1;
			global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass.PassData>(passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\CopyDepthPass.cs", 281);
			passData.copyDepthMaterial = m_CopyDepthMaterial;
			passData.msaaSamples = MsaaSamples;
			passData.cameraData = cameraData;
			passData.copyResolvedDepth = m_CopyResolvedDepth;
			passData.copyToDepth = CopyToDepth || CopyToDepthXR;
			passData.isDstBackbuffer = CopyToBackbuffer || CopyToDepthXR;
			if (cameraData.xr.enabled)
			{
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			if (CopyToDepth)
			{
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(destination, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
			}
			else if (CopyToDepthXR)
			{
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(destination, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				if (cameraData.xr.enabled && cameraData.xr.copyDepth)
				{
					global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo = renderGraph.GetRenderTargetInfo(resourceData.backBufferColor);
					if (renderTargetInfo.msaaSamples > 1)
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(renderTargetInfo.width, renderTargetInfo.height, dynamicResolution: false, xrReady: true)
						{
							name = "XR Copy Depth Dummy Render Target",
							slices = renderTargetInfo.volumeDepth,
							format = renderTargetInfo.format,
							msaaSamples = (global::UnityEngine.Rendering.MSAASamples)renderTargetInfo.msaaSamples,
							clearBuffer = false,
							bindTextureMS = renderTargetInfo.bindMS
						};
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex = renderGraph.CreateTexture(in desc);
						rasterRenderGraphBuilder.SetRenderAttachment(tex, 0);
					}
					else
					{
						rasterRenderGraphBuilder.SetRenderAttachment(resourceData.backBufferColor, 0);
					}
				}
			}
			else
			{
				rasterRenderGraphBuilder.SetRenderAttachment(destination, 0, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
			}
			passData.source = source;
			passData.destination = destination;
			rasterRenderGraphBuilder.UseTexture(in source);
			if (bindAsCameraDepth && destination.IsValid())
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in destination, global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass.ShaderConstants._CameraDepthTexture);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.CopyDepthPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				bool yflip = context.GetTextureUVOrigin(in data.source) != context.GetTextureUVOrigin(in data.destination);
				ExecutePass(context.cmd, data, data.source, yflip);
			});
		}
	}
}
