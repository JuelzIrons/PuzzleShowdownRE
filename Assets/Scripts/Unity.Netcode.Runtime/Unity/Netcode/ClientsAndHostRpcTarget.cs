namespace Unity.Netcode
{
	internal class ClientsAndHostRpcTarget : global::Unity.Netcode.BaseRpcTarget
	{
		private global::Unity.Netcode.BaseRpcTarget m_UnderlyingTarget;

		public override void Dispose()
		{
			m_UnderlyingTarget = null;
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			if (m_UnderlyingTarget == null)
			{
				if (behaviour.NetworkManager.ServerIsHost || (m_NetworkManager.DistributedAuthorityMode && m_NetworkManager.CMBServiceConnection))
				{
					m_UnderlyingTarget = behaviour.RpcTarget.Everyone;
				}
				else
				{
					m_UnderlyingTarget = behaviour.RpcTarget.NotServer;
				}
			}
			m_UnderlyingTarget.Send(behaviour, ref message, delivery, rpcParams);
		}

		internal ClientsAndHostRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
		}
	}
}
