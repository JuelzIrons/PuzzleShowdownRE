namespace TMPro
{
	public struct HighlightState
	{
		public global::UnityEngine.Color32 color;

		public global::TMPro.TMP_Offset padding;

		public HighlightState(global::UnityEngine.Color32 color, global::TMPro.TMP_Offset padding)
		{
			this.color = color;
			this.padding = padding;
		}

		public static bool operator ==(global::TMPro.HighlightState lhs, global::TMPro.HighlightState rhs)
		{
			if (lhs.color.Compare(rhs.color))
			{
				return lhs.padding == rhs.padding;
			}
			return false;
		}

		public static bool operator !=(global::TMPro.HighlightState lhs, global::TMPro.HighlightState rhs)
		{
			return !(lhs == rhs);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(global::TMPro.HighlightState other)
		{
			return base.Equals((object)other);
		}
	}
}
