namespace UnityEngine.U2D.Animation
{
	[global::System.Serializable]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	internal class SpriteCategoryEntry : global::UnityEngine.U2D.Animation.INameHash, global::UnityEngine.U2D.Animation.ISpriteLibraryLabel
	{
		[global::UnityEngine.SerializeField]
		private string m_Name;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Hash;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_Sprite;

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

		public global::UnityEngine.Sprite sprite
		{
			get
			{
				return m_Sprite;
			}
			set
			{
				m_Sprite = value;
			}
		}

		public void UpdateHash()
		{
			m_Hash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(m_Name);
		}
	}
}
