namespace UnityEngine.Rendering.Universal
{
	internal static class ShadowCulling
	{
		private static readonly global::UnityEngine.Rendering.ProfilingSampler computeShadowCasterCullingInfosMarker = new global::UnityEngine.Rendering.ProfilingSampler("UniversalRenderPipeline.ComputeShadowCasterCullingInfos");

		public static global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos> CullShadowCasters(ref global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, ref global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout shadowAtlasLayout, ref global::UnityEngine.Rendering.CullingResults cullResults)
		{
			ComputeShadowCasterCullingInfos(shadowData, ref shadowAtlasLayout, ref cullResults, out var shadowCullingInfos, out var urpVisibleLightsShadowCullingInfos);
			context.CullShadowCasters(cullResults, shadowCullingInfos);
			return urpVisibleLightsShadowCullingInfos;
		}

		private static void ComputeShadowCasterCullingInfos(global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, ref global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout shadowAtlasLayout, ref global::UnityEngine.Rendering.CullingResults cullingResults, out global::UnityEngine.Rendering.ShadowCastersCullingInfos shadowCullingInfos, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos> urpVisibleLightsShadowCullingInfos)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(computeShadowCasterCullingInfosMarker))
			{
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = cullingResults.visibleLights;
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ShadowSplitData> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ShadowSplitData>(visibleLights.Length * 6, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.LightShadowCasterCullingInfo> perLightInfos = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.LightShadowCasterCullingInfo>(visibleLights.Length, global::Unity.Collections.Allocator.Temp);
				urpVisibleLightsShadowCullingInfos = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos>(visibleLights.Length, global::Unity.Collections.Allocator.Temp);
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < visibleLights.Length; i++)
				{
					ref global::UnityEngine.Rendering.VisibleLight reference = ref cullingResults.visibleLights.UnsafeElementAt(i);
					global::UnityEngine.LightType lightType = reference.lightType;
					global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowSliceData> slices = default(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowSliceData>);
					uint num3 = 0u;
					switch (lightType)
					{
					case global::UnityEngine.LightType.Directional:
					{
						if (!shadowData.supportsMainLightShadows)
						{
							continue;
						}
						int mainLightShadowCascadesCount = shadowData.mainLightShadowCascadesCount;
						int mainLightRenderTargetWidth = shadowData.mainLightRenderTargetWidth;
						int mainLightRenderTargetHeight = shadowData.mainLightRenderTargetHeight;
						int mainLightShadowResolution = shadowData.mainLightShadowResolution;
						slices = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowSliceData>(mainLightShadowCascadesCount, global::Unity.Collections.Allocator.Temp);
						num3 = 0u;
						for (int j = 0; j < mainLightShadowCascadesCount; j++)
						{
							global::UnityEngine.Rendering.Universal.ShadowSliceData shadowSliceData = default(global::UnityEngine.Rendering.Universal.ShadowSliceData);
							if (global::UnityEngine.Rendering.Universal.ShadowUtils.ExtractDirectionalLightMatrix(ref cullingResults, shadowData, i, j, mainLightRenderTargetWidth, mainLightRenderTargetHeight, mainLightShadowResolution, reference.light.shadowNearPlane, out var _, out shadowSliceData))
							{
								num3 |= (uint)(1 << j);
							}
							slices[j] = shadowSliceData;
							nativeArray[num2 + j] = shadowSliceData.splitData;
						}
						break;
					}
					case global::UnityEngine.LightType.Point:
					{
						if (!shadowData.supportsAdditionalLightShadows || !shadowAtlasLayout.HasSpaceForLight(i))
						{
							continue;
						}
						int punctualLightShadowSlicesCount = global::UnityEngine.Rendering.Universal.ShadowUtils.GetPunctualLightShadowSlicesCount(in lightType);
						ushort allocatedResolution = shadowAtlasLayout.GetSliceShadowResolutionRequest(i, 0).allocatedResolution;
						bool shadowFiltering = reference.light.shadows == global::UnityEngine.LightShadows.Soft;
						float pointLightShadowFrustumFovBiasInDegrees = global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.GetPointLightShadowFrustumFovBiasInDegrees(allocatedResolution, shadowFiltering);
						slices = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowSliceData>(punctualLightShadowSlicesCount, global::Unity.Collections.Allocator.Temp);
						num3 = 0u;
						for (int k = 0; k < punctualLightShadowSlicesCount; k++)
						{
							global::UnityEngine.Rendering.Universal.ShadowSliceData value2 = default(global::UnityEngine.Rendering.Universal.ShadowSliceData);
							if (global::UnityEngine.Rendering.Universal.ShadowUtils.ExtractPointLightMatrix(ref cullingResults, shadowData, i, (global::UnityEngine.CubemapFace)k, pointLightShadowFrustumFovBiasInDegrees, out value2.shadowTransform, out value2.viewMatrix, out value2.projectionMatrix, out value2.splitData))
							{
								num3 |= (uint)(1 << k);
							}
							slices[k] = value2;
							nativeArray[num2 + k] = value2.splitData;
						}
						break;
					}
					case global::UnityEngine.LightType.Spot:
					{
						if (!shadowData.supportsAdditionalLightShadows || !shadowAtlasLayout.HasSpaceForLight(i))
						{
							continue;
						}
						slices = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowSliceData>(1, global::Unity.Collections.Allocator.Temp);
						num3 = 0u;
						global::UnityEngine.Rendering.Universal.ShadowSliceData value = default(global::UnityEngine.Rendering.Universal.ShadowSliceData);
						if (global::UnityEngine.Rendering.Universal.ShadowUtils.ExtractSpotLightMatrix(ref cullingResults, shadowData, i, out value.shadowTransform, out value.viewMatrix, out value.projectionMatrix, out value.splitData))
						{
							num3 |= 1;
						}
						slices[0] = value;
						nativeArray[num2] = value.splitData;
						break;
					}
					}
					urpVisibleLightsShadowCullingInfos[i] = new global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos
					{
						slices = slices,
						slicesValidMask = num3
					};
					perLightInfos[i] = new global::UnityEngine.Rendering.LightShadowCasterCullingInfo
					{
						splitRange = new global::UnityEngine.RangeInt(num2, slices.Length),
						projectionType = GetCullingProjectionType(lightType)
					};
					num2 += slices.Length;
					num += slices.Length;
				}
				shadowCullingInfos = default(global::UnityEngine.Rendering.ShadowCastersCullingInfos);
				shadowCullingInfos.splitBuffer = nativeArray.GetSubArray(0, num);
				shadowCullingInfos.perLightInfos = perLightInfos;
			}
		}

		private static global::UnityEngine.Rendering.BatchCullingProjectionType GetCullingProjectionType(global::UnityEngine.LightType type)
		{
			return type switch
			{
				global::UnityEngine.LightType.Point => global::UnityEngine.Rendering.BatchCullingProjectionType.Perspective, 
				global::UnityEngine.LightType.Spot => global::UnityEngine.Rendering.BatchCullingProjectionType.Perspective, 
				global::UnityEngine.LightType.Directional => global::UnityEngine.Rendering.BatchCullingProjectionType.Orthographic, 
				_ => global::UnityEngine.Rendering.BatchCullingProjectionType.Unknown, 
			};
		}
	}
}
