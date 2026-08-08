namespace Steamworks
{
	[global::System.Serializable]
	public struct HHTMLBrowser : global::System.IEquatable<global::Steamworks.HHTMLBrowser>, global::System.IComparable<global::Steamworks.HHTMLBrowser>
	{
		public static readonly global::Steamworks.HHTMLBrowser Invalid = new global::Steamworks.HHTMLBrowser(0u);

		public uint m_HHTMLBrowser;

		public HHTMLBrowser(uint value)
		{
			m_HHTMLBrowser = value;
		}

		public override string ToString()
		{
			return m_HHTMLBrowser.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HHTMLBrowser)
			{
				return this == (global::Steamworks.HHTMLBrowser)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HHTMLBrowser.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HHTMLBrowser x, global::Steamworks.HHTMLBrowser y)
		{
			return x.m_HHTMLBrowser == y.m_HHTMLBrowser;
		}

		public static bool operator !=(global::Steamworks.HHTMLBrowser x, global::Steamworks.HHTMLBrowser y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HHTMLBrowser(uint value)
		{
			return new global::Steamworks.HHTMLBrowser(value);
		}

		public static explicit operator uint(global::Steamworks.HHTMLBrowser that)
		{
			return that.m_HHTMLBrowser;
		}

		public bool Equals(global::Steamworks.HHTMLBrowser other)
		{
			return m_HHTMLBrowser == other.m_HHTMLBrowser;
		}

		public int CompareTo(global::Steamworks.HHTMLBrowser other)
		{
			return m_HHTMLBrowser.CompareTo(other.m_HHTMLBrowser);
		}
	}
}
