namespace Unity.Netcode
{
	[global::System.Serializable]
	public class NetworkConfig
	{
		internal const float MinSpawnTimeout = 10f;

		internal const float MaxSpawnTimeout = 3600f;

		[global::UnityEngine.Tooltip("Use this to make two builds incompatible with each other")]
		public ushort ProtocolVersion;

		[global::UnityEngine.Tooltip("The NetworkTransport to use")]
		public global::Unity.Netcode.NetworkTransport NetworkTransport;

		[global::UnityEngine.Tooltip("When set, NetworkManager will automatically create and spawn the assigned player prefab. This can be overridden by adding it to the NetworkPrefabs list and selecting override.")]
		public global::UnityEngine.GameObject PlayerPrefab;

		[global::UnityEngine.SerializeField]
		public global::Unity.Netcode.NetworkPrefabs Prefabs = new global::Unity.Netcode.NetworkPrefabs();

		[global::UnityEngine.Tooltip("The tickrate. This value controls how often netcode runs user code and sends out data. The value is in 'ticks per seconds' which means a value of 50 will result in 50 ticks being executed per second or a fixed delta time of 0.02.")]
		public uint TickRate = 30u;

		[global::UnityEngine.Tooltip("The amount of seconds for the server to wait for the connection approval handshake to complete before the client is disconnected")]
		public int ClientConnectionBufferTimeout = 10;

		[global::UnityEngine.Tooltip("Whether or not to force clients to be approved before they connect")]
		public bool ConnectionApproval;

		[global::UnityEngine.Tooltip("The connection data sent along with connection requests")]
		public byte[] ConnectionData = new byte[0];

		[global::UnityEngine.Tooltip("Enable this to re-sync the NetworkTime after the initial sync")]
		public bool EnableTimeResync;

		[global::UnityEngine.Tooltip("The amount of seconds between re-syncs of NetworkTime, if enabled")]
		public int TimeResyncInterval = 30;

		[global::UnityEngine.Tooltip("Ensures that NetworkVariables can be read even if a client accidental writes where its not allowed to. This will cost some CPU time and bandwidth")]
		public bool EnsureNetworkVariableLengthSafety;

		[global::UnityEngine.Tooltip("Enables scene management. This will allow network scene switches and automatic scene difference corrections upon connect.\nSoftSynced scene objects wont work with this disabled. That means that disabling SceneManagement also enables PrefabSync.")]
		public bool EnableSceneManagement = true;

		[global::UnityEngine.Tooltip("Whether or not the netcode should check for differences in the prefab lists at connection")]
		public bool ForceSamePrefabs = true;

		[global::UnityEngine.Tooltip("If true, NetworkIds will be reused after the NetworkIdRecycleDelay")]
		public bool RecycleNetworkIds = true;

		[global::UnityEngine.Tooltip("The amount of seconds a NetworkId has to unused in order for it to be reused")]
		public float NetworkIdRecycleDelay = 120f;

		[global::UnityEngine.Tooltip("The maximum amount of bytes to use for RPC messages.")]
		public global::Unity.Netcode.HashSize RpcHashSize;

		[global::UnityEngine.Tooltip("The amount of seconds to wait for all clients to load or unload a requested scene (only when EnableSceneManagement is enabled)")]
		public int LoadSceneTimeOut = 120;

		[global::UnityEngine.Tooltip("The amount of time a message will be held (deferred) if the destination NetworkObject needed to process the message doesn't exist yet. If the NetworkObject is not spawned within this time period, all deferred messages for that NetworkObject will be dropped.")]
		[global::UnityEngine.Range(10f, 3600f)]
		public float SpawnTimeout = 10f;

		public bool EnableNetworkLogs = true;

		public const int RttAverageSamples = 5;

		public const int RttWindowSize = 64;

		[global::UnityEngine.Tooltip("Determines whether to use the client-server or distributed authority network topology.")]
		public global::Unity.Netcode.NetworkTopologyTypes NetworkTopology;

		[global::UnityEngine.HideInInspector]
		public bool UseCMBService;

		[global::UnityEngine.Tooltip("When enabled (default), the player prefab will automatically be spawned (client-side) upon the client being approved and synchronized.")]
		public bool AutoSpawnPlayerPrefabClientSide = true;

		[global::UnityEngine.Tooltip("Enable (default) if you want to gather messaging metrics. Realtime Network Stats Monitor requires this to be enabled. Disabling this can improve performance in release builds.")]
		public bool NetworkMessageMetrics = true;

		[global::UnityEngine.Tooltip("Enable (default) if you want to profile network messages with development builds and defaults to being disabled in release builds. When disabled, network messaging profiling will be disabled in development builds.")]
		public bool NetworkProfilingMetrics = true;

		private ulong? m_ConfigHash;

		[global::System.NonSerialized]
		private bool m_DidWarnOldPrefabList;

		[global::UnityEngine.Serialization.FormerlySerializedAs("NetworkPrefabs")]
		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab> OldPrefabList;

		internal void OnValidate()
		{
			SpawnTimeout = global::UnityEngine.Mathf.Clamp(SpawnTimeout, 10f, 3600f);
		}

