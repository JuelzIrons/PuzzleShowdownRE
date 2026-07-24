namespace Unity.Multiplayer.Tools.NetStats
{
	internal interface INetStatSerializer
	{
		global::Unity.Collections.NativeArray<byte> Serialize(global::Unity.Multiplayer.Tools.NetStats.MetricCollection metricCollection);

		global::Unity.Multiplayer.Tools.NetStats.MetricCollection Deserialize(global::Unity.Collections.NativeArray<byte> bytes);
	}
}
