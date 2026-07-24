namespace UnityEngine.Rendering.Universal.Internal
{
	public class DepthNormalOnlyPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal bool enableRenderingLayers;

			internal global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList;
		}

		private global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private static readonly global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> k_DepthNormals = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>
		{
			new global::UnityEngine.Rendering.ShaderTagId("DepthNormals"),
			new global::UnityEngine.Rendering.ShaderTagId("DepthNormalsOnly")
		};

		private static readonly global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> k_DepthNormalsOnly = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>
		{
			new global::UnityEngine.Rendering.ShaderTagId("DepthNormalsOnly")
		};

		internal static readonly string k_CameraNormalsTextureName = "_CameraNormalsTexture";

		private static readonly int s_CameraDepthTextureID = global::UnityEngine.Shader.PropertyToID("_CameraDepthTexture");

		private static readonly int s_CameraNormalsTextureID = global::UnityEngine.Shader.PropertyToID(k_CameraNormalsTextureName);

		private static readonly int s_CameraRenderingLayersTextureID = global::UnityEngine.Shader.PropertyToID("_CameraRenderingLayersTexture");

		internal global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> shaderTagIds { get; set; }

		internal bool enableRenderingLayers { get; set; }

		internal global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize renderingLayersMaskSize { get; set; }

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void OnCameraSetup(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public DepthNormalOnlyPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.RenderQueueRange renderQueueRange, global::UnityEngine.LayerMask layerMask)
		{
			base.profilingSampler = global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.DrawDepthNormalPrepass);
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(renderQueueRange, layerMask);
			base.renderPassEvent = evt;
			shaderTagIds = k_DepthNormals;
		}

		public static global::UnityEngine.Experimental.Rendering.GraphicsFormat GetGraphicsFormat()
		{
			if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SNorm, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render))
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SNorm;
			}
			if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render))
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat;
			}
			return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32G32B32A32_SFloat;
		}

		public void Setup(global::UnityEngine.Rendering.RTHandle depthHandle, global::UnityEngine.Rendering.RTHandle normalHandle)
		{
			enableRenderingLayers = false;
		}

		public void Setup(global::UnityEngine.Rendering.RTHandle depthHandle, global::UnityEngine.Rendering.RTHandle normalHandle, global::UnityEngine.Rendering.RTHandle decalLayerHandle)
		{
			Setup(depthHandle, normalHandle);
			enableRenderingLayers = true;
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Internal.DepthNormalOnlyPass.PassData passData, global::UnityEngine.Rendering.RendererList rendererList)
		{
			if (passData.enableRenderingLayers)
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.WriteRenderingLayers, value: true);
			}
			cmd.DrawRendererList(rendererList);
			if (passData.enableRenderingLayers)
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.WriteRenderingLayers, value: false);
			}
		}

		public override void OnCameraCleanup(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new global::System.ArgumentNullException("cmd");
			}
			shaderTagIds = k_DepthNormals;
		}

		private global::UnityEngine.Rendering.RendererListParams InitRendererListParams(global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			global::UnityEngine.Rendering.SortingCriteria defaultOpaqueSortFlags = cameraData.defaultOpaqueSortFlags;
			global::UnityEngine.Rendering.DrawingSettings drawSettings = global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(shaderTagIds, renderingData, cameraData, lightData, defaultOpaqueSortFlags);
			drawSettings.perObjectData = global::UnityEngine.Rendering.PerObjectData.None;
			return new global::UnityEngine.Rendering.RendererListParams(renderingData.cullResults, drawSettings, m_FilteringSettings);
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraNormalsTexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTexture, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle renderingLayersTexture, uint batchLayerMask, bool setGlobalDepth, bool setGlobalNormalAndRenderingLayers, bool allowPartialPass)
		{
			if (allowPartialPass)
			{
				shaderTagIds = k_DepthNormalsOnly;
			}
			else
			{
				shaderTagIds = k_DepthNormals;
			}
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.Internal.DepthNormalOnlyPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.DepthNormalOnlyPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\DepthNormalOnlyPass.cs", 222);
			rasterRenderGraphBuilder.SetRenderAttachment(cameraNormalsTexture, 0);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(depthTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.enableRenderingLayers = enableRenderingLayers;
			if (passData.enableRenderingLayers)
			{
				rasterRenderGraphBuilder.SetRenderAttachment(renderingLayersTexture, 1);
				passData.maskSize = renderingLayersMaskSize;
			}
			global::UnityEngine.Rendering.RendererListParams desc = InitRendererListParams(renderingData, universalCameraData, lightData);
			desc.filteringSettings.batchLayerMask = batchLayerMask;
			passData.rendererList = renderGraph.CreateRendererList(in desc);
			rasterRenderGraphBuilder.UseRendererList(in passData.rendererList);
			if (universalCameraData.xr.enabled)
			{
				rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering && universalCameraData.xrUniversal.canFoveateIntermediatePasses);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			if (setGlobalNormalAndRenderingLayers)
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in cameraNormalsTexture, s_CameraNormalsTextureID);
				if (passData.enableRenderingLayers)
				{
					rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in renderingLayersTexture, s_CameraRenderingLayersTextureID);
				}
			}
			if (setGlobalDepth)
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in depthTexture, s_CameraDepthTextureID);
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.DepthNormalOnlyPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.Universal.RenderingLayerUtils.SetupProperties(context.cmd, data.maskSize);
				ExecutePass(context.cmd, data, data.rendererList);
			});
		}
	}
}
