namespace UnityEngine.Rendering.Universal
{
	public static class ShadowUtils
	{
		internal static readonly bool m_ForceShadowPointSampling;

		internal const int kMinimumPunctualLightHardShadowResolution = 8;

		internal const int kMinimumPunctualLightSoftShadowResolution = 16;

		static ShadowUtils()
		{
			m_ForceShadowPointSampling = global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.Metal && global::UnityEngine.Rendering.GraphicsSettings.HasShaderDefine(global::UnityEngine.Graphics.activeTier, global::UnityEngine.Rendering.BuiltinShaderDefine.UNITY_METAL_SHADOWS_USE_POINT_FILTERING);
		}

		public static bool ExtractDirectionalLightMatrix(ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.Universal.ShadowData shadowData, int shadowLightIndex, int cascadeIndex, int shadowmapWidth, int shadowmapHeight, int shadowResolution, float shadowNearPlane, out global::UnityEngine.Vector4 cascadeSplitDistance, out global::UnityEngine.Rendering.Universal.ShadowSliceData shadowSliceData, out global::UnityEngine.Matrix4x4 viewMatrix, out global::UnityEngine.Matrix4x4 projMatrix)
		{
			bool result = ExtractDirectionalLightMatrix(ref cullResults, ref shadowData, shadowLightIndex, cascadeIndex, shadowmapWidth, shadowmapHeight, shadowResolution, shadowNearPlane, out cascadeSplitDistance, out shadowSliceData);
			viewMatrix = shadowSliceData.viewMatrix;
			projMatrix = shadowSliceData.projectionMatrix;
			return result;
		}

		public static bool ExtractDirectionalLightMatrix(ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.Universal.ShadowData shadowData, int shadowLightIndex, int cascadeIndex, int shadowmapWidth, int shadowmapHeight, int shadowResolution, float shadowNearPlane, out global::UnityEngine.Vector4 cascadeSplitDistance, out global::UnityEngine.Rendering.Universal.ShadowSliceData shadowSliceData)
		{
			return ExtractDirectionalLightMatrix(ref cullResults, shadowData.universalShadowData, shadowLightIndex, cascadeIndex, shadowmapWidth, shadowmapHeight, shadowResolution, shadowNearPlane, out cascadeSplitDistance, out shadowSliceData);
		}

		public static bool ExtractDirectionalLightMatrix(ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, int shadowLightIndex, int cascadeIndex, int shadowmapWidth, int shadowmapHeight, int shadowResolution, float shadowNearPlane, out global::UnityEngine.Vector4 cascadeSplitDistance, out global::UnityEngine.Rendering.Universal.ShadowSliceData shadowSliceData)
		{
			bool result = cullResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(shadowLightIndex, cascadeIndex, shadowData.mainLightShadowCascadesCount, shadowData.mainLightShadowCascadesSplit, shadowResolution, shadowNearPlane, out shadowSliceData.viewMatrix, out shadowSliceData.projectionMatrix, out shadowSliceData.splitData);
			cascadeSplitDistance = shadowSliceData.splitData.cullingSphere;
			shadowSliceData.offsetX = cascadeIndex % 2 * shadowResolution;
			shadowSliceData.offsetY = cascadeIndex / 2 * shadowResolution;
			shadowSliceData.resolution = shadowResolution;
			shadowSliceData.shadowTransform = GetShadowTransform(shadowSliceData.projectionMatrix, shadowSliceData.viewMatrix);
			shadowSliceData.splitData.shadowCascadeBlendCullingFactor = 1f;
			if (shadowData.mainLightShadowCascadesCount > 1)
			{
				ApplySliceTransform(ref shadowSliceData, shadowmapWidth, shadowmapHeight);
			}
			return result;
		}

		public static bool ExtractSpotLightMatrix(ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.Universal.ShadowData shadowData, int shadowLightIndex, out global::UnityEngine.Matrix4x4 shadowMatrix, out global::UnityEngine.Matrix4x4 viewMatrix, out global::UnityEngine.Matrix4x4 projMatrix, out global::UnityEngine.Rendering.ShadowSplitData splitData)
		{
			return ExtractSpotLightMatrix(ref cullResults, shadowData.universalShadowData, shadowLightIndex, out shadowMatrix, out viewMatrix, out projMatrix, out splitData);
		}

