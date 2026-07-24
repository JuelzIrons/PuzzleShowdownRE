namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::System.Serializable]
	internal struct ServerLogEvent : global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent
	{
		public global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo Connection { get; }

		public global::Unity.Multiplayer.Tools.MetricTypes.LogLevel LogLevel { get; }

		public long BytesCount { get; }

		public ulong TreeViewId
		{
			get
			{
				long num = Connection.GetHashCode();
				ulong num2 = (ulong)BytesCount.GetHashCode();
				ulong num3 = (ulong)LogLevel.GetHashCode();
				return (ulong)(num + (long)num2) + num3;
			}
		}

		public ServerLogEvent(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, global::Unity.Multiplayer.Tools.MetricTypes.LogLevel logLevel, long bytesCount)
		{
			Connection = connection;
			LogLevel = logLevel;
			BytesCount = bytesCount;
		}
	}
}
