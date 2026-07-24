namespace UnityEngine.UI
{
	[global::System.Serializable]
	public struct Navigation : global::System.IEquatable<global::UnityEngine.UI.Navigation>
	{
		[global::System.Flags]
		public enum Mode
		{
			None = 0,
			Horizontal = 1,
			Vertical = 2,
			Automatic = 3,
			Explicit = 4
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Navigation.Mode m_Mode;

		[global::UnityEngine.Tooltip("Enables navigation to wrap around from last to first or first to last element. Does not work for automatic grid navigation")]
		[global::UnityEngine.SerializeField]
		private bool m_WrapAround;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Selectable m_SelectOnUp;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Selectable m_SelectOnDown;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Selectable m_SelectOnLeft;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Selectable m_SelectOnRight;

		public global::UnityEngine.UI.Navigation.Mode mode
		{
			get
			{
				return m_Mode;
			}
			set
			{
				m_Mode = value;
			}
		}

		public bool wrapAround
		{
			get
			{
				return m_WrapAround;
			}
			set
			{
				m_WrapAround = value;
			}
		}

		public global::UnityEngine.UI.Selectable selectOnUp
		{
			get
			{
				return m_SelectOnUp;
			}
			set
			{
				m_SelectOnUp = value;
			}
		}

		public global::UnityEngine.UI.Selectable selectOnDown
		{
			get
			{
				return m_SelectOnDown;
			}
			set
			{
				m_SelectOnDown = value;
			}
		}

		public global::UnityEngine.UI.Selectable selectOnLeft
		{
			get
			{
				return m_SelectOnLeft;
			}
			set
			{
				m_SelectOnLeft = value;
			}
		}

		public global::UnityEngine.UI.Selectable selectOnRight
		{
			get
			{
				return m_SelectOnRight;
			}
			set
			{
				m_SelectOnRight = value;
			}
		}

		public static global::UnityEngine.UI.Navigation defaultNavigation => new global::UnityEngine.UI.Navigation
		{
			m_Mode = global::UnityEngine.UI.Navigation.Mode.Automatic,
			m_WrapAround = false
		};

		public bool Equals(global::UnityEngine.UI.Navigation other)
		{
			if (mode == other.mode && selectOnUp == other.selectOnUp && selectOnDown == other.selectOnDown && selectOnLeft == other.selectOnLeft)
			{
				return selectOnRight == other.selectOnRight;
			}
			return false;
		}
	}
}
