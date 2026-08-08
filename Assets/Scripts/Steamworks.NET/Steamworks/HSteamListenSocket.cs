namespace Steamworks
{
	[global::System.Serializable]
	public struct HSteamListenSocket : global::System.IEquatable<global::Steamworks.HSteamListenSocket>, global::System.IComparable<global::Steamworks.HSteamListenSocket>
	{
		public static readonly global::Steamworks.HSteamListenSocket Invalid = new global::Steamworks.HSteamListenSocket(0u);

		public uint m_HSteamListenSocket;

		public HSteamListenSocket(uint value)
		{
			m_HSteamListenSocket = value;
		}

		public override string ToString()
		{
			return m_HSteamListenSocket.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HSteamListenSocket)
			{
				return this == (global::Steamworks.HSteamListenSocket)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HSteamListenSocket.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HSteamListenSocket x, global::Steamworks.HSteamListenSocket y)
		{
			return x.m_HSteamListenSocket == y.m_HSteamListenSocket;
		}

		public static bool operator !=(global::Steamworks.HSteamListenSocket x, global::Steamworks.HSteamListenSocket y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HSteamListenSocket(uint value)
		{
			return new global::Steamworks.HSteamListenSocket(value);
		}

		public static explicit operator uint(global::Steamworks.HSteamListenSocket that)
		{
			return that.m_HSteamListenSocket;
		}

		public bool Equals(global::Steamworks.HSteamListenSocket other)
		{
			return m_HSteamListenSocket == other.m_HSteamListenSocket;
		}

		public int CompareTo(global::Steamworks.HSteamListenSocket other)
		{
			return m_HSteamListenSocket.CompareTo(other.m_HSteamListenSocket);
		}
	}
}
