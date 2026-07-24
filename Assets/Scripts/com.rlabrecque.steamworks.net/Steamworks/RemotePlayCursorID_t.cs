namespace Steamworks
{
	[global::System.Serializable]
	public struct RemotePlayCursorID_t : global::System.IEquatable<global::Steamworks.RemotePlayCursorID_t>, global::System.IComparable<global::Steamworks.RemotePlayCursorID_t>
	{
		public uint m_RemotePlayCursorID;

		public RemotePlayCursorID_t(uint value)
		{
			m_RemotePlayCursorID = value;
		}

		public override string ToString()
		{
			return m_RemotePlayCursorID.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.RemotePlayCursorID_t)
			{
				return this == (global::Steamworks.RemotePlayCursorID_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_RemotePlayCursorID.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.RemotePlayCursorID_t x, global::Steamworks.RemotePlayCursorID_t y)
		{
			return x.m_RemotePlayCursorID == y.m_RemotePlayCursorID;
		}

		public static bool operator !=(global::Steamworks.RemotePlayCursorID_t x, global::Steamworks.RemotePlayCursorID_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.RemotePlayCursorID_t(uint value)
		{
			return new global::Steamworks.RemotePlayCursorID_t(value);
		}

		public static explicit operator uint(global::Steamworks.RemotePlayCursorID_t that)
		{
			return that.m_RemotePlayCursorID;
		}

		public bool Equals(global::Steamworks.RemotePlayCursorID_t other)
		{
			return m_RemotePlayCursorID == other.m_RemotePlayCursorID;
		}

		public int CompareTo(global::Steamworks.RemotePlayCursorID_t other)
		{
			return m_RemotePlayCursorID.CompareTo(other.m_RemotePlayCursorID);
		}
	}
}
