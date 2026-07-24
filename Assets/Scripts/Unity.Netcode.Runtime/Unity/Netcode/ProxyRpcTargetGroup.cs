namespace Unity.Netcode
{
	internal class ProxyRpcTargetGroup : global::Unity.Netcode.BaseRpcTarget, global::System.IDisposable, global::Unity.Netcode.IGroupRpcTarget
	{
		private global::Unity.Netcode.ServerRpcTarget m_ServerRpcTarget;

		private global::Unity.Netcode.LocalSendRpcTarget m_LocalSendRpcTarget;

		private bool m_Disposed;

		public global::Unity.Collections.NativeList<ulong> TargetClientIds;

		internal global::System.Collections.Generic.HashSet<ulong> Ids = new global::System.Collections.Generic.HashSet<ulong>();

		public global::Unity.Netcode.BaseRpcTarget Target => this;

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			if (TargetClientIds.Length != 0 || Ids.Count != 0)
			{
				global::Unity.Netcode.ProxyMessage message2 = new global::Unity.Netcode.ProxyMessage
				{
					Delivery = delivery,
					TargetClientIds = TargetClientIds.AsArray(),
					WrappedMessage = message
				};
				behaviour.NetworkManager.MessageManager.SendMessage(ref message2, delivery, 0uL);
				if (Ids.Contains(0uL))
				{
					m_ServerRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
				}
				if (Ids.Contains(m_NetworkManager.LocalClientId))
				{
					m_LocalSendRpcTarget.Send(behaviour, ref message, delivery, rpcParams);
				}
			}
		}

		internal ProxyRpcTargetGroup(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
			TargetClientIds = new global::Unity.Collections.NativeList<ulong>(global::Unity.Collections.Allocator.Persistent);
			m_ServerRpcTarget = new global::Unity.Netcode.ServerRpcTarget(manager);
			m_LocalSendRpcTarget = new global::Unity.Netcode.LocalSendRpcTarget(manager);
		}

		public override void Dispose()
		{
			CheckLockBeforeDispose();
			if (!m_Disposed)
			{
				TargetClientIds.Dispose();
				m_Disposed = true;
				m_ServerRpcTarget.Dispose();
				m_LocalSendRpcTarget.Dispose();
			}
		}

		public void Add(ulong clientId)
		{
			if (!Ids.Contains(clientId))
			{
				Ids.Add(clientId);
				if (clientId != 0L && clientId != m_NetworkManager.LocalClientId)
				{
					TargetClientIds.Add(in clientId);
				}
			}
		}

		public void Remove(ulong clientId)
		{
			Ids.Remove(clientId);
			for (int i = 0; i < TargetClientIds.Length; i++)
			{
				if (TargetClientIds[i] == clientId)
				{
					TargetClientIds.RemoveAt(i);
					break;
				}
			}
		}

		public void Clear()
		{
			Ids.Clear();
			TargetClientIds.Clear();
		}
	}
}
