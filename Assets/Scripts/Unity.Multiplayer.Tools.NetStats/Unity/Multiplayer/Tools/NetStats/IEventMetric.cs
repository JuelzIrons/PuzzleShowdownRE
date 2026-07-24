namespace Unity.Multiplayer.Tools.NetStats
{
	internal interface IEventMetric : global::Unity.Multiplayer.Tools.NetStats.IMetric
	{
		int Count { get; }

		int MaxNumberOfValues { get; }

		int NumberOfValuesReceived { get; }
	}
	internal interface IEventMetric<TValue> : global::Unity.Multiplayer.Tools.NetStats.IEventMetric, global::Unity.Multiplayer.Tools.NetStats.IMetric
	{
		global::System.Collections.Generic.IReadOnlyList<TValue> Values { get; }
	}
}
