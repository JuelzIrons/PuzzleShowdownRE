namespace Unity.Netcode
{
	public sealed class NetworkConnectionManager
	{
		private string m_DisconnectReason;

		internal string ServerDisconnectReason;

		internal global::Unity.Netcode.NetworkManager NetworkManager;

		internal global::Unity.Netcode.NetworkMessageManager MessageManager;

		internal global::Unity.Netcode.NetworkClient LocalClient = new global::Unity.Netcode.NetworkClient();

		internal global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse> ClientsToApprove = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse>();

		internal global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkClient> ConnectedClients = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkClient>();

		internal global::System.Collections.Generic.Dictionary<ulong, ulong> ClientIdToTransportIdMap = new global::System.Collections.Generic.Dictionary<ulong, ulong>();

		internal global::System.Collections.Generic.Dictionary<ulong, ulong> TransportIdToClientIdMap = new global::System.Collections.Generic.Dictionary<ulong, ulong>();

		internal global::System.Collections.Generic.List<global::Unity.Netcode.NetworkClient> ConnectedClientsList = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkClient>();

		internal global::System.Collections.Generic.List<ulong> ConnectedClientIds = new global::System.Collections.Generic.List<ulong>();

		internal global::System.Action<global::Unity.Netcode.NetworkManager.ConnectionApprovalRequest, global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse> ConnectionApprovalCallback;

		private global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.PendingClient> m_PendingClients = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.PendingClient>();

		internal global::UnityEngine.Coroutine LocalClientApprovalCoroutine;

		private ulong m_NextClientId = 1uL;

		private ulong m_LocalClientTransportId;

		private bool m_IsTransportConnected;

		private global::System.Collections.Generic.List<ulong> m_ClientsToDisconnect = new global::System.Collections.Generic.List<ulong>();

		internal bool EnableDistributeLogging;

		internal global::Unity.Netcode.NetworkTransport Transport;

		public string DisconnectReason => GetDisconnectReason();

		public bool IsListening { get; internal set; }

		internal global::System.Collections.Generic.IReadOnlyDictionary<ulong, global::Unity.Netcode.PendingClient> PendingClients => m_PendingClients;

		internal ulong ServerTransportId => GetServerTransportId();

		internal ulong LocalClientTransportId => m_LocalClientTransportId;

		internal global::Unity.Netcode.NetworkTransport.DisconnectEvents DisconnectEvent
		{
			get
			{
				if (!Transport)
				{
					return global::Unity.Netcode.NetworkTransport.DisconnectEvents.Disconnected;
				}
				return Transport.DisconnectEvent;
			}
		}

		public event global::System.Action<ulong> OnClientConnectedCallback;

		public event global::System.Action<ulong> OnClientDisconnectCallback;

		public event global::System.Action<global::Unity.Netcode.NetworkManager, global::Unity.Netcode.ConnectionEventData> OnConnectionEvent;

		public event global::System.Action OnTransportFailure;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal string GetDisconnectReason()
		{
			if (!string.IsNullOrEmpty(ServerDisconnectReason))
			{
				return ServerDisconnectReason;
			}
			return m_DisconnectReason;
		}

		internal void InvokeOnClientConnectedCallback(ulong clientId)
		{
			try
			{
				this.OnClientConnectedCallback?.Invoke(clientId);
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			if (NetworkManager.IsServer || NetworkManager.LocalClient.IsSessionOwner)
			{
				try
				{
					this.OnConnectionEvent?.Invoke(NetworkManager, new global::Unity.Netcode.ConnectionEventData
					{
						ClientId = clientId,
						EventType = global::Unity.Netcode.ConnectionEvent.ClientConnected
					});
					return;
				}
				catch (global::System.Exception exception2)
				{
					global::UnityEngine.Debug.LogException(exception2);
					return;
				}
			}
			global::Unity.Collections.NativeArray<ulong> nativeArray = new global::Unity.Collections.NativeArray<ulong>(global::System.Math.Max(ConnectedClientIds.Count - 1, 0), global::Unity.Collections.Allocator.Temp);
			using (nativeArray)
			{
				int num = 0;
				foreach (ulong connectedClientId in ConnectedClientIds)
				{
					if (connectedClientId != NetworkManager.LocalClientId && nativeArray.Length > num)
					{
						nativeArray[num] = connectedClientId;
						num++;
					}
				}
				try
				{
					this.OnConnectionEvent?.Invoke(NetworkManager, new global::Unity.Netcode.ConnectionEventData
					{
						ClientId = NetworkManager.LocalClientId,
						EventType = global::Unity.Netcode.ConnectionEvent.ClientConnected,
						PeerClientIds = nativeArray
					});
				}
				catch (global::System.Exception exception3)
				{
					global::UnityEngine.Debug.LogException(exception3);
				}
			}
		}

		internal void InvokeOnClientDisconnectCallback(ulong clientId)
		{
			try
			{
				this.OnClientDisconnectCallback?.Invoke(clientId);
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			try
			{
				this.OnConnectionEvent?.Invoke(NetworkManager, new global::Unity.Netcode.ConnectionEventData
				{
					ClientId = clientId,
					EventType = global::Unity.Netcode.ConnectionEvent.ClientDisconnected
				});
			}
			catch (global::System.Exception exception2)
			{
				global::UnityEngine.Debug.LogException(exception2);
			}
		}

		internal void InvokeOnPeerConnectedCallback(ulong clientId)
		{
			try
			{
				this.OnConnectionEvent?.Invoke(NetworkManager, new global::Unity.Netcode.ConnectionEventData
				{
					ClientId = clientId,
					EventType = global::Unity.Netcode.ConnectionEvent.PeerConnected
				});
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		internal void InvokeOnPeerDisconnectedCallback(ulong clientId)
		{
			try
			{
				this.OnConnectionEvent?.Invoke(NetworkManager, new global::Unity.Netcode.ConnectionEventData
				{
					ClientId = clientId,
					EventType = global::Unity.Netcode.ConnectionEvent.PeerDisconnected
				});
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		internal void StartClientApprovalCoroutine(ulong clientId)
		{
			LocalClientApprovalCoroutine = NetworkManager.StartCoroutine(ApprovalTimeout(clientId));
		}

		internal void StopClientApprovalCoroutine()
		{
			if (LocalClientApprovalCoroutine != null)
			{
				NetworkManager.StopCoroutine(LocalClientApprovalCoroutine);
				LocalClientApprovalCoroutine = null;
			}
		}

		internal void AddPendingClient(ulong clientId)
		{
			m_PendingClients.Add(clientId, new global::Unity.Netcode.PendingClient
			{
				ClientId = clientId,
				ConnectionState = global::Unity.Netcode.PendingClient.State.PendingConnection,
				ApprovalCoroutine = NetworkManager.StartCoroutine(ApprovalTimeout(clientId))
			});
			NetworkManager.PendingClients.Add(clientId, PendingClients[clientId]);
		}

		internal void RemovePendingClient(ulong clientId)
		{
			if (m_PendingClients.ContainsKey(clientId) && m_PendingClients[clientId].ApprovalCoroutine != null)
			{
				NetworkManager.StopCoroutine(m_PendingClients[clientId].ApprovalCoroutine);
			}
			m_PendingClients.Remove(clientId);
			NetworkManager.PendingClients.Remove(clientId);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal (ulong, bool) TransportIdToClientId(ulong transportId)
		{
			if (transportId == GetServerTransportId())
			{
				return (0uL, true);
			}
			if (TransportIdToClientIdMap.TryGetValue(transportId, out var value))
			{
				return (value, true);
			}
			return (0uL, false);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal (ulong, bool) ClientIdToTransportId(ulong clientId)
		{
			if (clientId == 0L)
			{
				return (GetServerTransportId(), true);
			}
			if (ClientIdToTransportIdMap.TryGetValue(clientId, out var value))
			{
				return (value, true);
			}
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel == global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogWarning($"Trying to get the transport client ID map for the NGO client ID ({clientId}) but did not find the map entry! Returning default transport ID value.");
			}
			return (0uL, false);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private ulong GetServerTransportId()
		{
			if (NetworkManager != null)
			{
				if (Transport == null && NetworkManager.NetworkConfig.NetworkTransport != null)
				{
					Transport = NetworkManager.NetworkConfig.NetworkTransport;
				}
				if ((bool)Transport)
				{
					return Transport.ServerClientId;
				}
				throw new global::System.NullReferenceException("The transport in the active NetworkConfig is null");
			}
			throw new global::System.Exception("There is no NetworkManager assigned to this instance!");
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal (ulong, bool) TransportIdCleanUp(ulong transportId)
		{
			if (!LocalClient.IsServer && !TransportIdToClientIdMap.ContainsKey(transportId))
			{
				return (NetworkManager.LocalClientId, true);
			}
			(ulong, bool) tuple = TransportIdToClientId(transportId);
			var (num, _) = tuple;
			if (!tuple.Item2)
			{
				return (0uL, false);
			}
			TransportIdToClientIdMap.Remove(transportId);
			ClientIdToTransportIdMap.Remove(num);
			return (num, true);
		}

		internal void PollAndHandleNetworkEvents()
		{
			global::Unity.Netcode.NetworkEvent networkEvent;
			do
			{
				networkEvent = Transport.PollEvent(out var clientId, out var payload, out var receiveTime);
				HandleNetworkEvent(networkEvent, clientId, payload, receiveTime);
			}
			while (networkEvent != global::Unity.Netcode.NetworkEvent.Disconnect && networkEvent != global::Unity.Netcode.NetworkEvent.TransportFailure && NetworkManager.IsListening && networkEvent != global::Unity.Netcode.NetworkEvent.Nothing);
		}

		internal void HandleNetworkEvent(global::Unity.Netcode.NetworkEvent networkEvent, ulong transportClientId, global::System.ArraySegment<byte> payload, float receiveTime)
		{
			switch (networkEvent)
			{
			case global::Unity.Netcode.NetworkEvent.Connect:
				ConnectEventHandler(transportClientId);
				break;
			case global::Unity.Netcode.NetworkEvent.Data:
				DataEventHandler(transportClientId, ref payload, receiveTime);
				break;
			case global::Unity.Netcode.NetworkEvent.Disconnect:
				DisconnectEventHandler(transportClientId);
				break;
			case global::Unity.Netcode.NetworkEvent.TransportFailure:
				TransportFailureEventHandler();
				break;
			}
		}

		internal void ConnectEventHandler(ulong transportId)
		{
			ulong num;
			if (LocalClient.IsServer)
			{
				if (TransportIdToClientId(transportId).Item2)
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogError($"[TransportApproval][Server] TransportId {transportId} is already connected to this server!");
					}
					return;
				}
				num = m_NextClientId++;
			}
			else
			{
				if (m_IsTransportConnected)
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogError("[TransportApproval][Client] Client received a transport connection event after already connecting!");
					}
					return;
				}
				m_IsTransportConnected = true;
				m_LocalClientTransportId = transportId;
				num = 0uL;
			}
			ClientIdToTransportIdMap[num] = transportId;
			TransportIdToClientIdMap[transportId] = num;
			MessageManager.ClientConnected(num);
			if (LocalClient.IsServer)
			{
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					string arg = (NetworkManager.IsHost ? "Host" : "Server");
					global::Unity.Netcode.NetworkLog.LogInfo($"[{arg}-Side] Transport connection established with pending Client-{num}.");
				}
				AddPendingClient(num);
				return;
			}
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				string text = ((!NetworkManager.DistributedAuthorityMode) ? "server" : (NetworkManager.CMBServiceConnection ? "service" : "DAHost"));
				global::Unity.Netcode.NetworkLog.LogInfo("[Approval Pending][Client] Transport connection with " + text + " established! Awaiting connection approval...");
			}
			SendConnectionRequest();
			StartClientApprovalCoroutine(num);
		}

		internal void DataEventHandler(ulong transportClientId, ref global::System.ArraySegment<byte> payload, float receiveTime)
		{
			(ulong, bool) tuple = TransportIdToClientId(transportClientId);
			var (clientId, _) = tuple;
			if (tuple.Item2)
			{
				MessageManager.HandleIncomingData(clientId, payload, receiveTime);
			}
		}

		private void GenerateDisconnectInformation(ulong clientId, ulong transportClientId, string reason = null)
		{
			string arg = $"[Disconnect Event][Client-{clientId}][TransportClientId-{transportClientId}]";
			string text = Transport.DisconnectEventMessage;
			if (reason != null)
			{
				text = reason + " " + text;
			}
			m_DisconnectReason = $"{arg}[{Transport.DisconnectEvent}] {text}";
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				string text2 = (string.IsNullOrEmpty(ServerDisconnectReason) ? string.Empty : ("\n" + ServerDisconnectReason));
				global::Unity.Netcode.NetworkLog.LogInfo(m_DisconnectReason + text2);
			}
		}

		internal void DisconnectEventHandler(ulong transportClientId)
		{
			(ulong, bool) tuple = TransportIdToClientId(transportClientId);
			var (num, _) = tuple;
			if (!tuple.Item2 && (NetworkManager.IsServer || (NetworkManager.CMBServiceConnection && m_LocalClientTransportId != 0L)))
			{
				return;
			}
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogInfo($"Disconnect Event From {num}");
			}
			if (!NetworkManager.IsServer && num == 0L)
			{
				num = NetworkManager.LocalClientId;
			}
			if (Transport.DisconnectEvent != global::Unity.Netcode.NetworkTransport.DisconnectEvents.TransportShutdown)
			{
				GenerateDisconnectInformation(num, transportClientId);
			}
			MessageManager.ProcessIncomingMessageQueue();
			if (LocalClient.IsServer)
			{
				OnClientDisconnectFromServer(num);
				return;
			}
			TransportIdCleanUp(transportClientId);
			try
			{
				InvokeOnClientDisconnectCallback(num);
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			m_LocalClientTransportId = 0uL;
			if (!NetworkManager.ShutdownInProgress)
			{
				NetworkManager.Shutdown(discardMessageQueue: true);
			}
		}

		internal void TransportFailureEventHandler(bool duringStart = false)
		{
			string text = ((!LocalClient.IsServer) ? "Client" : (LocalClient.IsHost ? "Host" : "Server"));
			string text2 = (duringStart ? "start failure" : "failure");
			global::Unity.Netcode.NetworkLog.LogError(text + " is shutting down due to network transport " + text2 + " of " + NetworkManager.NetworkConfig.NetworkTransport.GetType().Name + "!");
			this.OnTransportFailure?.Invoke();
			if (duringStart)
			{
				LocalClient.SetRole(isServer: false, isClient: false);
				NetworkManager.ShutdownInternal();
			}
			else
			{
				NetworkManager.Shutdown(discardMessageQueue: true);
			}
		}

		private void SendConnectionRequest()
		{
			global::Unity.Netcode.ConnectionRequestMessage message = new global::Unity.Netcode.ConnectionRequestMessage
			{
				DistributedAuthority = NetworkManager.DistributedAuthorityMode,
				ConfigHash = NetworkManager.NetworkConfig.GetConfig(cache: false),
				ShouldSendConnectionData = NetworkManager.NetworkConfig.ConnectionApproval,
				ConnectionData = NetworkManager.NetworkConfig.ConnectionData,
				MessageVersions = new global::Unity.Collections.NativeArray<global::Unity.Netcode.MessageVersionData>(MessageManager.MessageHandlers.Length, global::Unity.Collections.Allocator.Temp)
			};
			if (NetworkManager.DistributedAuthorityMode)
			{
				message.ClientConfig.SessionConfig = NetworkManager.SessionConfig;
				message.ClientConfig.TickRate = NetworkManager.NetworkConfig.TickRate;
				message.ClientConfig.EnableSceneManagement = NetworkManager.NetworkConfig.EnableSceneManagement;
			}
			for (int i = 0; i < MessageManager.MessageHandlers.Length; i++)
			{
				if (MessageManager.MessageTypes[i] != null)
				{
					global::System.Type type = MessageManager.MessageTypes[i];
					message.MessageVersions[i] = new global::Unity.Netcode.MessageVersionData
					{
						Hash = type.FullName.Hash32(),
						Version = MessageManager.GetLocalVersion(type)
					};
				}
			}
			SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ConnectionRequestMessage>.DefaultDelivery, 0uL);
			message.MessageVersions.Dispose();
		}

		private global::System.Collections.IEnumerator ApprovalTimeout(ulong clientId)
		{
			float num = (LocalClient.IsServer ? NetworkManager.LocalTime.TimeAsFloat : NetworkManager.RealTimeProvider.RealTimeSinceStartup);
			bool flag = false;
			bool flag2 = false;
			bool connectionNotApproved = false;
			float timeoutMarker = num + (float)NetworkManager.NetworkConfig.ClientConnectionBufferTimeout;
			while (NetworkManager.IsListening && !NetworkManager.ShutdownInProgress && !flag && !flag2)
			{
				yield return null;
				flag = timeoutMarker < (LocalClient.IsServer ? NetworkManager.LocalTime.TimeAsFloat : NetworkManager.RealTimeProvider.RealTimeSinceStartup);
				if (LocalClient.IsServer)
				{
					flag2 = !PendingClients.ContainsKey(clientId) && ConnectedClients.ContainsKey(clientId);
					connectionNotApproved = !PendingClients.ContainsKey(clientId) && !ConnectedClients.ContainsKey(clientId);
				}
				else
				{
					flag2 = NetworkManager.LocalClient.IsApproved;
				}
			}
			if (!NetworkManager.IsListening || NetworkManager.ShutdownInProgress || !(flag || connectionNotApproved))
			{
				yield break;
			}
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				if (flag)
				{
					if (LocalClient.IsServer)
					{
						global::Unity.Netcode.NetworkLog.LogWarning($"Server detected a transport connection from Client-{clientId}, but timed out waiting for the connection request message.");
					}
					else
					{
						global::Unity.Netcode.NetworkLog.LogInfo("Timed out waiting for the server to approve the connection request.");
					}
				}
				else if (connectionNotApproved)
				{
					global::Unity.Netcode.NetworkLog.LogInfo($"Client-{clientId} was either denied approval or disconnected while being approved.");
				}
			}
			if (LocalClient.IsServer)
			{
				DisconnectClient(clientId);
			}
			else
			{
				NetworkManager.Shutdown(discardMessageQueue: true);
			}
		}

		internal void ApproveConnection(ref global::Unity.Netcode.ConnectionRequestMessage connectionRequestMessage, ref global::Unity.Netcode.NetworkContext context)
		{
			if (ConnectionApprovalCallback != null)
			{
				global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse connectionApprovalResponse = new global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse();
				ClientsToApprove[context.SenderId] = connectionApprovalResponse;
				ConnectionApprovalCallback?.Invoke(new global::Unity.Netcode.NetworkManager.ConnectionApprovalRequest
				{
					Payload = connectionRequestMessage.ConnectionData,
					ClientNetworkId = context.SenderId
				}, connectionApprovalResponse);
			}
		}

		internal void ProcessPendingApprovals()
		{
			global::System.Collections.Generic.List<ulong> list = null;
			foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse> item in ClientsToApprove)
			{
				global::Unity.Netcode.NetworkManager.ConnectionApprovalResponse value = item.Value;
				ulong key = item.Key;
				if (value.Pending)
				{
					continue;
				}
				try
				{
					if (value.Approved)
					{
						HandleConnectionApproval(key, value.CreatePlayerObject, value.PlayerPrefabHash, value.Position, value.Rotation);
					}
					else
					{
						HandleConnectionDisconnect(key, value.Reason);
					}
					if (list == null)
					{
						list = new global::System.Collections.Generic.List<ulong>();
					}
					list.Add(key);
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogException(exception);
				}
			}
			if (list == null)
			{
				return;
			}
			foreach (ulong item2 in list)
			{
				ClientsToApprove.Remove(item2);
			}
		}

		private void HandleConnectionDisconnect(ulong ownerClientId, string reason = "")
		{
			if (!string.IsNullOrEmpty(reason))
			{
				global::Unity.Netcode.DisconnectReasonMessage message = new global::Unity.Netcode.DisconnectReasonMessage
				{
					Reason = reason
				};
				SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.DisconnectReasonMessage>.DefaultDelivery, ownerClientId);
				m_ClientsToDisconnect.Add(ownerClientId);
			}
			else
			{
				DisconnectRemoteClient(ownerClientId);
			}
		}

		internal void ProcessClientsToDisconnect()
		{
			if (m_ClientsToDisconnect.Count == 0)
			{
				return;
			}
			foreach (ulong item in m_ClientsToDisconnect)
			{
				try
				{
					DisconnectRemoteClient(item);
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogException(exception);
				}
			}
			m_ClientsToDisconnect.Clear();
		}

		internal void HandleConnectionApproval(ulong ownerClientId, bool createPlayerObject, uint? playerPrefabHash = null, global::UnityEngine.Vector3? playerPosition = null, global::UnityEngine.Quaternion? playerRotation = null)
		{
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogInfo($"[Server-Side] Pending Client-{ownerClientId} connection approved!");
			}
			RemovePendingClient(ownerClientId);
			global::Unity.Netcode.NetworkClient networkClient = AddClient(ownerClientId);
			if (ownerClientId == 0L)
			{
				LocalClient = networkClient;
				LocalClient.IsConnected = true;
				LocalClient.IsApproved = true;
			}
			if (!NetworkManager.DistributedAuthorityMode && createPlayerObject && (playerPrefabHash.HasValue || NetworkManager.NetworkConfig.PlayerPrefab != null))
			{
				global::Unity.Netcode.NetworkObject networkObject = (playerPrefabHash.HasValue ? NetworkManager.SpawnManager.GetNetworkObjectToSpawn(playerPrefabHash.Value, ownerClientId, playerPosition, playerRotation) : NetworkManager.SpawnManager.GetNetworkObjectToSpawn(NetworkManager.NetworkConfig.PlayerPrefab.GetComponent<global::Unity.Netcode.NetworkObject>().GlobalObjectIdHash, ownerClientId, playerPosition, playerRotation));
				if (networkObject == null)
				{
					global::UnityEngine.Debug.LogError("[NetworkObject] Player prefab is null! Cannot spawn player object!");
				}
				else
				{
					NetworkManager.SpawnManager.AuthorityLocalSpawn(networkObject, NetworkManager.SpawnManager.GetNetworkObjectId(), sceneObject: false, playerObject: true, ownerClientId, destroyWithScene: false);
					networkClient.AssignPlayerObject(ref networkObject);
				}
			}
			if (ownerClientId == 0L || !NetworkManager.NetworkConfig.EnableSceneManagement)
			{
				NetworkManager.SpawnManager.UpdateObservedNetworkObjects(ownerClientId);
			}
			if (ownerClientId != 0L)
			{
				SendConnectionApprovedMessage(ownerClientId);
				if (!NetworkManager.NetworkConfig.EnableSceneManagement)
				{
					NetworkManager.ConnectedClients[ownerClientId].IsConnected = true;
					InvokeOnClientConnectedCallback(ownerClientId);
					if (LocalClient.IsHost)
					{
						InvokeOnPeerConnectedCallback(ownerClientId);
					}
					NetworkManager.SpawnManager.DistributeNetworkObjects(ownerClientId);
				}
				else if (NetworkManager.DistributedAuthorityMode && NetworkManager.LocalClient.IsSessionOwner)
				{
					NetworkManager.SceneManager.SynchronizeNetworkObjects(ownerClientId);
				}
				else if (!NetworkManager.DistributedAuthorityMode)
				{
					NetworkManager.SceneManager.SynchronizeNetworkObjects(ownerClientId);
				}
			}
			else
			{
				if (NetworkManager.DistributedAuthorityMode && NetworkManager.DAHost)
				{
					NetworkManager.SetSessionOwner(NetworkManager.LocalClientId);
					NetworkManager.SceneManager.InitializeScenesLoaded();
				}
				if (NetworkManager.DistributedAuthorityMode && NetworkManager.AutoSpawnPlayerPrefabClientSide)
				{
					CreateAndSpawnPlayer(ownerClientId);
				}
			}
			if (createPlayerObject && (playerPrefabHash.HasValue || !(NetworkManager.NetworkConfig.PlayerPrefab == null)) && !NetworkManager.DistributedAuthorityMode)
			{
				ApprovedPlayerSpawn(ownerClientId, playerPrefabHash ?? NetworkManager.NetworkConfig.PlayerPrefab.GetComponent<global::Unity.Netcode.NetworkObject>().GlobalObjectIdHash);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SendConnectionApprovedMessage(ulong approvedClientId)
		{
			global::Unity.Netcode.ConnectionApprovedMessage message = new global::Unity.Netcode.ConnectionApprovedMessage
			{
				OwnerClientId = approvedClientId,
				NetworkTick = NetworkManager.LocalTime.Tick,
				IsDistributedAuthority = NetworkManager.DistributedAuthorityMode,
				ConnectedClientIds = new global::Unity.Collections.NativeArray<ulong>(ConnectedClientIds.Count, global::Unity.Collections.Allocator.Temp)
			};
			for (int i = 0; i < ConnectedClientIds.Count; i++)
			{
				message.ConnectedClientIds[i] = ConnectedClientIds[i];
			}
			if (!NetworkManager.NetworkConfig.EnableSceneManagement && NetworkManager.SpawnManager.SpawnedObjectsList.Count != 0)
			{
				message.SpawnedObjectsList = NetworkManager.SpawnManager.SpawnedObjectsList;
			}
			message.MessageVersions = new global::Unity.Collections.NativeArray<global::Unity.Netcode.MessageVersionData>(MessageManager.MessageHandlers.Length, global::Unity.Collections.Allocator.Temp);
			for (int j = 0; j < MessageManager.MessageHandlers.Length; j++)
			{
				if (MessageManager.MessageTypes[j] != null)
				{
					global::System.Type type = MessageManager.MessageTypes[j];
					message.MessageVersions[j] = new global::Unity.Netcode.MessageVersionData
					{
						Hash = type.FullName.Hash32(),
						Version = MessageManager.GetLocalVersion(type)
					};
				}
			}
			SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ConnectionApprovedMessage>.DefaultDelivery, approvedClientId);
			message.MessageVersions.Dispose();
			message.ConnectedClientIds.Dispose();
		}

		internal void CreateAndSpawnPlayer(ulong ownerId)
		{
			if (NetworkManager.DistributedAuthorityMode && NetworkManager.AutoSpawnPlayerPrefabClientSide)
			{
				global::UnityEngine.GameObject gameObject = NetworkManager.FetchLocalPlayerPrefabToSpawn();
				if (gameObject != null)
				{
					uint globalObjectIdHash = gameObject.GetComponent<global::Unity.Netcode.NetworkObject>().GlobalObjectIdHash;
					global::Unity.Netcode.NetworkObject networkObjectToSpawn = NetworkManager.SpawnManager.GetNetworkObjectToSpawn(globalObjectIdHash, ownerId, gameObject.transform.position, gameObject.transform.rotation);
					networkObjectToSpawn.IsSceneObject = false;
					networkObjectToSpawn.NetworkManagerOwner = NetworkManager;
					networkObjectToSpawn.SpawnAsPlayerObject(ownerId, networkObjectToSpawn.DestroyWithScene);
				}
			}
		}

		internal void ApprovedPlayerSpawn(ulong clientId, uint playerPrefabHash)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.NetworkClient> connectedClient in ConnectedClients)
			{
				if (connectedClient.Key != clientId && connectedClient.Key != 0L && !(ConnectedClients[clientId].PlayerObject == null) && ConnectedClients[clientId].PlayerObject.Observers.Contains(connectedClient.Key))
				{
					global::Unity.Netcode.CreateObjectMessage message = new global::Unity.Netcode.CreateObjectMessage
					{
						ObjectInfo = ConnectedClients[clientId].PlayerObject.Serialize(connectedClient.Key),
						IncludesSerializedObject = true
					};
					message.ObjectInfo.Hash = playerPrefabHash;
					message.ObjectInfo.IsSceneObject = false;
					message.ObjectInfo.HasParent = false;
					message.ObjectInfo.IsPlayerObject = true;
					message.ObjectInfo.OwnerClientId = clientId;
					int num = SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.CreateObjectMessage>.DefaultDelivery, connectedClient.Key);
					NetworkManager.NetworkMetrics.TrackObjectSpawnSent(connectedClient.Key, ConnectedClients[clientId].PlayerObject, num);
				}
			}
		}

		internal global::Unity.Netcode.NetworkClient AddClient(ulong clientId)
		{
			if (ConnectedClients.ContainsKey(clientId) && ConnectedClientIds.Contains(clientId) && ConnectedClientsList.Contains(ConnectedClients[clientId]))
			{
				return ConnectedClients[clientId];
			}
			global::Unity.Netcode.NetworkClient networkClient = ((clientId == NetworkManager.LocalClientId) ? LocalClient : new global::Unity.Netcode.NetworkClient());
			networkClient.SetRole(clientId == 0, isClient: true, NetworkManager);
			networkClient.ClientId = clientId;
			if (!ConnectedClients.ContainsKey(clientId))
			{
				ConnectedClients.Add(clientId, networkClient);
			}
			if (!ConnectedClientsList.Contains(networkClient))
			{
				ConnectedClientsList.Add(networkClient);
			}
			global::Unity.Netcode.NetworkDelivery defaultDelivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ClientConnectedMessage>.DefaultDelivery;
			if (NetworkManager.LocalClientId != clientId)
			{
				if ((!NetworkManager.DistributedAuthorityMode && NetworkManager.IsServer) || (NetworkManager.DistributedAuthorityMode && NetworkManager.NetworkConfig.EnableSceneManagement && NetworkManager.DAHost && NetworkManager.LocalClient.IsSessionOwner))
				{
					global::Unity.Netcode.ClientConnectedMessage message = new global::Unity.Netcode.ClientConnectedMessage
					{
						ClientId = clientId
					};
					NetworkManager.MessageManager.SendMessage<global::Unity.Netcode.ClientConnectedMessage, ulong[]>(ref message, defaultDelivery, global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(ConnectedClientIds, (ulong c) => c != NetworkManager.LocalClientId)));
				}
				else if (NetworkManager.DistributedAuthorityMode && NetworkManager.NetworkConfig.EnableSceneManagement && NetworkManager.DAHost && !NetworkManager.LocalClient.IsSessionOwner)
				{
					global::Unity.Netcode.ClientConnectedMessage message2 = new global::Unity.Netcode.ClientConnectedMessage
					{
						ShouldSynchronize = true,
						ClientId = clientId
					};
					NetworkManager.MessageManager.SendMessage(ref message2, defaultDelivery, NetworkManager.CurrentSessionOwner);
				}
			}
			if (!ConnectedClientIds.Contains(clientId))
			{
				ConnectedClientIds.Add(clientId);
			}
			bool distributedAuthorityMode = NetworkManager.DistributedAuthorityMode;
			if (!distributedAuthorityMode || (distributedAuthorityMode && !NetworkManager.NetworkConfig.EnableSceneManagement))
			{
				return networkClient;
			}
			ulong currentSessionOwner = NetworkManager.CurrentSessionOwner;
			bool isSessionOwner = NetworkManager.LocalClient.IsSessionOwner;
			foreach (global::Unity.Netcode.NetworkObject spawnedObjects in NetworkManager.SpawnManager.SpawnedObjectsList)
			{
				if (spawnedObjects.SpawnWithObservers && (!(spawnedObjects.IsOwner && distributedAuthorityMode) || isSessionOwner || spawnedObjects.Observers.Contains(currentSessionOwner)))
				{
					spawnedObjects.Observers.Add(clientId);
				}
			}
			return networkClient;
		}

		internal void RemoveClient(ulong clientId)
		{
			if (ConnectedClientIds.Contains(clientId))
			{
				ConnectedClientIds.Remove(clientId);
			}
			if (ConnectedClients.ContainsKey(clientId))
			{
				ConnectedClientsList.Remove(ConnectedClients[clientId]);
			}
			ConnectedClients.Remove(clientId);
			foreach (global::Unity.Netcode.NetworkObject spawnedObjects in NetworkManager.SpawnManager.SpawnedObjectsList)
			{
				spawnedObjects.Observers.Remove(clientId);
			}
		}

		internal void OnClientDisconnectFromServer(ulong clientId)
		{
			if (!LocalClient.IsServer)
			{
				throw new global::System.Exception("[OnClientDisconnectFromServer] Was invoked by non-server instance!");
			}
			if (NetworkManager.ShutdownInProgress && clientId == 0L)
			{
				InvokeOnClientDisconnectCallback(clientId);
				if (LocalClient.IsHost)
				{
					InvokeOnPeerDisconnectedCallback(clientId);
				}
				return;
			}
			if (ConnectedClients.TryGetValue(clientId, out var value))
			{
				global::Unity.Netcode.NetworkObject playerObject = value.PlayerObject;
				if (playerObject != null)
				{
					if (!playerObject.DontDestroyWithOwner)
					{
						if (playerObject.IsSpawned)
						{
							NetworkManager.SpawnManager.DespawnObject(playerObject, destroyObject: true, authorityOverride: true);
						}
						else
						{
							global::UnityEngine.Object.Destroy(playerObject.gameObject);
						}
					}
					else if (!NetworkManager.ShutdownInProgress)
					{
						playerObject.RemoveOwnership();
					}
				}
				global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> list = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(NetworkManager.SpawnManager.SpawnedObjectsList, (global::Unity.Netcode.NetworkObject c) => c.OwnerClientId == clientId));
				int num = 0;
				int num2 = ConnectedClientsList.Count - 1;
				global::System.Collections.Generic.List<global::Unity.Netcode.NetworkClient> list2 = (NetworkManager.DistributedAuthorityMode ? global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(ConnectedClientsList, (global::Unity.Netcode.NetworkClient c) => c.ClientId != clientId)) : null);
				for (int num3 = list.Count - 1; num3 >= 0; num3--)
				{
					global::Unity.Netcode.NetworkObject networkObject = list[num3];
					if ((bool)networkObject)
					{
						if (!networkObject.DontDestroyWithOwner)
						{
							if (networkObject.IsSpawned)
							{
								NetworkManager.SpawnManager.DespawnObject(networkObject, destroyObject: true, authorityOverride: true);
							}
							else
							{
								global::UnityEngine.Object.Destroy(networkObject.gameObject);
							}
						}
						else if (!NetworkManager.ShutdownInProgress)
						{
							if (NetworkManager.DistributedAuthorityMode)
							{
								if (!networkObject.IsOwnershipSessionOwner && !networkObject.GetCachedParent())
								{
									if (networkObject.IsOwnershipLocked)
									{
										networkObject.SetOwnershipLock(lockOwnership: false);
									}
									ulong num4 = 0uL;
									for (int num5 = 0; num5 < list2.Count; num5++)
									{
										num++;
										num %= num2;
										if (networkObject.Observers.Contains(list2[num].ClientId))
										{
											num4 = list2[num].ClientId;
											break;
										}
									}
									if (EnableDistributeLogging)
									{
										global::UnityEngine.Debug.Log($"[Disconnected][Client-{clientId}][NetworkObjectId-{networkObject.NetworkObjectId} Distributed to Client-{num4}");
									}
									NetworkManager.SpawnManager.ChangeOwnership(networkObject, num4, isAuthorized: true);
									global::Unity.Netcode.NetworkObject[] componentsInChildren = networkObject.GetComponentsInChildren<global::Unity.Netcode.NetworkObject>();
									foreach (global::Unity.Netcode.NetworkObject networkObject2 in componentsInChildren)
									{
										if (networkObject2 == networkObject || !networkObject2.DontDestroyWithOwner)
										{
											continue;
										}
										if (networkObject2.IsOwnershipLocked)
										{
											networkObject2.SetOwnershipLock(lockOwnership: false);
										}
										if (networkObject2.IsOwnershipSessionOwner || (networkObject2.OwnerClientId != clientId && (networkObject2.IsOwnershipDistributable || networkObject2.IsOwnershipTransferable)))
										{
											continue;
										}
										ulong num7 = num4;
										if (!networkObject2.Observers.Contains(num7))
										{
											for (int num8 = 0; num8 < list2.Count; num8++)
											{
												num++;
												num %= num2;
												if (networkObject.Observers.Contains(list2[num].ClientId))
												{
													num7 = list2[num].ClientId;
													break;
												}
											}
										}
										NetworkManager.SpawnManager.ChangeOwnership(networkObject2, num7, isAuthorized: true);
										if (EnableDistributeLogging)
										{
											global::UnityEngine.Debug.Log($"[Disconnected][Client-{clientId}][Child of {networkObject.NetworkObjectId}][NetworkObjectId-{networkObject.NetworkObjectId} Distributed to Client-{num4}");
										}
									}
								}
							}
							else
							{
								networkObject.RemoveOwnership();
							}
						}
					}
				}
				foreach (global::Unity.Netcode.NetworkObject spawnedObjects in NetworkManager.SpawnManager.SpawnedObjectsList)
				{
					spawnedObjects.Observers.Remove(clientId);
				}
				if (ConnectedClients.ContainsKey(clientId))
				{
					ConnectedClientsList.Remove(ConnectedClients[clientId]);
					ConnectedClients.Remove(clientId);
				}
				ConnectedClientIds.Remove(clientId);
				if (MessageManager != null)
				{
					global::Unity.Netcode.ClientDisconnectedMessage message = new global::Unity.Netcode.ClientDisconnectedMessage
					{
						ClientId = clientId
					};
					foreach (ulong connectedClientId in ConnectedClientIds)
					{
						if (connectedClientId != NetworkManager.LocalClientId)
						{
							MessageManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ClientDisconnectedMessage>.DefaultDelivery, connectedClientId);
						}
					}
				}
			}
			(ulong, bool) tuple = ClientIdToTransportId(clientId);
			var (num9, _) = tuple;
			if (tuple.Item2 && TransportIdCleanUp(num9).Item2)
			{
				NetworkManager.NetworkConfig.NetworkTransport.DisconnectRemoteClient(num9);
				InvokeOnClientDisconnectCallback(clientId);
				if (LocalClient.IsHost)
				{
					InvokeOnPeerDisconnectedCallback(clientId);
				}
			}
			RemovePendingClient(clientId);
			MessageManager.ClientDisconnected(clientId);
		}

		internal void DisconnectRemoteClient(ulong clientId)
		{
			MessageManager.ProcessSendQueues();
			OnClientDisconnectFromServer(clientId);
		}

		internal void DisconnectClient(ulong clientId, string reason = null)
		{
			if (!LocalClient.IsServer)
			{
				if (NetworkManager.NetworkConfig.NetworkTopology == global::Unity.Netcode.NetworkTopologyTypes.ClientServer)
				{
					throw new global::Unity.Netcode.NotServerException("Only server can disconnect remote clients. Please use `Shutdown()` instead.");
				}
				global::UnityEngine.Debug.LogWarning("Currently, clients cannot disconnect other clients from a distributed authority session. Please use `Shutdown()` instead.");
				return;
			}
			if (clientId == 0L)
			{
				global::UnityEngine.Debug.LogWarning("Disconnecting the local server-host client is not allowed. Use NetworkManager.Shutdown instead.");
				return;
			}
			(ulong, bool) tuple = ClientIdToTransportId(clientId);
			if (tuple.Item2)
			{
				GenerateDisconnectInformation(clientId, tuple.Item1, reason);
			}
			HandleConnectionDisconnect(clientId, reason);
		}

		internal void Initialize(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_LocalClientTransportId = 0uL;
			LocalClient.IsApproved = false;
			m_PendingClients.Clear();
			ConnectedClients.Clear();
			ConnectedClientsList.Clear();
			ConnectedClientIds.Clear();
			ClientIdToTransportIdMap.Clear();
			TransportIdToClientIdMap.Clear();
			m_IsTransportConnected = false;
			ClientsToApprove.Clear();
			global::Unity.Netcode.NetworkObject.OrphanChildren.Clear();
			m_DisconnectReason = string.Empty;
			ServerDisconnectReason = string.Empty;
			NetworkManager = networkManager;
			MessageManager = networkManager.MessageManager;
			Transport = NetworkManager.NetworkConfig.NetworkTransport;
			if ((bool)Transport)
			{
				Transport.NetworkMetrics = NetworkManager.MetricsManager.NetworkMetrics;
				Transport.OnTransportEvent += HandleNetworkEvent;
				Transport.Initialize(networkManager);
			}
		}

		internal void Shutdown()
		{
			if ((bool)Transport && IsListening)
			{
				Transport.ShuttingDown();
				ulong num = (NetworkManager ? NetworkManager.LocalClientId : 0);
				ulong transportClientId = ((num == 0L) ? 0 : m_LocalClientTransportId);
				GenerateDisconnectInformation(num, transportClientId, "NetworkConnectionManager was shutdown.");
			}
			if (LocalClient.IsServer)
			{
				global::System.Collections.Generic.HashSet<ulong> hashSet = new global::System.Collections.Generic.HashSet<ulong>();
				ulong serverTransportId = GetServerTransportId();
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.NetworkClient> connectedClient in ConnectedClients)
				{
					if (!hashSet.Contains(connectedClient.Key) && connectedClient.Key != serverTransportId)
					{
						hashSet.Add(connectedClient.Key);
					}
				}
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.PendingClient> pendingClient in PendingClients)
				{
					if (!hashSet.Contains(pendingClient.Key) && pendingClient.Key != serverTransportId)
					{
						hashSet.Add(pendingClient.Key);
					}
				}
				foreach (ulong item in hashSet)
				{
					DisconnectRemoteClient(item);
				}
				MessageManager?.ProcessSendQueues();
			}
			else if (NetworkManager != null && NetworkManager.IsListening && LocalClient.IsClient)
			{
				MessageManager?.ProcessSendQueues();
				try
				{
					Transport?.DisconnectLocalClient();
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogException(exception);
				}
			}
			LocalClient.IsApproved = false;
			LocalClient.IsConnected = false;
			ConnectedClients.Clear();
			ConnectedClientIds.Clear();
			ConnectedClientsList.Clear();
			if (NetworkManager != null && NetworkManager.NetworkConfig?.NetworkTransport != null)
			{
				NetworkManager.NetworkConfig.NetworkTransport.OnTransportEvent -= HandleNetworkEvent;
			}
			if (!IsListening)
			{
				return;
			}
			global::Unity.Netcode.NetworkTransport networkTransport = NetworkManager.NetworkConfig?.NetworkTransport;
			if (networkTransport != null)
			{
				networkTransport.Shutdown();
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogInfo("NetworkConnectionManager.Shutdown() -> IsListening && NetworkTransport != null -> NetworkTransport.Shutdown()");
				}
			}
		}

		internal unsafe int SendMessage<TMessageType, TClientIdListType>(ref TMessageType message, global::Unity.Netcode.NetworkDelivery delivery, in TClientIdListType clientIds) where TMessageType : global::Unity.Netcode.INetworkMessage where TClientIdListType : global::System.Collections.Generic.IReadOnlyList<ulong>
		{
			if (LocalClient.IsServer)
			{
				ulong* ptr = stackalloc ulong[clientIds.Count];
				int num = 0;
				for (int i = 0; i < clientIds.Count; i++)
				{
					if (clientIds[i] != 0L)
					{
						ptr[num++] = clientIds[i];
					}
				}
				if (num == 0)
				{
					return 0;
				}
				return MessageManager.SendMessage(ref message, delivery, ptr, num);
			}
			if (clientIds.Count != 1 || clientIds[0] != 0L)
			{
				throw new global::System.ArgumentException("Clients may only send messages to ServerClientId");
			}
			return MessageManager.SendMessage(ref message, delivery, in clientIds);
		}

		internal unsafe int SendMessage<T>(ref T message, global::Unity.Netcode.NetworkDelivery delivery, ulong* clientIds, int numClientIds) where T : global::Unity.Netcode.INetworkMessage
		{
			if (LocalClient.IsServer)
			{
				ulong* ptr = stackalloc ulong[numClientIds];
				int num = 0;
				for (int i = 0; i < numClientIds; i++)
				{
					if (clientIds[i] != 0L)
					{
						ptr[num++] = clientIds[i];
					}
				}
				if (num == 0)
				{
					return 0;
				}
				return MessageManager.SendMessage(ref message, delivery, ptr, num);
			}
			if (numClientIds != 1 || *clientIds != 0L)
			{
				throw new global::System.ArgumentException("Clients may only send messages to ServerClientId");
			}
			return MessageManager.SendMessage(ref message, delivery, clientIds, numClientIds);
		}

		internal unsafe int SendMessage<T>(ref T message, global::Unity.Netcode.NetworkDelivery delivery, in global::Unity.Collections.NativeArray<ulong> clientIds) where T : global::Unity.Netcode.INetworkMessage
		{
			return SendMessage(ref message, delivery, (ulong*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(clientIds), clientIds.Length);
		}

		internal int SendMessage<T>(ref T message, global::Unity.Netcode.NetworkDelivery delivery, ulong clientId) where T : global::Unity.Netcode.INetworkMessage
		{
			if ((LocalClient.IsServer && clientId == 0L) || MessageManager == null)
			{
				return 0;
			}
			if (!LocalClient.IsServer && clientId != 0L)
			{
				throw new global::System.ArgumentException("Clients may only send messages to ServerClientId");
			}
			return MessageManager.SendMessage(ref message, delivery, clientId);
		}

		[global::System.Diagnostics.Conditional("ENABLE_DAHOST_AUTOPROMOTE_SESSION_OWNER")]
		private void DaHostPromoteSessionOwner()
		{
			if (NetworkManager.DistributedAuthorityMode && !NetworkManager.ShutdownInProgress && NetworkManager.IsListening)
			{
				return;
			}
			ulong sessionOwner = NetworkManager.LocalClientId;
			if (ConnectedClientIds.Count > 1)
			{
				ulong num = ulong.MaxValue;
				global::Unity.Netcode.Transports.UTP.UnityTransport unityTransport = NetworkManager.NetworkConfig.NetworkTransport as global::Unity.Netcode.Transports.UTP.UnityTransport;
				foreach (ulong connectedClientId in ConnectedClientIds)
				{
					if (connectedClientId != NetworkManager.LocalClientId)
					{
						ulong currentRtt = unityTransport.GetCurrentRtt(connectedClientId);
						if (currentRtt < num)
						{
							sessionOwner = connectedClientId;
							num = currentRtt;
						}
					}
				}
			}
			global::Unity.Netcode.SessionOwnerMessage message = new global::Unity.Netcode.SessionOwnerMessage
			{
				SessionOwner = sessionOwner
			};
			MessageManager?.SendMessage(ref message, global::Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced, in ConnectedClientIds);
			NetworkManager.SetSessionOwner(sessionOwner);
		}
	}
}
