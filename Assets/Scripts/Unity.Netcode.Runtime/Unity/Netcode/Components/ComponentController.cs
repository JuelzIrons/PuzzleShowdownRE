namespace Unity.Netcode.Components
{
	public class ComponentController : global::Unity.Netcode.NetworkBehaviour
	{
		[global::System.Serializable]
		internal class ComponentEntry
		{
			private class PendingStateUpdate
			{
				internal bool TimeDeltaDelayInProgress;

				internal bool PendingState;

				internal float DelayTimeDelta;

				internal global::Unity.Netcode.Components.ComponentController.ComponentEntry ComponentEntry;

				internal bool CheckTimeDeltaDelay()
				{
					if (!TimeDeltaDelayInProgress)
					{
						return false;
					}
					bool flag = DelayTimeDelta > global::UnityEngine.Time.realtimeSinceStartup;
					if (!flag)
					{
						ComponentEntry.SetValue(PendingState);
					}
					TimeDeltaDelayInProgress = flag;
					return TimeDeltaDelayInProgress;
				}

				internal PendingStateUpdate(global::Unity.Netcode.Components.ComponentController.ComponentEntry componentControllerEntry, bool isEnabled, float relativeTimeOffset)
				{
					ComponentEntry = componentControllerEntry;
					float num = ((relativeTimeOffset > 0f) ? relativeTimeOffset : global::UnityEngine.Time.realtimeSinceStartup);
					if (ComponentEntry.GetRelativeEnabled(isEnabled))
					{
						DelayTimeDelta = num + ComponentEntry.EnableDelay;
					}
					else
					{
						DelayTimeDelta = num + ComponentEntry.DisableDelay;
					}
					TimeDeltaDelayInProgress = true;
					PendingState = isEnabled;
				}
			}

			[global::UnityEngine.HideInInspector]
			public string name;

			[global::UnityEngine.Tooltip("When enabled, this component will inversely mirror the currently applied ComponentController's enabled state.")]
			public bool InvertEnabled;

			[global::UnityEngine.Range(0f, 2f)]
			[global::UnityEngine.Tooltip("The amount of time to delay when transitioning this component from disabled to enabled. When 0, the change is immediate.")]
			public float EnableDelay;

			[global::UnityEngine.Tooltip("The amount of time to delay when transitioning this component from enabled to disabled. When 0, the change is immediate.")]
			[global::UnityEngine.Range(0f, 2f)]
			public float DisableDelay;

			[global::UnityEngine.Tooltip("The component that will have its enabled status synchonized. You can drop a GameObject onto this field and all valid components will be added to the list.")]
			public global::UnityEngine.Object Component;

			internal global::System.Reflection.PropertyInfo PropertyInfo;

			private global::System.Collections.Generic.List<global::Unity.Netcode.Components.ComponentController.ComponentEntry.PendingStateUpdate> m_PendingStateUpdates = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.ComponentController.ComponentEntry.PendingStateUpdate>();

			internal bool GetRelativeEnabled(bool enabled)
			{
				if (!InvertEnabled)
				{
					return enabled;
				}
				return !enabled;
			}

			internal bool QueueForDelay(bool enabled)
			{
				if (GetRelativeEnabled(enabled) ? (EnableDelay > 0f) : (DisableDelay > 0f))
				{
					float relativeTimeOffset = 0f;
					if (m_PendingStateUpdates.Count > 0)
					{
						relativeTimeOffset = m_PendingStateUpdates[m_PendingStateUpdates.Count - 1].DelayTimeDelta;
					}
					m_PendingStateUpdates.Insert(0, new global::Unity.Netcode.Components.ComponentController.ComponentEntry.PendingStateUpdate(this, enabled, relativeTimeOffset));
					return true;
				}
				return false;
			}

			internal void SetValue(bool isEnabled)
			{
				PropertyInfo.SetValue(Component, GetRelativeEnabled(isEnabled));
			}

			internal bool HasPendingStateUpdates()
			{
				for (int num = m_PendingStateUpdates.Count - 1; num >= 0; num--)
				{
					if (!m_PendingStateUpdates[num].CheckTimeDeltaDelay())
					{
						m_PendingStateUpdates.RemoveAt(num);
					}
				}
				return m_PendingStateUpdates.Count > 0;
			}
		}

		private class CoroutineObject
		{
			public global::UnityEngine.Coroutine Coroutine;

			public bool IsRunning;
		}

		[global::UnityEngine.Tooltip("The initial state of the component controllers enabled status when instantiated.")]
		public bool StartEnabled = true;

		[global::UnityEngine.Tooltip("The list of components to control. You can drag and drop an entire GameObject on this to include all components.")]
		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::Unity.Netcode.Components.ComponentController.ComponentEntry> Components;

		internal global::System.Collections.Generic.List<global::Unity.Netcode.Components.ComponentController.ComponentEntry> ValidComponents = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.ComponentController.ComponentEntry>();

		private bool m_IsEnabled;

		private global::Unity.Netcode.Components.ComponentController.CoroutineObject m_CoroutineObject = new global::Unity.Netcode.Components.ComponentController.CoroutineObject();

		public bool EnabledState => m_IsEnabled;

		protected override void OnSynchronize<T>(ref global::Unity.Netcode.BufferSerializer<T> serializer)
		{
			serializer.SerializeValue(ref m_IsEnabled, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			base.OnSynchronize(ref serializer);
		}

		protected virtual void OnAwake()
		{
		}

		private void Awake()
		{
			ValidComponents.Clear();
			if (Components == null)
			{
				return;
			}
			int num = 0;
			foreach (global::Unity.Netcode.Components.ComponentController.ComponentEntry component in Components)
			{
				if (component == null)
				{
					num++;
					continue;
				}
				global::System.Reflection.PropertyInfo property = component.Component.GetType().GetProperty("enabled", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
				if (property != null && property.PropertyType == typeof(bool))
				{
					component.PropertyInfo = property;
					ValidComponents.Add(component);
				}
				else
				{
					global::Unity.Netcode.NetworkLog.LogWarning(base.name + " does not contain a public enable property! (Ignoring)");
				}
			}
			if (num > 0)
			{
				global::Unity.Netcode.NetworkLog.LogWarning(string.Format("{0} has {1} emtpy(null) entries in the {2} list!", base.name, num, "Components"));
			}
			InitializeComponents();
			try
			{
				OnAwake();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		public override void OnNetworkSpawn()
		{
			if (OnHasAuthority())
			{
				m_IsEnabled = StartEnabled;
			}
			base.OnNetworkSpawn();
		}

		protected override void OnNetworkPostSpawn()
		{
			ApplyEnabled();
			base.OnNetworkPostSpawn();
		}

		public override void OnDestroy()
		{
			if (m_CoroutineObject.IsRunning)
			{
				StopCoroutine(m_CoroutineObject.Coroutine);
				m_CoroutineObject.IsRunning = false;
			}
			base.OnDestroy();
		}

		private void InitializeComponents()
		{
			foreach (global::Unity.Netcode.Components.ComponentController.ComponentEntry validComponent in ValidComponents)
			{
				bool flag = (validComponent.InvertEnabled ? (!StartEnabled) : StartEnabled);
				validComponent.PropertyInfo.SetValue(validComponent.Component, flag);
			}
		}

		private void ApplyEnabled(bool ignoreDelays = false)
		{
			foreach (global::Unity.Netcode.Components.ComponentController.ComponentEntry validComponent in ValidComponents)
			{
				if (!ignoreDelays && validComponent.QueueForDelay(m_IsEnabled))
				{
					if (!m_CoroutineObject.IsRunning)
					{
						m_CoroutineObject.Coroutine = StartCoroutine(PendingAppliedState());
						m_CoroutineObject.IsRunning = true;
					}
				}
				else
				{
					validComponent.SetValue(m_IsEnabled);
				}
			}
		}

		private global::System.Collections.IEnumerator PendingAppliedState()
		{
			bool continueProcessing = true;
			while (continueProcessing)
			{
				continueProcessing = false;
				foreach (global::Unity.Netcode.Components.ComponentController.ComponentEntry validComponent in ValidComponents)
				{
					if (validComponent.HasPendingStateUpdates())
					{
						continueProcessing = true;
					}
				}
				if (continueProcessing)
				{
					yield return null;
				}
			}
			m_CoroutineObject.IsRunning = false;
		}

		public void SetEnabled(bool isEnabled)
		{
			if (!base.IsSpawned)
			{
				global::UnityEngine.Debug.Log("[" + base.name + "] Must be spawned to use SetEnabled!");
			}
			else if (!OnHasAuthority())
			{
				global::UnityEngine.Debug.Log(string.Format("[Client-{0}] Attempting to invoke {1} without authority!", base.NetworkManager.LocalClientId, "SetEnabled"));
			}
			else
			{
				ChangeEnabled(isEnabled);
			}
		}

		private void ChangeEnabled(bool isEnabled)
		{
			m_IsEnabled = isEnabled;
			ApplyEnabled();
			if (OnHasAuthority())
			{
				ToggleEnabledRpc(m_IsEnabled);
			}
		}

		internal void ForceChangeEnabled(bool isEnabled, bool ignoreDelays = false)
		{
			m_IsEnabled = isEnabled;
			ApplyEnabled(ignoreDelays);
		}

		protected virtual bool OnHasAuthority()
		{
			return base.HasAuthority;
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.NotMe)]
		private void ToggleEnabledRpc(bool enabled, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
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
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(323473117u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotMe, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in enabled, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				__endSendRpc(ref bufferWriter, 323473117u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotMe, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				ChangeEnabled(enabled);
			}
		}

		protected override void __initializeVariables()
		{
			base.__initializeVariables();
		}

		protected override void __initializeRpcs()
		{
			__registerRpc(323473117u, __rpc_handler_323473117, "ToggleEnabledRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
			base.__initializeRpcs();
		}

		private static void __rpc_handler_323473117(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out bool value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.ComponentController)target).ToggleEnabledRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		protected internal override string __getTypeName()
		{
			return "ComponentController";
		}
	}
}
