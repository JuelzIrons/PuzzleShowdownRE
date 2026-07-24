namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamNetworkingPOPID : global::System.IEquatable<global::Steamworks.SteamNetworkingPOPID>, global::System.IComparable<global::Steamworks.SteamNetworkingPOPID>
	{
		public uint m_SteamNetworkingPOPID;

		public SteamNetworkingPOPID(uint value)
		{
			m_SteamNetworkingPOPID = value;
		}

		public override string ToString()
		{
			return m_SteamNetworkingPOPID.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SteamNetworkingPOPID)
			{
				return this == (global::Steamworks.SteamNetworkingPOPID)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamNetworkingPOPID.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SteamNetworkingPOPID x, global::Steamworks.SteamNetworkingPOPID y)
		{
			return x.m_SteamNetworkingPOPID == y.m_SteamNetworkingPOPID;
		}

		public static bool operator !=(global::Steamworks.SteamNetworkingPOPID x, global::Steamworks.SteamNetworkingPOPID y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SteamNetworkingPOPID(uint value)
		{
			return new global::Steamworks.SteamNetworkingPOPID(value);
		}

		public static explicit operator uint(global::Steamworks.SteamNetworkingPOPID that)
		{
			return that.m_SteamNetworkingPOPID;
		}

		public bool Equals(global::Steamworks.SteamNetworkingPOPID other)
		{
			return m_SteamNetworkingPOPID == other.m_SteamNetworkingPOPID;
		}

		public int CompareTo(global::Steamworks.SteamNetworkingPOPID other)
		{
			return m_SteamNetworkingPOPID.CompareTo(other.m_SteamNetworkingPOPID);
		}
	}
}
