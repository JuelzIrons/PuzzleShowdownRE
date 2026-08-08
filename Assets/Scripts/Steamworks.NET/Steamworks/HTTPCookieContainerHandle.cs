namespace Steamworks
{
	[global::System.Serializable]
	public struct HTTPCookieContainerHandle : global::System.IEquatable<global::Steamworks.HTTPCookieContainerHandle>, global::System.IComparable<global::Steamworks.HTTPCookieContainerHandle>
	{
		public static readonly global::Steamworks.HTTPCookieContainerHandle Invalid = new global::Steamworks.HTTPCookieContainerHandle(0u);

		public uint m_HTTPCookieContainerHandle;

		public HTTPCookieContainerHandle(uint value)
		{
			m_HTTPCookieContainerHandle = value;
		}

		public override string ToString()
		{
			return m_HTTPCookieContainerHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HTTPCookieContainerHandle)
			{
				return this == (global::Steamworks.HTTPCookieContainerHandle)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HTTPCookieContainerHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HTTPCookieContainerHandle x, global::Steamworks.HTTPCookieContainerHandle y)
		{
			return x.m_HTTPCookieContainerHandle == y.m_HTTPCookieContainerHandle;
		}

		public static bool operator !=(global::Steamworks.HTTPCookieContainerHandle x, global::Steamworks.HTTPCookieContainerHandle y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HTTPCookieContainerHandle(uint value)
		{
			return new global::Steamworks.HTTPCookieContainerHandle(value);
		}

		public static explicit operator uint(global::Steamworks.HTTPCookieContainerHandle that)
		{
			return that.m_HTTPCookieContainerHandle;
		}

		public bool Equals(global::Steamworks.HTTPCookieContainerHandle other)
		{
			return m_HTTPCookieContainerHandle == other.m_HTTPCookieContainerHandle;
		}

		public int CompareTo(global::Steamworks.HTTPCookieContainerHandle other)
		{
			return m_HTTPCookieContainerHandle.CompareTo(other.m_HTTPCookieContainerHandle);
		}
	}
}
