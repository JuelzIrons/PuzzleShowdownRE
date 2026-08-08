namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamItemInstanceID_t : global::System.IEquatable<global::Steamworks.SteamItemInstanceID_t>, global::System.IComparable<global::Steamworks.SteamItemInstanceID_t>
	{
		public static readonly global::Steamworks.SteamItemInstanceID_t Invalid = new global::Steamworks.SteamItemInstanceID_t(ulong.MaxValue);

		public ulong m_SteamItemInstanceID;

		public SteamItemInstanceID_t(ulong value)
		{
			m_SteamItemInstanceID = value;
		}

		public override string ToString()
		{
			return m_SteamItemInstanceID.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SteamItemInstanceID_t)
			{
				return this == (global::Steamworks.SteamItemInstanceID_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamItemInstanceID.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SteamItemInstanceID_t x, global::Steamworks.SteamItemInstanceID_t y)
		{
			return x.m_SteamItemInstanceID == y.m_SteamItemInstanceID;
		}

		public static bool operator !=(global::Steamworks.SteamItemInstanceID_t x, global::Steamworks.SteamItemInstanceID_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SteamItemInstanceID_t(ulong value)
		{
			return new global::Steamworks.SteamItemInstanceID_t(value);
		}

		public static explicit operator ulong(global::Steamworks.SteamItemInstanceID_t that)
		{
			return that.m_SteamItemInstanceID;
		}

		public bool Equals(global::Steamworks.SteamItemInstanceID_t other)
		{
			return m_SteamItemInstanceID == other.m_SteamItemInstanceID;
		}

		public int CompareTo(global::Steamworks.SteamItemInstanceID_t other)
		{
			return m_SteamItemInstanceID.CompareTo(other.m_SteamItemInstanceID);
		}
	}
}
