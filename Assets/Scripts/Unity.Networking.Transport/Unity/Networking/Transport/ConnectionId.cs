namespace Unity.Networking.Transport
{
	internal struct ConnectionId : global::System.IEquatable<global::Unity.Networking.Transport.ConnectionId>
	{
		public int Id;

		public int Version;

		public bool IsCreated => Version > 0;

		internal ConnectionId(int id, int version)
		{
			Id = id;
			Version = version;
		}

		public static bool operator ==(global::Unity.Networking.Transport.ConnectionId lhs, global::Unity.Networking.Transport.ConnectionId rhs)
		{
			if (lhs.Id == rhs.Id)
			{
				return lhs.Version == rhs.Version;
			}
			return false;
		}

		public static bool operator !=(global::Unity.Networking.Transport.ConnectionId lhs, global::Unity.Networking.Transport.ConnectionId rhs)
		{
			if (lhs.Id == rhs.Id)
			{
				return lhs.Version != rhs.Version;
			}
			return true;
		}

		public override bool Equals(object o)
		{
			return this == (global::Unity.Networking.Transport.ConnectionId)o;
		}

		public bool Equals(global::Unity.Networking.Transport.ConnectionId o)
		{
			return this == o;
		}

		public override int GetHashCode()
		{
			return (Id << 8) ^ Version;
		}

		public override string ToString()
		{
			return $"ConnectionId[id{Id},v{Version}]";
		}
	}
}
