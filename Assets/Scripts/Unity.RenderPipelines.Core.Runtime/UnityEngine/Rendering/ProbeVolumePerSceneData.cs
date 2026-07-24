namespace UnityEngine.Rendering
{
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.AddComponentMenu("")]
	public class ProbeVolumePerSceneData : global::UnityEngine.MonoBehaviour
	{
		[global::System.Serializable]
		internal struct ObsoletePerScenarioData
		{
			public int sceneHash;

			public global::UnityEngine.TextAsset cellDataAsset;

			public global::UnityEngine.TextAsset cellOptionalDataAsset;
		}

		[global::System.Serializable]
		private struct ObsoleteSerializablePerScenarioDataItem
		{
			public string scenario;

			public global::UnityEngine.Rendering.ProbeVolumePerSceneData.ObsoletePerScenarioData data;
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("bakingSet")]
		internal global::UnityEngine.Rendering.ProbeVolumeBakingSet serializedBakingSet;

		[global::UnityEngine.SerializeField]
		internal string sceneGUID = "";

		[global::UnityEngine.Serialization.FormerlySerializedAs("asset")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.ObsoleteProbeVolumeAsset obsoleteAsset;

		[global::UnityEngine.Serialization.FormerlySerializedAs("cellSharedDataAsset")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.TextAsset obsoleteCellSharedDataAsset;

		[global::UnityEngine.Serialization.FormerlySerializedAs("cellSupportDataAsset")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.TextAsset obsoleteCellSupportDataAsset;

		[global::UnityEngine.Serialization.FormerlySerializedAs("serializedScenarios")]
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeVolumePerSceneData.ObsoleteSerializablePerScenarioDataItem> obsoleteSerializedScenarios = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeVolumePerSceneData.ObsoleteSerializablePerScenarioDataItem>();

		public global::UnityEngine.Rendering.ProbeVolumeBakingSet bakingSet => serializedBakingSet;

		internal void Clear()
		{
			QueueSceneRemoval();
			serializedBakingSet = null;
		}

		internal void QueueSceneLoading()
		{
			if (!(serializedBakingSet == null))
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.instance.AddPendingSceneLoading(sceneGUID, serializedBakingSet);
			}
		}

		internal void QueueSceneRemoval()
		{
			if (serializedBakingSet != null)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.instance.AddPendingSceneRemoval(sceneGUID);
			}
		}

		private void OnEnable()
		{
			global::UnityEngine.Rendering.ProbeReferenceVolume.instance.RegisterPerSceneData(this);
		}

		private void OnDisable()
		{
			QueueSceneRemoval();
			global::UnityEngine.Rendering.ProbeReferenceVolume.instance.UnregisterPerSceneData(this);
		}

		private void OnValidate()
		{
		}

		internal void Initialize()
		{
			global::UnityEngine.Rendering.ProbeReferenceVolume.instance.RegisterBakingSet(this);
			QueueSceneRemoval();
			QueueSceneLoading();
		}

		internal bool ResolveCellData()
		{
			if (serializedBakingSet != null)
			{
				return serializedBakingSet.ResolveCellData(serializedBakingSet.GetSceneCellIndexList(sceneGUID));
			}
			return false;
		}
	}
}
