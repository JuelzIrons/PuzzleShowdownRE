namespace Unity.Netcode
{
	internal class AuthorityRpcTarget : global::Unity.Netcode.ServerRpcTarget
	{
		private global::Unity.Netcode.ProxyRpcTarget m_AuthorityTarget;

		private global::Unity.Netcode.DirectSendRpcTarget m_DirectSendTarget;

		public override void Dispose()
		{
			m_AuthorityTarget?.Dispose();
			m_AuthorityTarget = null;
			m_DirectSendTarget?.Dispose();
			m_DirectSendTarget = null;
			base.Dispose();
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			if (behaviour.NetworkManager.DistributedAuthorityMode)
			{
				if (behaviour.HasAuthority)
				{
					if (m_UnderlyingTarget == null)
					{
						m_UnderlyingTarget = new global::Unity.Netcode.LocalSendRpcTarget(m_NetworkManager);
					}
					m_UnderlyingTarget.Send(behaviour, ref message, delivery, rpcParams);
				}
				else if (behaviour.NetworkManager.DAHost)
				{
					if (m_DirectSendTarget == null)
					{
						m_DirectSendTarget = new global::Unity.Netcode.DirectSendRpcTarget(behaviour.OwnerClientId, m_NetworkManager);
					}
					else
					{
						m_DirectSendTarget.ClientId = behaviour.OwnerClientId;
					}
					m_DirectSendTarget.Send(behaviour, ref message, delivery, rpcParams);
				}
				else
				{
					if (m_AuthorityTarget == null)
					{
						m_AuthorityTarget = new global::Unity.Netcode.ProxyRpcTarget(behaviour.OwnerClientId, m_NetworkManager);
					}
					else
					{
						m_AuthorityTarget.SetClientId(behaviour.OwnerClientId);
					}
					m_AuthorityTarget.Send(behaviour, ref message, delivery, rpcParams);
				}
			}
			else
			{
				base.Send(behaviour, ref message, delivery, rpcParams);
			}
		}

		internal AuthorityRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
		}
	}
}
