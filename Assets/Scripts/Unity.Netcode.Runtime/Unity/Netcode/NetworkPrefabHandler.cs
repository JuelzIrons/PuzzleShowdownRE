namespace Unity.Netcode
{
	public class NetworkPrefabHandler
	{
		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		private readonly global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.INetworkPrefabInstanceHandler> m_PrefabAssetToPrefabHandler = new global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.INetworkPrefabInstanceHandler>();

		private readonly global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.INetworkPrefabInstanceHandlerWithData> m_PrefabAssetToPrefabHandlerWithData = new global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.INetworkPrefabInstanceHandlerWithData>();

		private readonly global::System.Collections.Generic.Dictionary<uint, uint> m_PrefabInstanceToPrefabAsset = new global::System.Collections.Generic.Dictionary<uint, uint>();

		internal static string PrefabDebugHelper(global::Unity.Netcode.NetworkPrefab networkPrefab)
		{
			return "NetworkPrefab \"" + networkPrefab.Prefab.name + "\"";
		}

		public bool AddHandler(global::UnityEngine.GameObject networkPrefabAsset, global::Unity.Netcode.INetworkPrefabInstanceHandler instanceHandler)
		{
			return AddHandler(networkPrefabAsset.GetComponent<global::Unity.Netcode.NetworkObject>().GlobalObjectIdHash, instanceHandler);
		}

		public bool AddHandler(global::Unity.Netcode.NetworkObject prefabAssetNetworkObject, global::Unity.Netcode.INetworkPrefabInstanceHandler instanceHandler)
		{
			return AddHandler(prefabAssetNetworkObject.GlobalObjectIdHash, instanceHandler);
		}

		public bool AddHandler(uint globalObjectIdHash, global::Unity.Netcode.INetworkPrefabInstanceHandler instanceHandler)
		{
			if (!m_PrefabAssetToPrefabHandler.ContainsKey(globalObjectIdHash))
			{
				m_PrefabAssetToPrefabHandler.Add(globalObjectIdHash, instanceHandler);
				if (instanceHandler is global::Unity.Netcode.INetworkPrefabInstanceHandlerWithData value)
				{
					m_PrefabAssetToPrefabHandlerWithData.Add(globalObjectIdHash, value);
				}
				return true;
			}
			return false;
		}

		public void SetInstantiationData<T>(global::UnityEngine.GameObject gameObject, T instantiationData) where T : struct, global::Unity.Netcode.INetworkSerializable
		{
			if (gameObject.TryGetComponent<global::Unity.Netcode.NetworkObject>(out var component))
			{
				SetInstantiationData(component, instantiationData);
			}
		}

		public void SetInstantiationData<T>(global::Unity.Netcode.NetworkObject networkObject, T instantiationData) where T : struct, global::Unity.Netcode.INetworkSerializable
		{
			if (!TryGetHandlerWithData(networkObject.GlobalObjectIdHash, out var handler) || !handler.HandlesDataType<T>())
			{
				global::UnityEngine.Debug.LogError("[InstantiationData] Cannot inject data: no compatible handler found for the specified data type.");
				return;
			}
			using global::Unity.Netcode.FastBufferWriter writer = new global::Unity.Netcode.FastBufferWriter(4, global::Unity.Collections.Allocator.Temp, int.MaxValue);
			global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter> serializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter>(new global::Unity.Netcode.BufferSerializerWriter(writer));
			try
			{
				instantiationData.NetworkSerialize(serializer);
				networkObject.InstantiationData = writer.ToArray();
			}
			catch (global::System.Exception arg)
			{
				global::Unity.Netcode.NetworkLog.LogError(string.Format("[InstantiationData] Failed to serialize instantiation data for {0} '{1}': {2}", "NetworkObject", networkObject.name, arg));
			}
		}

		public void RegisterHostGlobalObjectIdHashValues(global::UnityEngine.GameObject sourceNetworkPrefab, global::System.Collections.Generic.List<global::UnityEngine.GameObject> networkPrefabOverrides)
		{
			if (global::Unity.Netcode.NetworkManager.Singleton.IsListening)
			{
				if (global::Unity.Netcode.NetworkManager.Singleton.IsHost)
				{
					global::Unity.Netcode.NetworkObject component = sourceNetworkPrefab.GetComponent<global::Unity.Netcode.NetworkObject>();
					if (sourceNetworkPrefab != null)
					{
						uint globalObjectIdHash = component.GlobalObjectIdHash;
						{
							foreach (global::UnityEngine.GameObject networkPrefabOverride in networkPrefabOverrides)
							{
								if (networkPrefabOverride.TryGetComponent<global::Unity.Netcode.NetworkObject>(out var component2))
								{
									if (!m_PrefabInstanceToPrefabAsset.ContainsKey(component2.GlobalObjectIdHash))
									{
										m_PrefabInstanceToPrefabAsset.Add(component2.GlobalObjectIdHash, globalObjectIdHash);
									}
									else
									{
										global::UnityEngine.Debug.LogWarning(component2.name + " appears to be a duplicate entry!");
									}
									continue;
								}
								throw new global::System.Exception(component2.name + " does not have a NetworkObject component!");
							}
							return;
						}
					}
					throw new global::System.Exception(sourceNetworkPrefab.name + " does not have a NetworkObject component!");
				}
				throw new global::System.Exception("You should only call RegisterHostGlobalObjectIdHashValues as a Host!");
			}
			throw new global::System.Exception("You can only call RegisterHostGlobalObjectIdHashValues once NetworkManager is listening!");
		}

		public bool RemoveHandler(global::UnityEngine.GameObject networkPrefabAsset)
		{
			return RemoveHandler(networkPrefabAsset.GetComponent<global::Unity.Netcode.NetworkObject>().GlobalObjectIdHash);
		}

		public bool RemoveHandler(global::Unity.Netcode.NetworkObject networkObject)
		{
			return RemoveHandler(networkObject.GlobalObjectIdHash);
		}

		public bool RemoveHandler(uint globalObjectIdHash)
		{
			if (m_PrefabInstanceToPrefabAsset.ContainsValue(globalObjectIdHash))
			{
				uint key = 0u;
				foreach (global::System.Collections.Generic.KeyValuePair<uint, uint> item in m_PrefabInstanceToPrefabAsset)
				{
					if (item.Value == globalObjectIdHash)
					{
						key = item.Key;
						break;
					}
				}
				m_PrefabInstanceToPrefabAsset.Remove(key);
			}
			m_PrefabAssetToPrefabHandlerWithData.Remove(globalObjectIdHash);
			return m_PrefabAssetToPrefabHandler.Remove(globalObjectIdHash);
		}

		internal bool ContainsHandler(global::UnityEngine.GameObject networkPrefab)
		{
			return ContainsHandler(networkPrefab.GetComponent<global::Unity.Netcode.NetworkObject>().GlobalObjectIdHash);
		}

		internal bool ContainsHandler(global::Unity.Netcode.NetworkObject networkObject)
		{
			return ContainsHandler(networkObject.GlobalObjectIdHash);
		}

		internal bool ContainsHandler(uint networkPrefabHash)
		{
			if (!m_PrefabAssetToPrefabHandler.ContainsKey(networkPrefabHash))
			{
				return m_PrefabInstanceToPrefabAsset.ContainsKey(networkPrefabHash);
			}
			return true;
		}

		internal bool TryGetHandlerWithData(uint objectHash, out global::Unity.Netcode.INetworkPrefabInstanceHandlerWithData handler)
		{
			return m_PrefabAssetToPrefabHandlerWithData.TryGetValue(objectHash, out handler);
		}

		internal uint GetSourceGlobalObjectIdHash(uint networkPrefabHash)
		{
			if (m_PrefabAssetToPrefabHandler.ContainsKey(networkPrefabHash))
			{
				return networkPrefabHash;
			}
			if (m_PrefabInstanceToPrefabAsset.TryGetValue(networkPrefabHash, out var value))
			{
				return value;
			}
			return 0u;
		}

		internal global::Unity.Netcode.NetworkObject HandleNetworkPrefabSpawn(uint networkPrefabAssetHash, ulong ownerClientId, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation, byte[] instantiationData = null)
		{
			global::Unity.Netcode.NetworkObject networkObject = null;
			global::Unity.Netcode.INetworkPrefabInstanceHandler value2;
			if (instantiationData != null)
			{
				if (!m_PrefabAssetToPrefabHandlerWithData.TryGetValue(networkPrefabAssetHash, out var value))
				{
					global::UnityEngine.Debug.LogError($"[InstantiationData] Failed instantiate with data: no compatible data handler found for object hash {networkPrefabAssetHash}. Instantiation data will be dropped.");
					return null;
				}
				networkObject = value.Instantiate(ownerClientId, position, rotation, instantiationData);
			}
			else if (m_PrefabAssetToPrefabHandler.TryGetValue(networkPrefabAssetHash, out value2))
			{
				networkObject = value2.Instantiate(ownerClientId, position, rotation);
			}
			if (networkObject != null)
			{
				m_PrefabInstanceToPrefabAsset.TryAdd(networkObject.GlobalObjectIdHash, networkPrefabAssetHash);
			}
			return networkObject;
		}

		internal void HandleNetworkPrefabDestroy(global::Unity.Netcode.NetworkObject networkObjectInstance)
		{
			uint globalObjectIdHash = networkObjectInstance.GlobalObjectIdHash;
			global::Unity.Netcode.INetworkPrefabInstanceHandler value3;
			if (m_PrefabInstanceToPrefabAsset.TryGetValue(globalObjectIdHash, out var value))
			{
				if (m_PrefabAssetToPrefabHandler.TryGetValue(value, out var value2))
				{
					value2.Destroy(networkObjectInstance);
				}
			}
			else if (m_PrefabAssetToPrefabHandler.TryGetValue(globalObjectIdHash, out value3))
			{
				value3.Destroy(networkObjectInstance);
			}
		}

		public global::UnityEngine.GameObject GetNetworkPrefabOverride(global::UnityEngine.GameObject gameObject)
		{
			if (gameObject.TryGetComponent<global::Unity.Netcode.NetworkObject>(out var component) && m_NetworkManager.NetworkConfig.Prefabs.NetworkPrefabOverrideLinks.ContainsKey(component.GlobalObjectIdHash))
			{
				global::Unity.Netcode.NetworkPrefabOverride networkPrefabOverride = m_NetworkManager.NetworkConfig.Prefabs.NetworkPrefabOverrideLinks[component.GlobalObjectIdHash].Override;
				if ((uint)(networkPrefabOverride - 1) <= 1u)
				{
					return m_NetworkManager.NetworkConfig.Prefabs.NetworkPrefabOverrideLinks[component.GlobalObjectIdHash].OverridingTargetPrefab;
				}
			}
			return gameObject;
		}

		public void AddNetworkPrefab(global::UnityEngine.GameObject prefab)
		{
			if (m_NetworkManager.IsListening && m_NetworkManager.NetworkConfig.ForceSamePrefabs)
			{
				throw new global::System.Exception("All prefabs must be registered before starting NetworkManager when ForceSamePrefabs is enabled.");
			}
			global::Unity.Netcode.NetworkObject component = prefab.GetComponent<global::Unity.Netcode.NetworkObject>();
			if (!component)
			{
				throw new global::System.Exception("All NetworkPrefabs must contain a NetworkObject component.");
			}
			global::Unity.Netcode.NetworkPrefab networkPrefab = new global::Unity.Netcode.NetworkPrefab
			{
				Prefab = prefab
			};
			bool flag = m_NetworkManager.NetworkConfig.Prefabs.Add(networkPrefab);
			if (m_NetworkManager.IsListening && flag)
			{
				m_NetworkManager.DeferredMessageManager.ProcessTriggers(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnAddPrefab, component.GlobalObjectIdHash);
			}
		}

		public void RemoveNetworkPrefab(global::UnityEngine.GameObject prefab)
		{
			if (m_NetworkManager.IsListening && m_NetworkManager.NetworkConfig.ForceSamePrefabs)
			{
				throw new global::System.Exception("Prefabs cannot be removed after starting NetworkManager when ForceSamePrefabs is enabled.");
			}
			uint globalObjectIdHash = prefab.GetComponent<global::Unity.Netcode.NetworkObject>().GlobalObjectIdHash;
			m_NetworkManager.NetworkConfig.Prefabs.Remove(prefab);
			if (ContainsHandler(globalObjectIdHash))
			{
				RemoveHandler(globalObjectIdHash);
			}
		}

		internal void RegisterPlayerPrefab()
		{
			global::Unity.Netcode.NetworkConfig networkConfig = m_NetworkManager.NetworkConfig;
			if (!(networkConfig.PlayerPrefab != null))
			{
				return;
			}
			if (networkConfig.PlayerPrefab.TryGetComponent<global::Unity.Netcode.NetworkObject>(out var component))
			{
				if (!networkConfig.Prefabs.NetworkPrefabOverrideLinks.ContainsKey(component.GlobalObjectIdHash))
				{
					AddNetworkPrefab(networkConfig.PlayerPrefab);
				}
			}
			else
			{
				global::UnityEngine.Debug.LogError("PlayerPrefab (\"" + networkConfig.PlayerPrefab.name + "\") has no NetworkObject assigned to it!.");
			}
		}

		internal void Initialize(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_NetworkManager = networkManager;
		}

		internal void Shutdown()
		{
			m_PrefabInstanceToPrefabAsset.Clear();
		}
	}
}
