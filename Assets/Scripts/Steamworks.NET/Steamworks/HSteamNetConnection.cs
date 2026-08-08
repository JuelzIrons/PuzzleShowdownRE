namespace Steamworks
{
	[global::System.Serializable]
	public struct HSteamNetConnection : global::System.IEquatable<global::Steamworks.HSteamNetConnection>, global::System.IComparable<global::Steamworks.HSteamNetConnection>
	{
		public static readonly global::Steamworks.HSteamNetConnection Invalid = new global::Steamworks.HSteamNetConnection(0u);

		public uint m_HSteamNetConnection;

		public HSteamNetConnection(uint value)
		{
			m_HSteamNetConnection = value;
		}

		public override string ToString()
		{
			return m_HSteamNetConnection.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HSteamNetConnection)
			{
				return this == (global::Steamworks.HSteamNetConnection)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HSteamNetConnection.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HSteamNetConnection x, global::Steamworks.HSteamNetConnection y)
		{
			return x.m_HSteamNetConnection == y.m_HSteamNetConnection;
		}

		public static bool operator !=(global::Steamworks.HSteamNetConnection x, global::Steamworks.HSteamNetConnection y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HSteamNetConnection(uint value)
		{
			return new global::Steamworks.HSteamNetConnection(value);
		}

		public static explicit operator uint(global::Steamworks.HSteamNetConnection that)
		{
			return that.m_HSteamNetConnection;
		}

		public bool Equals(global::Steamworks.HSteamNetConnection other)
		{
			return m_HSteamNetConnection == other.m_HSteamNetConnection;
		}

		public int CompareTo(global::Steamworks.HSteamNetConnection other)
		{
			return m_HSteamNetConnection.CompareTo(other.m_HSteamNetConnection);
		}
	}
}
