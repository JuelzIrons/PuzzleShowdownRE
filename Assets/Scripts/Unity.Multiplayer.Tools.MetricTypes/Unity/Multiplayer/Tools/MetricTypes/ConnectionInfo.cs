namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::System.Serializable]
	internal struct ConnectionInfo
	{
		public ulong Id { get; }

		public ConnectionInfo(ulong id)
		{
			Id = id;
		}

		public static bool operator ==(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo a, global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo a, global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo b)
		{
			return !(a == b);
		}

		public bool Equals(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo other)
		{
			return Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
