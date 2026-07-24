namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::System.Serializable]
	internal struct NamedMessageEvent : global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent
	{
		public global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo Connection { get; }

		public global::Unity.Collections.FixedString64Bytes Name { get; }

		public long BytesCount { get; }

		public ulong TreeViewId
		{
			get
			{
				long num = Connection.GetHashCode();
				ulong num2 = (ulong)Name.GetHashCode();
				ulong num3 = (ulong)BytesCount.GetHashCode();
				return (ulong)(num + (long)num2) + num3;
			}
		}

		public NamedMessageEvent(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, string name, long bytesCount)
			: this(connection, global::Unity.Multiplayer.Tools.MetricTypes.StringConversionUtility.ConvertToFixedString(name), bytesCount)
		{
		}

		public NamedMessageEvent(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, global::Unity.Collections.FixedString64Bytes name, long bytesCount)
		{
			Connection = connection;
			Name = name;
			BytesCount = bytesCount;
		}
	}
}
