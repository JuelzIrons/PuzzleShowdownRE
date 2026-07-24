namespace UnityEngine.Rendering.UnifiedRayTracing
{
	public struct MeshInstanceDesc
	{
		public global::UnityEngine.Mesh mesh;

		public int subMeshIndex;

		public global::UnityEngine.Matrix4x4 localToWorldMatrix;

		public uint mask;

		public uint instanceID;

		public bool enableTriangleCulling;

		public bool frontTriangleCounterClockwise;

		public bool opaqueGeometry;

		public MeshInstanceDesc(global::UnityEngine.Mesh mesh, int subMeshIndex = 0)
		{
			this.mesh = mesh;
			this.subMeshIndex = subMeshIndex;
			localToWorldMatrix = global::UnityEngine.Matrix4x4.identity;
			mask = uint.MaxValue;
			instanceID = uint.MaxValue;
			enableTriangleCulling = true;
			frontTriangleCounterClockwise = false;
			opaqueGeometry = true;
		}
	}
}
