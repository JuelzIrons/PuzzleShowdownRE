namespace Steamworks
{
	[global::System.Serializable]
	public struct InputActionSetHandle_t : global::System.IEquatable<global::Steamworks.InputActionSetHandle_t>, global::System.IComparable<global::Steamworks.InputActionSetHandle_t>
	{
		public ulong m_InputActionSetHandle;

		public InputActionSetHandle_t(ulong value)
		{
			m_InputActionSetHandle = value;
		}

		public override string ToString()
		{
			return m_InputActionSetHandle.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.InputActionSetHandle_t)
			{
				return this == (global::Steamworks.InputActionSetHandle_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_InputActionSetHandle.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.InputActionSetHandle_t x, global::Steamworks.InputActionSetHandle_t y)
		{
			return x.m_InputActionSetHandle == y.m_InputActionSetHandle;
		}

		public static bool operator !=(global::Steamworks.InputActionSetHandle_t x, global::Steamworks.InputActionSetHandle_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.InputActionSetHandle_t(ulong value)
		{
			return new global::Steamworks.InputActionSetHandle_t(value);
		}

		public static explicit operator ulong(global::Steamworks.InputActionSetHandle_t that)
		{
			return that.m_InputActionSetHandle;
		}

		public bool Equals(global::Steamworks.InputActionSetHandle_t other)
		{
			return m_InputActionSetHandle == other.m_InputActionSetHandle;
		}

		public int CompareTo(global::Steamworks.InputActionSetHandle_t other)
		{
			return m_InputActionSetHandle.CompareTo(other.m_InputActionSetHandle);
		}
	}
}
