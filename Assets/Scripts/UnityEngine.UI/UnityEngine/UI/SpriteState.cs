namespace UnityEngine.UI
{
	[global::System.Serializable]
	public struct SpriteState : global::System.IEquatable<global::UnityEngine.UI.SpriteState>
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_HighlightedSprite;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_PressedSprite;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_HighlightedSprite")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_SelectedSprite;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_DisabledSprite;

		public global::UnityEngine.Sprite highlightedSprite
		{
			get
			{
				return m_HighlightedSprite;
			}
			set
			{
				m_HighlightedSprite = value;
			}
		}

		public global::UnityEngine.Sprite pressedSprite
		{
			get
			{
				return m_PressedSprite;
			}
			set
			{
				m_PressedSprite = value;
			}
		}

		public global::UnityEngine.Sprite selectedSprite
		{
			get
			{
				return m_SelectedSprite;
			}
			set
			{
				m_SelectedSprite = value;
			}
		}

		public global::UnityEngine.Sprite disabledSprite
		{
			get
			{
				return m_DisabledSprite;
			}
			set
			{
				m_DisabledSprite = value;
			}
		}

		public bool Equals(global::UnityEngine.UI.SpriteState other)
		{
			if (highlightedSprite == other.highlightedSprite && pressedSprite == other.pressedSprite && selectedSprite == other.selectedSprite)
			{
				return disabledSprite == other.disabledSprite;
			}
			return false;
		}
	}
}
