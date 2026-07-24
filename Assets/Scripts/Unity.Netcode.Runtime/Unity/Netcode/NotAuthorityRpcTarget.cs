namespace Unity.Netcode
{
	internal class NotAuthorityRpcTarget : global::Unity.Netcode.NotServerRpcTarget
	{
		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkObject networkObject = behaviour.NetworkObject;
			if (m_NetworkManager.DistributedAuthorityMode)
			{
				if (m_GroupSendTarget == null)
				{
					if (m_NetworkManager.DAHost)
					{
						m_GroupSendTarget = new global::Unity.Netcode.RpcTargetGroup(m_NetworkManager);
					}
					else
					{
						m_GroupSendTarget = new global::Unity.Netcode.ProxyRpcTargetGroup(m_NetworkManager);
					}
				}
				m_GroupSendTarget.Clear();
				if (behaviour.HasAuthority)
				{
					foreach (ulong observer in networkObject.Observers)
					{
						if (observer != behaviour.OwnerClientId && (observer != 0L || !m_NetworkManager.CMBServiceConnection))
						{
							m_GroupSendTarget.Add(observer);
						}
					}
				}
				else
				{
					foreach (ulong connectedClientId in ConnectionManager.ConnectedClientIds)
					{
						if (connectedClientId != behaviour.OwnerClientId && networkObject.Observers.Contains(connectedClientId) && (connectedClientId != 0L || !m_NetworkManager.CMBServiceConnection))
						{
							if (connectedClientId == m_NetworkManager.LocalClientId)
							{
								m_LocalSendRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
							}
							else
							{
								m_GroupSendTarget.Add(connectedClientId);
							}
						}
					}
				}
				m_GroupSendTarget.Target.Send(behaviour, ref message, delivery, rpcParams);
			}
			else
			{
				base.Send(behaviour, ref message, delivery, rpcParams);
			}
		}

		internal NotAuthorityRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
		}
	}
}
