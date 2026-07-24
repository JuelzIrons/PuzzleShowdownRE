namespace UnityEngine.U2D.Animation
{
	[global::System.Serializable]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	internal class SpriteLibCategory : global::UnityEngine.U2D.Animation.INameHash, global::UnityEngine.U2D.Animation.ISpriteLibraryCategory
	{
		[global::UnityEngine.SerializeField]
		private string m_Name;

		[global::UnityEngine.SerializeField]
		private int m_Hash;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntry> m_CategoryList;

		public string name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
				m_Hash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(m_Name);
			}
		}

		public int hash => m_Hash;

		public global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteCategoryEntry> categoryList
		{
			get
			{
				return m_CategoryList;
			}
			set
			{
				m_CategoryList = value;
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.U2D.Animation.ISpriteLibraryLabel> labels => m_CategoryList;

		public void UpdateHash()
		{
			m_Hash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(m_Name);
			foreach (global::UnityEngine.U2D.Animation.SpriteCategoryEntry category in m_CategoryList)
			{
				category.UpdateHash();
			}
		}

		internal void ValidateLabels(bool log = true)
		{
			global::UnityEngine.U2D.Animation.SpriteLibraryAsset.RenameDuplicate(m_CategoryList, delegate(string originalName, string newName)
			{
				if (log)
				{
					global::UnityEngine.Debug.LogWarning($"Label {originalName} renamed to {newName} due to hash clash");
				}
			});
		}
	}
}
