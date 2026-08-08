namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamItemDef_t : global::System.IEquatable<global::Steamworks.SteamItemDef_t>, global::System.IComparable<global::Steamworks.SteamItemDef_t>
	{
		public int m_SteamItemDef;

		public SteamItemDef_t(int value)
		{
			m_SteamItemDef = value;
		}

		public override string ToString()
		{
			return m_SteamItemDef.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SteamItemDef_t)
			{
				return this == (global::Steamworks.SteamItemDef_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamItemDef.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SteamItemDef_t x, global::Steamworks.SteamItemDef_t y)
		{
			return x.m_SteamItemDef == y.m_SteamItemDef;
		}

		public static bool operator !=(global::Steamworks.SteamItemDef_t x, global::Steamworks.SteamItemDef_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SteamItemDef_t(int value)
		{
			return new global::Steamworks.SteamItemDef_t(value);
		}

		public static explicit operator int(global::Steamworks.SteamItemDef_t that)
		{
			return that.m_SteamItemDef;
		}

		public bool Equals(global::Steamworks.SteamItemDef_t other)
		{
			return m_SteamItemDef == other.m_SteamItemDef;
		}

		public int CompareTo(global::Steamworks.SteamItemDef_t other)
		{
			return m_SteamItemDef.CompareTo(other.m_SteamItemDef);
		}
	}
}
