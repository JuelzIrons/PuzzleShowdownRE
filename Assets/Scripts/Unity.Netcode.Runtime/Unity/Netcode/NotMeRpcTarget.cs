namespace Unity.Netcode
{
	internal class NotMeRpcTarget : global::Unity.Netcode.BaseRpcTarget
	{
		private global::Unity.Netcode.IGroupRpcTarget m_GroupSendTarget;

		private global::Unity.Netcode.ServerRpcTarget m_ServerRpcTarget;

		public override void Dispose()
		{
			m_ServerRpcTarget.Dispose();
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
					if (observer != behaviour.NetworkManager.LocalClientId)
					{
						m_GroupSendTarget.Add(observer);
					}
				}
			}
			else
			{
				foreach (ulong connectedClientId in ConnectionManager.ConnectedClientIds)
				{
					if (connectedClientId != behaviour.NetworkManager.LocalClientId && (connectedClientId != 0L || (m_NetworkManager.DistributedAuthorityMode && !m_NetworkManager.CMBServiceConnection)))
					{
						m_GroupSendTarget.Add(connectedClientId);
					}
				}
			}
			m_GroupSendTarget.Target.Send(behaviour, ref message, delivery, rpcParams);
			if (!behaviour.IsServer && !m_NetworkManager.DistributedAuthorityMode)
			{
				m_ServerRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
			}
		}

		internal NotMeRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
			m_ServerRpcTarget = new global::Unity.Netcode.ServerRpcTarget(manager);
		}
	}
}
