namespace Steamworks
{
	[global::System.Serializable]
	public struct UGCFileWriteStreamHandle_t : global::System.IEquatable<global::Steamworks.UGCFileWriteStreamHandle_t>, global::System.IComparable<global::Steamworks.UGCFileWriteStreamHandle_t>
	{
		public static readonly global::Steamworks.UGCFileWriteStreamHandle_t Invalid = new global::Steamworks.UGCFileWriteStreamHandle_t(ulong.MaxValue);

		public ulong m_UGCFileWriteStreamHandle;

		public UGCFileWriteStreamHandle_t(ulong value)
		{
			m_UGCFileWriteStreamHandle = value;
		}

		public override string ToString()
		{
			return m_UGCFileWriteStreamHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.UGCFileWriteStreamHandle_t)
			{
				return this == (global::Steamworks.UGCFileWriteStreamHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_UGCFileWriteStreamHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.UGCFileWriteStreamHandle_t x, global::Steamworks.UGCFileWriteStreamHandle_t y)
		{
			return x.m_UGCFileWriteStreamHandle == y.m_UGCFileWriteStreamHandle;
		}

		public static bool operator !=(global::Steamworks.UGCFileWriteStreamHandle_t x, global::Steamworks.UGCFileWriteStreamHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.UGCFileWriteStreamHandle_t(ulong value)
		{
			return new global::Steamworks.UGCFileWriteStreamHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.UGCFileWriteStreamHandle_t that)
		{
			return that.m_UGCFileWriteStreamHandle;
		}

		public bool Equals(global::Steamworks.UGCFileWriteStreamHandle_t other)
		{
			return m_UGCFileWriteStreamHandle == other.m_UGCFileWriteStreamHandle;
		}

		public int CompareTo(global::Steamworks.UGCFileWriteStreamHandle_t other)
		{
			return m_UGCFileWriteStreamHandle.CompareTo(other.m_UGCFileWriteStreamHandle);
		}
	}
}
