namespace TMPro
{
	public class MaterialReferenceManager
	{
		private static global::TMPro.MaterialReferenceManager s_Instance;

		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Material> m_FontMaterialReferenceLookup = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Material>();

		private global::System.Collections.Generic.Dictionary<int, global::TMPro.TMP_FontAsset> m_FontAssetReferenceLookup = new global::System.Collections.Generic.Dictionary<int, global::TMPro.TMP_FontAsset>();

		private global::System.Collections.Generic.Dictionary<int, global::TMPro.TMP_SpriteAsset> m_SpriteAssetReferenceLookup = new global::System.Collections.Generic.Dictionary<int, global::TMPro.TMP_SpriteAsset>();

		private global::System.Collections.Generic.Dictionary<int, global::TMPro.TMP_ColorGradient> m_ColorGradientReferenceLookup = new global::System.Collections.Generic.Dictionary<int, global::TMPro.TMP_ColorGradient>();

		public static global::TMPro.MaterialReferenceManager instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::TMPro.MaterialReferenceManager();
				}
				return s_Instance;
			}
		}

		public static void AddFontAsset(global::TMPro.TMP_FontAsset fontAsset)
		{
			instance.AddFontAssetInternal(fontAsset);
		}

		private void AddFontAssetInternal(global::TMPro.TMP_FontAsset fontAsset)
		{
			if (!m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode))
			{
				m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
				m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
			}
		}

		public static void AddSpriteAsset(global::TMPro.TMP_SpriteAsset spriteAsset)
		{
			instance.AddSpriteAssetInternal(spriteAsset);
		}

		private void AddSpriteAssetInternal(global::TMPro.TMP_SpriteAsset spriteAsset)
		{
			if (!m_SpriteAssetReferenceLookup.ContainsKey(spriteAsset.hashCode))
			{
				m_SpriteAssetReferenceLookup.Add(spriteAsset.hashCode, spriteAsset);
				m_FontMaterialReferenceLookup.Add(spriteAsset.hashCode, spriteAsset.material);
			}
		}

		public static void AddSpriteAsset(int hashCode, global::TMPro.TMP_SpriteAsset spriteAsset)
		{
			instance.AddSpriteAssetInternal(hashCode, spriteAsset);
		}

		private void AddSpriteAssetInternal(int hashCode, global::TMPro.TMP_SpriteAsset spriteAsset)
		{
			if (!m_SpriteAssetReferenceLookup.ContainsKey(hashCode))
			{
				m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
				m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
				if (spriteAsset.hashCode == 0)
				{
					spriteAsset.hashCode = hashCode;
				}
			}
		}

		public static void AddFontMaterial(int hashCode, global::UnityEngine.Material material)
		{
			instance.AddFontMaterialInternal(hashCode, material);
		}

		private void AddFontMaterialInternal(int hashCode, global::UnityEngine.Material material)
		{
			m_FontMaterialReferenceLookup.Add(hashCode, material);
		}

		public static void AddColorGradientPreset(int hashCode, global::TMPro.TMP_ColorGradient spriteAsset)
		{
			instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
		}

		private void AddColorGradientPreset_Internal(int hashCode, global::TMPro.TMP_ColorGradient spriteAsset)
		{
			if (!m_ColorGradientReferenceLookup.ContainsKey(hashCode))
			{
				m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
			}
		}

		public bool Contains(global::TMPro.TMP_FontAsset font)
		{
			return m_FontAssetReferenceLookup.ContainsKey(font.hashCode);
		}

		public bool Contains(global::TMPro.TMP_SpriteAsset sprite)
		{
			return m_FontAssetReferenceLookup.ContainsKey(sprite.hashCode);
		}

		public static bool TryGetFontAsset(int hashCode, out global::TMPro.TMP_FontAsset fontAsset)
		{
			return instance.TryGetFontAssetInternal(hashCode, out fontAsset);
		}

		private bool TryGetFontAssetInternal(int hashCode, out global::TMPro.TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			return m_FontAssetReferenceLookup.TryGetValue(hashCode, out fontAsset);
		}

		public static bool TryGetSpriteAsset(int hashCode, out global::TMPro.TMP_SpriteAsset spriteAsset)
		{
			return instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
		}

		private bool TryGetSpriteAssetInternal(int hashCode, out global::TMPro.TMP_SpriteAsset spriteAsset)
		{
			spriteAsset = null;
			return m_SpriteAssetReferenceLookup.TryGetValue(hashCode, out spriteAsset);
		}

		public static bool TryGetColorGradientPreset(int hashCode, out global::TMPro.TMP_ColorGradient gradientPreset)
		{
			return instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
		}

		private bool TryGetColorGradientPresetInternal(int hashCode, out global::TMPro.TMP_ColorGradient gradientPreset)
		{
			gradientPreset = null;
			return m_ColorGradientReferenceLookup.TryGetValue(hashCode, out gradientPreset);
		}

		public static bool TryGetMaterial(int hashCode, out global::UnityEngine.Material material)
		{
			return instance.TryGetMaterialInternal(hashCode, out material);
		}

		private bool TryGetMaterialInternal(int hashCode, out global::UnityEngine.Material material)
		{
			material = null;
			return m_FontMaterialReferenceLookup.TryGetValue(hashCode, out material);
		}
	}
}
