namespace UnityEngine.Rendering.Universal
{
	internal class ScreenSpaceAmbientOcclusionPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private enum BlurTypes
		{
			Bilateral = 0,
			Gaussian = 1,
			Kawase = 2
		}

		private enum ShaderPasses
		{
			AmbientOcclusion = 0,
			BilateralBlurHorizontal = 1,
			BilateralBlurVertical = 2,
			BilateralBlurFinal = 3,
			BilateralAfterOpaque = 4,
			GaussianBlurHorizontal = 5,
			GaussianBlurVertical = 6,
			GaussianAfterOpaque = 7,
			KawaseBlur = 8,
			KawaseAfterOpaque = 9
		}

		private struct SSAOMaterialParams
		{
			internal bool orthographicCamera;

			internal bool aoBlueNoise;

			internal bool aoInterleavedGradient;

			internal bool sampleCountHigh;

			internal bool sampleCountMedium;

			internal bool sampleCountLow;

			internal bool sourceDepthNormals;

			internal bool sourceDepthHigh;

			internal bool sourceDepthMedium;

			internal bool sourceDepthLow;

			internal global::UnityEngine.Vector4 ssaoParams;

			internal SSAOMaterialParams(ref global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings settings, bool isOrthographic)
			{
				bool flag = settings.Source == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;
				float num = ((settings.AOMethod == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.BlueNoise) ? 1.5f : 1f);
				orthographicCamera = isOrthographic;
				aoBlueNoise = settings.AOMethod == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.BlueNoise;
				aoInterleavedGradient = settings.AOMethod == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.InterleavedGradient;
				sampleCountHigh = settings.Samples == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOSampleOption.High;
				sampleCountMedium = settings.Samples == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Medium;
				sampleCountLow = settings.Samples == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Low;
				sourceDepthNormals = settings.Source == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;
				sourceDepthHigh = !flag && settings.NormalSamples == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.NormalQuality.High;
				sourceDepthMedium = !flag && settings.NormalSamples == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.NormalQuality.Medium;
				sourceDepthLow = !flag && settings.NormalSamples == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.NormalQuality.Low;
				ssaoParams = new global::UnityEngine.Vector4(settings.Intensity, settings.Radius * num, 1f / (float)((!settings.Downsample) ? 1 : 2), settings.Falloff);
			}

			internal bool Equals(ref global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams other)
			{
				if (orthographicCamera == other.orthographicCamera && aoBlueNoise == other.aoBlueNoise && aoInterleavedGradient == other.aoInterleavedGradient && sampleCountHigh == other.sampleCountHigh && sampleCountMedium == other.sampleCountMedium && sampleCountLow == other.sampleCountLow && sourceDepthNormals == other.sourceDepthNormals && sourceDepthHigh == other.sourceDepthHigh && sourceDepthMedium == other.sourceDepthMedium && sourceDepthLow == other.sourceDepthLow)
				{
					return ssaoParams == other.ssaoParams;
				}
				return false;
			}
		}

		private class SSAOPassData
		{
			internal bool afterOpaque;

			internal global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions BlurQuality;

			internal global::UnityEngine.Material material;

			internal float directLightingStrength;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraColor;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle AOTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle finalTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle blurTexture;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraNormalsTexture;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;
		}

		private readonly bool m_SupportsR8RenderTextureFormat = global::UnityEngine.SystemInfo.SupportsRenderTextureFormat(global::UnityEngine.RenderTextureFormat.R8);

		private int m_BlueNoiseTextureIndex;

		private global::UnityEngine.Material m_Material;

		private global::UnityEngine.Texture2D[] m_BlueNoiseTextures;

		private global::UnityEngine.Vector4[] m_CameraTopLeftCorner = new global::UnityEngine.Vector4[2];

		private global::UnityEngine.Vector4[] m_CameraXExtent = new global::UnityEngine.Vector4[2];

		private global::UnityEngine.Vector4[] m_CameraYExtent = new global::UnityEngine.Vector4[2];

