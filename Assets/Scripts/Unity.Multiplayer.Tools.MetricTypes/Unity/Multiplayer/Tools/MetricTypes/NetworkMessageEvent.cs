namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::System.Serializable]
	internal struct NetworkMessageEvent : global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent
	{
		public global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo Connection { get; }

		public global::Unity.Collections.FixedString64Bytes Name { get; }

		public long BytesCount { get; }

		public ulong TreeViewId
		{
			get
			{
				long num = Name.GetHashCode();
				ulong num2 = (ulong)BytesCount.GetHashCode();
				ulong num3 = (ulong)Connection.GetHashCode();
				return (ulong)(num + (long)num2) + num3;
			}
		}

		public NetworkMessageEvent(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, string name, long bytesCount)
			: this(connection, global::Unity.Multiplayer.Tools.MetricTypes.StringConversionUtility.ConvertToFixedString(name), bytesCount)
		{
		}

		public NetworkMessageEvent(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, global::Unity.Collections.FixedString64Bytes name, long bytesCount)
		{
			this = default(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent);
			Connection = connection;
			Name = name;
			BytesCount = bytesCount;
		}
	}
}
