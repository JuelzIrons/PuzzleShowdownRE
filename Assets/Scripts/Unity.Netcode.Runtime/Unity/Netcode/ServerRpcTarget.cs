namespace Unity.Netcode
{
	internal class ServerRpcTarget : global::Unity.Netcode.BaseRpcTarget
	{
		protected global::Unity.Netcode.BaseRpcTarget m_UnderlyingTarget;

		protected global::Unity.Netcode.ProxyRpcTarget m_ProxyRpcTarget;

		public override void Dispose()
		{
			m_UnderlyingTarget?.Dispose();
			m_UnderlyingTarget = null;
			m_ProxyRpcTarget?.Dispose();
			m_ProxyRpcTarget = null;
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			if (behaviour.NetworkManager.DistributedAuthorityMode && behaviour.NetworkManager.CMBServiceConnection)
			{
				if (behaviour.IsOwner)
				{
					global::Unity.Netcode.NetworkContext context = new global::Unity.Netcode.NetworkContext
					{
						SenderId = m_NetworkManager.LocalClientId,
						Timestamp = m_NetworkManager.RealTimeProvider.RealTimeSinceStartup,
						SystemOwner = m_NetworkManager,
						Header = default(global::Unity.Netcode.NetworkMessageHeader),
						SerializedHeaderSize = 0,
						MessageSize = 0u
					};
					using global::Unity.Netcode.FastBufferReader readBuffer = new global::Unity.Netcode.FastBufferReader(message.WriteBuffer, global::Unity.Collections.Allocator.None);
					message.ReadBuffer = readBuffer;
					message.Handle(ref context);
					return;
				}
				if (m_ProxyRpcTarget == null)
				{
					m_ProxyRpcTarget = new global::Unity.Netcode.ProxyRpcTarget(behaviour.OwnerClientId, m_NetworkManager);
				}
				else
				{
					m_ProxyRpcTarget.SetClientId(behaviour.OwnerClientId);
				}
				m_ProxyRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
				return;
			}
			if (m_UnderlyingTarget == null)
			{
				if (behaviour.NetworkManager.IsServer)
				{
					m_UnderlyingTarget = new global::Unity.Netcode.LocalSendRpcTarget(m_NetworkManager);
				}
				else
				{
					m_UnderlyingTarget = new global::Unity.Netcode.DirectSendRpcTarget(m_NetworkManager)
					{
						ClientId = 0uL
					};
				}
			}
			m_UnderlyingTarget.Send(behaviour, ref message, delivery, rpcParams);
		}

		internal ServerRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
		}
	}
}
