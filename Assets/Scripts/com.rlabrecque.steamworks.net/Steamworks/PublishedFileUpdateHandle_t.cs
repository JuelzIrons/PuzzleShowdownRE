namespace Steamworks
{
	[global::System.Serializable]
	public struct PublishedFileUpdateHandle_t : global::System.IEquatable<global::Steamworks.PublishedFileUpdateHandle_t>, global::System.IComparable<global::Steamworks.PublishedFileUpdateHandle_t>
	{
		public static readonly global::Steamworks.PublishedFileUpdateHandle_t Invalid = new global::Steamworks.PublishedFileUpdateHandle_t(ulong.MaxValue);

		public ulong m_PublishedFileUpdateHandle;

		public PublishedFileUpdateHandle_t(ulong value)
		{
			m_PublishedFileUpdateHandle = value;
		}

		public override string ToString()
		{
			return m_PublishedFileUpdateHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.PublishedFileUpdateHandle_t)
			{
				return this == (global::Steamworks.PublishedFileUpdateHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_PublishedFileUpdateHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.PublishedFileUpdateHandle_t x, global::Steamworks.PublishedFileUpdateHandle_t y)
		{
			return x.m_PublishedFileUpdateHandle == y.m_PublishedFileUpdateHandle;
		}

		public static bool operator !=(global::Steamworks.PublishedFileUpdateHandle_t x, global::Steamworks.PublishedFileUpdateHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.PublishedFileUpdateHandle_t(ulong value)
		{
			return new global::Steamworks.PublishedFileUpdateHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.PublishedFileUpdateHandle_t that)
		{
			return that.m_PublishedFileUpdateHandle;
		}

		public bool Equals(global::Steamworks.PublishedFileUpdateHandle_t other)
		{
			return m_PublishedFileUpdateHandle == other.m_PublishedFileUpdateHandle;
		}

		public int CompareTo(global::Steamworks.PublishedFileUpdateHandle_t other)
		{
			return m_PublishedFileUpdateHandle.CompareTo(other.m_PublishedFileUpdateHandle);
		}
	}
}
