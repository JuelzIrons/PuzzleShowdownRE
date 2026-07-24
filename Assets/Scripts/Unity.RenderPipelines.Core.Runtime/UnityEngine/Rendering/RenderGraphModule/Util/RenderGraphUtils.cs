namespace UnityEngine.Rendering.RenderGraphModule.Util
{
	public static class RenderGraphUtils
	{
		private class CopyPassData
		{
			public bool isMSAA;

			public bool force2DForXR;
		}

		public enum BlitFilterMode
		{
			ClampNearest = 0,
			ClampBilinear = 1
		}

		private class BlitPassData
		{
			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;

			public global::UnityEngine.Vector2 scale;

			public global::UnityEngine.Vector2 offset;

			public int sourceSlice;

			public int destinationSlice;

			public int numSlices;

			public int sourceMip;

			public int destinationMip;

			public int numMips;

			public global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitFilterMode filterMode;

			public bool isXR;

			public bool isDepth;
		}

		public enum FullScreenGeometryType
		{
			Mesh = 0,
			ProceduralTriangle = 1,
			ProceduralQuad = 2
		}

		public struct BlitMaterialParameters
		{
			private static readonly int blitTextureProperty = global::UnityEngine.Shader.PropertyToID("_BlitTexture");

			private static readonly int blitSliceProperty = global::UnityEngine.Shader.PropertyToID("_BlitTexArraySlice");

			private static readonly int blitMipProperty = global::UnityEngine.Shader.PropertyToID("_BlitMipLevel");

			private static readonly int blitScaleBias = global::UnityEngine.Shader.PropertyToID("_BlitScaleBias");

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;

			public global::UnityEngine.Vector2 scale;

			public global::UnityEngine.Vector2 offset;

			public int sourceSlice;

			public int destinationSlice;

			public int numSlices;

			public int sourceMip;

			public int destinationMip;

			public int numMips;

			public global::UnityEngine.Material material;

			public int shaderPass;

			public global::UnityEngine.MaterialPropertyBlock propertyBlock;

			public int sourceTexturePropertyID;

			public int sourceSlicePropertyID;

			public int sourceMipPropertyID;

			public int scaleBiasPropertyID;

			public global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType geometry;

			public BlitMaterialParameters(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Material material, int shaderPass)
				: this(source, destination, global::UnityEngine.Vector2.one, global::UnityEngine.Vector2.zero, material, shaderPass)
			{
			}

			public BlitMaterialParameters(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Vector2 scale, global::UnityEngine.Vector2 offset, global::UnityEngine.Material material, int shaderPass)
			{
				this.source = source;
				this.destination = destination;
				this.scale = scale;
				this.offset = offset;
				sourceSlice = -1;
				destinationSlice = 0;
				numSlices = -1;
				sourceMip = -1;
				destinationMip = 0;
				numMips = 1;
				this.material = material;
				this.shaderPass = shaderPass;
				propertyBlock = null;
				sourceTexturePropertyID = blitTextureProperty;
				sourceSlicePropertyID = blitSliceProperty;
				sourceMipPropertyID = blitMipProperty;
				scaleBiasPropertyID = blitScaleBias;
				geometry = global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.ProceduralTriangle;
			}

			public BlitMaterialParameters(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock mpb, int destinationSlice, int destinationMip, int numSlices = -1, int numMips = 1, int sourceSlice = -1, int sourceMip = -1, global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType geometry = global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.Mesh, int sourceTexturePropertyID = -1, int sourceSlicePropertyID = -1, int sourceMipPropertyID = -1)
				: this(source, destination, global::UnityEngine.Vector2.one, global::UnityEngine.Vector2.zero, material, shaderPass, mpb, destinationSlice, destinationMip, numSlices, numMips, sourceSlice, sourceMip, geometry, sourceTexturePropertyID, sourceSlicePropertyID, sourceMipPropertyID)
			{
			}

			public BlitMaterialParameters(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Vector2 scale, global::UnityEngine.Vector2 offset, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock mpb, int destinationSlice, int destinationMip, int numSlices = -1, int numMips = 1, int sourceSlice = -1, int sourceMip = -1, global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType geometry = global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.Mesh, int sourceTexturePropertyID = -1, int sourceSlicePropertyID = -1, int sourceMipPropertyID = -1, int scaleBiasPropertyID = -1)
				: this(source, destination, scale, offset, material, shaderPass)
			{
				propertyBlock = mpb;
				this.sourceSlice = sourceSlice;
				this.destinationSlice = destinationSlice;
				this.numSlices = numSlices;
				this.sourceMip = sourceMip;
				this.destinationMip = destinationMip;
				this.numMips = numMips;
				if (sourceTexturePropertyID != -1)
				{
					this.sourceTexturePropertyID = sourceTexturePropertyID;
				}
				if (sourceSlicePropertyID != -1)
				{
					this.sourceSlicePropertyID = sourceSlicePropertyID;
				}
				if (sourceMipPropertyID != -1)
				{
					this.sourceMipPropertyID = sourceMipPropertyID;
				}
				if (scaleBiasPropertyID != -1)
				{
					this.scaleBiasPropertyID = scaleBiasPropertyID;
				}
				this.geometry = geometry;
			}

			public BlitMaterialParameters(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock mpb, global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType geometry = global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.Mesh, int sourceTexturePropertyID = -1, int sourceSlicePropertyID = -1, int sourceMipPropertyID = -1)
				: this(source, destination, global::UnityEngine.Vector2.one, global::UnityEngine.Vector2.zero, material, shaderPass, mpb, geometry, sourceTexturePropertyID, sourceSlicePropertyID, sourceMipPropertyID)
			{
			}

			public BlitMaterialParameters(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Vector2 scale, global::UnityEngine.Vector2 offset, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock mpb, global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType geometry = global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.Mesh, int sourceTexturePropertyID = -1, int sourceSlicePropertyID = -1, int sourceMipPropertyID = -1, int scaleBiasPropertyID = -1)
				: this(source, destination, scale, offset, material, shaderPass)
			{
				propertyBlock = mpb;
				if (sourceTexturePropertyID != -1)
				{
					this.sourceTexturePropertyID = sourceTexturePropertyID;
				}
				if (sourceSlicePropertyID != -1)
				{
					this.sourceSlicePropertyID = sourceSlicePropertyID;
				}
				if (sourceMipPropertyID != -1)
				{
					this.sourceMipPropertyID = sourceMipPropertyID;
				}
				if (scaleBiasPropertyID != -1)
				{
					this.scaleBiasPropertyID = scaleBiasPropertyID;
				}
				this.geometry = geometry;
			}
		}

		private class BlitMaterialPassData
		{
			public int sourceTexturePropertyID;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination;

			public global::UnityEngine.Vector2 scale;

			public global::UnityEngine.Vector2 offset;

			public global::UnityEngine.Material material;

			public int shaderPass;

			public global::UnityEngine.MaterialPropertyBlock propertyBlock;

			public int sourceSlice;

			public int destinationSlice;

			public int numSlices;

			public int sourceMip;

			public int destinationMip;

			public int numMips;

			public global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType geometry;

			public int sourceSlicePropertyID;

			public int sourceMipPropertyID;

			public int scaleBiasPropertyID;

			public bool isXR;
		}

		private static global::UnityEngine.MaterialPropertyBlock s_PropertyBlock = new global::UnityEngine.MaterialPropertyBlock();

		private static global::UnityEngine.Vector4 s_BlitScaleBias = default(global::UnityEngine.Vector4);

		public static bool CanAddCopyPassMSAA()
		{
			if (!IsFramebufferFetchEmulationMSAASupportedOnCurrentPlatform())
			{
				return false;
			}
			return global::UnityEngine.Rendering.Blitter.CanCopyMSAA();
		}

		public static bool CanAddCopyPassMSAA(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc sourceDesc)
		{
			if (!IsFramebufferFetchEmulationMSAASupportedOnCurrentPlatform())
			{
				return false;
			}
			return global::UnityEngine.Rendering.Blitter.CanCopyMSAA(sourceDesc.bindTextureMS);
		}

		public static bool CanAddCopyPassMSAA(bool bindTextureMS)
		{
			if (!IsFramebufferFetchEmulationMSAASupportedOnCurrentPlatform())
			{
				return false;
			}
			return global::UnityEngine.Rendering.Blitter.CanCopyMSAA(bindTextureMS);
		}

		internal static bool IsFramebufferFetchEmulationSupportedOnCurrentPlatform()
		{
			return true;
		}

		internal static bool IsFramebufferFetchEmulationMSAASupportedOnCurrentPlatform()
		{
			if (global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.PlayStation4 && global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.PlayStation5)
			{
				return global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.PlayStation5NGGC;
			}
			return false;
		}

		public static bool IsFramebufferFetchSupportedOnCurrentPlatform(this global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex)
		{
			if (!IsFramebufferFetchEmulationSupportedOnCurrentPlatform())
			{
				return false;
			}
			if (!IsFramebufferFetchEmulationMSAASupportedOnCurrentPlatform())
			{
				global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo = graph.GetRenderTargetInfo(tex);
				if (renderTargetInfo.msaaSamples > 1)
				{
					return renderTargetInfo.bindMS;
				}
			}
			return true;
		}

		public static bool CanAddCopyPass(this global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
		{
			if (!source.IsValid() || !destination.IsValid())
			{
				return false;
			}
			if (!graph.nativeRenderPassesEnabled)
			{
				return false;
			}
			if (!IsFramebufferFetchEmulationSupportedOnCurrentPlatform())
			{
				return false;
			}
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo = graph.GetRenderTargetInfo(source);
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo2 = graph.GetRenderTargetInfo(destination);
			if (renderTargetInfo.msaaSamples != renderTargetInfo2.msaaSamples)
			{
				return false;
			}
			if (renderTargetInfo.width != renderTargetInfo2.width || renderTargetInfo.height != renderTargetInfo2.height)
			{
				return false;
			}
			if (renderTargetInfo.volumeDepth != renderTargetInfo2.volumeDepth)
			{
				return false;
			}
			if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthFormat(renderTargetInfo.format) || global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthFormat(renderTargetInfo2.format))
			{
				return false;
			}
			if (renderTargetInfo.msaaSamples > 1 && !CanAddCopyPassMSAA(renderTargetInfo.bindMS))
			{
				return false;
			}
			return true;
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder AddCopyPass(this global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, string passName = "Copy Pass Utility", bool returnBuilder = false, [global::System.Runtime.CompilerServices.CallerFilePath] string file = "", [global::System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
		{
			if (!graph.nativeRenderPassesEnabled)
			{
				throw new global::System.ArgumentException("CopyPass only supported for native render pass. Please use the blit functions instead for non native render pass platforms.");
			}
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo = graph.GetRenderTargetInfo(source);
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo2 = graph.GetRenderTargetInfo(destination);
			if (renderTargetInfo.msaaSamples != renderTargetInfo2.msaaSamples)
			{
				throw new global::System.ArgumentException("MSAA samples from source and destination texture doesn't match.");
			}
			if (renderTargetInfo.width != renderTargetInfo2.width || renderTargetInfo.height != renderTargetInfo2.height)
			{
				throw new global::System.ArgumentException("Dimensions for source and destination texture doesn't match.");
			}
			if (renderTargetInfo.volumeDepth != renderTargetInfo2.volumeDepth)
			{
				throw new global::System.ArgumentException("Slice count for source and destination texture doesn't match.");
			}
			if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthFormat(renderTargetInfo.format) || global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthFormat(renderTargetInfo2.format))
			{
				throw new global::System.ArgumentException("Depth format for source or destination texture is not supported. Use AddBlitPass instead.");
			}
			bool flag = renderTargetInfo.msaaSamples > 1;
			if (flag && !CanAddCopyPassMSAA(renderTargetInfo.bindMS))
			{
				throw new global::System.ArgumentException("Target does not support MSAA for AddCopyPass. Please use the blit alternative or use non MSAA textures.");
			}
			global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.CopyPassData passData;
			global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.CopyPassData>(passName, out passData, file, line);
			try
			{
				bool useTexArray = global::UnityEngine.Rendering.TextureXR.useTexArray;
				bool flag2 = renderTargetInfo.volumeDepth > 1;
				passData.isMSAA = flag;
				passData.force2DForXR = useTexArray && !flag2;
				rasterRenderGraphBuilder.SetInputAttachment(source, 0);
				rasterRenderGraphBuilder.SetRenderAttachment(destination, 0);
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.CopyPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
				{
					CopyRenderFunc(data, context);
				});
				if (passData.force2DForXR)
				{
					rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
				}
			}
			catch
			{
				rasterRenderGraphBuilder.Dispose();
				throw;
			}
			if (returnBuilder)
			{
				return rasterRenderGraphBuilder;
			}
			rasterRenderGraphBuilder.Dispose();
			return null;
		}

		public static void AddCopyPass(this global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, int sourceSlice, int destinationSlice = 0, int sourceMip = 0, int destinationMip = 0, string passName = "Copy Pass Utility", [global::System.Runtime.CompilerServices.CallerFilePath] string file = "", [global::System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
		{
			graph.AddCopyPass(source, destination, passName, returnBuilder: false, file, line);
		}

		private static void CopyRenderFunc(global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.CopyPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
		{
			global::UnityEngine.Rendering.Blitter.CopyTexture(rgContext.cmd, data.isMSAA, data.force2DForXR);
		}

		internal static bool IsTextureXR(ref global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo destDesc, int sourceSlice, int destinationSlice, int numSlices, int numMips)
		{
			if (global::UnityEngine.Rendering.TextureXR.useTexArray && destDesc.volumeDepth > 1 && destDesc.volumeDepth == global::UnityEngine.Rendering.TextureXR.slices && sourceSlice == 0 && destinationSlice == 0 && numSlices == global::UnityEngine.Rendering.TextureXR.slices && numMips == 1)
			{
				return true;
			}
			return false;
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder AddBlitPass(this global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Vector2 scale, global::UnityEngine.Vector2 offset, int sourceSlice = 0, int destinationSlice = 0, int numSlices = -1, int sourceMip = 0, int destinationMip = 0, int numMips = 1, global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitFilterMode filterMode = global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitFilterMode.ClampBilinear, string passName = "Blit Pass Utility", bool returnBuilder = false, [global::System.Runtime.CompilerServices.CallerFilePath] string file = "", [global::System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
		{
			if (!source.IsValid())
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " source needs to be a valid texture handle.");
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureDesc = graph.GetTextureDesc(in source);
			if (!destination.IsValid())
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " destination needs to be a valid texture handle.");
			}
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo destDesc = graph.GetRenderTargetInfo(destination);
			int num = (int)global::Unity.Mathematics.math.log2(global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.max(textureDesc.width, textureDesc.height), textureDesc.slices)) + 1;
			int num2 = (int)global::Unity.Mathematics.math.log2(global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.max(destDesc.width, destDesc.height), destDesc.volumeDepth)) + 1;
			if (numSlices == -1)
			{
				numSlices = textureDesc.slices - sourceSlice;
			}
			if (numSlices > textureDesc.slices - sourceSlice || numSlices > destDesc.volumeDepth - destinationSlice)
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " attempts to blit too many slices. The pass will be skipped.");
			}
			if (numMips == -1)
			{
				numMips = num - sourceMip;
			}
			if (numMips > num - sourceMip || numMips > num2 - destinationMip)
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " attempts to blit too many mips. The pass will be skipped.");
			}
			bool num3 = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthFormat(textureDesc.format);
			bool flag = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthFormat(destDesc.format);
			if (!num3 && flag)
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " attempts to blit from a color texture to a depth texture. This is not allowed.");
			}
			if (num3 && !textureDesc.bindTextureMS && textureDesc.msaaSamples != global::UnityEngine.Rendering.MSAASamples.None)
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " source depth render texture is MSAA but doesn't have the bindTextureMS flag set to true, this is not supported. This is not allowed.");
			}
			if (graph.CanAddCopyPass(source, destination) && scale == global::UnityEngine.Vector2.one && offset == global::UnityEngine.Vector2.zero && numSlices == 1 && numMips == 1 && !flag)
			{
				return graph.AddCopyPass(source, destination, passName, returnBuilder, file, line);
			}
			global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitPassData passData;
			global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = graph.AddUnsafePass<global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitPassData>(passName, out passData, file, line);
			try
			{
				passData.isXR = IsTextureXR(ref destDesc, sourceSlice, destinationSlice, numSlices, numMips);
				passData.source = source;
				passData.destination = destination;
				passData.scale = scale;
				passData.offset = offset;
				passData.sourceSlice = sourceSlice;
				passData.destinationSlice = destinationSlice;
				passData.numSlices = numSlices;
				passData.sourceMip = sourceMip;
				passData.destinationMip = destinationMip;
				passData.numMips = numMips;
				passData.filterMode = filterMode;
				passData.isDepth = flag;
				unsafeRenderGraphBuilder.UseTexture(in source);
				unsafeRenderGraphBuilder.UseTexture(in destination, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
				unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
				{
					BlitRenderFunc(data, context);
				});
			}
			catch
			{
				unsafeRenderGraphBuilder.Dispose();
				throw;
			}
			if (returnBuilder)
			{
				return unsafeRenderGraphBuilder;
			}
			unsafeRenderGraphBuilder.Dispose();
			return null;
		}

		private static void BlitRenderFunc(global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
		{
			s_BlitScaleBias.x = data.scale.x;
			s_BlitScaleBias.y = data.scale.y;
			s_BlitScaleBias.z = data.offset.x;
			s_BlitScaleBias.w = data.offset.y;
			global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
			if (data.isDepth)
			{
				context.cmd.SetRenderTarget(data.destination, 0, global::UnityEngine.CubemapFace.Unknown, -1);
				global::UnityEngine.Rendering.Blitter.BlitDepth(nativeCommandBuffer, data.source, s_BlitScaleBias, 0f);
				return;
			}
			if (data.isXR)
			{
				context.cmd.SetRenderTarget(data.destination, 0, global::UnityEngine.CubemapFace.Unknown, -1);
				global::UnityEngine.Rendering.Blitter.BlitTexture(nativeCommandBuffer, data.source, s_BlitScaleBias, data.sourceMip, data.filterMode == global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitFilterMode.ClampBilinear);
				return;
			}
			for (int i = 0; i < data.numSlices; i++)
			{
				for (int j = 0; j < data.numMips; j++)
				{
					context.cmd.SetRenderTarget(data.destination, data.destinationMip + j, global::UnityEngine.CubemapFace.Unknown, data.destinationSlice + i);
					global::UnityEngine.Rendering.Blitter.BlitTexture(nativeCommandBuffer, data.source, s_BlitScaleBias, data.sourceMip + j, data.sourceSlice + i, data.filterMode == global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitFilterMode.ClampBilinear);
				}
			}
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder AddBlitPass(this global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitMaterialParameters blitParameters, string passName = "Blit Pass Utility w. Material", bool returnBuilder = false, [global::System.Runtime.CompilerServices.CallerFilePath] string file = "", [global::System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
		{
			if (!blitParameters.destination.IsValid())
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " destination needs to be a valid texture handle.");
			}
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo destDesc = graph.GetRenderTargetInfo(blitParameters.destination);
			int num = (int)global::Unity.Mathematics.math.log2(global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.max(destDesc.width, destDesc.height), destDesc.volumeDepth)) + 1;
			if (blitParameters.numSlices == -1)
			{
				blitParameters.numSlices = destDesc.volumeDepth - blitParameters.destinationSlice;
			}
			if (blitParameters.numMips == -1)
			{
				blitParameters.numMips = num - blitParameters.destinationMip;
			}
			if (blitParameters.source.IsValid())
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureDesc = graph.GetTextureDesc(in blitParameters.source);
				int num2 = (int)global::Unity.Mathematics.math.log2(global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.max(textureDesc.width, textureDesc.height), textureDesc.slices)) + 1;
				if (blitParameters.sourceSlice != -1 && blitParameters.numSlices > textureDesc.slices - blitParameters.sourceSlice)
				{
					throw new global::System.ArgumentException("BlitPass: " + passName + " attempts to blit too many slices. There are not enough slices in the source array. The pass will be skipped.");
				}
				if (blitParameters.sourceMip != -1 && blitParameters.numMips > num2 - blitParameters.sourceMip)
				{
					throw new global::System.ArgumentException("BlitPass: " + passName + " attempts to blit too many mips. There are not enough mips in the source texture. The pass will be skipped.");
				}
			}
			if (blitParameters.numSlices > destDesc.volumeDepth - blitParameters.destinationSlice)
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " attempts to blit too many slices. There are not enough slices in the destination array. The pass will be skipped.");
			}
			if (blitParameters.numMips > num - blitParameters.destinationMip)
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " attempts to blit too many mips. There are not enough mips in the destination texture. The pass will be skipped.");
			}
			if (blitParameters.material == null)
			{
				throw new global::System.ArgumentException("BlitPass: " + passName + " attempts to use a null material.");
			}
			global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitMaterialPassData passData;
			global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = graph.AddUnsafePass<global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitMaterialPassData>(passName, out passData, file, line);
			try
			{
				passData.sourceTexturePropertyID = blitParameters.sourceTexturePropertyID;
				passData.source = blitParameters.source;
				passData.destination = blitParameters.destination;
				passData.scale = blitParameters.scale;
				passData.offset = blitParameters.offset;
				passData.material = blitParameters.material;
				passData.shaderPass = blitParameters.shaderPass;
				passData.propertyBlock = blitParameters.propertyBlock;
				passData.sourceSlice = blitParameters.sourceSlice;
				passData.destinationSlice = blitParameters.destinationSlice;
				passData.numSlices = blitParameters.numSlices;
				passData.sourceMip = blitParameters.sourceMip;
				passData.destinationMip = blitParameters.destinationMip;
				passData.numMips = blitParameters.numMips;
				passData.geometry = blitParameters.geometry;
				passData.sourceSlicePropertyID = blitParameters.sourceSlicePropertyID;
				passData.sourceMipPropertyID = blitParameters.sourceMipPropertyID;
				passData.scaleBiasPropertyID = blitParameters.scaleBiasPropertyID;
				passData.isXR = IsTextureXR(ref destDesc, (passData.sourceSlice != -1) ? passData.sourceSlice : 0, passData.destinationSlice, passData.numSlices, passData.numMips);
				if (blitParameters.source.IsValid())
				{
					unsafeRenderGraphBuilder.UseTexture(in blitParameters.source);
				}
				unsafeRenderGraphBuilder.UseTexture(in blitParameters.destination, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
				unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitMaterialPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
				{
					BlitMaterialRenderFunc(data, context);
				});
			}
			catch
			{
				unsafeRenderGraphBuilder.Dispose();
				throw;
			}
			if (returnBuilder)
			{
				return unsafeRenderGraphBuilder;
			}
			unsafeRenderGraphBuilder.Dispose();
			return null;
		}

		private static void BlitMaterialRenderFunc(global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitMaterialPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
		{
			s_BlitScaleBias.x = data.scale.x;
			s_BlitScaleBias.y = data.scale.y;
			s_BlitScaleBias.z = data.offset.x;
			s_BlitScaleBias.w = data.offset.y;
			global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
			if (data.propertyBlock == null)
			{
				data.propertyBlock = s_PropertyBlock;
			}
			if (data.source.IsValid())
			{
				data.propertyBlock.SetTexture(data.sourceTexturePropertyID, data.source);
			}
			data.propertyBlock.SetVector(data.scaleBiasPropertyID, s_BlitScaleBias);
			if (data.isXR)
			{
				if (data.sourceSlice != -1)
				{
					data.propertyBlock.SetInt(data.sourceSlicePropertyID, 0);
				}
				if (data.sourceMip != -1)
				{
					data.propertyBlock.SetInt(data.sourceMipPropertyID, data.sourceMip);
				}
				context.cmd.SetRenderTarget(data.destination, 0, global::UnityEngine.CubemapFace.Unknown, -1);
				switch (data.geometry)
				{
				case global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.Mesh:
					global::UnityEngine.Rendering.Blitter.DrawQuadMesh(nativeCommandBuffer, data.material, data.shaderPass, data.propertyBlock);
					break;
				case global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.ProceduralQuad:
					global::UnityEngine.Rendering.Blitter.DrawQuad(nativeCommandBuffer, data.material, data.shaderPass, data.propertyBlock);
					break;
				case global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.ProceduralTriangle:
					global::UnityEngine.Rendering.Blitter.DrawTriangle(nativeCommandBuffer, data.material, data.shaderPass, data.propertyBlock);
					break;
				}
				return;
			}
			for (int i = 0; i < data.numSlices; i++)
			{
				for (int j = 0; j < data.numMips; j++)
				{
					if (data.sourceSlice != -1)
					{
						data.propertyBlock.SetInt(data.sourceSlicePropertyID, data.sourceSlice + i);
					}
					if (data.sourceMip != -1)
					{
						data.propertyBlock.SetInt(data.sourceMipPropertyID, data.sourceMip + j);
					}
					context.cmd.SetRenderTarget(data.destination, data.destinationMip + j, global::UnityEngine.CubemapFace.Unknown, data.destinationSlice + i);
					switch (data.geometry)
					{
					case global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.Mesh:
						global::UnityEngine.Rendering.Blitter.DrawQuadMesh(nativeCommandBuffer, data.material, data.shaderPass, data.propertyBlock);
						break;
					case global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.ProceduralQuad:
						global::UnityEngine.Rendering.Blitter.DrawQuad(nativeCommandBuffer, data.material, data.shaderPass, data.propertyBlock);
						break;
					case global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.FullScreenGeometryType.ProceduralTriangle:
						global::UnityEngine.Rendering.Blitter.DrawTriangle(nativeCommandBuffer, data.material, data.shaderPass, data.propertyBlock);
						break;
					}
				}
			}
		}
	}
}
