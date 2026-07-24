namespace Unity.Netcode
{
	public class RpcTarget
	{
		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		private global::Unity.Netcode.NetworkConnectionManager m_ConnectionManager;

		public global::Unity.Netcode.BaseRpcTarget Owner;

		public global::Unity.Netcode.BaseRpcTarget NotOwner;

		public global::Unity.Netcode.BaseRpcTarget Server;

		public global::Unity.Netcode.BaseRpcTarget NotServer;

		public global::Unity.Netcode.BaseRpcTarget Me;

		public global::Unity.Netcode.BaseRpcTarget NotMe;

		public global::Unity.Netcode.BaseRpcTarget Everyone;

		public global::Unity.Netcode.BaseRpcTarget ClientsAndHost;

		public global::Unity.Netcode.BaseRpcTarget Authority;

		public global::Unity.Netcode.BaseRpcTarget NotAuthority;

		private global::Unity.Netcode.ProxyRpcTargetGroup m_CachedProxyRpcTargetGroup;

		private global::Unity.Netcode.RpcTargetGroup m_CachedTargetGroup;

		private global::Unity.Netcode.DirectSendRpcTarget m_CachedDirectSendTarget;

		private global::Unity.Netcode.ProxyRpcTarget m_CachedProxyRpcTarget;

		internal RpcTarget(global::Unity.Netcode.NetworkManager manager)
		{
			m_NetworkManager = manager;
			m_ConnectionManager = manager.ConnectionManager;
			Everyone = new global::Unity.Netcode.EveryoneRpcTarget(manager);
			Owner = new global::Unity.Netcode.OwnerRpcTarget(manager);
			NotOwner = new global::Unity.Netcode.NotOwnerRpcTarget(manager);
			Server = new global::Unity.Netcode.ServerRpcTarget(manager);
			NotServer = new global::Unity.Netcode.NotServerRpcTarget(manager);
			NotMe = new global::Unity.Netcode.NotMeRpcTarget(manager);
			Me = new global::Unity.Netcode.LocalSendRpcTarget(manager);
			ClientsAndHost = new global::Unity.Netcode.ClientsAndHostRpcTarget(manager);
			Authority = new global::Unity.Netcode.AuthorityRpcTarget(manager);
			NotAuthority = new global::Unity.Netcode.NotAuthorityRpcTarget(manager);
			m_CachedProxyRpcTargetGroup = new global::Unity.Netcode.ProxyRpcTargetGroup(manager);
			m_CachedTargetGroup = new global::Unity.Netcode.RpcTargetGroup(manager);
			m_CachedDirectSendTarget = new global::Unity.Netcode.DirectSendRpcTarget(manager);
			m_CachedProxyRpcTarget = new global::Unity.Netcode.ProxyRpcTarget(0uL, manager);
			m_CachedProxyRpcTargetGroup.Lock();
			m_CachedTargetGroup.Lock();
			m_CachedDirectSendTarget.Lock();
			m_CachedProxyRpcTarget.Lock();
		}

		public void Dispose()
		{
			Everyone.Dispose();
			Owner.Dispose();
			NotOwner.Dispose();
			Server.Dispose();
			NotServer.Dispose();
			NotMe.Dispose();
			Me.Dispose();
			ClientsAndHost.Dispose();
			Authority.Dispose();
			NotAuthority.Dispose();
			m_CachedProxyRpcTargetGroup.Unlock();
			m_CachedTargetGroup.Unlock();
			m_CachedDirectSendTarget.Unlock();
			m_CachedProxyRpcTarget.Unlock();
			m_CachedProxyRpcTargetGroup.Dispose();
			m_CachedTargetGroup.Dispose();
			m_CachedDirectSendTarget.Dispose();
			m_CachedProxyRpcTarget.Dispose();
		}

		public global::Unity.Netcode.BaseRpcTarget Single(ulong clientId, global::Unity.Netcode.RpcTargetUse use)
		{
			if (clientId == m_NetworkManager.LocalClientId)
			{
				return Me;
			}
			if (m_NetworkManager.IsServer || clientId == 0L)
			{
				if (use == global::Unity.Netcode.RpcTargetUse.Persistent)
				{
					return new global::Unity.Netcode.DirectSendRpcTarget(clientId, m_NetworkManager);
				}
				m_CachedDirectSendTarget.SetClientId(clientId);
				return m_CachedDirectSendTarget;
			}
			if (use == global::Unity.Netcode.RpcTargetUse.Persistent)
			{
				return new global::Unity.Netcode.ProxyRpcTarget(clientId, m_NetworkManager);
			}
			m_CachedProxyRpcTarget.SetClientId(clientId);
			return m_CachedProxyRpcTarget;
		}

		public global::Unity.Netcode.BaseRpcTarget Not(ulong excludedClientId, global::Unity.Netcode.RpcTargetUse use)
		{
			global::Unity.Netcode.IGroupRpcTarget groupRpcTarget = (m_NetworkManager.IsServer ? ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedTargetGroup : new global::Unity.Netcode.RpcTargetGroup(m_NetworkManager))) : ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedProxyRpcTargetGroup : new global::Unity.Netcode.ProxyRpcTargetGroup(m_NetworkManager))));
			groupRpcTarget.Clear();
			foreach (ulong connectedClientId in m_ConnectionManager.ConnectedClientIds)
			{
				if (connectedClientId != excludedClientId)
				{
					groupRpcTarget.Add(connectedClientId);
				}
			}
			if (!m_NetworkManager.ServerIsHost && excludedClientId != 0L)
			{
				groupRpcTarget.Add(0uL);
			}
			return groupRpcTarget.Target;
		}

		public global::Unity.Netcode.BaseRpcTarget Group(global::Unity.Collections.NativeArray<ulong> clientIds, global::Unity.Netcode.RpcTargetUse use)
		{
			global::Unity.Netcode.IGroupRpcTarget groupRpcTarget = (m_NetworkManager.IsServer ? ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedTargetGroup : new global::Unity.Netcode.RpcTargetGroup(m_NetworkManager))) : ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedProxyRpcTargetGroup : new global::Unity.Netcode.ProxyRpcTargetGroup(m_NetworkManager))));
			groupRpcTarget.Clear();
			foreach (ulong item in clientIds)
			{
				groupRpcTarget.Add(item);
			}
			return groupRpcTarget.Target;
		}

		public global::Unity.Netcode.BaseRpcTarget Group(global::Unity.Collections.NativeList<ulong> clientIds, global::Unity.Netcode.RpcTargetUse use)
		{
			global::Unity.Collections.NativeArray<ulong> clientIds2 = clientIds.AsArray();
			return Group(clientIds2, use);
		}

		public global::Unity.Netcode.BaseRpcTarget Group(ulong[] clientIds, global::Unity.Netcode.RpcTargetUse use)
		{
			return Group(new global::Unity.Collections.NativeArray<ulong>(clientIds, global::Unity.Collections.Allocator.Temp), use);
		}

		public global::Unity.Netcode.BaseRpcTarget Group<T>(T clientIds, global::Unity.Netcode.RpcTargetUse use) where T : global::System.Collections.Generic.IEnumerable<ulong>
		{
			global::Unity.Netcode.IGroupRpcTarget groupRpcTarget = (m_NetworkManager.IsServer ? ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedTargetGroup : new global::Unity.Netcode.RpcTargetGroup(m_NetworkManager))) : ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedProxyRpcTargetGroup : new global::Unity.Netcode.ProxyRpcTargetGroup(m_NetworkManager))));
			groupRpcTarget.Clear();
			foreach (ulong item in clientIds)
			{
				groupRpcTarget.Add(item);
			}
			return groupRpcTarget.Target;
		}

		public global::Unity.Netcode.BaseRpcTarget Not(global::Unity.Collections.NativeArray<ulong> excludedClientIds, global::Unity.Netcode.RpcTargetUse use)
		{
			global::Unity.Netcode.IGroupRpcTarget groupRpcTarget = (m_NetworkManager.IsServer ? ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedTargetGroup : new global::Unity.Netcode.RpcTargetGroup(m_NetworkManager))) : ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedProxyRpcTargetGroup : new global::Unity.Netcode.ProxyRpcTargetGroup(m_NetworkManager))));
			groupRpcTarget.Clear();
			using global::Unity.Collections.NativeHashSet<ulong> nativeHashSet = new global::Unity.Collections.NativeHashSet<ulong>(excludedClientIds.Length, global::Unity.Collections.Allocator.Temp);
			foreach (ulong item in excludedClientIds)
			{
				nativeHashSet.Add(item);
			}
			foreach (ulong connectedClientId in m_ConnectionManager.ConnectedClientIds)
			{
				if (!nativeHashSet.Contains(connectedClientId))
				{
					groupRpcTarget.Add(connectedClientId);
				}
			}
			if (!m_NetworkManager.ServerIsHost && !nativeHashSet.Contains(0uL))
			{
				groupRpcTarget.Add(0uL);
			}
			return groupRpcTarget.Target;
		}

		public global::Unity.Netcode.BaseRpcTarget Not(global::Unity.Collections.NativeList<ulong> excludedClientIds, global::Unity.Netcode.RpcTargetUse use)
		{
			global::Unity.Collections.NativeArray<ulong> excludedClientIds2 = excludedClientIds.AsArray();
			return Not(excludedClientIds2, use);
		}

		public global::Unity.Netcode.BaseRpcTarget Not(ulong[] excludedClientIds, global::Unity.Netcode.RpcTargetUse use)
		{
			return Not(new global::Unity.Collections.NativeArray<ulong>(excludedClientIds, global::Unity.Collections.Allocator.Temp), use);
		}

		public global::Unity.Netcode.BaseRpcTarget Not<T>(T excludedClientIds, global::Unity.Netcode.RpcTargetUse use) where T : global::System.Collections.Generic.IEnumerable<ulong>
		{
			global::Unity.Netcode.IGroupRpcTarget groupRpcTarget = (m_NetworkManager.IsServer ? ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedTargetGroup : new global::Unity.Netcode.RpcTargetGroup(m_NetworkManager))) : ((global::Unity.Netcode.IGroupRpcTarget)((use != global::Unity.Netcode.RpcTargetUse.Persistent) ? m_CachedProxyRpcTargetGroup : new global::Unity.Netcode.ProxyRpcTargetGroup(m_NetworkManager))));
			groupRpcTarget.Clear();
			using global::Unity.Collections.NativeHashSet<ulong> nativeHashSet = new global::Unity.Collections.NativeHashSet<ulong>(m_ConnectionManager.ConnectedClientIds.Count, global::Unity.Collections.Allocator.Temp);
			foreach (ulong item in excludedClientIds)
			{
				nativeHashSet.Add(item);
			}
			foreach (ulong connectedClientId in m_ConnectionManager.ConnectedClientIds)
			{
				if (!nativeHashSet.Contains(connectedClientId))
				{
					groupRpcTarget.Add(connectedClientId);
				}
			}
			if (!m_NetworkManager.ServerIsHost && !nativeHashSet.Contains(0uL))
			{
				groupRpcTarget.Add(0uL);
			}
			return groupRpcTarget.Target;
		}
	}
}
