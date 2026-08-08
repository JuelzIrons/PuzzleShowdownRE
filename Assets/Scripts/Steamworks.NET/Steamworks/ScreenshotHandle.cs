namespace Steamworks
{
	[global::System.Serializable]
	public struct ScreenshotHandle : global::System.IEquatable<global::Steamworks.ScreenshotHandle>, global::System.IComparable<global::Steamworks.ScreenshotHandle>
	{
		public static readonly global::Steamworks.ScreenshotHandle Invalid = new global::Steamworks.ScreenshotHandle(0u);

		public uint m_ScreenshotHandle;

		public ScreenshotHandle(uint value)
		{
			m_ScreenshotHandle = value;
		}

		public override string ToString()
		{
			return m_ScreenshotHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.ScreenshotHandle)
			{
				return this == (global::Steamworks.ScreenshotHandle)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_ScreenshotHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.ScreenshotHandle x, global::Steamworks.ScreenshotHandle y)
		{
			return x.m_ScreenshotHandle == y.m_ScreenshotHandle;
		}

		public static bool operator !=(global::Steamworks.ScreenshotHandle x, global::Steamworks.ScreenshotHandle y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.ScreenshotHandle(uint value)
		{
			return new global::Steamworks.ScreenshotHandle(value);
		}

		public static explicit operator uint(global::Steamworks.ScreenshotHandle that)
		{
			return that.m_ScreenshotHandle;
		}

		public bool Equals(global::Steamworks.ScreenshotHandle other)
		{
			return m_ScreenshotHandle == other.m_ScreenshotHandle;
		}

		public int CompareTo(global::Steamworks.ScreenshotHandle other)
		{
			return m_ScreenshotHandle.CompareTo(other.m_ScreenshotHandle);
		}
	}
}
