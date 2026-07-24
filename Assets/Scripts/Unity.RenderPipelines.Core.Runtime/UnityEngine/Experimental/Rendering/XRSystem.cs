namespace UnityEngine.Experimental.Rendering
{
	public static class XRSystem
	{
		private static global::UnityEngine.Experimental.Rendering.XRLayoutStack s_Layout = new global::UnityEngine.Experimental.Rendering.XRLayoutStack();

		private static global::System.Func<global::UnityEngine.Experimental.Rendering.XRPassCreateInfo, global::UnityEngine.Experimental.Rendering.XRPass> s_PassAllocator = null;

		private static global::System.Collections.Generic.List<global::UnityEngine.XR.XRDisplaySubsystem> s_DisplayList = new global::System.Collections.Generic.List<global::UnityEngine.XR.XRDisplaySubsystem>();

		private static global::UnityEngine.XR.XRDisplaySubsystem s_Display;

		private static global::UnityEngine.Rendering.MSAASamples s_MSAASamples = global::UnityEngine.Rendering.MSAASamples.None;

		private static float s_OcclusionMeshScaling = 1f;

		private static bool s_UseVisibilityMesh = true;

		private static global::UnityEngine.Material s_OcclusionMeshMaterial;

		private static global::UnityEngine.Material s_MirrorViewMaterial;

		private static global::System.Action<global::UnityEngine.Experimental.Rendering.XRLayout, global::UnityEngine.Camera> s_LayoutOverride = null;

		public static readonly global::UnityEngine.Experimental.Rendering.XRPass emptyPass = new global::UnityEngine.Experimental.Rendering.XRPass();

		public static bool displayActive
		{
			get
			{
				if (s_Display == null)
				{
					return false;
				}
				return s_Display.running;
			}
		}

		public static bool isHDRDisplayOutputActive => s_Display?.hdrOutputSettings?.active == true;

		public static bool singlePassAllowed { get; set; } = true;

		public static global::UnityEngine.Rendering.FoveatedRenderingCaps foveatedRenderingCaps { get; set; }

		public static bool dumpDebugInfo { get; set; } = false;

		public static global::UnityEngine.XR.XRDisplaySubsystem GetActiveDisplay()
		{
			return s_Display;
		}

		public static void Initialize(global::System.Func<global::UnityEngine.Experimental.Rendering.XRPassCreateInfo, global::UnityEngine.Experimental.Rendering.XRPass> passAllocator, global::UnityEngine.Shader occlusionMeshPS, global::UnityEngine.Shader mirrorViewPS)
		{
			if (passAllocator == null)
			{
				throw new global::System.ArgumentNullException("passCreator");
			}
			s_PassAllocator = passAllocator;
			RefreshDeviceInfo();
			foveatedRenderingCaps = global::UnityEngine.SystemInfo.foveatedRenderingCaps;
			if (occlusionMeshPS != null && s_OcclusionMeshMaterial == null)
			{
				s_OcclusionMeshMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(occlusionMeshPS);
			}
			if (mirrorViewPS != null && s_MirrorViewMaterial == null)
			{
				s_MirrorViewMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(mirrorViewPS);
			}
			if (global::UnityEngine.Rendering.XRGraphicsAutomatedTests.enabled)
			{
				SetLayoutOverride(global::UnityEngine.Rendering.XRGraphicsAutomatedTests.OverrideLayout);
			}
			global::UnityEngine.Experimental.Rendering.SinglepassKeywords.STEREO_MULTIVIEW_ON = global::UnityEngine.Rendering.GlobalKeyword.Create("STEREO_MULTIVIEW_ON");
			global::UnityEngine.Experimental.Rendering.SinglepassKeywords.STEREO_INSTANCING_ON = global::UnityEngine.Rendering.GlobalKeyword.Create("STEREO_INSTANCING_ON");
		}

		public static void SetDisplayMSAASamples(global::UnityEngine.Rendering.MSAASamples msaaSamples)
		{
			if (s_MSAASamples == msaaSamples)
			{
				return;
			}
			s_MSAASamples = msaaSamples;
			global::UnityEngine.SubsystemManager.GetSubsystems(s_DisplayList);
			foreach (global::UnityEngine.XR.XRDisplaySubsystem s_Display in s_DisplayList)
			{
				s_Display.SetMSAALevel((int)s_MSAASamples);
			}
		}

		public static global::UnityEngine.Rendering.MSAASamples GetDisplayMSAASamples()
		{
			return s_MSAASamples;
		}

		internal static void SetOcclusionMeshScale(float occlusionMeshScale)
		{
			s_OcclusionMeshScaling = occlusionMeshScale;
		}

		internal static float GetOcclusionMeshScale()
		{
			return s_OcclusionMeshScaling;
		}

		internal static void SetUseVisibilityMesh(bool useVisibilityMesh)
		{
			s_UseVisibilityMesh = useVisibilityMesh;
		}

		internal static bool GetUseVisibilityMesh()
		{
			return s_UseVisibilityMesh;
		}

		internal static void SetMirrorViewMode(int mirrorBlitMode)
		{
			if (s_Display != null)
			{
				s_Display.SetPreferredMirrorBlitMode(mirrorBlitMode);
			}
		}

		internal static int GetMirrorViewMode()
		{
			if (s_Display == null)
			{
				return -6;
			}
			return s_Display.GetPreferredMirrorBlitMode();
		}

		public static void SetRenderScale(float renderScale)
		{
			global::UnityEngine.SubsystemManager.GetSubsystems(s_DisplayList);
			foreach (global::UnityEngine.XR.XRDisplaySubsystem s_Display in s_DisplayList)
			{
				s_Display.scaleOfAllRenderTargets = renderScale;
			}
		}

		public static float GetRenderViewportScale()
		{
			return s_Display.appliedViewportScale;
		}

		public static float GetDynamicResolutionScale()
		{
			return s_Display.globalDynamicScale;
		}

		public static int ScaleTextureWidthForXR(global::UnityEngine.RenderTexture texture)
		{
			return s_Display.ScaledTextureWidth(texture);
		}

		public static int ScaleTextureHeightForXR(global::UnityEngine.RenderTexture texture)
		{
			return s_Display.ScaledTextureHeight(texture);
		}

		public static global::UnityEngine.Experimental.Rendering.XRLayout NewLayout()
		{
			RefreshDeviceInfo();
			return s_Layout.New();
		}

		public static void EndLayout()
		{
			if (dumpDebugInfo)
			{
				s_Layout.top.LogDebugInfo();
			}
			s_Layout.Release();
		}

		public static void RenderMirrorView(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Camera camera)
		{
			global::UnityEngine.Experimental.Rendering.XRMirrorView.RenderMirrorView(cmd, camera, s_MirrorViewMaterial, s_Display);
		}

		public static void Dispose()
		{
			if (s_OcclusionMeshMaterial != null)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(s_OcclusionMeshMaterial);
				s_OcclusionMeshMaterial = null;
			}
			if (s_MirrorViewMaterial != null)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(s_MirrorViewMaterial);
				s_MirrorViewMaterial = null;
			}
		}

		internal static void SetDisplayZRange(float zNear, float zFar)
		{
			if (s_Display != null)
			{
				s_Display.zNear = zNear;
				s_Display.zFar = zFar;
			}
		}

		private static void SetLayoutOverride(global::System.Action<global::UnityEngine.Experimental.Rendering.XRLayout, global::UnityEngine.Camera> action)
		{
			s_LayoutOverride = action;
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void XRSystemInit()
		{
			if (global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline != null)
			{
				RefreshDeviceInfo();
			}
		}

		private static void RefreshDeviceInfo()
		{
			global::UnityEngine.SubsystemManager.GetSubsystems(s_DisplayList);
			if (s_DisplayList.Count > 0)
			{
				if (s_DisplayList.Count > 1)
				{
					throw new global::System.NotImplementedException("Only one XR display is supported!");
				}
				s_Display = s_DisplayList[0];
				s_Display.disableLegacyRenderer = true;
				s_Display.sRGB = global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear;
				s_Display.textureLayout = global::UnityEngine.XR.XRDisplaySubsystem.TextureLayout.Texture2DArray;
				global::UnityEngine.Rendering.TextureXR.maxViews = global::System.Math.Max(global::UnityEngine.Rendering.TextureXR.slices, 2);
			}
			else
			{
				s_Display = null;
			}
		}

		internal static void CreateDefaultLayout(global::UnityEngine.Camera camera, global::UnityEngine.Experimental.Rendering.XRLayout layout)
		{
			if (s_Display == null)
			{
				throw new global::System.NullReferenceException("s_Display");
			}
			for (int i = 0; i < s_Display.GetRenderPassCount(); i++)
			{
				s_Display.GetRenderPass(i, out var renderPass);
				s_Display.GetCullingParameters(camera, renderPass.cullingPassIndex, out var scriptableCullingParameters);
				int renderParameterCount = renderPass.GetRenderParameterCount();
				if (CanUseSinglePass(camera, renderPass))
				{
					global::UnityEngine.Experimental.Rendering.XRPassCreateInfo arg = BuildPass(renderPass, scriptableCullingParameters, layout, i == s_Display.GetRenderPassCount() - 1);
					global::UnityEngine.Experimental.Rendering.XRPass xrPass = s_PassAllocator(arg);
					for (int j = 0; j < renderParameterCount; j++)
					{
						AddViewToPass(xrPass, renderPass, j);
					}
					layout.AddPass(camera, xrPass);
				}
				else
				{
					for (int k = 0; k < renderParameterCount; k++)
					{
						global::UnityEngine.Experimental.Rendering.XRPassCreateInfo arg2 = BuildPass(renderPass, scriptableCullingParameters, layout, i == s_Display.GetRenderPassCount() - 1);
						global::UnityEngine.Experimental.Rendering.XRPass xrPass2 = s_PassAllocator(arg2);
						AddViewToPass(xrPass2, renderPass, k);
						layout.AddPass(camera, xrPass2);
					}
				}
			}
			s_LayoutOverride?.Invoke(layout, camera);
			void AddViewToPass(global::UnityEngine.Experimental.Rendering.XRPass xRPass, global::UnityEngine.XR.XRDisplaySubsystem.XRRenderPass renderPass2, int renderParamIndex)
			{
				renderPass2.GetRenderParameter(camera, renderParamIndex, out var renderParameter);
				xRPass.AddView(BuildView(renderPass2, renderParameter));
			}
		}

		internal static void ReconfigurePass(global::UnityEngine.Experimental.Rendering.XRPass xrPass, global::UnityEngine.Camera camera)
		{
			if (xrPass.enabled && s_Display != null)
			{
				s_Display.GetRenderPass(xrPass.multipassId, out var renderPass);
				s_Display.GetCullingParameters(camera, renderPass.cullingPassIndex, out var scriptableCullingParameters);
				xrPass.AssignCullingParams(renderPass.cullingPassIndex, scriptableCullingParameters);
				for (int i = 0; i < renderPass.GetRenderParameterCount(); i++)
				{
					renderPass.GetRenderParameter(camera, i, out var renderParameter);
					xrPass.AssignView(i, BuildView(renderPass, renderParameter));
				}
				s_LayoutOverride?.Invoke(s_Layout.top, camera);
			}
		}

		private static bool CanUseSinglePass(global::UnityEngine.Camera camera, global::UnityEngine.XR.XRDisplaySubsystem.XRRenderPass renderPass)
		{
			if (!singlePassAllowed)
			{
				return false;
			}
			if (renderPass.renderTargetDesc.dimension != global::UnityEngine.Rendering.TextureDimension.Tex2DArray)
			{
				return false;
			}
			if (renderPass.GetRenderParameterCount() != 2 || renderPass.renderTargetDesc.volumeDepth != 2)
			{
				return false;
			}
			renderPass.GetRenderParameter(camera, 0, out var renderParameter);
			renderPass.GetRenderParameter(camera, 1, out var renderParameter2);
			if (renderParameter.textureArraySlice != 0 || renderParameter2.textureArraySlice != 1)
			{
				return false;
			}
			if (renderParameter.viewport != renderParameter2.viewport)
			{
				return false;
			}
			return true;
		}

		private static global::UnityEngine.Experimental.Rendering.XRView BuildView(global::UnityEngine.XR.XRDisplaySubsystem.XRRenderPass renderPass, global::UnityEngine.XR.XRDisplaySubsystem.XRRenderParameter renderParameter)
		{
			global::UnityEngine.Rect viewport = renderParameter.viewport;
			viewport.x *= renderPass.renderTargetScaledWidth;
			viewport.width *= renderPass.renderTargetScaledWidth;
			viewport.y *= renderPass.renderTargetScaledHeight;
			viewport.height *= renderPass.renderTargetScaledHeight;
			global::UnityEngine.Mesh occlusionMesh = (global::UnityEngine.Rendering.XRGraphicsAutomatedTests.running ? null : renderParameter.occlusionMesh);
			global::UnityEngine.Mesh visibleMesh = (global::UnityEngine.Rendering.XRGraphicsAutomatedTests.running ? null : renderParameter.visibleMesh);
			return new global::UnityEngine.Experimental.Rendering.XRView(renderParameter.projection, renderParameter.view, renderParameter.previousView, renderParameter.isPreviousViewValid, viewport, occlusionMesh, visibleMesh, renderParameter.textureArraySlice);
		}

		private static global::UnityEngine.RenderTextureDescriptor XrRenderTextureDescToUnityRenderTextureDesc(global::UnityEngine.RenderTextureDescriptor xrDesc)
		{
			global::UnityEngine.RenderTextureDescriptor result = new global::UnityEngine.RenderTextureDescriptor(xrDesc.width, xrDesc.height, xrDesc.graphicsFormat, xrDesc.depthStencilFormat, xrDesc.mipCount);
			result.dimension = xrDesc.dimension;
			result.msaaSamples = xrDesc.msaaSamples;
			result.volumeDepth = xrDesc.volumeDepth;
			result.vrUsage = xrDesc.vrUsage;
			result.sRGB = xrDesc.sRGB;
			result.shadowSamplingMode = xrDesc.shadowSamplingMode;
			return result;
		}

		private static global::UnityEngine.Experimental.Rendering.XRPassCreateInfo BuildPass(global::UnityEngine.XR.XRDisplaySubsystem.XRRenderPass xrRenderPass, global::UnityEngine.Rendering.ScriptableCullingParameters cullingParameters, global::UnityEngine.Experimental.Rendering.XRLayout layout, bool isLastPass)
		{
			return new global::UnityEngine.Experimental.Rendering.XRPassCreateInfo
			{
				renderTarget = xrRenderPass.renderTarget,
				renderTargetDesc = XrRenderTextureDescToUnityRenderTextureDesc(xrRenderPass.renderTargetDesc),
				renderTargetScaledWidth = xrRenderPass.renderTargetScaledWidth,
				renderTargetScaledHeight = xrRenderPass.renderTargetScaledHeight,
				hasMotionVectorPass = xrRenderPass.hasMotionVectorPass,
				motionVectorRenderTarget = xrRenderPass.motionVectorRenderTarget,
				motionVectorRenderTargetDesc = XrRenderTextureDescToUnityRenderTextureDesc(xrRenderPass.motionVectorRenderTargetDesc),
				cullingParameters = cullingParameters,
				occlusionMeshMaterial = s_OcclusionMeshMaterial,
				occlusionMeshScale = GetOcclusionMeshScale(),
				foveatedRenderingInfo = xrRenderPass.foveatedRenderingInfo,
				multipassId = layout.GetActivePasses().Count,
				cullingPassId = xrRenderPass.cullingPassIndex,
				copyDepth = xrRenderPass.shouldFillOutDepth,
				spaceWarpRightHandedNDC = xrRenderPass.spaceWarpRightHandedNDC,
				xrSdkRenderPass = xrRenderPass,
				isLastCameraPass = isLastPass
			};
		}
	}
}
