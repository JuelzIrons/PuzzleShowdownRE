namespace Unity.Netcode
{
	[global::UnityEngine.AddComponentMenu("Netcode/Network Manager", -100)]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/manual/components/core/networkmanager.html")]
	public class NetworkManager : global::UnityEngine.MonoBehaviour, global::Unity.Netcode.INetworkUpdateSystem
	{
		public delegate void RpcReceiveHandler(global::Unity.Netcode.NetworkBehaviour behaviour, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams parameters);

		internal delegate global::Unity.Netcode.SessionConfig OnGetSessionConfigHandler();

		public delegate global::UnityEngine.GameObject OnFetchLocalPlayerPrefabToSpawnDelegateHandler();

		public delegate void OnSessionOwnerPromotedDelegateHandler(ulong sessionOwnerPromoted);

		internal enum ServerShutdownStates
		{
			None = 0,
			WaitForClientDisconnects = 1,
			InternalShutdown = 2,
			ShuttingDown = 3
		}

		public delegate void ReanticipateDelegate(double lastRoundTripTime);

		public class ConnectionApprovalResponse
		{
			public bool Approved;

			public bool CreatePlayerObject;

			public uint? PlayerPrefabHash;

			public global::UnityEngine.Vector3? Position;

			public global::UnityEngine.Quaternion? Rotation;

			public bool Pending;

			public string Reason;
		}

		public struct ConnectionApprovalRequest
		{
			public byte[] Payload;

			public ulong ClientNetworkId;
		}

		private enum StartType
		{
			Server = 0,
			Host = 1,
			Client = 2
		}

		public static readonly global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkManager.RpcReceiveHandler> __rpc_func_table = new global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkManager.RpcReceiveHandler>();

		public static readonly global::System.Collections.Generic.Dictionary<uint, string> __rpc_name_table = new global::System.Collections.Generic.Dictionary<uint, string>();

		internal global::Unity.Netcode.SessionConfig SessionConfig;

		internal global::Unity.Netcode.NetworkManager.OnGetSessionConfigHandler OnGetSessionConfig;

		internal static bool IsDistributedAuthority;

		public global::Unity.Netcode.NetworkManager.OnFetchLocalPlayerPrefabToSpawnDelegateHandler OnFetchLocalPlayerPrefabToSpawn;

		internal global::System.Collections.Generic.List<ulong> ClientsToRedistribute = new global::System.Collections.Generic.List<ulong>();

		internal bool RedistributeToClients;

		internal global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> DeferredDespawnObjects = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>();

		internal global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject> NetworkTransformUpdate = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject>();

		internal global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject> NetworkTransformFixedUpdate = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject>();

		internal global::Unity.Netcode.NetworkManager.ServerShutdownStates ServerShutdownState;

		private float m_ShutdownTimeout;

		public const ulong ServerClientId = 0uL;

		public readonly global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.PendingClient> PendingClients = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.PendingClient>();

		private bool m_ShuttingDown;

		[global::UnityEngine.HideInInspector]
		public global::Unity.Netcode.NetworkConfig NetworkConfig;

		[global::UnityEngine.HideInInspector]
		public bool RunInBackground = true;

		[global::UnityEngine.HideInInspector]
		public global::Unity.Netcode.LogLevel LogLevel = global::Unity.Netcode.LogLevel.Normal;

		private global::Unity.Netcode.NetworkPrefabHandler m_PrefabHandler;

		public global::Unity.Netcode.RpcTarget RpcTarget;

		internal global::Unity.Netcode.NetworkMetricsManager MetricsManager = new global::Unity.Netcode.NetworkMetricsManager();

		internal global::Unity.Netcode.NetworkConnectionManager ConnectionManager = new global::Unity.Netcode.NetworkConnectionManager();

		internal global::Unity.Netcode.NetworkMessageManager MessageManager;

		public bool DistributedAuthorityMode { get; private set; }

		public bool CMBServiceConnection => NetworkConfig.UseCMBService;

		public bool AutoSpawnPlayerPrefabClientSide => NetworkConfig.AutoSpawnPlayerPrefabClientSide;

		public bool DAHost => LocalClient.DAHost;

		public ulong CurrentSessionOwner { get; internal set; }

		public ulong LocalClientId
		{
			get
			{
				return ConnectionManager.LocalClient.ClientId;
			}
			internal set
			{
				ConnectionManager.LocalClient.ClientId = value;
			}
		}

		public global::System.Collections.Generic.IReadOnlyDictionary<ulong, global::Unity.Netcode.NetworkClient> ConnectedClients => ConnectionManager.ConnectedClients;

		public global::System.Collections.Generic.IReadOnlyList<global::Unity.Netcode.NetworkClient> ConnectedClientsList => ConnectionManager.ConnectedClientsList;

		public global::System.Collections.Generic.IReadOnlyList<ulong> ConnectedClientsIds => ConnectionManager.ConnectedClientIds;

		public global::Unity.Netcode.NetworkClient LocalClient => ConnectionManager.LocalClient;

		public bool IsServer => ConnectionManager.LocalClient.IsServer;

		public bool ServerIsHost => ConnectionManager.ConnectedClientIds.Contains(0uL);

		public bool IsClient => ConnectionManager.LocalClient.IsClient;

		public bool IsHost => ConnectionManager.LocalClient.IsHost;

		public string DisconnectReason => ConnectionManager.DisconnectReason;

		public global::Unity.Netcode.NetworkTransport.DisconnectEvents DisconnectEvent => ConnectionManager.DisconnectEvent;

		public bool IsListening
		{
			get
			{
				return ConnectionManager.IsListening;
			}
			internal set
			{
				ConnectionManager.IsListening = value;
			}
		}

		public bool IsConnectedClient
		{
			get
			{
				return ConnectionManager.LocalClient.IsConnected;
			}
			internal set
			{
				ConnectionManager.LocalClient.IsConnected = value;
			}
		}

		public bool IsApproved
		{
			get
			{
				return ConnectionManager.LocalClient.IsApproved;
			}
			internal set
			{
				ConnectionManager.LocalClient.IsApproved = value;
			}
		}

		public global::System.Action<global::Unity.Netcode.NetworkManager.ConnectionApprovalRequest, global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse> ConnectionApprovalCallback
		{
			get
			{
				return ConnectionManager.ConnectionApprovalCallback;
			}
			set
			{
				if (value != null && value.GetInvocationList().Length > 1)
				{
					throw new global::System.InvalidOperationException("Only one ConnectionApprovalCallback can be registered at a time.");
				}
				ConnectionManager.ConnectionApprovalCallback = value;
			}
		}

		public string ConnectedHostname => string.Empty;

		public bool ShutdownInProgress => m_ShuttingDown;

		public global::Unity.Netcode.NetworkTime LocalTime => NetworkTickSystem?.LocalTime ?? default(global::Unity.Netcode.NetworkTime);

		public global::Unity.Netcode.NetworkTime ServerTime => NetworkTickSystem?.ServerTime ?? default(global::Unity.Netcode.NetworkTime);

		public static global::Unity.Netcode.NetworkManager Singleton { get; private set; }

		public global::Unity.Netcode.NetworkPrefabHandler PrefabHandler
		{
			get
			{
				if (m_PrefabHandler == null)
				{
					m_PrefabHandler = new global::Unity.Netcode.NetworkPrefabHandler();
					m_PrefabHandler.Initialize(this);
				}
				return m_PrefabHandler;
			}
		}

		public global::Unity.Netcode.NetworkSpawnManager SpawnManager { get; private set; }

		internal global::Unity.Netcode.IDeferredNetworkMessageManager DeferredMessageManager { get; private set; }

		public global::Unity.Netcode.CustomMessagingManager CustomMessagingManager { get; private set; }

		public global::Unity.Netcode.NetworkSceneManager SceneManager { get; private set; }

		internal global::Unity.Netcode.NetworkBehaviourUpdater BehaviourUpdater { get; set; }

		public global::Unity.Netcode.NetworkTimeSystem NetworkTimeSystem { get; private set; }

		public global::Unity.Netcode.NetworkTickSystem NetworkTickSystem { get; private set; }

		internal global::Unity.Netcode.AnticipationSystem AnticipationSystem { get; private set; }

		internal global::Unity.Netcode.IRealTimeProvider RealTimeProvider { get; private set; }

		internal global::Unity.Netcode.INetworkMetrics NetworkMetrics => MetricsManager.NetworkMetrics;

		public int MaximumTransmissionUnitSize
		{
			get
			{
				return MessageManager.NonFragmentedMessageMaxSize;
			}
			set
			{
				MessageManager.NonFragmentedMessageMaxSize = value & -8;
			}
		}

		public int MaximumFragmentedMessageSize
		{
			get
			{
				return MessageManager.FragmentedMessageMaxSize;
			}
			set
			{
				MessageManager.FragmentedMessageMaxSize = value;
			}
		}

		public static event global::System.Action<global::Unity.Netcode.NetworkManager> OnInstantiated;

		public static event global::System.Action<global::Unity.Netcode.NetworkManager> OnDestroying;

		public event global::Unity.Netcode.NetworkManager.OnSessionOwnerPromotedDelegateHandler OnSessionOwnerPromoted;

		public event global::System.Action OnTransportFailure
		{
			add
			{
				ConnectionManager.OnTransportFailure += value;
			}
			remove
			{
				ConnectionManager.OnTransportFailure -= value;
			}
		}

		public event global::Unity.Netcode.NetworkManager.ReanticipateDelegate OnReanticipate
		{
			add
			{
				AnticipationSystem.OnReanticipate += value;
			}
			remove
			{
				AnticipationSystem.OnReanticipate -= value;
			}
		}

		public event global::System.Action<ulong> OnClientConnectedCallback
		{
			add
			{
				ConnectionManager.OnClientConnectedCallback += value;
			}
			remove
			{
				ConnectionManager.OnClientConnectedCallback -= value;
			}
		}

		public event global::System.Action<ulong> OnClientDisconnectCallback
		{
			add
			{
				ConnectionManager.OnClientDisconnectCallback += value;
			}
			remove
			{
				ConnectionManager.OnClientDisconnectCallback -= value;
			}
		}

		public event global::System.Action<global::Unity.Netcode.NetworkManager, global::Unity.Netcode.ConnectionEventData> OnConnectionEvent
		{
			add
			{
				ConnectionManager.OnConnectionEvent += value;
			}
			remove
			{
				ConnectionManager.OnConnectionEvent -= value;
			}
		}

		internal static event global::System.Action OnSingletonReady;

		public event global::System.Action OnServerStarted;

		public event global::System.Action OnClientStarted;

		public event global::System.Action OnPreShutdown;

		public event global::System.Action<bool> OnServerStopped;

		public event global::System.Action<bool> OnClientStopped;

		private global::Unity.Netcode.SessionConfig GetSessionConfig()
		{
			if (OnGetSessionConfig == null)
			{
				return new global::Unity.Netcode.SessionConfig();
			}
			return OnGetSessionConfig();
		}

		internal global::UnityEngine.GameObject FetchLocalPlayerPrefabToSpawn()
		{
			if (!AutoSpawnPlayerPrefabClientSide)
			{
				global::UnityEngine.Debug.LogError("[FetchLocalPlayerPrefabToSpawn] Invoked when AutoSpawnPlayerPrefabClientSide was not set! Check call paths!");
				return null;
			}
			if (OnFetchLocalPlayerPrefabToSpawn == null && NetworkConfig.PlayerPrefab == null)
			{
				return null;
			}
			if (OnFetchLocalPlayerPrefabToSpawn != null)
			{
				return OnFetchLocalPlayerPrefabToSpawn();
			}
			return NetworkConfig.PlayerPrefab;
		}

		internal void HandleRedistributionToClients()
		{
			foreach (ulong item in ClientsToRedistribute)
			{
				SpawnManager.DistributeNetworkObjects(item);
			}
			RedistributeToClients = false;
			ClientsToRedistribute.Clear();
		}

		internal void SetSessionOwner(ulong sessionOwner)
		{
			CurrentSessionOwner = sessionOwner;
			bool flag = LocalClientId == sessionOwner;
			LocalClient.IsSessionOwner = flag;
			foreach (global::Unity.Netcode.NetworkObject value in SpawnManager.SpawnedObjects.Values)
			{
				if (flag && value.IsOwnershipSessionOwner && value.OwnerClientId != LocalClientId)
				{
					SpawnManager.ChangeOwnership(value, LocalClientId, isAuthorized: true);
				}
				value.InvokeSessionOwnerPromoted(flag);
			}
			this.OnSessionOwnerPromoted?.Invoke(sessionOwner);
		}

		internal void PromoteSessionOwner(ulong clientId)
		{
			if (!DistributedAuthorityMode)
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer("[SceneManagement][NotDA] Invoking promote session owner while not in distributed authority mode!");
				return;
			}
			if (!DAHost)
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer("[SceneManagement][NotDAHost] Client is attempting to promote another client as the session owner!");
				return;
			}
			SetSessionOwner(clientId);
			global::Unity.Netcode.SessionOwnerMessage message = new global::Unity.Netcode.SessionOwnerMessage
			{
				SessionOwner = clientId
			};
			global::Unity.Netcode.NetworkDelivery defaultDelivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.SessionOwnerMessage>.DefaultDelivery;
			if (CMBServiceConnection)
			{
				ConnectionManager.SendMessage(ref message, defaultDelivery, 0uL);
				return;
			}
			ulong[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(ConnectionManager.ConnectedClientIds, (ulong c) => c != LocalClientId));
			foreach (ulong clientId2 in array)
			{
				ConnectionManager.SendMessage(ref message, defaultDelivery, clientId2);
			}
		}

		internal void NetworkTransformRegistration(global::Unity.Netcode.NetworkObject networkObject, bool onUpdate = true, bool register = true)
		{
			if (onUpdate)
			{
				if (register)
				{
					if (!NetworkTransformUpdate.ContainsKey(networkObject.NetworkObjectId))
					{
						NetworkTransformUpdate.Add(networkObject.NetworkObjectId, networkObject);
					}
				}
				else
				{
					NetworkTransformUpdate.Remove(networkObject.NetworkObjectId);
				}
			}
			else if (register)
			{
				if (!NetworkTransformFixedUpdate.ContainsKey(networkObject.NetworkObjectId))
				{
					NetworkTransformFixedUpdate.Add(networkObject.NetworkObjectId, networkObject);
				}
			}
			else
			{
				NetworkTransformFixedUpdate.Remove(networkObject.NetworkObjectId);
			}
		}

		private void UpdateTopology()
		{
			global::Unity.Netcode.NetworkTopologyTypes networkTopologyTypes = ((IsListening && IsConnectedClient) ? NetworkConfig.NetworkTransport.CurrentTopology() : NetworkConfig.NetworkTopology);
			if (networkTopologyTypes != NetworkConfig.NetworkTopology)
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer($"[Topology Mismatch][{networkTopologyTypes}:{networkTopologyTypes.GetType().Name}][NetworkManager.NetworkConfig:{NetworkConfig.NetworkTopology}] Transport detected an issue with the topology usage or setting! Disconnecting from session.");
				Shutdown(discardMessageQueue: true);
			}
			else
			{
				bool isDistributedAuthority = (DistributedAuthorityMode = networkTopologyTypes == global::Unity.Netcode.NetworkTopologyTypes.DistributedAuthority);
				IsDistributedAuthority = isDistributedAuthority;
			}
		}

		public void NetworkUpdate(global::Unity.Netcode.NetworkUpdateStage updateStage)
		{
			switch (updateStage)
			{
			case global::Unity.Netcode.NetworkUpdateStage.EarlyUpdate:
				UpdateTopology();
				NetworkConfig.NetworkTransport.EarlyUpdate();
				ConnectionManager.ProcessPendingApprovals();
				ConnectionManager.PollAndHandleNetworkEvents();
				DeferredMessageManager.ProcessTriggers(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnNextFrame, 0uL);
				AnticipationSystem.SetupForUpdate();
				MessageManager.ProcessIncomingMessageQueue();
				AnticipationSystem.ProcessReanticipation();
				{
					foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.NetworkObject> item in NetworkTransformFixedUpdate)
					{
						if (!item.Value.gameObject.activeInHierarchy || !item.Value.IsSpawned)
						{
							continue;
						}
						foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform in item.Value.NetworkTransforms)
						{
							if (networkTransform.enabled)
							{
								networkTransform.ResetFixedTimeDelta();
							}
						}
					}
					break;
				}
			case global::Unity.Netcode.NetworkUpdateStage.FixedUpdate:
			{
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.NetworkObject> item2 in NetworkTransformFixedUpdate)
				{
					if (!item2.Value.gameObject.activeInHierarchy || !item2.Value.IsSpawned)
					{
						continue;
					}
					foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform2 in item2.Value.NetworkTransforms)
					{
						if (networkTransform2.enabled)
						{
							networkTransform2.OnFixedUpdate();
						}
					}
				}
				break;
			}
			case global::Unity.Netcode.NetworkUpdateStage.PreUpdate:
			{
				int tick = ServerTime.Tick;
				NetworkTimeSystem.UpdateTime();
				if (ServerTime.Tick != tick)
				{
					global::Unity.Netcode.Components.NetworkTransform.CurrentTick = ((ServerTime.Tick - tick > 1) ? (tick + 1) : ServerTime.Tick);
					global::Unity.Netcode.Components.NetworkTransform.UpdateNetworkTick(this);
				}
				AnticipationSystem.Update();
				break;
			}
			case global::Unity.Netcode.NetworkUpdateStage.PreLateUpdate:
			{
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.NetworkObject> item3 in NetworkTransformUpdate)
				{
					if (!item3.Value.gameObject.activeInHierarchy || !item3.Value.IsSpawned)
					{
						continue;
					}
					foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform3 in item3.Value.NetworkTransforms)
					{
						if (networkTransform3.enabled)
						{
							networkTransform3.OnUpdate();
						}
					}
				}
				break;
			}
			case global::Unity.Netcode.NetworkUpdateStage.PostScriptLateUpdate:
				AnticipationSystem.Sync();
				AnticipationSystem.SetupForRender();
				break;
			case global::Unity.Netcode.NetworkUpdateStage.PostLateUpdate:
				if (DistributedAuthorityMode)
				{
					SpawnManager.DeferredDespawnUpdate(ServerTime);
				}
				SpawnManager.HandleNetworkObjectShow(forceSend: true);
				if (RedistributeToClients)
				{
					HandleRedistributionToClients();
				}
				SpawnManager.UpdateNetworkObjectSceneChanges();
				SceneManager.CheckForAndSendNetworkObjectSceneChanged();
				MessageManager.ProcessSendQueues();
				MetricsManager.UpdateMetrics();
				NetworkConfig.NetworkTransport.PostLateUpdate();
				global::Unity.Netcode.NetworkObject.VerifyParentingStatus();
				DeferredMessageManager.CleanupStaleTriggers();
				if (IsServer)
				{
					ConnectionManager.ProcessClientsToDisconnect();
				}
				MessageManager.CleanupDisconnectedClients();
				if (m_ShuttingDown)
				{
					if (IsServer)
					{
						ProcessServerShutdown();
					}
					else
					{
						ShutdownInternal();
					}
				}
				break;
			case global::Unity.Netcode.NetworkUpdateStage.Update:
				break;
			}
		}

		internal void ProcessServerShutdown()
		{
			int num = ((!IsHost) ? 1 : 2);
			switch (ServerShutdownState)
			{
			case global::Unity.Netcode.NetworkManager.ServerShutdownStates.None:
				if (ConnectedClients.Count >= num)
				{
					string text = (IsHost ? "host" : "server");
					string reason = "Disconnected due to " + text + " shutting down.";
					for (int num2 = ConnectedClientsIds.Count - 1; num2 >= 0; num2--)
					{
						ulong num3 = ConnectedClientsIds[num2];
						if (num3 != 0L)
						{
							ConnectionManager.DisconnectClient(num3, reason);
						}
					}
					ServerShutdownState = global::Unity.Netcode.NetworkManager.ServerShutdownStates.WaitForClientDisconnects;
					m_ShutdownTimeout = global::UnityEngine.Time.realtimeSinceStartup + 5f;
				}
				else
				{
					ServerShutdownState = global::Unity.Netcode.NetworkManager.ServerShutdownStates.InternalShutdown;
					ProcessServerShutdown();
				}
				break;
			case global::Unity.Netcode.NetworkManager.ServerShutdownStates.WaitForClientDisconnects:
				if (ConnectedClients.Count < num || m_ShutdownTimeout < global::UnityEngine.Time.realtimeSinceStartup)
				{
					ServerShutdownState = global::Unity.Netcode.NetworkManager.ServerShutdownStates.InternalShutdown;
					ProcessServerShutdown();
				}
				break;
			case global::Unity.Netcode.NetworkManager.ServerShutdownStates.InternalShutdown:
				ServerShutdownState = global::Unity.Netcode.NetworkManager.ServerShutdownStates.ShuttingDown;
				ShutdownInternal();
				break;
			}
		}

		internal bool NetworkManagerCheckForParent(bool ignoreNetworkManagerCache = false)
		{
			bool num = base.transform.root != base.transform;
			if (num)
			{
				throw new global::System.Exception(GenerateNestedNetworkManagerMessage(base.transform));
			}
			return num;
		}

		internal static string GenerateNestedNetworkManagerMessage(global::UnityEngine.Transform transform)
		{
			return transform.name + " is nested under " + transform.root.name + ". NetworkManager cannot be nested.\n";
		}

		private void OnTransformParentChanged()
		{
			NetworkManagerCheckForParent();
		}

		public void SetSingleton()
		{
			Singleton = this;
			global::Unity.Netcode.NetworkManager.OnSingletonReady?.Invoke();
		}

		private void Awake()
		{
			NetworkConfig?.InitializePrefabs();
			global::UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloaded;
			global::Unity.Netcode.NetworkManager.OnInstantiated?.Invoke(this);
		}

		private void OnEnable()
		{
			if (RunInBackground)
			{
				global::UnityEngine.Application.runInBackground = true;
			}
			if (Singleton == null)
			{
				SetSingleton();
			}
			if (!NetworkManagerCheckForParent())
			{
				global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		public global::UnityEngine.GameObject GetNetworkPrefabOverride(global::UnityEngine.GameObject gameObject)
		{
			return PrefabHandler.GetNetworkPrefabOverride(gameObject);
		}

		public void AddNetworkPrefab(global::UnityEngine.GameObject prefab)
		{
			PrefabHandler.AddNetworkPrefab(prefab);
		}

		public void RemoveNetworkPrefab(global::UnityEngine.GameObject prefab)
		{
			PrefabHandler.RemoveNetworkPrefab(prefab);
		}

		public void SetPeerMTU(ulong clientId, int size)
		{
			MessageManager.PeerMTUSizes[clientId] = size;
		}

		public int GetPeerMTU(ulong clientId)
		{
			if (MessageManager.PeerMTUSizes.TryGetValue(clientId, out var value))
			{
				return value;
			}
			return MessageManager.NonFragmentedMessageMaxSize;
		}

		internal void Initialize(bool server)
		{
			NetworkTransformFixedUpdate.Clear();
			NetworkTransformUpdate.Clear();
			UpdateTopology();
			if (DistributedAuthorityMode)
			{
				SessionConfig = GetSessionConfig();
			}
			if (server)
			{
				ServerShutdownState = global::Unity.Netcode.NetworkManager.ServerShutdownStates.None;
			}
			if (NetworkManagerCheckForParent(ignoreNetworkManagerCache: true))
			{
				return;
			}
			if (NetworkConfig.NetworkTransport == null)
			{
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogError("No transport has been selected!");
				}
				return;
			}
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogInfo("Initialize");
			}
			this.RegisterNetworkUpdate(global::Unity.Netcode.NetworkUpdateStage.EarlyUpdate);
			this.RegisterNetworkUpdate(global::Unity.Netcode.NetworkUpdateStage.FixedUpdate);
			this.RegisterNetworkUpdate(global::Unity.Netcode.NetworkUpdateStage.PreUpdate);
			this.RegisterNetworkUpdate(global::Unity.Netcode.NetworkUpdateStage.PostScriptLateUpdate);
			this.RegisterNetworkUpdate(global::Unity.Netcode.NetworkUpdateStage.PreLateUpdate);
			this.RegisterNetworkUpdate(global::Unity.Netcode.NetworkUpdateStage.PostLateUpdate);
			global::Unity.Netcode.ComponentFactory.SetDefaults();
			RealTimeProvider = global::Unity.Netcode.ComponentFactory.Create<global::Unity.Netcode.IRealTimeProvider>(this);
			MetricsManager.Initialize(this);
			MessageManager = new global::Unity.Netcode.NetworkMessageManager(new global::Unity.Netcode.DefaultMessageSender(this), this);
			MessageManager.Hook(new global::Unity.Netcode.NetworkManagerHooks(this));
			if (NetworkConfig.NetworkMessageMetrics)
			{
				MessageManager.Hook(new global::Unity.Netcode.MetricHooks(this));
			}
			MessageManager.ClientConnected(0uL);
			ConnectionManager.Initialize(this);
			NetworkTimeSystem = (server ? global::Unity.Netcode.NetworkTimeSystem.ServerTimeSystem() : new global::Unity.Netcode.NetworkTimeSystem(1.0 / (double)NetworkConfig.TickRate));
			NetworkTickSystem = NetworkTimeSystem.Initialize(this);
			AnticipationSystem = new global::Unity.Netcode.AnticipationSystem(this);
			SpawnManager = new global::Unity.Netcode.NetworkSpawnManager(this);
			DeferredMessageManager = global::Unity.Netcode.ComponentFactory.Create<global::Unity.Netcode.IDeferredNetworkMessageManager>(this);
			RpcTarget = new global::Unity.Netcode.RpcTarget(this);
			CustomMessagingManager = new global::Unity.Netcode.CustomMessagingManager(this);
			SceneManager = new global::Unity.Netcode.NetworkSceneManager(this);
			BehaviourUpdater = new global::Unity.Netcode.NetworkBehaviourUpdater();
			BehaviourUpdater.Initialize(this);
			NetworkConfig.InitializePrefabs();
			PrefabHandler.RegisterPlayerPrefab();
		}

		private bool CanStart(global::Unity.Netcode.NetworkManager.StartType type)
		{
			if (IsListening)
			{
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("Cannot start " + type.ToString() + " while an instance is already running");
				}
				return false;
			}
			if (NetworkConfig.ConnectionApproval && type != global::Unity.Netcode.NetworkManager.StartType.Client && ConnectionApprovalCallback == null && global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
			{
				global::Unity.Netcode.NetworkLog.LogWarning("No ConnectionApproval callback defined. Connection approval will timeout");
			}
			if (ConnectionApprovalCallback != null && !NetworkConfig.ConnectionApproval && global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
			{
				global::Unity.Netcode.NetworkLog.LogWarning("A ConnectionApproval callback is defined but ConnectionApproval is disabled. In order to use ConnectionApproval it has to be explicitly enabled ");
			}
			return true;
		}

		public bool StartServer()
		{
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogInfo("StartServer");
			}
			if (!CanStart(global::Unity.Netcode.NetworkManager.StartType.Server))
			{
				return false;
			}
			if (!ConnectionManager.LocalClient.SetRole(isServer: true, isClient: false, this))
			{
				return false;
			}
			ConnectionManager.LocalClient.ClientId = 0uL;
			Initialize(server: true);
			try
			{
				IsListening = NetworkConfig.NetworkTransport.StartServer();
				if (IsListening)
				{
					SpawnManager.ServerSpawnSceneObjectsOnStartSweep();
					SpawnManager.NotifyNetworkObjectsSynchronized();
					this.OnServerStarted?.Invoke();
					ConnectionManager.LocalClient.IsApproved = true;
					return true;
				}
				ConnectionManager.TransportFailureEventHandler(duringStart: true);
			}
			catch (global::System.Exception)
			{
				ConnectionManager.LocalClient.SetRole(isServer: false, isClient: false);
				IsListening = false;
				throw;
			}
			return IsListening;
		}

		public bool StartClient()
		{
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogInfo("StartClient");
			}
			if (!CanStart(global::Unity.Netcode.NetworkManager.StartType.Client))
			{
				return false;
			}
			if (!ConnectionManager.LocalClient.SetRole(isServer: false, isClient: true, this))
			{
				return false;
			}
			Initialize(server: false);
			try
			{
				IsListening = NetworkConfig.NetworkTransport.StartClient();
				if (!IsListening)
				{
					ConnectionManager.TransportFailureEventHandler(duringStart: true);
				}
				else
				{
					this.OnClientStarted?.Invoke();
				}
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
				ConnectionManager.LocalClient.SetRole(isServer: false, isClient: false);
				IsListening = false;
			}
			return IsListening;
		}

		public bool StartHost()
		{
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogInfo("StartHost");
			}
			if (!CanStart(global::Unity.Netcode.NetworkManager.StartType.Host))
			{
				return false;
			}
			if (!ConnectionManager.LocalClient.SetRole(isServer: true, isClient: true, this))
			{
				return false;
			}
			Initialize(server: true);
			try
			{
				IsListening = NetworkConfig.NetworkTransport.StartServer();
				if (!IsListening)
				{
					ConnectionManager.TransportFailureEventHandler(duringStart: true);
				}
				else
				{
					HostServerInitialize();
				}
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
				ConnectionManager.LocalClient.SetRole(isServer: false, isClient: false);
				IsListening = false;
			}
			return IsListening;
		}

		private void HostServerInitialize()
		{
			LocalClientId = 0uL;
			NetworkMetrics.SetConnectionId(LocalClientId);
			MessageManager.SetLocalClientId(LocalClientId);
			if (NetworkConfig.ConnectionApproval && ConnectionApprovalCallback != null)
			{
				global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse connectionApprovalResponse = new global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse();
				ConnectionApprovalCallback(new global::Unity.Netcode.NetworkManager.ConnectionApprovalRequest
				{
					Payload = NetworkConfig.ConnectionData,
					ClientNetworkId = 0uL
				}, connectionApprovalResponse);
				if (!connectionApprovalResponse.Approved && global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("You cannot decline the host connection. The connection was automatically approved.");
				}
				ConnectionManager.HandleConnectionApproval(0uL, connectionApprovalResponse.CreatePlayerObject, connectionApprovalResponse.PlayerPrefabHash, connectionApprovalResponse.Position, connectionApprovalResponse.Rotation);
			}
			else
			{
				bool createPlayerObject = DistributedAuthorityMode || NetworkConfig.PlayerPrefab != null;
				ConnectionManager.HandleConnectionApproval(0uL, createPlayerObject);
			}
			SpawnManager.ServerSpawnSceneObjectsOnStartSweep();
			SpawnManager.NotifyNetworkObjectsSynchronized();
			this.OnServerStarted?.Invoke();
			this.OnClientStarted?.Invoke();
			ConnectionManager.InvokeOnClientConnectedCallback(LocalClientId);
		}

		public ulong GetTransportIdFromClientId(ulong clientId)
		{
			(ulong, bool) tuple = ConnectionManager.ClientIdToTransportId(clientId);
			var (result, _) = tuple;
			if (!tuple.Item2)
			{
				return ulong.MaxValue;
			}
			return result;
		}

		public ulong GetClientIdFromTransportId(ulong transportId)
		{
			(ulong, bool) tuple = ConnectionManager.TransportIdToClientId(transportId);
			var (result, _) = tuple;
			if (!tuple.Item2)
			{
				return ulong.MaxValue;
			}
			return result;
		}

		public void DisconnectClient(ulong clientId)
		{
			ConnectionManager.DisconnectClient(clientId, $"Client-{clientId} disconnected by server.");
		}

		public void DisconnectClient(ulong clientId, string reason = null)
		{
			ConnectionManager.DisconnectClient(clientId, reason);
		}

		public void Shutdown(bool discardMessageQueue = false)
		{
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogInfo("Shutdown");
			}
			if (IsServer || IsClient)
			{
				m_ShuttingDown = true;
				if (MessageManager != null)
				{
					MessageManager.StopProcessing = discardMessageQueue;
				}
			}
		}

		private void OnSceneUnloaded(global::UnityEngine.SceneManagement.Scene scene)
		{
			if (base.gameObject != null && scene == base.gameObject.scene)
			{
				OnDestroy();
			}
		}

		internal void ShutdownInternal()
		{
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogInfo("ShutdownInternal");
			}
			try
			{
				this.OnPreShutdown?.Invoke();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			this.UnregisterAllNetworkUpdates();
			DeferredMessageManager?.CleanupAllTriggers();
			CustomMessagingManager = null;
			BehaviourUpdater?.Shutdown();
			BehaviourUpdater = null;
			SpawnManager?.DespawnAndDestroyNetworkObjects();
			SpawnManager?.ServerResetShudownStateForSceneObjects();
			RpcTarget?.Dispose();
			RpcTarget = null;
			ConnectionManager.Shutdown();
			MessageManager?.Dispose();
			MessageManager = null;
			SceneManager?.Dispose();
			SceneManager = null;
			SpawnManager = null;
			IsListening = false;
			m_ShuttingDown = false;
			if (IsHost)
			{
				ConnectionManager.InvokeOnClientDisconnectCallback(LocalClientId);
			}
			if (ConnectionManager.LocalClient.IsClient)
			{
				this.OnClientStopped?.Invoke(ConnectionManager.LocalClient.IsServer);
			}
			if (ConnectionManager.LocalClient.IsServer)
			{
				this.OnServerStopped?.Invoke(ConnectionManager.LocalClient.IsClient);
			}
			m_ShuttingDown = false;
			ConnectionManager.LocalClient = new global::Unity.Netcode.NetworkClient();
			NetworkConfig?.Prefabs?.Shutdown();
			PrefabHandler.Shutdown();
			NetworkConfig?.ClearConfigHash();
			NetworkTimeSystem?.Shutdown();
			NetworkTickSystem = null;
		}

		private void OnApplicationQuit()
		{
			this.UnregisterAllNetworkUpdates();
			m_ShuttingDown = true;
			if (!(Singleton == null) || IsListening)
			{
				OnDestroy();
			}
		}

		private void OnDestroy()
		{
			try
			{
				ShutdownInternal();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			global::UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= OnSceneUnloaded;
			try
			{
				global::Unity.Netcode.NetworkManager.OnDestroying?.Invoke(this);
			}
			catch (global::System.Exception exception2)
			{
				global::UnityEngine.Debug.LogException(exception2);
			}
			if (Singleton == this)
			{
				Singleton = null;
			}
		}
	}
}
