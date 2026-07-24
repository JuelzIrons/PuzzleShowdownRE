namespace Unity.Networking.Transport
{
	internal struct PacketMetadata
	{
		public int DataLength;

		public int DataOffset;

		public int DataCapacity;

		public global::Unity.Networking.Transport.ConnectionId Connection;

		public override bool Equals(object obj)
		{
			return this == (global::Unity.Networking.Transport.PacketMetadata)obj;
		}

		public override int GetHashCode()
		{
			int num = 1;
			num = 31 * num + DataLength;
			num = 31 * num + DataOffset;
			num = 31 * num + DataCapacity;
			return 31 * num + Connection.GetHashCode();
		}

		public override string ToString()
		{
			return $"PacketMetadata(offset: {DataOffset}, length: {DataLength}, capacity: {DataCapacity})";
		}

		public static bool operator ==(global::Unity.Networking.Transport.PacketMetadata lhs, global::Unity.Networking.Transport.PacketMetadata rhs)
		{
			if (lhs.DataLength == rhs.DataLength && lhs.DataOffset == rhs.DataOffset && lhs.DataCapacity == rhs.DataCapacity)
			{
				return lhs.Connection == rhs.Connection;
			}
			return false;
		}

		public static bool operator !=(global::Unity.Networking.Transport.PacketMetadata lhs, global::Unity.Networking.Transport.PacketMetadata rhs)
		{
			if (lhs.DataLength == rhs.DataLength && lhs.DataOffset == rhs.DataOffset && lhs.DataCapacity == rhs.DataCapacity)
			{
				return lhs.Connection != rhs.Connection;
			}
			return true;
		}
	}
}
