namespace Unity.Netcode
{
	[global::UnityEngine.AddComponentMenu("Netcode/Network Object", -99)]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/manual/components/core/networkobject.html")]
	public sealed class NetworkObject : global::UnityEngine.MonoBehaviour
	{
		public delegate bool OnDeferedDespawnCompleteDelegateHandler();

		[global::System.Flags]
		public enum OwnershipStatus
		{
			None = 0,
			Distributable = 1,
			Transferable = 2,
			RequestRequired = 4,
			SessionOwner = 8,
			All = -1
		}

		[global::System.Flags]
		internal enum OwnershipStatusExtended
		{
			Requested = 0x100,
			Locked = 0x200
		}

		public enum OwnershipPermissionsFailureStatus
		{
			Locked = 0,
			RequestRequired = 1,
			RequestInProgress = 2,
			NotTransferrable = 3,
			SessionOwnerOnly = 4
		}

		public delegate void OnOwnershipPermissionsFailureDelegateHandler(global::Unity.Netcode.NetworkObject.OwnershipPermissionsFailureStatus changeOwnershipFailure);

		public enum OwnershipRequestStatus
		{
			RequestSent = 0,
			AlreadyOwner = 1,
			RequestRequiredNotSet = 2,
			Locked = 3,
			RequestInProgress = 4,
			SessionOwnerOnly = 5,
			InvalidOperation = 6
		}

		public delegate bool OnOwnershipRequestedDelegateHandler(ulong clientRequesting);

		public enum OwnershipRequestResponseStatus
		{
			Approved = 0,
			Locked = 1,
			RequestInProgress = 2,
			CannotRequest = 3,
			Denied = 4
		}

		public delegate void OnOwnershipRequestResponseDelegateHandler(global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus ownershipRequestResponse);

		public enum OwnershipLockActions
		{
			None = 0,
			SetAndLock = 1,
			SetAndUnlock = 2
		}

		public delegate bool VisibilityDelegate(ulong clientId);

		public delegate bool SpawnDelegate(ulong clientId);

		internal struct SerializedObject
		{
			public struct TransformData : global::Unity.Netcode.INetworkSerializeByMemcpy
			{
				public global::UnityEngine.Vector3 Position;

				public global::UnityEngine.Quaternion Rotation;

				public global::UnityEngine.Vector3 Scale;
			}

			public uint Hash;

			public ulong NetworkObjectId;

			public ulong OwnerClientId;

			public ushort OwnershipFlags;

			private const ushort k_IsPlayerObject = 1;

			private const ushort k_HasParent = 2;

			private const ushort k_IsSceneObject = 4;

			private const ushort k_HasTransform = 8;

			private const ushort k_IsLatestParentSet = 16;

			private const ushort k_WorldPositionStays = 32;

			private const ushort k_DestroyWithScene = 64;

			private const ushort k_DontDestroyWithOwner = 128;

			private const ushort k_HasOwnershipFlags = 256;

			private const ushort k_SyncObservers = 512;

			private const ushort k_SpawnWithObservers = 1024;

			private const ushort k_HasInstantiationData = 2048;

			public bool IsPlayerObject;

			public bool HasParent;

			public bool IsSceneObject;

			public bool HasTransform;

			public bool IsLatestParentSet;

			public bool WorldPositionStays;

			public bool DestroyWithScene;

			public bool DontDestroyWithOwner;

			public bool HasOwnershipFlags;

			public bool SyncObservers;

			public bool SpawnWithObservers;

			public bool HasInstantiationData;

			public ulong[] Observers;

			public ulong ParentObjectId;

			public global::Unity.Netcode.NetworkObject.SerializedObject.TransformData Transform;

			public ulong? LatestParent;

			public global::Unity.Netcode.NetworkObject OwnerObject;

			public ulong TargetClientId;

			public global::Unity.Netcode.NetworkSceneHandle NetworkSceneHandle;

