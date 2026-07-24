namespace UnityEngine.UI
{
	[global::System.Serializable]
	public struct ColorBlock : global::System.IEquatable<global::UnityEngine.UI.ColorBlock>
	{
		[global::UnityEngine.Serialization.FormerlySerializedAs("normalColor")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_NormalColor;

		[global::UnityEngine.Serialization.FormerlySerializedAs("highlightedColor")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_HighlightedColor;

		[global::UnityEngine.Serialization.FormerlySerializedAs("pressedColor")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_PressedColor;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_HighlightedColor")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_SelectedColor;

		[global::UnityEngine.Serialization.FormerlySerializedAs("disabledColor")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_DisabledColor;

		[global::UnityEngine.Range(1f, 5f)]
		[global::UnityEngine.SerializeField]
		private float m_ColorMultiplier;

		[global::UnityEngine.Serialization.FormerlySerializedAs("fadeDuration")]
		[global::UnityEngine.SerializeField]
		private float m_FadeDuration;

		public static global::UnityEngine.UI.ColorBlock defaultColorBlock;

		public global::UnityEngine.Color normalColor
		{
			get
			{
				return m_NormalColor;
			}
			set
			{
				m_NormalColor = value;
			}
		}

		public global::UnityEngine.Color highlightedColor
		{
			get
			{
				return m_HighlightedColor;
			}
			set
			{
				m_HighlightedColor = value;
			}
		}

		public global::UnityEngine.Color pressedColor
		{
			get
			{
				return m_PressedColor;
			}
			set
			{
				m_PressedColor = value;
			}
		}

		public global::UnityEngine.Color selectedColor
		{
			get
			{
				return m_SelectedColor;
			}
			set
			{
				m_SelectedColor = value;
			}
		}

		public global::UnityEngine.Color disabledColor
		{
			get
			{
				return m_DisabledColor;
			}
			set
			{
				m_DisabledColor = value;
			}
		}

		public float colorMultiplier
		{
			get
			{
				return m_ColorMultiplier;
			}
			set
			{
				m_ColorMultiplier = value;
			}
		}

		public float fadeDuration
		{
			get
			{
				return m_FadeDuration;
			}
			set
			{
				m_FadeDuration = value;
			}
		}

		static ColorBlock()
		{
			defaultColorBlock = new global::UnityEngine.UI.ColorBlock
			{
				m_NormalColor = new global::UnityEngine.Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue),
				m_HighlightedColor = new global::UnityEngine.Color32(245, 245, 245, byte.MaxValue),
				m_PressedColor = new global::UnityEngine.Color32(200, 200, 200, byte.MaxValue),
				m_SelectedColor = new global::UnityEngine.Color32(245, 245, 245, byte.MaxValue),
				m_DisabledColor = new global::UnityEngine.Color32(200, 200, 200, 128),
				colorMultiplier = 1f,
				fadeDuration = 0.1f
			};
		}

		public override bool Equals(object obj)
		{
			if (!(obj is global::UnityEngine.UI.ColorBlock))
			{
				return false;
			}
			return Equals((global::UnityEngine.UI.ColorBlock)obj);
		}

		public bool Equals(global::UnityEngine.UI.ColorBlock other)
		{
			if (normalColor == other.normalColor && highlightedColor == other.highlightedColor && pressedColor == other.pressedColor && selectedColor == other.selectedColor && disabledColor == other.disabledColor && colorMultiplier == other.colorMultiplier)
			{
				return fadeDuration == other.fadeDuration;
			}
			return false;
		}

		public static bool operator ==(global::UnityEngine.UI.ColorBlock point1, global::UnityEngine.UI.ColorBlock point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(global::UnityEngine.UI.ColorBlock point1, global::UnityEngine.UI.ColorBlock point2)
		{
			return !point1.Equals(point2);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