		public static bool ExtractSpotLightMatrix(ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, int shadowLightIndex, out global::UnityEngine.Matrix4x4 shadowMatrix, out global::UnityEngine.Matrix4x4 viewMatrix, out global::UnityEngine.Matrix4x4 projMatrix, out global::UnityEngine.Rendering.ShadowSplitData splitData)
		{
			bool result = cullResults.ComputeSpotShadowMatricesAndCullingPrimitives(shadowLightIndex, out viewMatrix, out projMatrix, out splitData);
			shadowMatrix = GetShadowTransform(projMatrix, viewMatrix);
			return result;
		}

		public static bool ExtractPointLightMatrix(ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.Universal.ShadowData shadowData, int shadowLightIndex, global::UnityEngine.CubemapFace cubemapFace, float fovBias, out global::UnityEngine.Matrix4x4 shadowMatrix, out global::UnityEngine.Matrix4x4 viewMatrix, out global::UnityEngine.Matrix4x4 projMatrix, out global::UnityEngine.Rendering.ShadowSplitData splitData)
		{
			return ExtractPointLightMatrix(ref cullResults, shadowData.universalShadowData, shadowLightIndex, cubemapFace, fovBias, out shadowMatrix, out viewMatrix, out projMatrix, out splitData);
		}

		public static bool ExtractPointLightMatrix(ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, int shadowLightIndex, global::UnityEngine.CubemapFace cubemapFace, float fovBias, out global::UnityEngine.Matrix4x4 shadowMatrix, out global::UnityEngine.Matrix4x4 viewMatrix, out global::UnityEngine.Matrix4x4 projMatrix, out global::UnityEngine.Rendering.ShadowSplitData splitData)
		{
			bool result = cullResults.ComputePointShadowMatricesAndCullingPrimitives(shadowLightIndex, cubemapFace, fovBias, out viewMatrix, out projMatrix, out splitData);
			viewMatrix.m10 = 0f - viewMatrix.m10;
			viewMatrix.m11 = 0f - viewMatrix.m11;
			viewMatrix.m12 = 0f - viewMatrix.m12;
			viewMatrix.m13 = 0f - viewMatrix.m13;
			shadowMatrix = GetShadowTransform(projMatrix, viewMatrix);
			return result;
		}

		public static void RenderShadowSlice(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.ShadowSliceData shadowSliceData, ref global::UnityEngine.Rendering.ShadowDrawingSettings settings, global::UnityEngine.Matrix4x4 proj, global::UnityEngine.Matrix4x4 view)
		{
			cmd.SetGlobalDepthBias(1f, 2.5f);
			cmd.SetViewport(new global::UnityEngine.Rect(shadowSliceData.offsetX, shadowSliceData.offsetY, shadowSliceData.resolution, shadowSliceData.resolution));
			cmd.SetViewProjectionMatrices(view, proj);
			global::UnityEngine.Rendering.RendererList rendererList = context.CreateShadowRendererList(ref settings);
			cmd.DrawRendererList(rendererList);
			cmd.DisableScissorRect();
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			cmd.SetGlobalDepthBias(0f, 0f);
		}

		internal static void RenderShadowSlice(global::UnityEngine.Rendering.RasterCommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.ShadowSliceData shadowSliceData, ref global::UnityEngine.Rendering.RendererList shadowRendererList, global::UnityEngine.Matrix4x4 proj, global::UnityEngine.Matrix4x4 view)
		{
			cmd.SetGlobalDepthBias(1f, 2.5f);
			cmd.SetViewport(new global::UnityEngine.Rect(shadowSliceData.offsetX, shadowSliceData.offsetY, shadowSliceData.resolution, shadowSliceData.resolution));
			cmd.SetViewProjectionMatrices(view, proj);
			if (shadowRendererList.isValid)
			{
				cmd.DrawRendererList(shadowRendererList);
			}
			cmd.DisableScissorRect();
			cmd.SetGlobalDepthBias(0f, 0f);
		}

