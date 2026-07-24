namespace Unity.Multiplayer.Tools.Adapters.Ngo1WithUtp2
{
	internal static class Ngo1WithUtp2AdapterInitializer
	{
		private static bool s_Initialized;

		internal static readonly global::System.Collections.Generic.IDictionary<global::UnityEngine.EntityId, global::Unity.Multiplayer.Tools.Adapters.Utp2.Utp2Adapter> s_Adapters = new global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::Unity.Multiplayer.Tools.Adapters.Utp2.Utp2Adapter>();

		[global::UnityEngine.RuntimeInitializeOnLoadMethod]
		internal static void InitializeAdapter()
		{
			if (!s_Initialized)
			{
				s_Initialized = true;
				global::Unity.Netcode.Transports.UTP.UnityTransport.OnDriverInitialized += AddAdapter;
				global::Unity.Netcode.Transports.UTP.UnityTransport.OnDisposingDriver += RemoveAdapter;
			}
		}

		private static void AddAdapter(global::UnityEngine.EntityId entityId, global::Unity.Networking.Transport.NetworkDriver networkDriver)
		{
			if (!s_Adapters.ContainsKey(entityId))
			{
				global::Unity.Multiplayer.Tools.Adapters.Utp2.Utp2Adapter utp2Adapter = new global::Unity.Multiplayer.Tools.Adapters.Utp2.Utp2Adapter(networkDriver);
				s_Adapters[entityId] = utp2Adapter;
				global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.AddAdapter(utp2Adapter);
			}
		}

		private static void RemoveAdapter(global::UnityEngine.EntityId entityId)
		{
			if (s_Adapters.TryGetValue(entityId, out var value))
			{
				global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.RemoveAdapter(value);
				s_Adapters.Remove(entityId);
			}
		}
	}
}
