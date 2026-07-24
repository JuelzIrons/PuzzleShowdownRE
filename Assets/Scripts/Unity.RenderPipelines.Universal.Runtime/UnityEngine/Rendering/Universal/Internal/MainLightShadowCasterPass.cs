namespace UnityEngine.Rendering.Universal.Internal
{
	public class MainLightShadowCasterPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private static class MainLightShadowConstantBuffer
		{
			public static readonly int _WorldToShadow = global::UnityEngine.Shader.PropertyToID("_MainLightWorldToShadow");

			public static readonly int _ShadowParams = global::UnityEngine.Shader.PropertyToID("_MainLightShadowParams");

			public static readonly int _CascadeShadowSplitSpheres0 = global::UnityEngine.Shader.PropertyToID("_CascadeShadowSplitSpheres0");

			public static readonly int _CascadeShadowSplitSpheres1 = global::UnityEngine.Shader.PropertyToID("_CascadeShadowSplitSpheres1");

			public static readonly int _CascadeShadowSplitSpheres2 = global::UnityEngine.Shader.PropertyToID("_CascadeShadowSplitSpheres2");

			public static readonly int _CascadeShadowSplitSpheres3 = global::UnityEngine.Shader.PropertyToID("_CascadeShadowSplitSpheres3");

			public static readonly int _CascadeShadowSplitSphereRadii = global::UnityEngine.Shader.PropertyToID("_CascadeShadowSplitSphereRadii");

			public static readonly int _ShadowOffset0 = global::UnityEngine.Shader.PropertyToID("_MainLightShadowOffset0");

			public static readonly int _ShadowOffset1 = global::UnityEngine.Shader.PropertyToID("_MainLightShadowOffset1");

			public static readonly int _ShadowmapSize = global::UnityEngine.Shader.PropertyToID("_MainLightShadowmapSize");

			public static readonly int _MainLightShadowmapID = global::UnityEngine.Shader.PropertyToID("_MainLightShadowmapTexture");
		}

		private class PassData
		{
			internal bool emptyShadowmap;

			internal bool setKeywordForEmptyShadowmap;

			internal global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.Universal.UniversalLightData lightData;

			internal global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData;

			internal global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass pass;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle shadowmapTexture;

			internal readonly global::UnityEngine.Rendering.RendererList[] shadowRendererLists = new global::UnityEngine.Rendering.RendererList[4];

			internal readonly global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle[] shadowRendererListsHandle = new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle[4];
		}

		internal global::UnityEngine.Rendering.RTHandle m_MainLightShadowmapTexture;

		private int m_RenderTargetWidth;

		private int m_RenderTargetHeight;

		private int m_ShadowCasterCascadesCount;

		private bool m_CreateEmptyShadowmap;

		private bool m_SetKeywordForEmptyShadowmap;

		private float m_CascadeBorder;

		private float m_MaxShadowDistanceSq;

		private global::UnityEngine.RenderTextureDescriptor m_MainLightShadowDescriptor;

		private readonly global::UnityEngine.Vector4[] m_CascadeSplitDistances;

		private readonly global::UnityEngine.Matrix4x4[] m_MainLightShadowMatrices;

		private readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSetupSampler = new global::UnityEngine.Rendering.ProfilingSampler("Setup Main Shadowmap");

		private readonly global::UnityEngine.Rendering.Universal.ShadowSliceData[] m_CascadeSlices;

		private const int k_EmptyShadowMapDimensions = 1;

		private const int k_MaxCascades = 4;

		private const int k_ShadowmapBufferBits = 16;

		private const string k_MainLightShadowMapTextureName = "_MainLightShadowmapTexture";

		private static global::UnityEngine.Vector4 s_EmptyShadowParams = new global::UnityEngine.Vector4(0f, 0f, 1f, 0f);

		private static readonly global::UnityEngine.Vector4 s_EmptyShadowmapSize = new global::UnityEngine.Vector4(1f, 1f, 1f, 1f);

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Configure(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public MainLightShadowCasterPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw Main Light Shadowmap");
			base.renderPassEvent = evt;
			m_MainLightShadowMatrices = new global::UnityEngine.Matrix4x4[5];
			m_CascadeSlices = new global::UnityEngine.Rendering.Universal.ShadowSliceData[4];
			m_CascadeSplitDistances = new global::UnityEngine.Vector4[4];
		}

		public void Dispose()
		{
			m_MainLightShadowmapTexture?.Release();
		}

		public bool Setup(ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
			global::UnityEngine.Rendering.ContextContainer frameData = renderingData.frameData;
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData2 = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>();
			return Setup(renderingData2, cameraData, lightData, shadowData);
		}

		public bool Setup(global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			bool mainLightShadowsEnabled = shadowData.mainLightShadowsEnabled;
			bool supportsMainLightShadows = shadowData.supportsMainLightShadows;
			using (new global::UnityEngine.Rendering.ProfilingScope(m_ProfilingSetupSampler))
			{
				bool stripShadowsOffVariants = cameraData.renderer.stripShadowsOffVariants;
				Clear();
				int mainLightIndex = lightData.mainLightIndex;
				if (mainLightIndex == -1 || (cameraData.camera.targetTexture != null && cameraData.camera.targetTexture.format == global::UnityEngine.RenderTextureFormat.Depth))
				{
					if (mainLightShadowsEnabled)
					{
						return SetupForEmptyRendering(stripShadowsOffVariants, mainLightShadowsEnabled, null, cameraData, shadowData);
					}
					return false;
				}
				global::UnityEngine.Rendering.VisibleLight visibleLight = lightData.visibleLights[mainLightIndex];
				global::UnityEngine.Light light = visibleLight.light;
				if (supportsMainLightShadows && light.shadows == global::UnityEngine.LightShadows.None)
				{
					return SetupForEmptyRendering(stripShadowsOffVariants, mainLightShadowsEnabled, light, cameraData, shadowData);
				}
				if (!mainLightShadowsEnabled)
				{
					if (light.shadows != global::UnityEngine.LightShadows.None && light.bakingOutput.isBaked && light.bakingOutput.mixedLightingMode != global::UnityEngine.MixedLightingMode.IndirectOnly && light.bakingOutput.lightmapBakeType == global::UnityEngine.LightmapBakeType.Mixed)
					{
						return SetupForEmptyRendering(stripShadowsOffVariants, mainLightShadowsEnabled, light, cameraData, shadowData);
					}
					return false;
				}
				if (!supportsMainLightShadows)
				{
					return SetupForEmptyRendering(stripShadowsOffVariants, mainLightShadowsEnabled, null, cameraData, shadowData);
				}
				if (visibleLight.lightType != global::UnityEngine.LightType.Directional)
				{
					global::UnityEngine.Debug.LogWarning("Only directional lights are supported as main light.");
				}
				if (!renderingData.cullResults.GetShadowCasterBounds(mainLightIndex, out var _))
				{
					return SetupForEmptyRendering(stripShadowsOffVariants, mainLightShadowsEnabled, light, cameraData, shadowData);
				}
				m_ShadowCasterCascadesCount = shadowData.mainLightShadowCascadesCount;
				m_RenderTargetWidth = shadowData.mainLightRenderTargetWidth;
				m_RenderTargetHeight = shadowData.mainLightRenderTargetHeight;
				ref global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos reference = ref shadowData.visibleLightsShadowCullingInfos.UnsafeElementAt(mainLightIndex);
				for (int i = 0; i < m_ShadowCasterCascadesCount; i++)
				{
					ref global::UnityEngine.Rendering.Universal.ShadowSliceData reference2 = ref reference.slices.UnsafeElementAt(i);
					global::UnityEngine.Vector4[] cascadeSplitDistances = m_CascadeSplitDistances;
					int num = i;
					global::UnityEngine.Rendering.ShadowSplitData splitData = reference2.splitData;
					cascadeSplitDistances[num] = splitData.cullingSphere;
					m_CascadeSlices[i] = reference2;
					if (!reference.IsSliceValid(i))
					{
						return SetupForEmptyRendering(stripShadowsOffVariants, mainLightShadowsEnabled, light, cameraData, shadowData);
					}
				}
				UpdateTextureDescriptorIfNeeded();
				m_MaxShadowDistanceSq = cameraData.maxShadowDistance * cameraData.maxShadowDistance;
				m_CascadeBorder = shadowData.mainLightShadowCascadeBorder;
				m_CreateEmptyShadowmap = false;
				return true;
			}
		}

		private void UpdateTextureDescriptorIfNeeded()
		{
			if (m_MainLightShadowDescriptor.width != m_RenderTargetWidth || m_MainLightShadowDescriptor.height != m_RenderTargetHeight || m_MainLightShadowDescriptor.depthBufferBits != 16 || m_MainLightShadowDescriptor.colorFormat != global::UnityEngine.RenderTextureFormat.Shadowmap)
			{
				m_MainLightShadowDescriptor = new global::UnityEngine.RenderTextureDescriptor(m_RenderTargetWidth, m_RenderTargetHeight, global::UnityEngine.RenderTextureFormat.Shadowmap, 16);
			}
		}

		private bool SetupForEmptyRendering(bool stripShadowsOffVariants, bool shadowsEnabled, global::UnityEngine.Light light, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			if (!stripShadowsOffVariants)
			{
				return false;
			}
			m_CreateEmptyShadowmap = true;
			m_SetKeywordForEmptyShadowmap = shadowsEnabled;
			if (light == null)
			{
				s_EmptyShadowParams = new global::UnityEngine.Vector4(0f, 0f, 1f, 0f);
			}
			else
			{
				bool supportsSoftShadows = shadowData.supportsSoftShadows;
				float maxShadowDistance = cameraData.maxShadowDistance;
				float mainLightShadowCascadeBorder = shadowData.mainLightShadowCascadeBorder;
				bool softShadowsEnabled = light.shadows == global::UnityEngine.LightShadows.Soft && supportsSoftShadows;
				float y = global::UnityEngine.Rendering.Universal.ShadowUtils.SoftShadowQualityToShaderProperty(light, softShadowsEnabled);
				global::UnityEngine.Rendering.Universal.ShadowUtils.GetScaleAndBiasForLinearDistanceFade(maxShadowDistance, mainLightShadowCascadeBorder, out var scale, out var bias);
				s_EmptyShadowParams = new global::UnityEngine.Vector4(light.shadowStrength, y, scale, bias);
			}
			return true;
		}

		private void Clear()
		{
			for (int i = 0; i < m_MainLightShadowMatrices.Length; i++)
			{
				m_MainLightShadowMatrices[i] = global::UnityEngine.Matrix4x4.identity;
			}
			for (int j = 0; j < m_CascadeSplitDistances.Length; j++)
			{
				m_CascadeSplitDistances[j] = new global::UnityEngine.Vector4(0f, 0f, 0f, 0f);
			}
			for (int k = 0; k < m_CascadeSlices.Length; k++)
			{
				m_CascadeSlices[k].Clear();
			}
		}

		internal static void SetShadowParamsForEmptyShadowmap(global::UnityEngine.Rendering.RasterCommandBuffer rasterCommandBuffer)
		{
			rasterCommandBuffer.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowmapSize, s_EmptyShadowmapSize);
			rasterCommandBuffer.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowParams, s_EmptyShadowParams);
		}

		private void RenderMainLightCascadeShadowmap(global::UnityEngine.Rendering.RasterCommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.PassData data, bool isRenderGraph)
		{
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = data.lightData;
			int mainLightIndex = lightData.mainLightIndex;
			if (mainLightIndex == -1)
			{
				return;
			}
			global::UnityEngine.Rendering.VisibleLight shadowLight = lightData.visibleLights[mainLightIndex];
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.MainLightShadow)))
			{
				global::UnityEngine.Rendering.Universal.ShadowUtils.SetCameraPosition(cmd, data.cameraData.worldSpaceCameraPos);
				if (!isRenderGraph)
				{
					global::UnityEngine.Rendering.Universal.ShadowUtils.SetWorldToCameraAndCameraToWorldMatrices(cmd, data.cameraData.GetViewMatrix());
				}
				for (int i = 0; i < m_ShadowCasterCascadesCount; i++)
				{
					global::UnityEngine.Vector4 shadowBias = global::UnityEngine.Rendering.Universal.ShadowUtils.GetShadowBias(ref shadowLight, mainLightIndex, data.shadowData, m_CascadeSlices[i].projectionMatrix, m_CascadeSlices[i].resolution);
					global::UnityEngine.Rendering.Universal.ShadowUtils.SetupShadowCasterConstantBuffer(cmd, ref shadowLight, shadowBias);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.CastingPunctualLightShadow, value: false);
					global::UnityEngine.Rendering.RendererList shadowRendererList = (isRenderGraph ? ((global::UnityEngine.Rendering.RendererList)data.shadowRendererListsHandle[i]) : data.shadowRendererLists[i]);
					global::UnityEngine.Rendering.Universal.ShadowUtils.RenderShadowSlice(cmd, ref m_CascadeSlices[i], ref shadowRendererList, m_CascadeSlices[i].projectionMatrix, m_CascadeSlices[i].viewMatrix);
				}
				data.shadowData.isKeywordSoftShadowsEnabled = shadowLight.light.shadows == global::UnityEngine.LightShadows.Soft && data.shadowData.supportsSoftShadows;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadows, data.shadowData.mainLightShadowCascadesCount == 1);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadowCascades, data.shadowData.mainLightShadowCascadesCount > 1);
				global::UnityEngine.Rendering.Universal.ShadowUtils.SetSoftShadowQualityShaderKeywords(cmd, data.shadowData);
				SetupMainLightShadowReceiverConstants(cmd, ref shadowLight, data.shadowData);
			}
		}

		private void SetupMainLightShadowReceiverConstants(global::UnityEngine.Rendering.RasterCommandBuffer cmd, ref global::UnityEngine.Rendering.VisibleLight shadowLight, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			global::UnityEngine.Light light = shadowLight.light;
			bool softShadowsEnabled = shadowLight.light.shadows == global::UnityEngine.LightShadows.Soft && shadowData.supportsSoftShadows;
			int shadowCasterCascadesCount = m_ShadowCasterCascadesCount;
			for (int i = 0; i < shadowCasterCascadesCount; i++)
			{
				m_MainLightShadowMatrices[i] = m_CascadeSlices[i].shadowTransform;
			}
			global::UnityEngine.Matrix4x4 zero = global::UnityEngine.Matrix4x4.zero;
			zero.m22 = (global::UnityEngine.SystemInfo.usesReversedZBuffer ? 1f : 0f);
			for (int j = shadowCasterCascadesCount; j <= 4; j++)
			{
				m_MainLightShadowMatrices[j] = zero;
			}
			float num = 1f / (float)m_RenderTargetWidth;
			float num2 = 1f / (float)m_RenderTargetHeight;
			float num3 = 0.5f * num;
			float num4 = 0.5f * num2;
			float y = global::UnityEngine.Rendering.Universal.ShadowUtils.SoftShadowQualityToShaderProperty(light, softShadowsEnabled);
			global::UnityEngine.Rendering.Universal.ShadowUtils.GetScaleAndBiasForLinearDistanceFade(m_MaxShadowDistanceSq, m_CascadeBorder, out var scale, out var bias);
			cmd.SetGlobalMatrixArray(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._WorldToShadow, m_MainLightShadowMatrices);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowParams, new global::UnityEngine.Vector4(light.shadowStrength, y, scale, bias));
			if (m_ShadowCasterCascadesCount > 1)
			{
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres0, m_CascadeSplitDistances[0]);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres1, m_CascadeSplitDistances[1]);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres2, m_CascadeSplitDistances[2]);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres3, m_CascadeSplitDistances[3]);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSphereRadii, new global::UnityEngine.Vector4(m_CascadeSplitDistances[0].w * m_CascadeSplitDistances[0].w, m_CascadeSplitDistances[1].w * m_CascadeSplitDistances[1].w, m_CascadeSplitDistances[2].w * m_CascadeSplitDistances[2].w, m_CascadeSplitDistances[3].w * m_CascadeSplitDistances[3].w));
			}
			if (shadowData.supportsSoftShadows)
			{
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset0, new global::UnityEngine.Vector4(0f - num3, 0f - num4, num3, 0f - num4));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset1, new global::UnityEngine.Vector4(0f - num3, num4, num3, num4));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowmapSize, new global::UnityEngine.Vector4(num, num2, m_RenderTargetWidth, m_RenderTargetHeight));
			}
		}

		private void InitPassData(ref global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.PassData passData, global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			passData.pass = this;
			passData.emptyShadowmap = m_CreateEmptyShadowmap;
			passData.setKeywordForEmptyShadowmap = m_SetKeywordForEmptyShadowmap;
			passData.renderingData = renderingData;
			passData.cameraData = cameraData;
			passData.lightData = lightData;
			passData.shadowData = shadowData;
		}

		private void InitRendererLists(ref global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.PassData passData, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, bool useRenderGraph)
		{
			int mainLightIndex = passData.lightData.mainLightIndex;
			if (m_CreateEmptyShadowmap || mainLightIndex == -1)
			{
				return;
			}
			global::UnityEngine.Rendering.ShadowDrawingSettings shadowDrawingSettings = new global::UnityEngine.Rendering.ShadowDrawingSettings(passData.renderingData.cullResults, mainLightIndex);
			shadowDrawingSettings.useRenderingLayerMaskTest = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.useRenderingLayers;
			global::UnityEngine.Rendering.ShadowDrawingSettings settings = shadowDrawingSettings;
			for (int i = 0; i < m_ShadowCasterCascadesCount; i++)
			{
				if (useRenderGraph)
				{
					passData.shadowRendererListsHandle[i] = renderGraph.CreateShadowRendererList(ref settings);
				}
				else
				{
					passData.shadowRendererLists[i] = context.CreateShadowRendererList(ref settings);
				}
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>();
			global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\MainLightShadowCasterPass.cs", 482);
			InitPassData(ref passData, renderingData, cameraData, lightData, shadowData);
			InitRendererLists(ref passData, default(global::UnityEngine.Rendering.ScriptableRenderContext), graph, useRenderGraph: true);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle;
			if (!m_CreateEmptyShadowmap)
			{
				for (int i = 0; i < m_ShadowCasterCascadesCount; i++)
				{
					rasterRenderGraphBuilder.UseRendererList(in passData.shadowRendererListsHandle[i]);
				}
				textureHandle = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(graph, m_MainLightShadowDescriptor, "_MainLightShadowmapTexture", clear: true, (!global::UnityEngine.Rendering.Universal.ShadowUtils.m_ForceShadowPointSampling) ? global::UnityEngine.FilterMode.Bilinear : global::UnityEngine.FilterMode.Point);
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(textureHandle);
			}
			else
			{
				textureHandle = graph.defaultResources.defaultShadowTexture;
			}
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			if (textureHandle.IsValid())
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in textureHandle, global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.MainLightShadowConstantBuffer._MainLightShadowmapID);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
				if (!data.emptyShadowmap)
				{
					data.pass.RenderMainLightCascadeShadowmap(cmd, ref data, isRenderGraph: true);
				}
				else
				{
					if (data.setKeywordForEmptyShadowmap)
					{
						cmd.EnableKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadows);
					}
					SetShadowParamsForEmptyShadowmap(cmd);
				}
			});
			return textureHandle;
		}
	}
}
