namespace TMPro
{
	public static class TMP_MaterialManager
	{
		private class FallbackMaterial
		{
			public long fallbackID;

			public global::UnityEngine.Material sourceMaterial;

			internal int sourceMaterialCRC;

			public global::UnityEngine.Material fallbackMaterial;

			public int count;
		}

		private class MaskingMaterial
		{
			public global::UnityEngine.Material baseMaterial;

			public global::UnityEngine.Material stencilMaterial;

			public int count;

			public int stencilID;
		}

		private static global::System.Collections.Generic.List<global::TMPro.TMP_MaterialManager.MaskingMaterial> m_materialList;

		private static global::System.Collections.Generic.Dictionary<long, global::TMPro.TMP_MaterialManager.FallbackMaterial> m_fallbackMaterials;

		private static global::System.Collections.Generic.Dictionary<int, long> m_fallbackMaterialLookup;

		private static global::System.Collections.Generic.List<global::TMPro.TMP_MaterialManager.FallbackMaterial> m_fallbackCleanupList;

		private static bool isFallbackListDirty;

		static TMP_MaterialManager()
		{
			m_materialList = new global::System.Collections.Generic.List<global::TMPro.TMP_MaterialManager.MaskingMaterial>();
			m_fallbackMaterials = new global::System.Collections.Generic.Dictionary<long, global::TMPro.TMP_MaterialManager.FallbackMaterial>();
			m_fallbackMaterialLookup = new global::System.Collections.Generic.Dictionary<int, long>();
			m_fallbackCleanupList = new global::System.Collections.Generic.List<global::TMPro.TMP_MaterialManager.FallbackMaterial>();
			global::UnityEngine.Canvas.willRenderCanvases += OnPreRender;
		}

		private static void OnPreRender()
		{
			if (isFallbackListDirty)
			{
				CleanupFallbackMaterials();
				isFallbackListDirty = false;
			}
		}

		public static global::UnityEngine.Material GetStencilMaterial(global::UnityEngine.Material baseMaterial, int stencilID)
		{
			if (!baseMaterial.HasProperty(global::TMPro.ShaderUtilities.ID_StencilID))
			{
				global::UnityEngine.Debug.LogWarning("Selected Shader does not support Stencil Masking. Please select the Distance Field or Mobile Distance Field Shader.");
				return baseMaterial;
			}
			int instanceID = baseMaterial.GetInstanceID();
			for (int i = 0; i < m_materialList.Count; i++)
			{
				if (m_materialList[i].baseMaterial.GetInstanceID() == instanceID && m_materialList[i].stencilID == stencilID)
				{
					m_materialList[i].count++;
					return m_materialList[i].stencilMaterial;
				}
			}
			global::UnityEngine.Material material = new global::UnityEngine.Material(baseMaterial);
			material.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			material.shaderKeywords = baseMaterial.shaderKeywords;
			global::TMPro.ShaderUtilities.GetShaderPropertyIDs();
			material.SetFloat(global::TMPro.ShaderUtilities.ID_StencilID, stencilID);
			material.SetFloat(global::TMPro.ShaderUtilities.ID_StencilComp, 4f);
			global::TMPro.TMP_MaterialManager.MaskingMaterial maskingMaterial = new global::TMPro.TMP_MaterialManager.MaskingMaterial();
			maskingMaterial.baseMaterial = baseMaterial;
			maskingMaterial.stencilMaterial = material;
			maskingMaterial.stencilID = stencilID;
			maskingMaterial.count = 1;
			m_materialList.Add(maskingMaterial);
			return material;
		}

		public static void ReleaseStencilMaterial(global::UnityEngine.Material stencilMaterial)
		{
			int instanceID = stencilMaterial.GetInstanceID();
			for (int i = 0; i < m_materialList.Count; i++)
			{
				if (m_materialList[i].stencilMaterial.GetInstanceID() == instanceID)
				{
					if (m_materialList[i].count > 1)
					{
						m_materialList[i].count--;
						break;
					}
					global::UnityEngine.Object.DestroyImmediate(m_materialList[i].stencilMaterial);
					m_materialList.RemoveAt(i);
					stencilMaterial = null;
					break;
				}
			}
		}

