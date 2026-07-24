namespace Unity.Netcode
{
	internal class NotServerRpcTarget : global::Unity.Netcode.BaseRpcTarget
	{
		protected global::Unity.Netcode.IGroupRpcTarget m_GroupSendTarget;

		protected global::Unity.Netcode.LocalSendRpcTarget m_LocalSendRpcTarget;

		public override void Dispose()
		{
			m_LocalSendRpcTarget.Dispose();
			m_GroupSendTarget?.Target.Dispose();
			m_GroupSendTarget = null;
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			if (m_GroupSendTarget == null)
			{
				if (behaviour.IsServer)
				{
					m_GroupSendTarget = new global::Unity.Netcode.RpcTargetGroup(m_NetworkManager);
				}
				else
				{
					m_GroupSendTarget = new global::Unity.Netcode.ProxyRpcTargetGroup(m_NetworkManager);
				}
			}
			m_GroupSendTarget.Clear();
			if (behaviour.IsServer)
			{
				foreach (ulong observer in behaviour.NetworkObject.Observers)
				{
					if (observer != 0L)
					{
						m_GroupSendTarget.Add(observer);
					}
				}
			}
			else
			{
				foreach (ulong connectedClientId in ConnectionManager.ConnectedClientIds)
				{
					if (connectedClientId != 0L && (!m_NetworkManager.DistributedAuthorityMode || !m_NetworkManager.CMBServiceConnection || connectedClientId != behaviour.OwnerClientId))
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

		internal NotServerRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
			m_LocalSendRpcTarget = new global::Unity.Netcode.LocalSendRpcTarget(manager);
		}
	}
}
