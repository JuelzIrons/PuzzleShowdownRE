namespace UnityEngine.Rendering
{
	public static class RTHandles
	{
		private static global::UnityEngine.Rendering.RTHandleSystem s_DefaultInstance = new global::UnityEngine.Rendering.RTHandleSystem();

		public static int maxWidth => s_DefaultInstance.GetMaxWidth();

		public static int maxHeight => s_DefaultInstance.GetMaxHeight();

		public static global::UnityEngine.Rendering.RTHandleProperties rtHandleProperties => s_DefaultInstance.rtHandleProperties;

		public static global::UnityEngine.Vector2Int CalculateDimensions(global::UnityEngine.Vector2 scaleFactor)
		{
			return s_DefaultInstance.CalculateDimensions(scaleFactor);
		}

		public static global::UnityEngine.Vector2Int CalculateDimensions(global::UnityEngine.Rendering.ScaleFunc scaleFunc)
		{
			return s_DefaultInstance.CalculateDimensions(scaleFunc);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(int width, int height, int slices = 1, global::UnityEngine.Rendering.DepthBits depthBufferBits = global::UnityEngine.Rendering.DepthBits.None, global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			return s_DefaultInstance.Alloc(width, height, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(int width, int height, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, int slices = 1, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			return s_DefaultInstance.Alloc(width, height, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(int width, int height, global::UnityEngine.TextureWrapMode wrapModeU, global::UnityEngine.TextureWrapMode wrapModeV, global::UnityEngine.TextureWrapMode wrapModeW = global::UnityEngine.TextureWrapMode.Repeat, int slices = 1, global::UnityEngine.Rendering.DepthBits depthBufferBits = global::UnityEngine.Rendering.DepthBits.None, global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			return s_DefaultInstance.Alloc(width, height, wrapModeU, wrapModeV, wrapModeW, slices, depthBufferBits, colorFormat, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(int width, int height, global::UnityEngine.Rendering.RTHandleAllocInfo info)
		{
			return s_DefaultInstance.Alloc(width, height, info);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			return s_DefaultInstance.Alloc(descriptor.width, descriptor.height, GetRTHandleAllocInfo(in descriptor, filterMode, wrapMode, anisoLevel, mipMapBias, name));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::UnityEngine.Experimental.Rendering.GraphicsFormat GetFormat(global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencilFormat)
		{
			if (depthStencilFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
			{
				return depthStencilFormat;
			}
			return colorFormat;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::UnityEngine.Rendering.RTHandleAllocInfo GetRTHandleAllocInfo(in global::UnityEngine.RenderTextureDescriptor desc, global::UnityEngine.FilterMode filterMode, global::UnityEngine.TextureWrapMode wrapMode, int anisoLevel, float mipMapBias, string name)
		{
			global::UnityEngine.Rendering.RTHandleAllocInfo result = new global::UnityEngine.Rendering.RTHandleAllocInfo(name);
			result.slices = desc.volumeDepth;
			result.format = GetFormat(desc.graphicsFormat, desc.depthStencilFormat);
			result.filterMode = filterMode;
			result.wrapModeU = wrapMode;
			result.wrapModeV = wrapMode;
			result.wrapModeW = wrapMode;
			result.dimension = desc.dimension;
			result.enableRandomWrite = desc.enableRandomWrite;
			result.useMipMap = desc.useMipMap;
			result.autoGenerateMips = desc.autoGenerateMips;
			result.isShadowMap = desc.shadowSamplingMode != global::UnityEngine.Rendering.ShadowSamplingMode.None;
			result.anisoLevel = anisoLevel;
			result.mipMapBias = mipMapBias;
			result.msaaSamples = (global::UnityEngine.Rendering.MSAASamples)desc.msaaSamples;
			result.bindTextureMS = desc.bindMS;
			result.useDynamicScale = desc.useDynamicScale;
			result.useDynamicScaleExplicit = desc.useDynamicScaleExplicit;
			result.memoryless = desc.memoryless;
			result.vrUsage = desc.vrUsage;
			result.enableShadingRate = desc.enableShadingRate;
			return result;
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Vector2 scaleFactor, int slices = 1, global::UnityEngine.Rendering.DepthBits depthBufferBits = global::UnityEngine.Rendering.DepthBits.None, global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			return s_DefaultInstance.Alloc(scaleFactor, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Vector2 scaleFactor, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, int slices = 1, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			return s_DefaultInstance.Alloc(scaleFactor, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Vector2 scaleFactor, in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			return s_DefaultInstance.Alloc(scaleFactor, GetRTHandleAllocInfo(in descriptor, filterMode, wrapMode, anisoLevel, mipMapBias, name));
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Vector2 scaleFactor, global::UnityEngine.Rendering.RTHandleAllocInfo info)
		{
			return s_DefaultInstance.Alloc(scaleFactor, info);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.ScaleFunc scaleFunc, int slices = 1, global::UnityEngine.Rendering.DepthBits depthBufferBits = global::UnityEngine.Rendering.DepthBits.None, global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			return s_DefaultInstance.Alloc(scaleFunc, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.ScaleFunc scaleFunc, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, int slices = 1, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			return s_DefaultInstance.Alloc(scaleFunc, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.ScaleFunc scaleFunc, in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			return s_DefaultInstance.Alloc(scaleFunc, GetRTHandleAllocInfo(in descriptor, filterMode, wrapMode, anisoLevel, mipMapBias, name));
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.ScaleFunc scaleFunc, global::UnityEngine.Rendering.RTHandleAllocInfo info)
		{
			return s_DefaultInstance.Alloc(scaleFunc, info);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Texture tex)
		{
			return s_DefaultInstance.Alloc(tex);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.RenderTexture tex, bool transferOwnership = false)
		{
			return s_DefaultInstance.Alloc(tex, transferOwnership);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.RenderTargetIdentifier tex)
		{
			return s_DefaultInstance.Alloc(tex);
		}

		public static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.RenderTargetIdentifier tex, string name)
		{
			return s_DefaultInstance.Alloc(tex, name);
		}

		private static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.RTHandle tex)
		{
			global::UnityEngine.Debug.LogError("Allocation a RTHandle from another one is forbidden.");
			return null;
		}

		public static void Initialize(int width, int height)
		{
			s_DefaultInstance.Initialize(width, height);
		}

		[global::System.Obsolete("useLegacyDynamicResControl is deprecated. Please use SetHardwareDynamicResolutionState() instead. #from(2023.3)")]
		public static void Initialize(int width, int height, bool useLegacyDynamicResControl = false)
		{
			s_DefaultInstance.Initialize(width, height, useLegacyDynamicResControl);
		}

		public static void Release(global::UnityEngine.Rendering.RTHandle rth)
		{
			s_DefaultInstance.Release(rth);
		}

		public static void SetHardwareDynamicResolutionState(bool hwDynamicResRequested)
		{
			s_DefaultInstance.SetHardwareDynamicResolutionState(hwDynamicResRequested);
		}

		public static void SetReferenceSize(int width, int height)
		{
			s_DefaultInstance.SetReferenceSize(width, height);
		}

		public static void ResetReferenceSize(int width, int height)
		{
			s_DefaultInstance.ResetReferenceSize(width, height);
		}

		public static global::UnityEngine.Vector2 CalculateRatioAgainstMaxSize(int width, int height)
		{
			return s_DefaultInstance.CalculateRatioAgainstMaxSize(new global::UnityEngine.Vector2Int(width, height));
		}
	}
}
