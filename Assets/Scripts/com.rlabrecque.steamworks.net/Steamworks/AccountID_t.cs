namespace Steamworks
{
	[global::System.Serializable]
	public struct AccountID_t : global::System.IEquatable<global::Steamworks.AccountID_t>, global::System.IComparable<global::Steamworks.AccountID_t>
	{
		public static readonly global::Steamworks.AccountID_t Invalid = new global::Steamworks.AccountID_t(0u);

		public uint m_AccountID;

		public AccountID_t(uint value)
		{
			m_AccountID = value;
		}

		public override string ToString()
		{
			return m_AccountID.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.AccountID_t)
			{
				return this == (global::Steamworks.AccountID_t)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_AccountID.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.AccountID_t x, global::Steamworks.AccountID_t y)
		{
			return x.m_AccountID == y.m_AccountID;
		}

		public static bool operator !=(global::Steamworks.AccountID_t x, global::Steamworks.AccountID_t y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.AccountID_t(uint value)
		{
			return new global::Steamworks.AccountID_t(value);
		}

		public static explicit operator uint(global::Steamworks.AccountID_t that)
		{
			return that.m_AccountID;
		}

		public bool Equals(global::Steamworks.AccountID_t other)
		{
			return m_AccountID == other.m_AccountID;
		}

		public int CompareTo(global::Steamworks.AccountID_t other)
		{
			return m_AccountID.CompareTo(other.m_AccountID);
		}
	}
}
