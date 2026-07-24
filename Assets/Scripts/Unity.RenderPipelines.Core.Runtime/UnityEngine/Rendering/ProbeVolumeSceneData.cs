namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Obsolete("This class is no longer necessary for APV implementation. #from(2023.3)")]
	public class ProbeVolumeSceneData
	{
		internal global::UnityEngine.Object parentAsset;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("sceneBounds")]
		[global::System.Obsolete("This data is now serialized directly in the baking set asset. #from(2023.3)")]
		internal global::UnityEngine.Rendering.SerializedDictionary<string, global::UnityEngine.Bounds> obsoleteSceneBounds;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("hasProbeVolumes")]
		[global::System.Obsolete("This data is now serialized directly in the baking set asset. #from(2023.3)")]
		internal global::UnityEngine.Rendering.SerializedDictionary<string, bool> obsoleteHasProbeVolumes;

		public ProbeVolumeSceneData(global::UnityEngine.Object parentAsset)
		{
			SetParentObject(parentAsset);
		}

		[global::System.Obsolete("#from(2023.3)")]
		public void SetParentObject(global::UnityEngine.Object parent)
		{
			parentAsset = parent;
		}
	}
}
