namespace Unity.Multiplayer.Tools.Adapters
{
	internal interface IMetricCollectionEvent : global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent
	{
		event global::System.Action<global::Unity.Multiplayer.Tools.NetStats.MetricCollection> MetricCollectionEvent;
	}
}
