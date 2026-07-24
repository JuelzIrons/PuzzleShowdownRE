namespace TMPro
{
	public class TMP_FontAssetUtilities
	{
		private static readonly global::TMPro.TMP_FontAssetUtilities s_Instance;

		private static global::System.Collections.Generic.HashSet<int> k_SearchedAssets;

		public static global::TMPro.TMP_FontAssetUtilities instance => s_Instance;

		static TMP_FontAssetUtilities()
		{
			s_Instance = new global::TMPro.TMP_FontAssetUtilities();
		}

		public static global::TMPro.TMP_Character GetCharacterFromFontAsset(uint unicode, global::TMPro.TMP_FontAsset sourceFontAsset, bool includeFallbacks, global::TMPro.FontStyles fontStyle, global::TMPro.FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			if (includeFallbacks)
			{
				if (k_SearchedAssets == null)
				{
					k_SearchedAssets = new global::System.Collections.Generic.HashSet<int>();
				}
				else
				{
					k_SearchedAssets.Clear();
				}
			}
			return GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
		}

		private static global::TMPro.TMP_Character GetCharacterFromFontAsset_Internal(uint unicode, global::TMPro.TMP_FontAsset sourceFontAsset, bool includeFallbacks, global::TMPro.FontStyles fontStyle, global::TMPro.FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			bool flag = (fontStyle & global::TMPro.FontStyles.Italic) == global::TMPro.FontStyles.Italic;
			global::TMPro.TMP_Character value;
			global::TMPro.AtlasPopulationMode atlasPopulationMode;
			if (flag || fontWeight != global::TMPro.FontWeight.Regular)
			{
				uint key = (((uint)(0x80 | ((int)fontStyle << 4)) | ((uint)fontWeight / 100u)) << 24) | unicode;
				if (sourceFontAsset.characterLookupTable.TryGetValue(key, out value))
				{
					isAlternativeTypeface = true;
					if (value.textAsset != null)
					{
						return value;
					}
					sourceFontAsset.characterLookupTable.Remove(unicode);
				}
				global::TMPro.TMP_FontWeightPair[] fontWeightTable = sourceFontAsset.fontWeightTable;
				int num = 4;
				switch (fontWeight)
				{
				case global::TMPro.FontWeight.Thin:
					num = 1;
					break;
				case global::TMPro.FontWeight.ExtraLight:
					num = 2;
					break;
				case global::TMPro.FontWeight.Light:
					num = 3;
					break;
				case global::TMPro.FontWeight.Regular:
					num = 4;
					break;
				case global::TMPro.FontWeight.Medium:
					num = 5;
					break;
				case global::TMPro.FontWeight.SemiBold:
					num = 6;
					break;
				case global::TMPro.FontWeight.Bold:
					num = 7;
					break;
				case global::TMPro.FontWeight.Heavy:
					num = 8;
					break;
				case global::TMPro.FontWeight.Black:
					num = 9;
					break;
				}
				global::TMPro.TMP_FontAsset tMP_FontAsset = (flag ? fontWeightTable[num].italicTypeface : fontWeightTable[num].regularTypeface);
				if (tMP_FontAsset != null)
				{
					if (tMP_FontAsset.characterLookupTable.TryGetValue(unicode, out value))
					{
						if (value.textAsset != null)
						{
							isAlternativeTypeface = true;
							return value;
						}
						tMP_FontAsset.characterLookupTable.Remove(unicode);
					}
					atlasPopulationMode = tMP_FontAsset.atlasPopulationMode;
					if ((atlasPopulationMode == global::TMPro.AtlasPopulationMode.Dynamic || atlasPopulationMode == global::TMPro.AtlasPopulationMode.DynamicOS) && tMP_FontAsset.TryAddCharacterInternal(unicode, out value))
					{
						isAlternativeTypeface = true;
						return value;
					}
				}
				if (includeFallbacks)
				{
					global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
					if (fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
					{
						return SearchFallbacksForCharacter(unicode, sourceFontAsset, fontStyle, fontWeight, out isAlternativeTypeface);
					}
				}
				return null;
			}
			if (sourceFontAsset.characterLookupTable.TryGetValue(unicode, out value))
			{
				if (value.textAsset != null)
				{
					return value;
				}
				sourceFontAsset.characterLookupTable.Remove(unicode);
			}
			atlasPopulationMode = sourceFontAsset.atlasPopulationMode;
			if ((atlasPopulationMode == global::TMPro.AtlasPopulationMode.Dynamic || atlasPopulationMode == global::TMPro.AtlasPopulationMode.DynamicOS) && sourceFontAsset.TryAddCharacterInternal(unicode, out value))
			{
				return value;
			}
			if (includeFallbacks)
			{
				global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
				if (fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
				{
					return SearchFallbacksForCharacter(unicode, sourceFontAsset, fontStyle, fontWeight, out isAlternativeTypeface);
				}
			}
			return null;
		}

		private static global::TMPro.TMP_Character SearchFallbacksForCharacter(uint unicode, global::TMPro.TMP_FontAsset sourceFontAsset, global::TMPro.FontStyles fontStyle, global::TMPro.FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
			int count = fallbackFontAssetTable.Count;
			if (count == 0)
			{
				return null;
			}
			for (int i = 0; i < count; i++)
			{
				global::TMPro.TMP_FontAsset tMP_FontAsset = fallbackFontAssetTable[i];
				if (tMP_FontAsset == null)
				{
					continue;
				}
				int instanceID = tMP_FontAsset.instanceID;
				if (k_SearchedAssets.Add(instanceID))
				{
					global::TMPro.TMP_Character characterFromFontAsset_Internal = GetCharacterFromFontAsset_Internal(unicode, tMP_FontAsset, includeFallbacks: true, fontStyle, fontWeight, out isAlternativeTypeface);
					if (characterFromFontAsset_Internal != null)
					{
						return characterFromFontAsset_Internal;
					}
				}
			}
			return null;
		}

		public static global::TMPro.TMP_Character GetCharacterFromFontAssets(uint unicode, global::TMPro.TMP_FontAsset sourceFontAsset, global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> fontAssets, bool includeFallbacks, global::TMPro.FontStyles fontStyle, global::TMPro.FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			if (fontAssets == null || fontAssets.Count == 0)
			{
				return null;
			}
			if (includeFallbacks)
			{
				if (k_SearchedAssets == null)
				{
					k_SearchedAssets = new global::System.Collections.Generic.HashSet<int>();
				}
				else
				{
					k_SearchedAssets.Clear();
				}
			}
			int count = fontAssets.Count;
			for (int i = 0; i < count; i++)
			{
				global::TMPro.TMP_FontAsset tMP_FontAsset = fontAssets[i];
				if (!(tMP_FontAsset == null))
				{
					global::TMPro.TMP_Character characterFromFontAsset_Internal = GetCharacterFromFontAsset_Internal(unicode, tMP_FontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
					if (characterFromFontAsset_Internal != null)
					{
						return characterFromFontAsset_Internal;
					}
				}
			}
			return null;
		}

		internal static global::TMPro.TMP_TextElement GetTextElementFromTextAssets(uint unicode, global::TMPro.TMP_FontAsset sourceFontAsset, global::System.Collections.Generic.List<global::TMPro.TMP_Asset> textAssets, bool includeFallbacks, global::TMPro.FontStyles fontStyle, global::TMPro.FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			if (textAssets == null || textAssets.Count == 0)
			{
				return null;
			}
			if (includeFallbacks)
			{
				if (k_SearchedAssets == null)
				{
					k_SearchedAssets = new global::System.Collections.Generic.HashSet<int>();
				}
				else
				{
					k_SearchedAssets.Clear();
				}
			}
			int count = textAssets.Count;
			for (int i = 0; i < count; i++)
			{
				global::TMPro.TMP_Asset tMP_Asset = textAssets[i];
				if (tMP_Asset == null)
				{
					continue;
				}
				if (tMP_Asset.GetType() == typeof(global::TMPro.TMP_FontAsset))
				{
					global::TMPro.TMP_FontAsset sourceFontAsset2 = tMP_Asset as global::TMPro.TMP_FontAsset;
					global::TMPro.TMP_Character characterFromFontAsset_Internal = GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset2, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
					if (characterFromFontAsset_Internal != null)
					{
						return characterFromFontAsset_Internal;
					}
				}
				else
				{
					global::TMPro.TMP_SpriteAsset spriteAsset = tMP_Asset as global::TMPro.TMP_SpriteAsset;
					global::TMPro.TMP_SpriteCharacter spriteCharacterFromSpriteAsset_Internal = GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset, includeFallbacks: true);
					if (spriteCharacterFromSpriteAsset_Internal != null)
					{
						return spriteCharacterFromSpriteAsset_Internal;
					}
				}
			}
			return null;
		}

		public static global::TMPro.TMP_SpriteCharacter GetSpriteCharacterFromSpriteAsset(uint unicode, global::TMPro.TMP_SpriteAsset spriteAsset, bool includeFallbacks)
		{
			if (spriteAsset == null)
			{
				return null;
			}
			if (spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out var value))
			{
				return value;
			}
			if (includeFallbacks)
			{
				if (k_SearchedAssets == null)
				{
					k_SearchedAssets = new global::System.Collections.Generic.HashSet<int>();
				}
				else
				{
					k_SearchedAssets.Clear();
				}
				k_SearchedAssets.Add(spriteAsset.instanceID);
				global::System.Collections.Generic.List<global::TMPro.TMP_SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
				if (fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0)
				{
					int count = fallbackSpriteAssets.Count;
					for (int i = 0; i < count; i++)
					{
						global::TMPro.TMP_SpriteAsset tMP_SpriteAsset = fallbackSpriteAssets[i];
						if (tMP_SpriteAsset == null)
						{
							continue;
						}
						int instanceID = tMP_SpriteAsset.instanceID;
						if (k_SearchedAssets.Add(instanceID))
						{
							value = GetSpriteCharacterFromSpriteAsset_Internal(unicode, tMP_SpriteAsset, includeFallbacks: true);
							if (value != null)
							{
								return value;
							}
						}
					}
				}
			}
			return null;
		}

		private static global::TMPro.TMP_SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(uint unicode, global::TMPro.TMP_SpriteAsset spriteAsset, bool includeFallbacks)
		{
			if (spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out var value))
			{
				return value;
			}
			if (includeFallbacks)
			{
				global::System.Collections.Generic.List<global::TMPro.TMP_SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
				if (fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0)
				{
					int count = fallbackSpriteAssets.Count;
					for (int i = 0; i < count; i++)
					{
						global::TMPro.TMP_SpriteAsset tMP_SpriteAsset = fallbackSpriteAssets[i];
						if (tMP_SpriteAsset == null)
						{
							continue;
						}
						int instanceID = tMP_SpriteAsset.instanceID;
						if (k_SearchedAssets.Add(instanceID))
						{
							value = GetSpriteCharacterFromSpriteAsset_Internal(unicode, tMP_SpriteAsset, includeFallbacks: true);
							if (value != null)
							{
								return value;
							}
						}
					}
				}
			}
			return null;
		}

		internal static uint GetCodePoint(string text, ref int index)
		{
			char c = text[index];
			if (char.IsHighSurrogate(c) && index + 1 < text.Length && char.IsLowSurrogate(text[index + 1]))
			{
				int result = char.ConvertToUtf32(c, text[index + 1]);
				index++;
				return (uint)result;
			}
			return c;
		}

		internal static uint GetCodePoint(uint[] codesPoints, ref int index)
		{
			char c = (char)codesPoints[index];
			if (char.IsHighSurrogate(c) && index + 1 < codesPoints.Length && char.IsLowSurrogate((char)codesPoints[index + 1]))
			{
				int result = char.ConvertToUtf32(c, (char)codesPoints[index + 1]);
				index++;
				return (uint)result;
			}
			return c;
		}
	}
}
