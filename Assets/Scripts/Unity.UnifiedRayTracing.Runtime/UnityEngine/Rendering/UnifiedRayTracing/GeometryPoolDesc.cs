namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal struct GeometryPoolDesc
	{
		public int vertexPoolByteSize;

		public int indexPoolByteSize;

		public int meshChunkTablesByteSize;

		public static global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolDesc NewDefault()
		{
			return new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolDesc
			{
				vertexPoolByteSize = 268435456,
				indexPoolByteSize = 33554432,
				meshChunkTablesByteSize = 4194304
			};
		}
	}
}
