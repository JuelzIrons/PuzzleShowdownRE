namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", null, null)]
	public class RenderObjectsPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.RenderObjects.CustomCameraSettings cameraSettings;

			internal global::UnityEngine.Rendering.Universal.RenderPassEvent renderPassEvent;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle color;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererListHdl;

			internal global::UnityEngine.Rendering.Universal.DebugRendererLists debugRendererLists;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.RendererList rendererList;
		}

		private global::UnityEngine.Rendering.Universal.RenderQueueType renderQueueType;

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private global::UnityEngine.Rendering.Universal.RenderObjects.CustomCameraSettings m_CameraSettings;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> m_ShaderTagIdList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>();

		private global::UnityEngine.Rendering.Universal.RenderObjectsPass.PassData m_PassData;

		private global::UnityEngine.Rendering.RenderStateBlock m_RenderStateBlock;

		public global::UnityEngine.Material overrideMaterial { get; set; }

		public int overrideMaterialPassIndex { get; set; }

		public global::UnityEngine.Shader overrideShader { get; set; }

		public int overrideShaderPassIndex { get; set; }

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("Use SetDepthState instead. #from(2023.1) #breakingFrom(2023.1)", true)]
		public void SetDetphState(bool writeEnabled, global::UnityEngine.Rendering.CompareFunction function = global::UnityEngine.Rendering.CompareFunction.Less)
		{
			SetDepthState(writeEnabled, function);
		}

		public void SetDepthState(bool writeEnabled, global::UnityEngine.Rendering.CompareFunction function = global::UnityEngine.Rendering.CompareFunction.Less)
		{
			m_RenderStateBlock.mask |= global::UnityEngine.Rendering.RenderStateMask.Depth;
			m_RenderStateBlock.depthState = new global::UnityEngine.Rendering.DepthState(writeEnabled, function);
		}

		public void SetStencilState(int reference, global::UnityEngine.Rendering.CompareFunction compareFunction, global::UnityEngine.Rendering.StencilOp passOp, global::UnityEngine.Rendering.StencilOp failOp, global::UnityEngine.Rendering.StencilOp zFailOp)
		{
			global::UnityEngine.Rendering.StencilState defaultValue = global::UnityEngine.Rendering.StencilState.defaultValue;
			defaultValue.enabled = true;
			defaultValue.SetCompareFunction(compareFunction);
			defaultValue.SetPassOperation(passOp);
			defaultValue.SetFailOperation(failOp);
			defaultValue.SetZFailOperation(zFailOp);
			m_RenderStateBlock.mask |= global::UnityEngine.Rendering.RenderStateMask.Stencil;
			m_RenderStateBlock.stencilReference = reference;
			m_RenderStateBlock.stencilState = defaultValue;
		}

		public RenderObjectsPass(string profilerTag, global::UnityEngine.Rendering.Universal.RenderPassEvent renderPassEvent, string[] shaderTags, global::UnityEngine.Rendering.Universal.RenderQueueType renderQueueType, int layerMask, global::UnityEngine.Rendering.Universal.RenderObjects.CustomCameraSettings cameraSettings)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(profilerTag);
			Init(renderPassEvent, shaderTags, renderQueueType, layerMask, cameraSettings);
		}

		internal RenderObjectsPass(global::UnityEngine.Rendering.Universal.URPProfileId profileId, global::UnityEngine.Rendering.Universal.RenderPassEvent renderPassEvent, string[] shaderTags, global::UnityEngine.Rendering.Universal.RenderQueueType renderQueueType, int layerMask, global::UnityEngine.Rendering.Universal.RenderObjects.CustomCameraSettings cameraSettings)
		{
			base.profilingSampler = global::UnityEngine.Rendering.ProfilingSampler.Get(profileId);
			Init(renderPassEvent, shaderTags, renderQueueType, layerMask, cameraSettings);
		}

		internal void Init(global::UnityEngine.Rendering.Universal.RenderPassEvent renderPassEvent, string[] shaderTags, global::UnityEngine.Rendering.Universal.RenderQueueType renderQueueType, int layerMask, global::UnityEngine.Rendering.Universal.RenderObjects.CustomCameraSettings cameraSettings)
		{
			m_PassData = new global::UnityEngine.Rendering.Universal.RenderObjectsPass.PassData();
			base.renderPassEvent = renderPassEvent;
			this.renderQueueType = renderQueueType;
			overrideMaterial = null;
			overrideMaterialPassIndex = 0;
			overrideShader = null;
			overrideShaderPassIndex = 0;
			global::UnityEngine.Rendering.RenderQueueRange value = ((renderQueueType == global::UnityEngine.Rendering.Universal.RenderQueueType.Transparent) ? global::UnityEngine.Rendering.RenderQueueRange.transparent : global::UnityEngine.Rendering.RenderQueueRange.opaque);
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(value, layerMask);
			if (shaderTags != null && shaderTags.Length != 0)
			{
				foreach (string name in shaderTags)
				{
					m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId(name));
				}
			}
			else
			{
				m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("SRPDefaultUnlit"));
				m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("UniversalForward"));
				m_ShaderTagIdList.Add(new global::UnityEngine.Rendering.ShaderTagId("UniversalForwardOnly"));
			}
			m_RenderStateBlock = new global::UnityEngine.Rendering.RenderStateBlock(global::UnityEngine.Rendering.RenderStateMask.Nothing);
			m_CameraSettings = cameraSettings;
		}

		private static void ExecutePass(global::UnityEngine.Rendering.Universal.RenderObjectsPass.PassData passData, global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RendererList rendererList, bool isYFlipped)
		{
			global::UnityEngine.Camera camera = passData.cameraData.camera;
			global::UnityEngine.Rect pixelRect = passData.cameraData.pixelRect;
			float aspect = pixelRect.width / pixelRect.height;
			if (passData.cameraSettings.overrideCamera)
			{
				if (passData.cameraData.xr.enabled)
				{
					global::UnityEngine.Debug.LogWarning("RenderObjects pass is configured to override camera matrices. While rendering in stereo camera matrices cannot be overridden.");
				}
				else
				{
					global::UnityEngine.Matrix4x4 proj = global::UnityEngine.Matrix4x4.Perspective(passData.cameraSettings.cameraFieldOfView, aspect, camera.nearClipPlane, camera.farClipPlane);
					proj = global::UnityEngine.GL.GetGPUProjectionMatrix(proj, isYFlipped);
					global::UnityEngine.Matrix4x4 viewMatrix = passData.cameraData.GetViewMatrix();
					global::UnityEngine.Vector4 column = viewMatrix.GetColumn(3);
					viewMatrix.SetColumn(3, column + passData.cameraSettings.offset);
					global::UnityEngine.Rendering.Universal.RenderingUtils.SetViewAndProjectionMatrices(cmd, viewMatrix, proj, setInverseMatrices: false);
				}
			}
			if (global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(passData.cameraData) != null)
			{
				passData.debugRendererLists.DrawWithRendererList(cmd);
			}
			else
			{
				cmd.DrawRendererList(rendererList);
			}
			if (passData.cameraSettings.overrideCamera && passData.cameraSettings.restoreCamera && !passData.cameraData.xr.enabled)
			{
				global::UnityEngine.Rendering.Universal.RenderingUtils.SetViewAndProjectionMatrices(cmd, passData.cameraData.GetViewMatrix(), global::UnityEngine.GL.GetGPUProjectionMatrix(passData.cameraData.GetProjectionMatrix(), isYFlipped), setInverseMatrices: false);
			}
		}

		private void InitPassData(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, ref global::UnityEngine.Rendering.Universal.RenderObjectsPass.PassData passData)
		{
			passData.cameraSettings = m_CameraSettings;
			passData.renderPassEvent = base.renderPassEvent;
			passData.cameraData = cameraData;
		}

		private void InitRendererLists(global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, ref global::UnityEngine.Rendering.Universal.RenderObjectsPass.PassData passData, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, bool useRenderGraph)
		{
			global::UnityEngine.Rendering.SortingCriteria sortingCriteria = ((renderQueueType == global::UnityEngine.Rendering.Universal.RenderQueueType.Transparent) ? global::UnityEngine.Rendering.SortingCriteria.CommonTransparent : passData.cameraData.defaultOpaqueSortFlags);
			global::UnityEngine.Rendering.DrawingSettings drawingSettings = global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(m_ShaderTagIdList, renderingData, passData.cameraData, lightData, sortingCriteria);
			drawingSettings.overrideMaterial = overrideMaterial;
			drawingSettings.overrideMaterialPassIndex = overrideMaterialPassIndex;
			drawingSettings.overrideShader = overrideShader;
			drawingSettings.overrideShaderPassIndex = overrideShaderPassIndex;
			global::UnityEngine.Rendering.Universal.DebugHandler activeDebugHandler = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(passData.cameraData);
			_ = m_FilteringSettings;
			if (useRenderGraph)
			{
				if (activeDebugHandler != null)
				{
					passData.debugRendererLists = activeDebugHandler.CreateRendererListsWithDebugRenderState(renderGraph, ref renderingData.cullResults, ref drawingSettings, ref m_FilteringSettings, ref m_RenderStateBlock);
				}
				else
				{
					global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref renderingData.cullResults, drawingSettings, m_FilteringSettings, m_RenderStateBlock, ref passData.rendererListHdl);
				}
			}
			else if (activeDebugHandler != null)
			{
				passData.debugRendererLists = activeDebugHandler.CreateRendererListsWithDebugRenderState(context, ref renderingData.cullResults, ref drawingSettings, ref m_FilteringSettings, ref m_RenderStateBlock);
			}
			else
			{
				global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref renderingData.cullResults, drawingSettings, m_FilteringSettings, m_RenderStateBlock, ref passData.rendererList);
			}
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.RenderObjectsPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.RenderObjectsPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\RenderObjectsPass.cs", 275);
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			InitPassData(universalCameraData, ref passData);
			passData.color = universalResourceData.activeColorTexture;
			rasterRenderGraphBuilder.SetRenderAttachment(universalResourceData.activeColorTexture, 0);
			if (universalCameraData.imageScalingMode != global::UnityEngine.Rendering.Universal.ImageScalingMode.Upscaling || passData.renderPassEvent != global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingPostProcessing)
			{
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(universalResourceData.activeDepthTexture);
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle mainShadowsTexture = universalResourceData.mainShadowsTexture;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle additionalShadowsTexture = universalResourceData.additionalShadowsTexture;
			if (mainShadowsTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in mainShadowsTexture);
			}
			if (additionalShadowsTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in additionalShadowsTexture);
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] dBuffer = universalResourceData.dBuffer;
			for (int i = 0; i < dBuffer.Length; i++)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle = dBuffer[i];
				if (textureHandle.IsValid())
				{
					rasterRenderGraphBuilder.UseTexture(in textureHandle);
				}
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ssaoTexture = universalResourceData.ssaoTexture;
			if (ssaoTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in ssaoTexture);
			}
			InitRendererLists(renderingData, lightData, ref passData, default(global::UnityEngine.Rendering.ScriptableRenderContext), renderGraph, useRenderGraph: true);
			if (global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(passData.cameraData) != null)
			{
				passData.debugRendererLists.PrepareRendererListForRasterPass(rasterRenderGraphBuilder);
			}
			else
			{
				rasterRenderGraphBuilder.UseRendererList(in passData.rendererListHdl);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			if (universalCameraData.xr.enabled)
			{
				rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering && universalCameraData.xrUniversal.canFoveateIntermediatePasses);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.RenderObjectsPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
			{
				bool isYFlipped = global::UnityEngine.Rendering.Universal.RenderingUtils.IsHandleYFlipped(in rgContext, in data.color);
				ExecutePass(data, rgContext.cmd, data.rendererListHdl, isYFlipped);
			});
		}
	}
}
