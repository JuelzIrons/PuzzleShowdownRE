namespace UnityEngine.Rendering
{
	public class RTHandleSystem : global::System.IDisposable
	{
		internal enum ResizeMode
		{
			Auto = 0,
			OnDemand = 1
		}

		private bool m_HardwareDynamicResRequested;

		private global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.RTHandle> m_AutoSizedRTs;

		private global::UnityEngine.Rendering.RTHandle[] m_AutoSizedRTsArray;

		private global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.RTHandle> m_ResizeOnDemandRTs;

		private global::UnityEngine.Rendering.RTHandleProperties m_RTHandleProperties;

		private int m_MaxWidths;

		private int m_MaxHeights;

		public global::UnityEngine.Rendering.RTHandleProperties rtHandleProperties => m_RTHandleProperties;

		public RTHandleSystem()
		{
			m_AutoSizedRTs = new global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.RTHandle>();
			m_ResizeOnDemandRTs = new global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.RTHandle>();
			m_MaxWidths = 1;
			m_MaxHeights = 1;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		public void Initialize(int width, int height)
		{
			if (m_AutoSizedRTs.Count != 0)
			{
				string arg = "Unreleased RTHandles:";
				foreach (global::UnityEngine.Rendering.RTHandle autoSizedRT in m_AutoSizedRTs)
				{
					arg = $"{arg}\n    {autoSizedRT.name}";
				}
				global::UnityEngine.Debug.LogError($"RTHandleSystem.Initialize should only be called once before allocating any Render Texture. This may be caused by an unreleased RTHandle resource.\n{arg}\n");
			}
			m_MaxWidths = width;
			m_MaxHeights = height;
			m_HardwareDynamicResRequested = global::UnityEngine.Rendering.DynamicResolutionHandler.instance.RequestsHardwareDynamicResolution();
		}

		[global::System.Obsolete("useLegacyDynamicResControl is deprecated. Please use SetHardwareDynamicResolutionState() instead. #from(2023.3)")]
		public void Initialize(int width, int height, bool useLegacyDynamicResControl = false)
		{
			Initialize(width, height);
			if (useLegacyDynamicResControl)
			{
				m_HardwareDynamicResRequested = true;
			}
		}

		public void Release(global::UnityEngine.Rendering.RTHandle rth)
		{
			rth?.Release();
		}

		internal void Remove(global::UnityEngine.Rendering.RTHandle rth)
		{
			m_AutoSizedRTs.Remove(rth);
		}

		public void ResetReferenceSize(int width, int height)
		{
			m_MaxWidths = width;
			m_MaxHeights = height;
			SetReferenceSize(width, height, reset: true);
		}

		public void SetReferenceSize(int width, int height)
		{
			SetReferenceSize(width, height, reset: false);
		}

		public void SetReferenceSize(int width, int height, bool reset)
		{
			m_RTHandleProperties.previousViewportSize = m_RTHandleProperties.currentViewportSize;
			m_RTHandleProperties.previousRenderTargetSize = m_RTHandleProperties.currentRenderTargetSize;
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(GetMaxWidth(), GetMaxHeight());
			width = global::UnityEngine.Mathf.Max(width, 1);
			height = global::UnityEngine.Mathf.Max(height, 1);
			bool flag = width > GetMaxWidth() || height > GetMaxHeight() || reset;
			if (flag)
			{
				Resize(width, height, flag);
			}
			m_RTHandleProperties.currentViewportSize = new global::UnityEngine.Vector2Int(width, height);
			m_RTHandleProperties.currentRenderTargetSize = new global::UnityEngine.Vector2Int(GetMaxWidth(), GetMaxHeight());
			if (m_RTHandleProperties.previousViewportSize.x == 0)
			{
				m_RTHandleProperties.previousViewportSize = m_RTHandleProperties.currentViewportSize;
				m_RTHandleProperties.previousRenderTargetSize = m_RTHandleProperties.currentRenderTargetSize;
				vector = new global::UnityEngine.Vector2(GetMaxWidth(), GetMaxHeight());
			}
			global::UnityEngine.Vector2 vector2 = CalculateRatioAgainstMaxSize(in m_RTHandleProperties.currentViewportSize);
			if (global::UnityEngine.Rendering.DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled() && m_HardwareDynamicResRequested)
			{
				m_RTHandleProperties.rtHandleScale = new global::UnityEngine.Vector4(vector2.x, vector2.y, m_RTHandleProperties.rtHandleScale.x, m_RTHandleProperties.rtHandleScale.y);
				return;
			}
			global::UnityEngine.Vector2 vector3 = m_RTHandleProperties.previousViewportSize / vector;
			m_RTHandleProperties.rtHandleScale = new global::UnityEngine.Vector4(vector2.x, vector2.y, vector3.x, vector3.y);
		}

		internal global::UnityEngine.Vector2 CalculateRatioAgainstMaxSize(in global::UnityEngine.Vector2Int viewportSize)
		{
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(GetMaxWidth(), GetMaxHeight());
			if (global::UnityEngine.Rendering.DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled() && m_HardwareDynamicResRequested && viewportSize != global::UnityEngine.Rendering.DynamicResolutionHandler.instance.finalViewport)
			{
				global::UnityEngine.Vector2 scales = (global::UnityEngine.Vector2)viewportSize / (global::UnityEngine.Vector2)global::UnityEngine.Rendering.DynamicResolutionHandler.instance.finalViewport;
				vector = global::UnityEngine.Rendering.DynamicResolutionHandler.instance.ApplyScalesOnSize(new global::UnityEngine.Vector2Int(GetMaxWidth(), GetMaxHeight()), scales);
			}
			return new global::UnityEngine.Vector2((float)viewportSize.x / vector.x, (float)viewportSize.y / vector.y);
		}

		public void SetHardwareDynamicResolutionState(bool enableHWDynamicRes)
		{
			if (enableHWDynamicRes == m_HardwareDynamicResRequested)
			{
				return;
			}
			m_HardwareDynamicResRequested = enableHWDynamicRes;
			global::System.Array.Resize(ref m_AutoSizedRTsArray, m_AutoSizedRTs.Count);
			m_AutoSizedRTs.CopyTo(m_AutoSizedRTsArray);
			int i = 0;
			for (int num = m_AutoSizedRTsArray.Length; i < num; i++)
			{
				global::UnityEngine.Rendering.RTHandle rTHandle = m_AutoSizedRTsArray[i];
				global::UnityEngine.RenderTexture rT = rTHandle.m_RT;
				if ((bool)rT)
				{
					rT.Release();
					rT.useDynamicScale = m_HardwareDynamicResRequested && rTHandle.m_EnableHWDynamicScale;
					rT.Create();
				}
			}
		}

		internal void SwitchResizeMode(global::UnityEngine.Rendering.RTHandle rth, global::UnityEngine.Rendering.RTHandleSystem.ResizeMode mode)
		{
			if (!rth.useScaling)
			{
				return;
			}
			switch (mode)
			{
			case global::UnityEngine.Rendering.RTHandleSystem.ResizeMode.OnDemand:
				m_AutoSizedRTs.Remove(rth);
				m_ResizeOnDemandRTs.Add(rth);
				break;
			case global::UnityEngine.Rendering.RTHandleSystem.ResizeMode.Auto:
				if (m_ResizeOnDemandRTs.Contains(rth))
				{
					DemandResize(rth);
				}
				m_ResizeOnDemandRTs.Remove(rth);
				m_AutoSizedRTs.Add(rth);
				break;
			}
		}

		private void DemandResize(global::UnityEngine.Rendering.RTHandle rth)
		{
			global::UnityEngine.RenderTexture rT = rth.m_RT;
			rth.referenceSize = new global::UnityEngine.Vector2Int(m_MaxWidths, m_MaxHeights);
			global::UnityEngine.Vector2Int scaledSize = rth.GetScaledSize(rth.referenceSize);
			scaledSize = global::UnityEngine.Vector2Int.Max(global::UnityEngine.Vector2Int.one, scaledSize);
			if (rT.width != scaledSize.x || rT.height != scaledSize.y)
			{
				rT.Release();
				rT.width = scaledSize.x;
				rT.height = scaledSize.y;
				rT.name = global::UnityEngine.Rendering.CoreUtils.GetRenderTargetAutoName(rT.width, rT.height, rT.volumeDepth, (rT.depthStencilFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None) ? rT.depthStencilFormat : rT.graphicsFormat, rT.dimension, rth.m_Name, rT.useMipMap, rth.m_EnableMSAA, (global::UnityEngine.Rendering.MSAASamples)rT.antiAliasing, rT.useDynamicScale, rT.useDynamicScaleExplicit);
				rT.Create();
			}
		}

		public int GetMaxWidth()
		{
			return m_MaxWidths;
		}

		public int GetMaxHeight()
		{
			return m_MaxHeights;
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				global::System.Array.Resize(ref m_AutoSizedRTsArray, m_AutoSizedRTs.Count);
				m_AutoSizedRTs.CopyTo(m_AutoSizedRTsArray);
				int i = 0;
				for (int num = m_AutoSizedRTsArray.Length; i < num; i++)
				{
					global::UnityEngine.Rendering.RTHandle rth = m_AutoSizedRTsArray[i];
					Release(rth);
				}
				m_AutoSizedRTs.Clear();
				global::System.Array.Resize(ref m_AutoSizedRTsArray, m_ResizeOnDemandRTs.Count);
				m_ResizeOnDemandRTs.CopyTo(m_AutoSizedRTsArray);
				int j = 0;
				for (int num2 = m_AutoSizedRTsArray.Length; j < num2; j++)
				{
					global::UnityEngine.Rendering.RTHandle rth2 = m_AutoSizedRTsArray[j];
					Release(rth2);
				}
				m_ResizeOnDemandRTs.Clear();
				m_AutoSizedRTsArray = null;
			}
		}

		private void Resize(int width, int height, bool sizeChanged)
		{
			m_MaxWidths = global::System.Math.Max(width, m_MaxWidths);
			m_MaxHeights = global::System.Math.Max(height, m_MaxHeights);
			global::UnityEngine.Vector2Int vector2Int = new global::UnityEngine.Vector2Int(m_MaxWidths, m_MaxHeights);
			global::System.Array.Resize(ref m_AutoSizedRTsArray, m_AutoSizedRTs.Count);
			m_AutoSizedRTs.CopyTo(m_AutoSizedRTsArray);
			int i = 0;
			for (int num = m_AutoSizedRTsArray.Length; i < num; i++)
			{
				global::UnityEngine.Rendering.RTHandle rTHandle = m_AutoSizedRTsArray[i];
				rTHandle.referenceSize = vector2Int;
				global::UnityEngine.RenderTexture rT = rTHandle.m_RT;
				rT.Release();
				global::UnityEngine.Vector2Int scaledSize = rTHandle.GetScaledSize(vector2Int);
				rT.width = global::UnityEngine.Mathf.Max(scaledSize.x, 1);
				rT.height = global::UnityEngine.Mathf.Max(scaledSize.y, 1);
				rT.name = global::UnityEngine.Rendering.CoreUtils.GetRenderTargetAutoName(rT.width, rT.height, rT.volumeDepth, (rT.depthStencilFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None) ? rT.depthStencilFormat : rT.graphicsFormat, rT.dimension, rTHandle.m_Name, rT.useMipMap, rTHandle.m_EnableMSAA, (global::UnityEngine.Rendering.MSAASamples)rT.antiAliasing, rT.useDynamicScale, rT.useDynamicScaleExplicit);
				rT.Create();
			}
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(int width, int height, int slices = 1, global::UnityEngine.Rendering.DepthBits depthBufferBits = global::UnityEngine.Rendering.DepthBits.None, global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format = ((depthBufferBits != global::UnityEngine.Rendering.DepthBits.None) ? global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthStencilFormat((int)depthBufferBits) : colorFormat);
			return Alloc(width, height, format, wrapMode, wrapMode, wrapMode, slices, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(int width, int height, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, int slices = 1, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			return Alloc(width, height, format, wrapMode, wrapMode, wrapMode, slices, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(int width, int height, global::UnityEngine.TextureWrapMode wrapModeU, global::UnityEngine.TextureWrapMode wrapModeV, global::UnityEngine.TextureWrapMode wrapModeW = global::UnityEngine.TextureWrapMode.Repeat, int slices = 1, global::UnityEngine.Rendering.DepthBits depthBufferBits = global::UnityEngine.Rendering.DepthBits.None, global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format = ((depthBufferBits != global::UnityEngine.Rendering.DepthBits.None) ? global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthStencilFormat((int)depthBufferBits) : colorFormat);
			return Alloc(width, height, format, wrapModeU, wrapModeV, wrapModeW, slices, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(int width, int height, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, global::UnityEngine.TextureWrapMode wrapModeU, global::UnityEngine.TextureWrapMode wrapModeV, global::UnityEngine.TextureWrapMode wrapModeW = global::UnityEngine.TextureWrapMode.Repeat, int slices = 1, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			global::UnityEngine.RenderTexture rt = CreateRenderTexture(width, height, format, slices, filterMode, wrapModeU, wrapModeV, wrapModeW, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, enableShadingRate: false, name);
			global::UnityEngine.Rendering.RTHandle rTHandle = new global::UnityEngine.Rendering.RTHandle(this);
			rTHandle.SetRenderTexture(rt);
			rTHandle.useScaling = false;
			rTHandle.m_EnableRandomWrite = enableRandomWrite;
			rTHandle.m_EnableMSAA = msaaSamples != global::UnityEngine.Rendering.MSAASamples.None;
			rTHandle.m_EnableHWDynamicScale = useDynamicScale;
			rTHandle.m_Name = name;
			rTHandle.referenceSize = new global::UnityEngine.Vector2Int(width, height);
			return rTHandle;
		}

		private global::UnityEngine.RenderTexture CreateRenderTexture(int width, int height, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, int slices, global::UnityEngine.FilterMode filterMode, global::UnityEngine.TextureWrapMode wrapModeU, global::UnityEngine.TextureWrapMode wrapModeV, global::UnityEngine.TextureWrapMode wrapModeW, global::UnityEngine.Rendering.TextureDimension dimension, bool enableRandomWrite, bool useMipMap, bool autoGenerateMips, bool isShadowMap, int anisoLevel, float mipMapBias, global::UnityEngine.Rendering.MSAASamples msaaSamples, bool bindTextureMS, bool useDynamicScale, bool useDynamicScaleExplicit, global::UnityEngine.RenderTextureMemoryless memoryless, global::UnityEngine.VRTextureUsage vrUsage, bool enableShadingRate, string name)
		{
			bool flag = msaaSamples != global::UnityEngine.Rendering.MSAASamples.None;
			if (!flag && bindTextureMS)
			{
				global::UnityEngine.Debug.LogWarning("RTHandle allocated without MSAA but with bindMS set to true, forcing bindMS to false.");
				bindTextureMS = false;
			}
			if (flag && enableRandomWrite)
			{
				global::UnityEngine.Debug.LogWarning("RTHandle that is MSAA-enabled cannot allocate MSAA RT with 'enableRandomWrite = true'.");
				enableRandomWrite = false;
			}
			bool flag2 = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthStencilFormat(format);
			if (enableShadingRate && (isShadowMap || flag2))
			{
				global::UnityEngine.Debug.LogWarning("RTHandle allocated with incompatible enableShadingRate, forcing enableShadingRate to false.");
				enableShadingRate = false;
			}
			global::UnityEngine.Rendering.ShadowSamplingMode shadowSamplingMode = global::UnityEngine.Rendering.ShadowSamplingMode.None;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencilFormat;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat stencilFormat;
			string renderTargetAutoName;
			if (isShadowMap)
			{
				int num = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthBits(format);
				if (num < 16)
				{
					num = 16;
				}
				depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthStencilFormat(num, 0);
				colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				stencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				shadowSamplingMode = global::UnityEngine.Rendering.ShadowSamplingMode.CompareDepths;
				renderTargetAutoName = global::UnityEngine.Rendering.CoreUtils.GetRenderTargetAutoName(width, height, slices, global::UnityEngine.RenderTextureFormat.Shadowmap, name, useMipMap, flag, msaaSamples);
			}
			else if (flag2)
			{
				colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				depthStencilFormat = format;
				stencilFormat = ((memoryless == global::UnityEngine.RenderTextureMemoryless.None) ? GetStencilFormat(format) : global::UnityEngine.Experimental.Rendering.GraphicsFormat.None);
				renderTargetAutoName = global::UnityEngine.Rendering.CoreUtils.GetRenderTargetAutoName(width, height, slices, format, dimension, name, useMipMap, flag, msaaSamples, useDynamicScale, useDynamicScaleExplicit);
			}
			else
			{
				colorFormat = format;
				depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				stencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				renderTargetAutoName = global::UnityEngine.Rendering.CoreUtils.GetRenderTargetAutoName(width, height, slices, format, dimension, name, useMipMap, flag, msaaSamples, useDynamicScale, useDynamicScaleExplicit);
			}
			global::UnityEngine.RenderTextureDescriptor desc = new global::UnityEngine.RenderTextureDescriptor(width, height, colorFormat, depthStencilFormat);
			desc.msaaSamples = (int)msaaSamples;
			desc.volumeDepth = slices;
			desc.stencilFormat = stencilFormat;
			desc.dimension = dimension;
			desc.shadowSamplingMode = shadowSamplingMode;
			desc.vrUsage = vrUsage;
			desc.memoryless = memoryless;
			desc.useMipMap = useMipMap;
			desc.autoGenerateMips = autoGenerateMips;
			desc.enableRandomWrite = enableRandomWrite;
			desc.bindMS = bindTextureMS;
			desc.useDynamicScale = m_HardwareDynamicResRequested && useDynamicScale;
			desc.useDynamicScaleExplicit = m_HardwareDynamicResRequested && useDynamicScaleExplicit;
			desc.enableShadingRate = enableShadingRate;
			global::UnityEngine.RenderTexture renderTexture = new global::UnityEngine.RenderTexture(desc);
			renderTexture.name = renderTargetAutoName;
			renderTexture.anisoLevel = anisoLevel;
			renderTexture.mipMapBias = mipMapBias;
			renderTexture.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			renderTexture.filterMode = filterMode;
			renderTexture.wrapModeU = wrapModeU;
			renderTexture.wrapModeV = wrapModeV;
			renderTexture.wrapModeW = wrapModeW;
			renderTexture.Create();
			return renderTexture;
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(int width, int height, global::UnityEngine.Rendering.RTHandleAllocInfo info)
		{
			global::UnityEngine.RenderTexture rt = CreateRenderTexture(width, height, info.format, info.slices, info.filterMode, info.wrapModeU, info.wrapModeV, info.wrapModeW, info.dimension, info.enableRandomWrite, info.useMipMap, info.autoGenerateMips, info.isShadowMap, info.anisoLevel, info.mipMapBias, info.msaaSamples, info.bindTextureMS, info.useDynamicScale, info.useDynamicScaleExplicit, info.memoryless, info.vrUsage, info.enableShadingRate, info.name);
			global::UnityEngine.Rendering.RTHandle rTHandle = new global::UnityEngine.Rendering.RTHandle(this);
			rTHandle.SetRenderTexture(rt);
			rTHandle.useScaling = false;
			rTHandle.m_EnableRandomWrite = info.enableRandomWrite;
			rTHandle.m_EnableMSAA = info.msaaSamples != global::UnityEngine.Rendering.MSAASamples.None;
			rTHandle.m_EnableHWDynamicScale = info.useDynamicScale;
			rTHandle.m_Name = info.name;
			rTHandle.referenceSize = new global::UnityEngine.Vector2Int(width, height);
			if (info.enableShadingRate)
			{
				rTHandle.scaleFunc = (global::UnityEngine.Vector2Int refSize) => global::UnityEngine.Rendering.ShadingRateImage.GetAllocTileSize(refSize);
			}
			return rTHandle;
		}

		public global::UnityEngine.Vector2Int CalculateDimensions(global::UnityEngine.Vector2 scaleFactor)
		{
			return CalculateDimensions(scaleFactor, new global::UnityEngine.Vector2Int(GetMaxWidth(), GetMaxHeight()));
		}

		private static global::UnityEngine.Vector2Int CalculateDimensions(global::UnityEngine.Vector2 scaleFactor, global::UnityEngine.Vector2Int size)
		{
			return new global::UnityEngine.Vector2Int(global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.RoundToInt(scaleFactor.x * (float)size.x), 1), global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.RoundToInt(scaleFactor.y * (float)size.y), 1));
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Vector2 scaleFactor, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, int slices = 1, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			global::UnityEngine.Vector2Int referenceSize = CalculateDimensions(scaleFactor);
			bool enableShadingRate = false;
			global::UnityEngine.Rendering.RTHandle rTHandle = AllocAutoSizedRenderTexture(referenceSize.x, referenceSize.y, slices, format, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, enableShadingRate, name);
			rTHandle.referenceSize = referenceSize;
			rTHandle.scaleFactor = scaleFactor;
			return rTHandle;
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Vector2 scaleFactor, int slices = 1, global::UnityEngine.Rendering.DepthBits depthBufferBits = global::UnityEngine.Rendering.DepthBits.None, global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format = ((depthBufferBits != global::UnityEngine.Rendering.DepthBits.None) ? global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthStencilFormat((int)depthBufferBits) : colorFormat);
			return Alloc(scaleFactor, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Vector2 scaleFactor, global::UnityEngine.Rendering.RTHandleAllocInfo info)
		{
			int num = global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.RoundToInt(scaleFactor.x * (float)GetMaxWidth()), 1);
			int num2 = global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.RoundToInt(scaleFactor.y * (float)GetMaxHeight()), 1);
			global::UnityEngine.Rendering.RTHandle rTHandle = AllocAutoSizedRenderTexture(num, num2, info);
			rTHandle.referenceSize = new global::UnityEngine.Vector2Int(num, num2);
			if (info.enableShadingRate)
			{
				rTHandle.scaleFunc = (global::UnityEngine.Vector2Int refSize) => global::UnityEngine.Rendering.ShadingRateImage.GetAllocTileSize(CalculateDimensions(scaleFactor, refSize));
			}
			else
			{
				rTHandle.scaleFactor = scaleFactor;
			}
			return rTHandle;
		}

		public global::UnityEngine.Vector2Int CalculateDimensions(global::UnityEngine.Rendering.ScaleFunc scaleFunc)
		{
			global::UnityEngine.Vector2Int vector2Int = scaleFunc(new global::UnityEngine.Vector2Int(GetMaxWidth(), GetMaxHeight()));
			return new global::UnityEngine.Vector2Int(global::UnityEngine.Mathf.Max(vector2Int.x, 1), global::UnityEngine.Mathf.Max(vector2Int.y, 1));
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.ScaleFunc scaleFunc, int slices = 1, global::UnityEngine.Rendering.DepthBits depthBufferBits = global::UnityEngine.Rendering.DepthBits.None, global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format = ((depthBufferBits != global::UnityEngine.Rendering.DepthBits.None) ? global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthStencilFormat((int)depthBufferBits) : colorFormat);
			return Alloc(scaleFunc, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.ScaleFunc scaleFunc, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, int slices = 1, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, global::UnityEngine.RenderTextureMemoryless memoryless = global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage vrUsage = global::UnityEngine.VRTextureUsage.None, string name = "")
		{
			global::UnityEngine.Vector2Int referenceSize = CalculateDimensions(scaleFunc);
			bool enableShadingRate = false;
			global::UnityEngine.Rendering.RTHandle rTHandle = AllocAutoSizedRenderTexture(referenceSize.x, referenceSize.y, slices, format, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, enableShadingRate, name);
			rTHandle.referenceSize = referenceSize;
			rTHandle.scaleFunc = scaleFunc;
			return rTHandle;
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.ScaleFunc scaleFunc, global::UnityEngine.Rendering.RTHandleAllocInfo info)
		{
			global::UnityEngine.Vector2Int vector2Int = scaleFunc(new global::UnityEngine.Vector2Int(GetMaxWidth(), GetMaxHeight()));
			int num = global::UnityEngine.Mathf.Max(vector2Int.x, 1);
			int num2 = global::UnityEngine.Mathf.Max(vector2Int.y, 1);
			global::UnityEngine.Rendering.RTHandle rTHandle = AllocAutoSizedRenderTexture(num, num2, info);
			rTHandle.referenceSize = new global::UnityEngine.Vector2Int(num, num2);
			if (info.enableShadingRate)
			{
				rTHandle.scaleFunc = (global::UnityEngine.Vector2Int refSize) => global::UnityEngine.Rendering.ShadingRateImage.GetAllocTileSize(scaleFunc(refSize));
			}
			else
			{
				rTHandle.scaleFunc = scaleFunc;
			}
			return rTHandle;
		}

		internal global::UnityEngine.Rendering.RTHandle AllocAutoSizedRenderTexture(int width, int height, int slices, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, global::UnityEngine.FilterMode filterMode, global::UnityEngine.TextureWrapMode wrapMode, global::UnityEngine.Rendering.TextureDimension dimension, bool enableRandomWrite, bool useMipMap, bool autoGenerateMips, bool isShadowMap, int anisoLevel, float mipMapBias, global::UnityEngine.Rendering.MSAASamples msaaSamples, bool bindTextureMS, bool useDynamicScale, bool useDynamicScaleExplicit, global::UnityEngine.RenderTextureMemoryless memoryless, global::UnityEngine.VRTextureUsage vrUsage, bool enableShadingRate, string name)
		{
			if (enableShadingRate)
			{
				global::UnityEngine.Vector2Int allocTileSize = global::UnityEngine.Rendering.ShadingRateImage.GetAllocTileSize(width, height);
				width = allocTileSize.x;
				height = allocTileSize.y;
			}
			global::UnityEngine.RenderTexture rt = CreateRenderTexture(width, height, format, slices, filterMode, wrapMode, wrapMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, enableShadingRate, name);
			global::UnityEngine.Rendering.RTHandle rTHandle = new global::UnityEngine.Rendering.RTHandle(this);
			rTHandle.SetRenderTexture(rt);
			rTHandle.m_EnableMSAA = msaaSamples != global::UnityEngine.Rendering.MSAASamples.None;
			rTHandle.m_EnableRandomWrite = enableRandomWrite;
			rTHandle.useScaling = true;
			rTHandle.m_EnableHWDynamicScale = useDynamicScale;
			rTHandle.m_Name = name;
			m_AutoSizedRTs.Add(rTHandle);
			return rTHandle;
		}

		internal global::UnityEngine.Rendering.RTHandle AllocAutoSizedRenderTexture(int width, int height, global::UnityEngine.Rendering.RTHandleAllocInfo info)
		{
			if (info.enableShadingRate)
			{
				global::UnityEngine.Vector2Int allocTileSize = global::UnityEngine.Rendering.ShadingRateImage.GetAllocTileSize(width, height);
				width = allocTileSize.x;
				height = allocTileSize.y;
			}
			global::UnityEngine.RenderTexture rt = CreateRenderTexture(width, height, info.format, info.slices, info.filterMode, info.wrapModeU, info.wrapModeV, info.wrapModeW, info.dimension, info.enableRandomWrite, info.useMipMap, info.autoGenerateMips, info.isShadowMap, info.anisoLevel, info.mipMapBias, info.msaaSamples, info.bindTextureMS, info.useDynamicScale, info.useDynamicScaleExplicit, info.memoryless, info.vrUsage, info.enableShadingRate, info.name);
			global::UnityEngine.Rendering.RTHandle rTHandle = new global::UnityEngine.Rendering.RTHandle(this);
			rTHandle.SetRenderTexture(rt);
			rTHandle.m_EnableMSAA = info.msaaSamples != global::UnityEngine.Rendering.MSAASamples.None;
			rTHandle.m_EnableRandomWrite = info.enableRandomWrite;
			rTHandle.useScaling = true;
			rTHandle.m_EnableHWDynamicScale = info.useDynamicScale;
			rTHandle.m_Name = info.name;
			m_AutoSizedRTs.Add(rTHandle);
			return rTHandle;
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.RenderTexture texture, bool transferOwnership = false)
		{
			global::UnityEngine.Rendering.RTHandle rTHandle = new global::UnityEngine.Rendering.RTHandle(this);
			rTHandle.SetRenderTexture(texture, transferOwnership);
			rTHandle.m_EnableMSAA = false;
			rTHandle.m_EnableRandomWrite = false;
			rTHandle.useScaling = false;
			rTHandle.m_EnableHWDynamicScale = false;
			rTHandle.m_Name = texture.name;
			return rTHandle;
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Texture texture)
		{
			global::UnityEngine.Rendering.RTHandle rTHandle = new global::UnityEngine.Rendering.RTHandle(this);
			rTHandle.SetTexture(texture);
			rTHandle.m_EnableMSAA = false;
			rTHandle.m_EnableRandomWrite = false;
			rTHandle.useScaling = false;
			rTHandle.m_EnableHWDynamicScale = false;
			rTHandle.m_Name = texture.name;
			return rTHandle;
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.RenderTargetIdentifier texture)
		{
			return Alloc(texture, "");
		}

		public global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.RenderTargetIdentifier texture, string name)
		{
			global::UnityEngine.Rendering.RTHandle rTHandle = new global::UnityEngine.Rendering.RTHandle(this);
			rTHandle.SetTexture(texture);
			rTHandle.m_EnableMSAA = false;
			rTHandle.m_EnableRandomWrite = false;
			rTHandle.useScaling = false;
			rTHandle.m_EnableHWDynamicScale = false;
			rTHandle.m_Name = name;
			return rTHandle;
		}

		private static global::UnityEngine.Rendering.RTHandle Alloc(global::UnityEngine.Rendering.RTHandle tex)
		{
			global::UnityEngine.Debug.LogError("Allocation a RTHandle from another one is forbidden.");
			return null;
		}

		internal string DumpRTInfo()
		{
			string text = "";
			global::System.Array.Resize(ref m_AutoSizedRTsArray, m_AutoSizedRTs.Count);
			m_AutoSizedRTs.CopyTo(m_AutoSizedRTsArray);
			int i = 0;
			for (int num = m_AutoSizedRTsArray.Length; i < num; i++)
			{
				global::UnityEngine.RenderTexture rt = m_AutoSizedRTsArray[i].rt;
				text = $"{text}\nRT ({i})\t Format: {rt.format} W: {rt.width} H {rt.height}\n";
			}
			return text;
		}

		private global::UnityEngine.Experimental.Rendering.GraphicsFormat GetStencilFormat(global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencilFormat)
		{
			if (!global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsStencilFormat(depthStencilFormat) || !global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UInt, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.StencilSampling))
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			}
			return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UInt;
		}
	}
}
