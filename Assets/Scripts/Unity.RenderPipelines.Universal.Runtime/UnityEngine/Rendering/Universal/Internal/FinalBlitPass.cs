namespace UnityEngine.Rendering.Universal.Internal
{
	public class FinalBlitPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private static class BlitPassNames
		{
			public const string NearestSampler = "NearestDebugDraw";

			public const string BilinearSampler = "BilinearDebugDraw";
		}

		private enum BlitType
		{
			Core = 0,
			HDR = 1,
			Count = 2
		}

		private struct BlitMaterialData
		{
			public global::UnityEngine.Material material;

			public int nearestSamplerPass;

			public int bilinearSamplerPass;
		}

		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;

			internal int sourceID;

			internal global::UnityEngine.Vector4 hdrOutputLuminanceParams;

			internal bool requireSrgbConversion;

			internal bool enableAlphaOutput;

			internal global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.BlitMaterialData blitMaterialData;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal bool useFullScreenViewport;
		}

		private static readonly int s_CameraDepthTextureID = global::UnityEngine.Shader.PropertyToID("_CameraDepthTexture");

		private global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.BlitMaterialData[] m_BlitMaterialData;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void OnCameraSetup(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public FinalBlitPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Material blitMaterial, global::UnityEngine.Material blitHDRMaterial)
		{
			base.profilingSampler = global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.BlitFinalToBackBuffer);
			base.renderPassEvent = evt;
			m_BlitMaterialData = new global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.BlitMaterialData[2];
			for (int i = 0; i < 2; i++)
			{
				m_BlitMaterialData[i].material = ((i == 0) ? blitMaterial : blitHDRMaterial);
				m_BlitMaterialData[i].nearestSamplerPass = m_BlitMaterialData[i].material?.FindPass("NearestDebugDraw") ?? (-1);
				m_BlitMaterialData[i].bilinearSamplerPass = m_BlitMaterialData[i].material?.FindPass("BilinearDebugDraw") ?? (-1);
			}
		}

		public void Dispose()
		{
		}

		[global::System.Obsolete("Use RTHandles for colorHandle. #from(2022.1) #breakingFrom(2023.1)", true)]
		public void Setup(global::UnityEngine.RenderTextureDescriptor baseDescriptor, global::UnityEngine.Rendering.Universal.RenderTargetHandle colorHandle)
		{
			throw new global::System.NotSupportedException("Setup with RenderTargetHandle has been deprecated. Use it with RTHandles instead.");
		}

		public void Setup(global::UnityEngine.RenderTextureDescriptor baseDescriptor, global::UnityEngine.Rendering.RTHandle colorHandle)
		{
		}

		private static void SetupHDROutput(global::UnityEngine.ColorGamut hdrDisplayColorGamut, global::UnityEngine.Material material, global::UnityEngine.Rendering.HDROutputUtils.Operation hdrOperation, global::UnityEngine.Vector4 hdrOutputParameters, bool rendersOverlayUI)
		{
			material.SetVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.hdrOutputLuminanceParams, hdrOutputParameters);
			global::UnityEngine.Rendering.HDROutputUtils.ConfigureHDROutput(material, hdrDisplayColorGamut, hdrOperation);
			global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, "_HDR_OVERLAY", rendersOverlayUI);
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.PassData data, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Vector4 scaleBias)
		{
			bool flag = !cameraData.isSceneViewCamera;
			if (cameraData.xr.enabled)
			{
				flag = new global::UnityEngine.Rendering.RenderTargetIdentifier(destination.nameID, 0, global::UnityEngine.CubemapFace.Unknown, -1) == new global::UnityEngine.Rendering.RenderTargetIdentifier(cameraData.xr.renderTarget, 0, global::UnityEngine.CubemapFace.Unknown, -1);
			}
			global::UnityEngine.Rect pixelRect = (data.useFullScreenViewport ? new global::UnityEngine.Rect(0f, 0f, global::UnityEngine.Screen.width, global::UnityEngine.Screen.height) : cameraData.pixelRect);
			global::UnityEngine.Rendering.Universal.RenderingUtils.SetupOffscreenUIViewportParams(data.blitMaterialData.material, ref pixelRect, flag);
			if (flag)
			{
				cmd.SetViewport(pixelRect);
			}
			cmd.SetWireframe(enable: false);
			global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.blitMaterialData.material, "_ENABLE_ALPHA_OUTPUT", data.enableAlphaOutput);
			global::UnityEngine.RenderTexture rt = source.rt;
			int pass = (((object)rt != null && rt.filterMode == global::UnityEngine.FilterMode.Bilinear) ? data.blitMaterialData.bilinearSamplerPass : data.blitMaterialData.nearestSamplerPass);
			global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, scaleBias, data.blitMaterialData.material, pass);
		}

		private void InitPassData(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, ref global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.PassData passData, global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.BlitType blitType, bool enableAlphaOutput, bool useFullScreenViewport)
		{
			passData.cameraData = cameraData;
			passData.requireSrgbConversion = cameraData.requireSrgbConversion;
			passData.enableAlphaOutput = enableAlphaOutput;
			passData.useFullScreenViewport = useFullScreenViewport;
			passData.blitMaterialData = m_BlitMaterialData[(int)blitType];
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle src, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dest, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture, bool useFullScreenViewport = false)
		{
			global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\FinalBlitPass.cs", 285);
			frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			bool flag = cameraData.renderer is global::UnityEngine.Rendering.Universal.UniversalRenderer;
			if (cameraData.requiresDepthTexture && flag)
			{
				rasterRenderGraphBuilder.UseGlobalTexture(s_CameraDepthTextureID);
			}
			bool isHDROutputActive = cameraData.isHDROutputActive;
			bool isAlphaOutputEnabled = cameraData.isAlphaOutputEnabled;
			InitPassData(cameraData, ref passData, isHDROutputActive ? global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.BlitType.HDR : global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.BlitType.Core, isAlphaOutputEnabled, useFullScreenViewport);
			passData.sourceID = global::UnityEngine.Rendering.Universal.ShaderPropertyId.sourceTex;
			passData.source = src;
			rasterRenderGraphBuilder.UseTexture(in src);
			passData.destination = dest;
			global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write;
			bool flag2 = !global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps.HasFlag(global::UnityEngine.Rendering.FoveatedRenderingCaps.NonUniformRaster);
			rasterRenderGraphBuilder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && flag2);
			rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			if (cameraData.xr.enabled && cameraData.isDefaultViewport && !isAlphaOutputEnabled)
			{
				flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll;
			}
			rasterRenderGraphBuilder.SetRenderAttachment(dest, 0, flags);
			if (isHDROutputActive && overlayUITexture.IsValid())
			{
				global::UnityEngine.Rendering.Universal.Tonemapping component = global::UnityEngine.Rendering.VolumeManager.instance.stack.GetComponent<global::UnityEngine.Rendering.Universal.Tonemapping>();
				global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.GetHDROutputLuminanceParameters(passData.cameraData.hdrDisplayInformation, passData.cameraData.hdrDisplayColorGamut, component, out passData.hdrOutputLuminanceParams);
				rasterRenderGraphBuilder.UseTexture(in overlayUITexture);
			}
			else
			{
				passData.hdrOutputLuminanceParams = new global::UnityEngine.Vector4(-1f, -1f, -1f, -1f);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.FinalBlitPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				data.blitMaterialData.material.enabledKeywords = null;
				context.cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LinearToSRGBConversion, data.requireSrgbConversion);
				data.blitMaterialData.material.SetTexture(data.sourceID, data.source);
				global::UnityEngine.Rendering.Universal.DebugHandler activeDebugHandler = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(data.cameraData);
				bool num = activeDebugHandler?.WriteToDebugScreenTexture(data.cameraData.resolveFinalTarget) ?? false;
				if (data.hdrOutputLuminanceParams.w >= 0f)
				{
					global::UnityEngine.Rendering.HDROutputUtils.Operation operation = global::UnityEngine.Rendering.HDROutputUtils.Operation.None;
					if (activeDebugHandler == null || !activeDebugHandler.HDRDebugViewIsActive(data.cameraData.resolveFinalTarget))
					{
						operation |= global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding;
					}
					if (!data.cameraData.postProcessEnabled)
					{
						operation |= global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion;
					}
					SetupHDROutput(data.cameraData.hdrDisplayColorGamut, data.blitMaterialData.material, operation, data.hdrOutputLuminanceParams, data.cameraData.rendersOverlayUI);
				}
				if (num)
				{
					global::UnityEngine.Rendering.RTHandle rTHandle = data.source;
					global::UnityEngine.Vector2 vector = (rTHandle.useScaling ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
					global::UnityEngine.RenderTexture rt = rTHandle.rt;
					int pass = (((object)rt != null && rt.filterMode == global::UnityEngine.FilterMode.Bilinear) ? data.blitMaterialData.bilinearSamplerPass : data.blitMaterialData.nearestSamplerPass);
					global::UnityEngine.Rendering.Blitter.BlitTexture(context.cmd, rTHandle, vector, data.blitMaterialData.material, pass);
				}
				else
				{
					global::UnityEngine.Vector4 finalBlitScaleBias = global::UnityEngine.Rendering.Universal.RenderingUtils.GetFinalBlitScaleBias(in context, in data.source, in data.destination);
					ExecutePass(context.cmd, data, data.source, data.destination, data.cameraData, finalBlitScaleBias);
				}
			});
		}
	}
}
