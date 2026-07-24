namespace Unity.Multiplayer.Tools.Adapters
{
	internal static class NetworkAdapters
	{
		private static readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter> s_Adapters = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter>();

		public static global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter> Adapters => s_Adapters;

		public static event global::System.Action<global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter> OnAdapterAdded;

		public static event global::System.Action<global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter> OnAdapterRemoved;

		public static void AddAdapter(global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter)
		{
			if (!s_Adapters.Contains(adapter))
			{
				s_Adapters.Add(adapter);
				global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.OnAdapterAdded?.Invoke(adapter);
			}
		}

		public static void RemoveAdapter(global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter)
		{
			if (s_Adapters.Contains(adapter))
			{
				s_Adapters.Remove(adapter);
				global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.OnAdapterRemoved?.Invoke(adapter);
			}
		}

		public static global::Unity.Multiplayer.Tools.Adapters.UnsubscribeFromAllAdapters SubscribeToAll(global::System.Action<global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter> subscribeToAdapter, global::System.Action<global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter> unsubscribeFromAdapter)
		{
			foreach (global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter s_Adapter in s_Adapters)
			{
				subscribeToAdapter(s_Adapter);
			}
			OnAdapterAdded += subscribeToAdapter;
			OnAdapterRemoved += unsubscribeFromAdapter;
			return UnsubscribeFromAllAdapters;
			void UnsubscribeFromAllAdapters()
			{
				foreach (global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter s_Adapter2 in s_Adapters)
				{
					unsubscribeFromAdapter(s_Adapter2);
				}
				OnAdapterAdded -= subscribeToAdapter;
				OnAdapterRemoved -= unsubscribeFromAdapter;
			}
		}
	}
}
