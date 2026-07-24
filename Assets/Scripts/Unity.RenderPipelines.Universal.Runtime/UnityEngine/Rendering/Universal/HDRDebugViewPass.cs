namespace UnityEngine.Rendering.Universal
{
	internal class HDRDebugViewPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private enum HDRDebugPassId
		{
			CIExyPrepass = 0,
			DebugViewPass = 1
		}

		private class PassDataCIExy
		{
			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Vector4 luminanceParameters;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcColor;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle xyBuffer;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle passThrough;
		}

		private class PassDataDebugView
		{
			internal global::UnityEngine.Material material;

			internal global::UnityEngine.Rendering.Universal.HDRDebugMode hdrDebugMode;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Vector4 luminanceParameters;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle xyBuffer;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcColor;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dstColor;
		}

		internal class ShaderConstants
		{
			public static readonly int _DebugHDRModeId = global::UnityEngine.Shader.PropertyToID("_DebugHDRMode");

			public static readonly int _HDRDebugParamsId = global::UnityEngine.Shader.PropertyToID("_HDRDebugParams");

			public static readonly int _xyTextureId = global::UnityEngine.Shader.PropertyToID("_xyBuffer");

			public static readonly int _SizeOfHDRXYMapping = 512;

			public static readonly int _CIExyUAVIndex = 1;
		}

		private global::UnityEngine.Rendering.RTHandle m_PassthroughRT;

		private global::UnityEngine.Material m_material;

		public HDRDebugViewPass(global::UnityEngine.Material mat)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Blit HDR Debug Data");
			base.renderPassEvent = (global::UnityEngine.Rendering.Universal.RenderPassEvent)1003;
			m_material = mat;
		}

		public static void ConfigureDescriptorForCIEPrepass(ref global::UnityEngine.RenderTextureDescriptor descriptor)
		{
			descriptor.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat;
			int width = (descriptor.height = global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping);
			descriptor.width = width;
			descriptor.useMipMap = false;
			descriptor.autoGenerateMips = false;
			descriptor.useDynamicScale = true;
			descriptor.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			descriptor.enableRandomWrite = true;
			descriptor.msaaSamples = 1;
			descriptor.dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D;
			descriptor.vrUsage = global::UnityEngine.VRTextureUsage.None;
		}

		internal static global::UnityEngine.Vector4 GetLuminanceParameters(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Vector4 hdrOutputParameters = global::UnityEngine.Vector4.zero;
			if (cameraData.isHDROutputActive)
			{
				global::UnityEngine.Rendering.Universal.Tonemapping component = global::UnityEngine.Rendering.VolumeManager.instance.stack.GetComponent<global::UnityEngine.Rendering.Universal.Tonemapping>();
				global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.GetHDROutputLuminanceParameters(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, component, out hdrOutputParameters);
			}
			else
			{
				hdrOutputParameters.z = 1f;
			}
			return hdrOutputParameters;
		}

		private static void ExecuteCIExyPrepass(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.HDRDebugViewPass.PassDataCIExy data, global::UnityEngine.Rendering.RTHandle sourceTexture, global::UnityEngine.Rendering.RTHandle xyTarget, global::UnityEngine.Rendering.RTHandle destTexture)
		{
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destTexture, global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare, global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare, global::UnityEngine.Rendering.ClearFlag.None, global::UnityEngine.Color.clear);
			global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping, global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping, 0f, 0f);
			cmd.SetRandomWriteTarget(global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._CIExyUAVIndex, xyTarget);
			data.material.SetVector(global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._HDRDebugParamsId, value);
			data.material.SetVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.hdrOutputLuminanceParams, data.luminanceParameters);
			global::UnityEngine.Vector2 vector = (sourceTexture.useScaling ? new global::UnityEngine.Vector2(sourceTexture.rtHandleProperties.rtHandleScale.x, sourceTexture.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, sourceTexture, vector, data.material, 0);
			cmd.ClearRandomWriteTargets();
		}

		private static void ExecuteHDRDebugViewFinalPass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, in global::UnityEngine.Rendering.Universal.HDRDebugViewPass.PassDataDebugView data, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Rendering.RTHandle xyTarget)
		{
			if (data.cameraData.isHDROutputActive)
			{
				global::UnityEngine.Rendering.HDROutputUtils.ConfigureHDROutput(data.material, data.cameraData.hdrDisplayColorGamut, global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(data.material, "_HDR_OVERLAY", data.cameraData.rendersOverlayUI);
			}
			data.material.SetTexture(global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._xyTextureId, xyTarget);
			global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping, global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping, 0f, 0f);
			data.material.SetVector(global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._HDRDebugParamsId, value);
			data.material.SetVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.hdrOutputLuminanceParams, data.luminanceParameters);
			data.material.SetInteger(global::UnityEngine.Rendering.Universal.HDRDebugViewPass.ShaderConstants._DebugHDRModeId, (int)data.hdrDebugMode);
			global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier = global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget;
			if (data.cameraData.xr.enabled)
			{
				renderTargetIdentifier = data.cameraData.xr.renderTarget;
			}
			if (destination.nameID == renderTargetIdentifier || data.cameraData.targetTexture != null)
			{
				cmd.SetViewport(data.cameraData.pixelRect);
			}
			global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, scaleBias, data.material, 1);
		}

		public void Dispose()
		{
			m_PassthroughRT?.Release();
		}

		public void Setup(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.HDRDebugMode hdrdebugMode)
		{
			global::UnityEngine.RenderTextureDescriptor descriptor = cameraData.cameraTargetDescriptor;
			global::UnityEngine.Rendering.Universal.DebugHandler.ConfigureColorDescriptorForDebugScreen(ref descriptor, cameraData.pixelWidth, cameraData.pixelHeight);
			global::UnityEngine.Rendering.Universal.RenderingUtils.ReAllocateHandleIfNeeded(ref m_PassthroughRT, in descriptor, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Repeat, 1, 0f, "_HDRDebugDummyRT");
		}

		internal void RenderHDRDebug(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle srcColor, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dstColor, global::UnityEngine.Rendering.Universal.HDRDebugMode hdrDebugMode)
		{
			bool flag = hdrDebugMode != global::UnityEngine.Rendering.Universal.HDRDebugMode.ValuesAbovePaperWhite;
			global::UnityEngine.Vector4 luminanceParameters = GetLuminanceParameters(cameraData);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle = srcColor;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle xyBuffer = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			if (flag)
			{
				global::UnityEngine.RenderTextureDescriptor descriptor = cameraData.cameraTargetDescriptor;
				global::UnityEngine.Rendering.Universal.DebugHandler.ConfigureColorDescriptorForDebugScreen(ref descriptor, cameraData.pixelWidth, cameraData.pixelHeight);
				textureHandle = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_HDRDebugDummyRT", clear: false);
				ConfigureDescriptorForCIEPrepass(ref descriptor);
				xyBuffer = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_xyBuffer", clear: true);
				global::UnityEngine.Rendering.Universal.HDRDebugViewPass.PassDataCIExy passData;
				using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.HDRDebugViewPass.PassDataCIExy>("Blit HDR DebugView CIExy", out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\HDRDebugViewPass.cs", 245);
				passData.material = m_material;
				passData.luminanceParameters = luminanceParameters;
				passData.srcColor = srcColor;
				unsafeRenderGraphBuilder.UseTexture(in srcColor);
				passData.xyBuffer = xyBuffer;
				unsafeRenderGraphBuilder.UseTexture(in xyBuffer, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
				passData.passThrough = textureHandle;
				unsafeRenderGraphBuilder.UseTexture(in textureHandle, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
				unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.HDRDebugViewPass.PassDataCIExy data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
				{
					ExecuteCIExyPrepass(global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(context.cmd), data, data.srcColor, data.xyBuffer, data.passThrough);
				});
			}
			global::UnityEngine.Rendering.Universal.HDRDebugViewPass.PassDataDebugView passData2;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.HDRDebugViewPass.PassDataDebugView>("Blit HDR DebugView", out passData2, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\HDRDebugViewPass.cs", 263);
			passData2.material = m_material;
			passData2.hdrDebugMode = hdrDebugMode;
			passData2.luminanceParameters = luminanceParameters;
			passData2.cameraData = cameraData;
			if (flag)
			{
				passData2.xyBuffer = xyBuffer;
				rasterRenderGraphBuilder.UseTexture(in xyBuffer);
			}
			passData2.srcColor = srcColor;
			rasterRenderGraphBuilder.UseTexture(in srcColor);
			passData2.dstColor = dstColor;
			rasterRenderGraphBuilder.SetRenderAttachment(dstColor, 0, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
			if (overlayUITexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in overlayUITexture);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.HDRDebugViewPass.PassDataDebugView data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				data.material.enabledKeywords = null;
				global::UnityEngine.Vector4 finalBlitScaleBias = global::UnityEngine.Rendering.Universal.RenderingUtils.GetFinalBlitScaleBias(in context, in data.srcColor, in data.dstColor);
				ExecuteHDRDebugViewFinalPass(context.cmd, in data, data.srcColor, finalBlitScaleBias, data.dstColor, data.xyBuffer);
			});
		}
	}
}