		public static void RenderShadowSlice(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.ShadowSliceData shadowSliceData, ref global::UnityEngine.Rendering.ShadowDrawingSettings settings)
		{
			RenderShadowSlice(cmd, ref context, ref shadowSliceData, ref settings, shadowSliceData.projectionMatrix, shadowSliceData.viewMatrix);
		}

		public static int GetMaxTileResolutionInAtlas(int atlasWidth, int atlasHeight, int tileCount)
		{
			int num = global::UnityEngine.Mathf.Min(atlasWidth, atlasHeight);
			for (int num2 = atlasWidth / num * atlasHeight / num; num2 < tileCount; num2 = atlasWidth / num * atlasHeight / num)
			{
				num >>= 1;
			}
			return num;
		}

		public static void ApplySliceTransform(ref global::UnityEngine.Rendering.Universal.ShadowSliceData shadowSliceData, int atlasWidth, int atlasHeight)
		{
			global::UnityEngine.Matrix4x4 identity = global::UnityEngine.Matrix4x4.identity;
			float num = 1f / (float)atlasWidth;
			float num2 = 1f / (float)atlasHeight;
			identity.m00 = (float)shadowSliceData.resolution * num;
			identity.m11 = (float)shadowSliceData.resolution * num2;
			identity.m03 = (float)shadowSliceData.offsetX * num;
			identity.m13 = (float)shadowSliceData.offsetY * num2;
			shadowSliceData.shadowTransform = identity * shadowSliceData.shadowTransform;
		}

		public static global::UnityEngine.Vector4 GetShadowBias(ref global::UnityEngine.Rendering.VisibleLight shadowLight, int shadowLightIndex, ref global::UnityEngine.Rendering.Universal.ShadowData shadowData, global::UnityEngine.Matrix4x4 lightProjectionMatrix, float shadowResolution)
		{
			return GetShadowBias(ref shadowLight, shadowLightIndex, shadowData.bias, shadowData.supportsSoftShadows, lightProjectionMatrix, shadowResolution);
		}

		public static global::UnityEngine.Vector4 GetShadowBias(ref global::UnityEngine.Rendering.VisibleLight shadowLight, int shadowLightIndex, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, global::UnityEngine.Matrix4x4 lightProjectionMatrix, float shadowResolution)
		{
			return GetShadowBias(ref shadowLight, shadowLightIndex, shadowData.bias, shadowData.supportsSoftShadows, lightProjectionMatrix, shadowResolution);
		}

		private static global::UnityEngine.Vector4 GetShadowBias(ref global::UnityEngine.Rendering.VisibleLight shadowLight, int shadowLightIndex, global::System.Collections.Generic.List<global::UnityEngine.Vector4> bias, bool supportsSoftShadows, global::UnityEngine.Matrix4x4 lightProjectionMatrix, float shadowResolution)
		{
			if (shadowLightIndex < 0 || shadowLightIndex >= bias.Count)
			{
				global::UnityEngine.Debug.LogWarning($"{shadowLightIndex} is not a valid light index.");
				return global::UnityEngine.Vector4.zero;
			}
			float num;
			if (shadowLight.lightType == global::UnityEngine.LightType.Directional)
			{
				num = 2f / lightProjectionMatrix.m00;
			}
			else if (shadowLight.lightType == global::UnityEngine.LightType.Spot)
			{
				num = global::UnityEngine.Mathf.Tan(shadowLight.spotAngle * 0.5f * (global::System.MathF.PI / 180f)) * shadowLight.range;
			}
			else if (shadowLight.lightType == global::UnityEngine.LightType.Point)
			{
				float pointLightShadowFrustumFovBiasInDegrees = global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.GetPointLightShadowFrustumFovBiasInDegrees((int)shadowResolution, shadowLight.light.shadows == global::UnityEngine.LightShadows.Soft);
				num = global::UnityEngine.Mathf.Tan((90f + pointLightShadowFrustumFovBiasInDegrees) * 0.5f * (global::System.MathF.PI / 180f)) * shadowLight.range;
			}
			else
			{
				global::UnityEngine.Debug.LogWarning("Only point, spot and directional shadow casters are supported in universal pipeline");
				num = 0f;
			}
			float num2 = num / shadowResolution;
			float num3 = (0f - bias[shadowLightIndex].x) * num2;
			float num4 = (0f - bias[shadowLightIndex].y) * num2;
			if (shadowLight.lightType == global::UnityEngine.LightType.Point)
			{
				num4 = 0f;
			}
			if (supportsSoftShadows && shadowLight.light.shadows == global::UnityEngine.LightShadows.Soft)
			{
				global::UnityEngine.Rendering.Universal.SoftShadowQuality softShadowQuality = global::UnityEngine.Rendering.Universal.SoftShadowQuality.Medium;
				if (shadowLight.light.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData>(out var component))
				{
					softShadowQuality = component.softShadowQuality;
				}
				float num5 = 2.5f;
				switch (softShadowQuality)
				{
				case global::UnityEngine.Rendering.Universal.SoftShadowQuality.High:
					num5 = 3.5f;
					break;
				case global::UnityEngine.Rendering.Universal.SoftShadowQuality.Medium:
					num5 = 2.5f;
					break;
				case global::UnityEngine.Rendering.Universal.SoftShadowQuality.Low:
					num5 = 1.5f;
					break;
				}
				num3 *= num5;
				num4 *= num5;
			}
			return new global::UnityEngine.Vector4(num3, num4, (float)shadowLight.lightType, 0f);
		}

