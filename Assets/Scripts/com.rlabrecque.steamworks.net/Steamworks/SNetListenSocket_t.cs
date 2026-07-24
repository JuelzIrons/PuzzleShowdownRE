namespace Steamworks
{
	[global::System.Serializable]
	public struct SNetListenSocket_t : global::System.IEquatable<global::Steamworks.SNetListenSocket_t>, global::System.IComparable<global::Steamworks.SNetListenSocket_t>
	{
		public uint m_SNetListenSocket;

		public SNetListenSocket_t(uint value)
		{
			m_SNetListenSocket = value;
		}

		public override string ToString()
		{
			return m_SNetListenSocket.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SNetListenSocket_t)
			{
				return this == (global::Steamworks.SNetListenSocket_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SNetListenSocket.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SNetListenSocket_t x, global::Steamworks.SNetListenSocket_t y)
		{
			return x.m_SNetListenSocket == y.m_SNetListenSocket;
		}

		public static bool operator !=(global::Steamworks.SNetListenSocket_t x, global::Steamworks.SNetListenSocket_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SNetListenSocket_t(uint value)
		{
			return new global::Steamworks.SNetListenSocket_t(value);
		}

		public static explicit operator uint(global::Steamworks.SNetListenSocket_t that)
		{
			return that.m_SNetListenSocket;
		}

		public bool Equals(global::Steamworks.SNetListenSocket_t other)
		{
			return m_SNetListenSocket == other.m_SNetListenSocket;
		}

		public int CompareTo(global::Steamworks.SNetListenSocket_t other)
		{
			return m_SNetListenSocket.CompareTo(other.m_SNetListenSocket);
		}
	}
}
