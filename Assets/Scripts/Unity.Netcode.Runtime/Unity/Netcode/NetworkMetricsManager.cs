namespace Unity.Netcode
{
	internal class NetworkMetricsManager
	{
		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		internal global::Unity.Netcode.INetworkMetrics NetworkMetrics { get; private set; }

		public void UpdateMetrics()
		{
			NetworkMetrics.UpdateNetworkObjectsCount(m_NetworkManager.SpawnManager.SpawnedObjects.Count);
			NetworkMetrics.UpdateConnectionsCount((!m_NetworkManager.IsServer) ? 1 : m_NetworkManager.ConnectionManager.ConnectedClients.Count);
			NetworkMetrics.DispatchFrame();
		}

		public void Initialize(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_NetworkManager = networkManager;
			if (NetworkMetrics == null)
			{
				NetworkMetrics = new global::Unity.Netcode.NetworkMetrics();
			}
			global::Unity.Multiplayer.Tools.NetworkSolutionInterface.SetInterface(new global::Unity.Multiplayer.Tools.NetworkSolutionInterfaceParameters
			{
				NetworkObjectProvider = new global::Unity.Netcode.NetworkObjectProvider(networkManager)
			});
		}
	}
}
