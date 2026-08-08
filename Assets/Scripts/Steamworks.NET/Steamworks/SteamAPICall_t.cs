namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamAPICall_t : global::System.IEquatable<global::Steamworks.SteamAPICall_t>, global::System.IComparable<global::Steamworks.SteamAPICall_t>
	{
		public static readonly global::Steamworks.SteamAPICall_t Invalid = new global::Steamworks.SteamAPICall_t(0uL);

		public ulong m_SteamAPICall;

		public SteamAPICall_t(ulong value)
		{
			m_SteamAPICall = value;
		}

		public override string ToString()
		{
			return m_SteamAPICall.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SteamAPICall_t)
			{
				return this == (global::Steamworks.SteamAPICall_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamAPICall.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SteamAPICall_t x, global::Steamworks.SteamAPICall_t y)
		{
			return x.m_SteamAPICall == y.m_SteamAPICall;
		}

		public static bool operator !=(global::Steamworks.SteamAPICall_t x, global::Steamworks.SteamAPICall_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SteamAPICall_t(ulong value)
		{
			return new global::Steamworks.SteamAPICall_t(value);
		}

		public static explicit operator ulong(global::Steamworks.SteamAPICall_t that)
		{
			return that.m_SteamAPICall;
		}

		public bool Equals(global::Steamworks.SteamAPICall_t other)
		{
			return m_SteamAPICall == other.m_SteamAPICall;
		}

		public int CompareTo(global::Steamworks.SteamAPICall_t other)
		{
			return m_SteamAPICall.CompareTo(other.m_SteamAPICall);
		}
	}
}
