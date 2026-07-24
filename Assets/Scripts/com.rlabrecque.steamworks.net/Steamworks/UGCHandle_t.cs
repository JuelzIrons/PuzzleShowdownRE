namespace Steamworks
{
	[global::System.Serializable]
	public struct UGCHandle_t : global::System.IEquatable<global::Steamworks.UGCHandle_t>, global::System.IComparable<global::Steamworks.UGCHandle_t>
	{
		public static readonly global::Steamworks.UGCHandle_t Invalid = new global::Steamworks.UGCHandle_t(ulong.MaxValue);

		public ulong m_UGCHandle;

		public UGCHandle_t(ulong value)
		{
			m_UGCHandle = value;
		}

		public override string ToString()
		{
			return m_UGCHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.UGCHandle_t)
			{
				return this == (global::Steamworks.UGCHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_UGCHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.UGCHandle_t x, global::Steamworks.UGCHandle_t y)
		{
			return x.m_UGCHandle == y.m_UGCHandle;
		}

		public static bool operator !=(global::Steamworks.UGCHandle_t x, global::Steamworks.UGCHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.UGCHandle_t(ulong value)
		{
			return new global::Steamworks.UGCHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.UGCHandle_t that)
		{
			return that.m_UGCHandle;
		}

		public bool Equals(global::Steamworks.UGCHandle_t other)
		{
			return m_UGCHandle == other.m_UGCHandle;
		}

		public int CompareTo(global::Steamworks.UGCHandle_t other)
		{
			return m_UGCHandle.CompareTo(other.m_UGCHandle);
		}
	}
}
