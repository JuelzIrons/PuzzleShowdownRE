namespace UnityEngine.U2D.Animation
{
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.animation@latest/index.html?subfolder=/manual/AssetUpgrader.html%23upgrading-sprite-libraries")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	public class SpriteLibraryAsset : global::UnityEngine.ScriptableObject
	{
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategory> m_Labels = new global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategory>();

		[global::UnityEngine.SerializeField]
		private long m_ModificationHash;

		[global::UnityEngine.SerializeField]
		private int m_Version;

		internal global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategory> categories
		{
			get
			{
				return m_Labels;
			}
			set
			{
				m_Labels = value;
				ValidateCategories();
			}
		}

		internal long modificationHash
		{
			get
			{
				return m_ModificationHash;
			}
			set
			{
				m_ModificationHash = value;
			}
		}

		internal int version
		{
			set
			{
				m_Version = value;
			}
		}

		internal static global::UnityEngine.U2D.Animation.SpriteLibraryAsset CreateAsset(global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategory> categories, string assetName, long modificationHash)
		{
			global::UnityEngine.U2D.Animation.SpriteLibraryAsset spriteLibraryAsset = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.U2D.Animation.SpriteLibraryAsset>();
			spriteLibraryAsset.m_Labels = categories;
			spriteLibraryAsset.ValidateCategories();
			spriteLibraryAsset.name = assetName;
			spriteLibraryAsset.UpdateHashes();
			spriteLibraryAsset.m_ModificationHash = modificationHash;
			spriteLibraryAsset.version = 1;
			return spriteLibraryAsset;
		}

		private void OnEnable()
		{
			if (m_Version < 1)
			{
				UpdateToVersionOne();
			}
		}

		private void UpdateToVersionOne()
		{
			UpdateHashes();
			m_Version = 1;
		}

		internal global::UnityEngine.Sprite GetSprite(int categoryHash, int labelHash)
		{
			global::UnityEngine.U2D.Animation.SpriteLibCategory spriteLibCategory = global::System.Linq.Enumerable.FirstOrDefault(m_Labels, (global::UnityEngine.U2D.Animation.SpriteLibCategory x) => x.hash == categoryHash);
			if (spriteLibCategory != null)
			{
				global::UnityEngine.U2D.Animation.SpriteCategoryEntry spriteCategoryEntry = global::System.Linq.Enumerable.FirstOrDefault(spriteLibCategory.categoryList, (global::UnityEngine.U2D.Animation.SpriteCategoryEntry x) => x.hash == labelHash);
				if (spriteCategoryEntry != null)
				{
					return spriteCategoryEntry.sprite;
				}
			}
			return null;
		}

		internal global::UnityEngine.Sprite GetSprite(int categoryHash, int labelHash, out bool validEntry)
		{
			global::UnityEngine.U2D.Animation.SpriteLibCategory spriteLibCategory = null;
			for (int i = 0; i < m_Labels.Count; i++)
			{
				if (m_Labels[i].hash == categoryHash)
				{
					spriteLibCategory = m_Labels[i];
					break;
				}
			}
			if (spriteLibCategory != null)
			{
				global::UnityEngine.U2D.Animation.SpriteCategoryEntry spriteCategoryEntry = null;
				for (int j = 0; j < spriteLibCategory.categoryList.Count; j++)
				{
					if (spriteLibCategory.categoryList[j].hash == labelHash)
					{
						spriteCategoryEntry = spriteLibCategory.categoryList[j];
						break;
					}
				}
				if (spriteCategoryEntry != null)
				{
					validEntry = true;
					return spriteCategoryEntry.sprite;
				}
			}
			validEntry = false;
			return null;
		}

		public global::UnityEngine.Sprite GetSprite(string category, string label)
		{
			int categoryHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(category);
			int labelHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(label);
			return GetSprite(categoryHash, labelHash);
		}

		public global::System.Collections.Generic.IEnumerable<string> GetCategoryNames()
		{
			return global::System.Linq.Enumerable.Select(m_Labels, (global::UnityEngine.U2D.Animation.SpriteLibCategory x) => x.name);
		}

		[global::System.Obsolete("GetCategorylabelNames has been deprecated. Please use GetCategoryLabelNames (UnityUpgradable) -> GetCategoryLabelNames(*)")]
		public global::System.Collections.Generic.IEnumerable<string> GetCategorylabelNames(string category)
		{
			return GetCategoryLabelNames(category);
		}

		public global::System.Collections.Generic.IEnumerable<string> GetCategoryLabelNames(string category)
		{
			global::UnityEngine.U2D.Animation.SpriteLibCategory spriteLibCategory = global::System.Linq.Enumerable.FirstOrDefault(m_Labels, (global::UnityEngine.U2D.Animation.SpriteLibCategory x) => x.name == category);
			if (spriteLibCategory != null)
			{
				return global::System.Linq.Enumerable.Select(spriteLibCategory.categoryList, (global::UnityEngine.U2D.Animation.SpriteCategoryEntry x) => x.name);
			}
			return new string[0];
		}

		public void AddCategoryLabel(global::UnityEngine.Sprite sprite, string category, string label)
		{
			category = category.Trim();
			label = label?.Trim();
			if (string.IsNullOrEmpty(category))
			{
				throw new global::System.ArgumentException("Cannot add empty or null Category string");
			}
			int catHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(category);
			global::UnityEngine.U2D.Animation.SpriteCategoryEntry spriteCategoryEntry = null;
			global::UnityEngine.U2D.Animation.SpriteLibCategory spriteLibCategory = null;
			spriteLibCategory = global::System.Linq.Enumerable.FirstOrDefault(m_Labels, (global::UnityEngine.U2D.Animation.SpriteLibCategory x) => x.hash == catHash);
			if (spriteLibCategory != null)
			{
				if (string.IsNullOrEmpty(label))
				{
					throw new global::System.ArgumentException("Cannot add empty or null Label string");
				}
				int labelHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(label);
				spriteCategoryEntry = global::System.Linq.Enumerable.FirstOrDefault(spriteLibCategory.categoryList, (global::UnityEngine.U2D.Animation.SpriteCategoryEntry y) => y.hash == labelHash);
				if (spriteCategoryEntry != null)
				{
					spriteCategoryEntry.sprite = sprite;
					return;
				}
				spriteCategoryEntry = new global::UnityEngine.U2D.Animation.SpriteCategoryEntry
				{
					name = label,
					sprite = sprite
				};
				spriteLibCategory.categoryList.Add(spriteCategoryEntry);
			}
			else
			{
				global::UnityEngine.U2D.Animation.SpriteLibCategory spriteLibCategory2 = new global::UnityEngine.U2D.Animation.SpriteLibCategory
				{
					categoryList = new global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntry>(),
					name = category
				};
				if (!string.IsNullOrEmpty(label))
				{
					spriteLibCategory2.categoryList.Add(new global::UnityEngine.U2D.Animation.SpriteCategoryEntry
					{
						name = label,
						sprite = sprite
					});
				}
				m_Labels.Add(spriteLibCategory2);
			}
		}

		public void RemoveCategoryLabel(string category, string label, bool deleteCategory)
		{
			int catHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(category);
			global::UnityEngine.U2D.Animation.SpriteLibCategory libCategory = null;
			libCategory = global::System.Linq.Enumerable.FirstOrDefault(m_Labels, (global::UnityEngine.U2D.Animation.SpriteLibCategory x) => x.hash == catHash);
			if (libCategory == null)
			{
				return;
			}
			int labelHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(label);
			libCategory.categoryList.RemoveAll((global::UnityEngine.U2D.Animation.SpriteCategoryEntry x) => x.hash == labelHash);
			if (deleteCategory && libCategory.categoryList.Count == 0)
			{
				m_Labels.RemoveAll((global::UnityEngine.U2D.Animation.SpriteLibCategory x) => x.hash == libCategory.hash);
			}
		}

		internal void UpdateHashes()
		{
			foreach (global::UnityEngine.U2D.Animation.SpriteLibCategory label in m_Labels)
			{
				label.UpdateHash();
			}
		}

		internal void ValidateCategories(bool log = true)
		{
			RenameDuplicate(m_Labels, delegate(string originalName, string newName)
			{
				if (log)
				{
					global::UnityEngine.Debug.LogWarning("Category " + originalName + " renamed to " + newName + " due to hash clash");
				}
			});
			for (int num = 0; num < m_Labels.Count; num++)
			{
				m_Labels[num].ValidateLabels(log);
			}
		}

		internal static void RenameDuplicate(global::System.Collections.Generic.IEnumerable<global::UnityEngine.U2D.Animation.INameHash> nameHashList, global::System.Action<string, string> onRename)
		{
			for (int i = 0; i < global::System.Linq.Enumerable.Count(nameHashList); i++)
			{
				global::UnityEngine.U2D.Animation.INameHash category = global::System.Linq.Enumerable.ElementAt(nameHashList, i);
				global::System.Collections.Generic.IEnumerable<global::UnityEngine.U2D.Animation.INameHash> source = global::System.Linq.Enumerable.Where(nameHashList, (global::UnityEngine.U2D.Animation.INameHash x) => (x.hash == category.hash || x.name == category.name) && x != category);
				int num = 0;
				for (int num2 = 0; num2 < global::System.Linq.Enumerable.Count(source); num2++)
				{
					global::UnityEngine.U2D.Animation.INameHash categoryClash = global::System.Linq.Enumerable.ElementAt(source, num2);
					for (; num < 1000; num++)
					{
						string name = categoryClash.name;
						name = $"{name}_{num}";
						int nameHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(name);
						if (global::System.Linq.Enumerable.FirstOrDefault(nameHashList, (global::UnityEngine.U2D.Animation.INameHash x) => (x.hash == nameHash || x.name == name) && x != categoryClash) == null)
						{
							onRename(categoryClash.name, name);
							categoryClash.name = name;
							break;
						}
					}
				}
			}
		}
	}
}
