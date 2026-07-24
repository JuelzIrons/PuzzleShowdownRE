namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamInventoryUpdateHandle_t : global::System.IEquatable<global::Steamworks.SteamInventoryUpdateHandle_t>, global::System.IComparable<global::Steamworks.SteamInventoryUpdateHandle_t>
	{
		public static readonly global::Steamworks.SteamInventoryUpdateHandle_t Invalid = new global::Steamworks.SteamInventoryUpdateHandle_t(ulong.MaxValue);

		public ulong m_SteamInventoryUpdateHandle;

		public SteamInventoryUpdateHandle_t(ulong value)
		{
			m_SteamInventoryUpdateHandle = value;
		}

		public override string ToString()
		{
			return m_SteamInventoryUpdateHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.SteamInventoryUpdateHandle_t)
			{
				return this == (global::Steamworks.SteamInventoryUpdateHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamInventoryUpdateHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.SteamInventoryUpdateHandle_t x, global::Steamworks.SteamInventoryUpdateHandle_t y)
		{
			return x.m_SteamInventoryUpdateHandle == y.m_SteamInventoryUpdateHandle;
		}

		public static bool operator !=(global::Steamworks.SteamInventoryUpdateHandle_t x, global::Steamworks.SteamInventoryUpdateHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.SteamInventoryUpdateHandle_t(ulong value)
		{
			return new global::Steamworks.SteamInventoryUpdateHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.SteamInventoryUpdateHandle_t that)
		{
			return that.m_SteamInventoryUpdateHandle;
		}

		public bool Equals(global::Steamworks.SteamInventoryUpdateHandle_t other)
		{
			return m_SteamInventoryUpdateHandle == other.m_SteamInventoryUpdateHandle;
		}

		public int CompareTo(global::Steamworks.SteamInventoryUpdateHandle_t other)
		{
			return m_SteamInventoryUpdateHandle.CompareTo(other.m_SteamInventoryUpdateHandle);
		}
	}
}
