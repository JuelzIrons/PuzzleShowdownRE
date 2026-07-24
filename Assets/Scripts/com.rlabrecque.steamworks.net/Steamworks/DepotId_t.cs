namespace Steamworks
{
	[global::System.Serializable]
	public struct DepotId_t : global::System.IEquatable<global::Steamworks.DepotId_t>, global::System.IComparable<global::Steamworks.DepotId_t>
	{
		public static readonly global::Steamworks.DepotId_t Invalid = new global::Steamworks.DepotId_t(0u);

		public uint m_DepotId;

		public DepotId_t(uint value)
		{
			m_DepotId = value;
		}

		public override string ToString()
		{
			return m_DepotId.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.DepotId_t)
			{
				return this == (global::Steamworks.DepotId_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_DepotId.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.DepotId_t x, global::Steamworks.DepotId_t y)
		{
			return x.m_DepotId == y.m_DepotId;
		}

		public static bool operator !=(global::Steamworks.DepotId_t x, global::Steamworks.DepotId_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.DepotId_t(uint value)
		{
			return new global::Steamworks.DepotId_t(value);
		}

		public static explicit operator uint(global::Steamworks.DepotId_t that)
		{
			return that.m_DepotId;
		}

		public bool Equals(global::Steamworks.DepotId_t other)
		{
			return m_DepotId == other.m_DepotId;
		}

		public int CompareTo(global::Steamworks.DepotId_t other)
		{
			return m_DepotId.CompareTo(other.m_DepotId);
		}
	}
}
