namespace UnityEngine.Rendering.RadeonRays
{
	internal struct MeshBuildInfo
	{
		public global::UnityEngine.GraphicsBuffer vertices;

		public int verticesStartOffset;

		public uint vertexCount;

		public uint vertexStride;

		public int baseVertex;

		public global::UnityEngine.GraphicsBuffer triangleIndices;

		public int indicesStartOffset;

		public int baseIndex;

		public global::UnityEngine.Rendering.RadeonRays.IndexFormat indexFormat;

		public uint triangleCount;
	}
}
