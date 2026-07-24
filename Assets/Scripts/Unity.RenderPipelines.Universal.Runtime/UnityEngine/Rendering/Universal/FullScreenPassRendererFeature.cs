namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("")]
	public class FullScreenPassRendererFeature : global::UnityEngine.Rendering.Universal.ScriptableRendererFeature, global::UnityEngine.ISerializationCallbackReceiver
	{
		public enum InjectionPoint
		{
			BeforeRenderingTransparents = 450,
			BeforeRenderingPostProcessing = 550,
			AfterRenderingPostProcessing = 600
		}

		internal class FullScreenRenderPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
		{
			private class CopyPassData
			{
				internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputTexture;
			}

			private class MainPassData
			{
				internal global::UnityEngine.Material material;

				internal int passIndex;

				internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle inputTexture;
			}

			private global::UnityEngine.Material m_Material;

			private int m_PassIndex;

			private bool m_FetchActiveColor;

			private bool m_BindDepthStencilAttachment;

			private static global::UnityEngine.MaterialPropertyBlock s_SharedPropertyBlock = new global::UnityEngine.MaterialPropertyBlock();

			public FullScreenRenderPass(string passName)
			{
				base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(passName);
			}

			public void SetupMembers(global::UnityEngine.Material material, int passIndex, bool fetchActiveColor, bool bindDepthStencilAttachment)
			{
				m_Material = material;
				m_PassIndex = passIndex;
				m_FetchActiveColor = fetchActiveColor;
				m_BindDepthStencilAttachment = bindDepthStencilAttachment;
			}

			internal void ReAllocate(global::UnityEngine.RenderTextureDescriptor desc)
			{
			}

			private static void ExecuteCopyColorPass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle sourceTexture)
			{
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, sourceTexture, new global::UnityEngine.Vector4(1f, 1f, 0f, 0f), 0f, bilinear: false);
			}

			private static void ExecuteMainPass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle sourceTexture, global::UnityEngine.Material material, int passIndex)
			{
				s_SharedPropertyBlock.Clear();
				if (sourceTexture != null)
				{
					s_SharedPropertyBlock.SetTexture(global::UnityEngine.Rendering.Universal.ShaderPropertyId.blitTexture, sourceTexture);
				}
				s_SharedPropertyBlock.SetVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.blitScaleBias, new global::UnityEngine.Vector4(1f, 1f, 0f, 0f));
				cmd.DrawProcedural(global::UnityEngine.Matrix4x4.identity, material, passIndex, global::UnityEngine.MeshTopology.Triangles, 3, 1, s_SharedPropertyBlock);
			}

			public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
			{
				global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
				global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle activeColorTexture;
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle;
				if (m_FetchActiveColor)
				{
					global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = renderGraph.GetTextureDesc(universalResourceData.cameraColor);
					desc.name = "_CameraColorFullScreenPass";
					desc.clearBuffer = false;
					activeColorTexture = universalResourceData.activeColorTexture;
					textureHandle = renderGraph.CreateTexture(in desc);
					global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.AddBlitPass(renderGraph, activeColorTexture, textureHandle, global::UnityEngine.Vector2.one, global::UnityEngine.Vector2.zero, 0, 0, -1, 0, 0, 1, global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitFilterMode.ClampBilinear, "Copy Color Full Screen", returnBuilder: false, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\RendererFeatures\\FullScreenPassRendererFeature.cs", 238);
					activeColorTexture = textureHandle;
				}
				else
				{
					activeColorTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
				}
				textureHandle = universalResourceData.activeColorTexture;
				if (base.input != global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.None || m_BindDepthStencilAttachment)
				{
					AddFullscreenRenderPassInputPass(renderGraph, universalResourceData, cameraData, activeColorTexture, textureHandle);
					return;
				}
				global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitMaterialParameters blitParameters = new global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.BlitMaterialParameters(activeColorTexture, textureHandle, m_Material, m_PassIndex);
				global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.AddBlitPass(renderGraph, blitParameters, "Blit Color Full Screen", returnBuilder: false, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\RendererFeatures\\FullScreenPassRendererFeature.cs", 261);
			}

			private void AddFullscreenRenderPassInputPass(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourcesData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
			{
				global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.FullScreenRenderPass.MainPassData passData;
				using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.FullScreenRenderPass.MainPassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\RendererFeatures\\FullScreenPassRendererFeature.cs", 267);
				passData.material = m_Material;
				passData.passIndex = m_PassIndex;
				passData.inputTexture = source;
				if (passData.inputTexture.IsValid())
				{
					rasterRenderGraphBuilder.UseTexture(in passData.inputTexture);
				}
				bool num = (base.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Color) != 0;
				bool flag = (base.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth) != 0;
				bool flag2 = (base.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Motion) != 0;
				bool flag3 = (base.input & global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Normal) != 0;
				if (num && cameraData.renderer.SupportsCameraOpaque())
				{
					rasterRenderGraphBuilder.UseTexture(resourcesData.cameraOpaqueTexture);
				}
				if (flag)
				{
					rasterRenderGraphBuilder.UseTexture(resourcesData.cameraDepthTexture);
				}
				if (flag2 && cameraData.renderer.SupportsMotionVectors())
				{
					rasterRenderGraphBuilder.UseTexture(resourcesData.motionVectorColor);
					rasterRenderGraphBuilder.UseTexture(resourcesData.motionVectorDepth);
				}
				if (flag3 && cameraData.renderer.SupportsCameraNormals())
				{
					rasterRenderGraphBuilder.UseTexture(resourcesData.cameraNormalsTexture);
				}
				rasterRenderGraphBuilder.SetRenderAttachment(destination, 0);
				if (m_BindDepthStencilAttachment)
				{
					rasterRenderGraphBuilder.SetRenderAttachmentDepth(resourcesData.activeDepthTexture);
				}
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.FullScreenRenderPass.MainPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
				{
					ExecuteMainPass(rgContext.cmd, data.inputTexture, data.material, data.passIndex);
				});
			}

			private void AddCopyPassRenderPassFullscreen(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
			{
				global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.FullScreenRenderPass.CopyPassData passData;
				using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.FullScreenRenderPass.CopyPassData>("Copy Color Full Screen", out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\RendererFeatures\\FullScreenPassRendererFeature.cs", 327);
				passData.inputTexture = source;
				rasterRenderGraphBuilder.UseTexture(in passData.inputTexture);
				rasterRenderGraphBuilder.SetRenderAttachment(destination, 0);
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.FullScreenRenderPass.CopyPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
				{
					ExecuteCopyColorPass(rgContext.cmd, data.inputTexture);
				});
			}
		}

		private enum Version
		{
			Uninitialised = -1,
			Initial = 0,
			AddFetchColorBufferCheckbox = 1,
			Count = 2,
			Latest = 1
		}

		public global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.InjectionPoint injectionPoint = global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.InjectionPoint.AfterRenderingPostProcessing;

		public bool fetchColorBuffer = true;

		public global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput requirements;

		public global::UnityEngine.Material passMaterial;

		public int passIndex;

		public bool bindDepthStencilAttachment;

		private global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.FullScreenRenderPass m_FullScreenPass;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.Version m_Version = global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.Version.Uninitialised;

		public override void Create()
		{
			m_FullScreenPass = new global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.FullScreenRenderPass(base.name);
		}

		internal override bool RequireRenderingLayers(bool isDeferred, bool needsGBufferAccurateNormals, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event atEvent, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize)
		{
			atEvent = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.Opaque;
			maskSize = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits8;
			return false;
		}

		public override void AddRenderPasses(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
			if (renderingData.cameraData.cameraType != global::UnityEngine.CameraType.Preview && renderingData.cameraData.cameraType != global::UnityEngine.CameraType.Reflection && !global::UnityEngine.Rendering.Universal.UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData) && !(passMaterial == null))
			{
				if (passIndex < 0 || passIndex >= passMaterial.passCount)
				{
					global::UnityEngine.Debug.LogWarningFormat("The full screen feature \"{0}\" will not execute - the pass index is out of bounds for the material.", base.name);
					return;
				}
				m_FullScreenPass.renderPassEvent = (global::UnityEngine.Rendering.Universal.RenderPassEvent)injectionPoint;
				m_FullScreenPass.ConfigureInput(requirements);
				m_FullScreenPass.SetupMembers(passMaterial, passIndex, fetchColorBuffer, bindDepthStencilAttachment);
				m_FullScreenPass.requiresIntermediateTexture = fetchColorBuffer;
				renderer.EnqueuePass(m_FullScreenPass);
			}
		}

		private void UpgradeIfNeeded()
		{
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (m_Version == global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.Version.Uninitialised)
			{
				m_Version = global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.Version.AddFetchColorBufferCheckbox;
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (m_Version == global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.Version.Uninitialised)
			{
				m_Version = global::UnityEngine.Rendering.Universal.FullScreenPassRendererFeature.Version.Initial;
			}
			UpgradeIfNeeded();
		}
	}
}
