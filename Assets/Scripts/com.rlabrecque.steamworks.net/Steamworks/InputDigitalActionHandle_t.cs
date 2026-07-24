namespace Steamworks
{
	[global::System.Serializable]
	public struct InputDigitalActionHandle_t : global::System.IEquatable<global::Steamworks.InputDigitalActionHandle_t>, global::System.IComparable<global::Steamworks.InputDigitalActionHandle_t>
	{
		public ulong m_InputDigitalActionHandle;

		public InputDigitalActionHandle_t(ulong value)
		{
			m_InputDigitalActionHandle = value;
		}

		public override string ToString()
		{
			return m_InputDigitalActionHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.InputDigitalActionHandle_t)
			{
				return this == (global::Steamworks.InputDigitalActionHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_InputDigitalActionHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.InputDigitalActionHandle_t x, global::Steamworks.InputDigitalActionHandle_t y)
		{
			return x.m_InputDigitalActionHandle == y.m_InputDigitalActionHandle;
		}

		public static bool operator !=(global::Steamworks.InputDigitalActionHandle_t x, global::Steamworks.InputDigitalActionHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.InputDigitalActionHandle_t(ulong value)
		{
			return new global::Steamworks.InputDigitalActionHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.InputDigitalActionHandle_t that)
		{
			return that.m_InputDigitalActionHandle;
		}

		public bool Equals(global::Steamworks.InputDigitalActionHandle_t other)
		{
			return m_InputDigitalActionHandle == other.m_InputDigitalActionHandle;
		}

		public int CompareTo(global::Steamworks.InputDigitalActionHandle_t other)
		{
			return m_InputDigitalActionHandle.CompareTo(other.m_InputDigitalActionHandle);
		}
	}
}
