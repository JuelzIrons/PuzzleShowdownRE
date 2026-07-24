namespace TMPro
{
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/Sprites.html")]
	[global::UnityEngine.ExcludeFromPreset]
	public class TMP_SpriteAsset : global::TMPro.TMP_Asset
	{
		internal global::System.Collections.Generic.Dictionary<int, int> m_NameLookup;

		internal global::System.Collections.Generic.Dictionary<uint, int> m_GlyphIndexLookup;

		public global::UnityEngine.Texture spriteSheet;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::TMPro.TMP_SpriteCharacter> m_SpriteCharacterTable = new global::System.Collections.Generic.List<global::TMPro.TMP_SpriteCharacter>();

		internal global::System.Collections.Generic.Dictionary<uint, global::TMPro.TMP_SpriteCharacter> m_SpriteCharacterLookup;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_SpriteGlyphTable")]
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::TMPro.TMP_SpriteGlyph> m_GlyphTable = new global::System.Collections.Generic.List<global::TMPro.TMP_SpriteGlyph>();

		internal global::System.Collections.Generic.Dictionary<uint, global::TMPro.TMP_SpriteGlyph> m_SpriteGlyphLookup;

		public global::System.Collections.Generic.List<global::TMPro.TMP_Sprite> spriteInfoList;

		[global::UnityEngine.SerializeField]
		public global::System.Collections.Generic.List<global::TMPro.TMP_SpriteAsset> fallbackSpriteAssets;

		internal bool m_IsSpriteAssetLookupTablesDirty;

		private static global::System.Collections.Generic.HashSet<int> k_searchedSpriteAssets;

		public global::System.Collections.Generic.List<global::TMPro.TMP_SpriteCharacter> spriteCharacterTable
		{
			get
			{
				if (m_GlyphIndexLookup == null)
				{
					UpdateLookupTables();
				}
				return m_SpriteCharacterTable;
			}
			internal set
			{
				m_SpriteCharacterTable = value;
			}
		}

		public global::System.Collections.Generic.Dictionary<uint, global::TMPro.TMP_SpriteCharacter> spriteCharacterLookupTable
		{
			get
			{
				if (m_SpriteCharacterLookup == null)
				{
					UpdateLookupTables();
				}
				return m_SpriteCharacterLookup;
			}
			internal set
			{
				m_SpriteCharacterLookup = value;
			}
		}

		public global::System.Collections.Generic.List<global::TMPro.TMP_SpriteGlyph> spriteGlyphTable
		{
			get
			{
				return m_GlyphTable;
			}
			internal set
			{
				m_GlyphTable = value;
			}
		}

		private void Awake()
		{
			if (base.material != null && string.IsNullOrEmpty(m_Version))
			{
				UpgradeSpriteAsset();
			}
		}

		private global::UnityEngine.Material GetDefaultSpriteMaterial()
		{
			global::TMPro.ShaderUtilities.GetShaderPropertyIDs();
			global::UnityEngine.Material obj = new global::UnityEngine.Material(global::UnityEngine.Shader.Find("TextMeshPro/Sprite"));
			obj.SetTexture(global::TMPro.ShaderUtilities.ID_MainTex, spriteSheet);
			return obj;
		}

		public void UpdateLookupTables()
		{
			if (base.material != null && string.IsNullOrEmpty(m_Version))
			{
				UpgradeSpriteAsset();
			}
			if (m_GlyphIndexLookup == null)
			{
				m_GlyphIndexLookup = new global::System.Collections.Generic.Dictionary<uint, int>();
			}
			else
			{
				m_GlyphIndexLookup.Clear();
			}
			if (m_SpriteGlyphLookup == null)
			{
				m_SpriteGlyphLookup = new global::System.Collections.Generic.Dictionary<uint, global::TMPro.TMP_SpriteGlyph>();
			}
			else
			{
				m_SpriteGlyphLookup.Clear();
			}
			for (int i = 0; i < m_GlyphTable.Count; i++)
			{
				global::TMPro.TMP_SpriteGlyph tMP_SpriteGlyph = m_GlyphTable[i];
				uint index = tMP_SpriteGlyph.index;
				if (!m_GlyphIndexLookup.ContainsKey(index))
				{
					m_GlyphIndexLookup.Add(index, i);
				}
				if (!m_SpriteGlyphLookup.ContainsKey(index))
				{
					m_SpriteGlyphLookup.Add(index, tMP_SpriteGlyph);
				}
			}
			if (m_NameLookup == null)
			{
				m_NameLookup = new global::System.Collections.Generic.Dictionary<int, int>();
			}
			else
			{
				m_NameLookup.Clear();
			}
			if (m_SpriteCharacterLookup == null)
			{
				m_SpriteCharacterLookup = new global::System.Collections.Generic.Dictionary<uint, global::TMPro.TMP_SpriteCharacter>();
			}
			else
			{
				m_SpriteCharacterLookup.Clear();
			}
			for (int j = 0; j < m_SpriteCharacterTable.Count; j++)
			{
				global::TMPro.TMP_SpriteCharacter tMP_SpriteCharacter = m_SpriteCharacterTable[j];
				if (tMP_SpriteCharacter == null)
				{
					continue;
				}
				uint glyphIndex = tMP_SpriteCharacter.glyphIndex;
				if (m_SpriteGlyphLookup.ContainsKey(glyphIndex))
				{
					tMP_SpriteCharacter.glyph = m_SpriteGlyphLookup[glyphIndex];
					tMP_SpriteCharacter.textAsset = this;
					int key = global::TMPro.TMP_TextUtilities.GetHashCode(m_SpriteCharacterTable[j].name);
					if (!m_NameLookup.ContainsKey(key))
					{
						m_NameLookup.Add(key, j);
					}
					uint unicode = m_SpriteCharacterTable[j].unicode;
					if (unicode != 65534 && !m_SpriteCharacterLookup.ContainsKey(unicode))
					{
						m_SpriteCharacterLookup.Add(unicode, tMP_SpriteCharacter);
					}
				}
			}
			m_IsSpriteAssetLookupTablesDirty = false;
		}

		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			if (m_NameLookup == null)
			{
				UpdateLookupTables();
			}
			if (m_NameLookup.TryGetValue(hashCode, out var value))
			{
				return value;
			}
			return -1;
		}

		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			if (m_SpriteCharacterLookup == null)
			{
				UpdateLookupTables();
			}
			if (m_SpriteCharacterLookup.TryGetValue(unicode, out var value))
			{
				return (int)value.glyphIndex;
			}
			return -1;
		}

		public int GetSpriteIndexFromName(string name)
		{
			if (m_NameLookup == null)
			{
				UpdateLookupTables();
			}
			int num = global::TMPro.TMP_TextUtilities.GetHashCode(name);
			return GetSpriteIndexFromHashcode(num);
		}

		public static global::TMPro.TMP_SpriteAsset SearchForSpriteByUnicode(global::TMPro.TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (k_searchedSpriteAssets == null)
			{
				k_searchedSpriteAssets = new global::System.Collections.Generic.HashSet<int>();
			}
			else
			{
				k_searchedSpriteAssets.Clear();
			}
			int item = spriteAsset.GetInstanceID();
			k_searchedSpriteAssets.Add(item);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks: true, out spriteIndex);
			}
			if (includeFallbacks && global::TMPro.TMP_Settings.defaultSpriteAsset != null)
			{
				return SearchForSpriteByUnicodeInternal(global::TMPro.TMP_Settings.defaultSpriteAsset, unicode, includeFallbacks: true, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		private static global::TMPro.TMP_SpriteAsset SearchForSpriteByUnicodeInternal(global::System.Collections.Generic.List<global::TMPro.TMP_SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				global::TMPro.TMP_SpriteAsset tMP_SpriteAsset = spriteAssets[i];
				if (tMP_SpriteAsset == null)
				{
					continue;
				}
				int item = tMP_SpriteAsset.GetInstanceID();
				if (k_searchedSpriteAssets.Add(item))
				{
					tMP_SpriteAsset = SearchForSpriteByUnicodeInternal(tMP_SpriteAsset, unicode, includeFallbacks, out spriteIndex);
					if (tMP_SpriteAsset != null)
					{
						return tMP_SpriteAsset;
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		private static global::TMPro.TMP_SpriteAsset SearchForSpriteByUnicodeInternal(global::TMPro.TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks: true, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		public static global::TMPro.TMP_SpriteAsset SearchForSpriteByHashCode(global::TMPro.TMP_SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (k_searchedSpriteAssets == null)
			{
				k_searchedSpriteAssets = new global::System.Collections.Generic.HashSet<int>();
			}
			else
			{
				k_searchedSpriteAssets.Clear();
			}
			int item = spriteAsset.instanceID;
			k_searchedSpriteAssets.Add(item);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				global::TMPro.TMP_SpriteAsset result = SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, searchFallbacks: true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return result;
				}
			}
			if (includeFallbacks && global::TMPro.TMP_Settings.defaultSpriteAsset != null)
			{
				global::TMPro.TMP_SpriteAsset result = SearchForSpriteByHashCodeInternal(global::TMPro.TMP_Settings.defaultSpriteAsset, hashCode, searchFallbacks: true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return result;
				}
			}
			k_searchedSpriteAssets.Clear();
			uint missingCharacterSpriteUnicode = global::TMPro.TMP_Settings.missingCharacterSpriteUnicode;
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(missingCharacterSpriteUnicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			k_searchedSpriteAssets.Add(item);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				global::TMPro.TMP_SpriteAsset result = SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, missingCharacterSpriteUnicode, includeFallbacks: true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return result;
				}
			}
			if (includeFallbacks && global::TMPro.TMP_Settings.defaultSpriteAsset != null)
			{
				global::TMPro.TMP_SpriteAsset result = SearchForSpriteByUnicodeInternal(global::TMPro.TMP_Settings.defaultSpriteAsset, missingCharacterSpriteUnicode, includeFallbacks: true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return result;
				}
			}
			spriteIndex = -1;
			return null;
		}

		private static global::TMPro.TMP_SpriteAsset SearchForSpriteByHashCodeInternal(global::System.Collections.Generic.List<global::TMPro.TMP_SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				global::TMPro.TMP_SpriteAsset tMP_SpriteAsset = spriteAssets[i];
				if (tMP_SpriteAsset == null)
				{
					continue;
				}
				int item = tMP_SpriteAsset.instanceID;
				if (k_searchedSpriteAssets.Add(item))
				{
					tMP_SpriteAsset = SearchForSpriteByHashCodeInternal(tMP_SpriteAsset, hashCode, searchFallbacks, out spriteIndex);
					if (tMP_SpriteAsset != null)
					{
						return tMP_SpriteAsset;
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		private static global::TMPro.TMP_SpriteAsset SearchForSpriteByHashCodeInternal(global::TMPro.TMP_SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, searchFallbacks: true, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		public void SortGlyphTable()
		{
			if (m_GlyphTable != null && m_GlyphTable.Count != 0)
			{
				m_GlyphTable = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.OrderBy(m_GlyphTable, (global::TMPro.TMP_SpriteGlyph item) => item.index));
			}
		}

		internal void SortCharacterTable()
		{
			if (m_SpriteCharacterTable != null && m_SpriteCharacterTable.Count > 0)
			{
				m_SpriteCharacterTable = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.OrderBy(m_SpriteCharacterTable, (global::TMPro.TMP_SpriteCharacter c) => c.unicode));
			}
		}

		internal void SortGlyphAndCharacterTables()
		{
			SortGlyphTable();
			SortCharacterTable();
		}

		private void UpgradeSpriteAsset()
		{
			m_Version = "1.1.0";
			global::UnityEngine.Debug.Log("Upgrading sprite asset [" + base.name + "] to version " + m_Version + ".", this);
			m_SpriteCharacterTable.Clear();
			m_GlyphTable.Clear();
			for (int i = 0; i < spriteInfoList.Count; i++)
			{
				global::TMPro.TMP_Sprite tMP_Sprite = spriteInfoList[i];
				global::TMPro.TMP_SpriteGlyph tMP_SpriteGlyph = new global::TMPro.TMP_SpriteGlyph();
				tMP_SpriteGlyph.index = (uint)i;
				tMP_SpriteGlyph.sprite = tMP_Sprite.sprite;
				tMP_SpriteGlyph.metrics = new global::UnityEngine.TextCore.GlyphMetrics(tMP_Sprite.width, tMP_Sprite.height, tMP_Sprite.xOffset, tMP_Sprite.yOffset, tMP_Sprite.xAdvance);
				tMP_SpriteGlyph.glyphRect = new global::UnityEngine.TextCore.GlyphRect((int)tMP_Sprite.x, (int)tMP_Sprite.y, (int)tMP_Sprite.width, (int)tMP_Sprite.height);
				tMP_SpriteGlyph.scale = 1f;
				tMP_SpriteGlyph.atlasIndex = 0;
				m_GlyphTable.Add(tMP_SpriteGlyph);
				global::TMPro.TMP_SpriteCharacter tMP_SpriteCharacter = new global::TMPro.TMP_SpriteCharacter();
				tMP_SpriteCharacter.glyph = tMP_SpriteGlyph;
				tMP_SpriteCharacter.unicode = ((tMP_Sprite.unicode == 0) ? 65534u : ((uint)tMP_Sprite.unicode));
				tMP_SpriteCharacter.name = tMP_Sprite.name;
				tMP_SpriteCharacter.scale = tMP_Sprite.scale;
				m_SpriteCharacterTable.Add(tMP_SpriteCharacter);
			}
			UpdateLookupTables();
		}
	}
}
