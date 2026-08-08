namespace Steamworks
{
	[global::System.Serializable]
	public struct UGCQueryHandle_t : global::System.IEquatable<global::Steamworks.UGCQueryHandle_t>, global::System.IComparable<global::Steamworks.UGCQueryHandle_t>
	{
		public static readonly global::Steamworks.UGCQueryHandle_t Invalid = new global::Steamworks.UGCQueryHandle_t(ulong.MaxValue);

		public ulong m_UGCQueryHandle;

		public UGCQueryHandle_t(ulong value)
		{
			m_UGCQueryHandle = value;
		}

		public override string ToString()
		{
			return m_UGCQueryHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.UGCQueryHandle_t)
			{
				return this == (global::Steamworks.UGCQueryHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_UGCQueryHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.UGCQueryHandle_t x, global::Steamworks.UGCQueryHandle_t y)
		{
			return x.m_UGCQueryHandle == y.m_UGCQueryHandle;
		}

		public static bool operator !=(global::Steamworks.UGCQueryHandle_t x, global::Steamworks.UGCQueryHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.UGCQueryHandle_t(ulong value)
		{
			return new global::Steamworks.UGCQueryHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.UGCQueryHandle_t that)
		{
			return that.m_UGCQueryHandle;
		}

		public bool Equals(global::Steamworks.UGCQueryHandle_t other)
		{
			return m_UGCQueryHandle == other.m_UGCQueryHandle;
		}

		public int CompareTo(global::Steamworks.UGCQueryHandle_t other)
		{
			return m_UGCQueryHandle.CompareTo(other.m_UGCQueryHandle);
		}
	}
}
