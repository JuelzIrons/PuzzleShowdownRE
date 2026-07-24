public class _003CNetStats_TypeRegistration_003E
{
	[global::UnityEngine.Scripting.Preserve]
	static void Run()
	{
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent>();
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent>();
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent>();
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent>();
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent>();
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent>();
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent>();
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent>();
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent>();
		global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.RegisterType<global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric>();
	}
}