			internal int SynchronizationDataSize;

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal ushort GetBitsetRepresentation()
			{
				ushort num = 0;
				if (IsPlayerObject)
				{
					num |= 1;
				}
				if (HasParent)
				{
					num |= 2;
				}
				if (IsSceneObject)
				{
					num |= 4;
				}
				if (HasTransform)
				{
					num |= 8;
				}
				if (IsLatestParentSet)
				{
					num |= 0x10;
				}
				if (WorldPositionStays)
				{
					num |= 0x20;
				}
				if (DestroyWithScene)
				{
					num |= 0x40;
				}
				if (DontDestroyWithOwner)
				{
					num |= 0x80;
				}
				if (HasOwnershipFlags)
				{
					num |= 0x100;
				}
				if (SyncObservers)
				{
					num |= 0x200;
				}
				if (SpawnWithObservers)
				{
					num |= 0x400;
				}
				if (HasInstantiationData)
				{
					num |= 0x800;
				}
				return num;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal void SetStateFromBitset(ushort bitset)
			{
				IsPlayerObject = (bitset & 1) != 0;
				HasParent = (bitset & 2) != 0;
				IsSceneObject = (bitset & 4) != 0;
				HasTransform = (bitset & 8) != 0;
				IsLatestParentSet = (bitset & 0x10) != 0;
				WorldPositionStays = (bitset & 0x20) != 0;
				DestroyWithScene = (bitset & 0x40) != 0;
				DontDestroyWithOwner = (bitset & 0x80) != 0;
				HasOwnershipFlags = (bitset & 0x100) != 0;
				SyncObservers = (bitset & 0x200) != 0;
				SpawnWithObservers = (bitset & 0x400) != 0;
				HasInstantiationData = (bitset & 0x800) != 0;
			}

			public void Serialize(global::Unity.Netcode.FastBufferWriter writer)
			{
				if (OwnerObject.NetworkManagerOwner.DistributedAuthorityMode)
				{
					HasOwnershipFlags = true;
					SpawnWithObservers = OwnerObject.SpawnWithObservers;
				}
				writer.WriteValueSafe<ushort>(GetBitsetRepresentation(), default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				writer.WriteValueSafe(in Hash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, NetworkObjectId);
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, OwnerClientId);
				if (HasParent)
				{
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, ParentObjectId);
					if (IsLatestParentSet)
					{
						global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, LatestParent.Value);
					}
				}
				if (HasOwnershipFlags)
				{
					writer.WriteValueSafe(in OwnershipFlags, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				if (SyncObservers)
				{
					global::Unity.Netcode.BytePacker.WriteValuePacked(writer, Observers.Length);
					ulong[] observers = Observers;
					foreach (ulong value in observers)
					{
						global::Unity.Netcode.BytePacker.WriteValuePacked(writer, value);
					}
				}
				int num = 0;
				num += (HasTransform ? global::Unity.Netcode.FastBufferWriter.GetWriteSize<global::Unity.Netcode.NetworkObject.SerializedObject.TransformData>() : 0);
				num += global::Unity.Netcode.FastBufferWriter.GetWriteSize<int>();
				if (!writer.TryBeginWrite(num))
				{
					throw new global::System.OverflowException("Could not serialize SceneObject: Out of buffer space.");
				}
				if (HasTransform)
				{
					writer.WriteValue(in Transform, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
				}
				if (OwnerObject.NetworkManagerOwner.DistributedAuthorityMode)
				{
					writer.WriteValue(in OwnerObject.NetworkSceneHandle, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				}
				else
				{
					writer.WriteValue<global::Unity.Netcode.NetworkSceneHandle>(OwnerObject.GetSceneOriginHandle(), default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				}
				int position = writer.Position;
				writer.WriteValueSafe<int>(0, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				int position2 = writer.Position;
				if (HasInstantiationData)
				{
					writer.WriteValueSafe(OwnerObject.InstantiationData, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter> serializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter>(new global::Unity.Netcode.BufferSerializerWriter(writer));
				OwnerObject.SynchronizeNetworkBehaviours(ref serializer, TargetClientId);
				int position3 = writer.Position;
				writer.Seek(position);
				writer.WriteValueSafe<int>(position3 - position2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				writer.Seek(position3);
			}

			public void Deserialize(global::Unity.Netcode.FastBufferReader reader)
			{
				reader.ReadValueSafe(out ushort value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				SetStateFromBitset(value);
				reader.ReadValueSafe(out Hash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out NetworkObjectId);
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out OwnerClientId);
				if (HasParent)
				{
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ParentObjectId);
					if (IsLatestParentSet)
					{
						global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ulong value2);
						LatestParent = value2;
					}
				}
				if (HasOwnershipFlags)
				{
					reader.ReadValueSafe(out OwnershipFlags, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				if (SyncObservers)
				{
					int value3 = 0;
					ulong value4 = 0uL;
					global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out value3);
					Observers = new ulong[value3];
					for (int i = 0; i < value3; i++)
					{
						global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out value4);
						Observers[i] = value4;
					}
				}
				int num = 0;
				num += (HasTransform ? global::Unity.Netcode.FastBufferWriter.GetWriteSize<global::Unity.Netcode.NetworkObject.SerializedObject.TransformData>() : 0);
				num += global::Unity.Netcode.FastBufferWriter.GetWriteSize<int>();
				if (!reader.TryBeginRead(num))
				{
					throw new global::System.OverflowException("Could not deserialize SceneObject: Reading past the end of the buffer");
				}
				if (HasTransform)
				{
					reader.ReadValue(out Transform, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
				}
				reader.ReadValue(out NetworkSceneHandle, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				reader.ReadValueSafe(out SynchronizationDataSize, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		internal uint GlobalObjectIdHash;

		internal uint PrefabGlobalObjectIdHash;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		internal uint InScenePlacedSourceGlobalObjectIdHash;

		internal byte[] InstantiationData;

		internal bool IsSpawnAuthority;

		[global::UnityEngine.HideInInspector]
		public int DeferredDespawnTick;

		public global::Unity.Netcode.NetworkObject.OnDeferedDespawnCompleteDelegateHandler OnDeferredDespawnComplete;

		[global::UnityEngine.SerializeField]
		internal global::Unity.Netcode.NetworkObject.OwnershipStatus Ownership = global::Unity.Netcode.NetworkObject.OwnershipStatus.Distributable;

		public global::Unity.Netcode.NetworkObject.OnOwnershipPermissionsFailureDelegateHandler OnOwnershipPermissionsFailure;

		public global::Unity.Netcode.NetworkObject.OnOwnershipRequestedDelegateHandler OnOwnershipRequested;

		public global::Unity.Netcode.NetworkObject.OnOwnershipRequestResponseDelegateHandler OnOwnershipRequestResponse;

		internal global::Unity.Netcode.NetworkManager NetworkManagerOwner;

		internal ulong PreviousOwnerId;

		[global::UnityEngine.Tooltip("If enabled (default disabled), instances of this NetworkObject will ignore any parent(s) it might have and replicate on clients as the root being its parent.")]
		public bool AlwaysReplicateAsRoot;

		[global::UnityEngine.Tooltip("If enabled (default enabled), newly joining clients will be synchronized with the transform of the associated GameObject this component is attached to. Typical use case scenario would be for managment objects or in-scene placed objects that don't move and already have their transform settings applied within the scene information.")]
		public bool SynchronizeTransform = true;

		internal bool DestroyPendingSceneEvent;

		[global::UnityEngine.Tooltip("When enabled (default disabled), spawned instances of this NetworkObject will automatically migrate to any newly assigned active scene.")]
		public bool ActiveSceneSynchronization;

		[global::UnityEngine.Tooltip("When enabled (default enabled), dynamically spawned instances of this NetworkObject's migration to a different scene will automatically be synchonize amongst clients.")]
		public bool SceneMigrationSynchronization = true;

		public global::System.Action OnMigratedToNewScene;

		[global::UnityEngine.Tooltip("When disabled (default enabled), the NetworkObject will spawn with no observers. You control object visibility using NetworkShow. This applies to newly joining clients as well.")]
		public bool SpawnWithObservers = true;

		public global::Unity.Netcode.NetworkObject.VisibilityDelegate CheckObjectVisibility;

		public global::Unity.Netcode.NetworkObject.SpawnDelegate IncludeTransformWhenSpawning;

		[global::UnityEngine.Tooltip("When enabled (default disabled), instances of this NetworkObject will not be destroyed if the owning client disconnects.")]
		public bool DontDestroyWithOwner;

		[global::UnityEngine.Tooltip("When disabled (default enabled), NetworkObject parenting will not be automatically synchronized. This is typically used when you want to implement your own custom parenting solution.")]
		public bool AutoObjectParentSync = true;

		[global::UnityEngine.Tooltip("When disabled (default enabled), the owner will not apply a server or host's transform properties when parenting changes. Primarily useful for client-server network topology configurations.")]
		public bool SyncOwnerTransformWhenParented = true;

		[global::UnityEngine.Tooltip("When enabled (default disabled), owner's can parent a NetworkObject locally without having to send an RPC to the server or host. Only pertinent when using client-server network topology configurations.")]
		public bool AllowOwnerToParent;

		internal readonly global::System.Collections.Generic.HashSet<ulong> Observers = new global::System.Collections.Generic.HashSet<ulong>();

		private string m_CachedNameForMetrics;

		private readonly global::System.Collections.Generic.HashSet<ulong> m_EmptyULongHashSet = new global::System.Collections.Generic.HashSet<ulong>();

		internal global::Unity.Netcode.NetworkSceneHandle SceneOriginHandle;

		internal global::Unity.Netcode.NetworkSceneHandle NetworkSceneHandle;

		private global::UnityEngine.SceneManagement.Scene m_SceneOrigin;

		private ulong? m_LatestParent;

		private global::UnityEngine.Transform m_CachedParent;

		private bool m_CachedWorldPositionStays = true;

		internal bool AuthorityAppliedParenting;

		internal static global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject> OrphanChildren = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject>();

		private global::System.Collections.Generic.List<global::Unity.Netcode.NetworkBehaviour> m_ChildNetworkBehaviours;

		[global::UnityEngine.HideInInspector]
		public uint PrefabIdHash => GlobalObjectIdHash;

		public global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkTransform> NetworkTransforms { get; private set; }

		public global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkRigidbodyBase> NetworkRigidbodies { get; private set; }

		public global::Unity.Netcode.NetworkObject CurrentParent { get; private set; }

		public global::Unity.Netcode.NetworkManager NetworkManager
		{
			get
			{
				if (!NetworkManagerOwner)
				{
					return global::Unity.Netcode.NetworkManager.Singleton;
				}
				return NetworkManagerOwner;
			}
		}

		internal bool HasRemoteObservers
		{
			get
			{
				if (Observers.Count != 0)
				{
					if (Observers.Contains(NetworkManagerOwner.LocalClientId))
					{
						return Observers.Count != 1;
					}
					return true;
				}
				return false;
			}
		}

		public bool IsOwnershipDistributable => Ownership.HasFlag(global::Unity.Netcode.NetworkObject.OwnershipStatus.Distributable);

		public bool IsOwnershipSessionOwner => Ownership.HasFlag(global::Unity.Netcode.NetworkObject.OwnershipStatus.SessionOwner);

		public bool IsOwnershipLocked => ((global::Unity.Netcode.NetworkObject.OwnershipStatusExtended)Ownership).HasFlag(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Locked);

		public bool IsOwnershipTransferable => Ownership.HasFlag(global::Unity.Netcode.NetworkObject.OwnershipStatus.Transferable);

		public bool IsOwnershipRequestRequired => Ownership.HasFlag(global::Unity.Netcode.NetworkObject.OwnershipStatus.RequestRequired);

		public bool IsRequestInProgress => ((global::Unity.Netcode.NetworkObject.OwnershipStatusExtended)Ownership).HasFlag(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Requested);

		public bool HasAuthority => InternalHasAuthority();

		public ulong NetworkObjectId { get; internal set; }

		public ulong OwnerClientId { get; internal set; }

		public bool IsPlayerObject { get; internal set; }

		public bool IsLocalPlayer
		{
			get
			{
				if (IsSpawned && IsPlayerObject)
				{
					return OwnerClientId == NetworkManagerOwner.LocalClientId;
				}
				return false;
			}
		}

		public bool IsOwner
		{
			get
			{
				if (IsSpawned)
				{
					return OwnerClientId == NetworkManagerOwner.LocalClientId;
				}
				return false;
			}
		}

		public bool IsOwnedByServer
		{
			get
			{
				if (IsSpawned)
				{
					return OwnerClientId == 0;
				}
				return false;
			}
		}

		public bool IsSpawned { get; internal set; }

		public bool? IsSceneObject { get; internal set; }

		public bool DestroyWithScene { get; set; }

		internal global::UnityEngine.SceneManagement.Scene SceneOrigin
		{
			get
			{
				return m_SceneOrigin;
			}
			set
			{
				if (SceneOriginHandle.IsEmpty() && value.IsValid() && value.isLoaded)
				{
					m_SceneOrigin = value;
					SceneOriginHandle = value.handle;
				}
			}
		}

		internal global::System.Collections.Generic.List<global::Unity.Netcode.NetworkBehaviour> ChildNetworkBehaviours
		{
			get
			{
				if (m_ChildNetworkBehaviours != null)
				{
					return m_ChildNetworkBehaviours;
				}
				m_ChildNetworkBehaviours = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkBehaviour>();
				global::Unity.Netcode.NetworkBehaviour[] componentsInChildren = GetComponentsInChildren<global::Unity.Netcode.NetworkBehaviour>(includeInactive: true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (componentsInChildren[i].GetComponentInParent<global::Unity.Netcode.NetworkObject>() != this)
					{
						continue;
					}
					ushort behaviourId = (ushort)m_ChildNetworkBehaviours.Count;
					componentsInChildren[i].SetNetworkObject(this, behaviourId);
					m_ChildNetworkBehaviours.Add(componentsInChildren[i]);
					global::System.Type type = componentsInChildren[i].GetType();
					if (type == typeof(global::Unity.Netcode.Components.NetworkTransform) || type.IsInstanceOfType(typeof(global::Unity.Netcode.Components.NetworkTransform)) || type.IsSubclassOf(typeof(global::Unity.Netcode.Components.NetworkTransform)))
					{
						if (NetworkTransforms == null)
						{
							NetworkTransforms = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkTransform>();
						}
						global::Unity.Netcode.Components.NetworkTransform networkTransform = componentsInChildren[i] as global::Unity.Netcode.Components.NetworkTransform;
						networkTransform.IsNested = i != 0 && networkTransform.gameObject != base.gameObject;
						NetworkTransforms.Add(networkTransform);
					}
					else if (type.IsSubclassOf(typeof(global::Unity.Netcode.Components.NetworkRigidbodyBase)))
					{
						if (NetworkRigidbodies == null)
						{
							NetworkRigidbodies = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkRigidbodyBase>();
						}
						NetworkRigidbodies.Add(componentsInChildren[i] as global::Unity.Netcode.Components.NetworkRigidbodyBase);
					}
				}
				return m_ChildNetworkBehaviours;
			}
		}

		public void DeferDespawn(int tickOffset, bool destroy = true)
		{
			if (!NetworkManager.DistributedAuthorityMode)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[" + base.name + "] This method is only available in distributed authority mode.");
				}
				return;
			}
			if (!IsSpawned)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[" + base.name + "] Cannot defer despawn while not spawned.");
				}
				return;
			}
			if (!HasAuthority)
			{
				if (NetworkManagerOwner.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer(string.Format("[{0}] Only the authority can invoke {1} and local Client-{2} is not the authority of {3}!", base.name, "DeferDespawn", NetworkManagerOwner.LocalClientId, base.name));
				}
				return;
			}
			DeferredDespawnTick = NetworkManagerOwner.ServerTime.Tick + tickOffset;
			global::Unity.Netcode.NetworkConnectionManager connectionManager = NetworkManagerOwner.ConnectionManager;
			for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
			{
				ChildNetworkBehaviours[i].PreVariableUpdate();
				ChildNetworkBehaviours[i].OnDeferringDespawn(DeferredDespawnTick);
			}
			if (NetworkManagerOwner.DAHost)
			{
				for (int j = 0; j < connectionManager.ConnectedClientsList.Count; j++)
				{
					global::Unity.Netcode.NetworkClient networkClient = connectionManager.ConnectedClientsList[j];
					if (IsNetworkVisibleTo(networkClient.ClientId))
					{
						for (int k = 0; k < ChildNetworkBehaviours.Count; k++)
						{
							ChildNetworkBehaviours[k].NetworkVariableUpdate(networkClient.ClientId);
						}
					}
				}
			}
			else
			{
				for (int l = 0; l < ChildNetworkBehaviours.Count; l++)
				{
					ChildNetworkBehaviours[l].NetworkVariableUpdate(0uL);
				}
			}
			Despawn(destroy);
		}

		internal bool HasExtendedOwnershipStatus(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended extended)
		{
			global::Unity.Netcode.NetworkObject.OwnershipStatusExtended ownership = (global::Unity.Netcode.NetworkObject.OwnershipStatusExtended)Ownership;
			return ownership.HasFlag(extended);
		}

		internal void AddOwnershipExtended(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended extended)
		{
			global::Unity.Netcode.NetworkObject.OwnershipStatusExtended ownership = (global::Unity.Netcode.NetworkObject.OwnershipStatusExtended)Ownership;
			ownership |= extended;
			Ownership = (global::Unity.Netcode.NetworkObject.OwnershipStatus)ownership;
		}

		internal void RemoveOwnershipExtended(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended extended)
		{
			global::Unity.Netcode.NetworkObject.OwnershipStatusExtended ownership = (global::Unity.Netcode.NetworkObject.OwnershipStatusExtended)Ownership;
			ownership &= ~extended;
			Ownership = (global::Unity.Netcode.NetworkObject.OwnershipStatus)ownership;
		}

		public bool SetOwnershipLock(bool lockOwnership = true)
		{
			if (!NetworkManager.DistributedAuthorityMode)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[" + base.name + "][Feature Not Allowed In Client-Server Mode] Ownership flags are a distributed authority feature only!");
				}
				return false;
			}
			if (!IsSpawned)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[" + base.name + "][Attempted Lock While not spawned]");
				}
				return false;
			}
			if (!HasAuthority)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogWarningServer($"[{base.name}][Attempted Lock Without Authority] Client-{NetworkManagerOwner.LocalClientId} is trying to lock ownership but does not have authority!");
				}
				return false;
			}
			if ((!IsOwnershipTransferable && !IsPlayerObject) || IsOwnershipSessionOwner)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("[" + base.name + "] Trying to add or remove ownership lock on [" + base.name + "] which does not have the Transferable flag set!");
				}
				return false;
			}
			if (!(IsOwnershipLocked ^ lockOwnership))
			{
				return true;
			}
			if (lockOwnership)
			{
				AddOwnershipExtended(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Locked);
			}
			else
			{
				RemoveOwnershipExtended(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Locked);
			}
			if (IsSpawned)
			{
				SendOwnershipStatusUpdate();
			}
			return true;
		}

