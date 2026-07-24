namespace UnityEngine.Rendering.Universal
{
	internal static class ShadowRendering
	{
		internal enum ShadowTestType
		{
			Always = 0,
			Unshadow = 1
		}

		private static readonly int k_LightPosID = global::UnityEngine.Shader.PropertyToID("_LightPos");

		private static readonly int k_ShadowRadiusID = global::UnityEngine.Shader.PropertyToID("_ShadowRadius");

		private static readonly int k_ShadowColorMaskID = global::UnityEngine.Shader.PropertyToID("_ShadowColorMask");

		private static readonly int k_ShadowModelMatrixID = global::UnityEngine.Shader.PropertyToID("_ShadowModelMatrix");

		private static readonly int k_ShadowModelInvMatrixID = global::UnityEngine.Shader.PropertyToID("_ShadowModelInvMatrix");

		private static readonly int k_ShadowModelScaleID = global::UnityEngine.Shader.PropertyToID("_ShadowModelScale");

		private static readonly int k_ShadowContractionDistanceID = global::UnityEngine.Shader.PropertyToID("_ShadowContractionDistance");

		private static readonly int k_ShadowAlphaCutoffID = global::UnityEngine.Shader.PropertyToID("_ShadowAlphaCutoff");

		private static readonly int k_SoftShadowAngle = global::UnityEngine.Shader.PropertyToID("_SoftShadowAngle");

		private static readonly int k_ShadowSoftnessFalloffIntensityID = global::UnityEngine.Shader.PropertyToID("_ShadowSoftnessFalloffIntensity");

		private static readonly int k_ShadowShadowColorID = global::UnityEngine.Shader.PropertyToID("_ShadowColor");

		private static readonly int k_ShadowUnshadowColorID = global::UnityEngine.Shader.PropertyToID("_UnshadowColor");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerShadows = new global::UnityEngine.Rendering.ProfilingSampler("Draw 2D Shadow Texture");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerShadowsA = new global::UnityEngine.Rendering.ProfilingSampler("Draw 2D Shadows (A)");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerShadowsR = new global::UnityEngine.Rendering.ProfilingSampler("Draw 2D Shadows (R)");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerShadowsG = new global::UnityEngine.Rendering.ProfilingSampler("Draw 2D Shadows (G)");

		private static readonly global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerShadowsB = new global::UnityEngine.Rendering.ProfilingSampler("Draw 2D Shadows (B)");

		private static readonly float k_MaxShadowSoftnessAngle = 15f;

		private static readonly global::UnityEngine.Color k_ShadowColorLookup = new global::UnityEngine.Color(0f, 0f, 1f, 0f);

		private static readonly global::UnityEngine.Color k_UnshadowColorLookup = new global::UnityEngine.Color(0f, 1f, 0f, 0f);

		private static global::UnityEngine.Material CreateMaterial(global::UnityEngine.Shader shader, int offset, int pass)
		{
			global::UnityEngine.Material material = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(shader);
			material.SetInt(k_ShadowColorMaskID, 1 << offset + 1);
			material.SetPass(pass);
			return material;
		}

		private static global::UnityEngine.Material GetProjectedShadowMaterial(global::UnityEngine.Material material, global::System.Func<global::UnityEngine.Rendering.Universal.Renderer2DResources, global::UnityEngine.Shader> shaderFunc, int offset, int pass)
		{
			if (material != null)
			{
				return material;
			}
			if (!global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.Renderer2DResources>(out var settings))
			{
				return null;
			}
			global::UnityEngine.Shader shader = shaderFunc(settings);
			if (material != null && material.shader != shader)
			{
				material = null;
			}
			if (material == null)
			{
				material = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(shader);
				material.SetInt(k_ShadowColorMaskID, 1 << offset + 1);
				material.SetPass(pass);
			}
			return material;
		}

		internal static global::UnityEngine.Material GetProjectedShadowMaterial(this global::UnityEngine.Rendering.Universal.Renderer2DData rendererData)
		{
			rendererData.projectedShadowMaterial = GetProjectedShadowMaterial(rendererData.projectedShadowMaterial, (global::UnityEngine.Rendering.Universal.Renderer2DResources r) => r.projectedShadowShader, 0, 0);
			return rendererData.projectedShadowMaterial;
		}

		internal static global::UnityEngine.Material GetProjectedUnshadowMaterial(this global::UnityEngine.Rendering.Universal.Renderer2DData rendererData)
		{
			rendererData.projectedUnshadowMaterial = GetProjectedShadowMaterial(rendererData.projectedUnshadowMaterial, (global::UnityEngine.Rendering.Universal.Renderer2DResources r) => r.projectedShadowShader, 1, 1);
			return rendererData.projectedUnshadowMaterial;
		}

		private static global::UnityEngine.Material GetSpriteShadowMaterial(this global::UnityEngine.Rendering.Universal.Renderer2DData rendererData)
		{
			rendererData.spriteSelfShadowMaterial = GetProjectedShadowMaterial(rendererData.spriteSelfShadowMaterial, (global::UnityEngine.Rendering.Universal.Renderer2DResources r) => r.spriteShadowShader, 0, 0);
			return rendererData.spriteSelfShadowMaterial;
		}

		private static global::UnityEngine.Material GetSpriteUnshadowMaterial(this global::UnityEngine.Rendering.Universal.Renderer2DData rendererData)
		{
			rendererData.spriteUnshadowMaterial = GetProjectedShadowMaterial(rendererData.spriteUnshadowMaterial, (global::UnityEngine.Rendering.Universal.Renderer2DResources r) => r.spriteUnshadowShader, 1, 0);
			return rendererData.spriteUnshadowMaterial;
		}

		private static global::UnityEngine.Material GetGeometryShadowMaterial(this global::UnityEngine.Rendering.Universal.Renderer2DData rendererData)
		{
			rendererData.geometrySelfShadowMaterial = GetProjectedShadowMaterial(rendererData.geometrySelfShadowMaterial, (global::UnityEngine.Rendering.Universal.Renderer2DResources r) => r.geometryShadowShader, 0, 0);
			return rendererData.geometrySelfShadowMaterial;
		}

		private static global::UnityEngine.Material GetGeometryUnshadowMaterial(this global::UnityEngine.Rendering.Universal.Renderer2DData rendererData)
		{
			rendererData.geometryUnshadowMaterial = GetProjectedShadowMaterial(rendererData.geometryUnshadowMaterial, (global::UnityEngine.Rendering.Universal.Renderer2DResources r) => r.geometryUnshadowShader, 1, 0);
			return rendererData.geometryUnshadowMaterial;
		}

		private static void CalculateFrustumCornersPerspective(global::UnityEngine.Camera camera, float distance, global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> corners)
		{
			float fieldOfView = camera.fieldOfView;
			float num = global::UnityEngine.Mathf.Tan(0.5f * fieldOfView * (global::System.MathF.PI / 180f)) * distance;
			float num2 = num * camera.aspect;
			corners[0] = new global::UnityEngine.Vector3(num2, num, distance);
			corners[1] = new global::UnityEngine.Vector3(num2, 0f - num, distance);
			corners[2] = new global::UnityEngine.Vector3(0f - num2, num, distance);
			corners[3] = new global::UnityEngine.Vector3(0f - num2, 0f - num, distance);
		}

		private static void CalculateFrustumCornersOrthographic(global::UnityEngine.Camera camera, float distance, global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> corners)
		{
			float orthographicSize = camera.orthographicSize;
			float num = orthographicSize * camera.aspect;
			corners[0] = new global::UnityEngine.Vector3(num, orthographicSize, distance);
			corners[1] = new global::UnityEngine.Vector3(num, 0f - orthographicSize, distance);
			corners[2] = new global::UnityEngine.Vector3(0f - num, orthographicSize, distance);
			corners[3] = new global::UnityEngine.Vector3(0f - num, 0f - orthographicSize, distance);
		}

		private static global::UnityEngine.Bounds CalculateWorldSpaceBounds(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.ILight2DCullResult cullResult)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> corners = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(4, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> corners2 = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(4, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			if (camera.orthographic)
			{
				CalculateFrustumCornersOrthographic(camera, camera.nearClipPlane, corners);
				CalculateFrustumCornersOrthographic(camera, camera.farClipPlane, corners2);
			}
			else
			{
				CalculateFrustumCornersPerspective(camera, camera.nearClipPlane, corners);
				CalculateFrustumCornersPerspective(camera, camera.farClipPlane, corners2);
			}
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(float.MinValue, float.MinValue, float.MinValue);
			for (int i = 0; i < 4; i++)
			{
				vector2 = global::UnityEngine.Vector3.Max(vector2, camera.transform.TransformPoint(corners[i]));
				vector2 = global::UnityEngine.Vector3.Max(vector2, camera.transform.TransformPoint(corners2[i]));
				vector = global::UnityEngine.Vector3.Min(vector, camera.transform.TransformPoint(corners[i]));
				vector = global::UnityEngine.Vector3.Min(vector, camera.transform.TransformPoint(corners2[i]));
			}
			corners.Dispose();
			corners2.Dispose();
			for (int j = 0; j < cullResult.visibleLights.Count; j++)
			{
				global::UnityEngine.Vector3 position = cullResult.visibleLights[j].transform.position;
				vector2 = global::UnityEngine.Vector3.Max(vector2, position);
				vector = global::UnityEngine.Vector3.Min(vector, position);
			}
			global::UnityEngine.Vector3 center = 0.5f * (vector + vector2);
			global::UnityEngine.Vector3 size = vector2 - vector;
			return new global::UnityEngine.Bounds(center, size);
		}

		internal static void CallOnBeforeRender(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.ILight2DCullResult cullResult)
		{
			if (global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.shadowCasterGroups == null)
			{
				return;
			}
			global::UnityEngine.Bounds bounds = CalculateWorldSpaceBounds(camera, cullResult);
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D> shadowCasterGroups = global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.shadowCasterGroups;
			for (int i = 0; i < shadowCasterGroups.Count; i++)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCaster2D> shadowCasters = shadowCasterGroups[i].GetShadowCasters();
				if (shadowCasters == null)
				{
					continue;
				}
				for (int j = 0; j < shadowCasters.Count; j++)
				{
					global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster2D = shadowCasters[j];
					if (shadowCaster2D != null && shadowCaster2D.shadowCastingSource == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources.ShapeProvider)
					{
						global::UnityEngine.Rendering.Universal.ShapeProviderUtility.CallOnBeforeRender(shadowCaster2D.shadowShape2DProvider, shadowCaster2D.shadowShape2DComponent, shadowCaster2D.m_ShadowMesh, bounds);
					}
				}
			}
		}

		internal static void PrerenderShadows(global::UnityEngine.Rendering.UnsafeCommandBuffer cmdBuffer, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, ref global::UnityEngine.Rendering.Universal.LayerBatch layer, global::UnityEngine.Rendering.Universal.Light2D light, int shadowIndex, float shadowIntensity)
		{
			RenderShadows(cmdBuffer, rendererData, ref layer, light);
		}

		private static void SetShadowProjectionGlobals(global::UnityEngine.Rendering.UnsafeCommandBuffer cmdBuffer, global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster, global::UnityEngine.Rendering.Universal.Light2D light)
		{
			cmdBuffer.SetGlobalVector(k_ShadowModelScaleID, shadowCaster.m_CachedLossyScale);
			cmdBuffer.SetGlobalMatrix(k_ShadowModelMatrixID, shadowCaster.m_CachedShadowMatrix);
			cmdBuffer.SetGlobalMatrix(k_ShadowModelInvMatrixID, shadowCaster.m_CachedInverseShadowMatrix);
			cmdBuffer.SetGlobalFloat(k_ShadowSoftnessFalloffIntensityID, light.shadowSoftnessFalloffIntensity);
			if (shadowCaster.edgeProcessing == global::UnityEngine.Rendering.Universal.ShadowCaster2D.EdgeProcessing.None)
			{
				cmdBuffer.SetGlobalFloat(k_ShadowContractionDistanceID, shadowCaster.trimEdge);
			}
			else
			{
				cmdBuffer.SetGlobalFloat(k_ShadowContractionDistanceID, 0f);
			}
		}

		internal static void SetGlobalShadowProp(global::UnityEngine.Rendering.IRasterCommandBuffer cmdBuffer)
		{
			cmdBuffer.SetGlobalColor(k_ShadowShadowColorID, k_ShadowColorLookup);
			cmdBuffer.SetGlobalColor(k_ShadowUnshadowColorID, k_UnshadowColorLookup);
		}

		private static bool ShadowCasterIsVisible(global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster)
		{
			return true;
		}

		private static global::UnityEngine.Renderer GetRendererFromCaster(global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster, global::UnityEngine.Rendering.Universal.Light2D light, int layerToRender)
		{
			global::UnityEngine.Renderer component = null;
			if (shadowCaster.IsLit(light) && shadowCaster != null && shadowCaster.IsShadowedLayer(layerToRender))
			{
				shadowCaster.TryGetComponent<global::UnityEngine.Renderer>(out component);
			}
			return component;
		}

		private static void RenderProjectedShadows(global::UnityEngine.Rendering.UnsafeCommandBuffer cmdBuffer, int layerToRender, global::UnityEngine.Rendering.Universal.Light2D light, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCaster2D> shadowCasters, global::UnityEngine.Material projectedShadowsMaterial, int pass, global::UnityEngine.Rendering.Universal.ShadowRendering.ShadowTestType shadowTestType)
		{
			for (int i = 0; i < shadowCasters.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster2D = shadowCasters[i];
				if (ShadowTest(shadowTestType, shadowCaster2D) && ShadowCasterIsVisible(shadowCaster2D) && shadowCaster2D.castsShadows && shadowCaster2D.IsLit(light) && shadowCaster2D != null && projectedShadowsMaterial != null && shadowCaster2D.IsShadowedLayer(layerToRender) && shadowCaster2D.shadowCastingSource != global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources.None && shadowCaster2D.mesh != null)
				{
					SetShadowProjectionGlobals(cmdBuffer, shadowCaster2D, light);
					cmdBuffer.DrawMesh(shadowCaster2D.mesh, shadowCaster2D.transform.localToWorldMatrix, projectedShadowsMaterial, 0, pass);
				}
			}
		}

		private static int GetRendererSubmeshes(global::UnityEngine.Renderer renderer, global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster2D)
		{
			if (renderer is global::UnityEngine.U2D.SpriteShapeRenderer)
			{
				return ((global::UnityEngine.U2D.SpriteShapeRenderer)renderer).GetSplineMeshCount();
			}
			return shadowCaster2D.spriteMaterialCount;
		}

		private static void RenderSpriteShadow(global::UnityEngine.Rendering.UnsafeCommandBuffer cmdBuffer, int layerToRender, global::UnityEngine.Rendering.Universal.Light2D light, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCaster2D> shadowCasters, global::UnityEngine.Material spriteShadowMaterial, global::UnityEngine.Material spriteUnshadowMaterial, global::UnityEngine.Material geometryShadowMaterial, global::UnityEngine.Material geometryUnshadowMaterial, int pass, global::UnityEngine.Rendering.Universal.ShadowRendering.ShadowTestType shadowTestType)
		{
			for (int i = 0; i < shadowCasters.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster2D = shadowCasters[i];
				if (!ShadowTest(shadowTestType, shadowCaster2D) || !shadowCaster2D.IsLit(light))
				{
					continue;
				}
				global::UnityEngine.Renderer rendererFromCaster = GetRendererFromCaster(shadowCaster2D, light, layerToRender);
				cmdBuffer.SetGlobalFloat(k_ShadowAlphaCutoffID, shadowCaster2D.alphaCutoff);
				if (rendererFromCaster != null)
				{
					if (ShadowCasterIsVisible(shadowCaster2D) && shadowCaster2D.selfShadows)
					{
						int rendererSubmeshes = GetRendererSubmeshes(rendererFromCaster, shadowCaster2D);
						for (int j = 0; j < rendererSubmeshes; j++)
						{
							cmdBuffer.DrawRenderer(rendererFromCaster, spriteShadowMaterial, j, pass);
						}
					}
					else
					{
						int rendererSubmeshes2 = GetRendererSubmeshes(rendererFromCaster, shadowCaster2D);
						for (int k = 0; k < rendererSubmeshes2; k++)
						{
							cmdBuffer.DrawRenderer(rendererFromCaster, spriteUnshadowMaterial, k, pass);
						}
					}
				}
				else if (shadowCaster2D.mesh != null)
				{
					if (ShadowCasterIsVisible(shadowCaster2D) && shadowCaster2D.selfShadows)
					{
						cmdBuffer.DrawMesh(shadowCaster2D.mesh, shadowCaster2D.transform.localToWorldMatrix, geometryShadowMaterial, 0, pass);
					}
					else
					{
						cmdBuffer.DrawMesh(shadowCaster2D.mesh, shadowCaster2D.transform.localToWorldMatrix, geometryUnshadowMaterial, 0, pass);
					}
				}
			}
		}

		internal static bool ShadowTest(global::UnityEngine.Rendering.Universal.ShadowRendering.ShadowTestType shadowTestType, global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster)
		{
			return shadowTestType switch
			{
				global::UnityEngine.Rendering.Universal.ShadowRendering.ShadowTestType.Always => true, 
				global::UnityEngine.Rendering.Universal.ShadowRendering.ShadowTestType.Unshadow => !shadowCaster.selfShadows, 
				_ => false, 
			};
		}

		private static void RenderShadows(global::UnityEngine.Rendering.UnsafeCommandBuffer cmdBuffer, global::UnityEngine.Rendering.Universal.Renderer2DData rendererData, ref global::UnityEngine.Rendering.Universal.LayerBatch layer, global::UnityEngine.Rendering.Universal.Light2D light)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(cmdBuffer, m_ProfilingSamplerShadows))
			{
				float value = light.boundingSphere.radius + (light.transform.position - light.boundingSphere.position).magnitude;
				cmdBuffer.SetGlobalVector(k_LightPosID, light.transform.position);
				cmdBuffer.SetGlobalFloat(k_ShadowRadiusID, value);
				cmdBuffer.SetGlobalFloat(k_SoftShadowAngle, global::System.MathF.PI / 180f * light.shadowSoftness * k_MaxShadowSoftnessAngle);
				global::UnityEngine.Material projectedShadowMaterial = rendererData.GetProjectedShadowMaterial();
				rendererData.GetProjectedUnshadowMaterial();
				global::UnityEngine.Material spriteShadowMaterial = rendererData.GetSpriteShadowMaterial();
				global::UnityEngine.Material spriteUnshadowMaterial = rendererData.GetSpriteUnshadowMaterial();
				global::UnityEngine.Material geometryShadowMaterial = rendererData.GetGeometryShadowMaterial();
				global::UnityEngine.Material geometryUnshadowMaterial = rendererData.GetGeometryUnshadowMaterial();
				for (int i = 0; i < layer.shadowCasters.Count; i++)
				{
					global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCaster2D> shadowCasters = layer.shadowCasters[i].GetShadowCasters();
					RenderSpriteShadow(cmdBuffer, layer.startLayerID, light, shadowCasters, spriteShadowMaterial, spriteUnshadowMaterial, geometryShadowMaterial, geometryUnshadowMaterial, 0, global::UnityEngine.Rendering.Universal.ShadowRendering.ShadowTestType.Always);
					RenderProjectedShadows(cmdBuffer, layer.startLayerID, light, shadowCasters, projectedShadowMaterial, 0, global::UnityEngine.Rendering.Universal.ShadowRendering.ShadowTestType.Always);
					RenderProjectedShadows(cmdBuffer, layer.startLayerID, light, shadowCasters, projectedShadowMaterial, 1, global::UnityEngine.Rendering.Universal.ShadowRendering.ShadowTestType.Unshadow);
					RenderSpriteShadow(cmdBuffer, layer.startLayerID, light, shadowCasters, spriteShadowMaterial, spriteUnshadowMaterial, geometryShadowMaterial, geometryUnshadowMaterial, 1, global::UnityEngine.Rendering.Universal.ShadowRendering.ShadowTestType.Unshadow);
				}
			}
		}
	}
}
