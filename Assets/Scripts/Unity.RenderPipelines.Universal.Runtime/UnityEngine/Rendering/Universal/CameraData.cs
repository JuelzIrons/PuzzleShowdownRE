namespace UnityEngine.Rendering.Universal
{
	public struct CameraData
	{
		private global::UnityEngine.Rendering.ContextContainer frameData;

		internal global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();

		public ref global::UnityEngine.Camera camera => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().camera;

		public ref global::UnityEngine.Rendering.Universal.UniversalCameraHistory historyManager => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().m_HistoryManager;

		public ref global::UnityEngine.Rendering.Universal.CameraRenderType renderType => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().renderType;

		public ref global::UnityEngine.RenderTexture targetTexture => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().targetTexture;

		public ref global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().cameraTargetDescriptor;

		internal ref global::UnityEngine.Rect pixelRect => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().pixelRect;

		internal ref bool useScreenCoordOverride => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().useScreenCoordOverride;

		internal ref global::UnityEngine.Vector4 screenSizeOverride => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().screenSizeOverride;

		internal ref global::UnityEngine.Vector4 screenCoordScaleBias => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().screenCoordScaleBias;

		internal ref int pixelWidth => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().pixelWidth;

		internal ref int pixelHeight => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().pixelHeight;

		internal ref float aspectRatio => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().aspectRatio;

		public ref float renderScale => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().renderScale;

		internal ref global::UnityEngine.Rendering.Universal.ImageScalingMode imageScalingMode => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().imageScalingMode;

		internal ref global::UnityEngine.Rendering.Universal.ImageUpscalingFilter upscalingFilter => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().upscalingFilter;

		internal ref bool fsrOverrideSharpness => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().fsrOverrideSharpness;

		internal ref float fsrSharpness => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().fsrSharpness;

		internal ref global::UnityEngine.Rendering.Universal.HDRColorBufferPrecision hdrColorBufferPrecision => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().hdrColorBufferPrecision;

		public ref bool clearDepth => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().clearDepth;

		public ref global::UnityEngine.CameraType cameraType => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().cameraType;

		public ref bool isDefaultViewport => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().isDefaultViewport;

		public ref bool isHdrEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().isHdrEnabled;

		public ref bool allowHDROutput => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().allowHDROutput;

		public ref bool isAlphaOutputEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().isAlphaOutputEnabled;

		public ref bool requiresDepthTexture => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().requiresDepthTexture;

		public ref bool requiresOpaqueTexture => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().requiresOpaqueTexture;

		public ref bool postProcessingRequiresDepthTexture => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().postProcessingRequiresDepthTexture;

		public ref bool xrRendering => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().xrRendering;

		internal bool requireSrgbConversion => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().requireSrgbConversion;

		public bool isSceneViewCamera => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().isSceneViewCamera;

		public bool isPreviewCamera => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().isPreviewCamera;

		internal bool isRenderPassSupportedCamera => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().isRenderPassSupportedCamera;

		internal bool resolveToScreen => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().resolveToScreen;

		public bool isHDROutputActive => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().isHDROutputActive;

		public global::UnityEngine.Rendering.HDROutputUtils.HDRDisplayInformation hdrDisplayInformation => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().hdrDisplayInformation;

		public global::UnityEngine.ColorGamut hdrDisplayColorGamut => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().hdrDisplayColorGamut;

		public bool rendersOverlayUI => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().rendersOverlayUI;

		public ref global::UnityEngine.Rendering.SortingCriteria defaultOpaqueSortFlags => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().defaultOpaqueSortFlags;

		public global::UnityEngine.Experimental.Rendering.XRPass xr
		{
			get
			{
				return frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().xr;
			}
			internal set
			{
				frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().xr = value;
			}
		}

		internal global::UnityEngine.Rendering.Universal.XRPassUniversal xrUniversal => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().xrUniversal;

		public ref float maxShadowDistance => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().maxShadowDistance;

		public ref bool postProcessEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().postProcessEnabled;

		public ref global::System.Collections.Generic.IEnumerator<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>> captureActions => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().captureActions;

		public ref global::UnityEngine.LayerMask volumeLayerMask => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().volumeLayerMask;

		public ref global::UnityEngine.Transform volumeTrigger => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().volumeTrigger;

		public ref bool isStopNaNEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().isStopNaNEnabled;

		public ref bool isDitheringEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().isDitheringEnabled;

		public ref global::UnityEngine.Rendering.Universal.AntialiasingMode antialiasing => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().antialiasing;

		public ref global::UnityEngine.Rendering.Universal.AntialiasingQuality antialiasingQuality => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().antialiasingQuality;

		public ref global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().renderer;

		public ref bool resolveFinalTarget => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().resolveFinalTarget;

		public ref global::UnityEngine.Vector3 worldSpaceCameraPos => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().worldSpaceCameraPos;

		public ref global::UnityEngine.Color backgroundColor => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().backgroundColor;

		internal ref global::UnityEngine.Rendering.Universal.TaaHistory taaHistory => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().taaHistory;

		internal ref global::UnityEngine.Rendering.Universal.TemporalAA.Settings taaSettings => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().taaSettings;

		internal bool resetHistory => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().resetHistory;

		public ref global::UnityEngine.Camera baseCamera => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().baseCamera;

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

		internal CameraData(global::UnityEngine.Rendering.ContextContainer frameData)
		{
			this.frameData = frameData;
		}

		internal void SetViewAndProjectionMatrix(global::UnityEngine.Matrix4x4 viewMatrix, global::UnityEngine.Matrix4x4 projectionMatrix)
		{
			frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().SetViewAndProjectionMatrix(viewMatrix, projectionMatrix);
		}

		internal void SetViewProjectionAndJitterMatrix(global::UnityEngine.Matrix4x4 viewMatrix, global::UnityEngine.Matrix4x4 projectionMatrix, global::UnityEngine.Matrix4x4 jitterMatrix)
		{
			frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().SetViewProjectionAndJitterMatrix(viewMatrix, projectionMatrix, jitterMatrix);
		}

		internal void PushBuiltinShaderConstantsXR(global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool renderIntoTexture)
		{
			frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().PushBuiltinShaderConstantsXR(cmd, renderIntoTexture);
		}

		public global::UnityEngine.Matrix4x4 GetViewMatrix(int viewIndex = 0)
		{
			return frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().GetViewMatrix(viewIndex);
		}

		public global::UnityEngine.Matrix4x4 GetProjectionMatrix(int viewIndex = 0)
		{
			return frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().GetProjectionMatrix(viewIndex);
		}

		internal global::UnityEngine.Matrix4x4 GetProjectionMatrixNoJitter(int viewIndex = 0)
		{
			return frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().GetProjectionMatrixNoJitter(viewIndex);
		}

		internal global::UnityEngine.Matrix4x4 GetGPUProjectionMatrix(bool renderIntoTexture, int viewIndex = 0)
		{
			return frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().GetGPUProjectionMatrix(renderIntoTexture, viewIndex);
		}

		public bool IsHandleYFlipped(global::UnityEngine.Rendering.RTHandle handle)
		{
			return frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().IsHandleYFlipped(handle);
		}

		public bool IsRenderTargetProjectionMatrixFlipped(global::UnityEngine.Rendering.RTHandle color, global::UnityEngine.Rendering.RTHandle depth = null)
		{
			return frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().IsRenderTargetProjectionMatrixFlipped(color, depth);
		}

		internal bool IsTemporalAAEnabled()
		{
			return frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().IsTemporalAAEnabled();
		}
	}
}