		public global::Unity.Netcode.NetworkObject.OwnershipRequestStatus RequestOwnership()
		{
			if (!IsSpawned)
			{
				if (NetworkManagerOwner.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[" + base.name + "][Invalid Operation] Cannot request ownership of an NetworkObject before it is spawned.");
				}
				return global::Unity.Netcode.NetworkObject.OwnershipRequestStatus.InvalidOperation;
			}
			if (OwnerClientId == NetworkManagerOwner.LocalClientId)
			{
				return global::Unity.Netcode.NetworkObject.OwnershipRequestStatus.AlreadyOwner;
			}
			if (!IsOwnershipRequestRequired)
			{
				return global::Unity.Netcode.NetworkObject.OwnershipRequestStatus.RequestRequiredNotSet;
			}
			if (IsOwnershipLocked)
			{
				return global::Unity.Netcode.NetworkObject.OwnershipRequestStatus.Locked;
			}
			if (IsRequestInProgress)
			{
				return global::Unity.Netcode.NetworkObject.OwnershipRequestStatus.RequestInProgress;
			}
			if (IsOwnershipSessionOwner)
			{
				return global::Unity.Netcode.NetworkObject.OwnershipRequestStatus.SessionOwnerOnly;
			}
			global::Unity.Netcode.ChangeOwnershipMessage message = new global::Unity.Netcode.ChangeOwnershipMessage
			{
				ChangeMessageType = global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestOwnership,
				NetworkObjectId = NetworkObjectId,
				OwnerClientId = OwnerClientId,
				ClientIdCount = 1,
				RequestClientId = NetworkManagerOwner.LocalClientId,
				ClientIds = new ulong[1] { OwnerClientId },
				DistributedAuthorityMode = true,
				OwnershipFlags = (ushort)Ownership
			};
			ulong clientId = (NetworkManagerOwner.DAHost ? OwnerClientId : 0);
			NetworkManagerOwner.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ChangeOwnershipMessage>.DefaultDelivery, clientId);
			return global::Unity.Netcode.NetworkObject.OwnershipRequestStatus.RequestSent;
		}

		internal void OwnershipRequest(ulong clientRequestingOwnership)
		{
			global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus ownershipRequestResponseStatus = global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus.Approved;
			if (IsOwnershipLocked)
			{
				ownershipRequestResponseStatus = global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus.Locked;
			}
			else if (IsRequestInProgress)
			{
				ownershipRequestResponseStatus = global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus.RequestInProgress;
			}
			else if ((!IsOwnershipRequestRequired && !IsOwnershipTransferable) || IsOwnershipSessionOwner)
			{
				ownershipRequestResponseStatus = global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus.CannotRequest;
			}
			if (OnOwnershipRequested != null && !OnOwnershipRequested(clientRequestingOwnership))
			{
				ownershipRequestResponseStatus = global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus.Denied;
			}
			if (ownershipRequestResponseStatus == global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus.Approved)
			{
				AddOwnershipExtended(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Requested);
				NetworkManagerOwner.SpawnManager.ChangeOwnership(this, clientRequestingOwnership, HasAuthority, isRequestApproval: true);
				return;
			}
			global::Unity.Netcode.ChangeOwnershipMessage message = new global::Unity.Netcode.ChangeOwnershipMessage
			{
				ChangeMessageType = global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestDenied,
				NetworkObjectId = NetworkObjectId,
				OwnerClientId = NetworkManagerOwner.LocalClientId,
				RequestClientId = clientRequestingOwnership,
				DistributedAuthorityMode = true,
				OwnershipRequestResponseStatus = (byte)ownershipRequestResponseStatus,
				OwnershipFlags = (ushort)Ownership
			};
			ulong clientId = (NetworkManagerOwner.DAHost ? clientRequestingOwnership : 0);
			NetworkManagerOwner.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ChangeOwnershipMessage>.DefaultDelivery, clientId);
		}

		internal void OwnershipRequestResponse(global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus ownershipRequestResponse)
		{
			OnOwnershipRequestResponse?.Invoke(ownershipRequestResponse);
		}

		public bool SetOwnershipStatus(global::Unity.Netcode.NetworkObject.OwnershipStatus status, bool clearAndSet = false, global::Unity.Netcode.NetworkObject.OwnershipLockActions lockAction = global::Unity.Netcode.NetworkObject.OwnershipLockActions.None)
		{
			if (status.HasFlag(global::Unity.Netcode.NetworkObject.OwnershipStatus.SessionOwner) && !NetworkManagerOwner.LocalClient.IsSessionOwner)
			{
				global::Unity.Netcode.NetworkLog.LogWarning("[" + base.name + "] Only the session owner is allowed to set the ownership status to session owner only.");
				return false;
			}
			if (!clearAndSet && Ownership.HasFlag(status))
			{
				return false;
			}
			if (clearAndSet || status == global::Unity.Netcode.NetworkObject.OwnershipStatus.None)
			{
				Ownership = global::Unity.Netcode.NetworkObject.OwnershipStatus.None;
			}
			if (status.HasFlag(global::Unity.Netcode.NetworkObject.OwnershipStatus.SessionOwner))
			{
				Ownership = global::Unity.Netcode.NetworkObject.OwnershipStatus.SessionOwner;
			}
			else
			{
				if (Ownership.HasFlag(global::Unity.Netcode.NetworkObject.OwnershipStatus.SessionOwner))
				{
					global::Unity.Netcode.NetworkLog.LogWarning("[" + base.name + "] No other ownership statuses may be set while SessionOwner is set.");
					return false;
				}
				Ownership |= status;
				if (lockAction != global::Unity.Netcode.NetworkObject.OwnershipLockActions.None)
				{
					SetOwnershipLock(lockAction == global::Unity.Netcode.NetworkObject.OwnershipLockActions.SetAndLock);
				}
			}
			SendOwnershipStatusUpdate();
			return true;
		}

		public bool RemoveOwnershipStatus(global::Unity.Netcode.NetworkObject.OwnershipStatus status)
		{
			if (!Ownership.HasFlag(status) || status == global::Unity.Netcode.NetworkObject.OwnershipStatus.None)
			{
				return false;
			}
			Ownership &= ~status;
			SendOwnershipStatusUpdate();
			return true;
		}

		internal void SendOwnershipStatusUpdate()
		{
			if (!HasRemoteObservers)
			{
				return;
			}
			global::Unity.Netcode.ChangeOwnershipMessage message = new global::Unity.Netcode.ChangeOwnershipMessage
			{
				ChangeMessageType = global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipFlagsUpdate,
				NetworkObjectId = NetworkObjectId,
				OwnerClientId = OwnerClientId,
				DistributedAuthorityMode = true,
				OwnershipFlags = (ushort)Ownership
			};
			if (NetworkManagerOwner.DAHost)
			{
				foreach (ulong observer in Observers)
				{
					if (observer != NetworkManagerOwner.LocalClientId)
					{
						NetworkManagerOwner.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ChangeOwnershipMessage>.DefaultDelivery, observer);
					}
				}
				return;
			}
			message.ClientIdCount = Observers.Count;
			message.ClientIds = global::System.Linq.Enumerable.ToArray(Observers);
			NetworkManagerOwner.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ChangeOwnershipMessage>.DefaultDelivery, 0uL);
		}

		public bool HasOwnershipStatus(global::Unity.Netcode.NetworkObject.OwnershipStatus status)
		{
			return Ownership.HasFlag(status);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private bool InternalHasAuthority()
		{
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			if (!networkManager.DistributedAuthorityMode)
			{
				return networkManager.IsServer;
			}
			return OwnerClientId == networkManager.LocalClientId;
		}

		public void SetSceneObjectStatus(bool isSceneObject = false)
		{
			IsSceneObject = isSceneObject;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void AddObserver(ulong clientId)
		{
			Observers.Add(clientId);
		}

		internal string GetNameForMetrics()
		{
			return m_CachedNameForMetrics ?? (m_CachedNameForMetrics = base.name);
		}

		public global::System.Collections.Generic.HashSet<ulong>.Enumerator GetObservers()
		{
			if (!IsSpawned)
			{
				return m_EmptyULongHashSet.GetEnumerator();
			}
			return Observers.GetEnumerator();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool IsNetworkVisibleTo(ulong clientId)
		{
			if (!IsSpawned)
			{
				return false;
			}
			return Observers.Contains(clientId);
		}

		internal global::Unity.Netcode.NetworkSceneHandle GetSceneOriginHandle()
		{
			if (SceneOriginHandle.IsEmpty() && IsSpawned && IsSceneObject != false)
			{
				throw new global::System.Exception("GetSceneOriginHandle called when SceneOriginHandle is still zero but the NetworkObject is already spawned!");
			}
			if (SceneOriginHandle.IsEmpty())
			{
				return base.gameObject.scene.handle;
			}
			return SceneOriginHandle;
		}

		public void NetworkShow(ulong clientId)
		{
			if (!IsSpawned)
			{
				throw new global::Unity.Netcode.SpawnStateException("Object is not spawned");
			}
			if (!HasAuthority)
			{
				if (NetworkManagerOwner.DistributedAuthorityMode)
				{
					throw new global::Unity.Netcode.NotServerException("Only the owner-authority can change visibility when distributed authority mode is enabled!");
				}
				throw new global::Unity.Netcode.NotServerException("Only the authority can change visibility");
			}
			if (Observers.Contains(clientId))
			{
				if (!NetworkManagerOwner.DistributedAuthorityMode)
				{
					throw new global::Unity.Netcode.NotServerException("Only server can change visibility");
				}
				global::UnityEngine.Debug.LogError($"The object {base.name} is already visible to Client-{clientId}!");
			}
			else if (CheckObjectVisibility != null && !CheckObjectVisibility(clientId))
			{
				if (NetworkManagerOwner.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogWarning(string.Format("[NetworkShow] Trying to make {0} {1} visible to client ({2}) but {3} returned false!", "NetworkObject", base.name, clientId, "CheckObjectVisibility"));
				}
			}
			else
			{
				NetworkManagerOwner.SpawnManager.MarkObjectForShowingTo(this, clientId);
				AddObserver(clientId);
			}
		}

		public static void NetworkShow(global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> networkObjects, ulong clientId)
		{
			if (networkObjects == null || networkObjects.Count == 0)
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer("At least one NetworkObject has to be provided when showing a list of NetworkObjects!");
				return;
			}
			foreach (global::Unity.Netcode.NetworkObject networkObject in networkObjects)
			{
				networkObject.NetworkShow(clientId);
			}
		}

		public void NetworkHide(ulong clientId)
		{
			if (!IsSpawned)
			{
				throw new global::Unity.Netcode.SpawnStateException("Object is not spawned");
			}
			if (!HasAuthority && !NetworkManagerOwner.DAHost)
			{
				if (NetworkManagerOwner.DistributedAuthorityMode)
				{
					throw new global::Unity.Netcode.NotServerException("Only the owner-authority can change visibility when distributed authority mode is enabled!");
				}
				throw new global::Unity.Netcode.NotServerException("Only the authority can change visibility");
			}
			if (NetworkManagerOwner.SpawnManager.RemoveObjectFromShowingTo(this, clientId))
			{
				return;
			}
			if (!Observers.Contains(clientId) && NetworkManagerOwner.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::UnityEngine.Debug.LogWarning($"{base.name} is already hidden from Client-{clientId}! (ignoring)");
				return;
			}
			Observers.Remove(clientId);
			global::Unity.Netcode.DestroyObjectMessage message = new global::Unity.Netcode.DestroyObjectMessage
			{
				NetworkObjectId = NetworkObjectId,
				DestroyGameObject = !IsSceneObject.Value,
				IsDistributedAuthority = NetworkManagerOwner.DistributedAuthorityMode,
				IsTargetedDestroy = NetworkManagerOwner.DistributedAuthorityMode,
				TargetClientId = clientId,
				DeferredDespawnTick = DeferredDespawnTick
			};
			int num = 0;
			if (NetworkManagerOwner.DistributedAuthorityMode)
			{
				if (!NetworkManagerOwner.DAHost)
				{
					num = NetworkManagerOwner.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.DestroyObjectMessage>.DefaultDelivery, 0uL);
				}
				else
				{
					num = NetworkManagerOwner.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.DestroyObjectMessage>.DefaultDelivery, clientId);
					foreach (ulong connectedClientId in NetworkManagerOwner.ConnectionManager.ConnectedClientIds)
					{
						if (connectedClientId != clientId && connectedClientId != NetworkManagerOwner.LocalClientId)
						{
							num += NetworkManagerOwner.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.DestroyObjectMessage>.DefaultDelivery, connectedClientId);
						}
					}
				}
			}
			else
			{
				num = NetworkManagerOwner.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.DestroyObjectMessage>.DefaultDelivery, clientId);
			}
			NetworkManagerOwner.NetworkMetrics.TrackObjectDestroySent(clientId, this, num);
		}

		public static void NetworkHide(global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> networkObjects, ulong clientId)
		{
			if (networkObjects == null || networkObjects.Count == 0)
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer("At least one NetworkObject has to be provided when hiding a list of NetworkObjects!");
				return;
			}
			foreach (global::Unity.Netcode.NetworkObject networkObject in networkObjects)
			{
				networkObject.NetworkHide(clientId);
			}
		}

		private void OnDestroy()
		{
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			if (!networkManager)
			{
				return;
			}
			networkManager.SpawnManager?.RemoveNetworkObjectFromSceneChangedUpdates(this);
			if (IsSpawned && !networkManager.ShutdownInProgress)
			{
				bool num = HasAuthority || NetworkManager.DAHost || DestroyPendingSceneEvent;
				bool flag = base.gameObject != null && base.gameObject.scene.IsValid() && base.gameObject.scene.isLoaded;
				if (!num && IsSceneObject == false && flag)
				{
					if (networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
					{
						if (networkManager.DistributedAuthorityMode)
						{
							global::Unity.Netcode.NetworkLog.LogError(string.Format("[Invalid Destroy][{0}][NetworkObjectId:{1}] Destroy a spawned {2} on a non-owner client is not valid during a distributed authority session. Call {3} or {4} on the client-owner instead.", base.name, NetworkObjectId, "NetworkObject", "Destroy", "Despawn"));
						}
						else
						{
							global::Unity.Netcode.NetworkLog.LogErrorServer(string.Format("[Invalid Destroy][{0}][NetworkObjectId:{1}] Destroy a spawned {2} on a non-host client is not valid. Call {3} or {4} on the server/host instead.", base.name, NetworkObjectId, "NetworkObject", "Destroy", "Despawn"));
						}
					}
					return;
				}
			}
			if (networkManager.SpawnManager != null && networkManager.SpawnManager.SpawnedObjects.TryGetValue(NetworkObjectId, out var value) && this == value)
			{
				networkManager.SpawnManager.OnDespawnObject(value, destroyGameObject: false);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void SpawnInternal(bool destroyWithScene, ulong ownerClientId, bool playerObject)
		{
			if (NetworkManagerOwner == null)
			{
				NetworkManagerOwner = global::Unity.Netcode.NetworkManager.Singleton;
			}
			if (!NetworkManagerOwner.IsListening)
			{
				throw new global::Unity.Netcode.NotListeningException("NetworkManagerOwner is not listening, start a server or host before spawning objects");
			}
			if ((!NetworkManagerOwner.IsServer && !NetworkManagerOwner.DistributedAuthorityMode) || (NetworkManagerOwner.DistributedAuthorityMode && !NetworkManagerOwner.LocalClient.IsSessionOwner && NetworkManagerOwner.LocalClientId != ownerClientId))
			{
				if (NetworkManagerOwner.DistributedAuthorityMode)
				{
					throw new global::Unity.Netcode.NotServerException($"When distributed authority mode is enabled, you can only spawn NetworkObjects that belong to the local instance! Local instance id {NetworkManagerOwner.LocalClientId} is not the same as the assigned owner id: {ownerClientId}!");
				}
				throw new global::Unity.Netcode.NotServerException("Only server can spawn NetworkObjects");
			}
			if (NetworkManagerOwner.DistributedAuthorityMode)
			{
				if (NetworkManagerOwner.LocalClient == null || !NetworkManagerOwner.IsConnectedClient || !NetworkManagerOwner.ConnectionManager.LocalClient.IsApproved)
				{
					global::UnityEngine.Debug.LogError("Cannot spawn " + base.name + " until the client is fully connected to the session!");
					return;
				}
				if (NetworkManagerOwner.NetworkConfig.EnableSceneManagement)
				{
					if (!NetworkManagerOwner.SceneManager.ClientSceneHandleToServerSceneHandle.ContainsKey(base.gameObject.scene.handle))
					{
						if (NetworkManagerOwner.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
						{
							global::Unity.Netcode.NetworkLog.LogWarning($"[{base.name}] Failed to find scene handle {base.gameObject.scene.handle} for {base.gameObject.name}!");
						}
						NetworkSceneHandle = base.gameObject.scene.handle;
					}
					else
					{
						NetworkSceneHandle = NetworkManagerOwner.SceneManager.ClientSceneHandleToServerSceneHandle[base.gameObject.scene.handle];
					}
				}
				if (DontDestroyWithOwner && !IsOwnershipDistributable && NetworkManagerOwner.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("Don't destroy with owner is set but DistributeOwnership is not set. If the owner leaves, the ownership of " + base.name + " will be set to the SessionOwner.");
				}
			}
			NetworkManagerOwner.SpawnManager.AuthorityLocalSpawn(this, NetworkManagerOwner.SpawnManager.GetNetworkObjectId(), IsSceneObject.HasValue && IsSceneObject.Value, playerObject, ownerClientId, destroyWithScene);
			if ((NetworkManagerOwner.DistributedAuthorityMode && NetworkManagerOwner.DAHost) || (!NetworkManagerOwner.DistributedAuthorityMode && NetworkManagerOwner.IsServer))
			{
				for (int i = 0; i < NetworkManagerOwner.ConnectedClientsList.Count; i++)
				{
					if (NetworkManagerOwner.ConnectedClientsList[i].ClientId != 0L && Observers.Contains(NetworkManagerOwner.ConnectedClientsList[i].ClientId))
					{
						NetworkManagerOwner.SpawnManager.SendSpawnCallForObject(NetworkManagerOwner.ConnectedClientsList[i].ClientId, this);
					}
				}
			}
			else if (NetworkManagerOwner.DistributedAuthorityMode && !NetworkManagerOwner.DAHost)
			{
				if (SpawnWithObservers || (!SpawnWithObservers && Observers.Count > 1))
				{
					NetworkManagerOwner.SpawnManager.SendSpawnCallForObject(0uL, this);
				}
			}
			else
			{
				global::Unity.Netcode.NetworkLog.LogWarningServer("[" + base.name + "] Ran into unknown conditional check during spawn when determining distributed authority mode or not");
			}
		}

		public static global::Unity.Netcode.NetworkObject InstantiateAndSpawn(global::UnityEngine.GameObject networkPrefab, global::Unity.Netcode.NetworkManager networkManager, ulong ownerClientId = 0uL, bool destroyWithScene = false, bool isPlayerObject = false, bool forceOverride = false, global::UnityEngine.Vector3 position = default(global::UnityEngine.Vector3), global::UnityEngine.Quaternion rotation = default(global::UnityEngine.Quaternion))
		{
			global::Unity.Netcode.NetworkObject component = networkPrefab.GetComponent<global::Unity.Netcode.NetworkObject>();
			if (component == null)
			{
				global::UnityEngine.Debug.LogError("The NetworkPrefab " + networkPrefab.name + " does not have a NetworkObject component!");
				return null;
			}
			return component.InstantiateAndSpawn(networkManager, ownerClientId, destroyWithScene, isPlayerObject, forceOverride, position, rotation);
		}

		public global::Unity.Netcode.NetworkObject InstantiateAndSpawn(global::Unity.Netcode.NetworkManager networkManager, ulong ownerClientId = 0uL, bool destroyWithScene = false, bool isPlayerObject = false, bool forceOverride = false, global::UnityEngine.Vector3 position = default(global::UnityEngine.Vector3), global::UnityEngine.Quaternion rotation = default(global::UnityEngine.Quaternion))
		{
			if (networkManager == null)
			{
				global::UnityEngine.Debug.LogError(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrors[global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NetworkManagerNull]);
				return null;
			}
			if (!networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrors[global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NoActiveSession]);
				return null;
			}
			ownerClientId = (networkManager.DistributedAuthorityMode ? networkManager.LocalClientId : ownerClientId);
			if (!networkManager.IsServer && !networkManager.DistributedAuthorityMode)
			{
				global::UnityEngine.Debug.LogError(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrors[global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NotAuthority]);
				return null;
			}
			if (networkManager.ShutdownInProgress)
			{
				global::UnityEngine.Debug.LogWarning(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrors[global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.InvokedWhenShuttingDown]);
				return null;
			}
			if (!networkManager.NetworkConfig.Prefabs.Contains(base.gameObject))
			{
				global::UnityEngine.Debug.LogError(global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrors[global::Unity.Netcode.NetworkSpawnManager.InstantiateAndSpawnErrorTypes.NotRegisteredNetworkPrefab]);
				return null;
			}
			return networkManager.SpawnManager.InstantiateAndSpawnNoParameterChecks(this, networkManager, ownerClientId, destroyWithScene, isPlayerObject, forceOverride, position, rotation);
		}

		public void Spawn(bool destroyWithScene = false)
		{
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			ulong ownerClientId = (networkManager.DistributedAuthorityMode ? networkManager.LocalClientId : 0);
			SpawnInternal(destroyWithScene, ownerClientId, playerObject: false);
		}

		public void SpawnWithOwnership(ulong clientId, bool destroyWithScene = false)
		{
			SpawnInternal(destroyWithScene, clientId, playerObject: false);
		}

		public void SpawnAsPlayerObject(ulong clientId, bool destroyWithScene = false)
		{
			SpawnInternal(destroyWithScene, clientId, playerObject: true);
		}

		public void Despawn(bool destroy = true)
		{
			if (!IsSpawned)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[" + base.name + "][Attempted despawn before NetworkObject was spawned]");
				}
				return;
			}
			foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour in ChildNetworkBehaviours)
			{
				childNetworkBehaviour.MarkVariablesDirty(dirty: false);
			}
			NetworkManagerOwner.SpawnManager.DespawnObject(this, destroy);
		}

		internal void ResetOnDespawn()
		{
			Observers.Clear();
			IsSpawnAuthority = false;
			IsSpawned = false;
			DeferredDespawnTick = 0;
			m_LatestParent = null;
			RemoveOwnershipExtended(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Requested | global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Locked);
		}

		public void RemoveOwnership()
		{
			if (!IsSpawned)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[" + base.name + "][Attempted ownership removal before NetworkObject was spawned]");
				}
			}
			else
			{
				NetworkManagerOwner.SpawnManager.RemoveOwnership(this);
			}
		}

		public void ChangeOwnership(ulong newOwnerClientId)
		{
			if (!IsSpawned)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[" + base.name + "][Attempted ownership change before NetworkObject was spawned]");
				}
			}
			else
			{
				NetworkManagerOwner.SpawnManager.ChangeOwnership(this, newOwnerClientId, HasAuthority);
			}
		}

		internal void InvokeBehaviourOnOwnershipChanged(ulong originalOwnerClientId, ulong newOwnerClientId)
		{
			if (!IsSpawned)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[" + base.name + "][Attempted behavior invoke on ownership changed before NetworkObject was spawned]");
				}
				return;
			}
			bool distributedAuthorityMode = NetworkManagerOwner.DistributedAuthorityMode;
			bool isServer = NetworkManagerOwner.IsServer;
			bool flag = originalOwnerClientId == NetworkManagerOwner.LocalClientId;
			bool flag2 = newOwnerClientId == NetworkManagerOwner.LocalClientId;
			if (distributedAuthorityMode || flag)
			{
				NetworkManagerOwner.SpawnManager.UpdateOwnershipTable(this, originalOwnerClientId, isRemoving: true);
			}
			foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour in ChildNetworkBehaviours)
			{
				childNetworkBehaviour.UpdateNetworkProperties();
				if (distributedAuthorityMode || isServer || flag)
				{
					childNetworkBehaviour.OnLostOwnership();
				}
			}
			NetworkManagerOwner.SpawnManager.UpdateOwnershipTable(this, newOwnerClientId);
			if (!(distributedAuthorityMode || isServer || flag2))
			{
				return;
			}
			foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour2 in ChildNetworkBehaviours)
			{
				if (!childNetworkBehaviour2.gameObject.activeInHierarchy)
				{
					global::UnityEngine.Debug.LogWarning("[" + base.name + "] " + childNetworkBehaviour2.gameObject.name + " is disabled! Netcode for GameObjects does not support disabled NetworkBehaviours! The " + childNetworkBehaviour2.GetType().Name + " component was skipped during ownership assignment!");
				}
				else
				{
					childNetworkBehaviour2.InternalOnGainedOwnership();
				}
			}
		}

		internal void InvokeOwnershipChanged(ulong previous, ulong next)
		{
			for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
			{
				if (ChildNetworkBehaviours[i].gameObject.activeInHierarchy)
				{
					ChildNetworkBehaviours[i].InternalOnOwnershipChanged(previous, next);
					continue;
				}
				global::UnityEngine.Debug.LogWarning("[" + base.name + "] " + ChildNetworkBehaviours[i].gameObject.name + " is disabled! Netcode for GameObjects does not support disabled NetworkBehaviours! The " + ChildNetworkBehaviours[i].GetType().Name + " component was skipped during ownership assignment!");
			}
		}

		internal void InvokeSessionOwnerPromoted(bool isSessionOwner)
		{
			if (!IsSpawned)
			{
				return;
			}
			foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour in ChildNetworkBehaviours)
			{
				childNetworkBehaviour.IsSessionOwner = isSessionOwner;
			}
		}

		internal void InvokeBehaviourOnNetworkObjectParentChanged(global::Unity.Netcode.NetworkObject parentNetworkObject)
		{
			for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
			{
				if (ChildNetworkBehaviours[i].IsSpawned || ChildNetworkBehaviours[i].gameObject.activeInHierarchy)
				{
					ChildNetworkBehaviours[i].InternalOnNetworkObjectParentChanged(parentNetworkObject);
					ChildNetworkBehaviours[i].OnNetworkObjectParentChanged(parentNetworkObject);
				}
			}
		}

		public bool WorldPositionStays()
		{
			return m_CachedWorldPositionStays;
		}

		internal void SetCachedParent(global::UnityEngine.Transform parentTransform)
		{
			AuthorityAppliedParenting = false;
			m_CachedParent = parentTransform;
		}

		internal global::UnityEngine.Transform GetCachedParent()
		{
			return m_CachedParent;
		}

		internal ulong? GetNetworkParenting()
		{
			return m_LatestParent;
		}

		internal void SetNetworkParenting(ulong? latestParent, bool worldPositionStays)
		{
			m_LatestParent = latestParent;
			m_CachedWorldPositionStays = worldPositionStays;
		}

		public bool TrySetParent(global::UnityEngine.Transform parent, bool worldPositionStays = true)
		{
			if (parent == null)
			{
				return TrySetParent((global::Unity.Netcode.NetworkObject)null, worldPositionStays);
			}
			global::Unity.Netcode.NetworkObject component = parent.GetComponent<global::Unity.Netcode.NetworkObject>();
			if (!(component == null))
			{
				return TrySetParent(component, worldPositionStays);
			}
			return false;
		}

		public bool TrySetParent(global::UnityEngine.GameObject parent, bool worldPositionStays = true)
		{
			if (parent == null)
			{
				return TrySetParent((global::Unity.Netcode.NetworkObject)null, worldPositionStays);
			}
			global::Unity.Netcode.NetworkObject component = parent.GetComponent<global::Unity.Netcode.NetworkObject>();
			if (!(component == null))
			{
				return TrySetParent(component, worldPositionStays);
			}
			return false;
		}

		internal bool TryRemoveParentCachedWorldPositionStays()
		{
			return InternalTrySetParent(null, m_CachedWorldPositionStays);
		}

		public bool TryRemoveParent(bool worldPositionStays = true)
		{
			return TrySetParent((global::Unity.Netcode.NetworkObject)null, worldPositionStays);
		}

		public bool TrySetParent(global::Unity.Netcode.NetworkObject parent, bool worldPositionStays = true)
		{
			if (!AutoObjectParentSync || !IsSpawned || !NetworkManagerOwner.IsListening)
			{
				return false;
			}
			if (!HasAuthority && (!AllowOwnerToParent || !IsOwner) && !NetworkManagerOwner.ShutdownInProgress)
			{
				return false;
			}
			return InternalTrySetParent(parent, worldPositionStays);
		}

		internal bool InternalTrySetParent(global::Unity.Netcode.NetworkObject parent, bool worldPositionStays = true)
		{
			if ((bool)parent && (IsSpawned ^ parent.IsSpawned) && !NetworkManager.ShutdownInProgress)
			{
				if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					string text = (IsSpawned ? (" the parent (" + parent.name + ")") : ("the child (" + base.name + ")"));
					global::Unity.Netcode.NetworkLog.LogWarning("[" + base.name + "] Parenting failed because " + text + " is not spawned!");
				}
				return false;
			}
			m_CachedWorldPositionStays = worldPositionStays;
			CurrentParent = parent;
			base.transform.SetParent(CurrentParent?.transform, worldPositionStays);
			return true;
		}

		private void OnTransformParentChanged()
		{
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			if (!AutoObjectParentSync || networkManager.ShutdownInProgress || base.transform.parent == m_CachedParent)
			{
				return;
			}
			if (networkManager == null || !networkManager.IsListening)
			{
				if (networkManager.DistributedAuthorityMode && m_CachedParent != null && base.transform.parent == null)
				{
					m_CachedParent = null;
					return;
				}
				base.transform.parent = m_CachedParent;
				global::UnityEngine.Debug.LogException(new global::Unity.Netcode.NotListeningException("[" + base.name + "] networkManager is not listening, start a server or host before re-parenting"));
				return;
			}
			if (!IsSpawned)
			{
				AuthorityAppliedParenting = false;
				if (base.transform.parent == null)
				{
					m_LatestParent = null;
					SetCachedParent(null);
					InvokeBehaviourOnNetworkObjectParentChanged(null);
				}
				else
				{
					base.transform.parent = m_CachedParent;
					global::UnityEngine.Debug.LogException(new global::Unity.Netcode.SpawnStateException("[" + base.name + "] NetworkObject can only be re-parented after being spawned"));
				}
				return;
			}
			if (!HasAuthority && !AuthorityAppliedParenting && (!AllowOwnerToParent || !IsOwner))
			{
				base.transform.parent = m_CachedParent;
				if (networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					if (networkManager.DistributedAuthorityMode)
					{
						global::Unity.Netcode.NetworkLog.LogError("[" + base.name + "][Not Owner] Only the owner-authority of child " + base.gameObject.name + "'s NetworkObject component can re-parent it!");
					}
					else
					{
						global::UnityEngine.Debug.LogException(new global::Unity.Netcode.NotServerException("[" + base.name + "] Only the server can re-parent NetworkObjects"));
					}
				}
				return;
			}
			bool removeParent = false;
			global::UnityEngine.Transform parent = base.transform.parent;
			if (parent != null)
			{
				if (!base.transform.parent.TryGetComponent<global::Unity.Netcode.NetworkObject>(out var component))
				{
					base.transform.parent = m_CachedParent;
					AuthorityAppliedParenting = false;
					global::UnityEngine.Debug.LogException(new global::Unity.Netcode.InvalidParentException("[" + base.name + "] Invalid parenting, NetworkObject moved under a non-NetworkObject parent"));
					return;
				}
				if (!component.IsSpawned)
				{
					base.transform.parent = m_CachedParent;
					AuthorityAppliedParenting = false;
					global::UnityEngine.Debug.LogException(new global::Unity.Netcode.SpawnStateException("[" + base.name + "] NetworkObject can only be re-parented under another spawned NetworkObject"));
					return;
				}
				m_LatestParent = component.NetworkObjectId;
			}
			else
			{
				m_LatestParent = null;
				removeParent = m_CachedParent != null;
			}
			bool authorityAppliedParenting = AuthorityAppliedParenting;
			ApplyNetworkParenting(removeParent);
			global::Unity.Netcode.ParentSyncMessage message = new global::Unity.Netcode.ParentSyncMessage
			{
				NetworkObjectId = NetworkObjectId,
				IsLatestParentSet = (m_LatestParent.HasValue && m_LatestParent.HasValue),
				LatestParent = m_LatestParent,
				RemoveParent = removeParent,
				AuthorityApplied = authorityAppliedParenting,
				WorldPositionStays = m_CachedWorldPositionStays,
				Position = (m_CachedWorldPositionStays ? base.transform.position : base.transform.localPosition),
				Rotation = (m_CachedWorldPositionStays ? base.transform.rotation : base.transform.localRotation),
				Scale = base.transform.localScale
			};
			if (parent == null)
			{
				m_CachedWorldPositionStays = true;
			}
			if (!networkManager.IsServer)
			{
				if (!networkManager.DistributedAuthorityMode || Observers.Count > 1)
				{
					networkManager.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ParentSyncMessage>.DefaultDelivery, 0uL);
				}
				return;
			}
			foreach (ulong connectedClientId in networkManager.ConnectionManager.ConnectedClientIds)
			{
				if (connectedClientId != 0L && Observers.Contains(connectedClientId))
				{
					networkManager.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ParentSyncMessage>.DefaultDelivery, connectedClientId);
				}
			}
		}

		internal bool ApplyNetworkParenting(bool removeParent = false, bool ignoreNotSpawned = false, bool orphanedChildPass = false, bool enableNotification = true)
		{
			if (!AutoObjectParentSync)
			{
				return false;
			}
			if (!IsSpawned && !ignoreNotSpawned)
			{
				return false;
			}
			bool flag = IsSceneObject.HasValue && IsSceneObject.Value;
			if (base.transform.parent != null && !removeParent && !m_LatestParent.HasValue && flag)
			{
				global::Unity.Netcode.NetworkObject component = base.transform.parent.GetComponent<global::Unity.Netcode.NetworkObject>();
				if (component == null)
				{
					m_CachedWorldPositionStays = false;
					return true;
				}
				if (!component.IsSpawned)
				{
					OrphanChildren.Add(this);
					return false;
				}
				SetNetworkParenting(component.NetworkObjectId, worldPositionStays: false);
				SetCachedParent(component.transform);
				return true;
			}
			if (removeParent || !m_LatestParent.HasValue)
			{
				SetCachedParent(null);
				base.transform.SetParent(null, m_CachedWorldPositionStays);
				if (enableNotification)
				{
					InvokeBehaviourOnNetworkObjectParentChanged(null);
				}
				return true;
			}
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			if (m_LatestParent.HasValue && !networkManager.SpawnManager.SpawnedObjects.ContainsKey(m_LatestParent.Value))
			{
				OrphanChildren.Add(this);
				return false;
			}
			global::Unity.Netcode.NetworkObject networkObject = networkManager.SpawnManager.SpawnedObjects[m_LatestParent.Value];
			if (orphanedChildPass && OrphanChildren.Contains(networkObject))
			{
				return false;
			}
			SetCachedParent(networkObject.transform);
			base.transform.SetParent(networkObject.transform, m_CachedWorldPositionStays);
			if (enableNotification)
			{
				InvokeBehaviourOnNetworkObjectParentChanged(networkObject);
			}
			return true;
		}

		internal static void CheckOrphanChildren()
		{
			global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> list = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>();
			foreach (global::Unity.Netcode.NetworkObject orphanChild in OrphanChildren)
			{
				if (orphanChild.ApplyNetworkParenting(removeParent: false, ignoreNotSpawned: false, orphanedChildPass: true))
				{
					list.Add(orphanChild);
				}
			}
			foreach (global::Unity.Netcode.NetworkObject item in list)
			{
				OrphanChildren.Remove(item);
			}
		}

		internal void InvokeBehaviourNetworkPreSpawn()
		{
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
			{
				ChildNetworkBehaviours[i].NetworkPreSpawn(ref networkManager, this);
			}
		}

		internal void InvokeBehaviourNetworkSpawn()
		{
			NetworkManagerOwner.SpawnManager.UpdateOwnershipTable(this, OwnerClientId);
			foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour in ChildNetworkBehaviours)
			{
				if (!childNetworkBehaviour.gameObject.activeInHierarchy)
				{
					global::UnityEngine.Debug.LogWarning(GenerateDisabledNetworkBehaviourWarning(childNetworkBehaviour) ?? "");
				}
				else
				{
					childNetworkBehaviour.InternalOnNetworkSpawn();
				}
			}
			foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour2 in ChildNetworkBehaviours)
			{
				if (!childNetworkBehaviour2.gameObject.activeInHierarchy)
				{
					global::UnityEngine.Debug.LogWarning(GenerateDisabledNetworkBehaviourWarning(childNetworkBehaviour2) ?? "");
				}
				else
				{
					childNetworkBehaviour2.NetworkSpawn();
				}
			}
		}

		internal void InvokeBehaviourNetworkPostSpawn()
		{
			for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
			{
				if (ChildNetworkBehaviours[i].gameObject.activeInHierarchy)
				{
					ChildNetworkBehaviours[i].NetworkPostSpawn();
				}
			}
		}

		internal void InternalNetworkSessionSynchronized()
		{
			for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
			{
				if (ChildNetworkBehaviours[i].gameObject.activeInHierarchy)
				{
					ChildNetworkBehaviours[i].NetworkSessionSynchronized();
				}
			}
		}

		internal void InternalInSceneNetworkObjectsSpawned()
		{
			for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
			{
				if (ChildNetworkBehaviours[i].gameObject.activeInHierarchy)
				{
					ChildNetworkBehaviours[i].InSceneNetworkObjectsSpawned();
				}
			}
		}

		internal void InvokeBehaviourNetworkDespawn()
		{
			for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
			{
				ChildNetworkBehaviours[i].InternalOnNetworkPreDespawn();
			}
			NetworkManagerOwner.SpawnManager.UpdateOwnershipTable(this, OwnerClientId, isRemoving: true);
			NetworkManagerOwner.SpawnManager.RemoveNetworkObjectFromSceneChangedUpdates(this);
			for (int j = 0; j < ChildNetworkBehaviours.Count; j++)
			{
				ChildNetworkBehaviours[j].InternalOnNetworkDespawn();
			}
		}

		internal string GenerateDisabledNetworkBehaviourWarning(global::Unity.Netcode.NetworkBehaviour networkBehaviour)
		{
			return string.Format("[{0}][{1}][{2}: {3}] Disabled {4}s will be excluded from spawning and synchronization!", base.name, networkBehaviour.GetType().Name, "isActiveAndEnabled", networkBehaviour.isActiveAndEnabled, "NetworkBehaviour");
		}

		internal void SynchronizeOwnerNetworkVariables(ulong originalOwnerId, ulong originalPreviousOwnerId)
		{
			ulong ownerClientId = OwnerClientId;
			OwnerClientId = originalOwnerId;
			PreviousOwnerId = originalPreviousOwnerId;
			for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
			{
				ChildNetworkBehaviours[i].MarkOwnerReadDirtyAndCheckOwnerWriteIsDirty();
			}
			OwnerClientId = ownerClientId;
			PreviousOwnerId = originalOwnerId;
			NetworkManagerOwner.BehaviourUpdater.NetworkBehaviourUpdate(forceSend: true);
		}

		internal static void VerifyParentingStatus()
		{
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel > global::Unity.Netcode.LogLevel.Normal || OrphanChildren.Count <= 0)
			{
				return;
			}
			global::Unity.Netcode.NetworkLog.LogWarning(string.Format("{0} ({1}) children not resolved to parents by the end of frame", "NetworkObject", OrphanChildren.Count));
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel > global::Unity.Netcode.LogLevel.Developer)
			{
				return;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.AppendLine("Orphaned Children:");
			foreach (global::Unity.Netcode.NetworkObject orphanChild in OrphanChildren)
			{
				stringBuilder.Append($"| {orphanChild} ");
			}
			stringBuilder.AppendLine("|");
			global::Unity.Netcode.NetworkLog.LogWarning(stringBuilder.ToString());
		}

		public ushort GetNetworkBehaviourOrderIndex(global::Unity.Netcode.NetworkBehaviour instance)
		{
			if (instance.NetworkBehaviourIdCache < ChildNetworkBehaviours.Count)
			{
				if (ChildNetworkBehaviours[instance.NetworkBehaviourIdCache] == instance)
				{
					return instance.NetworkBehaviourIdCache;
				}
				instance.NetworkBehaviourIdCache = 0;
			}
			for (ushort num = 0; num < ChildNetworkBehaviours.Count; num++)
			{
				if (ChildNetworkBehaviours[num] == instance)
				{
					instance.NetworkBehaviourIdCache = num;
					return num;
				}
			}
			return 0;
		}

		public global::Unity.Netcode.NetworkBehaviour GetNetworkBehaviourAtOrderIndex(ushort index)
		{
			if (index >= ChildNetworkBehaviours.Count)
			{
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogError(string.Format("{0} index {1} was out of bounds for {2}. NetworkBehaviours must be the same, and in the same order, between server and client.", "NetworkBehaviour", index, base.name));
				}
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
					stringBuilder.Append("Known child NetworkBehaviours:");
					for (int i = 0; i < ChildNetworkBehaviours.Count; i++)
					{
						global::Unity.Netcode.NetworkBehaviour networkBehaviour = ChildNetworkBehaviours[i];
						stringBuilder.Append($" [{i}] {networkBehaviour.__getTypeName()}");
						stringBuilder.Append((i < ChildNetworkBehaviours.Count - 1) ? "," : ".");
					}
					global::Unity.Netcode.NetworkLog.LogInfo(stringBuilder.ToString());
				}
				return null;
			}
			return ChildNetworkBehaviours[index];
		}

		internal void SynchronizeNetworkBehaviours<T>(ref global::Unity.Netcode.BufferSerializer<T> serializer, ulong targetClientId = 0uL) where T : global::Unity.Netcode.IReaderWriter
		{
			if (serializer.IsWriter)
			{
				global::Unity.Netcode.FastBufferWriter fastBufferWriter = serializer.GetFastBufferWriter();
				foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour in ChildNetworkBehaviours)
				{
					childNetworkBehaviour.InitializeVariables();
					childNetworkBehaviour.WriteNetworkVariableData(fastBufferWriter, targetClientId);
				}
				int position = fastBufferWriter.Position;
				fastBufferWriter.WriteValueSafe<byte>((byte)0, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				byte value = 0;
				foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour2 in ChildNetworkBehaviours)
				{
					if (childNetworkBehaviour2.Synchronize(ref serializer, targetClientId))
					{
						value++;
					}
				}
				int position2 = fastBufferWriter.Position;
				fastBufferWriter.Seek(position);
				fastBufferWriter.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.Seek(position2);
				return;
			}
			global::Unity.Netcode.FastBufferReader fastBufferReader = serializer.GetFastBufferReader();
			foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour3 in ChildNetworkBehaviours)
			{
				childNetworkBehaviour3.InitializeVariables();
				childNetworkBehaviour3.SetNetworkVariableData(fastBufferReader, targetClientId);
			}
			fastBufferReader.ReadValueSafe(out byte value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < value2; i++)
			{
				fastBufferReader.ReadValueSafe(out ushort value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				GetNetworkBehaviourAtOrderIndex(value3).Synchronize(ref serializer, targetClientId);
			}
		}

		internal global::Unity.Netcode.NetworkObject.SerializedObject Serialize(ulong targetClientId = 0uL, bool syncObservers = false)
		{
			global::Unity.Netcode.NetworkObject.SerializedObject result = new global::Unity.Netcode.NetworkObject.SerializedObject
			{
				HasParent = (base.transform.parent != null),
				WorldPositionStays = m_CachedWorldPositionStays,
				NetworkObjectId = NetworkObjectId,
				OwnerClientId = OwnerClientId,
				IsPlayerObject = IsPlayerObject,
				IsSceneObject = (IsSceneObject ?? true),
				DestroyWithScene = DestroyWithScene,
				DontDestroyWithOwner = DontDestroyWithOwner,
				HasOwnershipFlags = NetworkManagerOwner.DistributedAuthorityMode,
				OwnershipFlags = (ushort)Ownership,
				SyncObservers = syncObservers,
				Observers = (syncObservers ? global::System.Linq.Enumerable.ToArray(Observers) : null),
				NetworkSceneHandle = NetworkSceneHandle,
				Hash = CheckForGlobalObjectIdHashOverride(),
				OwnerObject = this,
				TargetClientId = targetClientId,
				HasInstantiationData = (InstantiationData != null && InstantiationData.Length != 0)
			};
			if (!AlwaysReplicateAsRoot && result.HasParent)
			{
				global::Unity.Netcode.NetworkObject component = base.transform.parent.GetComponent<global::Unity.Netcode.NetworkObject>();
				if ((bool)component)
				{
					result.ParentObjectId = component.NetworkObjectId;
					result.LatestParent = GetNetworkParenting();
					result.IsLatestParentSet = result.LatestParent.HasValue && result.LatestParent.HasValue;
				}
			}
			if (IncludeTransformWhenSpawning == null || IncludeTransformWhenSpawning(OwnerClientId))
			{
				result.HasTransform = SynchronizeTransform;
				bool flag = result.HasParent && !m_CachedWorldPositionStays;
				bool flag2 = result.HasParent && !m_CachedWorldPositionStays;
				if (!AutoObjectParentSync)
				{
					flag = false;
					flag2 = result.HasParent;
				}
				result.Transform = new global::Unity.Netcode.NetworkObject.SerializedObject.TransformData
				{
					Position = (flag ? base.transform.localPosition : base.transform.position),
					Rotation = (flag ? base.transform.localRotation : base.transform.rotation),
					Scale = (flag2 ? base.transform.localScale : base.transform.lossyScale)
				};
			}
			return result;
		}

		internal static global::Unity.Netcode.NetworkObject Deserialize(in global::Unity.Netcode.NetworkObject.SerializedObject serializedObject, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.NetworkManager networkManager, bool invokedByMessage = false)
		{
			int num = reader.Position + serializedObject.SynchronizationDataSize;
			byte[] value = null;
			if (serializedObject.HasInstantiationData)
			{
				reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			global::Unity.Netcode.NetworkObject networkObject = networkManager.SpawnManager.CreateLocalNetworkObject(serializedObject, value);
			if (networkObject == null)
			{
				if (networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogError(string.Format("Failed to spawn {0} for Hash {1}.", "NetworkObject", serializedObject.Hash));
				}
				try
				{
					reader.Seek(num);
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogException(exception);
				}
				return null;
			}
			networkObject.NetworkManagerOwner = networkManager;
			networkObject.OwnerClientId = serializedObject.OwnerClientId;
			networkObject.InvokeBehaviourNetworkPreSpawn();
			try
			{
				global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader> serializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader>(new global::Unity.Netcode.BufferSerializerReader(reader));
				networkObject.SynchronizeNetworkBehaviours(ref serializer, networkManager.LocalClientId);
				if (reader.Position != num)
				{
					global::UnityEngine.Debug.LogWarning($"[Size mismatch] Expected: {num} Currently At: {reader.Position}!");
					reader.Seek(num);
				}
			}
			catch
			{
				reader.Seek(num);
			}
			if (serializedObject.IsSceneObject && !serializedObject.HasParent && networkObject.m_LatestParent.HasValue)
			{
				networkObject.m_LatestParent = null;
			}
			if (networkObject.IsSpawned)
			{
				throw new global::Unity.Netcode.SpawnStateException($"[{networkObject.name}] Object-{networkObject.NetworkObjectId} is already spawned!");
			}
			networkManager.SpawnManager.NonAuthorityLocalSpawn(networkObject, in serializedObject, serializedObject.DestroyWithScene);
			if (serializedObject.SyncObservers)
			{
				ulong[] observers = serializedObject.Observers;
				foreach (ulong clientId in observers)
				{
					networkObject.AddObserver(clientId);
				}
			}
			if (networkManager.DistributedAuthorityMode)
			{
				networkObject.SpawnWithObservers = serializedObject.SpawnWithObservers;
			}
			if (networkManager.DistributedAuthorityMode && (!invokedByMessage || networkObject.IsPlayerObject) && (networkObject.SpawnWithObservers || networkObject.Observers.Contains(networkManager.LocalClientId)) && networkManager.LocalClient != null && networkManager.LocalClient.PlayerObject != null)
			{
				global::Unity.Netcode.NetworkObject playerObject = networkManager.LocalClient.PlayerObject;
				if (networkObject.IsPlayerObject)
				{
					playerObject.AddObserver(networkObject.OwnerClientId);
				}
				networkObject.AddObserver(playerObject.OwnerClientId);
				if (networkObject.IsPlayerObject)
				{
					foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.NetworkObject> spawnedObject in networkManager.SpawnManager.SpawnedObjects)
					{
						if (spawnedObject.Value.SpawnWithObservers)
						{
							spawnedObject.Value.AddObserver(networkObject.OwnerClientId);
						}
					}
				}
				if (networkObject.SpawnWithObservers)
				{
					foreach (global::Unity.Netcode.NetworkObject playerObject2 in networkManager.SpawnManager.PlayerObjects)
					{
						networkObject.AddObserver(playerObject2.OwnerClientId);
					}
				}
			}
			return networkObject;
		}

		internal void SubscribeToActiveSceneForSynch()
		{
			if (ActiveSceneSynchronization && IsSceneObject.HasValue && !IsSceneObject.Value)
			{
				global::UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= CurrentlyActiveSceneChanged;
				global::UnityEngine.SceneManagement.SceneManager.activeSceneChanged += CurrentlyActiveSceneChanged;
			}
		}

		private void CurrentlyActiveSceneChanged(global::UnityEngine.SceneManagement.Scene current, global::UnityEngine.SceneManagement.Scene next)
		{
			if (IsSpawned && IsSceneObject == false && !NetworkManagerOwner.ShutdownInProgress && ActiveSceneSynchronization && IsSceneObject.HasValue && !IsSceneObject.Value && base.gameObject.scene != next && base.gameObject.transform.parent == null)
			{
				global::UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(base.gameObject, next);
				SceneChangedUpdate(next);
			}
		}

		internal void SceneChangedUpdate(global::UnityEngine.SceneManagement.Scene scene, bool notify = false)
		{
			if (!IsSpawned || NetworkManagerOwner.SceneManager == null || NetworkManagerOwner.SceneManager.IsSceneEventInProgress())
			{
				return;
			}
			bool hasAuthority = HasAuthority;
			SceneOriginHandle = scene.handle;
			if (!hasAuthority && NetworkManagerOwner.SceneManager.ClientSceneHandleToServerSceneHandle.ContainsKey(SceneOriginHandle))
			{
				NetworkSceneHandle = NetworkManagerOwner.SceneManager.ClientSceneHandleToServerSceneHandle[SceneOriginHandle];
			}
			else if (hasAuthority)
			{
				if (NetworkManagerOwner.DistributedAuthorityMode && NetworkManagerOwner.SceneManager.ClientSceneHandleToServerSceneHandle.ContainsKey(SceneOriginHandle))
				{
					NetworkSceneHandle = NetworkManagerOwner.SceneManager.ClientSceneHandleToServerSceneHandle[SceneOriginHandle];
				}
				else
				{
					NetworkSceneHandle = SceneOriginHandle;
				}
			}
			else if (NetworkManagerOwner.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogWarningServer($"[Client-{NetworkManagerOwner.LocalClientId}][{base.name}] Server - " + $"client scene mismatch detected! Client-side scene handle ({SceneOriginHandle}) for scene ({base.gameObject.scene.name})" + "has no associated server side (network) scene handle!");
			}
			OnMigratedToNewScene?.Invoke();
			if (hasAuthority && notify && !base.transform.parent)
			{
				NetworkManagerOwner.SceneManager.NotifyNetworkObjectSceneChanged(this);
			}
		}

		private void Awake()
		{
			m_ChildNetworkBehaviours = null;
			NetworkTransforms?.Clear();
			NetworkRigidbodies?.Clear();
			SetCachedParent(base.transform.parent);
			SceneOrigin = base.gameObject.scene;
		}

		internal bool UpdateForSceneChanges()
		{
			if (!SceneMigrationSynchronization || !IsSpawned || NetworkManagerOwner.ShutdownInProgress || !NetworkManagerOwner.NetworkConfig.EnableSceneManagement || IsSceneObject != false || !base.gameObject)
			{
				return false;
			}
			if (base.gameObject.scene.handle != SceneOriginHandle)
			{
				SceneChangedUpdate(base.gameObject.scene, notify: true);
			}
			return true;
		}

		internal uint CheckForGlobalObjectIdHashOverride()
		{
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			if (networkManager.IsServer || networkManager.DistributedAuthorityMode)
			{
				if (networkManager.PrefabHandler.ContainsHandler(this))
				{
					uint sourceGlobalObjectIdHash = networkManager.PrefabHandler.GetSourceGlobalObjectIdHash(GlobalObjectIdHash);
					if (sourceGlobalObjectIdHash != 0)
					{
						return sourceGlobalObjectIdHash;
					}
					return GlobalObjectIdHash;
				}
				if (!networkManager.NetworkConfig.EnableSceneManagement && IsSceneObject.Value && InScenePlacedSourceGlobalObjectIdHash != 0)
				{
					return InScenePlacedSourceGlobalObjectIdHash;
				}
				if (!IsSceneObject.Value && GlobalObjectIdHash != PrefabGlobalObjectIdHash)
				{
					if (PrefabGlobalObjectIdHash != 0)
					{
						return PrefabGlobalObjectIdHash;
					}
					if (networkManager.NetworkConfig.Prefabs.OverrideToNetworkPrefab.ContainsKey(GlobalObjectIdHash))
					{
						return networkManager.NetworkConfig.Prefabs.OverrideToNetworkPrefab[GlobalObjectIdHash];
					}
				}
			}
			return GlobalObjectIdHash;
		}

		internal void OnNetworkBehaviourDestroyed(global::Unity.Netcode.NetworkBehaviour networkBehaviour)
		{
			if (networkBehaviour.IsSpawned && IsSpawned)
			{
				if (NetworkManagerOwner.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("NetworkBehaviour-" + networkBehaviour.name + " is being destroyed while NetworkObject-" + base.name + " is still spawned! (could break state synchronization)");
				}
				ChildNetworkBehaviours.Remove(networkBehaviour);
			}
		}
	}
}
