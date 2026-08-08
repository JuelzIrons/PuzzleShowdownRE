namespace Steamworks
{
	[global::System.Serializable]
	public struct HTTPRequestHandle : global::System.IEquatable<global::Steamworks.HTTPRequestHandle>, global::System.IComparable<global::Steamworks.HTTPRequestHandle>
	{
		public static readonly global::Steamworks.HTTPRequestHandle Invalid = new global::Steamworks.HTTPRequestHandle(0u);

		public uint m_HTTPRequestHandle;

		public HTTPRequestHandle(uint value)
		{
			m_HTTPRequestHandle = value;
		}

		public override string ToString()
		{
			return m_HTTPRequestHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HTTPRequestHandle)
			{
				return this == (global::Steamworks.HTTPRequestHandle)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HTTPRequestHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HTTPRequestHandle x, global::Steamworks.HTTPRequestHandle y)
		{
			return x.m_HTTPRequestHandle == y.m_HTTPRequestHandle;
		}

		public static bool operator !=(global::Steamworks.HTTPRequestHandle x, global::Steamworks.HTTPRequestHandle y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HTTPRequestHandle(uint value)
		{
			return new global::Steamworks.HTTPRequestHandle(value);
		}

		public static explicit operator uint(global::Steamworks.HTTPRequestHandle that)
		{
			return that.m_HTTPRequestHandle;
		}

		public bool Equals(global::Steamworks.HTTPRequestHandle other)
		{
			return m_HTTPRequestHandle == other.m_HTTPRequestHandle;
		}

		public int CompareTo(global::Steamworks.HTTPRequestHandle other)
		{
			return m_HTTPRequestHandle.CompareTo(other.m_HTTPRequestHandle);
		}
	}
}
