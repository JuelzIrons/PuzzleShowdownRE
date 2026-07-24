namespace Unity.Multiplayer.Tools.Adapters.Ngo1
{
	internal static class Ngo1AdapterInitializer
	{
		private static bool s_Initialized;

		[global::UnityEngine.RuntimeInitializeOnLoadMethod]
		internal static void InitializeAdapter()
		{
			if (!s_Initialized)
			{
				s_Initialized = true;
				global::Unity.Multiplayer.Tools.Common.TaskExtensions.Forget(InitializeAdapterAsync());
			}
		}

		private static async global::System.Threading.Tasks.Task InitializeAdapterAsync()
		{
			global::Unity.Multiplayer.Tools.Adapters.Ngo1.Ngo1Adapter ngo1Adapter = new global::Unity.Multiplayer.Tools.Adapters.Ngo1.Ngo1Adapter(await GetNetworkManagerAsync());
			global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.AddAdapter(ngo1Adapter);
			global::Unity.Netcode.NetworkManager.OnInstantiated += async delegate
			{
				global::Unity.Netcode.NetworkManager networkManager = await GetNetworkManagerAsync();
				ngo1Adapter.ReplaceNetworkManager(networkManager);
			};
			global::Unity.Netcode.NetworkManager.OnDestroying += delegate
			{
				ngo1Adapter.Deinitialize();
				s_Initialized = false;
			};
		}

		private static async global::System.Threading.Tasks.Task<global::Unity.Netcode.NetworkManager> GetNetworkManagerAsync()
		{
			while (global::Unity.Netcode.NetworkManager.Singleton == null || global::Unity.Netcode.NetworkManager.Singleton.NetworkTickSystem == null)
			{
				await global::System.Threading.Tasks.Task.Yield();
			}
			return global::Unity.Netcode.NetworkManager.Singleton;
		}
	}
}
