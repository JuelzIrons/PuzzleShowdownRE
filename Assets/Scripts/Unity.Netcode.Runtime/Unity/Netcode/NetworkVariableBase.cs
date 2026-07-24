namespace Unity.Netcode
{
	public abstract class NetworkVariableBase : global::System.IDisposable
	{
		[global::UnityEngine.SerializeField]
		internal global::Unity.Netcode.NetworkVariableUpdateTraits UpdateTraits;

		[global::System.NonSerialized]
		internal double LastUpdateSent;

		private protected global::Unity.Netcode.NetworkBehaviour m_NetworkBehaviour;

		private protected global::Unity.Netcode.NetworkManager m_NetworkManager;

		private protected global::Unity.Netcode.NetworkObject m_NetworkObject;

		private bool m_UseServerTime;

		public const global::Unity.Netcode.NetworkVariableReadPermission DefaultReadPerm = global::Unity.Netcode.NetworkVariableReadPermission.Everyone;

		public const global::Unity.Netcode.NetworkVariableWritePermission DefaultWritePerm = global::Unity.Netcode.NetworkVariableWritePermission.Server;

		private bool m_IsDirty;

		public readonly global::Unity.Netcode.NetworkVariableReadPermission ReadPerm;

		internal global::Unity.Netcode.NetworkVariableWritePermission InternalWritePerm;

		internal static bool IgnoreInitializeWarning;

		internal bool NetworkUpdaterCheck;

		internal bool HasBeenInitialized { get; private set; }

		public string Name { get; internal set; }

		public global::Unity.Netcode.NetworkVariableWritePermission WritePerm => InternalWritePerm;

		internal string GetWritePermissionError()
		{
			return $"|Client-{m_NetworkManager.LocalClientId}|{m_NetworkBehaviour.name}|{Name}| Write permissions ({WritePerm}) for this client instance is not allowed!";
		}

		internal void LogWritePermissionError()
		{
			global::UnityEngine.Debug.LogError(GetWritePermissionError());
		}

		public global::Unity.Netcode.NetworkBehaviour GetBehaviour()
		{
			return m_NetworkBehaviour;
		}

		public void Initialize(global::Unity.Netcode.NetworkBehaviour networkBehaviour)
		{
			if (HasBeenInitialized)
			{
				return;
			}
			if (!networkBehaviour)
			{
				throw new global::System.Exception("[" + GetType().Name + "][Initialize] NetworkBehaviour parameter passed in is null!");
			}
			m_NetworkBehaviour = networkBehaviour;
			if (!m_NetworkBehaviour.NetworkManager || !m_NetworkBehaviour.NetworkObject)
			{
				return;
			}
			m_NetworkObject = m_NetworkBehaviour.NetworkObject;
			if ((bool)m_NetworkObject.NetworkManagerOwner)
			{
				m_NetworkManager = m_NetworkObject.NetworkManagerOwner;
				m_UseServerTime = m_NetworkManager.CMBServiceConnection || !m_NetworkManager.IsServer;
				InternalWritePerm = (m_NetworkManager.DistributedAuthorityMode ? global::Unity.Netcode.NetworkVariableWritePermission.Owner : InternalWritePerm);
				OnInitialize();
				if (m_NetworkManager.NetworkTimeSystem != null)
				{
					UpdateLastSentTime();
					HasBeenInitialized = true;
				}
				else if (m_NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::UnityEngine.Debug.LogWarning("[" + m_NetworkBehaviour.name + "][" + m_NetworkBehaviour.GetType().Name + "][" + GetType().Name + "][Initialize] NetworkManager has no NetworkTimeSystem assigned!");
				}
			}
		}

		internal void InternalOnSpawned()
		{
			if (m_NetworkObject.IsSpawnAuthority && IsDirty() && CanWrite() && CanSend())
			{
				UpdateLastSentTime();
				ResetDirty();
				SetDirty(isDirty: false);
			}
		}

		internal void InternalOnPreDespawn()
		{
		}

		internal void Deinitialize()
		{
			HasBeenInitialized = false;
		}

		public virtual void OnInitialize()
		{
		}

		public void SetUpdateTraits(global::Unity.Netcode.NetworkVariableUpdateTraits traits)
		{
			UpdateTraits = traits;
		}

		public virtual bool ExceedsDirtinessThreshold()
		{
			return true;
		}

		protected NetworkVariableBase(global::Unity.Netcode.NetworkVariableReadPermission readPerm = global::Unity.Netcode.NetworkVariableReadPermission.Everyone, global::Unity.Netcode.NetworkVariableWritePermission writePerm = global::Unity.Netcode.NetworkVariableWritePermission.Server)
		{
			ReadPerm = readPerm;
			InternalWritePerm = writePerm;
		}

		public virtual void SetDirty(bool isDirty)
		{
			m_IsDirty = isDirty;
			if (m_IsDirty)
			{
				MarkNetworkBehaviourDirty();
			}
		}

		internal bool CanSend()
		{
			double num = (m_UseServerTime ? m_NetworkManager.ServerTime.Time : m_NetworkManager.NetworkTimeSystem.LocalTime) - LastUpdateSent;
			if (!(UpdateTraits.MaxSecondsBetweenUpdates > 0f) || !(num >= (double)UpdateTraits.MaxSecondsBetweenUpdates))
			{
				if (num >= (double)UpdateTraits.MinSecondsBetweenUpdates)
				{
					return ExceedsDirtinessThreshold();
				}
				return false;
			}
			return true;
		}

		internal void UpdateLastSentTime()
		{
			LastUpdateSent = (m_UseServerTime ? m_NetworkManager.ServerTime.Time : m_NetworkManager.NetworkTimeSystem.LocalTime);
		}

		protected void MarkNetworkBehaviourDirty()
		{
			if (m_NetworkBehaviour == null)
			{
				if (!IgnoreInitializeWarning)
				{
					global::UnityEngine.Debug.LogWarning("NetworkVariable is written to, but doesn't know its NetworkBehaviour yet. Are you modifying a NetworkVariable before the NetworkObject is spawned?");
				}
			}
			else if (m_NetworkManager.ShutdownInProgress)
			{
				if (m_NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::UnityEngine.Debug.LogWarning("NetworkVariable is written to during the NetworkManager shutdown! Are you modifying a NetworkVariable within a NetworkBehaviour.OnDestroy or NetworkBehaviour.OnDespawn method?");
				}
			}
			else if (!m_NetworkManager.IsListening)
			{
				if (m_NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::UnityEngine.Debug.LogWarning("NetworkVariable is written to after the NetworkManager has already shutdown! Are you modifying a NetworkVariable within a NetworkBehaviour.OnDestroy or NetworkBehaviour.OnDespawn method?");
				}
			}
			else
			{
				m_NetworkManager.BehaviourUpdater?.AddForUpdate(m_NetworkObject);
			}
		}

		public virtual void ResetDirty()
		{
			m_IsDirty = false;
		}

		public virtual bool IsDirty()
		{
			return m_IsDirty;
		}

		public bool CanClientRead(ulong clientId)
		{
			if (!m_NetworkBehaviour)
			{
				return false;
			}
			if (m_NetworkManager.DistributedAuthorityMode)
			{
				return true;
			}
			global::Unity.Netcode.NetworkVariableReadPermission readPerm = ReadPerm;
			if (readPerm == global::Unity.Netcode.NetworkVariableReadPermission.Everyone || readPerm != global::Unity.Netcode.NetworkVariableReadPermission.Owner)
			{
				return true;
			}
			if (clientId != m_NetworkObject.OwnerClientId)
			{
				return clientId == 0;
			}
			return true;
		}

		public bool CanClientWrite(ulong clientId)
		{
			if (!m_NetworkBehaviour)
			{
				return false;
			}
			global::Unity.Netcode.NetworkVariableWritePermission writePerm = WritePerm;
			if (writePerm == global::Unity.Netcode.NetworkVariableWritePermission.Server || writePerm != global::Unity.Netcode.NetworkVariableWritePermission.Owner)
			{
				return clientId == 0;
			}
			return clientId == m_NetworkObject.OwnerClientId;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal bool CanWrite()
		{
			if ((bool)m_NetworkManager)
			{
				return CanClientWrite(m_NetworkManager.LocalClientId);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal bool CannotWrite()
		{
			if ((bool)m_NetworkManager)
			{
				return !CanClientWrite(m_NetworkManager.LocalClientId);
			}
			return false;
		}

		internal virtual void OnCheckIsDirtyState()
		{
		}

		public abstract void WriteDelta(global::Unity.Netcode.FastBufferWriter writer);

		public abstract void WriteField(global::Unity.Netcode.FastBufferWriter writer);

		public abstract void ReadField(global::Unity.Netcode.FastBufferReader reader);

		public abstract void ReadDelta(global::Unity.Netcode.FastBufferReader reader, bool keepDirtyDelta);

		internal virtual void PostDeltaRead()
		{
		}

		internal virtual void WriteFieldSynchronization(global::Unity.Netcode.FastBufferWriter writer)
		{
			WriteField(writer);
		}

		public virtual void Dispose()
		{
			HasBeenInitialized = false;
			m_NetworkBehaviour = null;
			m_NetworkObject = null;
			m_NetworkManager = null;
		}
	}
}
