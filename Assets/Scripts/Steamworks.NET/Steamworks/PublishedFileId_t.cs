namespace Steamworks
{
	[global::System.Serializable]
	public struct PublishedFileId_t : global::System.IEquatable<global::Steamworks.PublishedFileId_t>, global::System.IComparable<global::Steamworks.PublishedFileId_t>
	{
		public static readonly global::Steamworks.PublishedFileId_t Invalid = new global::Steamworks.PublishedFileId_t(0uL);

		public ulong m_PublishedFileId;

		public PublishedFileId_t(ulong value)
		{
			m_PublishedFileId = value;
		}

		public override string ToString()
		{
			return m_PublishedFileId.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.PublishedFileId_t)
			{
				return this == (global::Steamworks.PublishedFileId_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_PublishedFileId.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.PublishedFileId_t x, global::Steamworks.PublishedFileId_t y)
		{
			return x.m_PublishedFileId == y.m_PublishedFileId;
		}

		public static bool operator !=(global::Steamworks.PublishedFileId_t x, global::Steamworks.PublishedFileId_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.PublishedFileId_t(ulong value)
		{
			return new global::Steamworks.PublishedFileId_t(value);
		}

		public static explicit operator ulong(global::Steamworks.PublishedFileId_t that)
		{
			return that.m_PublishedFileId;
		}

		public bool Equals(global::Steamworks.PublishedFileId_t other)
		{
			return m_PublishedFileId == other.m_PublishedFileId;
		}

		public int CompareTo(global::Steamworks.PublishedFileId_t other)
		{
			return m_PublishedFileId.CompareTo(other.m_PublishedFileId);
		}
	}
}
