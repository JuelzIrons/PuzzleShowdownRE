namespace Unity.Netcode
{
	[global::System.Serializable]
	public class NetworkPrefabs
	{
		[global::UnityEngine.SerializeField]
		public global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefabsList> NetworkPrefabsLists = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefabsList>();

		[global::System.NonSerialized]
		public global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkPrefab> NetworkPrefabOverrideLinks = new global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkPrefab>();

		[global::System.NonSerialized]
		public global::System.Collections.Generic.Dictionary<uint, uint> OverrideToNetworkPrefab = new global::System.Collections.Generic.Dictionary<uint, uint>();

		[global::System.NonSerialized]
		private global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab> m_Prefabs = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab>();

		[global::System.NonSerialized]
		private global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab> m_RuntimeAddedPrefabs = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab>();

		public global::System.Collections.Generic.IReadOnlyList<global::Unity.Netcode.NetworkPrefab> Prefabs => m_Prefabs;

		private void AddTriggeredByNetworkPrefabList(global::Unity.Netcode.NetworkPrefab networkPrefab)
		{
			if (AddPrefabRegistration(networkPrefab))
			{
				m_Prefabs.Add(networkPrefab);
			}
		}

		private void RemoveTriggeredByNetworkPrefabList(global::Unity.Netcode.NetworkPrefab networkPrefab)
		{
			m_Prefabs.Remove(networkPrefab);
		}

		~NetworkPrefabs()
		{
			Shutdown();
		}

		internal void Shutdown()
		{
			foreach (global::Unity.Netcode.NetworkPrefabsList networkPrefabsList in NetworkPrefabsLists)
			{
				networkPrefabsList.OnAdd = (global::Unity.Netcode.NetworkPrefabsList.OnAddDelegate)global::System.Delegate.Remove(networkPrefabsList.OnAdd, new global::Unity.Netcode.NetworkPrefabsList.OnAddDelegate(AddTriggeredByNetworkPrefabList));
				networkPrefabsList.OnRemove = (global::Unity.Netcode.NetworkPrefabsList.OnRemoveDelegate)global::System.Delegate.Remove(networkPrefabsList.OnRemove, new global::Unity.Netcode.NetworkPrefabsList.OnRemoveDelegate(RemoveTriggeredByNetworkPrefabList));
			}
		}

		public void Initialize(bool warnInvalid = true)
		{
			m_Prefabs.Clear();
			NetworkPrefabsLists.RemoveAll((global::Unity.Netcode.NetworkPrefabsList x) => x == null);
			foreach (global::Unity.Netcode.NetworkPrefabsList networkPrefabsList in NetworkPrefabsLists)
			{
				networkPrefabsList.OnAdd = (global::Unity.Netcode.NetworkPrefabsList.OnAddDelegate)global::System.Delegate.Combine(networkPrefabsList.OnAdd, new global::Unity.Netcode.NetworkPrefabsList.OnAddDelegate(AddTriggeredByNetworkPrefabList));
				networkPrefabsList.OnRemove = (global::Unity.Netcode.NetworkPrefabsList.OnRemoveDelegate)global::System.Delegate.Combine(networkPrefabsList.OnRemove, new global::Unity.Netcode.NetworkPrefabsList.OnRemoveDelegate(RemoveTriggeredByNetworkPrefabList));
			}
			NetworkPrefabOverrideLinks.Clear();
			OverrideToNetworkPrefab.Clear();
			global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab> list = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab>();
			if (NetworkPrefabsLists.Count != 0)
			{
				foreach (global::Unity.Netcode.NetworkPrefabsList networkPrefabsList2 in NetworkPrefabsLists)
				{
					list.AddRange(networkPrefabsList2.PrefabList);
				}
			}
			m_Prefabs = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab>();
			global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab> list2 = null;
			if (warnInvalid)
			{
				list2 = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab>();
			}
			foreach (global::Unity.Netcode.NetworkPrefab item in list)
			{
				if (AddPrefabRegistration(item))
				{
					m_Prefabs.Add(item);
				}
				else
				{
					list2?.Add(item);
				}
			}
			foreach (global::Unity.Netcode.NetworkPrefab runtimeAddedPrefab in m_RuntimeAddedPrefabs)
			{
				if (AddPrefabRegistration(runtimeAddedPrefab))
				{
					m_Prefabs.Add(runtimeAddedPrefab);
				}
				else
				{
					list2?.Add(runtimeAddedPrefab);
				}
			}
			if (list2 != null && list2.Count > 0 && global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
			{
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder("Removing invalid prefabs from Network Prefab registration: ");
				stringBuilder.AppendJoin(", ", list2);
				global::Unity.Netcode.NetworkLog.LogWarning(stringBuilder.ToString());
			}
		}

		public bool Add(global::Unity.Netcode.NetworkPrefab networkPrefab)
		{
			if (AddPrefabRegistration(networkPrefab))
			{
				m_Prefabs.Add(networkPrefab);
				m_RuntimeAddedPrefabs.Add(networkPrefab);
				return true;
			}
			return false;
		}

		public void Remove(global::Unity.Netcode.NetworkPrefab prefab)
		{
			if (prefab == null)
			{
				throw new global::System.ArgumentNullException("prefab");
			}
			m_Prefabs.Remove(prefab);
			m_RuntimeAddedPrefabs.Remove(prefab);
			OverrideToNetworkPrefab.Remove(prefab.TargetPrefabGlobalObjectIdHash);
			NetworkPrefabOverrideLinks.Remove(prefab.SourcePrefabGlobalObjectIdHash);
		}

		public void Remove(global::UnityEngine.GameObject prefab)
		{
			if (prefab == null)
			{
				throw new global::System.ArgumentNullException("prefab");
			}
			for (int i = 0; i < m_Prefabs.Count; i++)
			{
				if (m_Prefabs[i].Prefab == prefab)
				{
					Remove(m_Prefabs[i]);
					return;
				}
			}
			for (int j = 0; j < m_RuntimeAddedPrefabs.Count; j++)
			{
				if (m_RuntimeAddedPrefabs[j].Prefab == prefab)
				{
					Remove(m_RuntimeAddedPrefabs[j]);
					break;
				}
			}
		}

		public bool Contains(global::UnityEngine.GameObject prefab)
		{
			for (int i = 0; i < m_Prefabs.Count; i++)
			{
				if (m_Prefabs[i].Prefab == prefab || m_Prefabs[i].SourcePrefabToOverride == prefab)
				{
					return true;
				}
			}
			return false;
		}

		public bool Contains(global::Unity.Netcode.NetworkPrefab prefab)
		{
			for (int i = 0; i < m_Prefabs.Count; i++)
			{
				if (m_Prefabs[i].Equals(prefab))
				{
					return true;
				}
			}
			return false;
		}

		private bool AddPrefabRegistration(global::Unity.Netcode.NetworkPrefab networkPrefab)
		{
			if (networkPrefab == null)
			{
				return false;
			}
			if (!networkPrefab.Validate())
			{
				return false;
			}
			uint sourcePrefabGlobalObjectIdHash = networkPrefab.SourcePrefabGlobalObjectIdHash;
			uint targetPrefabGlobalObjectIdHash = networkPrefab.TargetPrefabGlobalObjectIdHash;
			if (NetworkPrefabOverrideLinks.ContainsKey(sourcePrefabGlobalObjectIdHash))
			{
				global::Unity.Netcode.NetworkObject component = networkPrefab.Prefab.GetComponent<global::Unity.Netcode.NetworkObject>();
				global::UnityEngine.Debug.LogError(string.Format("{0} ({1}) has a duplicate {2} source entry value of: {3}!", "NetworkPrefab", component.name, "GlobalObjectIdHash", sourcePrefabGlobalObjectIdHash));
				return false;
			}
			if (networkPrefab.Override == global::Unity.Netcode.NetworkPrefabOverride.None)
			{
				NetworkPrefabOverrideLinks.Add(sourcePrefabGlobalObjectIdHash, networkPrefab);
				return true;
			}
			global::Unity.Netcode.NetworkPrefabOverride networkPrefabOverride = networkPrefab.Override;
			if ((uint)(networkPrefabOverride - 1) <= 1u)
			{
				NetworkPrefabOverrideLinks.Add(sourcePrefabGlobalObjectIdHash, networkPrefab);
				if (!OverrideToNetworkPrefab.ContainsKey(targetPrefabGlobalObjectIdHash))
				{
					OverrideToNetworkPrefab.Add(targetPrefabGlobalObjectIdHash, sourcePrefabGlobalObjectIdHash);
				}
			}
			return true;
		}
	}
}
