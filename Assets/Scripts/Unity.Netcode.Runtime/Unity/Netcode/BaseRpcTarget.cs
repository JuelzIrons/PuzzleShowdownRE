namespace Unity.Netcode
{
	public abstract class BaseRpcTarget : global::System.IDisposable
	{
		protected global::Unity.Netcode.NetworkManager m_NetworkManager;

		internal global::Unity.Netcode.NetworkConnectionManager ConnectionManager;

		private bool m_Locked;

		internal void Lock()
		{
			m_Locked = true;
		}

		internal void Unlock()
		{
			m_Locked = false;
		}

		internal BaseRpcTarget(global::Unity.Netcode.NetworkManager manager)
		{
			m_NetworkManager = manager;
			ConnectionManager = m_NetworkManager.ConnectionManager;
		}

		protected void CheckLockBeforeDispose()
		{
			if (m_Locked)
			{
				throw new global::System.Exception(string.Format("RPC targets obtained through {0}.{1} may not be disposed.", "RpcTargetUse", global::Unity.Netcode.RpcTargetUse.Temp));
			}
		}

		public abstract void Dispose();

		internal abstract void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams);

		private protected void SendMessageToClient(global::Unity.Netcode.NetworkBehaviour behaviour, ulong clientId, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery)
		{
			behaviour.NetworkManager.MessageManager.SendMessage(ref message, delivery, clientId);
		}
	}
}
