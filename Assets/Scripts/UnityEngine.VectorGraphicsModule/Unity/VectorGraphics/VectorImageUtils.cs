namespace Unity.VectorGraphics
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.VectorGraphicsModule" })]
	internal static class VectorImageUtils
	{
		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.VectorGraphicsModule" })]
		internal static void MakeVectorImageAsset(global::System.Collections.Generic.IEnumerable<global::Unity.VectorGraphics.VectorUtils.Geometry> geoms, global::UnityEngine.Rect rect, uint rasterSize, out global::UnityEngine.UIElements.VectorImage outAsset, out global::UnityEngine.Texture2D outTexAtlas)
		{
			global::Unity.VectorGraphics.VectorUtils.TextureAtlas textureAtlas = global::Unity.VectorGraphics.VectorUtils.GenerateAtlas(geoms, rasterSize, generatePOTTexture: false, encodeSettings: false, linear: false);
			if (textureAtlas != null)
			{
				global::Unity.VectorGraphics.VectorUtils.FillUVs(geoms, textureAtlas);
			}
			bool flag = textureAtlas != null && textureAtlas.Texture != null;
			outTexAtlas = (flag ? textureAtlas.Texture : null);
			global::System.Collections.Generic.List<global::UnityEngine.UIElements.VectorImageVertex> list = new global::System.Collections.Generic.List<global::UnityEngine.UIElements.VectorImageVertex>(100);
			global::System.Collections.Generic.List<ushort> list2 = new global::System.Collections.Generic.List<ushort>(300);
			global::System.Collections.Generic.List<global::UnityEngine.UIElements.GradientSettings> list3 = new global::System.Collections.Generic.List<global::UnityEngine.UIElements.GradientSettings>();
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(float.MaxValue, float.MaxValue);
			global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(float.MinValue, float.MinValue);
			foreach (global::Unity.VectorGraphics.VectorUtils.Geometry geom in geoms)
			{
				if (geom.Vertices.Length != 0)
				{
					global::UnityEngine.Vector2[] array = new global::UnityEngine.Vector2[geom.Vertices.Length];
					for (int i = 0; i < geom.Vertices.Length; i++)
					{
						global::UnityEngine.Vector2 vector3 = geom.WorldTransform.MultiplyPoint(geom.Vertices[i]);
						array[i] = vector3;
					}
					global::UnityEngine.Rect rect2 = global::Unity.VectorGraphics.VectorUtils.Bounds(array);
					vector = global::UnityEngine.Vector2.Min(vector, rect2.min);
					vector2 = global::UnityEngine.Vector2.Max(vector2, rect2.max);
				}
			}
			global::UnityEngine.Rect rect3 = global::UnityEngine.Rect.zero;
			if (vector.x != float.MaxValue)
			{
				rect3 = new global::UnityEngine.Rect(vector, vector2 - vector);
			}
			global::System.Collections.Generic.HashSet<int> hashSet = new global::System.Collections.Generic.HashSet<int>();
			hashSet.Add(0);
			global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.IFill, global::Unity.VectorGraphics.VectorUtils.PackRectItem> dictionary = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.IFill, global::Unity.VectorGraphics.VectorUtils.PackRectItem>();
			if (textureAtlas != null && textureAtlas.Entries != null)
			{
				foreach (global::Unity.VectorGraphics.VectorUtils.PackRectItem entry in textureAtlas.Entries)
				{
					if (entry.Fill != null)
					{
						dictionary[entry.Fill] = entry;
					}
				}
			}
			if (flag && textureAtlas != null && textureAtlas.Entries != null && textureAtlas.Entries.Count > 0)
			{
				global::Unity.VectorGraphics.VectorUtils.PackRectItem packRectItem = textureAtlas.Entries[textureAtlas.Entries.Count - 1];
				list3.Add(new global::UnityEngine.UIElements.GradientSettings
				{
					gradientType = global::UnityEngine.UIElements.GradientType.Linear,
					addressMode = global::UnityEngine.UIElements.AddressMode.Wrap,
					radialFocus = global::UnityEngine.Vector2.zero,
					location = new global::UnityEngine.RectInt((int)packRectItem.Position.x, (int)packRectItem.Position.y, (int)packRectItem.Size.x, (int)packRectItem.Size.y)
				});
			}
			foreach (global::Unity.VectorGraphics.VectorUtils.Geometry geom2 in geoms)
			{
				for (int j = 0; j < geom2.Vertices.Length; j++)
				{
					global::UnityEngine.Vector2 vector4 = geom2.WorldTransform.MultiplyPoint(geom2.Vertices[j]);
					vector4 -= rect3.position;
					geom2.Vertices[j] = vector4;
				}
				global::Unity.VectorGraphics.VectorUtils.AdjustWinding(geom2.Vertices, geom2.Indices, global::Unity.VectorGraphics.VectorUtils.WindingDir.CCW);
				int count = list.Count;
				for (int k = 0; k < geom2.Vertices.Length; k++)
				{
					global::UnityEngine.Vector3 position = geom2.Vertices[k];
					position.z = global::UnityEngine.UIElements.Vertex.nearZ;
					list.Add(new global::UnityEngine.UIElements.VectorImageVertex
					{
						position = position,
						uv = (flag ? geom2.UVs[k] : global::UnityEngine.Vector2.zero),
						tint = geom2.Color,
						settingIndex = (uint)geom2.SettingIndex
					});
				}
				ushort[] array2 = new ushort[geom2.Indices.Length];
				for (int l = 0; l < geom2.Indices.Length; l++)
				{
					array2[l] = (ushort)(geom2.Indices[l] + count);
				}
				list2.AddRange(array2);
				if (textureAtlas != null && textureAtlas.Entries != null && textureAtlas.Entries.Count > 0 && geom2.Fill != null && dictionary.TryGetValue(geom2.Fill, out var value) && !hashSet.Contains(value.SettingIndex))
				{
					hashSet.Add(value.SettingIndex);
					global::Unity.VectorGraphics.GradientFillType gradientType = global::Unity.VectorGraphics.GradientFillType.Linear;
					global::UnityEngine.Vector2 radialFocus = global::UnityEngine.Vector2.zero;
					global::Unity.VectorGraphics.AddressMode addressMode = global::Unity.VectorGraphics.AddressMode.Wrap;
					if (geom2.Fill is global::Unity.VectorGraphics.GradientFill gradientFill)
					{
						gradientType = gradientFill.Type;
						radialFocus = gradientFill.RadialFocus;
						addressMode = gradientFill.Addressing;
					}
					if (geom2.Fill is global::Unity.VectorGraphics.TextureFill textureFill)
					{
						addressMode = textureFill.Addressing;
					}
					list3.Add(new global::UnityEngine.UIElements.GradientSettings
					{
						gradientType = (global::UnityEngine.UIElements.GradientType)gradientType,
						addressMode = (global::UnityEngine.UIElements.AddressMode)addressMode,
						radialFocus = radialFocus,
						location = new global::UnityEngine.RectInt((int)value.Position.x, (int)value.Position.y, (int)value.Size.x, (int)value.Size.y)
					});
				}
			}
			if (rect == global::UnityEngine.Rect.zero)
			{
				rect = rect3;
			}
			else
			{
				global::UnityEngine.Vector2 vector5 = rect3.position - rect.position;
				for (int m = 0; m < list.Count; m++)
				{
					global::UnityEngine.UIElements.VectorImageVertex value2 = list[m];
					global::UnityEngine.Vector2 rhs = value2.position;
					rhs += vector5;
					rhs = global::UnityEngine.Vector2.Max(rect.min, global::UnityEngine.Vector2.Min(rect.max, rhs));
					value2.position = new global::UnityEngine.Vector3(rhs.x, rhs.y, value2.position.z);
					list[m] = value2;
				}
			}
			outAsset = MakeVectorImageAsset(list, list2, outTexAtlas, list3, rect);
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.VectorGraphicsModule" })]
		internal static global::UnityEngine.Texture2D RenderVectorImageToTexture2D(global::UnityEngine.UIElements.VectorImage vi, int width, int height, global::UnityEngine.Material mat, int antiAliasing = 1)
		{
			if (vi == null)
			{
				return null;
			}
			if (width <= 0 || height <= 0)
			{
				return null;
			}
			global::UnityEngine.RenderTexture renderTexture = null;
			global::UnityEngine.RenderTexture active = global::UnityEngine.RenderTexture.active;
			global::UnityEngine.RenderTextureDescriptor renderTextureDescriptor = new global::UnityEngine.RenderTextureDescriptor(width, height, global::UnityEngine.RenderTextureFormat.ARGB32, 0);
			renderTextureDescriptor.msaaSamples = antiAliasing;
			renderTextureDescriptor.sRGB = global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear;
			global::UnityEngine.RenderTextureDescriptor desc = renderTextureDescriptor;
			renderTexture = (global::UnityEngine.RenderTexture.active = global::UnityEngine.RenderTexture.GetTemporary(desc));
			global::UnityEngine.UIElements.PanelSettings panelSettings = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.UIElements.PanelSettings>();
			panelSettings.clearColor = true;
			panelSettings.clearDepthStencil = true;
			panelSettings.targetTexture = renderTexture;
			global::UnityEngine.GL.PushMatrix();
			global::UnityEngine.UIElements.BaseRuntimePanel panel = panelSettings.panel;
			global::UnityEngine.UIElements.VisualElement visualTree = panel.visualTree;
			global::UnityEngine.UIElements.VisualElementExtensions.StretchToParentSize(visualTree);
			visualTree.style.backgroundImage = new global::UnityEngine.UIElements.StyleBackground(vi);
			panel.Repaint(global::UnityEngine.Event.current);
			panel.Render();
			global::UnityEngine.GL.PopMatrix();
			global::UnityEngine.Object.DestroyImmediate(panelSettings);
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(width, height, global::UnityEngine.TextureFormat.RGBA32, mipChain: false);
			texture2D.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			texture2D.ReadPixels(new global::UnityEngine.Rect(0f, 0f, width, height), 0, 0);
			texture2D.Apply();
			global::UnityEngine.RenderTexture.active = active;
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			return texture2D;
		}

		private static global::UnityEngine.Texture2D BuildAtlasWithEncodedSettings(global::UnityEngine.UIElements.GradientSettings[] settings, global::UnityEngine.Texture2D atlas)
		{
			global::UnityEngine.RenderTexture active = global::UnityEngine.RenderTexture.active;
			int num = atlas.width + 3;
			int num2 = global::System.Math.Max(settings.Length, atlas.height);
			global::UnityEngine.RenderTextureDescriptor renderTextureDescriptor = new global::UnityEngine.RenderTextureDescriptor(num, num2, global::UnityEngine.RenderTextureFormat.ARGB32, 0);
			renderTextureDescriptor.sRGB = global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear;
			global::UnityEngine.RenderTextureDescriptor desc = renderTextureDescriptor;
			global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(desc);
			global::UnityEngine.GL.Clear(clearDepth: false, clearColor: true, global::UnityEngine.Color.black, 1f);
			global::UnityEngine.Graphics.Blit(atlas, temporary, global::UnityEngine.Vector2.one, new global::UnityEngine.Vector2(-3f / (float)num, 0f));
			global::UnityEngine.RenderTexture.active = temporary;
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(num, num2, global::UnityEngine.TextureFormat.RGBA32, mipChain: false);
			texture2D.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			texture2D.ReadPixels(new global::UnityEngine.Rect(0f, 0f, num, num2), 0, 0);
			global::Unity.VectorGraphics.VectorUtils.RawTexture dest = new global::Unity.VectorGraphics.VectorUtils.RawTexture
			{
				Width = 3,
				Height = settings.Length,
				Rgba = new global::UnityEngine.Color32[3 * settings.Length]
			};
			for (int i = 0; i < settings.Length; i++)
			{
				global::UnityEngine.UIElements.GradientSettings gradientSettings = settings[i];
				int num3 = 0;
				int destY = i;
				if (gradientSettings.gradientType == global::UnityEngine.UIElements.GradientType.Radial)
				{
					global::UnityEngine.Vector2 radialFocus = gradientSettings.radialFocus;
					radialFocus += global::UnityEngine.Vector2.one;
					radialFocus /= 2f;
					radialFocus.y = 1f - radialFocus.y;
					global::Unity.VectorGraphics.VectorUtils.WriteRawFloat4Packed(dest, (float)gradientSettings.gradientType / 255f, (float)gradientSettings.addressMode / 255f, radialFocus.x, radialFocus.y, num3++, destY);
				}
				else
				{
					global::Unity.VectorGraphics.VectorUtils.WriteRawFloat4Packed(dest, 0f, (float)gradientSettings.addressMode / 255f, 0f, 0f, num3++, destY);
				}
				global::UnityEngine.Vector2Int position = gradientSettings.location.position;
				global::UnityEngine.Vector2Int size = gradientSettings.location.size;
				size.x--;
				size.y--;
				global::Unity.VectorGraphics.VectorUtils.WriteRawInt2Packed(dest, position.x + 3, position.y, num3++, destY);
				global::Unity.VectorGraphics.VectorUtils.WriteRawInt2Packed(dest, size.x, size.y, num3++, destY);
			}
			texture2D.SetPixels32(0, 0, 3, settings.Length, dest.Rgba, 0);
			texture2D.Apply();
			global::UnityEngine.RenderTexture.active = active;
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
			return texture2D;
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.VectorGraphicsModule" })]
		internal static global::UnityEngine.UIElements.VectorImage MakeVectorImageAsset(global::System.Collections.Generic.List<global::UnityEngine.UIElements.VectorImageVertex> vertices, global::System.Collections.Generic.List<ushort> indices, global::UnityEngine.Texture2D atlas, global::System.Collections.Generic.List<global::UnityEngine.UIElements.GradientSettings> settings, global::UnityEngine.Rect rect)
		{
			global::UnityEngine.UIElements.VectorImage vectorImage = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.UIElements.VectorImage>();
			vectorImage.vertices = vertices.ToArray();
			vectorImage.indices = indices.ToArray();
			vectorImage.atlas = atlas;
			vectorImage.settings = settings.ToArray();
			vectorImage.size = rect.size;
			return vectorImage;
		}
	}
}
