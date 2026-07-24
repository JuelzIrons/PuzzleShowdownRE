namespace Unity.Multiplayer.Tools.Adapters.Ngo1
{
	internal class Ngo1Adapter : global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter, global::Unity.Multiplayer.Tools.Adapters.IGetConnectedClients, global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent, global::Unity.Multiplayer.Tools.Adapters.IMetricCollectionEvent, global::Unity.Multiplayer.Tools.Adapters.IGetConnectionStatus, global::Unity.Multiplayer.Tools.Adapters.IGetBandwidth, global::Unity.Multiplayer.Tools.Adapters.IGetClientId, global::Unity.Multiplayer.Tools.Adapters.IGetGameObject, global::Unity.Multiplayer.Tools.Adapters.IGetObjectIds, global::Unity.Multiplayer.Tools.Adapters.IGetOwnership, global::Unity.Multiplayer.Tools.Adapters.IGetRpcCount
	{
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		private readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.ClientId> m_ClientIds = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.ClientId>();

		private readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.ObjectId> m_ObjectIds = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.ObjectId>();

		private global::Unity.Multiplayer.Tools.Adapters.Ngo1.ObjectBandwidthCache m_BandwidthCache;

		private global::Unity.Multiplayer.Tools.Adapters.Ngo1.ObjectRpcCountCache m_RpcCountCache;

		private global::Unity.Netcode.NetworkSpawnManager SpawnManager
		{
			[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
			get
			{
				return m_NetworkManager?.SpawnManager;
			}
		}

		private global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject> SpawnedObjects
		{
			[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
			get
			{
				return SpawnManager?.SpawnedObjects;
			}
		}

		public global::Unity.Multiplayer.Tools.Adapters.AdapterMetadata Metadata { get; } = new global::Unity.Multiplayer.Tools.Adapters.AdapterMetadata
		{
			PackageInfo = new global::Unity.Multiplayer.Tools.Adapters.PackageInfo
			{
				PackageName = "com.unity.netcode.gameobjects",
				Version = new global::Unity.Multiplayer.Tools.Adapters.PackageVersion
				{
					Major = 1,
					Minor = 0,
					Patch = 0,
					PreRelease = ""
				}
			}
		};

		public global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.Adapters.ClientId> ConnectedClients => m_ClientIds;

		public global::Unity.Multiplayer.Tools.Adapters.ClientId LocalClientId => (global::Unity.Multiplayer.Tools.Adapters.ClientId)m_NetworkManager.LocalClientId;

		public global::Unity.Multiplayer.Tools.Adapters.ClientId ServerClientId => (global::Unity.Multiplayer.Tools.Adapters.ClientId)0L;

		public global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.Adapters.ObjectId> ObjectIds
		{
			get
			{
				RefreshObjectIds();
				return m_ObjectIds;
			}
		}

		public bool IsCacheEmpty
		{
			get
			{
				if (m_BandwidthCache != null)
				{
					return m_BandwidthCache.IsCold;
				}
				return true;
			}
		}

		public global::Unity.Multiplayer.Tools.Common.BandwidthTypes SupportedBandwidthTypes => global::Unity.Multiplayer.Tools.Common.BandwidthTypes.All;

		public event global::System.Action<global::Unity.Multiplayer.Tools.Adapters.ClientId> ClientConnectionEvent;

		public event global::System.Action<global::Unity.Multiplayer.Tools.Adapters.ClientId> ClientDisconnectionEvent;

		public event global::System.Action ServerOrClientStarted;

		public event global::System.Action ServerOrClientStopped;

		public event global::System.Action<global::Unity.Multiplayer.Tools.NetStats.MetricCollection> MetricCollectionEvent;

		private event global::System.Action m_OnBandwidthUpdated;

		public event global::System.Action OnBandwidthUpdated
		{
			add
			{
				if (m_BandwidthCache == null)
				{
					m_BandwidthCache = new global::Unity.Multiplayer.Tools.Adapters.Ngo1.ObjectBandwidthCache();
				}
				m_OnBandwidthUpdated += value;
			}
			remove
			{
				m_OnBandwidthUpdated -= value;
				if (this.m_OnBandwidthUpdated == null)
				{
					m_BandwidthCache = null;
				}
			}
		}

		private event global::System.Action m_OnRpcCountUpdated;

		public event global::System.Action OnRpcCountUpdated
		{
			add
			{
				if (m_RpcCountCache == null)
				{
					m_RpcCountCache = new global::Unity.Multiplayer.Tools.Adapters.Ngo1.ObjectRpcCountCache();
				}
				m_OnRpcCountUpdated += value;
			}
			remove
			{
				m_OnRpcCountUpdated -= value;
				if (this.m_OnRpcCountUpdated == null)
				{
					m_RpcCountCache = null;
				}
			}
		}

		public Ngo1Adapter([global::System.Diagnostics.CodeAnalysis.NotNull] global::Unity.Netcode.NetworkManager networkManager)
		{
			Init(networkManager);
		}

		internal void ReplaceNetworkManager(global::Unity.Netcode.NetworkManager networkManager)
		{
			Deinitialize();
			Init(networkManager);
		}

		private void Init(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_NetworkManager = networkManager;
			m_NetworkManager.OnConnectionEvent += OnConnectionEvent;
			m_NetworkManager.OnServerStarted += OnServerOrClientStarted;
			m_NetworkManager.OnClientStarted += OnServerOrClientStarted;
			m_NetworkManager.OnServerStopped += OnServerOrClientStopped;
			m_NetworkManager.OnClientStopped += OnServerOrClientStopped;
			if (m_NetworkManager.IsConnectedClient || m_NetworkManager.IsServer)
			{
				OnServerOrClientStarted();
			}
			global::Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher.OnMetricsReceived += OnMetricsReceived;
			RefreshClientIds();
		}

		internal void Deinitialize()
		{
			if (m_NetworkManager != null)
			{
				m_NetworkManager.OnConnectionEvent -= OnConnectionEvent;
				m_NetworkManager.OnServerStarted -= OnServerOrClientStarted;
				m_NetworkManager.OnClientStarted -= OnServerOrClientStarted;
				m_NetworkManager.OnServerStopped -= OnServerOrClientStopped;
				m_NetworkManager.OnClientStopped -= OnServerOrClientStopped;
				m_NetworkManager = null;
			}
			global::Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher.OnMetricsReceived -= OnMetricsReceived;
			ClearConnectedClients();
		}

		private void RefreshObjectIds()
		{
			m_ObjectIds.Clear();
			global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject> hashSet = m_NetworkManager.SpawnManager?.SpawnedObjectsList;
			if (hashSet == null)
			{
				return;
			}
			foreach (global::Unity.Netcode.NetworkObject item in hashSet)
			{
				m_ObjectIds.Add((global::Unity.Multiplayer.Tools.Adapters.ObjectId)item.NetworkObjectId);
			}
		}

		private void RefreshClientIds()
		{
			if (m_NetworkManager.IsServer)
			{
				m_ClientIds.Clear();
				for (int i = 0; i < m_NetworkManager.ConnectedClientsIds.Count; i++)
				{
					m_ClientIds.Add((global::Unity.Multiplayer.Tools.Adapters.ClientId)m_NetworkManager.ConnectedClientsIds[i]);
				}
			}
			else
			{
				if (m_NetworkManager.SpawnManager == null)
				{
					return;
				}
				foreach (var (clientId, dictionary2) in m_NetworkManager.SpawnManager.OwnershipToObjectsTable)
				{
					if (dictionary2.Count > 0)
					{
						OnClientConnected(clientId);
					}
				}
			}
		}

		public T GetComponent<T>() where T : class, global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent
		{
			return this as T;
		}

		private void OnClientConnected(ulong clientId)
		{
			if (!m_ClientIds.Contains((global::Unity.Multiplayer.Tools.Adapters.ClientId)clientId))
			{
				m_ClientIds.Add((global::Unity.Multiplayer.Tools.Adapters.ClientId)clientId);
			}
			this.ClientConnectionEvent?.Invoke((global::Unity.Multiplayer.Tools.Adapters.ClientId)clientId);
		}

		private void OnClientDisconnected(ulong clientId)
		{
			global::Unity.Multiplayer.Tools.Adapters.ClientId typedClientId = (global::Unity.Multiplayer.Tools.Adapters.ClientId)clientId;
			m_ClientIds.RemoveAll((global::Unity.Multiplayer.Tools.Adapters.ClientId id) => id == typedClientId);
			this.ClientDisconnectionEvent?.Invoke(typedClientId);
		}

		private void OnConnectionEvent(global::Unity.Netcode.NetworkManager networkManager, global::Unity.Netcode.ConnectionEventData clientConnectionData)
		{
			switch (clientConnectionData.EventType)
			{
			case global::Unity.Netcode.ConnectionEvent.ClientConnected:
			case global::Unity.Netcode.ConnectionEvent.PeerConnected:
				OnClientConnected(clientConnectionData.ClientId);
				{
					foreach (ulong peerClientId in clientConnectionData.PeerClientIds)
					{
						OnClientConnected(peerClientId);
					}
					break;
				}
			case global::Unity.Netcode.ConnectionEvent.ClientDisconnected:
			case global::Unity.Netcode.ConnectionEvent.PeerDisconnected:
				OnClientDisconnected(clientConnectionData.ClientId);
				break;
			default:
				global::UnityEngine.Debug.LogWarning("Unknown ConnectionEvent: " + clientConnectionData.EventType);
				break;
			}
		}

		private void OnServerOrClientStarted()
		{
			this.ServerOrClientStarted?.Invoke();
			RefreshClientIds();
		}

		private void OnServerOrClientStopped(bool isHost)
		{
			this.ServerOrClientStopped?.Invoke();
			ClearConnectedClients();
		}

		private void OnMetricsReceived(global::Unity.Multiplayer.Tools.NetStats.MetricCollection metricCollection)
		{
			UpdateNetworkTrafficCaches(metricCollection);
			this.MetricCollectionEvent?.Invoke(metricCollection);
		}

		public global::UnityEngine.GameObject GetGameObject(global::Unity.Multiplayer.Tools.Adapters.ObjectId objectId)
		{
			global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject> spawnedObjects = SpawnedObjects;
			if (spawnedObjects == null)
			{
				return null;
			}
			if (!spawnedObjects.TryGetValue((ulong)objectId, out var value))
			{
				return null;
			}
			return value.gameObject;
		}

		public global::Unity.Multiplayer.Tools.Adapters.ClientId GetOwner(global::Unity.Multiplayer.Tools.Adapters.ObjectId objectId)
		{
			global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject> spawnedObjects = SpawnedObjects;
			if (spawnedObjects != null && spawnedObjects.TryGetValue((ulong)objectId, out var value))
			{
				return (global::Unity.Multiplayer.Tools.Adapters.ClientId)value.OwnerClientId;
			}
			return (global::Unity.Multiplayer.Tools.Adapters.ClientId)0L;
		}

		private void UpdateNetworkTrafficCaches(global::Unity.Multiplayer.Tools.NetStats.MetricCollection metricCollection)
		{
			if (this.m_OnBandwidthUpdated != null)
			{
				m_BandwidthCache.Update(metricCollection);
				this.m_OnBandwidthUpdated();
			}
			if (this.m_OnRpcCountUpdated != null)
			{
				m_RpcCountCache.Update(metricCollection);
				this.m_OnRpcCountUpdated();
			}
		}

		public float GetBandwidthBytes(global::Unity.Multiplayer.Tools.Adapters.ObjectId objectId, global::Unity.Multiplayer.Tools.Common.BandwidthTypes bandwidthTypes = global::Unity.Multiplayer.Tools.Common.BandwidthTypes.All, global::Unity.Multiplayer.Tools.Common.NetworkDirection networkDirection = global::Unity.Multiplayer.Tools.Common.NetworkDirection.SentAndReceived)
		{
			return (m_BandwidthCache ?? throw new global::Unity.Multiplayer.Tools.Adapters.NoSubscribersException("IGetBandwidth", "OnBandwidthUpdated")).GetBandwidth(objectId, bandwidthTypes, networkDirection);
		}

		public int GetRpcCount(global::Unity.Multiplayer.Tools.Adapters.ObjectId objectId)
		{
			return (m_RpcCountCache ?? throw new global::Unity.Multiplayer.Tools.Adapters.NoSubscribersException("IGetRpcCount", "OnRpcCountUpdated")).GetRpcCount(objectId);
		}

		private void ClearConnectedClients()
		{
			foreach (global::Unity.Multiplayer.Tools.Adapters.ClientId item in new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Adapters.ClientId>(m_ClientIds))
			{
				OnClientDisconnected((ulong)item);
			}
			m_ClientIds.Clear();
		}
	}
}
