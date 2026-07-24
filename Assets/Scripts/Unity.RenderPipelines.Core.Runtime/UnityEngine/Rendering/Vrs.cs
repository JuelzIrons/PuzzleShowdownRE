namespace UnityEngine.Rendering
{
	public static class Vrs
	{
		private class ConversionPassData
		{
			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sriTextureHandle;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle mainTexHandle;

			public global::UnityEngine.Rendering.TextureDimension mainTexDimension;

			public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle mainTexLutHandle;

			public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle validatedShadingRateFragmentSizeHandle;

			public global::UnityEngine.ComputeShader computeShader;

			public int kernelIndex;

			public global::UnityEngine.Vector4 scaleBias;

			public global::UnityEngine.Vector2Int dispatchSize;

			public bool yFlip;
		}

		private class VisualizationPassData
		{
			public global::UnityEngine.Material material;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;

			public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle lut;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dummy;

			public global::UnityEngine.Vector4 visualizationParams;
		}

		internal static readonly int shadingRateFragmentSizeCount = global::System.Enum.GetNames(typeof(global::UnityEngine.Rendering.ShadingRateFragmentSize)).Length;

		private static global::UnityEngine.Rendering.VrsResources s_VrsResources;

		public static bool IsColorMaskTextureConversionSupported()
		{
			if (global::UnityEngine.SystemInfo.supportsComputeShaders && global::UnityEngine.Rendering.ShadingRateInfo.supportsPerImageTile)
			{
				return IsInitialized();
			}
			return false;
		}

		public static bool IsInitialized()
		{
			if (s_VrsResources != null && s_VrsResources.textureComputeShader != null && s_VrsResources.textureReduceKernel != -1)
			{
				return s_VrsResources.textureCopyKernel != -1;
			}
			return false;
		}

		public static void InitializeResources()
		{
			bool flag = global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore && global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3;
			if (global::UnityEngine.SystemInfo.supportsComputeShaders && flag)
			{
				s_VrsResources = new global::UnityEngine.Rendering.VrsResources(global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.VrsRenderPipelineRuntimeResources>());
			}
		}

		public static void DisposeResources()
		{
			s_VrsResources?.Dispose();
			s_VrsResources = null;
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ColorMaskTextureToShadingRateImage(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RTHandle sriRtHandle, global::UnityEngine.Rendering.RTHandle colorMaskRtHandle, bool yFlip)
		{
			if (renderGraph == null || sriRtHandle == null || colorMaskRtHandle == null)
			{
				global::UnityEngine.Debug.LogError("TextureToShadingRateImage: invalid argument.");
				return global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sriTextureHandle = renderGraph.ImportShadingRateImageTexture(sriRtHandle);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorMaskHandle = renderGraph.ImportTexture(colorMaskRtHandle);
			return ColorMaskTextureToShadingRateImage(renderGraph, sriTextureHandle, colorMaskHandle, ((global::UnityEngine.Texture)colorMaskRtHandle).dimension, yFlip);
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ColorMaskTextureToShadingRateImage(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sriTextureHandle, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorMaskHandle, global::UnityEngine.Rendering.TextureDimension colorMaskDimension, bool yFlip)
		{
			if (!IsColorMaskTextureConversionSupported())
			{
				global::UnityEngine.Debug.LogError("ColorMaskTextureToShadingRateImage: conversion not supported.");
				return global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor = sriTextureHandle.GetDescriptor(renderGraph);
			if (descriptor.dimension != global::UnityEngine.Rendering.TextureDimension.Tex2D)
			{
				global::UnityEngine.Debug.LogError("ColorMaskTextureToShadingRateImage: Vrs image not a texture 2D.");
				return global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			if (colorMaskDimension != global::UnityEngine.Rendering.TextureDimension.Tex2D && colorMaskDimension != global::UnityEngine.Rendering.TextureDimension.Tex2DArray)
			{
				global::UnityEngine.Debug.LogError("ColorMaskTextureToShadingRateImage: Input texture dimension not supported.");
				return global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			global::UnityEngine.Rendering.Vrs.ConversionPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IComputeRenderGraphBuilder computeRenderGraphBuilder = renderGraph.AddComputePass<global::UnityEngine.Rendering.Vrs.ConversionPassData>("TextureToShadingRateImage", out passData, s_VrsResources.conversionProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\Vrs\\Vrs.cs", 159);
			passData.sriTextureHandle = sriTextureHandle;
			passData.mainTexHandle = colorMaskHandle;
			passData.mainTexDimension = colorMaskDimension;
			passData.mainTexLutHandle = renderGraph.ImportBuffer(s_VrsResources.conversionLutBuffer);
			passData.validatedShadingRateFragmentSizeHandle = renderGraph.ImportBuffer(s_VrsResources.validatedShadingRateFragmentSizeBuffer);
			passData.computeShader = s_VrsResources.textureComputeShader;
			passData.kernelIndex = s_VrsResources.textureReduceKernel;
			passData.scaleBias = new global::UnityEngine.Vector4
			{
				x = 1f / (float)(descriptor.width * s_VrsResources.tileSize.x),
				y = 1f / (float)(descriptor.height * s_VrsResources.tileSize.y),
				z = descriptor.width,
				w = descriptor.height
			};
			passData.dispatchSize = new global::UnityEngine.Vector2Int(descriptor.width, descriptor.height);
			passData.yFlip = yFlip;
			computeRenderGraphBuilder.UseTexture(in passData.sriTextureHandle, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
			computeRenderGraphBuilder.UseTexture(in passData.mainTexHandle);
			computeRenderGraphBuilder.UseBuffer(in passData.mainTexLutHandle);
			computeRenderGraphBuilder.AllowGlobalStateModification(value: true);
			computeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Vrs.ConversionPassData innerPassData, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext context)
			{
				ConversionDispatch(context.cmd, innerPassData);
			});
			return passData.sriTextureHandle;
		}

		public static void ShadingRateImageToColorMaskTexture(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle sriTextureHandle, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorMaskHandle)
		{
			if (s_VrsResources == null)
			{
				global::UnityEngine.Debug.LogError("ShadingRateImageToColorMaskTexture: VRS not initialized.");
				return;
			}
			if (!colorMaskHandle.IsValid())
			{
				global::UnityEngine.Debug.LogError("ShadingRateImageToColorMaskTexture: Output target handle is not valid.");
				return;
			}
			global::UnityEngine.Rendering.Vrs.VisualizationPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Vrs.VisualizationPassData>("ShadingRateImageToTexture", out passData, s_VrsResources.visualizationProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\Vrs\\Vrs.cs", 214);
			passData.material = s_VrsResources.visualizationMaterial;
			if (sriTextureHandle.IsValid())
			{
				passData.source = sriTextureHandle;
			}
			else
			{
				passData.source = renderGraph.defaultResources.blackTexture;
			}
			passData.lut = renderGraph.ImportBuffer(s_VrsResources.visualizationLutBuffer);
			passData.dummy = renderGraph.defaultResources.blackTexture;
			passData.visualizationParams = new global::UnityEngine.Vector4(1f / (float)s_VrsResources.tileSize.x, 1f / (float)s_VrsResources.tileSize.y, 0f, 0f);
			rasterRenderGraphBuilder.UseTexture(in passData.source);
			rasterRenderGraphBuilder.UseBuffer(in passData.lut);
			rasterRenderGraphBuilder.UseTexture(in passData.dummy);
			rasterRenderGraphBuilder.SetRenderAttachment(colorMaskHandle, 0);
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Vrs.VisualizationPassData innerPassData, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				innerPassData.material.SetTexture(global::UnityEngine.Rendering.VrsShaders.s_ShadingRateImage, innerPassData.source);
				innerPassData.material.SetBuffer(global::UnityEngine.Rendering.VrsShaders.s_VisualizationLut, innerPassData.lut);
				innerPassData.material.SetVector(global::UnityEngine.Rendering.VrsShaders.s_VisualizationParams, innerPassData.visualizationParams);
				global::UnityEngine.Rendering.Blitter.BlitTexture(context.cmd, innerPassData.dummy, new global::UnityEngine.Vector4(1f, 1f, 0f, 0f), innerPassData.material, 0);
			});
		}

		private static void ConversionDispatch(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, global::UnityEngine.Rendering.Vrs.ConversionPassData conversionPassData)
		{
			global::UnityEngine.Rendering.LocalKeyword keyword = new global::UnityEngine.Rendering.LocalKeyword(conversionPassData.computeShader, "DISABLE_TEXTURE2D_X_ARRAY");
			if (conversionPassData.mainTexDimension == global::UnityEngine.Rendering.TextureDimension.Tex2DArray)
			{
				cmd.DisableKeyword(conversionPassData.computeShader, in keyword);
			}
			else
			{
				cmd.EnableKeyword(conversionPassData.computeShader, in keyword);
			}
			global::UnityEngine.Rendering.LocalKeyword keyword2 = new global::UnityEngine.Rendering.LocalKeyword(conversionPassData.computeShader, "APPLY_Y_FLIP");
			if (conversionPassData.yFlip)
			{
				cmd.EnableKeyword(conversionPassData.computeShader, in keyword2);
			}
			else
			{
				cmd.DisableKeyword(conversionPassData.computeShader, in keyword2);
			}
			cmd.SetComputeTextureParam(conversionPassData.computeShader, conversionPassData.kernelIndex, global::UnityEngine.Rendering.VrsShaders.s_MainTex, conversionPassData.mainTexHandle);
			cmd.SetComputeBufferParam(conversionPassData.computeShader, conversionPassData.kernelIndex, global::UnityEngine.Rendering.VrsShaders.s_MainTexLut, conversionPassData.mainTexLutHandle);
			cmd.SetComputeBufferParam(conversionPassData.computeShader, conversionPassData.kernelIndex, global::UnityEngine.Rendering.VrsShaders.s_ShadingRateNativeValues, conversionPassData.validatedShadingRateFragmentSizeHandle);
			cmd.SetComputeTextureParam(conversionPassData.computeShader, conversionPassData.kernelIndex, global::UnityEngine.Rendering.VrsShaders.s_ShadingRateImage, conversionPassData.sriTextureHandle);
			cmd.SetComputeVectorParam(conversionPassData.computeShader, global::UnityEngine.Rendering.VrsShaders.s_ScaleBias, conversionPassData.scaleBias);
			cmd.DispatchCompute(conversionPassData.computeShader, conversionPassData.kernelIndex, conversionPassData.dispatchSize.x, conversionPassData.dispatchSize.y, 1);
		}

		public static void ColorMaskTextureToShadingRateImageDispatch(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle sriDestination, global::UnityEngine.Texture colorMaskSource, bool yFlip = true)
		{
			if (sriDestination == null)
			{
				global::UnityEngine.Debug.LogError("ColorMaskTextureToShadingRateImageDispatch: VRS destination shading rate texture is null.");
				return;
			}
			if (colorMaskSource == null)
			{
				global::UnityEngine.Debug.LogError("ColorMaskTextureToShadingRateImageDispatch: VRS source color texture is null.");
				return;
			}
			if (!IsInitialized())
			{
				global::UnityEngine.Debug.LogError("ColorMaskTextureToShadingRateImageDispatch: VRS is not initialized.");
				return;
			}
			global::UnityEngine.ComputeShader textureComputeShader = s_VrsResources.textureComputeShader;
			int textureReduceKernel = s_VrsResources.textureReduceKernel;
			global::UnityEngine.GraphicsBuffer conversionLutBuffer = s_VrsResources.conversionLutBuffer;
			global::UnityEngine.GraphicsBuffer validatedShadingRateFragmentSizeBuffer = s_VrsResources.validatedShadingRateFragmentSizeBuffer;
			int width = sriDestination.rt.width;
			int height = sriDestination.rt.height;
			global::UnityEngine.Vector4 val = new global::UnityEngine.Vector4
			{
				x = 1f / (float)(width * s_VrsResources.tileSize.x),
				y = 1f / (float)(height * s_VrsResources.tileSize.y),
				z = width,
				w = height
			};
			global::UnityEngine.Vector2Int vector2Int = new global::UnityEngine.Vector2Int(width, height);
			global::UnityEngine.Rendering.LocalKeyword keyword = new global::UnityEngine.Rendering.LocalKeyword(textureComputeShader, "DISABLE_TEXTURE2D_X_ARRAY");
			if ((object)colorMaskSource != null && colorMaskSource.dimension == global::UnityEngine.Rendering.TextureDimension.Tex2DArray)
			{
				cmd.DisableKeyword(textureComputeShader, in keyword);
			}
			else
			{
				cmd.EnableKeyword(textureComputeShader, in keyword);
			}
			global::UnityEngine.Rendering.LocalKeyword keyword2 = new global::UnityEngine.Rendering.LocalKeyword(textureComputeShader, "APPLY_Y_FLIP");
			if (yFlip)
			{
				cmd.EnableKeyword(textureComputeShader, in keyword2);
			}
			else
			{
				cmd.DisableKeyword(textureComputeShader, in keyword2);
			}
			cmd.SetComputeTextureParam(textureComputeShader, textureReduceKernel, global::UnityEngine.Rendering.VrsShaders.s_MainTex, colorMaskSource);
			cmd.SetComputeBufferParam(textureComputeShader, textureReduceKernel, global::UnityEngine.Rendering.VrsShaders.s_MainTexLut, conversionLutBuffer);
			cmd.SetComputeBufferParam(textureComputeShader, textureReduceKernel, global::UnityEngine.Rendering.VrsShaders.s_ShadingRateNativeValues, validatedShadingRateFragmentSizeBuffer);
			cmd.SetComputeTextureParam(textureComputeShader, textureReduceKernel, global::UnityEngine.Rendering.VrsShaders.s_ShadingRateImage, sriDestination);
			cmd.SetComputeVectorParam(textureComputeShader, global::UnityEngine.Rendering.VrsShaders.s_ScaleBias, val);
			cmd.DispatchCompute(textureComputeShader, textureReduceKernel, vector2Int.x, vector2Int.y, 1);
		}

		public static void ShadingRateImageToColorMaskTextureBlit(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle sriSource, global::UnityEngine.Rendering.RTHandle colorMaskDestination)
		{
			if (sriSource == null)
			{
				global::UnityEngine.Debug.LogError("ShadingRateImageToColorMaskTextureBlit: VRS source shading rate texture is null.");
				return;
			}
			if (colorMaskDestination == null)
			{
				global::UnityEngine.Debug.LogError("ShadingRateImageToColorMaskTextureBlit: VRS destination color texture is null.");
				return;
			}
			if (!IsInitialized())
			{
				global::UnityEngine.Debug.LogError("ShadingRateImageToColorMaskTextureBlit: VRS is not initialized.");
				return;
			}
			global::UnityEngine.Material visualizationMaterial = s_VrsResources.visualizationMaterial;
			global::UnityEngine.GraphicsBuffer visualizationLutBuffer = s_VrsResources.visualizationLutBuffer;
			global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(1f / (float)s_VrsResources.tileSize.x, 1f / (float)s_VrsResources.tileSize.y, 0f, 0f);
			visualizationMaterial.SetTexture(global::UnityEngine.Rendering.VrsShaders.s_ShadingRateImage, sriSource);
			visualizationMaterial.SetBuffer(global::UnityEngine.Rendering.VrsShaders.s_VisualizationLut, visualizationLutBuffer);
			visualizationMaterial.SetVector(global::UnityEngine.Rendering.VrsShaders.s_VisualizationParams, value);
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, colorMaskDestination);
			global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, new global::UnityEngine.Vector4(1f, 1f, 0f, 0f), visualizationMaterial, 0);
		}
	}
}
