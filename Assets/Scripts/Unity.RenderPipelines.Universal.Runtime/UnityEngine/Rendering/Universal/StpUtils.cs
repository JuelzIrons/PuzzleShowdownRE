namespace UnityEngine.Rendering.Universal
{
	internal static class StpUtils
	{
		internal static global::UnityEngine.Rendering.Universal.TemporalAA.JitterFunc s_JitterFunc = CalculateJitter;

		private static void CalculateJitter(int frameIndex, out global::UnityEngine.Vector2 jitter, out bool allowScaling)
		{
			jitter = -global::UnityEngine.Rendering.STP.Jit16(frameIndex);
			allowScaling = false;
		}

		private static void PopulateStpConfig(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputColor, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputDepth, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputMotion, int debugViewIndex, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle debugView, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Texture2D noiseTexture, out global::UnityEngine.Rendering.STP.Config config)
		{
			cameraData.camera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component);
			global::UnityEngine.Rendering.Universal.MotionVectorsPersistentData motionVectorsPersistentData = component.motionVectorsPersistentData;
			config.enableHwDrs = false;
			config.enableTexArray = cameraData.xr.enabled && cameraData.xr.singlePassEnabled;
			config.enableMotionScaling = true;
			config.noiseTexture = noiseTexture;
			config.inputColor = inputColor;
			config.inputDepth = inputDepth;
			config.inputMotion = inputMotion;
			config.inputStencil = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			config.stencilMask = 0;
			config.debugView = debugView;
			config.destination = destination;
			global::UnityEngine.Rendering.Universal.StpHistory stpHistory = cameraData.stpHistory;
			int num = ((cameraData.xr.enabled && !cameraData.xr.singlePassEnabled) ? cameraData.xr.multipassId : 0);
			config.historyContext = stpHistory.GetHistoryContext(num);
			config.nearPlane = cameraData.camera.nearClipPlane;
			config.farPlane = cameraData.camera.farClipPlane;
			config.frameIndex = global::UnityEngine.Rendering.Universal.TemporalAA.CalculateTaaFrameIndex(ref cameraData.taaSettings);
			config.hasValidHistory = !cameraData.resetHistory;
			config.debugViewIndex = debugViewIndex;
			config.deltaTime = motionVectorsPersistentData.deltaTime;
			config.lastDeltaTime = motionVectorsPersistentData.lastDeltaTime;
			config.currentImageSize = new global::UnityEngine.Vector2Int(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
			config.priorImageSize = config.currentImageSize;
			config.outputImageSize = new global::UnityEngine.Vector2Int(cameraData.pixelWidth, cameraData.pixelHeight);
			int num2 = ((!cameraData.xr.enabled) ? 1 : cameraData.xr.viewCount);
			global::UnityEngine.Rendering.STP.PerViewConfig perViewConfig = default(global::UnityEngine.Rendering.STP.PerViewConfig);
			for (int i = 0; i < num2; i++)
			{
				int num3 = i + num;
				perViewConfig.currentProj = motionVectorsPersistentData.projectionStereo[num3];
				perViewConfig.lastProj = motionVectorsPersistentData.previousProjectionStereo[num3];
				perViewConfig.lastLastProj = motionVectorsPersistentData.previousPreviousProjectionStereo[num3];
				perViewConfig.currentView = motionVectorsPersistentData.viewStereo[num3];
				perViewConfig.lastView = motionVectorsPersistentData.previousViewStereo[num3];
				perViewConfig.lastLastView = motionVectorsPersistentData.previousPreviousViewStereo[num3];
				global::UnityEngine.Vector3 worldSpaceCameraPos = motionVectorsPersistentData.worldSpaceCameraPos;
				global::UnityEngine.Vector3 previousWorldSpaceCameraPos = motionVectorsPersistentData.previousWorldSpaceCameraPos;
				global::UnityEngine.Vector3 previousPreviousWorldSpaceCameraPos = motionVectorsPersistentData.previousPreviousWorldSpaceCameraPos;
				perViewConfig.currentView.SetColumn(3, new global::UnityEngine.Vector4(0f - worldSpaceCameraPos.x, 0f - worldSpaceCameraPos.y, 0f - worldSpaceCameraPos.z, 1f));
				perViewConfig.lastView.SetColumn(3, new global::UnityEngine.Vector4(0f - previousWorldSpaceCameraPos.x, 0f - previousWorldSpaceCameraPos.y, 0f - previousWorldSpaceCameraPos.z, 1f));
				perViewConfig.lastLastView.SetColumn(3, new global::UnityEngine.Vector4(0f - previousPreviousWorldSpaceCameraPos.x, 0f - previousPreviousWorldSpaceCameraPos.y, 0f - previousPreviousWorldSpaceCameraPos.z, 1f));
				global::UnityEngine.Rendering.STP.perViewConfigs[i] = perViewConfig;
			}
			config.numActiveViews = num2;
			config.perViewConfigs = global::UnityEngine.Rendering.STP.perViewConfigs;
		}

		internal static void Execute(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputColor, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputDepth, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputMotion, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination, global::UnityEngine.Texture2D noiseTexture)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			int debugViewIndex = 0;
			global::UnityEngine.Rendering.Universal.DebugHandler activeDebugHandler = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			if (activeDebugHandler != null && activeDebugHandler.TryGetFullscreenDebugMode(out var debugFullScreenMode) && debugFullScreenMode == global::UnityEngine.Rendering.Universal.DebugFullScreenMode.STP)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(cameraData.pixelWidth, cameraData.pixelHeight, dynamicResolution: false, cameraData.xr.enabled && cameraData.xr.singlePassEnabled)
				{
					name = "STP Debug View",
					format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm,
					clearBuffer = true,
					enableRandomWrite = true
				};
				textureHandle = renderGraph.CreateTexture(in desc);
				debugViewIndex = activeDebugHandler.stpDebugViewIndex;
				resourceData.stpDebugView = textureHandle;
			}
			PopulateStpConfig(cameraData, inputColor, inputDepth, inputMotion, debugViewIndex, textureHandle, destination, noiseTexture, out var config);
			global::UnityEngine.Rendering.STP.Execute(renderGraph, ref config);
		}
	}
}
