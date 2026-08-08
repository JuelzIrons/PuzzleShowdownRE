namespace Steamworks
{
	[global::System.Serializable]
	public struct HSteamNetPollGroup : global::System.IEquatable<global::Steamworks.HSteamNetPollGroup>, global::System.IComparable<global::Steamworks.HSteamNetPollGroup>
	{
		public static readonly global::Steamworks.HSteamNetPollGroup Invalid = new global::Steamworks.HSteamNetPollGroup(0u);

		public uint m_HSteamNetPollGroup;

		public HSteamNetPollGroup(uint value)
		{
			m_HSteamNetPollGroup = value;
		}

		public override string ToString()
		{
			return m_HSteamNetPollGroup.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HSteamNetPollGroup)
			{
				return this == (global::Steamworks.HSteamNetPollGroup)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HSteamNetPollGroup.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HSteamNetPollGroup x, global::Steamworks.HSteamNetPollGroup y)
		{
			return x.m_HSteamNetPollGroup == y.m_HSteamNetPollGroup;
		}

		public static bool operator !=(global::Steamworks.HSteamNetPollGroup x, global::Steamworks.HSteamNetPollGroup y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HSteamNetPollGroup(uint value)
		{
			return new global::Steamworks.HSteamNetPollGroup(value);
		}

		public static explicit operator uint(global::Steamworks.HSteamNetPollGroup that)
		{
			return that.m_HSteamNetPollGroup;
		}

		public bool Equals(global::Steamworks.HSteamNetPollGroup other)
		{
			return m_HSteamNetPollGroup == other.m_HSteamNetPollGroup;
		}

		public int CompareTo(global::Steamworks.HSteamNetPollGroup other)
		{
			return m_HSteamNetPollGroup.CompareTo(other.m_HSteamNetPollGroup);
		}
	}
}
