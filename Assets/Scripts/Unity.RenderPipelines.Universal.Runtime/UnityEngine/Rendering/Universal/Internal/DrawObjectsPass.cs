namespace UnityEngine.Rendering.Universal.Internal
{
	public class DrawObjectsPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		internal class PassData
		{
			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle albedoHdl;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthHdl;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle screenSpaceIrradianceHdl;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal bool isOpaque;

			internal bool shouldTransparentsReceiveShadows;

			internal uint batchLayerMask;

			internal bool isActiveTargetBackBuffer;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererListHdl;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle objectsWithErrorRendererListHdl;

			internal global::UnityEngine.Rendering.Universal.DebugRendererLists debugRendererLists;

			internal global::UnityEngine.Rendering.RendererList rendererList;

			internal global::UnityEngine.Rendering.RendererList objectsWithErrorRendererList;
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public bool m_IsActiveTargetBackBuffer;

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private global::UnityEngine.Rendering.RenderStateBlock m_RenderStateBlock;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> m_ShaderTagIdList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>();

		private bool m_IsOpaque;

		public bool m_ShouldTransparentsReceiveShadows;

		private static readonly int s_DrawObjectPassDataPropID = global::UnityEngine.Shader.PropertyToID("_DrawObjectPassData");

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void Setup(global::UnityEngine.Rendering.RTHandle colorAttachment, global::UnityEngine.Rendering.RTHandle renderingLayersTexture, global::UnityEngine.Rendering.RTHandle depthAttachment)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Configure(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public DrawObjectsPass(string profilerTag, global::UnityEngine.Rendering.ShaderTagId[] shaderTagIds, bool opaque, global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.RenderQueueRange renderQueueRange, global::UnityEngine.LayerMask layerMask, global::UnityEngine.Rendering.StencilState stencilState, int stencilReference)
		{
			Init(opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference, shaderTagIds);
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(profilerTag);
		}

		public DrawObjectsPass(string profilerTag, bool opaque, global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.RenderQueueRange renderQueueRange, global::UnityEngine.LayerMask layerMask, global::UnityEngine.Rendering.StencilState stencilState, int stencilReference)
			: this(profilerTag, null, opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference)
		{
		}

		internal DrawObjectsPass(global::UnityEngine.Rendering.Universal.URPProfileId profileId, bool opaque, global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.RenderQueueRange renderQueueRange, global::UnityEngine.LayerMask layerMask, global::UnityEngine.Rendering.StencilState stencilState, int stencilReference)
		{
			Init(opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference);
			base.profilingSampler = global::UnityEngine.Rendering.ProfilingSampler.Get(profileId);
		}

		internal void Init(bool opaque, global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.RenderQueueRange renderQueueRange, global::UnityEngine.LayerMask layerMask, global::UnityEngine.Rendering.StencilState stencilState, int stencilReference, global::UnityEngine.Rendering.ShaderTagId[] shaderTagIds = null)
		{
			if (shaderTagIds == null)
			{
				shaderTagIds = new global::UnityEngine.Rendering.ShaderTagId[3]
				{
					new global::UnityEngine.Rendering.ShaderTagId("SRPDefaultUnlit"),
					new global::UnityEngine.Rendering.ShaderTagId("UniversalForward"),
					new global::UnityEngine.Rendering.ShaderTagId("UniversalForwardOnly")
				};
			}
			global::UnityEngine.Rendering.ShaderTagId[] array = shaderTagIds;
			foreach (global::UnityEngine.Rendering.ShaderTagId item in array)
			{
				m_ShaderTagIdList.Add(item);
			}
			base.renderPassEvent = evt;
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(renderQueueRange, layerMask);
			m_RenderStateBlock = new global::UnityEngine.Rendering.RenderStateBlock(global::UnityEngine.Rendering.RenderStateMask.Nothing);
			m_IsOpaque = opaque;
			m_ShouldTransparentsReceiveShadows = false;
			if (stencilState.enabled)
			{
				m_RenderStateBlock.stencilReference = stencilReference;
				m_RenderStateBlock.mask = global::UnityEngine.Rendering.RenderStateMask.Stencil;
				m_RenderStateBlock.stencilState = stencilState;
			}
		}

		internal static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.PassData data, global::UnityEngine.Rendering.RendererList rendererList, global::UnityEngine.Rendering.RendererList objectsWithErrorRendererList, bool yFlip)
		{
			global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(0f, 0f, 0f, data.isOpaque ? 1f : 0f);
			cmd.SetGlobalVector(s_DrawObjectPassDataPropID, value);
			if (data.cameraData.xr.enabled && data.isActiveTargetBackBuffer)
			{
				cmd.SetViewport(data.cameraData.xr.GetViewport());
			}
			bool flag = data.screenSpaceIrradianceHdl.IsValid();
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ScreenSpaceIrradiance, flag);
			if (flag)
			{
				cmd.SetGlobalTexture(global::UnityEngine.Rendering.Universal.ShaderPropertyId.screenSpaceIrradiance, data.screenSpaceIrradianceHdl);
			}
			float num = (yFlip ? (-1f) : 1f);
			global::UnityEngine.Vector4 value2 = ((num < 0f) ? new global::UnityEngine.Vector4(num, 1f, -1f, 1f) : new global::UnityEngine.Vector4(num, 0f, 1f, 1f));
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.scaleBiasRt, value2);
			float value3 = ((data.cameraData.cameraTargetDescriptor.msaaSamples > 1 && data.isOpaque) ? 1f : 0f);
			cmd.SetGlobalFloat(global::UnityEngine.Rendering.Universal.ShaderPropertyId.alphaToMaskAvailable, value3);
			if (global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(data.cameraData) != null)
			{
				data.debugRendererLists.DrawWithRendererList(cmd);
			}
			else
			{
				cmd.DrawRendererList(rendererList);
			}
		}

		internal void InitPassData(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, ref global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.PassData passData, uint batchLayerMask, bool isActiveTargetBackBuffer = false)
		{
			passData.cameraData = cameraData;
			passData.isOpaque = m_IsOpaque;
			passData.shouldTransparentsReceiveShadows = m_ShouldTransparentsReceiveShadows;
			passData.batchLayerMask = batchLayerMask;
			passData.isActiveTargetBackBuffer = isActiveTargetBackBuffer;
		}

		internal void InitRendererLists(global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, ref global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.PassData passData, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, bool useRenderGraph, bool zWriteOff)
		{
			_ = cameraData.camera;
			global::UnityEngine.Rendering.SortingCriteria sortingCriteria = (m_IsOpaque ? cameraData.defaultOpaqueSortFlags : global::UnityEngine.Rendering.SortingCriteria.CommonTransparent);
			if (cameraData.renderer.useDepthPriming && m_IsOpaque && (cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base || cameraData.clearDepth))
			{
				sortingCriteria = global::UnityEngine.Rendering.SortingCriteria.SortingLayer | global::UnityEngine.Rendering.SortingCriteria.RenderQueue | global::UnityEngine.Rendering.SortingCriteria.OptimizeStateChanges | global::UnityEngine.Rendering.SortingCriteria.CanvasOrder;
			}
			global::UnityEngine.Rendering.FilteringSettings filteringSettings = m_FilteringSettings;
			filteringSettings.batchLayerMask = passData.batchLayerMask;
			global::UnityEngine.Rendering.DrawingSettings drawingSettings = global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(m_ShaderTagIdList, renderingData, cameraData, lightData, sortingCriteria);
			if (zWriteOff)
			{
				m_RenderStateBlock.depthState = new global::UnityEngine.Rendering.DepthState(writeEnabled: false, global::UnityEngine.Rendering.CompareFunction.Equal);
				m_RenderStateBlock.mask |= global::UnityEngine.Rendering.RenderStateMask.Depth;
			}
			else
			{
				m_RenderStateBlock.depthState = global::UnityEngine.Rendering.DepthState.defaultValue;
				m_RenderStateBlock.mask &= ~global::UnityEngine.Rendering.RenderStateMask.Depth;
			}
			global::UnityEngine.Rendering.Universal.DebugHandler activeDebugHandler = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			if (useRenderGraph)
			{
				if (activeDebugHandler != null)
				{
					passData.debugRendererLists = activeDebugHandler.CreateRendererListsWithDebugRenderState(renderGraph, ref renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref m_RenderStateBlock);
				}
				else
				{
					global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref renderingData.cullResults, drawingSettings, filteringSettings, m_RenderStateBlock, ref passData.rendererListHdl);
				}
			}
			else if (activeDebugHandler != null)
			{
				passData.debugRendererLists = activeDebugHandler.CreateRendererListsWithDebugRenderState(context, ref renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref m_RenderStateBlock);
			}
			else
			{
				global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref renderingData.cullResults, drawingSettings, filteringSettings, m_RenderStateBlock, ref passData.rendererList);
			}
		}

		internal static bool CanDisableZWrite(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool isOpaque)
		{
			if (cameraData.renderer.useDepthPriming && isOpaque)
			{
				if (cameraData.renderType != global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
				{
					return cameraData.clearDepth;
				}
				return true;
			}
			return false;
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorTarget, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTarget, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle mainShadowsTexture, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle additionalShadowsTexture, uint batchLayerMask = uint.MaxValue, bool isMainOpaquePass = false)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			bool flag = CanDisableZWrite(universalCameraData, m_IsOpaque);
			global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DrawObjectsPass.cs", 292);
			rasterRenderGraphBuilder.UseAllGlobalTextures(enable: true);
			InitPassData(universalCameraData, ref passData, batchLayerMask, universalResourceData.isActiveTargetBackBuffer);
			if (colorTarget.IsValid())
			{
				passData.albedoHdl = colorTarget;
				rasterRenderGraphBuilder.SetRenderAttachment(colorTarget, 0);
			}
			if (depthTarget.IsValid())
			{
				global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = (flag ? global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read : global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
				passData.depthHdl = depthTarget;
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(depthTarget, flags);
			}
			if (mainShadowsTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in mainShadowsTexture);
			}
			if (additionalShadowsTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in additionalShadowsTexture);
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ssaoTexture = universalResourceData.ssaoTexture;
			if (ssaoTexture.IsValid())
			{
				rasterRenderGraphBuilder.UseTexture(in ssaoTexture);
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle irradianceTexture = universalResourceData.irradianceTexture;
			if (irradianceTexture.IsValid())
			{
				passData.screenSpaceIrradianceHdl = irradianceTexture;
				rasterRenderGraphBuilder.UseTexture(in irradianceTexture);
			}
			global::UnityEngine.Rendering.Universal.RenderGraphUtils.UseDBufferIfValid(rasterRenderGraphBuilder, universalResourceData);
			InitRendererLists(renderingData, universalCameraData, lightData, ref passData, default(global::UnityEngine.Rendering.ScriptableRenderContext), renderGraph, useRenderGraph: true, flag);
			if (global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetActiveDebugHandler(universalCameraData) != null)
			{
				passData.debugRendererLists.PrepareRendererListForRasterPass(rasterRenderGraphBuilder);
			}
			else
			{
				rasterRenderGraphBuilder.UseRendererList(in passData.rendererListHdl);
				rasterRenderGraphBuilder.UseRendererList(in passData.objectsWithErrorRendererListHdl);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			if (universalCameraData.xr.enabled)
			{
				bool flag2 = universalCameraData.xrUniversal.canFoveateIntermediatePasses || universalResourceData.isActiveTargetBackBuffer;
				rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering && flag2);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.DrawObjectsPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				if (!data.isOpaque && !data.shouldTransparentsReceiveShadows)
				{
					global::UnityEngine.Rendering.Universal.TransparentSettingsPass.ExecutePass(context.cmd);
				}
				bool yFlip = global::UnityEngine.Rendering.Universal.RenderingUtils.IsHandleYFlipped(in context, in data.albedoHdl.IsValid() ? ref data.albedoHdl : ref data.depthHdl);
				bool flag3 = data.screenSpaceIrradianceHdl.IsValid();
				context.cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ScreenSpaceIrradiance, flag3);
				if (flag3)
				{
					context.cmd.SetGlobalTexture(global::UnityEngine.Rendering.Universal.ShaderPropertyId.screenSpaceIrradiance, data.screenSpaceIrradianceHdl);
				}
				ExecutePass(context.cmd, data, data.rendererListHdl, data.objectsWithErrorRendererListHdl, yFlip);
			});
		}
	}
}