		private global::UnityEngine.Vector4[] m_CameraZExtent = new global::UnityEngine.Vector4[2];

		private global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.BlurTypes m_BlurType;

		private global::UnityEngine.Matrix4x4[] m_CameraViewProjections = new global::UnityEngine.Matrix4x4[2];

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler = global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.SSAO);

		private global::UnityEngine.RenderTextureDescriptor m_AOPassDescriptor;

		private global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings m_CurrentSettings;

		private const string k_SSAOTextureName = "_ScreenSpaceOcclusionTexture";

		private const string k_AmbientOcclusionParamName = "_AmbientOcclusionParam";

		internal static readonly int s_AmbientOcclusionParamID = global::UnityEngine.Shader.PropertyToID("_AmbientOcclusionParam");

		private static readonly int s_SSAOParamsID = global::UnityEngine.Shader.PropertyToID("_SSAOParams");

		private static readonly int s_SSAOBlueNoiseParamsID = global::UnityEngine.Shader.PropertyToID("_SSAOBlueNoiseParams");

		private static readonly int s_BlueNoiseTextureID = global::UnityEngine.Shader.PropertyToID("_BlueNoiseTexture");

		private static readonly int s_SSAOFinalTextureID = global::UnityEngine.Shader.PropertyToID("_ScreenSpaceOcclusionTexture");

		private static readonly int s_CameraViewXExtentID = global::UnityEngine.Shader.PropertyToID("_CameraViewXExtent");

		private static readonly int s_CameraViewYExtentID = global::UnityEngine.Shader.PropertyToID("_CameraViewYExtent");

		private static readonly int s_CameraViewZExtentID = global::UnityEngine.Shader.PropertyToID("_CameraViewZExtent");

		private static readonly int s_ProjectionParams2ID = global::UnityEngine.Shader.PropertyToID("_ProjectionParams2");

		private static readonly int s_CameraViewProjectionsID = global::UnityEngine.Shader.PropertyToID("_CameraViewProjections");

		private static readonly int s_CameraViewTopLeftCornerID = global::UnityEngine.Shader.PropertyToID("_CameraViewTopLeftCorner");

		private static readonly int s_CameraNormalsTextureID = global::UnityEngine.Shader.PropertyToID("_CameraNormalsTexture");

		private global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams m_SSAOParamsPrev;

		internal ScreenSpaceAmbientOcclusionPass()
		{
			m_CurrentSettings = new global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings();
		}

		internal bool Setup(ref global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings featureSettings, ref global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref global::UnityEngine.Material material, ref global::UnityEngine.Texture2D[] blueNoiseTextures)
		{
			m_BlueNoiseTextures = blueNoiseTextures;
			m_Material = material;
			m_CurrentSettings = featureSettings;
			if (renderer is global::UnityEngine.Rendering.Universal.UniversalRenderer { usesDeferredLighting: not false })
			{
				base.renderPassEvent = (m_CurrentSettings.AfterOpaque ? global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques : global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingGbuffer);
				m_CurrentSettings.Source = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;
			}
			else
			{
				base.renderPassEvent = (m_CurrentSettings.AfterOpaque ? global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingTransparents : ((global::UnityEngine.Rendering.Universal.RenderPassEvent)201));
			}
			switch (m_CurrentSettings.Source)
			{
			case global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.DepthSource.Depth:
				ConfigureInput(global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth);
				break;
			case global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals:
				ConfigureInput(global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Depth | global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput.Normal);
				break;
			default:
				throw new global::System.ArgumentOutOfRangeException();
			}
			switch (m_CurrentSettings.BlurQuality)
			{
			case global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.High:
				m_BlurType = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.BlurTypes.Bilateral;
				break;
			case global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Medium:
				m_BlurType = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.BlurTypes.Gaussian;
				break;
			case global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Low:
				m_BlurType = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.BlurTypes.Kawase;
				break;
			default:
				throw new global::System.ArgumentOutOfRangeException();
			}
			if (m_Material != null && m_CurrentSettings.Intensity > 0f && m_CurrentSettings.Radius > 0f)
			{
				return m_CurrentSettings.Falloff > 0f;
			}
			return false;
		}

		private void SetupKeywordsAndParameters(ref global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings settings, ref global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			int num = ((!cameraData.xr.enabled || !cameraData.xr.singlePassEnabled) ? 1 : 2);
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.Matrix4x4 viewMatrix = cameraData.GetViewMatrix(i);
				global::UnityEngine.Matrix4x4 projectionMatrix = cameraData.GetProjectionMatrix(i);
				m_CameraViewProjections[i] = projectionMatrix * viewMatrix;
				global::UnityEngine.Matrix4x4 matrix4x = viewMatrix;
				matrix4x.SetColumn(3, new global::UnityEngine.Vector4(0f, 0f, 0f, 1f));
				global::UnityEngine.Matrix4x4 inverse = (projectionMatrix * matrix4x).inverse;
				global::UnityEngine.Vector4 vector = inverse.MultiplyPoint(new global::UnityEngine.Vector4(-1f, 1f, -1f, 1f));
				global::UnityEngine.Vector4 vector2 = inverse.MultiplyPoint(new global::UnityEngine.Vector4(1f, 1f, -1f, 1f));
				global::UnityEngine.Vector4 vector3 = inverse.MultiplyPoint(new global::UnityEngine.Vector4(-1f, -1f, -1f, 1f));
				global::UnityEngine.Vector4 vector4 = inverse.MultiplyPoint(new global::UnityEngine.Vector4(0f, 0f, 1f, 1f));
				m_CameraTopLeftCorner[i] = vector;
				m_CameraXExtent[i] = vector2 - vector;
				m_CameraYExtent[i] = vector3 - vector;
				m_CameraZExtent[i] = vector4;
			}
			m_Material.SetVector(s_ProjectionParams2ID, new global::UnityEngine.Vector4(1f / cameraData.camera.nearClipPlane, 0f, 0f, 0f));
			m_Material.SetMatrixArray(s_CameraViewProjectionsID, m_CameraViewProjections);
			m_Material.SetVectorArray(s_CameraViewTopLeftCornerID, m_CameraTopLeftCorner);
			m_Material.SetVectorArray(s_CameraViewXExtentID, m_CameraXExtent);
			m_Material.SetVectorArray(s_CameraViewYExtentID, m_CameraYExtent);
			m_Material.SetVectorArray(s_CameraViewZExtentID, m_CameraZExtent);
			if (settings.AOMethod == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.BlueNoise)
			{
				m_BlueNoiseTextureIndex = (m_BlueNoiseTextureIndex + 1) % m_BlueNoiseTextures.Length;
				global::UnityEngine.Texture2D value = m_BlueNoiseTextures[m_BlueNoiseTextureIndex];
				global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4((float)cameraData.pixelWidth / (float)m_BlueNoiseTextures[m_BlueNoiseTextureIndex].width, (float)cameraData.pixelHeight / (float)m_BlueNoiseTextures[m_BlueNoiseTextureIndex].height, global::UnityEngine.Random.value, global::UnityEngine.Random.value);
				m_Material.SetTexture(s_BlueNoiseTextureID, value);
				m_Material.SetVector(s_SSAOBlueNoiseParamsID, value2);
			}
			global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams other = new global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams(ref settings, cameraData.camera.orthographic);
			bool num2 = !m_SSAOParamsPrev.Equals(ref other);
			bool flag = m_Material.HasProperty(s_SSAOParamsID);
			if (!(!num2 && flag))
			{
				m_SSAOParamsPrev = other;
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_ORTHOGRAPHIC", other.orthographicCamera);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_BLUE_NOISE", other.aoBlueNoise);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_INTERLEAVED_GRADIENT", other.aoInterleavedGradient);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_SAMPLE_COUNT_HIGH", other.sampleCountHigh);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_SAMPLE_COUNT_MEDIUM", other.sampleCountMedium);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_SAMPLE_COUNT_LOW", other.sampleCountLow);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_SOURCE_DEPTH_NORMALS", other.sourceDepthNormals);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_SOURCE_DEPTH_HIGH", other.sourceDepthHigh);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_SOURCE_DEPTH_MEDIUM", other.sourceDepthMedium);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(m_Material, "_SOURCE_DEPTH_LOW", other.sourceDepthLow);
				m_Material.SetVector(s_SSAOParamsID, other.ssaoParams);
			}
		}

		private void InitSSAOPassData(ref global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.SSAOPassData data)
		{
			data.material = m_Material;
			data.BlurQuality = m_CurrentSettings.BlurQuality;
			data.afterOpaque = m_CurrentSettings.AfterOpaque;
			data.directLightingStrength = m_CurrentSettings.DirectLightingStrength;
		}

		private static global::UnityEngine.Vector4 ComputeScaleBias(in global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
		{
			global::UnityEngine.Rendering.RTHandle rTHandle = source;
			global::UnityEngine.Vector2 one = default(global::UnityEngine.Vector2);
			if (rTHandle != null && rTHandle.useScaling)
			{
				one.x = rTHandle.rtHandleProperties.rtHandleScale.x;
				one.y = rTHandle.rtHandleProperties.rtHandleScale.y;
			}
			else
			{
				one = global::UnityEngine.Vector2.one;
			}
			if (context.GetTextureUVOrigin(in source) != context.GetTextureUVOrigin(in destination))
			{
				return new global::UnityEngine.Vector4(one.x, 0f - one.y, 0f, one.y);
			}
			return new global::UnityEngine.Vector4(one.x, one.y, 0f, 0f);
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			CreateRenderTextureHandles(renderGraph, universalResourceData, cameraData, out var aoTexture, out var blurTexture, out var finalTexture);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepthTexture = universalResourceData.cameraDepthTexture;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraNormalsTexture = universalResourceData.cameraNormalsTexture;
			SetupKeywordsAndParameters(ref m_CurrentSettings, ref cameraData);
			global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.SSAOPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.SSAOPassData>("Blit SSAO", out passData, m_ProfilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\ScreenSpaceAmbientOcclusionPass.cs", 369);
			unsafeRenderGraphBuilder.AllowGlobalStateModification(value: true);
			InitSSAOPassData(ref passData);
			passData.cameraColor = universalResourceData.cameraColor;
			passData.AOTexture = aoTexture;
			passData.finalTexture = finalTexture;
			passData.blurTexture = blurTexture;
			passData.cameraData = cameraData;
			unsafeRenderGraphBuilder.UseTexture(in passData.AOTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			if (universalResourceData.cameraColor.IsValid())
			{
				unsafeRenderGraphBuilder.UseTexture(universalResourceData.cameraColor);
			}
			if (passData.BlurQuality != global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Low)
			{
				unsafeRenderGraphBuilder.UseTexture(in passData.blurTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			}
			if (cameraDepthTexture.IsValid())
			{
				unsafeRenderGraphBuilder.UseTexture(in cameraDepthTexture);
			}
			if (m_CurrentSettings.Source == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals && cameraNormalsTexture.IsValid())
			{
				unsafeRenderGraphBuilder.UseTexture(in cameraNormalsTexture);
				passData.cameraNormalsTexture = cameraNormalsTexture;
			}
			if (!passData.afterOpaque && finalTexture.IsValid())
			{
				unsafeRenderGraphBuilder.UseTexture(in passData.finalTexture, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
				unsafeRenderGraphBuilder.SetGlobalTextureAfterPass(in finalTexture, s_SSAOFinalTextureID);
			}
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.SSAOPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext rgContext)
			{
				global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(rgContext.cmd);
				global::UnityEngine.Rendering.RenderBufferLoadAction loadAction = ((!data.afterOpaque) ? global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare : global::UnityEngine.Rendering.RenderBufferLoadAction.Load);
				global::UnityEngine.Rendering.Universal.PostProcessUtils.SetSourceSize(nativeCommandBuffer, data.cameraData.cameraTargetDescriptor.width, data.cameraData.cameraTargetDescriptor.height, data.cameraColor);
				if (data.cameraNormalsTexture.IsValid())
				{
					data.material.SetTexture(s_CameraNormalsTextureID, data.cameraNormalsTexture);
				}
				global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.AOTexture, data.AOTexture, global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, data.material, 0);
				switch (data.BlurQuality)
				{
				case global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.High:
				{
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.AOTexture, data.blurTexture, global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, data.material, 1);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.blurTexture, data.AOTexture, global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, data.material, 2);
					global::UnityEngine.Vector4 scaleBias = ComputeScaleBias(in rgContext, in data.AOTexture, in data.finalTexture);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.AOTexture, data.finalTexture, scaleBias, loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, data.material, data.afterOpaque ? 4 : 3);
					break;
				}
				case global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Medium:
				{
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.AOTexture, data.blurTexture, global::UnityEngine.Rendering.RenderBufferLoadAction.Load, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, data.material, 5);
					global::UnityEngine.Vector4 scaleBias = ComputeScaleBias(in rgContext, in data.blurTexture, in data.finalTexture);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.blurTexture, data.finalTexture, scaleBias, loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, data.material, data.afterOpaque ? 7 : 6);
					break;
				}
				case global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Low:
				{
					global::UnityEngine.Vector4 scaleBias = ComputeScaleBias(in rgContext, in data.AOTexture, in data.finalTexture);
					global::UnityEngine.Rendering.Blitter.BlitCameraTexture(nativeCommandBuffer, data.AOTexture, data.finalTexture, scaleBias, loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, data.material, data.afterOpaque ? 9 : 8);
					break;
				}
				default:
					throw new global::System.ArgumentOutOfRangeException();
				}
				if (!data.afterOpaque)
				{
					rgContext.cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ScreenSpaceOcclusion, value: true);
					rgContext.cmd.SetGlobalVector(s_AmbientOcclusionParamID, new global::UnityEngine.Vector4(1f, 0f, 0f, data.directLightingStrength));
				}
			});
		}

		private void CreateRenderTextureHandles(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.UniversalResourceData resourceData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle aoTexture, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle blurTexture, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle finalTexture)
		{
			global::UnityEngine.RenderTextureDescriptor cameraTargetDescriptor = cameraData.cameraTargetDescriptor;
			cameraTargetDescriptor.colorFormat = (m_SupportsR8RenderTextureFormat ? global::UnityEngine.RenderTextureFormat.R8 : global::UnityEngine.RenderTextureFormat.ARGB32);
			cameraTargetDescriptor.depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			cameraTargetDescriptor.msaaSamples = 1;
			int num = ((!m_CurrentSettings.Downsample) ? 1 : 2);
			bool flag = m_SupportsR8RenderTextureFormat && m_BlurType > global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.BlurTypes.Bilateral;
			global::UnityEngine.RenderTextureDescriptor desc = cameraTargetDescriptor;
			desc.colorFormat = (flag ? global::UnityEngine.RenderTextureFormat.R8 : global::UnityEngine.RenderTextureFormat.ARGB32);
			desc.width /= num;
			desc.height /= num;
			aoTexture = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_SSAO_OcclusionTexture0", clear: false, global::UnityEngine.FilterMode.Bilinear);
			finalTexture = (m_CurrentSettings.AfterOpaque ? resourceData.activeColorTexture : global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, cameraTargetDescriptor, "_ScreenSpaceOcclusionTexture", clear: false, global::UnityEngine.FilterMode.Bilinear));
			if (m_CurrentSettings.BlurQuality != global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Low)
			{
				blurTexture = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_SSAO_OcclusionTexture1", clear: false, global::UnityEngine.FilterMode.Bilinear);
			}
			else
			{
				blurTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			if (!m_CurrentSettings.AfterOpaque)
			{
				resourceData.ssaoTexture = finalTexture;
			}
		}

		public override void OnCameraCleanup(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new global::System.ArgumentNullException("cmd");
			}
			if (!m_CurrentSettings.AfterOpaque)
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ScreenSpaceOcclusion, value: false);
			}
		}

		public void Dispose()
		{
			m_SSAOParamsPrev = default(global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams);
		}
	}
}
