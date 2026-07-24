namespace UnityEngine.Rendering.Universal
{
	public class UniversalCameraData : global::UnityEngine.Rendering.ContextItem
	{
		private global::UnityEngine.Matrix4x4 m_ViewMatrix;

		private global::UnityEngine.Matrix4x4 m_ProjectionMatrix;

		private global::UnityEngine.Matrix4x4 m_JitterMatrix;

		private bool m_CachedRenderIntoTextureXR;

		private bool m_InitBuiltinXRConstants;

		public global::UnityEngine.Camera camera;

		public int scaledWidth;

		public int scaledHeight;

		internal global::UnityEngine.Rendering.Universal.UniversalCameraHistory m_HistoryManager;

		public global::UnityEngine.Rendering.Universal.CameraRenderType renderType;

		public global::UnityEngine.RenderTexture targetTexture;

		public global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor;

		internal global::UnityEngine.Rect pixelRect;

		internal bool useScreenCoordOverride;

		internal global::UnityEngine.Vector4 screenSizeOverride;

		internal global::UnityEngine.Vector4 screenCoordScaleBias;

		internal int pixelWidth;

		internal int pixelHeight;

		internal float aspectRatio;

		public float renderScale;

		internal global::UnityEngine.Rendering.Universal.ImageScalingMode imageScalingMode;

		internal global::UnityEngine.Rendering.Universal.ImageUpscalingFilter upscalingFilter;

		internal bool fsrOverrideSharpness;

		internal float fsrSharpness;

		internal global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision hdrColorBufferPrecision;

		public bool clearDepth;

		public global::UnityEngine.CameraType cameraType;

		public bool isDefaultViewport;

		public bool isHdrEnabled;

		public bool allowHDROutput;

		public bool isAlphaOutputEnabled;

		public bool requiresDepthTexture;

		public bool requiresOpaqueTexture;

		public bool postProcessingRequiresDepthTexture;

		public bool xrRendering;

		internal bool useGPUOcclusionCulling;

		internal bool stackLastCameraOutputToHDR;

		internal bool rendersOffscreenUI;

		internal bool blitsOffscreenUICover;

		public global::UnityEngine.Rendering.SortingCriteria defaultOpaqueSortFlags;

		public float maxShadowDistance;

		public bool postProcessEnabled;

		internal bool stackAnyPostProcessingEnabled;

		public global::System.Collections.Generic.IEnumerator<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>> captureActions;

		public global::UnityEngine.LayerMask volumeLayerMask;

		public global::UnityEngine.Transform volumeTrigger;

		public bool isStopNaNEnabled;

		public bool isDitheringEnabled;

		public global::UnityEngine.Rendering.Universal.AntialiasingMode antialiasing;

		public global::UnityEngine.Rendering.Universal.AntialiasingQuality antialiasingQuality;

		public global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer;

		public bool resolveFinalTarget;

		public global::UnityEngine.Vector3 worldSpaceCameraPos;

		public global::UnityEngine.Color backgroundColor;

		internal global::UnityEngine.Rendering.Universal.TaaHistory taaHistory;

		internal global::UnityEngine.Rendering.Universal.StpHistory stpHistory;

		internal global::UnityEngine.Rendering.Universal.TemporalAA.Settings taaSettings;

		public global::UnityEngine.Camera baseCamera;

		internal bool isLastBaseCamera;

		public global::UnityEngine.Rendering.Universal.UniversalCameraHistory historyManager
		{
			get
			{
				return m_HistoryManager;
			}
			set
			{
				m_HistoryManager = value;
			}
		}

		internal bool requireSrgbConversion
		{
			get
			{
				if (xr.enabled)
				{
					if (!xr.renderTargetDesc.sRGB && (xr.renderTargetDesc.graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm || xr.renderTargetDesc.graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.B8G8R8A8_UNorm))
					{
						return global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear;
					}
					return false;
				}
				if (targetTexture == null)
				{
					return global::UnityEngine.Display.main.requiresSrgbBlitToBackbuffer;
				}
				return false;
			}
		}

		public bool isGameCamera => cameraType == global::UnityEngine.CameraType.Game;

		public bool isSceneViewCamera => cameraType == global::UnityEngine.CameraType.SceneView;

		public bool isPreviewCamera => cameraType == global::UnityEngine.CameraType.Preview;

		internal bool isRenderPassSupportedCamera
		{
			get
			{
				if (cameraType != global::UnityEngine.CameraType.Game)
				{
					return cameraType == global::UnityEngine.CameraType.Reflection;
				}
				return true;
			}
		}

		internal bool resolveToScreen
		{
			get
			{
				if (targetTexture == null && resolveFinalTarget)
				{
					if (cameraType != global::UnityEngine.CameraType.Game)
					{
						return camera.cameraType == global::UnityEngine.CameraType.VR;
					}
					return true;
				}
				return false;
			}
		}

		public bool isHDROutputActive
		{
			get
			{
				bool flag = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.HDROutputForMainDisplayIsActive();
				if (xr.enabled)
				{
					flag = xr.isHDRDisplayOutputActive;
				}
				if (flag && allowHDROutput)
				{
					return resolveToScreen;
				}
				return false;
			}
		}

		public global::UnityEngine.Rendering.HDROutputUtils.HDRDisplayInformation hdrDisplayInformation
		{
			get
			{
				if (xr.enabled)
				{
					return xr.hdrDisplayOutputInformation;
				}
				global::UnityEngine.HDROutputSettings main = global::UnityEngine.HDROutputSettings.main;
				return new global::UnityEngine.Rendering.HDROutputUtils.HDRDisplayInformation(main.maxFullFrameToneMapLuminance, main.maxToneMapLuminance, main.minToneMapLuminance, main.paperWhiteNits);
			}
		}

		public global::UnityEngine.ColorGamut hdrDisplayColorGamut
		{
			get
			{
				if (xr.enabled)
				{
					return xr.hdrDisplayOutputColorGamut;
				}
				return global::UnityEngine.HDROutputSettings.main.displayColorGamut;
			}
		}

		public bool rendersOverlayUI
		{
			get
			{
				if (global::UnityEngine.Rendering.SupportedRenderingFeatures.active.rendersUIOverlay)
				{
					return resolveToScreen;
				}
				return false;
			}
		}

		public global::UnityEngine.Experimental.Rendering.XRPass xr { get; internal set; }

		internal global::UnityEngine.Rendering.Universal.XRPassUniversal xrUniversal => xr as global::UnityEngine.Rendering.Universal.XRPassUniversal;

		internal bool resetHistory => taaSettings.resetHistoryFrames != 0;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Matrix4x4 GetGPUProjectionMatrix(int viewIndex = 0)
		{
			return default(global::UnityEngine.Matrix4x4);
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Matrix4x4 GetGPUProjectionMatrixNoJitter(int viewIndex = 0)
		{
			return default(global::UnityEngine.Matrix4x4);
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public bool IsCameraProjectionMatrixFlipped()
		{
			return false;
		}

		internal void SetViewAndProjectionMatrix(global::UnityEngine.Matrix4x4 viewMatrix, global::UnityEngine.Matrix4x4 projectionMatrix)
		{
			m_ViewMatrix = viewMatrix;
			m_ProjectionMatrix = projectionMatrix;
			m_JitterMatrix = global::UnityEngine.Matrix4x4.identity;
		}

		internal void SetViewProjectionAndJitterMatrix(global::UnityEngine.Matrix4x4 viewMatrix, global::UnityEngine.Matrix4x4 projectionMatrix, global::UnityEngine.Matrix4x4 jitterMatrix)
		{
			m_ViewMatrix = viewMatrix;
			m_ProjectionMatrix = projectionMatrix;
			m_JitterMatrix = jitterMatrix;
		}

		internal void PushBuiltinShaderConstantsXR(global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool renderIntoTexture)
		{
			if ((!m_InitBuiltinXRConstants || m_CachedRenderIntoTextureXR != renderIntoTexture || !xr.singlePassEnabled) && xr.enabled)
			{
				global::UnityEngine.Matrix4x4 projectionMatrix = GetProjectionMatrix();
				global::UnityEngine.Matrix4x4 viewMatrix = GetViewMatrix();
				cmd.SetViewProjectionMatrices(viewMatrix, projectionMatrix);
				if (xr.singlePassEnabled)
				{
					global::UnityEngine.Matrix4x4 projectionMatrix2 = GetProjectionMatrix(1);
					global::UnityEngine.Matrix4x4 viewMatrix2 = GetViewMatrix(1);
					global::UnityEngine.Experimental.Rendering.XRBuiltinShaderConstants.UpdateBuiltinShaderConstants(viewMatrix, projectionMatrix, renderIntoTexture, 0);
					global::UnityEngine.Experimental.Rendering.XRBuiltinShaderConstants.UpdateBuiltinShaderConstants(viewMatrix2, projectionMatrix2, renderIntoTexture, 1);
					global::UnityEngine.Experimental.Rendering.XRBuiltinShaderConstants.SetBuiltinShaderConstants(cmd);
				}
				else
				{
					global::UnityEngine.Vector3 vector = global::UnityEngine.Matrix4x4.Inverse(GetViewMatrix()).GetColumn(3);
					cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.worldSpaceCameraPos, vector);
					global::UnityEngine.Matrix4x4 gPUProjectionMatrix = GetGPUProjectionMatrix(renderIntoTexture);
					global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.Inverse(viewMatrix);
					global::UnityEngine.Matrix4x4 matrix4x2 = global::UnityEngine.Matrix4x4.Inverse(gPUProjectionMatrix);
					global::UnityEngine.Matrix4x4 value = matrix4x * matrix4x2;
					global::UnityEngine.Matrix4x4 value2 = global::UnityEngine.Matrix4x4.Scale(new global::UnityEngine.Vector3(1f, 1f, -1f)) * viewMatrix;
					global::UnityEngine.Matrix4x4 inverse = value2.inverse;
					cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.worldToCameraMatrix, value2);
					cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.cameraToWorldMatrix, inverse);
					cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.inverseViewMatrix, matrix4x);
					cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.inverseProjectionMatrix, matrix4x2);
					cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.inverseViewAndProjectionMatrix, value);
				}
				m_CachedRenderIntoTextureXR = renderIntoTexture;
				m_InitBuiltinXRConstants = true;
			}
		}

		public global::UnityEngine.Matrix4x4 GetViewMatrix(int viewIndex = 0)
		{
			if (xr.enabled)
			{
				return xr.GetViewMatrix(viewIndex);
			}
			return m_ViewMatrix;
		}

		public global::UnityEngine.Matrix4x4 GetProjectionMatrix(int viewIndex = 0)
		{
			if (xr.enabled)
			{
				return m_JitterMatrix * xr.GetProjMatrix(viewIndex);
			}
			return m_JitterMatrix * m_ProjectionMatrix;
		}

		internal global::UnityEngine.Matrix4x4 GetProjectionMatrixNoJitter(int viewIndex = 0)
		{
			if (xr.enabled)
			{
				return xr.GetProjMatrix(viewIndex);
			}
			return m_ProjectionMatrix;
		}

		internal global::UnityEngine.Matrix4x4 GetGPUProjectionMatrix(bool renderIntoTexture, int viewIndex = 0)
		{
			return global::UnityEngine.GL.GetGPUProjectionMatrix(GetProjectionMatrix(viewIndex), renderIntoTexture);
		}

		public bool IsHandleYFlipped(global::UnityEngine.Rendering.RTHandle handle)
		{
			if (!global::UnityEngine.SystemInfo.graphicsUVStartsAtTop)
			{
				return true;
			}
			if (cameraType == global::UnityEngine.CameraType.SceneView || cameraType == global::UnityEngine.CameraType.Preview)
			{
				return true;
			}
			global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier = new global::UnityEngine.Rendering.RenderTargetIdentifier(handle.nameID, 0);
			bool flag = renderTargetIdentifier == global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget || renderTargetIdentifier == global::UnityEngine.Rendering.BuiltinRenderTextureType.Depth;
			if (xr.enabled)
			{
				flag |= renderTargetIdentifier == new global::UnityEngine.Rendering.RenderTargetIdentifier(xr.renderTarget, 0);
			}
			return !flag;
		}

		public bool IsRenderTargetProjectionMatrixFlipped(global::UnityEngine.Rendering.RTHandle color, global::UnityEngine.Rendering.RTHandle depth = null)
		{
			if (!global::UnityEngine.SystemInfo.graphicsUVStartsAtTop)
			{
				return true;
			}
			if (!(targetTexture != null))
			{
				return IsHandleYFlipped(color ?? depth);
			}
			return true;
		}

		internal bool IsTemporalAARequested()
		{
			return antialiasing == global::UnityEngine.Rendering.Universal.AntialiasingMode.TemporalAntiAliasing;
		}

		internal bool IsTemporalAAEnabled()
		{
			camera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component);
			if (IsTemporalAARequested() && postProcessEnabled && taaHistory != null && cameraTargetDescriptor.msaaSamples == 1 && ((object)component == null || component.renderType != global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay) && ((object)component == null || component.cameraStack.Count <= 0) && !camera.allowDynamicResolution)
			{
				return renderer.SupportsMotionVectors();
			}
			return false;
		}

		internal bool IsSTPRequested()
		{
			if (imageScalingMode == global::UnityEngine.Rendering.Universal.ImageScalingMode.Upscaling)
			{
				return upscalingFilter == global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.STP;
			}
			return false;
		}

		internal bool IsSTPEnabled()
		{
			if (IsSTPRequested())
			{
				return IsTemporalAAEnabled();
			}
			return false;
		}

		public override void Reset()
		{
			m_ViewMatrix = default(global::UnityEngine.Matrix4x4);
			m_ProjectionMatrix = default(global::UnityEngine.Matrix4x4);
			m_JitterMatrix = default(global::UnityEngine.Matrix4x4);
			m_CachedRenderIntoTextureXR = false;
			m_InitBuiltinXRConstants = false;
			camera = null;
			renderType = global::UnityEngine.Rendering.Universal.CameraRenderType.Base;
			targetTexture = null;
			cameraTargetDescriptor = default(global::UnityEngine.RenderTextureDescriptor);
			pixelRect = default(global::UnityEngine.Rect);
			useScreenCoordOverride = false;
			screenSizeOverride = default(global::UnityEngine.Vector4);
			screenCoordScaleBias = default(global::UnityEngine.Vector4);
			pixelWidth = 0;
			pixelHeight = 0;
			aspectRatio = 0f;
			renderScale = 1f;
			imageScalingMode = global::UnityEngine.Rendering.Universal.ImageScalingMode.None;
			upscalingFilter = global::UnityEngine.Rendering.Universal.ImageUpscalingFilter.Point;
			fsrOverrideSharpness = false;
			fsrSharpness = 0f;
			hdrColorBufferPrecision = global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision._32Bits;
			clearDepth = false;
			cameraType = global::UnityEngine.CameraType.Game;
			isDefaultViewport = false;
			isHdrEnabled = false;
			allowHDROutput = false;
			isAlphaOutputEnabled = false;
			requiresDepthTexture = false;
			requiresOpaqueTexture = false;
			postProcessingRequiresDepthTexture = false;
			xrRendering = false;
			useGPUOcclusionCulling = false;
			defaultOpaqueSortFlags = global::UnityEngine.Rendering.SortingCriteria.None;
			xr = null;
			maxShadowDistance = 0f;
			postProcessEnabled = false;
			captureActions = null;
			volumeLayerMask = 0;
			volumeTrigger = null;
			isStopNaNEnabled = false;
			isDitheringEnabled = false;
			antialiasing = global::UnityEngine.Rendering.Universal.AntialiasingMode.None;
			antialiasingQuality = global::UnityEngine.Rendering.Universal.AntialiasingQuality.Low;
			renderer = null;
			resolveFinalTarget = false;
			worldSpaceCameraPos = default(global::UnityEngine.Vector3);
			backgroundColor = global::UnityEngine.Color.black;
			taaHistory = null;
			stpHistory = null;
			taaSettings = default(global::UnityEngine.Rendering.Universal.TemporalAA.Settings);
			baseCamera = null;
			isLastBaseCamera = false;
			stackAnyPostProcessingEnabled = false;
			stackLastCameraOutputToHDR = false;
			rendersOffscreenUI = false;
			blitsOffscreenUICover = false;
		}
	}
}
