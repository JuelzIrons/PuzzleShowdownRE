namespace Steamworks
{
	[global::System.Serializable]
	public struct RemotePlaySessionID_t : global::System.IEquatable<global::Steamworks.RemotePlaySessionID_t>, global::System.IComparable<global::Steamworks.RemotePlaySessionID_t>
	{
		public uint m_RemotePlaySessionID;

		public RemotePlaySessionID_t(uint value)
		{
			m_RemotePlaySessionID = value;
		}

		public override string ToString()
		{
			return m_RemotePlaySessionID.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.RemotePlaySessionID_t)
			{
				return this == (global::Steamworks.RemotePlaySessionID_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_RemotePlaySessionID.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.RemotePlaySessionID_t x, global::Steamworks.RemotePlaySessionID_t y)
		{
			return x.m_RemotePlaySessionID == y.m_RemotePlaySessionID;
		}

		public static bool operator !=(global::Steamworks.RemotePlaySessionID_t x, global::Steamworks.RemotePlaySessionID_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.RemotePlaySessionID_t(uint value)
		{
			return new global::Steamworks.RemotePlaySessionID_t(value);
		}

		public static explicit operator uint(global::Steamworks.RemotePlaySessionID_t that)
		{
			return that.m_RemotePlaySessionID;
		}

		public bool Equals(global::Steamworks.RemotePlaySessionID_t other)
		{
			return m_RemotePlaySessionID == other.m_RemotePlaySessionID;
		}

		public int CompareTo(global::Steamworks.RemotePlaySessionID_t other)
		{
			return m_RemotePlaySessionID.CompareTo(other.m_RemotePlaySessionID);
		}
	}
}