		public static global::UnityEngine.Material GetBaseMaterial(global::UnityEngine.Material stencilMaterial)
		{
			int num = m_materialList.FindIndex((global::TMPro.TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num == -1)
			{
				return null;
			}
			return m_materialList[num].baseMaterial;
		}

		public static global::UnityEngine.Material SetStencil(global::UnityEngine.Material material, int stencilID)
		{
			material.SetFloat(global::TMPro.ShaderUtilities.ID_StencilID, stencilID);
			if (stencilID == 0)
			{
				material.SetFloat(global::TMPro.ShaderUtilities.ID_StencilComp, 8f);
			}
			else
			{
				material.SetFloat(global::TMPro.ShaderUtilities.ID_StencilComp, 4f);
			}
			return material;
		}

		public static void AddMaskingMaterial(global::UnityEngine.Material baseMaterial, global::UnityEngine.Material stencilMaterial, int stencilID)
		{
			int num = m_materialList.FindIndex((global::TMPro.TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num == -1)
			{
				global::TMPro.TMP_MaterialManager.MaskingMaterial maskingMaterial = new global::TMPro.TMP_MaterialManager.MaskingMaterial();
				maskingMaterial.baseMaterial = baseMaterial;
				maskingMaterial.stencilMaterial = stencilMaterial;
				maskingMaterial.stencilID = stencilID;
				maskingMaterial.count = 1;
				m_materialList.Add(maskingMaterial);
			}
			else
			{
				stencilMaterial = m_materialList[num].stencilMaterial;
				m_materialList[num].count++;
			}
		}

		public static void RemoveStencilMaterial(global::UnityEngine.Material stencilMaterial)
		{
			int num = m_materialList.FindIndex((global::TMPro.TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num != -1)
			{
				m_materialList.RemoveAt(num);
			}
		}

		public static void ReleaseBaseMaterial(global::UnityEngine.Material baseMaterial)
		{
			int num = m_materialList.FindIndex((global::TMPro.TMP_MaterialManager.MaskingMaterial item) => item.baseMaterial == baseMaterial);
			if (num == -1)
			{
				global::UnityEngine.Debug.Log("No Masking Material exists for " + baseMaterial.name);
			}
			else if (m_materialList[num].count > 1)
			{
				m_materialList[num].count--;
				global::UnityEngine.Debug.Log("Removed (1) reference to " + m_materialList[num].stencilMaterial.name + ". There are " + m_materialList[num].count + " references left.");
			}
			else
			{
				global::UnityEngine.Debug.Log("Removed last reference to " + m_materialList[num].stencilMaterial.name + " with ID " + m_materialList[num].stencilMaterial.GetInstanceID());
				global::UnityEngine.Object.DestroyImmediate(m_materialList[num].stencilMaterial);
				m_materialList.RemoveAt(num);
			}
		}

		public static void ClearMaterials()
		{
			if (m_materialList.Count == 0)
			{
				global::UnityEngine.Debug.Log("Material List has already been cleared.");
				return;
			}
			for (int i = 0; i < m_materialList.Count; i++)
			{
				global::UnityEngine.Object.DestroyImmediate(m_materialList[i].stencilMaterial);
			}
			m_materialList.Clear();
		}

		public static int GetStencilID(global::UnityEngine.GameObject obj)
		{
			int num = 0;
			global::UnityEngine.Transform transform = obj.transform;
			global::UnityEngine.Transform transform2 = FindRootSortOverrideCanvas(transform);
			if (transform == transform2)
			{
				return num;
			}
			global::UnityEngine.Transform parent = transform.parent;
			global::System.Collections.Generic.List<global::UnityEngine.UI.Mask> list = global::TMPro.TMP_ListPool<global::UnityEngine.UI.Mask>.Get();
			while (parent != null)
			{
				parent.GetComponents(list);
				for (int i = 0; i < list.Count; i++)
				{
					global::UnityEngine.UI.Mask mask = list[i];
					if (mask != null && mask.MaskEnabled() && mask.graphic.IsActive())
					{
						num++;
						break;
					}
				}
				if (parent == transform2)
				{
					break;
				}
				parent = parent.parent;
			}
			global::TMPro.TMP_ListPool<global::UnityEngine.UI.Mask>.Release(list);
			return global::UnityEngine.Mathf.Min((1 << num) - 1, 255);
		}

		public static global::UnityEngine.Material GetMaterialForRendering(global::UnityEngine.UI.MaskableGraphic graphic, global::UnityEngine.Material baseMaterial)
		{
			if (baseMaterial == null)
			{
				return null;
			}
			global::System.Collections.Generic.List<global::UnityEngine.UI.IMaterialModifier> list = global::TMPro.TMP_ListPool<global::UnityEngine.UI.IMaterialModifier>.Get();
			graphic.GetComponents(list);
			global::UnityEngine.Material material = baseMaterial;
			for (int i = 0; i < list.Count; i++)
			{
				material = list[i].GetModifiedMaterial(material);
			}
			global::TMPro.TMP_ListPool<global::UnityEngine.UI.IMaterialModifier>.Release(list);
			return material;
		}

		private static global::UnityEngine.Transform FindRootSortOverrideCanvas(global::UnityEngine.Transform start)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Canvas> list = global::TMPro.TMP_ListPool<global::UnityEngine.Canvas>.Get();
			start.GetComponentsInParent(includeInactive: false, list);
			global::UnityEngine.Canvas canvas = null;
			for (int i = 0; i < list.Count; i++)
			{
				canvas = list[i];
				if (canvas.overrideSorting)
				{
					break;
				}
			}
			global::TMPro.TMP_ListPool<global::UnityEngine.Canvas>.Release(list);
			if (!(canvas != null))
			{
				return null;
			}
			return canvas.transform;
		}

		internal static global::UnityEngine.Material GetFallbackMaterial(global::TMPro.TMP_FontAsset fontAsset, global::UnityEngine.Material sourceMaterial, int atlasIndex)
		{
			int instanceID = sourceMaterial.GetInstanceID();
			global::UnityEngine.Texture texture = fontAsset.atlasTextures[atlasIndex];
			int instanceID2 = texture.GetInstanceID();
			long num = ((long)instanceID << 32) | (uint)instanceID2;
			if (m_fallbackMaterials.TryGetValue(num, out var value))
			{
				int num2 = sourceMaterial.ComputeCRC();
				if (num2 == value.sourceMaterialCRC)
				{
					return value.fallbackMaterial;
				}
				CopyMaterialPresetProperties(sourceMaterial, value.fallbackMaterial);
				value.sourceMaterialCRC = num2;
				return value.fallbackMaterial;
			}
			global::UnityEngine.Material material = new global::UnityEngine.Material(sourceMaterial);
			material.SetTexture(global::TMPro.ShaderUtilities.ID_MainTex, texture);
			material.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			value = new global::TMPro.TMP_MaterialManager.FallbackMaterial();
			value.fallbackID = num;
			value.sourceMaterial = fontAsset.material;
			value.sourceMaterialCRC = sourceMaterial.ComputeCRC();
			value.fallbackMaterial = material;
			value.count = 0;
			m_fallbackMaterials.Add(num, value);
			m_fallbackMaterialLookup.Add(material.GetInstanceID(), num);
			return material;
		}

		public static global::UnityEngine.Material GetFallbackMaterial(global::UnityEngine.Material sourceMaterial, global::UnityEngine.Material targetMaterial)
		{
			int instanceID = sourceMaterial.GetInstanceID();
			global::UnityEngine.Texture texture = targetMaterial.GetTexture(global::TMPro.ShaderUtilities.ID_MainTex);
			int instanceID2 = texture.GetInstanceID();
			long num = ((long)instanceID << 32) | (uint)instanceID2;
			if (m_fallbackMaterials.TryGetValue(num, out var value))
			{
				int num2 = sourceMaterial.ComputeCRC();
				if (num2 == value.sourceMaterialCRC)
				{
					return value.fallbackMaterial;
				}
				CopyMaterialPresetProperties(sourceMaterial, value.fallbackMaterial);
				value.sourceMaterialCRC = num2;
				return value.fallbackMaterial;
			}
			global::UnityEngine.Material material;
			if (sourceMaterial.HasProperty(global::TMPro.ShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(global::TMPro.ShaderUtilities.ID_GradientScale))
			{
				material = new global::UnityEngine.Material(sourceMaterial);
				material.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				material.SetTexture(global::TMPro.ShaderUtilities.ID_MainTex, texture);
				material.SetFloat(global::TMPro.ShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(global::TMPro.ShaderUtilities.ID_GradientScale));
				material.SetFloat(global::TMPro.ShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(global::TMPro.ShaderUtilities.ID_TextureWidth));
				material.SetFloat(global::TMPro.ShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(global::TMPro.ShaderUtilities.ID_TextureHeight));
				material.SetFloat(global::TMPro.ShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(global::TMPro.ShaderUtilities.ID_WeightNormal));
				material.SetFloat(global::TMPro.ShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(global::TMPro.ShaderUtilities.ID_WeightBold));
			}
			else
			{
				material = new global::UnityEngine.Material(targetMaterial);
				material.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			}
			value = new global::TMPro.TMP_MaterialManager.FallbackMaterial();
			value.fallbackID = num;
			value.sourceMaterial = sourceMaterial;
			value.sourceMaterialCRC = sourceMaterial.ComputeCRC();
			value.fallbackMaterial = material;
			value.count = 0;
			m_fallbackMaterials.Add(num, value);
			m_fallbackMaterialLookup.Add(material.GetInstanceID(), num);
			return material;
		}

		public static void AddFallbackMaterialReference(global::UnityEngine.Material targetMaterial)
		{
			if (!(targetMaterial == null))
			{
				int instanceID = targetMaterial.GetInstanceID();
				if (m_fallbackMaterialLookup.TryGetValue(instanceID, out var value) && m_fallbackMaterials.TryGetValue(value, out var value2))
				{
					value2.count++;
				}
			}
		}

		public static void RemoveFallbackMaterialReference(global::UnityEngine.Material targetMaterial)
		{
			if (targetMaterial == null)
			{
				return;
			}
			int instanceID = targetMaterial.GetInstanceID();
			if (m_fallbackMaterialLookup.TryGetValue(instanceID, out var value) && m_fallbackMaterials.TryGetValue(value, out var value2))
			{
				value2.count--;
				if (value2.count < 1)
				{
					m_fallbackCleanupList.Add(value2);
				}
			}
		}

		public static void CleanupFallbackMaterials()
		{
			if (m_fallbackCleanupList.Count == 0)
			{
				return;
			}
			for (int i = 0; i < m_fallbackCleanupList.Count; i++)
			{
				global::TMPro.TMP_MaterialManager.FallbackMaterial fallbackMaterial = m_fallbackCleanupList[i];
				if (fallbackMaterial.count < 1)
				{
					global::UnityEngine.Material fallbackMaterial2 = fallbackMaterial.fallbackMaterial;
					m_fallbackMaterials.Remove(fallbackMaterial.fallbackID);
					m_fallbackMaterialLookup.Remove(fallbackMaterial2.GetInstanceID());
					global::UnityEngine.Object.DestroyImmediate(fallbackMaterial2);
					fallbackMaterial2 = null;
				}
			}
			m_fallbackCleanupList.Clear();
		}

		public static void ReleaseFallbackMaterial(global::UnityEngine.Material fallbackMaterial)
		{
			if (fallbackMaterial == null)
			{
				return;
			}
			int instanceID = fallbackMaterial.GetInstanceID();
			if (m_fallbackMaterialLookup.TryGetValue(instanceID, out var value) && m_fallbackMaterials.TryGetValue(value, out var value2))
			{
				value2.count--;
				if (value2.count < 1)
				{
					m_fallbackCleanupList.Add(value2);
				}
			}
			isFallbackListDirty = true;
		}

		public static void CopyMaterialPresetProperties(global::UnityEngine.Material source, global::UnityEngine.Material destination)
		{
			if (source.HasProperty(global::TMPro.ShaderUtilities.ID_GradientScale) && destination.HasProperty(global::TMPro.ShaderUtilities.ID_GradientScale))
			{
				global::UnityEngine.Texture texture = destination.GetTexture(global::TMPro.ShaderUtilities.ID_MainTex);
				float value = destination.GetFloat(global::TMPro.ShaderUtilities.ID_GradientScale);
				float value2 = destination.GetFloat(global::TMPro.ShaderUtilities.ID_TextureWidth);
				float value3 = destination.GetFloat(global::TMPro.ShaderUtilities.ID_TextureHeight);
				float value4 = destination.GetFloat(global::TMPro.ShaderUtilities.ID_WeightNormal);
				float value5 = destination.GetFloat(global::TMPro.ShaderUtilities.ID_WeightBold);
				destination.shader = source.shader;
				destination.CopyPropertiesFromMaterial(source);
				destination.shaderKeywords = source.shaderKeywords;
				destination.SetTexture(global::TMPro.ShaderUtilities.ID_MainTex, texture);
				destination.SetFloat(global::TMPro.ShaderUtilities.ID_GradientScale, value);
				destination.SetFloat(global::TMPro.ShaderUtilities.ID_TextureWidth, value2);
				destination.SetFloat(global::TMPro.ShaderUtilities.ID_TextureHeight, value3);
				destination.SetFloat(global::TMPro.ShaderUtilities.ID_WeightNormal, value4);
				destination.SetFloat(global::TMPro.ShaderUtilities.ID_WeightBold, value5);
			}
		}
	}
}
