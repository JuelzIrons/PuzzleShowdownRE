namespace Steamworks
{
	[global::System.Serializable]
	public struct HSteamPipe : global::System.IEquatable<global::Steamworks.HSteamPipe>, global::System.IComparable<global::Steamworks.HSteamPipe>
	{
		public int m_HSteamPipe;

		public HSteamPipe(int value)
		{
			m_HSteamPipe = value;
		}

		public override string ToString()
		{
			return m_HSteamPipe.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HSteamPipe)
			{
				return this == (global::Steamworks.HSteamPipe)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HSteamPipe.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HSteamPipe x, global::Steamworks.HSteamPipe y)
		{
			return x.m_HSteamPipe == y.m_HSteamPipe;
		}

		public static bool operator !=(global::Steamworks.HSteamPipe x, global::Steamworks.HSteamPipe y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HSteamPipe(int value)
		{
			return new global::Steamworks.HSteamPipe(value);
		}

		public static explicit operator int(global::Steamworks.HSteamPipe that)
		{
			return that.m_HSteamPipe;
		}

		public bool Equals(global::Steamworks.HSteamPipe other)
		{
			return m_HSteamPipe == other.m_HSteamPipe;
		}

		public int CompareTo(global::Steamworks.HSteamPipe other)
		{
			return m_HSteamPipe.CompareTo(other.m_HSteamPipe);
		}
	}
}
