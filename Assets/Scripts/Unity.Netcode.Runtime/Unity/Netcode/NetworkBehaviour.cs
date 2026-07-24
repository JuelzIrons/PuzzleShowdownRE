namespace Unity.Netcode
{
	public abstract class NetworkBehaviour : global::UnityEngine.MonoBehaviour
	{
		public delegate void RpcReceiveHandler(global::Unity.Netcode.NetworkBehaviour behaviour, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams parameters);

		protected enum __RpcExecStage
		{
			Send = 0,
			Execute = 1,
			None = 0,
			Server = 1,
			Client = 2
		}

		protected internal static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkBehaviour.RpcReceiveHandler>> __rpc_func_table = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkBehaviour.RpcReceiveHandler>>();

		internal static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.RpcInvokePermission>> __rpc_permission_table = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.RpcInvokePermission>>();

		[global::System.NonSerialized]
		protected internal global::Unity.Netcode.NetworkBehaviour.__RpcExecStage __rpc_exec_stage;

		private const int k_RpcMessageDefaultSize = 1024;

		private const int k_RpcMessageMaximumSize = 65536;

		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		private global::Unity.Netcode.NetworkObject m_NetworkObject;

		internal ushort NetworkBehaviourIdCache;

		private bool m_VarInit;

		private readonly global::System.Collections.Generic.List<global::System.Collections.Generic.HashSet<int>> m_DeliveryMappedNetworkVariableIndices = new global::System.Collections.Generic.List<global::System.Collections.Generic.HashSet<int>>();

		private readonly global::System.Collections.Generic.List<global::Unity.Netcode.NetworkDelivery> m_DeliveryTypesForNetworkVariableGroups = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkDelivery>();

		protected internal readonly global::System.Collections.Generic.List<global::Unity.Netcode.NetworkVariableBase> NetworkVariableFields = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkVariableBase>();

		internal readonly global::System.Collections.Generic.List<int> NetworkVariableIndexesToReset = new global::System.Collections.Generic.List<int>();

		internal readonly global::System.Collections.Generic.HashSet<int> NetworkVariableIndexesToResetSet = new global::System.Collections.Generic.HashSet<int>();

		internal static bool LogSentVariableUpdateMessage;

		public global::Unity.Netcode.NetworkManager NetworkManager
		{
			get
			{
				if (m_NetworkManager != null)
				{
					return m_NetworkManager;
				}
				if (NetworkObject?.NetworkManager != null)
				{
					return NetworkObject.NetworkManager;
				}
				return global::Unity.Netcode.NetworkManager.Singleton;
			}
		}

		public global::Unity.Netcode.RpcTarget RpcTarget { get; private set; }

		public bool IsLocalPlayer { get; private set; }

		public bool IsOwner { get; internal set; }

		public bool IsServer { get; private set; }

		public bool HasAuthority { get; internal set; }

		public bool IsSessionOwner { get; internal set; }

		public bool ServerIsHost { get; private set; }

		public bool IsClient { get; private set; }

		public bool IsHost { get; private set; }

		public bool IsOwnedByServer { get; internal set; }

		public bool IsSpawned { get; internal set; }

		public global::Unity.Netcode.NetworkObject NetworkObject
		{
			get
			{
				if (m_NetworkObject != null)
				{
					return m_NetworkObject;
				}
				try
				{
					m_NetworkObject = GetComponentInParent<global::Unity.Netcode.NetworkObject>();
				}
				catch (global::System.Exception)
				{
					return null;
				}
				if (IsSpawned && m_NetworkObject == null && (m_NetworkManager == null || !m_NetworkManager.ShutdownInProgress) && global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("Could not get NetworkObject for the NetworkBehaviour. Are you missing a NetworkObject component?");
				}
				return m_NetworkObject;
			}
		}

		public bool HasNetworkObject => NetworkObject != null;

		public ulong NetworkObjectId { get; internal set; }

		public ushort NetworkBehaviourId { get; internal set; }

		public ulong OwnerClientId { get; internal set; }

		protected ulong m_TargetIdBeingSynchronized { get; private set; }

		protected internal virtual string __getTypeName()
		{
			return "NetworkBehaviour";
		}

		protected global::Unity.Netcode.FastBufferWriter __beginSendServerRpc(uint rpcMethodId, global::Unity.Netcode.ServerRpcParams serverRpcParams, global::Unity.Netcode.RpcDelivery rpcDelivery)
		{
			if (m_NetworkObject == null && !IsSpawned)
			{
				throw new global::Unity.Netcode.RpcException("The NetworkBehaviour must be spawned before calling this method.");
			}
			return new global::Unity.Netcode.FastBufferWriter(1024, global::Unity.Collections.Allocator.Temp, 65536);
		}

		protected void __endSendServerRpc(ref global::Unity.Netcode.FastBufferWriter bufferWriter, uint rpcMethodId, global::Unity.Netcode.ServerRpcParams serverRpcParams, global::Unity.Netcode.RpcDelivery rpcDelivery)
		{
			global::Unity.Netcode.NetworkManager networkManager = m_NetworkManager;
			global::Unity.Netcode.ServerRpcMessage message = new global::Unity.Netcode.ServerRpcMessage
			{
				Metadata = new global::Unity.Netcode.RpcMetadata
				{
					NetworkObjectId = NetworkObjectId,
					NetworkBehaviourId = NetworkBehaviourId,
					NetworkRpcMethodId = rpcMethodId
				},
				WriteBuffer = bufferWriter
			};
			global::Unity.Netcode.NetworkDelivery delivery;
			if (rpcDelivery == global::Unity.Netcode.RpcDelivery.Reliable || rpcDelivery != global::Unity.Netcode.RpcDelivery.Unreliable)
			{
				delivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ServerRpcMessage>.DefaultDelivery;
			}
			else
			{
				if (bufferWriter.Length > networkManager.MessageManager.NonFragmentedMessageMaxSize)
				{
					throw new global::System.OverflowException("RPC parameters are too large for unreliable delivery.");
				}
				delivery = global::Unity.Netcode.NetworkDelivery.Unreliable;
			}
			if (IsServer)
			{
				using global::Unity.Netcode.FastBufferReader readBuffer = new global::Unity.Netcode.FastBufferReader(bufferWriter, global::Unity.Collections.Allocator.Temp);
				global::Unity.Netcode.NetworkContext context = new global::Unity.Netcode.NetworkContext
				{
					SenderId = 0uL,
					Timestamp = networkManager.RealTimeProvider.RealTimeSinceStartup,
					SystemOwner = networkManager,
					Header = default(global::Unity.Netcode.NetworkMessageHeader),
					SerializedHeaderSize = 0,
					MessageSize = 0u
				};
				message.ReadBuffer = readBuffer;
				message.Handle(ref context);
				_ = readBuffer.Length;
			}
			else
			{
				networkManager.ConnectionManager.SendMessage(ref message, delivery, 0uL);
			}
			bufferWriter.Dispose();
		}

		protected global::Unity.Netcode.FastBufferWriter __beginSendClientRpc(uint rpcMethodId, global::Unity.Netcode.ClientRpcParams clientRpcParams, global::Unity.Netcode.RpcDelivery rpcDelivery)
		{
			if (m_NetworkObject == null && !IsSpawned)
			{
				throw new global::Unity.Netcode.RpcException("The NetworkBehaviour must be spawned before calling this method.");
			}
			return new global::Unity.Netcode.FastBufferWriter(1024, global::Unity.Collections.Allocator.Temp, 65536);
		}

		protected void __endSendClientRpc(ref global::Unity.Netcode.FastBufferWriter bufferWriter, uint rpcMethodId, global::Unity.Netcode.ClientRpcParams clientRpcParams, global::Unity.Netcode.RpcDelivery rpcDelivery)
		{
			global::Unity.Netcode.NetworkManager networkManager = m_NetworkManager;
			global::Unity.Netcode.ClientRpcMessage message = new global::Unity.Netcode.ClientRpcMessage
			{
				Metadata = new global::Unity.Netcode.RpcMetadata
				{
					NetworkObjectId = NetworkObjectId,
					NetworkBehaviourId = NetworkBehaviourId,
					NetworkRpcMethodId = rpcMethodId
				},
				WriteBuffer = bufferWriter
			};
			global::Unity.Netcode.NetworkDelivery delivery;
			if (rpcDelivery == global::Unity.Netcode.RpcDelivery.Reliable || rpcDelivery != global::Unity.Netcode.RpcDelivery.Unreliable)
			{
				delivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ClientRpcMessage>.DefaultDelivery;
			}
			else
			{
				if (bufferWriter.Length > networkManager.MessageManager.NonFragmentedMessageMaxSize)
				{
					throw new global::System.OverflowException("RPC parameters are too large for unreliable delivery.");
				}
				delivery = global::Unity.Netcode.NetworkDelivery.Unreliable;
			}
			bool flag = false;
			if (clientRpcParams.Send.TargetClientIds != null)
			{
				foreach (ulong targetClientId in clientRpcParams.Send.TargetClientIds)
				{
					if (targetClientId == 0L)
					{
						flag = true;
					}
					else if (networkManager.LogLevel >= global::Unity.Netcode.LogLevel.Error && !m_NetworkObject.Observers.Contains(targetClientId))
					{
						global::Unity.Netcode.NetworkLog.LogError(GenerateObserverErrorMessage(clientRpcParams, targetClientId));
					}
				}
				m_NetworkManager.ConnectionManager.SendMessage(ref message, delivery, in clientRpcParams.Send.TargetClientIds);
			}
			else if (clientRpcParams.Send.TargetClientIdsNativeArray.HasValue)
			{
				foreach (ulong item in clientRpcParams.Send.TargetClientIdsNativeArray.Value)
				{
					if (item == 0L)
					{
						flag = true;
					}
					else if (networkManager.LogLevel >= global::Unity.Netcode.LogLevel.Error && !m_NetworkObject.Observers.Contains(item))
					{
						global::Unity.Netcode.NetworkLog.LogError(GenerateObserverErrorMessage(clientRpcParams, item));
					}
				}
				networkManager.ConnectionManager.SendMessage(ref message, delivery, clientRpcParams.Send.TargetClientIdsNativeArray.Value);
			}
			else
			{
				global::System.Collections.Generic.HashSet<ulong>.Enumerator enumerator3 = m_NetworkObject.Observers.GetEnumerator();
				while (enumerator3.MoveNext())
				{
					if (IsHost && enumerator3.Current == networkManager.LocalClientId)
					{
						flag = true;
					}
					else
					{
						networkManager.ConnectionManager.SendMessage(ref message, delivery, enumerator3.Current);
					}
				}
			}
			if (flag)
			{
				using global::Unity.Netcode.FastBufferReader readBuffer = new global::Unity.Netcode.FastBufferReader(bufferWriter, global::Unity.Collections.Allocator.Temp);
				global::Unity.Netcode.NetworkContext context = new global::Unity.Netcode.NetworkContext
				{
					SenderId = 0uL,
					Timestamp = networkManager.RealTimeProvider.RealTimeSinceStartup,
					SystemOwner = networkManager,
					Header = default(global::Unity.Netcode.NetworkMessageHeader),
					SerializedHeaderSize = 0,
					MessageSize = 0u
				};
				message.ReadBuffer = readBuffer;
				message.Handle(ref context);
			}
			bufferWriter.Dispose();
		}

		protected global::Unity.Netcode.FastBufferWriter __beginSendRpc(uint rpcMethodId, global::Unity.Netcode.RpcParams rpcParams, global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams, global::Unity.Netcode.SendTo defaultTarget, global::Unity.Netcode.RpcDelivery rpcDelivery)
		{
			if (m_NetworkObject == null && !IsSpawned)
			{
				throw new global::Unity.Netcode.RpcException("The NetworkBehaviour must be spawned before calling this method.");
			}
			if (attributeParams.InvokePermission == global::Unity.Netcode.RpcInvokePermission.Server && !IsServer)
			{
				throw new global::Unity.Netcode.RpcException("This RPC can only be sent by the server.");
			}
			if ((attributeParams.RequireOwnership || attributeParams.InvokePermission == global::Unity.Netcode.RpcInvokePermission.Owner) && !IsOwner)
			{
				throw new global::Unity.Netcode.RpcException("This RPC can only be sent by its owner.");
			}
			return new global::Unity.Netcode.FastBufferWriter(1024, global::Unity.Collections.Allocator.Temp, 65536);
		}

		protected void __endSendRpc(ref global::Unity.Netcode.FastBufferWriter bufferWriter, uint rpcMethodId, global::Unity.Netcode.RpcParams rpcParams, global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams, global::Unity.Netcode.SendTo defaultTarget, global::Unity.Netcode.RpcDelivery rpcDelivery)
		{
			global::Unity.Netcode.RpcMessage message = new global::Unity.Netcode.RpcMessage
			{
				Metadata = new global::Unity.Netcode.RpcMetadata
				{
					NetworkObjectId = NetworkObjectId,
					NetworkBehaviourId = NetworkBehaviourId,
					NetworkRpcMethodId = rpcMethodId
				},
				SenderClientId = m_NetworkManager.LocalClientId,
				WriteBuffer = bufferWriter
			};
			global::Unity.Netcode.NetworkDelivery delivery;
			if (rpcDelivery == global::Unity.Netcode.RpcDelivery.Reliable || rpcDelivery != global::Unity.Netcode.RpcDelivery.Unreliable)
			{
				delivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.RpcMessage>.DefaultDelivery;
			}
			else
			{
				if (bufferWriter.Length > m_NetworkManager.MessageManager.NonFragmentedMessageMaxSize)
				{
					throw new global::System.OverflowException("RPC parameters are too large for unreliable delivery.");
				}
				delivery = global::Unity.Netcode.NetworkDelivery.Unreliable;
			}
			if (rpcParams.Send.Target == null)
			{
				switch (defaultTarget)
				{
				case global::Unity.Netcode.SendTo.Everyone:
					rpcParams.Send.Target = RpcTarget.Everyone;
					break;
				case global::Unity.Netcode.SendTo.Owner:
					rpcParams.Send.Target = RpcTarget.Owner;
					break;
				case global::Unity.Netcode.SendTo.Server:
					rpcParams.Send.Target = RpcTarget.Server;
					break;
				case global::Unity.Netcode.SendTo.NotServer:
					rpcParams.Send.Target = RpcTarget.NotServer;
					break;
				case global::Unity.Netcode.SendTo.NotMe:
					rpcParams.Send.Target = RpcTarget.NotMe;
					break;
				case global::Unity.Netcode.SendTo.NotOwner:
					rpcParams.Send.Target = RpcTarget.NotOwner;
					break;
				case global::Unity.Netcode.SendTo.Me:
					rpcParams.Send.Target = RpcTarget.Me;
					break;
				case global::Unity.Netcode.SendTo.ClientsAndHost:
					rpcParams.Send.Target = RpcTarget.ClientsAndHost;
					break;
				case global::Unity.Netcode.SendTo.Authority:
					rpcParams.Send.Target = RpcTarget.Authority;
					break;
				case global::Unity.Netcode.SendTo.NotAuthority:
					rpcParams.Send.Target = RpcTarget.NotAuthority;
					break;
				case global::Unity.Netcode.SendTo.SpecifiedInParams:
					throw new global::Unity.Netcode.RpcException("This method requires a runtime-specified send target.");
				}
			}
			else if (defaultTarget != global::Unity.Netcode.SendTo.SpecifiedInParams && !attributeParams.AllowTargetOverride)
			{
				throw new global::Unity.Netcode.RpcException("Target override is not allowed for this method.");
			}
			if (rpcParams.Send.LocalDeferMode == global::Unity.Netcode.LocalDeferMode.Default)
			{
				rpcParams.Send.LocalDeferMode = (attributeParams.DeferLocal ? global::Unity.Netcode.LocalDeferMode.Defer : global::Unity.Netcode.LocalDeferMode.SendImmediate);
			}
			rpcParams.Send.Target.Send(this, ref message, delivery, rpcParams);
			bufferWriter.Dispose();
		}

		protected static global::Unity.Collections.NativeList<T> __createNativeList<T>() where T : unmanaged
		{
			return new global::Unity.Collections.NativeList<T>(global::Unity.Collections.Allocator.Temp);
		}

		internal string GenerateObserverErrorMessage(global::Unity.Netcode.ClientRpcParams clientRpcParams, ulong targetClientId)
		{
			string arg = ((clientRpcParams.Send.TargetClientIds != null) ? "TargetClientIds" : "TargetClientIdsNativeArray");
			return $"Sending ClientRpc to non-observer! {arg} contains clientId {targetClientId} that is not an observer!";
		}

		internal bool IsBehaviourEditable()
		{
			if (!m_NetworkObject || !m_NetworkManager || !m_NetworkManager.IsListening)
			{
				return true;
			}
			return HasAuthority;
		}

		internal void SetNetworkObject(global::Unity.Netcode.NetworkObject networkObject, ushort behaviourId)
		{
			m_NetworkObject = networkObject;
			NetworkBehaviourId = behaviourId;
		}

		protected global::Unity.Netcode.NetworkBehaviour GetNetworkBehaviour(ushort behaviourId)
		{
			return NetworkObject.GetNetworkBehaviourAtOrderIndex(behaviourId);
		}

		internal void UpdateNetworkProperties()
		{
			global::Unity.Netcode.NetworkObject networkObject = m_NetworkObject;
			global::Unity.Netcode.NetworkManager networkManager = m_NetworkManager;
			NetworkObjectId = networkObject.NetworkObjectId;
			IsLocalPlayer = networkObject.IsLocalPlayer;
			IsOwnedByServer = networkObject.IsOwnedByServer;
			IsOwner = networkObject.IsOwner;
			OwnerClientId = networkObject.OwnerClientId;
			if (networkManager != null)
			{
				IsHost = networkManager.IsListening && networkManager.IsHost;
				IsClient = networkManager.IsListening && networkManager.IsClient;
				IsServer = networkManager.IsListening && networkManager.IsServer;
				IsSessionOwner = networkManager.IsListening && networkManager.LocalClient.IsSessionOwner;
				HasAuthority = networkObject.HasAuthority;
				ServerIsHost = networkManager.IsListening && networkManager.ServerIsHost;
			}
		}

		public virtual void OnDeferringDespawn(int despawnTick)
		{
		}

		protected virtual void OnNetworkPreSpawn(ref global::Unity.Netcode.NetworkManager networkManager)
		{
		}

		public virtual void OnNetworkSpawn()
		{
		}

		protected virtual void OnNetworkPostSpawn()
		{
		}

		protected internal virtual void InternalOnNetworkPostSpawn()
		{
		}

		protected virtual void OnNetworkSessionSynchronized()
		{
		}

		protected internal virtual void InternalOnNetworkSessionSynchronized()
		{
		}

		protected virtual void OnInSceneObjectsSpawned()
		{
		}

		public virtual void OnNetworkDespawn()
		{
		}

		public virtual void OnNetworkPreDespawn()
		{
		}

		internal virtual void InternalOnNetworkPreSpawn(ref global::Unity.Netcode.NetworkManager networkManager)
		{
		}

		internal void NetworkPreSpawn(ref global::Unity.Netcode.NetworkManager networkManager, global::Unity.Netcode.NetworkObject networkObject)
		{
			m_NetworkObject = networkObject;
			m_NetworkManager = networkManager;
			RpcTarget = networkManager.RpcTarget;
			UpdateNetworkProperties();
			InternalOnNetworkPreSpawn(ref networkManager);
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			try
			{
				OnNetworkPreSpawn(ref networkManager);
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		internal void InternalOnNetworkSpawn()
		{
			IsSpawned = true;
			InitializeVariables();
			UpdateNetworkProperties();
		}

		internal void NetworkSpawn()
		{
			try
			{
				OnNetworkSpawn();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		internal void NetworkPostSpawn()
		{
			try
			{
				InternalOnNetworkPostSpawn();
				OnNetworkPostSpawn();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			for (int i = 0; i < NetworkVariableFields.Count; i++)
			{
				NetworkVariableFields[i].InternalOnSpawned();
			}
		}

		internal void NetworkSessionSynchronized()
		{
			try
			{
				InternalOnNetworkSessionSynchronized();
				OnNetworkSessionSynchronized();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		internal void InSceneNetworkObjectsSpawned()
		{
			try
			{
				OnInSceneObjectsSpawned();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		internal void InternalOnNetworkPreDespawn()
		{
			try
			{
				OnNetworkPreDespawn();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			for (int i = 0; i < NetworkVariableFields.Count; i++)
			{
				NetworkVariableFields[i].InternalOnPreDespawn();
			}
		}

		internal void InternalOnNetworkDespawn()
		{
			IsSpawned = false;
			UpdateNetworkProperties();
			try
			{
				OnNetworkDespawn();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			for (int i = 0; i < NetworkVariableFields.Count; i++)
			{
				NetworkVariableFields[i].Deinitialize();
			}
		}

		public virtual void OnGainedOwnership()
		{
		}

		internal void InternalOnGainedOwnership()
		{
			if (IsOwner)
			{
				UpdateNetworkVariableOnOwnershipChanged();
			}
			OnGainedOwnership();
		}

		protected virtual void OnOwnershipChanged(ulong previous, ulong current)
		{
		}

		internal void InternalOnOwnershipChanged(ulong previous, ulong current)
		{
			OnOwnershipChanged(previous, current);
		}

		public virtual void OnLostOwnership()
		{
		}

		public virtual void OnNetworkObjectParentChanged(global::Unity.Netcode.NetworkObject parentNetworkObject)
		{
		}

		internal virtual void InternalOnNetworkObjectParentChanged(global::Unity.Netcode.NetworkObject parentNetworkObject)
		{
		}

		protected virtual void __initializeVariables()
		{
		}

		protected virtual void __initializeRpcs()
		{
		}

		protected void __registerRpc(uint hash, global::Unity.Netcode.NetworkBehaviour.RpcReceiveHandler handler, string rpcMethodName)
		{
			__registerRpc(hash, handler, rpcMethodName, global::Unity.Netcode.RpcInvokePermission.Everyone);
		}

		protected void __registerRpc(uint hash, global::Unity.Netcode.NetworkBehaviour.RpcReceiveHandler handler, string rpcMethodName, global::Unity.Netcode.RpcInvokePermission permission)
		{
			global::System.Type type = GetType();
			__rpc_func_table[type][hash] = handler;
			__rpc_permission_table[type][hash] = permission;
		}

		protected void __nameNetworkVariable(global::Unity.Netcode.NetworkVariableBase variable, string varName)
		{
			variable.Name = varName;
		}

		internal void InitializeVariables()
		{
			if (m_VarInit)
			{
				for (int i = 0; i < NetworkVariableFields.Count; i++)
				{
					if (!NetworkVariableFields[i].HasBeenInitialized)
					{
						NetworkVariableFields[i].Initialize(this);
					}
				}
				return;
			}
			m_VarInit = true;
			if (!__rpc_func_table.ContainsKey(GetType()))
			{
				__rpc_func_table[GetType()] = new global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkBehaviour.RpcReceiveHandler>();
				__rpc_permission_table[GetType()] = new global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.RpcInvokePermission>();
				__initializeRpcs();
			}
			__initializeVariables();
			global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkDelivery, int> dictionary = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkDelivery, int>();
			int num = 0;
			for (int j = 0; j < NetworkVariableFields.Count; j++)
			{
				global::Unity.Netcode.NetworkDelivery defaultDelivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.NetworkVariableDeltaMessage>.DefaultDelivery;
				if (!dictionary.ContainsKey(defaultDelivery))
				{
					dictionary.Add(defaultDelivery, num);
					m_DeliveryTypesForNetworkVariableGroups.Add(defaultDelivery);
					num++;
				}
				if (dictionary[defaultDelivery] >= m_DeliveryMappedNetworkVariableIndices.Count)
				{
					m_DeliveryMappedNetworkVariableIndices.Add(new global::System.Collections.Generic.HashSet<int>());
				}
				m_DeliveryMappedNetworkVariableIndices[dictionary[defaultDelivery]].Add(j);
			}
		}

		internal void PreNetworkVariableWrite()
		{
			NetworkVariableIndexesToReset.Clear();
			NetworkVariableIndexesToResetSet.Clear();
		}

		internal void PostNetworkVariableWrite(bool forced = false)
		{
			if (forced)
			{
				for (int i = 0; i < NetworkVariableFields.Count; i++)
				{
					global::Unity.Netcode.NetworkVariableBase networkVariableBase = NetworkVariableFields[i];
					if (networkVariableBase.IsDirty() && networkVariableBase.CanSend())
					{
						networkVariableBase.UpdateLastSentTime();
						networkVariableBase.ResetDirty();
						networkVariableBase.SetDirty(isDirty: false);
					}
				}
			}
			else
			{
				for (int j = 0; j < NetworkVariableIndexesToReset.Count; j++)
				{
					global::Unity.Netcode.NetworkVariableBase networkVariableBase2 = NetworkVariableFields[NetworkVariableIndexesToReset[j]];
					if (networkVariableBase2.IsDirty() && networkVariableBase2.CanSend())
					{
						networkVariableBase2.UpdateLastSentTime();
						networkVariableBase2.ResetDirty();
						networkVariableBase2.SetDirty(isDirty: false);
					}
				}
			}
			MarkVariablesDirty(dirty: false);
		}

		internal void PreVariableUpdate()
		{
			if (!m_VarInit)
			{
				InitializeVariables();
			}
			PreNetworkVariableWrite();
		}

		internal void NetworkVariableUpdate(ulong targetClientId, bool forceSend = false)
		{
			if (!forceSend && !CouldHaveDirtyNetworkVariables())
			{
				return;
			}
			global::Unity.Netcode.NetworkManager networkManager = m_NetworkManager;
			global::Unity.Netcode.NetworkObject networkObject = m_NetworkObject;
			global::Unity.Netcode.NetworkMessageManager messageManager = networkManager.MessageManager;
			global::Unity.Netcode.NetworkConnectionManager connectionManager = networkManager.ConnectionManager;
			for (int i = 0; i < m_DeliveryMappedNetworkVariableIndices.Count; i++)
			{
				global::Unity.Netcode.NetworkVariableBase networkVariableBase = null;
				bool flag = false;
				for (int j = 0; j < NetworkVariableFields.Count; j++)
				{
					networkVariableBase = NetworkVariableFields[j];
					if (networkVariableBase.IsDirty() && networkVariableBase.CanClientRead(targetClientId) && networkVariableBase.CanSend())
					{
						flag = true;
						break;
					}
				}
				if (flag && networkManager.DAHost && networkVariableBase.WritePerm == global::Unity.Netcode.NetworkVariableWritePermission.Owner && networkObject.OwnerClientId == targetClientId && networkObject.OwnerClientId != networkManager.LocalClientId && networkObject.PreviousOwnerId == networkObject.OwnerClientId)
				{
					flag = false;
				}
				if (!flag)
				{
					continue;
				}
				global::Unity.Netcode.NetworkVariableDeltaMessage message = new global::Unity.Netcode.NetworkVariableDeltaMessage
				{
					NetworkObjectId = NetworkObjectId,
					NetworkBehaviourIndex = NetworkBehaviourId,
					NetworkBehaviour = this,
					TargetClientId = targetClientId,
					DeliveryMappedNetworkVariableIndex = m_DeliveryMappedNetworkVariableIndices[i],
					NetworkDelivery = m_DeliveryTypesForNetworkVariableGroups[i]
				};
				if (IsServer && targetClientId == 0L)
				{
					global::Unity.Netcode.FastBufferWriter fastBufferWriter = new global::Unity.Netcode.FastBufferWriter(messageManager.NonFragmentedMessageMaxSize, global::Unity.Collections.Allocator.Temp, messageManager.FragmentedMessageMaxSize);
					using (fastBufferWriter)
					{
						message.Serialize(fastBufferWriter, message.Version);
					}
				}
				else
				{
					connectionManager.SendMessage(ref message, m_DeliveryTypesForNetworkVariableGroups[i], targetClientId);
				}
			}
		}

		private bool CouldHaveDirtyNetworkVariables()
		{
			for (int i = 0; i < NetworkVariableFields.Count; i++)
			{
				global::Unity.Netcode.NetworkVariableBase networkVariableBase = NetworkVariableFields[i];
				if (networkVariableBase.IsDirty())
				{
					if (networkVariableBase.CanSend())
					{
						return true;
					}
					m_NetworkManager.BehaviourUpdater.AddForUpdate(m_NetworkObject);
				}
			}
			return false;
		}

		internal void UpdateNetworkVariableOnOwnershipChanged()
		{
			for (int i = 0; i < NetworkVariableFields.Count; i++)
			{
				if (NetworkVariableFields[i].CanClientWrite(OwnerClientId))
				{
					NetworkVariableFields[i].OnInitialize();
				}
			}
		}

		internal void MarkVariablesDirty(bool dirty)
		{
			for (int i = 0; i < NetworkVariableFields.Count; i++)
			{
				NetworkVariableFields[i].SetDirty(dirty);
			}
		}

		internal void MarkOwnerReadDirtyAndCheckOwnerWriteIsDirty()
		{
			for (int i = 0; i < NetworkVariableFields.Count; i++)
			{
				if (NetworkVariableFields[i].ReadPerm == global::Unity.Netcode.NetworkVariableReadPermission.Owner)
				{
					NetworkVariableFields[i].SetDirty(isDirty: true);
				}
				if (NetworkVariableFields[i].WritePerm == global::Unity.Netcode.NetworkVariableWritePermission.Owner)
				{
					NetworkVariableFields[i].OnCheckIsDirtyState();
				}
			}
		}

		internal void WriteNetworkVariableData(global::Unity.Netcode.FastBufferWriter writer, ulong targetClientId)
		{
			bool ensureNetworkVariableLengthSafety = m_NetworkManager.NetworkConfig.EnsureNetworkVariableLengthSafety;
			foreach (global::Unity.Netcode.NetworkVariableBase networkVariableField in NetworkVariableFields)
			{
				if (networkVariableField.CanClientRead(targetClientId))
				{
					if (ensureNetworkVariableLengthSafety)
					{
						int position = writer.Position;
						writer.WriteValueSafe<int>(0, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						int position2 = writer.Position;
						networkVariableField.WriteFieldSynchronization(writer);
						int value = writer.Position - position2;
						writer.Seek(position);
						writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						writer.Seek(position2 + value);
					}
					else
					{
						networkVariableField.WriteFieldSynchronization(writer);
					}
				}
				else if (ensureNetworkVariableLengthSafety)
				{
					writer.WriteValueSafe<int>(0, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
			}
		}

		internal void SetNetworkVariableData(global::Unity.Netcode.FastBufferReader reader, ulong clientId)
		{
			global::Unity.Netcode.NetworkManager networkManager = m_NetworkManager;
			bool ensureNetworkVariableLengthSafety = networkManager.NetworkConfig.EnsureNetworkVariableLengthSafety;
			foreach (global::Unity.Netcode.NetworkVariableBase networkVariableField in NetworkVariableFields)
			{
				int value = 0;
				int num = 0;
				if (networkVariableField.CanClientRead(clientId))
				{
					if (ensureNetworkVariableLengthSafety)
					{
						reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						if (value == 0)
						{
							global::UnityEngine.Debug.LogError($"[{base.name}][NetworkObjectId: {NetworkObjectId}][NetworkBehaviourId: {NetworkBehaviourId}][{networkVariableField.Name}] Expected non-zero size readable NetworkVariable! (Skipping)");
							continue;
						}
						num = reader.Position;
					}
					networkVariableField.ReadField(reader);
					if (!ensureNetworkVariableLengthSafety)
					{
						continue;
					}
					int num2 = reader.Position - num;
					if (num2 != value)
					{
						if (networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
						{
							global::Unity.Netcode.NetworkLog.LogWarning($"[{base.name}][NetworkObjectId: {NetworkObjectId}][NetworkBehaviourId: {NetworkBehaviourId}][{networkVariableField.Name}] NetworkVariable read {num2} bytes but was expected to read {value} bytes during synchronization deserialization!");
						}
						reader.Seek(num + value);
					}
				}
				else if (ensureNetworkVariableLengthSafety)
				{
					reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					if (value != 0)
					{
						global::UnityEngine.Debug.LogError($"[{base.name}][NetworkObjectId: {NetworkObjectId}][NetworkBehaviourId: {NetworkBehaviourId}][{networkVariableField.Name}] Expected zero size for non-readable NetworkVariable when EnsureNetworkVariableLengthSafety is enabled! (Skipping)");
					}
				}
			}
		}

		protected global::Unity.Netcode.NetworkObject GetNetworkObject(ulong networkId)
		{
			return global::System.Collections.Generic.CollectionExtensions.GetValueOrDefault(m_NetworkManager.SpawnManager.SpawnedObjects, networkId);
		}

		protected virtual void OnSynchronize<T>(ref global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
		}

		public virtual void OnReanticipate(double lastRoundTripTime)
		{
		}

		internal bool Synchronize<T>(ref global::Unity.Netcode.BufferSerializer<T> serializer, ulong targetClientId = 0uL) where T : global::Unity.Netcode.IReaderWriter
		{
			m_TargetIdBeingSynchronized = targetClientId;
			if (serializer.IsWriter)
			{
				global::Unity.Netcode.FastBufferWriter fastBufferWriter = serializer.GetFastBufferWriter();
				int position = fastBufferWriter.Position;
				fastBufferWriter.WriteValueSafe<ushort>(NetworkBehaviourId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				int position2 = fastBufferWriter.Position;
				fastBufferWriter.WriteValueSafe<int>(0, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				int position3 = fastBufferWriter.Position;
				bool flag = false;
				try
				{
					OnSynchronize(ref serializer);
				}
				catch (global::System.Exception ex)
				{
					flag = true;
					if (m_NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						global::Unity.Netcode.NetworkLog.LogWarning(base.name + " threw an exception during synchronization serialization, this NetworkBehaviour is being skipped and will not be synchronized!");
						if (m_NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
						{
							global::Unity.Netcode.NetworkLog.LogError(ex.Message + "\n " + ex.StackTrace);
						}
					}
				}
				int position4 = fastBufferWriter.Position;
				m_TargetIdBeingSynchronized = 0uL;
				if (position4 == position3 || flag)
				{
					fastBufferWriter.Seek(position);
					fastBufferWriter.Truncate();
					return false;
				}
				int value = position4 - position3;
				fastBufferWriter.Seek(position2);
				fastBufferWriter.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.Seek(position4);
				return true;
			}
			global::Unity.Netcode.FastBufferReader fastBufferReader = serializer.GetFastBufferReader();
			fastBufferReader.ReadValueSafe(out int value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			int position5 = fastBufferReader.Position;
			bool flag2 = false;
			try
			{
				OnSynchronize(ref serializer);
			}
			catch (global::System.Exception ex2)
			{
				if (m_NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogWarning(base.name + " threw an exception during synchronization deserialization, this NetworkBehaviour is being skipped and will not be synchronized!");
					if (m_NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogError(ex2.Message + "\n " + ex2.StackTrace);
					}
				}
				flag2 = true;
			}
			int num = fastBufferReader.Position - position5;
			if (num != value2)
			{
				if (m_NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogWarning(string.Format("{0} read {1} bytes but was expected to read {2} bytes during synchronization deserialization! This {3}({4})is being skipped and will not be synchronized!", base.name, num, value2, "NetworkBehaviour", GetType().Name));
				}
				flag2 = true;
			}
			m_TargetIdBeingSynchronized = 0uL;
			if (flag2)
			{
				int num2 = position5 + value2;
				fastBufferReader.Seek(num2);
				return false;
			}
			return true;
		}

		internal virtual void InternalOnDestroy()
		{
		}

		public virtual void OnDestroy()
		{
			try
			{
				InternalOnDestroy();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			if (m_NetworkObject != null && m_NetworkObject.IsSpawned && IsSpawned)
			{
				m_NetworkObject.OnNetworkBehaviourDestroyed(this);
			}
			if (!m_VarInit)
			{
				InitializeVariables();
			}
			foreach (global::Unity.Netcode.NetworkVariableBase networkVariableField in NetworkVariableFields)
			{
				networkVariableField.Dispose();
			}
		}
	}
}
