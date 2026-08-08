namespace Steamworks
{
	[global::System.Serializable]
	public struct HSteamUser : global::System.IEquatable<global::Steamworks.HSteamUser>, global::System.IComparable<global::Steamworks.HSteamUser>
	{
		public int m_HSteamUser;

		public HSteamUser(int value)
		{
			m_HSteamUser = value;
		}

		public override string ToString()
		{
			return m_HSteamUser.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HSteamUser)
			{
				return this == (global::Steamworks.HSteamUser)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HSteamUser.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HSteamUser x, global::Steamworks.HSteamUser y)
		{
			return x.m_HSteamUser == y.m_HSteamUser;
		}

		public static bool operator !=(global::Steamworks.HSteamUser x, global::Steamworks.HSteamUser y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HSteamUser(int value)
		{
			return new global::Steamworks.HSteamUser(value);
		}

		public static explicit operator int(global::Steamworks.HSteamUser that)
		{
			return that.m_HSteamUser;
		}

		public bool Equals(global::Steamworks.HSteamUser other)
		{
			return m_HSteamUser == other.m_HSteamUser;
		}

		public int CompareTo(global::Steamworks.HSteamUser other)
		{
			return m_HSteamUser.CompareTo(other.m_HSteamUser);
		}
	}
}
