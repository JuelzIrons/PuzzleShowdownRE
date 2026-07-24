namespace Unity.Services.Multiplayer
{
	internal class NetworkManagerSession : global::System.IDisposable
	{
		private const string k_EnclosingType = "NetworkManagerSession";

		private bool m_IsTransportCached;

		private global::Unity.Netcode.NetworkTransport m_CachedTransport;

		private global::Unity.Services.Multiplayer.NetworkRole m_NetworkRole;

		private global::System.Threading.Tasks.TaskCompletionSource<object> m_StartAsyncCompletion;

		private global::System.Threading.Tasks.TaskCompletionSource<object> m_StopAsyncCompletion;

		private bool m_IsDASettingsCached;

		private bool m_CachedUseCMBService;

		private global::Unity.Netcode.NetworkTopologyTypes m_CachedTopologyType;

		internal static global::System.TimeSpan CancellationTimeout { get; set; } = global::System.TimeSpan.FromSeconds(5.0);

		internal global::Unity.Netcode.NetworkManager NetworkManager { get; private set; }

		internal bool Disposed { get; private set; }

		public NetworkManagerSession([global::System.Diagnostics.CodeAnalysis.NotNull] global::Unity.Netcode.NetworkManager manager, global::Unity.Services.Multiplayer.NetworkRole role)
		{
			NetworkManager = manager;
			m_NetworkRole = role;
		}

		public async global::System.Threading.Tasks.Task StartAsync()
		{
			if (Disposed)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallWarning("NetworkManagerSession", "Called after dispose.", "StartAsync");
				return;
			}
			if (NetworkManager.IsClient || NetworkManager.IsServer)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallWarning("NetworkManagerSession", "NetworkManager is already connected.", "StartAsync");
				return;
			}
			if (m_StartAsyncCompletion != null)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallWarning("NetworkManagerSession", "NetworkManagerSession is already started.", "StartAsync");
				await m_StartAsyncCompletion.Task;
				return;
			}
			SubscribeToRelevantCallbacks();
			m_StartAsyncCompletion = new global::System.Threading.Tasks.TaskCompletionSource<object>();
			using global::System.Threading.CancellationTokenSource cancelToken = global::System.Threading.CancellationTokenSource.CreateLinkedTokenSource(global::UnityEngine.Application.exitCancellationToken);
			cancelToken.CancelAfter(CancellationTimeout);
			await using (cancelToken.Token.Register(delegate
			{
				m_StartAsyncCompletion.TrySetCanceled();
			}))
			{
				_ = 1;
				try
				{
					if (m_NetworkRole switch
					{
						global::Unity.Services.Multiplayer.NetworkRole.Server => NetworkManager.StartServer() ? 1 : 0, 
						global::Unity.Services.Multiplayer.NetworkRole.Host => NetworkManager.StartHost() ? 1 : 0, 
						global::Unity.Services.Multiplayer.NetworkRole.Client => NetworkManager.StartClient() ? 1 : 0, 
						_ => 0, 
					} == 0)
					{
						throw new global::Unity.Services.Multiplayer.SessionException(string.Format("Failed to start {0} component as {1:G}.", "NetworkManager", m_NetworkRole), global::Unity.Services.Multiplayer.SessionError.NetworkManagerStartFailed);
					}
					await m_StartAsyncCompletion.Task;
				}
				catch (global::System.Exception)
				{
					m_StartAsyncCompletion.TrySetCanceled();
					Dispose();
					throw;
				}
			}
		}

		private void OnStartCompleted()
		{
			if (m_StartAsyncCompletion == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallError("NetworkManagerSession", "[NetworkManager has started but the task was never created.", "OnStartCompleted");
				Dispose();
			}
			else
			{
				m_StartAsyncCompletion.TrySetResult(null);
			}
		}

		public async global::System.Threading.Tasks.Task StopAsync()
		{
			if (Disposed)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallWarning("NetworkManagerSession", "Called after dispose.", "StopAsync");
				return;
			}
			if (m_StartAsyncCompletion == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallWarning("NetworkManagerSession", "NetworkManager was started outside of the session. Ensure Shutdown is called manually.", "StopAsync");
				return;
			}
			if (m_StopAsyncCompletion != null)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallWarning("NetworkManagerSession", "Called more than once.", "StopAsync");
				await m_StopAsyncCompletion.Task;
				return;
			}
			m_StopAsyncCompletion = new global::System.Threading.Tasks.TaskCompletionSource<object>();
			using global::System.Threading.CancellationTokenSource cancelToken = global::System.Threading.CancellationTokenSource.CreateLinkedTokenSource(global::UnityEngine.Application.exitCancellationToken);
			cancelToken.CancelAfter(CancellationTimeout);
			await using (cancelToken.Token.Register(delegate
			{
				global::Unity.Services.Multiplayer.Logger.LogCallError("NetworkManagerSession", "NetworkManager timed out while waiting to shut down.", "StopAsync");
				m_StopAsyncCompletion.TrySetResult(null);
			}))
			{
				NetworkManager.Shutdown();
				await m_StopAsyncCompletion.Task;
				Dispose();
			}
		}

		private async global::System.Threading.Tasks.Task OnStopCompleted()
		{
			if (m_StopAsyncCompletion == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallWarning("NetworkManagerSession", "Called from outside of the session.", "OnStopCompleted");
				return;
			}
			await global::UnityEngine.Awaitable.EndOfFrameAsync();
			m_StopAsyncCompletion.TrySetResult(null);
		}

		public void SetNetworkRole(global::Unity.Services.Multiplayer.NetworkRole newRole)
		{
			m_NetworkRole = newRole;
		}

		public global::Unity.Netcode.Transports.UTP.UnityTransport GetUnityTransport()
		{
			return NetworkManager.NetworkConfig.NetworkTransport as global::Unity.Netcode.Transports.UTP.UnityTransport;
		}

		public global::Unity.Services.DistributedAuthority.DistributedAuthorityTransport GetDistributedAuthorityTransport()
		{
			return NetworkManager.NetworkConfig.NetworkTransport as global::Unity.Services.DistributedAuthority.DistributedAuthorityTransport;
		}

		public void SetTransport(global::Unity.Netcode.Transports.UTP.UnityTransport transport)
		{
			if (!Disposed)
			{
				if (!m_IsTransportCached)
				{
					m_IsTransportCached = true;
					m_CachedTransport = NetworkManager.NetworkConfig.NetworkTransport;
				}
				NetworkManager.NetworkConfig.NetworkTransport = transport;
			}
		}

		public void ConfigureForDistributedAuthority()
		{
			if (!Disposed)
			{
				if (!m_IsDASettingsCached)
				{
					m_IsDASettingsCached = true;
					m_CachedUseCMBService = NetworkManager.NetworkConfig.UseCMBService;
					m_CachedTopologyType = NetworkManager.NetworkConfig.NetworkTopology;
				}
				NetworkManager.NetworkConfig.UseCMBService = true;
				NetworkManager.NetworkConfig.NetworkTopology = global::Unity.Netcode.NetworkTopologyTypes.DistributedAuthority;
			}
		}

		private void SubscribeToRelevantCallbacks()
		{
			switch (m_NetworkRole)
			{
			case global::Unity.Services.Multiplayer.NetworkRole.Server:
				NetworkManager.OnServerStarted += OnServerStarted;
				NetworkManager.OnServerStopped += OnManagerStopped;
				break;
			case global::Unity.Services.Multiplayer.NetworkRole.Host:
				NetworkManager.OnConnectionEvent += OnConnectionEvent;
				NetworkManager.OnServerStopped += OnManagerStopped;
				break;
			case global::Unity.Services.Multiplayer.NetworkRole.Client:
				NetworkManager.OnConnectionEvent += OnConnectionEvent;
				NetworkManager.OnClientStopped += OnManagerStopped;
				break;
			}
		}

		private void DisposeCallbacks()
		{
			NetworkManager.OnServerStarted -= OnServerStarted;
			NetworkManager.OnConnectionEvent -= OnConnectionEvent;
			NetworkManager.OnClientStopped -= OnManagerStopped;
			NetworkManager.OnServerStopped -= OnManagerStopped;
		}

		private void OnConnectionEvent(global::Unity.Netcode.NetworkManager eventManager, global::Unity.Netcode.ConnectionEventData eventData)
		{
			if (eventData.EventType == global::Unity.Netcode.ConnectionEvent.ClientConnected)
			{
				if (NetworkManager.LocalClientId != eventManager.LocalClientId || NetworkManager.LocalClientId != eventData.ClientId)
				{
					global::Unity.Services.Multiplayer.Logger.LogCallError("NetworkManagerSession", string.Format("[{0}] received unexpected {1} for Client-{2}.", "StartAsync", "ConnectionEvent", eventData.ClientId), "OnConnectionEvent");
					return;
				}
				OnStartCompleted();
				eventManager.OnConnectionEvent -= OnConnectionEvent;
			}
		}

		private void OnServerStarted()
		{
			OnStartCompleted();
			NetworkManager.OnServerStarted -= OnServerStarted;
		}

		private async void OnManagerStopped(bool _)
		{
			await OnStopCompleted();
			Dispose();
		}

		public void Dispose()
		{
			if (Disposed)
			{
				return;
			}
			DisposeCallbacks();
			if (m_IsTransportCached)
			{
				NetworkManager.NetworkConfig.NetworkTransport = m_CachedTransport;
			}
			if (m_IsDASettingsCached)
			{
				NetworkManager.NetworkConfig.UseCMBService = m_CachedUseCMBService;
				NetworkManager.NetworkConfig.NetworkTopology = m_CachedTopologyType;
			}
			if (m_StartAsyncCompletion != null)
			{
				if (!m_StartAsyncCompletion.Task.IsCompleted)
				{
					m_StartAsyncCompletion.TrySetCanceled();
				}
				m_StartAsyncCompletion = null;
			}
			if (m_StopAsyncCompletion != null)
			{
				if (!m_StopAsyncCompletion.Task.IsCompleted)
				{
					m_StopAsyncCompletion.TrySetCanceled();
				}
				m_StopAsyncCompletion = null;
			}
			Disposed = true;
		}
	}
}
