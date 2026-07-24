namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	internal class NetworkTransportApi : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkTransportApi, global::System.IDisposable
	{
		private readonly global::System.Collections.Generic.IList<global::Unity.Multiplayer.Tools.Adapters.INetworkAvailability> m_NetworkAvailabilityComponents = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.INetworkAvailability>();

		private readonly global::System.Collections.Generic.IList<global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnectAndReconnect> m_DisconnectAndReconnectComponents = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnectAndReconnect>();

		private readonly global::System.Collections.Generic.IList<global::Unity.Multiplayer.Tools.Adapters.IHandleNetworkParameters> m_HandleNetworkParametersComponents = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.IHandleNetworkParameters>();

		public bool IsAvailable
		{
			get
			{
				if (global::System.Linq.Enumerable.Any(m_NetworkAvailabilityComponents) && global::System.Linq.Enumerable.Any(m_DisconnectAndReconnectComponents))
				{
					return global::System.Linq.Enumerable.Any(m_HandleNetworkParametersComponents);
				}
				return false;
			}
		}

		public bool IsConnected
		{
			get
			{
				if (IsAvailable)
				{
					return global::System.Linq.Enumerable.All(m_NetworkAvailabilityComponents, (global::Unity.Multiplayer.Tools.Adapters.INetworkAvailability x) => x.IsConnected);
				}
				return false;
			}
		}

		public NetworkTransportApi()
		{
			SubscribeToAllAdapters();
		}

		public void Dispose()
		{
			UnsubscribeFromAllAdapters();
		}

		public void SimulateDisconnect()
		{
			foreach (global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnectAndReconnect disconnectAndReconnectComponent in m_DisconnectAndReconnectComponents)
			{
				disconnectAndReconnectComponent.SimulateDisconnect();
			}
		}

		public void SimulateReconnect()
		{
			foreach (global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnectAndReconnect disconnectAndReconnectComponent in m_DisconnectAndReconnectComponents)
			{
				disconnectAndReconnectComponent.SimulateReconnect();
			}
		}

		public void UpdateNetworkParameters(global::Unity.Multiplayer.Tools.Adapters.NetworkParameters networkParameters)
		{
			foreach (global::Unity.Multiplayer.Tools.Adapters.IHandleNetworkParameters handleNetworkParametersComponent in m_HandleNetworkParametersComponents)
			{
				handleNetworkParametersComponent.NetworkParameters = networkParameters;
			}
		}

		private void SubscribeToAllAdapters()
		{
			foreach (global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter in global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.Adapters)
			{
				SubscribeToAdapter(adapter);
			}
			global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.OnAdapterAdded += SubscribeToAdapter;
			global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.OnAdapterRemoved += UnsubscribeFromAdapter;
		}

		private void UnsubscribeFromAllAdapters()
		{
			foreach (global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter in global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.Adapters)
			{
				UnsubscribeFromAdapter(adapter);
			}
			global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.OnAdapterAdded -= SubscribeToAdapter;
			global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.OnAdapterRemoved -= UnsubscribeFromAdapter;
		}

		private void SubscribeToAdapter(global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter)
		{
			global::Unity.Multiplayer.Tools.Adapters.INetworkAvailability component = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.INetworkAvailability>();
			if (component != null)
			{
				m_NetworkAvailabilityComponents.Add(component);
			}
			global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnectAndReconnect component2 = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnectAndReconnect>();
			if (component2 != null)
			{
				m_DisconnectAndReconnectComponents.Add(component2);
			}
			global::Unity.Multiplayer.Tools.Adapters.IHandleNetworkParameters component3 = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.IHandleNetworkParameters>();
			if (component3 != null)
			{
				m_HandleNetworkParametersComponents.Add(component3);
			}
		}

		private void UnsubscribeFromAdapter(global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter)
		{
			global::Unity.Multiplayer.Tools.Adapters.INetworkAvailability component = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.INetworkAvailability>();
			if (component != null)
			{
				m_NetworkAvailabilityComponents.Remove(component);
			}
			global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnectAndReconnect component2 = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnectAndReconnect>();
			if (component2 != null)
			{
				m_DisconnectAndReconnectComponents.Remove(component2);
			}
			global::Unity.Multiplayer.Tools.Adapters.IHandleNetworkParameters component3 = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.IHandleNetworkParameters>();
			if (component3 != null)
			{
				m_HandleNetworkParametersComponents.Remove(component3);
			}
		}
	}
}
