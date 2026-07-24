namespace UnityEngine.Rendering.Universal
{
	internal class Light2DCullResult : global::UnityEngine.Rendering.Universal.ILight2DCullResult
	{
		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D> m_VisibleLights = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D>();

		private global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D> m_VisibleShadows = new global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D>();

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D> visibleLights => m_VisibleLights;

		public global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D> visibleShadows => m_VisibleShadows;

		public bool IsSceneLit()
		{
			return global::UnityEngine.Rendering.Universal.Light2DManager.lights.Count > 0;
		}

		public global::UnityEngine.Rendering.Universal.LightStats GetLightStatsByLayer(int layerID, ref global::UnityEngine.Rendering.Universal.LayerBatch layer)
		{
			layer.lights.Clear();
			layer.shadowIndices.Clear();
			layer.shadowCasters.Clear();
			global::UnityEngine.Rendering.Universal.LightStats result = default(global::UnityEngine.Rendering.Universal.LightStats);
			foreach (global::UnityEngine.Rendering.Universal.Light2D visibleLight in visibleLights)
			{
				if (!visibleLight.IsLitLayer(layerID))
				{
					continue;
				}
				if (visibleLight.normalMapQuality != global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality.Disabled)
				{
					result.totalNormalMapUsage++;
				}
				if (visibleLight.volumeIntensity > 0f && visibleLight.volumetricEnabled)
				{
					result.totalVolumetricUsage++;
				}
				if (visibleLight.volumeIntensity > 0f && visibleLight.volumetricEnabled && global::UnityEngine.Rendering.Universal.RendererLighting.CanCastShadows(visibleLight, layerID))
				{
					result.totalVolumetricShadowUsage++;
				}
				result.blendStylesUsed |= (uint)(1 << visibleLight.blendStyleIndex);
				if (visibleLight.lightType != global::UnityEngine.Rendering.Universal.Light2D.LightType.Global)
				{
					result.blendStylesWithLights |= (uint)(1 << visibleLight.blendStyleIndex);
				}
				bool flag = false;
				if (global::UnityEngine.Rendering.Universal.RendererLighting.CanCastShadows(visibleLight, layerID))
				{
					foreach (global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D visibleShadow in visibleShadows)
					{
						global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCaster2D> shadowCasters = visibleShadow.GetShadowCasters();
						if (shadowCasters == null)
						{
							continue;
						}
						foreach (global::UnityEngine.Rendering.Universal.ShadowCaster2D item in shadowCasters)
						{
							if (item.IsLit(visibleLight) && item.IsShadowedLayer(layerID))
							{
								flag = true;
								result.totalShadows++;
								if (!layer.shadowCasters.Contains(visibleShadow))
								{
									layer.shadowCasters.Add(visibleShadow);
								}
							}
						}
					}
				}
				if (flag)
				{
					result.totalShadowLights++;
					layer.shadowIndices.Add(layer.lights.Count);
				}
				result.totalLights++;
				layer.lights.Add(visibleLight);
			}
			return result;
		}

		public void SetupCulling(ref global::UnityEngine.Rendering.ScriptableCullingParameters cullingParameters, global::UnityEngine.Camera camera)
		{
			m_VisibleLights.Clear();
			foreach (global::UnityEngine.Rendering.Universal.Light2D light in global::UnityEngine.Rendering.Universal.Light2DManager.lights)
			{
				if ((camera.cullingMask & (1 << light.gameObject.layer)) == 0)
				{
					continue;
				}
				if (light.lightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Global)
				{
					m_VisibleLights.Add(light);
					continue;
				}
				global::UnityEngine.Vector3 position = light.boundingSphere.position;
				bool flag = false;
				for (int i = 0; i < cullingParameters.cullingPlaneCount; i++)
				{
					global::UnityEngine.Plane cullingPlane = cullingParameters.GetCullingPlane(i);
					if (global::Unity.Mathematics.math.dot(position, cullingPlane.normal) + cullingPlane.distance < 0f - light.boundingSphere.radius)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					m_VisibleLights.Add(light);
				}
			}
			m_VisibleLights.Sort((global::UnityEngine.Rendering.Universal.Light2D l1, global::UnityEngine.Rendering.Universal.Light2D l2) => l1.lightOrder - l2.lightOrder);
			m_VisibleShadows.Clear();
			if (global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.shadowCasterGroups == null)
			{
				return;
			}
			foreach (global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D shadowCasterGroup in global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.shadowCasterGroups)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCaster2D> shadowCasters = shadowCasterGroup.GetShadowCasters();
				if (shadowCasters == null)
				{
					continue;
				}
				foreach (global::UnityEngine.Rendering.Universal.ShadowCaster2D item in shadowCasters)
				{
					foreach (global::UnityEngine.Rendering.Universal.Light2D visibleLight in m_VisibleLights)
					{
						if (item.IsLit(visibleLight) && !m_VisibleShadows.Contains(shadowCasterGroup))
						{
							m_VisibleShadows.Add(shadowCasterGroup);
							break;
						}
					}
					if (m_VisibleShadows.Contains(shadowCasterGroup))
					{
						break;
					}
				}
			}
		}
	}
}
