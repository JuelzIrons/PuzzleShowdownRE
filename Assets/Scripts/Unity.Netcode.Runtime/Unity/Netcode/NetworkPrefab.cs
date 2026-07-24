namespace Unity.Netcode
{
	[global::System.Serializable]
	public class NetworkPrefab
	{
		public global::Unity.Netcode.NetworkPrefabOverride Override;

		public global::UnityEngine.GameObject Prefab;

		public global::UnityEngine.GameObject SourcePrefabToOverride;

		public uint SourceHashToOverride;

		public global::UnityEngine.GameObject OverridingTargetPrefab;

		public uint SourcePrefabGlobalObjectIdHash
		{
			get
			{
				switch (Override)
				{
				case global::Unity.Netcode.NetworkPrefabOverride.None:
				{
					if (Prefab != null && Prefab.TryGetComponent<global::Unity.Netcode.NetworkObject>(out var component2))
					{
						return component2.GlobalObjectIdHash;
					}
					throw new global::System.InvalidOperationException("Prefab field is not set or is not a NetworkObject");
				}
				case global::Unity.Netcode.NetworkPrefabOverride.Prefab:
				{
					if (SourcePrefabToOverride != null && SourcePrefabToOverride.TryGetComponent<global::Unity.Netcode.NetworkObject>(out var component))
					{
						return component.GlobalObjectIdHash;
					}
					throw new global::System.InvalidOperationException("Source Prefab field is not set or is not a NetworkObject");
				}
				case global::Unity.Netcode.NetworkPrefabOverride.Hash:
					return SourceHashToOverride;
				default:
					throw new global::System.ArgumentOutOfRangeException();
				}
			}
		}

		public uint TargetPrefabGlobalObjectIdHash
		{
			get
			{
				switch (Override)
				{
				case global::Unity.Netcode.NetworkPrefabOverride.None:
					return 0u;
				case global::Unity.Netcode.NetworkPrefabOverride.Prefab:
				case global::Unity.Netcode.NetworkPrefabOverride.Hash:
				{
					if (OverridingTargetPrefab != null && OverridingTargetPrefab.TryGetComponent<global::Unity.Netcode.NetworkObject>(out var component))
					{
						return component.GlobalObjectIdHash;
					}
					throw new global::System.InvalidOperationException("Target Prefab field is not set or is not a NetworkObject");
				}
				default:
					throw new global::System.ArgumentOutOfRangeException();
				}
			}
		}

		public bool Equals(global::Unity.Netcode.NetworkPrefab other)
		{
			if (Override == other.Override && Prefab == other.Prefab && SourcePrefabToOverride == other.SourcePrefabToOverride && SourceHashToOverride == other.SourceHashToOverride)
			{
				return OverridingTargetPrefab == other.OverridingTargetPrefab;
			}
			return false;
		}

		public bool Validate(int index = -1)
		{
			if (Override == global::Unity.Netcode.NetworkPrefabOverride.None)
			{
				if (Prefab == null)
				{
					global::Unity.Netcode.NetworkLog.LogWarning(string.Format("{0} cannot be null ({1} at index: {2})", "NetworkPrefab", "NetworkPrefab", index));
					return false;
				}
				global::Unity.Netcode.NetworkObject component = Prefab.GetComponent<global::Unity.Netcode.NetworkObject>();
				if (component == null)
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
					{
						global::Unity.Netcode.NetworkLog.LogWarning(global::Unity.Netcode.NetworkPrefabHandler.PrefabDebugHelper(this) + " is missing a NetworkObject component (entry will be ignored).");
					}
					return false;
				}
				return true;
			}
			switch (Override)
			{
			case global::Unity.Netcode.NetworkPrefabOverride.Hash:
				if (SourceHashToOverride == 0)
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
					{
						global::Unity.Netcode.NetworkLog.LogWarning("NetworkPrefab SourceHashToOverride is zero (entry will be ignored).");
					}
					return false;
				}
				break;
			case global::Unity.Netcode.NetworkPrefabOverride.Prefab:
			{
				if (SourcePrefabToOverride == null)
				{
					if (Prefab != null)
					{
						SourcePrefabToOverride = Prefab;
					}
					else if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
					{
						global::Unity.Netcode.NetworkLog.LogWarning("NetworkPrefab SourcePrefabToOverride is null (entry will be ignored).");
						return false;
					}
				}
				if (!SourcePrefabToOverride.TryGetComponent<global::Unity.Netcode.NetworkObject>(out var _))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
					{
						global::Unity.Netcode.NetworkLog.LogWarning("NetworkPrefab (" + SourcePrefabToOverride.name + ") is missing a NetworkObject component (entry will be ignored).");
					}
					return false;
				}
				break;
			}
			}
			if (OverridingTargetPrefab == null)
			{
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Error)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("NetworkPrefab OverridingTargetPrefab is null!");
				}
				switch (Override)
				{
				case global::Unity.Netcode.NetworkPrefabOverride.Hash:
					global::UnityEngine.Debug.LogWarning(string.Format("{0} override entry {1} will be removed and ignored.", "NetworkPrefab", SourceHashToOverride));
					break;
				case global::Unity.Netcode.NetworkPrefabOverride.Prefab:
					global::UnityEngine.Debug.LogWarning("NetworkPrefab override entry (" + SourcePrefabToOverride.name + ") will be removed and ignored.");
					break;
				}
				return false;
			}
			return true;
		}

		public override string ToString()
		{
			return $"{{SourceHash: {SourceHashToOverride}, TargetHash: {TargetPrefabGlobalObjectIdHash}}}";
		}
	}
}
