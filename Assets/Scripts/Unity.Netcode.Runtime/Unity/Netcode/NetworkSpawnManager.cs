namespace Unity.Netcode
{
	public class NetworkSpawnManager
	{
		internal enum InstantiateAndSpawnErrorTypes
		{
			NetworkPrefabNull = 0,
			NotAuthority = 1,
			InvokedWhenShuttingDown = 2,
			NotRegisteredNetworkPrefab = 3,
			NetworkManagerNull = 4,
			NoActiveSession = 5
		}

		internal struct DeferredDespawnObject
		{
			public int TickToDespawn;

			public bool HasDeferredDespawnCheck;

			public bool DestroyGameObject;

			public ulong NetworkObjectId;
		}

		internal global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>> ObjectsToShowToClient = new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>();

		internal global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkObject, global::System.Collections.Generic.List<ulong>> ClientsToShowObject = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkObject, global::System.Collections.Generic.List<ulong>>();

		public readonly global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject> SpawnedObjects = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject>();

		public readonly global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject> SpawnedObjectsList = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject>();

		public readonly global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject>> OwnershipToObjectsTable = new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject>>();

		private global::System.Collections.Generic.Dictionary<ulong, ulong> m_ObjectToOwnershipTable = new global::System.Collections.Generic.Dictionary<ulong, ulong>();

		private global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> m_PlayerObjects = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>();

		private global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>> m_PlayerObjectsTable = new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>();

		internal readonly global::System.Collections.Generic.Queue<global::Unity.Netcode.ReleasedNetworkId> ReleasedNetworkObjectIds = new global::System.Collections.Generic.Queue<global::Unity.Netcode.ReleasedNetworkId>();

		private ulong m_NetworkObjectIdCounter;

		private global::System.Collections.Generic.List<ulong> m_TargetClientIds = new global::System.Collections.Generic.List<ulong>();

		private global::System.Collections.Generic.Dictionary<ulong, float> m_LastChangeInOwnership = new global::System.Collections.Generic.Dictionary<ulong, float>();

		private const int k_MaximumTickOwnershipChangeMultiplier = 6;

		internal static readonly global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes, string> InstantiateAndSpawnErrors = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes, string>(new global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes, string>[6]
		{
			new global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes, string>(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NetworkPrefabNull, "The NetworkObject prefab parameter was null!"),
			new global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes, string>(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NotAuthority, "Only the server has authority to InstantiateAndSpawn!"),
			new global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes, string>(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.InvokedWhenShuttingDown, "Invoking InstantiateAndSpawn while shutting down! Calls to InstantiateAndSpawn will be ignored."),
			new global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes, string>(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NotRegisteredNetworkPrefab, "The NetworkObject parameter is not a registered network prefab. Did you forget to register it or are you trying to instantiate and spawn an instance of a network prefab?"),
			new global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes, string>(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NetworkManagerNull, "The NetworkManager parameter was null!"),
			new global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes, string>(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NoActiveSession, "You can only invoke this method when you are connected to an existing/in-progress network session!")
		});

		internal global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject> NetworkObjectsToSynchronizeSceneChanges = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject>();

		internal global::System.Collections.Generic.Stack<ulong> CleanUpDisposedObjects = new global::System.Collections.Generic.Stack<ulong>();

		internal bool EnableDistributeLogging;

		internal global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSpawnManager.DeferredDespawnObject> DeferredDespawnObjects = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSpawnManager.DeferredDespawnObject>();

		public global::System.Collections.Generic.IReadOnlyList<global::Unity.Netcode.NetworkObject> PlayerObjects => m_PlayerObjects;

		public global::Unity.Netcode.NetworkManager NetworkManager { get; }

		public global::System.Collections.Generic.List<ulong> GetConnectedPlayers()
		{
			return global::System.Linq.Enumerable.ToList(m_PlayerObjectsTable.Keys);
		}

		private void AddPlayerObject(global::Unity.Netcode.NetworkObject playerObject)
		{
			if (!playerObject.IsPlayerObject && NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Normal)
			{
				global::Unity.Netcode.NetworkLog.LogError("Attempting to register a NetworkObject as a player object but IsPlayerObject is not set!");
				return;
			}
			bool cMBServiceConnection = NetworkManager.CMBServiceConnection;
			bool enableSceneManagement = NetworkManager.NetworkConfig.EnableSceneManagement;
			foreach (global::Unity.Netcode.NetworkObject playerObject2 in m_PlayerObjects)
			{
				bool flag = !cMBServiceConnection || enableSceneManagement || !playerObject2.IsLocalPlayer;
				if (playerObject2.SpawnWithObservers && flag)
				{
					playerObject2.AddObserver(playerObject.OwnerClientId);
				}
				if (playerObject.SpawnWithObservers)
				{
					playerObject.AddObserver(playerObject2.OwnerClientId);
				}
			}
			if (playerObject.SpawnWithObservers || (NetworkManager.DistributedAuthorityMode && NetworkManager.LocalClientId == playerObject.OwnerClientId))
			{
				playerObject.AddObserver(playerObject.OwnerClientId);
			}
			m_PlayerObjects.Add(playerObject);
			if (!m_PlayerObjectsTable.ContainsKey(playerObject.OwnerClientId))
			{
				m_PlayerObjectsTable.Add(playerObject.OwnerClientId, new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>());
			}
			m_PlayerObjectsTable[playerObject.OwnerClientId].Add(playerObject);
		}

		internal void UpdateNetworkClientPlayer(global::Unity.Netcode.NetworkObject playerObject)
		{
			if (!NetworkManager.ConnectionManager.ConnectedClients.ContainsKey(playerObject.OwnerClientId))
			{
				NetworkManager.ConnectionManager.AddClient(playerObject.OwnerClientId);
			}
			global::Unity.Netcode.NetworkClient networkClient = NetworkManager.ConnectionManager.ConnectedClients[playerObject.OwnerClientId];
			if (networkClient.PlayerObject != null && m_PlayerObjects.Contains(networkClient.PlayerObject))
			{
				RemovePlayerObject(networkClient.PlayerObject);
			}
			NetworkManager.ConnectionManager.ConnectedClients[playerObject.OwnerClientId].AssignPlayerObject(ref playerObject);
			AddPlayerObject(playerObject);
		}

		private void RemovePlayerObject(global::Unity.Netcode.NetworkObject playerObject, bool destroyingObject = false)
		{
			if (!playerObject.IsPlayerObject && NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Normal)
			{
				global::Unity.Netcode.NetworkLog.LogError("Attempting to deregister a NetworkObject as a player object but IsPlayerObject is not set!");
				return;
			}
			playerObject.IsPlayerObject = false;
			m_PlayerObjects.Remove(playerObject);
			if (m_PlayerObjectsTable.ContainsKey(playerObject.OwnerClientId))
			{
				m_PlayerObjectsTable[playerObject.OwnerClientId].Remove(playerObject);
				if (m_PlayerObjectsTable[playerObject.OwnerClientId].Count == 0)
				{
					m_PlayerObjectsTable.Remove(playerObject.OwnerClientId);
				}
			}
			if (NetworkManager.ConnectionManager.ConnectedClients.ContainsKey(playerObject.OwnerClientId) && destroyingObject)
			{
				NetworkManager.ConnectionManager.ConnectedClients[playerObject.OwnerClientId].PlayerObject = null;
			}
		}

		internal void MarkObjectForShowingTo(global::Unity.Netcode.NetworkObject networkObject, ulong clientId)
		{
			if (!ObjectsToShowToClient.ContainsKey(clientId))
			{
				ObjectsToShowToClient.Add(clientId, new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>());
			}
			ObjectsToShowToClient[clientId].Add(networkObject);
			if (NetworkManager.DistributedAuthorityMode)
			{
				if (!ClientsToShowObject.ContainsKey(networkObject))
				{
					ClientsToShowObject.Add(networkObject, new global::System.Collections.Generic.List<ulong>());
				}
				ClientsToShowObject[networkObject].Add(clientId);
			}
		}

		internal bool RemoveObjectFromShowingTo(global::Unity.Netcode.NetworkObject networkObject, ulong clientId)
		{
			if (NetworkManager.DistributedAuthorityMode && ClientsToShowObject.ContainsKey(networkObject))
			{
				ClientsToShowObject[networkObject].Remove(clientId);
				if (ClientsToShowObject[networkObject].Count == 0)
				{
					ClientsToShowObject.Remove(networkObject);
				}
			}
			bool flag = false;
			if (!ObjectsToShowToClient.ContainsKey(clientId))
			{
				return false;
			}
			while (ObjectsToShowToClient[clientId].Contains(networkObject))
			{
				global::UnityEngine.Debug.LogWarning("Object was shown and hidden from the same client in the same Network frame. As a result, the client will _not_ receive a NetworkSpawn");
				ObjectsToShowToClient[clientId].Remove(networkObject);
				flag = true;
			}
			if (flag)
			{
				networkObject.Observers.Remove(clientId);
			}
			return flag;
		}

		internal void UpdateOwnershipTable(global::Unity.Netcode.NetworkObject networkObject, ulong newOwner, bool isRemoving = false)
		{
			ulong num = newOwner;
			if (m_ObjectToOwnershipTable.ContainsKey(networkObject.NetworkObjectId))
			{
				num = m_ObjectToOwnershipTable[networkObject.NetworkObjectId];
				if (isRemoving)
				{
					m_ObjectToOwnershipTable.Remove(networkObject.NetworkObjectId);
				}
				else
				{
					if (NetworkManager.DistributedAuthorityMode && num == newOwner)
					{
						return;
					}
					m_ObjectToOwnershipTable[networkObject.NetworkObjectId] = newOwner;
				}
			}
			else
			{
				m_ObjectToOwnershipTable.Add(networkObject.NetworkObjectId, newOwner);
			}
			if (num != newOwner && OwnershipToObjectsTable.ContainsKey(num))
			{
				if (!OwnershipToObjectsTable[num].ContainsKey(networkObject.NetworkObjectId))
				{
					throw new global::System.Exception(string.Format("Client-ID {0} had a partial {1} entry! Potentially corrupted {2}?", num, "m_ObjectToOwnershipTable", "OwnershipToObjectsTable"));
				}
				OwnershipToObjectsTable[num].Remove(networkObject.NetworkObjectId);
				if (isRemoving)
				{
					return;
				}
			}
			if (!OwnershipToObjectsTable.ContainsKey(newOwner))
			{
				OwnershipToObjectsTable.Add(newOwner, new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject>());
			}
			if (!OwnershipToObjectsTable[newOwner].ContainsKey(networkObject.NetworkObjectId))
			{
				OwnershipToObjectsTable[newOwner].Add(networkObject.NetworkObjectId, networkObject);
			}
			else if (isRemoving)
			{
				OwnershipToObjectsTable[num].Remove(networkObject.NetworkObjectId);
			}
			else if (NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer && num == newOwner)
			{
				global::Unity.Netcode.NetworkLog.LogWarning($"Setting ownership twice? Client-ID {num} already owns NetworkObject ID {networkObject.NetworkObjectId}!");
			}
		}

		public global::Unity.Netcode.NetworkObject[] GetClientOwnedObjects(ulong clientId)
		{
			if (!OwnershipToObjectsTable.ContainsKey(clientId))
			{
				OwnershipToObjectsTable.Add(clientId, new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject>());
			}
			return global::System.Linq.Enumerable.ToArray(OwnershipToObjectsTable[clientId].Values);
		}

		internal ulong GetNetworkObjectId()
		{
			if (ReleasedNetworkObjectIds.Count > 0 && NetworkManager.NetworkConfig.RecycleNetworkIds && NetworkManager.RealTimeProvider.UnscaledTime - ReleasedNetworkObjectIds.Peek().ReleaseTime >= NetworkManager.NetworkConfig.NetworkIdRecycleDelay)
			{
				return ReleasedNetworkObjectIds.Dequeue().NetworkId;
			}
			m_NetworkObjectIdCounter++;
			return m_NetworkObjectIdCounter + NetworkManager.LocalClientId * 10000;
		}

		public global::Unity.Netcode.NetworkObject GetLocalPlayerObject()
		{
			return GetPlayerNetworkObject(NetworkManager.LocalClientId);
		}

		public global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> GetPlayerNetworkObjects(ulong clientId)
		{
			if (m_PlayerObjectsTable.ContainsKey(clientId))
			{
				return m_PlayerObjectsTable[clientId];
			}
			return null;
		}

		public global::Unity.Netcode.NetworkObject GetPlayerNetworkObject(ulong clientId)
		{
			if (!NetworkManager.DistributedAuthorityMode)
			{
				if (!NetworkManager.IsServer && NetworkManager.LocalClientId != clientId)
				{
					throw new global::Unity.Netcode.NotServerException("Only the server can find player objects from other clients.");
				}
				if (TryGetNetworkClient(clientId, out var networkClient))
				{
					return networkClient.PlayerObject;
				}
			}
			else if (m_PlayerObjectsTable.ContainsKey(clientId))
			{
				return global::System.Linq.Enumerable.First(m_PlayerObjectsTable[clientId]);
			}
			return null;
		}

		private bool TryGetNetworkClient(ulong clientId, out global::Unity.Netcode.NetworkClient networkClient)
		{
			if (NetworkManager.IsServer)
			{
				return NetworkManager.ConnectedClients.TryGetValue(clientId, out networkClient);
			}
			if (NetworkManager.LocalClient != null && clientId == NetworkManager.LocalClient.ClientId)
			{
				networkClient = NetworkManager.LocalClient;
				return true;
			}
			networkClient = null;
			return false;
		}

		protected virtual void InternalOnOwnershipChanged(ulong perviousOwner, ulong newOwner)
		{
		}

		internal void RemoveOwnership(global::Unity.Netcode.NetworkObject networkObject)
		{
			if (NetworkManager.DistributedAuthorityMode && !NetworkManager.ShutdownInProgress)
			{
				global::UnityEngine.Debug.LogError("Removing ownership is invalid in Distributed Authority Mode. Use ChangeOwnership instead.");
			}
			else
			{
				ChangeOwnership(networkObject, 0uL, isAuthorized: true);
			}
		}

		internal void ChangeOwnership(global::Unity.Netcode.NetworkObject networkObject, ulong clientId, bool isAuthorized, bool isRequestApproval = false)
		{
			if (clientId == networkObject.OwnerClientId)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::UnityEngine.Debug.LogWarning(string.Format("[{0}][{1}] Attempting to change ownership to Client-{2} when the owner is already {3}! (Ignoring)", "NetworkSpawnManager", "ChangeOwnership", clientId, networkObject.OwnerClientId));
				}
				return;
			}
			bool distributedAuthorityMode = NetworkManager.DistributedAuthorityMode;
			if (NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer && !distributedAuthorityMode && m_LastChangeInOwnership.ContainsKey(networkObject.NetworkObjectId) && m_LastChangeInOwnership[networkObject.NetworkObjectId] > global::UnityEngine.Time.realtimeSinceStartup)
			{
				for (int i = 0; i < networkObject.ChildNetworkBehaviours.Count; i++)
				{
					if (networkObject.ChildNetworkBehaviours[i].NetworkVariableFields.Count > 0)
					{
						global::Unity.Netcode.NetworkLog.LogWarningServer($"[Rapid Ownership Change Detected][Potential Loss in State] Detected a rapid change in ownership that exceeds a frequency less than {6}x the current network tick rate! Provide at least {6}x the current network tick rate between ownership changes to avoid NetworkVariable state loss.");
						break;
					}
				}
			}
			if (distributedAuthorityMode)
			{
				if (networkObject.IsOwnershipSessionOwner && (!NetworkManager.LocalClient.IsSessionOwner || clientId != NetworkManager.CurrentSessionOwner))
				{
					if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogErrorServer(string.Format("[{0}][Session Owner Only] You cannot change ownership of a {1} that has the {2} flag set!", networkObject.name, "NetworkObject", global::Unity.Netcode.NetworkObject.OwnershipStatus.SessionOwner));
					}
					networkObject.OnOwnershipPermissionsFailure?.Invoke(global::Unity.Netcode.NetworkObject.OwnershipPermissionsFailureStatus.SessionOwnerOnly);
					return;
				}
				if (!isAuthorized && !isRequestApproval)
				{
					if (networkObject.IsOwnershipLocked)
					{
						if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
						{
							global::Unity.Netcode.NetworkLog.LogErrorServer("[" + networkObject.name + "][Locked] You cannot change ownership while a NetworkObject is locked!");
						}
						networkObject.OnOwnershipPermissionsFailure?.Invoke(global::Unity.Netcode.NetworkObject.OwnershipPermissionsFailureStatus.Locked);
						return;
					}
					if (networkObject.IsRequestInProgress)
					{
						if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
						{
							global::Unity.Netcode.NetworkLog.LogErrorServer("[" + networkObject.name + "][Request Pending] You cannot change ownership while a NetworkObject has a pending ownership request!");
						}
						networkObject.OnOwnershipPermissionsFailure?.Invoke(global::Unity.Netcode.NetworkObject.OwnershipPermissionsFailureStatus.RequestInProgress);
						return;
					}
					if (networkObject.IsOwnershipRequestRequired)
					{
						if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
						{
							global::Unity.Netcode.NetworkLog.LogErrorServer(string.Format("[{0}][Request Required] You cannot change ownership directly if a {1} has the {2} flag set!", networkObject.name, "NetworkObject", global::Unity.Netcode.NetworkObject.OwnershipStatus.RequestRequired));
						}
						networkObject.OnOwnershipPermissionsFailure?.Invoke(global::Unity.Netcode.NetworkObject.OwnershipPermissionsFailureStatus.RequestRequired);
						return;
					}
					if (!networkObject.IsOwnershipTransferable)
					{
						if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
						{
							global::Unity.Netcode.NetworkLog.LogErrorServer(string.Format("[{0}][Not transferrable] You cannot change ownership of a {1} that does not have the {2} flag set!", networkObject.name, "NetworkObject", global::Unity.Netcode.NetworkObject.OwnershipStatus.Transferable));
						}
						networkObject.OnOwnershipPermissionsFailure?.Invoke(global::Unity.Netcode.NetworkObject.OwnershipPermissionsFailureStatus.NotTransferrable);
						return;
					}
				}
			}
			else if (!isAuthorized)
			{
				throw new global::Unity.Netcode.NotServerException("Only the server can change ownership");
			}
			if (!networkObject.IsSpawned)
			{
				throw new global::Unity.Netcode.SpawnStateException("Object is not spawned");
			}
			if (!networkObject.Observers.Contains(clientId))
			{
				if (NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogWarningServer(string.Format("[Invalid Owner] Cannot send Ownership change as client-{0} cannot see {1}! Use {2} first.", clientId, networkObject.name, "NetworkShow"));
				}
				return;
			}
			ulong previousOwnerId = networkObject.PreviousOwnerId;
			ulong ownerClientId = networkObject.OwnerClientId;
			networkObject.PreviousOwnerId = networkObject.OwnerClientId;
			networkObject.OwnerClientId = clientId;
			networkObject.InvokeBehaviourOnOwnershipChanged(ownerClientId, clientId);
			if (ownerClientId == NetworkManager.LocalClientId)
			{
				networkObject.SynchronizeOwnerNetworkVariables(ownerClientId, previousOwnerId);
			}
			SendChangeOwnershipMessage(ref networkObject, isRequestApproval);
			networkObject.InvokeOwnershipChanged(networkObject.PreviousOwnerId, clientId);
			if (!distributedAuthorityMode)
			{
				if (!m_LastChangeInOwnership.ContainsKey(networkObject.NetworkObjectId))
				{
					m_LastChangeInOwnership.Add(networkObject.NetworkObjectId, 0f);
				}
				float num = 1f / (float)NetworkManager.NetworkConfig.TickRate;
				m_LastChangeInOwnership[networkObject.NetworkObjectId] = global::UnityEngine.Time.realtimeSinceStartup + num * 6f;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal bool IsObjectVisibilityPending(ulong clientId, ref global::Unity.Netcode.NetworkObject networkObject)
		{
			if (NetworkManager.DistributedAuthorityMode && ClientsToShowObject.ContainsKey(networkObject))
			{
				return ClientsToShowObject[networkObject].Contains(clientId);
			}
			if (ObjectsToShowToClient.ContainsKey(clientId))
			{
				return ObjectsToShowToClient[clientId].Contains(networkObject);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SendChangeOwnershipMessage(ref global::Unity.Netcode.NetworkObject networkObject, bool isRequestApproval)
		{
			bool distributedAuthorityMode = NetworkManager.DistributedAuthorityMode;
			bool flag = distributedAuthorityMode && !NetworkManager.DAHost;
			global::System.Collections.Generic.List<ulong> list = null;
			global::Unity.Netcode.ChangeOwnershipMessage message = new global::Unity.Netcode.ChangeOwnershipMessage
			{
				ChangeMessageType = global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipChanging,
				NetworkObjectId = networkObject.NetworkObjectId,
				OwnerClientId = networkObject.OwnerClientId
			};
			if (distributedAuthorityMode)
			{
				message.DistributedAuthorityMode = true;
				message.RequestClientId = networkObject.PreviousOwnerId;
				message.OwnershipFlags = (ushort)networkObject.Ownership;
				if (isRequestApproval)
				{
					message.ChangeMessageType = global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestApproved;
				}
				if (flag)
				{
					list = new global::System.Collections.Generic.List<ulong>(NetworkManager.ConnectedClientsIds.Count);
				}
			}
			foreach (ulong connectedClientsId in NetworkManager.ConnectedClientsIds)
			{
				if (connectedClientsId != NetworkManager.LocalClientId && networkObject.IsNetworkVisibleTo(connectedClientsId) && !IsObjectVisibilityPending(connectedClientsId, ref networkObject))
				{
					if (flag)
					{
						list.Add(connectedClientsId);
						continue;
					}
					int num = NetworkManager.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.NetworkDelivery.ReliableSequenced, connectedClientsId);
					NetworkManager.NetworkMetrics.TrackOwnershipChangeSent(connectedClientsId, networkObject, num);
				}
			}
			if (flag && list.Count > 0)
			{
				message.ClientIds = list.ToArray();
				message.ClientIdCount = list.Count;
				int num = NetworkManager.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.NetworkDelivery.ReliableSequenced, 0uL);
				NetworkManager.NetworkMetrics.TrackOwnershipChangeSent(0uL, networkObject, num);
			}
		}

		internal bool HasPrefab(global::Unity.Netcode.NetworkObject.SerializedObject serializedObject)
		{
			if (!NetworkManager.NetworkConfig.EnableSceneManagement || !serializedObject.IsSceneObject)
			{
				if (NetworkManager.PrefabHandler.ContainsHandler(serializedObject.Hash))
				{
					return true;
				}
				if (NetworkManager.NetworkConfig.Prefabs.NetworkPrefabOverrideLinks.TryGetValue(serializedObject.Hash, out var value))
				{
					global::Unity.Netcode.NetworkPrefabOverride networkPrefabOverride = value.Override;
					if (networkPrefabOverride == global::Unity.Netcode.NetworkPrefabOverride.None || (uint)(networkPrefabOverride - 1) > 1u)
					{
						return value.Prefab != null;
					}
					return value.OverridingTargetPrefab != null;
				}
				return false;
			}
			return NetworkManager.SceneManager.GetSceneRelativeInSceneNetworkObject(serializedObject.Hash, serializedObject.NetworkSceneHandle) != null;
		}

		public global::Unity.Netcode.NetworkObject InstantiateAndSpawn(global::Unity.Netcode.NetworkObject networkPrefab, ulong ownerClientId = 0uL, bool destroyWithScene = false, bool isPlayerObject = false, bool forceOverride = false, global::UnityEngine.Vector3 position = default(global::UnityEngine.Vector3), global::UnityEngine.Quaternion rotation = default(global::UnityEngine.Quaternion))
		{
			if (networkPrefab == null)
			{
				global::UnityEngine.Debug.LogError(InstantiateAndSpawnErrors[global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NetworkPrefabNull]);
				return null;
			}
			ownerClientId = (NetworkManager.DistributedAuthorityMode ? NetworkManager.LocalClientId : ownerClientId);
			if (!NetworkManager.IsServer && !NetworkManager.DistributedAuthorityMode)
			{
				global::UnityEngine.Debug.LogError(InstantiateAndSpawnErrors[global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NotAuthority]);
				return null;
			}
			if (NetworkManager.ShutdownInProgress)
			{
				global::UnityEngine.Debug.LogWarning(InstantiateAndSpawnErrors[global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.InvokedWhenShuttingDown]);
				return null;
			}
			if (!NetworkManager.NetworkConfig.Prefabs.Contains(networkPrefab.gameObject))
			{
				global::UnityEngine.Debug.LogError(InstantiateAndSpawnErrors[global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NotRegisteredNetworkPrefab]);
				return null;
			}
			return InstantiateAndSpawnNoParameterChecks(networkPrefab, NetworkManager, ownerClientId, destroyWithScene, isPlayerObject, forceOverride, position, rotation);
		}

		internal global::Unity.Netcode.NetworkObject InstantiateAndSpawnNoParameterChecks(global::Unity.Netcode.NetworkObject networkPrefab, global::Unity.Netcode.NetworkManager networkManager, ulong ownerClientId = 0uL, bool destroyWithScene = false, bool isPlayerObject = false, bool forceOverride = false, global::UnityEngine.Vector3 position = default(global::UnityEngine.Vector3), global::UnityEngine.Quaternion rotation = default(global::UnityEngine.Quaternion))
		{
			global::Unity.Netcode.NetworkObject networkObject = ((!forceOverride && !NetworkManager.IsClient && !NetworkManager.DistributedAuthorityMode && !NetworkManager.PrefabHandler.ContainsHandler(networkPrefab.GlobalObjectIdHash)) ? InstantiateNetworkPrefab(networkPrefab.gameObject, networkPrefab.GlobalObjectIdHash, position, rotation) : GetNetworkObjectToSpawn(networkPrefab.GlobalObjectIdHash, ownerClientId, position, rotation));
			if (networkObject == null)
			{
				global::UnityEngine.Debug.LogError("Failed to instantiate and spawn " + networkPrefab.name + "!");
				return null;
			}
			networkObject.NetworkManagerOwner = networkManager;
			networkObject.IsPlayerObject = isPlayerObject;
			networkObject.transform.SetPositionAndRotation(position, rotation);
			if (isPlayerObject)
			{
				networkObject.SpawnAsPlayerObject(ownerClientId, destroyWithScene);
			}
			else
			{
				networkObject.SpawnWithOwnership(ownerClientId, destroyWithScene);
			}
			return networkObject;
		}

		internal global::Unity.Netcode.NetworkObject GetNetworkObjectToSpawn(uint globalObjectIdHash, ulong ownerId, global::UnityEngine.Vector3? position, global::UnityEngine.Quaternion? rotation, bool isScenePlaced = false, byte[] instantiationData = null)
		{
			if (NetworkManager.PrefabHandler.ContainsHandler(globalObjectIdHash))
			{
				return NetworkManager.PrefabHandler.HandleNetworkPrefabSpawn(globalObjectIdHash, ownerId, position.GetValueOrDefault(), rotation.GetValueOrDefault(), instantiationData);
			}
			global::UnityEngine.GameObject gameObject = null;
			bool flag = !NetworkManager.NetworkConfig.EnableSceneManagement && isScenePlaced;
			if (NetworkManager.NetworkConfig.Prefabs.NetworkPrefabOverrideLinks.ContainsKey(globalObjectIdHash))
			{
				global::Unity.Netcode.NetworkPrefab networkPrefab = NetworkManager.NetworkConfig.Prefabs.NetworkPrefabOverrideLinks[globalObjectIdHash];
				global::Unity.Netcode.NetworkPrefabOverride networkPrefabOverride = networkPrefab.Override;
				gameObject = ((networkPrefabOverride == global::Unity.Netcode.NetworkPrefabOverride.None || (uint)(networkPrefabOverride - 1) > 1u) ? networkPrefab.Prefab : ((!flag) ? NetworkManager.NetworkConfig.Prefabs.NetworkPrefabOverrideLinks[globalObjectIdHash].OverridingTargetPrefab : (networkPrefab.SourcePrefabToOverride ? networkPrefab.SourcePrefabToOverride : networkPrefab.Prefab)));
			}
			if (gameObject == null)
			{
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogError(string.Format("Failed to create object locally. [{0}={1}]. {2} could not be found. Is the prefab registered with {3}?", "globalObjectIdHash", globalObjectIdHash, "NetworkPrefab", NetworkManager.name));
				}
				return null;
			}
			return InstantiateNetworkPrefab(gameObject, globalObjectIdHash, position, rotation);
		}

		internal global::Unity.Netcode.NetworkObject InstantiateNetworkPrefab(global::UnityEngine.GameObject networkPrefab, uint prefabGlobalObjectIdHash, global::UnityEngine.Vector3? position, global::UnityEngine.Quaternion? rotation)
		{
			global::Unity.Netcode.NetworkObject component = global::UnityEngine.Object.Instantiate(networkPrefab).GetComponent<global::Unity.Netcode.NetworkObject>();
			component.transform.SetPositionAndRotation(position ?? component.transform.position, rotation ?? component.transform.rotation);
			component.PrefabGlobalObjectIdHash = prefabGlobalObjectIdHash;
			return component;
		}

		internal global::Unity.Netcode.NetworkObject CreateLocalNetworkObject(global::Unity.Netcode.NetworkObject.SerializedObject serializedObject, byte[] instantiationData = null)
		{
			global::Unity.Netcode.NetworkObject networkObject = null;
			uint hash = serializedObject.Hash;
			global::UnityEngine.Vector3 vector = (serializedObject.HasTransform ? serializedObject.Transform.Position : default(global::UnityEngine.Vector3));
			global::UnityEngine.Quaternion quaternion = (serializedObject.HasTransform ? serializedObject.Transform.Rotation : default(global::UnityEngine.Quaternion));
			global::UnityEngine.Vector3 localScale = (serializedObject.HasTransform ? serializedObject.Transform.Scale : default(global::UnityEngine.Vector3));
			ulong value = (serializedObject.HasParent ? serializedObject.ParentObjectId : 0);
			bool flag = !serializedObject.HasParent || serializedObject.WorldPositionStays;
			if (!NetworkManager.NetworkConfig.EnableSceneManagement || !serializedObject.IsSceneObject)
			{
				networkObject = GetNetworkObjectToSpawn(serializedObject.Hash, serializedObject.OwnerClientId, vector, quaternion, serializedObject.IsSceneObject, instantiationData);
			}
			else
			{
				networkObject = NetworkManager.SceneManager.GetSceneRelativeInSceneNetworkObject(hash, serializedObject.NetworkSceneHandle);
				if (networkObject == null && global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogError(string.Format("{0} hash was not found! In-Scene placed {1} soft synchronization failure for Hash: {2}!", "NetworkPrefab", "NetworkObject", hash));
				}
				if (networkObject != null && !networkObject.gameObject.activeInHierarchy)
				{
					networkObject.gameObject.SetActive(value: true);
				}
			}
			if (networkObject != null)
			{
				networkObject.DestroyWithScene = serializedObject.DestroyWithScene;
				networkObject.NetworkSceneHandle = serializedObject.NetworkSceneHandle;
				networkObject.DontDestroyWithOwner = serializedObject.DontDestroyWithOwner;
				networkObject.Ownership = (global::Unity.Netcode.NetworkObject.OwnershipStatus)serializedObject.OwnershipFlags;
				bool flag2 = false;
				if (serializedObject.IsSceneObject && networkObject.transform.parent != null)
				{
					global::Unity.Netcode.NetworkObject component = networkObject.transform.parent.GetComponent<global::Unity.Netcode.NetworkObject>();
					flag2 = !component && serializedObject.HasParent;
					if ((bool)component && (!serializedObject.HasParent || (serializedObject.IsLatestParentSet && (serializedObject.LatestParent.Value != component.NetworkObjectId || serializedObject.WorldPositionStays))))
					{
						networkObject.ApplyNetworkParenting(removeParent: true, ignoreNotSpawned: true, orphanedChildPass: false, !serializedObject.HasParent);
					}
				}
				if (serializedObject.HasTransform)
				{
					if ((flag && !flag2) || !networkObject.AutoObjectParentSync)
					{
						networkObject.transform.SetPositionAndRotation(vector, quaternion);
					}
					else
					{
						networkObject.transform.SetLocalPositionAndRotation(vector, quaternion);
					}
					if (!serializedObject.IsPlayerObject)
					{
						networkObject.transform.localScale = localScale;
					}
				}
				if (serializedObject.HasParent)
				{
					ulong? latestParent = null;
					if (serializedObject.IsLatestParentSet)
					{
						latestParent = value;
					}
					networkObject.SetNetworkParenting(latestParent, flag);
				}
				if (!serializedObject.IsSceneObject && global::Unity.Netcode.NetworkSceneManager.IsSpawnedObjectsPendingInDontDestroyOnLoad)
				{
					global::UnityEngine.Object.DontDestroyOnLoad(networkObject.gameObject);
				}
			}
			return networkObject;
		}

		internal void AuthorityLocalSpawn([global::System.Diagnostics.CodeAnalysis.NotNull] global::Unity.Netcode.NetworkObject networkObject, ulong networkId, bool sceneObject, bool playerObject, ulong ownerClientId, bool destroyWithScene)
		{
			if (networkObject.IsSpawned)
			{
				global::UnityEngine.Debug.LogError(networkObject.name + " is already spawned!");
				return;
			}
			if (!sceneObject && networkObject.GetComponentsInChildren<global::Unity.Netcode.NetworkObject>().Length > 1)
			{
				global::UnityEngine.Debug.LogError("Spawning NetworkObjects with nested NetworkObjects is only supported for scene objects. Child NetworkObjects will not be spawned over the network!");
			}
			networkObject.IsSpawnAuthority = true;
			networkObject.NetworkManagerOwner = NetworkManager;
			networkObject.InvokeBehaviourNetworkPreSpawn();
			if (NetworkManager.DistributedAuthorityMode)
			{
				if (NetworkManager.NetworkConfig.EnableSceneManagement && sceneObject)
				{
					networkObject.SceneOriginHandle = networkObject.gameObject.scene.handle;
					networkObject.NetworkSceneHandle = NetworkManager.SceneManager.ClientSceneHandleToServerSceneHandle[networkObject.gameObject.scene.handle];
				}
				if (!networkObject.SpawnWithObservers)
				{
					networkObject.AddObserver(ownerClientId);
				}
				else
				{
					foreach (ulong connectedClientId in NetworkManager.ConnectionManager.ConnectedClientIds)
					{
						if (networkObject.CheckObjectVisibility == null || networkObject.CheckObjectVisibility(connectedClientId))
						{
							networkObject.AddObserver(connectedClientId);
						}
					}
					if (!networkObject.Observers.Contains(ownerClientId))
					{
						global::UnityEngine.Debug.LogError($"Client-{ownerClientId} is the owner of {networkObject.name} but is not an observer! Adding owner, but there is a bug in observer synchronization!");
						networkObject.AddObserver(ownerClientId);
					}
				}
			}
			SpawnNetworkObjectLocallyCommon(networkObject, networkId, sceneObject, playerObject, ownerClientId, destroyWithScene);
			networkObject.InvokeBehaviourNetworkPostSpawn();
		}

		internal void NonAuthorityLocalSpawn([global::System.Diagnostics.CodeAnalysis.NotNull] global::Unity.Netcode.NetworkObject networkObject, in global::Unity.Netcode.NetworkObject.SerializedObject serializedObject, bool destroyWithScene)
		{
			if (networkObject.IsSpawned)
			{
				global::UnityEngine.Debug.LogError($"[{networkObject.name}] Object-{networkObject.NetworkObjectId} is already spawned!");
				return;
			}
			SpawnNetworkObjectLocallyCommon(networkObject, serializedObject.NetworkObjectId, serializedObject.IsSceneObject, serializedObject.IsPlayerObject, serializedObject.OwnerClientId, destroyWithScene);
			networkObject.InvokeBehaviourNetworkPostSpawn();
			NetworkManager.DeferredMessageManager.ProcessTriggers(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnSpawn, networkObject.NetworkObjectId);
		}

		internal void SpawnNetworkObjectLocallyCommon(global::Unity.Netcode.NetworkObject networkObject, ulong networkId, bool sceneObject, bool playerObject, ulong ownerClientId, bool destroyWithScene)
		{
			if (networkObject.NetworkManagerOwner == null)
			{
				global::UnityEngine.Debug.LogError("NetworkManagerOwner should not be null!");
			}
			if (SpawnedObjects.ContainsKey(networkId))
			{
				global::UnityEngine.Debug.LogWarning(string.Format("[{0}] Trying to spawn {1} with a {2} of {3} but it is already in the spawned list!", NetworkManager.name, networkObject.name, "NetworkObjectId", networkId));
				return;
			}
			networkObject.IsSceneObject = sceneObject;
			if (networkObject.IsSceneObject != false && networkObject.SceneOriginHandle.IsEmpty())
			{
				networkObject.SceneOrigin = networkObject.gameObject.scene;
			}
			networkObject.NetworkObjectId = networkId;
			networkObject.DestroyWithScene = sceneObject || destroyWithScene;
			networkObject.IsPlayerObject = playerObject;
			networkObject.OwnerClientId = ownerClientId;
			networkObject.PreviousOwnerId = ownerClientId;
			if (NetworkManager.DistributedAuthorityMode && NetworkManager.LocalClientId == ownerClientId && playerObject)
			{
				networkObject.AddOwnershipExtended(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Locked);
			}
			networkObject.IsSpawned = true;
			SpawnedObjects.Add(networkObject.NetworkObjectId, networkObject);
			SpawnedObjectsList.Add(networkObject);
			if (!NetworkManager.DistributedAuthorityMode && NetworkManager.IsServer && networkObject.SpawnWithObservers)
			{
				if (!NetworkManager.IsHost)
				{
					networkObject.AddObserver(NetworkManager.LocalClientId);
				}
				for (int i = 0; i < NetworkManager.ConnectedClientsIds.Count; i++)
				{
					if (networkObject.CheckObjectVisibility == null || networkObject.CheckObjectVisibility(NetworkManager.ConnectedClientsIds[i]))
					{
						networkObject.AddObserver(NetworkManager.ConnectedClientsIds[i]);
					}
				}
			}
			networkObject.ApplyNetworkParenting();
			global::Unity.Netcode.NetworkObject.CheckOrphanChildren();
			AddNetworkObjectToSceneChangedUpdates(networkObject);
			networkObject.InvokeBehaviourNetworkSpawn();
			global::Unity.Netcode.NetworkObject[] componentsInChildren = networkObject.GetComponentsInChildren<global::Unity.Netcode.NetworkObject>();
			foreach (global::Unity.Netcode.NetworkObject networkObject2 in componentsInChildren)
			{
				if (!networkObject2.IsSceneObject.HasValue || networkObject2.IsSceneObject.Value)
				{
					networkObject2.IsSceneObject = sceneObject;
				}
			}
			if (!sceneObject)
			{
				networkObject.SubscribeToActiveSceneForSynch();
			}
			if (networkObject.IsPlayerObject)
			{
				UpdateNetworkClientPlayer(networkObject);
			}
			if (networkObject.IsSceneObject.Value && networkObject.InScenePlacedSourceGlobalObjectIdHash != 0)
			{
				networkObject.PrefabGlobalObjectIdHash = networkObject.InScenePlacedSourceGlobalObjectIdHash;
			}
		}

		internal void AddNetworkObjectToSceneChangedUpdates(global::Unity.Netcode.NetworkObject networkObject)
		{
			if (networkObject.SceneMigrationSynchronization && NetworkManager.NetworkConfig.EnableSceneManagement && !NetworkObjectsToSynchronizeSceneChanges.ContainsKey(networkObject.NetworkObjectId) && networkObject.UpdateForSceneChanges())
			{
				NetworkObjectsToSynchronizeSceneChanges.Add(networkObject.NetworkObjectId, networkObject);
			}
		}

		internal void RemoveNetworkObjectFromSceneChangedUpdates(global::Unity.Netcode.NetworkObject networkObject)
		{
			if (networkObject.SceneMigrationSynchronization && NetworkManager.NetworkConfig.EnableSceneManagement && NetworkObjectsToSynchronizeSceneChanges.ContainsKey(networkObject.NetworkObjectId))
			{
				NetworkObjectsToSynchronizeSceneChanges.Remove(networkObject.NetworkObjectId);
			}
		}

		internal void UpdateNetworkObjectSceneChanges()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.NetworkObject> networkObjectsToSynchronizeSceneChange in NetworkObjectsToSynchronizeSceneChanges)
			{
				if (!networkObjectsToSynchronizeSceneChange.Value.UpdateForSceneChanges())
				{
					CleanUpDisposedObjects.Push(networkObjectsToSynchronizeSceneChange.Key);
				}
			}
			while (CleanUpDisposedObjects.Count > 0)
			{
				NetworkObjectsToSynchronizeSceneChanges.Remove(CleanUpDisposedObjects.Pop());
			}
		}

		internal void SendSpawnCallForObject(ulong clientId, global::Unity.Netcode.NetworkObject networkObject)
		{
			if (NetworkManager.DistributedAuthorityMode)
			{
				_ = networkObject.SpawnWithObservers;
			}
			else
				_ = 0;
			if (clientId != 0L || NetworkManager.DistributedAuthorityMode)
			{
				global::Unity.Netcode.CreateObjectMessage message = new global::Unity.Netcode.CreateObjectMessage
				{
					ObjectInfo = networkObject.Serialize(clientId, NetworkManager.DistributedAuthorityMode),
					IncludesSerializedObject = true,
					UpdateObservers = NetworkManager.DistributedAuthorityMode,
					ObserverIds = (NetworkManager.DistributedAuthorityMode ? global::System.Linq.Enumerable.ToArray(networkObject.Observers) : null)
				};
				int num = NetworkManager.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.CreateObjectMessage>.DefaultDelivery, clientId);
				NetworkManager.NetworkMetrics.TrackObjectSpawnSent(clientId, networkObject, num);
			}
		}

		internal void SendSpawnCallForObserverUpdate(ulong[] newObservers, global::Unity.Netcode.NetworkObject networkObject)
		{
			if (!NetworkManager.DistributedAuthorityMode)
			{
				throw new global::System.Exception("[SendSpawnCallForObserverUpdate] Invoking a distributed authority only method when distributed authority is not enabled!");
			}
			global::Unity.Netcode.CreateObjectMessage message = new global::Unity.Netcode.CreateObjectMessage
			{
				ObjectInfo = networkObject.Serialize(0uL),
				ObserverIds = global::System.Linq.Enumerable.ToArray(networkObject.Observers),
				NewObserverIds = global::System.Linq.Enumerable.ToArray(newObservers),
				IncludesSerializedObject = true,
				UpdateObservers = true,
				UpdateNewObservers = true
			};
			int num = NetworkManager.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.CreateObjectMessage>.DefaultDelivery, 0uL);
			foreach (ulong receiverClientId in newObservers)
			{
				NetworkManager.NetworkMetrics.TrackObjectSpawnSent(receiverClientId, networkObject, num);
			}
		}

		internal ulong? GetSpawnParentId(global::Unity.Netcode.NetworkObject networkObject)
		{
			global::Unity.Netcode.NetworkObject networkObject2 = null;
			if (!networkObject.AlwaysReplicateAsRoot && networkObject.transform.parent != null)
			{
				networkObject2 = networkObject.transform.parent.GetComponent<global::Unity.Netcode.NetworkObject>();
			}
			if (networkObject2 == null)
			{
				return null;
			}
			return networkObject2.NetworkObjectId;
		}

		internal void DespawnObject(global::Unity.Netcode.NetworkObject networkObject, bool destroyObject = false, bool authorityOverride = false)
		{
			if (!NetworkManager.IsServer && !NetworkManager.DistributedAuthorityMode)
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer("Only server can despawn objects");
			}
			else if (NetworkManager.DistributedAuthorityMode && networkObject.OwnerClientId != NetworkManager.LocalClientId && (!NetworkManager.DAHost || (NetworkManager.DAHost && !authorityOverride)))
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer($"In distributed authority mode, only the owner of the NetworkObject can despawn it! Local Client is ({NetworkManager.LocalClientId}) while the owner is ({networkObject.OwnerClientId})");
			}
			else
			{
				OnDespawnObject(networkObject, destroyObject, authorityOverride);
			}
		}

		internal void ServerResetShudownStateForSceneObjects()
		{
			foreach (global::Unity.Netcode.NetworkObject item in global::System.Linq.Enumerable.Where(global::Unity.Netcode.FindObjects.ByType<global::Unity.Netcode.NetworkObject>(includeInactive: false, orderByIdentifier: true), (global::Unity.Netcode.NetworkObject c) => c.IsSceneObject.HasValue && c.IsSceneObject == true))
			{
				item.IsSpawned = false;
				item.DestroyWithScene = false;
				item.IsSceneObject = null;
			}
		}

		internal void ServerDestroySpawnedSceneObjects()
		{
			foreach (global::Unity.Netcode.NetworkObject item in global::System.Linq.Enumerable.ToList(SpawnedObjectsList))
			{
				if (item.IsSceneObject.HasValue && item.IsSceneObject.Value && item.DestroyWithScene && item.gameObject.scene != NetworkManager.SceneManager.DontDestroyOnLoadScene)
				{
					SpawnedObjectsList.Remove(item);
					global::UnityEngine.Object.Destroy(item.gameObject);
				}
			}
		}

		internal void DespawnAndDestroyNetworkObjects()
		{
			global::Unity.Netcode.NetworkObject[] array = global::Unity.Netcode.FindObjects.ByType<global::Unity.Netcode.NetworkObject>(includeInactive: false, orderByIdentifier: true);
			foreach (global::Unity.Netcode.NetworkObject networkObject in array)
			{
				if (networkObject.NetworkManager != NetworkManager)
				{
					continue;
				}
				networkObject.NetworkManagerOwner = NetworkManager;
				if (NetworkManager.PrefabHandler.ContainsHandler(networkObject))
				{
					OnDespawnObject(networkObject, destroyGameObject: false);
					NetworkManager.PrefabHandler.HandleNetworkPrefabDestroy(networkObject);
					continue;
				}
				bool flag = networkObject.IsSceneObject.HasValue && (!networkObject.IsSceneObject.HasValue || !networkObject.IsSceneObject.Value);
				if (flag)
				{
					global::Unity.Netcode.NetworkObject[] componentsInChildren = networkObject.GetComponentsInChildren<global::Unity.Netcode.NetworkObject>();
					foreach (global::Unity.Netcode.NetworkObject networkObject2 in componentsInChildren)
					{
						if (!(networkObject2 == networkObject) && networkObject2.IsSceneObject.HasValue && networkObject2.IsSceneObject.Value)
						{
							networkObject2.TryRemoveParent(networkObject2.WorldPositionStays());
						}
					}
				}
				OnDespawnObject(networkObject, flag);
			}
		}

		internal void DestroySceneObjects()
		{
			global::Unity.Netcode.NetworkObject[] array = global::Unity.Netcode.FindObjects.ByType<global::Unity.Netcode.NetworkObject>(includeInactive: false, orderByIdentifier: true);
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i].NetworkManager == NetworkManager) || (array[i].IsSceneObject.HasValue && !array[i].IsSceneObject.Value))
				{
					continue;
				}
				if (NetworkManager.PrefabHandler.ContainsHandler(array[i]))
				{
					if (SpawnedObjects.ContainsKey(array[i].NetworkObjectId))
					{
						OnDespawnObject(array[i], destroyGameObject: false);
					}
					else
					{
						NetworkManager.PrefabHandler.HandleNetworkPrefabDestroy(array[i]);
					}
				}
				else
				{
					global::UnityEngine.Object.Destroy(array[i].gameObject);
				}
			}
		}

		internal void ServerSpawnSceneObjectsOnStartSweep()
		{
			global::Unity.Netcode.NetworkObject[] array = global::Unity.Netcode.FindObjects.ByType<global::Unity.Netcode.NetworkObject>(includeInactive: false, orderByIdentifier: true);
			global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> list = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].NetworkManager == NetworkManager && (!array[i].IsSceneObject.HasValue || (array[i].IsSceneObject.HasValue && array[i].IsSceneObject.Value)))
				{
					ulong ownerClientId = array[i].OwnerClientId;
					if (NetworkManager.DistributedAuthorityMode)
					{
						ownerClientId = NetworkManager.LocalClientId;
					}
					AuthorityLocalSpawn(array[i], GetNetworkObjectId(), sceneObject: true, playerObject: false, ownerClientId, destroyWithScene: true);
					list.Add(array[i]);
				}
			}
			bool clearScenePlacedObjects = true;
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::UnityEngine.SceneManagement.Scene> item in NetworkManager.SceneManager.ScenesLoaded)
			{
				NetworkManager.SceneManager.PopulateScenePlacedObjects(item.Value, clearScenePlacedObjects);
				clearScenePlacedObjects = false;
			}
			foreach (global::Unity.Netcode.NetworkObject item2 in list)
			{
				item2.InternalInSceneNetworkObjectsSpawned();
			}
			list.Clear();
		}

		internal void OnDespawnNonAuthorityObject([global::System.Diagnostics.CodeAnalysis.NotNull] global::Unity.Netcode.NetworkObject networkObject, bool destroyGameObject)
		{
			if (networkObject.HasAuthority)
			{
				global::Unity.Netcode.NetworkLog.LogError($"OnDespawnNonAuthorityObject called on object {networkObject.NetworkObjectId} when is current client {NetworkManager.LocalClientId} has authority on this object.");
			}
			if (networkObject.IsSceneObject == false)
			{
				destroyGameObject = true;
			}
			OnDespawnObject(networkObject, destroyGameObject);
		}

		internal void OnDespawnObject(global::Unity.Netcode.NetworkObject networkObject, bool destroyGameObject, bool authorityOverride = false)
		{
			if (!NetworkManager)
			{
				return;
			}
			if (!networkObject)
			{
				global::Unity.Netcode.NetworkLog.LogWarning("Trying to destroy network object but it is null");
				return;
			}
			if (!SpawnedObjects.ContainsKey(networkObject.NetworkObjectId))
			{
				if (!NetworkManager.ShutdownInProgress && !NetworkManager.SceneManager.IsSceneEventInProgress())
				{
					global::Unity.Netcode.NetworkLog.LogWarning($"Trying to destroy object {networkObject.NetworkObjectId} but it doesn't seem to exist anymore!");
				}
				return;
			}
			bool distributedAuthorityMode = NetworkManager.DistributedAuthorityMode;
			if (!NetworkManager.ShutdownInProgress && (NetworkManager.IsServer || distributedAuthorityMode))
			{
				if (destroyGameObject && networkObject.IsSceneObject == true && !NetworkManager.SceneManager.IsSceneUnloading(networkObject))
				{
					global::Unity.Netcode.NetworkLog.LogWarning("Destroying in-scene network objects can lead to unexpected behavior. It is recommended to use NetworkObject.Despawn(false) instead.");
				}
				global::Unity.Netcode.NetworkObject[] componentsInChildren = networkObject.GetComponentsInChildren<global::Unity.Netcode.NetworkObject>();
				foreach (global::Unity.Netcode.NetworkObject networkObject2 in componentsInChildren)
				{
					if (networkObject2 == networkObject)
					{
						continue;
					}
					ulong? networkParenting = networkObject2.GetNetworkParenting();
					if (networkParenting.HasValue && networkParenting.Value != networkObject.NetworkObjectId)
					{
						continue;
					}
					networkObject2.AuthorityAppliedParenting = distributedAuthorityMode && !networkObject.HasAuthority;
					if (!networkObject2.TryRemoveParentCachedWorldPositionStays())
					{
						if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
						{
							global::Unity.Netcode.NetworkLog.LogError(string.Format("{0} #{1} could not be moved to the root when its parent {2} #{3} was being destroyed", "NetworkObject", networkObject2.NetworkObjectId, "NetworkObject", networkObject.NetworkObjectId));
						}
					}
					else if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogWarning(string.Format("{0} #{1} moved to the root because its parent {2} #{3} is destroyed", "NetworkObject", networkObject2.NetworkObjectId, "NetworkObject", networkObject.NetworkObjectId));
					}
				}
			}
			networkObject.InvokeBehaviourNetworkDespawn();
			bool flag = distributedAuthorityMode && (networkObject.HasAuthority || (NetworkManager.DAHost && authorityOverride));
			if (!NetworkManager.ShutdownInProgress && (flag || (!distributedAuthorityMode && NetworkManager.IsServer)))
			{
				if (NetworkManager.NetworkConfig.RecycleNetworkIds)
				{
					ReleasedNetworkObjectIds.Enqueue(new global::Unity.Netcode.ReleasedNetworkId
					{
						NetworkId = networkObject.NetworkObjectId,
						ReleaseTime = NetworkManager.RealTimeProvider.UnscaledTime
					});
				}
				m_TargetClientIds.Clear();
				if (flag && !NetworkManager.DAHost)
				{
					m_TargetClientIds.Add(0uL);
				}
				else
				{
					foreach (ulong connectedClientId in NetworkManager.ConnectionManager.ConnectedClientIds)
					{
						if ((!distributedAuthorityMode || connectedClientId != networkObject.OwnerClientId) && connectedClientId != NetworkManager.LocalClientId && networkObject.IsNetworkVisibleTo(connectedClientId))
						{
							m_TargetClientIds.Add(connectedClientId);
						}
					}
				}
				if (m_TargetClientIds.Count > 0)
				{
					global::Unity.Netcode.DestroyObjectMessage message = new global::Unity.Netcode.DestroyObjectMessage
					{
						NetworkObjectId = networkObject.NetworkObjectId,
						DeferredDespawnTick = networkObject.DeferredDespawnTick,
						DestroyGameObject = destroyGameObject,
						IsTargetedDestroy = false,
						IsDistributedAuthority = distributedAuthorityMode
					};
					global::Unity.Netcode.NetworkDelivery defaultDelivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.DestroyObjectMessage>.DefaultDelivery;
					foreach (ulong targetClientId in m_TargetClientIds)
					{
						int num = NetworkManager.ConnectionManager.SendMessage(ref message, defaultDelivery, targetClientId);
						NetworkManager.NetworkMetrics.TrackObjectDestroySent(targetClientId, networkObject, num);
					}
				}
			}
			networkObject.ResetOnDespawn();
			if (SpawnedObjects.Remove(networkObject.NetworkObjectId))
			{
				SpawnedObjectsList.Remove(networkObject);
			}
			if (networkObject.IsPlayerObject)
			{
				RemovePlayerObject(networkObject, destroyGameObject);
			}
			global::UnityEngine.GameObject gameObject = networkObject.gameObject;
			if (destroyGameObject && gameObject != null)
			{
				if (NetworkManager.PrefabHandler.ContainsHandler(networkObject))
				{
					NetworkManager.PrefabHandler.HandleNetworkPrefabDestroy(networkObject);
				}
				else
				{
					global::UnityEngine.Object.Destroy(gameObject);
				}
			}
		}

		internal void UpdateObservedNetworkObjects(ulong clientId)
		{
			foreach (global::Unity.Netcode.NetworkObject spawnedObjects in SpawnedObjectsList)
			{
				if (spawnedObjects.CheckObjectVisibility == null)
				{
					if (!spawnedObjects.Observers.Contains(clientId) && (spawnedObjects.SpawnWithObservers || clientId == 0L))
					{
						spawnedObjects.AddObserver(clientId);
					}
				}
				else if (spawnedObjects.CheckObjectVisibility(clientId))
				{
					spawnedObjects.AddObserver(clientId);
				}
				else
				{
					spawnedObjects.Observers.Remove(clientId);
				}
			}
		}

		internal void HandleNetworkObjectShow(bool forceSend = false)
		{
			bool flag = NetworkManager.DistributedAuthorityMode && !NetworkManager.DAHost;
			if ((flag && ClientsToShowObject.Count == 0) || (!flag && ObjectsToShowToClient.Count == 0))
			{
				return;
			}
			if (flag)
			{
				global::Unity.Netcode.NetworkBehaviourUpdater behaviourUpdater = NetworkManager.BehaviourUpdater;
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkObject, global::System.Collections.Generic.List<ulong>> item in ClientsToShowObject)
				{
					if (!(item.Key != null) || !item.Key.IsSpawned)
					{
						continue;
					}
					try
					{
						behaviourUpdater.ForceSendIfDirtyOnNetworkShow(item.Key);
						SendSpawnCallForObserverUpdate(item.Value.ToArray(), item.Key);
					}
					catch (global::System.Exception exception)
					{
						if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
						{
							global::UnityEngine.Debug.LogException(exception);
						}
					}
				}
				ClientsToShowObject.Clear();
				ObjectsToShowToClient.Clear();
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>> item2 in ObjectsToShowToClient)
			{
				ulong key = item2.Key;
				foreach (global::Unity.Netcode.NetworkObject item3 in item2.Value)
				{
					if (!(item3 != null) || !item3.IsSpawned)
					{
						continue;
					}
					try
					{
						if (forceSend)
						{
							NetworkManager.BehaviourUpdater.ForceSendIfDirtyOnNetworkShow(item3);
						}
						SendSpawnCallForObject(key, item3);
					}
					catch (global::System.Exception exception2)
					{
						if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
						{
							global::UnityEngine.Debug.LogException(exception2);
						}
					}
				}
			}
			ObjectsToShowToClient.Clear();
		}

		internal NetworkSpawnManager(global::Unity.Netcode.NetworkManager networkManager)
		{
			NetworkManager = networkManager;
		}

		~NetworkSpawnManager()
		{
			Shutdown();
		}

		internal void Shutdown()
		{
			NetworkObjectsToSynchronizeSceneChanges.Clear();
			CleanUpDisposedObjects.Clear();
		}

		internal void GetObjectDistribution(ulong clientId, ref global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>> objectByTypeAndOwner, ref global::System.Collections.Generic.Dictionary<uint, int> objectTypeCount)
		{
			bool cMBServiceConnection = NetworkManager.CMBServiceConnection;
			foreach (global::Unity.Netcode.NetworkObject spawnedObjects in NetworkManager.SpawnManager.SpawnedObjectsList)
			{
				if (spawnedObjects.IsOwnershipSessionOwner)
				{
					continue;
				}
				if (spawnedObjects.transform.parent != null)
				{
					global::Unity.Netcode.NetworkObject component = spawnedObjects.transform.parent.GetComponent<global::Unity.Netcode.NetworkObject>();
					if (component != null && component.OwnerClientId == spawnedObjects.OwnerClientId && (spawnedObjects.IsOwnershipDistributable || spawnedObjects.IsOwnershipTransferable))
					{
						continue;
					}
				}
				if (!spawnedObjects.IsOwnershipDistributable || spawnedObjects.IsOwnershipLocked || !spawnedObjects.Observers.Contains(clientId))
				{
					continue;
				}
				uint key = ((spawnedObjects.IsSceneObject.HasValue && spawnedObjects.IsSceneObject.Value) ? spawnedObjects.InScenePlacedSourceGlobalObjectIdHash : spawnedObjects.GlobalObjectIdHash);
				if (!objectTypeCount.ContainsKey(key))
				{
					objectTypeCount.Add(key, 0);
				}
				objectTypeCount[key]++;
				if (!cMBServiceConnection || spawnedObjects.IsOwner)
				{
					if (!objectByTypeAndOwner.ContainsKey(key))
					{
						objectByTypeAndOwner.Add(key, new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>());
					}
					if (!objectByTypeAndOwner[key].ContainsKey(spawnedObjects.OwnerClientId))
					{
						objectByTypeAndOwner[key].Add(spawnedObjects.OwnerClientId, new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>());
					}
					objectByTypeAndOwner[key][spawnedObjects.OwnerClientId].Add(spawnedObjects);
				}
			}
		}

		internal void DistributeNetworkObjects(ulong clientId)
		{
			if (!NetworkManager.DistributedAuthorityMode || NetworkManager.SessionConfig.ServiceSideDistribution)
			{
				return;
			}
			global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>> objectByTypeAndOwner = new global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>>();
			global::System.Collections.Generic.Dictionary<uint, int> objectTypeCount = new global::System.Collections.Generic.Dictionary<uint, int>();
			GetObjectDistribution(clientId, ref objectByTypeAndOwner, ref objectTypeCount);
			int count = NetworkManager.ConnectedClientsIds.Count;
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>> item in objectByTypeAndOwner)
			{
				int num = objectTypeCount[item.Key];
				float num2 = (float)num * (1f / (float)count);
				int num3 = (int)global::System.Math.Floor(num2);
				float num4 = num2 - (float)num3;
				int num5 = 0;
				num5 = ((!(num4 >= 0.556f)) ? num3 : ((int)global::System.Math.Round((float)num * (1f / (float)count))));
				if (num5 <= 0)
				{
					continue;
				}
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>> item2 in item.Value)
				{
					if (item2.Value.Count <= 1)
					{
						continue;
					}
					int num6 = global::UnityEngine.Mathf.Max(item2.Value.Count - num5, 1);
					int num7 = 0;
					int num8 = global::UnityEngine.Mathf.Max((int)global::System.Math.Round((float)(item2.Value.Count / num5)), 1);
					if (EnableDistributeLogging)
					{
						global::UnityEngine.Debug.Log($"[{num5} of {num}][Client-{item2.Key}] Count: {item2.Value.Count} | ObjPerClient: {num5} | maxD: {num6} | Offset: {num8}");
					}
					for (int i = 0; i < item2.Value.Count; i++)
					{
						if (i % num8 == 0)
						{
							global::Unity.Netcode.NetworkObject[] componentsInChildren = item2.Value[i].GetComponentsInChildren<global::Unity.Netcode.NetworkObject>();
							foreach (global::Unity.Netcode.NetworkObject networkObject in componentsInChildren)
							{
								if (!(networkObject == item2.Value[i]) && networkObject.OwnerClientId == item2.Value[i].OwnerClientId && networkObject.OwnerClientId != clientId && networkObject.Observers.Contains(clientId))
								{
									if (!networkObject.IsOwnershipDistributable || !networkObject.IsOwnershipTransferable)
									{
										global::Unity.Netcode.NetworkLog.LogWarning("Sibling " + networkObject.name + " of root parent " + item2.Value[i].name + " is neither transferable or distributable! Object distribution skipped and could lead to a potentially un-owned or owner-mismatched NetworkObject!");
									}
									else
									{
										ChangeOwnership(networkObject, clientId, isAuthorized: true);
									}
								}
							}
							ChangeOwnership(item2.Value[i], clientId, isAuthorized: true);
							if (EnableDistributeLogging)
							{
								global::UnityEngine.Debug.Log($"[Client-{item2.Key}][NetworkObjectId-{item2.Value[i].NetworkObjectId} Distributed to Client-{clientId}");
							}
							num7++;
						}
						if (num7 == num6)
						{
							break;
						}
					}
				}
			}
			if (!EnableDistributeLogging)
			{
				return;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			objectByTypeAndOwner.Clear();
			objectTypeCount.Clear();
			GetObjectDistribution(clientId, ref objectByTypeAndOwner, ref objectTypeCount);
			stringBuilder.AppendLine("Client Relative Distributed Object Count: (distribution follows)");
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>> item3 in objectByTypeAndOwner)
			{
				stringBuilder.AppendLine($"[GID: {item3.Key} | {(global::System.Linq.Enumerable.First(global::System.Linq.Enumerable.First(item3.Value).Value).name)}][Total Count: {objectTypeCount[item3.Key]}]");
				stringBuilder.AppendLine($"[GID: {item3.Key} | {(global::System.Linq.Enumerable.First(global::System.Linq.Enumerable.First(item3.Value).Value).name)}] Distribution:");
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>> item4 in item3.Value)
				{
					stringBuilder.AppendLine($"[Client-{item4.Key}] Count: {item4.Value.Count}");
				}
			}
			global::UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		internal void DeferDespawnNetworkObject(ulong networkObjectId, int tickToDespawn, bool hasDeferredDespawnCheck, bool destroyGameObject)
		{
			global::Unity.Netcode.NetworkSpawnManager.DeferredDespawnObject item = new global::Unity.Netcode.NetworkSpawnManager.DeferredDespawnObject
			{
				TickToDespawn = tickToDespawn,
				HasDeferredDespawnCheck = hasDeferredDespawnCheck,
				DestroyGameObject = destroyGameObject,
				NetworkObjectId = networkObjectId
			};
			DeferredDespawnObjects.Add(item);
		}

		internal void DeferredDespawnUpdate(global::Unity.Netcode.NetworkTime serverTime)
		{
			if (DeferredDespawnObjects.Count == 0)
			{
				return;
			}
			int tick = serverTime.Tick;
			int count = DeferredDespawnObjects.Count;
			for (int i = 0; i < count; i++)
			{
				global::Unity.Netcode.NetworkSpawnManager.DeferredDespawnObject deferredDespawnObject = DeferredDespawnObjects[i];
				if (!deferredDespawnObject.HasDeferredDespawnCheck || !SpawnedObjects.TryGetValue(deferredDespawnObject.NetworkObjectId, out var value))
				{
					continue;
				}
				if (value.OnDeferredDespawnComplete != null)
				{
					if (value.OnDeferredDespawnComplete())
					{
						deferredDespawnObject.TickToDespawn = tick;
					}
					else
					{
						deferredDespawnObject.TickToDespawn = value.DeferredDespawnTick;
					}
				}
				else
				{
					deferredDespawnObject.HasDeferredDespawnCheck = false;
				}
			}
			for (int num = count - 1; num >= 0; num--)
			{
				global::Unity.Netcode.NetworkSpawnManager.DeferredDespawnObject deferredDespawnObject2 = DeferredDespawnObjects[num];
				if (deferredDespawnObject2.TickToDespawn < tick)
				{
					if (SpawnedObjects.TryGetValue(deferredDespawnObject2.NetworkObjectId, out var value2))
					{
						OnDespawnNonAuthorityObject(value2, deferredDespawnObject2.DestroyGameObject);
					}
					DeferredDespawnObjects.RemoveAt(num);
				}
			}
		}

		internal void NotifyNetworkObjectsSynchronized()
		{
			foreach (global::Unity.Netcode.NetworkObject item in global::System.Linq.Enumerable.ToList(SpawnedObjectsList))
			{
				item.InternalNetworkSessionSynchronized();
			}
		}

		internal void ShowHiddenObjectsToNewlyJoinedClient(ulong newClientId)
		{
			if (NetworkManager == null || (NetworkManager.ShutdownInProgress && NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer))
			{
				global::UnityEngine.Debug.LogWarning("[Internal Error] ShowHiddenObjectsToNewlyJoinedClient invoked while shutdown is in progress!");
				return;
			}
			if (!NetworkManager.DistributedAuthorityMode)
			{
				global::UnityEngine.Debug.LogError("[Internal Error] ShowHiddenObjectsToNewlyJoinedClient should only be invoked when using a distributed authority network topology!");
				return;
			}
			if (NetworkManager.LocalClient.IsSessionOwner)
			{
				global::UnityEngine.Debug.LogError("[Internal Error] ShowHiddenObjectsToNewlyJoinedClient should only be invoked on a non-session owner client!");
				return;
			}
			ulong clientId = NetworkManager.LocalClient.ClientId;
			ulong currentSessionOwner = NetworkManager.CurrentSessionOwner;
			foreach (global::Unity.Netcode.NetworkObject spawnedObjects in SpawnedObjectsList)
			{
				if (!spawnedObjects.SpawnWithObservers || spawnedObjects.OwnerClientId != clientId || spawnedObjects.Observers.Contains(currentSessionOwner))
				{
					continue;
				}
				if (spawnedObjects.Observers.Contains(newClientId))
				{
					if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::UnityEngine.Debug.LogWarning("[" + spawnedObjects.name + "] Has new client as an observer but it is hidden from the session owner!");
					}
					spawnedObjects.Observers.Remove(newClientId);
				}
				spawnedObjects.NetworkShow(newClientId);
			}
		}

		internal void SynchronizeObjectsToNewlyJoinedClient(ulong newClientId)
		{
			if (NetworkManager == null || (NetworkManager.ShutdownInProgress && NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer))
			{
				global::UnityEngine.Debug.LogWarning("[Internal Error] SynchronizeObjectsToNewlyJoinedClient invoked while shutdown is in progress!");
				return;
			}
			if (!NetworkManager.DistributedAuthorityMode)
			{
				global::UnityEngine.Debug.LogError("[Internal Error] SynchronizeObjectsToNewlyJoinedClient should only be invoked when using a distributed authority network topology!");
				return;
			}
			if (NetworkManager.NetworkConfig.EnableSceneManagement)
			{
				global::UnityEngine.Debug.LogError("[Internal Error] SynchronizeObjectsToNewlyJoinedClient should only be invoked when scene management is disabled!");
				return;
			}
			ulong clientId = NetworkManager.LocalClient.ClientId;
			foreach (global::Unity.Netcode.NetworkObject spawnedObjects in SpawnedObjectsList)
			{
				if (spawnedObjects.SpawnWithObservers && spawnedObjects.OwnerClientId == clientId && !spawnedObjects.Observers.Contains(newClientId))
				{
					spawnedObjects.NetworkShow(newClientId);
				}
			}
		}
	}
}
