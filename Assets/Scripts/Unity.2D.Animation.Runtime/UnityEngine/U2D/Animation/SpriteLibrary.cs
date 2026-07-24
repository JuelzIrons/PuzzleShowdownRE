namespace UnityEngine.U2D.Animation
{
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.AddComponentMenu("2D Animation/Sprite Library")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.animation@latest/index.html?subfolder=/manual/SL-component.html")]
	public class SpriteLibrary : global::UnityEngine.MonoBehaviour, global::UnityEngine.U2D.Common.IPreviewable, global::UnityEngine.Animations.IAnimationPreviewable
	{
		private struct CategoryEntrySprite
		{
			public string category;

			public string entry;

			public global::UnityEngine.Sprite sprite;
		}

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategory> m_Library = new global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategory>();

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.Animation.SpriteLibraryAsset m_SpriteLibraryAsset;

		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.U2D.Animation.SpriteLibrary.CategoryEntrySprite> m_CategoryEntryHashCache;

		private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.HashSet<string>> m_CategoryEntryCache;

		private int m_PreviousSpriteLibraryAsset;

		private long m_PreviousModificationHash;

		public global::UnityEngine.U2D.Animation.SpriteLibraryAsset spriteLibraryAsset
		{
			get
			{
				return m_SpriteLibraryAsset;
			}
			set
			{
				if (m_SpriteLibraryAsset != value)
				{
					m_SpriteLibraryAsset = value;
					CacheOverrides();
					RefreshSpriteResolvers();
				}
			}
		}

		internal global::System.Collections.Generic.IEnumerable<string> categoryNames
		{
			get
			{
				UpdateCacheOverridesIfNeeded();
				return m_CategoryEntryCache.Keys;
			}
		}

		private void OnEnable()
		{
			CacheOverrides();
		}

		public void OnPreviewUpdate()
		{
		}

		public global::UnityEngine.Sprite GetSprite(string category, string label)
		{
			return GetSprite(GetHashForCategoryAndEntry(category, label));
		}

		private global::UnityEngine.Sprite GetSprite(int hash)
		{
			if (m_CategoryEntryHashCache.ContainsKey(hash))
			{
				return m_CategoryEntryHashCache[hash].sprite;
			}
			return null;
		}

		private void UpdateCacheOverridesIfNeeded()
		{
			if (m_CategoryEntryCache == null || m_PreviousSpriteLibraryAsset != m_SpriteLibraryAsset?.GetInstanceID() || m_PreviousModificationHash != m_SpriteLibraryAsset?.modificationHash)
			{
				CacheOverrides();
			}
		}

		internal bool GetCategoryAndEntryNameFromHash(int hash, out string category, out string entry)
		{
			UpdateCacheOverridesIfNeeded();
			if (m_CategoryEntryHashCache.ContainsKey(hash))
			{
				category = m_CategoryEntryHashCache[hash].category;
				entry = m_CategoryEntryHashCache[hash].entry;
				return true;
			}
			category = null;
			entry = null;
			return false;
		}

		internal static int GetHashForCategoryAndEntry(string category, string entry)
		{
			return global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(category + "_" + entry);
		}

		internal global::UnityEngine.Sprite GetSpriteFromCategoryAndEntryHash(int hash, out bool validEntry)
		{
			UpdateCacheOverridesIfNeeded();
			if (m_CategoryEntryHashCache.ContainsKey(hash))
			{
				validEntry = true;
				return m_CategoryEntryHashCache[hash].sprite;
			}
			validEntry = false;
			return null;
		}

		private global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntry> GetEntries(string category, bool addIfNotExist)
		{
			int num = m_Library.FindIndex((global::UnityEngine.U2D.Animation.SpriteLibCategory x) => x.name == category);
			if (num < 0)
			{
				if (!addIfNotExist)
				{
					return null;
				}
				m_Library.Add(new global::UnityEngine.U2D.Animation.SpriteLibCategory
				{
					name = category,
					categoryList = new global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntry>()
				});
				num = m_Library.Count - 1;
			}
			return m_Library[num].categoryList;
		}

		private static global::UnityEngine.U2D.Animation.SpriteCategoryEntry GetEntry(global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntry> entries, string entry, bool addIfNotExist)
		{
			int num = entries.FindIndex((global::UnityEngine.U2D.Animation.SpriteCategoryEntry x) => x.name == entry);
			if (num < 0)
			{
				if (!addIfNotExist)
				{
					return null;
				}
				entries.Add(new global::UnityEngine.U2D.Animation.SpriteCategoryEntry
				{
					name = entry
				});
				num = entries.Count - 1;
			}
			return entries[num];
		}

		public void AddOverride(global::UnityEngine.U2D.Animation.SpriteLibraryAsset spriteLib, string category, string label)
		{
			global::UnityEngine.Sprite sprite = spriteLib.GetSprite(category, label);
			GetEntry(GetEntries(category, addIfNotExist: true), label, addIfNotExist: true).sprite = sprite;
			CacheOverrides();
		}

		public void AddOverride(global::UnityEngine.U2D.Animation.SpriteLibraryAsset spriteLib, string category)
		{
			int categoryHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(category);
			global::UnityEngine.U2D.Animation.SpriteLibCategory spriteLibCategory = global::System.Linq.Enumerable.FirstOrDefault(spriteLib.categories, (global::UnityEngine.U2D.Animation.SpriteLibCategory x) => x.hash == categoryHash);
			if (spriteLibCategory != null)
			{
				global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntry> entries = GetEntries(category, addIfNotExist: true);
				for (int num = 0; num < spriteLibCategory.categoryList.Count; num++)
				{
					global::UnityEngine.U2D.Animation.SpriteCategoryEntry spriteCategoryEntry = spriteLibCategory.categoryList[num];
					GetEntry(entries, spriteCategoryEntry.name, addIfNotExist: true).sprite = spriteCategoryEntry.sprite;
				}
				CacheOverrides();
			}
		}

		public void AddOverride(global::UnityEngine.Sprite sprite, string category, string label)
		{
			GetEntry(GetEntries(category, addIfNotExist: true), label, addIfNotExist: true).sprite = sprite;
			CacheOverrides();
			RefreshSpriteResolvers();
		}

		public void RemoveOverride(string category)
		{
			int num = m_Library.FindIndex((global::UnityEngine.U2D.Animation.SpriteLibCategory x) => x.name == category);
			if (num >= 0)
			{
				m_Library.RemoveAt(num);
				CacheOverrides();
				RefreshSpriteResolvers();
			}
		}

		public void RemoveOverride(string category, string label)
		{
			global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntry> entries = GetEntries(category, addIfNotExist: false);
			if (entries != null)
			{
				int num = entries.FindIndex((global::UnityEngine.U2D.Animation.SpriteCategoryEntry x) => x.name == label);
				if (num >= 0)
				{
					entries.RemoveAt(num);
					CacheOverrides();
					RefreshSpriteResolvers();
				}
			}
		}

		public bool HasOverride(string category, string label)
		{
			global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntry> entries = GetEntries(category, addIfNotExist: false);
			if (entries != null)
			{
				return GetEntry(entries, label, addIfNotExist: false) != null;
			}
			return false;
		}

		public void RefreshSpriteResolvers()
		{
			global::UnityEngine.U2D.Animation.SpriteResolver[] componentsInChildren = GetComponentsInChildren<global::UnityEngine.U2D.Animation.SpriteResolver>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ResolveSpriteToSpriteRenderer();
			}
		}

		internal global::System.Collections.Generic.IEnumerable<string> GetEntryNames(string category)
		{
			UpdateCacheOverridesIfNeeded();
			if (m_CategoryEntryCache.ContainsKey(category))
			{
				return m_CategoryEntryCache[category];
			}
			return null;
		}

		internal void CacheOverrides()
		{
			m_PreviousSpriteLibraryAsset = 0;
			m_PreviousModificationHash = 0L;
			m_CategoryEntryHashCache = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.U2D.Animation.SpriteLibrary.CategoryEntrySprite>();
			m_CategoryEntryCache = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.HashSet<string>>();
			if ((bool)m_SpriteLibraryAsset)
			{
				m_PreviousSpriteLibraryAsset = m_SpriteLibraryAsset.GetInstanceID();
				m_PreviousModificationHash = m_SpriteLibraryAsset.modificationHash;
				foreach (global::UnityEngine.U2D.Animation.SpriteLibCategory category in m_SpriteLibraryAsset.categories)
				{
					string text = category.name;
					m_CategoryEntryCache.Add(text, new global::System.Collections.Generic.HashSet<string>());
					global::System.Collections.Generic.HashSet<string> hashSet = m_CategoryEntryCache[text];
					foreach (global::UnityEngine.U2D.Animation.SpriteCategoryEntry category2 in category.categoryList)
					{
						m_CategoryEntryHashCache.Add(GetHashForCategoryAndEntry(text, category2.name), new global::UnityEngine.U2D.Animation.SpriteLibrary.CategoryEntrySprite
						{
							category = text,
							entry = category2.name,
							sprite = category2.sprite
						});
						hashSet.Add(category2.name);
					}
				}
			}
			foreach (global::UnityEngine.U2D.Animation.SpriteLibCategory item in m_Library)
			{
				string text2 = item.name;
				if (!m_CategoryEntryCache.ContainsKey(text2))
				{
					m_CategoryEntryCache.Add(text2, new global::System.Collections.Generic.HashSet<string>());
				}
				global::System.Collections.Generic.HashSet<string> hashSet2 = m_CategoryEntryCache[text2];
				foreach (global::UnityEngine.U2D.Animation.SpriteCategoryEntry category3 in item.categoryList)
				{
					if (!hashSet2.Contains(category3.name))
					{
						hashSet2.Add(category3.name);
					}
					int hashForCategoryAndEntry = GetHashForCategoryAndEntry(text2, category3.name);
					if (!m_CategoryEntryHashCache.ContainsKey(hashForCategoryAndEntry))
					{
						m_CategoryEntryHashCache.Add(hashForCategoryAndEntry, new global::UnityEngine.U2D.Animation.SpriteLibrary.CategoryEntrySprite
						{
							category = text2,
							entry = category3.name,
							sprite = category3.sprite
						});
					}
					else
					{
						global::UnityEngine.U2D.Animation.SpriteLibrary.CategoryEntrySprite value = m_CategoryEntryHashCache[hashForCategoryAndEntry];
						value.sprite = category3.sprite;
						m_CategoryEntryHashCache[hashForCategoryAndEntry] = value;
					}
				}
			}
		}
	}
}
