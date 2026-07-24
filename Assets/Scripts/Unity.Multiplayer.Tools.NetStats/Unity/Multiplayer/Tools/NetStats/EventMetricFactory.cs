namespace Unity.Multiplayer.Tools.NetStats
{
	internal class EventMetricFactory : global::Unity.Multiplayer.Tools.NetStats.IMetricFactory
	{
		private interface IEventMetricFactory
		{
			global::Unity.Multiplayer.Tools.NetStats.IMetric Construct(global::Unity.Multiplayer.Tools.NetStats.MetricId id);
		}

		private class EventMetricFactoryImpl<T> : global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.IEventMetricFactory where T : unmanaged
		{
			public global::Unity.Multiplayer.Tools.NetStats.IMetric Construct(global::Unity.Multiplayer.Tools.NetStats.MetricId id)
			{
				return new global::Unity.Multiplayer.Tools.NetStats.EventMetric<T>(id);
			}
		}

		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.Collections.FixedString128Bytes, global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.IEventMetricFactory> k_FactoriesByName;

		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Collections.FixedString128Bytes> k_TypeNames;

		public static bool TryGetFactoryTypeName(global::System.Type type, out global::Unity.Collections.FixedString128Bytes typeName)
		{
			return k_TypeNames.TryGetValue(type, out typeName);
		}

		static EventMetricFactory()
		{
			k_FactoriesByName = new global::System.Collections.Generic.Dictionary<global::Unity.Collections.FixedString128Bytes, global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.IEventMetricFactory>();
			k_TypeNames = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Collections.FixedString128Bytes>();
			global::Unity.Multiplayer.Tools.NetStats.TypeRegistration.RunIfNeeded();
		}

		internal static void RegisterType<T>() where T : unmanaged
		{
			if (!k_TypeNames.ContainsKey(typeof(T)))
			{
				global::Unity.Collections.FixedString128Bytes fixedString128Bytes = typeof(T).FullName;
				k_FactoriesByName.Add(fixedString128Bytes, new global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.EventMetricFactoryImpl<T>());
				k_TypeNames.Add(typeof(T), fixedString128Bytes);
			}
		}

		public bool TryConstruct(global::Unity.Multiplayer.Tools.NetStats.MetricHeader header, out global::Unity.Multiplayer.Tools.NetStats.IMetric metric)
		{
			if (!k_FactoriesByName.TryGetValue(header.EventFactoryTypeName, out var value))
			{
				global::UnityEngine.Debug.LogError("Failed to find factory for event type " + header.EventFactoryTypeName.ToString());
				metric = null;
				return false;
			}
			metric = value.Construct(header.MetricId);
			return true;
		}
	}
}
