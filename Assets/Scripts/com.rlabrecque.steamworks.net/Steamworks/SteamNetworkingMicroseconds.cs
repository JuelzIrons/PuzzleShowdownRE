namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamNetworkingMicroseconds : global::System.IEquatable<global::Steamworks.SteamNetworkingMicroseconds>, global::System.IComparable<global::Steamworks.SteamNetworkingMicroseconds>
	{
		public long m_SteamNetworkingMicroseconds;

		public SteamNetworkingMicroseconds(long value)
		{
			m_SteamNetworkingMicroseconds = value;
		}

		public override string ToString()
		{
			return m_SteamNetworkingMicroseconds.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SteamNetworkingMicroseconds)
			{
				return this == (global::Steamworks.SteamNetworkingMicroseconds)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamNetworkingMicroseconds.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SteamNetworkingMicroseconds x, global::Steamworks.SteamNetworkingMicroseconds y)
		{
			return x.m_SteamNetworkingMicroseconds == y.m_SteamNetworkingMicroseconds;
		}

		public static bool operator !=(global::Steamworks.SteamNetworkingMicroseconds x, global::Steamworks.SteamNetworkingMicroseconds y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SteamNetworkingMicroseconds(long value)
		{
			return new global::Steamworks.SteamNetworkingMicroseconds(value);
		}

		public static explicit operator long(global::Steamworks.SteamNetworkingMicroseconds that)
		{
			return that.m_SteamNetworkingMicroseconds;
		}

		public bool Equals(global::Steamworks.SteamNetworkingMicroseconds other)
		{
			return m_SteamNetworkingMicroseconds == other.m_SteamNetworkingMicroseconds;
		}

		public int CompareTo(global::Steamworks.SteamNetworkingMicroseconds other)
		{
			return m_SteamNetworkingMicroseconds.CompareTo(other.m_SteamNetworkingMicroseconds);
		}
	}
}
