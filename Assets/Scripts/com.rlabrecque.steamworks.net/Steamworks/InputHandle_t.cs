namespace Steamworks
{
	[global::System.Serializable]
	public struct InputHandle_t : global::System.IEquatable<global::Steamworks.InputHandle_t>, global::System.IComparable<global::Steamworks.InputHandle_t>
	{
		public ulong m_InputHandle;

		public InputHandle_t(ulong value)
		{
			m_InputHandle = value;
		}

		public override string ToString()
		{
			return m_InputHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.InputHandle_t)
			{
				return this == (global::Steamworks.InputHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_InputHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.InputHandle_t x, global::Steamworks.InputHandle_t y)
		{
			return x.m_InputHandle == y.m_InputHandle;
		}

		public static bool operator !=(global::Steamworks.InputHandle_t x, global::Steamworks.InputHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.InputHandle_t(ulong value)
		{
			return new global::Steamworks.InputHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.InputHandle_t that)
		{
			return that.m_InputHandle;
		}

		public bool Equals(global::Steamworks.InputHandle_t other)
		{
			return m_InputHandle == other.m_InputHandle;
		}

		public int CompareTo(global::Steamworks.InputHandle_t other)
		{
			return m_InputHandle.CompareTo(other.m_InputHandle);
		}
	}
}
