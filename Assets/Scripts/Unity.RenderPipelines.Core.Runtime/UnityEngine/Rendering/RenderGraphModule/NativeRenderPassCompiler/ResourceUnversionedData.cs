namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal struct ResourceUnversionedData
	{
		public readonly bool isImported;

		public bool isShared;

		public int tag;

		public int lastUsePassID;

		public int lastWritePassID;

		public int firstUsePassID;

		public bool memoryLess;

		public readonly int width;

		public readonly int height;

		public readonly int volumeDepth;

		public readonly int msaaSamples;

		public readonly global::UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat;

		public int latestVersionNumber;

		public readonly bool clear;

		public readonly bool discard;

		public readonly bool bindMS;

		public global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection textureUVOrigin;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string GetName(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h)
		{
			return ctx.GetResourceName(in h);
		}

		public ResourceUnversionedData(global::UnityEngine.Rendering.RenderGraphModule.TextureResource rll, ref global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo info, ref global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc, bool isResourceShared)
		{
			isImported = rll.imported;
			isShared = isResourceShared;
			tag = 0;
			firstUsePassID = -1;
			lastUsePassID = -1;
			lastWritePassID = -1;
			memoryLess = false;
			width = info.width;
			height = info.height;
			volumeDepth = info.volumeDepth;
			msaaSamples = info.msaaSamples;
			latestVersionNumber = (int)rll.writeCount;
			clear = desc.clearBuffer;
			discard = desc.discardBuffer;
			bindMS = info.bindMS;
			textureUVOrigin = rll.textureUVOrigin;
			graphicsFormat = desc.format;
		}

		public ResourceUnversionedData(global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource rll, ref global::UnityEngine.Rendering.RenderGraphModule.BufferDesc _, bool isResourceShared)
		{
			isImported = rll.imported;
			isShared = isResourceShared;
			tag = 0;
			firstUsePassID = -1;
			lastUsePassID = -1;
			lastWritePassID = -1;
			memoryLess = false;
			width = -1;
			height = -1;
			volumeDepth = -1;
			msaaSamples = -1;
			latestVersionNumber = (int)rll.writeCount;
			clear = false;
			discard = false;
			bindMS = false;
			textureUVOrigin = global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown;
			graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
		}

		public ResourceUnversionedData(global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource rll, ref global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureDesc _, bool isResourceShared)
		{
			isImported = rll.imported;
			isShared = isResourceShared;
			tag = 0;
			firstUsePassID = -1;
			lastUsePassID = -1;
			lastWritePassID = -1;
			memoryLess = false;
			width = -1;
			height = -1;
			volumeDepth = -1;
			msaaSamples = -1;
			latestVersionNumber = (int)rll.writeCount;
			clear = false;
			discard = false;
			bindMS = false;
			textureUVOrigin = global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown;
			graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
		}

		public void InitializeNullResource()
		{
			firstUsePassID = -1;
			lastUsePassID = -1;
			lastWritePassID = -1;
			textureUVOrigin = global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown;
		}
	}
}
