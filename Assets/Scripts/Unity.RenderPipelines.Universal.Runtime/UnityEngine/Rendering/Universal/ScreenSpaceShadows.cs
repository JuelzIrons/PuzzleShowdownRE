namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.Rendering.Universal.SupportedOnRenderer(typeof(global::UnityEngine.Rendering.Universal.UniversalRendererData))]
	[global::UnityEngine.Rendering.Universal.DisallowMultipleRendererFeature("Screen Space Shadows")]
	[global::UnityEngine.Tooltip("Screen Space Shadows")]
	internal class ScreenSpaceShadows : global::UnityEngine.Rendering.Universal.ScriptableRendererFeature
	{
		private class ScreenSpaceShadowsPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
		{
			private class PassData
			{
				internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle target;

				internal global::UnityEngine.Material material;
			}

			private global::UnityEngine.Material m_Material;

			private global::UnityEngine.Rendering.Universal.ScreenSpaceShadowsSettings m_CurrentSettings;

			private int m_ScreenSpaceShadowmapTextureID;

			internal ScreenSpaceShadowsPass()
			{
				base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Blit Screen Space Shadows");
				m_CurrentSettings = new global::UnityEngine.Rendering.Universal.ScreenSpaceShadowsSettings();
				m_ScreenSpaceShadowmapTextureID = global::UnityEngine.Shader.PropertyToID("_ScreenSpaceShadowmapTexture");
			}

			internal bool Setup(global::UnityEngine.Rendering.Universal.ScreenSpaceShadowsSettings featureSettings, global::UnityEngine.Material material)
			{
				m_CurrentSettings = featureSettings;
				m_Material = material;
				ConfigureInput(global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth);
				return m_Material != null;
			}

			private void InitPassData(ref global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData passData)
			{
				passData.material = m_Material;
			}

			public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
			{
				if (m_Material == null)
				{
					global::UnityEngine.Debug.LogErrorFormat("{0}.Execute(): Missing material. ScreenSpaceShadows pass will not execute. Check for missing reference in the renderer resources.", GetType().Name);
					return;
				}
				global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>().cameraTargetDescriptor;
				cameraTargetDescriptor.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
				cameraTargetDescriptor.msaaSamples = 1;
				cameraTargetDescriptor.graphicsFormat = (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend) ? global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm : global::UnityEngine.Experimental.Rendering.GraphicsFormat.B8G8R8A8_UNorm);
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle target = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, cameraTargetDescriptor, "_ScreenSpaceShadowmapTexture", clear: true);
				global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData passData;
				using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\RendererFeatures\\ScreenSpaceShadows.cs", 214);
				passData.target = target;
				unsafeRenderGraphBuilder.UseTexture(in target, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
				InitPassData(ref passData);
				unsafeRenderGraphBuilder.AllowGlobalStateModification(value: true);
				if (target.IsValid())
				{
					unsafeRenderGraphBuilder.SetGlobalTextureAfterPass(in target, m_ScreenSpaceShadowmapTextureID);
				}
				unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext rgContext)
				{
					ExecutePass(rgContext.cmd, data, data.target);
				});
			}

			private static void ExecutePass(global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData data, global::UnityEngine.Rendering.RTHandle target)
			{
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, target, global::UnityEngine.Vector2.one, data.material, 0);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadows, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadowCascades, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadowScreen, value: true);
			}
		}

		private class ScreenSpaceShadowsPostPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
		{
			internal class PassData
			{
				internal global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData;
			}

			internal ScreenSpaceShadowsPostPass()
			{
				base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Set Screen Space Shadow Keywords");
			}

			private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
			{
				int mainLightShadowCascadesCount = shadowData.mainLightShadowCascadesCount;
				bool supportsMainLightShadows = shadowData.supportsMainLightShadows;
				bool value = supportsMainLightShadows && mainLightShadowCascadesCount == 1;
				bool value2 = supportsMainLightShadows && mainLightShadowCascadesCount > 1;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadowScreen, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadows, value);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadowCascades, value2);
			}

			public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
			{
				global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPostPass.PassData passData;
				using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPostPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\RendererFeatures\\ScreenSpaceShadows.cs", 329);
				global::UnityEngine.Rendering.RenderGraphModule.TextureHandle activeColorTexture = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>().activeColorTexture;
				rasterRenderGraphBuilder.SetRenderAttachment(activeColorTexture, 0);
				passData.shadowData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>();
				rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
				rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPostPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext rgContext)
				{
					ExecutePass(rgContext.cmd, data.shadowData);
				});
			}
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Shader m_Shader;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ScreenSpaceShadowsSettings m_Settings = new global::UnityEngine.Rendering.Universal.ScreenSpaceShadowsSettings();

		private global::UnityEngine.Material m_Material;

		private global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPass m_SSShadowsPass;

		private global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPostPass m_SSShadowsPostPass;

		private const string k_ShaderName = "Hidden/Universal Render Pipeline/ScreenSpaceShadows";

		public override void Create()
		{
			if (m_SSShadowsPass == null)
			{
				m_SSShadowsPass = new global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPass();
			}
			if (m_SSShadowsPostPass == null)
			{
				m_SSShadowsPostPass = new global::UnityEngine.Rendering.Universal.ScreenSpaceShadows.ScreenSpaceShadowsPostPass();
			}
			LoadMaterial();
			m_SSShadowsPass.renderPassEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingGbuffer;
			m_SSShadowsPostPass.renderPassEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingTransparents;
		}

		public override void AddRenderPasses(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
			if (!global::UnityEngine.Rendering.Universal.UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData))
			{
				if (!LoadMaterial())
				{
					global::UnityEngine.Debug.LogErrorFormat("{0}.AddRenderPasses(): Missing material. {1} render pass will not be added. Check for missing reference in the renderer resources.", GetType().Name, base.name);
				}
				else if (renderingData.shadowData.supportsMainLightShadows && renderingData.lightData.mainLightIndex != -1 && m_SSShadowsPass.Setup(m_Settings, m_Material))
				{
					bool flag = renderer is global::UnityEngine.Rendering.Universal.UniversalRenderer universalRenderer && universalRenderer.usesDeferredLighting;
					m_SSShadowsPass.renderPassEvent = (flag ? global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingGbuffer : ((global::UnityEngine.Rendering.Universal.RenderPassEvent)201));
					renderer.EnqueuePass(m_SSShadowsPass);
					renderer.EnqueuePass(m_SSShadowsPostPass);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			m_SSShadowsPass = null;
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_Material);
		}

		private bool LoadMaterial()
		{
			if (m_Material != null)
			{
				return true;
			}
			if (m_Shader == null)
			{
				m_Shader = global::UnityEngine.Shader.Find("Hidden/Universal Render Pipeline/ScreenSpaceShadows");
				if (m_Shader == null)
				{
					return false;
				}
			}
			m_Material = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(m_Shader);
			return m_Material != null;
		}
	}
}
