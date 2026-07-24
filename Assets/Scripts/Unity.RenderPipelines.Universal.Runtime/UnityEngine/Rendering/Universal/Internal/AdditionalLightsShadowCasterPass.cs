namespace UnityEngine.Rendering.Universal.Internal
{
	public class AdditionalLightsShadowCasterPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private static class AdditionalShadowsConstantBuffer
		{
			public static readonly int _AdditionalLightsWorldToShadow = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsWorldToShadow");

			public static readonly int _AdditionalShadowParams = global::UnityEngine.Shader.PropertyToID("_AdditionalShadowParams");

			public static readonly int _AdditionalShadowOffset0 = global::UnityEngine.Shader.PropertyToID("_AdditionalShadowOffset0");

			public static readonly int _AdditionalShadowOffset1 = global::UnityEngine.Shader.PropertyToID("_AdditionalShadowOffset1");

			public static readonly int _AdditionalShadowFadeParams = global::UnityEngine.Shader.PropertyToID("_AdditionalShadowFadeParams");

			public static readonly int _AdditionalShadowmapSize = global::UnityEngine.Shader.PropertyToID("_AdditionalShadowmapSize");

			public static readonly int _AdditionalLightsShadowmapID = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsShadowmapTexture");

			public static readonly int _AdditionalLightsWorldToShadow_SSBO = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsWorldToShadow_SSBO");

			public static readonly int _AdditionalShadowParams_SSBO = global::UnityEngine.Shader.PropertyToID("_AdditionalShadowParams_SSBO");
		}

		private class PassData
		{
			internal int shadowmapID;

			internal bool emptyShadowmap;

			internal bool setKeywordForEmptyShadowmap;

			internal bool useStructuredBuffer;

			internal bool stripShadowsOffVariants;

			internal global::UnityEngine.Matrix4x4 viewMatrix;

			internal global::UnityEngine.Vector2Int allocatedShadowAtlasSize;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle shadowmapTexture;

			internal global::UnityEngine.Rendering.Universal.UniversalLightData lightData;

			internal global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData;

			internal global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass pass;

			internal readonly global::UnityEngine.Rendering.RendererList[] shadowRendererLists = new global::UnityEngine.Rendering.RendererList[256];

			internal readonly global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle[] shadowRendererListsHdl = new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle[256];
		}

		[global::System.Obsolete("AdditionalLightsShadowCasterPass.m_AdditionalShadowsBufferId was deprecated. Shadow slice matrix is now passed to the GPU using an entry in buffer m_AdditionalLightsWorldToShadow_SSBO #from(2021.1) #breakingFrom(2023.1)", true)]
		public static int m_AdditionalShadowsBufferId;

		[global::System.Obsolete("AdditionalLightsShadowCasterPass.m_AdditionalShadowsIndicesId was deprecated. Shadow slice index is now passed to the GPU using last member of an entry in buffer m_AdditionalShadowParams_SSBO #from(2021.1) #breakingFrom(2023.1)", true)]
		public static int m_AdditionalShadowsIndicesId;

		internal global::UnityEngine.Rendering.RTHandle m_AdditionalLightsShadowmapHandle;

		private int renderTargetWidth;

		private int renderTargetHeight;

		private bool m_CreateEmptyShadowmap;

		private bool m_SetKeywordForEmptyShadowmap;

		private bool m_IssuedMessageAboutShadowSlicesTooMany;

		private bool m_IssuedMessageAboutShadowMapsRescale;

		private bool m_IssuedMessageAboutShadowMapsTooBig;

		private bool m_IssuedMessageAboutRemovedShadowSlices;

		private static bool m_IssuedMessageAboutPointLightHardShadowResolutionTooSmall;

		private static bool m_IssuedMessageAboutPointLightSoftShadowResolutionTooSmall;

		private readonly bool m_UseStructuredBuffer;

		private float m_MaxShadowDistanceSq;

		private float m_CascadeBorder;

		private bool[] m_VisibleLightIndexToIsCastingShadows;

		private short[] m_VisibleLightIndexToAdditionalLightIndex;

		private short[] m_AdditionalLightIndexToVisibleLightIndex;

		private global::UnityEngine.Vector4[] m_AdditionalLightIndexToShadowParams;

		private global::UnityEngine.Matrix4x4[] m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix;

		private global::UnityEngine.Rendering.Universal.ShadowSliceData[] m_AdditionalLightsShadowSlices;

		private readonly global::System.Collections.Generic.List<byte> m_GlobalShadowSliceIndexToPerLightShadowSliceIndex = new global::System.Collections.Generic.List<byte>();

		private readonly global::System.Collections.Generic.List<short> m_ShadowSliceToAdditionalLightIndex = new global::System.Collections.Generic.List<short>();

		private readonly global::System.Collections.Generic.Dictionary<int, ulong> m_ShadowRequestsHashes = new global::System.Collections.Generic.Dictionary<int, ulong>();

		private readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSetupSampler = new global::UnityEngine.Rendering.ProfilingSampler("Setup Additional Shadows");

		private global::UnityEngine.RenderTextureDescriptor m_AdditionalLightShadowDescriptor;

		private const int k_ShadowmapBufferBits = 16;

		private const float k_LightTypeIdentifierInShadowParams_Spot = 0f;

		private const float k_LightTypeIdentifierInShadowParams_Point = 1f;

		private const string k_AdditionalLightShadowMapTextureName = "_AdditionalLightsShadowmapTexture";

		private static readonly global::UnityEngine.Vector4 c_DefaultShadowParams = new global::UnityEngine.Vector4(0f, 0f, 0f, -1f);

		private static global::UnityEngine.Vector4 s_EmptyAdditionalShadowFadeParams;

		private static global::UnityEngine.Vector4[] s_EmptyAdditionalLightIndexToShadowParams;

		private static bool isAdditionalShadowParamsDirty;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Configure(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public AdditionalLightsShadowCasterPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw Additional Lights Shadowmap");
			base.renderPassEvent = evt;
			m_UseStructuredBuffer = global::UnityEngine.Rendering.Universal.RenderingUtils.useStructuredBuffer;
			int maxVisibleAdditionalLights = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights;
			int num = maxVisibleAdditionalLights + 1;
			int num2 = (m_UseStructuredBuffer ? num : global::System.Math.Min(num, maxVisibleAdditionalLights));
			m_AdditionalLightIndexToVisibleLightIndex = new short[num2];
			m_VisibleLightIndexToAdditionalLightIndex = new short[num];
			m_VisibleLightIndexToIsCastingShadows = new bool[num];
			m_AdditionalLightIndexToShadowParams = new global::UnityEngine.Vector4[num2];
			s_EmptyAdditionalLightIndexToShadowParams = new global::UnityEngine.Vector4[num2];
			for (int i = 0; i < s_EmptyAdditionalLightIndexToShadowParams.Length; i++)
			{
				s_EmptyAdditionalLightIndexToShadowParams[i] = c_DefaultShadowParams;
			}
			if (!m_UseStructuredBuffer)
			{
				m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix = new global::UnityEngine.Matrix4x4[maxVisibleAdditionalLights];
			}
		}

		public void Dispose()
		{
			m_AdditionalLightsShadowmapHandle?.Release();
		}

		internal static float CalcGuardAngle(float frustumAngleInDegrees, float guardBandSizeInTexels, float sliceResolutionInTexels)
		{
			float num = frustumAngleInDegrees * (global::System.MathF.PI / 180f) / 2f;
			float num2 = global::UnityEngine.Mathf.Tan(num);
			float num3 = sliceResolutionInTexels / 2f;
			float num4 = guardBandSizeInTexels / 2f;
			float num5 = 1f + num4 / num3;
			float num6 = global::UnityEngine.Mathf.Atan(num2 * num5) - num;
			return 2f * num6 * 57.29578f;
		}

		internal static float GetPointLightShadowFrustumFovBiasInDegrees(int shadowSliceResolution, bool shadowFiltering)
		{
			float num = 4f;
			if (shadowSliceResolution <= 8)
			{
				if (!m_IssuedMessageAboutPointLightHardShadowResolutionTooSmall)
				{
					global::UnityEngine.Debug.LogWarning("Too many additional punctual lights shadows, increase shadow atlas size or remove some shadowed lights");
					m_IssuedMessageAboutPointLightHardShadowResolutionTooSmall = true;
				}
			}
			else if (shadowSliceResolution <= 16)
			{
				num = 43f;
			}
			else if (shadowSliceResolution <= 32)
			{
				num = 18.55f;
			}
			else if (shadowSliceResolution <= 64)
			{
				num = 8.63f;
			}
			else if (shadowSliceResolution <= 128)
			{
				num = 4.13f;
			}
			else if (shadowSliceResolution <= 256)
			{
				num = 2.03f;
			}
			else if (shadowSliceResolution <= 512)
			{
				num = 1f;
			}
			else if (shadowSliceResolution <= 1024)
			{
				num = 0.5f;
			}
			else if (shadowSliceResolution <= 2048)
			{
				num = 0.25f;
			}
			if (shadowFiltering)
			{
				if (shadowSliceResolution <= 16)
				{
					if (!m_IssuedMessageAboutPointLightSoftShadowResolutionTooSmall)
					{
						global::UnityEngine.Debug.LogWarning("Too many additional punctual lights shadows to use Soft Shadows. Increase shadow atlas size, remove some shadowed lights or use Hard Shadows.");
						m_IssuedMessageAboutPointLightSoftShadowResolutionTooSmall = true;
					}
				}
				else if (shadowSliceResolution <= 32)
				{
					num += 9.35f;
				}
				else if (shadowSliceResolution <= 64)
				{
					num += 4.07f;
				}
				else if (shadowSliceResolution <= 128)
				{
					num += 1.77f;
				}
				else if (shadowSliceResolution <= 256)
				{
					num += 0.85f;
				}
				else if (shadowSliceResolution <= 512)
				{
					num += 0.39f;
				}
				else if (shadowSliceResolution <= 1024)
				{
					num += 0.17f;
				}
				else if (shadowSliceResolution <= 2048)
				{
					num += 0.074f;
				}
			}
			return num;
		}

		private ulong ResolutionLog2ForHash(int resolution)
		{
			return resolution switch
			{
				4096 => 12uL, 
				2048 => 11uL, 
				1024 => 10uL, 
				512 => 9uL, 
				_ => 8uL, 
			};
		}

		private ulong ComputeShadowRequestHash(global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			ulong num = 0uL;
			ulong num2 = 0uL;
			ulong num3 = 0uL;
			ulong num4 = 0uL;
			ulong num5 = 0uL;
			ulong num6 = 0uL;
			ulong num7 = 0uL;
			ulong num8 = 0uL;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = lightData.visibleLights;
			for (int i = 0; i < visibleLights.Length; i++)
			{
				ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAt(i);
				global::UnityEngine.Light light = reference.light;
				if (global::UnityEngine.Rendering.Universal.ShadowUtils.IsValidShadowCastingLight(lightData, i, reference.lightType, light.shadows, light.shadowStrength))
				{
					switch (reference.lightType)
					{
					case global::UnityEngine.LightType.Spot:
						num2++;
						break;
					case global::UnityEngine.LightType.Point:
						num++;
						break;
					}
					switch (shadowData.resolution[i])
					{
					case 128:
						num3++;
						break;
					case 256:
						num4++;
						break;
					case 512:
						num5++;
						break;
					case 1024:
						num6++;
						break;
					case 2048:
						num7++;
						break;
					case 4096:
						num8++;
						break;
					}
				}
			}
			return (ResolutionLog2ForHash(shadowData.additionalLightsShadowmapWidth) - 8) | (num << 3) | (num2 << 11) | (num3 << 19) | (num4 << 27) | (num5 << 35) | (num6 << 43) | (num7 << 50) | (num8 << 57);
		}

		private float GetLightTypeIdentifierForShadowParams(global::UnityEngine.LightType lightType)
		{
			return lightType switch
			{
				global::UnityEngine.LightType.Spot => 0f, 
				global::UnityEngine.LightType.Point => 1f, 
				_ => -1f, 
			};
		}

		private bool UsesBakedShadows(global::UnityEngine.Light light)
		{
			return light.bakingOutput.lightmapBakeType != global::UnityEngine.LightmapBakeType.Realtime;
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
			using (new global::UnityEngine.Rendering.ProfilingScope(m_ProfilingSetupSampler))
			{
				bool additionalLightShadowsEnabled = shadowData.additionalLightShadowsEnabled;
				if (!additionalLightShadowsEnabled)
				{
					if (AnyAdditionalLightHasMixedShadows(lightData))
					{
						return SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants, additionalLightShadowsEnabled, lightData, shadowData);
					}
					return false;
				}
				if (!shadowData.supportsAdditionalLightShadows || (cameraData.camera.targetTexture != null && cameraData.camera.targetTexture.format == global::UnityEngine.RenderTextureFormat.Depth))
				{
					return SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants, additionalLightShadowsEnabled, lightData, shadowData);
				}
				Clear();
				renderTargetWidth = shadowData.additionalLightsShadowmapWidth;
				renderTargetHeight = shadowData.additionalLightsShadowmapHeight;
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = lightData.visibleLights;
				ref global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout shadowAtlasLayout = ref shadowData.shadowAtlasLayout;
				if (m_VisibleLightIndexToAdditionalLightIndex.Length < visibleLights.Length)
				{
					m_VisibleLightIndexToAdditionalLightIndex = new short[visibleLights.Length];
					m_VisibleLightIndexToIsCastingShadows = new bool[visibleLights.Length];
				}
				int num = (m_UseStructuredBuffer ? visibleLights.Length : global::System.Math.Min(visibleLights.Length, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights));
				if (m_AdditionalLightIndexToVisibleLightIndex.Length < num)
				{
					m_AdditionalLightIndexToVisibleLightIndex = new short[num];
					m_AdditionalLightIndexToShadowParams = new global::UnityEngine.Vector4[num];
				}
				int totalShadowSlicesCount = shadowAtlasLayout.GetTotalShadowSlicesCount();
				int totalShadowResolutionRequestCount = shadowAtlasLayout.GetTotalShadowResolutionRequestCount();
				int shadowSlicesScaleFactor = shadowAtlasLayout.GetShadowSlicesScaleFactor();
				bool flag = shadowAtlasLayout.HasTooManyShadowMaps();
				int atlasSize = shadowAtlasLayout.GetAtlasSize();
				if (totalShadowSlicesCount < totalShadowResolutionRequestCount && !m_IssuedMessageAboutRemovedShadowSlices)
				{
					global::UnityEngine.Debug.LogWarning($"Too many additional punctual lights shadows to look good, URP removed {totalShadowResolutionRequestCount - totalShadowSlicesCount} shadow maps to make the others fit in the shadow atlas. To avoid this, increase shadow atlas size, remove some shadowed lights, replace soft shadows by hard shadows ; or replace point lights by spot lights");
					m_IssuedMessageAboutRemovedShadowSlices = true;
				}
				if (!m_IssuedMessageAboutShadowMapsTooBig && flag)
				{
					global::UnityEngine.Debug.LogWarning($"Too many additional punctual lights shadows. URP tried reducing shadow resolutions by {shadowSlicesScaleFactor} but it was still too much. Increase shadow atlas size, decrease big shadow resolutions, or reduce the number of shadow maps active in the same frame (currently was {totalShadowSlicesCount}).");
					m_IssuedMessageAboutShadowMapsTooBig = true;
				}
				if (!m_IssuedMessageAboutShadowMapsRescale && shadowSlicesScaleFactor > 1)
				{
					global::UnityEngine.Debug.Log($"Reduced additional punctual light shadows resolution by {shadowSlicesScaleFactor} to make {totalShadowSlicesCount} shadow maps fit in the {atlasSize}x{atlasSize} shadow atlas. To avoid this, increase shadow atlas size, decrease big shadow resolutions, or reduce the number of shadow maps active in the same frame");
					m_IssuedMessageAboutShadowMapsRescale = true;
				}
				if (m_AdditionalLightsShadowSlices == null || m_AdditionalLightsShadowSlices.Length < totalShadowSlicesCount)
				{
					m_AdditionalLightsShadowSlices = new global::UnityEngine.Rendering.Universal.ShadowSliceData[totalShadowSlicesCount];
				}
				if (m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix == null || (m_UseStructuredBuffer && m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix.Length < totalShadowSlicesCount))
				{
					m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix = new global::UnityEngine.Matrix4x4[totalShadowSlicesCount];
				}
				for (int i = 0; i < num; i++)
				{
					m_AdditionalLightIndexToShadowParams[i] = c_DefaultShadowParams;
				}
				for (int j = 0; j < m_VisibleLightIndexToAdditionalLightIndex.Length; j++)
				{
					m_VisibleLightIndexToAdditionalLightIndex[j] = -1;
					m_VisibleLightIndexToIsCastingShadows[j] = false;
				}
				short num2 = 0;
				short num3 = 0;
				bool supportsSoftShadows = shadowData.supportsSoftShadows;
				global::UnityEngine.Rendering.Universal.UniversalRenderer obj = (global::UnityEngine.Rendering.Universal.UniversalRenderer)cameraData.renderer;
				bool flag2 = obj.renderingModeActual == global::UnityEngine.Rendering.Universal.RenderingMode.Deferred;
				bool shadowTransparentReceive = obj.shadowTransparentReceive;
				bool flag3 = !flag2 || shadowTransparentReceive;
				for (int k = 0; k < visibleLights.Length; k++)
				{
					if (k == lightData.mainLightIndex)
					{
						continue;
					}
					short num4 = ((!flag3) ? num3 : num2++);
					m_VisibleLightIndexToAdditionalLightIndex[k] = num4;
					if (num4 >= m_AdditionalLightIndexToVisibleLightIndex.Length)
					{
						continue;
					}
					m_AdditionalLightIndexToVisibleLightIndex[num4] = (short)k;
					if (m_ShadowSliceToAdditionalLightIndex.Count >= totalShadowSlicesCount)
					{
						continue;
					}
					ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAt(k);
					global::UnityEngine.Light light = reference.light;
					if (light == null)
					{
						break;
					}
					global::UnityEngine.LightType lightType = reference.lightType;
					bool flag4 = UsesBakedShadows(light);
					float lightTypeIdentifierForShadowParams = GetLightTypeIdentifierForShadowParams(lightType);
					int punctualLightShadowSlicesCount = global::UnityEngine.Rendering.Universal.ShadowUtils.GetPunctualLightShadowSlicesCount(in lightType);
					bool flag5 = global::UnityEngine.Rendering.Universal.ShadowUtils.IsValidShadowCastingLight(lightData, k, reference.lightType, light.shadows, light.shadowStrength);
					if (flag5 && m_ShadowSliceToAdditionalLightIndex.Count + punctualLightShadowSlicesCount > totalShadowSlicesCount)
					{
						if (!m_IssuedMessageAboutShadowSlicesTooMany)
						{
							global::UnityEngine.Debug.Log("There are too many shadowed additional punctual lights active at the same time, URP will not render all the shadows. To ensure all shadows are rendered, reduce the number of shadowed additional lights in the scene ; make sure they are not active at the same time ; or replace point lights by spot lights (spot lights use less shadow maps than point lights).");
							m_IssuedMessageAboutShadowSlicesTooMany = true;
						}
						break;
					}
					float y = global::UnityEngine.Rendering.Universal.ShadowUtils.SoftShadowQualityToShaderProperty(light, supportsSoftShadows && light.shadows == global::UnityEngine.LightShadows.Soft);
					int count = m_ShadowSliceToAdditionalLightIndex.Count;
					bool flag6 = false;
					for (byte b = 0; b < punctualLightShadowSlicesCount; b++)
					{
						int count2 = m_ShadowSliceToAdditionalLightIndex.Count;
						global::UnityEngine.Bounds outBounds;
						bool shadowCasterBounds = renderingData.cullResults.GetShadowCasterBounds(k, out outBounds);
						if (!shadowData.supportsAdditionalLightShadows || !flag5 || !shadowCasterBounds)
						{
							if (flag4 && lightTypeIdentifierForShadowParams > -1f)
							{
								m_AdditionalLightIndexToShadowParams[num4] = new global::UnityEngine.Vector4(light.shadowStrength, y, lightTypeIdentifierForShadowParams, num4);
								m_VisibleLightIndexToIsCastingShadows[k] = flag4;
							}
						}
						else if (shadowAtlasLayout.HasSpaceForLight(k))
						{
							switch (lightType)
							{
							case global::UnityEngine.LightType.Spot:
							{
								ref global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos reference4 = ref shadowData.visibleLightsShadowCullingInfos.UnsafeElementAt(k);
								ref global::UnityEngine.Rendering.Universal.ShadowSliceData reference5 = ref reference4.slices.UnsafeElementAt(0);
								m_AdditionalLightsShadowSlices[count2].viewMatrix = reference5.viewMatrix;
								m_AdditionalLightsShadowSlices[count2].projectionMatrix = reference5.projectionMatrix;
								m_AdditionalLightsShadowSlices[count2].splitData = reference5.splitData;
								if (reference4.IsSliceValid(0))
								{
									m_ShadowSliceToAdditionalLightIndex.Add(num4);
									m_GlobalShadowSliceIndexToPerLightShadowSliceIndex.Add(b);
									m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[count2] = reference5.shadowTransform;
									m_AdditionalLightIndexToShadowParams[num4] = new global::UnityEngine.Vector4(light.shadowStrength, y, lightTypeIdentifierForShadowParams, count);
									flag6 = true;
								}
								break;
							}
							case global::UnityEngine.LightType.Point:
							{
								ref global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos reference2 = ref shadowData.visibleLightsShadowCullingInfos.UnsafeElementAt(k);
								ref global::UnityEngine.Rendering.Universal.ShadowSliceData reference3 = ref reference2.slices.UnsafeElementAt(b);
								m_AdditionalLightsShadowSlices[count2].viewMatrix = reference3.viewMatrix;
								m_AdditionalLightsShadowSlices[count2].projectionMatrix = reference3.projectionMatrix;
								m_AdditionalLightsShadowSlices[count2].splitData = reference3.splitData;
								if (reference2.IsSliceValid(b))
								{
									m_ShadowSliceToAdditionalLightIndex.Add(num4);
									m_GlobalShadowSliceIndexToPerLightShadowSliceIndex.Add(b);
									m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[count2] = reference3.shadowTransform;
									m_AdditionalLightIndexToShadowParams[num4] = new global::UnityEngine.Vector4(light.shadowStrength, y, lightTypeIdentifierForShadowParams, count);
									flag6 = true;
								}
								break;
							}
							}
						}
					}
					if (flag6)
					{
						m_VisibleLightIndexToIsCastingShadows[k] = true;
						m_VisibleLightIndexToAdditionalLightIndex[k] = num4;
						m_AdditionalLightIndexToVisibleLightIndex[num4] = (short)k;
						num3++;
					}
					else
					{
						m_VisibleLightIndexToIsCastingShadows[k] = flag4;
						m_AdditionalLightIndexToShadowParams[num4] = new global::UnityEngine.Vector4(light.shadowStrength, y, lightTypeIdentifierForShadowParams, c_DefaultShadowParams.w);
					}
				}
				if (num3 == 0)
				{
					return SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants, additionalLightShadowsEnabled, lightData, shadowData);
				}
				int count3 = m_ShadowSliceToAdditionalLightIndex.Count;
				int num5 = 0;
				int num6 = 0;
				for (int l = 0; l < totalShadowSlicesCount; l++)
				{
					global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest sortedShadowResolutionRequest = shadowAtlasLayout.GetSortedShadowResolutionRequest(l);
					num5 = global::UnityEngine.Mathf.Max(num5, sortedShadowResolutionRequest.offsetX + sortedShadowResolutionRequest.allocatedResolution);
					num6 = global::UnityEngine.Mathf.Max(num6, sortedShadowResolutionRequest.offsetY + sortedShadowResolutionRequest.allocatedResolution);
				}
				renderTargetWidth = global::UnityEngine.Mathf.NextPowerOfTwo(num5);
				renderTargetHeight = global::UnityEngine.Mathf.NextPowerOfTwo(num6);
				float num7 = 1f / (float)renderTargetWidth;
				float num8 = 1f / (float)renderTargetHeight;
				for (int m = 0; m < count3; m++)
				{
					int num9 = m_ShadowSliceToAdditionalLightIndex[m];
					if (!global::UnityEngine.Mathf.Approximately(m_AdditionalLightIndexToShadowParams[num9].x, 0f) && !global::UnityEngine.Mathf.Approximately(m_AdditionalLightIndexToShadowParams[num9].w, -1f))
					{
						int originalVisibleLightIndex = m_AdditionalLightIndexToVisibleLightIndex[num9];
						int sliceIndex = m_GlobalShadowSliceIndexToPerLightShadowSliceIndex[m];
						global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest sliceShadowResolutionRequest = shadowAtlasLayout.GetSliceShadowResolutionRequest(originalVisibleLightIndex, sliceIndex);
						int allocatedResolution = sliceShadowResolutionRequest.allocatedResolution;
						global::UnityEngine.Matrix4x4 identity = global::UnityEngine.Matrix4x4.identity;
						identity.m00 = (float)allocatedResolution * num7;
						identity.m11 = (float)allocatedResolution * num8;
						m_AdditionalLightsShadowSlices[m].offsetX = sliceShadowResolutionRequest.offsetX;
						m_AdditionalLightsShadowSlices[m].offsetY = sliceShadowResolutionRequest.offsetY;
						m_AdditionalLightsShadowSlices[m].resolution = allocatedResolution;
						identity.m03 = (float)m_AdditionalLightsShadowSlices[m].offsetX * num7;
						identity.m13 = (float)m_AdditionalLightsShadowSlices[m].offsetY * num8;
						m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[m] = identity * m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[m];
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
			if (m_AdditionalLightShadowDescriptor.width != renderTargetWidth || m_AdditionalLightShadowDescriptor.height != renderTargetHeight || m_AdditionalLightShadowDescriptor.depthBufferBits != 16 || m_AdditionalLightShadowDescriptor.colorFormat != global::UnityEngine.RenderTextureFormat.Shadowmap)
			{
				m_AdditionalLightShadowDescriptor = new global::UnityEngine.RenderTextureDescriptor(renderTargetWidth, renderTargetHeight, global::UnityEngine.RenderTextureFormat.Shadowmap, 16);
			}
		}

		private bool AnyAdditionalLightHasMixedShadows(global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			for (int i = 0; i < lightData.visibleLights.Length; i++)
			{
				if (i != lightData.mainLightIndex)
				{
					global::UnityEngine.Light light = lightData.visibleLights[i].light;
					if (light.shadows != global::UnityEngine.LightShadows.None && light.bakingOutput.isBaked && light.bakingOutput.mixedLightingMode != global::UnityEngine.MixedLightingMode.IndirectOnly && light.bakingOutput.lightmapBakeType == global::UnityEngine.LightmapBakeType.Mixed)
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool SetupForEmptyRendering(bool stripShadowsOffVariants, bool shadowsEnabled, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			if (!stripShadowsOffVariants)
			{
				return false;
			}
			shadowData.isKeywordAdditionalLightShadowsEnabled = true;
			m_CreateEmptyShadowmap = true;
			m_SetKeywordForEmptyShadowmap = shadowsEnabled;
			global::UnityEngine.Rendering.Universal.ShadowUtils.GetScaleAndBiasForLinearDistanceFade(m_MaxShadowDistanceSq, m_CascadeBorder, out var scale, out var bias);
			s_EmptyAdditionalShadowFadeParams = new global::UnityEngine.Vector4(scale, bias, 0f, 0f);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = lightData.visibleLights;
			if (s_EmptyAdditionalLightIndexToShadowParams.Length < visibleLights.Length)
			{
				m_VisibleLightIndexToAdditionalLightIndex = new short[visibleLights.Length];
				m_VisibleLightIndexToIsCastingShadows = new bool[visibleLights.Length];
				s_EmptyAdditionalLightIndexToShadowParams = new global::UnityEngine.Vector4[visibleLights.Length];
				isAdditionalShadowParamsDirty = true;
			}
			if (isAdditionalShadowParamsDirty)
			{
				isAdditionalShadowParamsDirty = false;
				global::UnityEngine.Debug.LogWarning($"The number of visible additional lights {visibleLights.Length} exceeds the maximum supported lights {(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights)}." + " Please refer URP documentation to change maximum number of visible lights or reduce the number of lights to maximum allowed additional lights.");
			}
			short num = 0;
			for (int i = 0; i < visibleLights.Length; i++)
			{
				if (i == lightData.mainLightIndex)
				{
					continue;
				}
				global::UnityEngine.Light light = visibleLights.UnsafeElementAt(i).light;
				if (light == null)
				{
					continue;
				}
				float lightTypeIdentifierForShadowParams = GetLightTypeIdentifierForShadowParams(light.type);
				if (!(lightTypeIdentifierForShadowParams < 0f))
				{
					short num2 = num++;
					global::UnityEngine.LightShadows shadows = light.shadows;
					if (shadows != global::UnityEngine.LightShadows.None)
					{
						bool flag = shadows != global::UnityEngine.LightShadows.Soft;
						bool supportsSoftShadows = shadowData.supportsSoftShadows;
						float y = global::UnityEngine.Rendering.Universal.ShadowUtils.SoftShadowQualityToShaderProperty(light, supportsSoftShadows && flag);
						s_EmptyAdditionalLightIndexToShadowParams[num2] = new global::UnityEngine.Vector4(light.shadowStrength, y, lightTypeIdentifierForShadowParams, c_DefaultShadowParams.w);
					}
					else
					{
						s_EmptyAdditionalLightIndexToShadowParams[num2] = c_DefaultShadowParams;
					}
					m_VisibleLightIndexToAdditionalLightIndex[i] = num2;
					m_VisibleLightIndexToIsCastingShadows[i] = UsesBakedShadows(light);
				}
			}
			return true;
		}

		public int GetShadowLightIndexFromLightIndex(int visibleLightIndex)
		{
			if (visibleLightIndex < 0 || visibleLightIndex >= m_VisibleLightIndexToAdditionalLightIndex.Length || !m_VisibleLightIndexToIsCastingShadows[visibleLightIndex])
			{
				return -1;
			}
			return m_VisibleLightIndexToAdditionalLightIndex[visibleLightIndex];
		}

		private void Clear()
		{
			m_ShadowSliceToAdditionalLightIndex.Clear();
			m_GlobalShadowSliceIndexToPerLightShadowSliceIndex.Clear();
		}

		internal static void SetShadowParamsForEmptyShadowmap(global::UnityEngine.Rendering.RasterCommandBuffer rasterCommandBuffer)
		{
			rasterCommandBuffer.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowFadeParams, s_EmptyAdditionalShadowFadeParams);
			if (global::UnityEngine.Rendering.Universal.RenderingUtils.useStructuredBuffer)
			{
				global::UnityEngine.ComputeBuffer additionalLightShadowParamsStructuredBuffer = global::UnityEngine.Rendering.Universal.ShaderData.instance.GetAdditionalLightShadowParamsStructuredBuffer(s_EmptyAdditionalLightIndexToShadowParams.Length);
				additionalLightShadowParamsStructuredBuffer.SetData(s_EmptyAdditionalLightIndexToShadowParams);
				rasterCommandBuffer.SetGlobalBuffer(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams_SSBO, additionalLightShadowParamsStructuredBuffer);
			}
			else if (s_EmptyAdditionalLightIndexToShadowParams.Length <= global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights)
			{
				rasterCommandBuffer.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams, s_EmptyAdditionalLightIndexToShadowParams);
			}
		}

		private void RenderAdditionalShadowmapAtlas(global::UnityEngine.Rendering.RasterCommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.PassData data, bool useRenderGraph)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = data.lightData.visibleLights;
			bool flag = false;
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.AdditionalLightsShadow)))
			{
				if (!useRenderGraph)
				{
					global::UnityEngine.Rendering.Universal.ShadowUtils.SetWorldToCameraAndCameraToWorldMatrices(cmd, data.viewMatrix);
				}
				bool flag2 = false;
				int count = m_ShadowSliceToAdditionalLightIndex.Count;
				if (count > 0)
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.CastingPunctualLightShadow, value: true);
				}
				global::UnityEngine.Vector4 b = new global::UnityEngine.Vector4(-10f, -10f, -10f, -10f);
				for (int i = 0; i < count; i++)
				{
					int num = m_ShadowSliceToAdditionalLightIndex[i];
					if (!global::UnityEngine.Rendering.Universal.ShadowUtils.FastApproximately(m_AdditionalLightIndexToShadowParams[num].x, 0f) && !global::UnityEngine.Rendering.Universal.ShadowUtils.FastApproximately(m_AdditionalLightIndexToShadowParams[num].w, -1f))
					{
						int num2 = m_AdditionalLightIndexToVisibleLightIndex[num];
						ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAt(num2);
						global::UnityEngine.Rendering.Universal.ShadowSliceData shadowSliceData = m_AdditionalLightsShadowSlices[i];
						global::UnityEngine.Vector4 shadowBias = global::UnityEngine.Rendering.Universal.ShadowUtils.GetShadowBias(ref reference, num2, data.shadowData, shadowSliceData.projectionMatrix, shadowSliceData.resolution);
						if (i == 0 || !global::UnityEngine.Rendering.Universal.ShadowUtils.FastApproximately(shadowBias, b))
						{
							global::UnityEngine.Rendering.Universal.ShadowUtils.SetShadowBias(cmd, shadowBias);
							b = shadowBias;
						}
						global::UnityEngine.Vector3 lightPosition = reference.localToWorldMatrix.GetColumn(3);
						global::UnityEngine.Rendering.Universal.ShadowUtils.SetLightPosition(cmd, lightPosition);
						global::UnityEngine.Rendering.RendererList shadowRendererList = (useRenderGraph ? ((global::UnityEngine.Rendering.RendererList)data.shadowRendererListsHdl[i]) : data.shadowRendererLists[i]);
						global::UnityEngine.Rendering.Universal.ShadowUtils.RenderShadowSlice(cmd, ref shadowSliceData, ref shadowRendererList, shadowSliceData.projectionMatrix, shadowSliceData.viewMatrix);
						flag |= reference.light.shadows == global::UnityEngine.LightShadows.Soft;
						flag2 = true;
					}
				}
				bool flag3 = data.shadowData.supportsMainLightShadows && data.lightData.mainLightIndex != -1 && visibleLights[data.lightData.mainLightIndex].light.shadows == global::UnityEngine.LightShadows.Soft;
				bool flag4 = !data.stripShadowsOffVariants;
				data.shadowData.isKeywordAdditionalLightShadowsEnabled = !flag4 || flag2;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.AdditionalLightShadows, data.shadowData.isKeywordAdditionalLightShadowsEnabled);
				bool flag5 = data.shadowData.supportsSoftShadows && (flag3 || flag);
				data.shadowData.isKeywordSoftShadowsEnabled = flag5;
				global::UnityEngine.Rendering.Universal.ShadowUtils.SetSoftShadowQualityShaderKeywords(cmd, data.shadowData);
				if (flag2)
				{
					SetupAdditionalLightsShadowReceiverConstants(cmd, data.allocatedShadowAtlasSize, data.useStructuredBuffer, flag5);
				}
			}
		}

		private void SetupAdditionalLightsShadowReceiverConstants(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Vector2Int allocatedShadowAtlasSize, bool useStructuredBuffer, bool softShadows)
		{
			if (useStructuredBuffer)
			{
				global::UnityEngine.ComputeBuffer additionalLightShadowParamsStructuredBuffer = global::UnityEngine.Rendering.Universal.ShaderData.instance.GetAdditionalLightShadowParamsStructuredBuffer(m_AdditionalLightIndexToShadowParams.Length);
				additionalLightShadowParamsStructuredBuffer.SetData(m_AdditionalLightIndexToShadowParams);
				cmd.SetGlobalBuffer(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams_SSBO, additionalLightShadowParamsStructuredBuffer);
				global::UnityEngine.ComputeBuffer additionalLightShadowSliceMatricesStructuredBuffer = global::UnityEngine.Rendering.Universal.ShaderData.instance.GetAdditionalLightShadowSliceMatricesStructuredBuffer(m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix.Length);
				additionalLightShadowSliceMatricesStructuredBuffer.SetData(m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix);
				cmd.SetGlobalBuffer(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalLightsWorldToShadow_SSBO, additionalLightShadowSliceMatricesStructuredBuffer);
			}
			else
			{
				cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams, m_AdditionalLightIndexToShadowParams);
				cmd.SetGlobalMatrixArray(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalLightsWorldToShadow, m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix);
			}
			global::UnityEngine.Rendering.Universal.ShadowUtils.GetScaleAndBiasForLinearDistanceFade(m_MaxShadowDistanceSq, m_CascadeBorder, out var scale, out var bias);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowFadeParams, new global::UnityEngine.Vector4(scale, bias, 0f, 0f));
			if (softShadows)
			{
				global::UnityEngine.Vector2 vector = global::UnityEngine.Vector2.one / allocatedShadowAtlasSize;
				global::UnityEngine.Vector2 vector2 = vector * 0.5f;
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset0, new global::UnityEngine.Vector4(0f - vector2.x, 0f - vector2.y, vector2.x, 0f - vector2.y));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset1, new global::UnityEngine.Vector4(0f - vector2.x, vector2.y, vector2.x, vector2.y));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowmapSize, new global::UnityEngine.Vector4(vector.x, vector.y, allocatedShadowAtlasSize.x, allocatedShadowAtlasSize.y));
			}
		}

		private void InitPassData(ref global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.PassData passData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			passData.pass = this;
			passData.lightData = lightData;
			passData.shadowData = shadowData;
			passData.viewMatrix = cameraData.GetViewMatrix();
			passData.stripShadowsOffVariants = cameraData.renderer.stripShadowsOffVariants;
			passData.emptyShadowmap = m_CreateEmptyShadowmap;
			passData.setKeywordForEmptyShadowmap = m_SetKeywordForEmptyShadowmap;
			passData.useStructuredBuffer = m_UseStructuredBuffer;
		}

		private void InitRendererLists(ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.PassData passData, global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, bool useRenderGraph)
		{
			if (m_CreateEmptyShadowmap)
			{
				return;
			}
			for (int i = 0; i < m_ShadowSliceToAdditionalLightIndex.Count; i++)
			{
				int num = m_ShadowSliceToAdditionalLightIndex[i];
				int lightIndex = m_AdditionalLightIndexToVisibleLightIndex[num];
				global::UnityEngine.Rendering.ShadowDrawingSettings shadowDrawingSettings = new global::UnityEngine.Rendering.ShadowDrawingSettings(cullResults, lightIndex);
				shadowDrawingSettings.useRenderingLayerMaskTest = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.useRenderingLayers;
				global::UnityEngine.Rendering.ShadowDrawingSettings settings = shadowDrawingSettings;
				if (useRenderGraph)
				{
					passData.shadowRendererListsHdl[i] = renderGraph.CreateShadowRendererList(ref settings);
				}
				else
				{
					passData.shadowRendererLists[i] = context.CreateShadowRendererList(ref settings);
				}
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph graph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>();
			global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = graph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\AdditionalLightsShadowCasterPass.cs", 1018);
			InitPassData(ref passData, cameraData, lightData, shadowData);
			InitRendererLists(ref universalRenderingData.cullResults, ref passData, default(global::UnityEngine.Rendering.ScriptableRenderContext), graph, useRenderGraph: true);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle;
			if (!m_CreateEmptyShadowmap)
			{
				for (int i = 0; i < m_ShadowSliceToAdditionalLightIndex.Count; i++)
				{
					rasterRenderGraphBuilder.UseRendererList(in passData.shadowRendererListsHdl[i]);
				}
				textureHandle = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(graph, m_AdditionalLightShadowDescriptor, "_AdditionalLightsShadowmapTexture", clear: true, (!global::UnityEngine.Rendering.Universal.ShadowUtils.m_ForceShadowPointSampling) ? global::UnityEngine.FilterMode.Bilinear : global::UnityEngine.FilterMode.Point);
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(textureHandle);
			}
			else
			{
				textureHandle = graph.defaultResources.defaultShadowTexture;
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor = textureHandle.GetDescriptor(graph);
			passData.allocatedShadowAtlasSize = new global::UnityEngine.Vector2Int(descriptor.width, descriptor.height);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			if (textureHandle.IsValid())
			{
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(in textureHandle, global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalLightsShadowmapID);
			}
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				global::UnityEngine.Rendering.RasterCommandBuffer cmd = context.cmd;
				if (!data.emptyShadowmap)
				{
					data.pass.RenderAdditionalShadowmapAtlas(cmd, ref data, useRenderGraph: true);
				}
				else
				{
					if (data.setKeywordForEmptyShadowmap)
					{
						cmd.EnableKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.AdditionalLightShadows);
					}
					SetShadowParamsForEmptyShadowmap(cmd);
				}
			});
			return textureHandle;
		}
	}
}
