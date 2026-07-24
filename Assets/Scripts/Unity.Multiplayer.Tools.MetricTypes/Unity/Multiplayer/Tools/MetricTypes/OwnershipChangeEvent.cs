namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::System.Serializable]
	internal struct OwnershipChangeEvent : global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent, global::Unity.Multiplayer.Tools.MetricTypes.INetworkObjectEvent
	{
		public global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo Connection { get; }

		public global::Unity.Multiplayer.Tools.MetricTypes.NetworkObjectIdentifier NetworkId { get; }

		public long BytesCount { get; }

		public ulong TreeViewId
		{
			get
			{
				long num = NetworkId.GetHashCode();
				ulong num2 = (ulong)BytesCount.GetHashCode();
				ulong num3 = (ulong)Connection.GetHashCode();
				return (ulong)(num + (long)num2) + num3;
			}
		}

		public OwnershipChangeEvent(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, global::Unity.Multiplayer.Tools.MetricTypes.NetworkObjectIdentifier networkId, long bytesCount)
		{
			Connection = connection;
			NetworkId = networkId;
			BytesCount = bytesCount;
		}
	}
}
