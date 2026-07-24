namespace Steamworks
{
	[global::System.Serializable]
	public struct PartyBeaconID_t : global::System.IEquatable<global::Steamworks.PartyBeaconID_t>, global::System.IComparable<global::Steamworks.PartyBeaconID_t>
	{
		public static readonly global::Steamworks.PartyBeaconID_t Invalid = new global::Steamworks.PartyBeaconID_t(0uL);

		public ulong m_PartyBeaconID;

		public PartyBeaconID_t(ulong value)
		{
			m_PartyBeaconID = value;
		}

		public override string ToString()
		{
			return m_PartyBeaconID.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.PartyBeaconID_t)
			{
				return this == (global::Steamworks.PartyBeaconID_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_PartyBeaconID.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.PartyBeaconID_t x, global::Steamworks.PartyBeaconID_t y)
		{
			return x.m_PartyBeaconID == y.m_PartyBeaconID;
		}

		public static bool operator !=(global::Steamworks.PartyBeaconID_t x, global::Steamworks.PartyBeaconID_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.PartyBeaconID_t(ulong value)
		{
			return new global::Steamworks.PartyBeaconID_t(value);
		}

		public static explicit operator ulong(global::Steamworks.PartyBeaconID_t that)
		{
			return that.m_PartyBeaconID;
		}

		public bool Equals(global::Steamworks.PartyBeaconID_t other)
		{
			return m_PartyBeaconID == other.m_PartyBeaconID;
		}

		public int CompareTo(global::Steamworks.PartyBeaconID_t other)
		{
			return m_PartyBeaconID.CompareTo(other.m_PartyBeaconID);
		}
	}
}
