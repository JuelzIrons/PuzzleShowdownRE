namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamLeaderboard_t : global::System.IEquatable<global::Steamworks.SteamLeaderboard_t>, global::System.IComparable<global::Steamworks.SteamLeaderboard_t>
	{
		public ulong m_SteamLeaderboard;

		public SteamLeaderboard_t(ulong value)
		{
			m_SteamLeaderboard = value;
		}

		public override string ToString()
		{
			return m_SteamLeaderboard.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SteamLeaderboard_t)
			{
				return this == (global::Steamworks.SteamLeaderboard_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamLeaderboard.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SteamLeaderboard_t x, global::Steamworks.SteamLeaderboard_t y)
		{
			return x.m_SteamLeaderboard == y.m_SteamLeaderboard;
		}

		public static bool operator !=(global::Steamworks.SteamLeaderboard_t x, global::Steamworks.SteamLeaderboard_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SteamLeaderboard_t(ulong value)
		{
			return new global::Steamworks.SteamLeaderboard_t(value);
		}

		public static explicit operator ulong(global::Steamworks.SteamLeaderboard_t that)
		{
			return that.m_SteamLeaderboard;
		}

		public bool Equals(global::Steamworks.SteamLeaderboard_t other)
		{
			return m_SteamLeaderboard == other.m_SteamLeaderboard;
		}

		public int CompareTo(global::Steamworks.SteamLeaderboard_t other)
		{
			return m_SteamLeaderboard.CompareTo(other.m_SteamLeaderboard);
		}
	}
}
