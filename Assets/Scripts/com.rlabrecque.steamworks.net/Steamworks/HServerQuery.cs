namespace Steamworks
{
	[global::System.Serializable]
	public struct HServerQuery : global::System.IEquatable<global::Steamworks.HServerQuery>, global::System.IComparable<global::Steamworks.HServerQuery>
	{
		public static readonly global::Steamworks.HServerQuery Invalid = new global::Steamworks.HServerQuery(-1);

		public int m_HServerQuery;

		public HServerQuery(int value)
		{
			m_HServerQuery = value;
		}

		public override string ToString()
		{
			return m_HServerQuery.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HServerQuery)
			{
				return this == (global::Steamworks.HServerQuery)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HServerQuery.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HServerQuery x, global::Steamworks.HServerQuery y)
		{
			return x.m_HServerQuery == y.m_HServerQuery;
		}

		public static bool operator !=(global::Steamworks.HServerQuery x, global::Steamworks.HServerQuery y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HServerQuery(int value)
		{
			return new global::Steamworks.HServerQuery(value);
		}

		public static explicit operator int(global::Steamworks.HServerQuery that)
		{
			return that.m_HServerQuery;
		}

		public bool Equals(global::Steamworks.HServerQuery other)
		{
			return m_HServerQuery == other.m_HServerQuery;
		}

		public int CompareTo(global::Steamworks.HServerQuery other)
		{
			return m_HServerQuery.CompareTo(other.m_HServerQuery);
		}
	}
}
