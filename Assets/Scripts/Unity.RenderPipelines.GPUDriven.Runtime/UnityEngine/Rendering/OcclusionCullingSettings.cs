namespace UnityEngine.Rendering
{
	public struct OcclusionCullingSettings
	{
		public int viewInstanceID;

		public global::UnityEngine.Rendering.OcclusionTest occlusionTest;

		public int instanceMultiplier;

		public OcclusionCullingSettings(int viewInstanceID, global::UnityEngine.Rendering.OcclusionTest occlusionTest)
		{
			this.viewInstanceID = viewInstanceID;
			this.occlusionTest = occlusionTest;
			instanceMultiplier = 1;
		}
	}
}
