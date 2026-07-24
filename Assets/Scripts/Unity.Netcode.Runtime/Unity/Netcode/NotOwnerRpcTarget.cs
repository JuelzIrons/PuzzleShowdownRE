namespace Unity.Netcode
{
	internal class NotOwnerRpcTarget : global::Unity.Netcode.BaseRpcTarget
	{
		private global::Unity.Netcode.IGroupRpcTarget m_GroupSendTarget;

		private global::Unity.Netcode.ServerRpcTarget m_ServerRpcTarget;

		private global::Unity.Netcode.NotAuthorityRpcTarget m_NotAuthorityRpcTarget;

		private global::Unity.Netcode.LocalSendRpcTarget m_LocalSendRpcTarget;

		public override void Dispose()
		{
			m_ServerRpcTarget.Dispose();
			m_LocalSendRpcTarget.Dispose();
			m_NotAuthorityRpcTarget.Dispose();
			m_GroupSendTarget?.Target.Dispose();
			m_GroupSendTarget = null;
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			if (m_NetworkManager.DistributedAuthorityMode)
			{
				m_NotAuthorityRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
				return;
			}
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
					if (observer != behaviour.OwnerClientId && observer != 0L)
					{
						if (observer == behaviour.NetworkManager.LocalClientId)
						{
							m_LocalSendRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
						}
						else
						{
							m_GroupSendTarget.Add(observer);
						}
					}
				}
			}
			else
			{
				foreach (ulong connectedClientId in ConnectionManager.ConnectedClientIds)
				{
					if (connectedClientId != behaviour.OwnerClientId && connectedClientId != 0L)
					{
						if (connectedClientId == behaviour.NetworkManager.LocalClientId)
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
			if (behaviour.OwnerClientId != 0L)
			{
				m_ServerRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
			}
		}

		internal NotOwnerRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
			m_ServerRpcTarget = new global::Unity.Netcode.ServerRpcTarget(manager);
			m_LocalSendRpcTarget = new global::Unity.Netcode.LocalSendRpcTarget(manager);
			m_NotAuthorityRpcTarget = new global::Unity.Netcode.NotAuthorityRpcTarget(manager);
		}
	}
}
