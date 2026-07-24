namespace UnityEngine.U2D.Animation
{
	[global::System.Serializable]
	internal class SpriteLibCategoryOverride : global::UnityEngine.U2D.Animation.SpriteLibCategory
	{
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntryOverride> m_OverrideEntries;

		[global::UnityEngine.SerializeField]
		private bool m_FromMain;

		[global::UnityEngine.SerializeField]
		private int m_EntryOverrideCount;

		public bool fromMain
		{
			get
			{
				return m_FromMain;
			}
			set
			{
				m_FromMain = value;
			}
		}

		public int entryOverrideCount
		{
			get
			{
				return m_EntryOverrideCount;
			}
			set
			{
				m_EntryOverrideCount = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntryOverride> overrideEntries
		{
			get
			{
				return m_OverrideEntries;
			}
			set
			{
				m_OverrideEntries = value;
			}
		}

		public void UpdateOverrideCount()
		{
			int num = 0;
			if (fromMain)
			{
				foreach (global::UnityEngine.U2D.Animation.SpriteCategoryEntryOverride overrideEntry in overrideEntries)
				{
					if (!overrideEntry.fromMain || overrideEntry.sprite != overrideEntry.spriteOverride)
					{
						num++;
					}
				}
			}
			else
			{
				num = overrideEntries?.Count ?? 0;
			}
			entryOverrideCount = num;
		}

		public void RenameDuplicateOverrideEntries()
		{
			if (overrideEntries != null)
			{
				global::UnityEngine.U2D.Animation.SpriteLibraryAsset.RenameDuplicate(overrideEntries, delegate
				{
				});
			}
		}
	}
}
