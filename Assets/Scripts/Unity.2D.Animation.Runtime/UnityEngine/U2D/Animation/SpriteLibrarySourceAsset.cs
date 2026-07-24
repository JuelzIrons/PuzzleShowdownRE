namespace UnityEngine.U2D.Animation
{
	internal class SpriteLibrarySourceAsset : global::UnityEngine.ScriptableObject
	{
		public const string defaultName = "New Sprite Library Asset";

		public const string extension = ".spriteLib";

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategoryOverride> m_Library = new global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategoryOverride>();

		[global::UnityEngine.SerializeField]
		private string m_PrimaryLibraryGUID;

		[global::UnityEngine.SerializeField]
		private long m_ModificationHash;

		[global::UnityEngine.SerializeField]
		private int m_Version = 1;

		public global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.U2D.Animation.SpriteLibCategoryOverride> library => m_Library;

		public string primaryLibraryGUID => m_PrimaryLibraryGUID;

		public long modificationHash => m_ModificationHash;

		public int version => m_Version;

		public void InitializeWithAsset(global::UnityEngine.U2D.Animation.SpriteLibrarySourceAsset source)
		{
			m_Library = new global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategoryOverride>(source.m_Library);
			m_PrimaryLibraryGUID = source.m_PrimaryLibraryGUID;
			m_ModificationHash = source.m_ModificationHash;
		}

		public void SetLibrary(global::System.Collections.Generic.IList<global::UnityEngine.U2D.Animation.SpriteLibCategoryOverride> newLibrary)
		{
			if (!m_Library.Equals(newLibrary))
			{
				m_Library = new global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteLibCategoryOverride>(newLibrary);
				UpdateModificationHash();
			}
		}

		public void SetPrimaryLibraryGUID(string newPrimaryLibraryGUID)
		{
			if (m_PrimaryLibraryGUID != newPrimaryLibraryGUID)
			{
				m_PrimaryLibraryGUID = newPrimaryLibraryGUID;
				UpdateModificationHash();
			}
		}

		public void AddCategory(global::UnityEngine.U2D.Animation.SpriteLibCategoryOverride newCategory)
		{
			if (!m_Library.Contains(newCategory))
			{
				m_Library.Add(newCategory);
				UpdateModificationHash();
			}
		}

		public void RemoveCategory(global::UnityEngine.U2D.Animation.SpriteLibCategoryOverride categoryToRemove)
		{
			if (m_Library.Contains(categoryToRemove))
			{
				m_Library.Remove(categoryToRemove);
				UpdateModificationHash();
			}
		}

		public void ClearCategories()
		{
			m_Library.Clear();
		}

		public void RemoveCategory(int indexToRemove)
		{
			if (indexToRemove >= 0 && indexToRemove < m_Library.Count)
			{
				m_Library.RemoveAt(indexToRemove);
				UpdateModificationHash();
			}
		}

		private void UpdateModificationHash()
		{
			m_ModificationHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GenerateHash();
		}
	}
}
