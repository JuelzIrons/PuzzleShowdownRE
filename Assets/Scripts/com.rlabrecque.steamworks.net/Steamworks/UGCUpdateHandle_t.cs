namespace Steamworks
{
	[global::System.Serializable]
	public struct UGCUpdateHandle_t : global::System.IEquatable<global::Steamworks.UGCUpdateHandle_t>, global::System.IComparable<global::Steamworks.UGCUpdateHandle_t>
	{
		public static readonly global::Steamworks.UGCUpdateHandle_t Invalid = new global::Steamworks.UGCUpdateHandle_t(ulong.MaxValue);

		public ulong m_UGCUpdateHandle;

		public UGCUpdateHandle_t(ulong value)
		{
			m_UGCUpdateHandle = value;
		}

		public override string ToString()
		{
			return m_UGCUpdateHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.UGCUpdateHandle_t)
			{
				return this == (global::Steamworks.UGCUpdateHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_UGCUpdateHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.UGCUpdateHandle_t x, global::Steamworks.UGCUpdateHandle_t y)
		{
			return x.m_UGCUpdateHandle == y.m_UGCUpdateHandle;
		}

		public static bool operator !=(global::Steamworks.UGCUpdateHandle_t x, global::Steamworks.UGCUpdateHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.UGCUpdateHandle_t(ulong value)
		{
			return new global::Steamworks.UGCUpdateHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.UGCUpdateHandle_t that)
		{
			return that.m_UGCUpdateHandle;
		}

		public bool Equals(global::Steamworks.UGCUpdateHandle_t other)
		{
			return m_UGCUpdateHandle == other.m_UGCUpdateHandle;
		}

		public int CompareTo(global::Steamworks.UGCUpdateHandle_t other)
		{
			return m_UGCUpdateHandle.CompareTo(other.m_UGCUpdateHandle);
		}
	}
}
