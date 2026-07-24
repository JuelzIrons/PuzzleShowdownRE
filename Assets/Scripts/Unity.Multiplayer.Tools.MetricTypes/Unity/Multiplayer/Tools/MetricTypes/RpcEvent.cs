namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::System.Serializable]
	internal struct RpcEvent : global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent, global::Unity.Multiplayer.Tools.MetricTypes.INetworkObjectEvent
	{
		public global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo Connection { get; }

		public global::Unity.Multiplayer.Tools.MetricTypes.NetworkObjectIdentifier NetworkId { get; }

		public global::Unity.Collections.FixedString64Bytes Name { get; }

		public global::Unity.Collections.FixedString64Bytes NetworkBehaviourName { get; }

		public long BytesCount { get; }

		public ulong TreeViewId
		{
			get
			{
				long num = Name.GetHashCode();
				ulong num2 = (ulong)NetworkId.GetHashCode();
				ulong num3 = (ulong)NetworkBehaviourName.GetHashCode();
				ulong num4 = (ulong)BytesCount.GetHashCode();
				ulong num5 = (ulong)Connection.GetHashCode();
				return (ulong)(num + (long)num2 + (long)num3 + (long)num4) + num5;
			}
		}

		public RpcEvent(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, global::Unity.Multiplayer.Tools.MetricTypes.NetworkObjectIdentifier networkId, string name, string networkBehaviourName, long bytesCount)
			: this(connection, networkId, global::Unity.Multiplayer.Tools.MetricTypes.StringConversionUtility.ConvertToFixedString(name), global::Unity.Multiplayer.Tools.MetricTypes.StringConversionUtility.ConvertToFixedString(networkBehaviourName), bytesCount)
		{
		}

		public RpcEvent(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, global::Unity.Multiplayer.Tools.MetricTypes.NetworkObjectIdentifier networkId, global::Unity.Collections.FixedString64Bytes name, global::Unity.Collections.FixedString64Bytes networkBehaviourName, long bytesCount)
		{
			Connection = connection;
			NetworkId = networkId;
			Name = name;
			NetworkBehaviourName = networkBehaviourName;
			BytesCount = bytesCount;
		}
	}
}