		public string ToBase64()
		{
			global::Unity.Netcode.FastBufferWriter fastBufferWriter = new global::Unity.Netcode.FastBufferWriter(1024, global::Unity.Collections.Allocator.Temp);
			using (fastBufferWriter)
			{
				fastBufferWriter.WriteValueSafe(in ProtocolVersion, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in TickRate, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in ClientConnectionBufferTimeout, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in ConnectionApproval, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in LoadSceneTimeOut, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in EnableTimeResync, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in EnsureNetworkVariableLengthSafety, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in RpcHashSize, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				fastBufferWriter.WriteValueSafe(in ForceSamePrefabs, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in EnableSceneManagement, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in RecycleNetworkIds, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in NetworkIdRecycleDelay, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in EnableNetworkLogs, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				return global::System.Convert.ToBase64String(fastBufferWriter.ToArray());
			}
		}

		public void FromBase64(string base64)
		{
			byte[] buffer = global::System.Convert.FromBase64String(base64);
			using global::Unity.Netcode.FastBufferReader fastBufferReader = new global::Unity.Netcode.FastBufferReader(buffer, global::Unity.Collections.Allocator.Temp);
			using (fastBufferReader)
			{
				fastBufferReader.ReadValueSafe(out ProtocolVersion, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out TickRate, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out ClientConnectionBufferTimeout, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out ConnectionApproval, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out LoadSceneTimeOut, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out EnableTimeResync, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out EnsureNetworkVariableLengthSafety, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out RpcHashSize, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				fastBufferReader.ReadValueSafe(out ForceSamePrefabs, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out EnableSceneManagement, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out RecycleNetworkIds, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out NetworkIdRecycleDelay, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferReader.ReadValueSafe(out EnableNetworkLogs, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}

		internal void ClearConfigHash()
		{
			m_ConfigHash = null;
		}

		public ulong GetConfig(bool cache = true)
		{
			if (m_ConfigHash.HasValue && cache)
			{
				return m_ConfigHash.Value;
			}
			global::Unity.Netcode.FastBufferWriter fastBufferWriter = new global::Unity.Netcode.FastBufferWriter(1024, global::Unity.Collections.Allocator.Temp, int.MaxValue);
			using (fastBufferWriter)
			{
				fastBufferWriter.WriteValueSafe(in ProtocolVersion, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe("15.0.0");
				if (ForceSamePrefabs)
				{
					foreach (global::System.Collections.Generic.KeyValuePair<uint, global::Unity.Netcode.NetworkPrefab> item in global::System.Linq.Enumerable.OrderBy(Prefabs.NetworkPrefabOverrideLinks, (global::System.Collections.Generic.KeyValuePair<uint, global::Unity.Netcode.NetworkPrefab> x) => x.Key))
					{
						fastBufferWriter.WriteValueSafe<uint>(item.Key, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					}
				}
				fastBufferWriter.WriteValueSafe(in TickRate, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in ConnectionApproval, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in ForceSamePrefabs, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in EnableSceneManagement, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in EnsureNetworkVariableLengthSafety, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				fastBufferWriter.WriteValueSafe(in RpcHashSize, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				if (cache)
				{
					m_ConfigHash = fastBufferWriter.ToArray().Hash64();
					return m_ConfigHash.Value;
				}
				return fastBufferWriter.ToArray().Hash64();
			}
		}

		public bool CompareConfig(ulong hash)
		{
			return hash == GetConfig();
		}

		internal void InitializePrefabs()
		{
			if (HasOldPrefabList())
			{
				MigrateOldNetworkPrefabsToNetworkPrefabsList();
			}
			Prefabs.Initialize();
		}

		private void WarnOldPrefabList()
		{
			if (!m_DidWarnOldPrefabList)
			{
				global::UnityEngine.Debug.LogWarning("Using Legacy Network Prefab List. Consider Migrating.");
				m_DidWarnOldPrefabList = true;
			}
		}

		internal bool HasOldPrefabList()
		{
			global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab> oldPrefabList = OldPrefabList;
			if (oldPrefabList == null)
			{
				return false;
			}
			return oldPrefabList.Count > 0;
		}

		internal global::Unity.Netcode.NetworkPrefabsList MigrateOldNetworkPrefabsToNetworkPrefabsList()
		{
			if (OldPrefabList == null || OldPrefabList.Count == 0)
			{
				return null;
			}
			if (Prefabs == null)
			{
				throw new global::System.Exception("Prefabs field is null.");
			}
			Prefabs.NetworkPrefabsLists.Add(global::UnityEngine.ScriptableObject.CreateInstance<global::Unity.Netcode.NetworkPrefabsList>());
			global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab> oldPrefabList = OldPrefabList;
			if (oldPrefabList != null && oldPrefabList.Count > 0)
			{
				foreach (global::Unity.Netcode.NetworkPrefab oldPrefab in OldPrefabList)
				{
					Prefabs.NetworkPrefabsLists[Prefabs.NetworkPrefabsLists.Count - 1].Add(oldPrefab);
				}
			}
			OldPrefabList = null;
			return Prefabs.NetworkPrefabsLists[Prefabs.NetworkPrefabsLists.Count - 1];
		}
	}
}
