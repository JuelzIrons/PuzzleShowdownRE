namespace Unity.Netcode
{
	internal class EveryoneRpcTarget : global::Unity.Netcode.BaseRpcTarget
	{
		private global::Unity.Netcode.NotServerRpcTarget m_NotServerRpcTarget;

		private global::Unity.Netcode.ServerRpcTarget m_ServerRpcTarget;

		private global::Unity.Netcode.NotAuthorityRpcTarget m_NotAuthorityRpcTarget;

		private global::Unity.Netcode.AuthorityRpcTarget m_AuthorityRpcTarget;

		public override void Dispose()
		{
			m_NotServerRpcTarget.Dispose();
			m_ServerRpcTarget.Dispose();
			m_NotAuthorityRpcTarget.Dispose();
			m_AuthorityRpcTarget.Dispose();
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			if (global::Unity.Netcode.NetworkManager.IsDistributedAuthority)
			{
				m_NotAuthorityRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
				m_AuthorityRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
			}
			else
			{
				m_NotServerRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
				m_ServerRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
			}
		}

		internal EveryoneRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
			m_NotServerRpcTarget = new global::Unity.Netcode.NotServerRpcTarget(manager);
			m_ServerRpcTarget = new global::Unity.Netcode.ServerRpcTarget(manager);
			m_NotAuthorityRpcTarget = new global::Unity.Netcode.NotAuthorityRpcTarget(manager);
			m_AuthorityRpcTarget = new global::Unity.Netcode.AuthorityRpcTarget(manager);
		}
	}
}
