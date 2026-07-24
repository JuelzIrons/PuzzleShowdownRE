namespace UnityEngine.Rendering.Universal
{
	internal struct LayerBatch
	{
		public int startLayerID;

		public int endLayerValue;

		public global::UnityEngine.Rendering.SortingLayerRange layerRange;

		public global::UnityEngine.Rendering.Universal.LightStats lightStats;

		public bool useNormals;

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D> lights;

		public global::System.Collections.Generic.List<int> shadowIndices;

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D> shadowCasters;

		internal int[] activeBlendStylesIndices;

		public void InitRTIds(int index)
		{
			lights = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D>();
			shadowIndices = new global::System.Collections.Generic.List<int>();
			shadowCasters = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D>();
		}
	}
}
