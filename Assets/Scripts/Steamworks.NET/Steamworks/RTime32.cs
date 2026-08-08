namespace Steamworks
{
	[global::System.Serializable]
	public struct RTime32 : global::System.IEquatable<global::Steamworks.RTime32>, global::System.IComparable<global::Steamworks.RTime32>
	{
		public uint m_RTime32;

		public RTime32(uint value)
		{
			m_RTime32 = value;
		}

		public override string ToString()
		{
			return m_RTime32.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.RTime32)
			{
				return this == (global::Steamworks.RTime32)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_RTime32.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.RTime32 x, global::Steamworks.RTime32 y)
		{
			return x.m_RTime32 == y.m_RTime32;
		}

		public static bool operator !=(global::Steamworks.RTime32 x, global::Steamworks.RTime32 y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.RTime32(uint value)
		{
			return new global::Steamworks.RTime32(value);
		}

		public static explicit operator uint(global::Steamworks.RTime32 that)
		{
			return that.m_RTime32;
		}

		public bool Equals(global::Steamworks.RTime32 other)
		{
			return m_RTime32 == other.m_RTime32;
		}

		public int CompareTo(global::Steamworks.RTime32 other)
		{
			return m_RTime32.CompareTo(other.m_RTime32);
		}
	}
}
