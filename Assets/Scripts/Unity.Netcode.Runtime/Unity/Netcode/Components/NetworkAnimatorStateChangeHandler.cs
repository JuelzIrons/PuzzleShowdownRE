namespace Unity.Netcode.Components
{
	internal class NetworkAnimatorStateChangeHandler : global::Unity.Netcode.INetworkUpdateSystem
	{
		private struct AnimationUpdate
		{
			public global::Unity.Netcode.RpcParams RpcParams;

			public global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage AnimationMessage;
		}

		private struct ParameterUpdate
		{
			public global::Unity.Netcode.RpcParams RpcParams;

			public global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage ParametersUpdateMessage;
		}

		private struct TriggerUpdate
		{
			public bool SendToServer;

			public global::Unity.Netcode.RpcParams RpcParams;

			public global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage AnimationTriggerMessage;
		}

		private global::Unity.Netcode.Components.NetworkAnimator m_NetworkAnimator;

		private bool m_IsServer;

		private global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.AnimationUpdate> m_SendAnimationUpdates = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.AnimationUpdate>();

		private global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.ParameterUpdate> m_SendParameterUpdates = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.ParameterUpdate>();

		private global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage> m_ProcessParameterUpdates = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage>();

		private global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.TriggerUpdate> m_SendTriggerUpdates = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.TriggerUpdate>();

		private void FlushMessages()
		{
			foreach (global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.AnimationUpdate sendAnimationUpdate in m_SendAnimationUpdates)
			{
				if (m_NetworkAnimator.DistributedAuthorityMode)
				{
					m_NetworkAnimator.SendAnimStateRpc(sendAnimationUpdate.AnimationMessage);
				}
				else
				{
					m_NetworkAnimator.SendClientAnimStateRpc(sendAnimationUpdate.AnimationMessage, sendAnimationUpdate.RpcParams);
				}
			}
			m_SendAnimationUpdates.Clear();
			foreach (global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.ParameterUpdate sendParameterUpdate in m_SendParameterUpdates)
			{
				if (m_NetworkAnimator.DistributedAuthorityMode)
				{
					m_NetworkAnimator.SendParametersUpdateRpc(sendParameterUpdate.ParametersUpdateMessage);
				}
				else
				{
					m_NetworkAnimator.SendClientParametersUpdateRpc(sendParameterUpdate.ParametersUpdateMessage, sendParameterUpdate.RpcParams);
				}
			}
			m_SendParameterUpdates.Clear();
			foreach (global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.TriggerUpdate sendTriggerUpdate in m_SendTriggerUpdates)
			{
				if (m_NetworkAnimator.DistributedAuthorityMode)
				{
					m_NetworkAnimator.SendAnimTriggerRpc(sendTriggerUpdate.AnimationTriggerMessage);
				}
				else if (!sendTriggerUpdate.SendToServer)
				{
					m_NetworkAnimator.SendClientAnimTriggerRpc(sendTriggerUpdate.AnimationTriggerMessage, sendTriggerUpdate.RpcParams);
				}
				else
				{
					m_NetworkAnimator.SendServerAnimTriggerRpc(sendTriggerUpdate.AnimationTriggerMessage);
				}
			}
			m_SendTriggerUpdates.Clear();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private bool HasAuthority()
		{
			bool flag = m_NetworkAnimator.IsServerAuthoritative();
			if (flag || !m_NetworkAnimator.IsOwner)
			{
				if (flag)
				{
					return m_NetworkAnimator.IsServer;
				}
				return false;
			}
			return true;
		}

		public void NetworkUpdate(global::Unity.Netcode.NetworkUpdateStage updateStage)
		{
			if (updateStage != global::Unity.Netcode.NetworkUpdateStage.PreUpdate)
			{
				return;
			}
			bool flag = HasAuthority();
			if (flag || m_IsServer || (m_NetworkAnimator.IsServerAuthoritative() && m_NetworkAnimator.IsOwner))
			{
				FlushMessages();
			}
			if (m_ProcessParameterUpdates.Count > 0)
			{
				for (int i = 0; i < m_ProcessParameterUpdates.Count; i++)
				{
					global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage parametersUpdate = m_ProcessParameterUpdates[i];
					m_NetworkAnimator.UpdateParameters(ref parametersUpdate);
				}
				m_ProcessParameterUpdates.Clear();
			}
			if (flag)
			{
				m_NetworkAnimator.CheckForAnimatorChanges();
			}
		}

		internal void SendAnimationUpdate(global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage animationMessage, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			m_SendAnimationUpdates.Add(new global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.AnimationUpdate
			{
				RpcParams = rpcParams,
				AnimationMessage = animationMessage
			});
		}

		internal void SendParameterUpdate(global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage parametersUpdateMessage, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			m_SendParameterUpdates.Add(new global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.ParameterUpdate
			{
				RpcParams = rpcParams,
				ParametersUpdateMessage = parametersUpdateMessage
			});
		}

		internal void ProcessParameterUpdate(global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage parametersUpdateMessage)
		{
			m_ProcessParameterUpdates.Add(parametersUpdateMessage);
		}

		internal void QueueTriggerUpdateToClient(global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage animationTriggerMessage, global::Unity.Netcode.RpcParams clientRpcParams = default(global::Unity.Netcode.RpcParams))
		{
			m_SendTriggerUpdates.Add(new global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.TriggerUpdate
			{
				RpcParams = clientRpcParams,
				AnimationTriggerMessage = animationTriggerMessage
			});
		}

		internal void QueueTriggerUpdateToServer(global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage animationTriggerMessage)
		{
			m_SendTriggerUpdates.Add(new global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler.TriggerUpdate
			{
				AnimationTriggerMessage = animationTriggerMessage,
				SendToServer = true
			});
		}

		internal void DeregisterUpdate()
		{
			this.UnregisterNetworkUpdate(global::Unity.Netcode.NetworkUpdateStage.PreUpdate);
		}

		internal NetworkAnimatorStateChangeHandler(global::Unity.Netcode.Components.NetworkAnimator networkAnimator)
		{
			m_NetworkAnimator = networkAnimator;
			m_IsServer = networkAnimator.NetworkManager.IsServer;
			this.RegisterNetworkUpdate(global::Unity.Netcode.NetworkUpdateStage.PreUpdate);
		}
	}
}
