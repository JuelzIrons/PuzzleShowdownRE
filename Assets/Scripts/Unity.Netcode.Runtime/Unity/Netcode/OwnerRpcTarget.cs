namespace Unity.Netcode
{
	internal class OwnerRpcTarget : global::Unity.Netcode.BaseRpcTarget
	{
		private global::Unity.Netcode.IIndividualRpcTarget m_UnderlyingTarget;

		private global::Unity.Netcode.LocalSendRpcTarget m_LocalRpcTarget;

		private global::Unity.Netcode.ServerRpcTarget m_ServerRpcTarget;

		private global::Unity.Netcode.AuthorityRpcTarget m_AuthorityRpcTarget;

		public override void Dispose()
		{
			m_AuthorityRpcTarget.Dispose();
			m_ServerRpcTarget.Dispose();
			m_LocalRpcTarget.Dispose();
			m_UnderlyingTarget?.Target.Dispose();
			m_UnderlyingTarget = null;
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			if (m_NetworkManager.DistributedAuthorityMode)
			{
				m_AuthorityRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
				return;
			}
			if (behaviour.OwnerClientId == behaviour.NetworkManager.LocalClientId)
			{
				m_LocalRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
				return;
			}
			if (behaviour.OwnerClientId == 0L)
			{
				m_ServerRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
				return;
			}
			if (m_UnderlyingTarget == null)
			{
				if (behaviour.NetworkManager.IsServer)
				{
					m_UnderlyingTarget = new global::Unity.Netcode.DirectSendRpcTarget(m_NetworkManager);
				}
				else
				{
					m_UnderlyingTarget = new global::Unity.Netcode.ProxyRpcTarget(behaviour.OwnerClientId, m_NetworkManager);
				}
			}
			m_UnderlyingTarget.SetClientId(behaviour.OwnerClientId);
			m_UnderlyingTarget.Target.Send(behaviour, ref message, delivery, rpcParams);
		}

		internal OwnerRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
			m_LocalRpcTarget = new global::Unity.Netcode.LocalSendRpcTarget(manager);
			m_ServerRpcTarget = new global::Unity.Netcode.ServerRpcTarget(manager);
			m_AuthorityRpcTarget = new global::Unity.Netcode.AuthorityRpcTarget(manager);
		}
	}
}
