namespace Steamworks
{
	[global::System.Serializable]
	public struct AppId_t : global::System.IEquatable<global::Steamworks.AppId_t>, global::System.IComparable<global::Steamworks.AppId_t>
	{
		public static readonly global::Steamworks.AppId_t Invalid = new global::Steamworks.AppId_t(0u);

		public uint m_AppId;

		public AppId_t(uint value)
		{
			m_AppId = value;
		}

		public override string ToString()
		{
			return m_AppId.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.AppId_t)
			{
				return this == (global::Steamworks.AppId_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_AppId.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.AppId_t x, global::Steamworks.AppId_t y)
		{
			return x.m_AppId == y.m_AppId;
		}

		public static bool operator !=(global::Steamworks.AppId_t x, global::Steamworks.AppId_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.AppId_t(uint value)
		{
			return new global::Steamworks.AppId_t(value);
		}

		public static explicit operator uint(global::Steamworks.AppId_t that)
		{
			return that.m_AppId;
		}

		public bool Equals(global::Steamworks.AppId_t other)
		{
			return m_AppId == other.m_AppId;
		}

		public int CompareTo(global::Steamworks.AppId_t other)
		{
			return m_AppId.CompareTo(other.m_AppId);
		}
	}
}
