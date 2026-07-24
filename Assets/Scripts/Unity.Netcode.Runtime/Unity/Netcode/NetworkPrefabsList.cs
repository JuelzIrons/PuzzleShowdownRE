namespace Unity.Netcode
{
	[global::UnityEngine.CreateAssetMenu(fileName = "NetworkPrefabsList", menuName = "Netcode/Network Prefabs List")]
	public class NetworkPrefabsList : global::UnityEngine.ScriptableObject
	{
		internal delegate void OnAddDelegate(global::Unity.Netcode.NetworkPrefab prefab);

		internal delegate void OnRemoveDelegate(global::Unity.Netcode.NetworkPrefab prefab);

		internal global::Unity.Netcode.NetworkPrefabsList.OnAddDelegate OnAdd;

		internal global::Unity.Netcode.NetworkPrefabsList.OnRemoveDelegate OnRemove;

		[global::UnityEngine.SerializeField]
		internal bool IsDefault;

		[global::UnityEngine.Serialization.FormerlySerializedAs("Prefabs")]
		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab> List = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkPrefab>();

		public global::System.Collections.Generic.IReadOnlyList<global::Unity.Netcode.NetworkPrefab> PrefabList => List;

		public void Add(global::Unity.Netcode.NetworkPrefab prefab)
		{
			List.Add(prefab);
			OnAdd?.Invoke(prefab);
		}

		public void Remove(global::Unity.Netcode.NetworkPrefab prefab)
		{
			List.Remove(prefab);
			OnRemove?.Invoke(prefab);
		}

		public bool Contains(global::UnityEngine.GameObject prefab)
		{
			for (int i = 0; i < List.Count; i++)
			{
				if (List[i].Prefab == prefab)
				{
					return true;
				}
			}
			return false;
		}

		public bool Contains(global::Unity.Netcode.NetworkPrefab prefab)
		{
			for (int i = 0; i < List.Count; i++)
			{
				if (List[i].Equals(prefab))
				{
					return true;
				}
			}
			return false;
		}
	}
}
