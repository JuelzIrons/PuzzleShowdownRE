namespace UnityEngine.Rendering.Universal
{
	public class XRDepthMotionPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle objMotionRendererList;

			internal global::UnityEngine.Matrix4x4[] previousViewProjectionStereo = new global::UnityEngine.Matrix4x4[2];

			internal global::UnityEngine.Matrix4x4[] viewProjectionStereo = new global::UnityEngine.Matrix4x4[2];

			internal global::UnityEngine.Material xrMotionVector;
		}

		public const string k_MotionOnlyShaderTagIdName = "XRMotionVectors";

		private static readonly global::UnityEngine.Rendering.ShaderTagId k_MotionOnlyShaderTagId = new global::UnityEngine.Rendering.ShaderTagId("XRMotionVectors");

		private static readonly int k_SpaceWarpNDCModifier = global::UnityEngine.Shader.PropertyToID("_SpaceWarpNDCModifier");

		private global::UnityEngine.Rendering.RTHandle m_XRMotionVectorColor;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle xrMotionVectorColor;

		private global::UnityEngine.Rendering.RTHandle m_XRMotionVectorDepth;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle xrMotionVectorDepth;

		private bool m_XRSpaceWarpRightHandedNDC;

		private const int k_XRViewCountPerPass = 2;

		private global::UnityEngine.Matrix4x4[] m_StagingMatrixArray = new global::UnityEngine.Matrix4x4[2];

		private global::UnityEngine.Matrix4x4[] m_PreviousStagingMatrixArray = new global::UnityEngine.Matrix4x4[2];

		private const int k_XRViewCount = 4;

		private global::UnityEngine.Matrix4x4[] m_ViewProjection = new global::UnityEngine.Matrix4x4[4];

		private global::UnityEngine.Matrix4x4[] m_PreviousViewProjection = new global::UnityEngine.Matrix4x4[4];

		private int m_LastFrameIndex;

		private global::UnityEngine.Material m_XRMotionVectorMaterial;

		public XRDepthMotionPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Shader xrMotionVector)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("XRDepthMotionPass");
			base.renderPassEvent = evt;
			ResetMotionData();
			m_XRMotionVectorMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(xrMotionVector);
			xrMotionVectorColor = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			m_XRMotionVectorColor = null;
			xrMotionVectorDepth = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			m_XRMotionVectorDepth = null;
		}

		private static global::UnityEngine.Rendering.DrawingSettings GetObjectMotionDrawingSettings(global::UnityEngine.Camera camera)
		{
			global::UnityEngine.Rendering.SortingSettings sortingSettings = new global::UnityEngine.Rendering.SortingSettings(camera);
			sortingSettings.criteria = global::UnityEngine.Rendering.SortingCriteria.CommonOpaque;
			global::UnityEngine.Rendering.SortingSettings sortingSettings2 = sortingSettings;
			global::UnityEngine.Rendering.DrawingSettings drawingSettings = new global::UnityEngine.Rendering.DrawingSettings(k_MotionOnlyShaderTagId, sortingSettings2);
			drawingSettings.perObjectData = global::UnityEngine.Rendering.PerObjectData.MotionVectors;
			drawingSettings.enableDynamicBatching = false;
			drawingSettings.enableInstancing = true;
			global::UnityEngine.Rendering.DrawingSettings result = drawingSettings;
			result.SetShaderPassName(0, k_MotionOnlyShaderTagId);
			return result;
		}

		private void InitObjectMotionRendererLists(ref global::UnityEngine.Rendering.Universal.XRDepthMotionPass.PassData passData, ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Camera camera)
		{
			global::UnityEngine.Rendering.DrawingSettings objectMotionDrawingSettings = GetObjectMotionDrawingSettings(camera);
			global::UnityEngine.Rendering.FilteringSettings fs = new global::UnityEngine.Rendering.FilteringSettings(global::UnityEngine.Rendering.RenderQueueRange.opaque, camera.cullingMask);
			fs.forceAllMotionVectorObjects = true;
			global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(rsb: new global::UnityEngine.Rendering.RenderStateBlock(global::UnityEngine.Rendering.RenderStateMask.Nothing), renderGraph: renderGraph, cullResults: ref cullResults, ds: objectMotionDrawingSettings, fs: fs, rl: ref passData.objMotionRendererList);
		}

		private void InitPassData(ref global::UnityEngine.Rendering.Universal.XRDepthMotionPass.PassData passData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Experimental.Rendering.XRPass xr = cameraData.xr;
			int sourceIndex = xr.viewCount * xr.multipassId;
			global::System.Array.Copy(m_PreviousViewProjection, sourceIndex, m_PreviousStagingMatrixArray, 0, xr.viewCount);
			passData.previousViewProjectionStereo = m_PreviousStagingMatrixArray;
			global::System.Array.Copy(m_ViewProjection, sourceIndex, m_StagingMatrixArray, 0, xr.viewCount);
			passData.viewProjectionStereo = m_StagingMatrixArray;
			passData.xrMotionVector = m_XRMotionVectorMaterial;
		}

		private void ImportXRMotionColorAndDepth(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.RenderTargetIdentifier motionVectorRenderTarget = cameraData.xr.motionVectorRenderTarget;
			if (m_XRMotionVectorColor == null)
			{
				m_XRMotionVectorColor = global::UnityEngine.Rendering.RTHandles.Alloc(motionVectorRenderTarget);
			}
			else if (m_XRMotionVectorColor.nameID != motionVectorRenderTarget)
			{
				global::UnityEngine.Rendering.RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref m_XRMotionVectorColor, motionVectorRenderTarget);
			}
			global::UnityEngine.Rendering.RenderTargetIdentifier motionVectorRenderTarget2 = cameraData.xr.motionVectorRenderTarget;
			if (m_XRMotionVectorDepth == null)
			{
				m_XRMotionVectorDepth = global::UnityEngine.Rendering.RTHandles.Alloc(motionVectorRenderTarget2);
			}
			else if (m_XRMotionVectorDepth.nameID != motionVectorRenderTarget2)
			{
				global::UnityEngine.Rendering.RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref m_XRMotionVectorDepth, motionVectorRenderTarget2);
			}
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo = new global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo
			{
				width = cameraData.xr.motionVectorRenderTargetDesc.width,
				height = cameraData.xr.motionVectorRenderTargetDesc.height,
				volumeDepth = cameraData.xr.motionVectorRenderTargetDesc.volumeDepth,
				msaaSamples = cameraData.xr.motionVectorRenderTargetDesc.msaaSamples,
				format = cameraData.xr.motionVectorRenderTargetDesc.graphicsFormat
			};
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo renderTargetInfo2 = default(global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo);
			renderTargetInfo2 = renderTargetInfo;
			renderTargetInfo2.format = cameraData.xr.motionVectorRenderTargetDesc.depthStencilFormat;
			global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams importParams = new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
			{
				clearOnFirstUse = true,
				clearColor = global::UnityEngine.Color.black,
				discardOnLastUse = false
			};
			global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams importParams2 = new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
			{
				clearOnFirstUse = true,
				clearColor = global::UnityEngine.Color.black,
				discardOnLastUse = false
			};
			xrMotionVectorColor = renderGraph.ImportTexture(m_XRMotionVectorColor, renderTargetInfo, importParams);
			xrMotionVectorDepth = renderGraph.ImportTexture(m_XRMotionVectorDepth, renderTargetInfo2, importParams2);
			m_XRSpaceWarpRightHandedNDC = cameraData.xr.spaceWarpRightHandedNDC;
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			if (!universalCameraData.xr.enabled || !universalCameraData.xr.singlePassEnabled)
			{
				global::UnityEngine.Debug.LogWarning("XRDepthMotionPass::Render is skipped because either XR is not enabled or singlepass rendering is not enabled.");
				return;
			}
			if (!universalCameraData.xr.hasMotionVectorPass)
			{
				global::UnityEngine.Debug.LogWarning("XRDepthMotionPass::Render is skipped because XR motion vector is not enabled for the current XRPass.");
				return;
			}
			ImportXRMotionColorAndDepth(renderGraph, universalCameraData);
			universalCameraData.camera.depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth | global::UnityEngine.DepthTextureMode.MotionVectors;
			global::UnityEngine.Rendering.Universal.XRDepthMotionPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.XRDepthMotionPass.PassData>("XR Motion Pass", out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\XRDepthMotionPass.cs", 201);
			rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering);
			rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			rasterRenderGraphBuilder.SetRenderAttachment(xrMotionVectorColor, 0);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(xrMotionVectorDepth);
			InitObjectMotionRendererLists(ref passData, ref universalRenderingData.cullResults, renderGraph, universalCameraData.camera);
			rasterRenderGraphBuilder.UseRendererList(in passData.objMotionRendererList);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			InitPassData(ref passData, universalCameraData);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.XRDepthMotionPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				context.cmd.SetGlobalMatrixArray(global::UnityEngine.Rendering.Universal.ShaderPropertyId.previousViewProjectionNoJitterStereo, data.previousViewProjectionStereo);
				context.cmd.SetGlobalMatrixArray(global::UnityEngine.Rendering.Universal.ShaderPropertyId.viewProjectionNoJitterStereo, data.viewProjectionStereo);
				context.cmd.SetGlobalFloat(k_SpaceWarpNDCModifier, m_XRSpaceWarpRightHandedNDC ? (-1f) : 1f);
				context.cmd.DrawRendererList(passData.objMotionRendererList);
				context.cmd.DrawProcedural(global::UnityEngine.Matrix4x4.identity, data.xrMotionVector, 0, global::UnityEngine.MeshTopology.Triangles, 3, 1);
			});
		}

		private void ResetMotionData()
		{
			for (int i = 0; i < 4; i++)
			{
				m_ViewProjection[i] = global::UnityEngine.Matrix4x4.identity;
				m_PreviousViewProjection[i] = global::UnityEngine.Matrix4x4.identity;
			}
			m_LastFrameIndex = -1;
		}

		public void Update(ref global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (!cameraData.xr.enabled || !cameraData.xr.singlePassEnabled)
			{
				global::UnityEngine.Debug.LogWarning("XRDepthMotionPass::Update is skipped because either XR is not enabled or singlepass rendering is not enabled.");
			}
			else if (m_LastFrameIndex != global::UnityEngine.Time.frameCount)
			{
				global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(), renderIntoTexture: false) * cameraData.GetViewMatrix();
				global::UnityEngine.Matrix4x4 matrix4x2 = global::UnityEngine.GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(1), renderIntoTexture: false) * cameraData.GetViewMatrix(1);
				global::UnityEngine.Experimental.Rendering.XRPass xr = cameraData.xr;
				int num = xr.viewCount * xr.multipassId;
				m_PreviousViewProjection[num] = m_ViewProjection[num];
				m_PreviousViewProjection[num + 1] = m_ViewProjection[num + 1];
				m_ViewProjection[num] = matrix4x;
				m_ViewProjection[num + 1] = matrix4x2;
				if (cameraData.xr.isLastCameraPass)
				{
					m_LastFrameIndex = global::UnityEngine.Time.frameCount;
				}
			}
		}

		public void Dispose()
		{
			m_XRMotionVectorColor?.Release();
			m_XRMotionVectorDepth?.Release();
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_XRMotionVectorMaterial);
		}
	}
}
