namespace Steamworks
{
	[global::System.Serializable]
	public struct InputAnalogActionHandle_t : global::System.IEquatable<global::Steamworks.InputAnalogActionHandle_t>, global::System.IComparable<global::Steamworks.InputAnalogActionHandle_t>
	{
		public ulong m_InputAnalogActionHandle;

		public InputAnalogActionHandle_t(ulong value)
		{
			m_InputAnalogActionHandle = value;
		}

		public override string ToString()
		{
			return m_InputAnalogActionHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.InputAnalogActionHandle_t)
			{
				return this == (global::Steamworks.InputAnalogActionHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_InputAnalogActionHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.InputAnalogActionHandle_t x, global::Steamworks.InputAnalogActionHandle_t y)
		{
			return x.m_InputAnalogActionHandle == y.m_InputAnalogActionHandle;
		}

		public static bool operator !=(global::Steamworks.InputAnalogActionHandle_t x, global::Steamworks.InputAnalogActionHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.InputAnalogActionHandle_t(ulong value)
		{
			return new global::Steamworks.InputAnalogActionHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.InputAnalogActionHandle_t that)
		{
			return that.m_InputAnalogActionHandle;
		}

		public bool Equals(global::Steamworks.InputAnalogActionHandle_t other)
		{
			return m_InputAnalogActionHandle == other.m_InputAnalogActionHandle;
		}

		public int CompareTo(global::Steamworks.InputAnalogActionHandle_t other)
		{
			return m_InputAnalogActionHandle.CompareTo(other.m_InputAnalogActionHandle);
		}
	}
}
