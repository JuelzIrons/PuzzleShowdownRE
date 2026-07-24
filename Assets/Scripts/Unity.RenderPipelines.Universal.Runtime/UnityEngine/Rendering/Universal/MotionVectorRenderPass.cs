namespace UnityEngine.Rendering.Universal
{
	internal sealed class MotionVectorRenderPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Camera camera;

			internal global::UnityEngine.Experimental.Rendering.XRPass xr;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepth;

			internal global::UnityEngine.Material cameraMaterial;

			internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererListHdl;

			internal global::UnityEngine.Rendering.RendererList rendererList;
		}

		public class MotionMatrixPassData
		{
			public global::UnityEngine.Rendering.Universal.MotionVectorsPersistentData motionData;

			public global::UnityEngine.Experimental.Rendering.XRPass xr;
		}

		internal const string k_MotionVectorTextureName = "_MotionVectorTexture";

		internal const string k_MotionVectorDepthTextureName = "_MotionVectorDepthTexture";

		internal const global::UnityEngine.Experimental.Rendering.GraphicsFormat k_TargetFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16_SFloat;

		public const string k_MotionVectorsLightModeTag = "MotionVectors";

		private static readonly string[] s_ShaderTags = new string[1] { "MotionVectors" };

		private static readonly int s_CameraDepthTextureID = global::UnityEngine.Shader.PropertyToID("_CameraDepthTexture");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler s_SetMotionMatrixProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Set Motion Vector Global Matrices");

		private readonly global::UnityEngine.Material m_CameraMaterial;

		private readonly global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		internal MotionVectorRenderPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Material cameraMaterial, global::UnityEngine.LayerMask opaqueLayerMask)
		{
			base.profilingSampler = global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.DrawMotionVectors);
			base.renderPassEvent = evt;
			m_CameraMaterial = cameraMaterial;
			m_FilteringSettings = new global::UnityEngine.Rendering.FilteringSettings(global::UnityEngine.Rendering.RenderQueueRange.opaque, opaqueLayerMask);
			ConfigureInput(global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth);
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.PassData passData, global::UnityEngine.Rendering.RendererList rendererList)
		{
			global::UnityEngine.Material cameraMaterial = passData.cameraMaterial;
			if (!(cameraMaterial == null))
			{
				global::UnityEngine.Camera camera = passData.camera;
				if (camera.cameraType != global::UnityEngine.CameraType.Preview)
				{
					camera.depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth | global::UnityEngine.DepthTextureMode.MotionVectors;
					DrawCameraMotionVectors(cmd, passData.xr, cameraMaterial);
					DrawObjectMotionVectors(cmd, passData.xr, ref rendererList);
				}
			}
		}

		private static global::UnityEngine.Rendering.DrawingSettings GetDrawingSettings(global::UnityEngine.Camera camera, bool supportsDynamicBatching)
		{
			global::UnityEngine.Rendering.SortingSettings sortingSettings = new global::UnityEngine.Rendering.SortingSettings(camera);
			sortingSettings.criteria = global::UnityEngine.Rendering.SortingCriteria.CommonOpaque;
			global::UnityEngine.Rendering.SortingSettings sortingSettings2 = sortingSettings;
			global::UnityEngine.Rendering.DrawingSettings drawingSettings = new global::UnityEngine.Rendering.DrawingSettings(global::UnityEngine.Rendering.ShaderTagId.none, sortingSettings2);
			drawingSettings.perObjectData = global::UnityEngine.Rendering.PerObjectData.MotionVectors;
			drawingSettings.enableDynamicBatching = supportsDynamicBatching;
			drawingSettings.enableInstancing = true;
			drawingSettings.lodCrossFadeStencilMask = 0;
			global::UnityEngine.Rendering.DrawingSettings result = drawingSettings;
			for (int i = 0; i < s_ShaderTags.Length; i++)
			{
				result.SetShaderPassName(i, new global::UnityEngine.Rendering.ShaderTagId(s_ShaderTags[i]));
			}
			return result;
		}

		private static void DrawCameraMotionVectors(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Experimental.Rendering.XRPass xr, global::UnityEngine.Material cameraMaterial)
		{
			bool supportsFoveatedRendering = xr.supportsFoveatedRendering;
			bool flag = supportsFoveatedRendering && global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps.HasFlag(global::UnityEngine.Rendering.FoveatedRenderingCaps.NonUniformRaster);
			if (supportsFoveatedRendering)
			{
				if (flag)
				{
					cmd.SetFoveatedRenderingMode(global::UnityEngine.Rendering.FoveatedRenderingMode.Disabled);
				}
				else
				{
					cmd.SetFoveatedRenderingMode(global::UnityEngine.Rendering.FoveatedRenderingMode.Enabled);
				}
			}
			cmd.DrawProcedural(global::UnityEngine.Matrix4x4.identity, cameraMaterial, 0, global::UnityEngine.MeshTopology.Triangles, 3, 1);
			if (supportsFoveatedRendering && !flag)
			{
				cmd.SetFoveatedRenderingMode(global::UnityEngine.Rendering.FoveatedRenderingMode.Disabled);
			}
		}

		private static void DrawObjectMotionVectors(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Experimental.Rendering.XRPass xr, ref global::UnityEngine.Rendering.RendererList rendererList)
		{
			bool supportsFoveatedRendering = xr.supportsFoveatedRendering;
			if (supportsFoveatedRendering)
			{
				cmd.SetFoveatedRenderingMode(global::UnityEngine.Rendering.FoveatedRenderingMode.Enabled);
			}
			cmd.DrawRendererList(rendererList);
			if (supportsFoveatedRendering)
			{
				cmd.SetFoveatedRenderingMode(global::UnityEngine.Rendering.FoveatedRenderingMode.Disabled);
			}
		}

		private void InitPassData(ref global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.PassData passData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			passData.camera = cameraData.camera;
			passData.xr = cameraData.xr;
			passData.cameraMaterial = m_CameraMaterial;
		}

		private void InitRendererLists(ref global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.PassData passData, ref global::UnityEngine.Rendering.CullingResults cullResults, bool supportsDynamicBatching, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, bool useRenderGraph)
		{
			global::UnityEngine.Rendering.DrawingSettings drawingSettings = GetDrawingSettings(passData.camera, supportsDynamicBatching);
			global::UnityEngine.Rendering.RenderStateBlock rsb = new global::UnityEngine.Rendering.RenderStateBlock(global::UnityEngine.Rendering.RenderStateMask.Nothing);
			if (useRenderGraph)
			{
				global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref cullResults, drawingSettings, m_FilteringSettings, rsb, ref passData.rendererListHdl);
			}
			else
			{
				global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref cullResults, drawingSettings, m_FilteringSettings, rsb, ref passData.rendererList);
			}
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepthTexture, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle motionVectorColor, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle motionVectorDepth)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\MotionVectorRenderPass.cs", 217);
			rasterRenderGraphBuilder.UseAllGlobalTextures(enable: true);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			if (universalCameraData.xr.enabled)
			{
				rasterRenderGraphBuilder.EnableFoveatedRasterization(universalCameraData.xr.supportsFoveatedRendering && universalCameraData.xrUniversal.canFoveateIntermediatePasses);
				rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			}
			rasterRenderGraphBuilder.SetRenderAttachment(motionVectorColor, 0);
			rasterRenderGraphBuilder.SetRenderAttachmentDepth(motionVectorDepth);
			InitPassData(ref passData, universalCameraData);
			passData.cameraDepth = cameraDepthTexture;
			rasterRenderGraphBuilder.UseTexture(in cameraDepthTexture);
			InitRendererLists(ref passData, ref universalRenderingData.cullResults, universalRenderingData.supportsDynamicBatching, default(global::UnityEngine.Rendering.ScriptableRenderContext), renderGraph, useRenderGraph: true);
			rasterRenderGraphBuilder.UseRendererList(in passData.rendererListHdl);
			if (motionVectorColor.IsValid())
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in motionVectorColor, global::UnityEngine.Shader.PropertyToID("_MotionVectorTexture"));
			}
			if (motionVectorDepth.IsValid())
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in motionVectorDepth, global::UnityEngine.Shader.PropertyToID("_MotionVectorDepthTexture"));
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				if (data.cameraMaterial != null)
				{
					data.cameraMaterial.SetTexture(s_CameraDepthTextureID, data.cameraDepth);
				}
				ExecutePass(context.cmd, data, data.rendererListHdl);
			});
		}

		internal static void SetRenderGraphMotionVectorGlobalMatrices(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			if (!cameraData.camera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component))
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.MotionMatrixPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.MotionMatrixPassData>(s_SetMotionMatrixProfilingSampler.name, out passData, s_SetMotionMatrixProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\MotionVectorRenderPass.cs", 275);
			passData.motionData = component.motionVectorsPersistentData;
			passData.xr = cameraData.xr;
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.MotionVectorRenderPass.MotionMatrixPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				data.motionData.SetGlobalMotionMatrices(context.cmd, data.xr);
			});
		}
	}
}
