namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	internal class DecalSettings
	{
		public global::UnityEngine.Rendering.Universal.DecalTechniqueOption technique;

		public float maxDrawDistance = 1000f;

		public bool decalLayers;

		public global::UnityEngine.Rendering.Universal.DBufferSettings dBufferSettings;

		public global::UnityEngine.Rendering.Universal.DecalScreenSpaceSettings screenSpaceSettings;
	}
}
