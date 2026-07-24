namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::System.Serializable]
	internal struct UnnamedMessageEvent : global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent
	{
		public global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo Connection { get; }

		public long BytesCount { get; }

		public ulong TreeViewId
		{
			get
			{
				long num = Connection.GetHashCode();
				ulong num2 = (ulong)BytesCount.GetHashCode();
				return (ulong)num + num2;
			}
		}

		public UnnamedMessageEvent(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, long bytesCount)
		{
			Connection = connection;
			BytesCount = bytesCount;
		}
	}
}
