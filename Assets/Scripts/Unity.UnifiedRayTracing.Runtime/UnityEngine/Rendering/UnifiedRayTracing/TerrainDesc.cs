namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal struct TerrainDesc
	{
		public global::UnityEngine.Terrain terrain;

		public global::UnityEngine.Matrix4x4 localToWorldMatrix;

		public uint mask;

		public uint renderingLayerMask;

		public uint materialID;

		public bool enableTriangleCulling;

		public bool frontTriangleCounterClockwise;

		public TerrainDesc(global::UnityEngine.Terrain terrain)
		{
			this.terrain = terrain;
			localToWorldMatrix = global::UnityEngine.Matrix4x4.identity;
			mask = uint.MaxValue;
			renderingLayerMask = uint.MaxValue;
			materialID = 0u;
			enableTriangleCulling = true;
			frontTriangleCounterClockwise = false;
		}
	}
}
