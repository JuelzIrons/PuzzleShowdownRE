namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamLeaderboardEntries_t : global::System.IEquatable<global::Steamworks.SteamLeaderboardEntries_t>, global::System.IComparable<global::Steamworks.SteamLeaderboardEntries_t>
	{
		public ulong m_SteamLeaderboardEntries;

		public SteamLeaderboardEntries_t(ulong value)
		{
			m_SteamLeaderboardEntries = value;
		}

		public override string ToString()
		{
			return m_SteamLeaderboardEntries.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SteamLeaderboardEntries_t)
			{
				return this == (global::Steamworks.SteamLeaderboardEntries_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamLeaderboardEntries.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SteamLeaderboardEntries_t x, global::Steamworks.SteamLeaderboardEntries_t y)
		{
			return x.m_SteamLeaderboardEntries == y.m_SteamLeaderboardEntries;
		}

		public static bool operator !=(global::Steamworks.SteamLeaderboardEntries_t x, global::Steamworks.SteamLeaderboardEntries_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SteamLeaderboardEntries_t(ulong value)
		{
			return new global::Steamworks.SteamLeaderboardEntries_t(value);
		}

		public static explicit operator ulong(global::Steamworks.SteamLeaderboardEntries_t that)
		{
			return that.m_SteamLeaderboardEntries;
		}

		public bool Equals(global::Steamworks.SteamLeaderboardEntries_t other)
		{
			return m_SteamLeaderboardEntries == other.m_SteamLeaderboardEntries;
		}

		public int CompareTo(global::Steamworks.SteamLeaderboardEntries_t other)
		{
			return m_SteamLeaderboardEntries.CompareTo(other.m_SteamLeaderboardEntries);
		}
	}
}
