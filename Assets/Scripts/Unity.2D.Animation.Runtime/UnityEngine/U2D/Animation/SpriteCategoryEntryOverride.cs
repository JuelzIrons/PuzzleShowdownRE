namespace UnityEngine.U2D.Animation
{
	[global::System.Serializable]
	internal class SpriteCategoryEntryOverride : global::UnityEngine.U2D.Animation.SpriteCategoryEntry
	{
		[global::UnityEngine.SerializeField]
		private bool m_FromMain;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_SpriteOverride;

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

		public global::UnityEngine.Sprite spriteOverride
		{
			get
			{
				return m_SpriteOverride;
			}
			set
			{
				m_SpriteOverride = value;
			}
		}
	}
}
