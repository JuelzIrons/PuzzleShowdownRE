namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamInventoryResult_t : global::System.IEquatable<global::Steamworks.SteamInventoryResult_t>, global::System.IComparable<global::Steamworks.SteamInventoryResult_t>
	{
		public static readonly global::Steamworks.SteamInventoryResult_t Invalid = new global::Steamworks.SteamInventoryResult_t(-1);

		public int m_SteamInventoryResult;

		public SteamInventoryResult_t(int value)
		{
			m_SteamInventoryResult = value;
		}

		public override string ToString()
		{
			return m_SteamInventoryResult.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SteamInventoryResult_t)
			{
				return this == (global::Steamworks.SteamInventoryResult_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamInventoryResult.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SteamInventoryResult_t x, global::Steamworks.SteamInventoryResult_t y)
		{
			return x.m_SteamInventoryResult == y.m_SteamInventoryResult;
		}

		public static bool operator !=(global::Steamworks.SteamInventoryResult_t x, global::Steamworks.SteamInventoryResult_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SteamInventoryResult_t(int value)
		{
			return new global::Steamworks.SteamInventoryResult_t(value);
		}

		public static explicit operator int(global::Steamworks.SteamInventoryResult_t that)
		{
			return that.m_SteamInventoryResult;
		}

		public bool Equals(global::Steamworks.SteamInventoryResult_t other)
		{
			return m_SteamInventoryResult == other.m_SteamInventoryResult;
		}

		public int CompareTo(global::Steamworks.SteamInventoryResult_t other)
		{
			return m_SteamInventoryResult.CompareTo(other.m_SteamInventoryResult);
		}
	}
}