		internal static void GetScaleAndBiasForLinearDistanceFade(float fadeDistance, float border, out float scale, out float bias)
		{
			if (border < 0.0001f)
			{
				bias = (0f - fadeDistance) * (scale = 1000f);
				return;
			}
			border = 1f - border;
			border *= border;
			float num = border * fadeDistance;
			scale = 1f / (fadeDistance - num);
			bias = (0f - num) / (fadeDistance - num);
		}

		public static void SetupShadowCasterConstantBuffer(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.VisibleLight shadowLight, global::UnityEngine.Vector4 shadowBias)
		{
			SetupShadowCasterConstantBuffer(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd), ref shadowLight, shadowBias);
		}

		internal static void SetupShadowCasterConstantBuffer(global::UnityEngine.Rendering.RasterCommandBuffer cmd, ref global::UnityEngine.Rendering.VisibleLight shadowLight, global::UnityEngine.Vector4 shadowBias)
		{
			SetShadowBias(cmd, shadowBias);
			global::UnityEngine.Vector3 lightDirection = -shadowLight.localToWorldMatrix.GetColumn(2);
			SetLightDirection(cmd, lightDirection);
			global::UnityEngine.Vector3 lightPosition = shadowLight.localToWorldMatrix.GetColumn(3);
			SetLightPosition(cmd, lightPosition);
		}

