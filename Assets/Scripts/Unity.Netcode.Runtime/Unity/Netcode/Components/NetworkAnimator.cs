namespace Unity.Netcode.Components
{
	[global::UnityEngine.AddComponentMenu("Netcode/Network Animator")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/manual/components/helper/networkanimator.html")]
	public class NetworkAnimator : global::Unity.Netcode.NetworkBehaviour, global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::System.Serializable]
		internal class TransitionStateinfo
		{
			public bool IsCrossFadeExit;

			public int Layer;

			public int OriginatingState;

			public int DestinationState;

			public float TransitionDuration;

			public int TriggerNameHash;

			public int TransitionIndex;
		}

		public enum AuthorityModes
		{
			Server = 0,
			Owner = 1
		}

		[global::System.Serializable]
		internal class AnimatorParameterEntry
		{
			[global::UnityEngine.HideInInspector]
			public string name;

			public int NameHash;

			public bool Synchronize;

			public global::UnityEngine.AnimatorControllerParameterType ParameterType;
		}

		[global::System.Serializable]
		internal class AnimatorParametersListContainer
		{
			public global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimator.AnimatorParameterEntry> ParameterEntries = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimator.AnimatorParameterEntry>();
		}

		internal struct AnimationState : global::Unity.Netcode.INetworkSerializable
		{
			internal bool HasBeenProcessed;

			internal int StateHash;

			internal float NormalizedTime;

			internal int Layer;

			internal float Weight;

			internal float Duration;

			internal bool Transition;

			internal bool CrossFade;

			private const byte k_IsTransition = 1;

			private const byte k_IsCrossFade = 2;

			private byte m_StateFlags;

			internal int DestinationStateHash;

			public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
			{
				if (serializer.IsWriter)
				{
					global::Unity.Netcode.FastBufferWriter fastBufferWriter = serializer.GetFastBufferWriter();
					m_StateFlags = 0;
					if (Transition)
					{
						m_StateFlags |= 1;
					}
					if (CrossFade)
					{
						m_StateFlags |= 2;
					}
					serializer.SerializeValue(ref m_StateFlags);
					global::Unity.Netcode.BytePacker.WriteValuePacked(fastBufferWriter, StateHash);
					global::Unity.Netcode.BytePacker.WriteValuePacked(fastBufferWriter, Layer);
					if (Transition)
					{
						global::Unity.Netcode.BytePacker.WriteValuePacked(fastBufferWriter, DestinationStateHash);
					}
				}
				else
				{
					global::Unity.Netcode.FastBufferReader fastBufferReader = serializer.GetFastBufferReader();
					serializer.SerializeValue(ref m_StateFlags);
					Transition = (m_StateFlags & 1) == 1;
					CrossFade = (m_StateFlags & 2) == 2;
					global::Unity.Netcode.ByteUnpacker.ReadValuePacked(fastBufferReader, out StateHash);
					global::Unity.Netcode.ByteUnpacker.ReadValuePacked(fastBufferReader, out Layer);
					if (Transition)
					{
						global::Unity.Netcode.ByteUnpacker.ReadValuePacked(fastBufferReader, out DestinationStateHash);
					}
				}
				serializer.SerializeValue(ref NormalizedTime, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				serializer.SerializeValue(ref Weight, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				if (CrossFade)
				{
					serializer.SerializeValue(ref Duration, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
			}
		}

		internal struct AnimationMessage : global::Unity.Netcode.INetworkSerializable
		{
			internal bool HasBeenProcessed;

			internal global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimator.AnimationState> AnimationStates;

			internal int IsDirtyCount;

			public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
			{
				global::Unity.Netcode.Components.NetworkAnimator.AnimationState animationState = default(global::Unity.Netcode.Components.NetworkAnimator.AnimationState);
				if (serializer.IsReader)
				{
					AnimationStates = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimator.AnimationState>();
					serializer.SerializeValue(ref IsDirtyCount, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					for (int i = 0; i < IsDirtyCount; i++)
					{
						animationState = default(global::Unity.Netcode.Components.NetworkAnimator.AnimationState);
						serializer.SerializeValue(ref animationState, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
						AnimationStates.Add(animationState);
					}
				}
				else
				{
					serializer.SerializeValue(ref IsDirtyCount, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					for (int j = 0; j < IsDirtyCount; j++)
					{
						animationState = AnimationStates[j];
						serializer.SerializeNetworkSerializable(ref animationState);
					}
				}
			}
		}

		internal struct ParametersUpdateMessage : global::Unity.Netcode.INetworkSerializable
		{
			internal byte[] Parameters;

			public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
			{
				serializer.SerializeValue(ref Parameters, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}

		internal struct AnimationTriggerMessage : global::Unity.Netcode.INetworkSerializable
		{
			internal int Hash;

			internal bool IsTriggerSet;

			public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
			{
				serializer.SerializeValue(ref Hash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				serializer.SerializeValue(ref IsTriggerSet, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}

		private struct AnimatorParamCache
		{
			internal bool Exclude;

			internal int Hash;

			internal int Type;

			internal unsafe fixed byte Value[4];
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct AnimationParamEnumWrapper
		{
			internal static readonly int AnimatorControllerParameterInt;

			internal static readonly int AnimatorControllerParameterFloat;

			internal static readonly int AnimatorControllerParameterBool;

			internal static readonly int AnimatorControllerParameterTriggerBool;

			static AnimationParamEnumWrapper()
			{
				AnimatorControllerParameterInt = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.EnumToInt(global::UnityEngine.AnimatorControllerParameterType.Int);
				AnimatorControllerParameterFloat = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.EnumToInt(global::UnityEngine.AnimatorControllerParameterType.Float);
				AnimatorControllerParameterBool = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.EnumToInt(global::UnityEngine.AnimatorControllerParameterType.Bool);
				AnimatorControllerParameterTriggerBool = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.EnumToInt(global::UnityEngine.AnimatorControllerParameterType.Trigger);
			}
		}

		[global::UnityEngine.Tooltip("Selects who has authority(sends state updates) over the<see cref=\"NetworkAnimator\"/> instance.When the network topology is set to distributed authority, this always defaults to owner authority.If server (the default), then only server-side adjustments to the <see cref=\"NetworkAnimator\"> instance will be synchronized with clients. If owner (or client), then only the owner-side adjustments to the <see cref=\"NetworkAnimator\"/> instance will be synchronized with both the server and other clients.")]
		public global::Unity.Netcode.Components.NetworkAnimator.AuthorityModes AuthorityMode;

		[global::UnityEngine.Tooltip("The animator that this NetworkAnimator component will be synchronizing.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Animator m_Animator;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimator.TransitionStateinfo> TransitionStateInfoList;

		private global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<int, global::Unity.Netcode.Components.NetworkAnimator.TransitionStateinfo>> m_DestinationStateToTransitioninfo = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<int, global::Unity.Netcode.Components.NetworkAnimator.TransitionStateinfo>>();

		private global::Unity.Netcode.NetworkManager m_LocalNetworkManager;

		internal bool DistributedAuthorityMode;

		[global::UnityEngine.SerializeField]
		internal global::Unity.Netcode.Components.NetworkAnimator.AnimatorParametersListContainer AnimatorParameterEntries;

		internal global::System.Collections.Generic.Dictionary<int, global::Unity.Netcode.Components.NetworkAnimator.AnimatorParameterEntry> AnimatorParameterEntryTable = new global::System.Collections.Generic.Dictionary<int, global::Unity.Netcode.Components.NetworkAnimator.AnimatorParameterEntry>();

		private int[] m_TransitionHash;

		private int[] m_AnimationHash;

		private float[] m_LayerWeights;

		private static byte[] s_EmptyArray = new byte[0];

		private global::System.Collections.Generic.List<int> m_ParametersToUpdate;

		private global::Unity.Netcode.RpcParams m_RpcParams;

		private global::Unity.Netcode.IGroupRpcTarget m_TargetGroup;

		private global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage m_AnimationMessage;

		private global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler m_NetworkAnimatorStateChangeHandler;

		internal global::System.Collections.Generic.List<global::UnityEngine.AnimatorStateInfo> SynchronizationStateInfo;

		private global::Unity.Netcode.FastBufferWriter m_ParameterWriter;

		private global::Unity.Collections.NativeArray<global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache> m_CachedAnimatorParameters;

		public global::UnityEngine.Animator Animator
		{
			get
			{
				return m_Animator;
			}
			set
			{
				m_Animator = value;
			}
		}

		private void BuildDestinationToTransitionInfoTable()
		{
			foreach (global::Unity.Netcode.Components.NetworkAnimator.TransitionStateinfo transitionStateInfo in TransitionStateInfoList)
			{
				if (!m_DestinationStateToTransitioninfo.ContainsKey(transitionStateInfo.Layer))
				{
					m_DestinationStateToTransitioninfo.Add(transitionStateInfo.Layer, new global::System.Collections.Generic.Dictionary<int, global::Unity.Netcode.Components.NetworkAnimator.TransitionStateinfo>());
				}
				global::System.Collections.Generic.Dictionary<int, global::Unity.Netcode.Components.NetworkAnimator.TransitionStateinfo> dictionary = m_DestinationStateToTransitioninfo[transitionStateInfo.Layer];
				if (!dictionary.ContainsKey(transitionStateInfo.DestinationState))
				{
					dictionary.Add(transitionStateInfo.DestinationState, transitionStateInfo);
				}
			}
		}

		public void OnAfterDeserialize()
		{
			BuildDestinationToTransitionInfoTable();
		}

		public void OnBeforeSerialize()
		{
		}

		public bool IsServerAuthoritative()
		{
			return OnIsServerAuthoritative();
		}

		protected virtual bool OnIsServerAuthoritative()
		{
			if (DistributedAuthorityMode)
			{
				return false;
			}
			return AuthorityMode == global::Unity.Netcode.Components.NetworkAnimator.AuthorityModes.Server;
		}

		private void SpawnCleanup()
		{
			m_NetworkAnimatorStateChangeHandler?.DeregisterUpdate();
			m_NetworkAnimatorStateChangeHandler = null;
		}

		public override void OnDestroy()
		{
			SpawnCleanup();
			m_TargetGroup?.Target?.Dispose();
			_ = m_CachedAnimatorParameters;
			if (m_CachedAnimatorParameters.IsCreated)
			{
				m_CachedAnimatorParameters.Dispose();
			}
			if (m_ParameterWriter.IsInitialized)
			{
				m_ParameterWriter.Dispose();
			}
			base.OnDestroy();
		}

		protected unsafe virtual void Awake()
		{
			if (!m_Animator)
			{
				global::UnityEngine.Debug.LogError("NetworkAnimator " + base.name + " does not have an Animator assigned to it. The NetworkAnimator will not initialize properly.");
				return;
			}
			foreach (global::Unity.Netcode.Components.NetworkAnimator.AnimatorParameterEntry parameterEntry in AnimatorParameterEntries.ParameterEntries)
			{
				AnimatorParameterEntryTable.TryAdd(parameterEntry.NameHash, parameterEntry);
			}
			int layerCount = m_Animator.layerCount;
			m_TransitionHash = new int[layerCount];
			m_AnimationHash = new int[layerCount];
			m_LayerWeights = new float[layerCount];
			m_AnimationMessage = new global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage
			{
				AnimationStates = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkAnimator.AnimationState>()
			};
			for (int i = 0; i < m_Animator.layerCount; i++)
			{
				m_AnimationMessage.AnimationStates.Add(default(global::Unity.Netcode.Components.NetworkAnimator.AnimationState));
				float layerWeight = m_Animator.GetLayerWeight(i);
				if (layerWeight != m_LayerWeights[i])
				{
					m_LayerWeights[i] = layerWeight;
				}
			}
			int num = 4;
			global::UnityEngine.AnimatorControllerParameter[] parameters = m_Animator.parameters;
			m_CachedAnimatorParameters = new global::Unity.Collections.NativeArray<global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache>(parameters.Length, global::Unity.Collections.Allocator.Persistent);
			m_ParametersToUpdate = new global::System.Collections.Generic.List<int>(parameters.Length);
			for (int j = 0; j < parameters.Length; j++)
			{
				global::UnityEngine.AnimatorControllerParameter animatorControllerParameter = parameters[j];
				bool flag = true;
				if (AnimatorParameterEntryTable.ContainsKey(animatorControllerParameter.nameHash))
				{
					flag = AnimatorParameterEntryTable[animatorControllerParameter.nameHash].Synchronize;
				}
				global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache value = new global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache
				{
					Type = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.EnumToInt(animatorControllerParameter.type),
					Hash = animatorControllerParameter.nameHash,
					Exclude = !flag
				};
				switch (animatorControllerParameter.type)
				{
				case global::UnityEngine.AnimatorControllerParameterType.Float:
				{
					float value3 = m_Animator.GetFloat(value.Hash);
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(value.Value, 0, value3);
					break;
				}
				case global::UnityEngine.AnimatorControllerParameterType.Int:
				{
					int integer = m_Animator.GetInteger(value.Hash);
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(value.Value, 0, integer);
					break;
				}
				case global::UnityEngine.AnimatorControllerParameterType.Bool:
				{
					bool value2 = m_Animator.GetBool(value.Hash);
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(value.Value, 0, value2);
					break;
				}
				}
				m_CachedAnimatorParameters[j] = value;
				switch (animatorControllerParameter.type)
				{
				case global::UnityEngine.AnimatorControllerParameterType.Int:
					num += 8;
					break;
				case global::UnityEngine.AnimatorControllerParameterType.Bool:
				case global::UnityEngine.AnimatorControllerParameterType.Trigger:
					num += 5;
					break;
				case global::UnityEngine.AnimatorControllerParameterType.Float:
					num += 8;
					break;
				}
			}
			if (m_ParameterWriter.IsInitialized)
			{
				m_ParameterWriter.Dispose();
			}
			m_ParameterWriter = new global::Unity.Netcode.FastBufferWriter(num, global::Unity.Collections.Allocator.Persistent);
		}

		internal global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage GetAnimationMessage()
		{
			return m_AnimationMessage;
		}

		internal override void InternalOnNetworkPreSpawn(ref global::Unity.Netcode.NetworkManager networkManager)
		{
			m_LocalNetworkManager = networkManager;
			DistributedAuthorityMode = m_LocalNetworkManager.DistributedAuthorityMode;
		}

		public override void OnNetworkSpawn()
		{
			if (m_Animator == null)
			{
				global::Unity.Netcode.NetworkLog.LogWarningServer("[" + base.gameObject.name + "][NetworkAnimator] Animator is not assigned! Animation synchronization will not work for this instance!");
			}
			m_TargetGroup = base.RpcTarget.Group(new global::System.Collections.Generic.List<ulong>(128), global::Unity.Netcode.RpcTargetUse.Persistent) as global::Unity.Netcode.IGroupRpcTarget;
			m_RpcParams = new global::Unity.Netcode.RpcParams
			{
				Send = new global::Unity.Netcode.RpcSendParams
				{
					Target = m_TargetGroup?.Target
				}
			};
			m_NetworkAnimatorStateChangeHandler = new global::Unity.Netcode.Components.NetworkAnimatorStateChangeHandler(this);
		}

		public override void OnNetworkDespawn()
		{
			SpawnCleanup();
		}

		private void WriteSynchronizationData<T>(ref global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
			m_ParametersToUpdate.Clear();
			for (int i = 0; i < m_CachedAnimatorParameters.Length; i++)
			{
				if (!m_CachedAnimatorParameters[i].Exclude)
				{
					m_ParametersToUpdate.Add(i);
				}
			}
			WriteParameters(ref m_ParameterWriter);
			global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage value = new global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage
			{
				Parameters = m_ParameterWriter.ToArray()
			};
			serializer.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			m_AnimationMessage.IsDirtyCount = 0;
			for (int j = 0; j < m_Animator.layerCount; j++)
			{
				global::UnityEngine.AnimatorStateInfo currentAnimatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(j);
				SynchronizationStateInfo?.Add(currentAnimatorStateInfo);
				int stateHash = currentAnimatorStateInfo.fullPathHash;
				float normalizedTime = currentAnimatorStateInfo.normalizedTime;
				bool flag = m_Animator.IsInTransition(j);
				global::Unity.Netcode.Components.NetworkAnimator.AnimationState value2 = m_AnimationMessage.AnimationStates[j];
				if (flag)
				{
					global::UnityEngine.AnimatorTransitionInfo animatorTransitionInfo = m_Animator.GetAnimatorTransitionInfo(j);
					global::UnityEngine.AnimatorStateInfo nextAnimatorStateInfo = m_Animator.GetNextAnimatorStateInfo(j);
					if (nextAnimatorStateInfo.length > 0f)
					{
						float num = nextAnimatorStateInfo.speed * nextAnimatorStateInfo.speedMultiplier;
						float num2 = nextAnimatorStateInfo.length * num;
						float num3 = global::UnityEngine.Mathf.Min(animatorTransitionInfo.duration, animatorTransitionInfo.duration * animatorTransitionInfo.normalizedTime) * 0.5f;
						normalizedTime = global::UnityEngine.Mathf.Min(1f, (num3 > 0f) ? (num3 / num2) : 0f);
					}
					else
					{
						normalizedTime = 0f;
					}
					stateHash = nextAnimatorStateInfo.fullPathHash;
					if (m_DestinationStateToTransitioninfo.ContainsKey(j) && m_DestinationStateToTransitioninfo[j].ContainsKey(nextAnimatorStateInfo.shortNameHash))
					{
						global::Unity.Netcode.Components.NetworkAnimator.TransitionStateinfo transitionStateinfo = m_DestinationStateToTransitioninfo[j][nextAnimatorStateInfo.shortNameHash];
						stateHash = transitionStateinfo.OriginatingState;
						value2.DestinationStateHash = transitionStateinfo.DestinationState;
					}
				}
				value2.Transition = flag;
				value2.StateHash = stateHash;
				value2.NormalizedTime = normalizedTime;
				value2.Layer = j;
				value2.Weight = m_LayerWeights[j];
				m_AnimationMessage.AnimationStates[j] = value2;
			}
			m_AnimationMessage.IsDirtyCount = m_Animator.layerCount;
			m_AnimationMessage.NetworkSerialize(serializer);
		}

		protected override void OnSynchronize<T>(ref global::Unity.Netcode.BufferSerializer<T> serializer)
		{
			if (serializer.IsWriter)
			{
				WriteSynchronizationData(ref serializer);
				return;
			}
			global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage value = default(global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage);
			global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage value2 = default(global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage);
			serializer.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			UpdateParameters(ref value);
			serializer.SerializeValue(ref value2, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			foreach (global::Unity.Netcode.Components.NetworkAnimator.AnimationState animationState in value2.AnimationStates)
			{
				UpdateAnimationState(animationState);
			}
		}

		private void CheckForStateChange(int layer)
		{
			bool flag = false;
			global::Unity.Netcode.Components.NetworkAnimator.AnimationState value = m_AnimationMessage.AnimationStates[m_AnimationMessage.IsDirtyCount];
			float layerWeight = m_Animator.GetLayerWeight(layer);
			value.CrossFade = false;
			value.Transition = false;
			value.NormalizedTime = 0f;
			value.Layer = layer;
			value.Duration = 0f;
			value.Weight = m_LayerWeights[layer];
			value.DestinationStateHash = 0;
			if (layerWeight != m_LayerWeights[layer])
			{
				m_LayerWeights[layer] = layerWeight;
				flag = true;
				value.Weight = layerWeight;
			}
			global::UnityEngine.AnimatorStateInfo currentAnimatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(layer);
			if (m_Animator.IsInTransition(layer))
			{
				global::UnityEngine.AnimatorTransitionInfo animatorTransitionInfo = m_Animator.GetAnimatorTransitionInfo(layer);
				global::UnityEngine.AnimatorStateInfo nextAnimatorStateInfo = m_Animator.GetNextAnimatorStateInfo(layer);
				if (animatorTransitionInfo.anyState && animatorTransitionInfo.fullPathHash == 0 && m_TransitionHash[layer] != nextAnimatorStateInfo.fullPathHash)
				{
					m_TransitionHash[layer] = nextAnimatorStateInfo.fullPathHash;
					m_AnimationHash[layer] = 0;
					value.DestinationStateHash = nextAnimatorStateInfo.fullPathHash;
					value.CrossFade = true;
					value.Transition = true;
					value.Duration = animatorTransitionInfo.duration;
					value.NormalizedTime = animatorTransitionInfo.normalizedTime;
					flag = true;
				}
				else if (!animatorTransitionInfo.anyState && animatorTransitionInfo.fullPathHash != m_TransitionHash[layer] && (!m_DestinationStateToTransitioninfo.ContainsKey(layer) || (m_DestinationStateToTransitioninfo.ContainsKey(layer) && m_DestinationStateToTransitioninfo[layer].ContainsKey(nextAnimatorStateInfo.fullPathHash))))
				{
					m_TransitionHash[layer] = animatorTransitionInfo.fullPathHash;
					m_AnimationHash[layer] = 0;
					value.StateHash = animatorTransitionInfo.fullPathHash;
					value.CrossFade = false;
					value.Transition = true;
					value.NormalizedTime = animatorTransitionInfo.normalizedTime;
					if (m_DestinationStateToTransitioninfo.ContainsKey(layer) && m_DestinationStateToTransitioninfo[layer].ContainsKey(nextAnimatorStateInfo.fullPathHash))
					{
						value.DestinationStateHash = nextAnimatorStateInfo.fullPathHash;
					}
					flag = true;
				}
			}
			else if (currentAnimatorStateInfo.fullPathHash != m_AnimationHash[layer])
			{
				m_TransitionHash[layer] = 0;
				m_AnimationHash[layer] = currentAnimatorStateInfo.fullPathHash;
				if (m_AnimationHash[layer] != 0)
				{
					value.StateHash = currentAnimatorStateInfo.fullPathHash;
					value.NormalizedTime = currentAnimatorStateInfo.normalizedTime;
				}
				flag = true;
			}
			if (flag)
			{
				m_AnimationMessage.AnimationStates[m_AnimationMessage.IsDirtyCount] = value;
				m_AnimationMessage.IsDirtyCount++;
			}
		}

		internal void CheckForAnimatorChanges()
		{
			if (CheckParametersChanged())
			{
				SendParametersUpdate();
			}
			if (m_Animator.runtimeAnimatorController == null)
			{
				if (m_LocalNetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::UnityEngine.Debug.LogError("[" + GetType().Name + "] Could not find an assigned RuntimeAnimatorController! Cannot check Animator for changes in state!");
				}
				return;
			}
			m_AnimationMessage.IsDirtyCount = 0;
			for (int i = 0; i < m_Animator.layerCount; i++)
			{
				global::UnityEngine.AnimatorStateInfo currentAnimatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(i);
				float num = currentAnimatorStateInfo.speed * currentAnimatorStateInfo.speedMultiplier;
				if (num > 0f)
				{
					_ = 1f / num;
				}
				CheckForStateChange(i);
			}
			if (m_AnimationMessage.IsDirtyCount <= 0)
			{
				return;
			}
			if (DistributedAuthorityMode)
			{
				SendAnimStateRpc(m_AnimationMessage);
				return;
			}
			if (!base.IsServer && base.IsOwner)
			{
				SendServerAnimStateRpc(m_AnimationMessage);
				return;
			}
			m_TargetGroup.Clear();
			foreach (ulong connectedClientId in m_LocalNetworkManager.ConnectionManager.ConnectedClientIds)
			{
				if (connectedClientId != m_LocalNetworkManager.LocalClientId && base.NetworkObject.Observers.Contains(connectedClientId))
				{
					m_TargetGroup.Add(connectedClientId);
				}
			}
			m_RpcParams.Send.Target = m_TargetGroup.Target;
			SendClientAnimStateRpc(m_AnimationMessage, m_RpcParams);
		}

		private void SendParametersUpdate(global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams), bool sendDirect = false)
		{
			WriteParameters(ref m_ParameterWriter);
			global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage parametersUpdateMessage = new global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage
			{
				Parameters = m_ParameterWriter.ToArray()
			};
			if (DistributedAuthorityMode)
			{
				if (base.IsOwner)
				{
					SendParametersUpdateRpc(parametersUpdateMessage);
				}
				else
				{
					global::UnityEngine.Debug.LogError($"[{base.name}][Client-{m_LocalNetworkManager.LocalClientId}] Attempting to send parameter updates but not the owner!");
				}
			}
			else if (!base.IsServer)
			{
				SendServerParametersUpdateRpc(parametersUpdateMessage);
			}
			else if (sendDirect)
			{
				SendClientParametersUpdateRpc(parametersUpdateMessage, rpcParams);
			}
			else
			{
				m_NetworkAnimatorStateChangeHandler.SendParameterUpdate(parametersUpdateMessage, rpcParams);
			}
		}

		private unsafe T GetValue<T>(ref global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache animatorParamCache)
		{
			T result;
			fixed (byte* value = animatorParamCache.Value)
			{
				void* source = value;
				result = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(source, 0);
			}
			return result;
		}

		private unsafe bool CheckParametersChanged()
		{
			m_ParametersToUpdate.Clear();
			for (int i = 0; i < m_CachedAnimatorParameters.Length; i++)
			{
				ref global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_CachedAnimatorParameters), i);
				if (reference.Exclude || m_Animator.IsParameterControlledByCurve(reference.Hash))
				{
					continue;
				}
				int hash = reference.Hash;
				if (reference.Type == global::Unity.Netcode.Components.NetworkAnimator.AnimationParamEnumWrapper.AnimatorControllerParameterInt)
				{
					int integer = m_Animator.GetInteger(hash);
					if (GetValue<int>(ref reference) != integer)
					{
						m_ParametersToUpdate.Add(i);
					}
				}
				else if (reference.Type == global::Unity.Netcode.Components.NetworkAnimator.AnimationParamEnumWrapper.AnimatorControllerParameterBool)
				{
					bool flag = m_Animator.GetBool(hash);
					if (GetValue<bool>(ref reference) != flag)
					{
						m_ParametersToUpdate.Add(i);
					}
				}
				else if (reference.Type == global::Unity.Netcode.Components.NetworkAnimator.AnimationParamEnumWrapper.AnimatorControllerParameterFloat)
				{
					float num = m_Animator.GetFloat(hash);
					if (GetValue<float>(ref reference) != num)
					{
						m_ParametersToUpdate.Add(i);
					}
				}
			}
			return m_ParametersToUpdate.Count > 0;
		}

		private unsafe void WriteParameters(ref global::Unity.Netcode.FastBufferWriter writer)
		{
			writer.Seek(0);
			writer.Truncate();
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, (uint)m_ParametersToUpdate.Count);
			foreach (int item in m_ParametersToUpdate)
			{
				ref global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_CachedAnimatorParameters), item);
				if (reference.Exclude)
				{
					global::UnityEngine.Debug.LogWarning($"Parameter hash:{reference.Hash} should be excluded but is in the parameters to update list when writing parameter values!");
					continue;
				}
				int hash = reference.Hash;
				global::Unity.Netcode.BytePacker.WriteValuePacked(writer, (uint)item);
				if (reference.Type == global::Unity.Netcode.Components.NetworkAnimator.AnimationParamEnumWrapper.AnimatorControllerParameterInt)
				{
					int integer = m_Animator.GetInteger(hash);
					fixed (byte* value = reference.Value)
					{
						void* destination = value;
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(destination, 0, integer);
						global::Unity.Netcode.BytePacker.WriteValuePacked(writer, (uint)integer);
					}
				}
				else if (reference.Type == global::Unity.Netcode.Components.NetworkAnimator.AnimationParamEnumWrapper.AnimatorControllerParameterBool)
				{
					bool value2 = m_Animator.GetBool(hash);
					fixed (byte* value = reference.Value)
					{
						void* destination2 = value;
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(destination2, 0, value2);
						global::Unity.Netcode.BytePacker.WriteValuePacked(writer, value2);
					}
				}
				else
				{
					if (reference.Type != global::Unity.Netcode.Components.NetworkAnimator.AnimationParamEnumWrapper.AnimatorControllerParameterFloat)
					{
						continue;
					}
					float value3 = m_Animator.GetFloat(hash);
					fixed (byte* value = reference.Value)
					{
						void* destination3 = value;
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(destination3, 0, value3);
						global::Unity.Netcode.BytePacker.WriteValuePacked(writer, value3);
					}
				}
			}
		}

		private unsafe void ReadParameters(global::Unity.Netcode.FastBufferReader reader)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out uint value);
			for (int i = 0; i < value; i++)
			{
				global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out uint value2);
				ref global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_CachedAnimatorParameters), (int)value2);
				int hash = reference.Hash;
				if (reference.Type == global::Unity.Netcode.Components.NetworkAnimator.AnimationParamEnumWrapper.AnimatorControllerParameterInt)
				{
					global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out uint value3);
					m_Animator.SetInteger(hash, (int)value3);
					fixed (byte* value4 = reference.Value)
					{
						void* destination = value4;
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(destination, 0, value3);
					}
				}
				else if (reference.Type == global::Unity.Netcode.Components.NetworkAnimator.AnimationParamEnumWrapper.AnimatorControllerParameterBool)
				{
					global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out bool value5);
					m_Animator.SetBool(hash, value5);
					fixed (byte* value4 = reference.Value)
					{
						void* destination2 = value4;
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(destination2, 0, value5);
					}
				}
				else if (reference.Type == global::Unity.Netcode.Components.NetworkAnimator.AnimationParamEnumWrapper.AnimatorControllerParameterFloat)
				{
					global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out float value6);
					m_Animator.SetFloat(hash, value6);
					fixed (byte* value4 = reference.Value)
					{
						void* destination3 = value4;
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(destination3, 0, value6);
					}
				}
			}
		}

		internal unsafe void UpdateParameters(ref global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage parametersUpdate)
		{
			if (parametersUpdate.Parameters != null && parametersUpdate.Parameters.Length != 0)
			{
				fixed (byte* parameters = parametersUpdate.Parameters)
				{
					global::Unity.Netcode.FastBufferReader reader = new global::Unity.Netcode.FastBufferReader(parameters, global::Unity.Collections.Allocator.None, parametersUpdate.Parameters.Length);
					ReadParameters(reader);
				}
			}
		}

		internal void UpdateAnimationState(global::Unity.Netcode.Components.NetworkAnimator.AnimationState animationState)
		{
			if (animationState.Layer < m_LayerWeights.Length && m_LayerWeights[animationState.Layer] != animationState.Weight)
			{
				m_Animator.SetLayerWeight(animationState.Layer, animationState.Weight);
				m_LayerWeights[animationState.Layer] = animationState.Weight;
			}
			if (animationState.StateHash == 0 && !animationState.Transition)
			{
				return;
			}
			global::UnityEngine.AnimatorStateInfo currentAnimatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(animationState.Layer);
			if (animationState.Transition && !animationState.CrossFade)
			{
				if (!m_DestinationStateToTransitioninfo.ContainsKey(animationState.Layer))
				{
					return;
				}
				if (m_DestinationStateToTransitioninfo[animationState.Layer].ContainsKey(animationState.DestinationStateHash))
				{
					if (currentAnimatorStateInfo.shortNameHash == animationState.StateHash)
					{
						global::Unity.Netcode.Components.NetworkAnimator.TransitionStateinfo transitionStateinfo = m_DestinationStateToTransitioninfo[animationState.Layer][animationState.DestinationStateHash];
						m_Animator.CrossFade(transitionStateinfo.DestinationState, transitionStateinfo.TransitionDuration, transitionStateinfo.Layer, 0f, animationState.NormalizedTime);
					}
					else if (m_LocalNetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogWarning($"Current State Hash ({currentAnimatorStateInfo.fullPathHash}) != AnimationState.StateHash ({animationState.StateHash})");
					}
				}
				else if (m_LocalNetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogError($"[DestinationState To Transition Info] Layer ({animationState.Layer}) sub-table does not contain destination state ({animationState.DestinationStateHash})!");
				}
			}
			else if (animationState.Transition && animationState.CrossFade)
			{
				m_Animator.CrossFade(animationState.DestinationStateHash, animationState.Duration, animationState.Layer, animationState.NormalizedTime);
			}
			else if (currentAnimatorStateInfo.fullPathHash != animationState.StateHash && m_Animator.HasState(animationState.Layer, animationState.StateHash))
			{
				m_Animator.Play(animationState.StateHash, animationState.Layer, animationState.NormalizedTime);
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, AllowTargetOverride = true, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner)]
		private void SendServerParametersUpdateRpc(global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage parametersUpdate, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcParams rpcParams2 = rpcParams;
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					AllowTargetOverride = true,
					InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner
				};
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(3694964623u, rpcParams2, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in parametersUpdate, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 3694964623u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				return;
			}
			__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			if (IsServerAuthoritative())
			{
				m_NetworkAnimatorStateChangeHandler.SendParameterUpdate(parametersUpdate);
			}
			else
			{
				if (rpcParams.Receive.SenderClientId != base.OwnerClientId)
				{
					return;
				}
				UpdateParameters(ref parametersUpdate);
				global::System.Collections.Generic.List<ulong> connectedClientIds = m_LocalNetworkManager.ConnectionManager.ConnectedClientIds;
				if (connectedClientIds.Count <= ((!base.IsHost) ? 1 : 2))
				{
					return;
				}
				m_TargetGroup.Clear();
				foreach (ulong item in connectedClientIds)
				{
					if (item != rpcParams.Receive.SenderClientId && item != 0L && base.NetworkObject.Observers.Contains(item))
					{
						m_TargetGroup.Add(item);
					}
				}
				m_RpcParams.Send.Target = m_TargetGroup.Target;
				m_NetworkAnimatorStateChangeHandler.SendParameterUpdate(parametersUpdate, m_RpcParams);
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.NotAuthority, AllowTargetOverride = true, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner)]
		internal void SendParametersUpdateRpc(global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage parametersUpdate, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					AllowTargetOverride = true,
					InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner
				};
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(551951539u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotAuthority, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in parametersUpdate, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 551951539u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotAuthority, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				m_NetworkAnimatorStateChangeHandler.ProcessParameterUpdate(parametersUpdate);
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.NotMe, AllowTargetOverride = true)]
		internal void SendClientParametersUpdateRpc(global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage parametersUpdate, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					AllowTargetOverride = true
				};
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(1191808936u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotMe, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in parametersUpdate, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 1191808936u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotMe, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				bool flag = IsServerAuthoritative();
				if ((!flag && !base.IsOwner) || (flag && !base.IsServer))
				{
					m_NetworkAnimatorStateChangeHandler.ProcessParameterUpdate(parametersUpdate);
				}
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, AllowTargetOverride = true)]
		private void SendServerAnimStateRpc(global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage animationMessage, global::Unity.Netcode.RpcParams rcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcParams rpcParams = rcParams;
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					AllowTargetOverride = true
				};
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(3791456297u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in animationMessage, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 3791456297u, rcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				return;
			}
			__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			if (IsServerAuthoritative())
			{
				m_NetworkAnimatorStateChangeHandler.SendAnimationUpdate(animationMessage);
			}
			else
			{
				if (rcParams.Receive.SenderClientId != base.OwnerClientId)
				{
					return;
				}
				foreach (global::Unity.Netcode.Components.NetworkAnimator.AnimationState animationState in animationMessage.AnimationStates)
				{
					UpdateAnimationState(animationState);
				}
				global::System.Collections.Generic.List<ulong> connectedClientIds = m_LocalNetworkManager.ConnectionManager.ConnectedClientIds;
				if (connectedClientIds.Count <= ((!base.IsHost) ? 1 : 2))
				{
					return;
				}
				m_TargetGroup.Clear();
				foreach (ulong item in connectedClientIds)
				{
					if (item != rcParams.Receive.SenderClientId && item != 0L && base.NetworkObject.Observers.Contains(item))
					{
						m_TargetGroup.Add(item);
					}
				}
				m_RpcParams.Send.Target = m_TargetGroup.Target;
				m_NetworkAnimatorStateChangeHandler.SendAnimationUpdate(animationMessage, m_RpcParams);
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.NotServer, AllowTargetOverride = true)]
		internal void SendClientAnimStateRpc(global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage animationMessage, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					AllowTargetOverride = true
				};
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(1325919956u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotServer, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in animationMessage, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 1325919956u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotServer, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				ProcessAnimStates(animationMessage);
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.NotAuthority, AllowTargetOverride = true, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner)]
		internal void SendAnimStateRpc(global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage animationMessage, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					AllowTargetOverride = true,
					InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner
				};
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(1811559919u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotAuthority, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in animationMessage, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 1811559919u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotAuthority, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				ProcessAnimStates(animationMessage);
			}
		}

		private void ProcessAnimStates(global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage animationMessage)
		{
			if (base.HasAuthority)
			{
				if (m_LocalNetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					string text = (DistributedAuthorityMode ? "Owner" : "Host");
					string text2 = (DistributedAuthorityMode ? "distributed authority" : "client-server");
					global::Unity.Netcode.NetworkLog.LogWarning("Detected the " + text + " is sending itself animation updates in " + text2 + " mode! Please report this issue.");
				}
				return;
			}
			foreach (global::Unity.Netcode.Components.NetworkAnimator.AnimationState animationState in animationMessage.AnimationStates)
			{
				UpdateAnimationState(animationState);
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, AllowTargetOverride = true)]
		internal void SendServerAnimTriggerRpc(global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage animationTriggerMessage, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcParams rpcParams2 = rpcParams;
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					AllowTargetOverride = true
				};
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(2164765908u, rpcParams2, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in animationTriggerMessage, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 2164765908u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				return;
			}
			__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			if (rpcParams.Receive.SenderClientId != base.OwnerClientId)
			{
				if (m_LocalNetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("[Owner Authoritative] Detected the a non-authoritative client is sending the server animation trigger updates. If you recently changed ownership of the " + base.name + " object, then this could be the reason.");
				}
				return;
			}
			InternalSetTrigger(animationTriggerMessage.Hash, animationTriggerMessage.IsTriggerSet);
			global::System.Collections.Generic.List<ulong> connectedClientIds = m_LocalNetworkManager.ConnectionManager.ConnectedClientIds;
			m_TargetGroup.Clear();
			foreach (ulong item in connectedClientIds)
			{
				if (item != 0L && base.NetworkObject.Observers.Contains(item))
				{
					m_TargetGroup.Add(item);
				}
			}
			if (IsServerAuthoritative())
			{
				m_NetworkAnimatorStateChangeHandler.QueueTriggerUpdateToClient(animationTriggerMessage, m_RpcParams);
			}
			else if (connectedClientIds.Count > ((!base.IsHost) ? 1 : 2))
			{
				m_NetworkAnimatorStateChangeHandler.QueueTriggerUpdateToClient(animationTriggerMessage, m_RpcParams);
			}
		}

		private void InternalSetTrigger(int hash, bool isSet = true)
		{
			m_Animator.SetBool(hash, isSet);
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.NotAuthority, AllowTargetOverride = true, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner)]
		internal void SendAnimTriggerRpc(global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage animationTriggerMessage, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					AllowTargetOverride = true,
					InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner
				};
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(2626440142u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotAuthority, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in animationTriggerMessage, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 2626440142u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotAuthority, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				InternalSetTrigger(animationTriggerMessage.Hash, animationTriggerMessage.IsTriggerSet);
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.NotServer, AllowTargetOverride = true)]
		internal void SendClientAnimTriggerRpc(global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage animationTriggerMessage, global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams))
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					AllowTargetOverride = true
				};
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(3966065403u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotServer, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in animationTriggerMessage, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				__endSendRpc(ref bufferWriter, 3966065403u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.NotServer, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				InternalSetTrigger(animationTriggerMessage.Hash, animationTriggerMessage.IsTriggerSet);
			}
		}

		public void SetTrigger(string triggerName)
		{
			SetTrigger(global::UnityEngine.Animator.StringToHash(triggerName));
		}

		public void SetTrigger(int hash, bool setTrigger = true)
		{
			if (!base.IsSpawned)
			{
				global::Unity.Netcode.NetworkLog.LogError("[" + base.gameObject.name + "] Cannot set a synchronized trigger when the NetworkObject is not spawned!");
				return;
			}
			global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage animationTriggerMessage = new global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage
			{
				Hash = hash,
				IsTriggerSet = setTrigger
			};
			if (DistributedAuthorityMode && base.HasAuthority)
			{
				m_NetworkAnimatorStateChangeHandler.QueueTriggerUpdateToClient(animationTriggerMessage);
				InternalSetTrigger(hash, setTrigger);
			}
			else
			{
				if (DistributedAuthorityMode || (!base.IsOwner && !base.IsServer))
				{
					return;
				}
				if (base.IsServer)
				{
					m_NetworkAnimatorStateChangeHandler.QueueTriggerUpdateToClient(animationTriggerMessage);
					InternalSetTrigger(hash, setTrigger);
					return;
				}
				m_NetworkAnimatorStateChangeHandler.QueueTriggerUpdateToServer(animationTriggerMessage);
				if (!IsServerAuthoritative())
				{
					InternalSetTrigger(hash, setTrigger);
				}
			}
		}

		public void ResetTrigger(string triggerName)
		{
			ResetTrigger(global::UnityEngine.Animator.StringToHash(triggerName));
		}

		public void ResetTrigger(int hash)
		{
			SetTrigger(hash, setTrigger: false);
		}

		public void EnableParameterSynchronization(string parameterName, bool isEnabled)
		{
			EnableParameterSynchronization(global::UnityEngine.Animator.StringToHash(parameterName), isEnabled);
		}

		public void EnableParameterSynchronization(int parameterNameHash, bool isEnabled)
		{
			bool flag = OnIsServerAuthoritative();
			if (base.IsSpawned && (!flag || !base.IsServer) && (flag || !base.IsOwner))
			{
				return;
			}
			for (int i = 0; i < m_CachedAnimatorParameters.Length; i++)
			{
				global::Unity.Netcode.Components.NetworkAnimator.AnimatorParamCache value = m_CachedAnimatorParameters[i];
				if (value.Hash == parameterNameHash)
				{
					value.Exclude = !isEnabled;
					m_CachedAnimatorParameters[i] = value;
					break;
				}
			}
		}

		protected override void __initializeVariables()
		{
			base.__initializeVariables();
		}

		protected override void __initializeRpcs()
		{
			__registerRpc(3694964623u, __rpc_handler_3694964623, "SendServerParametersUpdateRpc", global::Unity.Netcode.RpcInvokePermission.Owner);
			__registerRpc(551951539u, __rpc_handler_551951539, "SendParametersUpdateRpc", global::Unity.Netcode.RpcInvokePermission.Owner);
			__registerRpc(1191808936u, __rpc_handler_1191808936, "SendClientParametersUpdateRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
			__registerRpc(3791456297u, __rpc_handler_3791456297, "SendServerAnimStateRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
			__registerRpc(1325919956u, __rpc_handler_1325919956, "SendClientAnimStateRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
			__registerRpc(1811559919u, __rpc_handler_1811559919, "SendAnimStateRpc", global::Unity.Netcode.RpcInvokePermission.Owner);
			__registerRpc(2164765908u, __rpc_handler_2164765908, "SendServerAnimTriggerRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
			__registerRpc(2626440142u, __rpc_handler_2626440142, "SendAnimTriggerRpc", global::Unity.Netcode.RpcInvokePermission.Owner);
			__registerRpc(3966065403u, __rpc_handler_3966065403, "SendClientAnimTriggerRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
			base.__initializeRpcs();
		}

		private static void __rpc_handler_3694964623(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkAnimator)target).SendServerParametersUpdateRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		private static void __rpc_handler_551951539(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkAnimator)target).SendParametersUpdateRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		private static void __rpc_handler_1191808936(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.Components.NetworkAnimator.ParametersUpdateMessage value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkAnimator)target).SendClientParametersUpdateRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		private static void __rpc_handler_3791456297(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkAnimator)target).SendServerAnimStateRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		private static void __rpc_handler_1325919956(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkAnimator)target).SendClientAnimStateRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		private static void __rpc_handler_1811559919(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.Components.NetworkAnimator.AnimationMessage value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkAnimator)target).SendAnimStateRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		private static void __rpc_handler_2164765908(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkAnimator)target).SendServerAnimTriggerRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		private static void __rpc_handler_2626440142(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkAnimator)target).SendAnimTriggerRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		private static void __rpc_handler_3966065403(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.Components.NetworkAnimator.AnimationTriggerMessage value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkAnimator)target).SendClientAnimTriggerRpc(value, ext);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		protected internal override string __getTypeName()
		{
			return "NetworkAnimator";
		}
	}
}
