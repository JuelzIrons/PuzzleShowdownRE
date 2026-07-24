namespace Unity.Netcode
{
	internal class RpcTargetGroup : global::Unity.Netcode.BaseRpcTarget, global::Unity.Netcode.IGroupRpcTarget
	{
		internal global::System.Collections.Generic.List<global::Unity.Netcode.BaseRpcTarget> Targets = new global::System.Collections.Generic.List<global::Unity.Netcode.BaseRpcTarget>();

		private global::Unity.Netcode.LocalSendRpcTarget m_LocalSendRpcTarget;

		private global::System.Collections.Generic.HashSet<ulong> m_Ids = new global::System.Collections.Generic.HashSet<ulong>();

		private global::System.Collections.Generic.Stack<global::Unity.Netcode.DirectSendRpcTarget> m_TargetCache = new global::System.Collections.Generic.Stack<global::Unity.Netcode.DirectSendRpcTarget>();

		public global::Unity.Netcode.BaseRpcTarget Target => this;

		public override void Dispose()
		{
			CheckLockBeforeDispose();
			foreach (global::Unity.Netcode.BaseRpcTarget target in Targets)
			{
				target.Dispose();
			}
			foreach (global::Unity.Netcode.DirectSendRpcTarget item in m_TargetCache)
			{
				item.Dispose();
			}
			m_LocalSendRpcTarget.Dispose();
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			foreach (global::Unity.Netcode.BaseRpcTarget target in Targets)
			{
				target.Send(behaviour, ref message, delivery, rpcParams);
			}
		}

		public void Add(ulong clientId)
		{
			if (!m_Ids.Contains(clientId))
			{
				m_Ids.Add(clientId);
				if (clientId == m_NetworkManager.LocalClientId)
				{
					Targets.Add(m_LocalSendRpcTarget);
				}
				else if (m_TargetCache.Count == 0)
				{
					Targets.Add(new global::Unity.Netcode.DirectSendRpcTarget(m_NetworkManager)
					{
						ClientId = clientId
					});
				}
				else
				{
					global::Unity.Netcode.DirectSendRpcTarget directSendRpcTarget = m_TargetCache.Pop();
					directSendRpcTarget.ClientId = clientId;
					Targets.Add(directSendRpcTarget);
				}
			}
		}

		public void Clear()
		{
			m_Ids.Clear();
			foreach (global::Unity.Netcode.BaseRpcTarget target in Targets)
			{
				if (target is global::Unity.Netcode.DirectSendRpcTarget item)
				{
					m_TargetCache.Push(item);
				}
			}
			Targets.Clear();
		}

		internal RpcTargetGroup(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
			m_LocalSendRpcTarget = new global::Unity.Netcode.LocalSendRpcTarget(manager);
		}
	}
}
