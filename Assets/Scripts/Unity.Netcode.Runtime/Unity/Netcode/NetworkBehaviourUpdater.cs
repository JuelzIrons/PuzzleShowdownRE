namespace Unity.Netcode
{
	public class NetworkBehaviourUpdater
	{
		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		private global::Unity.Netcode.NetworkConnectionManager m_ConnectionManager;

		private global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject> m_DirtyNetworkObjects = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject>();

		private global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject> m_PendingDirtyNetworkObjects = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject>();

		internal void AddForUpdate(global::Unity.Netcode.NetworkObject networkObject)
		{
			m_PendingDirtyNetworkObjects.Add(networkObject);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void ProcessDirtyObjectServer(global::Unity.Netcode.NetworkObject dirtyObj, bool forceSend)
		{
			foreach (global::Unity.Netcode.NetworkClient connectedClients in m_ConnectionManager.ConnectedClientsList)
			{
				if (m_NetworkManager.DistributedAuthorityMode || dirtyObj.IsNetworkVisibleTo(connectedClients.ClientId))
				{
					for (int i = 0; i < dirtyObj.ChildNetworkBehaviours.Count; i++)
					{
						dirtyObj.ChildNetworkBehaviours[i].NetworkVariableUpdate(connectedClients.ClientId, forceSend);
					}
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void ProcessDirtyObjectClient(global::Unity.Netcode.NetworkObject dirtyObj, bool forceSend)
		{
			for (int i = 0; i < dirtyObj.ChildNetworkBehaviours.Count; i++)
			{
				dirtyObj.ChildNetworkBehaviours[i].NetworkVariableUpdate(0uL, forceSend);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void PostProcessDirtyObject(global::Unity.Netcode.NetworkObject dirtyObj)
		{
			for (int i = 0; i < dirtyObj.ChildNetworkBehaviours.Count; i++)
			{
				global::Unity.Netcode.NetworkBehaviour networkBehaviour = dirtyObj.ChildNetworkBehaviours[i];
				for (int j = 0; j < networkBehaviour.NetworkVariableFields.Count; j++)
				{
					networkBehaviour.NetworkVariableFields[j].NetworkUpdaterCheck = true;
					if (networkBehaviour.NetworkVariableFields[j].IsDirty() && !networkBehaviour.NetworkVariableIndexesToResetSet.Contains(j))
					{
						networkBehaviour.NetworkVariableIndexesToResetSet.Add(j);
						networkBehaviour.NetworkVariableIndexesToReset.Add(j);
					}
					networkBehaviour.NetworkVariableFields[j].NetworkUpdaterCheck = false;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void ResetDirtyObject(global::Unity.Netcode.NetworkObject dirtyObj, bool forceSend)
		{
			foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour in dirtyObj.ChildNetworkBehaviours)
			{
				childNetworkBehaviour.PostNetworkVariableWrite(forceSend);
			}
		}

		internal void ForceSendIfDirtyOnNetworkShow(global::Unity.Netcode.NetworkObject networkObject)
		{
			if (m_PendingDirtyNetworkObjects.Contains(networkObject) || m_DirtyNetworkObjects.Contains(networkObject))
			{
				ProcessDirtyObject(networkObject, forceSend: true);
				m_PendingDirtyNetworkObjects.Remove(networkObject);
				m_DirtyNetworkObjects.Remove(networkObject);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void ProcessDirtyObject(global::Unity.Netcode.NetworkObject networkObject, bool forceSend)
		{
			if (m_NetworkManager.IsServer || networkObject.IsOwner)
			{
				for (int i = 0; i < networkObject.ChildNetworkBehaviours.Count; i++)
				{
					networkObject.ChildNetworkBehaviours[i].PreVariableUpdate();
				}
				if (m_NetworkManager.IsServer)
				{
					ProcessDirtyObjectServer(networkObject, forceSend);
				}
				else
				{
					ProcessDirtyObjectClient(networkObject, forceSend);
				}
				PostProcessDirtyObject(networkObject);
				ResetDirtyObject(networkObject, forceSend);
			}
		}

		internal void NetworkBehaviourUpdate(bool forceSend = false)
		{
			foreach (global::Unity.Netcode.NetworkObject pendingDirtyNetworkObject in m_PendingDirtyNetworkObjects)
			{
				m_DirtyNetworkObjects.Add(pendingDirtyNetworkObject);
			}
			m_PendingDirtyNetworkObjects.Clear();
			m_DirtyNetworkObjects.RemoveWhere((global::Unity.Netcode.NetworkObject sobj) => sobj == null);
			foreach (global::Unity.Netcode.NetworkObject dirtyNetworkObject in m_DirtyNetworkObjects)
			{
				ProcessDirtyObject(dirtyNetworkObject, forceSend);
			}
			m_DirtyNetworkObjects.Clear();
		}

		internal void Initialize(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_NetworkManager = networkManager;
			m_ConnectionManager = networkManager.ConnectionManager;
			m_NetworkManager.NetworkTickSystem.Tick += OnNetworkTick;
		}

		internal void Shutdown()
		{
			m_NetworkManager.NetworkTickSystem.Tick -= OnNetworkTick;
		}

		private void OnNetworkTick()
		{
			NetworkBehaviourUpdate();
		}
	}
}
