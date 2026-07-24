namespace Steamworks
{
	[global::System.Serializable]
	public struct HServerListRequest : global::System.IEquatable<global::Steamworks.HServerListRequest>
	{
		public static readonly global::Steamworks.HServerListRequest Invalid = new global::Steamworks.HServerListRequest(global::System.IntPtr.Zero);

		public global::System.IntPtr m_HServerListRequest;

		public HServerListRequest(global::System.IntPtr value)
		{
			m_HServerListRequest = value;
		}

		public override string ToString()
		{
			return m_HServerListRequest.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.HServerListRequest)
			{
				return this == (global::Steamworks.HServerListRequest)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_HServerListRequest.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.HServerListRequest x, global::Steamworks.HServerListRequest y)
		{
			return x.m_HServerListRequest == y.m_HServerListRequest;
		}

		public static bool operator !=(global::Steamworks.HServerListRequest x, global::Steamworks.HServerListRequest y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.HServerListRequest(global::System.IntPtr value)
		{
			return new global::Steamworks.HServerListRequest(value);
		}

		public static explicit operator global::System.IntPtr(global::Steamworks.HServerListRequest that)
		{
			return that.m_HServerListRequest;
		}

		public bool Equals(global::Steamworks.HServerListRequest other)
		{
			return m_HServerListRequest == other.m_HServerListRequest;
		}
	}
}
