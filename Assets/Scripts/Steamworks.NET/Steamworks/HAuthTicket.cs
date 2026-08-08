namespace Steamworks
{
	[global::System.Serializable]
	public struct HAuthTicket : global::System.IEquatable<global::Steamworks.HAuthTicket>, global::System.IComparable<global::Steamworks.HAuthTicket>
	{
		public static readonly global::Steamworks.HAuthTicket Invalid = new global::Steamworks.HAuthTicket(0u);

		public uint m_HAuthTicket;

		public HAuthTicket(uint value)
		{
			m_HAuthTicket = value;
		}

		public override string ToString()
		{
			return m_HAuthTicket.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HAuthTicket)
			{
				return this == (global::Steamworks.HAuthTicket)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HAuthTicket.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HAuthTicket x, global::Steamworks.HAuthTicket y)
		{
			return x.m_HAuthTicket == y.m_HAuthTicket;
		}

		public static bool operator !=(global::Steamworks.HAuthTicket x, global::Steamworks.HAuthTicket y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HAuthTicket(uint value)
		{
			return new global::Steamworks.HAuthTicket(value);
		}

		public static explicit operator uint(global::Steamworks.HAuthTicket that)
		{
			return that.m_HAuthTicket;
		}

		public bool Equals(global::Steamworks.HAuthTicket other)
		{
			return m_HAuthTicket == other.m_HAuthTicket;
		}

		public int CompareTo(global::Steamworks.HAuthTicket other)
		{
			return m_HAuthTicket.CompareTo(other.m_HAuthTicket);
		}
	}
}
