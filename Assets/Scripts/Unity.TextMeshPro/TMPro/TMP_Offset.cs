namespace TMPro
{
	public struct TMP_Offset
	{
		private float m_Left;

		private float m_Right;

		private float m_Top;

		private float m_Bottom;

		private static readonly global::TMPro.TMP_Offset k_ZeroOffset = new global::TMPro.TMP_Offset(0f, 0f, 0f, 0f);

		public float left
		{
			get
			{
				return m_Left;
			}
			set
			{
				m_Left = value;
			}
		}

		public float right
		{
			get
			{
				return m_Right;
			}
			set
			{
				m_Right = value;
			}
		}

		public float top
		{
			get
			{
				return m_Top;
			}
			set
			{
				m_Top = value;
			}
		}

		public float bottom
		{
			get
			{
				return m_Bottom;
			}
			set
			{
				m_Bottom = value;
			}
		}

		public float horizontal
		{
			get
			{
				return m_Left;
			}
			set
			{
				m_Left = value;
				m_Right = value;
			}
		}

		public float vertical
		{
			get
			{
				return m_Top;
			}
			set
			{
				m_Top = value;
				m_Bottom = value;
			}
		}

		public static global::TMPro.TMP_Offset zero => k_ZeroOffset;

		public TMP_Offset(float left, float right, float top, float bottom)
		{
			m_Left = left;
			m_Right = right;
			m_Top = top;
			m_Bottom = bottom;
		}

		public TMP_Offset(float horizontal, float vertical)
		{
			m_Left = horizontal;
			m_Right = horizontal;
			m_Top = vertical;
			m_Bottom = vertical;
		}

		public static bool operator ==(global::TMPro.TMP_Offset lhs, global::TMPro.TMP_Offset rhs)
		{
			if (lhs.m_Left == rhs.m_Left && lhs.m_Right == rhs.m_Right && lhs.m_Top == rhs.m_Top)
			{
				return lhs.m_Bottom == rhs.m_Bottom;
			}
			return false;
		}

		public static bool operator !=(global::TMPro.TMP_Offset lhs, global::TMPro.TMP_Offset rhs)
		{
			return !(lhs == rhs);
		}

		public static global::TMPro.TMP_Offset operator *(global::TMPro.TMP_Offset a, float b)
		{
			return new global::TMPro.TMP_Offset(a.m_Left * b, a.m_Right * b, a.m_Top * b, a.m_Bottom * b);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(global::TMPro.TMP_Offset other)
		{
			return base.Equals((object)other);
		}
	}
}
