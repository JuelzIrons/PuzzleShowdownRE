namespace Unity.Netcode.Components
{
	public class AttachableBehaviour : global::Unity.Netcode.NetworkBehaviour
	{
		[global::System.Serializable]
		internal class ComponentControllerEntry
		{
			[global::System.Flags]
			public enum TriggerTypes : byte
			{
				Nothing = 0,
				OnAttach = 1,
				OnDetach = 2
			}

			[global::UnityEngine.HideInInspector]
			public string name;

			public global::Unity.Netcode.Components.AttachableBehaviour.ComponentControllerEntry.TriggerTypes AutoTrigger;

			public bool EnableOnAttach = true;

			public global::Unity.Netcode.Components.ComponentController ComponentController;

			[global::UnityEngine.HideInInspector]
			[global::UnityEngine.SerializeField]
			internal bool HasInitialized;
		}

		[global::System.Flags]
		public enum AutoDetachTypes
		{
			None = 0,
			OnOwnershipChange = 1,
			OnDespawn = 2,
			OnAttachNodeDestroy = 3
		}

		public enum AttachState
		{
			Detached = 0,
			Attaching = 1,
			Attached = 2,
			Detaching = 3
		}

		public global::Unity.Netcode.Components.AttachableBehaviour.AutoDetachTypes AutoDetach = global::Unity.Netcode.Components.AttachableBehaviour.AutoDetachTypes.OnAttachNodeDestroy;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::Unity.Netcode.Components.AttachableBehaviour.ComponentControllerEntry> ComponentControllers;

		private global::Unity.Netcode.NetworkBehaviourReference m_AttachedNodeReference = new global::Unity.Netcode.NetworkBehaviourReference(null);

		private global::UnityEngine.Vector3 m_OriginalLocalPosition;

		private global::UnityEngine.Quaternion m_OriginalLocalRotation;

		protected global::Unity.Netcode.Components.AttachableBehaviour.AttachState m_AttachState { get; private set; }

		protected global::UnityEngine.GameObject m_DefaultParent { get; private set; }

		protected AttachableNode m_AttachableNode { get; private set; }

		internal AttachableNode InternalAttachableNode => m_AttachableNode;

		public event global::System.Action<global::Unity.Netcode.Components.AttachableBehaviour.AttachState, AttachableNode> AttachStateChange;

		protected override void OnSynchronize<T>(ref global::Unity.Netcode.BufferSerializer<T> serializer)
		{
			serializer.SerializeValue(ref m_AttachedNodeReference, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			base.OnSynchronize(ref serializer);
		}

		protected virtual void OnAwake()
		{
		}

		protected virtual void Awake()
		{
			m_DefaultParent = ((base.transform.parent == null) ? base.gameObject : base.transform.parent.gameObject);
			m_OriginalLocalPosition = base.transform.localPosition;
			m_OriginalLocalRotation = base.transform.localRotation;
			m_AttachState = global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detached;
			m_AttachableNode = null;
			OnAwake();
		}

		protected override void OnNetworkSessionSynchronized()
		{
			UpdateAttachedState();
			base.OnNetworkSessionSynchronized();
		}

		internal void ForceDetach()
		{
			if (m_AttachState != global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detached && m_AttachState != global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detaching)
			{
				ForceComponentChange(isAttaching: false, forcedChange: true);
				InternalDetach();
				NotifyAttachedStateChanged(m_AttachState, m_AttachableNode);
				m_AttachedNodeReference = new global::Unity.Netcode.NetworkBehaviourReference(null);
				if ((bool)m_AttachableNode)
				{
					m_AttachableNode.Detach(this);
					m_AttachableNode = null;
				}
			}
		}

		public override void OnNetworkPreDespawn()
		{
			if (AutoDetach.HasFlag(global::Unity.Netcode.Components.AttachableBehaviour.AutoDetachTypes.OnDespawn))
			{
				ForceDetach();
			}
			base.OnNetworkDespawn();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void UpdateAttachedState()
		{
			AttachableNode networkBehaviour;
			bool flag = m_AttachedNodeReference.TryGet(out networkBehaviour, base.NetworkManager);
			if (!(networkBehaviour == m_AttachableNode) || ((!flag || m_AttachState != global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Attached) && (flag || m_AttachState != global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detached)))
			{
				if (flag && m_AttachableNode != null && m_AttachState == global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Attached)
				{
					NotifyAttachedStateChanged(global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detaching, m_AttachableNode);
					InternalDetach();
					NotifyAttachedStateChanged(global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detached, m_AttachableNode);
					m_AttachableNode.Detach(this);
					m_AttachableNode = null;
				}
				NotifyAttachedStateChanged(flag ? global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Attaching : global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detaching, flag ? networkBehaviour : m_AttachableNode);
				ForceComponentChange(flag, forcedChange: false);
				if (flag)
				{
					InternalAttach(networkBehaviour);
				}
				else
				{
					InternalDetach();
				}
				NotifyAttachedStateChanged(m_AttachState, m_AttachableNode);
				if (!flag && (bool)m_AttachableNode)
				{
					m_AttachableNode.Detach(this);
					m_AttachableNode = null;
				}
			}
		}

		protected virtual void OnAttachStateChanged(global::Unity.Netcode.Components.AttachableBehaviour.AttachState attachState, AttachableNode attachableNode)
		{
		}

		private void NotifyAttachedStateChanged(global::Unity.Netcode.Components.AttachableBehaviour.AttachState attachState, AttachableNode attachableNode)
		{
			try
			{
				this.AttachStateChange?.Invoke(attachState, attachableNode);
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			try
			{
				OnAttachStateChanged(attachState, attachableNode);
			}
			catch (global::System.Exception exception2)
			{
				global::UnityEngine.Debug.LogException(exception2);
			}
		}

		protected override void OnOwnershipChanged(ulong previous, ulong current)
		{
			if (AutoDetach.HasFlag(global::Unity.Netcode.Components.AttachableBehaviour.AutoDetachTypes.OnOwnershipChange))
			{
				ForceDetach();
			}
			base.OnOwnershipChanged(previous, current);
		}

		internal void ForceComponentChange(bool isAttaching, bool forcedChange)
		{
			global::Unity.Netcode.Components.AttachableBehaviour.ComponentControllerEntry.TriggerTypes triggerTypes = (isAttaching ? global::Unity.Netcode.Components.AttachableBehaviour.ComponentControllerEntry.TriggerTypes.OnAttach : global::Unity.Netcode.Components.AttachableBehaviour.ComponentControllerEntry.TriggerTypes.OnDetach);
			foreach (global::Unity.Netcode.Components.AttachableBehaviour.ComponentControllerEntry componentController in ComponentControllers)
			{
				if (componentController.AutoTrigger.HasFlag(triggerTypes))
				{
					componentController.ComponentController.ForceChangeEnabled(componentController.EnableOnAttach ? isAttaching : (!isAttaching), forcedChange);
				}
			}
		}

		internal void InternalAttach(AttachableNode attachableNode)
		{
			m_AttachState = global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Attached;
			m_AttachableNode = attachableNode;
			base.transform.SetParent(m_AttachableNode.transform, worldPositionStays: false);
			m_AttachableNode.Attach(this);
		}

		public void Attach(AttachableNode attachableNode)
		{
			if (!base.IsSpawned)
			{
				global::Unity.Netcode.NetworkLog.LogError("[" + base.name + "][Attach][Not Spawned] Cannot attach before being spawned!");
			}
			else if (!OnHasAuthority())
			{
				global::Unity.Netcode.NetworkLog.LogError($"[{base.name}][Attach][Not Authority] Client-{base.NetworkManager.LocalClientId} is not the authority!");
			}
			else if (attachableNode.NetworkObject == base.NetworkObject)
			{
				global::Unity.Netcode.NetworkLog.LogError($"[{base.name}][Attach] Cannot attach to the original {base.NetworkObject} instance!");
			}
			else if (m_AttachableNode != null && m_AttachState == global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Attached && m_AttachableNode == attachableNode)
			{
				global::Unity.Netcode.NetworkLog.LogError("[" + base.name + "][Attach] Cannot attach! " + base.name + " is already attached to " + attachableNode.name + "!");
			}
			else
			{
				ChangeReference(new global::Unity.Netcode.NetworkBehaviourReference(attachableNode));
			}
		}

		internal void InternalDetach()
		{
			if ((bool)m_AttachableNode)
			{
				if ((bool)m_DefaultParent)
				{
					base.transform.SetParent(m_DefaultParent.transform, worldPositionStays: false);
					base.transform.SetLocalPositionAndRotation(m_OriginalLocalPosition, m_OriginalLocalRotation);
				}
				m_AttachState = global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detached;
			}
		}

		public void Detach()
		{
			if (!base.gameObject)
			{
				return;
			}
			if (!base.IsSpawned)
			{
				global::Unity.Netcode.NetworkLog.LogError("[" + base.name + "][Detach][Not Spawned] Cannot detach if not spawned!");
			}
			else if (!OnHasAuthority())
			{
				global::Unity.Netcode.NetworkLog.LogError($"[{base.name}][Detach][Not Authority] Client-{base.NetworkManager.LocalClientId} is not the authority!");
			}
			else if (m_AttachState == global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detached || m_AttachState == global::Unity.Netcode.Components.AttachableBehaviour.AttachState.Detaching || m_AttachableNode == null)
			{
				if (!m_AttachableNode)
				{
					global::Unity.Netcode.NetworkLog.LogError(string.Format("[{0}][Detach] Invalid state detected! {1}'s state is still {2} but has no {3} assigned!", base.name, base.name, m_AttachState, "AttachableNode"));
					if ((bool)base.NetworkManager && base.NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogWarning("[" + base.name + "][Detach] Cannot detach! " + base.name + " is not attached to anything!");
					}
				}
				else
				{
					global::Unity.Netcode.NetworkLog.LogError("[" + base.name + "][Detach] Invalid state detected! " + base.name + " is still referencing AttachableNode " + m_AttachableNode.name + "! Could Detach be getting invoked more than once for the same instance?");
				}
			}
			else
			{
				ChangeReference(new global::Unity.Netcode.NetworkBehaviourReference(null));
			}
		}

		protected virtual bool OnHasAuthority()
		{
			return base.HasAuthority;
		}

		private void ChangeReference(global::Unity.Netcode.NetworkBehaviourReference networkBehaviourReference)
		{
			m_AttachedNodeReference = networkBehaviourReference;
			UpdateAttachedState();
			if (OnHasAuthority())
			{
				UpdateAttachStateRpc(m_AttachedNodeReference);
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.NotMe)]
		private void UpdateAttachStateRpc(global::Unity.Netcode.NetworkBehaviourReference attachedNodeReference, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = default(global::Unity.Netcode.RpcAttribute.RpcAttributeParams);
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(1126200704u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotMe, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in attachedNodeReference, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 1126200704u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotMe, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				ChangeReference(attachedNodeReference);
			}
		}

		internal void OnAttachNodeDestroy()
		{
			if (AutoDetach.HasFlag(global::Unity.Netcode.Components.AttachableBehaviour.AutoDetachTypes.OnAttachNodeDestroy))
			{
				ForceDetach();
			}
		}

		protected override void __initializeVariables()
		{
			base.__initializeVariables();
		}

		protected override void __initializeRpcs()
		{
			__registerRpc(1126200704u, __rpc_handler_1126200704, "UpdateAttachStateRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
			base.__initializeRpcs();
		}

		private static void __rpc_handler_1126200704(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.NetworkBehaviourReference value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.AttachableBehaviour)target).UpdateAttachStateRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		protected internal override string __getTypeName()
		{
			return "AttachableBehaviour";
		}
	}
}
