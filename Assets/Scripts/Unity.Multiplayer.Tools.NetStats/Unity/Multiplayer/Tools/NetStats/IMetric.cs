namespace Unity.Multiplayer.Tools.NetStats
{
	internal interface IMetric
	{
		string Name { get; }

		global::Unity.Multiplayer.Tools.NetStats.MetricId Id { get; }

		global::Unity.Multiplayer.Tools.NetStats.MetricContainerType MetricContainerType { get; }

		global::Unity.Collections.FixedString128Bytes FactoryTypeName { get; }

		int GetWriteSize();

		void Write(global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter writer);

		void Read(global::Unity.Multiplayer.Tools.NetStats.FastBufferReader reader);
	}
	internal interface IMetric<TValue> : global::Unity.Multiplayer.Tools.NetStats.IMetric
	{
		TValue Value { get; }
	}
}