		internal static void SetShadowBias(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Vector4 shadowBias)
		{
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.shadowBias, shadowBias);
		}

		internal static void SetLightDirection(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Vector3 lightDirection)
		{
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.lightDirection, new global::UnityEngine.Vector4(lightDirection.x, lightDirection.y, lightDirection.z, 0f));
		}

		internal static void SetLightPosition(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Vector3 lightPosition)
		{
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.lightPosition, new global::UnityEngine.Vector4(lightPosition.x, lightPosition.y, lightPosition.z, 1f));
		}

		internal static void SetCameraPosition(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Vector3 worldSpaceCameraPos)
		{
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.worldSpaceCameraPos, worldSpaceCameraPos);
		}

		internal static void SetWorldToCameraAndCameraToWorldMatrices(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Matrix4x4 viewMatrix)
		{
			global::UnityEngine.Matrix4x4 value = global::UnityEngine.Matrix4x4.Scale(new global::UnityEngine.Vector3(1f, 1f, -1f)) * viewMatrix;
			global::UnityEngine.Matrix4x4 inverse = value.inverse;
			cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.worldToCameraMatrix, value);
			cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.cameraToWorldMatrix, inverse);
		}

		private static global::UnityEngine.RenderTextureDescriptor GetTemporaryShadowTextureDescriptor(int width, int height, int bits)
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencilFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthStencilFormat(bits, 0);
			global::UnityEngine.RenderTextureDescriptor result = new global::UnityEngine.RenderTextureDescriptor(width, height, global::UnityEngine.Experimental.Rendering.GraphicsFormat.None, depthStencilFormat);
			result.shadowSamplingMode = ((!global::UnityEngine.Rendering.Universal.RenderingUtils.SupportsRenderTextureFormat(global::UnityEngine.RenderTextureFormat.Shadowmap)) ? global::UnityEngine.Rendering.ShadowSamplingMode.None : global::UnityEngine.Rendering.ShadowSamplingMode.CompareDepths);
			return result;
		}

		[global::System.Obsolete("Use AllocShadowRT or ShadowRTReAllocateIfNeeded. #from(2022.1) #breakingFrom(2023.1)", true)]
		public static global::UnityEngine.RenderTexture GetTemporaryShadowTexture(int width, int height, int bits)
		{
			global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(GetTemporaryShadowTextureDescriptor(width, height, bits));
			temporary.filterMode = ((!m_ForceShadowPointSampling) ? global::UnityEngine.FilterMode.Bilinear : global::UnityEngine.FilterMode.Point);
			temporary.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
			return temporary;
		}

		public static bool ShadowRTNeedsReAlloc(global::UnityEngine.Rendering.RTHandle handle, int width, int height, int bits, int anisoLevel, float mipMapBias, string name)
		{
			if (handle == null || handle.rt == null)
			{
				return true;
			}
			global::UnityEngine.RenderTextureDescriptor temporaryShadowTextureDescriptor = GetTemporaryShadowTextureDescriptor(width, height, bits);
			if (m_ForceShadowPointSampling)
			{
				if (handle.rt.filterMode != global::UnityEngine.FilterMode.Point)
				{
					return true;
				}
			}
			else if (handle.rt.filterMode != global::UnityEngine.FilterMode.Bilinear)
			{
				return true;
			}
			return global::UnityEngine.Rendering.Universal.RenderingUtils.RTHandleNeedsReAlloc(handle, global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(temporaryShadowTextureDescriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit, anisoLevel, mipMapBias, (!m_ForceShadowPointSampling) ? global::UnityEngine.FilterMode.Bilinear : global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Clamp, name), scaled: false);
		}

		public static global::UnityEngine.Rendering.RTHandle AllocShadowRT(int width, int height, int bits, int anisoLevel, float mipMapBias, string name)
		{
			return global::UnityEngine.Rendering.RTHandles.Alloc(GetTemporaryShadowTextureDescriptor(width, height, bits), (!m_ForceShadowPointSampling) ? global::UnityEngine.FilterMode.Bilinear : global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Clamp, isShadowMap: true, 1, 0f, name);
		}

		public static bool ShadowRTReAllocateIfNeeded(ref global::UnityEngine.Rendering.RTHandle handle, int width, int height, int bits, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			if (ShadowRTNeedsReAlloc(handle, width, height, bits, anisoLevel, mipMapBias, name))
			{
				handle?.Release();
				handle = AllocShadowRT(width, height, bits, anisoLevel, mipMapBias, name);
				return true;
			}
			return false;
		}

		private static global::UnityEngine.Matrix4x4 GetShadowTransform(global::UnityEngine.Matrix4x4 proj, global::UnityEngine.Matrix4x4 view)
		{
			if (global::UnityEngine.SystemInfo.usesReversedZBuffer)
			{
				proj.m20 = 0f - proj.m20;
				proj.m21 = 0f - proj.m21;
				proj.m22 = 0f - proj.m22;
				proj.m23 = 0f - proj.m23;
			}
			global::UnityEngine.Matrix4x4 matrix4x = proj * view;
			global::UnityEngine.Matrix4x4 identity = global::UnityEngine.Matrix4x4.identity;
			identity.m00 = 0.5f;
			identity.m11 = 0.5f;
			identity.m22 = 0.5f;
			identity.m03 = 0.5f;
			identity.m23 = 0.5f;
			identity.m13 = 0.5f;
			return identity * matrix4x;
		}

		internal static float SoftShadowQualityToShaderProperty(global::UnityEngine.Light light, bool softShadowsEnabled)
		{
			float num = (softShadowsEnabled ? 1f : 0f);
			if (light.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData>(out var component))
			{
				num *= (float)global::System.Math.Max((int)((component.softShadowQuality != global::UnityEngine.Rendering.Universal.SoftShadowQuality.UsePipelineSettings) ? new global::UnityEngine.Rendering.Universal.SoftShadowQuality?(component.softShadowQuality) : global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset?.softShadowQuality).Value, 1);
			}
			return num;
		}

		internal static bool SupportsPerLightSoftShadowQuality()
		{
			return true;
		}

		internal static void SetPerLightSoftShadowKeyword(global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool hasSoftShadows)
		{
			if (SupportsPerLightSoftShadowQuality())
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadows, hasSoftShadows);
			}
		}

		internal static void SetSoftShadowQualityShaderKeywords(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData)
		{
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadows, shadowData.isKeywordSoftShadowsEnabled);
			if (SupportsPerLightSoftShadowQuality())
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsLow, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsMedium, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsHigh, value: false);
				return;
			}
			if (shadowData.isKeywordSoftShadowsEnabled)
			{
				global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset;
				if ((object)asset != null && asset.softShadowQuality == global::UnityEngine.Rendering.Universal.SoftShadowQuality.Low)
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsLow, value: true);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsMedium, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsHigh, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadows, value: false);
					return;
				}
			}
			if (shadowData.isKeywordSoftShadowsEnabled)
			{
				global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset2 = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset;
				if ((object)asset2 != null && asset2.softShadowQuality == global::UnityEngine.Rendering.Universal.SoftShadowQuality.Medium)
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsLow, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsMedium, value: true);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsHigh, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadows, value: false);
					return;
				}
			}
			if (shadowData.isKeywordSoftShadowsEnabled)
			{
				global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset3 = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset;
				if ((object)asset3 != null && asset3.softShadowQuality == global::UnityEngine.Rendering.Universal.SoftShadowQuality.High)
				{
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsLow, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsMedium, value: false);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsHigh, value: true);
					cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadows, value: false);
				}
			}
		}

		internal static bool IsValidShadowCastingLight(global::UnityEngine.Rendering.Universal.UniversalLightData lightData, int i)
		{
			ref global::UnityEngine.Rendering.VisibleLight reference = ref lightData.visibleLights.UnsafeElementAt(i);
			global::UnityEngine.Light light = reference.light;
			if (light == null)
			{
				return false;
			}
			return IsValidShadowCastingLight(lightData, i, reference.lightType, light.shadows, light.shadowStrength);
		}

		internal static bool IsValidShadowCastingLight(global::UnityEngine.Rendering.Universal.UniversalLightData lightData, int i, global::UnityEngine.LightType lightType, global::UnityEngine.LightShadows lightShadows, float shadowStrength)
		{
			if (i == lightData.mainLightIndex)
			{
				return false;
			}
			if (lightType == global::UnityEngine.LightType.Directional)
			{
				return false;
			}
			if (lightShadows != global::UnityEngine.LightShadows.None)
			{
				return shadowStrength > 0f;
			}
			return false;
		}

		internal static int GetPunctualLightShadowSlicesCount(in global::UnityEngine.LightType lightType)
		{
			return lightType switch
			{
				global::UnityEngine.LightType.Spot => 1, 
				global::UnityEngine.LightType.Point => 6, 
				_ => 0, 
			};
		}

		internal static bool FastApproximately(float a, float b)
		{
			return global::UnityEngine.Mathf.Abs(a - b) < 1E-06f;
		}

		internal static bool FastApproximately(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			if (FastApproximately(a.x, b.x) && FastApproximately(a.y, b.y) && FastApproximately(a.z, b.z))
			{
				return FastApproximately(a.w, b.w);
			}
			return false;
		}

		internal static int MinimalPunctualLightShadowResolution(bool softShadow)
		{
			if (!softShadow)
			{
				return 8;
			}
			return 16;
		}
	}
}
