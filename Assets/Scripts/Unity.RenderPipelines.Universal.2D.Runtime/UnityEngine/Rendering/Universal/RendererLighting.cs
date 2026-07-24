namespace UnityEngine.Rendering.Universal
{
	internal static class RendererLighting
	{
		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Draw Normals");

		private static readonly global::UnityEngine.Rendering.ShaderTagId k_NormalsRenderingPassName = new global::UnityEngine.Rendering.ShaderTagId("NormalsRendering");

		public static readonly global::UnityEngine.Color k_NormalClearColor = new global::UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f);

		private static readonly string k_UsePointLightCookiesKeyword = "USE_POINT_LIGHT_COOKIES";

		private static readonly string k_LightQualityFastKeyword = "LIGHT_QUALITY_FAST";

		private static readonly string k_UseNormalMap = "USE_NORMAL_MAP";

		private static readonly string k_UseShadowMap = "USE_SHADOW_MAP";

		private static readonly string k_UseAdditiveBlendingKeyword = "USE_ADDITIVE_BLENDING";

		private static readonly string k_UseVolumetric = "USE_VOLUMETRIC";

		private static readonly string[] k_UseBlendStyleKeywords = new string[4] { "USE_SHAPE_LIGHT_TYPE_0", "USE_SHAPE_LIGHT_TYPE_1", "USE_SHAPE_LIGHT_TYPE_2", "USE_SHAPE_LIGHT_TYPE_3" };

		private static readonly int[] k_BlendFactorsPropIDs = new int[4]
		{
			global::UnityEngine.Shader.PropertyToID("_ShapeLightBlendFactors0"),
			global::UnityEngine.Shader.PropertyToID("_ShapeLightBlendFactors1"),
			global::UnityEngine.Shader.PropertyToID("_ShapeLightBlendFactors2"),
			global::UnityEngine.Shader.PropertyToID("_ShapeLightBlendFactors3")
		};

		private static readonly int[] k_MaskFilterPropIDs = new int[4]
		{
			global::UnityEngine.Shader.PropertyToID("_ShapeLightMaskFilter0"),
			global::UnityEngine.Shader.PropertyToID("_ShapeLightMaskFilter1"),
			global::UnityEngine.Shader.PropertyToID("_ShapeLightMaskFilter2"),
			global::UnityEngine.Shader.PropertyToID("_ShapeLightMaskFilter3")
		};

		private static readonly int[] k_InvertedFilterPropIDs = new int[4]
		{
			global::UnityEngine.Shader.PropertyToID("_ShapeLightInvertedFilter0"),
			global::UnityEngine.Shader.PropertyToID("_ShapeLightInvertedFilter1"),
			global::UnityEngine.Shader.PropertyToID("_ShapeLightInvertedFilter2"),
			global::UnityEngine.Shader.PropertyToID("_ShapeLightInvertedFilter3")
		};

		public static readonly string[] k_ShapeLightTextureIDs = new string[4] { "_ShapeLightTexture0", "_ShapeLightTexture1", "_ShapeLightTexture2", "_ShapeLightTexture3" };

		private static global::UnityEngine.Experimental.Rendering.GraphicsFormat s_RenderTextureFormatToUse = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;

		private static bool s_HasSetupRenderTextureFormatToUse;

		private static readonly int k_SrcBlendID = global::UnityEngine.Shader.PropertyToID("_SrcBlend");

		private static readonly int k_DstBlendID = global::UnityEngine.Shader.PropertyToID("_DstBlend");

		private static readonly int k_CookieTexID = global::UnityEngine.Shader.PropertyToID("_CookieTex");

		private static readonly int k_PointLightCookieTexID = global::UnityEngine.Shader.PropertyToID("_PointLightCookieTex");

		private static readonly int k_L2DInvMatrix = global::UnityEngine.Shader.PropertyToID("L2DInvMatrix");

		private static readonly int k_L2DColor = global::UnityEngine.Shader.PropertyToID("L2DColor");

		private static readonly int k_L2DPosition = global::UnityEngine.Shader.PropertyToID("L2DPosition");

		private static readonly int k_L2DFalloffIntensity = global::UnityEngine.Shader.PropertyToID("L2DFalloffIntensity");

		private static readonly int k_L2DFalloffDistance = global::UnityEngine.Shader.PropertyToID("L2DFalloffDistance");

		private static readonly int k_L2DOuterAngle = global::UnityEngine.Shader.PropertyToID("L2DOuterAngle");

		private static readonly int k_L2DInnerAngle = global::UnityEngine.Shader.PropertyToID("L2DInnerAngle");

		private static readonly int k_L2DInnerRadiusMult = global::UnityEngine.Shader.PropertyToID("L2DInnerRadiusMult");

		private static readonly int k_L2DVolumeOpacity = global::UnityEngine.Shader.PropertyToID("L2DVolumeOpacity");

		private static readonly int k_L2DShadowIntensity = global::UnityEngine.Shader.PropertyToID("L2DShadowIntensity");

		private static readonly int k_L2DLightType = global::UnityEngine.Shader.PropertyToID("L2DLightType");

		internal static global::UnityEngine.Rendering.Universal.LightBatch lightBatch = new global::UnityEngine.Rendering.Universal.LightBatch();

		internal static global::UnityEngine.Experimental.Rendering.GraphicsFormat GetRenderTextureFormat()
		{
			if (!s_HasSetupRenderTextureFormatToUse)
			{
				if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend))
				{
					s_RenderTextureFormatToUse = global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32;
				}
				else if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend))
				{
					s_RenderTextureFormatToUse = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat;
				}
				s_HasSetupRenderTextureFormatToUse = true;
			}
			return s_RenderTextureFormatToUse;
		}

		internal static void EnableBlendStyle(global::UnityEngine.Rendering.IRasterCommandBuffer cmd, int blendStyleIndex, bool enabled)
		{
			string keyword = k_UseBlendStyleKeywords[blendStyleIndex];
			if (enabled)
			{
				cmd.EnableShaderKeyword(keyword);
			}
			else
			{
				cmd.DisableShaderKeyword(keyword);
			}
		}

		internal static void DisableAllKeywords(global::UnityEngine.Rendering.IRasterCommandBuffer cmd)
		{
			string[] array = k_UseBlendStyleKeywords;
			foreach (string keyword in array)
			{
				cmd.DisableShaderKeyword(keyword);
			}
		}

		internal static void GetTransparencySortingMode(global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, global::UnityEngine.Camera camera, ref global::UnityEngine.Rendering.SortingSettings sortingSettings)
		{
			global::UnityEngine.TransparencySortMode transparencySortMode = rendererData.transparencySortMode;
			if (transparencySortMode == global::UnityEngine.TransparencySortMode.Default)
			{
				transparencySortMode = ((!camera.orthographic) ? global::UnityEngine.TransparencySortMode.Perspective : global::UnityEngine.TransparencySortMode.Orthographic);
			}
			switch (transparencySortMode)
			{
			case global::UnityEngine.TransparencySortMode.Perspective:
				sortingSettings.distanceMetric = global::UnityEngine.Rendering.DistanceMetric.Perspective;
				break;
			case global::UnityEngine.TransparencySortMode.Orthographic:
				sortingSettings.distanceMetric = global::UnityEngine.Rendering.DistanceMetric.Orthographic;
				break;
			default:
				sortingSettings.distanceMetric = global::UnityEngine.Rendering.DistanceMetric.CustomAxis;
				sortingSettings.customAxis = rendererData.transparencySortAxis;
				break;
			}
		}

		internal static bool CanCastShadows(global::UnityEngine.Rendering.Universal.Light2D light, int layerToRender)
		{
			if (light.shadowsEnabled && light.shadowIntensity > 0f)
			{
				return light.IsLitLayer(layerToRender);
			}
			return false;
		}

		internal static void SetLightShaderGlobals(global::UnityEngine.Rendering.IRasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Light2DBlendStyle[] lightBlendStyles, int[] blendStyleIndices)
		{
			foreach (int num in blendStyleIndices)
			{
				if (num < k_BlendFactorsPropIDs.Length)
				{
					global::UnityEngine.Rendering.Universal.Light2DBlendStyle light2DBlendStyle = lightBlendStyles[num];
					cmd.SetGlobalVector(k_BlendFactorsPropIDs[num], light2DBlendStyle.blendFactors);
					cmd.SetGlobalVector(k_MaskFilterPropIDs[num], light2DBlendStyle.maskTextureChannelFilter.mask);
					cmd.SetGlobalVector(k_InvertedFilterPropIDs[num], light2DBlendStyle.maskTextureChannelFilter.inverted);
					continue;
				}
				break;
			}
		}

		private static float GetNormalizedInnerRadius(global::UnityEngine.Rendering.Universal.Light2D light)
		{
			return light.pointLightInnerRadius / light.pointLightOuterRadius;
		}

		private static float GetNormalizedAngle(float angle)
		{
			return angle / 360f;
		}

		private static void GetScaledLightInvMatrix(global::UnityEngine.Rendering.Universal.Light2D light, out global::UnityEngine.Matrix4x4 retMatrix)
		{
			float pointLightOuterRadius = light.pointLightOuterRadius;
			global::UnityEngine.Vector3 one = global::UnityEngine.Vector3.one;
			global::UnityEngine.Vector3 s = new global::UnityEngine.Vector3(one.x * pointLightOuterRadius, one.y * pointLightOuterRadius, one.z * pointLightOuterRadius);
			global::UnityEngine.Transform transform = light.transform;
			global::UnityEngine.Matrix4x4 m = global::UnityEngine.Matrix4x4.TRS(transform.position, transform.rotation, s);
			retMatrix = global::UnityEngine.Matrix4x4.Inverse(m);
		}

		internal static void SetPerLightShaderGlobals(global::UnityEngine.Rendering.IRasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Light2D light, int slot, bool isVolumetric, bool hasShadows, bool batchingSupported)
		{
			global::UnityEngine.Color value = light.intensity * light.color.a * light.color;
			value.a = 1f;
			float num = (light.volumetricEnabled ? light.volumeIntensity : 1f);
			if (batchingSupported)
			{
				global::UnityEngine.Rendering.Universal.PerLight2D light2 = lightBatch.GetLight(slot);
				light2.Position = new global::Unity.Mathematics.float4(light.transform.position, light.normalMapDistance);
				light2.FalloffIntensity = light.falloffIntensity;
				light2.FalloffDistance = light.shapeLightFalloffSize;
				light2.Color = new global::Unity.Mathematics.float4(value.r, value.g, value.b, value.a);
				light2.VolumeOpacity = num;
				light2.LightType = (int)light.lightType;
				light2.ShadowIntensity = 1f;
				if (hasShadows)
				{
					light2.ShadowIntensity = (isVolumetric ? (1f - light.shadowVolumeIntensity) : (1f - light.shadowIntensity));
				}
				lightBatch.SetLight(slot, light2);
			}
			else
			{
				cmd.SetGlobalVector(k_L2DPosition, new global::Unity.Mathematics.float4(light.transform.position, light.normalMapDistance));
				cmd.SetGlobalFloat(k_L2DFalloffIntensity, light.falloffIntensity);
				cmd.SetGlobalFloat(k_L2DFalloffDistance, light.shapeLightFalloffSize);
				cmd.SetGlobalColor(k_L2DColor, value);
				cmd.SetGlobalFloat(k_L2DVolumeOpacity, num);
				cmd.SetGlobalInt(k_L2DLightType, (int)light.lightType);
				cmd.SetGlobalFloat(k_L2DShadowIntensity, (!hasShadows) ? 1f : (isVolumetric ? (1f - light.shadowVolumeIntensity) : (1f - light.shadowIntensity)));
			}
			if (hasShadows)
			{
				global::UnityEngine.Rendering.Universal.ShadowRendering.SetGlobalShadowProp(cmd);
			}
		}

		internal static void SetPerPointLightShaderGlobals(global::UnityEngine.Rendering.IRasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Light2D light, int slot, bool batchingSupported)
		{
			GetScaledLightInvMatrix(light, out var retMatrix);
			float normalizedInnerRadius = GetNormalizedInnerRadius(light);
			float normalizedAngle = GetNormalizedAngle(light.pointLightInnerAngle);
			float normalizedAngle2 = GetNormalizedAngle(light.pointLightOuterAngle);
			float num = 1f / (1f - normalizedInnerRadius);
			if (batchingSupported)
			{
				global::UnityEngine.Rendering.Universal.PerLight2D light2 = lightBatch.GetLight(slot);
				light2.InvMatrix = new global::Unity.Mathematics.float4x4(retMatrix.GetColumn(0), retMatrix.GetColumn(1), retMatrix.GetColumn(2), retMatrix.GetColumn(3));
				light2.InnerRadiusMult = num;
				light2.InnerAngle = normalizedAngle;
				light2.OuterAngle = normalizedAngle2;
				lightBatch.SetLight(slot, light2);
			}
			else
			{
				cmd.SetGlobalMatrix(k_L2DInvMatrix, retMatrix);
				cmd.SetGlobalFloat(k_L2DInnerRadiusMult, num);
				cmd.SetGlobalFloat(k_L2DInnerAngle, normalizedAngle);
				cmd.SetGlobalFloat(k_L2DOuterAngle, normalizedAngle2);
			}
		}

		internal static void SetCookieShaderProperties(global::UnityEngine.Rendering.Universal.Light2D light, global::UnityEngine.MaterialPropertyBlock properties)
		{
			if (light.useCookieSprite && light.m_CookieSpriteTextureHandle.IsValid())
			{
				properties.SetTexture((light.lightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Sprite) ? k_CookieTexID : k_PointLightCookieTexID, light.m_CookieSpriteTextureHandle);
			}
		}

		private static void SetBlendModes(global::UnityEngine.Material material, global::UnityEngine.Rendering.BlendMode src, global::UnityEngine.Rendering.BlendMode dst)
		{
			material.SetFloat(k_SrcBlendID, (float)src);
			material.SetFloat(k_DstBlendID, (float)dst);
		}

		private static uint GetLightMaterialIndex(global::UnityEngine.Rendering.Universal.Light2D light, bool isVolume, bool useShadows)
		{
			bool isPointLight = light.isPointLight;
			int num = 0;
			uint num2 = (isVolume ? ((uint)(1 << num)) : 0u);
			num++;
			uint num3 = ((isVolume && !isPointLight) ? ((uint)(1 << num)) : 0u);
			num++;
			uint num4 = ((light.overlapOperation != global::UnityEngine.Rendering.Universal.Light2D.OverlapOperation.AlphaBlend) ? ((uint)(1 << num)) : 0u);
			num++;
			uint num5 = ((isPointLight && light.lightCookieSprite != null && light.lightCookieSprite.texture != null) ? ((uint)(1 << num)) : 0u);
			num++;
			int num6 = ((light.normalMapQuality == global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality.Fast) ? (1 << num) : 0);
			num++;
			uint num7 = ((light.normalMapQuality != global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality.Disabled) ? ((uint)(1 << num)) : 0u);
			num++;
			uint num8 = (useShadows ? ((uint)(1 << num)) : 0u);
			return (uint)num6 | num5 | num4 | num3 | num2 | num7 | num8;
		}

		private static global::UnityEngine.Material CreateLightMaterial(global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, global::UnityEngine.Rendering.Universal.Light2D light, bool isVolume, bool useShadows)
		{
			if (!global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.Renderer2DResources>(out var settings))
			{
				return null;
			}
			bool isPointLight = light.isPointLight;
			global::UnityEngine.Material material = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.lightShader);
			if (!isVolume)
			{
				if (light.overlapOperation == global::UnityEngine.Rendering.Universal.Light2D.OverlapOperation.Additive)
				{
					SetBlendModes(material, global::UnityEngine.Rendering.BlendMode.One, global::UnityEngine.Rendering.BlendMode.One);
					material.EnableKeyword(k_UseAdditiveBlendingKeyword);
				}
				else
				{
					SetBlendModes(material, global::UnityEngine.Rendering.BlendMode.SrcAlpha, global::UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				}
			}
			else
			{
				material.EnableKeyword(k_UseVolumetric);
				if (light.lightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Point)
				{
					SetBlendModes(material, global::UnityEngine.Rendering.BlendMode.One, global::UnityEngine.Rendering.BlendMode.One);
				}
				else
				{
					SetBlendModes(material, global::UnityEngine.Rendering.BlendMode.SrcAlpha, global::UnityEngine.Rendering.BlendMode.One);
				}
			}
			if (isPointLight && light.lightCookieSprite != null && light.lightCookieSprite.texture != null)
			{
				material.EnableKeyword(k_UsePointLightCookiesKeyword);
			}
			if (light.normalMapQuality == global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality.Fast)
			{
				material.EnableKeyword(k_LightQualityFastKeyword);
			}
			if (light.normalMapQuality != global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality.Disabled)
			{
				material.EnableKeyword(k_UseNormalMap);
			}
			if (useShadows)
			{
				material.EnableKeyword(k_UseShadowMap);
			}
			return material;
		}

		internal static global::UnityEngine.Material GetLightMaterial(this global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, global::UnityEngine.Rendering.Universal.Light2D light, bool isVolume, bool useShadows)
		{
			uint lightMaterialIndex = GetLightMaterialIndex(light, isVolume, useShadows);
			if (!rendererData.lightMaterials.TryGetValue(lightMaterialIndex, out var value))
			{
				value = CreateLightMaterial(rendererData, light, isVolume, useShadows);
				rendererData.lightMaterials[lightMaterialIndex] = value;
			}
			return value;
		}

		internal static short GetCameraSortingLayerBoundsIndex(this global::UnityEngine.Rendering.Universal.Renderer2DData rendererData)
		{
			global::UnityEngine.SortingLayer[] cachedSortingLayer = global::UnityEngine.Rendering.Universal.Light2DManager.GetCachedSortingLayer();
			for (short num = 0; num < cachedSortingLayer.Length; num++)
			{
				if (cachedSortingLayer[num].id == rendererData.cameraSortingLayerTextureBound)
				{
					return (short)cachedSortingLayer[num].value;
				}
			}
			return short.MinValue;
		}
	}
}
