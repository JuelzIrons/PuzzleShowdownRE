namespace Steamworks
{
	[global::System.Serializable]
	public struct TimelineEventHandle_t : global::System.IEquatable<global::Steamworks.TimelineEventHandle_t>, global::System.IComparable<global::Steamworks.TimelineEventHandle_t>
	{
		public ulong m_TimelineEventHandle;

		public TimelineEventHandle_t(ulong value)
		{
			m_TimelineEventHandle = value;
		}

		public override string ToString()
		{
			return m_TimelineEventHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.TimelineEventHandle_t)
			{
				return this == (global::Steamworks.TimelineEventHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_TimelineEventHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.TimelineEventHandle_t x, global::Steamworks.TimelineEventHandle_t y)
		{
			return x.m_TimelineEventHandle == y.m_TimelineEventHandle;
		}

		public static bool operator !=(global::Steamworks.TimelineEventHandle_t x, global::Steamworks.TimelineEventHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.TimelineEventHandle_t(ulong value)
		{
			return new global::Steamworks.TimelineEventHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.TimelineEventHandle_t that)
		{
			return that.m_TimelineEventHandle;
		}

		public bool Equals(global::Steamworks.TimelineEventHandle_t other)
		{
			return m_TimelineEventHandle == other.m_TimelineEventHandle;
		}

		public int CompareTo(global::Steamworks.TimelineEventHandle_t other)
		{
			return m_TimelineEventHandle.CompareTo(other.m_TimelineEventHandle);
		}
	}
}
